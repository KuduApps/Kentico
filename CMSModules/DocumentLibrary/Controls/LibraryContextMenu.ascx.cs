using System;
using System.Text;
using System.Web.UI.WebControls;

using CMS.CMSHelper;
using CMS.ExtendedControls;
using CMS.GlobalHelper;
using CMS.SiteProvider;
using CMS.TreeEngine;
using CMS.UIControls;
using CMS.WorkflowEngine;
using CMS.SettingsProvider;
using CMS.FormEngine;

using TreeNode = CMS.TreeEngine.TreeNode;

public partial class CMSModules_DocumentLibrary_Controls_LibraryContextMenu : CMSUserControl
{
    #region "Constants"

    private const string CMS_FILE = "cms.file";

    #endregion


    #region "Variables"

    private TreeProvider mTreeProvider = null;
    private WorkflowManager mWorkflowManager = null;
    private MouseButtonEnum mMouseButton = MouseButtonEnum.Right;
    protected VerticalPositionEnum mVerticalPosition = VerticalPositionEnum.Cursor;
    protected HorizontalPositionEnum mHorizontalPosition = HorizontalPositionEnum.Cursor;

    private FormFieldInfo mFieldInfo = null;
    private DataClassInfo mDataClass = null;

    protected int mOffsetX = 0;
    protected int mOffsetY = 0;
    protected string mActiveItemCssClass = "SelectedRowContext";
    protected int mActiveItemOffset = 0;
    protected string mActiveItemInactiveCssClass = "Row";
    protected string mJavaScriptPrefix = null;
    private string mDocumentForm = null;

    #endregion


    #region "Properties"

    /// <summary>
    /// Prefix used to avoid javascript function name collision.
    /// </summary>
    public string JavaScriptPrefix
    {
        get
        {
            return mJavaScriptPrefix ?? (mJavaScriptPrefix = "DLCM_" + Parent.ClientID + "_");
        }
        set
        {
            mJavaScriptPrefix = value;
        }
    }


    /// <summary>
    /// Gets or sets handled mouse button(s).
    /// </summary>
    public MouseButtonEnum MouseButton
    {
        get
        {
            return mMouseButton;
        }
        set
        {
            mMouseButton = value;
            libraryMenuElem.MouseButton = value;
        }
    }


    /// <summary>
    /// Vertical position.
    /// </summary>
    public VerticalPositionEnum VerticalPosition
    {
        get
        {
            return mVerticalPosition;
        }
        set
        {
            mVerticalPosition = value;
        }
    }


    /// <summary>
    /// Horizontal position.
    /// </summary>
    public HorizontalPositionEnum HorizontalPosition
    {
        get
        {
            return mHorizontalPosition;
        }
        set
        {
            mHorizontalPosition = value;
        }
    }


    /// <summary>
    /// Offset X of the menu.
    /// </summary>
    public int OffsetX
    {
        get
        {
            return mOffsetX;
        }
        set
        {
            mOffsetX = value;
            libraryMenuElem.OffsetX = value;
        }
    }


    /// <summary>
    /// Offset Y of the menu.
    /// </summary>
    public int OffsetY
    {
        get
        {
            return mOffsetY;
        }
        set
        {
            mOffsetY = value;
            libraryMenuElem.OffsetY = value;
        }
    }


    /// <summary>
    /// Active item CSS class.
    /// </summary>
    public string ActiveItemCssClass
    {
        get
        {
            return mActiveItemCssClass;
        }
        set
        {
            mActiveItemCssClass = value;
        }
    }


    /// <summary>
    /// CSS class of highlighted element when context menu is closed.
    /// </summary>
    public string ActiveItemInactiveCssClass
    {
        get
        {
            return mActiveItemInactiveCssClass;
        }
        set
        {
            mActiveItemInactiveCssClass = value;
        }
    }


    /// <summary>
    /// Active item level offset.
    /// </summary>
    public int ActiveItemOffset
    {
        get
        {
            return mActiveItemOffset;
        }
        set
        {
            mActiveItemOffset = value;
        }
    }


    /// <summary>
    /// Tree provider instance.
    /// </summary>
    public TreeProvider TreeProvider
    {
        get
        {
            if (mTreeProvider == null)
            {
                mTreeProvider = new TreeProvider(CMSContext.CurrentUser);
                mTreeProvider.UseCache = false;
            }
            return mTreeProvider;
        }
    }


