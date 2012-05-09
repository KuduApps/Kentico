using CMS.PortalControls;
using CMS.GlobalHelper;

public partial class CMSWebParts_Messaging_Outbox : CMSAbstractWebPart
{
    #region "Public properties"

    /// <summary>
    /// Gets or sets the text which is displayed when no data found.
    /// </summary>
    public string ZeroRowsText
    {
        get
        {
            return DataHelper.GetNotEmpty(ValidationHelper.GetString(this.GetValue("ZeroRowsText"), this.ucOutbox.ZeroRowsText), this.ucOutbox.ZeroRowsText);
        }
        set
        {
            this.SetValue("ZeroRowsText", value);
            this.ucOutbox.ZeroRowsText = value;
        }
    }


    /// <summary>
    /// Gets or sets the size of the page when paging is used.
    /// </summary>
    public int PageSize
    {
        get
        {
            return ValidationHelper.GetInteger(this.GetValue("PageSize"), this.ucOutbox.PageSize);
        }
        set
        {
            this.SetValue("PageSize", value);
            this.ucOutbox.PageSize = value;
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether the original message should be shown.
    /// </summary>
    public bool ShowOriginalMessage
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("ShowOriginalMessage"), this.ucOutbox.ShowOriginalMessage);
        }
        set
        {
            this.SetValue("ShowOriginalMessage", value);
            this.ucOutbox.ShowOriginalMessage = value;
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
            ucOutbox.StopProcessing = value;
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
        if (this.StopProcessing)
        {
            // Do nothing
            ucOutbox.StopProcessing = true;
        }
        else
        {
            this.ucOutbox.ZeroRowsText = this.ZeroRowsText;
            this.ucOutbox.PageSize = this.PageSize;
            this.ucOutbox.ShowOriginalMessage = this.ShowOriginalMessage;
        }
    }


    /// <summary>
    /// Reloads the data.
    /// </summary>
    public override void ReloadData()
    {
        this.SetupControl();
    }
}
