using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.SiteProvider;
using CMS.GlobalHelper;
using CMS.SettingsProvider;
using CMS.UIControls;
using CMS.CMSHelper;

public partial class CMSModules_Membership_Pages_Roles_Role_Edit_Permissions_Header : CMSRolesPage
{    
    #region "Page Events"

    protected override void OnPreInit(EventArgs e)
    {
        base.OnPreInit(e);
        ((Panel)this.CurrentMaster.PanelBody.FindControl("pnlContent")).CssClass = "";
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        // Get parameters
        this.prmhdrHeader.SiteID = SiteID;
        this.prmhdrHeader.RoleID = QueryHelper.GetInteger("roleid", 0);        
        this.prmhdrHeader.HideSiteSelector = ((SiteID == 0) && CMSContext.CurrentUser.UserSiteManagerAdmin);
        this.prmhdrHeader.ShowUserSelector = false;
    }


    protected void Page_PreRender(object sender, EventArgs e)
    {
        // Register script for load permission matrix to other frame
        string scriptText = ScriptHelper.GetScript(string.Format(@"parent.frames['content'].location = 'Role_Edit_Permissions_Matrix.aspx?roleid={0}&id={1}&type={2}&siteid={3}';",
            this.prmhdrHeader.RoleID, this.prmhdrHeader.SelectedID, this.prmhdrHeader.SelectedType, this.prmhdrHeader.SiteID));
        ScriptHelper.RegisterStartupScript(this.Page, typeof(string), "InitMatrix", scriptText);
    }

    #endregion
}