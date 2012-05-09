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
using System.Collections.Generic;

using CMS.GlobalHelper;
using CMS.FormControls;
using CMS.Reporting;
using CMS.FormEngine;
using CMS.IO;
using CMS.ExtendedControls;
using CMS.SettingsProvider;

public partial class CMSModules_Reporting_FormControls_ReportItemSelector : FormEngineUserControl
{
    #region "Private variables"

    ReportItemType mReportItemType;
    int mReportID;
    bool mDisplay = true;
    DataSet currentDataSet = null;
    bool keepDataInWindowsHelper = false;
    string firstItemText = String.Empty;
    bool mShowItemSelector = true;

    #endregion


    #region "Public properties"

    /// <summary>
    /// Returns an array of values of any other fields returned by the control.
    /// </summary>
    /// <remarks>It returns an array where first dimension is attribute name and the second dimension is its value.</remarks>
    public override object[,] GetOtherValues()
    {
        // Get current dataset
        DataSet ds = CurrentDataSet;

        // Set properties names
        object[,] values = new object[2, 2];
        values[0, 0] = "ParametersXmlSchema";
        values[1, 0] = "ParametersXmlData";

        // Check whether dataset is defined
        if (!DataHelper.DataSourceIsEmpty(ds))
        {
            // Set dataset values
            values[0, 1] = ds.GetXmlSchema();
            values[1, 1] = ds.GetXml();
        }

        return values;
    }


    /// <summary>
    /// Gets the current data set.
    /// </summary>
    private DataSet CurrentDataSet
    {
        get
        {
            DataSet ds = WindowHelper.GetItem(CurrentGuid().ToString()) as DataSet;
            if (DataHelper.DataSourceIsEmpty(ds))
            {
                if (DataHelper.DataSourceIsEmpty(currentDataSet))
                {
                    currentDataSet = LoadFromXML(Convert.ToString(this.ViewState["ParametersXmlData"]), Convert.ToString(this.ViewState["ParametersXmlSchema"]));
                }
                ds = currentDataSet;
            }
            return ds;
        }
    }


    /// <summary>
    /// If false control shows only report selector.
    /// </summary>
    public bool ShowItemSelector
    {
        get
        {
            return mShowItemSelector;
        }
        set
        {
            mShowItemSelector = value;
        }
    }


    /// <summary>
    /// Gets or sets field value.
    /// </summary>
    public override object Value
    {
        get
        {
            EnsureChildControls();

            string usReportsValue = GetString(usReports.Value.ToString());
            string usItemsValue = GetString(usItems.Value.ToString());

            if (!ShowItemSelector)
            {
                if (usReportsValue == "0")
                {
                    return String.Empty;
                }
                return usReportsValue;
            }
            else
            {
                if ((usReportsValue == "0") || (usItemsValue == "0"))
                {
                    return String.Empty;
                }
                return String.Format("{0};{1}", usReportsValue, usItemsValue);
            }
            
        }
        set
        {
            EnsureChildControls();

            // Convert input value to string
            string values = Convert.ToString(value);

            // Check whether value is defined
            if (!String.IsNullOrEmpty(values))
            {
                if (ShowItemSelector)
                {
                    // Get report name and item name
                    string[] items = values.Split(';');
                    // Check whether all required items are defined
                    if ((items != null) && (items.Length == 2))
                    {
                        // Set report and item values
                        usReports.Value = items[0];
                        usItems.Value = items[1];
                    }
                }
                else
                {
                    usReports.Value = values;
                }

                if ((this.Form != null) && (this.Form.Data != null) && !RequestHelper.IsPostBack())
                {
                    // Check if the schema information is available
                    IDataContainer data = this.Form.Data;
                    if (data.ContainsColumn("ParametersXmlSchema") && data.ContainsColumn("ParametersXmlData"))
                    {
                        // Get xml schema and data                    
                        string schema = Convert.ToString(this.Form.GetFieldValue("ParametersXmlSchema"));
                        string xml = Convert.ToString(this.Form.GetFieldValue("ParametersXmlData"));

                        LoadFromXML(xml, schema);
                    }
                }
            }
        }
    }


    /// <summary>
    /// Type of report (graph,table,value).
    /// </summary>
    public ReportItemType ReportType
    {
        get
        {
            return mReportItemType;
        }
        set
        {
            mReportItemType = value;
        }
    }


    /// <summary>
    /// Report Id.
    /// </summary>
    public int ReportID
    {
        get
        {
            return mReportID;
        }
        set
        {
            mReportID = value;
        }
    }


    /// <summary>
    /// Gets the item uniselector drop down list client id for javascript use.
    /// </summary>
    public string UniSelectorClientID
    {
        get
        {
            EnsureChildControls();
            return (string)usItems.DropDownSingleSelect.ClientID;
        }
    }


    /// <summary>
    /// Indicates weather display report drop down list.
    /// </summary>
    public bool Display
    {
        get
        {
            return mDisplay;
        }
        set
        {
            mDisplay = value;
        }
    }

    #endregion


    #region "Methods"