    /// <summary>
    /// Workflow manager instance.
    /// </summary>
    public WorkflowManager WorkflowManager
    {
        get
        {
            return mWorkflowManager ?? (mWorkflowManager = new WorkflowManager(TreeProvider));
        }
    }


    /// <summary>
    /// If true, the access permissions to the items are checked.
    /// </summary>
    public bool CheckPermissions
    {
        get
        {
            return ValidationHelper.GetBoolean(ViewState["CheckPermissions"], true);
        }
        set
        {
            ViewState["CheckPermissions"] = value;
        }
    }


    /// <summary>
    /// Class field info for the attachment.
    /// </summary>
    private FormFieldInfo FieldInfo
    {
        get
        {
            if (mFieldInfo == null)
            {
                FormInfo fi = null;
                if (!string.IsNullOrEmpty(DocumentForm))
                {
                    fi = FormHelper.GetFormInfo(DocumentForm, true);
                }
                else
                {
                    fi = FormHelper.GetFormInfo(DataClass.ClassName, true);
                }

                // Get valid extensions from form field info
                mFieldInfo = fi.GetFormField("FileAttachment");
            }
            return mFieldInfo;
        }
    }


    /// <summary>
    /// Data class info for the cms.file.
    /// </summary>
    private DataClassInfo DataClass
    {
        get
        {
            if (mDataClass == null)
            {
                // Get document type ('cms.file') settings
                mDataClass = DataClassInfoProvider.GetDataClass(CMS_FILE);
                if (mDataClass == null)
                {
                    throw new Exception("[DocumentLibrary.DataClass]: Class '" + CMS_FILE + "' is missing!");
                }
            }
            return mDataClass;
        }
    }


    /// <summary>
    /// Specifies the form used for editing document properties.
    /// </summary>
    public string DocumentForm
    {
        get
        {
            return mDocumentForm;
        }
        set
        {
            mDocumentForm = value;
        }
    }

    #endregion


    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Initialize menu element
        libraryMenuElem.OnReloadData += libraryMenuElem_OnReloadData;

        libraryMenuElem.LoadingContent = "<div class=\"LibraryContextMenu\"><div class=\"ItemPadding\">" + GetString("ContextMenu.Loading") + "</div></div>";
        libraryMenuElem.MenuID = "libraryMenu_" + ClientID;
        libraryMenuElem.MouseButton = MouseButton;
        libraryMenuElem.OffsetX = OffsetX;
        libraryMenuElem.OffsetY = OffsetY;
        libraryMenuElem.VerticalPosition = VerticalPosition;
        libraryMenuElem.HorizontalPosition = HorizontalPosition;
        libraryMenuElem.ActiveItemCssClass = ActiveItemCssClass;
        libraryMenuElem.ActiveItemOffset = ActiveItemOffset;
        libraryMenuElem.ActiveItemInactiveCssClass = ActiveItemInactiveCssClass;

