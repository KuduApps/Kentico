using System;
using System.Web.UI;

using CMS.CMSHelper;
using CMS.ExtendedControls;
using CMS.FormEngine;
using CMS.GlobalHelper;
using CMS.LicenseProvider;
using CMS.PortalEngine;
using CMS.SettingsProvider;
using CMS.SiteProvider;
using CMS.TreeEngine;
using CMS.UIControls;
using CMS.WorkflowEngine;

public partial class CMSModules_Content_CMSDesk_New_NewPage : CMSContentPage, IPostBackEventHandler
{
    #region "Variables"

    int parentNodeId = 0;


    private const string pageClassName = "CMS.MenuItem";

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Register script files
        ScriptHelper.RegisterShortcuts(this);
        ScriptHelper.RegisterSpellChecker(this);

        ltlScript.Text = GetSpellCheckDialog();

        parentNodeId = QueryHelper.GetInteger("nodeid", 0);
        txtPageName.MaxLength = TreePathUtils.MaxNameLength;

        TreeProvider tp = new TreeProvider(CMSContext.CurrentUser);
        // For new node is not document culture important, preffered culture is used
        TreeNode node = tp.SelectSingleNode(parentNodeId);
        if (node != null)
        {
            selTemplate.DocumentID = node.DocumentID;
            selTemplate.ParentNodeID = parentNodeId;
        }

        // Register progress script
        ScriptHelper.RegisterProgress(Page);

        // Check permission to create page with redirect
        CheckSecurity(true);

        if (!LicenseHelper.LicenseVersionCheck(URLHelper.GetCurrentDomain(), FeatureEnum.Documents, VersionActionEnum.Insert))
        {
            RedirectToAccessDenied(String.Format(GetString("cmsdesk.documentslicenselimits"), ""));
        }

        // Hide error label
        lblError.Style.Add("display", "none");

        string jsValidation = "function ValidateNewPage(){" +
        " var value = document.getElementById('" + txtPageName.ClientID + "').value;" +
        " value = value.replace(/^\\s+|\\s+$/g, '');" +
        " var errorLabel = document.getElementById('" + lblError.ClientID + "'); " +
        " if (value == '') {" +
        " errorLabel.style.display = ''; errorLabel.innerHTML  = " + ScriptHelper.GetString(GetString("newpage.nameempty")) + "; resizearea(); return false;}";

        jsValidation += selTemplate.GetValidationScript();

        jsValidation += " return true;}";

