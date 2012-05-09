using System;
using System.Data;

using CMS.GlobalHelper;
using CMS.Synchronization;
using CMS.UIControls;
using CMS.SettingsProvider;

public partial class CMSModules_ImportExport_SiteManager_ExportHistory_ExportHistory_Edit_Tasks : CMSImportExportPage
{
    #region "Private variables"

    private const string columns = "TaskID, TaskSiteID, TaskTitle, TaskTime, TaskType";

    #endregion


    #region "Page & control events"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Set unigrid properties
        UniGrid.OnAction += uniGrid_OnAction;
        UniGrid.OnDataReload += UniGrid_OnDataReload;
        UniGrid.Columns = columns;
        UniGrid.OrderBy = "TaskTime DESC";
        UniGrid.ShowActionsMenu = true;
        ExportTaskInfo eti = new ExportTaskInfo();
        UniGrid.AllColumns = SqlHelperClass.MergeColumns(eti.ColumnNames.ToArray());


        // Set master page properties
        CurrentMaster.Title.TitleText = GetString("ExportHistory.Tasks");
        CurrentMaster.Title.HelpName = "helpTopic";
        CurrentMaster.Title.HelpTopicName = "exporthistory_tasks_tab";
        CurrentMaster.Title.TitleImage = GetImageUrl("Objects/Export_Task/object.png");
        CurrentMaster.DisplaySiteSelectorPanel = true;

        // Initialize javascripts
        ScriptHelper.RegisterDialogScript(this);
        lnkDeleteAll.Attributes.Add("onclick", "return confirm(" + ScriptHelper.GetString(GetString("tasks.confirmdeleteall")) + ");");

        // Load sites list
        LoadSites();
    }


    protected DataSet UniGrid_OnDataReload(string completeWhere, string currentOrder, int currentTopN, string columns, int currentOffset, int currentPageSize, ref int totalRecords)
    {
        // Where condition
        string where = null;

        int siteId = ValidationHelper.GetInteger(siteSelector.Value, 0);

        // Site dropdownlist
        if (siteId != 0)
        {
            where = "TaskSiteID = " + siteId;
        }
        else
        {
            where = "TaskSiteID IS NULL";
        }

        where = SqlHelperClass.AddWhereCondition(where, completeWhere);

        // Get the data
        DataSet resultSet = ExportTaskInfoProvider.SelectTaskList(siteId, "", where, currentOrder, currentTopN, columns, currentOffset, currentPageSize, ref totalRecords);
        // Set visibility of delete button
        lnkDeleteAll.Visible = (totalRecords > 0);
        return resultSet;
    }


    protected void lnkDeleteAll_Click(object sender, EventArgs e)
    {
        int siteId = ValidationHelper.GetInteger(siteSelector.Value, 0);
        ExportTaskInfoProvider.DeleteExportTasks(siteId);
        UniGrid.ReloadData();
        pnlUpdate.Update();
    }


    /// <summary>
    /// Handles site selection change event.
    /// </summary>
    protected void UniSelector_OnSelectionChanged(object sender, EventArgs e)
    {
        pnlUpdate.Update();
    }

    #endregion


    #region "UniGrid actions"

    /// <summary>
    /// Handles the UniGrid's OnAction event.
    /// </summary>
    /// <param name="actionName">Name of item (button) that throws event</param>
    /// <param name="actionArgument">ID (value of Primary key) of corresponding data row</param>
    protected void uniGrid_OnAction(string actionName, object actionArgument)
    {
        if (actionName == "delete")
        {
            // delete ExportHistoryTaskInfo object from database
            ExportTaskInfoProvider.DeleteExportTaskInfo(Convert.ToInt32(actionArgument));
        }
    }

    #endregion


    #region "Private methods"

    /// <summary>
    /// Load list of sites.
    /// </summary>
    private void LoadSites()
    {
        int siteId = QueryHelper.GetInteger("siteid", 0);

        // Set site selector
        siteSelector.DropDownSingleSelect.AutoPostBack = true;
        siteSelector.AllowAll = false;
        siteSelector.UniSelector.SpecialFields = new string[,] { { GetString("ExportConfiguration.NoSite"), "0" } };
        siteSelector.UniSelector.OnSelectionChanged += UniSelector_OnSelectionChanged;

        if (!RequestHelper.IsPostBack())
        {
            if (siteId != 0)
            {
                siteSelector.Value = siteId;
                siteSelector.Enabled = false;
            }
            else
            {
                siteSelector.Value = "0";
            }
        }
    }

    #endregion
}
