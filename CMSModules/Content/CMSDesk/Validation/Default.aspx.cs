using System;

using CMS.GlobalHelper;
using CMS.ExtendedControls;
using CMS.CMSHelper;
using CMS.TreeEngine;
using CMS.PortalEngine;
using CMS.UIControls;

using TreeNode = CMS.TreeEngine.TreeNode;

public partial class CMSModules_Content_CMSDesk_Validation_Default : CMSValidationPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int nodeId = QueryHelper.GetInteger("nodeid", 0);

        // Get current node
        TreeProvider tree = new TreeProvider(CMSContext.CurrentUser);
        TreeNode node = tree.SelectSingleNode(nodeId);
        
        // Set edited document
        EditedDocument = node;

        frameHeader.Attributes.Add("src", "header.aspx" + URLHelper.Url.Query);

        if (CultureHelper.IsUICultureRTL())
        {
            ControlsHelper.ReverseFrames(colsFrameset);
        }
    }
}
