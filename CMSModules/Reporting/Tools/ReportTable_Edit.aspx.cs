using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.CMSHelper;
using CMS.FormEngine;
using CMS.GlobalHelper;
using CMS.Reporting;
using CMS.UIControls;
using CMS.Synchronization;
using CMS.ExtendedControls;
using CMS.SettingsProvider;

public partial class CMSModules_Reporting_Tools_ReportTable_Edit : CMSReportingModalPage
{
    #region "Variables"

    protected ReportTableInfo tableInfo = null;
    protected int tableId;
    protected ReportInfo reportInfo = null;
    bool newTable = false;

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


    protected void Page_Load(object sender, EventArgs e)
    {
        // Test permission for query
        if (!CMSContext.CurrentUser.IsAuthorizedPerResource("CMS.Reporting", "EditSQLQueries"))
        {
            txtQuery.Enabled = false;
        }
        else
        {
            txtQuery.Enabled = true;
        }

        versionList.OnAfterRollback += new EventHandler(versionList_onAfterRollback);

        // Own javascript tab change handling -> because tab control raises changetab after prerender - too late
        // Own selected tab change handling
        RegisterTabScript(hdnSelectedTab.ClientID, tabControlElem);

        // Register common resize and refresh scripts
        RegisterResizeAndRollbackScript(divFooter.ClientID, divScrolable.ClientID);

        string[,] tabs = new string[4, 4];
        tabs[0, 0] = GetString("general.general");
        tabs[0, 1] = "SetHelpTopic('helpTopic', 'report_table_properties')";
        tabs[1, 0] = GetString("general.preview");
        tabs[0, 1] = "SetHelpTopic('helpTopic', 'report_table_properties')";

        tabControlElem.Tabs = tabs;
        tabControlElem.UsePostback = true;
        CurrentMaster.Title.HelpName = "helpTopic";
        CurrentMaster.Title.HelpTopicName = "report_table_properties";
        CurrentMaster.Title.TitleCssClass = "PageTitleHeader";

        rfvCodeName.ErrorMessage = GetString("general.requirescodename");
        rfvDisplayName.ErrorMessage = GetString("general.requiresdisplayname");
        btnApply.Text = GetString("General.apply");
        btnOk.Text = GetString("General.OK");
        btnCancel.Text = GetString("General.Cancel");

        int reportId = QueryHelper.GetInteger("reportid", 0);
        bool preview = QueryHelper.GetBoolean("preview", false);
        if (reportId > 0)
        {
            reportInfo = ReportInfoProvider.GetReportInfo(reportId);
        }

        // If preview by URL -> select preview tab
        bool isPreview = QueryHelper.GetBoolean("preview", false);
        if (isPreview && !RequestHelper.IsPostBack())
        {
            SelectedTab = 1;
        }

        if (PersistentEditedObject == null)
        {
            if (reportInfo != null) // Must be valid reportid parameter
            {
                string tableName = ValidationHelper.GetString(Request.QueryString["itemname"], "");

                // Try to load tableName from hidden field (adding new graph & preview)
                if (tableName == String.Empty)
                {
                    tableName = txtNewTableHidden.Value;
                }

                if (ValidationHelper.IsIdentifier(tableName))
                {
                    PersistentEditedObject = ReportTableInfoProvider.GetReportTableInfo(reportInfo.ReportName + "." + tableName);
                    tableInfo = PersistentEditedObject as ReportTableInfo;
                }
            }
        }
        else
        {
            tableInfo = PersistentEditedObject as ReportTableInfo;
        }

        if (reportInfo != null)
        {
            // Control text initializations
            if (tableInfo != null)
            {
                CurrentMaster.Title.TitleText = GetString("Reporting_ReportTable_Edit.TitleText");
                CurrentMaster.Title.TitleImage = GetImageUrl("Objects/Reporting_ReportTable/object.png");

                tableId = tableInfo.TableID;

                if (ObjectVersionManager.DisplayVersionsTab(tableInfo))
                {
                    tabs[2, 0] = GetString("objectversioning.tabtitle");
                    tabs[2, 1] = "SetHelpTopic('helpTopic', 'objectversioning_general');";

                    versionList.Object = tableInfo;
                    versionList.IsLiveSite = false;
                }
            }
            else // New item
            {
                CurrentMaster.Title.TitleText = GetString("Reporting_ReportTable_Edit.NewItemTitleText");
                CurrentMaster.Title.TitleImage = GetImageUrl("Objects/Reporting_ReportTable/new.png");

                if (!RequestHelper.IsPostBack())
                {
                    txtPageSize.Text = "15";
                    txtQueryNoRecordText.Text = GetString("attachmentswebpart.nodatafound");
                    chkExportEnable.Checked = true;
                }

                newTable = true;
            }

            // Set help key
            CurrentMaster.Title.HelpTopicName = "report_table_properties";

            if (!RequestHelper.IsPostBack())
            {
                DataHelper.FillListControlWithEnum(typeof(PagerButtons), drpPageMode, "PagerButtons.", null);
                // Preselect page numbers paging
                drpPageMode.SelectedValue = ((int)PagerButtons.Numeric).ToString();

                LoadData();
            }
        }

        if ((preview) && (!RequestHelper.IsPostBack()))
        {
            tabControlElem.SelectedTab = 1;
            ShowPreview();
        }

        // In case of preview paging without saving table
        if (RequestHelper.IsPostBack() && tabControlElem.SelectedTab == 1)
        {
            // Reload deafult parameters
            FormInfo fi = new FormInfo(reportInfo.ReportParameters);
            // Get datarow with required columns
            ctrlReportTable.ReportParameters = fi.GetDataRow();
            fi.LoadDefaultValues(ctrlReportTable.ReportParameters, true);

            // Colect data and put them in talbe info
            Save(false);
            ctrlReportTable.TableInfo = tableInfo;
        }
    }


