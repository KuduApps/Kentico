using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.UIControls;
using CMS.Controls;
using CMS.GlobalHelper;
using CMS.ExtendedControls;
using CMS.SettingsProvider;
using CMS.CMSHelper;
using CMS.SiteProvider;

public partial class CMSAdminControls_UI_UniSelector_Controls_SelectorMenu : CMSContextMenuControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.iRemoveAll.ImageUrl = UIHelper.GetImageUrl(this.Page, "Design/Controls/UniGrid/Actions/Delete.png");
        this.iRemoveAll.Text = ResHelper.GetString("General.RemoveAll");
        this.iRemoveAll.Attributes.Add("onclick", "US_ContextRemoveAll(GetContextMenuParameter('" + this.ContextMenu.MenuID + "'));");

        if (!IsLiveSite)
        {
            pnlSelectorMenu.CssClass = "PortalContextMenu WebPartContextMenu";
        }
    }
}
