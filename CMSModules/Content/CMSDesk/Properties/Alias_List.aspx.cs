using System;

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.SettingsProvider;
using CMS.SiteProvider;
using CMS.Synchronization;
using CMS.TreeEngine;
using CMS.UIControls;
using CMS.WorkflowEngine;

using TreeNode = CMS.TreeEngine.TreeNode;

[RegisterTitle("content.ui.propertiesurls")]
public partial class CMSModules_Content_CMSDesk_Properties_Alias_List : CMSPropertiesPage
{
    #region "Private variables"

    protected int nodeId = 0;
    protected string mSave = null;

    private TreeNode node = null;
    private TreeProvider tree = null;
    private bool isRoot = false;
    private bool displaySplitMode = CMSContext.DisplaySplitMode;

    #endregion


    #region "Methods"

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        this.UniGridAlias.StopProcessing = this.pnlUIDocumentAlias.IsHidden;

        if (!CMSContext.CurrentUser.IsAuthorizedPerUIElement("CMS.Content", "Properties.URLs"))
        {
            RedirectToCMSDeskUIElementAccessDenied("CMS.Content", "Properties.URLs");
        }

        // Redirect to information page when no UI elements displayed
        if (this.pnlUIDocumentAlias.IsHidden && this.pnlUIExtended.IsHidden && this.pnlUIPath.IsHidden)
        {
            RedirectToUINotAvailable();
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        // Register the scripts
        ScriptHelper.RegisterProgress(Page);

        UIContext.PropertyTab = PropertyTabEnum.URLs;

        nodeId = QueryHelper.GetInteger("nodeid", 0);

        // Set where condition - show nothing when nodeId is zero
        UniGridAlias.WhereCondition = "AliasNodeID = " + nodeId;

        if (nodeId > 0)
        {
            // Get the node
            tree = new TreeProvider(CMSContext.CurrentUser);
            node = tree.SelectSingleNode(nodeId, CMSContext.PreferredCultureCode, false);

            // Redirect to page 'New culture version' in split mode. It must be before setting EditedDocument.
            if ((node == null) && displaySplitMode)
            {
                URLHelper.Redirect("~/CMSModules/Content/CMSDesk/New/NewCultureVersion.aspx" + URLHelper.Url.Query);
            }
            // Set edited document
            EditedDocument = node;

            if (node != null)
            {
                // Set IsRoot flag
                isRoot = (node.NodeClassName.ToLower() == "cms.root");

                imgSave.DisabledImageUrl = GetImageUrl("CMSModules/CMS_Content/EditMenu/savedisabled.png");
                imgSave.ImageUrl = GetImageUrl("CMSModules/CMS_Content/EditMenu/save.png");
                imgNewAlias.ImageUrl = GetImageUrl("CMSModules/CMS_Content/Properties/adddocumentalias.png");
                imgNewAlias.DisabledImageUrl = GetImageUrl("CMSModules/CMS_Content/Properties/adddocumentaliasdisabled.png");
                if (node.NodeAliasPath == "/")
                {
                    valAlias.Visible = false;
                }

                // Check read permissions
                if (CMSContext.CurrentUser.IsAuthorizedPerDocument(node, NodePermissionsEnum.Read) == AuthorizationResultEnum.Denied)
                {
                    RedirectToAccessDenied(String.Format(GetString("cmsdesk.notauthorizedtoreaddocument"), node.NodeAliasPath));
                }
                // Check modify permissions
                else if (CMSContext.CurrentUser.IsAuthorizedPerDocument(node, NodePermissionsEnum.Modify) == AuthorizationResultEnum.Denied)
                {
                    UniGridAlias.GridView.Enabled = false;
                    lnkNewAlias.Enabled = false;
                    imgNewAlias.Enabled = false;

                    chkCustomExtensions.Enabled = false;
                    ctrlURL.Enabled = false;

                    lblInfo.Visible = true;
                    lblInfo.Text = String.Format(GetString("cmsdesk.notauthorizedtoeditdocument"), node.NodeAliasPath);

                    // Disable save button
                    lnkSave.Enabled = false;
                    lnkSave.CssClass = "MenuItemEditDisabled";

                }
                else
                {
                    lblInfo.Visible = false;
                    ltlScript.Text = ScriptHelper.GetScript("var node = " + nodeId + "; \n var deleteMsg = '" + GetString("DocumentAlias.DeleteMsg") + "';");

                    UniGridAlias.OnAction += UniGridAlias_OnAction;
                    UniGridAlias.OnExternalDataBound += UniGridAlias_OnExternalDataBound;

                    // Register Save Document script
                    ScriptHelper.RegisterSaveShortcut(lnkSave, null, false);
                }

                lnkNewAlias.Text = GetString("doc.urls.addnewalias");
                lnkNewAlias.NavigateUrl = "Alias_Edit.aspx?nodeid=" + nodeId;


                mSave = GetString("general.save");
                lblAlias.Text = GetString("GeneralProperties.Alias");

                chkCustomExtensions.Text = GetString("GeneralProperties.UseCustomExtensions");
                valAlias.ErrorMessage = GetString("GeneralProperties.RequiresAlias");

                lblExtensions.Text = GetString("doc.urls.urlextensions") + ResHelper.Colon;

                pnlURLPath.GroupingText = GetString("GeneralProperties.UrlPath");
                pnlAlias.GroupingText = GetString("GeneralProperties.DocumentAlias");

                pnlDocumentAlias.GroupingText = GetString("doc.urls.documentalias");
                pnlExtended.GroupingText = GetString("doc.urls.extendedproperties");

                if (!isRoot)
                {
                    txtAlias.Enabled = !TreePathUtils.AutomaticallyUpdateDocumentAlias(CMSContext.CurrentSiteName);
                }

                if (!RequestHelper.IsPostBack())
                {
                    ReloadData();
                }

                ctrlURL.AutomaticURLPath = TreePathUtils.GetUrlPathFromNamePath(node.DocumentNamePath, node.NodeLevel, CMSContext.CurrentSiteName);
            }

            // Register js synchronization script for split mode
            if (displaySplitMode)
            {
                RegisterSplitModeSync(true, false);
            }
        }
    }


