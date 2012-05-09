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

using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.Blogs;

public partial class CMSModules_Blogs_CMSPages_Unsubscribe : LivePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Get info on subscription
        Guid subscriptionGuid = QueryHelper.GetGuid("subscriptionguid", Guid.Empty);
        if (subscriptionGuid != Guid.Empty)
        {
            BlogPostSubscriptionInfo subscriptionInfo = BlogPostSubscriptionInfoProvider.GetBlogPostSubscriptionInfo(subscriptionGuid);
            if (subscriptionInfo != null)
            {
                try
                {
                    // Remove subscription information from the system
                    BlogPostSubscriptionInfoProvider.DeleteBlogPostSubscriptionInfo(subscriptionInfo.SubscriptionID);

                    // Inform user on success
                    this.lblInfo.Visible = true;
                    this.lblInfo.Text = GetString("blog.unsubscribe.success");
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
                lblError.Text = GetString("blog.unsubscribe.subscriptiondontexist");
            }
        }
    }
}
