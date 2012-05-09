using System;
using System.Web.UI.WebControls;
using System.Data;

using CMS.Community;
using CMS.GlobalHelper;
using CMS.SiteProvider;
using CMS.CMSHelper;
using CMS.UIControls;


public partial class CMSModules_Groups_Controls_Members_MemberList : CMSAdminListControl
{
    #region "Variables"

    private int mGroupId = 0;

    #endregion


    #region "Public properties"

    /// <summary>
    /// Gets or sets the group ID for which the members should be displayed.
    /// </summary>
    public int GroupID
    {
        get
        {
            return this.mGroupId;
        }
        set
        {
            this.mGroupId = value;
            this.gridElem.WhereCondition = CreateWhereCondition();
        }
    }

    #endregion


    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Initialize unigrid
        gridElem.OnAction += new OnActionEventHandler(gridElem_OnAction);
        gridElem.OnExternalDataBound += new OnExternalDataBoundEventHandler(gridElem_OnExternalDataBound);
        gridElem.WhereCondition = CreateWhereCondition();
        gridElem.ImageDirectoryPath = GetImageUrl("Design/Controls/UniGrid/Actions/", IsLiveSite, true);
        gridElem.IsLiveSite = this.IsLiveSite;
        gridElem.OnBeforeDataReload += new OnBeforeDataReload(gridElem_OnBeforeDataReload);
        gridElem.ZeroRowsText = GetString("general.nodatafound");
    }

    #endregion


    #region "GridView actions handling"

    /// <summary>
    /// On before data reyload action.
    /// </summary>
    void gridElem_OnBeforeDataReload()
    {
        string where = CreateWhereCondition();
        
        // Prepare where condition
        if (!string.IsNullOrEmpty(filterMembers.WhereCondition))
        {
            where = where + " AND (" + filterMembers.WhereCondition + ")";
        }

        gridElem.WhereCondition = where;
    }

    /// <summary>
    /// Unigrid OnExternalDataBound event.
    /// </summary>
    protected object gridElem_OnExternalDataBound(object sender, string sourceName, object parameter)
    {
        GroupMemberStatus status = GroupMemberStatus.Approved;
        DataRowView drv = null;
        GridViewRow gvr = null;
        bool current = false;

        switch (sourceName.ToLower())
        {
            case "memberapprovedwhen":
            case "memberrejectedwhen":
                if (parameter != DBNull.Value)
                {
                    // Get current dateTime
                    return CMSContext.ConvertDateTime(Convert.ToDateTime(parameter), this);
                }
                break;

            case "approve":
                gvr = parameter as GridViewRow;
                if (gvr != null)
                {
                    drv = gvr.DataItem as DataRowView;
                    if (drv != null)
                    {
                        // Check for current user
                        if (IsLiveSite && (CMSContext.CurrentUser.UserID == ValidationHelper.GetInteger(drv["MemberUserID"], 0)))
                        {
                            current = true;
                        }

                        // Do not allow approve hidden or disabled users
                        bool hiddenOrDisabled = ValidationHelper.GetBoolean(drv["UserIsHidden"], false) || !ValidationHelper.GetBoolean(drv["UserEnabled"], true);

                        status = (GroupMemberStatus)ValidationHelper.GetInteger(drv["MemberStatus"], 0);

                        // Enable or disable Approve button
                        if (!current && (status != GroupMemberStatus.Approved) && !hiddenOrDisabled)
                        {
                            ImageButton button = ((ImageButton)sender);
                            button.ImageUrl = GetImageUrl("Design/Controls/UniGrid/Actions/Approve.png");
                            button.ToolTip = GetString("general.approve");
                            button.Enabled = true;
                        }
                        else
                        {
                            ImageButton button = ((ImageButton)sender);
                            button.ImageUrl = GetImageUrl("Design/Controls/UniGrid/Actions/approvedisabled.png");
                            button.ToolTip = GetString("general.approve");
                            button.Enabled = false;
                        }
                    }
                }

                break;

            case "reject":
                gvr = parameter as GridViewRow;
                if (gvr != null)
                {
                    drv = gvr.DataItem as DataRowView;
                    if (drv != null)
                    {
                        // Check for current user
                        if (IsLiveSite && (CMSContext.CurrentUser.UserID == ValidationHelper.GetInteger(drv.Row["MemberUserID"], 0)))
                        {
                            current = true;
                        }

                        status = (GroupMemberStatus)ValidationHelper.GetInteger(drv["MemberStatus"], 0);

                        // Enable or disable Reject button
                        if (!current && (status != GroupMemberStatus.Rejected))
                        {
                            ImageButton button = ((ImageButton)sender);
                            button.ImageUrl = GetImageUrl("Design/Controls/UniGrid/Actions/Reject.png");
                            button.ToolTip = GetString("general.reject");
                            button.Enabled = true;
                        }
                        else
                        {
                            ImageButton button = ((ImageButton)sender);
                            button.ImageUrl = GetImageUrl("Design/Controls/UniGrid/Actions/rejectdisabled.png");
                            button.ToolTip = GetString("general.reject");
                            button.Enabled = false;
                        }
                    }
                }
                break;

            case "formattedusername":
                return HTMLHelper.HTMLEncode(Functions.GetFormattedUserName(Convert.ToString(parameter), this.IsLiveSite));

            case "edit":
                gvr = parameter as GridViewRow;
                if (gvr != null)
                {
                    drv = gvr.DataItem as DataRowView;
                    if (drv != null)
                    {
                        // Do not allow approve hidden or disabled users
                        bool hiddenOrDisabled = ValidationHelper.GetBoolean(drv["UserIsHidden"], false) || !ValidationHelper.GetBoolean(drv["UserEnabled"], true);

                        // Enable or disable Edit button
                        if (!hiddenOrDisabled)
                        {
                            ImageButton button = ((ImageButton)sender);
                            button.ImageUrl = GetImageUrl("Design/Controls/UniGrid/Actions/Edit.png");
                            button.ToolTip = GetString("general.edit");
                            button.Enabled = true;
                        }
                        else
                        {
                            ImageButton button = ((ImageButton)sender);
                            button.ImageUrl = GetImageUrl("Design/Controls/UniGrid/Actions/editdisabled.png");
                            button.ToolTip = GetString("general.edit");
                            button.Enabled = false;
                        }
                    }
                }
                break;
        }
        return parameter;
    }


    /// <summary>
    /// Handles the UniGrid's OnAction event.
    /// </summary>
    /// <param name="actionName">Name of item (button) that throws event</param>
    /// <param name="actionArgument">ID (value of Primary key) of corresponding data row</param>
    protected void gridElem_OnAction(string actionName, object actionArgument)
    {
        switch (actionName)
        {
            case "delete":
            case "approve":
            case "reject":

                // Check MANAGE permission for groups module
                if (!CheckPermissions("cms.groups", CMSAdminControl.PERMISSION_MANAGE, this.GroupID))
                {
                    return;
                }

                break;
        }

        if (actionName == "delete")
        {
            // Delete member
            GroupMemberInfoProvider.DeleteGroupMemberInfo(ValidationHelper.GetInteger(actionArgument, 0));
        }
        else if (actionName == "approve")
        {
            // Approve member
            GroupMemberInfo gmi = GroupMemberInfoProvider.GetGroupMemberInfo(ValidationHelper.GetInteger(actionArgument, 0));
            if (gmi != null)
            {
                gmi.MemberApprovedByUserID = CMSContext.CurrentUser.UserID;
                gmi.MemberStatus = GroupMemberStatus.Approved;
                gmi.MemberApprovedWhen = DateTime.Now;
                gmi.MemberRejectedWhen = DataHelper.DATETIME_NOT_SELECTED;
                GroupMemberInfoProvider.SetGroupMemberInfo(gmi);
                GroupInfo group = GroupInfoProvider.GetGroupInfo(this.GroupID);
                if ((group != null) && (group.GroupSendWaitingForApprovalNotification))
                {
                    GroupMemberInfoProvider.SendNotificationMail("Groups.MemberApproved", CMSContext.CurrentSiteName, gmi, false);
                }
            }
        }
        else if (actionName == "reject")
        {
            // Reject member
            GroupMemberInfo gmi = GroupMemberInfoProvider.GetGroupMemberInfo(ValidationHelper.GetInteger(actionArgument, 0));
            if (gmi != null)
            {
                gmi.MemberApprovedByUserID = CMSContext.CurrentUser.UserID;
                gmi.MemberStatus = GroupMemberStatus.Rejected;
                gmi.MemberApprovedWhen = DataHelper.DATETIME_NOT_SELECTED;
                gmi.MemberRejectedWhen = DateTime.Now;
                GroupMemberInfoProvider.SetGroupMemberInfo(gmi);
                GroupInfo group = GroupInfoProvider.GetGroupInfo(this.GroupID);
                if ((group != null) && (group.GroupSendWaitingForApprovalNotification))
                {
                    GroupMemberInfoProvider.SendNotificationMail("Groups.MemberRejected", CMSContext.CurrentSiteName, gmi, false);
                }
            }
        }
        this.RaiseOnAction(actionName, actionArgument);
    }

    #endregion


    #region "Methods"

    /// <summary>
    /// Reloads the grid data.
    /// </summary>
    public void ReloadGrid()
    {
        this.gridElem.ReloadData();
    }


    /// <summary>
    /// Creates where condition for unigrid according to the parameters.
    /// </summary>
    private string CreateWhereCondition()
    {                
        // Prepare where condition
        string where = "(MemberGroupID = " + this.mGroupId + ") AND (SiteID = " + CMSContext.CurrentSiteID + ")";

        if (IsLiveSite)
        {
            where += " AND (UserIsHidden = 0 OR UserIsHidden IS NULL) AND UserEnabled = 1";
        }       

        return where;
    }

    #endregion
}
