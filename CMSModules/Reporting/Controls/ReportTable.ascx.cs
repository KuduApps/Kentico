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

using CMS.Reporting;
using CMS.GlobalHelper;
using CMS.EventLog;
using CMS.DataEngine;
using CMS.CMSHelper;
using CMS.FormEngine;

public partial class CMSModules_Reporting_Controls_ReportTable : AbstractReportControl
{
    #region "Variables"

    private string mReportTableName = String.Empty;
    private GridView mGridTable = null;
    private ReportTableInfo mTableInfo = null;
    private string mParameter = string.Empty;
    private int mPageSize = 0;
    private bool? mEnablePaging = null;

    #endregion


    #region "Properties"

    /// <summary>
    /// Gets the GridView object.
    /// </summary>
    protected GridView GridViewObject
    {
        get
        {
            if (mGridTable == null)
            {
                mGridTable = new GridView();
            }
            return mGridTable;
        }
    }


    /// <summary>
    /// Table name - prevent using viewstate  (problems with displayreportcontrol and postback).
    /// </summary>
    public override string Parameter
    {
        get
        {
            return mParameter;
        }
        set
        {
            mParameter = value;
        }
    }


    /// <summary>
    /// Table name.
    /// </summary>
    private string ReportTableName
    {
        get
        {
            return mReportTableName;
        }
        set
        {
            mReportTableName = value;
        }
    }


    /// <summary>
    /// Direct table info used by preview.
    /// </summary>
    public ReportTableInfo TableInfo
    {
        get
        {
            if (mTableInfo == null)
            {
                mTableInfo = ReportTableInfoProvider.GetReportTableInfo(Parameter);
            }
            return mTableInfo;
        }
        set
        {
            mTableInfo = value;
        }
    }


    /// <summary>
    /// Page size for paged tables
    /// </summary>
    public int PageSize
    {
        get
        {
            return mPageSize;
        }
        set
        {
            this.mPageSize = value;
        }
    }


    /// <summary>
    /// Enables/disables paging (if null report settings is used)
    /// </summary>
    public bool? EnablePaging
    {
        get
        {
            return mEnablePaging;
        }
        set
        {
            this.mEnablePaging = value;
        }
    }

    #endregion


    #region "Methods"

    /// <summary>
    /// Ensires teble info object.
    /// </summary>
    private void EnsureTableInfo()
    {
        ReportTableInfo rti = TableInfo;

        if (rti != null)
        {
            this.QueryIsStoredProcedure = rti.TableQueryIsStoredProcedure;

            // Resolve dynamic data macros
            if (DynamicMacros != null)
            {
                for (int i = 0; i <= this.DynamicMacros.GetUpperBound(0); i++)
                {
                    this.ContextResolver.AddDynamicParameter(DynamicMacros[i, 0], DynamicMacros[i, 1]);
                }
            }

            this.QueryText = ResolveMacros(rti.TableQuery);
            this.TableInfo = rti;
        }
    }


    /// <summary>
    /// Adds GridView to the controls collectio.
    /// </summary>
    private void LoadTableInfo()
    {
        EnsureTableInfo();

        if (TableInfo != null)
        {
            this.QueryIsStoredProcedure = TableInfo.TableQueryIsStoredProcedure;
            this.QueryText = ResolveMacros(TableInfo.TableQuery);

            // Enable paging has higher priority - if not set use report settings
            GridViewObject.AllowPaging = EnablePaging.HasValue ? EnablePaging.Value : ValidationHelper.GetBoolean(TableInfo.TableSettings["enablepaging"], false);

            if (GridViewObject.AllowPaging)
            {
                // Webpart - higher priority
                GridViewObject.PageSize = (PageSize > 0) ? PageSize : ValidationHelper.GetInteger(TableInfo.TableSettings["pagesize"], 10);
                GridViewObject.PagerSettings.Mode = (PagerButtons)ValidationHelper.GetInteger(TableInfo.TableSettings["pagemode"], (int)PagerButtons.Numeric);
                GridViewObject.PageIndexChanging += new GridViewPageEventHandler(GridViewObject_PageIndexChanging);
            }
            GridViewObject.AllowSorting = false;

            // Get SkinID from reportTable custom data
            string skinId = ValidationHelper.GetString(TableInfo.TableSettings["skinid"], "");
            if (skinId != String.Empty)
            {
                if (String.IsNullOrEmpty((GridViewObject.SkinID)))
                {
                    GridViewObject.SkinID = skinId;
                }
            }

            GridViewObject.ID = "reportGrid";

            // Add grid view control to the page
            this.plcGrid.Controls.Clear();
            this.plcGrid.Controls.Add(GridViewObject);

            if ((RenderCssClasses) && (String.IsNullOrEmpty(GridViewObject.SkinID)))
            {
                //Clear the css styles to eliminate control state
                GridViewObject.HeaderStyle.CssClass = "";
                GridViewObject.CssClass = "";
                GridViewObject.RowStyle.CssClass = "";
                GridViewObject.AlternatingRowStyle.CssClass = "";
            }
        }
    }


