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

using CMS.GlobalHelper;
using CMS.TreeEngine;
using CMS.SiteProvider;
using CMS.CMSHelper;
using CMS.WorkflowEngine;
using CMS.Synchronization;
using CMS.UIControls;
using CMS.SettingsProvider;

using TreeNode = CMS.TreeEngine.TreeNode;

public partial class CMSModules_Content_CMSDesk_Properties_Alias_Edit : CMSPropertiesPage
{
    #region "Private variables"

    protected int aliasId = 0;
    protected int nodeId = 0;

    protected TreeNode node = null;
    protected TreeProvider tree = null;

    #endregion


    #region "Page events"

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        if (!CMSContext.CurrentUser.IsAuthorizedPerUIElement("CMS.Content", "Properties.URLs"))
        {
            RedirectToCMSDeskUIElementAccessDenied("CMS.Content", "Properties.URLs");
        }

        if (!CMSContext.CurrentUser.IsAuthorizedPerUIElement("CMS.Content", "URLs.Aliases"))
        {
            RedirectToCMSDeskUIElementAccessDenied("CMS.Content", "URLs.Aliases");
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        nodeId = ValidationHelper.GetInteger(Request.QueryString["nodeid"], 0);

        if (nodeId > 0)
        {
            // Get the node
            tree = new TreeProvider(CMSContext.CurrentUser);
            node = tree.SelectSingleNode(nodeId);

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
                    this.lblInfo.Text = String.Format(GetString("cmsdesk.notauthorizedtoeditdocument"), node.NodeAliasPath);

                    usSelectCampaign.Enabled = false;
                    txtURLExtensions.Enabled = false;

                    ctrlURL.Enabled = false;

                    cultureSelector.Enabled = false;
                }

                if (QueryHelper.GetInteger("saved", 0) == 1)
                {
                    lblInfo.Text = GetString("general.changessaved");
                }

                lblDocumentCulture.Text = GetString("general.culture") + ResHelper.Colon;
                lblTrackCampaign.Text = GetString("doc.urls.trackcampaign") + ResHelper.Colon;
                lblURLExtensions.Text = GetString("doc.urls.urlextensions") + ResHelper.Colon;

                btnOk.Text = GetString("general.ok");

                // Initialiaze page title
                string urls = GetString("Properties.Urls");
                string urlsUrl = "~/CMSModules/Content/CMSDesk/Properties/Alias_List.aspx?nodeid=" + nodeId.ToString();
                string addAlias = GetString("doc.urls.addnewalias");

                // Initializes page title
                string[,] pageTitleTabs = new string[2, 3];
                pageTitleTabs[0, 0] = urls;
                pageTitleTabs[0, 1] = urlsUrl;
                pageTitleTabs[0, 2] = "";
                pageTitleTabs[1, 0] = addAlias;
                pageTitleTabs[1, 1] = "";
                pageTitleTabs[1, 2] = "";
                pageAlias.Breadcrumbs = pageTitleTabs;

                this.cultureSelector.AddDefaultRecord = false;
                this.cultureSelector.SpecialFields = new string[,] { { GetString("general.selectall"), "" } };
                this.cultureSelector.CssClass = "ContentMenuLangDrop";

                aliasId = ValidationHelper.GetInteger(Request.QueryString["aliasid"], 0);

                pageAlias.HelpName = "helpTopic";
                pageAlias.HelpTopicName = "doc_documentalias_edit";

                if (!RequestHelper.IsPostBack())
                {
                    this.cultureSelector.Value = node.DocumentCulture;

                    // Edit existing alias
                    if (aliasId > 0)
                    {
                        DocumentAliasInfo dai = DocumentAliasInfoProvider.GetDocumentAliasInfo(aliasId);

                        if (dai != null)
                        {
                            usSelectCampaign.Value = dai.AliasCampaign;

                            txtURLExtensions.Text = dai.AliasExtensions;
                            ctrlURL.URLPath = dai.AliasURLPath;

                            cultureSelector.Value = dai.AliasCulture;
                            pageTitleTabs[1, 0] = addAlias = dai.AliasURLPath;
                        }
                    }
                }

                pageAlias.Breadcrumbs = pageTitleTabs;

                // Register js synchronization script for split mode
                if (QueryHelper.GetBoolean("refresh", false) && CMSContext.DisplaySplitMode)
                {
                    RegisterSplitModeSync(true, false, true);
                }
            }
        }
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        this.lblInfo.Visible = (this.lblInfo.Text != "");
        this.lblError.Visible = (this.lblError.Text != "");
    }

    #endregion


    protected void btnOK_Click(object sender, EventArgs e)
    {
        if (String.IsNullOrEmpty(this.ctrlURL.PlainURLPath))
        {
            lblError.Text = GetString("doc.urls.requiresurlpath");
            return;
        }

        if (!usSelectCampaign.IsValid())
        {
            lblError.Visible = true;
            lblError.Text = GetString("campaign.validcodename");
            return;
        }

        // Get the document
        TreeProvider tree = new TreeProvider(CMSContext.CurrentUser);
        node = tree.SelectSingleNode(nodeId, CMSContext.PreferredCultureCode);
        if (node != null)
        {
            // Check modify permissions
            if (CMSContext.CurrentUser.IsAuthorizedPerDocument(node, NodePermissionsEnum.Modify) == AuthorizationResultEnum.Denied)
            {
                lblInfo.Text = string.Empty;
                lblError.Text = String.Format(GetString("cmsdesk.notauthorizedtoreaddocument"), node.NodeAliasPath);
                return;
            }

            DocumentAliasInfo dai = null;

            // Edit existing alias
            if (aliasId > 0)
            {
                dai = DocumentAliasInfoProvider.GetDocumentAliasInfo(aliasId);
            }

            if (dai == null)
            {
                dai = new DocumentAliasInfo();
            }

            // Set object properties
            dai.AliasURLPath = ctrlURL.URLPath;

            dai.AliasCampaign = ValidationHelper.GetString(usSelectCampaign.Value, String.Empty).Trim();
            dai.AliasExtensions = txtURLExtensions.Text.Trim();
            dai.AliasCulture = ValidationHelper.GetString(cultureSelector.Value, "");
            dai.AliasSiteID = CMSContext.CurrentSite.SiteID;

            if (nodeId > 0)
            {
                dai.AliasNodeID = nodeId;
            }

            // Insert into database
            DocumentAliasInfoProvider.SetDocumentAliasInfo(dai, node.NodeSiteName);

            // Log synchronization
            DocumentSynchronizationHelper.LogDocumentChange(node, TaskTypeEnum.UpdateDocument, tree);

            nodeId = dai.AliasNodeID;
            aliasId = dai.AliasID;

            string url = "Alias_Edit.aspx?saved=1&nodeid=" + nodeId.ToString() + "&aliasid=" + aliasId.ToString();

            // Refresh the second frame in split mode
            if (CMSContext.DisplaySplitMode)
            {
                if (string.Compare(CMSContext.PreferredCultureCode, CMSContext.SplitModeCultureCode, StringComparison.InvariantCultureIgnoreCase) == 0)
                {
                    url += "&refresh=1";
                }
            }

            URLHelper.Redirect(url);
        }
    }
}
