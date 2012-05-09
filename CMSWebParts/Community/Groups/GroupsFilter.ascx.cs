using CMS.PortalControls;
using CMS.GlobalHelper;

public partial class CMSWebParts_Community_Groups_GroupsFilter : CMSAbstractWebPart
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
            this.filterGroups.ButtonText = value;
        }
    }


    /// <summary>
    /// Gets or sets the group name link text.
    /// </summary>
    public string SortGroupNameLinkText
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("SortGroupNameLinkText"), "");
        }
        set
        {
            this.SetValue("SortGroupNameLinkText", value);
            this.filterGroups.SortGroupNameLinkText = value;
        }
    }


    /// <summary>
    /// Gets or sets the group created link text.
    /// </summary>
    public string SortGroupCreatedLinkText
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("SortGroupCreatedLinkText"), "");
        }
        set
        {
            this.SetValue("SortGroupCreatedLinkText", value);
            this.filterGroups.SortGroupCreatedLinkText = value;
        }
    }


    /// <summary>
    /// Gets or sets the filter button text.
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
            this.filterGroups.DisableFilterCaching = value;
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
            filterGroups.FilterName = ValidationHelper.GetString(GetValue("WebPartControlID"), "");
            filterGroups.ButtonText = ButtonText;
            filterGroups.SortGroupCreatedLinkText = SortGroupCreatedLinkText;
            filterGroups.SortGroupNameLinkText = SortGroupNameLinkText;
            filterGroups.DisableFilterCaching = DisableFilterCaching;
        }
    }
}