    protected object UniGridAlias_OnExternalDataBound(object sender, string sourceName, object parameter)
    {
        switch (sourceName.ToLower())
        {
            case "culture":
                return UniGridFunctions.CultureDisplayName(parameter);

            case "urlpath":
                {
                    // Parse the URL path
                    string urlPath = ValidationHelper.GetString(parameter, "");

                    string prefix = null;
                    TreePathUtils.ParseUrlPath(ref urlPath, out prefix, null);

                    if (prefix.StartsWith(TreePathUtils.URL_PREFIX_MVC, StringComparison.InvariantCultureIgnoreCase))
                    {
                        urlPath = GetString("URLPath.MVC") + ": " + urlPath;
                    }
                    else if (prefix.StartsWith(TreePathUtils.URL_PREFIX_ROUTE, StringComparison.InvariantCultureIgnoreCase))
                    {
                        urlPath = GetString("URLPath.Route") + ": " + urlPath;
                    }

                    return urlPath;
                }
        }

        return parameter;
    }


    void UniGridAlias_OnAction(string actionName, object actionArgument)
    {
        if (node != null)
        {
            // Check modify permissions
            if (CMSContext.CurrentUser.IsAuthorizedPerDocument(node, NodePermissionsEnum.Modify) == AuthorizationResultEnum.Denied)
            {
                return;
            }

            string action = DataHelper.GetNotEmpty(actionName, String.Empty).ToLower();

            switch (action)
            {
                case "edit":
                    // Edit action
                    URLHelper.Redirect("Alias_Edit.aspx?nodeid=" + nodeId + "&aliasid=" + Convert.ToString(actionArgument));
                    break;

                case "delete":
                    // Delete action
                    int aliasId = ValidationHelper.GetInteger(actionArgument, 0);
                    if (aliasId > 0)
                    {
                        // Delete
                        DocumentAliasInfoProvider.DeleteDocumentAliasInfo(aliasId);

                        // Log synchronization
                        DocumentSynchronizationHelper.LogDocumentChange(node, TaskTypeEnum.UpdateDocument, tree);
                    }
                    break;
            }
        }
    }


    protected void chkCustomExtensions_CheckedChanged(object sender, EventArgs e)
    {
        txtExtensions.Enabled = chkCustomExtensions.Checked;
        if (!chkCustomExtensions.Checked)
        {
            txtExtensions.Text = node.DocumentExtensions;
        }
    }


