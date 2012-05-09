using CMS.PortalControls;
using CMS.GlobalHelper;

public partial class CMSWebParts_Newsletters_MySubscriptionsWebpart : CMSAbstractWebPart
{

    #region "Public Properties"

    /// <summary>
    /// Indicates whether send emails when (un)subscribed.
    /// </summary>
    public bool SendConfirmationEmails
    {
        get
        {
            return ValidationHelper.GetBoolean(GetValue("SendConfirmationEmails"), true); ;
        }
        set
        {
            SetValue("SendConfirmationEmails", value);
        }
    }

    #endregion 


    #region "Methods"

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
            // Stop processing
            this.ucMySubsriptions.StopProcessing = true;
        }
        else
        {
            this.ucMySubsriptions.ControlContext = this.ControlContext;

            this.ucMySubsriptions.CacheMinutes = this.CacheMinutes;
            this.ucMySubsriptions.ExternalUse = true;
            this.ucMySubsriptions.SendConfirmationEmail = SendConfirmationEmails; 
        }
    }


    /// <summary>
    /// Reload data.
    /// </summary>
    public override void ReloadData()
    {
        base.ReloadData();

        SetupControl();
        this.ucMySubsriptions.ExternalUse = false;
        this.ucMySubsriptions.LoadData();
    }

    #endregion 
}

