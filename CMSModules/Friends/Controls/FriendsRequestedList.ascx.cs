using System;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.Community;
using CMS.GlobalHelper;
using CMS.SiteProvider;
using CMS.UIControls;
using CMS.SettingsProvider;

public partial class CMSModules_Friends_Controls_FriendsRequestedList : CMSAdminListControl
{
    #region "Private variables"

    private bool mDisplayFilter = true;

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

            // Register the dialog script
            ScriptHelper.RegisterDialogScript(this.Page);

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

            // Register script for action 'Remove'
            StringBuilder actionScript = new StringBuilder();
            actionScript.AppendLine("function FM_RemoveAction_" + ClientID + "()");
            actionScript.AppendLine("{");
            actionScript.AppendLine("   if(!" + gridElem.GetCheckSelectionScript(false) + ")");
            actionScript.AppendLine("   {");
            actionScript.AppendLine("      if (confirm(" + ScriptHelper.GetString(GetString("friends.ConfirmRemove")) + "))");
            actionScript.AppendLine("      {");
            actionScript.AppendLine(Page.ClientScript.GetPostBackEventReference(btnRemoveSelected, null) + ";");
            actionScript.AppendLine("      }");
            actionScript.AppendLine("   }");
            actionScript.AppendLine("   else");
            actionScript.AppendLine("   {");
            actionScript.AppendLine("       FM_ShowLabel('" + lblInfo.ClientID + "');");
            actionScript.AppendLine("   }");
            actionScript.AppendLine("   return false;");
            actionScript.AppendLine("}");

            ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "actionScript_" + ClientID, ScriptHelper.GetScript(actionScript.ToString()));
            // Add action to button
            btnRemoveSelected.OnClientClick = "return FM_RemoveAction_" + ClientID + "();";
            // Hide label
            lblInfo.Attributes["style"] = "display: none;";

            // Register the refreshing script
            string refreshScript = ScriptHelper.GetScript("function refreshList(){" + Page.ClientScript.GetPostBackEventReference(hdnRefresh, string.Empty) + "}");
            ScriptHelper.RegisterClientScriptBlock(Page, typeof(string), "friendsListRefresh", refreshScript);

            gridElem.OnAction += gridElem_OnAction;
            gridElem.OnExternalDataBound += gridElem_OnExternalDataBound;
            gridElem.OnAfterDataReload += new OnAfterDataReload(gridElem_OnAfterDataReload);
            gridElem.OrderBy = "UserName";
            gridElem.IsLiveSite = IsLiveSite;
            // Default where condition
            string defaultWhere = "((FriendStatus = " + Convert.ToInt32(FriendshipStatusEnum.Waiting) + " AND FriendUserID = " + UserID + ") OR (FriendStatus = " + Convert.ToInt32(FriendshipStatusEnum.Rejected) + " AND FriendRejectedBy <> " + UserID + "))";
            // Add search where condition
            gridElem.WhereCondition = SqlHelperClass.AddWhereCondition(defaultWhere, searchElem.WhereCondition);
            // Add parameter @UserID
            if (gridElem.QueryParameters == null)
            {
                gridElem.QueryParameters = new QueryDataParameters();
            }
            gridElem.QueryParameters.Add("@UserID", UserID);
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


    protected object gridElem_OnExternalDataBound(object sender, string sourceName, object parameter)
    {
        switch (sourceName.ToLower())
        {
            case "remove":
                bool rejected = ValidationHelper.GetString(((DataRowView)((GridViewRow)parameter).DataItem).Row["FriendStatus"], string.Empty) == "1";
                // Disable checkbox
                GridViewRow row = (GridViewRow)parameter;

                Control control = CMS.ExtendedControls.ControlsHelper.GetChildControl(row, typeof(CheckBox));
                if (control != null)
                {
                    CheckBox checkBox = (CheckBox)control;
                    checkBox.Enabled = !rejected;
                }
                // Disable button
                if (rejected)
                {
                    ImageButton button = ((ImageButton)sender);
                    button.ImageUrl = GetImageUrl("Design/Controls/UniGrid/Actions/deletedisabled.png");
                    button.Enabled = false;
                }
                break;

            case "status":
                // Set status (rejected/waiting)
                FriendshipStatusEnum status =
                    (FriendshipStatusEnum)Enum.Parse(typeof(FriendshipStatusEnum), parameter.ToString());
                switch (status)
                {
                    case FriendshipStatusEnum.Waiting:
                        parameter = "<span class=\"Waiting\">" + GetString("friends.waiting") + "</span>";
                        break;

                    case FriendshipStatusEnum.Rejected:
                        parameter = "<span class=\"Rejected\">" + GetString("general.rejected") + "</span>";
                        break;
                }
                break;

            case "formattedusername":
                return HTMLHelper.HTMLEncode(Functions.GetFormattedUserName(Convert.ToString(parameter), this.IsLiveSite));

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
        RaiseOnCheckPermissions(PERMISSION_MANAGE, this);

        FriendInfo fi = FriendInfoProvider.GetFriendInfo(ValidationHelper.GetInteger(actionArgument, 0));
        if (fi != null)
        {
            switch (actionName)
            {
                case "remove":
                    if (fi.FriendStatus != FriendshipStatusEnum.Rejected)
                    {
                        FriendInfoProvider.DeleteFriendInfo(fi);
                    }
                    gridElem.ReloadData();
                    break;
            }
        }
    }

    #endregion


    #region "Button handling"

    protected void btnRemoveSelected_Click(object sender, EventArgs e)
    {
        // If there user selected some items
        if (gridElem.SelectedItems.Count > 0)
        {
            RaiseOnCheckPermissions(PERMISSION_MANAGE, this);
            // Create where condition
            string where = "FriendID IN (";
            foreach (object friendId in gridElem.SelectedItems)
            {
                where += ValidationHelper.GetInteger(friendId, 0) + ",";
            }
            where = where.TrimEnd(',') + ")";
            // Get all needed friendships
            DataSet friendships = FriendInfoProvider.GetFriends(where, null);
            if (!DataHelper.DataSourceIsEmpty(friendships))
            {
                // Delete all these friendships
                foreach (DataRow friendship in friendships.Tables[0].Rows)
                {
                    FriendInfo fi = new FriendInfo(friendship);
                    if (fi.FriendStatus != FriendshipStatusEnum.Rejected)
                    {
                        FriendInfoProvider.DeleteFriendInfo(fi);
                    }
                }
            }
            gridElem.ResetSelection();
            // Reload grid
            gridElem.ReloadData();
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