    protected void lnkSave_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtAlias.Text.Trim()) && !isRoot)
        {
            lblError.Visible = true;
            lblError.Text = GetString("general.errorvalidationerror");
        }
        else
        {
            // Get the document
            if (node != null)
            {
                // Check modify permissions
                if (CMSContext.CurrentUser.IsAuthorizedPerDocument(node, NodePermissionsEnum.Modify) == AuthorizationResultEnum.Denied)
                {
                    return;
                }

                // PATH group is displayed
                if (!this.pnlUIPath.IsHidden)
                {
                    node.NodeAlias = txtAlias.Text.Trim();

                    node.DocumentUseNamePathForUrlPath = !ctrlURL.IsCustom;
                    if (node.DocumentUseNamePathForUrlPath)
                    {
                        string urlPath = TreePathUtils.GetUrlPathFromNamePath(node.DocumentNamePath, node.NodeLevel, CMSContext.CurrentSiteName);
                        node.DocumentUrlPath = urlPath;
                    }
                    else
                    {
                        node.DocumentUrlPath = TreePathUtils.GetSafeUrlPath(ctrlURL.URLPath, CMSContext.CurrentSiteName, true);
                    }
                }

                // EXTENDED group is displayed
                if (!this.pnlUIExtended.IsHidden)
                {
                    node.DocumentUseCustomExtensions = chkCustomExtensions.Checked;
                    if (node.DocumentUseCustomExtensions)
                    {
                        node.DocumentExtensions = txtExtensions.Text;
                    }
                }

                // Save the data
                node.Update();

                // Update search index
                if ((node.PublishedVersionExists) && (SearchIndexInfoProvider.SearchEnabled))
                {
                    SearchTaskInfoProvider.CreateTask(SearchTaskTypeEnum.Update, PredefinedObjectType.DOCUMENT, SearchHelper.ID_FIELD, node.GetSearchID());
                }

                txtAlias.Text = node.NodeAlias;

                // Load the URL path
                LoadURLPath(node);

                // Log synchronization
                DocumentSynchronizationHelper.LogDocumentChange(node, TaskTypeEnum.UpdateDocument, tree);

                lblInfo.Visible = true;
                lblInfo.Text = GetString("General.ChangesSaved");

                UniGridAlias.ReloadData();
            }
        }
    }


    private void ReloadData()
    {
        if (node != null)
        {
            // Check read permissions
            if (CMSContext.CurrentUser.IsAuthorizedPerDocument(node, NodePermissionsEnum.Read) == AuthorizationResultEnum.Denied)
            {
                RedirectToAccessDenied(String.Format(GetString("cmsdesk.notauthorizedtoreaddocument"), node.NodeAliasPath));
            }
            else
            {
                // Check modify permissions
                if (CMSContext.CurrentUser.IsAuthorizedPerDocument(node, NodePermissionsEnum.Modify) == AuthorizationResultEnum.Denied)
                {
                    // show access denied message
                    lblInfo.Text = String.Format(GetString("cmsdesk.notauthorizedtoeditdocument"), node.NodeAliasPath);

                    pnlAlias.Enabled = false;
                    pnlURLPath.Enabled = false;
                    pnlExtended.Enabled = false;
                    pnlDocumentAlias.Enabled = false;
                }

                ctrlURL.IsCustom = !node.DocumentUseNamePathForUrlPath;
                chkCustomExtensions.Checked = node.DocumentUseCustomExtensions;

                txtExtensions.Text = node.DocumentExtensions;
                txtAlias.Text = node.NodeAlias;

                // Load the URL path
                LoadURLPath(node);
            }
        }
    }


    /// <summary>
    /// Loads the URL path to the UI
    /// </summary>
    private void LoadURLPath(TreeNode node)
    {
        ctrlURL.URLPath = node.DocumentUrlPath;

        txtExtensions.Enabled = chkCustomExtensions.Checked;

        if (isRoot)
        {
            txtAlias.Enabled = false;
            valAlias.Enabled = false;

            ctrlURL.Enabled = false;

            chkCustomExtensions.Enabled = false;
        }

        if (node.IsLink)
        {
            ctrlURL.Enabled = false;
        }
    }

    #endregion
}