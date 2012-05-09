using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.CMSHelper;
using CMS.ExtendedControls;
using CMS.GlobalHelper;
using CMS.SiteProvider;
using CMS.UIControls;
using CMS.WebAnalytics;
using CMS.SettingsProvider;
using CMS.PortalEngine;

public partial class CMSModules_AbuseReport_Controls_AbuseReportEdit : CMSAdminControl
{
    #region "Variables"

    private string mConfirmationText = string.Empty;
    private string mReportTitle = string.Empty;
    private int mReportObjectID = 0;
    private string mReportObjectType = string.Empty;
    private string mReportURL = string.Empty;
    private LocalizedButton mReportButton = null;
    private LocalizedButton mCancelButton = null;

    #endregion


    #region "Properties"

    /// <summary>
    /// Gets and sets Confirmation text.
    /// </summary>
    public string ConfirmationText
    {
        get
        {
            if (string.IsNullOrEmpty(mConfirmationText))
            {
                return "abuse.saved";
            }
            else
            {
                return mConfirmationText;
            }
        }
        set
        {
            mConfirmationText = value;
        }
    }


    /// <summary>
    /// Gets or sets Report title.
    /// </summary>
    public string ReportTitle
    {
        get
        {
            return mReportTitle;
        }
        set
        {
            mReportTitle = value;
        }
    }


    /// <summary>
    /// Gets or sets Report Object ID.
    /// </summary>
    public int ReportObjectID
    {
        get
        {
            return mReportObjectID;
        }
        set
        {
            mReportObjectID = value;
        }
    }


    /// <summary>
    /// Gets or sets the URL of the abuse to be reported.
    /// </summary>
    public string ReportURL
    {
        get
        {
            if (string.IsNullOrEmpty(mReportURL))
            {
                mReportURL = URLHelper.GetAbsoluteUrl(URLHelper.CurrentURL);
            }
            return mReportURL;
        }
        set
        {
            mReportURL = value;
        }
    }


    /// <summary>
    /// Gets or sets Report Object type.
    /// </summary>
    public string ReportObjectType
    {
        get
        {
            return mReportObjectType;
        }
        set
        {
            mReportObjectType = value;
        }
    }


    /// <summary>
    /// Returns textbox control.
    /// </summary>
    public TextBox TextField
    {
        get
        {
            return txtText;
        }
    }


    /// <summary>
    /// Returns report button control.
    /// </summary>
    public LocalizedButton ReportButton
    {
        get
        {
            if (mReportButton == null)
            {
                mReportButton = btnReport;
            }
            return mReportButton;
        }
        set
        {
            mReportButton = value;
        }
    }


    /// <summary>
    /// Returns cancel button control.
    /// </summary>
    public LocalizedButton CancelButton
    {
        get
        {
            if (mCancelButton == null)
            {
                mCancelButton = btnCancel;
            }
            return mCancelButton;
        }
        set
        {
            mCancelButton = value;
        }
    }


    /// <summary>
    /// Gets or sets cancel button visible property.
    /// </summary>
    public bool ShowCancelButton
    {
        get
        {
            return btnCancel.Visible;
        }
        set
        {
            btnCancel.Visible = value;
        }
    }


    /// <summary>
    /// Indicates if buttons should be displayed.
    /// </summary>
    public bool DisplayButtons
    {
        get
        {
            return plcButtons.Visible;
        }
        set
        {
            plcButtons.Visible = value;
        }
    }


    /// <summary>
    /// Returns panel control.
    /// </summary>
    public Panel BodyPanel
    {
        get
        {
            return pnlBody;
        }
    }

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        rfvText.ErrorMessage = GetString("abuse.textreqired");
        rfvText.ValidationGroup = "Abuse" + ClientID;
        ReportButton.ValidationGroup = "Abuse" + ClientID;

        // WAI validation
        lblText.Attributes.Add("style", "display:none;");

