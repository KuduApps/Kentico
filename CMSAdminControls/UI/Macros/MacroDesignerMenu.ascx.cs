using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.UIControls;
using CMS.Controls;
using CMS.GlobalHelper;
using CMS.ExtendedControls;
using CMS.CMSHelper;
using CMS.SiteProvider;

public partial class CMSAdminControls_UI_Macros_MacroDesignerMenu : CMSContextMenuControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.itemMoveUp.ImageUrl = UIHelper.GetImageUrl(this.Page, "Design/Controls/UniGrid/Actions/Up.png");
        this.itemMoveUp.Text = ResHelper.GetString("General.MoveUp");
        this.itemMoveUp.Attributes.Add("onclick", "MoveDesignerGroup(GetContextMenuParameter('" + this.ContextMenu.MenuID + "').split('@')[0], '" + ResHelper.GetString("macrodesigner.cannotmoveup") + "');");

        this.itemMoveDown.ImageUrl = UIHelper.GetImageUrl(this.Page, "Design/Controls/UniGrid/Actions/Down.png");
        this.itemMoveDown.Text = ResHelper.GetString("General.MoveDown");
        this.itemMoveDown.Attributes.Add("onclick", "MoveDesignerGroup(GetContextMenuParameter('" + this.ContextMenu.MenuID + "').split('@')[1], '" + ResHelper.GetString("macrodesigner.cannotmovedown") + "');");

        this.itemMoveToParent.ImageUrl = UIHelper.GetImageUrl(this.Page, "Design/Controls/MacroDesigner/MoveToParent.png");
        this.itemMoveToParent.Text = ResHelper.GetString("macrodesigner.movetoparent");
        this.itemMoveToParent.Attributes.Add("onclick", "MoveDesignerGroup(GetContextMenuParameter('" + this.ContextMenu.MenuID + "').split('@')[2], '" + ResHelper.GetString("macrodesigner.cannotmovetoparent") + "');");
    }
}
