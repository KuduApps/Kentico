using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using CMS.UIControls;
using CMS.CMSHelper;
using CMS.Forums;
using CMS.GlobalHelper;
using CMS.SettingsProvider;
using CMS.TreeEngine;

public partial class CMSModules_Forums_Controls_Forums_ForumPostsWaitingForApproval : CMSAdminListControl
{
    #region "Variables"

    private string mWhereCondition = String.Empty;
    private int mCommunityGroupId = 0;
    private bool process = true;
    private string mGroupNames = String.Empty;
    private string mItemsPerPage = string.Empty;
    private string mZeroRowText = String.Empty;
    private string mSiteName = String.Empty;
    private bool mHideControlForNoData = false;

    #endregion


    #region "Public properties"

    /// <summary>
    /// Filter site name.
    /// </summary>
    public string SiteName
    {
        get
        {
            return mSiteName;
        }
        set
        {
            mSiteName = value;
        }
    }


    /// <summary>
    /// If no data found - hide control.
    /// </summary>
    public bool HideControlForNoData
    {
        get
        {
            return mHideControlForNoData;
        }
        set
        {
            mHideControlForNoData = value;
        }
    }


    /// <summary>
    /// Gets or sets ID of current group.
    /// </summary>
    public int CommunityGroupId
    {
        get
        {
            return this.mCommunityGroupId;
        }
        set
        {
            this.mCommunityGroupId = value;
        }
    }


    /// <summary>
    /// Additional WHERE condition to filter data.
    /// </summary>
    public string WhereCondition
    {
        get
        {
            return this.mWhereCondition;
        }
        set
        {
            this.mWhereCondition = value;
        }
    }


    /// <summary>
    /// Returns datasource of gid.
    /// </summary>
    public object DataSource
    {
        get
        {
            return gridApprove.GridView.DataSource;
        }
    }


    /// <summary>
    /// Group names filter.
    /// </summary>
    public string GroupNames
    {
        get
        {
            return this.mGroupNames;
        }
        set
        {
            this.mGroupNames = value;
        }
    }


    /// <summary>
    /// Text for no data.
    /// </summary>
    public string ZeroRowText
    {
        get
        {
            return this.mZeroRowText;
        }
        set
        {
            this.mZeroRowText = value;
        }
    }


    /// <summary>
    /// Items per page.
    /// </summary>
    public string ItemsPerPage
    {
        get
        {
            return this.mItemsPerPage;
        }
        set
        {
            mItemsPerPage = value;
        }
    }

    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        process = true;
        // If control is not visible don't process anything       
        if (!this.Visible || this.StopProcessing)
        {
            process = false;
            EnableViewState = false;
            return;
        }
        string forumIDs = null;
        // Group where condition part    
        string groupWhere = String.Empty;
        if (SiteName == string.Empty)
        {
            SiteName = CMSContext.CurrentSiteName;
        }
        if (SiteName != TreeProvider.ALL_SITES)
        {
            groupWhere = "GroupSiteID IN (SELECT SiteID FROM CMS_Site WHERE SiteName = N'" + SqlHelperClass.GetSafeQueryString(SiteName, false) + "')";
        }

        if (this.CommunityGroupId > 0)
        {
            groupWhere = SqlHelperClass.AddWhereCondition(groupWhere, "GroupGroupID = " + this.CommunityGroupId);
        }

        // Add where condition from property
        if (this.WhereCondition != String.Empty)
        {
            groupWhere = SqlHelperClass.AddWhereCondition(groupWhere, this.WhereCondition);
        }

        bool hasGroupRights = false;

        if (this.CommunityGroupId > 0)
        {
            if (CMSContext.CurrentUser.IsGroupAdministrator(CommunityGroupId) ||
                CMSContext.CurrentUser.IsAuthorizedPerResource("cms.groups", "Manage"))
            {
                hasGroupRights = true;
            }
        }

