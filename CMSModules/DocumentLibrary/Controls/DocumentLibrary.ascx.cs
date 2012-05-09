using System;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;

using CMS.Controls;
using CMS.ExtendedControls;
using CMS.GlobalHelper;
using CMS.PortalEngine;
using CMS.SettingsProvider;
using CMS.TreeEngine;
using CMS.UIControls;
using CMS.WorkflowEngine;
using CMS.SiteProvider;
using CMS.CMSHelper;
using CMS.LicenseProvider;
using CMS.FormEngine;

using TimeZoneInfo = CMS.SiteProvider.TimeZoneInfo;
using TreeNode = CMS.TreeEngine.TreeNode;

public partial class CMSModules_DocumentLibrary_Controls_DocumentLibrary : CMSUserControl, IPostBackEventHandler
{
    #region "Constants"

    private const string CMS_FILE = "cms.file";

    #endregion


    #region "Variables"

    private SiteInfo mCurrentSite = null;
    private TreeProvider mTreeProvider = null;
    private TimeZoneInfo usedTimeZone = null;
    private WorkflowManager mWorkflowManager = null;
    private VersionManager mVersionManager = null;
    private TreeNode mLibraryNode = null;
    private CurrentUserInfo mCurrentUser = null;
    private FormFieldInfo mFieldInfo = null;
    private DataClassInfo mDataClass = null;

    private string mNodeAliasPath = null;
    private string mPreferredCultureCode = null;
    private int mPageSize = 0;
    private int mGroupID = 0;
    private string mDocumentForm = null;
    private bool? mIsGroupAdmin = null;
    private bool? mIsAuthorizedToCreate = null;

    /// <summary>
    /// Document properties object.
    /// </summary>
    public CMSDataProperties mDocumentProperties = new CMSDataProperties();

    #endregion


    #region "Properties"

