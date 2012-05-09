using System;

using CMS.GlobalHelper;
using CMS.SiteProvider;
using CMS.ResourceManager;
using CMS.SettingsProvider;
using CMS.UIControls;

public partial class CMSModules_UICultures_Pages_Development_ResourceString_New : SiteManagerPage
{
    #region "Variables"

    int uiCultureID = 0;
    UICultureInfo uic = null;

    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        // Get UI culture ID from query string
        uiCultureID = QueryHelper.GetInteger("UIcultureID", 0);

        lblEnglishText.Text = String.Format(GetString("Administration-UICulture_String_New.EnglishText"), CultureHelper.DefaultUICulture);
        rfvKey.ErrorMessage = GetString("Administration-UICulture_String_New.EmptyKey");

        if (!RequestHelper.IsPostBack())
        {
            chkCustomString.Checked = ValidationHelper.GetBoolean(SettingsHelper.AppSettings["CMSCreateCustomStringsByDefault"], true);
        }

        // Get the culture info
        uic = UICultureInfoProvider.GetUICultureInfo(uiCultureID);

        // If default culture, do not show field for the default text
        plcDefaultText.Visible = !uic.UICultureCode.Equals(CultureHelper.DefaultUICulture, StringComparison.InvariantCultureIgnoreCase);

        InitializeMasterPage(plcDefaultText.Visible);
    }


    /// <summary>
    /// Initializes MasterPage.
    /// </summary>
    protected void InitializeMasterPage(bool defaultTextVisible)
    {
        // Initializes page title
        string[,] tabs = new string[2, 3];
        tabs[0, 0] = GetString("UICultures_Strings.Strings");
        tabs[0, 1] = ResolveUrl("List.aspx?uicultureid=" + uiCultureID);
        tabs[0, 2] = String.Empty;
        tabs[1, 0] = GetString("UICultures_Strings.NewString");
        tabs[1, 1] = String.Empty;
        tabs[1, 2] = String.Empty;
        this.CurrentMaster.Title.Breadcrumbs = tabs;

        this.CurrentMaster.Title.HelpName = "helpTopic";
        if (defaultTextVisible)
        {
            this.CurrentMaster.Title.HelpTopicName = "newedit_string";
        }
        else
        {
            this.CurrentMaster.Title.HelpTopicName = "DefaultNewEdit_string";
        }
    }


    protected void btnOK_Click(object sender, EventArgs e)
    {
        try
        {
            // Trim the key before save
            string key = txtKey.Text.Trim();
            // Validate the key
            string result = new Validator().NotEmpty(key, rfvKey.ErrorMessage).IsCodeName(key, GetString("Administration-UICulture_String_New.InvalidCodeName")).Result;

            if (String.IsNullOrEmpty(result))
            {
                // Check if resource string already exists with given code name
                ResourceStringInfo ri = SqlResourceManager.GetResourceStringInfo(key);
                if (ri == null)
                {
                    ri = new ResourceStringInfo();
                    ri.StringKey = key;
                    ri.StringIsCustom = chkCustomString.Checked;
                    ri.UICultureCode = uic.UICultureCode;
                    ri.TranslationText = txtText.Text;
                    SqlResourceManager.SetResourceStringInfo(ri);

                    // If text for default culture is set, store it
                    if (!string.IsNullOrEmpty(txtEnglishText.Text.Trim()))
                    {
                        // Set code of default UI culture
                        ri.UICultureCode = CultureHelper.DefaultUICulture;
                        ri.TranslationText = txtEnglishText.Text;
                        SqlResourceManager.SetResourceStringInfo(ri);
                    }

                    URLHelper.Redirect("Edit.aspx?uicultureid=" + uiCultureID + "&stringid=" + ri.StringId + "&saved=1&showBack=0");
                }
                else
                {
                    lblError.Text = String.Format(GetString("Administration-UICulture_String_New.StringExists"), key);
                    lblError.Visible = true;
                }
            }

            if (!String.IsNullOrEmpty(result))
            {
                lblError.Text = result;
                lblError.Visible = true;
            }
        }
        catch (Exception ex)
        {
            lblError.Text = GetString("general.erroroccurred") + ex.Message;
            lblError.Visible = true;
        }
    }
}