        // Register validate script
        ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "ValidateNewPage", ScriptHelper.GetScript(jsValidation));

        // Register save document script
        ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "SaveDocument",
            ScriptHelper.GetScript("function SaveDocument(nodeId, createAnother) {if (ValidateNewPage()) { " + ControlsHelper.GetPostBackEventReference(this, "#", false).Replace("'#'", "createAnother+''") + "; return false; }}"));

        // Set default focus on page name field
        if (!RequestHelper.IsPostBack())
        {
            txtPageName.Focus();
        }
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        // For blank page and page template selector - focus page name text box to proper ctrl+c function.
        if ((selTemplate.TemplateSelectionState == 1) || (selTemplate.TemplateSelectionState == 3))
        {
            txtPageName.Focus();
        }

        // Register the scripts
        ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "SpellCheck_" + ClientID, ltlScript.Text);
    }


    /// <summary>
    /// Check the permission to create page(menuitem) under node.
    /// </summary>
    /// <param name="redirect">If true and user doesn't have permission, user is redirected to access denied page</param>
    /// <returns>True if user has permission, otherwise false</returns>
    private bool CheckSecurity(bool redirect)
    {
        // Check if user is allowed to create page under this node
        if (!CMSContext.CurrentUser.IsAuthorizedToCreateNewDocument(parentNodeId, pageClassName))
        {
            // Redirect if enabled
            if (redirect)
            {
                RedirectToAccessDenied(GetString("cmsdesk.notauthorizedtocreatedocument"));
            }
            return false;
        }

        return true;
    }
    

    /// <summary>
    /// Creates new page(CMS.MenuItem) and assignes selected template.
    /// </summary>
    protected void Save(bool createAnother)
    {
        // Check security
        CheckSecurity(true);

        string newPageName = txtPageName.Text.Trim();

        string errorMessage = null;

        if (!String.IsNullOrEmpty(newPageName))
        {
            // Limit length to 100 characters
            newPageName = TextHelper.LimitLength(newPageName, TreePathUtils.MaxNameLength, String.Empty);
        }
        else
        {
            errorMessage = GetString("newpage.nameempty");
        }

        if (parentNodeId == 0)
        {
            errorMessage = GetString("newpage.invalidparentnode");
        }

        // If error, show error message and return
        if (String.IsNullOrEmpty(errorMessage))
        {
            TreeProvider tree = new TreeProvider(CMSContext.CurrentUser);
            TreeNode node = TreeNode.New("CMS.MenuItem", tree);

            // Load default data
            FormHelper.LoadDefaultValues(node);

            node.DocumentName = newPageName;
            lblError.Style.Remove("display");

            node.DocumentPageTemplateID = selTemplate.EnsureTemplate(node.DocumentName, ref errorMessage);

            // Switch to design mode (the template is empty at this moment)
            if (selTemplate.NewTemplateIsEmpty && !createAnother)
            {
                CMSContext.ViewMode = ViewModeEnum.Design;
            }

            // Insert node if no error
            if (String.IsNullOrEmpty(errorMessage))
            {
                node.DocumentCulture = CMSContext.PreferredCultureCode;

                // Insert the document
                DocumentHelper.InsertDocument(node, parentNodeId, tree);
                //node.Insert(parentNodeId);

                // Create default SKU if configured
                if (ModuleEntry.CheckModuleLicense(ModuleEntry.ECOMMERCE, URLHelper.GetCurrentDomain(), FeatureEnum.Ecommerce, VersionActionEnum.Insert))
                {
                    bool? skuCreated = node.CreateDefaultSKU();
                    if (skuCreated.HasValue && !skuCreated.Value)
                    {
                        lblError.Text = GetString("com.CreateDefaultSKU.Error");
                    }
                }

                if (node.NodeSKUID > 0)
                {
                    DocumentHelper.UpdateDocument(node, tree);
                }

                // Automatically publish
                // Check if allowed 'Automatically publish changes'
                WorkflowManager wm = new WorkflowManager(tree);
                WorkflowInfo wi = wm.GetNodeWorkflow(node);
                if ((wi != null) && wi.WorkflowAutoPublishChanges && !wi.UseCheckInCheckOut(CMSContext.CurrentSiteName))
                {
                    wm.AutomaticallyPublish(node, wi, null);
                }

                // Process create another flag
                string script = null;
                if (createAnother)
                {
                    script = ScriptHelper.GetScript("parent.PassiveRefresh(" + node.NodeParentID + ", " + node.NodeParentID + "); parent.CreateAnother();");
                }
                else
                {
                    script = ScriptHelper.GetScript("parent.RefreshTree(" + node.NodeID + ", " + node.NodeID + ");");
                    script += ScriptHelper.GetScript("parent.SelectNode(" + node.NodeID + ");");
                }

                ScriptHelper.RegisterClientScriptBlock(Page, typeof(string), "Refresh", script);
            }
        }

        // Insert node if no error
        if (!String.IsNullOrEmpty(errorMessage))
        {
            lblError.Text = errorMessage;
            lblError.Visible = true;
            return;
        }
    }

    #endregion


    #region "IPostBackEventHandler members"

    /// <summary>
    /// Raises event postback event.
    /// </summary>
    /// <param name="eventArgument"></param>
    public void RaisePostBackEvent(string eventArgument)
    {
        bool createAnother = ValidationHelper.GetBoolean(eventArgument, false);

        // Create document
        Save(createAnother);
    }

    #endregion
}