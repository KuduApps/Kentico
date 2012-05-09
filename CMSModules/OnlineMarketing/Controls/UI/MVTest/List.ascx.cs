using System;
using System.Data;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.OnlineMarketing;
using CMS.SettingsProvider;

public partial class CMSModules_OnlineMarketing_Controls_UI_MVTest_List : CMSAdminListControl
{
    #region "Variables"

    private bool mShowFilter = false;
    private string mAliasPath = String.Empty;
    private int mNodeID = 0;
    private DateTime mTo = DateTimeHelper.ZERO_TIME;
    private DateTime mFrom = DateTimeHelper.ZERO_TIME;
    private bool mApplyTimeCondition = true;
    private bool mHideDeleteAction = false;
    protected string mEditPage = "Edit.aspx";

    #endregion


    #region "Properties"

    /// <summary>
    /// Inner grid.
    /// </summary>
    public UniGrid Grid
    {
        get
        {
            return this.gridElem;
        }
    }


    /// <summary>
    /// If true, time condition is applied (but only if filter is not shown)
    /// </summary>
    public bool ApplyTimeCondition
    {
        get
        {
            return mApplyTimeCondition;
        }
        set
        {
            mApplyTimeCondition = value;
        }
    }


    /// <summary>
    /// Test edit page
    /// </summary>
    public String EditPage
    {
        get
        {
            return mEditPage;
        }
        set
        {
            mEditPage = value;
        }
    }


    /// <summary>
    /// Indicates if the control should perform the operations.
    /// </summary>
    public override bool StopProcessing
    {
        get
        {
            return base.StopProcessing;
        }
        set
        {
            base.StopProcessing = value;
            this.gridElem.StopProcessing = value;
        }
    }


    /// <summary>
    /// Shows / hide object's context menu
    /// </summary>
    public bool ShowObjectMenu
    {
        get
        {
            return gridElem.ShowObjectMenu;
        }
        set
        {
            gridElem.ShowObjectMenu = value;
        }
    }


    /// <summary>
    /// Shows/Hide delete button in actions column
    /// </summary>
    public bool HideDeleteAction
    {
        get
        {
            return mHideDeleteAction;
        }
        set
        {
            mHideDeleteAction = value;
        }
    }


    /// <summary>
    /// Indicates if the control is used on the live site.
    /// </summary>
    public override bool IsLiveSite
    {
        get
        {
            return base.IsLiveSite;
        }
        set
        {
            base.IsLiveSite = value;
            gridElem.IsLiveSite = value;
        }
    }


    /// <summary>
    /// Alias path of the document which this MVT tests belongs to.
    /// </summary>
    public string AliasPath
    {
        get
        {
            return mAliasPath;
        }
        set
        {
            mAliasPath = value;
        }
    }


    /// <summary>
    /// Start of shown period.
    /// </summary>
    public DateTime From
    {
        get
        {
            return mFrom;
        }
        set
        {
            mFrom = value;
        }
    }


    /// <summary>
    /// End of show period.
    /// </summary>
    public DateTime To
    {
        get
        {
            return mTo;
        }
        set
        {
            mTo = value;
        }
    }


    /// <summary>
    /// If true delayed reload is used for grid.
    /// </summary>
    public bool DelayedReload
    {
        get
        {
            return gridElem.DelayedReload;
        }
        set
        {
            gridElem.DelayedReload = value;
        }
    }


    /// <summary>
    /// Indicates whether to show the filter.
    /// </summary>
    public bool ShowFilter
    {
        get
        {
            return mShowFilter;
        }
        set
        {
            mShowFilter = value;
        }
    }


    /// <summary>
    /// Show actions menu for unigrid
    /// </summary>
    public bool ShowActionsMenu
    {
        get
        {
            return gridElem.ShowActionsMenu;
        }
        set
        {
            gridElem.ShowActionsMenu = value;
        }
    }


    /// <summary>
    /// NodeID of the current document. (Used for checking the access permissions).
    /// </summary>
    public int NodeID
    {
        get
        {
            return mNodeID;
        }
        set
        {
            mNodeID = value;
        }
    }

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Grid initialization                
        gridElem.OnAction += new OnActionEventHandler(gridElem_OnAction);
        gridElem.OnExternalDataBound += new OnExternalDataBoundEventHandler(gridElem_OnExternalDataBound);

        // Add site dependecy to where condition
        gridElem.WhereCondition = SqlHelperClass.AddWhereCondition(gridElem.WhereCondition, "MVTestSiteID = " + CMSContext.CurrentSiteID);

