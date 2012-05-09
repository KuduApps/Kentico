using CMS.PortalControls;
using CMS.GlobalHelper;

public partial class CMSWebParts_AbuseReport_InlineAbuseReport : CMSAbstractWebPart
{
    #region "Properties"

    /// <summary>
    /// Gets or sets Confirmation text.
    /// </summary>
    public string ConfirmationText
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("ConfirmationText"), editReport.ConfirmationText);
        }
        set
        {
            this.SetValue("ConfirmationText", value);
            editReport.ConfirmationText = value;
        }
    }


    /// <summary>
    /// Gets or sets Report title.
    /// </summary>
    public string ReportTitle
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("ReportTitle"), editReport.ReportTitle);
        }
        set
        {
            this.SetValue("ReportTitle", value);
            editReport.ReportTitle = value;
        }
    }


    /// <summary>
    /// Gets or sets Report dialog title.
    /// </summary>
    public string ReportDialogTitle
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("ReportDialogTitle"), editReport.ReportDialogTitle);
        }
        set
        {
            this.SetValue("ReportDialogTitle", value);
            editReport.ReportDialogTitle = value;
        }
    }


    /// <summary>
    /// Gets or sets Report Object ID.
    /// </summary>
    public int ReportObjectID
    {
        get
        {
            return ValidationHelper.GetInteger(this.GetValue("ReportObjectID"), editReport.ReportObjectID);
        }
        set
        {
            this.SetValue("ReportObjectID", value);
            editReport.ReportObjectID = value;
        }
    }


    /// <summary>
    /// Gets or sets Report Object type.
    /// </summary>
    public string ReportObjectType
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("ReportObjectType"), editReport.ReportObjectType);
        }
        set
        {
            this.SetValue("ReportObjectType", value);
            editReport.ReportObjectType = value;
        }
    }

    #endregion


    #region "Stop processing"

    /// <summary>
    /// Returns true if the control processing should be stopped.
    /// </summary>
    public override bool StopProcessing
    {
        get
        {
            return base.StopProcessing;
        }
        set
        {
            base.StopProcessing = value;
            editReport.StopProcessing = value;
        }
    }

    #endregion


    /// <summary>
    /// Content loaded event handler.
    /// </summary>
    public override void OnContentLoaded()
    {
        base.OnContentLoaded();
        SetupControl();
    }


    /// <summary>
    /// Initializes the control properties.
    /// </summary>
    protected void SetupControl()
    {
        if (StopProcessing)
        {
            // Do nothing
        }
        else
        {
            editReport.ConfirmationText = this.ConfirmationText;
            editReport.ReportTitle = this.ReportTitle;
            editReport.ReportObjectID = this.ReportObjectID;
            editReport.ReportObjectType = this.ReportObjectType;
            editReport.ReportDialogTitle = this.ReportDialogTitle;
        }
    }
}
