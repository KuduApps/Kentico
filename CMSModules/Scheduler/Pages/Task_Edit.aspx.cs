using System;

using CMS.GlobalHelper;
using CMS.SiteProvider;
using CMS.Scheduler;
using CMS.CMSHelper;
using CMS.WebFarmSync;
using CMS.SettingsProvider;
using CMS.UIControls;

public partial class CMSModules_Scheduler_Pages_Task_Edit : CMSScheduledTasksPage
{
    #region "Variables"

    protected int taskId;
    protected TaskInfo taskObj;
    protected SiteInfo si;
    protected bool cmsdesk;
    protected int siteID = 0;
    protected bool developmentMode = SettingsKeyProvider.DevelopmentMode;

    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        rfvAssembly.ErrorMessage = GetString("Task_Edit.EmptyAssembly");
        rfvDisplayName.ErrorMessage = GetString("general.requiresdisplayname");
        rfvName.ErrorMessage = GetString("Task_Edit.EmptyName");
        rfvClass.ErrorMessage = GetString("Task_Edit.EmptyClass");

        // Control initializations
        lblTaskAssemblyName.Text = GetString("Task_Edit.TaskAssemblyNameLabel");

        lblTaskClass.Text = GetString("Task_Edit.TaskClassLabel");
        lblTaskData.Text = GetString("Task_Edit.TaskDataLabel");
        lblTaskName.Text = GetString("Task_Edit.TaskNameLabel");
        lblTaskEnabled.Text = GetString("Task_Edit.TaskEnabledLabel");
        lblTaskInterval.Text = GetString("Task_Edit.TaskIntervalLabel");
        lblTaskDisplayName.Text = GetString("Task_Edit.TaskDisplayNameLabel");
        lblTaskDeleteAfterLastRun.Text = GetString("Task_Edit.TaskDeleteAfterLastRunLabel");
        lblServerName.Text = GetString("Task_Edit.TaskServerNameLabel");
        chkAllServers.Text = GetString("Task_Edit.TaskAllServers");
        
        plcDevelopment.Visible = developmentMode;
        // Show 'Allow run in external service' checkbox in development mode
        plcAllowExternalService.Visible = developmentMode;

        btnOk.Text = GetString("General.OK");

        string currentTask = GetString("Task_Edit.NewItemCaption");
        string title = GetString("Task_Edit.HeaderCaption");

        taskId = ValidationHelper.GetInteger(Request.QueryString["taskname"], 0);
        if (taskId > 0)
        {
            taskObj = TaskInfoProvider.GetTaskInfo(taskId);
            if (taskObj != null)
            {
                // Global task and user is not global administrator and task's site id is different than current site id
                if (!CMSContext.CurrentUser.UserSiteManagerAdmin && ((taskObj.TaskSiteID == 0) || (taskObj.TaskSiteID != SiteID)))
                {
                    RedirectToAccessDenied(GetString("general.nopermission"));
                }

                currentTask = taskObj.TaskDisplayName;
                title = GetString("Task_Edit.HeaderCaptionEdit");

                if (!RequestHelper.IsPostBack())
                {
                    ReloadData();

                    // Show that the task was created or updated successfully
                    if (Request.QueryString["saved"] == "1")
                    {
                        lblInfo.Visible = true;
                        lblInfo.Text = GetString("General.ChangesSaved");
                    }
                }
            }
        }
        else
        {
            // Check "modify" permission
            if (!CMSContext.CurrentUser.IsAuthorizedPerResource("CMS.ScheduledTasks", "Modify"))
            {
                RedirectToAccessDenied("CMS.ScheduledTasks", "Modify");
            }

            if (WebFarmServerInfoProvider.UseWebFarmSynchronization())
            {
                if (!RequestHelper.IsPostBack())
                {
                    chkAllServers.Visible = true;
                }
                chkAllServers.Attributes.Add("onclick", "document.getElementById('" + txtServerName.ClientID + "').disabled = document.getElementById('" + chkAllServers.ClientID + "').checked;");
            }
        }

        siteID = 0;
        if (SiteID != 0)
        {
            cmsdesk = true;
            siteID = SiteID;
        }

