using System;

using CMS.GlobalHelper;
using CMS.ExtendedControls;
using CMS.CMSHelper;
using CMS.TreeEngine;
using CMS.PortalEngine;
using CMS.UIControls;

using TreeNode = CMS.TreeEngine.TreeNode;

[RegisterTitle("content.ui.properties")]
public partial class CMSModules_Content_CMSDesk_Properties_Default : CMSPropertiesPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int nodeId = QueryHelper.GetInteger("nodeid", 0);

        // Get current node
        TreeProvider tree = new TreeProvider(CMSContext.CurrentUser);
        TreeNode node = tree.SelectSingleNode(nodeId);
        // Set edited document
        EditedDocument = node;

        if (node != null)
        {
            // Check read permissions
            if (CMSContext.CurrentUser.IsAuthorizedPerDocument(node, NodePermissionsEnum.Read) == AuthorizationResultEnum.Denied)
            {
                RedirectToAccessDenied(String.Format(GetString("cmsdesk.notauthorizedtoreaddocument"), node.NodeAliasPath));
            }

            CMSContext.ViewMode = ViewModeEnum.Properties;
        }

        frameHeader.Attributes.Add("src", "header.aspx" + URLHelper.Url.Query);

        if (CultureHelper.IsUICultureRTL())
        {
            ControlsHelper.ReverseFrames(colsFrameset);
        }

        ScriptHelper.RegisterTitleScript(this, GetString("content.ui.properties"));

        // Register js script for split mode
        if (CMSContext.DisplaySplitMode)
        {
            ScriptHelper.RegisterClientScriptBlock(Page, typeof(string), "closeSplitMode", ScriptHelper.GetScript("function CloseSplitMode() { parent.CloseSplitMode(); }"));
        }
    }
}
