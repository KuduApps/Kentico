using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using CMS.CMSHelper;
using CMS.FormEngine;
using CMS.GlobalHelper;
using CMS.Reporting;
using CMS.UIControls;
using CMS.Synchronization;
using CMS.ExtendedControls;
using CMS.SettingsProvider;


public partial class CMSModules_Reporting_Tools_ReportHtmlGraph_Edit : CMSReportingModalPage
{
    #region "Variables"

    protected ReportInfo reportInfo = null;
    protected ReportGraphInfo graphInfo = null;
    protected int graphId;
    bool newReport = false;

    #endregion


    #region "Properties"

    /// <summary>
    ///  Tab state (edit, preview, version)
    /// </summary>
    private int SelectedTab
    {
        get
        {
            return ValidationHelper.GetInteger(hdnSelectedTab.Value, 0);
        }
        set
        {
            hdnSelectedTab.Value = value.ToString();
        }
    }

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Test permission for query
        if (!CMSContext.CurrentUser.IsAuthorizedPerResource("CMS.Reporting", "EditSQLQueries"))
        {
            txtQueryQuery.Enabled = false;
        }
        else
        {
            txtQueryQuery.Enabled = true;
        }

        versionList.OnAfterRollback += new EventHandler(versionList_onAfterRollback);

        // Own javascript tab change handling -> because tab control raises changetab after prerender - too late
        // Own selected tab change handling
        RegisterTabScript(hdnSelectedTab.ClientID, tabControlElem);

        // Register script for resize and rolback
        RegisterResizeAndRollbackScript(divFooter.ClientID, divScrolable.ClientID);

        string[,] tabs = new string[4, 4];
        tabs[0, 0] = GetString("general.general");
        tabs[0, 1] = "SetHelpTopic('helpTopic', 'report_htmlgraph_properties')";
        tabs[1, 0] = GetString("general.preview");
        tabs[1, 1] = "SetHelpTopic('helpTopic', 'report_htmlgraph_properties');";

        tabControlElem.Tabs = tabs;
        tabControlElem.UsePostback = true;

        CurrentMaster.Title.TitleCssClass = "PageTitleHeader";
        CurrentMaster.Title.HelpTopicName = "report_htmlgraph_properties";
        CurrentMaster.Title.HelpName = "helpTopic";
        Title = "ReportGraph Edit";

        btnOk.Text = GetString("General.OK");
        btnCancel.Text = GetString("General.Cancel");
        btnApply.Text = GetString("General.apply");

        rfvCodeName.ErrorMessage = GetString("general.requirescodename");
        rfvDisplayName.ErrorMessage = GetString("general.requiresdisplayname");

        int reportId = QueryHelper.GetInteger("reportid", 0);
        bool isPreview = QueryHelper.GetBoolean("preview", false);

        // If preview by URL -> select preview tab
        if (isPreview && !RequestHelper.IsPostBack())
        {
            SelectedTab = 1;
        }

        if (reportId > 0)
        {
            reportInfo = ReportInfoProvider.GetReportInfo(reportId);
        }

        if (PersistentEditedObject == null)
        {
            if (reportInfo != null) //must be valid reportid parameter
            {
                string graphName = QueryHelper.GetString("itemname", "");

                // try to load graphname from hidden field (adding new graph & preview)
                if (graphName == String.Empty)
                {
                    graphName = txtNewGraphHidden.Value;
                }

                if (ValidationHelper.IsIdentifier(graphName))
                {
                    PersistentEditedObject = ReportGraphInfoProvider.GetReportGraphInfo(reportInfo.ReportName + "." + graphName);
                    graphInfo = PersistentEditedObject as ReportGraphInfo;
                }
            }
        }
        else
        {
            graphInfo = PersistentEditedObject as ReportGraphInfo;
        }


        if (graphInfo != null)
        {
            CurrentMaster.Title.TitleText = GetString("Reporting_ReportGraph_EditHTML.TitleText");
            CurrentMaster.Title.TitleImage = GetImageUrl("Objects/Reporting_ReportGraph/object.png");

            graphId = graphInfo.GraphID;

            if (ObjectVersionManager.DisplayVersionsTab(graphInfo))
            {
                tabs[2, 0] = GetString("objectversioning.tabtitle");
                tabs[2, 1] = "SetHelpTopic('helpTopic', 'objectversioning_general');";
                versionList.Object = graphInfo;
                versionList.IsLiveSite = false;
            }
        }
        else
        {
            CurrentMaster.Title.TitleText = GetString("Reporting_ReportGraph_EditHTML.NewItemTitleText");
            CurrentMaster.Title.TitleImage = GetImageUrl("Objects/Reporting_ReportGraph/new.png");

            newReport = true;
        }

