using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.ProjectManagement;

public partial class CMSModules_Groups_Tools_ProjectManagement_Project_Edit : CMSGroupProjectManagementPage
{
    #region "Variables"

    int projectId = 0;
    int groupId = 0;

    #endregion


    #region "Methods"

    protected override void OnInit(EventArgs e)
    {
        // Get the ID from query string
        projectId = QueryHelper.GetInteger("projectid", 0);
        groupId = QueryHelper.GetInteger("groupid", 0);

        editElem.ProjectID = projectId;
        editElem.CommunityGroupID = groupId;
        editElem.OnSaved += new EventHandler(editElem_OnSaved);
        editElem.OnCheckPermissions += new CMSAdminControl.CheckPermissionsEventHandler(editElem_OnCheckPermissions);

        base.OnInit(e);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        PageTitle title = this.CurrentMaster.Title;

        // Prepare the header
        string name = "";
        if (editElem.ProjectObj == null)
        {
            name = GetString("pm.project.new");
            title.TitleImage = GetImageUrl("Objects/PM_Project/new.png");
            title.HelpTopicName = "pm_project_new";

            // Prepare the breadcrumbs       
            string[,] breadcrumbs = new string[2, 3];
            breadcrumbs[0, 0] = GetString("pm.project.list");
            breadcrumbs[0, 1] = URLHelper.ResolveUrl("~/CMSModules/Groups/Tools/ProjectManagement/Project/List.aspx?projectid=" + projectId + "&groupid=" + groupId);
            breadcrumbs[1, 0] = HTMLHelper.HTMLEncode(name);

            // Set the title
            title.Breadcrumbs = breadcrumbs;
        }
    }


    protected void editElem_OnSaved(object sender, EventArgs e)
    {
        if (projectId > 0)
        {
            // Existing item
            ScriptHelper.RefreshTabHeader(Page, GetString("general.general"));
        }
        else
        {
            // New item
            URLHelper.Redirect("~/CMSModules/Groups/Tools/ProjectManagement/Project/Frameset.aspx?projectid=" + editElem.ItemID + "&groupid=" + groupId);
        }
    }


    /// <summary>
    /// Check permissions handler.
    /// </summary>
    /// <param name="permissionType">Type of a permission to check</param>
    /// <param name="sender">Sender</param>
    protected void editElem_OnCheckPermissions(string permissionType, CMSAdminControl sender)
    {
        // Check permissions
        CheckPermissions(groupId, permissionType);
    }

    #endregion
}