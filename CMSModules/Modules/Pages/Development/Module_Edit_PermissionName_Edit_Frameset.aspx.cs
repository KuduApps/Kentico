using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.UIControls;
using CMS.GlobalHelper;

public partial class CMSModules_Modules_Pages_Development_Module_Edit_PermissionName_Edit_Frameset : SiteManagerPage
{
    #region "Variables"

    private int mModuleId;
    private int mPermissionId;
    private int mTabIndex;
    private int mSaved;

    #endregion


    #region "Page Events"

    protected void Page_Load(object sender, EventArgs e)
    {
        this.mModuleId = QueryHelper.GetInteger("moduleId", 0);
        this.mPermissionId = QueryHelper.GetInteger("permissionId", 0);
        this.mTabIndex = QueryHelper.GetInteger("tabindex", 0);
        this.mSaved = QueryHelper.GetInteger("saved", 0);

        if (this.mTabIndex == 0)
        {
            this.editContent.Attributes["src"] = ResolveUrl("~/CMSModules/Modules/Pages/Development/Module_Edit_PermissionName_Edit.aspx") + "?hidebreadcrumbs=1&moduleId=" + this.mModuleId + "&permissionId=" + this.mPermissionId + "&saved=" + this.mSaved + "&tabIndex=" + this.mTabIndex;
        }
        else
        {
            this.editContent.Attributes["src"] = ResolveUrl("~/CMSModules/Modules/Pages/Development/Module_Edit_PermissionName_Roles.aspx") + "?moduleID=" + this.mModuleId + "&permissionId=" + this.mPermissionId + "&saved=" + this.mSaved + "&tabIndex=" + this.mTabIndex;
        }
    }

    #endregion
}
