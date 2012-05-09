using System;
using System.Web;
using System.Web.UI;

using CMS.CMSHelper;
using CMS.FormEngine;
using CMS.GlobalHelper;
using CMS.LicenseProvider;
using CMS.PortalEngine;
using CMS.SettingsProvider;
using CMS.SiteProvider;
using CMS.TreeEngine;
using CMS.UIControls;
using CMS.WorkflowEngine;

public partial class CMSModules_Content_CMSDesk_Edit_Edit : CMSContentPage
{
    #region "Protected variables"

    protected int nodeId = 0;
    protected int templateId = 0;
    protected int classId = 0;
    protected bool newdocument = false;
    protected bool newculture = false;
    protected bool mShowToolbar = false;
    protected bool confirmChanges = false;

    protected DataClassInfo ci = null;
    protected INewProductControl ucNewProduct = null;
    protected TreeNode node = null;
    protected TreeProvider mTreeProvider = null;
    private WorkflowManager mWorkflowManager = null;
    private VersionManager mVersionManager = null;
    private WorkflowInfo wi = null;
    private bool displaySplitMode = CMSContext.DisplaySplitMode;

    #endregion


    #region "Protected properties"

    /// <summary>
    /// Indicates if check-in/check-out functionality is automatic
    /// </summary>
    protected bool AutoCheck
    {
        get
        {
            if (node != null)
            {
                // Get workflow info
                wi = WorkflowManager.GetNodeWorkflow(node);

                // Check if the document uses workflow
                if (wi != null)
                {
                    return !wi.UseCheckInCheckOut(CMSContext.CurrentSiteName);
                }
            }
            return false;
        }
    }


    /// <summary>
    /// Gets Workflow manager instance.
    /// </summary>
    protected WorkflowManager WorkflowManager
    {
        get
        {
            return mWorkflowManager ?? (mWorkflowManager = new WorkflowManager(TreeProvider));
        }
    }


    /// <summary>
    /// Gets Version manager instance.
    /// </summary>
    protected VersionManager VersionManager
    {
        get
        {
            return mVersionManager ?? (mVersionManager = new VersionManager(TreeProvider));
        }
    }


    /// <summary>
    /// Tree provider instance.
    /// </summary>
    protected TreeProvider TreeProvider
    {
        get
        {
            return mTreeProvider ?? (mTreeProvider = new TreeProvider(CMSContext.CurrentUser));
        }
    }

    #endregion


    #region "Methods"

    /// <summary>
    /// Returns true if the changes should be saved.
    /// </summary>
    public bool SaveChanges
    {
        get
        {
            return ValidationHelper.GetBoolean(HttpContext.Current.Request.Params["saveChanges"], false);
        }
    }


