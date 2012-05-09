using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.ProjectManagement;

public partial class CMSModules_ProjectManagement_Pages_Tools_Configuration_ProjectTaskPriority_Edit : CMSProjectManagementConfigurationPage
{
    #region "Methods"

    protected override void OnInit(EventArgs e)
    {
        // Get the ID from query string
        editElem.TaskPriorityID = QueryHelper.GetInteger("projecttaskpriorityid", 0);
        editElem.OnSaved += new EventHandler(editElem_OnSaved);
    
        base.OnInit(e);
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        PageTitle title = this.CurrentMaster.Title;
        title.HelpTopicName = "pm_projecttaskpriority_edit";
        
        // Prepare the header
        string name = "";
        string header = "";
        if (editElem.ProjecttaskpriorityObj != null)
        {
            name = editElem.ProjecttaskpriorityObj.TaskPriorityDisplayName;
            header = GetString("pm.projecttaskpriority.edit");
            title.TitleImage = GetImageUrl("Objects/PM_ProjectTaskPriority/object.png");
        }
        else
        {
            name = GetString("pm.projecttaskpriority.new");
            header = name;
            title.TitleImage = GetImageUrl("Objects/PM_ProjectTaskPriority/new.png");
        }

        // Prepare the breadcrumbs       
        string[,] breadcrumbs = new string[2, 3];
        breadcrumbs[0, 0] = GetString("pm.projecttaskpriority.list");
        breadcrumbs[0, 1] = "~/CMSModules/ProjectManagement/Pages/Tools//Configuration/ProjectTaskPriority/List.aspx";
        breadcrumbs[1, 0] = HTMLHelper.HTMLEncode(name);
        
        // Set the title
        title.Breadcrumbs = breadcrumbs;
        title.TitleText = header;
    }
    
    
    protected void editElem_OnSaved(object sender, EventArgs e)
    {
        URLHelper.Redirect("Edit.aspx?saved=1&projecttaskpriorityid=" + editElem.ItemID);
    }

    #endregion
}