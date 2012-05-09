using System;
using System.Web.UI;
using System.Collections;
using System.Data;

using CMS.UIControls;
using CMS.GlobalHelper;
using CMS.SettingsProvider;
using CMS.ExtendedControls;
using CMS.SiteProvider;
using CMS.CMSHelper;
using CMS.TreeEngine;
using CMS.WorkflowEngine;
using CMS.IO;

using TreeNode = CMS.TreeEngine.TreeNode;

public partial class CMSModules_Content_Controls_Dialogs_LinkMediaSelector_LinkMediaSelector : LinkMediaSelector
{
    #region "Constants"

    private const string NODE_COLUMNS = "ClassDisplayName, AttachmentName, AttachmentTitle, AttachmentDescription, AttachmentExtension, AttachmentImageWidth, AttachmentImageHeight, NodeSiteID, SiteName, NodeGUID, DocumentUrlPath, NodeAlias, NodeAliasPath, AttachmentGUID, AttachmentID, DocumentName, AttachmentSize, NodeClassID, DocumentModifiedWhen, NodeACLID, NodeChildNodesCount, DocumentCheckedOutVersionHistoryID, NodeOwner, DocumentExtensions, ClassName, DocumentLastVersionName, DocumentType, DocumentLastVersionType";

    #endregion


    #region "Private variables"

    // Content variables
    private int mSiteID = 0;
    private int mNodeID = 0;

    private bool mIsAction = false;

    private TreeNode mTreeNodeObj = null;
    private AttachmentManager mAttachmentManager = null;
    private AttachmentInfo mCurrentAttachment = null;

    #endregion


    #region "Private properties"

    /// <summary>
    /// Gets or sets last searched value.
    /// </summary>
    private string LastSearchedValue
    {
        get
        {
            return hdnLastSearchedValue.Value;
        }
        set
        {
            hdnLastSearchedValue.Value = value;
        }
    }


    /// <summary>
    /// Gets current action name.
    /// </summary>
    private string CurrentAction
    {
        get
        {
            return hdnAction.Value.ToLower().Trim();
        }
        set
        {
            hdnAction.Value = value;
        }
    }


    /// <summary>
    /// Gets current action argument value.
    /// </summary>
    private string CurrentArgument
    {
        get
        {
            return hdnArgument.Value;
        }
    }


    /// <summary>
    /// Returns current properties (according to OutputFormat).
    /// </summary>
    protected override ItemProperties Properties
    {
        get
        {
            switch (Config.OutputFormat)
            {
                case OutputFormatEnum.HTMLMedia:
                    return htmlMediaProp;

                case OutputFormatEnum.HTMLLink:
                    return htmlLinkProp;

                case OutputFormatEnum.BBMedia:
                    return bbMediaProp;

                case OutputFormatEnum.BBLink:
                    return bbLinkProp;

                case OutputFormatEnum.NodeGUID:
                    return nodeGuidProp;

                default:
                    if ((Config.CustomFormatCode == "copy") || (Config.CustomFormatCode == "move") || (Config.CustomFormatCode == "link") || (Config.CustomFormatCode == "linkdoc"))
                    {
                        return docCopyMoveProp;
                    }
                    return urlProp;
            }
        }
    }


    /// <summary>
    /// Update panel where properties control resides.
    /// </summary>
    protected override UpdatePanel PropertiesUpdatePanel
    {
        get
        {
            return pnlUpdateProperties;
        }
    }


    /// <summary>
    /// Gets ID of the site files are being displayed for.
    /// </summary>
    private int SiteID
    {
        get
        {
            if (mSiteID == 0)
            {
                mSiteID = siteSelector.SiteID;
            }
            return mSiteID;
        }
        set
        {
            mSiteID = value;
        }
    }


    /// <summary>
    /// Gets name of the site files are being displayed for.
    /// </summary>
    private string SiteName
    {
        get
        {
            return siteSelector.SiteName;
        }
        set
        {
            siteSelector.SiteName = value;
        }
    }


    /// <summary>
    /// Gets or sets ID of the node selected in the content tree.
    /// </summary>
    private int NodeID
    {
        get
        {
            if (mNodeID == 0)
            {
                mNodeID = ValidationHelper.GetInteger(hdnLastNodeSlected.Value, 0);
            }
            return mNodeID;
        }
        set
        {
            // Set node only if changed
            if (mNodeID != value)
            {
                mNodeID = value;
                hdnLastNodeSlected.Value = value.ToString();
                mTreeNodeObj = null;
            }
        }
    }


    /// <summary>
    /// Indicates whether the attachments are temporary.
    /// </summary>
    private bool AttachmentsAreTemporary
    {
        get
        {
            return ((Config.AttachmentFormGUID != Guid.Empty) && (Config.AttachmentDocumentID == 0));
        }
    }


    /// <summary>
    /// Gets the node attachments are related to.
    /// </summary>
    private TreeNode TreeNodeObj
    {
        get
        {
            if (mTreeNodeObj == null)
            {
                // Content tab
                if (SourceType == MediaSourceEnum.Content)
                {
                    if (NodeID > 0)
                    {
                        TreeProvider tree = new TreeProvider(CMSContext.CurrentUser)
                        {
                            CombineWithDefaultCulture = true
                        };
                        mTreeNodeObj = DocumentHelper.GetDocument(NodeID, TreeProvider.ALL_CULTURES, true, tree);
                    }
                }
                // Attachments tab
                else if (!AttachmentsAreTemporary)
                {
                    TreeProvider tree = new TreeProvider(CMSContext.CurrentUser);
                    mTreeNodeObj = DocumentHelper.GetDocument(Config.AttachmentDocumentID, tree);
                }

                mediaView.TreeNodeObj = mTreeNodeObj;
            }
            return mTreeNodeObj;
        }
        set
        {
            mTreeNodeObj = value;
        }
    }


    /// <summary>
    /// Gets manager object for manipulating with attachments.
    /// </summary>
    private AttachmentManager AttachmentManager
    {
        get
        {
            if (mAttachmentManager == null)
            {
                mAttachmentManager = new AttachmentManager();
            }
            return mAttachmentManager;
        }
    }


    /// <summary>
    /// Gets or sets GUID of the recently selected attachment.
    /// </summary>
    private Guid LastAttachmentGuid
    {
        get
        {
            return ValidationHelper.GetGuid(ViewState["LastAttachmentGuid"], Guid.Empty);
        }
        set
        {
            ViewState["LastAttachmentGuid"] = value;
        }
    }


    /// <summary>
    /// Currently selected item.
    /// </summary>
    private AttachmentInfo CurrentAttachmentInfo
    {
        get
        {
            return mCurrentAttachment;
        }
        set
        {
            mCurrentAttachment = value;
        }
    }


    /// <summary>
    /// Indicates whether the asynchronous postback occurs on the page.
    /// </summary>
    private bool IsAsyncPostback
    {
        get
        {
            return ScriptManager.GetCurrent(Page).IsInAsyncPostBack;
        }
    }


    /// <summary>
    /// Indicates whether the post back is result of some hidden action.
    /// </summary>
    private bool IsAction
    {
        get
        {
            return mIsAction;
        }
        set
        {
            mIsAction = value;
        }
    }


    /// <summary>
    /// Indicates if full listing mode is enabled. This mode enables navigation to child and parent folders/documents from current view.
    /// </summary>
    private bool IsFullListingMode
    {
        get
        {
            return mediaView.IsFullListingMode;
        }
        set
        {
            mediaView.IsFullListingMode = value;
        }
    }


    /// <summary>
    /// Gets or sets selected item to colorize.
    /// </summary>
    private Guid ItemToColorize
    {
        get
        {
            return ValidationHelper.GetGuid(ViewState["ItemToColorize"], Guid.Empty);
        }
        set
        {
            ViewState["ItemToColorize"] = value;
        }
    }


    /// <summary>
    /// Gets or sets ID of the node reflecting new root specified by starting path.
    /// </summary>
    private int StartingPathNodeID
    {
        get
        {
            return ValidationHelper.GetInteger(ViewState["StartingPathNodeID"], 0);
        }
        set
        {
            ViewState["StartingPathNodeID"] = value;
        }
    }


    /// <summary>
    /// Indicates if properties are displayed in full height mode.
    /// </summary>
    private bool IsFullDisplay
    {
        get
        {
            return ValidationHelper.GetBoolean(ViewState["IsFullDisplay"], false);
        }
        set
        {
            ViewState["IsFullDisplay"] = value;
        }
    }


    /// <summary>
    /// Indicates whether the control is displayed as part of the copy/move dialog.
    /// </summary>
    private bool IsCopyMoveLinkDialog
    {
        get
        {
            switch (Config.CustomFormatCode)
            {
                case "copy":
                case "move":
                case "link":
                case "linkdoc":
                case "selectpath":
                case "relationship":
                    return true;

                default:
                    return false;
            }
        }
    }


    /// <summary>
    /// Indicates wether the control output is link.
    /// </summary>
    private bool IsLinkOutput
    {
        get
        {
            return ((Config.OutputFormat == OutputFormatEnum.HTMLLink) || (Config.OutputFormat == OutputFormatEnum.BBLink) || (Config.OutputFormat == OutputFormatEnum.URL));
        }
    }


    /// <summary>
    /// Indicates whether the folder was already selected for the current site.
    /// </summary>
    private bool FolderNotSelectedYet
    {
        get
        {
            return ValidationHelper.GetBoolean(ViewState["FolderNotSelectedYet"], true);
        }
        set
        {
            ViewState["FolderNotSelectedYet"] = value;
        }
    }

    #endregion


    #region "Page events"

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        menuElem.Config = Config;
        mediaView.Config = Config;

        // Get source type according URL parameters
        SourceType = CMSDialogHelper.GetMediaSource(QueryHelper.GetString("source", "attachments"));
        mediaView.OutputFormat = Config.OutputFormat;

