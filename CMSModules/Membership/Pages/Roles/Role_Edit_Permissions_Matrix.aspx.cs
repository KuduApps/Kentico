using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.DataEngine;
using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.SiteProvider;
using CMS.UIControls;
using CMS.SettingsProvider;

public partial class CMSModules_Membership_Pages_Roles_Role_Edit_Permissions_Matrix : CMSRolesPage
{
    #region "Page Events"

    protected override void OnPreInit(EventArgs e)
    {
        ((Panel)this.CurrentMaster.PanelBody.FindControl("pnlContent")).CssClass = "";
        base.OnPreInit(e);
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        // Check "read" permission
        if (!CMSContext.CurrentUser.IsAuthorizedPerResource("CMS.Permissions", "Read"))
        {
            RedirectToAccessDenied("CMS.Permissions", "Read");
        }

        this.prmMatrix.SelectedID = QueryHelper.GetString("id", string.Empty);
        if (this.prmMatrix.SelectedID != "0")
        {
            this.prmMatrix.SelectedType = QueryHelper.GetString("type", string.Empty);
        }

        this.prmMatrix.GlobalRoles = ((SiteID <= 0) && CMSContext.CurrentUser.UserSiteManagerAdmin); 
        this.prmMatrix.SiteID = SiteID;
        this.prmMatrix.RoleID = QueryHelper.GetInteger("roleid", 0);
        this.prmMatrix.OnDataLoaded += new CMSModules_Permissions_Controls_PermissionsMatrix.OnMatrixDataLoaded(prmMatrix_DataLoaded);
        this.prmMatrix.CornerText = GetString("Administration.Roles.Permission");
    }

    #endregion


    #region "Event handlers"

    /// <summary>
    /// Replaces text in column header with localized string.
    /// </summary>
    /// <param name="ds"></param>
    protected void prmMatrix_DataLoaded(DataSet ds)
    {
        if (!DataHelper.DataSourceIsEmpty(ds))
        {
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                dr["RoleDisplayName"] = GetString("Administration.Roles.AllowPermission");
            }
        }
    }

    #endregion
}
