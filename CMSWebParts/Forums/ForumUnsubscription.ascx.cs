using System;
using System.Data;
using System.Web;
using System.Web.UI;

using CMS.GlobalHelper;
using CMS.PortalControls;
using CMS.Forums;

public partial class CMSWebParts_Forums_ForumUnsubscription : CMSAbstractWebPart
{
    #region "Public properties"

    /// <summary>
    /// Gets or sets the text which is displayed after successful unsubscription.
    /// </summary>
    public string UnsubscriptionText
    {
        get
        {
            return ValidationHelper.GetString(GetValue("UnsubscriptionText"), "");
        }
        set
        {
            SetValue("UnsubscriptionText", value);
        }
    }


    /// <summary>
    /// Gets or sets the text which is displayed after unsuccessful unsubscription.
    /// </summary>
    public string UnsuccessfulUnsubscriptionText
    {
        get
        {
            return ValidationHelper.GetString(GetValue("UnsuccessfulUnsubscriptionText"), "");
        }
        set
        {
            SetValue("UnsuccessfulUnsubscriptionText", value);
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
            Guid unsubscribeGuid = QueryHelper.GetGuid("unsubscribe", Guid.Empty);

            if (unsubscribeGuid != Guid.Empty)
            {
                ForumSubscriptionInfo fsi = ForumSubscriptionInfoProvider.GetForumSubscriptionInfo(unsubscribeGuid);
                if (fsi != null)
                {
                    ForumSubscriptionInfoProvider.DeleteForumSubscriptionInfo(fsi.SubscriptionID);

                    lblInfoText.Visible = true;
                    lblInfoText.Text = UnsubscriptionText;
                }
                else
                {
                    lblInfoText.Visible = true;
                    lblInfoText.Text = UnsuccessfulUnsubscriptionText;
                    lblInfoText.CssClass = "ErrorLabel";
                    return;
                }
            }
            else
            {
                Visible = false;
            }
        }
    }
}
