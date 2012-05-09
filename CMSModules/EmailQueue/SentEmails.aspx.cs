using System;
using System.Collections;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.EmailEngine;
using CMS.GlobalHelper;
using CMS.SettingsProvider;
using CMS.UIControls;

public partial class CMSModules_EmailQueue_SentEmails : SiteManagerPage, ICallbackEventHandler
{
    #region "ICallbackEventHandler Members"

    /// <summary>
    /// Gets callback result.
    /// </summary>
    public string GetCallbackResult()
    {
        mParameters = new Hashtable();
        mParameters["where"] = gridElem.WhereCondition;
        mParameters["orderby"] = gridElem.SortDirect;

        WindowHelper.Add(Identifier, mParameters);

        string queryString = "?params=" + Identifier;

        queryString = URLHelper.AddParameterToUrl(queryString, "hash", QueryHelper.GetHash(queryString));
        queryString = URLHelper.AddParameterToUrl(queryString, "emailid", EmailID.ToString());

        return queryString;
    }


    /// <summary>
    /// Raise callback method.
    /// </summary>
    public void RaiseCallbackEvent(string eventArgument)
    {
        EmailID = ValidationHelper.GetInteger(eventArgument, 0);
    }

    #endregion


    #region "Variables"

    protected int siteId;


    private Hashtable mParameters;

    #endregion


    #region "Properties"

    /// <summary>
    /// Dialog control identificator.
    /// </summary>
    private string Identifier
    {
        get
        {
            string identifier = hdnIdentificator.Value;
            if (string.IsNullOrEmpty(identifier))
            {
                identifier = Guid.NewGuid().ToString();
                hdnIdentificator.Value = identifier;
            }

            return identifier;
        }
    }


    /// <summary>
    /// Gets or sets the email id.
    /// </summary>
    private int EmailID 
    { 
        get; 
        set; 
    }

    #endregion


    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        Title = GetString("emailqueue.archive.title");
        siteId = QueryHelper.GetInteger("siteid", -1);

        // Initialize top menu
        InitializeActionMenu();

        // Load drop-down lists
        if (!RequestHelper.IsPostBack())
        {
            if (drpPriority.Items.Count <= 0)
            {
                drpPriority.Items.Add(new ListItem(GetString("general.selectall"), "-1"));
                drpPriority.Items.Add(new ListItem(GetString("emailpriority.low"), EmailPriorityEnum.Low.ToString("D")));
                drpPriority.Items.Add(new ListItem(GetString("emailpriority.normal"), EmailPriorityEnum.Normal.ToString("D")));
                drpPriority.Items.Add(new ListItem(GetString("emailpriority.high"), EmailPriorityEnum.High.ToString("D")));
            }

            btnShowFilter.ResourceString = "emailqueue.displayfilter";
            imgShowFilter.ImageUrl = GetImageUrl("Design/Controls/UniGrid/Actions/SortDown.png");
        }

        gridElem.WhereCondition = GetWhereCondition();
        gridElem.OnAction += gridElem_OnAction;
        gridElem.OnExternalDataBound += gridElem_OnExternalDataBound;

        // Register the dialog script
        ScriptHelper.RegisterDialogScript(this);

