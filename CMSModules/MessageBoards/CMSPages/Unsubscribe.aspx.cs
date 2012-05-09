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

using CMS.MessageBoard;
using CMS.GlobalHelper;
using CMS.UIControls;

public partial class CMSModules_MessageBoards_CMSPages_Unsubscribe : LivePage
{
    protected void Page_Load(object sender, EventArgs e)
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
                    this.lblInfo.Text = GetString("board.unsubscribe.success");
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
    }
}
