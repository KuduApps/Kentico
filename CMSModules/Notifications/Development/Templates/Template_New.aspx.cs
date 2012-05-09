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
using CMS.UIControls;

public partial class CMSModules_Notifications_Development_Templates_Template_New : CMSNotificationsPage
{
    protected void Page_Load(object sender, EventArgs e)
    {        
        int siteId = QueryHelper.GetInteger("siteid", 0);
        templateEditElem.SiteID = siteId;

        // Set up page title control
        // initializes breadcrumbs 		
        string[,] pageTitleTabs = new string[2, 3];
        pageTitleTabs[0, 0] = GetString("notifications.templates.title");
        pageTitleTabs[0, 1] = "~/CMSModules/Notifications/Development/Templates/Template_List.aspx" + ((siteId > 0) ? "?siteid=" + siteId : "");
        pageTitleTabs[0, 2] = "";
        pageTitleTabs[1, 0] = GetString("notifications.templates.newitem");
        pageTitleTabs[1, 1] = "";
        pageTitleTabs[1, 2] = "";
        this.CurrentMaster.Title.Breadcrumbs = pageTitleTabs;
    }
}
