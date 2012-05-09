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
using System.Data.Sql;

using CMS.GlobalHelper;
using CMS.SiteProvider;
using CMS.CMSHelper;
using CMS.LicenseProvider;
using CMS.UIControls;
using CMS.SettingsProvider;

public partial class CMSModules_Membership_Pages_Users_User_Edit_Sites : CMSUsersPage
{
    #region "Variables"

    private int userId = 0;
    private string currentValues = string.Empty;
    private UserInfo ui = null;

    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        lblAvialable.Text = GetString("User_Sites.Available");

        // Get user ID
        userId = QueryHelper.GetInteger("userid", 0);
        if (userId > 0)
        {
            // Check that only global administrator can edit global administrator's accouns
            ui = UserInfoProvider.GetUserInfo(userId);
            EditedObject = ui;
            CheckUserAvaibleOnSite(ui); 

            if (!CheckGlobalAdminEdit(ui))
            {
                plcTable.Visible = false;
                lblError.Text = GetString("Administration-User_List.ErrorGlobalAdmin");
                lblError.Visible = true;
            }
            else
            {
                // Get the user sites
                currentValues = GetUserSites();

                if (!RequestHelper.IsPostBack())
                {
                    usSites.Value = currentValues;
                }

                usSites.OnSelectionChanged += usSites_OnSelectionChanged;
            }
        }
    }


    /// <summary>
    /// Returns string with site ids where user is member.
    /// </summary>    
    private string GetUserSites()
    {
        DataSet ds = UserSiteInfoProvider.GetUserSites("SiteID", "UserID = " + userId, null, 0);
        if (!DataHelper.DataSourceIsEmpty(ds))
        {
            return TextHelper.Join(";", SqlHelperClass.GetStringValues(ds.Tables[0], "SiteID"));
        }

        return String.Empty;
    }


    /// <summary>
    /// Handles site selector selection change event.
    /// </summary>
    protected void usSites_OnSelectionChanged(object sender, EventArgs e)
    {
        SaveSites();
    }


    protected void SaveSites()
    {
        // Check "modify" permission
        if (!CMSContext.CurrentUser.IsAuthorizedPerResource("CMS.Users", "Modify"))
        {
            RedirectToAccessDenied("CMS.Users", "Modify");
        }

        // Remove old items
        string newValues = ValidationHelper.GetString(usSites.Value, null);
        string items = DataHelper.GetNewItemsInList(newValues, currentValues);
        if (!String.IsNullOrEmpty(items))
        {
            string[] newItems = items.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            if (newItems != null)
            {
                // Add all new items to site
                foreach (string item in newItems)
                {
                    int siteId = ValidationHelper.GetInteger(item, 0);

                    // remove user from the site
                    UserInfoProvider.RemoveUserFromSite(userId, siteId);

                    // Remove user's allowed cultures for this site
                    UserCultureInfoProvider.RemoveUserFromSite(userId, siteId);
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
                bool falseValues = false;

                // Add all new items to site
                foreach (string item in newItems)
                {
                    int siteId = ValidationHelper.GetInteger(item, 0);

                    SiteInfo si = SiteInfoProvider.GetSiteInfo(siteId);
                    if (si != null)
                    {
                        if (ui.IsEditor)
                        {
                            if (!UserInfoProvider.LicenseVersionCheck(si.DomainName, FeatureEnum.Editors, VersionActionEnum.Insert, false))
                            {
                                falseValues = true;
                                lblError.Visible = true;
                                lblError.Text = GetString("LicenseVersion.EditorSite");
                                continue;
                            }
                        }

                        if (!UserInfoProvider.LicenseVersionCheck(si.DomainName, FeatureEnum.SiteMembers, VersionActionEnum.Insert, false))
                        {
                            falseValues = true;
                            lblError.Visible = true;
                            lblError.Text = GetString("LicenseVersion.SiteMembersSite");
                            continue;
                        }

                        // Check if email would be unique in site
                        if (UserInfoProvider.IsEmailUnique(ui.Email, si.SiteName, ui.UserID))
                        {
                            UserSiteInfoProvider.AddUserToSite(ui, si);
                        }
                        else
                        {
                            falseValues = true;
                            lblError.Visible = true;
                            lblError.Text = GetString("UserInfo.EmailAlreadyExistInSomeSite");
                        }
                    }
                }

                // If some of sites could not be assigned reload selector value
                if (falseValues)
                {
                    usSites.Value = GetUserSites(); 
                    usSites.Reload(true);
                }
            }
        }

        lblInfo.Visible = true;
        lblInfo.Text = GetString("General.ChangesSaved");
    }
}
