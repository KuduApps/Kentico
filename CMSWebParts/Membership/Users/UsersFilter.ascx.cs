using CMS.GlobalHelper;
using CMS.PortalControls;

public partial class CMSWebParts_Membership_Users_UsersFilter : CMSAbstractWebPart
{
    #region "Public properties"

    /// <summary>
    /// Gets or sets the filter button text.
    /// </summary>
    public string ButtonText
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("ButtonText"), "");
        }
        set
        {
            this.SetValue("ButtonText", value);
            this.filterUsers.ButtonText = value;
        }
    }


    /// <summary>
    /// Gets or sets the activity link text.
    /// </summary>
    public string SortActivityLinkText
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("SortActivityLinkText"), "");
        }
        set
        {
            this.SetValue("SortActivityLinkText", value);
            this.filterUsers.SortActivityLinkText = value;
        }
    }


    /// <summary>
    /// Gets or sets the user name link text.
    /// </summary>
    public string SortUserNameLinkText
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("SortUserNameLinkText"), "");
        }
        set
        {
            this.SetValue("SortUserNameLinkText", value);
            this.filterUsers.SortUserNameLinkText = value;
        }
    }
    

    /// <summary>
    /// Gets or sets the value that indicates whether cache should be disabled
    /// </summary>
    public bool DisableFilterCaching
    {
        get
        {
        	 return ValidationHelper.GetBoolean(this.GetValue("DisableFilterCaching"), false); 
        }
        set
        {
            this.SetValue("DisableFilterCaching", value);
            this.filterUsers.DisableFilterCaching = value;
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
    public void SetupControl()
    {
        if (this.StopProcessing)
        {
            // Do nothing
        }
        else
        {
            filterUsers.FilterName = ValidationHelper.GetString(GetValue("WebPartControlID"), "");
            filterUsers.ButtonText = ButtonText;
            filterUsers.SortUserNameLinkText = SortUserNameLinkText;
            filterUsers.SortActivityLinkText = SortActivityLinkText;
            filterUsers.DisableFilterCaching = DisableFilterCaching;
        }
    }
}