        // All sites for copy, move and link dialog
        siteSelector.OnlyRunningSites = !IsCopyMoveLinkDialog;
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        // High-light item being edited
        if (ItemToColorize != Guid.Empty)
        {
            ColorizeRow(ItemToColorize.ToString());
        }

        if (!URLHelper.IsPostback() && (siteSelector.DropDownSingleSelect.SelectedItem == null))
        {
            EnsureLoadedSite();
        }

        // Handle empty site selector
        HandleSiteEmpty();

        // Display info on listing more content
        mediaView.ShowParentButton = (mediaView.ShowParentButton && (StartingPathNodeID != NodeID) ? IsFullListingMode : false);
        if (!IsCopyMoveLinkDialog && IsFullListingMode && (TreeNodeObj != null))
        {
            string closeLink = String.Format("<span class=\"ListingClose\" style=\"cursor: pointer;\" onclick=\"SetAction('closelisting', ''); RaiseHiddenPostBack(); return false;\">{0}</span>", GetString("general.close"));
            string docNamePath = String.Format("<span class=\"ListingPath\">{0}</span>", TreeNodeObj.DocumentNamePath);

            string listingMsg = string.Format(GetString("dialogs.content.listingInfo"), docNamePath, closeLink);
            mediaView.DisplayListingInfo(listingMsg);
        }

        if (IsCopyMoveLinkDialog)
        {
            DisplayMediaElements();
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!StopProcessing)
        {
            CheckPermissions();

            SetupProperties();

            InitializeDialogs();

            SetupControls();

            EnsureLoadedData();
        }
        else
        {
            siteSelector.StopProcessing = true;
            Visible = false;
        }