    /// <summary>
    /// Created grid view based on parameter from report table.
    /// </summary>
    protected override void CreateChildControls()
    {
        base.CreateChildControls();

        // Register context menu for export - if allowed
        if ((TableInfo != null) && ValidationHelper.GetBoolean(TableInfo.TableSettings["ExportEnabled"], false))
        {
            RegisterContextMenu(menuCont);
        }

        // Set table name from inline parameter
        this.ReportTableName = this.Parameter;
        LoadTableInfo();
    }


    /// <summary>
    /// Returns true if graph belongs to report.
    /// </summary>
    /// <param name="report">Report to validate</param>
    public override bool IsValid(ReportInfo report)
    {
        ReportTableInfo rti = TableInfo;

        if ((report != null) && (rti != null) && (report.ReportID == rti.TableReportID))
        {
            return true;
        }

        return false;
    }


    /// <summary>
    /// Reload data.
    /// </summary>
    public override void ReloadData(bool forceLoad)
    {
        if ((GraphImageWidth != 0) && (ComputedWidth == 0))
        {
            // Graph width is computed no need to create graph
            return;
        }

        this.Visible = true;

        // Indicaates whether exception was throw during data loading
        bool errorOccurred = false;
        ReportInfo ri = null;

        try
        {
            this.ReportTableName = this.Parameter;

            this.EnsureTableInfo();
            this.EnsureChildControls();

            //Test security
            if (TableInfo != null)
            {
                ri = ReportInfoProvider.GetReportInfo(TableInfo.TableReportID);
                if (ri != null)
                {
                    if (ri.ReportAccess != ReportAccessEnum.All)
                    {
                        if (!CMSContext.CurrentUser.IsAuthenticated())
                        {
                            this.Visible = false;
                            return;
                        }
                    }

                    //Set default parametrs directly if not set
                    if (this.ReportParameters == null)
                    {
                        FormInfo fi = new FormInfo(ri.ReportParameters);
                        // Get datarow with required columns
                        this.ReportParameters = fi.GetDataRow();
                        fi.LoadDefaultValues(this.ReportParameters, true);
                    }

                    ApplyTimeParameters();
                }
            }

            // Only use base parameters in case of stored procedure
            if (this.QueryIsStoredProcedure)
            {
                this.AllParameters = SpecialFunctions.ConvertDataRowToParams(this.ReportParameters, null);
            }

            // Load data
            DataSet ds = this.LoadData();

            // If no data load, set empty dataset
            if (DataHelper.DataSourceIsEmpty(ds))
            {
                string noRecordText = ValidationHelper.GetString(TableInfo.TableSettings["QueryNoRecordText"], String.Empty);
                if (noRecordText != String.Empty)
                {
                    GridViewObject.Visible = false;
                    lblInfo.Text = noRecordText;
                    lblInfo.Visible = true;
                    return;
                }

                this.Visible = false;
            }
            else
            {
                GridViewObject.Visible = true;
                // Resolve macros in column names
                int i = 0;
                foreach (DataColumn dc in ds.Tables[0].Columns)
                {
                    if (dc.ColumnName == "Column" + ((int)(i + 1)).ToString())
                    {
                        dc.ColumnName = ResolveMacros(ds.Tables[0].Rows[0][i].ToString());
                    }
                    else
                    {
                        dc.ColumnName = ResolveMacros(dc.ColumnName);
                    }
                    i++;
                }

                // Resolve macros in dataset
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    foreach (DataColumn dc in ds.Tables[0].Columns)
                    {
                        if (dc.DataType.FullName.ToLower() == "system.string")
                        {
                            dr[dc.ColumnName] = ResolveMacros(ValidationHelper.GetString(dr[dc.ColumnName], ""));
                        }
                    }
                }
            }

            ApplyStyles();

            // Databind to gridview control
            GridViewObject.DataSource = ds;
            this.EnsurePageIndex();
            GridViewObject.DataBind();

            if ((TableFirstColumnWidth != Unit.Empty) && (GridViewObject.Rows.Count > 0))
            {
                GridViewObject.Rows[0].Cells[0].Width = TableFirstColumnWidth;
            }

        }
        catch (Exception ex)
        {
            // Display error message, if data load fail
            lblError.Visible = true;
            lblError.Text = "[ReportTable.ascx] Error loading the data: " + ex.Message;
            EventLogProvider ev = new EventLogProvider();
            ev.LogEvent("Report table", "E", ex);
            errorOccurred = true;
        }

