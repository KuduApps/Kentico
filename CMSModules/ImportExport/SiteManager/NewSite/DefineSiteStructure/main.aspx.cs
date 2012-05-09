using System;

using CMS.GlobalHelper;
using CMS.TreeEngine;
using CMS.PortalEngine;
using CMS.WorkflowEngine;
using CMS.UIControls;
using CMS.CMSHelper;

using TreeNode = CMS.TreeEngine.TreeNode;

public partial class CMSModules_ImportExport_SiteManager_NewSite_DefineSiteStructure_main : SiteManagerPage
{
    #region "Variables"

    private TreeProvider tree = null;
    private TreeNode node = null;
    private string action = string.Empty;
    private string siteName = string.Empty;
    private int nodeId = 0;

    #endregion


    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        tree = new TreeProvider(CMSContext.CurrentUser);

        // Register the dialog script
        ScriptHelper.RegisterDialogScript(this);
                
        // Initialize resources
        btnInheritFromParent.Text = GetString("DefineSiteStructure.InheritFromParent");
        InheritFromParent.Value = GetString("DefineSiteStructure.inheritedPageTemplate");
        reqItemName.ErrorMessage = GetString("DefineSiteStructure.PageNameRequired");
        reqItemPageTemplate.ErrorMessage = GetString("DefineSiteStructure.PageTemplateNameRequired");

        siteName = QueryHelper.GetString("sitename", string.Empty);
        nodeId = QueryHelper.GetInteger("nodeid", 0);

        // If action not specified, edit mode
        action = QueryHelper.GetString("action", "edit");

        if (nodeId > 0)
        {
        }
        else
        {
            TreeNode rootNode = tree.SelectSingleNode(siteName, "/", TreeProvider.ALL_CULTURES);
            if (rootNode != null)
            {
                nodeId = rootNode.NodeID;
                AddScript("SetSelectedNodeId(" + nodeId + ")");
                action = "edit";
            }
        }

        bool isNewPage = false ;

        if (!RequestHelper.IsPostBack())
        {
            switch (action)
            {
                case "new":
                    // Initialize PageTitle
                    PageTitle1.TitleText = GetString("DefineSiteStructure.NewPagePropertiesCaption");
                    PageTitle1.TitleImage = GetImageUrl("CMSModules/CMS_ImportExport/PageProperties.png");
                    txtPageName.Text = "";
                    txtPageTemplate.Text = "";
                    isNewPage = true ;
                    break;

                case "edit":
                    // Initialize PageTitle
                    PageTitle1.TitleText = GetString("DefineSiteStructure.PagePropertiesCaption");
                    PageTitle1.TitleImage = GetImageUrl("CMSModules/CMS_ImportExport/PageProperties.png");

                    node = tree.SelectSingleNode(nodeId);
                    if (node != null)
                    {
                        txtPageName.Text = node.DocumentName;
                        // If page template is set
                        if (node.DocumentPageTemplateID > 0)
                        {
                            btnInheritFromParent.Enabled = true;
                            PageTemplateInfo pi = PageTemplateInfoProvider.GetPageTemplateInfo(node.DocumentPageTemplateID);
                            txtPageTemplate.Text = pi.DisplayName;
                            SelectedPageTemplateId.Value = node.DocumentPageTemplateID.ToString();
                        }
                        else
                        {
                            btnInheritFromParent.Enabled = false;
                            txtPageTemplate.Text = (node.NodeParentID == 0) ? GetString("DefineSiteStructure.noPageTemplate") : GetString("DefineSiteStructure.inheritedPageTemplate");
                            SelectedPageTemplateId.Value = "-1";
                        }
                        // If root node is selected
                        if (node.NodeParentID == 0)
                        {
                            InheritFromParent.Value = GetString("DefineSiteStructure.noPageTemplate");
                            btnInheritFromParent.Text = GetString("DefineSiteStructure.RemoveTemplate");
                            txtPageName.Enabled = false;
                            reqItemName.Enabled = false;
                        }
                    }
                    break;

                default:
                    // Initialize PageTitle
                    PageTitle1.TitleText = GetString("DefineSiteStructure.NewPagePropertiesCaption");
                    PageTitle1.TitleImage = GetImageUrl("CMSModules/CMS_ImportExport/PageProperties.png");
                    break;
            }
        }

        // Modal dialog
        btnSelectPageTemplate.Attributes.Add("onclick", "modalDialog('" + ResolveUrl("~/CMSModules/PortalEngine/UI/Layout/PageTemplateSelector.aspx?isnewpage=" + ValidationHelper.GetString(isNewPage, "") + "&nodeid=" + ValidationHelper.GetString(nodeId, "0")) + "', 'PageTemplateSelection', '90%', '85%');document.getElementById('" + btnInheritFromParent.ClientID + "').disabled=false;return false;");

    }

    #endregion


    #region "Button handling"

    protected void btnSave_Click(object sender, EventArgs e)
    {
        // Inserts new document
        if (action == "new")
        {
            try
            {
                if ((txtPageTemplate.Text != null) && (txtPageName.Text != null))
                {
                    string name = txtPageName.Text;
                    node = TreeNode.New("CMS.menuitem");
                    node.DocumentName = name;
                    int selectedPageTemplateId = Convert.ToInt32(SelectedPageTemplateId.Value);
                    if (selectedPageTemplateId == -1)
                    {
                        node.SetValue("DocumentPageTemplateID", null);
                    }
                    else
                    {
                        node.SetValue("DocumentPageTemplateID", selectedPageTemplateId);
                    }

                    node.SetValue("MenuItemName", name);

                    node.DocumentCulture = CultureHelper.GetDefaultCulture(siteName);

                    DocumentHelper.InsertDocument(node, nodeId, tree);

                    AddScript("     RefreshNode(" + node.NodeParentID + "," + node.NodeID + ");");
                    AddScript("     SelectNode(" + node.NodeID + ");");
                }
            }
            catch (Exception ex)
            {
                AddAlert(GetString("DefineSiteStructure.InsertFailed") + " : " + ex.Message);
            }
        }
        else if (action == "edit")
        {
            // Updates node
            try
            {
                node = tree.SelectSingleNode(nodeId);
                node.SetValue("DocumentName", txtPageName.Text);
                int selectedPageTemplateId = Convert.ToInt32(SelectedPageTemplateId.Value);
                if (selectedPageTemplateId == -1)
                {
                    node.SetValue("DocumentPageTemplateID", null);
                }
                else
                {
                    node.SetValue("DocumentPageTemplateID", selectedPageTemplateId);
                }

                DocumentHelper.UpdateDocument(node, tree);

                AddScript("     RefreshNode(" + node.NodeParentID + "," + node.NodeID + ");");
                AddScript("     SelectNode(" + node.NodeID + ");");
            }
            catch (Exception ex)
            {
                AddAlert(GetString("DefineSiteStructure.UpdateFailed") + " : " + ex.Message);
            }
        }
    }

    #endregion


    #region "Other methods"

    /// <summary>
    /// Adds the alert message to the output request window.
    /// </summary>
    /// <param name="message">Message to display</param>
    private void AddAlert(string message)
    {
        ltlScript.Text += ScriptHelper.GetAlertScript(message);
    }


    /// <summary>
    /// Adds the script to the output request window.
    /// </summary>
    /// <param name="script">Script to add</param>
    public override void AddScript(string script)
    {
        ltlScript.Text += ScriptHelper.GetScript(script);
    }

    #endregion
}
