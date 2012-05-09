using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using CMS.UIControls;
using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.SiteProvider;
using CMS.SettingsProvider;
using CMS.ExtendedControls;

public partial class CMSModules_Membership_Pages_Users_User_Edit_Membership : CMSUsersPage
{
    #region "Variables

    private int mUserID;
    private string currentValues = String.Empty;
    protected UserInfo ui = null;


    #endregion


    #region "Properties"

    /// <summary>
    /// Current user ID.
    /// </summary>
    private int UserID
    {
        get
        {
            if (this.mUserID == 0)
            {
                this.mUserID = QueryHelper.GetInteger("userid", 0);
            }

            return this.mUserID;
        }
    }

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        CurrentUserInfo user = CMSContext.CurrentUser;
        // Check UI profile for membership
        if (!user.IsAuthorizedPerUIElement("CMS.Administration", "Membership"))
        {
            RedirectToCMSDeskUIElementAccessDenied("CMS.Administration", "Membership");
        }

        // Check "read" permission
        if (!user.IsAuthorizedPerResource("CMS.Membership", "Read"))
        {
            RedirectToAccessDenied("CMS.Membership", "Read");
        }

        ScriptHelper.RegisterJQuery(Page);
        ui = UserInfoProvider.GetUserInfo(UserID);
        CheckUserAvaibleOnSite(ui); 
        EditedObject = ui;

        if (!CheckGlobalAdminEdit(ui))
        {
            plcTable.Visible = false;
            lblErrorDeskAdmin.Text = GetString("Administration-User_List.ErrorGlobalAdmin");
            lblErrorDeskAdmin.Visible = true;
            return;
        }

        DataSet ds = MembershipUserInfoProvider.GetMembershipUsers("UserID = " + UserID, String.Empty);

        if ((SiteID > 0) && !CMSContext.CurrentUser.IsGlobalAdministrator)
        {
            this.CurrentMaster.DisplaySiteSelectorPanel = false;
        }
        else
        {
            this.CurrentMaster.DisplaySiteSelectorPanel = true;            
        }

        if (!DataHelper.DataSourceIsEmpty(ds))
        {
            currentValues = TextHelper.Join(";", SqlHelperClass.GetStringValues(ds.Tables[0], "MembershipID"));
        }

        if (!RequestHelper.IsPostBack())
        {
            // Set values
            usMemberships.Value = currentValues;
        }

        // Init uni selector
        usMemberships.SelectItemPageUrl = "~/CMSModules/Membership/Pages/Users/User_Edit_Add_Item_Dialog.aspx";
        usMemberships.ListingWhereCondition = "UserID=" + UserID;
        usMemberships.ReturnColumnName = "MembershipID";
        usMemberships.DynamicColumnName = false;
        usMemberships.GridName = "User_Membership_List.xml";
        usMemberships.OnAdditionalDataBound += new CMSAdminControls_UI_UniSelector_UniSelector.AdditionalDataBoundEventHandler(usMemberships_OnAdditionalDataBound);
        usMemberships.OnSelectionChanged += new EventHandler(usMemberships_OnSelectionChanged);
        usMemberships.AdditionalColumns = "ValidTo";

        // Init 
        int siteID = SiteID;
        if (this.CurrentMaster.DisplaySiteSelectorPanel)
        {            
            // Set site selector
            siteSelector.DropDownSingleSelect.AutoPostBack = true;
            siteSelector.AllowAll = false;
            siteSelector.AllowEmpty = false;
            siteSelector.AllowGlobal = true;
            // Only sites assigned to user
            siteSelector.UserId = UserID;
            siteSelector.OnlyRunningSites = false;
            siteSelector.UniSelector.OnSelectionChanged += new EventHandler(UniSelector_OnSelectionChanged);

            if (!RequestHelper.IsPostBack())
            {
                siteID = CMSContext.CurrentSiteID;

                // If user is member of current site
                if (UserSiteInfoProvider.GetUserSiteInfo(UserID, siteID) != null)
                {
                    // Force uniselector to preselect current site
                    siteSelector.Value = siteID;
                }
            }

            siteID = siteSelector.SiteID;
        }

        if (!String.IsNullOrEmpty(currentValues))
        {
            usMemberships.WhereCondition = "MembershipID NOT IN (" + currentValues.Replace(';', ',') + ")";
        }

        string siteWhere = (siteID <= 0) ? "MembershipSiteID IS NULL" : "MembershipSiteID =" + siteID;
        usMemberships.ListingWhereCondition = SqlHelperClass.AddWhereCondition(usMemberships.ListingWhereCondition, siteWhere);
        usMemberships.WhereCondition = SqlHelperClass.AddWhereCondition(usMemberships.WhereCondition, siteWhere);

