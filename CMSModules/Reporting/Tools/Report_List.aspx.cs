using System;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections.Generic;

using CMS.GlobalHelper;
using CMS.Reporting;
using CMS.CMSHelper;
using CMS.SiteProvider;
using CMS.UIControls;
using CMS.SettingsProvider;

public partial class CMSModules_Reporting_Tools_Report_List : CMSReportingPage
{
    #region "Constants"

    // Report macros
    const string REP_TABLE_MACRO = "%%control:ReportTable?";
    const string REP_GRAPH_MACRO = "%%control:ReportGraph?";
    const string REP_HTMLGRAPH_MACRO = "%%control:ReportHtmlGraph?";
    const string REP_VALUE_MACRO = "%%control:ReportValue?";

    #endregion


    #region "Variables"

    protected int categoryId = 0;

    #endregion


    #region "Page events"

    /// <summary>
    /// Handles the Load event of the Page control.
    /// </summary>
    protected void Page_Load(object sender, EventArgs e)
    {
        categoryId = QueryHelper.GetInteger("categoryid", 0);

        // Register script for refresh tree after delete/destroy
        string script = @"function RefreshAdditionalContent(){
                            if (parent.parent.frames['reportcategorytree'])
                            {
                                parent.parent.frames['reportcategorytree'].location.href = 'ReportCategory_tree.aspx?categoryid=" + categoryId + @"';
                            }
                        }";

        ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "RefreshTree", ScriptHelper.GetScript(script));

        // Used for clone and delete calls
        int reportID = QueryHelper.GetInteger("reportid", 0);
        bool clone = QueryHelper.GetBoolean("clone", false);
        if (clone)
        {
            Clone(reportID);
        }

        ReportCategoryInfo info = new ReportCategoryInfo();
        info = ReportCategoryInfoProvider.GetReportCategoryInfo(categoryId);
        if (info != null)
        {
            string categoryPath = info.CategoryPath;
            // Add the slash character at the end of the categoryPath
            if (!categoryPath.EndsWith("/"))
            {
                categoryPath += "/";
            }
            UniGrid.WhereCondition = "ObjectPath LIKE '" + SqlHelperClass.GetSafeQueryString(categoryPath, false) + "%' AND ObjectType = 'Report'";
        }

        UniGrid.OnAction += uniGrid_OnAction;
        UniGrid.ZeroRowsText = GetString("general.nodatafound");

        InitializeMasterPage();
    }


    /// <summary>
    /// Handles the UniGrid's OnAction event.
    /// </summary>
    /// <param name="actionName">Name of item (button) that throws event</param>
    /// <param name="actionArgument">ID (value of Primary key) of corresponding data row</param>
    protected void uniGrid_OnAction(string actionName, object actionArgument)
    {
        int reportId = ValidationHelper.GetInteger(actionArgument, 0);

        switch (actionName)
        {
            case "delete":
                // Check 'Modify' permission
                if (!CMSContext.CurrentUser.IsAuthorizedPerResource("cms.reporting", "Modify"))
                {
                    RedirectToAccessDenied("cms.reporting", "Modify");
                }

                // Delete ReportInfo object from database
                ReportInfoProvider.DeleteReportInfo(reportId);

                // Refresh tree after delete
                ltlScript.Text += ScriptHelper.GetScript("RefreshAdditionalContent();");
                break;

            case "clone":
                Clone(reportId);
                break;
        }
    }

    #endregion


    #region "Other methods"

    /// <summary>
    /// Initializes Master Page.
    /// </summary>
    protected void InitializeMasterPage()
    {
        Title = "Report list";
    }


    /// <summary>
    /// Clones the given report (including attachment files).
    /// </summary>
    /// <param name="reportId">Report id</param>
    protected void Clone(int reportId)
    {
        // Check 'Modify' permission
        if (!CMSContext.CurrentUser.IsAuthorizedPerResource("cms.reporting", "Modify"))
        {
            RedirectToAccessDenied("cms.reporting", "Modify");
        }

        // Try to get report info
        ReportInfo oldri = ReportInfoProvider.GetReportInfo(reportId);
        if (oldri == null)
        {
            return;
        }

        DataSet graph_ds = ReportGraphInfoProvider.GetGraphs(reportId);
        DataSet table_ds = ReportTableInfoProvider.GetTables(reportId);
        DataSet value_ds = ReportValueInfoProvider.GetValues(reportId);

        // Duplicate report info object
        ReportInfo ri = new ReportInfo(oldri, false);
        ri.ReportID = 0;
        ri.ReportGUID = Guid.NewGuid();

        // Duplicate report info
        string reportName = ri.ReportName;
        string oldReportName = ri.ReportName;

        string reportDispName = ri.ReportDisplayName;

        while (ReportInfoProvider.GetReportInfo(reportName) != null)
        {
            reportName = Increment(reportName, "_", "", 100);
            reportDispName = Increment(reportDispName, "(", ")", 450);
        }

        ri.ReportName = reportName;
        ri.ReportDisplayName = reportDispName;

        // Used to eliminate version from create object task
        using (CMSActionContext context = new CMSActionContext())
        {
            context.CreateVersion = false;
            ReportInfoProvider.SetReportInfo(ri);
        }

        string name;

        // Duplicate graph data
        if (!DataHelper.DataSourceIsEmpty(graph_ds))
        {
            foreach (DataRow dr in graph_ds.Tables[0].Rows)
            {
                // Duplicate the graph
                ReportGraphInfo rgi = new ReportGraphInfo(dr);
                rgi.GraphID = 0;
                rgi.GraphGUID = Guid.NewGuid();
                rgi.GraphReportID = ri.ReportID;
                name = rgi.GraphName;

                // Replace layout based on HTML or regular graph type
                ri.ReportLayout = ReplaceMacro(ri.ReportLayout, rgi.GraphIsHtml ? REP_HTMLGRAPH_MACRO : REP_GRAPH_MACRO, rgi.GraphName, name, oldReportName, reportName);
                rgi.GraphName = name;

                ReportGraphInfoProvider.SetReportGraphInfo(rgi);
            }
        }

        // Duplicate table data
        if (!DataHelper.DataSourceIsEmpty(table_ds))
        {
            foreach (DataRow dr in table_ds.Tables[0].Rows)
            {
                // Duplicate the table
                ReportTableInfo rti = new ReportTableInfo(dr);
                rti.TableID = 0;
                rti.TableGUID = Guid.NewGuid();
                rti.TableReportID = ri.ReportID;
                name = rti.TableName;

                ri.ReportLayout = ReplaceMacro(ri.ReportLayout, REP_TABLE_MACRO, rti.TableName, name, oldReportName, reportName);
                rti.TableName = name;

                ReportTableInfoProvider.SetReportTableInfo(rti);
            }
        }

        // Duplicate value data
        if (!DataHelper.DataSourceIsEmpty(value_ds))
        {
            foreach (DataRow dr in value_ds.Tables[0].Rows)
            {
                // Duplicate the value
                ReportValueInfo rvi = new ReportValueInfo(dr);
                rvi.ValueID = 0;
                rvi.ValueGUID = Guid.NewGuid();
                rvi.ValueReportID = ri.ReportID;
                name = rvi.ValueName;

                ri.ReportLayout = ReplaceMacro(ri.ReportLayout, REP_VALUE_MACRO, rvi.ValueName, name, oldReportName, reportName);
                rvi.ValueName = name;

                ReportValueInfoProvider.SetReportValueInfo(rvi);
            }
        }

        List<Guid> convTable = new List<Guid>();
        try
        {
            MetaFileInfoProvider.CopyMetaFiles(reportId, ri.ReportID, ReportingObjectType.REPORT, MetaFileInfoProvider.OBJECT_CATEGORY_LAYOUT, convTable);
        }
        catch (Exception e)
        {
            lblError.Visible = true;
            lblError.Text = e.Message;
            ReportInfoProvider.DeleteReportInfo(ri);
            return;
        }

        for (int i = 0; i < convTable.Count; i += 2)
        {
            Guid oldGuid = convTable[i];
            Guid newGuid = convTable[i + 1];
            ri.ReportLayout = ri.ReportLayout.Replace(oldGuid.ToString(), newGuid.ToString());
        }

        ReportInfoProvider.SetReportInfo(ri);

        // Refresh tree
        ltlScript.Text += "<script type=\"text/javascript\">";
        ltlScript.Text += @"if (parent.frames['reportcategorytree'])
                                {
                                    parent.frames['reportcategorytree'].location.href = 'ReportCategory_tree.aspx?reportid=" + ri.ReportID + @"';
                                }    
                                if (parent.parent.frames['reportcategorytree'])
                                {
                                    parent.parent.frames['reportcategorytree'].location.href = 'ReportCategory_tree.aspx?reportid=" + ri.ReportID + @"';
                                }                           
                 this.location.href = 'Report_Edit.aspx?reportId=" + Convert.ToString(ri.ReportID) + @"&saved=1&categoryID=" + categoryId + @"'
                </script>";
    }


    /// <summary>
    /// Increment counter at the end of string.
    /// </summary>
    /// <param name="s">String</param>
    /// <param name="lpar">Left parathenses</param>
    /// <param name="rpar">Right parathenses</param>
    /// <param name="lenghtLimit">Maximum length of output string</param>
    protected string Increment(string s, string lpar, string rpar, int lenghtLimit)
    {
        int i = 1;
        s = s.Trim();
        if ((rpar == String.Empty) || s.EndsWith(rpar))
        {
            int leftpar = s.LastIndexOf(lpar);
            if (lpar == rpar)
            {
                leftpar = s.LastIndexOf(lpar, leftpar - 1);
            }

            if (leftpar >= 0)
            {
                i = ValidationHelper.GetSafeInteger(s.Substring(leftpar + lpar.Length, s.Length - leftpar - lpar.Length - rpar.Length), 0);
                if (i > 0) // Remove parathenses only if parentheses found
                {
                    s = s.Remove(leftpar);
                }
                i++;
            }
        }

        string tail = lpar + i + rpar;
        if (s.Length + tail.Length > lenghtLimit)
        {
            s = s.Substring(0, s.Length - (s.Length + tail.Length - lenghtLimit));
        }
        s += tail;
        return s;
    }


    /// <summary>
    /// Replaces old macro with new macro in template string.
    /// </summary>
    /// <param name="template">Template</param>
    /// <param name="macro">Macro</param>
    /// <param name="oldValue">Old macro value</param>
    /// <param name="newValue">New macro value</param>
    /// <param name="oldReportName">Old report name</param>    
    /// <param name="newReportName">New report name</param>
    protected string ReplaceMacro(string template, string macro, string oldValue, string newValue, string oldReportName, string newReportName)
    {
        // Old name, old macro style, backward compatible
        string oldOrigValue = macro + oldValue + "%%";
        // Old macro name, but with full specification
        string oldFullValue = macro + oldReportName + "." + oldValue + "%%";

        newValue = macro + newReportName + "." + newValue + "%%";

        return template.Replace(oldOrigValue, newValue).Replace(oldFullValue, newValue);
    }

    #endregion
}
