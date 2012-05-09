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

using CMS.GlobalHelper;
using CMS.SiteProvider;
using CMS.CMSHelper;
using CMS.EmailEngine;
using CMS.WebAnalytics;
using CMS.UIControls;
using CMS.SettingsProvider;
using CMS.EventLog;

public partial class CMSModules_Membership_Pages_Users_General_User_WaitingForApproval : CMSUsersPage
{
    #region "Methods"

    /// <summary>
    /// Page load.
    /// </summary>
    protected void Page_Load(object sender, EventArgs e)
    {
        // Pagetitle
        this.Title = "Users - Waiting for approval";

        // Set the master page header
        this.CurrentMaster.Title.TitleText = GetString("administration.users_header.myapproval");
        this.CurrentMaster.Title.TitleImage = GetImageUrl("Objects/CMS_User/waitingforapproval.png");

        // Set grid list properties
        gridElem.OnAction += new OnActionEventHandler(gridElem_OnAction);
        gridElem.OnBeforeDataReload += new OnBeforeDataReload(gridElem_OnBeforeDataReload);
        gridElem.OnExternalDataBound += new OnExternalDataBoundEventHandler(gridElem_OnExternalDataBound);
        gridElem.ZeroRowsText = GetString("general.nodatafound");

        // Setup buttons       
        this.btnApproveAllSelected.Attributes.Add("onclick", "if(confirm(" + ScriptHelper.GetString(GetString("administration.users.approveallselectedquestion")) + ")){}else{return false}");
        this.ltlScript.Text = "";

        // Register script for javascript postback
        ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "UpdatePage", ScriptHelper.GetScript(
                "function UpdatePage(){ " + this.Page.ClientScript.GetPostBackEventReference(this.btnUpdate, "") + "; } \n"));

        // Register dialog script for modal window
        ScriptHelper.RegisterDialogScript(this);

        // Register opener for modal window
        ScriptHelper.RegisterClientScriptBlock(this, this.GetType(), "OpenReject",
            ScriptHelper.GetScript(
                "function OpenReject(user)\n" +
                "{\n" +
                "document.getElementById('" + this.hdnUser.ClientID + "').value = user;  \n" +
                "modalDialog('User_Reject.aspx', 'UserReject', 650, 480); \n" +
                "return false;\n" +
                "}\n"));


        // Register script for recieving data from modal window
        ScriptHelper.RegisterClientScriptBlock(this, this.GetType(), "SetRejectParam",
            ScriptHelper.GetScript(
                "function SetRejectParam(reason, email, confirmDelete)\n" +
                "{\n" +
                    "document.getElementById('" + this.hdnReason.ClientID + "').value = reason;\n" +
                    "document.getElementById('" + this.hdnSendEmail.ClientID + "').value  = email;\n" +
                    "document.getElementById('" + this.hdnConfirmDelete.ClientID + "').value  = confirmDelete;\n" +
                    "UpdatePage();\n" +
                "}\n"));

