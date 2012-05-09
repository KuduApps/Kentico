using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.UIControls;
using CMS.GlobalHelper;
using CMS.OnlineMarketing;

public partial class CMSModules_ContactManagement_Pages_Tools_Account_Frameset : CMSContactManagementAccountsPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Modify high of the first row...
        if (QueryHelper.GetBoolean("dialogmode", false))
        {
            //  in modal dialog
            frm.FrameHeight = TabsFrameHeight;
        }
        else if (!ContactHelper.IsSiteManager)
        {
            // in CMSDesk
            frm.FrameHeight = TabsBreadHeadFrameHeight;
        }
        else
        {
            // in SiteManager
            frm.FrameHeight = TabsBreadFrameHeight;
        }
        frm.HeaderUrl = URLHelper.AddParameterToUrl(frm.HeaderUrl, "siteid", this.SiteID.ToString());
        if (ContactHelper.IsSiteManager)
        {
            frm.HeaderUrl = URLHelper.AddParameterToUrl(frm.HeaderUrl, "issitemanager", "1");
        }
    }
}