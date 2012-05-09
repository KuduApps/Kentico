using System;

using CMS.CMSHelper;
using CMS.TreeEngine;
using CMS.GlobalHelper;
using CMS.ExtendedControls;
using CMS.UIControls;

using TreeNode = CMS.TreeEngine.TreeNode;

public partial class CMSModules_Content_CMSDesk_Default : CMSContentPage
{
    protected string treeUrl = "tree.aspx";
    protected string contentUrl = "contenteditframeset.aspx";
    protected string menuUrl = "menu.aspx";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (CultureHelper.IsUICultureRTL())
        {
            ControlsHelper.ReverseFrames(colsFrameset);
        }

        CurrentUserInfo currentUser = CMSContext.CurrentUser;

        // Set the culture if specified
        string culture = QueryHelper.GetString("culture", string.Empty);
        if (culture != "")
        {
            currentUser.PreferredCultureCode = culture;
        }

        // Check (and ensure) the proper content culture
        CheckPreferredCulture(false);

        // Process the action
        switch (QueryHelper.GetString("action", string.Empty).ToLower())
        {
            case "notallowed":
                contentUrl = "notallowed.aspx?action=" + QueryHelper.GetString("subaction", string.Empty).ToLower();
                break;

            case "new":
            case "delete":
                contentUrl = "documentframeset.aspx";
                break;

            case "edit":
                CMSContext.ViewMode = CMS.PortalEngine.ViewModeEnum.Edit;
                break;
        }

        treeUrl += URLHelper.Url.Query;
        contentUrl += URLHelper.Url.Query;
        menuUrl += URLHelper.Url.Query;

        // If no node specified, add the root node id
        int nodeId = QueryHelper.GetInteger("nodeid", 0);
        if (nodeId <= 0)
        {
            // Get the root node
            TreeProvider tree = new TreeProvider(currentUser);
            string baseDoc = "/"; // Root
            if (currentUser.UserStartingAliasPath != String.Empty)
            {
                baseDoc = currentUser.UserStartingAliasPath; // Change to user's root page
            }

            // Get the root node
            TreeNode rootNode = tree.SelectSingleNode(CMSContext.CurrentSiteName, baseDoc, TreeProvider.ALL_CULTURES, false, null, false);
            if (rootNode != null)
            {
                string nodeString = null;
                if (URLHelper.Url.Query != "")
                {
                    nodeString = "&nodeid=" + rootNode.NodeID;
                }
                else
                {
                    nodeString = "?nodeid=" + rootNode.NodeID;
                }
                treeUrl += nodeString;
                contentUrl += nodeString;
                menuUrl += nodeString;
            }
        }

        frameTree.Attributes.Add("src", treeUrl);
        frameView.Attributes.Add("src", contentUrl);
    }
}
