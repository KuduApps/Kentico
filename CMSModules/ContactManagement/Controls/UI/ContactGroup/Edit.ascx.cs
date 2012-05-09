using System;
using System.Data;
using System.Data.SqlTypes;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.CMSHelper;
using CMS.FormControls;
using CMS.GlobalHelper;
using CMS.Scheduler;
using CMS.UIControls;
using CMS.OnlineMarketing;
using CMS.SettingsProvider;

public partial class CMSModules_ContactManagement_Controls_UI_ContactGroup_Edit : CMSAdminEditControl
{
    #region "Variables"

    private int mSiteID = 0;

    private int deleteScheduledTaskId = 0;

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

    /// <summary>
    /// SiteID of current contact group.
    /// </summary>
    public int SiteID
    {
        get
        {
            return mSiteID;
        }
        set
        {
            mSiteID = value;
            if ((mSiteID > 0) && !CMSContext.CurrentUser.UserSiteManagerAdmin)
            {
                mSiteID = CMSContext.CurrentSiteID;
            }
        }
    }

    #endregion


    #region "Page events"

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        EditForm.OnBeforeSave += new EventHandler(EditForm_OnBeforeSave);
        EditForm.OnAfterSave += new EventHandler(EditForm_OnAfterSave);
        InitHeaderActions(false);
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        string url = "Frameset.aspx?groupid={%EditedObject.ID%}&saved=1";
        url = URLHelper.AddParameterToUrl(url, "siteid", SiteID.ToString());
        if (ContactHelper.IsSiteManager)
        {
            url = URLHelper.AddParameterToUrl(url, "issitemanager", "1");
        }
        EditForm.RedirectUrlAfterCreate = url;

        // Get edited contact group
        ContactGroupInfo cgi = (ContactGroupInfo)EditForm.EditedObject;

        // Get scheduled task
        if (cgi.ContactGroupScheduledTaskID > 0)
        {
            task = TaskInfoProvider.GetTaskInfo(cgi.ContactGroupScheduledTaskID);
        }

        if (!RequestHelper.IsPostBack())
        {
            // Hide dialog for condition when creating new contact group
            plcUpdate.Visible = (cgi.ContactGroupID > 0);

            chkSchedule.Checked = schedulerInterval.Visible = ((task != null) && task.TaskEnabled);

            if (schedulerInterval.Visible)
            {
                // Initialize interval control
                schedulerInterval.ScheduleInterval = task.TaskInterval;
            }
        }

        // Set proper resolver to condition builder
        conditionBuilder.SetValue("Resolver", EmailTemplateMacros.ContactResolver);

