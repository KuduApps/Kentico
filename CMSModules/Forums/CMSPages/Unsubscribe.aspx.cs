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

using CMS.Forums;
using CMS.GlobalHelper;
using CMS.UIControls;

public partial class CMSModules_Forums_CMSPages_Unsubscribe : LivePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Forums
        Guid subGuid = QueryHelper.GetGuid("subGuid", Guid.Empty);
        if (subGuid != Guid.Empty)
        {
            ForumSubscriptionInfo fsi = null;

            if (subGuid != Guid.Empty)
            {
                fsi = ForumSubscriptionInfoProvider.GetForumSubscriptionInfo(subGuid);
            }
            else
            {
                lblError.Visible = true;
                lblError.Text = GetString("Forum.UnsubscribeUnsuccessfull");
                return;
            }

            if (fsi != null)
            {
                ForumSubscriptionInfoProvider.DeleteForumSubscriptionInfo(fsi.SubscriptionID);

                lblInfo.Visible = true;
                lblInfo.Text = GetString("Unsubscribe.Unsubscribed");
            }
            else
            {
                lblError.Visible = true;
                lblError.Text = GetString("forum.invalidsubscribeid");
            }
        }
    }
}