        if (!RequestHelper.IsPostBack())
        {
            Reload();
        }
    }


    /// <summary>
    /// Resets all properties.
    /// </summary>
    public void Reload()
    {
        txtText.Visible = true;
        ReportButton.Visible = true;
        lblSaved.Text = String.Empty;
        lblSaved.Visible = false;
        txtText.Text = String.Empty;
    }


    /// <summary>
    /// Log activity
    /// </summary>
    /// <param name="ari">Report info</param>
    private void LogActivity(AbuseReportInfo ari)
    {
        if ((CMSContext.ViewMode != ViewModeEnum.LiveSite) ||
            (ari == null) ||
            !ActivitySettingsHelper.ActivitiesEnabledForThisUser(CMSContext.CurrentUser) ||
            !ActivitySettingsHelper.ActivitiesEnabledAndModuleLoaded(ari.ReportSiteID) ||
            !ActivitySettingsHelper.AbuseReportEnabled(ari.ReportSiteID))
        {
            return;
        }
        var data = new ActivityData()
        {
            ContactID = ModuleCommands.OnlineMarketingGetCurrentContactID(),
            SiteID = ari.ReportSiteID,
            Type = PredefinedActivityType.ABUSE_REPORT,
            TitleData = ari.ReportTitle,
            ItemID = ari.ReportID,
            URL = URLHelper.CurrentRelativePath,
            Campaign = CMSContext.Campaign
        };
        ActivityLogProvider.LogActivity(data);
    }

    #endregion


    #region "Button handling"

    /// <summary>
    /// Cancel button event handler.
    /// </summary>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Reload();
    }


    /// <summary>
    /// Report abuse event handler.
    /// </summary>
    protected void btnReport_Click(object sender, EventArgs e)
    {
        PerformAction();
    }


    /// <summary>
    /// Performes reporting of abuse.
    /// </summary>
    public void PerformAction()
    {
        // Check banned ip
        if (!BannedIPInfoProvider.IsAllowed(CMSContext.CurrentSiteName, BanControlEnum.AllNonComplete))
        {
            lblSaved.CssClass = "ErrorLabel";
            lblSaved.Text = GetString("General.BannedIP");
            return;
        }

        string report = txtText.Text;

        // Check that text area is not empty or too long
        report = report.Trim();
        report = TextHelper.LimitLength(report, 1000);

        if (report.Length > 0)
        {
            // Create new AbuseReport
            AbuseReportInfo abuseReport = new AbuseReportInfo();
            if (ReportTitle != "")
            {
                // Set AbuseReport properties
                // Decode first, from forums it can be encoded
                ReportTitle = Server.HtmlDecode(ReportTitle);
                // Remove BBCode tags
                ReportTitle = DiscussionMacroHelper.RemoveTags(ReportTitle);
                abuseReport.ReportTitle = TextHelper.LimitLength(ReportTitle, 100);
                abuseReport.ReportURL = ReportURL;
                abuseReport.ReportCulture = CMSContext.PreferredCultureCode;
                if (ReportObjectID > 0)
                {
                    abuseReport.ReportObjectID = ReportObjectID;
                }

                if (ReportObjectType != "")
                {
                    abuseReport.ReportObjectType = ReportObjectType;
                }

                abuseReport.ReportComment = report;

                if (CMSContext.CurrentUser.UserID > 0)
                {
                    abuseReport.ReportUserID = CMSContext.CurrentUser.UserID;
                }

                abuseReport.ReportWhen = DateTime.Now;
                abuseReport.ReportStatus = AbuseReportStatusEnum.New;
                abuseReport.ReportSiteID = CMSContext.CurrentSite.SiteID;

                // Save AbuseReport
                AbuseReportInfoProvider.SetAbuseReportInfo(abuseReport);

                LogActivity(abuseReport);

                lblSaved.ResourceString = ConfirmationText;
                lblSaved.Visible = true;
                txtText.Visible = false;
                ReportButton.Visible = false;
            }
            else
            {
                lblSaved.ResourceString = "abuse.errors.reporttitle";
                lblSaved.CssClass = "ErrorLabel";
                lblSaved.Visible = true;
            }
        }
        else
        {
            lblSaved.ResourceString = "abuse.errors.reportcomment";
            lblSaved.CssClass = "ErrorLabel";
            lblSaved.Visible = true;
        }

        // Additional form modification
        ReportButton.Visible = false;
        CancelButton.ResourceString = "general.close";
    }

    #endregion
}
