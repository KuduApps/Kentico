using System;

using CMS.GlobalHelper;
using CMS.ResourceManager;
using CMS.SettingsProvider;
using CMS.SiteProvider;
using CMS.Synchronization;
using CMS.UIControls;

public partial class CMSModules_UICultures_Pages_Development_ResourceString_Edit : SiteManagerPage
{
    #region "Variables"

    protected int stringID;


    protected int uiCultureID;


    private bool saved;


    protected bool showBack;


    protected string[,] tabs = new string[2, 3];


    protected UICultureInfo uic;    

    #endregion


    #region "Properties

    protected int BackCount
    {
        get
        {
            return ValidationHelper.GetInteger(ViewState["BackCount"], 1);
        }
        set
        {
            ViewState["BackCount"] = value;
        }
    }


    private bool DialogMode
    {
        get;
        set;
    }

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Get parameters from query string
        GetParameters();

        if (saved)
        {            
            lblInfo.Visible = true;
        }

        lblEnglishText.Text = string.Format(GetString("Administration-UICulture_String_New.EnglishText"), CultureHelper.DefaultUICulture);
        rfvKey.ErrorMessage = GetString("Administration-UICulture_String_New.EmptyKey");

        ResourceStringInfo ri = SqlResourceManager.GetResourceStringInfo(stringID, uiCultureID);
        EditedObject = ri;

        uic = UICultureInfoProvider.GetUICultureInfo(uiCultureID);
        if (uic.UICultureCode == CultureHelper.DefaultUICulture)
        {
            // Default culture
            plcDefaultText.Visible = false;
            txtKey.Visible = true;
            lblKeyEng.Visible = false;

            if (!RequestHelper.IsPostBack())
            {
                txtKey.Text = ri.StringKey;
                txtText.Text = SqlResourceManager.GetStringStrictly(ri.StringKey, CultureHelper.DefaultUICulture);
            }
        }
        else
        {
            // Other cultures
            plcDefaultText.Visible = true;
            txtKey.Visible = false;
            rfvKey.Enabled = false;
            lblKeyEng.Visible = true;

            lblKeyEng.Text = ri.StringKey;
            lblEnglishValue.Text = HTMLHelper.HTMLEncode(MacroResolver.RemoveSecurityParameters(SqlResourceManager.GetStringStrictly(ri.StringKey, CultureHelper.DefaultUICulture), true, null));

            if (!RequestHelper.IsPostBack())
            {
                txtKey.Text = ri.StringKey;
                txtText.Text = SqlResourceManager.GetStringStrictly(ri.StringKey, uic.UICultureCode);
            }
        }

        if (!DialogMode)
        {
            // Initialize master page
            InitializeMasterPage(ri, plcDefaultText.Visible);
        }
        else
        {
            txtKey.Enabled = false;
            plcCustom.Visible = false;
        }

        if (!RequestHelper.IsPostBack() && (!DialogMode))
        {
            chkCustomString.Checked = ri.StringIsCustom;
        }
    }


    /// <summary>
    /// Initializes Maste Page.
    /// </summary>
    protected void InitializeMasterPage(ResourceStringInfo ri, bool defaultTextVisible)
    {
        // Initializes page breadcrumbs
        tabs[0, 0] = GetString("UICultures_Strings.Strings");
        tabs[0, 1] = ResolveUrl("List.aspx?UIcultureID=" + uiCultureID);        
        tabs[1, 0] = ri.StringKey;        
        CurrentMaster.Title.Breadcrumbs = tabs;

        // Set actions
        string[,] actions = new string[1, 8];
        actions[0, 0] = "HyperLink";
        actions[0, 1] = GetString("Development-UICulture_Strings_List.NewString");
        actions[0, 3] = ResolveUrl("New.aspx?uicultureid=" + uiCultureID);
        actions[0, 5] = GetImageUrl("CMSModules/CMS_UICultures/addstring.png");

        CurrentMaster.HeaderActions.Actions = actions;

        CurrentMaster.Title.HelpName = "helpTopic";
        CurrentMaster.Title.HelpTopicName = defaultTextVisible ? "newedit_string" : "DefaultNewEdit_string";
    }


    protected void btnOK_Click(object sender, EventArgs e)
    {
        // History back count
        BackCount++;
        string result = null;

        // Trim the key before save
        string key = txtKey.Text.Trim();

        // Validate the code name if default culture
        if (uic.UICultureCode == CultureHelper.DefaultUICulture)
        {
            result = new Validator()
                .NotEmpty(key, rfvKey.ErrorMessage)
                .IsCodeName(key, GetString("Administration-UICulture_String_New.InvalidCodeName"))
                .Result;
        }

        if (!string.IsNullOrEmpty(result))
        {
            // Display error message
            lblError.Text = result;
            lblError.Visible = true;
            lblInfo.Visible = false;
            return;
        }

        // Update the string
        ResourceStringInfo ri = SqlResourceManager.GetResourceStringInfo(stringID, uiCultureID);
        if (ri != null)
        {
            // Check if string with given key is not already defined
            ResourceStringInfo existing = SqlResourceManager.GetResourceStringInfo(key);
            if ((existing == null) || (existing.StringId == ri.StringId))
            {
                ri.StringIsCustom = chkCustomString.Checked;
                ri.UICultureCode = uic.UICultureCode;
                ri.TranslationText = txtText.Text;

                if (txtKey.Visible)
                {
                    // If key changed, log deletion of old string
                    string newKey = key;

                    if ((!string.Equals(ri.StringKey, newKey, StringComparison.OrdinalIgnoreCase)) && 
                        (ri.Generalized.LogSynchronization == SynchronizationTypeEnum.LogSynchronization))                    
                    {
                        SynchronizationHelper.LogObjectChange(ri, TaskTypeEnum.DeleteObject);
                    }

                    ri.StringKey = key;
                }

                // Update key
                SqlResourceManager.SetResourceStringInfo(ri);

                lblInfo.Visible = true;
                lblError.Visible = false;

                tabs[1, 0] = ri.StringKey;
            }
            else
            {
                lblError.Text = string.Format(GetString("Administration-UICulture_String_New.StringExists"), key);
                lblError.Visible = true;
                lblInfo.Visible = false;
            }
        }
    }


    private void GetParameters()
    {
        stringID = QueryHelper.GetInteger("stringID", 0);
        uiCultureID = QueryHelper.GetInteger("uicultureID", 0);
        DialogMode = QueryHelper.GetBoolean("modal", false);
        saved = QueryHelper.GetBoolean("saved", false);
    }

    #endregion
}