        string script = "function setNewDateTime(date) {$j('#" + hdnDate.ClientID + "').val(date);}";
        ScriptHelper.RegisterClientScriptBlock(Page, typeof(string), "NewDateUniSelectorScript", ScriptHelper.GetScript(script));

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
                MembershipUserInfo mi = MembershipUserInfoProvider.GetMembershipUserInfo(id, UserID);
                if (mi != null)
                {
                    mi.ValidTo = dt;
                    MembershipUserInfoProvider.SetMembershipUserInfo(mi);

                    // Invalidate changes                        
                    if (ui != null)
                    {
                        ui.Invalidate();
                    }

                    this.lblInfo.Visible = true;
                }
            }
        }
    }


    /// Handles site selection change event
    /// </summary>
    protected void UniSelector_OnSelectionChanged(object sender, EventArgs e)
    {
        this.lblInfo.Visible = false;
        this.pnlUpdate.Update();
    }


    /// <summary>
    /// Callback event for create calendar icon.
    /// </summary>
    /// <param name="sender">Sender object</param>
    /// <param name="sourceName">Event source name</param>
    /// <param name="parameter">Event parameter</param>
    /// <param name="val">Value from basic external data bound event</param>
    object usMemberships_OnAdditionalDataBound(object sender, string sourceName, object parameter, string val)
    {
        switch (sourceName.ToLowerInvariant())
        {
            case "calendar":
                DataRowView drv = (parameter as DataRowView);
                string itemID = drv[usMemberships.ReturnColumnName].ToString();
                string date = drv["ValidTo"].ToString();

                string imageID = "img_" + itemID;
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

                val = "<img class=\"CalendarIcon\" Title=\"" + GetString("membership.changevalidity") + "\" Alt=\"" + GetString("membership.changevalidity") + "\"  ID= \"" + imageID + "\" src=\"" + ResolveUrl("~/CMSAdminControls/ModalCalendar/Themes/calendar.png") + "\"" + onClick + ">";
                break;
        }

        return val;
    }


    protected void usMemberships_OnSelectionChanged(object sender, EventArgs ea)
    {
        SaveData();
    }


    /// <summary>
    /// Check whether current user is allowed to modify another user.
    /// </summary>
    /// <param name="userId">Modified user</param>
    /// <returns>"" or error message.</returns>
    protected static string ValidateGlobalAndDeskAdmin(UserInfo ui)
    {
        string result = String.Empty;

        if (CMSContext.CurrentUser.IsGlobalAdministrator)
        {
            return result;
        }

        if (ui == null)
        {
            result = ResHelper.GetString("Administration-User.WrongUserId");
        }
        else
        {
            if (ui.IsGlobalAdministrator)
            {
                result = ResHelper.GetString("Administration-User.NotAllowedToModify");
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


    /// <summary>
    /// Store selected (unselected) roles.
    /// </summary>
    private void SaveData()
    {
        bool updateUser = false;
        
        // Check permission for manage membership for users
        if (!CMSContext.CurrentUser.IsAuthorizedPerResource("CMS.Membership", "ManageUserMembership"))
        {
            RedirectToAccessDenied("CMS.Membership", "Manage user membership");
        }

        this.lblInfo.Visible = false;
        string result = ValidateGlobalAndDeskAdmin(ui);
        if (result != String.Empty)
        {
            lblErrorDeskAdmin.Visible = true;
            lblErrorDeskAdmin.Text = result;
            return;
        }

        // Remove old items
        string newValues = ValidationHelper.GetString(usMemberships.Value, null);
        string items = DataHelper.GetNewItemsInList(newValues, currentValues);
        if (!String.IsNullOrEmpty(items))
        {
            string[] newItems = items.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            if (newItems != null)
            {
                foreach (string item in newItems)
                {
                    int membershipID = ValidationHelper.GetInteger(item, 0);
                    MembershipUserInfoProvider.RemoveMembershipFromUser(membershipID, UserID);
                    updateUser = true;
                }
            }
        }


        // Add new items
        items = DataHelper.GetNewItemsInList(currentValues, newValues);
        if (!String.IsNullOrEmpty(items))
        {
            string[] newItems = items.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            if (newItems != null)
            {
                DateTime dt = ValidationHelper.GetDateTime(hdnDate.Value, DateTimeHelper.ZERO_TIME);

                // Add all new items to membership
                foreach (string item in newItems)
                {
                    int membershipID = ValidationHelper.GetInteger(item, 0);
                    MembershipUserInfoProvider.AddMembershipToUser(membershipID, UserID, dt);
                    updateUser = true;
                }
            }
        }

        if (updateUser)
        {
            usMemberships.Reload(true);

            // Invalidate user object            
            if (ui != null)
            {
                ui.Invalidate();
            }

            lblInfo.Visible = true;
        }        
    }

    #endregion
}

