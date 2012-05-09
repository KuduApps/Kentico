using System;
using System.Web.UI.WebControls;

using CMS.EmailEngine;
using CMS.GlobalHelper;
using CMS.SettingsProvider;
using CMS.UIControls;

public partial class CMSModules_EmailQueue_EmailQueue : SiteManagerPage
{
    #region "Variables"

    protected int siteId;

    #endregion


    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        Title = GetString("emailqueue.queue.title");
        siteId = QueryHelper.GetInteger("siteid", -1);

        InitializeActionMenu();

        string siteName = EmailHelper.GetSiteName(siteId);

        // Display disabled information
        if (!EmailHelper.Settings.EmailsEnabled(siteName))
        {
            lblDisabled.Visible = true;
        }

        // Load drop-down lists
        if (!RequestHelper.IsPostBack())
        {
            InitializeFilterDropdowns();

            btnShowFilter.ResourceString = "emailqueue.displayfilter";
            imgShowFilter.ImageUrl = GetImageUrl("Design/Controls/UniGrid/Actions/SortDown.png");
        }

        gridEmailQueue.EmailGrid.WhereCondition = GetWhereCondition();

        ScriptHelper.RegisterDialogScript(this);

        // Register script for modal dialog
        string script = @"
                          function DisplayRecipients(emailId) {
                            if ( emailId != 0 ) {
                                modalDialog('MassEmails_Recipients.aspx?emailid=' + emailId, 'emailrecipients', 920, 700);
                            } 
                          }
                        ";
        ScriptHelper.RegisterClientScriptBlock(this, GetType(), "displayModal", script, true);

