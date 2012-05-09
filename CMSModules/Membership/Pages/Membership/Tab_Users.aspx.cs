using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using CMS.UIControls;
using CMS.GlobalHelper;
using CMS.SiteProvider;
using CMS.CMSHelper;
using CMS.SettingsProvider;
using CMS.ExtendedControls;


public partial class CMSModules_Membership_Pages_Membership_Tab_Users : CMSMembershipPage
{
    #region "Variables"

    /// <summary>
    /// Membership ID.
    /// </summary>
    int membershipID = 0;

    /// <summary>
    /// User IDs of edited membership.
    /// </summary>
    string currentValues = String.Empty;

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        membershipID = QueryHelper.GetInteger("membershipID", 0);
        MembershipInfo mi = MembershipInfoProvider.GetMembershipInfo(membershipID);

        EditedObject = mi;

        // Test permissions
        CheckMembershipPermissions(mi);

        usUsers.AdditionalColumns = "UserID,ValidTo";
        usUsers.GridName = "~/CMSModules/Membership/Pages/Users/UsersValidTo.xml";
        usUsers.DynamicColumnName = false;
        usUsers.SelectItemPageUrl = "~/CMSModules/Membership/Pages/Users/User_Edit_Add_Item_Dialog.aspx";
        usUsers.AdditionalUrlParameters = "&UseSendNotification=1";
        usUsers.WhereCondition = (mi.MembershipSiteID > 0) ? "UserID IN (SELECT UserID FROM CMS_UserSite WHERE SiteID=" + mi.MembershipSiteID + ")" : String.Empty;
        usUsers.ListingWhereCondition = "MembershipID =" + membershipID;
        usUsers.ReturnColumnName = "UserID";

        // Load data in administration
        currentValues = GetMembershipUsers();
        if (!RequestHelper.IsPostBack())
        {
            usUsers.Value = currentValues;
        }

        if (!String.IsNullOrEmpty(currentValues))
        {
            usUsers.WhereCondition = SqlHelperClass.AddWhereCondition(usUsers.WhereCondition, "UserID NOT IN (" + currentValues.Replace(';', ',') + ")");
        }

        string script = "function setNewDateTime(date) {$j('#" + hdnDate.ClientID + "').val(date);}";
        script += "function setNewSendNotification(sendNotification) {$j('#" + this.hdnSendNotification.ClientID + "').val(sendNotification);}";
        ScriptHelper.RegisterClientScriptBlock(Page, typeof(string), "key", ScriptHelper.GetScript(script));

        usUsers.OnSelectionChanged += UniSelector_OnSelectionChanged;
        usUsers.OnAdditionalDataBound += new CMSAdminControls_UI_UniSelector_UniSelector.AdditionalDataBoundEventHandler(usUsers_OnAdditionalDataBound);