    /// <summary>
    /// Reload data for table in prerender because of paging
    /// </summary>
    /// <param name="e">Event arguments</param>
    protected override void OnPreRender(EventArgs e)
    {
        ShowActionButtons();
        switch (SelectedTab)
        {
            // Versions
            case 2:
                divPanelHolder.Visible = false;
                categoryList.Visible = false;
                pnlPreview.Visible = false;
                if (tableInfo != null)
                {
                    pnlVersions.Visible = true;
                    ScriptHelper.RegisterStartupScript(this, typeof(string), "SetTabHelpTopic", ScriptHelper.GetScript("SetHelpTopic('helpTopic', 'objectversioning_general');"));
                }
                HideActionButtons();
                break;

            // Preview
            case 1:
                // Create table preview
                if (Save(false))
                {
                    ShowPreview();
                }
                else
                {
                    tabControlElem.SelectedTab = 0;
                }
                break;

            // Edit
            case 0:
                pnlPreview.Visible = false;
                pnlVersions.Visible = false;
                divPanelHolder.Visible = true;
                categoryList.Visible = true;
                break;
        }

        tabControlElem.SelectedTab = SelectedTab;
        base.OnPreRender(e);
    }


    /// <summary>
    /// Loads data from tableInfo
    /// </summary>
    protected void LoadData()
    {
        if (tableInfo != null)
        {
            txtDisplayName.Text = tableInfo.TableDisplayName;
            txtCodeName.Text = tableInfo.TableName;
            txtQuery.Text = tableInfo.TableQuery;
            chkIsProcedure.Checked = tableInfo.TableQueryIsStoredProcedure;
            txtSkinID.Text = ValidationHelper.GetString(tableInfo.TableSettings["skinid"], "");
            txtPageSize.Text = ValidationHelper.GetInteger(tableInfo.TableSettings["pagesize"], 10).ToString();
            chkEnablePaging.Checked = ValidationHelper.GetBoolean(tableInfo.TableSettings["enablepaging"], false);
            drpPageMode.SelectedValue = ValidationHelper.GetString(tableInfo.TableSettings["pagemode"], "");
            txtQueryNoRecordText.Text = ValidationHelper.GetString(tableInfo.TableSettings["QueryNoRecordText"], String.Empty);
            chkExportEnable.Checked = ValidationHelper.GetBoolean(tableInfo.TableSettings["ExportEnabled"], false);
        }
    }