        // Get forums moderated by current user
        else if (!CMSContext.CurrentUser.IsGlobalAdministrator)
        {
            // Get forumId where the user is moderator and forum satisfy group where condition
            string whereCond = "UserID =" + CMSContext.CurrentUser.UserID;
            if (groupWhere != String.Empty)
            {
                whereCond += " AND ForumID IN ( SELECT ForumID FROM Forums_Forum WHERE " +
                           "ForumGroupID IN (SELECT GroupID FROM Forums_ForumGroup WHERE " + groupWhere + "))";
            }

            // Get forums where user is moderator
            DataSet ds = ForumModeratorInfoProvider.GetGroupForumsModerators(whereCond, null);

            if (!DataHelper.DataSourceIsEmpty(ds))
            {
                forumIDs = "";

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    forumIDs += ValidationHelper.GetString(dr["ForumID"], "") + ",";
                }

                // Remove ending ,
                forumIDs = forumIDs.TrimEnd(',');
            }
        }

        string zeroRowText = String.Empty;
        if (ZeroRowText == String.Empty)
        {
            zeroRowText = GetString("general.nodatafound");
        }
        else
        {
            zeroRowText = HTMLHelper.HTMLEncode(ZeroRowText);
        }

        // Hide approvals
        if ((!CMSContext.CurrentUser.IsGlobalAdministrator) && (String.IsNullOrEmpty(forumIDs)) && (hasGroupRights == false))
        {
            if (!HideControlForNoData)
            {
                gridApprove.StopProcessing = true;
                process = false;
                lblInfo.Text = zeroRowText;
                lblInfo.Visible = true;
                return;
            }
            else
            {
                this.Visible = false;
            }
        }

        gridApprove.ZeroRowsText = zeroRowText;
        gridApprove.OnAction += new OnActionEventHandler(gridApprove_OnAction);
        gridApprove.GridView.AllowSorting = false;
        gridApprove.OnExternalDataBound += new OnExternalDataBoundEventHandler(gridApprove_OnExternalDataBound);
        gridApprove.IsLiveSite = this.IsLiveSite;
        gridApprove.HideControlForZeroRows = false;

        if ((!RequestHelper.IsPostBack()) && (!string.IsNullOrEmpty(ItemsPerPage)))
        {
            gridApprove.Pager.DefaultPageSize = ValidationHelper.GetInteger(ItemsPerPage, -1);
        }

        if (CMSContext.CurrentUser.IsGlobalAdministrator || hasGroupRights)
        {
            if (groupWhere != String.Empty)
            {
                gridApprove.WhereCondition = "(PostApproved IS NULL OR PostApproved = 0) AND (PostForumID IN (SELECT ForumID FROM [Forums_Forum] WHERE ForumGroupID IN (SELECT GroupID FROM [Forums_ForumGroup] WHERE " + groupWhere + ")))";
            }
            // Show only posts waiting for approval
            else
            {
                gridApprove.WhereCondition = "(PostApproved IS NULL OR PostApproved = 0)";
            }
        }
        else if (forumIDs != null)
        {
            gridApprove.WhereCondition = "((PostApproved IS NULL) OR (PostApproved = 0)) AND (PostForumID IN  (SELECT ForumID FROM [Forums_Forum] WHERE (ForumID IN (" +
               forumIDs + "))";
            if (groupWhere != String.Empty)
            {
                gridApprove.WhereCondition += " AND (ForumGroupID IN (SELECT GroupID FROM [Forums_ForumGroup] WHERE " + groupWhere + "))))";
            }
            else
            {
                gridApprove.WhereCondition += "))";
            }
        }


        //Filter group names
        if (GroupNames != String.Empty)
        {
            string where = String.Empty;
            string parsedNames = String.Empty;
            string[] names = GroupNames.Split(';');
            if (names.Length > 0)
            {
                foreach (string name in names)
                {
                    parsedNames += "'" + SqlHelperClass.GetSafeQueryString(name, false) + "',";
                }
                parsedNames = parsedNames.TrimEnd(',');
                where = "(PostForumID IN (SELECT ForumID FROM [Forums_Forum] WHERE (ForumGroupID IN (SELECT GroupID FROM [Forums_ForumGroup] WHERE GroupName IN (" + parsedNames + ")))))";
                gridApprove.WhereCondition = SqlHelperClass.AddWhereCondition(gridApprove.WhereCondition, where);
            }
        }
    }


    /// <summary>
    /// Reloads the grid data.
    /// </summary>
    public override void ReloadData()
    {
        this.gridApprove.ReloadData();
        base.ReloadData();
    }


    protected override void OnPreRender(EventArgs e)
    {
        if (!this.IsLiveSite && process)
        {
            ReloadData();
        }

        string dilaogUrl = null;
        if (CommunityGroupId > 0)
        {
            if (IsLiveSite)
            {
                dilaogUrl = "~/CMSModules/Groups/CMSPages/LiveForumPostApprove.aspx";
            }
            else
            {
                dilaogUrl = "~/CMSModules/Groups/CMSPages/ForumPostApprove.aspx";
            }
        }
        else
        {
            dilaogUrl = "~/CMSModules/Forums/CMSPages/ForumPostApprove.aspx";
        }

        ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "ForumPostApproveScript", ScriptHelper.GetScript(@"
            function ForumPostApprove(id) {
                var url = '" + URLHelper.ResolveUrl(dilaogUrl) + @"';
                url = url + '?postid=' + id;
                modalDialog(url, 'forumPostApproveDialog', 600, 600);
                return false;
            }

            function RefreshPage(){
                window.location.replace(document.URL);
            }
        "));

        base.OnPreRender(e);
    }   


    #region "UniGrid events"

    /// <summary>
    /// OnExterna databound.
    /// </summary>
    /// <param name="sender">Sender</param>
    /// <param name="sourceName">Source name</param>
    /// <param name="parameter">Parameter</param>
    object gridApprove_OnExternalDataBound(object sender, string sourceName, object parameter)
    {
        switch (sourceName.ToLower())
        {
            case "forum":
                ForumInfo fi = ForumInfoProvider.GetForumInfo(ValidationHelper.GetInteger(parameter, 0));
                if (fi != null)
                {
                    return HTMLHelper.HTMLEncode(fi.ForumDisplayName);
                }
                break;

            case "content":
                DataRowView dr = parameter as DataRowView;
                if (dr != null)
                {
                    string toReturn = "";
                    toReturn = "<strong>" + HTMLHelper.HTMLEncode(ValidationHelper.GetString(dr["PostUserName"], "")) + ":</strong> ";
                    toReturn += HTMLHelper.HTMLEncode(ValidationHelper.GetString(dr["PostSubject"], "")) + "<br />";
                    toReturn += TextHelper.LimitLength(HTMLHelper.HTMLEncode(ValidationHelper.GetString(dr["PostText"], "")), 150);
                    return toReturn;
                }
                break;
        }

        return "";
    }


    /// <summary>
    /// Approve, reject or delete post.
    /// </summary>
    protected void gridApprove_OnAction(string actionName, object actionArgument)
    {
        switch (actionName.ToLower())
        {
            case "deletepost":
                ForumPostInfoProvider.DeleteForumPostInfo(ValidationHelper.GetInteger(actionArgument, 0));
                break;

            case "approve":
                ForumPostInfo fpi = ForumPostInfoProvider.GetForumPostInfo(ValidationHelper.GetInteger(actionArgument, 0));
                if (fpi != null)
                {
                    fpi.PostApprovedByUserID = CMSContext.CurrentUser.UserID;
                    fpi.PostApproved = true;
                    ForumPostInfoProvider.SetForumPostInfo(fpi);
                }
                break;
        }

        this.RaiseOnAction(actionName, actionArgument);
    }

    #endregion
}