        this.btnRejectAllSelected.OnClientClick = "OpenReject(); return false;";
    }


    /// <summary>
    /// Page pre render.
    /// </summary>
    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (gridElem.GridView.Rows.Count < 1)
        {
            this.btnApproveAllSelected.Visible = false;
            this.btnRejectAllSelected.Visible = false;
        }
        else
        {
            this.btnApproveAllSelected.Visible = true;
            this.btnRejectAllSelected.Visible = true;
        }
    }


    /// <summary>
    /// Sets grid where condition before data binding.
    /// </summary>
    protected void gridElem_OnBeforeDataReload()
    {
        if (!RequestHelper.IsAsyncPostback())
        {
            // Add now parameter
            if (gridElem.QueryParameters == null)
            {
                gridElem.QueryParameters = new QueryDataParameters();
            }
            gridElem.QueryParameters.Add("@Now", DateTime.Now);

            gridElem.WhereCondition = GetWhereCondition();
        }
    }


    private string GetWhereCondition()
    {
        string whereCondition = "(UserWaitingForApproval = 1)";

        // Get site related condition
        if (SiteID != 0)
        {
            whereCondition += " AND (UserID IN (SELECT UserID FROM CMS_UserSite WHERE SiteID=" + SiteID + "))";
        }

        // Get filter where condition
        if (!string.IsNullOrEmpty(this.userFilterElem.WhereCondition))
        {
            whereCondition += "AND (" + this.userFilterElem.WhereCondition + ")";
        }

        return whereCondition;
    }


    /// <summary>
    /// Handles Unigrid's OnExternalDataBound event.
    /// </summary>
    protected object gridElem_OnExternalDataBound(object sender, string sourceName, object parameter)
    {
        switch (sourceName.ToLower())
        {
            case "userenabled":
                return UniGridFunctions.ColoredSpanYesNo(parameter);

            case "formattedusername":
                return HTMLHelper.HTMLEncode(Functions.GetFormattedUserName(Convert.ToString(parameter)));

        }
        return parameter;
    }


    /// <summary>
    /// Handles the UniGrid's OnAction event.
    /// </summary>
    /// <param name="actionName">Name of item (button) that threw event</param>
    /// <param name="actionArgument">ID (value of Primary key) of corresponding data row</param>
    protected void gridElem_OnAction(string actionName, object actionArgument)
    {
        // Check "modify" permission
        if (!CMSContext.CurrentUser.IsAuthorizedPerResource("CMS.Users", "Modify"))
        {
            RedirectToAccessDenied("CMS.Users", "Modify");
        }

        // Approve action
        if (actionName == "approve")
        {
            this.ApproveUsers(ValidationHelper.GetInteger(actionArgument, 0));
        }
    }


    /// <summary>
    /// Approves all selected users.
    /// </summary>
    protected void btnApproveAllClick(object sender, EventArgs e)
    {
        // Check "modify" permission
        if (!CMSContext.CurrentUser.IsAuthorizedPerResource("CMS.Users", "Modify"))
        {
            RedirectToAccessDenied("CMS.Users", "Modify");
        }

        if (this.gridElem.SelectedItems.Count > 0)
        {
            ApproveUsers(0);
        }
        else
        {
            this.ltlScript.Text = ScriptHelper.GetScript("alert('" + GetString("administration.users.nousers") + "');");
        }
    }


    /// <summary>
    /// Reject all selected users.
    /// </summary>
    protected void btnRejectAllClick(object sender, EventArgs e)
    {
        // Check "modify" permission
        if (!CMSContext.CurrentUser.IsAuthorizedPerResource("CMS.Users", "Modify"))
        {
            RedirectToAccessDenied("CMS.Users", "Modify");
        }

        this.hdnUser.Value = "0";
    }


    /// <summary>
    /// Approve users.
    /// </summary>
    /// <param name="actionArgument">User ID</param>
    protected void ApproveUsers(int actionArgument)
    {
        // Process single user
        if (actionArgument > 0)
        {
            SetUserInfo(actionArgument);
        }
        // Process all selected users
        else
        {
            // Get selected users
            ArrayList users = this.gridElem.SelectedItems;

            // Set user activation properties
            foreach (string userID in users)
            {
                SetUserInfo(ValidationHelper.GetInteger(userID, 0));
            }
        }
        this.gridElem.ResetSelection();
        this.gridElem.ReloadData();
    }


    /// <summary>
    /// Reject users.
    /// </summary>
    /// <param name="actionArgument">User ID</param>
    private void RejectUsers(int actionArgument)
    {
        // Process single user
        if (actionArgument > 0)
        {
            DeleteUser(actionArgument);
        }
        // Process all selected users
        else
        {
            // Get selected users
            ArrayList users = this.gridElem.SelectedItems;

            // Set user activation properties
            foreach (string userID in users)
            {
                DeleteUser(ValidationHelper.GetInteger(userID, 0));
            }
        }
        // Reload data
        this.hdnReason.Value = "";
        this.hdnSendEmail.Value = "";
        this.hdnConfirmDelete.Value = "";
        this.hdnUser.Value = "";
        this.gridElem.ResetSelection();
        this.gridElem.ReloadData();
    }


    /// <summary>
    /// Sets new UserInfo for approved user.
    /// </summary>
    /// <param name="userID">User to be approved</param>
    protected void SetUserInfo(int userID)
    {
        UserInfo user = UserInfoProvider.GetFullUserInfo(userID);

        // Cancel waiting for approval attribute
        user.UserSettings.UserWaitingForApproval = false;
        // Set activation time to now
        user.UserSettings.UserActivationDate = DateTime.Now;
        // Set user who activated this account
        user.UserSettings.UserActivatedByUserID = CMSContext.CurrentUser.UserID;
        // Enable user
        user.UserEnabled = true;

        UserInfoProvider.SetUserInfo(user);

        // Send e-mail to user
        if (!String.IsNullOrEmpty(user.Email))
        {
            EmailTemplateInfo template = EmailTemplateProvider.GetEmailTemplate("RegistrationUserApproved", CMSContext.CurrentSiteName);
            if (template != null)
            {
                EmailMessage email = new EmailMessage();
                email.EmailFormat = EmailFormatEnum.Default;
                // Get e-mail sender and subject from template, if used
                email.From = EmailHelper.GetSender(template, SettingsKeyProvider.GetStringValue(CMSContext.CurrentSiteName + ".CMSNoreplyEmailAddress"));
                email.Recipients = user.Email;

                string[,] replacements = new string[1, 2];
                // Prepare macro replacements
                replacements[0, 0] = "homepageurl";
                replacements[0, 1] = URLHelper.GetAbsoluteUrl("~/");

                MacroResolver resolver = CMSContext.CurrentResolver;
                resolver.EncodeResolvedValues = true;
                resolver.SourceParameters = replacements;
                email.Body = resolver.ResolveMacros(template.TemplateText);

                resolver.EncodeResolvedValues = false;
                string emailSubject = EmailHelper.GetSubject(template, GetString("registrationform.registrationapprovalemailsubject"));
                email.Subject = resolver.ResolveMacros(emailSubject);

                email.PlainTextBody = resolver.ResolveMacros(template.TemplatePlainText);

                email.CcRecipients = template.TemplateCc;
                email.BccRecipients = template.TemplateBcc;

                try
                {
                    // Add attachments
                    MetaFileInfoProvider.ResolveMetaFileImages(email, template.TemplateID, EmailObjectType.EMAILTEMPLATE, MetaFileInfoProvider.OBJECT_CATEGORY_TEMPLATE);
                    EmailSender.SendEmail(CMSContext.CurrentSiteName, email);
                }
                catch
                {
                    EventLogProvider ev = new EventLogProvider();
                    ev.LogEvent("E", DateTime.Now, "Membership", "WaitingForApprovalEmail", CMSContext.CurrentSite.SiteID);
                }
            }
            else
            {
                // Log missing e-mail template
                try
                {
                    EventLogProvider el = new EventLogProvider();
                    el.LogEvent("E", DateTime.Now, "RegistrationUserApproved", "GetEmailTemplate", HTTPHelper.GetAbsoluteUri());
                }
                catch
                {
                }
            }
        }

        // User is approved and enabled, could be logged into statistics
        AnalyticsHelper.LogRegisteredUser(CMSContext.CurrentSiteName, user);
    }


    /// <summary>
    /// Rejects and deletes user.
    /// </summary>
    /// <param name="userID">User to be rejected</param>
    protected void DeleteUser(int userID)
    {
        // Find user
        UserInfo user = UserInfoProvider.GetFullUserInfo(userID);

        // Send e-mail if requested
        if (hdnSendEmail.Value == "true")
        {
            EmailMessage em = new EmailMessage();

            // Set message content
            em.From = SettingsKeyProvider.GetStringValue(CMSContext.CurrentSiteName + ".CMSNoreplyEmailAddress");
            em.Recipients = user.Email;
            em.Subject = GetString("administration.users.rejected");
            em.EmailFormat = EmailFormatEnum.Default;

            string emailBody = this.hdnReason.Value.Trim();

            em.Body = HTMLHelper.HTMLEncode(emailBody);
            em.PlainTextBody = emailBody;

            // Send message
            EmailSender.SendEmail(em);
        }

        // Delete user
        SessionManager.RemoveUser(userID);
        UserInfoProvider.DeleteUser(userID);
    }


    /// <summary>
    /// Reject all selected users.
    /// </summary>
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        // Check "modify" permission
        if (!CMSContext.CurrentUser.IsAuthorizedPerResource("CMS.Users", "Modify"))
        {
            RedirectToAccessDenied("CMS.Users", "Modify");
        }

        // Run reject function
        if ((this.hdnConfirmDelete.Value != null) && (this.hdnConfirmDelete.Value != ""))
        {
            this.RejectUsers(ValidationHelper.GetInteger(hdnUser.Value, 0));
        }
    }

    #endregion
}
