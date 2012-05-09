using CMS.PortalControls;
using CMS.GlobalHelper;
using CMS.PortalEngine;

public partial class CMSWebParts_Messaging_MyMessages : CMSAbstractWebPart
{
    #region "Public properties"

    /// <summary>
    /// Gets or sets the WebPart CSS class value.
    /// </summary>
    public override string CssClass
    {
        get
        {
            return base.CssClass;
        }
        set
        {
            base.CssClass = value;
            this.ucMyMessages.CssClass = value;
        }
    }


    /// <summary>
    /// Gets or sets the query string parameter name.
    /// </summary>
    public string ParameterName
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("ParameterName"), this.ucMyMessages.ParameterName);
        }
        set
        {
            this.SetValue("ParameterName", value);
            this.ucMyMessages.ParameterName = value;
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether 'inbox' is displayed.
    /// </summary>
    public bool DisplayInbox
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("DisplayInbox"), this.ucMyMessages.DisplayInbox);
        }
        set
        {
            this.SetValue("DisplayInbox", value);
            this.ucMyMessages.DisplayInbox = value;
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether 'outbox' is displayed.
    /// </summary>
    public bool DisplayOutbox
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("DisplayOutbox"), this.ucMyMessages.DisplayOutbox);
        }
        set
        {
            this.SetValue("DisplayOutbox", value);
            this.ucMyMessages.DisplayOutbox = value;
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether 'contact list' is displayed.
    /// </summary>
    public bool DisplayContactList
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("DisplayContactList"), this.ucMyMessages.DisplayContactList);
        }
        set
        {
            this.SetValue("DisplayContactList", value);
            this.ucMyMessages.DisplayContactList = value;
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether 'ignore list' is displayed.
    /// </summary>
    public bool DisplayIgnoreList
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("DisplayIgnoreList"), this.ucMyMessages.DisplayIgnoreList);
        }
        set
        {
            this.SetValue("DisplayIgnoreList", value);
            this.ucMyMessages.DisplayIgnoreList = value;
        }
    }


    /// <summary>
    /// Gets or sets the message which should be displayed for public users.
    /// </summary>
    public string NotAuthenticatedMessage
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("NotAuthenticatedMessage"), string.Empty);
        }
        set
        {
            this.SetValue("NotAuthenticatedMessage", value);
            this.ucMyMessages.NotAuthenticatedMessage = value;
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
            ucMyMessages.StopProcessing = value;
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
    /// Setup control.
    /// </summary>
    protected void SetupControl()
    {
        if (this.StopProcessing)
        {
            // Do nothing
            ucMyMessages.StopProcessing = true;
        }
        else
        {
            ucMyMessages.ParameterName = this.ParameterName;
            ucMyMessages.CssClass = this.CssClass;
            ucMyMessages.DisplayInbox = this.DisplayInbox;
            ucMyMessages.DisplayOutbox = this.DisplayOutbox;
            ucMyMessages.DisplayIgnoreList = this.DisplayIgnoreList;
            ucMyMessages.DisplayContactList = this.DisplayContactList;
            ucMyMessages.NotAuthenticatedMessage = this.NotAuthenticatedMessage;
            ucMyMessages.IsLiveSite = (ViewMode != ViewModeEnum.DashboardWidgets);
            this.AdditonalCssClass = "MyMessagesWebPart";
        }
    }


    /// <summary>
    /// Reload data.
    /// </summary>
    public override void ReloadData()
    {
        base.ReloadData();
        this.SetupControl();
    }
}