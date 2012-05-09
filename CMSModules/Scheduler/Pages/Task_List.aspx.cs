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

using CMS.SiteProvider;
using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.Scheduler;
using CMS.SettingsProvider;
using CMS.UIControls;
using CMS.ExtendedControls;

public partial class CMSModules_Scheduler_Pages_Task_List : CMSScheduledTasksPage
{
    #region "Variables"

    private SiteInfo si = null;

    #endregion


    #region "Page events"

    protected override void OnPreInit(EventArgs e)
    {
        ((Panel)this.CurrentMaster.PanelBody.FindControl("pnlContent")).CssClass = "";
        base.OnPreInit(e);
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        // Setup page title text and image
        this.CurrentMaster.Title.TitleText = GetString("Task_List.HeaderCaption");
        this.CurrentMaster.Title.TitleImage = GetImageUrl("Objects/CMS_ScheduledTask/object.png");

        this.CurrentMaster.Title.HelpTopicName = "tasks_list";
        this.CurrentMaster.Title.HelpName = "helpTopic";

        this.btnRestart.Text = GetString("Task_List.Restart");
        this.btnRun.Text = GetString("Task_List.RunNow");

        // control initialization
        HyperlinkNew.Text = GetString("Task_List.NewItemCaption");
        HyperlinkRefresh.Text = GetString("General.Refresh");

        UniGridTasks.OnAction += new OnActionEventHandler(UniGridTasks_OnAction);
        UniGridTasks.OnExternalDataBound += new OnExternalDataBoundEventHandler(UniGridTasks_OnExternalDataBound);

        ImageNew.ImageUrl = GetImageUrl("Objects/CMS_ScheduledTask/add.png");
        ImageRefresh.ImageUrl = GetImageUrl("Objects/CMS_ScheduledTask/refresh.png");

        // Show info that scheduler is disables
        if (!SchedulingHelper.EnableScheduler)
        {
            lblInfo.Text = GetString("scheduledtask.disabled");
            lblInfo.Visible = true;
            btnRun.Enabled = false;
        }

        if (SiteID > 0)
        {
            pnlSites.Visible = false;
            siteSelector.StopProcessing = true;
            si = SiteInfoProvider.GetSiteInfo(SiteID);
        }
        else
        {
            // Set site selector
            siteSelector.DropDownSingleSelect.AutoPostBack = true;
            siteSelector.OnlyRunningSites = false;
            siteSelector.AllowAll = false;
            siteSelector.UniSelector.SpecialFields = new string[1, 2] { { GetString("general.global"), "0" } };
            siteSelector.UniSelector.OnSelectionChanged += new EventHandler(UniSelector_OnSelectionChanged);

            if (!RequestHelper.IsPostBack())
            {
                if (SelectedSiteID == -1)
                {
                    SelectedSiteID = CMSContext.CurrentSiteID;
                }

                siteSelector.Value = SelectedSiteID;
            }
            else
            {
                SelectedSiteID = ValidationHelper.GetInteger(siteSelector.Value, 0);
            }

            si = SiteInfoProvider.GetSiteInfo(SelectedSiteID);
        }

        UniGridTasks.WhereCondition = GenerateWhereCondition();
        UniGridTasks.ZeroRowsText = GetString("general.nodatafound");


        HyperlinkNew.NavigateUrl = "Task_Edit.aspx?" + GetSiteOrSelectedSite();
        HyperlinkRefresh.NavigateUrl = "Task_List.aspx?" + GetSiteOrSelectedSite();

        // Force action buttons to cause full postback so that tasks can be properly executed in global.asax
        ControlsHelper.RegisterPostbackControl(UniGridTasks);
        ControlsHelper.RegisterPostbackControl(btnRestart);
        ControlsHelper.RegisterPostbackControl(btnRun);
    }