    /// <summary>
    /// Saves data
    /// </summary>
    /// <returns></returns>
    protected bool Save(bool save)
    {
        string errorMessage = String.Empty;
        // Check 'Modify' permission
        if (!CMSContext.CurrentUser.IsAuthorizedPerResource("cms.reporting", "Modify"))
        {
            RedirectToAccessDenied("cms.reporting", "Modify");
        }

        if (save)
        {
            errorMessage = new Validator()
                .NotEmpty(txtDisplayName.Text, rfvDisplayName.ErrorMessage)
                .NotEmpty(txtCodeName.Text, rfvCodeName.ErrorMessage)
                .NotEmpty(txtQuery.Text, GetString("Reporting_ReportGraph_Edit.ErrorQuery")).Result;

            if ((errorMessage == "") && (!ValidationHelper.IsIdentifier(txtCodeName.Text.Trim())))
            {
                errorMessage = GetString("general.erroridentificatorformat");
            }

            string fullName = reportInfo.ReportName + "." + txtCodeName.Text.Trim();
            ReportTableInfo codeNameCheck = ReportTableInfoProvider.GetReportTableInfo(fullName);

            if ((errorMessage == "") && (codeNameCheck != null) && (codeNameCheck.TableID != tableId))
            {
                errorMessage = GetString("Reporting_ReportTable_Edit.ErrorCodeNameExist");
            }

        }

        // Test query in all cases
        if (errorMessage == String.Empty)
        {
            errorMessage = new Validator().NotEmpty(txtQuery.Text, GetString("Reporting_ReportGraph_Edit.ErrorQuery")).Result;
        }

        if ((errorMessage == "") && (txtPageSize.Text.Trim() != String.Empty) && (!ValidationHelper.IsInteger(txtPageSize.Text) || !ValidationHelper.IsPositiveNumber(txtPageSize.Text)))
        {
            errorMessage = GetString("Reporting_ReportTable_Edit.errorinvalidpagesize");
        }

        if ((errorMessage == ""))
        {
            // New table
            if (tableInfo == null)
            {
                tableInfo = new ReportTableInfo();
            }

            tableInfo.TableDisplayName = txtDisplayName.Text.Trim();
            tableInfo.TableName = txtCodeName.Text.Trim();

            if (CMSContext.CurrentUser.IsAuthorizedPerResource("CMS.Reporting", "EditSQLQueries"))
            {
                tableInfo.TableQuery = txtQuery.Text.Trim();
            }

            tableInfo.TableQueryIsStoredProcedure = chkIsProcedure.Checked;
            tableInfo.TableReportID = reportInfo.ReportID;

            tableInfo.TableSettings["SkinID"] = txtSkinID.Text.Trim();
            tableInfo.TableSettings["enablepaging"] = chkEnablePaging.Checked.ToString();
            tableInfo.TableSettings["pagesize"] = txtPageSize.Text;
            tableInfo.TableSettings["pagemode"] = drpPageMode.SelectedValue;
            tableInfo.TableSettings["QueryNoRecordText"] = txtQueryNoRecordText.Text;
            tableInfo.TableSettings["ExportEnabled"] = chkExportEnable.Checked.ToString();

            if (save)
            {
                ReportTableInfoProvider.SetReportTableInfo(tableInfo);
            }
        }
        else
        {
            lblError.Visible = true;
            lblError.Text = errorMessage;
            return false;
        }

        return true;
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
            SelectedTab = 0;
        }
    }


    /// <summary>
    /// Save changed data
    /// </summary>
    /// <param name="sender">Apply button object</param>
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
            if (newTable)
            {
                Response.Redirect(ResolveUrl("ReportTable_Edit.aspx?reportId=" + tableInfo.TableReportID + "&itemName=" + tableInfo.TableName));
            }
        }
    }


    /// <summary>
    /// Show preview
    /// </summary>
    private void ShowPreview()
    {
        divPanelHolder.Visible = false;
        categoryList.Visible = false;
        pnlVersions.Visible = false;

        if (reportInfo != null)
        {
            pnlPreview.Visible = true;

            FormInfo fi = new FormInfo(reportInfo.ReportParameters);
            // Get datarow with required columns
            DataRow dr = fi.GetDataRow();

            fi.LoadDefaultValues(dr, true);

            // ReportGraph.ContextParameters 
            ctrlReportTable.ReportParameters = dr;

            // Prepare fully qualified graph name = with reportname
            if (tableInfo != null)
            {
                string fullReportGraphName = reportInfo.ReportName + "." + tableInfo.TableName;
                ctrlReportTable.Parameter = fullReportGraphName;
            }
            ctrlReportTable.TableInfo = tableInfo;


            ctrlReportTable.ReloadData(true);
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
    /// Tab changed event
    /// </summary>
    /// <param name="sender">Tab control</param>
    /// <param name="ea">Event arguments</param>
    protected void tabControlElem_clicked(object sender, EventArgs ea)
    {
        tabControlElem.SelectedTab = SelectedTab;
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
            tableInfo = gi.MainObject as ReportTableInfo ;
        }
        LoadData();
    }


    protected void versionList_onAfterRollback(object sender, EventArgs e)
    {
        ReloadDataAfrterRollback();
    }
}


