using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.Synchronization;
using CMS.TreeEngine;
using CMS.UIControls;
using CMS.WorkflowEngine;
using CMS.SiteProvider;

using TreeNode = CMS.TreeEngine.TreeNode;

public partial class CMSModules_Content_CMSDesk_Properties_Relateddocs_List : CMSPropertiesPage
{
    #region "Protected variables"

    protected int nodeId = 0;
    protected TreeNode node = null;
    protected TreeProvider tree = null;

    #endregion


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
        UIContext.PropertyTab = PropertyTabEnum.RelatedDocs;

        nodeId = QueryHelper.GetInteger("nodeid", 0);

        // Check if any relationship exists
        DataSet dsRel = RelationshipNameInfoProvider.GetRelationshipNames("RelationshipNameID", "RelationshipAllowedObjects LIKE '%" + CMSObjectHelper.GROUP_DOCUMENTS + "%' AND RelationshipNameID IN (SELECT RelationshipNameID FROM CMS_RelationshipNameSite WHERE SiteID = " + CMSContext.CurrentSiteID + ")", null, 1);
        if (DataHelper.DataSourceIsEmpty(dsRel))
        {
            pnlNewItem.Visible = false;
            relatedDocuments.Visible = false;
            lblInfo.Text = ResHelper.GetString("relationship.norelationship");
            lblInfo.Visible = true;
        }
        else
        {
            if (nodeId > 0)
            {
                // Get the node
                tree = new TreeProvider(CMSContext.CurrentUser);
                node = tree.SelectSingleNode(nodeId, CMSContext.PreferredCultureCode, tree.CombineWithDefaultCulture);
                // Set edited document
                EditedDocument = node;

                if (node != null)
                {
                    // Check read permissions
                    if (CMSContext.CurrentUser.IsAuthorizedPerDocument(node, NodePermissionsEnum.Read) == AuthorizationResultEnum.Denied)
                    {
                        RedirectToAccessDenied(String.Format(GetString("cmsdesk.notauthorizedtoreaddocument"), node.NodeAliasPath));
                    }
                    // Check modify permissions
                    else if (CMSContext.CurrentUser.IsAuthorizedPerDocument(node, NodePermissionsEnum.Modify) == AuthorizationResultEnum.Denied)
                    {
                        relatedDocuments.Enabled = false;
                        lnkNewRelationship.Enabled = false;
                        imgNewRelationship.Enabled = false;
                        lblInfo.Visible = true;
                        lblInfo.Text = String.Format(GetString("cmsdesk.notauthorizedtoeditdocument"), node.NodeAliasPath);
                    }
                    else
                    {
                        lblInfo.Visible = false;
                    }

                    // Set tree node
                    relatedDocuments.TreeNode = node;

                    // Initialize controls
                    lnkNewRelationship.NavigateUrl = "~/CMSModules/Content/CMSDesk/Properties/Relateddocs_Add.aspx?nodeid=" + nodeId;
                    imgNewRelationship.ImageUrl = GetImageUrl("CMSModules/CMS_Content/Properties/addrelationship.png");
                    imgNewRelationship.DisabledImageUrl = GetImageUrl("CMSModules/CMS_Content/Properties/addrelationshipdisabled.png");
                }
            }
        }
    }


    protected void Page_PreRender(object sender, EventArgs e)
    {
        pnlInfo.Visible = !string.IsNullOrEmpty(lblInfo.Text);
    }

    #endregion
}