        if (task != null)
        {
            // Display info panel for dynamic contact group
            pnlInfo.Visible = true;

            // Display basic info about dynamic contact group
            InitInfoPanel(cgi, false);
        }
    }

    #endregion


    #region "Events"

    /// <summary>
    /// OnBeforeSave event handler.
    /// </summary>
    protected void EditForm_OnBeforeSave(object sender, EventArgs e)
    {
        // Set site ID only when creating new object
        if ((EditForm.EditedObject != null))
        {
            int groupId = EditForm.EditedObject.Generalized.ObjectID;

            if (groupId == 0)
            {
                if (SiteID > 0)
                {
                    EditForm.Data["ContactGroupSiteID"] = SiteID;
                }
                else
                {
                    EditForm.Data["ContactGroupSiteID"] = DBNull.Value;
                }
            }
            else
            {
                if (!chkDynamic.Checked)
                {
                    // Remove dynamic condition
                    ((FormEngineUserControl)EditForm.FieldControls["ContactGroupDynamicCondition"]).Value = null;

                    // Remove dynamicaly created members
                    if (ValidationHelper.GetBoolean(hdnConfirmDelete.Value, false))
                    {
                        ContactGroupMemberInfoProvider.DeleteContactGroupMembers("ContactGroupMemberContactGroupID = " + groupId + " AND (ContactGroupMemberFromCondition = 1 AND (ContactGroupMemberFromAccount = 0 OR ContactGroupMemberFromAccount IS NULL) AND (ContactGroupMemberFromManual = 0 OR ContactGroupMemberFromManual IS NULL))", groupId, false, false);
                    }
                }
                else
                {
                    // Get new condition
                    string condition = ((FormEngineUserControl)EditForm.FieldControls["ContactGroupDynamicCondition"]).Value.ToString();

                    // Display error if the condition is empty
                    if (string.IsNullOrEmpty(condition))
                    {
                        EditForm.StopProcessing = true;
                        EditForm.ErrorLabel.Text = GetString("om.contactgroup.nocondition");
                    }
                    else
                    {
                        // Get current object to compare dynamic conditions
                        ContactGroupInfo currentGroup = ContactGroupInfoProvider.GetContactGroupInfo(EditForm.EditedObject.Generalized.ObjectID);
                        if ((currentGroup != null) && (!condition.Equals(currentGroup.ContactGroupDynamicCondition, StringComparison.InvariantCultureIgnoreCase)))
                        {
                            // Set 'Rebuild required' status
                            EditForm.Data["ContactGroupStatus"] = 2;
                        }
                    }
                }
            }
        }
    }


    /// <summary>
    /// OnAfterSave event handler.
    /// </summary>
    protected void EditForm_OnAfterSave(object sender, EventArgs e)
    {
        // Get edited contact group
        ContactGroupInfo cgi = (ContactGroupInfo)EditForm.EditedObject;

        if (chkDynamic.Checked)
        {
            // Set info for scheduled task
            task = GetScheduledTask(cgi);

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

            cgi.ContactGroupScheduledTaskID = task.TaskID;
            pnlInfo.Visible = true;
            InitInfoPanel(cgi, true);
        }
        else
        {
            if (cgi.ContactGroupScheduledTaskID > 0)
            {
                // Store task ID for deletion
                deleteScheduledTaskId = cgi.ContactGroupScheduledTaskID;
            }
            cgi.ContactGroupScheduledTaskID = 0;
            cgi.ContactGroupStatus = ContactGroupStatusEnum.Unspecified;
            schedulerInterval.Visible = false;
            pnlInfo.Visible = false;
        }

        // Update contact group
        ContactGroupInfoProvider.SetContactGroupInfo(cgi);

        if (deleteScheduledTaskId > 0)
        {
            // Delete scheduled task if schedule evaluation was unchecked
            TaskInfoProvider.DeleteTaskInfo(deleteScheduledTaskId);
        }

        InitHeaderActions(false);
        ((CMSPage)Page).CurrentMaster.HeaderActions.ReloadData();

        // Refresh breadcrumbs after data are loaded
        ScriptHelper.RefreshTabHeader(Page, null); 
    }


    /// <summary>
    /// Checkbox chkDynamic event handler.
    /// </summary>
    protected void chkDynamic_CheckedChanged(object sender, EventArgs e)
    {
        plcDynamic.Visible = chkDynamic.Checked;

        // Set confirmation dialog
        if (!chkDynamic.Checked)
        {
            ContactGroupInfo cgi = ContactGroupInfoProvider.GetContactGroupInfo(EditForm.EditedObject.Generalized.ObjectID);
            if ((cgi != null) && (!String.IsNullOrEmpty(cgi.ContactGroupDynamicCondition)))
            {
                InitHeaderActions(true);
                ((CMSPage)Page).CurrentMaster.HeaderActions.ReloadData();
            }
        }
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
    private void InitHeaderActions(bool confirmDelete)
    {
        bool isDynamic = !String.IsNullOrEmpty(ValidationHelper.GetString(this.EditForm.Data["ContactGroupDynamicCondition"], null));

        if (!RequestHelper.IsPostBack())
        {
            plcDynamic.Visible = chkDynamic.Checked = isDynamic;
        }

        // Header actions
        string[,] actions;
        if (isDynamic)
        {
            actions = new string[2, 11];
        }
        else
        {
            actions = new string[1, 11];
        }

        actions[0, 0] = HeaderActions.TYPE_SAVEBUTTON;
        actions[0, 1] = GetString("General.Save");
        actions[0, 5] = GetImageUrl("CMSModules/CMS_Content/EditMenu/save.png");
        actions[0, 6] = "save";
        actions[0, 8] = "true";

        if (confirmDelete)
        {
            ScriptHelper.RegisterStartupScript(this.Page, typeof(string), "ConfirmDelete", ScriptHelper.GetScript(
@"
function ConfirmDelete()
{
document.getElementById('" + hdnConfirmDelete.ClientID + "').value = confirm('" + GetString("om.contactgroup.ConfirmDelete") + @"');
}"));
            actions[0, 2] = "ConfirmDelete();";
        }

        if (isDynamic)
        {
            actions[1, 0] = HeaderActions.TYPE_SAVEBUTTON;
            actions[1, 1] = GetString("om.contacgroup.rebuild");
            actions[1, 5] = GetImageUrl("CMSModules/CMS_ContactManagement/evaluate.png");
            actions[1, 6] = "evaluate";
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
        if (ContactGroupHelper.AuthorizedModifyContactGroup(this.SiteID, true))
        {
            switch (e.CommandName.ToLower())
            {
                case "save":
                    // Save changes in the contact group
                    EditForm.SaveData(null);
                    break;

                case "evaluate":
                    if (EditForm.EditedObject != null)
                    {
                        ContactGroupInfo cgi = (ContactGroupInfo)EditForm.EditedObject;
                        if (cgi != null)
                        {
                            // Set 'Rebuilding' status
                            cgi.ContactGroupStatus = ContactGroupStatusEnum.Rebuilding;
                            ContactGroupInfoProvider.SetContactGroupInfo(cgi);

                            // Evaluate the membership of the contact group
                            ContactGroupEvaluator evaluator = new ContactGroupEvaluator();
                            evaluator.ContactGroupID = cgi.ContactGroupID;
                            evaluator.Execute(null);

                            EditForm.InfoLabel.Text = GetString("om.contactgroup.evaluationstarted");
                            EditForm.InfoLabel.Visible = true;

                            // Get scheduled task and update last run time
                            if (cgi.ContactGroupScheduledTaskID > 0)
                            {
                                task = TaskInfoProvider.GetTaskInfo(cgi.ContactGroupScheduledTaskID);
                                if (task != null)
                                {
                                    task.TaskLastRunTime = DateTime.Now;
                                    TaskInfoProvider.SetTaskInfo(task);
                                }
                            }

                            // Display basic info about dynamic contact group
                            InitInfoPanel(cgi, false);
                        }
                    }
                    break;
            }
        }
    }


    /// <summary>
    /// Returns scheduled task of the contact group or creates new one.
    /// </summary>
    /// <param name="cgi">Contact group info</param>
    private TaskInfo GetScheduledTask(ContactGroupInfo cgi)
    {
        if (cgi == null)
        {
            return null;
        }

        if (cgi.ContactGroupScheduledTaskID > 0)
        {
            return TaskInfoProvider.GetTaskInfo(ValidationHelper.GetInteger(cgi.ContactGroupScheduledTaskID, 0)) ??
                   CreateScheduledTask(cgi);
        }
        else
        {
            return CreateScheduledTask(cgi);
        }
    }


    /// <summary>
    /// Creates new scheduled task with basic properties set.
    /// </summary>
    /// <param name="cgi">Contact group info</param>
    private TaskInfo CreateScheduledTask(ContactGroupInfo cgi)
    {
        return new TaskInfo()
        {
            TaskAssemblyName = "CMS.OnlineMarketing",
            TaskClass = "CMS.OnlineMarketing.ContactGroupEvaluator",
            TaskEnabled = true,
            TaskLastResult = string.Empty,
            TaskSiteID = cgi.ContactGroupSiteID,
            TaskData = cgi.ContactGroupID.ToString(),
            TaskDisplayName = "Contact group '" + cgi.ContactGroupDisplayName + "' rebuild",
            TaskName = "ContactGroupRebuild_" + cgi.ContactGroupName
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
    /// <param name="cgi">Contact group info object</param>
    /// <param name="forceReload">If TRUE number of group members is reloaded</param>
    private void InitInfoPanel(ContactGroupInfo cgi, bool forceReload)
    {
        if (cgi != null && task != null)
        {
            pnlInfo.GroupingText = GetString("om.contactgroup.info");
            pnlInfo.Attributes.Add("style", "margin:0 30px;");

            // Last evaluation time
            if (task.TaskLastRunTime != DateTimeHelper.ZERO_TIME)
            {
                lblLastEvalValue.Text = task.TaskLastRunTime.ToString();
            }
            else
            {
                lblLastEvalValue.Text = GetString("general.na");
            }

            // Display contact group status...
            switch (cgi.ContactGroupStatus)
            {
                case ContactGroupStatusEnum.Rebuilding:
                    // Status and progress if the status is 'Rebuilding'
                    ltrProgress.Text = String.Empty;

                    string buildStr = GetString("om.contactgroup.rebuilding");
                    ltrProgress.Text = "<img style=\"width:12px;height:12px;\" src=\"" + UIHelper.GetImageUrl(this.Page, "Design/Preloaders/preload16.gif") + "\" alt=\"" + buildStr + "\" tooltip=\"" + buildStr + "\"  />";
                    lblStatusValue.Text = "<span class=\"StatusDisabled\">" + buildStr + "</span>";
                    break;

                case ContactGroupStatusEnum.Ready:
                    // 'Ready' status
                    lblStatusValue.Text = "<span class=\"StatusEnabled\">" + GetString("om.contactgroup.ready") + "</span>";
                    break;

                case ContactGroupStatusEnum.ConditionChanged:
                    // 'Condition changed' status
                    lblStatusValue.Text = "<span class=\"StatusDisabled\">" + GetString("om.contactgroup.rebuildrequired") + "</span>";
                    break;
            }

            // Display current number of contacts in the group
            if (!RequestHelper.IsPostBack() || (cgi.ContactGroupStatus == ContactGroupStatusEnum.Rebuilding)
                || (DateTime.Now.Subtract(cgi.ContactGroupLastModified).TotalSeconds < 5) || forceReload)
            {
                lblNumberOfItemsValue.Text = ContactGroupMemberInfoProvider.GetNumberOfContactsInGroup(cgi.ContactGroupID, true).ToString();
            }
        }
    }

    #endregion
}