using System;

using CMS.SiteProvider;
using CMS.GlobalHelper;
using CMS.UIControls;

public partial class CMSModules_UICultures_Pages_Development_UICulture_New : SiteManagerPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        rfvUICultureCode.ErrorMessage = GetString("UICultures_New.EmptyCultureCode");
        rfvUICultureName.ErrorMessage = GetString("UICultures_New.EmptyCultureName");

        InitializeMasterPage();
    }


    /// <summary>
    /// Initializes the master page elements.
    /// </summary>
    private void InitializeMasterPage()
    {
        string uiCultures = GetString("UICultures.UICultures");
        string currentUICulture = GetString("UICultures.NewUICulture");
        string UICulturesURL = ResolveUrl("List.aspx");

        // Initializes page breadcrumbs
        string[,] tabs = new string[2, 3];
        tabs[0, 0] = uiCultures;
        tabs[0, 1] = UICulturesURL;
        tabs[0, 2] = "";
        tabs[1, 0] = currentUICulture;
        tabs[1, 1] = "";
        tabs[1, 2] = "";

        this.CurrentMaster.Title.Breadcrumbs = tabs;
        
        // Set help icon
        this.CurrentMaster.Title.HelpTopicName = "new_culturegeneral_tab";
        this.CurrentMaster.Title.HelpName = "helpTopic";
    }


    protected void btnOK_Click(object sender, EventArgs e)
    {
        // Find whether required fields are not empty
        string result = new Validator()
            .NotEmpty(txtUICultureCode.Text, GetString("UICultures_New.EmptyCultureCode"))
            .NotEmpty(txtUICultureName.Text, GetString("UICultures_New.EmptyCultureName"))
            .Result;

        try
        {
            System.Globalization.CultureInfo obj = new System.Globalization.CultureInfo(txtUICultureCode.Text);
            if (obj != null)
            {
                // Neutral cultures are not allowed in UI cultures
                if (obj.IsNeutralCulture)
                {
                    result = GetString("uiculture.neutralculturecannotbeused");
                }
            }
            else
            {
                result = GetString("UICulture.ErrorNoGlobalCulture");
            }
        }
        catch
        {
            result = GetString("UICulture.ErrorNoGlobalCulture");
        }

        if (String.IsNullOrEmpty(result))
        {
            try
            {
                UICultureInfo ui = UICultureInfoProvider.GetUICultureInfo(txtUICultureCode.Text);
                lblError.Visible = true;
                lblError.Text = GetString("UICulture.UICultureAlreadyExist");
            }
            catch 
            {
                int uiCultureId = SaveNewUICulture();

                if (uiCultureId > 0)
                {
                    URLHelper.Redirect("Frameset.aspx?uicultureid=" + uiCultureId + "&update=1");
                }            
            }            
        }
        else
        {
            lblError.Visible = true;
            lblError.Text = result;
        }
    }


    protected int SaveNewUICulture()
    {
        UICultureInfo ui = new UICultureInfo();
        ui.UICultureCode = txtUICultureCode.Text;
        ui.UICultureName = txtUICultureName.Text;
        UICultureInfoProvider.SetUICultureInfo(ui);
        return ui.UICultureID;
    }
}
