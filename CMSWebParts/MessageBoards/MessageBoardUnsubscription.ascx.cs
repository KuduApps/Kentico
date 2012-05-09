using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using CMS.PortalControls;
using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.MessageBoard;

public partial class CMSWebParts_MessageBoards_MessageBoardUnsubscription : CMSAbstractWebPart
{
    #region "Public properties"

    /// <summary>
    /// Gets or sets the unsubscribed text.
    /// </summary>
    public string UnsubscribedText
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("UnsubscribedText"), "");
        }
        set
        {
            this.SetValue("UnsubscribedText", value);
        }
    }

    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
    }


    public override void OnContentLoaded()
    {
        base.OnContentLoaded();

        // Initialize controls
        SetupControls();
    }


    #region "Private methods"

    /// <summary>
    /// Initializes the controls.
    /// </summary>
    private void SetupControls()
    {
        if (!this.StopProcessing)
        {
            // Get info on subscription
            Guid subscriptionGuid = QueryHelper.GetGuid("subscriptionguid", Guid.Empty);
            if (subscriptionGuid != Guid.Empty)
            {
                DataSet subscriptionInfo = BoardSubscriptionInfoProvider.GetSubscriptions("SubscriptionGUID='" + subscriptionGuid + "'", null);
                if (!DataHelper.DataSourceIsEmpty(subscriptionInfo))
                {
                    try
                    {
                        int subscriptionId = ValidationHelper.GetInteger(subscriptionInfo.Tables[0].Rows[0]["SubscriptionID"], 0);

                        // Remove subscription information from the system
                        BoardSubscriptionInfoProvider.DeleteBoardSubscriptionInfo(subscriptionId);

                        // Inform user on success
                        this.lblInfo.Visible = true;
                        this.lblInfo.Text = ((this.UnsubscribedText.Trim() != "") ? this.UnsubscribedText : GetString("board.unsubscribe.success"));
                    }
                    catch (Exception ex)
                    {
                        // Inform user on general error
                        lblError.Visible = true;
                        lblError.Text = GetString("general.erroroccurred") + " " + ex.Message;
                    }
                }
                else
                {
                    // Inform user when specified subscription wasn't find in the system at all
                    lblError.Visible = true;
                    lblError.Text = GetString("board.unsubscribe.subscriptiondontexist");
                }
            }
            else
            {
                // Inform user when subscription information is missing
                this.Visible = false;
            }
        }
    }

    #endregion
}
