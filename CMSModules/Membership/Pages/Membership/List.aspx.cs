using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.UIControls;

public partial class CMSModules_Membership_Pages_Membership_List : CMSMembershipPage
{
    #region "Methods"

    protected override void OnPreInit(EventArgs e)
    {
        ((Panel)this.CurrentMaster.PanelBody.FindControl("pnlContent")).CssClass = "";
        base.OnPreInit(e);
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        siteSelector.DropDownSingleSelect.AutoPostBack = true;
        siteSelector.UniSelector.OnSelectionChanged += new EventHandler(DropDownSingleSelect_OnSelectionChanged);
        siteSelector.AllowGlobal = true;
        siteSelector.AllowAll = false;
        siteSelector.AllowSetSpecialFields = true;

        // Check site manager or cms desk
        if (SiteID != 0)
        {
            pnlSite.Visible = false;
            listElem.SiteID = SiteID;
            listElem.SiteQueryUrl = "&SiteID=" + SiteID;
        }
        else
        {
            if (!RequestHelper.IsPostBack())
            {
                siteSelector.Value = (SelectedSiteID != 0) ? SelectedSiteID.ToString() : siteSelector.GlobalRecordValue;
            }

            if (ValidationHelper.GetString(siteSelector.Value, String.Empty) != siteSelector.GlobalRecordValue)
            {
                listElem.SiteID = siteSelector.SiteID;
                SelectedSiteID = siteSelector.SiteID;
                listElem.SiteQueryUrl = "&SelectedSiteID=" + SelectedSiteID;
            }
            else
            {
                // (global) picked - set selectedID as 0 (used in new link generation)
                SelectedSiteID = 0;
            }
        }

        // New membership initialization
        lnkNewMembership.Text = GetString("membership.membership.new");
        lnkNewMembership.NavigateUrl = "javascript: AddNewItem();";

        imgNewMembership.ImageUrl = GetImageUrl("Objects/CMS_Membership/add.png");
        imgNewMembership.AlternateText = GetString("membership.membership.new");
        
        // Set the actions
        ICMSMasterPage master = this.CurrentMaster;

        // Set the title
        PageTitle title = master.Title;
        title.TitleText = GetString("membership.membership.list");
        title.TitleImage = GetImageUrl("Objects/CMS_Membership/object.png");
        title.HelpTopicName = "membership_list";
        title.HelpName = "helpTopic";

        listElem.OnCheckPermissions += new CMSAdminControl.CheckPermissionsEventHandler(membershipListElem_OnCheckPermissions);

    }


    protected override void OnPreRender(EventArgs e)
    {
        ScriptHelper.RegisterClientScriptBlock(this.Page, typeof(string), "AddNewItem", ScriptHelper.GetScript(
            "function AddNewItem() { this.window.location = '" + "New.aspx" + GetSiteOrSelectedSite() + "'} "));

        base.OnPreRender(e);
    }


    protected void membershipListElem_OnCheckPermissions(string permissionType, CMSAdminControl sender)
    {
        if (!CMSContext.CurrentUser.IsAuthorizedPerResource("CMS.Membership", permissionType))
        {
            RedirectToCMSDeskAccessDenied("CMS.Membership", permissionType);
        }
    }


    /// <summary>
    /// Returns 'siteid' or 'selectedsiteid' parametr depending on QueryString.
    /// </summary>
    /// <returns>Query parameter</returns>
    private string GetSiteOrSelectedSite()
    {
        // Site ID is used in CMS desk
        if (SiteID > 0)
        {
            return "?siteId=" + SiteID;
        }
        // SelectedSiteID is used in CMS Site Manager
        else if (SelectedSiteID > 0)
        {
            return "?selectedsiteid=" + SelectedSiteID;
        }

        return String.Empty;
    }


    void DropDownSingleSelect_OnSelectionChanged(object sender, EventArgs e)
    {
        pnlUpdate.Update();
    }

    #endregion
}
