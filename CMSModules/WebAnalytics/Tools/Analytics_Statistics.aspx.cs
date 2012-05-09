using System;
using System.Collections;
using System.Data;
using System.Web.UI.WebControls;

using CMS.GlobalHelper;
using CMS.SettingsProvider;
using CMS.SiteProvider;
using CMS.UIControls;
using CMS.WebAnalytics;
using CMS.CMSHelper;

public partial class CMSModules_WebAnalytics_Tools_Analytics_Statistics : CMSWebAnalyticsPage
{
    #region "Variables"

    /// <summary>
    /// List of special conversions not displayed either in custom node list or in ui list.
    /// </summary>
    const string additionalConversions = "'visitfirst';'visitreturn';'referringsite_direct';'referringsite_search';'referringsite_referring';'referringsite_local';'avgtimeonpage'";


    /// <summary>
    /// Where condition used for select custom conversions.
    /// </summary>
    string customWhereCondition = String.Empty;


    /// <summary>
    /// Sets to true when the first node (containing a page url) is selected.
    /// </summary>
    bool firstElementSelected = false;


    /// <summary>
    /// Name of tree node that gets preselected.
    /// </summary>
    string selectedNode = null;

    #endregion


    #region "Methods"

    protected override void OnInit(EventArgs e)
    {
        // Get selected node from URL
        this.selectedNode = QueryHelper.GetString("node", string.Empty);

        base.OnInit(e);
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        this.menuElem.OnNodeCreated += new CMSAdminControls_UI_UIProfiles_UIMenu.NodeCreatedEventHandler(menuElem_OnNodeCreated);

        ScriptHelper.RegisterClientScriptBlock(this.Page, typeof(string), "AdministrationLoadItem", ScriptHelper.GetScript(
            "function LoadItem(elementName, elementUrl) \n" +
            "{ \n" +
            "  parent.frames['analyticsDefault'].location.href = elementUrl; \n" +
            "} \n"));

        this.menuElem.EnableRootSelect = false;

        // If node preselection is happening, expand the navigation tree
        if (!String.IsNullOrEmpty(this.selectedNode))
        {
            this.menuElem.ExpandLevel = 1;
        }
        else
        {
            this.menuElem.ExpandLevel = 0;
        }

        // Get NOT custom conversions from UI elements 
        UIElementInfo root = UIElementInfoProvider.GetRootUIElementInfo("CMS.WebAnalytics");
        if (root != null)
        {
            // Get all UI elements to filter custom reports
            DataSet dsElems = UIElementInfoProvider.GetUIElements("ElementIDPath LIKE '" + DataHelper.EscapeLikeQueryPatterns(root.ElementIDPath, true, true, true) + "/%'", String.Empty, 0, "ElementName,ElementTargetUrl");
            if (!SqlHelperClass.DataSourceIsEmpty(dsElems) && (dsElems.Tables.Count > 0))
            {
                // Condition for custom reports
                customWhereCondition = "StatisticsCode NOT IN (";
                foreach (DataRow dr in dsElems.Tables[0].Rows)
                {
                    string codeName = ValidationHelper.GetString(dr["ElementName"], String.Empty);
                    customWhereCondition += "N'" + SqlHelperClass.GetSafeQueryString(codeName, false) + "',";
                }

                // Add special cases - dont want to show them in UI or Custom report section
                customWhereCondition += additionalConversions.Replace(';', ',');

                customWhereCondition = customWhereCondition.TrimEnd(new char[] { ',' });
                customWhereCondition += ")";

                // Filter AB Testing
                customWhereCondition += " AND (StatisticsCode NOT LIKE 'abconversion;%') AND (StatisticsCode NOT LIKE 'mvtconversion;%') AND (StatisticsCode NOT LIKE 'campconversion;%') ";
            }
        }
    }


