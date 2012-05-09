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
using CMS.Ecommerce;
using CMS.UIControls;
using CMS.SettingsProvider;
using CMS.LicenseProvider;

public partial class CMSModules_Ecommerce_Pages_Administration_Users_User_Edit_Departments : CMSUsersPage
{
    #region "Variables"

    protected int siteId = 0;
    protected int userId = 0;
    protected string currentValues = string.Empty;

    #endregion


    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Check the license
        LicenseHelper.CheckFeatureAndRedirect(URLHelper.GetCurrentDomain(), FeatureEnum.Ecommerce);

        bool ecommerceOnSite = ResourceSiteInfoProvider.IsResourceOnSite("CMS.Ecommerce", CMSContext.CurrentSiteName);

        // Check 'ConfigurationRead' permission
        if (!ecommerceOnSite || !CMSContext.CurrentUser.IsAuthorizedPerResource("CMS.Ecommerce", "ConfigurationRead"))
        {
            RedirectToAccessDenied("CMS.Ecommerce", "ConfigurationRead");
        }

        // Check 'ConfigurationModify' permission
        if (!CMSContext.CurrentUser.IsAuthorizedPerResource("CMS.Ecommerce", "ConfigurationModify"))
        {
            this.uniSelector.Enabled = false;
        }

        userId = QueryHelper.GetInteger("userid", 0);

        // Show contentplaceholder where site selector can be shown
        this.CurrentMaster.DisplaySiteSelectorPanel = true;

        if ((SiteID > 0) && !CMSContext.CurrentUser.IsGlobalAdministrator)
        {
            this.plcSites.Visible = false;
            this.CurrentMaster.DisplaySiteSelectorPanel = false;
        }

        if (userId > 0)
        {
            // Check that only global administrator can edit global administrator's accouns
            UserInfo ui = UserInfoProvider.GetUserInfo(userId);
            EditedObject = ui;

            if (!CheckGlobalAdminEdit(ui))
            {
                plcTable.Visible = false;
                lblError.Text = GetString("Administration-User_List.ErrorGlobalAdmin");
                lblError.Visible = true;
                return;
            }

            // Set site selector
            siteSelector.DropDownSingleSelect.AutoPostBack = true;
            siteSelector.AllowAll = false;
            siteSelector.AllowEmpty = false;

            // Global departments only for global admin
            if (CMSContext.CurrentUser.UserSiteManagerAdmin)
            {
                siteSelector.AllowGlobal = true;
            }

            // Only sites assigned to user
            siteSelector.UserId = userId;
            siteSelector.OnlyRunningSites = false;
            siteSelector.UniSelector.OnSelectionChanged += new EventHandler(UniSelector_OnSelectionChanged);

            if (!RequestHelper.IsPostBack())
            {
                siteId = CMSContext.CurrentSiteID;

                // If user is member of current site
                if (UserSiteInfoProvider.GetUserSiteInfo(userId, siteId) != null)
                {
                    // Force uniselector to preselect current site
                    siteSelector.Value = siteId;
                }

                // Force to load data
                siteSelector.UpdateWhereCondition();
                siteSelector.Reload(true);
            }

            // Get truly selected item
            siteId = ValidationHelper.GetInteger(siteSelector.Value, 0);

            DataSet ds = DepartmentInfoProvider.GetUserDepartments(userId);
            if (!DataHelper.DataSourceIsEmpty(ds))
            {
                currentValues = TextHelper.Join(";", SqlHelperClass.GetStringValues(ds.Tables[0], "DepartmentID"));
            }

            // If not postback or site selection changed
            if (!RequestHelper.IsPostBack() || (siteId != Convert.ToInt32(ViewState["departmentsOldSiteId"])))
            {
                // Set values
                uniSelector.Value = currentValues;
            }

            // Store selected site id
            ViewState["departmentsOldSiteId"] = siteId;
        }
        uniSelector.ButtonAddItems.Text = GetString("general.ok");
        uniSelector.IconPath = GetImageUrl("Objects/Ecommerce_Department/object.png");
        uniSelector.OnSelectionChanged += usSites_OnSelectionChanged;

        uniSelector.WhereCondition = (siteId <= 0) ? " DepartmentSiteID IS NULL " : " DepartmentSiteID =" + siteId;
        if (siteId > 0)
        {
            string siteName = SiteInfoProvider.GetSiteName(siteId);
            if (ECommerceSettings.AllowGlobalDepartments(siteName))
            {
                uniSelector.WhereCondition += " OR (DepartmentSiteID IS NULL)";
            }
        }
    }

    #endregion


    #region "Events"

    /// <summary>
    /// Handles site selection change event.
    /// </summary>
    protected void UniSelector_OnSelectionChanged(object sender, EventArgs e)
    {
        this.lblInfo.Visible = false;
        this.pnlUpdate.Update();
    }


    protected void usSites_OnSelectionChanged(object sender, EventArgs e)
    {
        // check 'ConfigurationModify' permission
        if (!CMSContext.CurrentUser.IsAuthorizedPerResource("CMS.Ecommerce", "ConfigurationModify"))
        {
            RedirectToAccessDenied("CMS.Ecommerce", "ConfigurationModify");
        }

        string result = ValidateGlobalAndDeskAdmin(userId);

        if (result != String.Empty)
        {
            lblError.Visible = true;
            lblError.Text = result;
            return;
        }

        // Remove old items
        string newValues = ValidationHelper.GetString(uniSelector.Value, null);
        string items = DataHelper.GetNewItemsInList(newValues, currentValues);
        if (!String.IsNullOrEmpty(items))
        {
            string[] newItems = items.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            if (newItems != null)
            {
                // Add all new items to site
                foreach (string item in newItems)
                {
                    int departmentId = ValidationHelper.GetInteger(item, 0);
                    UserDepartmentInfoProvider.RemoveUserFromDepartment(departmentId, userId);
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
                // Add all new items to site
                foreach (string item in newItems)
                {
                    int departmentId = ValidationHelper.GetInteger(item, 0);
                    UserDepartmentInfoProvider.AddUserToDepartment(departmentId, userId);
                }
            }
        }

        lblInfo.Visible = true;
        lblInfo.Text = GetString("General.ChangesSaved");
    }

    #endregion


    #region "Methods"

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
                result = GetString("Administration-User.NotAllowedToModify");
            }
        }
        return result;
    }

    #endregion
}
