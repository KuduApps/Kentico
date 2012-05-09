using System;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.CMSHelper;
using CMS.Community;
using CMS.GlobalHelper;
using CMS.SiteProvider;
using CMS.UIControls;
using CMS.SettingsProvider;

using TimeZoneInfo = CMS.SiteProvider.TimeZoneInfo;

public partial class CMSModules_Friends_Controls_FriendsToApprovalList : CMSAdminListControl
{
    #region "Private variables"

    private bool mDisplayFilter = true;
    protected string dialogsUrl = "~/CMSModules/Friends/Dialogs/";
    private TimeZoneInfo usedTimeZone = null;
    private UserInfo currentUserInfo = null;
    private SiteInfo currentSiteInfo = null;
    private string mRejectDialogUrl = null;
    private string mApproveDialogUrl = null;

    #endregion


    #region "Public properties"

    /// <summary>
    /// Gets and sets User ID.
    /// </summary>
    public int UserID
    {
        get;
        set;
    }


    /// <summary>
    /// Gets and sets whether to show filter.
    /// </summary>
    public bool DisplayFilter
    {
        get
        {
            return mDisplayFilter;
        }
        set
        {
            mDisplayFilter = value;
            searchElem.Visible = value;
        }
    }


    /// <summary>
    /// Gets reject dialog url.
    /// </summary>
    protected string RejectDialogUrl
    {
        get
        {
            if (mRejectDialogUrl == null)
            {
                mRejectDialogUrl = CMSContext.ResolveDialogUrl(dialogsUrl + "Friends_Reject.aspx") + "?userid=" + UserID;
            }

            return mRejectDialogUrl;
        }
    }


    /// <summary>
    /// Gets approve dialog url.
    /// </summary>
    protected string ApproveDialogUrl
    {
        get
        {
            if (mApproveDialogUrl == null)
            {
                mApproveDialogUrl = CMSContext.ResolveDialogUrl(dialogsUrl + "Friends_Approve.aspx") + "?userid=" + UserID;
            }

            return mApproveDialogUrl;
        }
    }


    /// <summary>
    /// Zero rows text.
    /// </summary>
    public string ZeroRowsText
    {
        get
        {
            return gridElem.ZeroRowsText;
        }
        set
        {
            gridElem.ZeroRowsText = value;
        }
    }

    #endregion


    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        if (StopProcessing)
        {
            Visible = false;
            // Do not load data
            gridElem.StopProcessing = true;
        }
        else
        {
            RaiseOnCheckPermissions(PERMISSION_READ, this);
            Visible = true;

            if (IsLiveSite)
            {
                dialogsUrl = "~/CMSModules/Friends/CMSPages/";
            }

            // Register the dialog script
            ScriptHelper.RegisterDialogScript(this.Page);

            // Register the refreshing script
            string refreshScript = ScriptHelper.GetScript("function refreshList(){" + Page.ClientScript.GetPostBackEventReference(hdnRefresh, string.Empty) + "}");
            ScriptHelper.RegisterClientScriptBlock(Page, typeof(string), "friendsListRefresh", refreshScript);

            // Setup grid
            gridElem.OnAfterDataReload += new OnAfterDataReload(gridElem_OnAfterDataReload);
            gridElem.OnExternalDataBound += gridElem_OnExternalDataBound;
            gridElem.OrderBy = "UserName";
            gridElem.IsLiveSite = IsLiveSite;
            // Default where condition
            string defaultWhere = "(FriendStatus = " + Convert.ToInt32(FriendshipStatusEnum.Waiting) + ") AND (FriendUserID <> " + UserID + ")";
            // Add search where condition
            gridElem.WhereCondition = SqlHelperClass.AddWhereCondition(defaultWhere, searchElem.WhereCondition);
            // Add parameter @UserID
            if (gridElem.QueryParameters == null)
            {
                gridElem.QueryParameters = new QueryDataParameters();
            }
            gridElem.QueryParameters.Add("@UserID", UserID);

            // Register the dialog script
            ScriptHelper.RegisterDialogScript(this.Page);

            // Register reject script
            StringBuilder rejectScript = new StringBuilder();
            rejectScript.AppendLine("function FM_Reject_" + ClientID + "(item, gridId, url)");
            rejectScript.AppendLine("{");
            rejectScript.AppendLine("   var items = '';");
            rejectScript.AppendLine("   if(item == null)");
            rejectScript.AppendLine("   {");
            rejectScript.AppendLine("       items = document.getElementById(gridId).value;");
            rejectScript.AppendLine("   }");
            rejectScript.AppendLine("   else");
            rejectScript.AppendLine("   {");
            rejectScript.AppendLine("       items = item;");
            rejectScript.AppendLine("   }");
            rejectScript.AppendLine("   if((url != null) && (items != '') && (items != '|'))");
            rejectScript.AppendLine("   {");
            rejectScript.AppendLine("       modalDialog(url + '&ids=' + items, 'rejectDialog', 480, 350);");
            rejectScript.AppendLine("   }");
            rejectScript.AppendLine("   else");
            rejectScript.AppendLine("   {");
            rejectScript.AppendLine("       FM_ShowLabel('" + lblInfo.ClientID + "');");
            rejectScript.AppendLine("   }");
            rejectScript.AppendLine("   return false;");
            rejectScript.AppendLine("}");
            ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "rejectScript_" + ClientID, ScriptHelper.GetScript(rejectScript.ToString()));

