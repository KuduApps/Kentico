using System;
using System.Data;
using System.Web.UI;

using CMS.CMSHelper;
using CMS.FormEngine;
using CMS.GlobalHelper;
using CMS.Reporting;
using CMS.UIControls;
using CMS.Synchronization;
using CMS.ExtendedControls;
using CMS.SettingsProvider;

public partial class CMSModules_Reporting_Tools_ReportValue_Edit : CMSReportingModalPage
{
    protected ReportValueInfo valueInfo = null;
    protected int valueId;
    protected ReportInfo reportInfo = null;
    bool newValue = false;

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

        string[,] tabs = new string[4, 4];
        tabs[0, 0] = GetString("general.general");
        tabs[0, 1] = "SetHelpTopic('helpTopic', 'report_value_properties')";
        tabs[1, 0] = GetString("general.preview");
        tabs[0, 1] = "SetHelpTopic('helpTopic', 'report_value_properties')";

        tabControlElem.Tabs = tabs;
        tabControlElem.UsePostback = true;
        CurrentMaster.Title.HelpName = "helpTopic";
        CurrentMaster.Title.HelpTopicName = "report_value_properties";
        CurrentMaster.Title.TitleCssClass = "PageTitleHeader";

        RegisterResizeAndRollbackScript(divFooter.ClientID, divScrolable.ClientID);

        rfvCodeName.ErrorMessage = GetString("general.requirescodename");
        rfvDisplayName.ErrorMessage = GetString("general.requiresdisplayname");

        int reportId = QueryHelper.GetInteger("reportid", 0);
        bool preview = QueryHelper.GetBoolean("preview", false);
        if (reportId > 0)
        {
            reportInfo = ReportInfoProvider.GetReportInfo(reportId);
        }

        if (reportInfo != null) //must be valid reportid parameter
        {
            if (PersistentEditedObject == null)
            {

                string valueName = ValidationHelper.GetString(Request.QueryString["itemname"], "");
                if (ValidationHelper.IsIdentifier(valueName))
                {
                    PersistentEditedObject = ReportValueInfoProvider.GetReportValueInfo(reportInfo.ReportName + "." + valueName);
                    valueInfo = PersistentEditedObject as ReportValueInfo;
                }
            }
            else
            {
                valueInfo = PersistentEditedObject as ReportValueInfo;
            }

            if (valueInfo != null)
            {
                CurrentMaster.Title.TitleText = GetString("Reporting_ReportValue_Edit.TitleText");
                CurrentMaster.Title.TitleImage = GetImageUrl("Objects/Reporting_ReportValue/object.png");

                valueId = valueInfo.ValueID;

                if (ObjectVersionManager.DisplayVersionsTab(valueInfo))
                {
                    tabs[2, 0] = GetString("objectversioning.tabtitle");
                    tabs[2, 1] = "SetHelpTopic('helpTopic', 'objectversioning_general');";
                    versionList.Object = valueInfo;
                    versionList.IsLiveSite = false;
                }
            }
            else //new item
            {
                CurrentMaster.Title.TitleText = GetString("Reporting_ReportValue_Edit.NewItemTitleText");
                CurrentMaster.Title.TitleImage = GetImageUrl("Objects/Reporting_ReportValue/new.png");

                newValue = true;
            }

            // set help key
            CurrentMaster.Title.HelpTopicName = "report_value_properties";

            if (!RequestHelper.IsPostBack())
            {
                LoadData();
            }
        }
        else
        {
            btnOk.Visible = false;
            lblError.Visible = true;
            lblError.Text = GetString("Reporting_ReportValue_Edit.InvalidReportId");
        }
        btnOk.Text = GetString("General.OK");
        btnCancel.Text = GetString("General.Cancel");
        btnApply.Text = GetString("General.Apply");

