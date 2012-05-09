using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.UIControls;
using CMS.Controls;
using CMS.GlobalHelper;
using CMS.ExtendedControls;

public partial class CMSModules_Messaging_Controls_MessageMenu : CMSContextMenuControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string menuId = ContextMenu.MenuID;
        string parentElemId = ContextMenu.ParentElementClientID;

        string actionPattern = "ContextMessageAction_" + parentElemId + "('{0}', GetContextMenuParameter('" + menuId + "'));";

        // Main menu
        imgReply.ImageUrl = UIHelper.GetImageUrl(Page, "Design/Controls/UniGrid/Actions/reply.png");
        lblReply.Text = ResHelper.GetString("messaging.reply");
        pnlReply.Attributes.Add("onclick", string.Format(actionPattern, "reply"));

        imgForward.ImageUrl = UIHelper.GetImageUrl(Page, "Design/Controls/UniGrid/Actions/forward.png");
        lblForward.Text = ResHelper.GetString("messaging.forward");
        pnlForward.Attributes.Add("onclick", string.Format(actionPattern, "forward"));

        imgMarkRead.ImageUrl = UIHelper.GetImageUrl(Page, "Design/Controls/UniGrid/Actions/markread.png");
        lblMarkRead.Text = ResHelper.GetString("Messaging.Action.MarkAsRead");
        pnlMarkRead.Attributes.Add("onclick", string.Format(actionPattern, "markread"));

        imgMarkUnread.ImageUrl = UIHelper.GetImageUrl(Page, "Design/Controls/UniGrid/Actions/markunread.png");
        lblMarkUnread.Text = ResHelper.GetString("Messaging.Action.MarkAsUnread");
        pnlMarkUnread.Attributes.Add("onclick", string.Format(actionPattern, "markunread"));
    }
}
