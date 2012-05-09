using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.UIControls;
using CMS.SiteProvider;
using CMS.ExtendedControls;

[Security(Resource = "CMS.Categories", Permission = "Read")]
public partial class CMSModules_Membership_Pages_Users_User_Edit_Categories : CMSUsersPage
{
    protected int userId = 0;

    protected override void OnPreInit(EventArgs e)
    {
        base.OnPreInit(e);
        this.CurrentMaster.PanelBody.CssClass = "";

        userId = QueryHelper.GetInteger("userid", 0);

        this.CategoriesElem.UserID = userId;
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        if (userId > 0)
        {
            // Check that only global administrator can edit global administrator's accouns
            UserInfo ui = UserInfoProvider.GetUserInfo(userId);
            CheckUserAvaibleOnSite(ui);
            EditedObject = ui;
        }

        Panel pnlContent = ControlsHelper.GetChildControl(Page, typeof(Panel), "pnlContent") as Panel;
        if (pnlContent != null)
        {
            pnlContent.CssClass = "";
        }
    }
}
