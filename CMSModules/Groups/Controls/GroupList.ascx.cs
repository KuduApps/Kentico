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

using CMS.Community;
using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.CMSHelper;

public partial class CMSModules_Groups_Controls_GroupList : CMSAdminListControl
{
    #region "Variables"

    private int mSiteId = 0;

    #endregion


    #region "Public properties"

    /// <summary>
    /// Gets or sets the site ID for which the roles should be displayed.
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
            this.gridElem.WhereCondition = CreateWhereCondition();
        }
    }

    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        // Unigrid
        gridElem.OnAction += new OnActionEventHandler(gridElem_OnAction);
        gridElem.WhereCondition = CreateWhereCondition();
        gridElem.IsLiveSite = this.IsLiveSite;
        gridElem.OnExternalDataBound += new OnExternalDataBoundEventHandler(gridElem_OnExternalDataBound);
        gridElem.OnBeforeDataReload += new OnBeforeDataReload(gridElem_OnBeforeDataReload);
        gridElem.ZeroRowsText = GetString("general.nodatafound");
    }


    void gridElem_OnBeforeDataReload()
    {
        // Filter the grid
        gridElem.WhereClause = filterGroups.WhereCondition;
    }


    /// <summary>
    /// Unigrid external databound handler.
    /// </summary>
    object gridElem_OnExternalDataBound(object sender, string sourceName, object parameter)
    {
        bool approve = false;
        bool reject = false;

        switch (sourceName)
        {
            case "GroupApproved":
                {
                    if (ValidationHelper.GetBoolean(parameter, false))
                    {
                        return "<span class=\"Approved\">" + GetString("general.yes") + "</span>";
                    }
                    else
                    {
                        return "<span class=\"NotApproved\">" + GetString("general.no") + "</span>";
                    }
                }


            case "approve":
                approve = ValidationHelper.GetBoolean(((DataRowView)((GridViewRow)parameter).DataItem).Row["GroupApproved"], false);
                if (!approve)
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
                break;

            case "reject":
                reject = ValidationHelper.GetBoolean(((DataRowView)((GridViewRow)parameter).DataItem).Row["GroupApproved"], false);
                if (reject)
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
                break;
        }

        return parameter.ToString();
    }


    /// <summary>
    /// Handles the UniGrid's OnAction event.
    /// </summary>
    /// <param name="actionName">Name of item (button) that throws event</param>
    /// <param name="actionArgument">ID (value of Primary key) of corresponding data row</param>
    protected void gridElem_OnAction(string actionName, object actionArgument)
    {
        GroupInfo gi = null;

        // Handle event
        switch (actionName)
        {
            case "approve":
                CheckPermissions();
                gi = GroupInfoProvider.GetGroupInfo(ValidationHelper.GetInteger(actionArgument, 0));
                gi.GroupApproved = true;
                gi.GroupApprovedByUserID = CMSContext.CurrentUser.UserID;
                GroupInfoProvider.SetGroupInfo(gi);
                break;

            case "reject":
                CheckPermissions();
                gi = GroupInfoProvider.GetGroupInfo(ValidationHelper.GetInteger(actionArgument, 0));
                gi.GroupApproved = false;
                gi.GroupApprovedByUserID = 0;
                GroupInfoProvider.SetGroupInfo(gi);
                break;
        }

        this.RaiseOnAction(actionName, actionArgument);
    }


    /// <summary>
    /// Check manage permission.
    /// </summary>
    private void CheckPermissions()
    {
        if (!CheckPermissions("cms.groups", CMSAdminControl.PERMISSION_MANAGE))
        {
            return;
        }
    }


    /// <summary>
    /// Creates where condition for unigrid according to the parameters.
    /// </summary>
    private string CreateWhereCondition()
    {
        string where = null;

        if (this.SiteID > 0)
        {
            where = "(GroupSiteID = " + this.SiteID + ")";
        }

        return where;
    }
}
