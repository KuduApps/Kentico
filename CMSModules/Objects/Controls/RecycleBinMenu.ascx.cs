using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.ExtendedControls;
using CMS.GlobalHelper;
using CMS.CMSHelper;

public partial class CMSModules_Objects_Controls_RecycleBinMenu : CMSContextMenuControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Main menu       
        string menuId = ContextMenu.MenuID;
        string parentElemId = ContextMenu.ParentElementClientID;

        string actionPattern = "return ContextBinAction_" + parentElemId + "('{0}', GetContextMenuParameter('" + menuId + "'));";
        string confPattern = "if(confirm('" + ResHelper.GetString("objectversioning.recyclebin.confirmrestore") + "')) {0} return false;";

        imgRestoreBindings.ImageUrl = UIHelper.GetImageUrl(Page, "Design/Controls/UniGrid/Actions/undo.png");
        pnlRestoreBindings.Attributes.Add("onclick", string.Format(confPattern, string.Format(actionPattern, "restorewithoutbindings")));

        // Display restore to current site only if current site available
        CurrentSiteInfo si = CMSContext.CurrentSite;
        if (si != null)
        {
            imgRestoreCurrent.ImageUrl = UIHelper.GetImageUrl(Page, "CMSModules/CMS_RecycleBin/restorecurrentsite.png");
            pnlRestoreCurrent.Attributes.Add("onclick", string.Format(confPattern, string.Format(actionPattern, "restorecurrentsite")));
            lblRestoreCurrent.Text = String.Format(ResHelper.GetString("objectversioning.recyclebin.restoretocurrentsite"), HTMLHelper.HTMLEncode(si.DisplayName));
        }
        else
        {
            pnlRestoreCurrent.Visible = false;
        }
    }
}