        if (AliasPath != String.Empty)
        {
            // Add alias path condition - used in document depending abtest
            gridElem.WhereCondition = SqlHelperClass.AddWhereCondition(gridElem.WhereCondition, "MVTestPage = N'" + SqlHelperClass.GetSafeQueryString(AliasPath, false) + "'");
        }

        if (ShowFilter)
        {
            gridElem.GridName = "~/CMSModules/OnlineMarketing/Controls/UI/MVTest/ListNoFilter.xml";
            gridElem.WhereCondition = SqlHelperClass.AddWhereCondition(gridElem.WhereCondition, ucFilter.WhereCondition);

            if (ucFilter.Parameters != null)
            {
                gridElem.QueryParameters = ucFilter.Parameters;
            }
        }
        else
        {
            pnlFiler.Visible = false;
        }
    }


    /// <summary>
    /// PreRender event handler.
    /// </summary>
    protected override void OnPreRender(EventArgs e)
    {
        if (HideDeleteAction)
        {
            if (((gridElem.GridActions != null) && (gridElem.GridActions.Actions != null) && (gridElem.GridActions.Actions.Count > 0)))
            {
                gridElem.GridActions.Actions.RemoveAt(1);
            }
        }

        // If filter is show dont add time correction directly
        if (!ShowFilter && ApplyTimeCondition)
        {
            AddTimeCondition();
        }

        if (DelayedReload)
        {
            gridElem.ReloadData();
        }

        base.OnPreRender(e);
    }


    /// <summary>
    /// Handles UniGrid's OnAction event.
    /// </summary>
    /// <param name="actionName">Name of the action which should be performed</param>
    /// <param name="actionArgument">ID of the item the action should be performed with</param>
    protected void gridElem_OnAction(string actionName, object actionArgument)
    {
        int mvtestId = ValidationHelper.GetInteger(actionArgument, 0);
        if (mvtestId > 0)
        {
            switch (actionName.ToLower())
            {
                case "edit":
                    this.SelectedItemID = mvtestId;
                    this.RaiseOnEdit();
                    break;

                case "delete":
                    // Check manage permissions
                    if (CheckPermissions("CMS.MVTest", CMSAdminControl.PERMISSION_MANAGE))
                    {
                        // Delete the object
                        MVTestInfoProvider.DeleteMVTestInfo(mvtestId);
                        this.RaiseOnDelete();

                        // Reload data
                        gridElem.ReloadData();
                    }
                    break;
            }
        }
    }


    /// <summary>
    /// Handles Unigrid's OnExternalDataBound event.
    /// </summary>
    protected object gridElem_OnExternalDataBound(object sender, string sourceName, object parameter)
    {
        switch (sourceName.ToLower())
        {
            case "mvteststatus":
                int testID = ValidationHelper.GetInteger(parameter, 0);
                MVTestInfo testInfo = MVTestInfoProvider.GetMVTestInfo(testID);
                if (testInfo != null)
                {
                    if (!testInfo.MVTestEnabled)
                    {
                        return "<span class=\"StatusDisabled\">" + GetString("general.disabled") + "</span>";
                    }
                    else
                    {
                        MVTestStatusEnum statusOut = MVTestStatusEnum.Disabled;
                        string status = string.Empty;
                        if (MVTestInfoProvider.MVTestIsRunning(testInfo, out statusOut))
                        {
                            status = "<span class=\"StatusEnabled\">" + GetString("mvtest.status." + MVTestInfoProvider.GetMVTestStatus(testInfo)) + "</span>";
                        }
                        else
                        {
                            status = GetString("mvtest.status." + MVTestInfoProvider.GetMVTestStatus(testInfo));
                        }

                        return status;
                    }
                }

                return string.Empty;
        }
        return parameter;
    }


    /// <summary>
    /// Add from and to condition depending on given properties.
    /// </summary>
    private void AddTimeCondition()
    {
        QueryDataParameters parameters = new QueryDataParameters();

        if (From != DateTimeHelper.ZERO_TIME)
        {
            parameters.Add("@MVTestOpenFrom", From);

            gridElem.WhereCondition = SqlHelperClass.AddWhereCondition(gridElem.WhereCondition, "(MVTestOpenFrom>= @MVTestOpenFrom) OR (MVTestOpenTo IS NULL)");
        }

        if (To != DateTimeHelper.ZERO_TIME)
        {
            parameters.Add("@MVTestOpenTo", To);

            gridElem.WhereCondition = SqlHelperClass.AddWhereCondition(gridElem.WhereCondition, "(ABTestOpenTo<= @MVTestOpenTo) OR (MVTestOpenTo IS NULL) ");
        }

        gridElem.QueryParameters = parameters;
    }

    #endregion
}