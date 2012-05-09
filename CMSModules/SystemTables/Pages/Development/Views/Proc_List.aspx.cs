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

public partial class CMSModules_SystemTables_Pages_Development_Views_Proc_List : SiteManagerPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Prepare the new object header element
        string[,] actions = new string[1, 6];
        actions[0, 0] = HeaderActions.TYPE_HYPERLINK;
        string query = "?new=1";
        query += "&hash=" + QueryHelper.GetHash(query);
        actions[0, 3] = "~/CMSModules/SystemTables/Pages/Development/Views/Proc_Edit.aspx" + query;
        actions[0, 1] = ResHelper.GetString("sysdev.procedures.createprocedure");
        actions[0, 5] = GetImageUrl("/Objects/CMS_SystemTable/procedure.png");

        this.CurrentMaster.HeaderActions.Actions = actions;
        lstSQL.Views = false;
        lstSQL.EditPage = "Proc_Edit.aspx";
    }
}
