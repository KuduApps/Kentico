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

[RegisterTitle("mvtest.list")]
public partial class CMSModules_OnlineMarketing_Pages_Content_MVTest_List : CMSMVTestContentPage
{
    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        listElem.EditPage = "frameset.aspx";

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

        if (!AnalyticsHelper.TrackConversionsEnabled(siteName))
        {
            this.pnlDisabled.Visible = true;
            this.lblTrackConversionsDisabled.Text = GetString("webanalytics.tackconversionsdisabled");
        }

        // Set NodeID in order to check the access to the document
        listElem.NodeID = NodeID;

        // Get the alias path of the current node
        if (this.Node != null)
        {
            listElem.AliasPath = Node.NodeAliasPath;
        }
        else
        {
            listElem.StopProcessing = true;
        }

        // Prepare the actions
        string[,] actions = new string[1, 6];
        actions[0, 0] = HeaderActions.TYPE_HYPERLINK;
        actions[0, 1] = GetString("mvtest.new");        
        actions[0, 3] = ResolveUrl("new.aspx?nodeid=" + NodeID);
        actions[0, 5] = GetImageUrl("Objects/OM_MVTest/add.png");

        // Set the actions
        ICMSMasterPage master = this.CurrentMaster;        
        master.HeaderActions.Actions = actions;
    }

    #endregion
}
