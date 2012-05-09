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
using CMS.WebAnalytics;

public partial class CMSModules_OnlineMarketing_Controls_UI_AbTest_List : CMSAdminListControl
{
    #region "Variables"

    private bool mShowFilter = true;
    private string mAliasPath = String.Empty;
    private int mNodeID = 0;
    private DateTime mTo = DateTimeHelper.ZERO_TIME;
    private DateTime mFrom = DateTimeHelper.ZERO_TIME;
    private bool webAnalyticsEnabled = true;
    private bool mHideDeleteAction = false;

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
    /// Alias path of document to which this abtest belongs.
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
    /// Indicates whether show filter.
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
    /// NodeID.
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
        gridElem.WhereCondition = SqlHelperClass.AddWhereCondition(gridElem.WhereCondition, "ABTestSiteID=" + CMSContext.CurrentSiteID);

        // Add alias path condition - used in document depending abtest
        if (AliasPath != String.Empty)
        {
            gridElem.WhereCondition = SqlHelperClass.AddWhereCondition(gridElem.WhereCondition, "ABTestOriginalPage=N'" + SqlHelperClass.GetSafeQueryString(AliasPath, false) + "'");
        }

        if (ShowFilter)
        {
            gridElem.GridName = "~/CMSModules/OnlineMarketing/Controls/UI/AbTest/ListNoFilter.xml";
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

        // Display disabled information
        if (!AnalyticsHelper.AnalyticsEnabled(CMSContext.CurrentSiteName)
            || !AnalyticsHelper.TrackConversionsEnabled(CMSContext.CurrentSiteName))
        {
            webAnalyticsEnabled = false;
        }
    }


    /// <summary>
    /// Add from and to condition depending on given properties.
    /// </summary>
    private void AddTimeCondition()
    {
        QueryDataParameters parameters = new QueryDataParameters();

        if (From != DateTimeHelper.ZERO_TIME)
        {
            parameters.Add("@ABTestOpenFrom", From);

            gridElem.WhereCondition = SqlHelperClass.AddWhereCondition(gridElem.WhereCondition, "(ABTestOpenFrom>= @ABTestOpenFrom) OR (ABTestOpenTo IS NULL)");
        }

        if (To != DateTimeHelper.ZERO_TIME)
        {
            parameters.Add("@ABTestOpenTo", To);

            gridElem.WhereCondition = SqlHelperClass.AddWhereCondition(gridElem.WhereCondition, "(ABTestOpenTo<= @ABTestOpenTo) OR(ABTestOpenTo IS NULL) ");
        }

        gridElem.QueryParameters = parameters;
    }


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
        if (!ShowFilter)
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
        int abTestId = ValidationHelper.GetInteger(actionArgument, 0);
        if (abTestId > 0)
        {
            switch (actionName.ToLower())
            {
                case "edit":
                    this.SelectedItemID = abTestId;
                    this.RaiseOnEdit();
                    break;

                case "delete":
                    if (CheckPermissions("CMS.ABTest", CMSAdminControl.PERMISSION_MANAGE))
                    {
                        // Delete the object
                        ABTestInfoProvider.DeleteABTestInfo(abTestId);
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
            case "abteststatus":
                int testID = ValidationHelper.GetInteger(parameter, 0);
                ABTestInfo testInfo = ABTestInfoProvider.GetABTestInfo(testID);
                if (testInfo != null)
                {
                    if (!testInfo.ABTestEnabled)
                    {
                        return "<span class=\"StatusDisabled\">" + GetString("general.disabled") + "</span>";
                    }
                    else
                    {
                        string status = string.Empty;

                        if (ABTestInfoProvider.ABTestIsRunning(testInfo))
                        {
                            // Display disabled information
                            if (!webAnalyticsEnabled)
                            {
                                return GetString("abtesting.statusNone");
                            }

                            if (!ABTestInfoProvider.ContainsVariants(testInfo))
                            {
                                // Display warning when the test does not contain any variant
                                return "<img src=\"" + GetImageUrl("Design/Controls/UniGrid/Actions/Warning.png") + "\" alt=\"" + GetString("abtest.novariants") + "\" title=\""
                                    + GetString("abtest.novariants") + "\" />&nbsp;&nbsp;"
                                    + GetString("abtesting.status" + ABTestStatusEnum.None);
                            }

                            status += "<span class=\"StatusEnabled\">" + GetString("abtesting.status" + ABTestInfoProvider.GetABTestStatus(testInfo)) + "</span>";
                        }
                        else
                        {
                            status += GetString("abtesting.status" + ABTestInfoProvider.GetABTestStatus(testInfo));
                        }

                        return status;
                    }
                }

                return string.Empty;
        }

        return parameter;
    }

    #endregion
}