    protected override void OnPreRender(EventArgs e)
    {
        if (si != null)
        {
            pnlLastRun.Visible = true;

            if (SchedulingHelper.UseAutomaticScheduler || !SchedulingHelper.RunSchedulerWithinRequest)
            {
                lblLastRun.Visible = true;
                btnRestart.Visible = true;

                string siteName = si.SiteName.ToLower();

                if (SchedulingTimer.TimerExists(siteName))
                {
                    btnRun.Enabled = true;
                    DateTime lastRun = ValidationHelper.GetDateTime(SchedulingTimer.LastRuns[siteName], DateTimeHelper.ZERO_TIME);

                    if (lastRun != DateTimeHelper.ZERO_TIME)
                    {
                        this.lblLastRun.Text = GetString("Task_List.LastRun") + " " + lastRun.ToString();
                    }
                    else
                    {
                        this.lblLastRun.Text = GetString("Task_List.Running");
                    }
                }
                else
                {
                    btnRun.Enabled = false;
                    this.lblLastRun.Text = GetString("Task_List.NoRun");
                }
            }
            else
            {
                lblLastRun.Visible = false;
                btnRestart.Visible = false;
            }
        }
        else
        {
            // Hide panel in sitemanager for global scheduled tasks
            pnlLastRun.Visible = false;
        }

        this.pnlUpdateTimer.Update();

        base.OnPreRender(e);
    }

    #endregion


    /// <summary>
    /// Generates where condition for unigrid.
    /// </summary>    
    private string GenerateWhereCondition()
    {
        if (SiteID > 0)
        {
            return "TaskSiteID = " + SiteID;
        }
        else if (SelectedSiteID > 0)
        {
            return "TaskSiteID = " + SelectedSiteID;
        }
        else
        {
            return "TaskSiteID IS NULL";
        }
    }


    /// <summary>
    /// Returns 'siteid' or 'selectedsiteid' parameter depending on query string.
    /// </summary>
    /// <returns>Query parameter</returns>
    private string GetSiteOrSelectedSite()
    {
        // Site ID is used in CMS desk
        if (SiteID > 0)
        {
            return "siteId=" + SiteID;
        }
        // SelectedSiteID is used in CMS Site Manager
        else
        {
            return "selectedsiteid=" + SelectedSiteID;
        }
    }


    /// <summary>
    /// Handles site selection change event.
    /// </summary>
    protected void UniSelector_OnSelectionChanged(object sender, EventArgs e)
    {
        // Update unigrid
        this.pnlUpdate.Update();
    }


    /// <summary>
    /// Handles the UniGrid's OnAction event.
    /// </summary>
    /// <param name="actionName">Name of item (button) that threw event</param>
    /// <param name="actionArgument">ID (value of Primary key) of corresponding data row</param>
    protected void UniGridTasks_OnAction(string actionName, object actionArgument)
    {
        switch (actionName.ToLower())
        {
            case "edit":

                URLHelper.Redirect("Task_Edit.aspx?taskname=" + actionArgument.ToString() + "&" + GetSiteOrSelectedSite());

                break;

            case "delete":
                {
                    // Check "modify" permission
                    if (!CMSContext.CurrentUser.IsAuthorizedPerResource("CMS.ScheduledTasks", "Modify"))
                    {
                        RedirectToAccessDenied("CMS.ScheduledTasks", "Modify");
                    }

                    // Delete the task
                    try
                    {
                        int taskId = Convert.ToInt32(actionArgument);

                        TaskInfo ti = TaskInfoProvider.GetTaskInfo(taskId);
                        if (ti != null)
                        {
                            ti.Generalized.LogSynchronization = SynchronizationTypeEnum.LogSynchronization;
                            ti.Generalized.LogIntegration = true;
                            ti.Generalized.LogEvents = true;
                            TaskInfoProvider.DeleteTaskInfo(ti);
                        }
                    }
                    catch (Exception ex)
                    {
                        lblError.Visible = true;
                        lblError.Text = GetString("Task_List.DeleteError") + " Original exception: " + ex.Message;
                    }
                }
                break;

            case "execute":
                {
                    // Check "modify" permission
                    if (!CMSContext.CurrentUser.IsAuthorizedPerResource("CMS.ScheduledTasks", "Modify"))
                    {
                        RedirectToAccessDenied("CMS.ScheduledTasks", "Modify");
                    }

                    TaskInfo ti = TaskInfoProvider.GetTaskInfo(Convert.ToInt32(actionArgument));
                    if (ti != null)
                    {
                        if (ti.TaskEnabled)
                        {
                            TaskInterval interval = new TaskInterval();
                            interval = SchedulingHelper.DecodeInterval(ti.TaskInterval);

                            if ((ti.TaskNextRunTime != DateTimeHelper.ZERO_TIME) || (interval.Period == SchedulingHelper.PERIOD_ONCE))
                            {
                                ti.TaskNextRunTime = DateTime.Now;

                                // Update the task
                                TaskInfoProvider.SetTaskInfo(ti);

                                // Run the task
                                SchedulingTimer.RunSchedulerImmediately = true;
                                if (si != null)
                                {
                                    SchedulingTimer.SchedulerRunImmediatelySiteName = si.SiteName;
                                }

                                string url = URLHelper.Url.AbsoluteUri;
                                url = URLHelper.AddParameterToUrl(url, "selectedsiteid", SelectedSiteID.ToString());

                                lblInfo.Text = GetString("ScheduledTask.WasExecuted");
                                lblInfo.Visible = true;

                                //ScriptHelper.RegisterStartupScript(this, typeof(string), "InformExecuted",
                                //        "alert('" + GetString("ScheduledTask.WasExecuted") + "'); \n" +
                                //        "document.location = '" + url + "'; \n", true);
                            }
                            else
                            {
                                lblInfo.Text = GetString("ScheduledTask.TaskAlreadyrunning");
                                lblInfo.Visible = true;
                            }
                        }
                        else
                        {
                            lblError.Text = GetString("ScheduledTask.TaskNotEnabled");
                            lblError.Visible = true;
                        }
                    }
                }
                break;
        }
    }