    TreeNode menuElem_OnNodeCreated(UIElementInfo uiElement, TreeNode defaultNode)
    {
        String elementName = uiElement.ElementName.ToLower();

        // Remove Optimalization node when module OnlineMarketing not present        
        if (elementName == "optimalization")
        {
            if (!ModuleEntry.IsModuleLoaded(ModuleEntry.ONLINEMARKETING))
            {
                return null;
            }
        }

        // Select first intem under node
        if (!firstElementSelected)
        {
            // Resolve hash
            string url = URLHelper.EnsureHashToQueryParameters(uiElement.ElementTargetURL);

            // Is a page node (with page url)
            if (url != "@")
            {
                firstElementSelected = true;
                SelectItem(uiElement.ElementName, url);
            }
            // Is a category node (without page url)
            else
            {
                // Try to display a child element
                if (uiElement.ElementChildCount > 0)
                {
                    defaultNode.Expanded = true;
                }
            }
        }

        // Preselect node 
        if (uiElement.ElementName.ToLower() == this.selectedNode.ToLower())
        {
            this.SelectItem(uiElement.ElementName, URLHelper.EnsureHashToQueryParameters(uiElement.ElementTargetURL));
        }

        String imagesUrl = "CMSModules/CMS_WebAnalytics/";
        if (uiElement != null)
        {
            if (!IsToolsUIElementAvailable(uiElement))
            {
                return null;
            }
        }

        // Add all custom reports
        if (elementName == "custom")
        {
            customWhereCondition = SqlHelperClass.AddWhereCondition(customWhereCondition, " StatisticsSiteID = " + CMSContext.CurrentSiteID);
            DataSet ds = StatisticsInfoProvider.GetCodeNames(customWhereCondition, "StatisticsCode ASC", 0, "StatisticsCode");

            // If no custom reports found - hide Custom Reports node
            if (DataHelper.DataSourceIsEmpty(ds))
            {
                return null;
            }

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                TreeNode childNode = new TreeNode();

                string codeName = ValidationHelper.GetString(dr["StatisticsCode"], String.Empty).ToLower();
                string name = GetString("analytics_codename." + codeName);
                string dataCodeName = GetDataCodeName(codeName);
                string reportCodeName = GetReportCodeNames(codeName);
                string reportUrl = "Analytics_Report.aspx?statCodeName=" + codeName + "&dataCodeName=" + dataCodeName + "&reportCodeName=" + reportCodeName + "&isCustom=1";

                childNode.Text = "<span id=\"node_" + codeName + "\" class=\"ContentTreeItem\" name=\"treeNode\" onclick=\"SelectNode('" + codeName + "');parent.frames['analyticsDefault'].location.href = '" + reportUrl + "' ; \"><span class=\"Name\">" + name + "</span></span>";
                childNode.NavigateUrl = "~/CMSModules/WebAnalytics/Tools/Analytics_Statistics.aspx#";

                // Icon 
                String imgPath = GetImageUrl(imagesUrl + codeName.Replace(".", "_") + ".png");
                if (FileHelper.FileExists(imgPath))
                {
                    childNode.ImageUrl = imgPath;
                }
                else
                {
                    childNode.ImageUrl = GetImageUrl(imagesUrl + "statistics.png");
                }
                defaultNode.ChildNodes.Add(childNode);
            }
        }
        return defaultNode;
    }


    /// <summary>
    /// Select given node.
    /// </summary>
    /// <param name="node">Name of node to select</param>
    /// <param name="url">Node's url</param>
    private void SelectItem(string node, string url)
    {
        ltlScript.Text = ScriptHelper.GetScript("SelectNode('" + node + "');parent.frames['analyticsDefault'].location.href = '" + ResolveUrl(url) + "'");
    }


    /// <summary>
    /// Returns generic report code names (based on analytics code name).
    /// </summary>
    /// <param name="statCodeName">Analytics code name (pageviews, pageviews.multilingual...)</param>
    private static string GetReportCodeNames(string statCodeName)
    {
        string result = "";
        result += statCodeName + ".yearreport";
        result += ";" + statCodeName + ".monthreport";
        result += ";" + statCodeName + ".weekreport";
        result += ";" + statCodeName + ".dayreport";
        result += ";" + statCodeName + ".hourreport";
        return result;
    }


    /// <summary>
    /// Returns data code name from analytics code name.
    /// </summary>
    /// <param name="statCodeName">Analytics code name (pageviews, pageviews.multilingual...)</param>
    private static string GetDataCodeName(string statCodeName)
    {
        int pos = statCodeName.IndexOf('.');
        if (pos > 0)
        {
            return statCodeName.Substring(0, pos);
        }

        return statCodeName;
    }

    #endregion
}