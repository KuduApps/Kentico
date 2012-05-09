using System;
using System.Data;

using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.CMSHelper;
using CMS.SiteProvider;
using CMS.Scheduler;
using CMS.SettingsProvider;
using System.Collections;


[Title(Text = "Scheduled tasks", ImageUrl = "Objects/CMS_ScheduledTask/object.png")]
public partial class CMSAPIExamples_Code_Administration_ScheduledTasks_Default : CMSAPIExamplePage
{
    #region "Initialization"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Scheduled task
        this.apiCreateScheduledTask.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(CreateScheduledTask);
        this.apiGetAndUpdateScheduledTask.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(GetAndUpdateScheduledTask);
        this.apiGetAndBulkUpdateScheduledTasks.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(GetAndBulkUpdateScheduledTasks);
        this.apiDeleteScheduledTask.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(DeleteScheduledTask);
        this.apiRunTask.RunExample += new CMSAPIExamples_Controls_APIExample.OnRunExample(RunTask);
    }

    #endregion


    #region "Mass actions"

    /// <summary>
    /// Runs all creating and managing examples.
    /// </summary>
    public override void RunAll()
    {
        base.RunAll();

        // Scheduled task
        this.apiCreateScheduledTask.Run();
        this.apiGetAndUpdateScheduledTask.Run();
        this.apiGetAndBulkUpdateScheduledTasks.Run();
        this.apiRunTask.Run();
    }


    /// <summary>
    /// Runs all cleanup examples.
    /// </summary>
    public override void CleanUpAll()
    {
        base.CleanUpAll();

        // Scheduled task
        this.apiDeleteScheduledTask.Run();
    }

    #endregion


    #region "API examples - Scheduled task"

    /// <summary>
    /// Creates scheduled task. Called when the "Create task" button is pressed.
    /// </summary>
    private bool CreateScheduledTask()
    {
        // Create new scheduled task object
        TaskInfo newTask = new TaskInfo();

        // Set the properties
        newTask.TaskDisplayName = "My new task";
        newTask.TaskName = "MyNewTask";
        newTask.TaskAssemblyName = "CMS.WorkflowEngine";
        newTask.TaskClass = "CMS.WorkflowEngine.ContentPublisher";

        // Create interval
        TaskInterval interval = new TaskInterval();

        // Set interval properties
        interval.Period = SchedulingHelper.PERIOD_DAY;
        interval.StartTime = DateTime.Now;
        interval.Every = 2;

        // Add some days to interval
        ArrayList days = new ArrayList();
        days.Add(DayOfWeek.Monday.ToString());
        days.Add(DayOfWeek.Sunday.ToString());
        days.Add(DayOfWeek.Thursday.ToString());

        interval.Days = days;

        newTask.TaskInterval = SchedulingHelper.EncodeInterval(interval);
        newTask.TaskSiteID = CMSContext.CurrentSiteID;
        newTask.TaskData = "<data></data>";
        newTask.TaskEnabled = true;
        newTask.TaskNextRunTime = SchedulingHelper.GetNextTime(newTask.TaskInterval, DateTime.Now, DateTime.Now);

        // Save the scheduled task
        TaskInfoProvider.SetTaskInfo(newTask);

        return true;
    }


    /// <summary>
    /// Gets and updates scheduled task. Called when the "Get and update task" button is pressed.
    /// Expects the CreateScheduledTask method to be run first.
    /// </summary>
    private bool GetAndUpdateScheduledTask()
    {
        // Get the scheduled task
        TaskInfo updateTask = TaskInfoProvider.GetTaskInfo("MyNewTask", CMSContext.CurrentSiteID);
        if (updateTask != null)
        {
            // Update the properties
            updateTask.TaskDisplayName = updateTask.TaskDisplayName.ToLower();

            // Save the changes
            TaskInfoProvider.SetTaskInfo(updateTask);

            return true;
        }

        return false;
    }


    /// <summary>
    /// Gets and bulk updates scheduled tasks. Called when the "Get and bulk update tasks" button is pressed.
    /// Expects the CreateScheduledTask method to be run first.
    /// </summary>
    private bool GetAndBulkUpdateScheduledTasks()
    {
        // Get the data
        DataSet tasks = TaskInfoProvider.GetAllTasks();
        if (!DataHelper.DataSourceIsEmpty(tasks))
        {
            // Loop through the individual items
            foreach (DataRow taskDr in tasks.Tables[0].Rows)
            {
                // Create object from DataRow
                TaskInfo modifyTask = new TaskInfo(taskDr);

                // Update the properties
                modifyTask.TaskDisplayName = modifyTask.TaskDisplayName.ToUpper();

                // Save the changes
                TaskInfoProvider.SetTaskInfo(modifyTask);
            }

            return true;
        }

        return false;
    }


    /// <summary>
    /// Deletes scheduled task. Called when the "Delete task" button is pressed.
    /// Expects the CreateScheduledTask method to be run first.
    /// </summary>
    private bool DeleteScheduledTask()
    {
        // Get the scheduled task
        TaskInfo deleteTask = TaskInfoProvider.GetTaskInfo("MyNewTask",CMSContext.CurrentSiteID);

        // Delete the scheduled task
        TaskInfoProvider.DeleteTaskInfo(deleteTask);

        return (deleteTask != null);
    }


    /// <summary>
    /// Runs scheduled task. Called when the "Run task" button is pressed.
    /// Expects the CreateScheduledTask method to be run first.
    /// </summary>
    private bool RunTask()
    {
        // Get the scheduled task
        TaskInfo runTask = TaskInfoProvider.GetTaskInfo("MyNewTask", CMSContext.CurrentSiteID);

        if (runTask != null)
        {
            // Run task
            SchedulingExecutor.ExecuteTask(runTask);

            return true;
        }

        return false;
    }


    #endregion
}
