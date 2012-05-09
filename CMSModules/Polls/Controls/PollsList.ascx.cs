using System;
using System.Data;
using System.Web;

using CMS.GlobalHelper;
using CMS.Polls;
using CMS.UIControls;
using System.Web.UI.WebControls;

public partial class CMSModules_Polls_Controls_PollsList : CMSAdminListControl
{

    #region "Variables"

    private int mGroupId = 0;
    private string mWhereCondition = String.Empty;

    #endregion


    #region "Public properties"

    /// <summary>
    /// Gets or sets ID of current group.
    /// </summary>
    public int GroupId
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
    /// Indicates if DelayedReload for UniGrid should be used.
    /// </summary>
    public bool DelayedReload
    {
        get
        {
            return UniGrid.DelayedReload;
        }
        set
        {
            UniGrid.DelayedReload = value;
        }
    }


    /// <summary>
    /// Indicates if global polls should be marked.
    /// </summary>
    public bool DisplayGlobalColumn
    {
        get;
        set;
    }

    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        // Setup the grid
        UniGrid.IsLiveSite = this.IsLiveSite;
        UniGrid.OnAction += new OnActionEventHandler(UniGrid_OnAction);
        UniGrid.HideControlForZeroRows = false;
        UniGrid.OnBeforeSorting += new OnBeforeSorting(UniGrid_OnBeforeSorting);
        UniGrid.OnPageChanged += new EventHandler<EventArgs>(UniGrid_OnPageChanged);
        UniGrid.OnExternalDataBound += new OnExternalDataBoundEventHandler(UniGrid_OnExternalDataBound);
        UniGrid.ZeroRowsText = GetString("general.nodatafound");
        UniGrid.OnBeforeDataReload += new OnBeforeDataReload(UniGrid_OnBeforeDataReload);
        UniGrid.GroupObject = (GroupId > 0);
        this.SetupControl();
    }


    protected object UniGrid_OnExternalDataBound(object sender, string sourceName, object parameter)
    {
        switch (sourceName)
        {
            case "isglobal":
                DataRowView drv = (DataRowView)parameter;
                bool isglobal = (ValidationHelper.GetInteger(drv["PollSiteID"], 0) <= 0);
                if (isglobal)
                {
                    return "<span class=\"StatusEnabled\">" + GetString("general.yes") + "</span>";
                }
                else
                {
                    return null;
                }
        }

        return parameter;
    }


    protected void UniGrid_OnBeforeDataReload()
    {
        UniGrid.GridView.Columns[5].Visible = this.DisplayGlobalColumn;
    }


    /// <summary>
    /// Handles the UniGrid's OnAction event.
    /// </summary>
    /// <param name="actionName">Name of item (button) that throws event</param>
    /// <param name="actionArgument">ID (value of Primary key) of corresponding data row</param>
    protected void UniGrid_OnAction(string actionName, object actionArgument)
    {
        if (actionName == "edit")
        {
            this.SelectedItemID = Convert.ToInt32(actionArgument);
            RaiseOnEdit();
        }
        else if (actionName == "delete")
        {
            PollInfo pi = PollInfoProvider.GetPollInfo(Convert.ToInt32(actionArgument));
            if (pi != null)
            {
                if ((pi.PollSiteID > 0) && !CheckPermissions("cms.polls", CMSAdminControl.PERMISSION_MODIFY) ||
                    (pi.PollSiteID <= 0) && !CheckPermissions("cms.polls", CMSAdminControl.PERMISSION_GLOBALMODIFY))
                {
                    return;
                }

                // Delete PollInfo object from database with it's dependences
                PollInfoProvider.DeletePollInfo(Convert.ToInt32(actionArgument));
            }

            this.ReloadData();
        }
    }


    void UniGrid_OnPageChanged(object sender, EventArgs e)
    {
        if (this.IsLiveSite)
        {
            this.ReloadData();
        }
    }


    void UniGrid_OnBeforeSorting(object sender, EventArgs e)
    {
        if (this.IsLiveSite)
        {
            this.ReloadData();
        }
    }


    /// <summary>
    /// Setups control.
    /// </summary>
    private void SetupControl()
    {
        if (this.GroupId > 0)
        {
            UniGrid.WhereCondition = "PollGroupID='" + this.GroupId.ToString() + "'";
        }

        // Add where condition from property
        if (this.WhereCondition != String.Empty)
        {
            if (!String.IsNullOrEmpty(UniGrid.WhereCondition) && (UniGrid.WhereCondition != this.WhereCondition))
            {
                UniGrid.WhereCondition += " AND " + this.WhereCondition;
            }
            else
            {
                UniGrid.WhereCondition = this.WhereCondition;
            }
        }
    }


    /// <summary>
    /// Reloads data.
    /// </summary>
    public override void ReloadData()
    {
        this.SetupControl();
        UniGrid.ReloadData();
    }
}
