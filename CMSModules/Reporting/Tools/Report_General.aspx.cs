using System;

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.Reporting;
using CMS.SiteProvider;
using CMS.UIControls;

public partial class CMSModules_Reporting_Tools_Report_General : CMSReportingPage
{
    #region "Variables"

    protected int reportId = 0;


    private ReportInfo ri = null;


    protected string mSave = null;

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        Title = "Report General";

        ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "ReloadPage", ScriptHelper.GetScript("function ReloadPage() { \n" + Page.ClientScript.GetPostBackEventReference(btnHdnReload, null) + "}"));

        reportId = ValidationHelper.GetInteger(Request.QueryString["reportId"], 0);

        // control initializations				
        rfvReportDisplayName.ErrorMessage = GetString("Report_New.EmptyDisplayName");
        rfvReportName.ErrorMessage = GetString("Report_New.EmptyCodeName");

        lblReportDisplayName.Text = GetString("Report_New.DisplayNameLabel");
        lblReportName.Text = GetString("Report_New.NameLabel");
        lblReportCategory.Text = GetString("Report_General.CategoryLabel");
        lblLayout.Text = GetString("Report_General.LayoutLabel");
        lblGraphs.Text = GetString("Report_General.GraphsLabel") + ":";
        lblHtmlGraphs.Text = GetString("Report_General.HtmlGraphsLabel") + ":";
        lblTables.Text = GetString("Report_General.TablesLabel") + ":";
        lblValues.Text = GetString("Report_General.TablesValues") + ":";
        lblReportAccess.Text = GetString("Report_General.ReportAccessLabel");

        ScriptHelper.RegisterSaveShortcut(lnkSave, null, false);

        imgSave.ImageUrl = GetImageUrl("CMSModules/CMS_Content/EditMenu/save.png");
        mSave = GetString("general.save");

        AttachmentTitle.TitleText = GetString("general.attachments");

        attachmentList.AllowPasteAttachments = true;
        attachmentList.ObjectID = reportId;
        attachmentList.ObjectType = ReportingObjectType.REPORT;
        attachmentList.Category = MetaFileInfoProvider.OBJECT_CATEGORY_LAYOUT;

        // Get report info
        ri = ReportInfoProvider.GetReportInfo(reportId);

        if (!RequestHelper.IsPostBack())
        {
            LoadData();
        }

        htmlTemplateBody.AutoDetectLanguage = false;
        htmlTemplateBody.DefaultLanguage = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
        htmlTemplateBody.EditorAreaCSS = "";

        ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "ReportingHTML", ScriptHelper.GetScript(" var reporting_htmlTemplateBody = '" + htmlTemplateBody.ClientID + "'"));

        // initialize item list controls
        ilGraphs.Report = ri;
        ilTables.Report = ri;
        ilValues.Report = ri;
        ilHtmlGraphs.Report = ri;

        ilGraphs.EditUrl = "ReportGraph_Edit.aspx";
        ilTables.EditUrl = "ReportTable_Edit.aspx";
        ilValues.EditUrl = "ReportValue_Edit.aspx";
        ilHtmlGraphs.EditUrl = "ReportHtmlGraph_Edit.aspx";

        ilGraphs.ItemType = ReportItemType.Graph;
        ilTables.ItemType = ReportItemType.Table;
        ilValues.ItemType = ReportItemType.Value;
        ilHtmlGraphs.ItemType = ReportItemType.HtmlGraph;
    }


    /// <summary>
    /// Load data.
    /// </summary>
    protected void LoadData()
    {
        if (ri == null)
        {
            return;
        }
        chkReportAccess.Checked = (ri.ReportAccess == ReportAccessEnum.All);
        txtReportDisplayName.Text = ri.ReportDisplayName;
        txtReportName.Text = ri.ReportName;
        htmlTemplateBody.ResolvedValue = ri.ReportLayout;

        selectCategory.Value = ri.ReportCategoryID;
    }


    /// <summary>
    /// Sets data to database.
    /// </summary>
    protected void lnkSave_Click(object sender, EventArgs e)
    {
        // Check 'Modify' permission
        if (!CMSContext.CurrentUser.IsAuthorizedPerResource("cms.reporting", "Modify"))
        {
            RedirectToAccessDenied("cms.reporting", "Modify");
        }
        string errorMessage = new Validator().NotEmpty(txtReportDisplayName.Text.Trim(), rfvReportDisplayName.ErrorMessage).NotEmpty(txtReportName.Text.Trim(), rfvReportName.ErrorMessage).Result;

        if ((errorMessage == "") && (!ValidationHelper.IsCodeName(txtReportName.Text.Trim())))
        {
            errorMessage = GetString("general.invalidcodename");
        }

        ReportAccessEnum reportAccess = ReportAccessEnum.All;

        if (!chkReportAccess.Checked)
        {
            reportAccess = ReportAccessEnum.Authenticated;
        }

        if (errorMessage == "")
        {
            ReportInfo reportInfo = ReportInfoProvider.GetReportInfo(reportId);
            ReportInfo nri = ReportInfoProvider.GetReportInfo(txtReportName.Text.Trim());

            // If report with given name already exists show error message
            if ((nri != null) && (nri.ReportID != reportInfo.ReportID))
            {
                lblError.Visible = true;
                lblError.Text = GetString("Report_New.ReportAlreadyExists");
                return;
            }

            if (reportInfo != null)
            {
                reportInfo.ReportLayout = htmlTemplateBody.ResolvedValue;

                // If there was a change in report code name change codenames in layout
                if (reportInfo.ReportName != txtReportName.Text.Trim())
                {
                    // part of old macro
                    string oldValue = "?" + reportInfo.ReportName + ".";
                    string newValue = "?" + txtReportName.Text.Trim() + ".";

                    reportInfo.ReportLayout = reportInfo.ReportLayout.Replace(oldValue, newValue);

                    // Set updated text back to HTML editor
                    htmlTemplateBody.ResolvedValue = reportInfo.ReportLayout;
                }
                int categoryID = ValidationHelper.GetInteger(selectCategory.Value, reportInfo.ReportCategoryID);

                // If there was a change in display name refresh category tree 
                if ((reportInfo.ReportDisplayName != txtReportDisplayName.Text.Trim()) || (reportInfo.ReportCategoryID != categoryID))
                {
                    ltlScript.Text += ScriptHelper.GetScript(@"if ((parent != null) && (parent.Refresh != null)) {parent.Refresh();}");
                }

                reportInfo.ReportDisplayName = txtReportDisplayName.Text.Trim();
                reportInfo.ReportName = txtReportName.Text.Trim();
                reportInfo.ReportAccess = reportAccess;
                reportInfo.ReportCategoryID = categoryID;

                ReportInfoProvider.SetReportInfo(reportInfo);

                lblInfo.Visible = true;
                lblInfo.Text = GetString("General.ChangesSaved");

                // Reload header if changes were saved
                ScriptHelper.RefreshTabHeader(Page, GetString("general.general"));
            }
        }
        else
        {
            lblError.Visible = true;
            lblError.Text = errorMessage;
        }
    }

    #endregion
}