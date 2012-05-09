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
using CMS.SettingsProvider;
using CMS.DataEngine;
using CMS.UIControls;

public partial class CMSModules_SystemTables_Pages_Development_Views_Views_List : SiteManagerPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Prepare the new object header element
        string[,] actions = new string[2, 7];
        actions[0, 0] = HeaderActions.TYPE_HYPERLINK;
        string query = "?new=0";
        query += "&hash=" + QueryHelper.GetHash(query);
        actions[0, 3] = "~/CMSModules/SystemTables/Pages/Development/Views/View_Edit.aspx" + query;
        actions[0, 1] = ResHelper.GetString("sysdev.views.createview");
        actions[0, 5] = GetImageUrl("/Objects/CMS_SystemTable/view.png");

        actions[1, 0] = HeaderActions.TYPE_LINKBUTTON;
        actions[1, 1] = ResHelper.GetString("sysdev.views.refreshviews");
        actions[1, 5] = GetImageUrl("/Objects/CMS_SystemTable/refresh.png");
        actions[1, 6] = "refreshallviews";

        this.CurrentMaster.HeaderActions.Actions = actions;
        this.CurrentMaster.HeaderActions.ActionPerformed += new CommandEventHandler(HeaderActions_ActionPerformed);

        lstSQL.Views = true;
        lstSQL.EditPage = "ViewEdit_Frameset.aspx";
    }


    void HeaderActions_ActionPerformed(object sender, CommandEventArgs e)
    {
        if (e.CommandName == "refreshallviews")
        {
            lstSQL.RefresViews();
            lblInfo.Visible = true;
            lblInfo.Text = GetString("systbl.views.allviewsrefreshed");
        }
    }
}