        if (preview)
        {
            tabControlElem.SelectedTab = 1;
            ShowPreview();
        }
    }


    /// <summary>
    /// PreRender event handler.
    /// </summary>
    protected override void OnPreRender(EventArgs e)
    {
        // Set correct help topic
        Control pbCtrl = ControlsHelper.GetPostBackControl(this);
        if ((tabControlElem.SelectedTab == 2) && ((pbCtrl != null) && !pbCtrl.ClientID.StartsWith(tabControlElem.ClientID)))
        {
            ScriptHelper.RegisterStartupScript(this, typeof(string), "SetTabHelpTopic", ScriptHelper.GetScript("SetHelpTopic('helpTopic', 'objectversioning_general');"));
            HideActionButtons();
        }
        else
        {
            ShowActionButtons();
        }

        base.OnPreRender(e);
    }



    /// <summary>
    /// Load data from db.
    /// </summary>
    protected void LoadData()
    {
        if (valueInfo != null)
        {
            txtDisplayName.Text = valueInfo.ValueDisplayName;
            txtCodeName.Text = valueInfo.ValueName;
            txtQuery.Text = valueInfo.ValueQuery;
            chkIsProcedure.Checked = valueInfo.ValueQueryIsStoredProcedure;
            txtFormatString.Text = valueInfo.ValueFormatString;
        }
    }


    /// <summary>
    /// Saves the data and close the editor.
    /// </summary>
    /// <param name="sender">Button save</param>
    /// <param name="e">Params</param>
    protected void btnOK_Click(object sender, EventArgs e)
    {
        if (Save(true))
        {
            ltlScript.Text += ScriptHelper.GetScript("RefreshContent();window.close();");
        }
        else
        {
            tabControlElem.SelectedTab = 0;
            pnlPreview.Visible = false;
            divPanelHolder.Visible = true;
            categoryList.Visible = true;
        }
    }


    /// <summary>
    /// Apply data changes.
    /// </summary>
    /// <param name="sender">Button apply</param>
    /// <param name="e">Params</param>
    protected void btnApply_Click(object sender, EventArgs e)
    {
        if (!Save(true))
        {
            tabControlElem.SelectedTab = 0;
            pnlPreview.Visible = false;
            divPanelHolder.Visible = true;
            categoryList.Visible = true;
        }
        else
            // Redirect for new reports
            if (newValue)
            {
                Response.Redirect(ResolveUrl("ReportValue_Edit.aspx?reportId=" + valueInfo.ValueReportID + "&itemName=" + valueInfo.ValueName));
            }
    }


    /// <summary>
    /// Save data.
    /// </summary>
    /// <returns>return true if save was successfull</returns>
    protected bool Save(bool save)
    {
        string errorMessage = String.Empty;
        if (save)
        {
            // Check 'Modify' permission
            if (!CMSContext.CurrentUser.IsAuthorizedPerResource("cms.reporting", "Modify"))
            {
                RedirectToAccessDenied("cms.reporting", "Modify");
            }

            errorMessage = new Validator()
                .NotEmpty(txtDisplayName.Text, rfvDisplayName.ErrorMessage)
                .NotEmpty(txtCodeName.Text, rfvCodeName.ErrorMessage)
                .NotEmpty(txtQuery.Text, GetString("Reporting_ReportGraph_Edit.ErrorQuery")).Result;

            if ((errorMessage == "") && (!ValidationHelper.IsIdentifier(txtCodeName.Text.Trim())))
            {
                errorMessage = GetString("general.erroridentificatorformat");
            }

            string fullName = reportInfo.ReportName + "." + txtCodeName.Text.Trim();
            ReportValueInfo codeNameCheck = ReportValueInfoProvider.GetReportValueInfo(fullName);

            if ((errorMessage == "") && (codeNameCheck != null) && (codeNameCheck.ValueID != valueId))
            {
                errorMessage = GetString("Reporting_ReportValue_Edit.ErrorCodeNameExist");
            }
        }

        //test query in all cases
        if (!save)
        {
            errorMessage = new Validator().NotEmpty(txtQuery.Text, GetString("Reporting_ReportGraph_Edit.ErrorQuery")).Result;
        }

        if ((errorMessage == ""))
        {
            //new Value
            if (valueInfo == null)
            {
                valueInfo = new ReportValueInfo();
            }

            valueInfo.ValueDisplayName = txtDisplayName.Text.Trim();
            valueInfo.ValueName = txtCodeName.Text.Trim();

            if (CMSContext.CurrentUser.IsAuthorizedPerResource("CMS.Reporting", "EditSQLQueries"))
            {
                valueInfo.ValueQuery = txtQuery.Text.Trim();
            }

            valueInfo.ValueQueryIsStoredProcedure = chkIsProcedure.Checked;
            valueInfo.ValueFormatString = txtFormatString.Text.Trim();
            valueInfo.ValueReportID = reportInfo.ReportID;

            if (save)
            {
                ReportValueInfoProvider.SetReportValueInfo(valueInfo);
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
    /// Show preview.
    /// </summary>
    private void ShowPreview()
    {
        if (reportInfo != null)
        {
            pnlPreview.Visible = true;
            divPanelHolder.Visible = false;
            categoryList.Visible = false;
            pnlVersions.Visible = false;

            FormInfo fi = new FormInfo(reportInfo.ReportParameters);
            // Get datarow with required columns
            DataRow dr = fi.GetDataRow();

            fi.LoadDefaultValues(dr, true);

            //reportGraph.ContextParameters 
            ctrlReportValue.ReportParameters = dr;

            ctrlReportValue.Visible = true;
            ctrlReportValue.ValueInfo = valueInfo;

            ctrlReportValue.ReloadData(true);
        }
    }


    /// <summary>
    /// Hide buttons that are connected with reporting object.
    /// </summary>
    private void HideActionButtons()
    {
        btnApply.Visible = false;
        btnOk.Visible = false;
        btnCancel.Text = GetString("general.close");
    }


    /// <summary>
    /// Show buttons that are connected with reporting object.
    /// </summary>
    private void ShowActionButtons()
    {
        btnApply.Visible = true;
        btnOk.Visible = true;
        btnCancel.Text = GetString("general.cancel");
    }


    /// <summary>
    /// Tab change.
    /// </summary>
    /// <param name="sender">Tab control</param>
    /// <param name="ea">Event params</param>
    protected void tabControlElem_clicked(object sender, EventArgs ea)
    {
        ShowActionButtons();
        if (tabControlElem.SelectedTab == 2)
        {
            divPanelHolder.Visible = false;
            categoryList.Visible = false;
            pnlPreview.Visible = false;
            if (valueInfo != null)
            {
                pnlVersions.Visible = true;
                ScriptHelper.RegisterStartupScript(this, typeof(string), "SetTabHelpTopic", ScriptHelper.GetScript("SetHelpTopic('helpTopic', 'objectversioning_general');"));
            }
            HideActionButtons();
        }
        else if (tabControlElem.SelectedTab == 1)
        {
            //createGraphImage
            if (Save(false))
            {
                ShowPreview();
            }
            else
            {
                tabControlElem.SelectedTab = 0;
            }
        }
        else
        {
            pnlPreview.Visible = false;
            pnlVersions.Visible = false;
            divPanelHolder.Visible = true;
            categoryList.Visible = true;
        }
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
            valueInfo = gi.MainObject as ReportValueInfo;
        }
        LoadData();
    }


    protected void versionList_onAfterRollback(object sender, EventArgs e)
    {
        ReloadDataAfrterRollback();
    }
}