            // Register approve script
            StringBuilder approveScript = new StringBuilder();
            approveScript.AppendLine("function FM_Approve_" + ClientID + "(item, gridId, url)");
            approveScript.AppendLine("{");
            approveScript.AppendLine("   var items = '';");
            approveScript.AppendLine("   if(item == null)");
            approveScript.AppendLine("   {");
            approveScript.AppendLine("       items = document.getElementById(gridId).value;");
            approveScript.AppendLine("   }");
            approveScript.AppendLine("   else");
            approveScript.AppendLine("   {");
            approveScript.AppendLine("       items = item;");
            approveScript.AppendLine("   }");
            approveScript.AppendLine("   if((url != null) && (items != '') && (items != '|'))");
            approveScript.AppendLine("   {");
            approveScript.AppendLine("      modalDialog(url + '&ids=' + items, 'approveDialog', 480, 350);");
            approveScript.AppendLine("   }");
            approveScript.AppendLine("   else");
            approveScript.AppendLine("   {");
            approveScript.AppendLine("       FM_ShowLabel('" + lblInfo.ClientID + "');");
            approveScript.AppendLine("   }");
            approveScript.AppendLine("   return false;");
            approveScript.AppendLine("}");
            ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "approveScript_" + ClientID, ScriptHelper.GetScript(approveScript.ToString()));

            // Add actions to buttons
            btnApproveSelected.OnClientClick = "return FM_Approve_" + ClientID + "(null,'" + gridElem.SelectionHiddenField.ClientID + "','" + ApproveDialogUrl + "');";
            btnRejectSelected.OnClientClick = "return FM_Reject_" + ClientID + "(null,'" + gridElem.SelectionHiddenField.ClientID + "','" + RejectDialogUrl + "');";
            // Hide label
            lblInfo.Attributes["style"] = "display: none;";

            // Register script for displaying label
            StringBuilder showLabelScript = new StringBuilder();
            showLabelScript.AppendLine("function FM_ShowLabel(labelId)");
            showLabelScript.AppendLine("{");
            showLabelScript.AppendLine("   var label = document.getElementById(labelId);");
            showLabelScript.AppendLine("   if (label != null)");
            showLabelScript.AppendLine("   {");
            showLabelScript.AppendLine("      label.innerHTML = '" + GetString("friends.selectfriends") + "';");
            showLabelScript.AppendLine("      label.style['display'] = 'block';");
            showLabelScript.AppendLine("   }");
            showLabelScript.AppendLine("}");
            ScriptHelper.RegisterClientScriptBlock(Page, typeof(string), "showLabel", ScriptHelper.GetScript(showLabelScript.ToString()));
        }
    }


    protected override void OnPreRender(EventArgs e)
    {
        if (!DisplayFilter)
        {
            plcNoData.Visible = HasData();
        }
        base.OnPreRender(e);

        // Reset grid selection after multiple action
        if (RequestHelper.IsPostBack())
        {
            string invokerName = Page.Request.Params.Get("__EVENTTARGET");
            Control invokeControl = !string.IsNullOrEmpty(invokerName) ? Page.FindControl(invokerName) : null;
            if (invokeControl == hdnRefresh)
            {
                gridElem.ResetSelection();
            }
        }
    }

    #endregion


    #region "Grid events"

    protected object gridElem_OnExternalDataBound(object sender, string sourceName, object parameter)
    {
        DataRowView drv = null;
        switch (sourceName)
        {
            case "reject":
                if (sender is ImageButton)
                {
                    // Get action button
                    ImageButton rejectBtn = (ImageButton)sender;
                    // Get full row view
                    drv = UniGridFunctions.GetDataRowView((DataControlFieldCell)rejectBtn.Parent);
                    // Add custom reject action
                    rejectBtn.Attributes["onclick"] = "return FM_Reject_" + ClientID + "('" + drv["FriendID"] + "',null,'" + RejectDialogUrl + "');";
                    return rejectBtn;
                }
                else
                {
                    return string.Empty;
                }

            case "approve":
                if (sender is ImageButton)
                {
                    // Get action button
                    ImageButton approveBtn = (ImageButton)sender;
                    // Get full row view
                    drv = UniGridFunctions.GetDataRowView((DataControlFieldCell)approveBtn.Parent);
                    // Add custom reject action
                    approveBtn.Attributes["onclick"] = "return FM_Approve_" + ClientID + "('" + drv["FriendID"] + "',null,'" + ApproveDialogUrl + "');";
                    return approveBtn;
                }
                else
                {
                    return string.Empty;
                }

            case "friendrequestedwhen":
                if (currentUserInfo == null)
                {
                    currentUserInfo = CMSContext.CurrentUser;
                }
                if (currentSiteInfo == null)
                {
                    currentSiteInfo = CMSContext.CurrentSite;
                }
                DateTime currentDateTime = ValidationHelper.GetDateTime(parameter, DateTimeHelper.ZERO_TIME);
                if (IsLiveSite)
                {
                    return CMSContext.ConvertDateTime(currentDateTime, this);
                }
                else
                {
                    return TimeZoneHelper.GetCurrentTimeZoneDateTimeString(currentDateTime, currentUserInfo, currentSiteInfo, out usedTimeZone);
                }

            case "formattedusername":
                return HTMLHelper.HTMLEncode(Functions.GetFormattedUserName(Convert.ToString(parameter), this.IsLiveSite));

        }
        return parameter;
    }


    protected void gridElem_OnAfterDataReload()
    {
        // Hide filter when not needed
        if (searchElem.FilterIsSet)
        {
            DisplayFilter = true;
        }
        else if (!DataHelper.DataSourceIsEmpty(gridElem.GridView.DataSource))
        {
            DataSet friendships = gridElem.GridView.DataSource as DataSet;
            if (friendships != null)
            {
                int rowsCount = friendships.Tables[0].Rows.Count;
                DisplayFilter = (gridElem.FilterLimit <= 0) || (rowsCount > gridElem.FilterLimit);
            }
        }
        else
        {
            DisplayFilter = false;
        }
    }

    #endregion


    #region "Methods"

    /// <summary>
    /// Indicates if there are some data.
    /// </summary>
    public bool HasData()
    {
        return (!DataHelper.DataSourceIsEmpty(gridElem.GridView.DataSource));
    }

    #endregion
}
