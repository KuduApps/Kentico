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
using CMS.Forums;
using CMS.CMSHelper;
using CMS.UIControls;
using CMS.ExtendedControls;
using CMS.SettingsProvider;

public partial class CMSModules_Forums_Controls_Forums_ForumModerators : CMSAdminEditControl
{
    #region "Variables"

    protected int mForumId = 0;
    protected ForumInfo forum = null;
    private string currentValues = String.Empty;

    #endregion


    #region "Public properties"

    /// <summary>
    /// Gets or sets the ID of the forum to edit.
    /// </summary>
    public int ForumID
    {
        get
        {
            return this.mForumId;
        }
        set
        {
            this.mForumId = value;
        }
    }

    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        bool process = true;

        if (!this.Visible || this.StopProcessing)
        {
            this.EnableViewState = true;
            process = false;
        }

        this.lblModerators.Text = GetString("Forum_Edit.Moderators");
        this.chkForumModerated.Text = GetString("Forum_Edit.ForumModerated");

        // Get community group id
        int communityGroupID = 0;
        forum = ForumInfoProvider.GetForumInfo(this.mForumId);

        if (forum == null)
        {
            return;
        }

        if (forum.ForumGroupID > 0)
        {
            ForumGroupInfo fgi = ForumGroupInfoProvider.GetForumGroupInfo(forum.ForumGroupID);
            if (fgi != null)
            {
                communityGroupID = fgi.GroupGroupID;
            }
        }

        userSelector.ForumID = this.ForumID;
        userSelector.GroupID = communityGroupID;
        userSelector.CurrentSelector.SelectionMode = SelectionModeEnum.Multiple;
        userSelector.ShowSiteFilter = false;
        userSelector.SiteID = CMSContext.CurrentSiteID;
        userSelector.IsLiveSite = this.IsLiveSite;
        userSelector.Changed += userSelector_Changed;

        if (!this.IsLiveSite && process)
        {
            ReloadData(false);
        }
    }


    /// <summary>
    /// Selector changed.
    /// </summary>
    void userSelector_Changed(object sender, EventArgs e)
    {
        currentValues = GetModerators();

        // Remove old items
        string newValues = ValidationHelper.GetString(userSelector.CurrentSelector.Value, null);
        string items = DataHelper.GetNewItemsInList(newValues, currentValues);

        if (!String.IsNullOrEmpty(items))
        {
            string[] newItems = items.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            if (newItems != null)
            {
                foreach (string item in newItems)
                {
                    int userId = ValidationHelper.GetInteger(item, 0);
                    if (userId > 0)
                    {
                        ForumInfoProvider.RemoveModerator(userId, ForumID);
                    }
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
                // Add all new items to forum
                foreach (string item in newItems)
                {
                    int userId = ValidationHelper.GetInteger(item, 0);
                    if (userId > 0)
                    {
                        ForumInfoProvider.AddModerator(userId, ForumID);
                    }
                }
            }
        }
    }


    /// <summary>
    /// Returns ID of users who are moderators to this forum.
    /// </summary>
    protected string GetModerators()
    {
        // Get all message board moderators
        DataSet ds = ForumInfoProvider.GetModerators(this.mForumId);
        if (!DataHelper.DataSourceIsEmpty(ds))
        {
            return TextHelper.Join(";", SqlHelperClass.GetStringValues(ds.Tables[0], "UserID"));
        }

        return String.Empty;
    }


    /// <summary>
    /// Board moderated checkbox change.
    /// </summary>
    protected void chkForumModerated_CheckedChanged(object sender, EventArgs e)
    {
        if (!CheckPermissions("cms.forums", CMSAdminControl.PERMISSION_MODIFY))
        {
            return;
        }

        if (forum != null)
        {
            forum.ForumModerated = chkForumModerated.Checked;

            ForumInfoProvider.SetForumInfo(forum);

            lblInfo.Visible = true;
            lblInfo.Text = GetString("General.Changessaved");

            this.RaiseOnSaved();
        }
    }


    /// <summary>
    /// Reloads the form data.
    /// </summary>
    public override void ReloadData(bool forceReload)
    {
        currentValues = GetModerators();

        if (forceReload)
        {
            forum = ForumInfoProvider.GetForumInfo(this.ForumID);

            if (forum != null)
            {
                userSelector.CurrentValues = currentValues;
                userSelector.ReloadData();
            }
        }

        if (!RequestHelper.IsPostBack() || forceReload)
        {
            chkForumModerated.Checked = forum.ForumModerated;

            // Load current values to uniselector
            userSelector.CurrentValues = currentValues;
            userSelector.ReloadData();
        }
    }
}
