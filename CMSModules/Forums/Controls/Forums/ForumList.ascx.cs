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

using CMS.Forums;
using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.LicenseProvider;
using CMS.UIControls;

public partial class CMSModules_Forums_Controls_Forums_ForumList : CMSAdminListControl
{
    #region "Variables"

    protected int mGroupId = 0;
    private bool process = true;
    private bool reloadUnigrid = false;

    #endregion


    #region "Public properties"

    /// <summary>
    /// Gets or sets the ID of the group for which the forums should be loaded.
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
        }
    }

    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        process = true;
        if (!this.Visible || StopProcessing)
        {
            this.EnableViewState = false;
            process = false;
        }

        // Initialize this.gridElem control
        this.gridElem.OnAction += new OnActionEventHandler(gridElem_OnAction);
        this.gridElem.GridView.DataBound += new EventHandler(GridView_DataBound);
        this.gridElem.OnExternalDataBound += new OnExternalDataBoundEventHandler(gridElem_OnExternalDataBound);
        this.gridElem.OrderBy = "ForumOrder";
        this.gridElem.GridView.AllowSorting = false;
        this.gridElem.IsLiveSite = this.IsLiveSite;
        this.gridElem.WhereCondition = "ForumGroupID=" + this.mGroupId;
        this.gridElem.GroupObject = (mGroupId > 0);
        this.gridElem.ZeroRowsText = GetString("general.nodatafound");
    }


    protected override void OnPreRender(EventArgs e)
    {
        if (!this.IsLiveSite && process)
        {
            ReloadData();
            this.gridElem.ReloadData();
        }
        else if (reloadUnigrid)
        {
            this.gridElem.ReloadData();
        }
    }


    /// <summary>
    /// Reloads the data in the grid.
    /// </summary>
    public override void ReloadData()
    {
        base.ReloadData();
        if (this.GroupID > 0)
        {
            this.gridElem.WhereCondition = "ForumGroupID=" + this.mGroupId;
        }

        reloadUnigrid = true;
    }


    #region "UniGrid events handling"

    void GridView_DataBound(object sender, EventArgs e)
    {
        //convert boolean values from DB to user-friendly information strings in the list
        for (int i = 0; i < this.gridElem.GridView.Rows.Count; i++)
        {
            // Date time string
            string dateTime = String.Empty;

            // Change timezone for live site
            DateTime dt = CMSContext.ConvertDateTime(ValidationHelper.GetDateTime(this.gridElem.GridView.Rows[i].Cells[6].Text, DateTimeHelper.ZERO_TIME), this);
            if (dt != DateTimeHelper.ZERO_TIME)
            {
                dateTime = dt.ToString();
            }

            // Set value to the grid
            this.gridElem.GridView.Rows[i].Cells[6].Text = dateTime;
        }
    }


    /// <summary>
    /// Unigrid external bind event handler.
    /// </summary>
    object gridElem_OnExternalDataBound(object sender, string sourceName, object parameter)
    {
        switch (sourceName.ToLower())
        {
            case "open":
                if (ValidationHelper.GetBoolean(parameter, false))
                {
                    return GetString("Forum_List.Open");
                }
                else
                {
                    return GetString("Forum_List.Close");
                }

            case "moderated":
                return UniGridFunctions.ColoredSpanYesNo(parameter);
        }

        return null;
    }


    /// <summary>
    /// Handles the UniGrids's OnAction event.
    /// </summary>
    /// <param name="actionName">Name of item (button) that throws event</param>
    /// <param name="actionArgument">ID (value of Primary key) of corresponding data row</param>
    protected void gridElem_OnAction(string actionName, object actionArgument)
    {
        switch (actionName.ToLower())
        {
            case "delete":
            case "up":
            case "down":
                if (!CheckPermissions("cms.forums", CMSAdminControl.PERMISSION_MODIFY))
                {
                    return;
                }
                break;
        }

        switch (actionName.ToLower())
        {
            case "delete":
                ForumInfoProvider.DeleteForumInfo(Convert.ToInt32(actionArgument));
                break;

            case "up":
                ForumInfoProvider.MoveForumUp(Convert.ToInt32(actionArgument));
                break;

            case "down":
                ForumInfoProvider.MoveForumDown(Convert.ToInt32(actionArgument));
                break;
        }

        this.RaiseOnAction(actionName, actionArgument);
    }

    #endregion
}