    protected override void OnInit(EventArgs e)
    {
        ManagersContainer = plcManagers;

        CurrentUserInfo currentUser = CMSContext.CurrentUser;

        // Check UIProfile
        if (!currentUser.IsAuthorizedPerUIElement("CMS.Content", "EditForm"))
        {
            RedirectToCMSDeskUIElementAccessDenied("CMS.Content", "EditForm");
        }

        base.OnInit(e);

        ScriptHelper.RegisterStartupScript(this, typeof(string), "spellCheckDialog", GetSpellCheckDialog());

        // Register scripts
        string script = "function " + formElem.ClientID + "_RefreshForm(){" + Page.ClientScript.GetPostBackEventReference(btnRefresh, "") + " }";
        ScriptHelper.RegisterClientScriptBlock(this, typeof(string), formElem.ClientID + "_RefreshForm", ScriptHelper.GetScript(script));

        ScriptHelper.RegisterCompletePageScript(this);
        ScriptHelper.RegisterProgress(this);
        ScriptHelper.RegisterDialogScript(this);

        confirmChanges = SettingsKeyProvider.GetBoolValue(CMSContext.CurrentSiteName + ".CMSConfirmChanges");

        formElem.TreeProvider = TreeProvider;
        formElem.OnAfterDataLoad += formElem_OnAfterDataLoad;

        // Current nodeID
        nodeId = QueryHelper.GetInteger("nodeid", 0);

        // ClassID, used when creating new document                
        classId = QueryHelper.GetInteger("classid", 0);

        // TemplateID, used when Use template selection is enabled in actual class
        templateId = QueryHelper.GetInteger("templateid", 0);


        // Analyze the action parameter
        switch (QueryHelper.GetString("action", "").ToLower())
        {
            case "new":
                // Check if document type is allowed under parent node
                if ((nodeId > 0) && (classId > 0))
                {
                    // Get the node                    
                    TreeNode treeNode = TreeProvider.SelectSingleNode(nodeId);
                    DataClassInfo dci = DataClassInfoProvider.GetDataClass(classId);
                    if ((treeNode == null) || (dci == null))
                    {
                        throw new Exception("[Content.Edit]: Given node or document class cannot be found!");
                    }

                    // Check allowed document type
                    if (!DataClassInfoProvider.IsChildClassAllowed(ValidationHelper.GetInteger(treeNode.GetValue("NodeClassID"), 0), classId))
                    {
                        AddNotAllowedScript("child");
                    }

                    if (!currentUser.IsAuthorizedToCreateNewDocument(treeNode, dci.ClassName))
                    {
                        AddNotAllowedScript("new");
                    }
                }

                newdocument = true;
                break;

            case "newculture":
                // Check permissions
                bool authorized = false;
                if (nodeId > 0)
                {
                    // Get the node                    
                    TreeNode treeNode = TreeProvider.SelectSingleNode(nodeId);
                    if (treeNode != null)
                    {
                        authorized = currentUser.IsAuthorizedToCreateNewDocument(treeNode.NodeParentID, treeNode.NodeClassName);
                    }
                }

                if (!authorized)
                {
                    AddNotAllowedScript("newculture");
                }

                newculture = true;
                break;
        }

        // Get the node
        if (newculture || newdocument)
        {
            node = DocumentHelper.GetDocument(nodeId, TreeProvider.ALL_CULTURES, TreeProvider);
            if (newculture)
            {
                DocumentHelper.ClearWorkflowInformation(node);
            }
        }
        else
        {
            node = DocumentHelper.GetDocument(nodeId, CMSContext.PreferredCultureCode, TreeProvider);
            if ((node == null) && displaySplitMode)
            {
                URLHelper.Redirect("~/CMSModules/Content/CMSDesk/New/NewCultureVersion.aspx" + URLHelper.Url.Query);
            }
        }

        // Set edited document
        EditedDocument = node;

        if (!confirmChanges)
        {
            AddScript("confirmChanges = false;");
        }

        // If node found, init the form
        if (node != null)
        {
            // CMSForm initialization
            formElem.NodeId = node.NodeID;
            formElem.CultureCode = CMSContext.PreferredCultureCode;

            // Set the form mode
            if (newdocument)
            {
                ci = DataClassInfoProvider.GetDataClass(classId);
                if (ci == null)
                {
                    throw new Exception("[Content/Edit.aspx]: Class ID '" + classId + "' not found.");
                }

                if (ci.ClassName.ToLower() == "cms.blog")
                {
                    if (!LicenseHelper.LicenseVersionCheck(URLHelper.GetCurrentDomain(), FeatureEnum.Blogs, VersionActionEnum.Insert))
                    {
                        RedirectToAccessDenied(String.Format(GetString("cmsdesk.bloglicenselimits"), ""));
                    }
                }

                if (!LicenseHelper.LicenseVersionCheck(URLHelper.GetCurrentDomain(), FeatureEnum.Documents, VersionActionEnum.Insert))
                {
                    RedirectToAccessDenied(String.Format(GetString("cmsdesk.documentslicenselimits"), ""));
                }

                // Check if need template selection, if so, then redirect to template selection page
                if ((ci.ClassShowTemplateSelection) && (templateId == 0) && (ci.ClassName.ToLower() != "cms.menuitem"))
                {
                    URLHelper.Redirect("~/CMSModules/Content/CMSDesk/TemplateSelection.aspx" + URLHelper.Url.Query);
                }

                // Set default template ID
                formElem.DefaultPageTemplateID = templateId > 0 ? templateId : ci.ClassDefaultPageTemplateID;

                formElem.FormMode = FormModeEnum.Insert;
                string newClassName = ci.ClassName;
                formElem.FormName = newClassName + ".default";
            }
            else if (newculture)
            {
                if (node.NodeClassName.ToLower() == "cms.blog")
                {
                    if (!LicenseHelper.LicenseVersionCheck(URLHelper.GetCurrentDomain(), FeatureEnum.Blogs, VersionActionEnum.Insert))
                    {
                        RedirectToAccessDenied(String.Format(GetString("cmsdesk.bloglicenselimits"), ""));
                    }
                }

                if (!LicenseHelper.LicenseVersionCheck(URLHelper.GetCurrentDomain(), FeatureEnum.Documents, VersionActionEnum.Insert))
                {
                    RedirectToAccessDenied(String.Format(GetString("cmsdesk.documentslicenselimits"), ""));
                }

                formElem.FormMode = FormModeEnum.InsertNewCultureVersion;
                // Default data document ID
                formElem.CopyDefaultDataFromDocumentId = ValidationHelper.GetInteger(Request.QueryString["sourcedocumentid"], 0);

                ci = DataClassInfoProvider.GetDataClass(node.NodeClassName);
                formElem.FormName = node.NodeClassName + ".default";
            }
            else
            {
                formElem.FormMode = FormModeEnum.Update;
                ci = DataClassInfoProvider.GetDataClass(node.NodeClassName);
            }
            formElem.Visible = true;

            // Display / hide the CK editor toolbar area
            FormInfo fi = FormHelper.GetFormInfo(ci.ClassName, false);

            if (fi.UsesHtmlArea())
            {
                // Add script to display toolbar
                if (formElem.HtmlAreaToolbarLocation.ToLower() == "out:cktoolbar")
                {
                    mShowToolbar = true;
                }
            }

            ReloadForm();
        }

        AddScript(
            "function SaveDocument(nodeId, createAnother) { document.getElementById('hidAnother').value = createAnother; " + (confirmChanges ? "AllowSubmit(); " : "") + ClientScript.GetPostBackEventReference(btnSave, null) + "; } \n" +
            "function Approve(nodeId) { SubmitAction(); " + ClientScript.GetPostBackEventReference(btnApprove, null) + "; } \n" +
            "function Reject(nodeId) { SubmitAction(); " + ClientScript.GetPostBackEventReference(btnReject, null) + "; } \n" +
            "function CheckIn(nodeId) { SubmitAction(); " + ClientScript.GetPostBackEventReference(btnCheckIn, null) + "; } \n" +
            (confirmChanges ? "var confirmLeave='" + GetString("Content.ConfirmLeave") + "'; \n" : "")
            );
    }


    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);

        // Register script files
        ScriptHelper.RegisterSpellChecker(this);
        ScriptHelper.RegisterScriptFile(this, "cmsedit.js");
        ScriptHelper.RegisterScriptFile(this, "~/CMSModules/Content/CMSDesk/Edit/Edit.js");
        ScriptHelper.RegisterSaveChanges(this);
        ScriptHelper.RegisterTooltip(this);

        // Register js synchronization script for split mode
        if (!newculture && !newdocument && displaySplitMode)
        {
            RegisterSplitModeSync(true, false);
        }

        formElem.lblInfo.Text = "";

        if (newdocument && SettingsKeyProvider.GetBoolValue(CMSContext.CurrentSiteName + ".ProductTabEnabled"))
        {
            InitializeProductControls();
        }
        else
        {
            // Hide all product controls
            plcNewProduct.Visible = false;
        }
    }


    private void ReloadForm()
    {
        lblWorkflowInfo.Text = "";

        if ((node != null) && !newdocument && !newculture)
        {
            // Check read permissions
            if (CMSContext.CurrentUser.IsAuthorizedPerDocument(node, NodePermissionsEnum.Read) == AuthorizationResultEnum.Denied)
            {
                RedirectToAccessDenied(String.Format(GetString("cmsdesk.notauthorizedtoreaddocument"), node.NodeAliasPath));
            }
            // Check modify permissions
            else if (CMSContext.CurrentUser.IsAuthorizedPerDocument(node, NodePermissionsEnum.Modify) == AuthorizationResultEnum.Denied)
            {
                formElem.Enabled = false;
                lblWorkflowInfo.Text = String.Format(GetString("cmsdesk.notauthorizedtoeditdocument"), node.NodeAliasPath);
            }
            else
            {
                // Setup the workflow information
                wi = WorkflowManager.GetNodeWorkflow(node);
                if ((wi != null) && (!newculture))
                {
                    // Get current step info, do not update document
                    WorkflowStepInfo si = null;
                    if (node.IsPublished && (node.DocumentCheckedOutVersionHistoryID <= 0))
                    {
                        si = WorkflowStepInfoProvider.GetWorkflowStepInfo("published", wi.WorkflowID);
                    }
                    else
                    {
                        si = WorkflowManager.GetStepInfo(node, false) ??
                             WorkflowManager.GetFirstWorkflowStep(node, wi);
                    }

                    bool canApprove = WorkflowManager.CanUserApprove(node, CMSContext.CurrentUser);
                    string stepName = si.StepName.ToLower();
                    if (!(canApprove || (stepName == "edit") || (stepName == "published") || (stepName == "archived")))
                    {
                        formElem.Enabled = false;
                    }

                    bool useCheckInCheckOut = wi.UseCheckInCheckOut(CMSContext.CurrentSiteName);
                    if (!node.IsCheckedOut)
                    {
                        // Check-in, Check-out
                        if (useCheckInCheckOut)
                        {
                            // If not checked out, add the check-out information
                            if (canApprove || (stepName == "edit") || (stepName == "published") || (stepName == "archived"))
                            {
                                lblWorkflowInfo.Text = GetString("EditContent.DocumentCheckedIn");
                            }
                            formElem.Enabled = newculture;
                        }
                    }
                    else
                    {
                        // If checked out by current user, add the check-in button
                        int checkedOutBy = node.DocumentCheckedOutByUserID;
                        if (checkedOutBy == CMSContext.CurrentUser.UserID)
                        {
                            // Document is checked out
                            lblWorkflowInfo.Text = GetString("EditContent.DocumentCheckedOut");
                        }
                        else
                        {
                            // Checked out by somebody else
                            string userName = UserInfoProvider.GetUserNameById(checkedOutBy);
                            lblWorkflowInfo.Text = String.Format(GetString("EditContent.DocumentCheckedOutByAnother"), userName);
                            formElem.Enabled = newculture;
                        }
                    }

                    // Document approval
                    switch (stepName)
                    {
                        case "edit":
                        case "published":
                        case "archived":
                            break;

                        default:
                            // If the user is authorized to perform the step, display the approve and reject buttons
                            if (!canApprove)
                            {
                                lblWorkflowInfo.Text += " " + GetString("EditContent.NotAuthorizedToApprove");
                            }
                            break;
                    }
                    // If workflow isn't auto publish or step name isn't 'published' or 'check-in/check-out' is allowed then show current step name
                    if (!wi.WorkflowAutoPublishChanges || (stepName != "published") || useCheckInCheckOut)
                    {
                        lblWorkflowInfo.Text += " " + String.Format(GetString("EditContent.CurrentStepInfo"), HTMLHelper.HTMLEncode(ResHelper.LocalizeString(si.StepDisplayName)));
                    }
                }
            }
        }
        pnlWorkflowInfo.Visible = (lblWorkflowInfo.Text != "");
    }


    /// <summary>
    /// Adds the alert message to the output request window.
    /// </summary>
    /// <param name="message">Message to display</param>
    private void AddAlert(string message)
    {
        ScriptHelper.RegisterStartupScript(this, typeof(string), message.GetHashCode().ToString(), ScriptHelper.GetAlertScript(message));
    }


    /// <summary>
    /// Adds script for redirecting to NotAllowed page.
    /// </summary>
    /// <param name="action">Action string</param>
    private void AddNotAllowedScript(string action)
    {
        AddScript("window.parent.parent.location.replace('../NotAllowed.aspx?action=" + action + "')");
    }


    /// <summary>
    /// Adds the script to the output request window.
    /// </summary>
    /// <param name="script">Script to add</param>
    public override void AddScript(string script)
    {
        ScriptHelper.RegisterStartupScript(this, typeof(string), script.GetHashCode().ToString(), ScriptHelper.GetScript(script));
    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        SaveDocument(false);
    }


    /// <summary>
    /// Save new or existing document.
    /// </summary>
    /// <param name="forceRefreshTree">Indicates if content tree should be refreshed</param>
    private void SaveDocument(bool forceRefreshTree)
    {
        if (nodeId > 0)
        {
            // Validate the form first
            if (formElem.BasicForm.ValidateData())
            {
                bool createAnother = newdocument && ValidationHelper.GetBoolean(Request.Params["hidAnother"], false);

                if (!newdocument && !newculture)
                {
                    // Get the document
                    node = DocumentHelper.GetDocument(nodeId, CMSContext.PreferredCultureCode, TreeProvider);

                    // Check modify permissions
                    if (CMSContext.CurrentUser.IsAuthorizedPerDocument(node, NodePermissionsEnum.Modify) == AuthorizationResultEnum.Denied)
                    {
                        formElem.Enabled = false;
                        lblWorkflowInfo.Text = String.Format(GetString("cmsdesk.notauthorizedtoeditdocument"), node.NodeAliasPath);
                        PassiveRefresh(nodeId, node.NodeParentID, node.DocumentName);
                        return;
                    }
                }

                try
                {
                    // Backup original document's values - before saving
                    string originalDocumentName = node.DocumentName;
                    DateTime originalPublishFrom = node.DocumentPublishFrom;
                    DateTime originalPublishTo = node.DocumentPublishTo;
                    bool wasArchived = node.IsArchived;
                    bool wasInPublishedStep = node.IsInPublishStep;

                    // If not using check-in/check-out, check out automatically (not for new document and new culture version)
                    if (!newdocument && !newculture && AutoCheck)
                    {
                        if (node != null)
                        {
                            // Check out
                            VersionManager.CheckOut(node, node.IsPublished, true);
                        }
                    }

                    // Tree node is returned from CMSForm.Save method
                    if ((node != null) && !newdocument && !newculture)
                    {
                        formElem.Save(node);
                    }
                    else
                    {
                        // Document was not saved yet
                        node = null;

                        // Product should be created -> save document only when product data are valid              
                        if ((chkCreateProduct.Checked) && (ucNewProduct != null))
                        {
                            if (ucNewProduct.ValidateData())
                            {
                                node = formElem.Save();
                            }
                        }
                        // Product should not be created -> save document
                        else
                        {
                            node = formElem.Save();
                        }
                    }

                    if (node != null)
                    {
                        // Create product only if the doc. type can be product
                        if ((newdocument) && (chkCreateProduct.Checked) && (ucNewProduct != null) && (ucNewProduct.ClassObj.ClassIsProduct))
                        {
                            // Create product
                            ucNewProduct.Node = node;

                            GeneralizedInfo skuObj = ucNewProduct.SaveData();
                            if (skuObj != null)
                            {
                                // Asssign new product to the document
                                node.NodeSKUID = skuObj.ObjectID;
                                DocumentHelper.UpdateDocument(node, TreeProvider, true);
                            }
                        }

                        // If not using check-in/check-out 
                        if (AutoCheck)
                        {
                            // Check in the document (not for new document and new culture version)
                            if (!newdocument && !newculture)
                            {
                                VersionManager.CheckIn(node, null, null);
                            }
                            else
                            {
                                // Automatically publish
                                // Check if allowed 'Automatically publish changes'
                                WorkflowInfo wi = WorkflowManager.GetNodeWorkflow(node);
                                if (wi.WorkflowAutoPublishChanges)
                                {
                                    WorkflowManager.AutomaticallyPublish(node, wi, null);
                                }
                            }
                        }

                        int newNodeId = node.NodeID;
                        if (newdocument || newculture)
                        {
                            // Store error text
                            if (!string.IsNullOrEmpty(formElem.lblError.Text))
                            {
                                SessionHelper.SetValue("FormErrorText|" + newNodeId, formElem.lblError.Text);
                            }

                            if (createAnother)
                            {
                                PassiveRefresh(node.NodeParentID, node.NodeParentID, "");
                                AddScript("CreateAnother();");
                            }
                            else
                            {
                                // Refresh frame in split mode
                                if (newculture && displaySplitMode && (CultureHelper.GetOriginalPreferredCulture() != node.DocumentCulture))
                                {
                                    AddScript("SplitModeRefreshFrame();");
                                }
                                else
                                {
                                    // Document tree is refreshed and new document is displayed
                                    AddScript("RefreshTree(" + newNodeId + "," + newNodeId + "); SelectNode(" + newNodeId + ");");
                                }
                            }
                        }
                        else
                        {
                            // Refresh content tree and frames if 'forceRefreshTree' is True or document was changed
                            if (forceRefreshTree || ((originalDocumentName != node.DocumentName) || (wasInPublishedStep != node.IsInPublishStep) || (wasArchived != node.IsArchived)
                                || (((originalPublishFrom != node.DocumentPublishFrom) || (originalPublishTo != node.DocumentPublishTo))
                                && (AutoCheck || (WorkflowManager.GetNodeWorkflow(node) == null)))))
                            {
                                // Refresh content tree and frames
                                PassiveRefresh(nodeId, node.NodeParentID, node.DocumentName);
                            }
                            else
                            {
                                // Refresh frames
                                FramesRefresh(false, nodeId);
                            }

                            // Reload the form
                            ReloadForm();
                            string oldInfo = formElem.lblInfo.Text;
                            formElem.LoadForm(false);
                            formElem.lblInfo.Text = oldInfo;
                            formElem.lblInfo.Text += "<br />";
                        }

                        // If not menuitem type, switch to form mode to keep editing the form
                        if (!TreePathUtils.IsMenuItemType(node.NodeClassName))
                        {
                            CMSContext.ViewMode = ViewModeEnum.EditForm;
                        }
                    }
                }
                catch (Exception ex)
                {
                    AddAlert(GetString("ContentRequest.SaveFailed") + " : " + ex.Message);
                }
            }
        }
    }


    protected void btnApprove_Click(object sender, EventArgs e)
    {
        if (node != null)
        {
            // Check modify permissions
            if (CMSContext.CurrentUser.IsAuthorizedPerDocument(node, NodePermissionsEnum.Modify) == AuthorizationResultEnum.Denied)
            {
                formElem.Enabled = false;
                lblWorkflowInfo.Text = String.Format(GetString("cmsdesk.notauthorizedtoeditdocument"), node.NodeAliasPath);
                return;
            }

            // Validate the form first
            if (formElem.BasicForm.ValidateData())
            {
                if (AutoCheck)
                {
                    SaveDocument(false);
                }

                // Approve the document - Go to next workflow step
                // Get original step
                WorkflowStepInfo originalStep = WorkflowManager.GetStepInfo(node);

                // Approve the document
                WorkflowStepInfo nextStep = WorkflowManager.MoveToNextStep(node, "");

                if (nextStep != null)
                {
                    string nextStepName = nextStep.StepName.ToLower();
                    string originalStepName = originalStep.StepName.ToLower();

                    bool published = (nextStepName == "published");
                    if (published)
                    {
                        formElem.LoadForm(false);
                    }

                    // Send workflow e-mails
                    if (SettingsKeyProvider.GetBoolValue(CMSContext.CurrentSiteName + ".CMSSendWorkflowEmails"))
                    {
                        WorkflowManager.SendWorkflowEmails(node, CMSContext.CurrentUser, originalStep, nextStep, published ? WorkflowActionEnum.Published : WorkflowActionEnum.Approved, "");
                    }

                    // Document wasn't changed
                    if (!SaveChanges)
                    {
                        // Refresh content tree and frames if document is in publish step
                        if (published)
                        {
                            PassiveRefresh(nodeId, node.NodeParentID, node.DocumentName);
                        }
                        else
                        {
                            FramesRefresh(false, node.NodeID);
                        }
                    }

                    // Ensure correct message is displayed
                    if (published)
                    {
                        formElem.lblInfo.Text += " " + GetString("workflowstep.customtopublished");
                    }
                    else if (originalStepName == "edit" && nextStepName != "published")
                    {
                        formElem.lblInfo.Text += " " + GetString("workflowstep.edittocustom");
                    }
                    else if (originalStepName != "edit" && nextStepName != "published" && nextStepName != "archived")
                    {
                        formElem.lblInfo.Text += " " + GetString("workflowstep.customtocustom");
                    }
                    else
                    {
                        formElem.lblInfo.Text += " " + GetString("ContentEdit.WasApproved");
                    }
                }
                else
                {
                    // Workflow has been removed
                    PassiveRefresh(nodeId, node.NodeParentID, node.DocumentName);
                    formElem.lblInfo.Text += " " + GetString("ContentEdit.WasApproved");
                }
            }
        }

        ReloadForm();
    }


    protected void btnReject_Click(object sender, EventArgs e)
    {
        // Reject the document - Go to previous workflow step
        if (node != null)
        {
            // Check modify permissions
            if (CMSContext.CurrentUser.IsAuthorizedPerDocument(node, NodePermissionsEnum.Modify) == AuthorizationResultEnum.Denied)
            {
                formElem.Enabled = false;
                lblWorkflowInfo.Text = String.Format(GetString("cmsdesk.notauthorizedtoeditdocument"), node.NodeAliasPath);
                return;
            }

            // Validate the form first
            if (formElem.BasicForm.ValidateData())
            {
                bool passiveRefresh = true;

                // Save the document first
                if (AutoCheck)
                {
                    if (SaveChanges)
                    {
                        SaveDocument(true);
                        passiveRefresh = false;
                    }
                    else
                    {
                        formElem.BasicForm.LoadControlValues();
                    }
                }

                if (passiveRefresh)
                {
                    PassiveRefresh(nodeId, node.NodeParentID, node.DocumentName);
                }

                // Get original step
                WorkflowStepInfo originalStep = WorkflowManager.GetStepInfo(node);

                // Reject the document
                WorkflowStepInfo previousStep = WorkflowManager.MoveToPreviousStep(node, "");

                if (previousStep != null)
                {
                    // Send workflow e-mails
                    if (SettingsKeyProvider.GetBoolValue(CMSContext.CurrentSiteName + ".CMSSendWorkflowEmails"))
                    {
                        WorkflowManager.SendWorkflowEmails(node, CMSContext.CurrentUser, originalStep, previousStep, WorkflowActionEnum.Rejected, "");
                    }
                }
                else
                {
                    // Workflow has been removed
                    AddScript("SelectNode(" + nodeId + ");");
                }

                formElem.lblInfo.Text += " " + GetString("ContentEdit.WasRejected");
            }
        }
        ReloadForm();
    }


    protected void btnCheckIn_Click(object sender, EventArgs e)
    {
        try
        {
            // Check in the document
            if (node != null)
            {
                // Check modify permissions
                if (CMSContext.CurrentUser.IsAuthorizedPerDocument(node, NodePermissionsEnum.Modify) == AuthorizationResultEnum.Denied)
                {
                    formElem.Enabled = false;
                    lblWorkflowInfo.Text = String.Format(GetString("cmsdesk.notauthorizedtoeditdocument"), node.NodeAliasPath);
                    return;
                }

                // Validate the form first
                if (formElem.BasicForm.ValidateData())
                {
                    // Save the document first
                    if (SaveChanges)
                    {
                        SaveDocument(true);
                    }
                    else
                    {
                        formElem.BasicForm.LoadControlValues();
                        PassiveRefresh(nodeId, node.NodeParentID, node.DocumentName);
                    }

                    VersionManager verMan = new VersionManager(TreeProvider);

                    // Check in the document        
                    verMan.CheckIn(node, null, null);

                    formElem.lblInfo.Text += " " + GetString("ContentEdit.WasCheckedIn");
                }
            }
        }
        catch (WorkflowException)
        {
            formElem.lblError.Text += GetString("EditContent.DocumentCannotCheckIn");
        }
        catch (Exception ex)
        {
            formElem.lblError.Text += ex.Message;
        }

        ReloadForm();
    }


    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        // Check permission to modify document
        if (CMSContext.CurrentUser.IsAuthorizedPerDocument(node, NodePermissionsEnum.Modify) == AuthorizationResultEnum.Allowed)
        {
            // Get curent step
            WorkflowStepInfo currentStep = WorkflowManager.GetStepInfo(node);
            string currentStepName = currentStep.StepName.ToLower();
            bool wasArchived = currentStepName == "archived";

            // Move to edit step
            DocumentHelper.MoveDocumentToEditStep(node, node.TreeProvider);

            // Refresh frames and tree
            FramesRefresh(wasArchived, node.NodeID);

            // Reload form
            ReloadForm();
            if (SaveChanges)
            {
                ScriptHelper.RegisterStartupScript(this, typeof(string), "moveToEditStepChange", ScriptHelper.GetScript("Changed();"));
            }
        }
    }


    protected void formElem_OnAfterDataLoad(object sender, EventArgs e)
    {
        if (node != null)
        {
            // Show stored error message
            string frmError = SessionHelper.GetValue("FormErrorText|" + node.NodeID) as string;
            if (!string.IsNullOrEmpty(frmError))
            {
                formElem.lblError.Text = frmError;
                // Remove error message
                SessionHelper.Remove("FormErrorText|" + node.NodeID);
            }
        }
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        ScriptHelper.RegisterStartupScript(this, typeof(string), "InitializePage", ScriptHelper.GetScript("InitializePage()"));

        if (formElem.Enabled)
        {
            // Add the shortcuts script
            ScriptHelper.RegisterShortcuts(this);
        }
    }


    private void PassiveRefresh(int nodeId, int parentNodeId, string newName)
    {
        ScriptHelper.RegisterStartupScript(this, typeof(string), "passiveRefresh_" + nodeId + "_" + parentNodeId, ScriptHelper.GetScript("PassiveRefresh(" + nodeId + "," + parentNodeId + ", " + ScriptHelper.GetString(newName, true) + ");"));
    }


    private void FramesRefresh(bool refreshTree, int currentodeId)
    {
        // Refresh frames or tree
        string script = "if(FramesRefresh){FramesRefresh(" + refreshTree.ToString().ToLower() + ", " + nodeId + ");}";
        ScriptHelper.RegisterStartupScript(this, typeof(string), "refreshAction", ScriptHelper.GetScript(script));
    }


    private void InitializeProductControls()
    {
        // Load NewProduct control
        if (FileHelper.FileExists("~/CMSModules/Ecommerce/Pages/Content/Product/NewProduct.ascx"))
        {
            try
            {
                // Try to load product control
                Control ctrl = Page.LoadControl("~/CMSModules/Ecommerce/Pages/Content/Product/NewProduct.ascx");
                ctrl.ID = "ucNewProduct";
                pnlNewProduct.Controls.Add(ctrl);

                // Initialize product control
                ucNewProduct = (INewProductControl)ctrl;
            }
            catch { }
        }

        if (ucNewProduct == null)
        {
            plcNewProduct.Visible = false;
            return;
        }

        // Initialize product controls
        chkCreateProduct.Text = GetString("NewDocument.CreateProduct");
        chkCreateProduct.Attributes["onclick"] = "ShowHideSKUControls()";
        ucNewProduct.ClassID = classId;

        // Register script to show / hide SKU controls
        string script =
            "function ShowHideSKUControls() { \n" +
            "   var checkbox = document.getElementById('" + chkCreateProduct.ClientID + "'); \n" +
            "   var panel = document.getElementById('" + pnlNewProduct.ClientID + "'); \n" +
            "   if (panel != null) { if ((checkbox != null) && (checkbox.checked)) { panel.style.display = 'block'; } else { panel.style.display = 'none'; }} \n" +
            "} \n";

        AddScript(script);

        if (!RequestHelper.IsPostBack())
        {
            if ((ucNewProduct.ClassObj == null) || ucNewProduct.ClassObj.ClassCreateSKU || !ucNewProduct.ClassObj.ClassIsProduct)
            {
                // Hide checkbox when SKU should be created automatically or the doc. type is not product
                chkCreateProduct.Visible = false;
                // Hide also new product form
                plcNewProduct.Visible = false;
            }
            else
            {
                // Show checkbox to enable to enter SKU data
                chkCreateProduct.Checked = false;
            }
        }

        AddScript("ShowHideSKUControls();");
    }

    #endregion
}