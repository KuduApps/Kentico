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
using CMS.TreeEngine;

public partial class CMSModules_OnlineMarketing_Controls_UI_ABVariant_List : CMSAdminListControl
{
    #region "Variables"

    private int mTestID = 0;
    protected int nodeID = 0;
    protected ABTestInfo mABTest = null;
    protected bool hideValidityColumn = true;
    TreeProvider mTreeProvider = null;
    DataSet dsOriginal = null;

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
    /// Parent test ID.
    /// </summary>
    public int TestID
    {
        get
        {
            return mTestID;
        }
        set
        {
            mTestID = value;
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
    /// Gets ab test on demand.
    /// </summary>
    private ABTestInfo ABTest
    {
        get
        {
            if (mABTest == null)
            {
                mABTest = ABTestInfoProvider.GetABTestInfo(TestID);
            }
            return mABTest;
        }
    }


    /// <summary>
    /// Gets tree provider object on demand.
    /// </summary>
    private TreeProvider TreeProvider
    {
        get
        {
            if (mTreeProvider == null)
            {
                mTreeProvider = new TreeProvider();
            }
            return mTreeProvider;
        }
    }

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        nodeID = QueryHelper.GetInteger("nodeID", 0);
        // Grid initialization                
        gridElem.OnAction += new OnActionEventHandler(gridElem_OnAction);
        gridElem.OnExternalDataBound += new OnExternalDataBoundEventHandler(gridElem_OnExternalDataBound);
        gridElem.WhereCondition = SqlHelperClass.AddWhereCondition(gridElem.WhereCondition, "ABVariantTestID =" + TestID);
        gridElem.OnBeforeDataReload += new OnBeforeDataReload(gridElem_OnBeforeDataReload);
        gridElem.GridView.RowDataBound += GridView_RowDataBound;
    }


    /// <summary>
    /// Handles Unigrid's ExternalDataBound event.
    /// </summary>
    /// <param name="sender">Sender</param>
    /// <param name="sourceName">Source name</param>
    /// <param name="parameter">Parameter</param>
    protected object gridElem_OnExternalDataBound(object sender, string sourceName, object parameter)
    {
        switch (sourceName.ToLower())
        {
            case "culture":
                string result = "<img src=\"" + GetImageUrl("Design/Controls/UniGrid/Actions/Warning.png") + "\" alt=\"" + GetString("abtesting.variantculturewarning") + "\" title=\"" + GetString("abtesting.variantculturewarning") + "\"/>";
                if (ABTest != null)
                {
                    string currentSiteName = CMSContext.CurrentSiteName;
                    // If AB test is for specified culture
                    if (!string.IsNullOrEmpty(ABTest.ABTestCulture))
                    {
                        // Try to find document in required culture
                        if (TreeProvider.SelectSingleNode(currentSiteName, parameter.ToString(), ABTest.ABTestCulture, SettingsKeyProvider.GetBoolValue(currentSiteName + ".CombineWithDefaultCulture"), null, true) != null)
                        {
                            return string.Empty;
                        }
                    }
                    else
                    {
                        if (dsOriginal == null)
                        {
                            // Get all original document cultures 
                            dsOriginal = TreeProvider.SelectNodes(currentSiteName, ABTest.ABTestOriginalPage, null, SettingsKeyProvider.GetBoolValue(currentSiteName + ".CombineWithDefaultCulture"), null, null, "DocumentCulture", 0, true, 0, "DocumentCulture");
                        }

                        // Get all variant cultures
                        DataSet dsVariant = TreeProvider.SelectNodes(currentSiteName, parameter.ToString(), null, SettingsKeyProvider.GetBoolValue(currentSiteName + ".CombineWithDefaultCulture"), null, null, "DocumentCulture", 0, true, 0, "DocumentCulture");
                        if (!DataHelper.DataSourceIsEmpty(dsOriginal) && !DataHelper.DataSourceIsEmpty(dsVariant))
                        {
                            // First, check if document count is equal
                            int count = dsOriginal.Tables[0].Rows.Count;
                            if (count == dsVariant.Tables[0].Rows.Count)
                            {
                                // Loop thru all cultures and check if are equal
                                for (int i = 0; i != count; i++)
                                {
                                    if (!string.Equals(ValidationHelper.GetString(dsOriginal.Tables[0].Rows[i]["DocumentCulture"], ""), ValidationHelper.GetString(dsVariant.Tables[0].Rows[i]["DocumentCulture"], ""), StringComparison.InvariantCultureIgnoreCase))
                                    {
                                        hideValidityColumn = false;
                                        return result;
                                    }
                                }
                                return string.Empty;
                            }
                        }
                    }
                }
                hideValidityColumn = false;
                return result;
        }
        return string.Empty;
    }


    protected void GridView_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if ((e.Row.RowType == DataControlRowType.DataRow) && (ABTest != null))
        {
            string path = ValidationHelper.GetString(((DataRowView)(e.Row.DataItem)).Row["ABVariantPath"], string.Empty);
            if (path == ABTest.ABTestOriginalPage)
            {
                e.Row.Style.Add("background-color", "#FFFFDA");
            }                      
        }
    }


    /// <summary>
    /// Handles UniGrid's OnAction event.
    /// </summary>
    /// <param name="actionName">Name of the action which should be performed</param>
    /// <param name="actionArgument">ID of the item the action should be performed with</param>
    protected void gridElem_OnAction(string actionName, object actionArgument)
    {
        int variantId = ValidationHelper.GetInteger(actionArgument, 0);
        if (variantId > 0)
        {
            switch (actionName.ToLower())
            {
                case "edit":
                    this.SelectedItemID = variantId;
                    this.RaiseOnEdit();
                    break;

                case "delete":
                    if (CheckPermissions("CMS.ABTest", CMSAdminControl.PERMISSION_MANAGE))
                    {
                        // Delete the object
                        ABVariantInfoProvider.DeleteABVariantInfo(variantId);
                        this.RaiseOnDelete();

                        // Reload data
                        gridElem.ReloadData();
                    }
                    break;
            }
        }
    }


    protected void gridElem_OnBeforeDataReload()
    {
        // Hides semifinal (validity) column.
        if (hideValidityColumn)
        {
            gridElem.GridView.Columns[gridElem.GridView.Columns.Count - 2].Visible = false;
        }
    }

    #endregion
}