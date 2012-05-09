using System;
using System.Data;
using System.Data.SqlTypes;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.CMSHelper;
using CMS.FormControls;
using CMS.GlobalHelper;
using CMS.OnlineMarketing;
using CMS.Scheduler;
using CMS.SettingsProvider;
using CMS.UIControls;

public partial class CMSModules_Scoring_Controls_UI_Score_Edit : CMSAdminEditControl
{
    #region "Variables"

    private TaskInfo task = null;

    #endregion


    #region "Properties"

    /// <summary>
    /// UIForm control used for editing objects properties.
    /// </summary>
    public UIForm UIFormControl
    {
        get
        {
            return this.EditForm;
        }
    }


    /// <summary>
    /// Indicates if the control should perform the operations.
    /// </summary>
    public override bool StopProcessing
    {
        get
        {
            return base.StopProcessing;
        }
        set
        {
            base.StopProcessing = value;
            this.EditForm.StopProcessing = value;
        }
    }


    /// <summary>
    /// Indicates if the control is used on the live site.
    /// </summary>
    public override bool IsLiveSite
    {
        get
        {
            return base.IsLiveSite;
        }
        set
        {
            base.IsLiveSite = value;
            EditForm.IsLiveSite = value;
        }
    }

    #endregion


    #region "Page events"

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        EditForm.OnBeforeSave += new EventHandler(EditForm_OnBeforeSave);
        EditForm.OnAfterSave += new EventHandler(EditForm_OnAfterSave);
        InitHeaderActions();
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        // Get edited score
        ScoreInfo score = (ScoreInfo)EditForm.EditedObject;

        // Get scheduled task
        if (score.ScoreScheduledTaskID > 0)
        {
            task = TaskInfoProvider.GetTaskInfo(score.ScoreScheduledTaskID);
        }

        if (!RequestHelper.IsPostBack())
        {
            // Hide advanced properties when creating new score
            plcUpdate.Visible = (score.ScoreID > 0);

            chkSchedule.Checked = schedulerInterval.Visible = ((task != null) && task.TaskEnabled);

            if (schedulerInterval.Visible)
            {
                // Initialize interval control
                schedulerInterval.ScheduleInterval = task.TaskInterval;
            }
        }

        if (score.ScoreID > 0)
        {
            // Display info panel
            pnlInfo.Visible = true;

            // Display basic info about score
            InitInfoPanel(score);
        }

        // Set tab name of editing UI to refresh upon change
        EditForm.RefreshTab = GetString("general.general");

