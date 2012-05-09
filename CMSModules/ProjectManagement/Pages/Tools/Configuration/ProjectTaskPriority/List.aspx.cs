using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.ProjectManagement;

public partial class CMSModules_ProjectManagement_Pages_Tools_Configuration_ProjectTaskPriority_List : CMSProjectManagementConfigurationPage
{
    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Prepare the actions
        string[,] actions = new string[1, 6];
        actions[0, 0] = HeaderActions.TYPE_HYPERLINK;
        actions[0, 1] = GetString("pm.projecttaskpriority.new");        
        actions[0, 3] = ResolveUrl("Edit.aspx");       
        actions[0, 5] = GetImageUrl("Objects/PM_ProjectTaskPriority/add.png");

        // Set the actions
        ICMSMasterPage master = this.CurrentMaster;        
        master.HeaderActions.Actions = actions;

        // Set the title
        PageTitle title = master.Title;
        title.TitleText = GetString("pm.projecttaskpriority.list");
        title.TitleImage = GetImageUrl("Objects/PM_ProjectTaskPriority/object.png");
        title.HelpTopicName = "pm_projecttaskpriority_list";
        title.HelpName = "helpTopic";
    }

    #endregion
}
