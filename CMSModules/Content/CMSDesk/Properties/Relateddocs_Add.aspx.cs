using System;
using System.Web;
using System.Web.UI;

using CMS.TreeEngine;
using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.WorkflowEngine;
using CMS.Synchronization;
using CMS.UIControls;
using CMS.ExtendedControls;

using TreeNode = CMS.TreeEngine.TreeNode;

public partial class CMSModules_Content_CMSDesk_Properties_Relateddocs_Add : CMSPropertiesPage
{
    #region "Page events"

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        if (!CMSContext.CurrentUser.IsAuthorizedPerUIElement("CMS.Content", "Properties.RelatedDocs"))
        {
            RedirectToCMSDeskUIElementAccessDenied("CMS.Content", "Properties.RelatedDocs");
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        int currentNodeId = QueryHelper.GetInteger("nodeid", 0);

        // Initializes page breadcrumbs
        string[,] pageTitleTabs = new string[2, 3];
        pageTitleTabs[0, 0] = GetString("Relationship.RelatedDocs");
        pageTitleTabs[0, 1] = "~/CMSModules/Content/CMSDesk/Properties/Relateddocs_List.aspx?nodeid=" + currentNodeId;
        pageTitleTabs[0, 2] = "propedit";
        pageTitleTabs[1, 0] = GetString("Relationship.AddRelatedDocs");
        pageTitleTabs[1, 1] = string.Empty;
        pageTitleTabs[1, 2] = string.Empty;
        titleElem.Breadcrumbs = pageTitleTabs;

        if (currentNodeId > 0)
        {
            TreeProvider treeProvider = new TreeProvider(CMSContext.CurrentUser);
            TreeNode node = treeProvider.SelectSingleNode(currentNodeId);
            // Set edited document
            EditedDocument = node;

            // Set node
            addRelatedDocument.TreeNode = node;
            addRelatedDocument.IsLiveSite = false;
        }
    }

    #endregion
}