    /// <summary>
    /// Loads dataset to windows helper from schema and data xml definitions.
    /// </summary>
    /// <param name="xml">XML data</param>
    /// <param name="schema">XML schema</param>
    protected DataSet LoadFromXML(string xml, string schema)
    {
        // Check whether schema and data are defined
        if (!String.IsNullOrEmpty(schema) && !String.IsNullOrEmpty(xml))
        {
            //Create data set from xml
            DataSet ds = new DataSet();

            // Load schnema
            StringReader sr = StringReader.New(schema);
            ds.ReadXmlSchema(sr);

            // Load data
            ds.TryReadXml(xml);

            // Set current dataset
            WindowHelper.Add(CurrentGuid().ToString(), ds);

            return ds;
        }

        return null;
    }


    /// <summary>
    /// Builds conditions for particular type of selector.
    /// </summary>
    protected void BuildConditions()
    {
        switch (mReportItemType)
        {
            case ReportItemType.Graph:
                usItems.WhereCondition = "GraphReportID = " + ReportID + " AND (GraphIsHtml IS NULL OR GraphIsHtml = 0)";
                usItems.DisplayNameFormat = "{%GraphDisplayName%}";
                usItems.ReturnColumnName = "GraphName";
                usItems.ObjectType = "reporting.reportgraph";
                firstItemText = GetString("rep.graph.pleaseselect");
                usReports.WhereCondition = "EXISTS (SELECT GraphID FROM Reporting_ReportGraph as graph WHERE (graph.GraphIsHtml IS NULL OR graph.GraphIsHtml = 0) AND graph.GraphReportID = ReportID)";
                break;

            case ReportItemType.HtmlGraph:
                usItems.WhereCondition = "GraphReportID = " + ReportID + " AND (GraphIsHtml = 1)";
                usItems.DisplayNameFormat = "{%GraphDisplayName%}";
                usItems.ReturnColumnName = "GraphName";
                usItems.ObjectType = "reporting.reportgraph";
                firstItemText = GetString("rep.graph.pleaseselect");
                usReports.WhereCondition = "EXISTS (SELECT GraphID FROM Reporting_ReportGraph as graph WHERE (graph.GraphIsHtml = 1) AND graph.GraphReportID = ReportID)";
                break;

            case ReportItemType.Table:
                usItems.WhereCondition = "TableReportID = " + ReportID;
                usItems.ObjectType = "reporting.reporttable";
                usItems.DisplayNameFormat = "{%TableDisplayName%}";
                usItems.ReturnColumnName = "TableName";
                firstItemText = GetString("rep.table.pleaseselect");
                usReports.WhereCondition = "EXISTS (SELECT TableID FROM Reporting_ReportTable as reporttable WHERE reporttable.TableReportID = ReportID)";
                break;

            case ReportItemType.Value:
                usItems.WhereCondition = "ValueReportID = " + ReportID;
                usItems.ObjectType = "reporting.reportvalue";
                usItems.DisplayNameFormat = "{%ValueDisplayName%}";
                usItems.ReturnColumnName = "ValueName";
                firstItemText = GetString("rep.value.pleaseselect");
                usReports.WhereCondition = "EXISTS (SELECT ValueID FROM Reporting_ReportValue as value WHERE value.ValueReportID = ReportID)";
                break;

            // By default do notning
            default:
                break;
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        usReports.OnSelectionChanged += usReports_OnSelectionChanged;        
        string [,]specialFields = new string [1,2];

        pnlReports.Attributes.Add("style", "margin-bottom:3px");        

        string reportName = ValidationHelper.GetString(usReports.Value, String.Empty);
        ReportInfo ri = ReportInfoProvider.GetReportInfo(reportName);
        if (ri != null)
        {
            usItems.Enabled = true;
            //test if there is any item visible in report parameters
            FormInfo fi = new FormInfo(ri.ReportParameters);

            //Get dataset from cache
            DataSet ds = (DataSet)WindowHelper.GetItem(hdnGuid.Value);
            DataRow dr = fi.GetDataRow();
            fi.LoadDefaultValues(dr);
            bool itemVisible = false;
            List<FormItem> items = fi.ItemsList;
            foreach (FormItem item in items)
            {
                FormFieldInfo ffi = item as FormFieldInfo;
                if (ffi != null)
                {
                    if (ffi.Visible)
                    {
                        itemVisible = true;
                        break;
                    }
                }
            }

            ReportID = ri.ReportID;

            if (!itemVisible)
            {
                plcParametersButtons.Visible = false;
            }
            else
            {
                plcParametersButtons.Visible = true;
            }                        
        }
        else
        {
            plcParametersButtons.Visible = false;
            usItems.Enabled = false;
        }

        ltlScript.Text = ScriptHelper.GetScript("function refresh () {" + ControlsHelper.GetPostBackEventReference(pnlUpdate, String.Empty) + "}");
        usReports.DropDownSingleSelect.AutoPostBack = true;

        if (!mDisplay)
        {
            pnlReports.Visible = false;
            plcParametersButtons.Visible = false;
            usItems.Enabled = true;
        }

        if (!ShowItemSelector)
        {
            pnlItems.Visible = false;
        }

        usItems.IsLiveSite = IsLiveSite;
        usReports.IsLiveSite = IsLiveSite;
        ugParameters.GridName = "~/CMSModules/Reporting/FormControls/ReportParametersList.xml";
        ugParameters.ZeroRowsText = String.Empty;
        ugParameters.PageSize = "##ALL##";
        ugParameters.Pager.DefaultPageSize = -1;

        BuildConditions();

        // First item as "please select string" - not default "none"
        usItems.AllowEmpty = false;
        usReports.AllowEmpty = false;
        specialFields[0, 0] = "(" + GetString("rep.pleaseselect") + ")";
        specialFields[0, 1] = "0";
        usReports.SpecialFields = specialFields;

        if (ri == null)
        {
            string[,] itemSpecialFields = new string[1, 2];
            itemSpecialFields[0, 0] = "(" + firstItemText + ")";
            itemSpecialFields[0, 1] = "0";
            usItems.SpecialFields = itemSpecialFields;
        }

        if (ShowItemSelector)
        {
            ReloadItems();
        }
    }


    /// <summary>
    /// Forces Items Uni select to reload.
    /// </summary>
    public void ReloadItems()
    {
        string selected = usItems.DropDownSingleSelect.SelectedValue;

        usItems.Reload(true);

        try
        {
            usItems.DropDownSingleSelect.SelectedValue = selected;
        }
        catch
        {
        }
    }


    /// <summary>
    /// Gets GUID from hidden field .. if not there create new one.
    /// </summary>
    private Guid CurrentGuid()
    {
        Guid guid = Guid.Empty;

        // Try get current guid from hidden field
        string hiddenValue = hdnGuid.Value; 

        // On postback hidden field must be already set. Can be empty if control state not already loaded =>
        // Do nothing - will be loaded afterwards.
        if (!URLHelper.IsPostback())
        {
            // If hidden value is not defined generate new one
            if (String.IsNullOrEmpty(hiddenValue))
            {
                guid = Guid.NewGuid();
                hdnGuid.Value = guid.ToString();
            }
            else
            {
                hdnGuid.Value = hiddenValue;
                guid = new Guid(hiddenValue);
            }
        }

        return guid;
    }


    protected override void OnPreRender(EventArgs e)
    {
        //apply reportid condition if report was selected wia uniselector
        string reportName = ValidationHelper.GetString(usReports.Value, String.Empty);
        ReportInfo ri = ReportInfoProvider.GetReportInfo(reportName);
        if (ri != null)
        {
            FormInfo fi = new FormInfo(ri.ReportParameters);
            DataRow dr = fi.GetDataRow();
            DataSet ds = this.CurrentDataSet;

            this.ViewState["ParametersXmlData"] = null;
            this.ViewState["ParametersXmlSchema"] = null;

            if (!DataHelper.DataSourceIsEmpty(ds))
            {
                this.ViewState["ParametersXmlData"] = ds.GetXml();
                this.ViewState["ParametersXmlSchema"] = ds.GetXmlSchema();
            }

            if (!keepDataInWindowsHelper)
            {
                WindowHelper.Remove(CurrentGuid().ToString());
            }

            if (!DataHelper.DataSourceIsEmpty(ds))
            {
                ds = DataHelper.DataSetPivot(ds, new string[] { "ParameterName", "ParameterValue" });
                ugParameters.DataSource = ds;
                ugParameters.ReloadData();
                pnlParameters.Visible = true;
            }
            else
            {
                pnlParameters.Visible = false;
            }
        }
        else
        {
            pnlParameters.Visible = false;
        }

        base.OnPreRender(e);
    }


    protected void btnSet_Click(object sender, EventArgs e)
    {
        WindowHelper.Add(CurrentGuid().ToString(), CurrentDataSet);
        ScriptHelper.RegisterStartupScript(this.Page, typeof(Page), "OpenModalWindowReportItem", ScriptHelper.GetScript("modalDialog('" + ResolveUrl("~/CMSModules/Reporting/Dialogs/ReportParametersSelector.aspx?ReportID=" + ReportID + "&guid=" + CurrentGuid().ToString()) + "','ReportParametersDialog', 700, 500);"));
        keepDataInWindowsHelper = true;
    }


    protected void btnClear_Click(object sender, EventArgs e)
    {
        WindowHelper.Remove(CurrentGuid().ToString());
        this.ViewState["ParametersXmlData"] = null;
        this.ViewState["ParametersXmlSchema"] = null;
    }


    protected void usReports_OnSelectionChanged(object sender, EventArgs ea)
    {
        WindowHelper.Remove(CurrentGuid().ToString());
        this.ViewState["ParametersXmlData"] = null;
        this.ViewState["ParametersXmlSchema"] = null;

        // Try to set first item
        if (usItems.DropDownSingleSelect.Items.Count > 0)
        {
            usItems.DropDownSingleSelect.SelectedIndex = 0;
        }
    }

    #endregion

}