        if (!RequestHelper.IsPostBack())
        {
            // Load default data for new report
            if (newReport)
            {
                txtQueryNoRecordText.Text = GetString("attachmentswebpart.nodatafound");
                txtItemValueFormat.Text = "{%yval%}";
                txtSeriesItemTooltip.Text = "{%ser%}";
                chkExportEnable.Checked = true;
            }
            // Otherwise load saved data
            else
            {
                LoadData();
            }
        }
    }



    /// <summary>
    /// Loads data from graph storage
    /// </summary>
    private void LoadData()
    {
        if (graphInfo != null)
        {
            txtDefaultName.Text = graphInfo.GraphDisplayName;
            txtDefaultCodeName.Text = graphInfo.GraphName;
            txtQueryQuery.Text = graphInfo.GraphQuery;
            chkIsStoredProcedure.Checked = graphInfo.GraphQueryIsStoredProcedure;

            ReportCustomData settings = graphInfo.GraphSettings;
            txtQueryNoRecordText.Text = settings["QueryNoRecordText"];
            txtGraphTitle.Text = graphInfo.GraphTitle;
            txtLegendTitle.Text = settings["LegendTitle"];
            txtItemValueFormat.Text = settings["ItemValueFormat"];
            chkDisplayLegend.Checked = ValidationHelper.GetBoolean(settings["DisplayLegend"], false);
            txtSeriesItemTooltip.Text = settings["SeriesItemToolTip"];
            txtSeriesItemLink.Text = settings["SeriesItemLink"];
            txtItemNameFormat.Text = settings["SeriesItemNameFormat"];
            chkExportEnable.Checked = ValidationHelper.GetBoolean(settings["ExportEnabled"], false);
        }
    }


    /// <summary>
    /// PreRender event handler
    /// </summary>
    protected override void OnPreRender(EventArgs e)
    {
        ShowActionButtons();
        switch (SelectedTab)
        {
            // Edit
            case 0:
                pnlPreview.Visible = false;
                pnlVersions.Visible = false;
                FormPanelHolder.Visible = true;
                categoryList.Visible = true;
                break;

            // Preview
            case 1:
                if (Save(false))
                {
                    ShowPreview();
                }
                else
                {
                    SelectedTab = 0;
                }
                break;

            // Version
            case 2:
                FormPanelHolder.Visible = false;
                categoryList.Visible = false;
                pnlPreview.Visible = false;
                if (graphInfo != null)
                {
                    pnlVersions.Visible = true;
                    ScriptHelper.RegisterStartupScript(this, typeof(string), "SetTabHelpTopic", ScriptHelper.GetScript("SetHelpTopic('helpTopic', 'objectversioning_general');"));
                }
                HideActionButtons();
                break;
        }

        tabControlElem.SelectedTab = SelectedTab;

        // Register the dialog script
        ScriptHelper.RegisterDialogScript(this);
        AddNoCacheTag();
        RegisterModalPageScripts();

        base.OnPreRender(e);
    }


    /// <summary>
    /// Save the changes to DB
    /// </summary>
    private bool Save(bool save)
    {
        string errorMessage = String.Empty;
        if (save)
        {
            if (!CMSContext.CurrentUser.IsAuthorizedPerResource("cms.reporting", "Modify"))
            {
                RedirectToAccessDenied("cms.reporting", "Modify");
            }

            errorMessage = new Validator()
                .NotEmpty(txtDefaultName.Text, rfvDisplayName.ErrorMessage)
                .NotEmpty(txtDefaultCodeName.Text, rfvCodeName.ErrorMessage)
                .NotEmpty(txtQueryQuery.Text, GetString("Reporting_ReportGraph_Edit.ErrorQuery")).Result;

            if ((errorMessage == "") && (!ValidationHelper.IsIdentifier(txtDefaultCodeName.Text.Trim())))
            {
                errorMessage = GetString("general.erroridentificatorformat");
            }

            string fullName = reportInfo.ReportName + "." + txtDefaultCodeName.Text.Trim();
            ReportGraphInfo codeNameCheck = ReportGraphInfoProvider.GetReportGraphInfo(fullName);
            if ((errorMessage == "") && (codeNameCheck != null) && (codeNameCheck.GraphID != graphId))
            {
                errorMessage = GetString("Reporting_ReportGraph_Edit.ErrorCodeNameExist");
            }
        }

        // Test query in all cases
        if (errorMessage == String.Empty)
        {
            errorMessage = new Validator().NotEmpty(txtQueryQuery.Text, GetString("Reporting_ReportGraph_Edit.ErrorQuery")).Result;
        }

        if (errorMessage == String.Empty)
        {
            // New graph
            if (graphInfo == null)
            {
                graphInfo = new ReportGraphInfo();
            }

            graphInfo.GraphDisplayName = txtDefaultName.Text.Trim();
            graphInfo.GraphName = txtDefaultCodeName.Text.Trim();

            if (CMSContext.CurrentUser.IsAuthorizedPerResource("CMS.Reporting", "EditSQLQueries"))
            {
                graphInfo.GraphQuery = txtQueryQuery.Text;
            }

            graphInfo.GraphQueryIsStoredProcedure = chkIsStoredProcedure.Checked;
            graphInfo.GraphReportID = reportInfo.ReportID;
            graphInfo.GraphTitle = txtGraphTitle.Text;
            graphInfo.GraphIsHtml = true;
            graphInfo.GraphType = String.Empty;

            ReportCustomData settings = (ReportCustomData)graphInfo.GraphSettings;
            settings["QueryNoRecordText"] = txtQueryNoRecordText.Text;
            settings["LegendTitle"] = txtLegendTitle.Text;
            settings["DisplayLegend"] = chkDisplayLegend.Checked.ToString();
            settings["SeriesItemToolTip"] = txtSeriesItemTooltip.Text;
            settings["SeriesItemLink"] = txtSeriesItemLink.Text;
            settings["ItemValueFormat"] = txtItemValueFormat.Text;
            settings["SeriesItemNameFormat"] = txtItemNameFormat.Text;
            settings["ExportEnabled"] = chkExportEnable.Checked.ToString();

            if (save)
            {
                ReportGraphInfoProvider.SetReportGraphInfo(graphInfo);
            }

            return true;
        }
        else
        {
            lblError.Visible = true;
            lblError.Text = errorMessage;
            return false;
        }
    }


    /// <summary>
    /// Save data and close editor
    /// </summary>
    /// <param name="sender">Button save object</param>
    /// <param name="e">Event arguments</param>
    protected void btnOK_Click(object sender, EventArgs e)
    {
        if (Save(true))
        {
            ltlScript.Text += ScriptHelper.GetScript("window.RefreshContent();window.close();");
        }
        else
        {
            tabControlElem.SelectedTab = 0;
        }
    }


    /// <summary>
    /// Apply changes
    /// </summary>
    /// <param name="sender">Button apply object</param>
    /// <param name="e">Event arguments</param>
    protected void btnApply_Click(object sender, EventArgs e)
    {
        if (!Save(true))
        {
            SelectedTab = 0;
        }
        else
        {
            // Redirect for new reports
            if (newReport)
            {
                Response.Redirect(ResolveUrl("ReportHtmlGraph_Edit.aspx?reportId=" + graphInfo.GraphReportID + "&itemName=" + graphInfo.GraphName));
            }
        }
    }



    /// <summary>
    /// Tab changes
    /// </summary>
    /// <param name="sender">Tab control object</param>
    /// <param name="e">Event arguments</param>
    protected void tabControlElem_clicked(object sender, EventArgs e)
    {
        // Set tab's selectedtab not by Request param, but from property
        tabControlElem.SelectedTab = SelectedTab;
    }


    /// <summary>
    /// Show graph in preview mode
    /// </summary>
    private void ShowPreview()
    {
        FormPanelHolder.Visible = false;
        categoryList.Visible = false;
        pnlVersions.Visible = false;

        if (reportInfo != null)
        {
            pnlPreview.Visible = true;

            FormInfo fi = new FormInfo(reportInfo.ReportParameters);
            // Get datarow with required columns
            DataRow dr = fi.GetDataRow();

            fi.LoadDefaultValues(dr, true);

            ctrlReportGraph.ReportParameters = dr;
            ctrlReportGraph.Visible = true;

            // Prepare fully qualified graph name = with reportname
            string fullReportGraphName = reportInfo.ReportName + "." + graphInfo.GraphName;
            ctrlReportGraph.ReportGraphInfo = graphInfo;
            ctrlReportGraph.Parameter = fullReportGraphName;

            ctrlReportGraph.ReloadData(true);
        }
    }


    /// <summary>
    /// Hide buttons that are connected with reporting object
    /// </summary>
    private void HideActionButtons()
    {
        btnApply.Visible = false;
        btnOk.Visible = false;
        btnCancel.Text = GetString("general.close");
    }


    /// <summary>
    /// Show buttons that are connected with reporting object
    /// </summary>
    private void ShowActionButtons()
    {
        btnApply.Visible = true;
        btnOk.Visible = true;
        btnCancel.Text = GetString("general.cancel");
    }


    /// <summary>
    /// Get info from PersistentEditedObject and reload data
    /// </summary>
    private void ReloadDataAfrterRollback()
    {
        // Load rollbacked info
        GeneralizedInfo gi = PersistentEditedObject as GeneralizedInfo;
        if (gi != null)
        {
            graphInfo = gi.MainObject as ReportGraphInfo;
        }
        LoadData();
    }


    protected void versionList_onAfterRollback(object sender, EventArgs e)
    {
        ReloadDataAfrterRollback();
    }

    #endregion
}