        // Register WebDAV
        if (CMSContext.IsWebDAVEnabled(CMSContext.CurrentSiteName) && RequestHelper.IsWindowsAuthentication())
        {
            ScriptHelper.RegisterWebDAV(Page);
        }
    }

    #endregion


    #region "Dynamic menu loading"

    protected void libraryMenuElem_OnReloadData(object sender, EventArgs e)
    {
        string[] parameters = ValidationHelper.GetString(libraryMenuElem.Parameter, string.Empty).Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
        if (parameters.Length == 2)
        {
            // Parse identifier and document culture from library parameter
            int nodeId = ValidationHelper.GetInteger(parameters[0], 0);
            string cultureCode = ValidationHelper.GetString(parameters[1], string.Empty);

            // Get document using based on node identifier and culture
            TreeNode node = DocumentHelper.GetDocument(nodeId, cultureCode, TreeProvider);

            bool contextMenuVisible = false;
            bool localizeVisible = false;
            bool editVisible = false;
            bool uploadVisible = false;
            bool copyVisible = false;
            bool deleteVisible = false;
            bool openVisible = false;
            bool propertiesVisible = false;
            bool permissionsVisible = false;
            bool versionHistoryVisible = false;

            bool checkOutVisible = false;
            bool checkInVisible = false;
            bool undoCheckoutVisible = false;
            bool submitToApprovalVisible = false;
            bool rejectVisible = false;
            bool archiveVisible = false;

            if ((node != null) && (CMSContext.CurrentUser.IsAuthorizedPerDocument(node, NodePermissionsEnum.Read) == AuthorizationResultEnum.Allowed))
            {
                // Get original node (in case of linked documents)
                TreeNode originalNode = TreeProvider.GetOriginalNode(node);

                string documentType = ValidationHelper.GetString(node.GetValue("DocumentType"), string.Empty);
                string siteName = CMSContext.CurrentSiteName;
                string currentDocumentCulture = CMSContext.CurrentDocumentCulture.CultureCode;

                if (CMSContext.CurrentSiteID != originalNode.NodeSiteID)
                {
                    SiteInfo si = SiteInfoProvider.GetSiteInfo(originalNode.NodeSiteID);
                    siteName = si.SiteName;
                }

                // Get permissions
                const bool authorizedToRead = true;
                bool authorizedToDelete = (CMSContext.CurrentUser.IsAuthorizedPerDocument(node, NodePermissionsEnum.Delete) == AuthorizationResultEnum.Allowed);
                bool authorizedToModify = (CMSContext.CurrentUser.IsAuthorizedPerDocument(node, NodePermissionsEnum.Modify) == AuthorizationResultEnum.Allowed);
                bool authorizedCultureToModify = (CMSContext.CurrentUser.IsAuthorizedPerDocument(node, NodePermissionsEnum.Modify, false) == AuthorizationResultEnum.Allowed) && TreeSecurityProvider.HasUserCultureAllowed(NodePermissionsEnum.Modify, currentDocumentCulture, CMSContext.CurrentUser, siteName);
                bool authorizedToModifyPermissions = (CMSContext.CurrentUser.IsAuthorizedPerDocument(node, NodePermissionsEnum.ModifyPermissions) == AuthorizationResultEnum.Allowed);
                bool authorizedToCreate = CMSContext.CurrentUser.IsAuthorizedToCreateNewDocument(node.NodeParentID, node.NodeClassName);
                bool allowEditByCurrentUser = false;

                // Hide menu when user has no 'Read' permissions on document
                libraryMenuElem.Visible = authorizedToRead;

                // First evaluation of control's visibility
                bool differentCulture = (string.Compare(node.DocumentCulture, currentDocumentCulture, StringComparison.InvariantCultureIgnoreCase) != 0);
                localizeVisible = differentCulture && authorizedToCreate && authorizedCultureToModify;
                uploadVisible = authorizedToModify;
                copyVisible = authorizedToCreate && authorizedToModify;
                deleteVisible = authorizedToDelete;
                openVisible = authorizedToRead;
                propertiesVisible = authorizedToModify;
                permissionsVisible = authorizedToModifyPermissions;
                versionHistoryVisible = authorizedToModify;
                editVisible = authorizedToModify && CMSContext.IsWebDAVEnabled(CMSContext.CurrentSiteName) && RequestHelper.IsWindowsAuthentication() && WebDAVSettings.IsExtensionAllowedForEditMode(documentType, CMSContext.CurrentSiteName);

                // Get workflow object
                WorkflowInfo wi = WorkflowManager.GetNodeWorkflow(node);

                if ((wi != null) && authorizedToModify)
                {
                    bool autoPublishChanges = wi.WorkflowAutoPublishChanges;
                    // Get current step info, do not update document
                    WorkflowStepInfo si = WorkflowManager.GetStepInfo(node, false) ?? WorkflowManager.GetFirstWorkflowStep(node, wi);

                    bool canApprove = true;

                    // If license does not allow custom steps, 'can approve' check is meaningless
                    if (WorkflowInfoProvider.IsCustomStepAllowed())
                    {
                        canApprove = WorkflowManager.CanUserApprove(node, CMSContext.CurrentUser);
                    }

                    // Get name of current workflow step
                    string stepName = si.StepName.ToLower();
                    bool useCheckinCheckout = wi.UseCheckInCheckOut(CMSContext.CurrentSiteName);
                    int nodeCheckedOutByUser = node.DocumentCheckedOutByUserID;
                    bool nodeIsCheckedOut = (nodeCheckedOutByUser != 0);
                    bool allowEdit = canApprove || (stepName == "edit") || (stepName == "published") || (stepName == "archived");

                    // If document is checked in
                    if (nodeIsCheckedOut)
                    {
                        // If checked out by current user, add the check-in button and undo checkout button
                        if (nodeCheckedOutByUser == CMSContext.CurrentUser.UserID)
                        {
                            undoCheckoutVisible = true;
                            checkInVisible = true;
                            allowEditByCurrentUser = allowEdit;
                        }
                    }
                    else
                    {
                        // Hide check-out menu item if user can't apporve or document is in specific step
                        if (allowEdit)
                        {
                            // If site uses check-out / check-in
                            if (useCheckinCheckout)
                            {
                                checkOutVisible = true;
                            }
                            else
                            {
                                allowEditByCurrentUser = true;
                            }
                        }
                    }

                    rejectVisible = canApprove && !nodeIsCheckedOut && (stepName != "edit") && (stepName != "published") && (stepName != "archived") && !autoPublishChanges;
                    submitToApprovalVisible = (canApprove || ((stepName == "edit") && authorizedToRead)) && !nodeIsCheckedOut && (stepName != "published") && (stepName != "archived") && !autoPublishChanges;
                    archiveVisible = canApprove && !nodeIsCheckedOut && (stepName == "published");
                }
                else
                {
                    allowEditByCurrentUser = true;
                }

                // Check whether the document is not checked out by another user
                editVisible &= allowEditByCurrentUser;
                uploadVisible &= allowEditByCurrentUser;

                string parameterScript = "GetContextMenuParameter('" + libraryMenuElem.MenuID + "')";

                // Initialize edit menu item
                Guid attachmentGuid = ValidationHelper.GetGuid(node.GetValue("FileAttachment"), Guid.Empty);

                // If attachment field doesn't allow empty value and the value is empty
                if ((FieldInfo != null) && !FieldInfo.AllowEmpty && (attachmentGuid == Guid.Empty))
                {
                    submitToApprovalVisible = false;
                    archiveVisible = false;
                    checkInVisible = false;
                }

                // Get attachment
                AttachmentInfo ai = DocumentHelper.GetAttachment(attachmentGuid, TreeProvider, siteName, false);

                Panel previousPanel = null;
                Panel currentPanel = pnlEdit;

                if (editVisible)
                {
                    if (ai != null)
                    {
                        // Load WebDAV edit control and initialize it
                        WebDAVEditControl editAttachment = Page.LoadControl("~/CMSModules/WebDAV/Controls/AttachmentWebDAVEditControl.ascx") as WebDAVEditControl;

                        if (editAttachment != null)
                        {
                            editAttachment.ID = "editAttachment";
                            editAttachment.NodeAliasPath = node.NodeAliasPath;
                            editAttachment.NodeCultureCode = node.DocumentCulture;
                            editAttachment.AttachmentFieldName = "FileAttachment";
                            editAttachment.FileName = ai.AttachmentName;
                            editAttachment.IsLiveSite = IsLiveSite;
                            editAttachment.UseImageButton = true;
                            editAttachment.LabelText = GetString("general.edit");
                            editAttachment.CssClass = "Icon";
                            editAttachment.LabelCssClass = "Name";
                            editAttachment.RefreshScript = JavaScriptPrefix + "PerformAction(" + parameterScript + ", 'WebDAVRefresh');";
                            // Set Group ID for live site
                            editAttachment.GroupID = IsLiveSite ? node.GetIntegerValue("NodeGroupID") : 0;
                            editAttachment.ReloadData(true);

                            pnlEditPadding.Controls.Add(editAttachment);
                            pnlEditPadding.CssClass = editAttachment.EnabledResult ? "ItemPadding" : "ItemPaddingDisabled";
                        }
                    }
                    else
                    {
                        editVisible = false;
                        openVisible = false;
                    }
                }

                previousPanel = currentPanel;
                currentPanel = pnlUpload;

                // Initialize upload menu item
                if (authorizedToModify)
                {
                    string uploaderImg = "<img class=\"UploaderImage\" src=\"" + GetImageUrl("Design/Controls/ContextMenu/DocumentLibrary/Upload.png", IsLiveSite) + "\" alt=\"" + GetString("general.update") + "\" />";
                    string uploaderText = "<span class=\"UploaderText\">" + GetString("general.update") + "</span>";

                    StringBuilder uploaderInnerHtml = new StringBuilder();

                    bool isRTL = (IsLiveSite && CultureHelper.IsPreferredCultureRTL()) || (!IsLiveSite && CultureHelper.IsUICultureRTL());
                    if (isRTL)
                    {
                        uploaderInnerHtml.Append(uploaderText);
                        uploaderInnerHtml.Append(uploaderImg);
                    }
                    else
                    {
                        uploaderInnerHtml.Append(uploaderImg);
                        uploaderInnerHtml.Append(uploaderText);
                    }

                    // Initialize direct file uploader
                    updateAttachment.InnerDivHtml = uploaderInnerHtml.ToString();
                    updateAttachment.InnerDivClass = "LibraryContextUploader";
                    updateAttachment.DocumentID = node.DocumentID;
                    updateAttachment.ParentElemID = ClientID;
                    updateAttachment.SourceType = MediaSourceEnum.Attachment;
                    updateAttachment.AttachmentGUIDColumnName = "FileAttachment";
                    updateAttachment.DisplayInline = true;
                    updateAttachment.IsLiveSite = IsLiveSite;

                    // Set allowed extensions
                    if ((FieldInfo != null) && ValidationHelper.GetString(FieldInfo.Settings["extensions"], "") == "custom")
                    {
                        // Load allowed extensions
                        updateAttachment.AllowedExtensions = ValidationHelper.GetString(FieldInfo.Settings["allowed_extensions"], "");
                    }
                    else
                    {
                        // Use site settings
                        updateAttachment.AllowedExtensions = SettingsKeyProvider.GetStringValue(siteName + ".CMSUploadExtensions");
                    }

                    updateAttachment.ReloadData();
                    SetupPanelClasses(currentPanel, previousPanel);
                }

                previousPanel = currentPanel;
                currentPanel = pnlLocalize;

                // Initialize localize menu item
                if (localizeVisible)
                {
                    lblLocalize.RefreshText();
                    imgLocalize.AlternateText = lblLocalize.Text;
                    imgLocalize.ImageUrl = GetImageUrl("Design/Controls/ContextMenu/DocumentLibrary/Localize.png", IsLiveSite);
                    pnlLocalize.Attributes.Add("onclick", JavaScriptPrefix + "PerformAction(" + parameterScript + ", 'Localize');");
                    SetupPanelClasses(currentPanel, previousPanel);
                }

                previousPanel = null;
                currentPanel = pnlCopy;

                // Initialize copy menu item
                if (copyVisible)
                {
                    lblCopy.RefreshText();
                    imgCopy.ImageUrl = GetImageUrl("Design/Controls/ContextMenu/DocumentLibrary/Copy.png", IsLiveSite);
                    pnlCopy.Attributes.Add("onclick", JavaScriptPrefix + "PerformAction(" + parameterScript + ",'Copy');");
                    SetupPanelClasses(currentPanel, previousPanel);
                }

                previousPanel = currentPanel;
                currentPanel = pnlDelete;

                // Initialize delete menu item
                if (deleteVisible)
                {
                    lblDelete.RefreshText();
                    imgDelete.ImageUrl = GetImageUrl("Design/Controls/ContextMenu/DocumentLibrary/Delete.png", IsLiveSite);
                    pnlDelete.Attributes.Add("onclick", JavaScriptPrefix + "PerformAction(" + parameterScript + ", 'Delete');");
                    SetupPanelClasses(currentPanel, previousPanel);
                }

                previousPanel = currentPanel;
                currentPanel = pnlOpen;

                // Initialize open menu item
                if (openVisible)
                {
                    lblOpen.RefreshText();
                    imgOpen.ImageUrl = GetImageUrl("Design/Controls/ContextMenu/DocumentLibrary/Open.png", IsLiveSite);
                    if (ai != null)
                    {
                        // Get document URL
                        string attachmentUrl = CMSContext.ResolveUIUrl(AttachmentURLProvider.GetPermanentAttachmentUrl(node.NodeGUID, node.NodeAlias));
                        if (authorizedToModify)
                        {
                            attachmentUrl = URLHelper.AddParameterToUrl(attachmentUrl, "latestfordocid", ValidationHelper.GetString(node.DocumentID, string.Empty));
                            attachmentUrl = URLHelper.AddParameterToUrl(attachmentUrl, "hash", ValidationHelper.GetHashString("d" + node.DocumentID));
                        }
                        attachmentUrl = URLHelper.UpdateParameterInUrl(attachmentUrl, "chset", Guid.NewGuid().ToString());

                        if (!string.IsNullOrEmpty(attachmentUrl))
                        {
                            pnlOpen.Attributes.Add("onclick", "location.href = " + ScriptHelper.GetString(attachmentUrl) + ";");
                        }
                    }
                    SetupPanelClasses(currentPanel, previousPanel);
                }

                previousPanel = null;
                currentPanel = pnlProperties;

                // Initialize properties menu item
                lblProperties.RefreshText();
                imgProperties.ImageUrl = GetImageUrl("Design/Controls/ContextMenu/DocumentLibrary/Properties.png", IsLiveSite);
                pnlProperties.Attributes.Add("onclick", JavaScriptPrefix + "PerformAction(" + parameterScript + ", 'Properties');");
                SetupPanelClasses(currentPanel, previousPanel);

                previousPanel = currentPanel;
                currentPanel = pnlPermissions;

                // Initialize permissions menu item
                lblPermissions.RefreshText();
                imgPermissions.ImageUrl = GetImageUrl("Design/Controls/ContextMenu/DocumentLibrary/Permissions.png", IsLiveSite);
                pnlPermissions.Attributes.Add("onclick", JavaScriptPrefix + "PerformAction(" + parameterScript + ", 'Permissions');");
                SetupPanelClasses(currentPanel, previousPanel);

                previousPanel = currentPanel;
                currentPanel = pnlVersionHistory;

                // Initialize version history menu item
                lblVersionHistory.RefreshText();
                imgVersionHistory.ImageUrl = GetImageUrl("Design/Controls/ContextMenu/DocumentLibrary/VersionHistory.png", IsLiveSite);
                pnlVersionHistory.Attributes.Add("onclick", JavaScriptPrefix + "PerformAction(" + parameterScript + ", 'VersionHistory');");
                SetupPanelClasses(currentPanel, previousPanel);

                previousPanel = null;
                currentPanel = pnlCheckOut;

                // Initialize checkout menu item
                if (checkOutVisible)
                {
                    lblCheckOut.RefreshText();
                    imgCheckOut.ImageUrl = GetImageUrl("Design/Controls/ContextMenu/DocumentLibrary/CheckOut.png", IsLiveSite);
                    pnlCheckOut.Attributes.Add("onclick", JavaScriptPrefix + "PerformAction(" + parameterScript + ", 'CheckOut');");
                    SetupPanelClasses(currentPanel, previousPanel);
                }

                previousPanel = currentPanel;
                currentPanel = pnlCheckIn;

                // Initialize checkin menu item
                if (checkInVisible)
                {
                    lblCheckIn.RefreshText();
                    imgCheckIn.ImageUrl = GetImageUrl("Design/Controls/ContextMenu/DocumentLibrary/CheckIn.png", IsLiveSite);
                    pnlCheckIn.Attributes.Add("onclick", JavaScriptPrefix + "PerformAction(" + parameterScript + ", 'CheckIn');");
                    SetupPanelClasses(currentPanel, previousPanel);
                }

                previousPanel = currentPanel;
                currentPanel = pnlUndoCheckout;

                // Initialize undo checkout menu item
                if (undoCheckoutVisible)
                {
                    lblUndoCheckout.RefreshText();
                    imgUndoCheckout.ImageUrl = GetImageUrl("Design/Controls/ContextMenu/DocumentLibrary/UndoCheckout.png", IsLiveSite);
                    pnlUndoCheckout.Attributes.Add("onclick", JavaScriptPrefix + "PerformAction(" + parameterScript + ", 'UndoCheckout');");
                    SetupPanelClasses(currentPanel, previousPanel);
                }

                previousPanel = currentPanel;
                currentPanel = pnlSubmitToApproval;

                // Initialize submit to approval / publish menu item
                if (submitToApprovalVisible)
                {
                    if (wi != null)
                    {
                        // Get next step info
                        WorkflowStepInfo nsi = WorkflowManager.GetNextStepInfo(node);
                        if (nsi.StepName.ToLower() == "published")
                        {
                            // Set 'Publish' label
                            lblSubmitToApproval.ResourceString = "general.publish";
                        }
                    }

                    lblSubmitToApproval.RefreshText();
                    imgSubmitToApproval.ImageUrl = GetImageUrl("Design/Controls/ContextMenu/DocumentLibrary/SubmitToApproval.png", IsLiveSite);
                    pnlSubmitToApproval.Attributes.Add("onclick", JavaScriptPrefix + "PerformAction(" + parameterScript + ", 'SubmitToApproval');");
                    SetupPanelClasses(currentPanel, previousPanel);
                }

                previousPanel = currentPanel;
                currentPanel = pnlReject;

                // Initialize reject menu item
                if (rejectVisible)
                {
                    lblReject.RefreshText();
                    imgReject.ImageUrl = GetImageUrl("Design/Controls/ContextMenu/DocumentLibrary/Reject.png", IsLiveSite);
                    pnlReject.Attributes.Add("onclick", JavaScriptPrefix + "PerformAction(" + parameterScript + ", 'Reject');");
                    SetupPanelClasses(currentPanel, previousPanel);
                }

                previousPanel = currentPanel;
                currentPanel = pnlArchive;

                // Initialize archive menu item
                if (archiveVisible)
                {
                    lblArchive.RefreshText();
                    imgArchive.ImageUrl = GetImageUrl("Design/Controls/ContextMenu/DocumentLibrary/Archive.png", IsLiveSite);
                    pnlArchive.Attributes.Add("onclick", JavaScriptPrefix + "PerformAction(" + parameterScript + ", 'Archive');");
                    SetupPanelClasses(currentPanel, previousPanel);
                }

                // Set up visibility of menu items
                pnlLocalize.Visible = localizeVisible;
                pnlUpload.Visible = uploadVisible;
                pnlDelete.Visible = deleteVisible;
                pnlCopy.Visible = copyVisible;
                pnlOpen.Visible = openVisible;
                pnlProperties.Visible = propertiesVisible;
                pnlPermissions.Visible = permissionsVisible;
                pnlVersionHistory.Visible = versionHistoryVisible;
                pnlEdit.Visible = editVisible;

                pnlCheckOut.Visible = checkOutVisible;
                pnlCheckIn.Visible = checkInVisible;
                pnlUndoCheckout.Visible = undoCheckoutVisible;
                pnlSubmitToApproval.Visible = submitToApprovalVisible;
                pnlReject.Visible = rejectVisible;
                pnlArchive.Visible = archiveVisible;

                // Set up visibility of whole menu
                contextMenuVisible = true;
            }

            // Set up visibility of separators
            bool firstGroupVisible = editVisible || uploadVisible || localizeVisible;
            bool secondGroupVisible = copyVisible || deleteVisible || openVisible;
            bool thirdGroupVisible = propertiesVisible || permissionsVisible || versionHistoryVisible;
            bool fourthGroupVisible = checkOutVisible || checkInVisible || undoCheckoutVisible || submitToApprovalVisible || rejectVisible || archiveVisible;

            pnlSep1.Visible = firstGroupVisible && secondGroupVisible;
            pnlSep2.Visible = secondGroupVisible && thirdGroupVisible;
            pnlSep3.Visible = thirdGroupVisible && fourthGroupVisible;

            // Setup 'No action available' menu item
            pnlNoAction.Visible = !contextMenuVisible;
            lblNoAction.RefreshText();
        }
    }


    /// <summary>
    /// Sets up CSS classes of menu items.
    /// </summary>
    /// <param name="currentPanel">Current menu item</param>
    /// <param name="previousPanel">Previous menu item</param>
    private static void SetupPanelClasses(Panel currentPanel, Panel previousPanel)
    {
        if (previousPanel == null)
        {
            if (currentPanel != null)
            {
                currentPanel.CssClass = "Item";
            }
        }
        else
        {
            previousPanel.CssClass = "Item";
            if (currentPanel != null)
            {
                currentPanel.CssClass = "ItemLast";
            }
        }
    }

    #endregion
}
