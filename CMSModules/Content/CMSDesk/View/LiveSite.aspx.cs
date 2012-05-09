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

using CMS.TreeEngine;
using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.PortalEngine;
using CMS.UIControls;
using CMS.URLRewritingEngine;
using CMS.SiteProvider;

using TreeNode = CMS.TreeEngine.TreeNode;

public partial class CMSModules_Content_CMSDesk_View_LiveSite : CMSContentPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Switch the view mode
        CMSContext.ViewMode = ViewModeEnum.LiveSite;

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
        string url = null;
        if (node != null)
        {
            // Check if the node is published or available
            if (!node.DocumentCulture.Equals(CMSContext.PreferredCultureCode, StringComparison.InvariantCultureIgnoreCase) && (!SiteInfoProvider.CombineWithDefaultCulture(siteName) || !node.DocumentCulture.Equals(CultureHelper.GetDefaultCulture(siteName), StringComparison.InvariantCultureIgnoreCase)))
            {
                url = "~/CMSMessages/PageNotAvailable.aspx?reason=missingculture";
            }

            if ((url == null) && !node.IsPublished && URLRewriter.PageNotFoundForNonPublished(siteName))
            {
                // Try to find published document in default culture
                if (SiteInfoProvider.CombineWithDefaultCulture(siteName))
                {
                    string defaultCulture = CultureHelper.GetDefaultCulture(siteName);
                    node = tree.SelectSingleNode(nodeId, defaultCulture, false);
                    if ((node != null) && node.IsPublished)
                    {
                        // Do not use document URL path - preferred culture could be changed
                        url = CMSContext.GetUrl(node.NodeAliasPath, null);
                    }
                }

                if (url == null)
                {
                    // Document is not published
                    url = "~/CMSMessages/PageNotAvailable.aspx?reason=notpublished";
                }
            }
        }
        else
        {
            url = isSplitMode ? "~/CMSModules/Content/CMSDesk/New/NewCultureVersion.aspx" + URLHelper.Url.Query : "~/CMSMessages/PageNotAvailable.aspx?reason=missingculture&showlink=false";
        }

        if (url == null)
        {
            // Do not use document URL path - preferred culture could be changed
            url = CMSContext.GetUrl(node.NodeAliasPath, null);
        }

        // Split mode
        if (CMSContext.DisplaySplitMode)
        {
            url = URLHelper.AddParameterToUrl(url, "cmssplitmode", "1");
        }

        URLHelper.Redirect(url);
    }
}