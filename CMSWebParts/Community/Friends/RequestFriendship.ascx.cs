using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.PortalControls;

public partial class CMSWebParts_Community_Friends_RequestFriendship : CMSAbstractWebPart
{
    /// <summary>
    /// Gets or sets link text.
    /// </summary>
    public string LinkText
    {
        get
        {
            return ValidationHelper.GetString(GetValue("LinkText"), string.Empty);
        }
        set
        {
            SetValue("LinkText", value);
        }
    }


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
        if (StopProcessing || CMSContext.CurrentUser.IsPublic())
        {
            requestFriendshipElem.StopProcessing = true;
            this.Visible = false;
        }
        else
        {
            requestFriendshipElem.LinkText = LinkText;
            requestFriendshipElem.UserID = CMSContext.CurrentUser.UserID;
        }
    }
}
