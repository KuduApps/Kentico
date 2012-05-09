using System;
using System.Web.UI.WebControls;

using CMS.CMSHelper;
using CMS.Community;
using CMS.ExtendedControls;
using CMS.GlobalHelper;
using CMS.SettingsProvider;
using CMS.TreeEngine;
using CMS.UIControls;
using CMS.WebAnalytics;
using CMS.PortalEngine;

using TreeNode = CMS.TreeEngine.TreeNode;

public partial class CMSModules_Groups_Controls_GroupLeave : CMSAdminControl
{
    #region "Variables"

    private string mLeaveText;
    private string mSuccessfulLeaveText;
    private string mUnSuccessfulLeaveText;
    private GroupInfo mGroup = null;
    private bool mIsOnModalPage = true;
    private CMSButton mLeaveButton = null;
    private CMSButton mCancelButton = null;

    #endregion


    #region "Private properties"
    
    /// <summary>
    /// Returns group name of current group.
    /// </summary>
    private string GroupName
    {
        get
        {
            if (this.Group != null)
            {
                return " " + this.Group.GroupDisplayName;
            }

            return "";
        }
    }

    #endregion


    #region "Public properties"

    /// <summary>
    /// Gets or sets the text which should be displayed on join dialog.
    /// </summary>
    public string LeaveText
    {
        get
        {
        	 return DataHelper.GetNotEmpty(mLeaveText, GetString("Community.Group.Leave") + HTMLHelper.HTMLEncode(this.GroupName) + "?"); 
        }
        set
        {
        	 mLeaveText = value; 
        }
    }


    /// <summary>
    /// Gets or sets the text which should be displayed on join dialog after successful join action.
    /// </summary>
    public string SuccessfulLeaveText
    {
        get
        {
        	 return DataHelper.GetNotEmpty(mSuccessfulLeaveText, GetString("Community.Group.SuccessfulLeave").Replace("##GroupName##", HTMLHelper.HTMLEncode(this.GroupName))); 
        }
        set
        {
        	 mSuccessfulLeaveText = value; 
        }
    }


    /// <summary>
    /// Gets or sets the text which should be displayed on join dialog if join actin was unsuccessful.
    /// </summary>
    public string UnSuccessfulLeaveText
    {
        get
        {
        	 return DataHelper.GetNotEmpty(mUnSuccessfulLeaveText, GetString("Community.Group.UnSuccessfulLeave")); 
        }
        set
        {
        	 mUnSuccessfulLeaveText = value; 
        }
    }


    /// <summary>
    /// Gets or sets the group info object for destination group.
    /// </summary>
    public GroupInfo Group
    {
        get
        {
            return mGroup;
        }
        set
        {
            mGroup = value;
        }
    }

    
    /// <summary>
    /// Specifies if control is placed on modal page or not.
    /// </summary>
    public bool IsOnModalPage
    {
        get
        {
        	 return mIsOnModalPage; 
        }
        set
        {
        	 mIsOnModalPage = value; 
        }
    }


    /// <summary>
    /// Indicates if control buttons should be displayed.
    /// </summary>
    public bool DisplayButtons
    {
        get
        {
            return plcButtons.Visible;
        }
        set
        {
            plcButtons.Visible = value;
        }
    }


    /// <summary>
    /// Leave button.
    /// </summary>
    public CMSButton LeaveButton
    {
        get
        {
            if (mLeaveButton == null)
            {
                mLeaveButton = btnLeave;
            }
            return mLeaveButton;
        }
        set
        {
            mLeaveButton = value;
        }
    }


    /// <summary>
    /// Cancel button.
    /// </summary>
    public CMSButton CancelButton
    {
        get
        {
            if (mCancelButton == null)
            {
                mCancelButton = btnCancel;
            }
            return mCancelButton;
        }
        set
        {
            mCancelButton = value;
        }
    }

    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptHelper.RegisterWOpenerScript(Page);
   
