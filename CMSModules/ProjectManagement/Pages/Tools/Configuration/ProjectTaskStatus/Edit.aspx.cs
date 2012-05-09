using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.ProjectManagement;

public partial class CMSModules_ProjectManagement_Pages_Tools_Configuration_ProjectTaskStatus_Edit : CMSProjectManagementConfigurationPage
{
    #region "Methods"

    protected override void OnInit(EventArgs e)
    {
        // Get the ID from query string
        editElem.TaskStatusID = QueryHelper.GetInteger("projecttaskstatusid", 0);
        editElem.OnSaved += new EventHandler(editElem_OnSaved);
    
        base.OnInit(e);
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        PageTitle title = this.CurrentMaster.Title;
        title.HelpTopicName = "pm_projecttaskstatus_edit";
        
        // Prepare the header
        string name = "";
        string header = "";
        if (editElem.ProjecttaskstatusObj != null)
        {
            name = editElem.ProjecttaskstatusObj.TaskStatusDisplayName;
            header = GetString("pm.projecttaskstatus.properties");
            title.TitleImage = GetImageUrl("Objects/PM_ProjectTaskStatus/object.png");
        }
        else
        {
            name = GetString("pm.projecttaskstatus.new");
            header = name;
            title.TitleImage = GetImageUrl("Objects/PM_ProjectTaskStatus/new.png");
        }

        // Prepare the breadcrumbs       
        string[,] breadcrumbs = new string[2, 3];
        breadcrumbs[0, 0] = GetString("pm.projecttaskstatus.list");
        breadcrumbs[0, 1] = "~/CMSModules/ProjectManagement/Pages/Tools//Configuration/ProjectTaskStatus/List.aspx";
        breadcrumbs[1, 0] = HTMLHelper.HTMLEncode(name);
        
        // Set the title
        title.Breadcrumbs = breadcrumbs;
        title.TitleText = header;
    }
    
    
    protected void editElem_OnSaved(object sender, EventArgs e)
    {
        URLHelper.Redirect("Edit.aspx?saved=1&projecttaskstatusid=" + editElem.ItemID);
    }

    #endregion
}