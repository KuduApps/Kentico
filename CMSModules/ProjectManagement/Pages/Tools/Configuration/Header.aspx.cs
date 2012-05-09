using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.ProjectManagement;

public partial class CMSModules_ProjectManagement_Pages_Tools_Configuration_Header : CMSProjectManagementConfigurationPage
{
    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Prepare the tabs
        string[,] tabs = new string[3, 4];
        tabs[0, 0] = GetString("pm.projectstatus.list");
        tabs[0, 2] = "ProjectStatus/List.aspx";

        tabs[1, 0] = GetString("pm.projecttaskstatus.list");
        tabs[1, 2] = "ProjectTaskStatus/List.aspx";

        tabs[2, 0] = GetString("pm.projecttaskpriority.list");
        tabs[2, 2] = "ProjecttaskPriority/List.aspx";

        // Set the tabs
        ICMSMasterPage master = this.CurrentMaster;
        master.Tabs.Tabs = tabs;
        master.Tabs.UrlTarget = "content";
    }

    #endregion
}