        // Manage single item valid to change by calendar
        string eventTarget = Request["__EVENTTARGET"];
        string eventArgument = Request["__EVENTARGUMENT"];
        if (eventTarget == ucCalendar.DateTimeTextBox.UniqueID)
        {
            // Check "modify" permission
            if (!CMSContext.CurrentUser.IsAuthorizedPerResource("CMS.Membership", "ManageUserMembership"))
            {
                RedirectToAccessDenied("CMS.Membership", "Manage user membership");
            }

            int id = ValidationHelper.GetInteger(hdnDate.Value, 0);

            if (id != 0)
            {
                DateTime dt = ValidationHelper.GetDateTime(eventArgument, DateTimeHelper.ZERO_TIME);
                MembershipUserInfo mui = MembershipUserInfoProvider.GetMembershipUserInfo(mi.MembershipID, id);
                if (mui != null)
                {
                    mui.ValidTo = dt;
                    MembershipUserInfoProvider.SetMembershipUserInfo(mui);
                    UserInfo ui = UserInfoProvider.GetUserInfo(id);

                    // Invalidate changes                        
                    if (ui != null)
                    {
                        ui.Invalidate();
                    }

                    this.lblInfo.Visible = true;
                    this.lblInfo.Text = GetString("General.ChangesSaved");
                }
            }
        }
    }


    /// <summary>
    /// Callback event for create calendar icon.
    /// </summary>
    /// <param name="sender">Sender object</param>
    /// <param name="sourceName">Event source name</param>
    /// <param name="parameter">Event parameter</param>
    /// <param name="val">Value from basic external data bound event</param>
    object usUsers_OnAdditionalDataBound(object sender, string sourceName, object parameter, string val)
    {
        DataRowView drv = null;
        switch (sourceName.ToLowerInvariant())
        {
            // Resolve calendar image
            case "calendar":
                drv = (parameter as DataRowView);
                string itemID = drv[usUsers.ReturnColumnName].ToString();
                string imageID = "img_" + itemID;
                string date = drv["ValidTo"].ToString();
                string postback = ControlsHelper.GetPostBackEventReference(ucCalendar.DateTimeTextBox, "#").Replace("'#'", "$j('#" + ucCalendar.DateTimeTextBox.ClientID + "').val()");
                string onClick = String.Empty;

                ucCalendar.DateTimeTextBox.Attributes["OnChange"] = postback;

                if (!ucCalendar.UseCustomCalendar)
                {
                    onClick = " onClick=\" $j('#" + hdnDate.ClientID + "').val('" + itemID + "');" + ucCalendar.GenerateNonCustomCalendarImageEvent() + "\"";
                }
                else
                {
                    onClick = " onClick=\" $j('#" + hdnDate.ClientID + "').val('" + itemID + "'); var dt = $j('#" + ucCalendar.DateTimeTextBox.ClientID + "'); dt.val('" + date + "'); dt.datepicker('setLocation','" + imageID + "'); dt.datepicker ('show');\"";
                }

                val = "<img class=\"CalendarIcon\" Title=\"" + GetString("membership.changevalidity") + "\" Alt=\"" + GetString("membership.changevalidity") + "\" ID= \"" + imageID + "\" src=\"" + ResolveUrl("~/CMSAdminControls/ModalCalendar/Themes/calendar.png") + "\"" + onClick + ">";
                break;

            // Resolve User name
            case "name":
                drv = (parameter as DataRowView);
                string name = ValidationHelper.GetString(drv["UserName"], String.Empty);
                string fullName = ValidationHelper.GetString(drv["FullName"], String.Empty);

                val = HTMLHelper.HTMLEncode(Functions.GetFormattedUserName(name, fullName, String.Empty, false));
                break;
        }

        return val;
    }


    protected void UniSelector_OnSelectionChanged(object sender, EventArgs e)
    {
        SaveUsers();
    }


    private void SaveUsers()
    {
        // Check "manage user membership" permission
        if (!CMSContext.CurrentUser.IsAuthorizedPerResource("CMS.Membership", "ManageUserMembership"))
        {
            RedirectToAccessDenied("CMS.Membership", "Manage user membership");
        }

        // Remove old items
        string newValues = ValidationHelper.GetString(usUsers.Value, null);
        string items = DataHelper.GetNewItemsInList(newValues, currentValues);
        bool reload = false;
        bool saved = false;

        if (!String.IsNullOrEmpty(items))
        {
            string[] newItems = items.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            if (newItems != null)
            {
                // Add all new items to user
                foreach (string item in newItems)
                {
                    int userId = ValidationHelper.GetInteger(item, 0);
                    string result = ValidateGlobalAndDeskAdmin(userId);

                    if (result != String.Empty)
                    {
                        lblError.Visible = true;
                        lblError.Text += result;
                        reload = true;
                        continue;
                    }
                    else
                    {

                        MembershipUserInfoProvider.RemoveMembershipFromUser(membershipID, userId);
                        // Invalidate UserInfo
                        UserInfo ui = UserInfoProvider.GetUserInfo(userId);
                        if (ui != null)
                        {
                            ui.Invalidate();
                        }

                        saved = true;
                    }
                }
            }
        }

        // Add new items
        items = DataHelper.GetNewItemsInList(currentValues, newValues);
        if (!String.IsNullOrEmpty(items))
        {
            string[] newItems = items.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            DateTime dt = ValidationHelper.GetDateTime(hdnDate.Value, DateTimeHelper.ZERO_TIME);
            bool sendNotification = ValidationHelper.GetBoolean(this.hdnSendNotification.Value, false);

            if (newItems != null)
            {
                // Add all new items to user
                foreach (string item in newItems)
                {
                    int userId = ValidationHelper.GetInteger(item, 0);
                    string result = ValidateGlobalAndDeskAdmin(userId);

                    if (result != String.Empty)
                    {
                        lblError.Visible = true;
                        lblError.Text += result;
                        reload = true;
                        continue;
                    }
                    else
                    {
                        MembershipUserInfoProvider.AddMembershipToUser(membershipID, userId, dt, sendNotification);

                        // Invalidate UserInfo
                        UserInfo ui = UserInfoProvider.GetUserInfo(userId);
                        if (ui != null)
                        {
                            ui.Invalidate();
                        }

                        saved = true;
                    }
                }
            }
        }

        if (reload)
        {
            currentValues = GetMembershipUsers();
            usUsers.Value = currentValues;
        }

        if (saved)
        {
            lblInfo.Visible = true;
            lblInfo.Text = GetString("General.ChangesSaved");
        }

        usUsers.Reload(true);
    }


    /// <summary>
    /// Returns users related to membersihp.
    /// </summary>
    /// <returns></returns>
    private string GetMembershipUsers()
    {
        DataSet ds = MembershipUserInfoProvider.GetMembershipUsers("MembershipID = " + membershipID, null, 0, "UserID");
        if (!DataHelper.DataSourceIsEmpty(ds))
        {
            return TextHelper.Join(";", SqlHelperClass.GetStringValues(ds.Tables[0], "UserID"));
        }

        return String.Empty;
    }


    /// <summary>
    /// Check whether current user is allowed to modify another user. Return "" or error message.
    /// </summary>
    /// <param name="userId">Modified user</param>
    protected string ValidateGlobalAndDeskAdmin(int userId)
    {
        string result = String.Empty;

        if (CMSContext.CurrentUser.IsGlobalAdministrator)
        {
            return result;
        }

        UserInfo userInfo = UserInfoProvider.GetUserInfo(userId);
        if (userInfo == null)
        {
            result = GetString("Administration-User.WrongUserId");
        }
        else
        {
            if (userInfo.IsGlobalAdministrator)
            {
                result = String.Format(GetString("Administration-User.NotAllowedToModifySpecific"), userInfo.FullName + " (" + userInfo.UserName + ")");
            }
        }
        return result;
    }


    protected override void OnPreRender(EventArgs e)
    {
        if (RequestHelper.IsPostBack())
        {
            pnlBasic.Update();
        }

        base.OnPreRender(e);
    }

    #endregion
}