    /// <summary>
    /// Gets current user.
    /// </summary>
    public CurrentUserInfo CurrentUser
    {
        get
        {
            return mCurrentUser ?? (mCurrentUser = CMSContext.CurrentUser);
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
    /// Version manager.
    /// </summary>
    public VersionManager VersionManager
    {
        get
        {
            return mVersionManager ?? (mVersionManager = new VersionManager(TreeProvider));
        }
    }


    /// <summary>
    /// Gets current site.
    /// </summary>
    public SiteInfo CurrentSite
    {
        get
        {
            return mCurrentSite ?? (mCurrentSite = CMSContext.CurrentSite);
        }
    }


    /// <summary>
    /// Determines whether to send workflow emails.
    /// </summary>
    private bool SendWorkflowEmails
    {
        get
        {
            return SettingsKeyProvider.GetBoolValue(CurrentSite.SiteName + ".CMSSendWorkflowEmails");
        }
    }


    /// <summary>
    /// Gets current document culture.
    /// </summary>
    public string PreferredCultureCode
    {
        get
        {
            return mPreferredCultureCode ?? (mPreferredCultureCode = CMSContext.PreferredCultureCode);
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether documents are combined with default culture version.
    /// </summary>
    public bool CombineWithDefaultCulture
    {
        get
        {
            return mDocumentProperties.CombineWithDefaultCulture;
        }
        set
        {
            mDocumentProperties.CombineWithDefaultCulture = value;
        }
    }


    /// <summary>
    /// Alias path determining parent document of library.
    /// </summary>
    public string LibraryPath
    {
        get
        {
            string aliasPath = DataHelper.GetNotEmpty(mNodeAliasPath, CMSContext.CurrentAliasPath);
            return TreePathUtils.EnsureSingleNodePath(aliasPath);
        }
        set
        {
            mNodeAliasPath = value;
        }
    }


    /// <summary>
    /// Gets parent document of library.
    /// </summary>
    private TreeNode LibraryNode
    {
        get
        {
            return mLibraryNode ?? (mLibraryNode = DocumentHelper.GetDocument(CurrentSite.SiteName, LibraryPath, CMSContext.CurrentDocumentCulture.CultureCode, CombineWithDefaultCulture, null, null, null, -1, false, null, TreeProvider));
        }
    }


    /// <summary>
    /// Number of displayed documents.
    /// </summary>
    public int PageSize
    {
        get
        {
            return mPageSize;
        }
        set
        {
            mPageSize = value;
            if (gridDocuments == null)
            {
                EnsureChildControls();
            }
            gridDocuments.PageSize = value.ToString();
        }
    }


    /// <summary>
    /// Gets or sets group identifier (if not set only non-group documents are being selected).
    /// </summary>
    public int GroupID
    {
        get
        {
            return mGroupID;
        }
        set
        {
            mGroupID = value;
        }
    }


    /// <summary>
    /// Specifies the form used for editing document properties.
    /// </summary>
    public string DocumentForm
    {
        get
        {
            if (string.IsNullOrEmpty(mDocumentForm))
            {
                mDocumentForm = CMS_FILE + ".documentlibrary";
            }
            return mDocumentForm;
        }
        set
        {
            mDocumentForm = value;
        }
    }


    /// <summary>
    /// Gets or sets identifier of current dialog.
    /// </summary>
    private string CurrentDialogID
    {
        get
        {
            return ValidationHelper.GetString(ViewState["CurrentDialogID"], string.Empty);
        }
        set
        {
            ViewState["CurrentDialogID"] = value;
        }
    }


    /// <summary>
    /// Gets or sets current modal dialog ID.
    /// </summary>
    private string CurrentModalID
    {
        get
        {
            return ValidationHelper.GetString(ViewState["CurrentModalID"], string.Empty);
        }
        set
        {
            ViewState["CurrentModalID"] = value;
        }
    }


    /// <summary>
    /// Gets current dialog control.
    /// </summary>
    private Panel CurrentDialog
    {
        get
        {
            return (Panel)FindControl(CurrentDialogID);
        }
    }


    /// <summary>
    /// Gets current modal control.
    /// </summary>
    private ModalPopupDialog CurrentModal
    {
        get
        {
            return (ModalPopupDialog)FindControl(CurrentModalID);
        }
    }


    /// <summary>
    /// Prefix used to avoid javascript function name collision.
    /// </summary>
    public string JS_PREFIX
    {
        get
        {
            return "DLCM_" + ClientID + "_";
        }
    }


    /// <summary>
    /// Number of minutes the retrieved content is cached for. Zero indicates that the content will not be cached.
    /// </summary>
    /// <remarks>
    /// This parameter allows you to set up caching of content so that it's not retrieved from the database each time a user requests the page.
    /// </remarks>
    public virtual int CacheMinutes
    {
        get
        {
            return mDocumentProperties.CacheMinutes;
        }
        set
        {
            mDocumentProperties.CacheMinutes = value;
        }
    }


    /// <summary>
    /// Name of the cache item the control will use.
    /// </summary>
    /// <remarks>
    /// By setting this name dynamically, you can achieve caching based on URL parameter or some other variable - simply put the value of the parameter to the CacheItemName property. If no value is set, the control stores its content to the item named "URL|ControlID".
    /// </remarks>
    public virtual string CacheItemName
    {
        get
        {
            return mDocumentProperties.CacheItemName;
        }
        set
        {
            mDocumentProperties.CacheItemName = value;
        }
    }


    /// <summary>
    /// Cache dependencies, each cache dependency on a new line.
    /// </summary>
    public virtual string CacheDependencies
    {
        get
        {
            return mDocumentProperties.CacheDependencies;
        }
        set
        {
            mDocumentProperties.CacheDependencies = value;
        }
    }


    /// <summary>
    /// Gets or sets the text which is displayed for zero rows result.
    /// </summary>
    public string ZeroRowsText
    {
        get
        {
            return ValidationHelper.GetString(GetValue("ZeroRowsText"), gridDocuments.ZeroRowsText);
        }
        set
        {
            SetValue("ZeroRowsText", value);
            gridDocuments.ZeroRowsText = value;
        }
    }


    /// <summary>
    /// Permissions icon image URL.
    /// </summary>
    public string PermissionsIconImageUrl
    {
        get
        {
            return DataHelper.GetNotEmpty(this.GetValue("PermissionsIconImageUrl"), GetImageUrl("CMSModules/CMS_DocumentLibrary/LibraryPermissions.png", IsLiveSite));
        }
        set
        {
            this.SetValue("PermissionsIconImageUrl", value);
            this.imgPermissions.ImageUrl = value;
        }
    }


    /// <summary>
    /// New file icon image URL.
    /// </summary>
    public string NewFileIconImageUrl
    {
        get
        {
            return DataHelper.GetNotEmpty(this.GetValue("NewFileIconImageUrl"), GetImageUrl("CMSModules/CMS_DocumentLibrary/NewDocument.png", IsLiveSite));
        }
        set
        {
            this.SetValue("NewFileIconImageUrl", value);
        }
    }


    /// <summary>
    /// Allows you to specify whether to check permissions of the current user. If the value is 'false' (default value) no permissions are checked. Otherwise, only nodes for which the user has read permission are displayed.
    /// </summary>
    public virtual bool CheckPermissions
    {
        get
        {
            return mDocumentProperties.CheckPermissions;
        }
        set
        {
            mDocumentProperties.CheckPermissions = value;
        }
    }


    /// <summary>
    /// Indicates if user is authorized to create new document in the document library.
    /// </summary>
    private bool IsAuthorizedToCreate
    {
        get
        {
            if (mIsAuthorizedToCreate == null)
            {
                mIsAuthorizedToCreate = CurrentUser.IsAuthorizedToCreateNewDocument(LibraryNode, CMS_FILE);
            }
            return mIsAuthorizedToCreate.Value;
        }
        set
        {
            mIsAuthorizedToCreate = value;
        }
    }


    /// <summary>
    /// Identifies if user is group administrator.
    /// </summary>
    private bool IsGroupAdmin
    {
        get
        {
            if (mIsGroupAdmin == null)
            {
                mIsGroupAdmin = (GroupID > 0) ? CurrentUser.IsGroupAdministrator(GroupID) : false;
            }
            return mIsGroupAdmin.Value;
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
                FormInfo fi = FormHelper.GetFormInfo(DataClass.ClassName, true);

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

    #endregion


    #region "Enums"

    private enum Action
    {
        RefreshGrid,
        RefreshGridSimple,
        WebDAVRefresh,
        InitRefresh,
        HidePopup,
        Localize,
        Copy,
        Delete,
        Properties,
        Permissions,
        VersionHistory,
        CheckOut,
        CheckIn,
        UndoCheckout,
        SubmitToApproval,
        Reject,
        Archive
    }

    #endregion


    #region "Page events"

    protected override void EnsureChildControls()
    {
        base.EnsureChildControls();
        if (gridDocuments == null)
        {
            pnlUpdate.LoadContainer();
        }
    }


    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        // Register trigger for async postbacks
        AsyncPostBackTrigger asyncPostBackTrigger = new AsyncPostBackTrigger();
        asyncPostBackTrigger.ControlID = ID;
        pnlUpdate.Triggers.Add(asyncPostBackTrigger);
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        // Check license
        LicenseHelper.CheckFeatureAndRedirect(URLHelper.GetCurrentDomain(), FeatureEnum.DocumentLibrary);

        if (StopProcessing || (LibraryNode == null))
        {
            gridDocuments.StopProcessing = true;
            arrowContextMenu.StopProcessing = true;
            rowContextMenu.StopProcessing = true;
            copyElem.StopProcessing = true;
            deleteElem.StopProcessing = true;
            localizeElem.StopProcessing = true;
            propertiesElem.StopProcessing = true;
            permissionsElem.StopProcessing = true;
            versionsElem.StopProcessing = true;
            pnlHeader.Visible = false;

            if (LibraryNode == null)
            {
                lblError.Text = ResHelper.Format("documentlibrary.libraryrootnotexist", LibraryPath);
            }
        }
        else
        {
            if (BrowserHelper.IsIE9())
            {
                ScriptHelper.RegisterSpellChecker(Page);
                ScriptHelper.RegisterShortcuts(Page);
                ScriptHelper.RegisterSaveChanges(Page);
            }
            gridDocuments.OnDataReload += gridDocuments_OnDataReload;
            gridDocuments.OnExternalDataBound += gridDocuments_OnExternalDataBound;
            gridDocuments.OrderBy = "DocumentName ASC";
            gridDocuments.IsLiveSite = IsLiveSite;
            copyTitle.IsLiveSite = IsLiveSite;
            propertiesTitle.IsLiveSite = IsLiveSite;
            permissionsElem.IsLiveSite = IsLiveSite;
            arrowContextMenu.IsLiveSite = IsLiveSite;
            arrowContextMenu.JavaScriptPrefix = JS_PREFIX;
            arrowContextMenu.DocumentForm = DocumentForm;
            rowContextMenu.IsLiveSite = IsLiveSite;
            rowContextMenu.JavaScriptPrefix = JS_PREFIX;
            rowContextMenu.DocumentForm = DocumentForm;

            // Optimize context menu position for all supported browsers
            bool isRTL = (IsLiveSite && CultureHelper.IsPreferredCultureRTL()) || (!IsLiveSite && CultureHelper.IsUICultureRTL());
            if (BrowserHelper.IsGecko() || BrowserHelper.IsIE8())
            {
                arrowContextMenu.OffsetX = isRTL ? 0 : -1;
                if (BrowserHelper.IsIE8())
                {
                    arrowContextMenu.OffsetY = 21;
                }
            }
            else if (BrowserHelper.IsIE7())
            {
                arrowContextMenu.OffsetX = isRTL ? 0 : -2;
            }
            else if (BrowserHelper.IsWebKit())
            {
                arrowContextMenu.OffsetX = isRTL ? -1 : 1;
                arrowContextMenu.OffsetY = 21;
            }

            // Register full postback
            ControlsHelper.RegisterPostbackControl(btnLocalizeSaveClose);
            ControlsHelper.RegisterPostbackControl(btnPropertiesSaveClose);

            // Highlighting script
            StringBuilder highLightScript = new StringBuilder();
            highLightScript.AppendLine("function DL_Highlight(element, highlight)");
            highLightScript.AppendLine("{");
            highLightScript.AppendLine("   if(element.className != 'SelectedRowContext')");
            highLightScript.AppendLine("   {");
            highLightScript.AppendLine("       if(highlight)");
            highLightScript.AppendLine("       {");
            highLightScript.AppendLine("           element.className = 'SelectedRow';");
            highLightScript.AppendLine("       }");
            highLightScript.AppendLine("       else");
            highLightScript.AppendLine("       {");
            highLightScript.AppendLine("           element.className = 'Row';");
            highLightScript.AppendLine("       }");
            highLightScript.AppendLine("   }");
            highLightScript.AppendLine("}");
            ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "DL_highLightScript", ScriptHelper.GetScript(highLightScript.ToString()));

            // Action handling scripts
            StringBuilder actionScript = new StringBuilder();

            actionScript.AppendLine("function SetParameter_" + ClientID + "(parameter)");
            actionScript.AppendLine("{");
            actionScript.AppendLine("   document.getElementById('" + hdnParameter.ClientID + "').value =  parameter;");
            actionScript.AppendLine("}");

            actionScript.AppendLine("function " + Action.RefreshGridSimple + "_" + ClientID + "()");
            actionScript.AppendLine("{");
            actionScript.AppendLine("   " + Page.ClientScript.GetPostBackEventReference(this, Action.RefreshGridSimple.ToString()) + ";");
            actionScript.AppendLine("}");

            actionScript.AppendLine("function " + Action.HidePopup + "_" + ClientID + "()");
            actionScript.AppendLine("{");
            actionScript.AppendLine("   " + Page.ClientScript.GetPostBackEventReference(this, Action.HidePopup.ToString()) + ";");
            actionScript.AppendLine("}");

            actionScript.AppendLine("function " + Action.InitRefresh + "_" + ClientID + "(message, fullRefresh, mode)");
            actionScript.AppendLine("{");
            actionScript.AppendLine("   if(message != '')");
            actionScript.AppendLine("   {");
            actionScript.AppendLine("       alert(message);");
            actionScript.AppendLine("   }");
            actionScript.AppendLine("   else");
            actionScript.AppendLine("   {");
            actionScript.AppendLine("       " + Action.RefreshGridSimple + "_" + ClientID + "();");
            actionScript.AppendLine("   }");
            actionScript.AppendLine("}");

            actionScript.AppendLine("function InitRefresh_" + arrowContextMenu.ClientID + "(message, fullRefresh, mode)");
            actionScript.AppendLine("{");
            actionScript.AppendLine(Action.InitRefresh + "_" + ClientID + "(message, fullRefresh, mode);");
            actionScript.AppendLine("}");

            actionScript.AppendLine("function InitRefresh_" + rowContextMenu.ClientID + "(message, fullRefresh, mode)");
            actionScript.AppendLine("{");
            actionScript.AppendLine(Action.InitRefresh + "_" + ClientID + "(message, fullRefresh, mode);");
            actionScript.AppendLine("}");

            actionScript.AppendLine("function " + JS_PREFIX + "PerformAction(parameter, action)");
            actionScript.AppendLine("{");
            actionScript.AppendLine("   SetParameter_" + ClientID + "(parameter);");
            actionScript.AppendLine("   " + Page.ClientScript.GetPostBackEventReference(this, "##PARAM##").Replace("'##PARAM##'", "action"));
            actionScript.AppendLine("}");

            ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "actionScript" + ClientID, ScriptHelper.GetScript(actionScript.ToString()));

            // Setup title elements
            localizeTitle.TitleText = GetString("documentlibrary.localizedocument");
            localizeTitle.TitleImage = GetImageUrl("CMSModules/CMS_DocumentLibrary/Localize.png", IsLiveSite);

            copyTitle.TitleText = GetString("documentlibrary.copydocument");
            copyTitle.TitleImage = GetImageUrl("CMSModules/CMS_DocumentLibrary/Copy.png", IsLiveSite);

            deleteTitle.TitleText = GetString("documentlibrary.deletedocument");
            deleteTitle.TitleImage = GetImageUrl("CMSModules/CMS_DocumentLibrary/Delete.png", IsLiveSite);

            propertiesTitle.TitleText = GetString("documentlibrary.documentproperties");
            propertiesTitle.TitleImage = GetImageUrl("CMSModules/CMS_DocumentLibrary/Properties.png", IsLiveSite);

            permissionsTitle.TitleText = GetString("documentlibrary.documentpermissions");
            permissionsTitle.TitleImage = GetImageUrl("CMSModules/CMS_DocumentLibrary/Permissions.png", IsLiveSite);

            versionsTitle.TitleText = GetString("LibraryContextMenu.VersionHistory");
            versionsTitle.TitleImage = GetImageUrl("CMSModules/CMS_DocumentLibrary/VersionHistory.png", IsLiveSite);

            // Initialize new file uploader
            bool uploadVisible = (CurrentUser.IsAuthorizedPerDocument(LibraryNode, new NodePermissionsEnum[] { NodePermissionsEnum.Read, NodePermissionsEnum.Modify }) == AuthorizationResultEnum.Allowed) && IsAuthorizedToCreate;
            uploadAttachment.Visible = uploadVisible;
            if (uploadVisible)
            {
                string imgTag = String.IsNullOrEmpty(NewFileIconImageUrl) ? null : String.Format("<img alt=\"\" src=\"{0}\" />", ResolveUrl(NewFileIconImageUrl));
                uploadAttachment.InnerDivHtml = String.Format("{0}<span>{1}</span>", imgTag, GetString("documentlibrary.newdocument"));
                uploadAttachment.InnerDivClass = "LibraryUploader";
                uploadAttachment.NodeParentNodeID = LibraryNode.NodeID;
                uploadAttachment.ParentElemID = ClientID;
                uploadAttachment.SourceType = MediaSourceEnum.Content;
                uploadAttachment.DisplayInline = true;
                uploadAttachment.IsLiveSite = IsLiveSite;
                uploadAttachment.NodeGroupID = GroupID;
                uploadAttachment.DocumentCulture = CMSContext.CurrentDocumentCulture.CultureCode;
                uploadAttachment.CheckPermissions = true;
                uploadAttachment.Width = 100;

                // Set allowed extensions
                if ((FieldInfo != null) && ValidationHelper.GetString(FieldInfo.Settings["extensions"], "") == "custom")
                {
                    // Load allowed extensions
                    uploadAttachment.AllowedExtensions = ValidationHelper.GetString(FieldInfo.Settings["allowed_extensions"], "");
                }
                else
                {
                    // Use site settings
                    string siteName = CMSContext.CurrentSiteName;
                    uploadAttachment.AllowedExtensions = SettingsKeyProvider.GetStringValue(siteName + ".CMSUploadExtensions");
                }
                uploadAttachment.ReloadData();
            }

            // Initialize library permissions link
            imgPermissions.ImageUrl = PermissionsIconImageUrl;
            imgPermissions.AlternateText = GetString("documentlibrary.librarypermissions");

            // Check permissions, for group document library also group administrator can modify the permissions
            bool hasModifyPermission = (CurrentUser.IsAuthorizedPerDocument(LibraryNode, new NodePermissionsEnum[] { NodePermissionsEnum.Read, NodePermissionsEnum.ModifyPermissions }) == AuthorizationResultEnum.Allowed) || IsGroupAdmin;
            lblPermissions.Visible = hasModifyPermission;
            pnlHeader.Visible = hasModifyPermission || uploadVisible;
        }
    }


    protected override void OnPreRender(EventArgs e)
    {
        bool errorOccurred = !string.IsNullOrEmpty(lblError.Text);
        if (StopProcessing)
        {
            Visible = false;
        }
        else
        {
            pnlUpdate.Visible = !errorOccurred;

            btnPropertiesSaveClose.Visible = propertiesElem.AllowSave;
            btnLocalizeSaveClose.Visible = localizeElem.AllowSave;

            // Ensure correct dialog buttons
            if ((CurrentModal != null) && (((CurrentModal == mdlProperties) && (propertiesElem.Node != null)) || ((CurrentModal == mdlLocalize) && (localizeElem.Node != null))))
            {
                bool properties = (CurrentModal == mdlProperties);
                TreeNode currentNode = properties ? propertiesElem.Node : localizeElem.Node;

                // Get workflow information
                WorkflowInfo wi = WorkflowManager.GetNodeWorkflow(currentNode);
                if (wi != null)
                {
                    if (!wi.WorkflowAutoPublishChanges)
                    {
                        if (properties)
                        {
                            btnPropertiesSaveClose.Visible = false;
                        }
                        else
                        {
                            btnLocalizeSaveClose.Visible = false;
                        }
                    }
                }
            }
        }

        // Set up the captions
        btnClose1.ResourceString = btnLocalizeSaveClose.Visible ? "general.cancel" : "general.close";
        btnClose2.ResourceString = btnPropertiesSaveClose.Visible ? "general.cancel" : "general.close";

        if (Visible && pnlUpdate.Visible)
        {
            if (RequestHelper.IsPostBack() && (CurrentModal != null))
            {
                // Show popup after postback
                CurrentModal.Show();
            }
        }

        lblError.Visible = errorOccurred && (CMSContext.ViewMode == ViewModeEnum.Design);

        // Set viewstate
        pnlLocalizePopup.EnableViewState = (CurrentDialog == pnlLocalizePopup);
        pnlCopyPopup.EnableViewState = (CurrentDialog == pnlCopyPopup);
        pnlDeletePopup.EnableViewState = (CurrentDialog == pnlDeletePopup);
        pnlPermissionsPopup.EnableViewState = (CurrentDialog == pnlPermissionsPopup);
        pnlVersionsPopup.EnableViewState = (CurrentDialog == pnlVersionsPopup);
        pnlPropertiesPopup.EnableViewState = (CurrentDialog == pnlPropertiesPopup);
    }

    #endregion


    #region "Grid events"

    protected DataSet gridDocuments_OnDataReload(string completeWhere, string currentOrder, int currentTopN, string columns, int currentOffset, int currentPageSize, ref int totalRecords)
    {
        columns = SqlHelperClass.MergeColumns(DocumentHelper.GETDOCUMENTS_REQUIRED_COLUMNS, "NodeAlias, NodeGUID, DocumentName, DocumentCulture, DocumentModifiedWhen, Published, DocumentType, DocumentWorkflowStepID, DocumentCheckedOutByUserID, SiteName, NodeSiteID, NodeOwner, FileAttachment, FileDescription, DocumentName AS PublishedDocumentName, DocumentType AS PublishedDocumentType");
        string whereCondition = null;

        // Filter group documents
        whereCondition = (GroupID != 0) ? SqlHelperClass.AddWhereCondition(whereCondition, "(NodeGroupID=" + GroupID + ") OR (NodeGroupID IS NULL)") : SqlHelperClass.AddWhereCondition(whereCondition, "NodeGroupID IS NULL");

        // Retrieve documents
        DataSet documentsDataSet = DocumentHelper.GetDocuments(CurrentSite.SiteName, CMSContext.ResolveCurrentPath(LibraryPath) + "/%", null, CombineWithDefaultCulture, CMS_FILE, whereCondition, currentOrder, 1, false, currentTopN, columns, TreeProvider);

        NodePermissionsEnum[] permissionsToCheck = null;

        // Filter documents by permissions
        if (CheckPermissions)
        {
            documentsDataSet = TreeSecurityProvider.FilterDataSetByPermissions(documentsDataSet, NodePermissionsEnum.Read, CurrentUser);
            permissionsToCheck = new NodePermissionsEnum[] { NodePermissionsEnum.Modify, NodePermissionsEnum.ModifyPermissions, NodePermissionsEnum.Delete };
        }
        else
        {
            permissionsToCheck = new NodePermissionsEnum[] { NodePermissionsEnum.Modify, NodePermissionsEnum.ModifyPermissions, NodePermissionsEnum.Delete, NodePermissionsEnum.Read };
        }

        string cultures = PreferredCultureCode;
        if (CombineWithDefaultCulture)
        {
            string siteDefaultCulture = CultureHelper.GetDefaultCulture(CMSContext.CurrentSiteName);
            if (string.Compare(siteDefaultCulture, PreferredCultureCode, StringComparison.InvariantCultureIgnoreCase) != 0)
            {
                cultures += ";" + siteDefaultCulture;
            }
        }

        // Ensure permissions flags
        documentsDataSet = TreeSecurityProvider.FilterDataSetByPermissions(documentsDataSet, permissionsToCheck, CurrentUser, false, cultures);

        // Filter archived documents for users without modify permission
        if (!DataHelper.DataSourceIsEmpty(documentsDataSet))
        {
            DataTable dt = documentsDataSet.Tables[0];
            ArrayList deleteRows = new ArrayList();
            foreach (DataRow dr in dt.Rows)
            {
                // If the document is not published and user hasn't modify permission, remove it from data set
                bool isPublished = ValidationHelper.GetBoolean(dr["Published"], true);
                string documentCulture = ValidationHelper.GetString(dr["DocumentCulture"], null);
                bool hasModify = TreeSecurityProvider.CheckPermission(dr, NodePermissionsEnum.Modify, documentCulture);
                if (!isPublished && !hasModify)
                {
                    deleteRows.Add(dr);
                }
            }

            // Remove archived documents
            foreach (DataRow dr in deleteRows)
            {
                dt.Rows.Remove(dr);
            }
        }

        totalRecords = DataHelper.GetItemsCount(documentsDataSet);
        return documentsDataSet;
    }


    protected object gridDocuments_OnExternalDataBound(object sender, string sourceName, object parameter)
    {
        sourceName = sourceName.ToLower();
        DataRowView drv = null;
        bool modifyPermission = true;
        string documentCulture = null;

        switch (sourceName)
        {
            case "documentname":
                drv = parameter as DataRowView;
                string documentName = null;
                string encodedDocumentName = null;
                string documentType = null;
                documentCulture = ValidationHelper.GetString(drv.Row["DocumentCulture"], string.Empty);
                string alias = ValidationHelper.GetString(drv.Row["NodeAlias"], string.Empty);
                int nodeId = ValidationHelper.GetInteger(drv.Row["NodeID"], 0);
                int documentId = ValidationHelper.GetInteger(drv.Row["DocumentID"], 0);
                bool isLinked = (ValidationHelper.GetInteger(drv["NodeLinkedNodeID"], 0) != 0);
                string siteName = ValidationHelper.GetString(drv.Row["SiteName"], string.Empty);
                Guid nodeGuid = ValidationHelper.GetGuid(drv.Row["NodeGUID"], Guid.Empty);
                string fileDescription = ValidationHelper.GetString(drv.Row["FileDescription"], string.Empty);
                Guid fileAttachment = ValidationHelper.GetGuid(drv.Row["FileAttachment"], Guid.Empty);

                // Get permissions flags
                modifyPermission = TreeSecurityProvider.CheckPermission(drv.Row, NodePermissionsEnum.Modify, documentCulture);
                bool modifyCulturePermission = TreeSecurityProvider.CheckPermission(drv.Row, NodePermissionsEnum.Modify, PreferredCultureCode);
                bool deletePermission = TreeSecurityProvider.CheckPermission(drv.Row, NodePermissionsEnum.Delete, documentCulture);
                bool modifyPermissionsPermission = TreeSecurityProvider.CheckPermission(drv.Row, NodePermissionsEnum.ModifyPermissions, documentCulture);
                bool readPermission = TreeSecurityProvider.CheckPermission(drv.Row, NodePermissionsEnum.Read, documentCulture);

                if (modifyPermission)
                {
                    documentName = ValidationHelper.GetString(drv.Row["DocumentName"], string.Empty);
                    documentType = ValidationHelper.GetString(drv.Row["DocumentType"], string.Empty);
                }
                else
                {
                    documentName = ValidationHelper.GetString(drv.Row["PublishedDocumentName"], string.Empty);
                    documentType = ValidationHelper.GetString(drv.Row["PublishedDocumentType"], string.Empty);
                }

                encodedDocumentName = HTMLHelper.HTMLEncode(documentName);

                string fileTypeIcon = "<img class=\"Icon\" src=\"" + GetFileIconUrl(documentType, "List") + "\" alt=\"" + HTMLHelper.HTMLEncode(documentType) + "\" />";
                string flagIcon = null;
                if (documentCulture.ToLower() != PreferredCultureCode.ToLower())
                {
                    flagIcon = "<img class=\"Icon\" src=\"" + GetFlagIconUrl(documentCulture, "16x16") + "\" alt=\"" + HTMLHelper.HTMLEncode(documentCulture) + "\" />";
                }

                string menuParameter = ScriptHelper.GetString(nodeId + "|" + documentCulture);

                string attachmentName = encodedDocumentName;

                string toolTip = UIHelper.GetTooltipAttributes(null, 0, 0, null, null, null, fileDescription, null, 300);

                // Generate link to open document
                if (fileAttachment != Guid.Empty)
                {
                    // Get document URL
                    string attachmentUrl = CMSContext.ResolveUIUrl(AttachmentURLProvider.GetPermanentAttachmentUrl(nodeGuid, alias));
                    if (modifyPermission)
                    {
                        attachmentUrl = URLHelper.AddParameterToUrl(attachmentUrl, "latestfordocid", ValidationHelper.GetString(documentId, string.Empty));
                        attachmentUrl = URLHelper.AddParameterToUrl(attachmentUrl, "hash", ValidationHelper.GetHashString("d" + documentId));
                    }
                    attachmentUrl = URLHelper.UpdateParameterInUrl(attachmentUrl, "chset", Guid.NewGuid().ToString());

                    if (!string.IsNullOrEmpty(attachmentUrl))
                    {
                        attachmentName = "<a href=\"" + URLHelper.EncodeQueryString(attachmentUrl) + "\" " + toolTip + ">" + encodedDocumentName + "</a> ";
                    }
                }
                else
                {
                    attachmentName = "<span" + toolTip + ">" + encodedDocumentName + "</span>";
                }

                // Add linked flag
                if (isLinked)
                {
                    attachmentName += UIHelper.GetDocumentMarkImage(Page, DocumentMarkEnum.Link);
                }

                bool showContextMenu = readPermission && (modifyPermission || modifyPermissionsPermission || deletePermission || (IsAuthorizedToCreate && modifyCulturePermission));
                // Generate row with icons, hover action and context menu
                StringBuilder contextMenuString = new StringBuilder();
                contextMenuString.Append("<table class=\"Row\" ");
                if (showContextMenu)
                {
                    contextMenuString.Append("onmousemove=\"DL_Highlight(this, true);\" onmouseover=\"DL_Highlight(this, true);\" onmouseout=\"DL_Highlight(this, false);\" ");
                }
                contextMenuString.Append("style=\"border-collapse: collapse;width:100%;\" cellpadding=\"0\" cellspacing=\"0\">");
                contextMenuString.Append("  <tr>");

                if (showContextMenu)
                {
                    contextMenuString.Append(ContextMenuContainer.GetStartTag("libraryMenu_" + arrowContextMenu.ClientID, menuParameter, false, HtmlTextWriterTag.Td, "ArrowIcon", null));
                    contextMenuString.Append("&nbsp;");
                    contextMenuString.Append(ContextMenuContainer.GetEndTag(HtmlTextWriterTag.Td));
                }
                else
                {
                    contextMenuString.Append("<td class=\"NoIcon\">&nbsp;</td>");
                }

                if (showContextMenu)
                {
                    contextMenuString.Append(ContextMenuContainer.GetStartTag("libraryMenu_" + rowContextMenu.ClientID, menuParameter, true, HtmlTextWriterTag.Td, "FileTypeIcon", null));
                }
                else
                {
                    contextMenuString.Append("<td class=\"FileTypeIcon\">");
                }
                contextMenuString.Append(fileTypeIcon);
                contextMenuString.Append(ContextMenuContainer.GetEndTag(HtmlTextWriterTag.Td));

                if (showContextMenu)
                {
                    contextMenuString.Append(ContextMenuContainer.GetStartTag("libraryMenu_" + rowContextMenu.ClientID, menuParameter, true, HtmlTextWriterTag.Td, "RowContent", "width: 100%;"));
                }
                else
                {
                    contextMenuString.Append("<td class=\"RowContent\" style=\"width: 100%;\">");
                }
                contextMenuString.Append(attachmentName);
                contextMenuString.Append(ContextMenuContainer.GetEndTag(HtmlTextWriterTag.Td));

                if (!string.IsNullOrEmpty(flagIcon))
                {
                    contextMenuString.Append(ContextMenuContainer.GetStartTag("libraryMenu_" + rowContextMenu.ClientID, menuParameter, true, HtmlTextWriterTag.Td, "FlagIcon", null));
                    contextMenuString.Append(flagIcon);
                    contextMenuString.Append(ContextMenuContainer.GetEndTag(HtmlTextWriterTag.Td));
                }
                contextMenuString.Append("  </tr>");
                contextMenuString.Append("</table>");
                return contextMenuString.ToString();

            case "modifiedwhen":
            case "modifiedwhentooltip":
                if (string.IsNullOrEmpty(parameter.ToString()))
                {
                    return string.Empty;
                }
                else
                {
                    // Handle time zones
                    DateTime modifiedWhen = ValidationHelper.GetDateTime(parameter, DateTimeHelper.ZERO_TIME);

                    if (sourceName == "modifiedwhen")
                    {
                        if (IsLiveSite)
                        {
                            return CMSContext.ConvertDateTime(modifiedWhen, this);
                        }
                        else
                        {
                            return TimeZoneHelper.GetCurrentTimeZoneDateTimeString(modifiedWhen, CurrentUser, CurrentSite, out usedTimeZone);
                        }
                    }
                    else
                    {
                        if (!IsLiveSite)
                        {
                            if (TimeZoneHelper.TimeZonesEnabled() && (usedTimeZone == null))
                            {
                                TimeZoneHelper.GetCurrentTimeZoneDateTimeString(modifiedWhen, CurrentUser, CurrentSite, out usedTimeZone);
                            }
                            return TimeZoneHelper.GetGMTLongStringOffset(usedTimeZone);
                        }
                        else
                        {
                            return null;
                        }
                    }
                }

            case "status":
                string stepName = string.Empty;
                string toReturn = string.Empty;

                // Extract datarow
                drv = parameter as DataRowView;

                // Gain desired values from datarow view
                int workflowStepId = ValidationHelper.GetInteger(drv["DocumentWorkflowStepID"], 0);
                int checkedOutByUserId = ValidationHelper.GetInteger(drv["DocumentCheckedOutByUserID"], 0);
                documentCulture = ValidationHelper.GetString(drv.Row["DocumentCulture"], string.Empty);
                modifyPermission = TreeSecurityProvider.CheckPermission(drv.Row, NodePermissionsEnum.Modify, documentCulture);

                // Add 'checked out' icon
                if ((checkedOutByUserId > 0) && modifyPermission)
                {
                    toReturn = " " + UIHelper.GetDocumentMarkImage(Page, DocumentMarkEnum.CheckedOut);
                }
                // Add workflow step name
                if ((workflowStepId > 0) && modifyPermission)
                {
                    WorkflowStepInfo workflowStepInfo = WorkflowStepInfoProvider.GetWorkflowStepInfo(workflowStepId);
                    if (workflowStepInfo != null)
                    {
                        stepName = HTMLHelper.HTMLEncode(ResHelper.LocalizeString(workflowStepInfo.StepDisplayName));
                    }
                }
                else
                {
                    // Add dash if step name is not present
                    stepName = GetString("general.dash");
                }
                toReturn = stepName + toReturn;
                return toReturn;

        }
        return parameter;
    }

    #endregion


    #region "Methods"

    /// <summary>
    /// Refreshes the unigrid control.
    /// </summary>
    private void RefreshGrid()
    {
        gridDocuments.ReloadData();
    }


    /// <summary>
    /// Performs actions necessary to hide popup dialog.
    /// </summary>
    private void HideCurrentPopup()
    {
        if ((CurrentModal != null) && (CurrentDialog != null))
        {
            // Hide modal dialog
            CurrentModal.Hide();

            // Reset dialog control's viewstate and visibility
            CurrentDialog.EnableViewState = false;
            CurrentDialog.Visible = false;
        }

        // Reset identifiers
        CurrentModalID = null;
        CurrentDialogID = null;
    }


    /// <summary>
    /// Performs actions necessary to show the popup dialog.
    /// </summary>
    /// <param name="dialogControl">New dialog control</param>
    /// <param name="modalPopup">Modal control</param>
    private void ShowPopup(Control dialogControl, ModalPopupDialog modalPopup)
    {
        // Set new identifiers
        CurrentModalID = modalPopup.ID;
        CurrentDialogID = dialogControl.ID;

        if ((CurrentModal != null) && (CurrentDialog != null))
        {
            // Enable dialog control's viewstate and visibility
            CurrentDialog.EnableViewState = true;
            CurrentDialog.Visible = true;

            // Show modal popup
            CurrentModal.Show();
        }
    }

    #endregion


    #region "IPostBackEventHandler Members"

    public void RaisePostBackEvent(string eventArgument)
    {
        try
        {
            // Parse action to perform
            Action action = (Action)Enum.Parse(typeof(Action), eventArgument);
            // Parse action parameter
            string[] parameters = ValidationHelper.GetString(hdnParameter.Value, string.Empty).Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);

            // Check whether all parameters are present for parametrized popups
            if (parameters.Length == 2)
            {
                int nodeId = ValidationHelper.GetInteger(parameters[0], 0);
                string cultureCode = ValidationHelper.GetString(parameters[1], string.Empty);
                WorkflowStepInfo originalStep = null;
                WorkflowStepInfo nextStep = null;
                TreeNode currentNode = null;

                switch (action)
                {
                    case Action.Localize:
                        // Initialize localization dialog
                        currentNode = DocumentHelper.GetDocument(nodeId, cultureCode, TreeProvider);
                        localizeElem.NodeID = nodeId;
                        localizeElem.AlternativeFormName = DocumentForm;
                        localizeElem.CopyDefaultDataFromDocumentID = currentNode.DocumentID;
                        localizeElem.CheckPermissions = true;
                        localizeElem.FunctionsPrefix = ID + "_";
                        pnlLocalizePopup.Visible = true;
                        localizeElem.ReloadData(false);
                        ShowPopup(pnlLocalizePopup, mdlLocalize);
                        break;

                    case Action.Copy:
                        // Initialize copy dialog
                        copyElem.CopiedNodeID = nodeId;
                        copyElem.CopiedDocumentCulture = cultureCode;
                        copyElem.TargetNodeID = LibraryNode.NodeID;
                        copyElem.ReloadData(true);
                        copyElem.CancelButton.OnClientClick = Action.HidePopup + "_" + ClientID + "();return false;";
                        ShowPopup(pnlCopyPopup, mdlCopy);
                        break;

                    case Action.Delete:
                        // Initialize delete dialog
                        deleteElem.DeletedNodeID = nodeId;
                        deleteElem.DeletedDocumentCulture = cultureCode;
                        deleteElem.ReloadData(true);
                        deleteElem.CancelButton.OnClientClick = Action.HidePopup + "_" + ClientID + "();return false;";
                        ShowPopup(pnlDeletePopup, mdlDelete);
                        break;

                    case Action.Properties:
                        // Initialize properties dialog
                        propertiesElem.NodeID = nodeId;
                        propertiesElem.CultureCode = cultureCode;
                        propertiesElem.AlternativeFormName = DocumentForm;
                        propertiesElem.CheckPermissions = true;
                        propertiesElem.FunctionsPrefix = ID + "_";
                        pnlPropertiesPopup.Visible = true;
                        propertiesElem.ReloadData(false);
                        ShowPopup(pnlPropertiesPopup, mdlProperties);
                        break;

                    case Action.VersionHistory:
                        // Initialize version history dialog
                        versionsElem.NodeID = nodeId;
                        versionsElem.DocumentCulture = cultureCode;
                        versionsElem.SetupControl();
                        versionsElem.ReloadData();
                        ShowPopup(pnlVersionsPopup, mdlVersions);
                        break;

                    case Action.Permissions:
                        // Initialize permissions dialog
                        currentNode = DocumentHelper.GetDocument(nodeId, cultureCode, TreeProvider);
                        permissionsElem.Node = currentNode;
                        permissionsElem.DisplayButtons = false;
                        permissionsElem.ReloadData(true);
                        ReloadPermissions();
                        ShowPopup(pnlPermissionsPopup, mdlPermissions);
                        break;

                    case Action.CheckOut:
                        // Perform document checkout
                        currentNode = DocumentHelper.GetDocument(nodeId, cultureCode, TreeProvider);
                        VersionManager.CheckOut(currentNode);
                        RefreshGrid();
                        break;

                    case Action.CheckIn:
                        // Perform document checkin
                        currentNode = DocumentHelper.GetDocument(nodeId, cultureCode, TreeProvider);
                        VersionManager.CheckIn(currentNode, null, null);
                        RefreshGrid();
                        break;

                    case Action.UndoCheckout:
                        // Perform undo checkout on document
                        currentNode = DocumentHelper.GetDocument(nodeId, cultureCode, TreeProvider);
                        VersionManager.UndoCheckOut(currentNode);
                        RefreshGrid();
                        break;

                    case Action.SubmitToApproval:
                        currentNode = DocumentHelper.GetDocument(nodeId, cultureCode, TreeProvider);

                        // Get original step
                        originalStep = WorkflowManager.GetStepInfo(currentNode);

                        // Approve document
                        nextStep = WorkflowManager.MoveToNextStep(currentNode, null);

                        // Send workflow e-mails
                        if (SendWorkflowEmails)
                        {
                            if ((nextStep == null) || (nextStep.StepName.ToLower() == "published"))
                            {
                                // Publish e-mails
                                WorkflowManager.SendWorkflowEmails(currentNode, CurrentUser, originalStep, nextStep, WorkflowActionEnum.Published, null);
                            }
                            else
                            {
                                // Approve e-mails
                                WorkflowManager.SendWorkflowEmails(currentNode, CurrentUser, originalStep, nextStep, WorkflowActionEnum.Approved, null);
                            }
                        }

                        RefreshGrid();
                        break;

                    case Action.Reject:
                        currentNode = DocumentHelper.GetDocument(nodeId, cultureCode, TreeProvider);

                        // Get original step
                        originalStep = WorkflowManager.GetStepInfo(currentNode);

                        // Reject document
                        WorkflowStepInfo previousStep = WorkflowManager.MoveToPreviousStep(currentNode, null);

                        // Send workflow e-mails
                        if (SendWorkflowEmails)
                        {
                            WorkflowManager.SendWorkflowEmails(currentNode, CurrentUser, originalStep, previousStep, WorkflowActionEnum.Rejected, null);
                        }

                        RefreshGrid();
                        break;

                    case Action.Archive:
                        currentNode = DocumentHelper.GetDocument(nodeId, cultureCode, TreeProvider);

                        // Get original step
                        originalStep = WorkflowManager.GetStepInfo(currentNode);

                        // Archive document
                        nextStep = WorkflowManager.ArchiveDocument(currentNode, null);


                        // Send workflow e-mails
                        if (SendWorkflowEmails)
                        {
                            WorkflowManager.SendWorkflowEmails(currentNode, CurrentUser, originalStep, nextStep, WorkflowActionEnum.Archived, null);
                        }

                        RefreshGrid();
                        break;

                    case Action.WebDAVRefresh:
                        currentNode = DocumentHelper.GetDocument(nodeId, cultureCode, TreeProvider);
                        WorkflowInfo wi = WorkflowManager.GetNodeWorkflow(currentNode);

                        // Check if document uses workflow  and check-out/check-in is disabled
                        if ((wi != null) && !wi.UseCheckInCheckOut(CurrentSite.SiteName) && !wi.WorkflowAutoPublishChanges)
                        {
                            // Check permission to modify document
                            if (CMSContext.CurrentUser.IsAuthorizedPerDocument(currentNode, NodePermissionsEnum.Modify) == AuthorizationResultEnum.Allowed)
                            {
                                // Get current step 
                                WorkflowStepInfo currentStep = WorkflowManager.GetStepInfo(currentNode);

                                string currentStepName = currentStep.StepName.ToLower();

                                // Check if document uses workflow and step is 'published' or 'archived'
                                if ((currentStepName == "published") || (currentStepName == "archived"))
                                {
                                    // Get the first step 'Edit'
                                    WorkflowStepInfo firstStep = WorkflowManager.GetFirstWorkflowStep(currentNode, wi);
                                    // Move to step 'Edit'
                                    WorkflowManager.MoveToSpecificStep(currentNode, firstStep, null);

                                    // Refresh grid
                                    RefreshGrid();
                                }
                            }
                        }
                        break;
                }
            }
            // General actions
            switch (action)
            {
                case Action.RefreshGrid:
                    // Refresh unigrid
                    RefreshGrid();
                    break;

                case Action.HidePopup:
                    // Find and hide current popup
                    HideCurrentPopup();
                    break;
            }
        }
        catch (Exception ex)
        {
            AddAlert(GetString("general.erroroccurred") + " " + ex.Message);
        }
    }

    #endregion


    #region "Control events"

    protected void btnClose_Click(object sender, EventArgs e)
    {
        if ((CurrentModal != null) && (CurrentModal == mdlPermissions))
        {
            // Refresh header
            pnlUpdateHeader.Update();
        }
        // Hide current popup dialog
        HideCurrentPopup();
    }


    protected void btnSavePermissions_Click(object sender, EventArgs e)
    {
        // Save permissions
        permissionsElem.Save();
        ReloadPermissions();
    }


    protected void lnkPermissions_Click(object sender, EventArgs e)
    {
        // Display library permissions dialog
        permissionsElem.Node = LibraryNode;
        permissionsElem.GroupID = GroupID;
        permissionsElem.ReloadData(true);
        permissionsElem.DisplayButtons = false;
        ReloadPermissions();
        ShowPopup(pnlPermissionsPopup, mdlPermissions);
    }


    protected void btnPropertiesSaveClose_Click(object sender, EventArgs e)
    {
        if (propertiesElem.SaveDocument())
        {
            RefreshGrid();
            HideCurrentPopup();
        }
    }


    protected void btnLocalizeSaveClose_Click(object sender, EventArgs e)
    {
        if (localizeElem.SaveDocument())
        {
            RefreshGrid();
            HideCurrentPopup();
        }
    }

    #endregion


    #region "Other methods"

    private void ReloadPermissions()
    {
        btnSavePermissions.Enabled = (CurrentUser.IsAuthorizedPerDocument(permissionsElem.Node, new NodePermissionsEnum[] { NodePermissionsEnum.Read, NodePermissionsEnum.ModifyPermissions }) == AuthorizationResultEnum.Allowed) || IsGroupAdmin;
    }


    private void AddAlert(string message)
    {
        AddScript(ScriptHelper.GetAlertScript(message));
    }


    private void AddScript(string script)
    {
        ScriptHelper.RegisterStartupScript(this, typeof(string), script.GetHashCode().ToString(), script);
    }

    #endregion
}