        // Export data
        if ((ri != null) && (!errorOccurred))
        {
            ProcessExport(ValidationHelper.GetCodeName(ri.ReportDisplayName));
        }
    }


    /// <summary>
    /// Ensures the current page index with dependenco on request data du to different contol's life cycle.
    /// </summary>
    private void EnsurePageIndex()
    {
        if ((GridViewObject != null) && (GridViewObject.AllowPaging))
        {
            // Get current postback target
            string eventTarget = Request.Params["__EVENTTARGET"];

            // Handle paging manually because of lifecycle of the control
            if (String.Compare(eventTarget, GridViewObject.UniqueID) == 0)
            {
                // Get the current page value
                string eventArg = ValidationHelper.GetString(Request.Params["__EVENTARGUMENT"], String.Empty);

                string[] args = eventArg.Split('$');
                if ((args.Length == 2) && (String.Compare(args[0], "page", true) == 0))
                {
                    string pageValue = args[1];
                    int pageIndex = 0;
                    // Switch by page value  0,1.... first,last
                    switch (pageValue.ToLower())
                    {
                        // Last item
                        case "last":
                            // Check whether page count is available
                            if (GridViewObject.PageCount > 0)
                            {
                                pageIndex = GridViewObject.PageCount - 1;
                            }
                            // if page count is not defined, try compute page count
                            else
                            {
                                DataSet ds = GridViewObject.DataSource as DataSet;
                                if (!DataHelper.DataSourceIsEmpty(ds))
                                {
                                    pageIndex = ds.Tables[0].Rows.Count / GridViewObject.PageSize;
                                }
                            }
                            break;

                        case "next":
                            pageIndex = GridViewObject.PageIndex + 1;
                            break;

                        case "prev":
                            pageIndex = GridViewObject.PageIndex - 1;
                            break;

                        // Page number
                        default:
                            pageIndex = ValidationHelper.GetInteger(pageValue, 1) - 1;
                            break;
                    }

                    GridViewObject.PageIndex = pageIndex;
                }

            }
        }
    }


    /// <summary>
    /// Sets default grid styles and apply skinID (if any).
    /// </summary>
    private void ApplyStyles()
    {
        // Style frid view
        if (GridViewObject != null)
        {
            GridViewObject.CellPadding = 3;
            GridViewObject.GridLines = GridLines.Horizontal;
            GridViewObject.HeaderStyle.HorizontalAlign = HorizontalAlign.Left;

            if ((RenderCssClasses) && (String.IsNullOrEmpty(GridViewObject.SkinID)))
            {
                GridViewObject.HeaderStyle.CssClass = "UniGridHead";
                GridViewObject.HeaderStyle.Wrap = false;
                GridViewObject.CssClass = "UniGridGrid";
                GridViewObject.RowStyle.CssClass = "EvenRow";
                GridViewObject.AlternatingRowStyle.CssClass = "OddRow";
                GridViewObject.FooterStyle.CssClass = "UniGridHead";
                GridViewObject.PagerStyle.CssClass = "PagerRow";
            }
        }
    }


    /// <summary>
    /// Handles paging on live site.
    /// </summary>
    protected void GridViewObject_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }

    #endregion
}
