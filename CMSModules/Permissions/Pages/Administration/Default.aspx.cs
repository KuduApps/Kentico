using System;
using System.Web.UI.WebControls;

using CMS.LicenseProvider;
using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.SettingsProvider;

public partial class CMSModules_Permissions_Pages_Administration_Default : CMSPermissionsPage
{
    #region "Page Events"

    protected override void OnPreInit(EventArgs e)
    {
        ((Panel)this.CurrentMaster.PanelBody.FindControl("pnlContent")).CssClass = "";

        // Set site id for the control
        this.prmhdrHeader.SiteID = SiteID;

        base.OnPreInit(e);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        // Setup page title text and image
        CurrentMaster.Title.TitleText = GetString("Administration-Permissions_Header.PermissionsTitle");
        CurrentMaster.Title.TitleImage = GetImageUrl("Objects/CMS_Permission/object.png");

        CurrentMaster.Title.HelpTopicName = "permissions_list";
        CurrentMaster.Title.HelpName = "helpTopic";

        // First inicialization for callback actions
        InitializeMatrix();      
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        // Second initialization for cases when values in filter changed 
        InitializeMatrix();
    }

    #endregion


    #region "Private methods"

    /// <summary>
    /// Initialize permission matrix control.
    /// </summary>
    private void InitializeMatrix()
    {
        if (this.prmhdrHeader.HasSites)
        {
            // If global roles selected - Set sideID to zero
            int siteID = (SiteID > 0) ? SiteID : prmhdrHeader.SelectedSiteID;

            if (siteID == ValidationHelper.GetInteger(this.prmhdrHeader.GlobalRecordValue, 0))
            {
                siteID = 0;
            }

            prmMatrix.SelectedID = prmhdrHeader.SelectedID;
            prmMatrix.CornerText = GetString("Administration.Permissions.Role");
            if (prmMatrix.SelectedID != "0")
            {
                prmMatrix.SelectedType = prmhdrHeader.SelectedType;
            }
            prmMatrix.SiteID = siteID;
            prmMatrix.GlobalRoles = (siteID == 0) && CMSContext.CurrentUser.UserSiteManagerAdmin;
            prmMatrix.SelectedUserID = prmhdrHeader.SelectedUserID;
            prmMatrix.UserRolesOnly = prmhdrHeader.UserRolesOnly;

            prmMatrix.FilterChanged = prmhdrHeader.FilterChanged;
        }
    }

    #endregion
}
