using System;

using CMS.TreeEngine;
using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.WorkflowEngine;
using CMS.PortalEngine;
using CMS.UIControls;
using CMS.SiteProvider;
using CMS.URLRewritingEngine;

using TreeNode = CMS.TreeEngine.TreeNode;

public partial class CMSModules_Content_CMSDesk_View_Preview : CMSContentPage
{
    #region "Variables"

    protected string viewpage = "../blank.htm";
    protected string headersize = "43";

    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        // Switch the view mode
        CMSContext.ViewMode = ViewModeEnum.Preview;

        // Get the document
        int nodeId = 0;
        if (Request.QueryString["nodeid"] != null)
        {
            nodeId = ValidationHelper.GetInteger(Request.QueryString["nodeid"], 0);
        }

        string siteName = CMSContext.CurrentSiteName;
        TreeProvider tree = new TreeProvider(CMSContext.CurrentUser);
        TreeNode node = null;

        // Check split mode 
        bool isSplitMode = CMSContext.DisplaySplitMode;
        bool combineWithDefaultCulture = isSplitMode ? false : SiteInfoProvider.CombineWithDefaultCulture(siteName);

        // Get the document
        node = tree.SelectSingleNode(nodeId, CMSContext.PreferredCultureCode, combineWithDefaultCulture);
                
        // Redirect to the live URL
        if (node != null)
        {
            // If no workflow defined, hide the menu bar
            WorkflowManager wm = new WorkflowManager(tree);
            WorkflowInfo wi = wm.GetNodeWorkflow(node);
            if (wi == null)
            {
                headersize = "0";
            }
            else
            {
                // Get current step info
                WorkflowStepInfo si = wm.GetStepInfo(node);
                if (si != null)
                {
                    switch (si.StepName.ToLower())
                    {
                        case "published":
                        case "archived":
                            headersize = "0";
                            break;
                    }
                }
            }

            // Check the document availability
            if (!node.DocumentCulture.Equals(CMSContext.PreferredCultureCode, StringComparison.InvariantCultureIgnoreCase) && (!SiteInfoProvider.CombineWithDefaultCulture(siteName) || !node.DocumentCulture.Equals(CultureHelper.GetDefaultCulture(siteName), StringComparison.InvariantCultureIgnoreCase)))
            {
                viewpage = "~/CMSMessages/PageNotAvailable.aspx?reason=missingculture";
            }
            else
            {
                // Use permanent URL to get proper preview mode
                viewpage = URLRewriter.GetEditingUrl(node);
            }
        }
        else
        {
            viewpage = isSplitMode ? "~/CMSModules/Content/CMSDesk/New/NewCultureVersion.aspx" + URLHelper.Url.Query : "~/CMSMessages/PageNotAvailable.aspx?reason=missingculture&showlink=false";
        }

        // Register synchronization script for split mode
        if (isSplitMode)
        {
            RegisterSplitModeSync(false, false);
        }

        viewpage = ResolveUrl(viewpage);
    }
}