        LeaveButton.Click += new EventHandler(btnLeave_Click);
        LeaveButton.Text = GetString("General.Yes");
        CancelButton.Text = GetString("General.No");

        // Set up js action if webpart is placed on modal page
        if (this.IsOnModalPage)
        {
            CancelButton.OnClientClick = "window.close()";
        }
        else
        {
            // Get return url
            string returnUrl = QueryHelper.GetString("returnurl", "") ;
            if (!string.IsNullOrEmpty(returnUrl))
            {
                // Redirect
                URLHelper.Redirect(returnUrl);
            }
        }
        lblInfo.Text = this.LeaveText;
    }


    /// <summary>
    /// Join handler.
    /// </summary>
    /// <param name="sender">Sender</param>
    /// <param name="e">EventArgs</param>
    protected void btnLeave_Click(object sender, EventArgs e)
    {
        LeaveButton.Visible = false;
        // Set up js action if webpart is placed on modal page
        if (this.IsOnModalPage)
        {
            CancelButton.Text = GetString("General.Close");
            CancelButton.OnClientClick = "if (wopener != null) {wopener.ReloadPage();} window.close();";
        }
        else
        {
            CancelButton.Text = GetString("General.Ok");
            CancelButton.Click += new EventHandler(btnCancel_Click);            
        }

        if (this.Group == null)
        {
            return;
        }

        // Get group member info        
        GroupMemberInfo gmi = GroupMemberInfoProvider.GetGroupMemberInfo(CMSContext.CurrentUser.UserID, this.Group.GroupID);
        if (gmi != null)
        {
            GroupMemberInfoProvider.DeleteGroupMemberInfo(gmi);

            // Log activity
            LogLeaveActivity(gmi, this.Group.GroupLogActivity, this.Group.GroupDisplayName);

            if (this.Group.GroupSendJoinLeaveNotification)
            {
                GroupMemberInfoProvider.SendNotificationMail("Groups.MemberLeave", CMSContext.CurrentSiteName, gmi, true);
            }

            this.lblInfo.Text = this.SuccessfulLeaveText;
            return;
        }
        

        this.lblInfo.Text = this.SuccessfulLeaveText;
    }


    /// <summary>
    /// Cancel click.
    /// </summary>    
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        URLHelper.Redirect(URLHelper.CurrentURL);
    }


    /// <summary>
    /// Log activity
    /// </summary>
    /// <param name="gmi">Member info</param>
    /// <param name="logActivity">Determines whether activity logging is enabled for current group</param>
    /// <param name="groupDisplayName">Display name of the group</param>
    private void LogLeaveActivity(GroupMemberInfo gmi, bool logActivity, string groupDisplayName)
    {
        string siteName = CMSContext.CurrentSiteName;
        if (!logActivity || (CMSContext.ViewMode != ViewModeEnum.LiveSite) || (gmi == null) || !ActivitySettingsHelper.ActivitiesEnabledAndModuleLoaded(siteName)
            || !ActivitySettingsHelper.ActivitiesEnabledForThisUser(CMSContext.CurrentUser) || !ActivitySettingsHelper.JoiningAGroupEnabled(siteName))
        {
            return;
        }

        TreeNode currentDoc = CMSContext.CurrentDocument;
        var data = new ActivityData()
        {
            ContactID = ModuleCommands.OnlineMarketingGetCurrentContactID(),
            SiteID = CMSContext.CurrentSiteID,
            Type = PredefinedActivityType.LEAVE_GROUP,
            TitleData = groupDisplayName,
            ItemID = gmi.MemberGroupID,
            URL = URLHelper.CurrentRelativePath,
            NodeID = (currentDoc != null ? currentDoc.NodeID : 0),
            Culture = (currentDoc != null ? currentDoc.DocumentCulture : null),
            Campaign = CMSContext.Campaign
        };
        ActivityLogProvider.LogActivity(data);
    }
}