        if (SelectedSiteID != 0)
        {
            cmsdesk = false;
            if (Request.QueryString["selectedsiteid"] != "Global")
            {
                siteID = SelectedSiteID;
            }
        }

        // Get the site record
        if (siteID > 0)
        {
            si = SiteInfoProvider.GetSiteInfo(siteID);
        }

        // Initializes page title control		
        string[,] tabs = new string[2, 3];
        tabs[0, 0] = GetString("Task_Edit.ItemListLink");
        if (cmsdesk)
        {
            tabs[0, 1] = "~/CMSModules/Scheduler/Pages/Task_List.aspx?siteid=" + siteID;
        }
        else
        {
            tabs[0, 1] = "~/CMSModules/Scheduler/Pages/Task_List.aspx?selectedsiteid=" + siteID;
        }

        tabs[1, 0] = currentTask;
        tabs[1, 1] = "";
        tabs[1, 2] = "";
        this.CurrentMaster.Title.Breadcrumbs = tabs;

        // Setup page title text and image
        this.CurrentMaster.Title.TitleText = title;
        this.CurrentMaster.Title.TitleImage = GetImageUrl("Objects/CMS_ScheduledTask/new.png");
        this.CurrentMaster.Title.HelpTopicName = "newedit_task";
        this.CurrentMaster.Title.HelpName = "helpTopic";
    }


    /// <summary>
    /// Load data of editing task.
    /// </summary>
    protected void ReloadData()
    {
        if (taskObj != null)
        {
            txtTaskAssemblyName.Text = taskObj.TaskAssemblyName;
            txtTaskClass.Text = taskObj.TaskClass;
            txtTaskData.Text = taskObj.TaskData;
            txtTaskName.Text = taskObj.TaskName;
            chkTaskEnabled.Checked = taskObj.TaskEnabled;
            chkTaskDeleteAfterLastRun.Checked = taskObj.TaskDeleteAfterLastRun;
            ScheduleInterval1.ScheduleInterval = taskObj.TaskInterval;
            txtTaskDisplayName.Text = taskObj.TaskDisplayName;
            txtServerName.Text = taskObj.TaskServerName;
            chkRunTaskInSeparateThread.Checked = taskObj.TaskRunInSeparateThread;

            bool allowExternalService = taskObj.TaskAllowExternalService;
            chkTaskAllowExternalService.Checked = allowExternalService;
            chkTaskUseExternalService.Checked = taskObj.TaskUseExternalService;

            // Show 'Use external service' if task can run in external service
            plcUseExternalService.Visible = developmentMode || allowExternalService;

            drpModule.Value = taskObj.TaskResourceID;
        }
    }


    /// <summary>
    /// Sets data to database.
    /// </summary>
    protected void btnOK_Click(object sender, EventArgs e)
    {
        // Check "modify" permission
        if (!CMSContext.CurrentUser.IsAuthorizedPerResource("CMS.ScheduledTasks", "Modify"))
        {
            RedirectToAccessDenied("CMS.ScheduledTasks", "Modify");
        }

        // Check required fields format
        string errorMessage = new Validator().NotEmpty(txtTaskDisplayName.Text, rfvDisplayName.ErrorMessage).NotEmpty(txtTaskName.Text, rfvName.ErrorMessage).NotEmpty(txtTaskAssemblyName.Text, rfvAssembly.ErrorMessage).NotEmpty(txtTaskClass.Text, rfvClass.ErrorMessage).
            IsCodeName(txtTaskName.Text, GetString("Task_Edit.InvalidTaskName")).
            IsCodeName(txtTaskAssemblyName.Text, GetString("Task_Edit.InvalidTaskAssemblyName")).
            IsCodeName(txtTaskClass.Text, GetString("Task_Edit.InvalidTaskClass"))
            .Result;

        if ((errorMessage == String.Empty) && !ScheduleInterval1.CheckIntervalPreceedings())
        {
            errorMessage = GetString("Task_Edit.BetweenIntervalPreceedingsError");
        }

        if ((errorMessage == String.Empty) && !ScheduleInterval1.CheckOneDayMinimum())
        {
            errorMessage = GetString("Task_Edit.AtLeastOneDayError");
        }

        // Checking date/time limit (SQL limit)
        if (errorMessage == String.Empty)
        {
            TaskInterval ti = SchedulingHelper.DecodeInterval(ScheduleInterval1.ScheduleInterval);
            if ((ti != null) && ((ti.StartTime < new DateTime(1755, 1, 1, 0, 0, 0)) ||
                (ti.StartTime > new DateTime(9997, 12, 31, 23, 59, 59))))
            {
                ti.StartTime = DateTime.Now;
                ScheduleInterval1.ScheduleInterval = SchedulingHelper.EncodeInterval(ti);
            }
        }

        if (errorMessage != "")
        {
            lblError.Visible = true;
            lblError.Text = errorMessage;
        }
        else
        {
            // Check existing task name
            TaskInfo existingTask;
            if (si != null)
            {
                existingTask = TaskInfoProvider.GetTaskInfo(txtTaskName.Text.Trim(), si.SiteName);
            }
            else
            {
                existingTask = TaskInfoProvider.GetTaskInfo(txtTaskName.Text.Trim(), null);
            }

            if ((existingTask != null) && ((taskObj == null) || (existingTask.TaskID != taskObj.TaskID)))
            {
                lblError.Visible = true;
                lblError.Text = GetString("Task_Edit.TaskNameExists").Replace("%%name%%", existingTask.TaskName);
                return;
            }

            if (taskObj == null)
            {
                // create new item -> insert
                taskObj = new TaskInfo();
                taskObj.TaskSiteID = siteID;
                if (!developmentMode)
                {
                    taskObj.TaskAllowExternalService = true;
                }
            }

            taskObj.TaskAssemblyName = txtTaskAssemblyName.Text.Trim();
            taskObj.TaskClass = txtTaskClass.Text.Trim();
            taskObj.TaskData = txtTaskData.Text.Trim();
            taskObj.TaskName = txtTaskName.Text.Trim();
            taskObj.TaskEnabled = chkTaskEnabled.Checked;
            taskObj.TaskDeleteAfterLastRun = chkTaskDeleteAfterLastRun.Checked;
            taskObj.TaskInterval = ScheduleInterval1.ScheduleInterval;
            taskObj.TaskDisplayName = txtTaskDisplayName.Text.Trim();
            taskObj.TaskServerName = txtServerName.Text.Trim();
            taskObj.TaskRunInSeparateThread = chkRunTaskInSeparateThread.Checked;
            taskObj.TaskUseExternalService = chkTaskUseExternalService.Checked;
            if (plcAllowExternalService.Visible)
            {
                taskObj.TaskAllowExternalService = chkTaskAllowExternalService.Checked;
            }
            if (plcUseExternalService.Visible)
            {
                taskObj.TaskUseExternalService = chkTaskUseExternalService.Checked;
            }


            taskObj.TaskNextRunTime = SchedulingHelper.GetNextTime(taskObj.TaskInterval, new DateTime(), new DateTime());

            if (drpModule.Visible)
            {
                taskObj.TaskResourceID = ValidationHelper.GetInteger(drpModule.Value, 0);
            }

            // Set synchronization to true (default is false for Scheduled task)
            taskObj.Generalized.LogSynchronization = SynchronizationTypeEnum.LogSynchronization;
            taskObj.Generalized.LogIntegration = true;
            taskObj.Generalized.LogEvents = true;

            // If web farm support, create the tasks for all servers
            if (chkAllServers.Checked)
            {
                TaskInfoProvider.CreateWebFarmTasks(taskObj);
            }
            else
            {
                TaskInfoProvider.SetTaskInfo(taskObj);
            }

            if (cmsdesk)
            {
                URLHelper.Redirect("Task_Edit.aspx?taskname=" + taskObj.TaskID + "&saved=1&siteid=" + siteID);
            }
            else
            {
                URLHelper.Redirect("Task_Edit.aspx?taskname=" + taskObj.TaskID + "&saved=1&selectedsiteid=" + siteID);
            }
        }
    }
}