    protected object UniGridTasks_OnExternalDataBound(object sender, string sourceName, object parameter)
    {
        switch (sourceName.ToLower())
        {
            case "useexternalservice":
                // Use external service
                {
                    ImageButton imgButton = sender as ImageButton;
                    if (imgButton != null)
                    {
                        bool visible = false;
                        // Only if setting 'Use external service' is allowed
                        if (SchedulingHelper.UseExternalService)
                        {
                            DataRowView drv = UniGridFunctions.GetDataRowView(imgButton.Parent as DataControlFieldCell);
                            if (drv != null)
                            {
                                // Indicates whether the task is processed by an external service
                                bool taskUseExternalService = ValidationHelper.GetBoolean(drv["TaskUseExternalService"], false);
                                // Indicates whether the task is enabled
                                bool taskEnabled = ValidationHelper.GetBoolean(drv["TaskEnabled"], false);

                                if (taskUseExternalService && taskEnabled)
                                {
                                    DateTime taskLastRunTime = ValidationHelper.GetDateTime(drv["TaskLastRunTime"], DateTimeHelper.ZERO_TIME);

                                    if (taskLastRunTime != DateTimeHelper.ZERO_TIME)
                                    {
                                        // Task period
                                        string interval = ValidationHelper.GetString(drv["TaskInterval"], null);
                                        DateTime now = DateTime.Now;
                                        TimeSpan period = SchedulingHelper.GetNextTime(interval, taskLastRunTime, now).Subtract(now);
                                        // Actual different time between now date and last run time
                                        TimeSpan actualDifferent = now.Subtract(taskLastRunTime);

                                        // Show image if actual different time is three times larger than task period
                                        if ((period.TotalSeconds > 0) && (actualDifferent.TotalSeconds > (period.TotalSeconds * 3)))
                                        {
                                            imgButton.ToolTip = GetString("scheduledtask.useservicewarning");
                                            visible = true;
                                        }
                                    }
                                }
                            }
                        }

                        imgButton.Visible = visible;
                        if (imgButton.Visible)
                        {
                            imgButton.OnClientClick = "return false;";
                            imgButton.Style.Add(HtmlTextWriterStyle.Cursor, "default");
                        }
                    }
                }
                break;

            case "taskexecutions":
                if (string.IsNullOrEmpty(Convert.ToString(parameter)))
                {
                    return 0;
                }
                break;
        }
        return parameter;
    }


    protected void btnRestart_Click(object sender, EventArgs e)
    {
        // Check "modify" permission
        if (!CMSContext.CurrentUser.IsAuthorizedPerResource("CMS.ScheduledTasks", "Modify"))
        {
            RedirectToAccessDenied("CMS.ScheduledTasks", "Modify");
        }

        SchedulingTimer.RestartTimer(si.SiteName);
    }


    protected void btnRun_Click(object sender, EventArgs e)
    {
        // Check "modify" permission
        if (!CMSContext.CurrentUser.IsAuthorizedPerResource("CMS.ScheduledTasks", "Modify"))
        {
            RedirectToAccessDenied("CMS.ScheduledTasks", "Modify");
        }

        SchedulingTimer.RunSchedulerASAP(si.SiteName);
        SchedulingTimer.SchedulerRunImmediatelySiteName = si.SiteName;
    }
}


