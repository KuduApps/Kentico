using System;
using System.Web.UI.WebControls;

using CMS.GlobalHelper;
using CMS.SettingsProvider;
using CMS.UIControls;
using CMS.CMSHelper;

// Set edited object
[EditedObject("cms.query", "queryid", "CustomTable_Edit_Query_Frameset.aspx")]

public partial class CMSModules_CustomTables_CustomTable_Edit_Query_Edit : CMSCustomTablesPage
{
    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Check permissions - user must have the permission to edit the code
        if (!CMSContext.CurrentUser.IsAuthorizedPerResource("CMS.Design", "EditSQLCode"))
        {
            RedirectToCMSDeskAccessDenied("CMS.Design", "EditSQLCode");
        }

        Title = "Custom table edit - Query";

        // Get the current class ID
        int classId = QueryHelper.GetInteger("customtableid", 0);

        Query query = EditedObject as Query;
        
        queryEdit.IsSiteManager = true;

        // If the existing query is being edited
        if (query != null)
        {
            queryEdit.QueryID = query.QueryId;

            InitBreadcrumb(query.QueryName, query.QueryClassId);
            InitHeaderActions();
            queryEdit.EditMode = true;
        }
        else
        {
            queryEdit.QueryID = 0;
            InitBreadcrumb("customtable.edit.newquery", classId);
        }

        // If the new query is being created        
        if (classId > 0)
        {
            queryEdit.ClassID = classId;
        }

        queryEdit.RefreshPageURL = "~/CMSModules/CustomTables/CustomTable_Edit_Query_Edit.aspx";

        if (TabMode && (QueryHelper.GetInteger("saved", 0) == 1))
        {
            // Reload header if changes were saved
            ScriptHelper.RefreshTabHeader(Page, null);
        }
    }


    /// <summary>
    /// Sets the page breadcrumb control.
    /// </summary>
    /// <param name="caption">Caption of the breadcrumb item displayed in the page title as resource string key</param>
    /// <param name="classId">ID of a class</param>
    private void InitBreadcrumb(string caption, int classId)
    {
        // Initialize the breadcrumb settings
        string[,] breadCrumbInfo = new string[2, 3];

        // Add the base item
        breadCrumbInfo[0, 0] = GetString("general.queries");

        string backUrl = "~/CMSModules/CustomTables/CustomTable_Edit_Query_List.aspx";

        if (classId > 0)
        {
            backUrl += "?customtableid=" + classId;
        }

        breadCrumbInfo[0, 1] = backUrl;
        breadCrumbInfo[0, 2] = "";

        // Add the custom item
        breadCrumbInfo[1, 0] = GetString(caption);
        breadCrumbInfo[1, 1] = "";
        breadCrumbInfo[1, 2] = "";

        // Send the breadcrumb settings to the master page
        if (CurrentMaster != null)
        {
            CurrentMaster.Title.Breadcrumbs = breadCrumbInfo;
        }
    }


    private void InitHeaderActions()
    {
        string[,] actions = new string[1, 12];

        actions[0, 0] = HeaderActions.TYPE_SAVEBUTTON;
        actions[0, 1] = GetString("General.Save");
        actions[0, 5] = GetImageUrl("CMSModules/CMS_Content/EditMenu/save.png");
        actions[0, 6] = "save";
        actions[0, 8] = "true";

        this.CurrentMaster.HeaderActions.LinkCssClass = "ContentSaveLinkButton";
        this.CurrentMaster.HeaderActions.ActionPerformed += HeaderActions_ActionPerformed;
        this.CurrentMaster.HeaderActions.Actions = actions;
    }


    private void HeaderActions_ActionPerformed(object sender, CommandEventArgs e)
    {
        if (e.CommandName == "save")
        {
            queryEdit.Save(false);
        }
    }

    #endregion
}