using System;
using System.Web.UI.WebControls;

using CMS.UIControls;
using CMS.SiteProvider;
using CMS.GlobalHelper;
using CMS.SettingsProvider;
using CMS.CMSHelper;
using CMS.CMSImportExport;
using CMS.TreeEngine;
using CMS.ExtendedControls;

public partial class CMSModules_AbuseReport_Controls_AbuseReportStatusEdit : CMSAdminEditControl
{
    #region "Private variables"

    private AbuseReportInfo mCurrentReport = null;
    private int mReportID = 0;

    #endregion


    #region "Public properties"

    /// <summary>
    ///ID of current report.
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
    /// Gets current report.
    /// </summary>
    private AbuseReportInfo CurrentReport
    {
        get
        {
            if (mCurrentReport == null)
            {
                mCurrentReport = AbuseReportInfoProvider.GetAbuseReportInfo(ReportID);
                // Set edited object
                
                EditedObject = mCurrentReport;
                if (mCurrentReport == null)
                {                    
                    throw new Exception(string.Format("[AbuseReportStatusEdit.CurrentReport]: The abuse report with ID '{0}' doesn't exist.", ReportID));
                }
            }
            return mCurrentReport;
        }
    }

    #endregion


    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!StopProcessing)
        {
            ReloadData(true);
        }

        btnOk.Click += btnOK_Click;
        rfvText.ErrorMessage = GetString("abuse.textreqired");
    }

    #endregion


    #region "Other events"

    /// <summary>
    /// Button OK click event handler.
    /// </summary>
    protected void btnOK_Click(object sender, EventArgs e)
    {
        // Check permissions
        if (!CheckPermissions("CMS.AbuseReport", "Manage"))
        {
            return;
        }

        // Check that text area is not empty
        txtCommentValue.Text = txtCommentValue.Text.Trim();
        if (txtCommentValue.Text.Length > 1000)
        {
            txtCommentValue.Text = txtCommentValue.Text.Substring(0, 1000);
        }

        if (txtCommentValue.Text.Length > 0)
        {

            // Load new values
            CurrentReport.ReportComment = txtCommentValue.Text;
            CurrentReport.ReportStatus = (AbuseReportStatusEnum)ValidationHelper.GetInteger(drpStatus.SelectedValue, 0);

            // Save AbuseReport
            AbuseReportInfoProvider.SetAbuseReportInfo(CurrentReport);
            lblSaved.Visible = true;
        }
        else
        {
            lblError.Visible = true;
        }
    }

    #endregion


    #region "Methods"

    /// <summary>
    /// Reloads all data.
    /// </summary>
    public override void ReloadData(bool forceLoad)
    {
        if (CurrentReport != null)
        {
            // Load labels
            if (!RequestHelper.IsPostBack() || forceLoad)
            {
                // Create query parameters
                string query = "?ObjectID=" + CurrentReport.ReportObjectID;

                // Set link value
                string url = CurrentReport.ReportURL;
                if (CurrentReport.ReportCulture != String.Empty)
                {
                    url = URLHelper.AddParameterToUrl(url, URLHelper.LanguageParameterName, CurrentReport.ReportCulture);
                }
                lnkUrlValue.Text = HTMLHelper.HTMLEncode(url);
                lnkUrlValue.NavigateUrl = url;
                lnkUrlValue.ToolTip = HTMLHelper.HTMLEncode(url);
                lnkUrlValue.Target = "_blank";

                // Set culture value
                System.Globalization.CultureInfo ci = CultureHelper.GetCultureInfo(CurrentReport.ReportCulture);
                lblCultureValue.Text = (ci != null) ? ci.DisplayName : ResHelper.Dash;

                // Set site value
                SiteInfo si = SiteInfoProvider.GetSiteInfo(CurrentReport.ReportSiteID);
                lblSiteValue.Text = (si != null) ? HTMLHelper.HTMLEncode(si.DisplayName) : ResHelper.Dash;

                // Set title
                lblTitleValue.Text = HTMLHelper.HTMLEncode(CurrentReport.ReportTitle);

                // Set labels
                if (!string.IsNullOrEmpty(CurrentReport.ReportObjectType))
                {
                    lblObjectTypeValue.Text = GetString("ObjectType." + ImportExportHelper.GetSafeObjectTypeName(CurrentReport.ReportObjectType));
                    query += "&ObjectType=" + CurrentReport.ReportObjectType;
                    if ((CurrentReport.ReportObjectID > 0) && (CurrentReport.ReportObjectType.ToLower() != TreeObjectType.DOCUMENT))
                    {
                        pnlLink.Visible = true;
                    }
                }
                else
                {
                    lblObjectTypeValue.Text = ResHelper.Dash;
                }

                // Get object display name
                lblObjectNameValue.Text = ResHelper.Dash;
                if ((CurrentReport.ReportObjectID > 0) && (!string.IsNullOrEmpty(CurrentReport.ReportObjectType)))
                {
                    GeneralizedInfo info = CMSObjectHelper.GetReadOnlyObject(CurrentReport.ReportObjectType);
                    if ((info != null) && (CurrentReport.ReportObjectType.ToLower() != TreeObjectType.DOCUMENT.ToLower()))
                    {
                        GeneralizedInfo obj = info.GetObject(CurrentReport.ReportObjectID);
                        if ((obj != null) && !string.IsNullOrEmpty(obj.ObjectDisplayName))
                        {
                            lblObjectNameValue.Text = HTMLHelper.HTMLEncode(obj.ObjectDisplayName);
                        }
                    }
                }

                // Set Reported by label
                lblReportedByValue.Text = ResHelper.Dash;
                if (CurrentReport.ReportUserID != 0)
                {
                    UserInfo ui = UserInfoProvider.GetUserInfo(CurrentReport.ReportUserID);
                    lblReportedByValue.Text = (ui != null) ? HTMLHelper.HTMLEncode(ui.FullName) : GetString("general.NA");
                }

                // Set other parameters
                lblReportedWhenValue.Text = CurrentReport.ReportWhen.ToString();

                if ((CurrentReport.ReportObjectID > 0) && (!string.IsNullOrEmpty(CurrentReport.ReportObjectType)) && AbuseReportInfoProvider.IsObjectTypeSupported(CurrentReport.ReportObjectType))
                {
                    lnkShowDetails.Visible = true;
                    string detailUrl = "~/CMSModules/AbuseReport/AbuseReport_ObjectDetails.aspx" + query;
                    lnkShowDetails.NavigateUrl = URLHelper.AddParameterToUrl(detailUrl, "hash", QueryHelper.GetHash(detailUrl));
                }

                if (ControlsHelper.GetPostBackControl(Page) != btnOk)
                {
                    txtCommentValue.Text = CurrentReport.ReportComment;
                    LoadStatus((int)CurrentReport.ReportStatus);
                }
            }
        }
    }


    /// <summary>
    /// Loads status from enumeration to dropdown list.
    /// </summary>
    private void LoadStatus(int reportStatus)
    {
        drpStatus.Items.Clear();
        drpStatus.Items.Add(new ListItem(GetString("general.new"), "0"));
        drpStatus.Items.Add(new ListItem(GetString("general.solved"), "1"));
        drpStatus.Items.Add(new ListItem(GetString("general.rejected"), "2"));
        drpStatus.SelectedValue = reportStatus.ToString();
    }

    #endregion
}
