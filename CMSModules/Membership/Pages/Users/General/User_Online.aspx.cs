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
using CMS.CMSHelper;
using CMS.SiteProvider;
using CMS.SettingsProvider;
using CMS.UIControls;
using CMS.LicenseProvider;

public partial class CMSModules_Membership_Pages_Users_General_User_Online : CMSUsersPage
{
    #region "Variables"

    protected string siteName = string.Empty;

    #endregion


    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Check permissions
        LicenseHelper.CheckFeatureAndRedirect(URLHelper.GetCurrentDomain(), FeatureEnum.OnlineUsers);

        // If session management is disabled informm about it
        if (!SessionManager.OnlineUsersEnabled)
        {
            this.lblDisabled.Visible = true;
            this.userFilterElem.Visible = false;
            this.gridElem.Visible = false;

            return;
        }
        else
        {
            // Refresh link
            string[,] actions = new string[1, 6];
            actions[0, 0] = HeaderActions.TYPE_HYPERLINK;
            actions[0, 1] = GetString("General.Refresh");
            actions[0, 2] = null;
            actions[0, 3] = SiteID > 0 ? ResolveUrl("user_online.aspx?siteid=" + SiteID) : ResolveUrl("user_online.aspx"); ;
            actions[0, 4] = null;
            actions[0, 5] = GetImageUrl("Design/Controls/Dialogs/refresh.png");
            CurrentMaster.HeaderActions.Actions = actions;


            // Get sitename        
            if (SiteID > 0)
            {
                SiteInfo si = SiteInfoProvider.GetSiteInfo(SiteID);
                if (si != null)
                {
                    siteName = si.SiteName;
                }
            }

            // Get count of users
            int publicUsers = 0;
            int authenticatedUsers = 0;
            SessionManager.GetUsersNumber(siteName, null, true, false, out publicUsers, out authenticatedUsers);

            this.lblGeneralInfo.Text = String.Format(GetString("OnlineUsers.GeneralInfo"), publicUsers + authenticatedUsers, publicUsers, authenticatedUsers) + "<br /><br />";

            // Get online users condition
            string usersWhere = ValidationHelper.GetString(SessionManager.GetUsersWhereCondition(null, siteName, true, true), String.Empty);

            if (!String.IsNullOrEmpty(usersWhere))
            {
                this.gridElem.ObjectType = "cms.userlist";
                this.gridElem.WhereClause = usersWhere;
            }
            else
            {
                // Clear the object type
                this.gridElem.ObjectType = string.Empty;
            }

            // Setup unigrid events
            this.gridElem.OnAction += new OnActionEventHandler(gridElem_OnAction);
            this.gridElem.OnBeforeDataReload += new OnBeforeDataReload(gridElem_OnBeforeDataReload);
            this.gridElem.OnExternalDataBound += new OnExternalDataBoundEventHandler(gridElem_OnExternalDataBound);
            this.gridElem.ZeroRowsText = GetString("general.nodatafound");
        }
    }

    #endregion


    #region "Unigrid"

    /// <summary>
    /// Sets where condition before data binding.
    /// </summary>
    protected void gridElem_OnBeforeDataReload()
    {
        if (!RequestHelper.IsAsyncPostback())
        {
            if (gridElem.QueryParameters == null)
            {
                gridElem.QueryParameters = new QueryDataParameters();
            }
            gridElem.QueryParameters.Add("@Now", DateTime.Now);
            gridElem.WhereCondition = userFilterElem.WhereCondition;
        }
    }


    /// <summary>
    ///  On action event.
    /// </summary>
    void gridElem_OnAction(string actionName, object actionArgument)
    {
        // Check "modify" permission
        if (!CMSContext.CurrentUser.IsAuthorizedPerResource("CMS.Users", "Modify"))
        {
            RedirectToAccessDenied("CMS.Users", "Modify");
        }

        int userId = ValidationHelper.GetInteger(actionArgument, 0);
        switch (actionName.ToLower())
        {
            // Kick action
            case "kick":
                SessionManager.KickUser(userId);
                break;
            // Undo kick action
            case "undokick":
                SessionManager.RemoveUserFromKicked(userId);
                break;
        }
    }


    /// <summary>
    ///  On external databound event.
    /// </summary>
    object gridElem_OnExternalDataBound(object sender, string sourceName, object parameter)
    {
        int userID = 0;
        switch (sourceName)
        {
            // Check if user was kicked and if so inform about it
            case "formattedusername":
                DataRowView drv = (DataRowView)parameter;
                if (drv != null)
                {
                    UserInfo ui = new UserInfo(drv.Row);
                    if (ui != null)
                    {
                        string userName = Functions.GetFormattedUserName(ui.UserName);
                        if (UserInfoProvider.UserKicked(ui.UserID))
                        {
                            return HTMLHelper.HTMLEncode(userName) + " <span style=\"color:#ee0000;\">" + GetString("administration.users.onlineusers.kicked") + "</span>";
                        }

                        return HTMLHelper.HTMLEncode(userName);
                    }
                }
                return "";

            // Is user enabled
            case "userenabled":
                return UniGridFunctions.ColoredSpanYesNo(parameter);

            case "kick":
                userID = ValidationHelper.GetInteger(((DataRowView)((GridViewRow)parameter).DataItem).Row["UserID"], 0);
                bool userIsAdmin = ValidationHelper.GetBoolean(((DataRowView)((GridViewRow)parameter).DataItem).Row["UserIsGlobalAdministrator"], false);

                if (UserInfoProvider.UserKicked(userID) || userIsAdmin)
                {
                    ImageButton button = ((ImageButton)sender);
                    button.ImageUrl = GetImageUrl("Design/Controls/UniGrid/Actions/Kickdisabled.png");
                    button.Enabled = false;
                }
                else
                {
                    ImageButton button = ((ImageButton)sender);
                    button.ImageUrl = GetImageUrl("Design/Controls/UniGrid/Actions/Kick.png");
                    button.Enabled = true;
                }
                return "";

            case "undokick":
                userID = ValidationHelper.GetInteger(((DataRowView)((GridViewRow)parameter).DataItem).Row["UserID"], 0);
                if (UserInfoProvider.UserKicked(userID))
                {
                    ImageButton button = ((ImageButton)sender);
                    button.ImageUrl = GetImageUrl("Design/Controls/UniGrid/Actions/Undo.png");
                    button.Enabled = true;
                }
                else
                {
                    ImageButton button = ((ImageButton)sender);
                    button.ImageUrl = GetImageUrl("Design/Controls/UniGrid/Actions/Undodisabled.png");
                    button.Enabled = false;
                }
                return "";

            default:
                return "";
        }
    }

    #endregion
}