        ScriptHelper.RegisterWOpenerScript(Page);
    }

    #endregion


    #region "Inherited methods"

    /// <summary>
    /// Initializes its properties according to the URL parameters.
    /// </summary>
    public void InitFromQueryString()
    {
        switch (Config.OutputFormat)
        {
            case OutputFormatEnum.HTMLMedia:
                SelectableContent = SelectableContentEnum.OnlyMedia;
                break;

            case OutputFormatEnum.HTMLLink:
                SelectableContent = SelectableContentEnum.AllContent;
                break;

            case OutputFormatEnum.BBMedia:
                SelectableContent = SelectableContentEnum.OnlyImages;
                break;

            case OutputFormatEnum.BBLink:
                SelectableContent = SelectableContentEnum.AllContent;
                break;

            case OutputFormatEnum.URL:
            case OutputFormatEnum.NodeGUID:
                string content = QueryHelper.GetString("content", "");
                SelectableContent = CMSDialogHelper.GetSelectableContent(content);
                break;
        }
    }


    /// <summary>
    /// Returns selected item parameters as name-value collection.
    /// </summary>
    public void GetSelectedItem()
    {
        // Clear unused information from session
        ClearSelectedItemInfo();
        ClearActionElems();
        if (Properties.Validate())
        {
            // Store tab information in the user's dialogs configuration
            StoreDialogsConfiguration();

            // Get selected item information
            Hashtable properties = Properties.GetItemProperties();

            // Get JavaScript for inserting the item
            string script = GetInsertItem(properties);
            if (!string.IsNullOrEmpty(script))
            {
                ScriptHelper.RegisterStartupScript(Page, typeof(Page), "insertItemScript", ScriptHelper.GetScript(script));
            }
            if ((Config.CustomFormatCode.ToLower() == "copy") || (Config.CustomFormatCode.ToLower() == "move") || (Config.CustomFormatCode.ToLower() == "link") || (Config.CustomFormatCode.ToLower() == "linkdoc"))
            {
                // Reload the iframe
                pnlUpdateProperties.Update();
            }
        }
        else
        {
            // Display error message
            pnlUpdateProperties.Update();
        }
    }

    #endregion


    #region "Dialog configuration"

    /// <summary>
    /// Stores current tab's configuration for the user.
    /// </summary>
    private void StoreDialogsConfiguration()
    {
        UserInfo ui = UserInfoProvider.GetUserInfo(CMSContext.CurrentUser.UserID);
        if (ui != null)
        {
            // Store configuration based on the current tab
            switch (SourceType)
            {
                case MediaSourceEnum.Content:
                    // Actualize configuration
                    ui.UserSettings.UserDialogsConfiguration["content.sitename"] = SiteName;
                    ui.UserSettings.UserDialogsConfiguration["content.path"] = GetContentPath(NodeID);
                    ui.UserSettings.UserDialogsConfiguration["content.viewmode"] = CMSDialogHelper.GetDialogViewMode(menuElem.SelectedViewMode);
                    break;

                case MediaSourceEnum.DocumentAttachments:
                    // Actualize configuration
                    ui.UserSettings.UserDialogsConfiguration["attachments.viewmode"] = CMSDialogHelper.GetDialogViewMode(menuElem.SelectedViewMode);
                    break;
            }
            ui.UserSettings.UserDialogsConfiguration["selectedtab"] = CMSDialogHelper.GetMediaSource(SourceType);

            UserInfoProvider.SetUserInfo(ui);
        }
    }


    /// <summary>
    /// Initializes dialogs according URL configuration, selected item or user configuration.
    /// </summary>
    private void InitializeDialogs()
    {
        if (!URLHelper.IsPostback())
        {
            LoadDialogConfiguration();

            // Item is selected in the editor
            if (MediaSource != null)
            {
                LoadItemConfiguration();
            }
            else if (!IsCopyMoveLinkDialog)
            {
                LoadUserConfiguration();
            }
        }
    }


    /// <summary>
    /// Loads dialogs according configuration coming from the URL.
    /// </summary>
    private void LoadDialogConfiguration()
    {
        if (SourceType == MediaSourceEnum.Content)
        {
            // Initialize site selector
            siteSelector.StopProcessing = false;
            string siteWhereCondition = GetSiteWhere();
            siteSelector.UniSelector.WhereCondition = siteWhereCondition;

            if (!string.IsNullOrEmpty(Config.ContentSelectedSite))
            {
                contentTree.SiteName = Config.ContentSelectedSite;
                siteSelector.SiteName = Config.ContentSelectedSite;
            }
            else
            {
                // Select default site
                string siteName = CMSContext.CurrentSiteName;
                // Try select current site
                if (!string.IsNullOrEmpty(siteName))
                {
                    contentTree.SiteName = siteName;
                    siteSelector.SiteName = siteName;
                }
                else
                {
                    // Select first site from users sites
                    DataSet ds = SiteInfoProvider.GetSites(siteWhereCondition, "SiteDisplayName");
                    if (!DataHelper.DataSourceIsEmpty(ds))
                    {
                        siteName = ValidationHelper.GetString(ds.Tables[0].Rows[0]["SiteName"], String.Empty);
                        if (!String.IsNullOrEmpty(siteName))
                        {
                            contentTree.SiteName = siteName;
                            siteSelector.SiteName = siteName;
                        }
                    }
                }
            }

            pnlUpdateSelectors.Update();
        }
    }


    /// <summary>
    /// Gets WHERE condition for available sites according field configuration.
    /// </summary>
    private string GetSiteWhere()
    {
        string siteName = null;

        // First check configuration
        if (Config.ContentSites == AvailableSitesEnum.OnlySingleSite)
        {
            siteName = Config.ContentSelectedSite;
        }
        else if (Config.ContentSites == AvailableSitesEnum.OnlyCurrentSite)
        {
            siteName = CMSContext.CurrentSiteName;
        }

        string where = IsCopyMoveLinkDialog ? null : "SiteStatus = 'RUNNING'";
        if (!String.IsNullOrEmpty(siteName))
        {
            // Ensure that selected site is allways loaded (cancel the "only running" condition)
            where = SqlHelperClass.AddWhereCondition(where, String.Format("SiteName LIKE N'{0}'", siteName));
        }

        // Get only current user's sites
        if (!CMSContext.CurrentUser.IsGlobalAdministrator)
        {
            where = SqlHelperClass.AddWhereCondition(where, String.Format("SiteID IN (SELECT SiteID FROM CMS_UserSite WHERE UserID = {0})", CMSContext.CurrentUser.UserID));
        }

        return where;
    }


    /// <summary>
    /// Loads selected item parameters into the selector.
    /// </summary>
    public void LoadItemConfiguration()
    {
        if (MediaSource != null)
        {
            IsItemLoaded = true;

            switch (MediaSource.SourceType)
            {
                case MediaSourceEnum.Content:
                    siteSelector.SiteID = MediaSource.SiteID;
                    contentTree.SiteName = SiteName;

                    // Try to select node in the tree
                    if (MediaSource.NodeID > 0)
                    {
                        NodeID = MediaSource.NodeID;
                        contentTree.NodeID = NodeID;
                    }
                    break;

                default:
                    break;
            }

            // Reload HTML properties
            if (Config.OutputFormat == OutputFormatEnum.HTMLMedia)
            {
                // Force media properties control to load selected item
                htmlMediaProp.ViewMode = MediaSource.MediaType;
            }

            // Display properties in full size
            if (SourceType == MediaSourceEnum.Content)
            {
                DisplayFull();
            }

            htmlMediaProp.HistoryID = MediaSource.HistoryID;
        }

        // Load properties
        Properties.LoadItemProperties(Parameters);
        pnlUpdateProperties.Update();

        // Remember item to colorize
        ItemToColorize = (SourceType == MediaSourceEnum.Content) ? MediaSource.NodeGuid : MediaSource.AttachmentGuid;
        LastAttachmentGuid = ItemToColorize;

        ClearSelectedItemInfo();
    }


    /// <summary>
    /// Loads dialogs according user's configuration.
    /// </summary>
    private void LoadUserConfiguration()
    {
        CurrentUserInfo currentUser = CMSContext.CurrentUser;
        if ((currentUser.UserSettings.UserDialogsConfiguration != null) &&
            (currentUser.UserSettings.UserDialogsConfiguration.ColumnNames != null))
        {
            XmlData dialogConfig = currentUser.UserSettings.UserDialogsConfiguration;

            string siteName = "";
            string aliasPath = "";
            string viewMode = "";

            // Store configuration based on the current tab
            switch (SourceType)
            {
                case MediaSourceEnum.Content:
                    siteName = (dialogConfig.ContainsColumn("content.sitename") ? (string)dialogConfig["content.sitename"] : "");
                    aliasPath = (dialogConfig.ContainsColumn("content.path") ? (string)dialogConfig["content.path"] : "");
                    viewMode = (dialogConfig.ContainsColumn("content.viewmode") ? (string)dialogConfig["content.viewmode"] : "");
                    break;

                case MediaSourceEnum.DocumentAttachments:
                    viewMode = (dialogConfig.ContainsColumn("attachments.viewmode") ? (string)dialogConfig["attachments.viewmode"] : "");
                    break;
            }

            // Update dialog configuration (only if ContentSelectedSite is not set)
            if (!string.IsNullOrEmpty(siteName) && string.IsNullOrEmpty(Config.ContentSelectedSite))
            {
                // Check if site from user settings exists and is running
                SiteInfo si = SiteInfoProvider.GetSiteInfo(siteName);
                if ((si == null) || (si.Status == SiteStatusEnum.Stopped) || !currentUser.IsInSite(siteName))
                {
                    // If not, use current site
                    siteName = CMSContext.CurrentSiteName;
                }

                siteSelector.SiteName = siteName;
            }

            if (!string.IsNullOrEmpty(aliasPath))
            {
                NodeID = (aliasPath.StartsWith(Config.ContentStartingPath.ToLower()) ? GetContentNodeId(aliasPath) : GetContentNodeId(Config.ContentStartingPath));

                // Initialize root node
                if (NodeID == 0)
                {
                    NodeID = GetContentNodeId("/");
                    mediaView.ShowParentButton = false;
                }

                // Select and expand node
                contentTree.NodeID = NodeID;
                contentTree.ExpandNodeID = NodeID;
            }
            else if (SourceType == MediaSourceEnum.Content)
            {
                SelectRootNode();
            }

            if (!string.IsNullOrEmpty(viewMode))
            {
                menuElem.SelectedViewMode = CMSDialogHelper.GetDialogViewMode(viewMode);
            }
        }
        else
        {
            // Initialize site selector
            if (!string.IsNullOrEmpty(Config.ContentSelectedSite))
            {
                siteSelector.SiteName = Config.ContentSelectedSite;
            }
            else
            {
                siteSelector.SiteID = CMSContext.CurrentSiteID;
            }
            SelectRootNode();
        }
    }

    #endregion


    #region "Private methods"

    /// <summary>
    /// Performs necessary permissions check.
    /// </summary>
    private void CheckPermissions()
    {
        // Check 'READ' permission for the specific document if attachments are being created
        if ((SourceType == MediaSourceEnum.DocumentAttachments) && (!AttachmentsAreTemporary))
        {
            if (CMSContext.CurrentUser.IsAuthorizedPerDocument(TreeNodeObj, NodePermissionsEnum.Read) != AuthorizationResultEnum.Allowed)
            {
                string errMsg = string.Format(GetString("cmsdesk.notauthorizedtoreaddocument"), TreeNodeObj.DocumentName);

                // Redirect to access denied page
                AccessDenied(errMsg);
            }
        }
    }


    /// <summary>
    /// Ensures that required data are displayed.
    /// </summary>
    private void EnsureLoadedData()
    {
        bool processLoad = true;
        bool isLink = (Config.OutputFormat == OutputFormatEnum.BBLink || Config.OutputFormat == OutputFormatEnum.HTMLLink) ||
            (Config.OutputFormat == OutputFormatEnum.URL && SelectableContent == SelectableContentEnum.AllContent);

        // If all content is selctable do not select root by default - leave selection empty
        if ((SelectableContent == SelectableContentEnum.AllContent) && !isLink && !URLHelper.IsPostback())
        {
            // Even no file is selected by default load source for the Attachment tab
            processLoad = (SourceType == MediaSourceEnum.DocumentAttachments);

            NodeID = 0;
            contentTree.NodeID = NodeID;
            Properties.ClearProperties(true);
        }

        // Clear properties if link dialog is opened and no link is edited
        if (!URLHelper.IsPostback() && isLink && !IsItemLoaded)
        {
            Properties.ClearProperties(true);
        }

        // If no action takes place
        if ((CurrentAction == "") ||
            !(isLink && CurrentAction.Contains("edit")))
        {
            if (!CurrentAction.Contains("edit") && processLoad && !(!URLHelper.IsPostback() && processLoad))
            {
                LoadDataSource();
            }

            // Select folder comming from user/selected item configuration
            if (!URLHelper.IsPostback() && processLoad)
            {
                HandleFolderAction(NodeID.ToString(), false, false);
            }
        }
    }


    /// <summary>
    /// Ensures that loaded site is really the one selected in the drop-down list.
    /// </summary>
    private void EnsureLoadedSite()
    {
        if (SourceType != MediaSourceEnum.DocumentAttachments)
        {
            siteSelector.Reload(true);

            // Name of the site selected in the site DDL
            string siteName = "";

            int siteId = siteSelector.SiteID;
            if (siteId > 0)
            {
                SiteInfo si = SiteInfoProvider.GetSiteInfo(siteId);
                if (si != null)
                {
                    siteName = si.SiteName;
                }
            }

            if (siteName != SiteName)
            {
                SiteName = siteName;

                // Get site root by default
                TreeProvider tree = new TreeProvider(CMSContext.CurrentUser);
                if (tree != null)
                {
                    TreeNodeObj = tree.SelectSingleNode(SiteName, "/", null, false, "cms.root", null, null, 1, true, TreeProvider.SELECTNODES_REQUIRED_COLUMNS);
                    NodeID = TreeNodeObj.NodeID;

                    InitializeContentTree();

                    contentTree.NodeID = NodeID;
                    contentTree.ExpandNodeID = NodeID;

                    EnsureLoadedData();
                }
            }
        }
    }


    /// <summary>
    /// Makes sure that media elements aren't active while no folder is selected.
    /// </summary>
    private void DisplayMediaElements()
    {
        if (FolderNotSelectedYet)
        {
            // Hide media elemnents
            mediaView.StopProcessing = true;
            mediaView.Visible = false;

            // Disable New folder button
            ScriptHelper.RegisterStartupScript(Page, typeof(Page), "DisableNewFolderOnLoad", ScriptHelper.GetScript("if (window.DisableNewFolderBtn) { window.DisableNewFolderBtn(); }"));
        }
        else if (!mediaView.Visible)
        {
            mediaView.Visible = true;
            mediaView.Reload();
        }
    }


    /// <summary>
    /// Initializes properties controls.
    /// </summary>
    private void SetupProperties()
    {
        htmlLinkProp.Visible = false;
        htmlMediaProp.Visible = false;
        bbLinkProp.Visible = false;
        bbMediaProp.Visible = false;
        urlProp.Visible = false;
        docCopyMoveProp.Visible = false;

        Properties.Visible = true;

        htmlLinkProp.StopProcessing = !htmlLinkProp.Visible;
        htmlMediaProp.StopProcessing = !htmlMediaProp.Visible;
        bbLinkProp.StopProcessing = !bbLinkProp.Visible;
        bbMediaProp.StopProcessing = !bbMediaProp.Visible;
        urlProp.StopProcessing = !urlProp.Visible;
        nodeGuidProp.StopProcessing = !nodeGuidProp.Visible;
        docCopyMoveProp.StopProcessing = !docCopyMoveProp.Visible;

        Properties.Config = Config;
    }


    /// <summary>
    /// Initializes additional controls.
    /// </summary>
    private void SetupControls()
    {
        // Generate permanent URLs whenever node GUID output required        
        if (Config.OutputFormat != OutputFormatEnum.NodeGUID)
        {
            UsePermanentUrls = SettingsKeyProvider.GetBoolValue(CMSContext.CurrentSiteName + ".CMSUsePermanentURLs");
        }
        else
        {
            // Select current site and disable change
            siteSelector.SiteID = SiteID = CMSContext.CurrentSiteID;
            siteSelector.UserId = CMSContext.CurrentUser.UserID;
            siteSelector.Enabled = false;

        }

        if (SourceType != MediaSourceEnum.DocumentAttachments)
        {
            siteSelector.DropDownSingleSelect.AutoPostBack = true;
            siteSelector.UniSelector.OnSelectionChanged += UniSelector_OnSelectionChanged;
            siteSelector.DropDownSingleSelect.CssClass = "DialogSiteDropdown";
        }
        else
        {
            siteSelector.StopProcessing = true;
            pnlUpdateSelectors.Visible = false;
        }

        mediaView.UsePermanentUrls = UsePermanentUrls;
        // Set editor client id for properties
        Properties.EditorClientID = Config.EditorClientID;

        InitializeMenuElement();
        InitializeDesignScripts();

        mediaView.IsLiveSite = IsLiveSite;
        mediaView.SelectableContent = SelectableContent;
        mediaView.SourceType = SourceType;
        mediaView.ViewMode = menuElem.SelectedViewMode;
        mediaView.ResizeToHeight = Config.ResizeToHeight;
        mediaView.ResizeToMaxSideSize = Config.ResizeToMaxSideSize;
        mediaView.ResizeToWidth = Config.ResizeToWidth;
        mediaView.AtachmentNodeParentID = Config.AttachmentParentID;
        mediaView.ListReloadRequired += mediaView_ListReloadRequired;

        Properties.IsLiveSite = IsLiveSite;
        Properties.SourceType = SourceType;

        if (!IsAsyncPostback)
        {
            // Initialize scripts
            InitializeControlScripts();
            if (URLHelper.IsPostback() && (SourceType != MediaSourceEnum.DocumentAttachments))
            {
                UniSelector_OnSelectionChanged(this, null);
            }
        }

        // Based on the required source type perform setting of necessary controls
        if (SourceType == MediaSourceEnum.Content)
        {
            if (!IsAsyncPostback)
            {
                // Initialize content tree control
                InitializeContentTree();
            }
            else
            {
                contentTree.Visible = false;
                contentTree.StopProcessing = true;
            }
        }
        else
        {
            // Hide and disable content related controls
            HideContentControls();
        }

        // If folder was changed reset current page index for control displaying content
        switch (CurrentAction)
        {
            case "morecontentselect":
            case "contentselect":
            case "parentselect":
                ResetPageIndex();
                break;

            default:
                break;
        }
    }


    /// <summary>
    /// Initialize design jQuery scripts.
    /// </summary>
    private void InitializeDesignScripts()
    {
        ScriptHelper.RegisterStartupScript(Page, typeof(Page), "designScript", ScriptHelper.GetScript(@"
setTimeout('InitializeDesign();',200);
$j(window).unbind('resize').resize(function() { 
    InitializeDesign(); 
});"));
    }


    /// <summary>
    /// Initializes menu element.
    /// </summary>
    private void InitializeMenuElement()
    {
        // Let child controls now what the source type is
        menuElem.IsCopyMoveLinkDialog = IsCopyMoveLinkDialog;
        menuElem.DisplayMode = DisplayMode;
        menuElem.IsLiveSite = IsLiveSite;
        menuElem.SourceType = SourceType;
        menuElem.ResizeToHeight = Config.ResizeToHeight;
        menuElem.ResizeToMaxSideSize = Config.ResizeToMaxSideSize;
        menuElem.ResizeToWidth = Config.ResizeToWidth;

        // Based on the required source type perform setting of necessary controls
        if (SourceType == MediaSourceEnum.Content)
        {
            menuElem.NodeID = NodeID;
        }
        else
        {
            // Initialize menu element for attachments
            menuElem.DocumentID = Config.AttachmentDocumentID;
            menuElem.FormGUID = Config.AttachmentFormGUID;
            menuElem.ParentNodeID = Config.AttachmentParentID;
        }
        menuElem.UpdateViewMenu();
    }


    /// <summary>
    /// Initializes all the script required for communication between controls.
    /// </summary>
    private void InitializeControlScripts()
    {
        // Prepare for upload
        string refreshType = CMSDialogHelper.GetMediaSource(SourceType);
        string cmdName = (SourceType == MediaSourceEnum.DocumentAttachments) ? "attachment" : "content";

        ltlScript.Text = ScriptHelper.GetScript(String.Format(@"
function SetAction(action, argument) {{ 
    var hdnAction = document.getElementById('{0}');
    var hdnArgument = document.getElementById('{1}');
    if ((hdnAction != null) && (hdnArgument != null)) {{                             
        if (action != null) {{                                                       
            hdnAction.value = action;                                               
        }}                                                                           
        if (argument != null) {{                                                     
            hdnArgument.value = argument;                                           
        }}                                                                           
    }}                                                                               
}}
function InitRefresh_{2}(message, fullRefresh, refreshTree, itemInfo, action) {{
    if((message != null) && (message != ''))                                   
    {{                                                                          
        window.alert(message);                                                  
    }}                                                                          
    else                                                                       
    {{
        if(action == 'insert')
        {{
            SetAction('{3}created', itemInfo);
        }}
        else if(action == 'update')
        {{
            SetAction('{3}updated', itemInfo);
        }}                                                                               
        else if(action == 'refresh')
        {{
            SetAction('{3}edit', itemInfo);
        }}                                                                                                                                                                   
        RaiseHiddenPostBack();                                                  
    }}                                                                          
}}
function imageEdit_AttachmentRefresh(arg){{
    SetAction('attachmentedit', arg);
    RaiseHiddenPostBack();
}}
function imageEdit_ContentRefresh(arg){{
    SetAction('contentedit', arg);
    RaiseHiddenPostBack();
}}
function RaiseHiddenPostBack(){{
    {4};
}}
", hdnAction.ClientID, hdnArgument.ClientID, refreshType, cmdName, ControlsHelper.GetPostBackEventReference(hdnButton, "")));
    }


    /// <summary>
    /// Loads all files for the view control.
    /// </summary>
    private void LoadDataSource()
    {
        if (SourceType == MediaSourceEnum.Content)
        {
            LoadContentDataSource(LastSearchedValue);
        }
        else
        {
            LoadAttachmentsDataSource(LastSearchedValue);
        }
    }


    private void mediaView_ListReloadRequired()
    {
        LoadDataSource();
    }


    /// <summary>
    /// Performs actions necessary to select particular item from a list.
    /// </summary>
    private void SelectMediaItem(string argument)
    {
        if (!string.IsNullOrEmpty(argument))
        {
            Hashtable argTable = CMSModules_Content_Controls_Dialogs_LinkMediaSelector_MediaView.GetArgumentsTable(argument);
            if (argTable.Count >= 2)
            {
                // Get information from argument
                string name = argTable["name"].ToString();
                string ext = argTable["attachmentextension"].ToString();
                int imageWidth = ValidationHelper.GetInteger(argTable["attachmentimagewidth"], 0);
                int imageHeight = ValidationHelper.GetInteger(argTable["attachmentimageheight"], 0);
                int nodeID = ValidationHelper.GetInteger(argTable["nodeid"], NodeID);
                long size = ValidationHelper.GetLong(argTable["attachmentsize"], 0);
                string url = argTable["url"].ToString();
                string aliasPath = null;

                // Do not update properties when selecting recently edited image item
                bool avoidPropUpdate = false;

                // Remember last selected attachment GUID
                if (SourceType == MediaSourceEnum.DocumentAttachments)
                {
                    Guid attGuid = ValidationHelper.GetGuid(argTable["attachmentguid"], Guid.Empty);

                    avoidPropUpdate = (LastAttachmentGuid == attGuid);

                    LastAttachmentGuid = attGuid;
                    ItemToColorize = LastAttachmentGuid;
                }
                else
                {
                    aliasPath = argTable["nodealiaspath"].ToString();
                    Guid nodeAttGuid = ValidationHelper.GetGuid(argTable["attachmentguid"], Guid.Empty);
                    Properties.SiteDomainName = mediaView.SiteObj.DomainName;

                    avoidPropUpdate = (ItemToColorize == nodeAttGuid);

                    ItemToColorize = nodeAttGuid;
                    if (ItemToColorize == Guid.Empty)
                    {
                        ItemToColorize = ValidationHelper.GetGuid(argTable["nodeguid"], Guid.Empty);
                    }
                }

                avoidPropUpdate = (avoidPropUpdate && IsEditImage);
                if (!avoidPropUpdate)
                {
                    if (SourceType == MediaSourceEnum.DocumentAttachments)
                    {
                        int versionHistoryId = 0;

                        if (TreeNodeObj != null)
                        {
                            // Get the node workflow
                            WorkflowManager wm = new WorkflowManager(TreeNodeObj.TreeProvider);
                            WorkflowInfo wi = wm.GetNodeWorkflow(TreeNodeObj);
                            if (wi != null)
                            {
                                // Get the document version
                                versionHistoryId = TreeNodeObj.DocumentCheckedOutVersionHistoryID;
                            }
                        }

                        MediaItem item = InitializeMediaItem(name, ext, imageWidth, imageHeight, size, url, null, versionHistoryId, nodeID, aliasPath);

                        SelectMediaItem(item);
                    }
                    else
                    {
                        // Select item
                        SelectMediaItem(name, ext, imageWidth, imageHeight, size, url, null, nodeID, aliasPath);
                    }
                }
            }
        }
    }


    /// <summary>
    /// Performs actions necessary to select particular item from a list.
    /// </summary>
    private void SelectMediaItem(string docName, string url, string aliasPath)
    {
        SelectMediaItem(docName, null, 0, 0, 0, url, null, NodeID, aliasPath);
    }


    /// <summary>
    /// Selects root node of currently selected site.
    /// </summary>
    private void SelectRootNode()
    {
        // Reset selected node to root node
        NodeID = GetContentNodeId("/");
        contentTree.NodeID = NodeID;
        contentTree.ExpandNodeID = NodeID;
        mediaView.ShowParentButton = false;
    }


    /// <summary>
    /// Clears hidden control elements fo future use.
    /// </summary>
    private void ClearActionElems()
    {
        CurrentAction = "";
        hdnArgument.Value = "";
    }


    /// <summary>
    /// Displays properties in full size.
    /// </summary>
    private void DisplayFull()
    {
        if (divDialogView.Attributes["class"] == "DialogViewContent")
        {
            divDialogView.Attributes["class"] = "DialogElementHidden";
            divDialogResizer.Attributes["class"] = "DialogElementHidden";
            divDialogProperties.Attributes["class"] = "DialogPropertiesFullSize";

            if (IsFullDisplay)
            {
                pnlUpdateView.Update();
                pnlUpdateProperties.Update();
            }
            else
            {
                pnlUpdateContent.Update();
                IsFullDisplay = true;
            }
        }
    }


    /// <summary>
    /// Displays properties in default size.
    /// </summary>
    private void DisplayNormal()
    {
        if (divDialogView.Attributes["class"] == "DialogElementHidden")
        {
            divDialogView.Attributes["class"] = "DialogViewContent";
            divDialogResizer.Attributes["class"] = "DialogResizerVLine";
            divDialogProperties.Attributes["class"] = "DialogProperties";

            if (IsFullDisplay)
            {
                pnlUpdateContent.Update();
                IsFullDisplay = false;
            }
            else
            {
                pnlUpdateView.Update();
                pnlUpdateProperties.Update();
            }
        }
    }


    /// <summary>
    /// Ensures that filter is no more applied.
    /// </summary>
    private void ResetSearchFilter()
    {
        mediaView.ResetSearch();
        LastSearchedValue = "";
    }


    /// <summary>
    /// Ensures first page is displayed in the control displaying the content.
    /// </summary>
    private void ResetPageIndex()
    {
        mediaView.ResetPageIndex();
    }

    #endregion


    #region "Content methods"

    /// <summary>
    /// Hides and disables content related controls.
    /// </summary>
    private void HideContentControls()
    {
        pnlLeftContent.Visible = false;
        plcSeparator.Visible = false;
        pnlRightContent.CssClass = "DialogCompleteBlock";
        siteSelector.StopProcessing = true;
        contentTree.StopProcessing = true;
    }


    /// <summary>
    /// Initializes content tree element.
    /// </summary>
    private void InitializeContentTree()
    {
        contentTree.Visible = true;

        contentTree.DeniedNodePostback = false;
        contentTree.AllowMarks = true;
        contentTree.NodeTextTemplate = "<span class=\"ContentTreeItem\" onclick=\"SelectNode(##NODEID##, this); SetAction('contentselect', '##NODEID##|##NODECHILDNODESCOUNT##'); RaiseHiddenPostBack(); return false;\">##ICON##<span class=\"Name\">##NODENAME##</span></span>";
        contentTree.SelectedNodeTextTemplate = "<span id=\"treeSelectedNode\" class=\"ContentTreeSelectedItem\" onclick=\"SelectNode(##NODEID##, this); SetAction('contentselect', '##NODEID##|##NODECHILDNODESCOUNT##'); RaiseHiddenPostBack(); return false;\">##ICON##<span class=\"Name\">##NODENAME##</span></span>";
        contentTree.MaxTreeNodeText = String.Format("<span class=\"ContentTreeItem\" onclick=\"SetAction('morecontentselect', ##PARENTNODEID##); RaiseHiddenPostBack(); return false;\"><span class=\"Name\" style=\"font-style: italic;\">{0}</span></span>", GetString("ContentTree.SeeListing"));
        contentTree.IsLiveSite = IsLiveSite;
        contentTree.SelectOnlyPublished = IsLiveSite;
        contentTree.SelectPublishedData = IsLiveSite;

        contentTree.SiteName = SiteName;

        // Starting path node ID
        StartingPathNodeID = GetStartNodeId();
    }


    /// <summary>
    /// Returns ID of the starting node according current starting path settings.
    /// </summary>
    private int GetStartNodeId()
    {
        if (!string.IsNullOrEmpty(Config.ContentStartingPath))
        {
            contentTree.Path = Config.ContentStartingPath;
        }
        else if (!string.IsNullOrEmpty(CMSContext.CurrentUser.UserStartingAliasPath))
        {
            contentTree.Path = CMSContext.CurrentUser.UserStartingAliasPath;
        }
        return GetContentNodeId(contentTree.Path);
    }


    /// <summary>
    /// Returns path of the node specified by its ID.
    /// </summary>
    /// <param name="nodeId">ID of the node</param>
    private static int GetParentNodeID(int nodeId)
    {
        if (nodeId > 0)
        {
            TreeProvider tree = new TreeProvider(CMSContext.CurrentUser);
            if (tree != null)
            {
                // Get node and return its alias path
                using (TreeNode node = tree.SelectSingleNode(nodeId))
                {
                    if (node != null)
                    {
                        return node.NodeParentID;
                    }
                }
            }
        }

        return 0;
    }

    /// <summary>
    /// Returns path of the node specified by its ID.
    /// </summary>
    /// <param name="nodeId">ID of the node</param>
    private static string GetContentPath(int nodeId)
    {
        if (nodeId > 0)
        {
            TreeProvider tree = new TreeProvider(CMSContext.CurrentUser);
            if (tree != null)
            {
                // Get node and return its alias path
                using (TreeNode node = tree.SelectSingleNode(nodeId))
                {
                    if (node != null)
                    {
                        return ((node.NodeChildNodesCount == 0) ? TreePathUtils.GetParentPath(node.NodeAliasPath) : node.NodeAliasPath).ToLower();
                    }
                }
            }
        }

        return string.Empty;
    }


    /// <summary>
    /// Returns ID of the content node specified by its alias path.
    /// </summary>
    /// <param name="aliasPath">Alias path of the node</param>
    private int GetContentNodeId(string aliasPath)
    {
        if (!string.IsNullOrEmpty(aliasPath))
        {
            TreeProvider tree = new TreeProvider(CMSContext.CurrentUser);
            if (tree != null)
            {
                using (TreeNode node = tree.SelectSingleNode(SiteName, aliasPath, CMSContext.PreferredCultureCode))
                {
                    if (node != null)
                    {
                        // Return node's ID
                        return node.NodeID;
                    }
                }
            }
        }

        return 0;
    }


    /// <summary>
    /// Applies loaded nodes to the view control.
    /// </summary>
    /// <param name="nodes">Nodes to load</param>
    private void LoadNodes(DataSet nodes)
    {
        bool originalNotEmpty = !DataHelper.DataSourceIsEmpty(nodes);
        if (!DataHelper.DataSourceIsEmpty(nodes))
        {
            mediaView.DataSource = nodes;
        }
        else if (originalNotEmpty && IsLiveSite)
        {
            mediaView.InfoText = GetString("dialogs.document.NotAuthorizedToViewAny");
        }
    }


    /// <summary>
    /// Gets all child nodes in the specified parent path.
    /// </summary>
    /// <param name="searchText">Text to filter searched nodes</param>
    /// <param name="tree">Tree provider used to obtain nodes</param>
    /// <param name="parentAliasPath">Alias path of the parent</param>
    /// <param name="siteName">Name of the related site</param>
    private DataSet GetNodes(string searchText, TreeProvider tree, string parentAliasPath, string siteName)
    {
        // Create WHERE condition
        string where = "(NodeAliasPath <> '/')";
        if (!string.IsNullOrEmpty(searchText))
        {
            string searchTextSafe = SqlHelperClass.GetSafeQueryString(searchText, false);
            where = SqlHelperClass.AddWhereCondition(where, String.Format("((AttachmentName LIKE N'%{0}%') OR (DocumentName LIKE N'%{0}%'))", searchTextSafe));
        }

        // If not all content is selectable and no additional content being displayed
        if ((SelectableContent != SelectableContentEnum.AllContent) && !IsFullListingMode)
        {
            where = SqlHelperClass.AddWhereCondition(where, "(ClassName = 'CMS.File')");
        }

        string columns = SqlHelperClass.MergeColumns((IsLiveSite ? TreeProvider.SELECTNODES_REQUIRED_COLUMNS : DocumentHelper.GETDOCUMENTS_REQUIRED_COLUMNS), NODE_COLUMNS);

        int topN = mediaView.CurrentTopN;

        // Get files
        tree.SelectQueryName = "selectattachments";
        DataSet nodes = IsLiveSite ? tree.SelectNodes(siteName, parentAliasPath.TrimEnd('/') + "/%", TreeProvider.ALL_CULTURES, true, null, where, "NodeOrder", 1, true, topN, columns) :
            DocumentHelper.GetDocuments(siteName, parentAliasPath.TrimEnd('/') + "/%", TreeProvider.ALL_CULTURES, true, null, where, "NodeOrder", 1, false, topN, columns, tree);

        // Check permissions
        return TreeSecurityProvider.FilterDataSetByPermissions(nodes, NodePermissionsEnum.Read, CMSContext.CurrentUser);
    }


    /// <summary>
    /// Loads all files for the view control.
    /// </summary>
    /// <param name="searchText">Text to filter loaded files</param>
    private void LoadContentDataSource(string searchText)
    {
        DataSet nodes = null;

        // Load data
        if (NodeID > 0)
        {
            // Get selected node            
            TreeNode node = TreeNodeObj;
            if ((TreeNodeObj != null) && !(Config.OutputFormat == OutputFormatEnum.NodeGUID && node.NodeSiteID != CMSContext.CurrentSiteID))
            {
                // Get selected node site info
                SiteInfo si = SiteInfoProvider.GetSiteInfo(node.NodeSiteID);
                if (si != null)
                {
                    // Ensure culture prefix
                    if (URLHelper.UseLangPrefixForUrls(si.SiteName))
                    {
                        CultureInfo ci = CultureInfoProvider.GetCultureInfo(node.DocumentCulture);
                        if (ci != null)
                        {
                            URLHelper.CurrentUrlLangPrefix = (String.IsNullOrEmpty(ci.CultureAlias) ? ci.CultureCode : ci.CultureAlias);
                        }
                    }

                    // List view
                    if (node.NodeClassName.ToLower() != "cms.file")
                    {
                        if (node.TreeProvider.CheckDocumentUIPermissions(si.SiteName) && (CMSContext.CurrentUser.IsAuthorizedPerDocument(node, NodePermissionsEnum.Read) != AuthorizationResultEnum.Allowed))
                        {
                            return;
                        }

                        // Check permissions
                        if (CMSContext.CurrentUser.IsAuthorizedPerDocument(node, NodePermissionsEnum.ExploreTree) == AuthorizationResultEnum.Allowed || !IsLiveSite)
                        {
                            TreeProvider tree = new TreeProvider(CMSContext.CurrentUser) { UseCache = false };
                            nodes = GetNodes(searchText, tree, node.NodeAliasPath, si.SiteName);

                            LoadNodes(nodes);

                            // If all content selectable
                            bool selectableAll = SelectableContent == SelectableContentEnum.AllContent;
                            if (selectableAll && !IsFullListingMode && (IsAction || !URLHelper.IsPostback()))
                            {
                                if ((ItemToColorize == Guid.Empty) || (ItemToColorize == node.NodeGUID))
                                {
                                    string fileExtension = TreePathUtils.GetUrlExtension();
                                    if (String.IsNullOrEmpty(fileExtension))
                                    {
                                        fileExtension = node.DocumentExtensions;
                                    }
                                    string url = mediaView.GetContentItemUrl(node.NodeGUID, node.DocumentUrlPath, node.NodeAlias,
                                        node.NodeAliasPath, node.IsLink, 0, 0, 0, true, fileExtension);

                                    ItemToColorize = node.NodeGUID;

                                    SelectMediaItem(node.DocumentName, url, node.NodeAliasPath);
                                }
                            }

                            // Display full-size properties if detailed view required
                            if (!IsCopyMoveLinkDialog && !IsFullListingMode && selectableAll && (node.NodeChildNodesCount == 0) && !IsLinkOutput)
                            {
                                DisplayFull();
                            }
                            else
                            {
                                DisplayNormal();
                            }
                        }
                        else
                        {
                            mediaView.InfoText = GetString("dialogs.document.NotAuthorizedToExpolore");
                        }
                    }
                    else
                    {
                        // Check permissions
                        if ((CMSContext.CurrentUser.IsAuthorizedPerDocument(node, NodePermissionsEnum.Read) == AuthorizationResultEnum.Allowed))
                        {
                            // Get attachment info and initialize displayed attachment properties
                            Guid attachmentGUID = ValidationHelper.GetGuid(node.GetValue("FileAttachment"), Guid.Empty);
                            if (attachmentGUID != Guid.Empty)
                            {
                                // Get the attachment
                                TreeProvider tree = new TreeProvider(CMSContext.CurrentUser) { UseCache = false };
                                AttachmentInfo atInfo = DocumentHelper.GetAttachment(node, attachmentGUID, tree, false);
                                if (atInfo != null)
                                {
                                    // Get the data
                                    string extension = atInfo.AttachmentExtension;
                                    bool isContentFile = (node.NodeClassName.ToLower() == "cms.file");

                                    if (CMSDialogHelper.IsItemSelectable(SelectableContent, extension, isContentFile))
                                    {
                                        string fileExtension = null;
                                        fileExtension = (isContentFile ? TreePathUtils.GetFilesUrlExtension() : TreePathUtils.GetUrlExtension());
                                        if (String.IsNullOrEmpty(fileExtension))
                                        {
                                            fileExtension = node.DocumentExtensions;
                                        }
                                        // Set 'get file path'
                                        atInfo.AttachmentUrl = mediaView.GetContentItemUrl(node.NodeGUID, node.DocumentUrlPath, node.NodeAlias, node.NodeAliasPath, node.IsLink, 0, 0, 0, false, fileExtension);

                                        CurrentAttachmentInfo = atInfo;

                                        if (!IsCopyMoveLinkDialog && !IsFullListingMode && (node.NodeChildNodesCount == 0) && !IsLinkOutput)
                                        {
                                            // Display properties in full size
                                            DisplayFull();
                                        }
                                        else
                                        {
                                            if (node.NodeChildNodesCount == 0)
                                            {
                                                // Load child nodes                                            
                                                nodes = GetNodes(searchText, tree, node.NodeAliasPath, si.SiteName);

                                                LoadNodes(nodes);
                                            }

                                            DisplayNormal();
                                        }
                                    }
                                    else
                                    {
                                        mediaView.InfoText = GetString("dialogs.item.notselectable");

                                        DisplayNormal();
                                    }
                                }
                            }
                            else
                            {
                                DisplayNormal();
                            }
                        }
                        else
                        {
                            DisplayNormal();

                            mediaView.InfoText = GetString("dialogs.document.NotAuthorizedToViewNode");
                        }
                    }
                }
            }
        }

        mediaView.DataSource = nodes;
    }


    /// <summary>
    /// Handles actions related to the folders.
    /// </summary>
    /// <param name="argument">Argument related to the folder action</param>
    /// <param name="reloadTree">Indicates if the content tree should be reloaded</param>
    private void HandleFolderAction(string argument, bool reloadTree)
    {
        HandleFolderAction(argument, reloadTree, true);
    }


    /// <summary>
    /// Handles actions related to the folders.
    /// </summary>
    /// <param name="argument">Argument related to the folder action</param>
    /// <param name="isNewFolder">Indicates if is new folder</param>
    /// <param name="callSelection">Indicates if selection should be called</param>
    private void HandleFolderAction(string argument, bool reloadTree, bool callSelection)
    {
        NodeID = ValidationHelper.GetInteger(argument, 0);

        // Update new folder information
        menuElem.NodeID = NodeID;
        menuElem.UpdateActionsMenu();

        // Reload content tree if new folder was created
        if (reloadTree)
        {
            InitializeContentTree();

            // Fill with new info
            contentTree.NodeID = NodeID;
            contentTree.ExpandNodeID = NodeID;

            contentTree.ReloadData();
            pnlUpdateTree.Update();

            ScriptHelper.RegisterStartupScript(Page, typeof(Page), "EnsureTopWindow", ScriptHelper.GetScript("if (self.focus) { self.focus(); }"));
        }

        // Load new data 
        LoadDataSource();

        // Load selected item
        if (CurrentAttachmentInfo != null)
        {
            string fileName = Path.GetFileNameWithoutExtension(CurrentAttachmentInfo.AttachmentName);

            if (callSelection)
            {
                SelectMediaItem(fileName, CurrentAttachmentInfo.AttachmentExtension, CurrentAttachmentInfo.AttachmentImageWidth,
                    CurrentAttachmentInfo.AttachmentImageHeight, CurrentAttachmentInfo.AttachmentSize, CurrentAttachmentInfo.AttachmentUrl);
            }

            ItemToColorize = CurrentAttachmentInfo.AttachmentGUID;
            ColorizeRow(ItemToColorize.ToString());
        }
        else
        {
            ColorizeLastSelectedRow();
        }

        // Get parent node ID info
        int parentId = StartingPathNodeID != NodeID ? GetParentNodeID(NodeID) : 0;
        mediaView.ShowParentButton = (parentId > 0);
        mediaView.NodeParentID = parentId;

        // Reload view control's content
        mediaView.Reload();
        pnlUpdateView.Update();

        ClearActionElems();
    }


    /// <summary>
    /// Handles actions occurring when new content (CMS.File) document was created.
    /// </summary>
    /// <param name="argument">Argument holding information on new document node ID</param>
    private void HandleContentFileCreatedAction(string argument)
    {
        string[] argArr = argument.Split('|');
        if (argArr.Length == 1)
        {
            HandleFolderAction(argArr[0], true);
        }
    }


    /// <summary>
    /// Handles attachment edit action.
    /// </summary>
    /// <param name="argument">Attachment GUID coming from view control</param>
    private void HandleContentEdit(string argument)
    {
        IsEditImage = true;

        if (!string.IsNullOrEmpty(argument))
        {
            Hashtable argTable = CMSModules_Content_Controls_Dialogs_LinkMediaSelector_MediaView.GetArgumentsTable(argument);

            Guid attachmentGuid = ValidationHelper.GetGuid(argTable["attachmentguid"], Guid.Empty);

            // Node ID was specified
            int nodeId = 0;
            if (argTable.Count == 2)
            {
                nodeId = ValidationHelper.GetInteger(argTable["nodeid"], 0);
            }

            AttachmentInfo ai = AttachmentManager.GetAttachmentInfo(attachmentGuid, SiteName);
            if (ai != null)
            {
                // Get attachment node by ID
                TreeNode node = (nodeId > 0 ? TreeHelper.SelectSingleNode(nodeId) : TreeHelper.SelectSingleDocument(ai.AttachmentDocumentID));
                if (node != null)
                {
                    // Check node site
                    if (node.NodeSiteID != CMSContext.CurrentSiteID)
                    {
                        mediaView.SiteObj = SiteInfoProvider.GetSiteInfo(node.NodeSiteID);
                    }

                    string fileExt = TreePathUtils.GetFilesUrlExtension();
                    if (String.IsNullOrEmpty(fileExt))
                    {
                        fileExt = node.DocumentExtensions;
                    }

                    // Get node URL
                    string url = mediaView.GetContentItemUrl(node.NodeGUID, node.DocumentUrlPath, node.NodeAlias, node.NodeAliasPath, node.IsLink, 0, 0, 0, false, fileExt);

                    // Update properties if node is currently selected
                    if (attachmentGuid == ItemToColorize)
                    {
                        SelectMediaItem(ai.AttachmentName, ai.AttachmentExtension, ai.AttachmentImageWidth, ai.AttachmentImageHeight, ai.AttachmentSize, url, null, node.NodeID, node.NodeAliasPath);
                    }

                    // Update select action to reflect changes made during editing
                    LoadDataSource();
                    mediaView.Reload();
                    pnlUpdateView.Update();
                }
            }
        }

        ClearActionElems();
    }


    /// <summary>
    /// Ensures content tree is refreshed when new folder is created in Copy/Move dialog.
    /// </summary>
    private void RefreshContentTree()
    {
        // Refresh content tree
        ScriptHelper.RegisterStartupScript(Page, typeof(Page), "RefreshContentTree", ScriptHelper.GetScript(@"
var wopener = (window.top.opener ? window.top.opener : window.top.dialogArguments);
if (wopener == null) {
    wopener = opener;
}              
if (wopener.parent != null) {
    if (wopener.parent.frames['contenttree'] != null) {
        if (wopener.parent.frames['contenttree'].RefreshTree != null) {
            wopener.parent.frames['contenttree'].RefreshTree();
        }
    }
}"));
    }

    #endregion


    #region "Attachment methods"

    /// <summary>
    /// Loads all attachments for the view control.
    /// </summary>
    /// <param name="searchText">Text to filter loaded files</param>
    private void LoadAttachmentsDataSource(string searchText)
    {
        DataSet attachments = null;
        int topN = mediaView.CurrentTopN;

        // Only unsorted attachments are being displayed
        string where = String.IsNullOrEmpty(searchText) ? "(AttachmentIsUnsorted = 1)" :
            SqlHelperClass.AddWhereCondition("(AttachmentIsUnsorted = 1)", String.Format("(AttachmentName LIKE N'%{0}%')", SqlHelperClass.GetSafeQueryString(searchText, false)));

        // Get document attachments
        if (Config.AttachmentDocumentID != 0)
        {
            if (TreeNodeObj != null)
            {
                // Check permissions
                if (CMSContext.CurrentUser.IsAuthorizedPerDocument(TreeNodeObj, NodePermissionsEnum.Read) == AuthorizationResultEnum.Allowed)
                {
                    TreeProvider tree = new TreeProvider(CMSContext.CurrentUser) { UseCache = false };
                    attachments = DocumentHelper.GetAttachments(TreeNodeObj, where, "AttachmentOrder, AttachmentName", false, tree, topN);
                }
            }
        }
        // Get form attachments 
        else if (AttachmentsAreTemporary)
        {
            where = SqlHelperClass.AddWhereCondition(where, String.Format("(AttachmentFormGUID = '{0}')", Config.AttachmentFormGUID));
            attachments = AttachmentManager.GetAttachments(where, "AttachmentOrder, AttachmentName", false, topN);
        }

        mediaView.DataSource = attachments;
    }


    /// <summary>
    /// Checks attachment permissions.
    /// </summary>
    private string CheckAttachmentPermissions()
    {
        string message = "";

        // For new document
        if (Config.AttachmentFormGUID != Guid.Empty)
        {
            if (Config.AttachmentParentID == 0)
            {
                message = "Node parent node ID has to be set.";
            }

            if (!RaiseOnCheckPermissions("Create", this))
            {
                if (!CMSContext.CurrentUser.IsAuthorizedToCreateNewDocument(Config.AttachmentParentID, "CMS.File"))
                {
                    message = GetString("attach.actiondenied");
                }
            }
        }
        // For existing document
        else if (Config.AttachmentDocumentID > 0)
        {
            TreeProvider tree = new TreeProvider(CMSContext.CurrentUser);
            // Get document node
            using (TreeNode node = DocumentHelper.GetDocument(Config.AttachmentDocumentID, tree))
            {
                if (node == null)
                {
                    message = "Given document doesn't exist!";
                }
                if (!RaiseOnCheckPermissions("Modify", this))
                {
                    if (CMSContext.CurrentUser.IsAuthorizedPerDocument(node, NodePermissionsEnum.Modify) != AuthorizationResultEnum.Allowed)
                    {
                        message = GetString("attach.actiondenied");
                    }
                }
            }
        }

        return message;
    }


    /// <summary>
    /// Handles new attachment create action.
    /// </summary>
    /// <param name="argument">Argument coming from upload control</param>
    private void HandleAttachmentCreatedAction(string argument)
    {
        HandleAttachmentAction(argument, false);

        // Reload view
        LoadDataSource();

        mediaView.Reload();
        pnlUpdateView.Update();
    }


    /// <summary>
    /// Handles attachment update action.
    /// </summary>
    /// <param name="argument">Argument coming from upload control</param>
    private void HandleAttachmentUpdatedAction(string argument)
    {
        HandleAttachmentAction(argument, true);
    }


    /// <summary>
    /// Handles attachment edit action.
    /// </summary>
    /// <param name="argument">Attachment GUID coming from view control</param>
    private void HandleAttachmentEdit(string argument)
    {
        IsEditImage = true;

        if (!string.IsNullOrEmpty(argument))
        {
            string[] argArr = argument.Split('|');

            Guid attachmentGuid = ValidationHelper.GetGuid(argArr[1], Guid.Empty);

            AttachmentInfo ai = null;

            int versionHistoryId = 0;
            if (TreeNodeObj != null)
            {
                versionHistoryId = TreeNodeObj.DocumentCheckedOutVersionHistoryID;
            }

            if (versionHistoryId == 0)
            {
                ai = AttachmentManager.GetAttachmentInfo(attachmentGuid, CMSContext.CurrentSiteName);
            }
            else
            {
                VersionManager vm = new VersionManager(TreeNodeObj.TreeProvider);
                if (vm != null)
                {
                    // Get the attachment version data
                    AttachmentHistoryInfo attachmentVersion = vm.GetAttachmentVersion(versionHistoryId, attachmentGuid, false);
                    if (attachmentVersion == null)
                    {
                        ai = null;
                    }
                    else
                    {
                        // Create the attachment info from given data
                        ai = new AttachmentInfo(attachmentVersion.Generalized.DataClass);
                        ai.AttachmentID = attachmentVersion.AttachmentHistoryID;
                    }
                    if (ai != null)
                    {
                        ai.AttachmentLastHistoryID = versionHistoryId;
                    }
                }
            }

            if (ai != null)
            {
                string nodeAliasPath = "";
                if (TreeNodeObj != null)
                {
                    nodeAliasPath = TreeNodeObj.NodeAliasPath;
                }

                string url = mediaView.GetAttachmentItemUrl(ai.AttachmentGUID, ai.AttachmentName, nodeAliasPath, ai.AttachmentImageHeight, ai.AttachmentImageWidth, 0);

                if (LastAttachmentGuid == attachmentGuid)
                {
                    SelectMediaItem(ai.AttachmentName, ai.AttachmentExtension, ai.AttachmentImageWidth, ai.AttachmentImageHeight, ai.AttachmentSize, url);
                }

                // Update select action to reflect changes made during editing
                LoadDataSource();
                mediaView.Reload();
                pnlUpdateView.Update();
            }
        }

        ClearActionElems();
    }


    /// <summary>
    /// Handles attachment action.
    /// </summary>
    /// <param name="argument">Argument coming from upload control</param>
    private void HandleAttachmentAction(string argument, bool isUpdate)
    {
        // Get attachment URL first
        Guid attachmentGuid = ValidationHelper.GetGuid(argument, Guid.Empty);
        if (attachmentGuid != Guid.Empty)
        {
            // Get attachment info
            TreeProvider tree = new TreeProvider(CMSContext.CurrentUser);

            // Ensure site information
            SiteInfo si = CMSContext.CurrentSite;
            if ((TreeNodeObj != null) && (si.SiteID != TreeNodeObj.NodeSiteID))
            {
                si = SiteInfoProvider.GetSiteInfo(TreeNodeObj.NodeSiteID);
            }

            AttachmentInfo ai = DocumentHelper.GetAttachment(attachmentGuid, tree, si.SiteName, false);
            if (ai != null)
            {
                string nodeAliasPath = (TreeNodeObj != null) ? TreeNodeObj.NodeAliasPath : null;

                if (CMSDialogHelper.IsItemSelectable(SelectableContent, ai.AttachmentExtension))
                {
                    // Get attachment URL
                    string url = mediaView.GetAttachmentItemUrl(ai.AttachmentGUID, ai.AttachmentName, nodeAliasPath, 0, 0, 0);

                    // Remember last selected attachment GUID
                    if (SourceType == MediaSourceEnum.DocumentAttachments)
                    {
                        LastAttachmentGuid = ai.AttachmentGUID;
                    }

                    // Get the node workflow
                    int versionHistoryId = 0;
                    if (TreeNodeObj != null)
                    {
                        WorkflowManager wm = new WorkflowManager(TreeNodeObj.TreeProvider);
                        WorkflowInfo wi = wm.GetNodeWorkflow(TreeNodeObj);
                        if (wi != null)
                        {
                            // Ensure the document version
                            VersionManager vm = new VersionManager(TreeNodeObj.TreeProvider);
                            versionHistoryId = vm.EnsureVersion(TreeNodeObj, TreeNodeObj.IsPublished);
                        }
                    }

                    MediaItem item = InitializeMediaItem(ai.AttachmentName, ai.AttachmentExtension, ai.AttachmentImageWidth, ai.AttachmentImageHeight, ai.AttachmentSize, url, null, versionHistoryId, 0, "");

                    SelectMediaItem(item);

                    ItemToColorize = attachmentGuid;

                    ColorizeRow(ItemToColorize.ToString());
                }
                else
                {
                    // Unselect old attachment and clear properties
                    ColorizeRow("");
                    Properties.ClearProperties(true);
                    pnlUpdateProperties.Update();
                }

                mediaView.InfoText = (isUpdate ? GetString("dialogs.attachment.updated") : GetString("dialogs.attachment.created"));

                pnlUpdateView.Update();
            }
        }

        ClearActionElems();
    }


    /// <summary>
    /// Hadnles actions occurring when attachment is moved.
    /// </summary>
    /// <param name="argument">Argument holding information on attachment being moved</param>
    /// <param name="action">Action specifying whether the attachment is moved up/down</param>
    private void HandleAttachmentMoveAction(string argument, string action)
    {
        // Check permissions
        string errMsg = CheckAttachmentPermissions();

        if (errMsg == "")
        {
            Guid attachmentGuid = ValidationHelper.GetGuid(argument, Guid.Empty);
            if (attachmentGuid != Guid.Empty)
            {
                // Move temporary attachement
                if (!AttachmentsAreTemporary)
                {
                    if (action == "attachmentmoveup")
                    {
                        DocumentHelper.MoveAttachmentUp(attachmentGuid, TreeNodeObj);
                    }
                    else
                    {
                        DocumentHelper.MoveAttachmentDown(attachmentGuid, TreeNodeObj);
                    }
                }
                else
                {
                    if (action == "attachmentmoveup")
                    {
                        AttachmentManager.MoveAttachmentUp(attachmentGuid, 0);
                    }
                    else
                    {
                        AttachmentManager.MoveAttachmentDown(attachmentGuid, 0);
                    }
                }

                // Reload data
                LoadDataSource();

                plcError.Visible = false;

                mediaView.Reload();
            }
        }
        else
        {
            // Display error
            lblError.Text = errMsg;
            plcError.Visible = true;
        }

        ClearActionElems();

        ColorizeLastSelectedRow();

        pnlUpdateView.Update();
    }


    /// <summary>
    /// Hadnles actions occurring when some attachment is being removed.
    /// </summary>
    /// <param name="argument">Argument holding information on attachment</param>
    private void HandleDeleteAttachmentAction(string argument)
    {
        string errMsg = CheckAttachmentPermissions();

        if (errMsg == "")
        {
            Guid attachmentGuid = ValidationHelper.GetGuid(argument, Guid.Empty);
            if (attachmentGuid != Guid.Empty)
            {
                if (!AttachmentsAreTemporary)
                {
                    TreeProvider tree = new TreeProvider(CMSContext.CurrentUser);

                    DocumentHelper.DeleteAttachment(TreeNodeObj, attachmentGuid, tree);
                }
                else
                {
                    // Delete temporary attachment
                    AttachmentManager.DeleteTemporaryAttachment(attachmentGuid, CMSContext.CurrentSiteName);
                }

                // Reload data
                LoadDataSource();
                mediaView.Reload();

                // Selected attachment was removed
                if (LastAttachmentGuid == attachmentGuid)
                {
                    // Reset properties
                    Properties.ClearProperties();
                    pnlUpdateProperties.Update();
                }
                else
                {
                    ColorizeLastSelectedRow();
                }
            }
        }
        else
        {
            // Display error
            lblError.Text = errMsg;
            plcError.Visible = true;
        }

        pnlUpdateView.Update();
    }

    #endregion


    #region "Common event methods"

    /// <summary>
    /// Handles actions occurring when some text is searched.
    /// </summary>
    /// <param name="argument">Argument holding information on searched text</param>
    private void HandleSearchAction(string argument)
    {
        LastSearchedValue = argument;

        // Load new data filtered by searched text 
        LoadDataSource();

        // Reload content
        mediaView.Reload();
        pnlUpdateView.Update();

        // Keep focus in search text box
        ScriptHelper.RegisterStartupScript(Page, typeof(string), "SetSearchFocus", ScriptHelper.GetScript("setTimeout('SetSearchFocus();', 200);"));
    }


    /// <summary>
    /// Handles actions occurring when some item is selected.
    /// </summary>
    /// <param name="argument">Argument holding information on selected item</param>
    private void HandleSelectAction(string argument)
    {
        // Create new selected media item
        SelectMediaItem(argument);

        // Forget recent action
        ClearActionElems();
    }


    /// <summary>
    /// Handles actions occuring when some item in copy/move/link/select path dialog is selected.
    /// </summary>
    private void HandleDialogSelect()
    {
        if (TreeNodeObj != null)
        {
            string columns = SqlHelperClass.MergeColumns((IsLiveSite ? TreeProvider.SELECTNODES_REQUIRED_COLUMNS : DocumentHelper.GETDOCUMENTS_REQUIRED_COLUMNS), NODE_COLUMNS);

            // Get files
            TreeNodeObj.TreeProvider.SelectQueryName = "selectattachments";

            DataSet nodeDetails = null;
            if (IsLiveSite)
            {
                // Get published files
                nodeDetails = TreeNodeObj.TreeProvider.SelectNodes(SiteName, TreeNodeObj.NodeAliasPath, CMSContext.CurrentUser.PreferredCultureCode, true, null, null, "DocumentName", 1, true, 1, columns);
            }
            else
            {
                // Get latest files
                nodeDetails = DocumentHelper.GetDocuments(SiteName, TreeNodeObj.NodeAliasPath, CMSContext.CurrentUser.PreferredCultureCode, true, null, null, "DocumentName", 1, false, 1, columns, TreeNodeObj.TreeProvider);
            }

            // If node details exists
            if (!DataHelper.IsEmpty(nodeDetails))
            {
                IDataContainer data = new DataRowContainer(nodeDetails.Tables[0].Rows[0]);

                string argument = mediaView.GetArgumentSet(data);
                bool notAttachment = (SourceType == MediaSourceEnum.Content) && !((data.GetValue("ClassName").ToString().ToLower() == "cms.file") && (ValidationHelper.GetGuid(data.GetValue("AttachmentGUID"), Guid.Empty) != Guid.Empty));
                string url = mediaView.GetItemUrl(argument, 0, 0, 0, notAttachment);

                SelectMediaItem(String.Format("{0}|URL|{1}", argument, url));
            }

            ItemToColorize = TreeNodeObj.NodeGUID;
        }
        else
        {
            // Remove selected item
            ItemToColorize = Guid.Empty;
        }

        ClearColorizedRow();

        // Forget recent action
        ClearActionElems();
    }


    private void HandleSiteEmpty()
    {
        if ((SourceType != MediaSourceEnum.DocumentAttachments) && String.IsNullOrEmpty(SiteName))
        {
            contentTree.Visible = false;
            siteSelector.Enabled = false;
            lblTreeInfo.Visible = true;

            // Disable menu
            ScriptHelper.RegisterStartupScript(Page, typeof(Page), "DialogsDisableMenuActions", ScriptHelper.GetScript("if(DisableNewFileBtn){ DisableNewFileBtn(); } if(DisableNewFolderBtn){ DisableNewFolderBtn(); }"));
        }
    }

    #endregion


    #region "Event handlers"

    protected void UniSelector_OnSelectionChanged(object sender, EventArgs e)
    {
        IsAction = true;
        FolderNotSelectedYet = true;

        // Update information on current site 
        SiteID = siteSelector.SiteID;
        if (SiteID > 0)
        {
            mediaView.SiteObj = SiteInfoProvider.GetSiteInfo(SiteID);
        }

        // Reset selected node to root node
        NodeID = StartingPathNodeID = GetStartNodeId();
        if (NodeID == 0)
        {
            NodeID = GetContentNodeId("/");
            mediaView.ShowParentButton = false;
        }

        if (SelectableContent != SelectableContentEnum.AllContent)
        {
            contentTree.NodeID = NodeID;
            contentTree.ExpandNodeID = NodeID;

            // Reload media view for new site
            LoadDataSource();
        }
        else
        {
            mediaView.DataSource = null;
        }

        // Update information on parent node ID for new folder creation
        menuElem.NodeID = NodeID;
        menuElem.UpdateActionsMenu();

        // Reload content tree for new site
        contentTree.SiteName = siteSelector.SiteName;
        InitializeContentTree();
        pnlUpdateTree.Update();

        // Load selected item
        if (CurrentAttachmentInfo != null)
        {
            SelectMediaItem(CurrentAttachmentInfo.AttachmentName, CurrentAttachmentInfo.AttachmentExtension,
                CurrentAttachmentInfo.AttachmentImageWidth, CurrentAttachmentInfo.AttachmentImageHeight, CurrentAttachmentInfo.AttachmentSize, CurrentAttachmentInfo.AttachmentUrl);
        }

        // Setup media view
        mediaView.Reload();
        pnlUpdateView.Update();

        // Setup properties
        DisplayNormal();
        Properties.ClearProperties();
        pnlUpdateProperties.Update();
    }


    /// <summary>
    /// Behaves as mediator in communication line between control taking action and the rest of the same level controls.
    /// </summary>
    protected void hdnButton_Click(object sender, EventArgs e)
    {
        IsAction = true;

        switch (CurrentAction)
        {
            case "insertitem":
                GetSelectedItem();
                break;

            case "search":
                HandleSearchAction(CurrentArgument);
                break;

            case "select":
                HandleSelectAction(CurrentArgument);
                break;

            case "morecontentselect":
            case "contentselect":
                FolderNotSelectedYet = false;
                ResetSearchFilter();

                string[] argArr = CurrentArgument.Split('|');
                if (IsLinkOutput)
                {
                    CurrentAttachmentInfo = null;
                }

                // If more content is requested, enable the full listing
                if (!IsFullListingMode)
                {
                    IsFullListingMode = (CurrentAction == "morecontentselect");
                }

                HandleFolderAction(argArr[0], IsFullListingMode);

                if (IsCopyMoveLinkDialog || IsLinkOutput)
                {
                    HandleDialogSelect();
                }
                break;

            case "parentselect":
                ResetSearchFilter();

                HandleFolderAction(CurrentArgument, true);

                if (IsCopyMoveLinkDialog)
                {
                    HandleDialogSelect();
                }
                break;

            case "refreshtree":
                ResetSearchFilter();
                HandleFolderAction(CurrentArgument, true);
                break;

            case "contentcreated":
                HandleContentFileCreatedAction(CurrentArgument);

                if (IsCopyMoveLinkDialog)
                {
                    HandleDialogSelect();
                }
                break;

            case "closelisting":
                IsFullListingMode = false;
                HandleFolderAction(NodeID.ToString(), false);
                break;

            case "newfolder":
                ResetSearchFilter();
                HandleFolderAction(CurrentArgument, true);

                if (IsCopyMoveLinkDialog)
                {
                    // Refresh content tree when new folder is created in Copy/Move dialog
                    RefreshContentTree();
                    HandleDialogSelect();
                }
                break;

            case "cancelfolder":
                ScriptHelper.RegisterStartupScript(Page, typeof(Page), "EnsureTopWindow", ScriptHelper.GetScript("if (self.focus) { self.focus(); }"));
                ClearActionElems();
                break;

            case "attachmentmoveup":
                HandleAttachmentMoveAction(CurrentArgument, CurrentAction);
                break;

            case "attachmentmovedown":
                HandleAttachmentMoveAction(CurrentArgument, CurrentAction);
                break;

            case "attachmentdelete":
                HandleDeleteAttachmentAction(CurrentArgument);
                break;

            case "attachmentcreated":
                HandleAttachmentCreatedAction(CurrentArgument);
                break;

            case "attachmentupdated":
                HandleAttachmentUpdatedAction(CurrentArgument);
                break;

            case "attachmentedit":
                HandleAttachmentEdit(CurrentArgument);
                break;

            case "contentedit":
                HandleContentEdit(CurrentArgument);
                break;

            default:
                ColorizeLastSelectedRow();
                pnlUpdateView.Update();
                break;
        }
    }

    #endregion
}
