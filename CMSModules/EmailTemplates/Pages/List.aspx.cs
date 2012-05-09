using System;
using System.Web.UI.WebControls;

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.UIControls;

public partial class CMSModules_EmailTemplates_Pages_List : CMSEmailTemplatesPage
{

    #region "Page events"

    protected override void OnPreInit(EventArgs e)
    {
        ((Panel)this.CurrentMaster.PanelBody.FindControl("pnlContent")).CssClass = string.Empty;
        base.OnPreInit(e);
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        // Setup page title text and image
        CurrentMaster.Title.TitleText = GetString("EmailTemplate_List.Title");
        CurrentMaster.Title.TitleImage = GetImageUrl("Objects/CMS_EmailTemplate/object.png");

        CurrentMaster.Title.HelpTopicName = "e_mail_templates_list";
        CurrentMaster.Title.HelpName = "helpTopic";

        // Control initialization
        lnkNewEmailTemplate.Text = GetString("EmailTemplate_List.NewTemplateButton");
        lnkNewEmailTemplate.NavigateUrl = "javascript: AddNewItem();";

        imgNewEmailTemplate.ImageUrl = GetImageUrl("Objects/CMS_EmailTemplate/add.png");
        imgNewEmailTemplate.AlternateText = GetString("EmailTemplate_List.NewTemplateButton");

        if (SiteID != 0)
        {
            pnlSites.Visible = false;
        }
        else
        {
            // Set site selector
            siteSelector.DropDownSingleSelect.AutoPostBack = true;
            siteSelector.AllowAll = false;
            siteSelector.AllowEmpty = false;
            siteSelector.OnlyRunningSites = false;
            siteSelector.UniSelector.OnSelectionChanged += new EventHandler(UniSelector_OnSelectionChanged);
            siteSelector.AllowSetSpecialFields = true;

            if (!RequestHelper.IsPostBack())
            {
                if (SelectedSiteID > 0)
                {
                    siteSelector.Value = SelectedSiteID;
                }
                else if (CMSContext.CurrentUser.UserSiteManagerAdmin)
                {
                    siteSelector.Value = siteSelector.GlobalRecordValue;
                    SelectedSiteID = ValidationHelper.GetInteger(siteSelector.GlobalRecordValue, UniSelector.US_GLOBAL_RECORD);
                }
            }
            else
            {
                SelectedSiteID = ValidationHelper.GetInteger(siteSelector.Value, 0);
            }
        }

        // Initialize email template list control
        emailTemplateListElem.SiteID = (SiteID > 0) ? SiteID : SelectedSiteID;
        emailTemplateListElem.OnCheckPermissions += emailTemplateListElem_OnCheckPermissions;
        emailTemplateListElem.OnEdit += emailTemplateListElem_OnEdit;
    }


    protected void Page_PreRender(object sender, EventArgs e)
    {
        // Register correct script for new item
        ScriptHelper.RegisterClientScriptBlock(this.Page, typeof(string), "AddNewItem", ScriptHelper.GetScript(
            "function AddNewItem() { this.window.location = '" + ResolveUrl("Edit.aspx?" + GetSiteOrSelectedSite() + "'} ")));

        // Register export script
        this.RegisterExportScript();
    }

    #endregion


    #region "Private methods"

    /// <summary>
    /// Returns 'siteid' or 'selectedsiteid' parametr depending on QueryString.
    /// </summary>
    /// <returns>Query parameter</returns>
    private string GetSiteOrSelectedSite()
    {
        // Site ID is used in CMS desk
        if (SiteID > 0)
        {
            return "siteid=" + SiteID;
        }
        // SelectedSiteID is used in CMS Site Manager
        else if (SelectedSiteID > 0)
        {
            return "selectedsiteid=" + SelectedSiteID;
        }

        return String.Empty;
    }

    #endregion


    #region "Control events"

    /// <summary>
    /// Handles site selection change event.
    /// </summary>
    protected void UniSelector_OnSelectionChanged(object sender, EventArgs e)
    {
        // Update unigrid
        pnlUpdate.Update();
    }


    /// <summary>
    /// Edit event handler.
    /// </summary>
    protected void emailTemplateListElem_OnEdit(object sender, EventArgs e)
    {
        URLHelper.Redirect("Edit.aspx?templateid=" + emailTemplateListElem.SelectedItemID + "&" + GetSiteOrSelectedSite());
    }


    /// <summary>
    /// On check permissions event handler.
    /// </summary>
    protected void emailTemplateListElem_OnCheckPermissions(string permissionType, CMSAdminControl sender)
    {
        if (!CMSContext.CurrentUser.IsAuthorizedPerResource("cms.emailtemplates", permissionType))
        {
            RedirectToCMSDeskAccessDenied("CMS.EmailTemplates", permissionType);
        }
    }

    #endregion

}
