using System;

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.TreeEngine;
using CMS.WorkflowEngine;

[RegisterTitle("content.ui.masterpage")]
public partial class CMSModules_Content_CMSDesk_MasterPage_PageEditFrameset : CMSContentPage
{
    private bool displaySplitMode = CMSContext.DisplaySplitMode;

    protected void Page_Load(object sender, EventArgs e)
    {
        CurrentUserInfo user = CMSContext.CurrentUser;

        // Check UIProfile
        if (!user.IsAuthorizedPerUIElement("CMS.Content", "MasterPage"))
        {
            RedirectToCMSDeskUIElementAccessDenied("CMS.Content", "MasterPage");
        }

        // Check "Design" permission
        if (!user.IsAuthorizedPerResource("CMS.Design", "Design"))
        {
            RedirectToAccessDenied("CMS.Design", "Design");
        }

        if(displaySplitMode)
        {
            // Get document node
            int nodeId = QueryHelper.GetInteger("nodeid", 0);
            TreeNode node = DocumentHelper.GetDocument(nodeId, CMSContext.PreferredCultureCode, false, null);
           
            // Redirect to page 'New culture version' in split mode.
            if ((node == null) && displaySplitMode)
            {
                URLHelper.Redirect("~/CMSModules/Content/CMSDesk/New/NewCultureVersion.aspx" + URLHelper.Url.Query);
            }
        }

        // Add progress script to the header collection (can't use inline code blocks or ScriptHelper)
        HTMLHelper.AddToHeader(this, ScriptHelper.GetIncludeScript("progress.js"));

        HTMLHelper.AddToHeader(this, ScriptHelper.GetScript("var IsCMSDesk = true;"));

        // Register synchronization script for split mode
        if (CMSContext.DisplaySplitMode)
        {
            RegisterSplitModeSync(false, false);
        }
    }
}