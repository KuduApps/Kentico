using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.OnlineMarketing;
using CMS.TreeEngine;
using CMS.WebAnalytics;

using TreeNode = CMS.TreeEngine.TreeNode;

public partial class CMSModules_OnlineMarketing_Pages_Content_MVTest_Edit : CMSMVTestContentPage
{
    #region "Methods"

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        String siteName = CMSContext.CurrentSiteName;

        // Display disabled information
        if (!AnalyticsHelper.AnalyticsEnabled(siteName))
        {
            this.pnlDisabled.Visible = true;
            this.lblDisabled.Text = GetString("WebAnalytics.Disabled") + "<br/>";
        }

        if (!MVTestInfoProvider.MVTestingEnabled(siteName))
        {
            this.pnlDisabled.Visible = true;
            this.lblMVTestingDisabled.Text = GetString("mvt.disabled");
        }

        // Get the alias path of the current node
        if (Node != null)
        {
            editElem.AliasPath = Node.NodeAliasPath;
        }
        else
        {
            editElem.StopProcessing = true;
        }

        editElem.MVTestID = QueryHelper.GetInteger("mvtestId", 0);
        editElem.OnSaved += new EventHandler(editElem_OnSaved);
    }


    protected void editElem_OnSaved(object sender, EventArgs e)
    {
        URLHelper.Redirect("Edit.aspx?saved=1&mvtestId=" + editElem.ItemID + "&nodeid=" + NodeID);
    }

    #endregion
}