        if (EmailHelper.Queue.SendingInProgess)
        {
            ShowInformation(GetString("emailqueue.sendingemails"));
        }
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        // Enables/disables action buttons according to grid content
        InitializeActionMenu();
        CurrentMaster.HeaderActions.ReloadData();
    }

    #endregion


    #region "Private methods"

    /// <summary>
    /// Initializes action menu in master page.
    /// </summary>
    protected void InitializeActionMenu()
    {
        bool sending = EmailHelper.Queue.SendingInProgess;        

        // Resend all failed
        string[,] actions = new string[8, 10];
        actions[0, 0] = HeaderActions.TYPE_LINKBUTTON;
        actions[0, 1] = GetString("emailqueue.queue.resendfailed");
        actions[0, 2] = !sending ? string.Format("if (!confirm({0})) return false;", ScriptHelper.GetString(GetString("EmailQueue.ResendAllFailedConfirmation"))) :
                                    null;
        actions[0, 5] = !sending ? GetImageUrl("CMSModules/CMS_EmailQueue/resendallfailed.png") :
                                    GetImageUrl("CMSModules/CMS_EmailQueue/resendallfailed_disabled.png");
        actions[0, 6] = "resendallfailed";
        actions[0, 9] = (!sending).ToString();

        // Resend selected
        actions[1, 0] = HeaderActions.TYPE_LINKBUTTON;
        actions[1, 1] = GetString("emailqueue.queue.resendselected");
        actions[1, 2] = !sending ? string.Format("if (!confirm({0})) return false;", ScriptHelper.GetString(GetString("EmailQueue.ResendSelectedConfirmation"))) :
                                    null;
        actions[1, 5] = !sending ? GetImageUrl("CMSModules/CMS_EmailQueue/resendselected.png") :
                                    GetImageUrl("CMSModules/CMS_EmailQueue/resendselected_disabled.png");
        actions[1, 6] = "resendselected";
        actions[1, 9] = (!sending).ToString();

        // Resend all
        actions[2, 0] = HeaderActions.TYPE_LINKBUTTON;
        actions[2, 1] = GetString("emailqueue.queue.resend");
        actions[2, 2] = !sending ? string.Format("if (!confirm({0})) return false;", ScriptHelper.GetString(GetString("EmailQueue.ResendAllConfirmation"))) :
                                    null;
        actions[2, 5] = !sending ? GetImageUrl("CMSModules/CMS_EmailQueue/resendall.png") :
                                    GetImageUrl("CMSModules/CMS_EmailQueue/resendall_disabled.png");
        actions[2, 6] = "resendall";
        actions[2, 9] = (!sending).ToString();

        // Delete all failed
        actions[3, 0] = HeaderActions.TYPE_LINKBUTTON;
        actions[3, 1] = GetString("emailqueue.queue.deletefailed");
        actions[3, 2] = !sending ? string.Format("if (!confirm({0})) return false;", ScriptHelper.GetString(GetString("EmailQueue.DeleteAllFailedConfirmation"))) :
                                    null;
        actions[3, 5] = !sending ? GetImageUrl("CMSModules/CMS_EmailQueue/deleteallfailed.png") :
                                    GetImageUrl("CMSModules/CMS_EmailQueue/deleteallfailed_disabled.png");
        actions[3, 6] = "deleteallfailed";
        actions[3, 9] = (!sending).ToString();

        // Delete selected
        actions[4, 0] = HeaderActions.TYPE_LINKBUTTON;
        actions[4, 1] = GetString("emailqueue.queue.deleteselected");
        actions[4, 2] = !sending ? string.Format("if (!confirm({0})) return false;", ScriptHelper.GetString(GetString("EmailQueue.DeleteSelectedConfirmation"))) :
                                    null;
        actions[4, 5] = !sending ? GetImageUrl("CMSModules/CMS_EmailQueue/deleteselected.png") :
                                    GetImageUrl("CMSModules/CMS_EmailQueue/deleteselected_disabled.png");
        actions[4, 6] = "deleteselected";
        actions[4, 9] = (!sending).ToString();

        // Delete all
        actions[5, 0] = HeaderActions.TYPE_LINKBUTTON;
        actions[5, 1] = GetString("emailqueue.queue.delete");
        actions[5, 2] = !sending ? string.Format("if (!confirm({0})) return false;", ScriptHelper.GetString(GetString("EmailQueue.DeleteAllConfirmation"))) :
                                    null;
        actions[5, 5] = !sending ? GetImageUrl("CMSModules/CMS_EmailQueue/deleteall.png") :
                                    GetImageUrl("CMSModules/CMS_EmailQueue/deleteall_disabled.png");
        actions[5, 6] = "deleteall";
        actions[5, 9] = (!sending).ToString();

        // Stop send
        actions[6, 0] = HeaderActions.TYPE_LINKBUTTON;
        actions[6, 1] = GetString("emailqueue.queue.stop");
        actions[6, 2] = sending ? string.Format("if (!confirm({0})) return false;", ScriptHelper.GetString(GetString("EmailQueue.StopConfirmation"))) :
                                  null;
        actions[6, 5] = sending ? GetImageUrl("CMSModules/CMS_EmailQueue/stopsend.png") :
                                  GetImageUrl("CMSModules/CMS_EmailQueue/stopsend_disabled.png");
        actions[6, 6] = "stop";
        actions[6, 9] = sending.ToString();

        // Refresh
        actions[7, 0] = HeaderActions.TYPE_LINKBUTTON;
        actions[7, 1] = GetString("general.refresh");
        actions[7, 5] = GetImageUrl("CMSModules/CMS_EmailQueue/refresh.png");
        actions[7, 6] = "refresh";

        CurrentMaster.HeaderActions.Actions = actions;
        CurrentMaster.HeaderActions.ActionPerformed += HeaderActions_ActionPerformed;
    }


    private void InitializeFilterDropdowns()
    {
        drpPriority.Items.Add(new ListItem(GetString("general.selectall"), "-1"));
        drpPriority.Items.Add(new ListItem(GetString("emailpriority.low"), EmailPriorityEnum.Low.ToString("D")));
        drpPriority.Items.Add(new ListItem(GetString("emailpriority.normal"), EmailPriorityEnum.Normal.ToString("D")));
        drpPriority.Items.Add(new ListItem(GetString("emailpriority.high"), EmailPriorityEnum.High.ToString("D")));

        drpStatus.Items.Add(new ListItem(GetString("general.selectall"), "-1"));
        drpStatus.Items.Add(new ListItem(GetString("emailstatus.created"), EmailStatusEnum.Created.ToString("D")));
        drpStatus.Items.Add(new ListItem(GetString("emailstatus.sending"), EmailStatusEnum.Sending.ToString("D")));
        drpStatus.Items.Add(new ListItem(GetString("emailstatus.waiting"), EmailStatusEnum.Waiting.ToString("D")));
    }

    #endregion


    #region "Button events"

    /// <summary>
    /// Displays/hides filter.
    /// </summary>
    protected void btnShowFilter_Click(object sender, EventArgs e)
    {
        // Hide filter
        if (plcFilter.Visible)
        {
            plcFilter.Visible = false;
            btnShowFilter.ResourceString = "emailqueue.displayfilter";
            imgShowFilter.ImageUrl = GetImageUrl("Design/Controls/UniGrid/Actions/SortDown.png");
        }
        // Display filter
        else
        {
            plcFilter.Visible = true;
            btnShowFilter.ResourceString = "emailqueue.hidefilter";
            imgShowFilter.ImageUrl = GetImageUrl("Design/Controls/UniGrid/Actions/SortUp.png");
        }
    }


    /// <summary>
    /// Filter button clicked.
    /// </summary>
    protected void btnFilter_Clicked(object sender, EventArgs e)
    {
        gridEmailQueue.EmailGrid.WhereCondition = GetWhereCondition();
    }

    #endregion


    #region "Header action event"

    void HeaderActions_ActionPerformed(object sender, CommandEventArgs e)
    {
        bool reloaded = true;

        switch (e.CommandName.ToLower())
        {
            case "resendallfailed":
                EmailHelper.Queue.SendAllFailed();
                ShowInformation(GetString("emailqueue.sendingemails"));
                break;

            case "resendselected":
                EmailHelper.Queue.Send(gridEmailQueue.GetSelectedEmailIDs());
                gridEmailQueue.EmailGrid.ResetSelection();
                ShowInformation(GetString("emailqueue.sendingemails"));

                break;

            case "resendall":
                EmailHelper.Queue.SendAll();
                ShowInformation(GetString("emailqueue.sendingemails"));

                break;

            case "deleteallfailed":
                EmailHelper.Queue.DeleteAllFailed(siteId);
                break;

            case "deleteselected":
                EmailHelper.Queue.Delete(gridEmailQueue.GetSelectedEmailIDs());
                gridEmailQueue.EmailGrid.ResetSelection();
                break;

            case "deleteall":
                EmailHelper.Queue.DeleteAll(siteId);
                break;

            case "stop":
                EmailHelper.Queue.CancelSending();
                break;

            case "refresh":
                reloaded = false;
                break;
        }

        gridEmailQueue.ReloadData();

        // Reload on first page if no data found after perfoming action
        if (reloaded && DataHelper.DataSourceIsEmpty(gridEmailQueue.EmailGrid.GridView.DataSource))
        {
            gridEmailQueue.EmailGrid.Pager.UniPager.CurrentPage = 1;
            gridEmailQueue.ReloadData();
        }
    }

    #endregion


    #region "Filter methods"

    /// <summary>
    /// Returns WHERE condition.
    /// </summary>
    protected string GetWhereCondition()
    {
        string where = string.Empty;

        where = SqlHelperClass.AddWhereCondition(where, fltFrom.GetCondition());
        where = SqlHelperClass.AddWhereCondition(where, fltSubject.GetCondition());
        where = SqlHelperClass.AddWhereCondition(where, fltBody.GetCondition());
        where = SqlHelperClass.AddWhereCondition(where, fltLastResult.GetCondition());

        // EmailTo condition
        string emailTo = fltTo.FilterText.Trim();
        if (!String.IsNullOrEmpty(emailTo))
        {
            if (!String.IsNullOrEmpty(where))
            {
                where += " AND ";
            }
            string toText = SqlHelperClass.GetSafeQueryString(emailTo, false);
            string op = fltTo.FilterOperator;
            if (op.Contains("LIKE"))
            {
                toText = "%" + toText + "%";
            }
            toText = " N'" + toText + "'";
            string combineOp = " OR ";
            bool includeNullCondition = false;
            if ((op == "<>") || op.Contains("NOT"))
            {
                combineOp = " AND ";
                includeNullCondition = true;
            }
            where += string.Format("(EmailTo {0}{1}{2}(EmailCc {0}{1}{3}){2}(EmailBcc {0}{1}{4}))",
                op, toText, combineOp, includeNullCondition ? " OR EmailCc IS NULL" : string.Empty, includeNullCondition ? " OR EmailBcc IS NULL" : string.Empty);
        }

        // Condition for priority
        int priority = ValidationHelper.GetInteger(drpPriority.SelectedValue, -1);
        if (priority >= 0)
        {
            if (!String.IsNullOrEmpty(where))
            {
                where += " AND ";
            }
            where += "EmailPriority=" + drpPriority.SelectedValue;
        }

        // Condition for e-mail status
        int status = ValidationHelper.GetInteger(drpStatus.SelectedValue, -1);
        if (status >= 0)
        {
            if (!string.IsNullOrEmpty(where))
            {
                where += " AND ";
            }

            where += "EmailStatus=" + drpStatus.SelectedValue;
        }

        // Condition for site
        if (!string.IsNullOrEmpty(where))
        {
            where += " AND ";
        }
        where += string.Format("(NOT EmailStatus = {0:D})", EmailStatusEnum.Archived);

        if (siteId == 0)
        {
            // Global
            where += " AND (EmailSiteID IS NULL OR  EmailSiteID = 0)";
        }
        else if (siteId > 0)
        {
            where += string.Format(" AND (EmailSiteID = {0})", siteId);
        }

        return where;
    }

    #endregion
}