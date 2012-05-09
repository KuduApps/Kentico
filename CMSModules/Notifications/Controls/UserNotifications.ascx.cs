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
using CMS.Notifications;
using CMS.UIControls;
using CMS.SiteProvider;

public partial class CMSModules_Notifications_Controls_UserNotifications : CMSAdminControl
{
    #region "Variables"

    private int mUserId = 0;
    private int mSiteId = 0;
    private int mDisplayNameLength = 50;
    private string mZeroRowsText = "";

    #endregion


    #region "Properties"

    /// <summary>
    /// Maximum length of the displayname (whole display name is displayed in tooltip).
    /// </summary>
    public int DisplayNameLength
    {
        get
        {
            return this.mDisplayNameLength;
        }
        set
        {
            this.mDisplayNameLength = value;
        }
    }


    /// <summary>
    /// User id.
    /// </summary>
    public int UserId
    {
        get
        {
            return this.mUserId;
        }
        set
        {
            this.mUserId = value;
        }
    }


    /// <summary>
    /// Text displayed when no notifications exist.
    /// </summary>
    public string ZeroRowsText
    {
        get
        {
            if (this.mZeroRowsText == "")
            {
                this.mZeroRowsText = GetString("notifications.userhasnonotifications");
            }
            return this.mZeroRowsText;
        }
        set
        {
            this.mZeroRowsText = value;
        }
    }


    /// <summary>
    /// Gets or sets directory path for images.
    /// </summary>
    public string UnigridImageDirectory
    {
        get
        {
            return ValidationHelper.GetString(ViewState["UnigridImageDirectory"], null);
        }
        set
        {
            ViewState["UnigridImageDirectory"] = value;
        }
    }


    /// <summary>
    /// Site ID (If this value is set, then only subscriptions for specified site and global subscriptions are
    /// displayed, If this value equals to zero then all subscriptions are displayed).
    /// </summary>
    public int SiteID
    {
        get
        {
            return this.mSiteId;
        }
        set
        {
            this.mSiteId = value;
        }
    }

    #endregion


    #region "Page Events"

    protected void Page_Load(object sender, EventArgs e)
    {
        this.gridElem.IsLiveSite = this.IsLiveSite;

        // In design mode is pocessing of control stoped
        if (this.StopProcessing)
        {
            // Do nothing
            this.gridElem.StopProcessing = true;
            this.gridElem.Visible = false;
        }
        else
        {
            if (this.UnigridImageDirectory != null)
            {
                this.gridElem.ImageDirectoryPath = this.UnigridImageDirectory;
            }

            this.gridElem.ZeroRowsText = this.ZeroRowsText;
            this.gridElem.OnExternalDataBound += new OnExternalDataBoundEventHandler(gridElem_OnExternalDataBound);
            this.gridElem.OnAction += new OnActionEventHandler(gridElem_OnAction);
            this.gridElem.WhereCondition = "(SubscriptionUserID = " + this.UserId + (this.SiteID > 0 ? " AND (SubscriptionSiteID IS NULL OR SubscriptionSiteID = " + this.SiteID + "))" : ")");
        }
    }

    #endregion


    #region "UniGrid Events"

    /// <summary>
    /// Handles Unigrid's OnExternalDataBound event.
    /// </summary>
    protected object gridElem_OnExternalDataBound(object sender, string sourceName, object parameter)
    {
        switch (sourceName.ToLower())
        {
            case "subscriptioneventdisplayname":
                string displayName = Convert.ToString(parameter);

                if (displayName.Length <= this.DisplayNameLength)
                {
                    return HTMLHelper.HTMLEncode(displayName);
                }
                else
                {
                    return HTMLHelper.HTMLEncode(displayName.Substring(0, this.DisplayNameLength)) + " ...";
                }
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
        if (actionName == "delete")
        {
            try
            {
                if (!RaiseOnCheckPermissions(CMSAdminControl.PERMISSION_MODIFY, this))
                {
                    CurrentUserInfo cui = CMSContext.CurrentUser;
                    if ((cui == null) || ((this.UserId != cui.UserID) && !cui.IsAuthorizedPerResource("CMS.Users", CMSAdminControl.PERMISSION_MODIFY)))
                    {
                        RedirectToAccessDenied("CMS.Users", CMSAdminControl.PERMISSION_MODIFY);
                    }
                }

                NotificationSubscriptionInfoProvider.DeleteNotificationSubscriptionInfo(Convert.ToInt32(actionArgument));
            }
            catch (Exception ex)
            {
                lblError.Visible = true;
                lblError.Text = ex.Message;
            }
        }
    }

    #endregion


    /// <summary>
    /// Overriden SetValue - because of MyAccount webpart.
    /// </summary>
    /// <param name="propertyName">Name of the property to set</param>
    /// <param name="value">Value to set</param>
    public override void SetValue(string propertyName, object value)
    {
        base.SetValue(propertyName, value);

        switch (propertyName.ToLower())
        {
            case "userid":
                this.UserId = ValidationHelper.GetInteger(value, 0);
                break;
            case "unigridimagedirectory":
                this.UnigridImageDirectory = ValidationHelper.GetString(value, "");
                break;
        }
    }
}