        // Allow multiple addresses in e-mail input control
        fEmail.EditingControl.SetValue("allowmultipleaddresses", true);
    }

    #endregion


    #region "Events"

    /// <summary>
    /// OnBeforeSave event handler.
    /// </summary>
    protected void EditForm_OnBeforeSave(object sender, EventArgs e)
    {
        if ((EditForm.EditedObject != null))
        {
            if (EditForm.EditedObject.Generalized.ObjectID == 0)
            {
                // Set site ID only when creating new object
                EditForm.Data["ScoreSiteID"] = CMSContext.CurrentSiteID;
                // Set 'New' status
                EditForm.Data["ScoreStatus"] = 2;
            }
        }
    }


    /// <summary>
    /// OnAfterSave event handler.
    /// </summary>
    protected void EditForm_OnAfterSave(object sender, EventArgs e)
    {
        // Get edited contact group
        ScoreInfo score = (ScoreInfo)EditForm.EditedObject;

        // Set info for scheduled task
        task = GetScheduledTask(score);

        // Update scheduled task
        if (chkSchedule.Checked)
        {
            if (!schedulerInterval.CheckOneDayMinimum())
            {
                // If problem occurred while setting schedule interval
                EditForm.ErrorLabel.Text = GetString("Newsletter_Edit.NoDaySelected");
                EditForm.ErrorLabel.Visible = true;
                EditForm.StopProcessing = true;
                return;
            }

            if (!IsValidDate(SchedulingHelper.DecodeInterval(schedulerInterval.ScheduleInterval).StartTime))
            {
                // Start date is not in valid format
                EditForm.ErrorLabel.Text = GetString("Newsletter.IncorrectDate");
                EditForm.ErrorLabel.Visible = true;
                EditForm.StopProcessing = true;
                return;
            }

            task.TaskInterval = schedulerInterval.ScheduleInterval;
            task.TaskNextRunTime = SchedulingHelper.GetNextTime(task.TaskInterval, new DateTime(), new DateTime());
            task.TaskEnabled = true;
        }
        else
        {
            task.TaskInterval = schedulerInterval.ScheduleInterval;
            task.TaskNextRunTime = TaskInfoProvider.NO_TIME;
            task.TaskEnabled = false;
        }
        TaskInfoProvider.SetTaskInfo(task);

        score.ScoreScheduledTaskID = task.TaskID;
        pnlInfo.Visible = true;
        InitInfoPanel(score);

        // Update score
        ScoreInfoProvider.SetScoreInfo(score);

        InitHeaderActions();
        ((CMSPage)Page).CurrentMaster.HeaderActions.ReloadData();
    }


    /// <summary>
    /// Checkbox chkSchedule event handler.
    /// </summary>
    protected void chkSchedule_CheckedChanged(object sender, EventArgs e)
    {
        schedulerInterval.Visible = chkSchedule.Checked;

        if (schedulerInterval.Visible && (task != null))
        {
            // Set scheduled interval
            schedulerInterval.ScheduleInterval = task.TaskInterval;
        }
    }

    #endregion


    #region "Methods"

    /// <summary>
    /// Initializes header action control.
    /// </summary>
    private void InitHeaderActions()
    {
        bool isNew = ((EditForm.EditedObject != null) && (EditForm.EditedObject.Generalized.ObjectID == 0));

        // Header actions
        string[,] actions;
        if (isNew)
        {
            actions = new string[1, 11];
        }
        else
        {
            actions = new string[2, 11];
        }

        // Init save button
        actions[0, 0] = HeaderActions.TYPE_SAVEBUTTON;
        actions[0, 1] = GetString("General.Save");
        actions[0, 5] = GetImageUrl("CMSModules/CMS_Content/EditMenu/save.png");
        actions[0, 6] = "save";
        actions[0, 8] = "true";

        if (!isNew)
        {
            // Init recalculate button
            actions[1, 0] = HeaderActions.TYPE_SAVEBUTTON;
            actions[1, 1] = GetString("om.score.recalculate");
            actions[1, 5] = GetImageUrl("CMSModules/CMS_ContactManagement/evaluate.png");
            actions[1, 6] = "recalculate";
            actions[1, 8] = "false";
        }

        ((CMSPage)Page).CurrentMaster.HeaderActions.LinkCssClass = "ContentSaveLinkButton";
        ((CMSPage)Page).CurrentMaster.HeaderActions.ActionPerformed += HeaderActions_ActionPerformed;
        ((CMSPage)Page).CurrentMaster.HeaderActions.Actions = actions;
    }


    /// <summary>
    /// Actions handler.
    /// </summary>
    protected void HeaderActions_ActionPerformed(object sender, CommandEventArgs e)
    {
        // Check modify permission
        CheckPermissions("cms.scoring", "modify");

        switch (e.CommandName.ToLower())
        {
            case "save":
                // Save changes in the contact group
                EditForm.SaveData(null);
                break;

            case "recalculate":
                if (EditForm.EditedObject != null)
                {
                    ScoreInfo score = (ScoreInfo)EditForm.EditedObject;
                    if (score != null)
                    {
                        // Set 'Recalculating' status
                        score.ScoreStatus = ScoreStatusEnum.Recalculating;
                        // Ensure scheduled task
                        if (score.ScoreScheduledTaskID == 0)
                        {
                            // Create and initialize new scheduled task
                            task = GetScheduledTask(score);
                            task.TaskInterval = schedulerInterval.ScheduleInterval;
                            task.TaskNextRunTime = TaskInfoProvider.NO_TIME;
                            task.TaskEnabled = false;
                            TaskInfoProvider.SetTaskInfo(task);

                            // Update score info
                            score.ScoreScheduledTaskID = task.TaskID;
                        }
                        ScoreInfoProvider.SetScoreInfo(score);

                        // Recalculate the score
                        ScoreEvaluator evaluator = new ScoreEvaluator();
                        evaluator.ScoreID = score.ScoreID;
                        evaluator.Execute(null);

                        EditForm.InfoLabel.Text = GetString("om.score.recalculationstarted");
                        EditForm.InfoLabel.Visible = true;

                        // Get scheduled task and update last run time
                        if (task == null)
                        {
                            task = TaskInfoProvider.GetTaskInfo(score.ScoreScheduledTaskID);
                        }
                        if (task != null)
                        {
                            task.TaskLastRunTime = DateTime.Now;
                            TaskInfoProvider.SetTaskInfo(task);
                        }

                        // Display basic info about score
                        InitInfoPanel(score);
                    }
                }
                break;
        }
    }


    /// <summary>
    /// Returns scheduled task of the contact group or creates new one.
    /// </summary>
    /// <param name="score">Score info</param>
    private TaskInfo GetScheduledTask(ScoreInfo score)
    {
        if (score == null)
        {
            return null;
        }

        if (score.ScoreScheduledTaskID > 0)
        {
            return TaskInfoProvider.GetTaskInfo(ValidationHelper.GetInteger(score.ScoreScheduledTaskID, 0)) ??
                   CreateScheduledTask(score);
        }
        else
        {
            return CreateScheduledTask(score);
        }
    }


    /// <summary>
    /// Creates new scheduled task with basic properties set.
    /// </summary>
    /// <param name="score">Score info</param>
    private TaskInfo CreateScheduledTask(ScoreInfo score)
    {
        return new TaskInfo()
        {
            TaskAssemblyName = "CMS.OnlineMarketing",
            TaskClass = "CMS.OnlineMarketing.ScoreEvaluator",
            TaskEnabled = true,
            TaskLastResult = string.Empty,
            TaskSiteID = score.ScoreSiteID,
            TaskData = score.ScoreID.ToString(),
            TaskDisplayName = "Score '" + score.ScoreDisplayName + "' recalculation",
            TaskName = "ScoreRecalculation_" + score.ScoreName
        };
    }


    /// <summary>
    /// Returns if start date of the scheduled interval is valid.
    /// </summary>
    /// <param name="date">Start date of the scheduled interval</param>
    private bool IsValidDate(DateTime date)
    {
        return (date > SqlDateTime.MinValue.Value) && (date < SqlDateTime.MaxValue.Value);
    }


    /// <summary>
    /// Initializes panel with basic info.
    /// </summary>
    /// <param name="score">ScoreInfo object</param>
    private void InitInfoPanel(ScoreInfo score)
    {
        if (score != null)
        {
            pnlInfo.GroupingText = GetString("om.score.info");
            pnlInfo.Attributes.Add("style", "margin:0 30px;");

            // Last evaluation time
            if ((task != null) && (task.TaskLastRunTime != DateTimeHelper.ZERO_TIME))
            {
                lblLastEvalValue.Text = task.TaskLastRunTime.ToString();
            }
            else
            {
                lblLastEvalValue.Text = GetString("general.na");
            }

            // Display score status...
            switch (score.ScoreStatus)
            {
                case ScoreStatusEnum.Recalculating:
                    // Status and progress if the status is 'Recalculating'
                    ltrProgress.Text = String.Empty;

                    string buildStr = GetString("om.score.recalculating");
                    ltrProgress.Text = "<img style=\"width:12px;height:12px;\" src=\"" + UIHelper.GetImageUrl(this.Page, "Design/Preloaders/preload16.gif") + "\" alt=\"" + buildStr + "\" tooltip=\"" + buildStr + "\"  />";
                    lblStatusValue.Text = "<span class=\"StatusDisabled\">" + buildStr + "</span>";
                    break;

                case ScoreStatusEnum.Ready:
                    // 'Ready' status
                    lblStatusValue.Text = "<span class=\"StatusEnabled\">" + GetString("om.contactgroup.ready") + "</span>";
                    break;

                case ScoreStatusEnum.New:
                    // 'Condition changed' status
                    lblStatusValue.Text = "<span class=\"StatusDisabled\">" + GetString("om.score.recalcrequired") + "</span>";
                    break;

                case ScoreStatusEnum.Unspecified:
                    // 'Unspecified' status
                    lblStatusValue.Text = "<span class=\"StatusDisabled\">" + GetString("general.na") + "</span>";
                    break;
            }
        }
    }

    #endregion
}