        // Register script for modal dialog with mass email recipients
        string script = @"
                          function DisplayRecipients(emailId) {
                            if ( emailId != 0 ) {
                                modalDialog('MassEmails_Recipients.aspx?emailid=' + emailId, 'emailrecipients', 920, 700); 
                            } 
                          }
                        ";
        ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "displayModal", script, true);

        // Script for opening modal dialog displaying email detail
        string openEmailDetailScript = string.Format(@"
            function OpenEmailDetail(queryParameters) {{
                modalDialog({0} + queryParameters, 'emaildetails', 920, 700);
            }}", 
            ScriptHelper.GetString(ResolveUrl("~/CMSModules/EmailQueue/EmailQueue_Details.aspx")));
        
        ScriptHelper.RegisterClientScriptBlock(this, GetType(), "Email_OpenDetail", ScriptHelper.GetScript(openEmailDetailScript));
        ScriptHelper.RegisterClientScriptBlock(this, GetType(), "Email_" + ClientID, ScriptHelper.GetScript("var emailDialogParams_" + ClientID + " = '';"));
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        // Enable/disable actions according to unigrid content
        InitializeActionMenu();
        CurrentMaster.HeaderActions.ReloadData();
    }

    #endregion


    #region "Button events"

    /// <summary>
    /// Show/Hide filter button click.
    /// </summary>
    protected void btnShowFilter_Click(object sender, EventArgs e)
    {
        // If filter is displayed then hide it
        if (plcFilter.Visible)
        {
            plcFilter.Visible = false;
            btnShowFilter.ResourceString = "emailqueue.displayfilter";
            imgShowFilter.ImageUrl = GetImageUrl("Design/Controls/UniGrid/Actions/SortDown.png");
        }
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
        gridElem.WhereCondition = GetWhereCondition();
    }

    #endregion


    #region "Unigrid events"

    /// <summary>
    /// Handles Unigrid's OnExternalDataBound event.
    /// </summary>
    protected object gridElem_OnExternalDataBound(object sender, string sourceName, object parameter)
    {
        switch (sourceName.ToLower())
        {
            case "subject":
                return HTMLHelper.HTMLEncode(parameter.ToString());

            case "priority":
                return GetEmailPriority(parameter);

            case "emailto":
                return GetEmailRecipients(parameter);
                
            case "edit":
                ImageButton viewBtn = (ImageButton)sender;
                viewBtn.OnClientClick = string.Format("emailDialogParams_{0} = '{1}';{2};return false;", 
                                                      ClientID, 
                                                      viewBtn.CommandArgument, 
                                                      Page.ClientScript.GetCallbackEventReference(this, "emailDialogParams_" + ClientID, "OpenEmailDetail", null));

                break;
        }

        return null;
    }


    /// <summary>
    /// Handles the UniGrid's OnAction event.
    /// </summary>
    /// <param name="actionName">Name of item (button) that threw event</param>
    /// <param name="actionArgument">ID (value of Primary key) of corresponding data row</param>
    protected void gridElem_OnAction(string actionName, object actionArgument)
    {
        switch (actionName.ToLower())
        {
            case "delete":                
                int deleteEmailId = ValidationHelper.GetInteger(actionArgument, 0);
                EmailHelper.Queue.Delete(deleteEmailId);                
                break;

            case "resend":                
                int sendEmailId = ValidationHelper.GetInteger(actionArgument, -1);
                EmailHelper.Queue.Send(sendEmailId);                
                break;
        }
    }


    /// <summary>
    /// Gets the email priority.
    /// </summary>
    /// <param name="parameter">The parameter</param>
    /// <returns>The e-mail priority</returns>
    private object GetEmailPriority(object parameter)
    {        
        switch ((EmailPriorityEnum)parameter)
        {
            case EmailPriorityEnum.Low:
                return GetString("emailpriority.low");

            case EmailPriorityEnum.Normal:
                return GetString("emailpriority.normal");

            case EmailPriorityEnum.High:
                return GetString("emailpriority.high");

            default:
                return string.Empty;
        }        
    }


    /// <summary>
    /// Gets the e-mail recipients.
    /// </summary>
    /// <param name="parameter">The parameter</param>
    /// <returns>The e-mail recipients</returns>
    private object GetEmailRecipients(object parameter)
    {         
        DataRowView dr = (DataRowView)parameter;
        if (ValidationHelper.GetBoolean(dr["EmailIsMass"], false))
        {
            return string.Format("<a href=\"#\" onclick=\"javascript: DisplayRecipients({0}); return false; \">{1}</a>", 
                                 ValidationHelper.GetInteger(dr["EmailID"], 0), 
                                 GetString("emailqueue.queue.massdetails"));
        }
        else
        {             
            return HTMLHelper.HTMLEncode(ValidationHelper.GetString(dr["EmailTo"], string.Empty));
        }
    }
    #endregion


    #region "Header actions events"

    protected void HeaderActions_ActionPerformed(object sender, CommandEventArgs e)
    {
        bool reloaded = false;

        switch (e.CommandName.ToLower())
        {
            case "deleteall":
                // Delete all archived e-mails
                EmailHelper.Queue.DeleteArchived(siteId);  
              
                gridElem.ReloadData();
                reloaded = true;
                break;

            case "deleteselected":
                // Delete selected e-mails
                foreach (string emailId in gridElem.SelectedItems)
                {
                    EmailHelper.Queue.Delete(ValidationHelper.GetInteger(emailId, 0));                
                }

                gridElem.ResetSelection();
                gridElem.ReloadData();
                reloaded = true;
                break;

            case "refresh":
                // Refresh grid data
                gridElem.ReloadData();
                break;
        }
        // Reload on first page if no data found after perfoming action
        if (reloaded && DataHelper.DataSourceIsEmpty(gridElem.GridView.DataSource))
        {
            gridElem.Pager.UniPager.CurrentPage = 1;
            gridElem.ReloadData();
        }
    }

    #endregion


    #region "Private methods"

    /// <summary>
    /// Initializes action menu in master page.
    /// </summary>
    private void InitializeActionMenu()
    {
        bool enabled = !DataHelper.DataSourceIsEmpty(gridElem.GridView.DataSource);

        // Resend all failed
        string[,] actions = new string[5, 10];
        actions[0, 0] = HeaderActions.TYPE_LINKBUTTON;
        actions[0, 1] = GetString("emailqueue.queue.delete");
        actions[0, 2] = enabled ? string.Format("if (!confirm({0})) return false;", ScriptHelper.GetString(GetString("EmailQueue.DeleteAllConfirmation"))) : 
                                  null;
        actions[0, 5] = enabled ? GetImageUrl("CMSModules/CMS_EmailQueue/deleteall.png") :
                                  GetImageUrl("CMSModules/CMS_EmailQueue/deleteall_disabled.png");
        actions[0, 6] = "deleteall";
        actions[0, 9] = enabled.ToString();

        // Resend all
        actions[1, 0] = HeaderActions.TYPE_LINKBUTTON;
        actions[1, 1] = GetString("emailqueue.queue.deleteselected");
        actions[1, 2] = enabled ? string.Format("if (!confirm({0})) return false;", ScriptHelper.GetString(GetString("EmailQueue.DeleteSelectedConfirmation"))) : 
                                  null;
        actions[1, 5] = enabled ? GetImageUrl("CMSModules/CMS_EmailQueue/deleteselected.png") :
                                  GetImageUrl("CMSModules/CMS_EmailQueue/deleteselected_disabled.png");
        actions[1, 6] = "deleteselected";
        actions[1, 9] = enabled.ToString();

        // Refresh
        actions[2, 0] = HeaderActions.TYPE_LINKBUTTON;
        actions[2, 1] = GetString("general.refresh");
        actions[2, 5] = GetImageUrl("CMSModules/CMS_EmailQueue/refresh.png");
        actions[2, 6] = "refresh";

        CurrentMaster.HeaderActions.Actions = actions;
        CurrentMaster.HeaderActions.ActionPerformed += HeaderActions_ActionPerformed;
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

        // EmailTo condition
        string emailTo = fltTo.FilterText.Trim();
        if (!string.IsNullOrEmpty(emailTo))
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
            if (!string.IsNullOrEmpty(where))
            {
                where += " AND ";
            }
            where += "EmailPriority=" + drpPriority.SelectedValue;
        }

        // Condition for site (and sent/archived e-mails)
        if (!string.IsNullOrEmpty(where))
        {
            where += " AND ";
        }

        where += string.Format("(EmailStatus = {0:D})", EmailStatusEnum.Archived);

        if (siteId == 0)
        {
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