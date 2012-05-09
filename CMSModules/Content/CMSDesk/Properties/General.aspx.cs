using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.CMSHelper;
using CMS.ExtendedControls;
using CMS.FormControls;
using CMS.GlobalHelper;
using CMS.LicenseProvider;
using CMS.PortalEngine;
using CMS.SettingsProvider;
using CMS.SiteProvider;
using CMS.TreeEngine;
using CMS.UIControls;
using CMS.WorkflowEngine;

using TimeZoneInfo = CMS.SiteProvider.TimeZoneInfo;
using TreeNode = CMS.TreeEngine.TreeNode;

[RegisterTitle("content.ui.propertiesgeneral")]
public partial class CMSModules_Content_CMSDesk_Properties_General : CMSPropertiesPage
{
    #region "Private variables"

    protected string mForums = null;
    protected string mMessageBoards = null;
    protected string mEditableContent = null;
    protected int nodeId = 0;
    protected string mSave = null;

    protected bool canEditOwner = false;
    protected bool canEdit = true;

    protected TreeNode node = null;
    protected FormEngineUserControl fcDocumentGroupSelector = null;

    private bool hasAdHocBoard = false;
    private bool hasAdHocForum = false;

    private FormEngineUserControl usrOwner = null;
    private bool displaySplitMode = CMSContext.DisplaySplitMode;

    #endregion


    #region "Page events"

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        // Check UI element permission
        if (!CMSContext.CurrentUser.IsAuthorizedPerUIElement("CMS.Content", "Properties.General"))
        {
            RedirectToCMSDeskUIElementAccessDenied("CMS.Content", "Properties.General");
        }

        // Redirect to information page when no UI elements displayed
        if (this.pnlUIAdvanced.IsHidden && this.pnlUICache.IsHidden && this.pnlUIDesign.IsHidden &&
                this.pnlUIOther.IsHidden && this.pnlUIOwner.IsHidden)
        {
            RedirectToUINotAvailable();
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        // Register the scripts
        ScriptHelper.RegisterProgress(this.Page);
        ScriptHelper.RegisterTooltip(this.Page);
        ScriptHelper.RegisterDialogScript(this);

        // Set user control properties
        usrOwner = this.Page.LoadControl("~/CMSModules/Membership/FormControls/Users/selectuser.ascx") as FormEngineUserControl;
        usrOwner.ID = "ctrlUsrOwner";
        usrOwner.IsLiveSite = false;
        usrOwner.SetValue("ShowSiteFilter", false);
        usrOwner.StopProcessing = this.pnlUIOwner.IsHidden;
        plcUsrOwner.Controls.Add(usrOwner);

        UIContext.PropertyTab = PropertyTabEnum.General;

        nodeId = QueryHelper.GetInteger("nodeid", 0);

        // Init strings
        pnlDesign.GroupingText = GetString("GeneralProperties.DesignGroup");
        pnlCache.GroupingText = GetString("GeneralProperties.CacheGroup");
        pnlOther.GroupingText = GetString("GeneralProperties.OtherGroup");
        pnlAdvanced.GroupingText = GetString("GeneralProperties.AdvancedGroup");
        pnlOwner.GroupingText = GetString("GeneralProperties.OwnerGroup");

        // Advanced section
        mEditableContent = GetString("GeneralProperties.EditableContent");
        mForums = GetString("PageProperties.AdHocForum");
        mMessageBoards = GetString("PageProperties.MessageBoards");
        lnkEditableContent.OnClientClick = "ShowEditableContent(); return false;";
        lnkMessageBoards.OnClientClick = "ShowMessageBoards(); return false;";
        lnkForums.OnClientClick = "ShowForums(); return false;";
        imgEditableContent.ImageUrl = GetImageUrl("CMSModules/CMS_Content/EditableContent/editablecontent.png");
        imgMessageBoards.ImageUrl = GetImageUrl("CMSModules/CMS_MessageBoards/module.png");
        imgForums.ImageUrl = GetImageUrl("CMSModules/CMS_Forums/module.png");

        // Get strings for radio buttons
        radInherit.Text = GetString("GeneralProperties.radInherit");
        radYes.Text = GetString("general.yes");
        radNo.Text = GetString("general.no");

        lblCacheMinutes.Text = GetString("GeneralProperties.cacheMinutes");

        // Get strings for labels
        lblNameTitle.Text = GetString("GeneralProperties.Name");
        lblNamePathTitle.Text = GetString("GeneralProperties.NamePath");
        lblAliasPathTitle.Text = GetString("GeneralProperties.AliasPath");
        lblTypeTitle.Text = GetString("GeneralProperties.Type");
        lblNodeIDTitle.Text = GetString("GeneralProperties.NodeID");
        lblLastModifiedByTitle.Text = GetString("GeneralProperties.LastModifiedBy");
        lblLastModifiedTitle.Text = GetString("GeneralProperties.LastModified");
        lblLiveURLTitle.Text = GetString("GeneralProperties.LiveURL");
        lblPreviewURLTitle.Text = GetString("GeneralProperties.PreviewURL");
        lblGUIDTitle.Text = GetString("GeneralProperties.GUID");
        lblDocGUIDTitle.Text = GetString("GeneralProperties.DocumentGUID");
        lblDocIDTitle.Text = GetString("GeneralProperties.DocumentID");
        lblCultureTitle.Text = GetString("GeneralProperties.Culture");
        lblCreatedByTitle.Text = GetString("GeneralProperties.CreatedBy");
        lblCreatedTitle.Text = GetString("GeneralProperties.Created");
        lblOwnerTitle.Text = GetString("GeneralProperties.Owner");
        lblCssStyle.Text = GetString("PageProperties.CssStyle");
        lblPublishedTitle.Text = GetString("PageProperties.Published");

        chkExcludeFromSearch.Text = GetString("GeneralProperties.ExcludeFromSearch");
        chkCssStyle.Text = GetString("Metadata.Inherit");
        pnlOnlineMarketing.GroupingText = GetString("general.onlinemarketing");


        // Set default item value
        string defaultStyleSheet = "-1";
        CssStylesheetInfo cssInfo = CMSContext.CurrentSiteStylesheet;

        // If current site default stylesheet defined, choose it
        if (cssInfo != null)
        {
            defaultStyleSheet = "default";
        }
        ctrlSiteSelectStyleSheet.CurrentSelector.SpecialFields = new string[1, 2] { { GetString("general.defaultchoice"), defaultStyleSheet } };
        ctrlSiteSelectStyleSheet.ReturnColumnName = "StyleSheetID";
        ctrlSiteSelectStyleSheet.SiteId = CMSContext.CurrentSiteID;

        if (CMSContext.CurrentSite != null)
        {
            usrOwner.SetValue("SiteID", CMSContext.CurrentSite.SiteID);
        }

        // Initialize Save button
        imgSave.ImageUrl = GetImageUrl("CMSModules/CMS_Content/EditMenu/save.png");
        mSave = GetString("general.save");

        int documentId = 0;

        // Get the document
        TreeProvider tree = new TreeProvider(CMSContext.CurrentUser);
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
            documentId = node.DocumentID;
            canEditOwner = (CMSContext.CurrentUser.IsAuthorizedPerDocument(node, NodePermissionsEnum.ModifyPermissions) == AuthorizationResultEnum.Allowed);

            ReloadData();
        }

        // Generate executive script
        string script = "function ShowEditableContent() { modalDialog('" + ResolveUrl("Advanced/EditableContent/default.aspx") + "?nodeid=" + nodeId + "', 'EditableContent', 1015, 700); } \n";

        if (hasAdHocBoard)
        {
            plcAdHocBoards.Visible = true;
            script += "function ShowMessageBoards() { modalDialog('" + ResolveUrl("~/CMSModules/MessageBoards/Content/Properties/default.aspx") + "?documentid=" + documentId + "', 'MessageBoards', 1020, 680); } \n";
        }

        if (hasAdHocForum)
        {
            plcAdHocForums.Visible = true;
            script += "function ShowForums() { modalDialog('" + ResolveUrl("~/CMSModules/Forums/Content/Properties/default.aspx") + "?documentid=" + documentId + "', 'Forums', 1130, 680); } \n";
        }

        ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "ModalDialogsToAdvancedSection", ScriptHelper.GetScript(script));

        // Register js synchronization script for split mode
        if (displaySplitMode)
        {
            RegisterSplitModeSync(true, false);
        }
    }


    protected void chkPageVisitInherit_CheckedChanged(object sender, EventArgs e)
    {
        chkLogPageVisit.Enabled = !chkPageVisitInherit.Checked;
        if (chkPageVisitInherit.Checked && (node != null))
        {
            string siteName = CMSContext.CurrentSiteName;
            if (!String.IsNullOrEmpty(siteName))
            {
                chkLogPageVisit.Checked = ValidationHelper.GetBoolean(node.GetInheritedValue("DocumentLogVisitActivity", SiteInfoProvider.CombineWithDefaultCulture(siteName)), false);
            }
        }
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        if (canEdit)
        {
            // Register Save Document script
            ScriptHelper.RegisterSaveShortcut(lnkSave, null, false);
        }
    }

    #endregion


    #region "Private methods"

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
                // Log activities checkboxes
                if (!RequestHelper.IsPostBack())
                {
                    bool? logVisit = node.DocumentLogVisitActivity;
                    chkLogPageVisit.Checked = (logVisit == true);
                    chkPageVisitInherit.Checked = (logVisit == null);
                    chkLogPageVisit.Enabled = !chkPageVisitInherit.Checked;
                    if (logVisit == null)
                    {
                        chkPageVisitInherit_CheckedChanged(null, EventArgs.Empty);
                    }
                }

                // Show document group owner selector
                if (ModuleEntry.IsModuleLoaded(ModuleEntry.COMMUNITY) && canEditOwner && LicenseHelper.CheckFeature(URLHelper.GetCurrentDomain(), FeatureEnum.Groups))
                {
                    plcOwnerGroup.Controls.Clear();
                    // Initialize table
                    TableRow rowOwner = new TableRow();
                    TableCell cellTitle = new TableCell();
                    TableCell cellSelector = new TableCell();

                    // Initialize caption
                    LocalizedLabel lblOwnerGroup = new LocalizedLabel();
                    lblOwnerGroup.EnableViewState = false;
                    lblOwnerGroup.ResourceString = "community.group.documentowner";
                    lblOwnerGroup.ID = "lblOwnerGroup";
                    cellTitle.Controls.Add(lblOwnerGroup);

                    // Initialize selector
                    fcDocumentGroupSelector = (FormEngineUserControl)Page.LoadControl("~/CMSAdminControls/UI/Selectors/DocumentGroupSelector.ascx");
                    fcDocumentGroupSelector.ID = "fcDocumentGroupSelector";
                    fcDocumentGroupSelector.StopProcessing = this.pnlUIOwner.IsHidden;
                    cellSelector.Controls.Add(fcDocumentGroupSelector);
                    fcDocumentGroupSelector.Value = ValidationHelper.GetInteger(node.GetValue("NodeGroupID"), 0);
                    fcDocumentGroupSelector.SetValue("siteid", CMSContext.CurrentSiteID);
                    fcDocumentGroupSelector.SetValue("nodeid", nodeId);

                    // Add controls to containers
                    rowOwner.Cells.Add(cellTitle);
                    rowOwner.Cells.Add(cellSelector);
                    plcOwnerGroup.Controls.Add(rowOwner);
                    plcOwnerGroup.Visible = true;
                }

                // Check modify permissions
                if (CMSContext.CurrentUser.IsAuthorizedPerDocument(node, NodePermissionsEnum.Modify) == AuthorizationResultEnum.Denied)
                {
                    // disable form editing                                                            
                    DisableFormEditing();

                    // show access denied message
                    lblInfo.Text = String.Format(GetString("cmsdesk.notauthorizedtoeditdocument"), node.NodeAliasPath);
                    lblInfo.Visible = true;
                }

                // Show owner editing only when authorized to change the permissions
                if (canEditOwner)
                {
                    lblOwner.Visible = false;
                    usrOwner.Visible = true;
                    usrOwner.SetValue("AdditionalUsers", new int[] { node.NodeOwner });
                }
                else
                {
                    usrOwner.Visible = false;
                }

                if (!RequestHelper.IsPostBack())
                {
                    if (canEditOwner)
                    {
                        usrOwner.Value = node.GetValue("NodeOwner");
                    }

                    // Search
                    chkExcludeFromSearch.Checked = node.DocumentSearchExcluded;
                }

                // Load the data
                lblName.Text = HttpUtility.HtmlEncode(node.DocumentName);
                lblNamePath.Text = HttpUtility.HtmlEncode(Convert.ToString(node.GetValue("DocumentNamePath")));
                lblAliasPath.Text = Convert.ToString(node.NodeAliasPath);
                string typeName = DataClassInfoProvider.GetDataClass(node.NodeClassName).ClassDisplayName;
                lblType.Text = HttpUtility.HtmlEncode(ResHelper.LocalizeString(typeName));
                lblNodeID.Text = Convert.ToString(node.NodeID);

                // Modifier
                SetUserLabel(lblLastModifiedBy, "DocumentModifiedByUserId");

                // Get modified time
                TimeZoneInfo usedTimeZone = null;
                DateTime lastModified = ValidationHelper.GetDateTime(node.GetValue("DocumentModifiedWhen"), DateTimeHelper.ZERO_TIME);
                lblLastModified.Text = TimeZoneHelper.GetCurrentTimeZoneDateTimeString(lastModified, CMSContext.CurrentUser, CMSContext.CurrentSite, out usedTimeZone);
                ScriptHelper.AppendTooltip(lblLastModified, TimeZoneHelper.GetGMTLongStringOffset(usedTimeZone), "help");

                if (!canEditOwner)
                {
                    // Owner
                    SetUserLabel(lblOwner, "NodeOwner");
                }

                // Creator
                SetUserLabel(lblCreatedBy, "DocumentCreatedByUserId");
                DateTime createdWhen = ValidationHelper.GetDateTime(node.GetValue("DocumentCreatedWhen"), DateTimeHelper.ZERO_TIME);
                lblCreated.Text = TimeZoneHelper.GetCurrentTimeZoneDateTimeString(createdWhen, CMSContext.CurrentUser, CMSContext.CurrentSite, out usedTimeZone);
                ScriptHelper.AppendTooltip(lblCreated, TimeZoneHelper.GetGMTLongStringOffset(usedTimeZone), "help");


                // URL
                string liveUrl = node.IsLink ? CMSContext.GetUrl(node.NodeAliasPath, null) : CMSContext.GetUrl(node.NodeAliasPath, node.DocumentUrlPath);
                lnkLiveURL.Text = ResolveUrl(liveUrl);
                lnkLiveURL.NavigateUrl = liveUrl;

                bool isRoot = (node.NodeClassName.ToLower() == "cms.root");

                // Preview URL
                if (!isRoot)
                {
                    plcPreview.Visible = true;
                    string path = canEdit ? "/CMSModules/CMS_Content/Properties/resetlink.png" : "/CMSModules/CMS_Content/Properties/resetlinkdisabled.png";
                    btnResetPreviewGuid.ImageUrl = GetImageUrl(path);
                    btnResetPreviewGuid.ToolTip = GetString("GeneralProperties.InvalidatePreviewURL");
                    btnResetPreviewGuid.ImageAlign = ImageAlign.AbsBottom;
                    btnResetPreviewGuid.Click += new ImageClickEventHandler(btnResetPreviewGuid_Click);
                    btnResetPreviewGuid.OnClientClick = "if(!confirm('" + GetString("GeneralProperties.GeneratePreviewURLConf") + "')){return false;}";

                    InitPreviewUrl();
                }

                lblGUID.Text = Convert.ToString(node.NodeGUID);
                lblDocGUID.Text = (node.DocumentGUID == Guid.Empty) ? ResHelper.Dash : node.DocumentGUID.ToString();
                lblDocID.Text = Convert.ToString(node.DocumentID);

                // Culture
                CultureInfo ci = CultureInfoProvider.GetCultureInfo(node.DocumentCulture);
                lblCulture.Text = ((ci != null) ?  ResHelper.LocalizeString(ci.CultureName) : node.DocumentCulture);

                lblPublished.Text = (node.IsPublished ? "<span class=\"DocumentPublishedYes\">" + GetString("General.Yes") + "</span>" : "<span class=\"DocumentPublishedNo\">" + GetString("General.No") + "</span>");

                if (!RequestHelper.IsPostBack())
                {
                    // Init radio buttons for cache settings
                    if (isRoot)
                    {
                        radInherit.Visible = false;
                        chkCssStyle.Visible = false;
                        switch (node.NodeCacheMinutes)
                        {
                            case -1:
                                // Cache is off
                                radNo.Checked = true;
                                radYes.Checked = false;
                                radInherit.Checked = false;
                                txtCacheMinutes.Text = "";
                                break;

                            case 0:
                                // Cache is off
                                radNo.Checked = true;
                                radYes.Checked = false;
                                radInherit.Checked = false;
                                txtCacheMinutes.Text = "";
                                break;

                            default:
                                // Cache is enabled
                                radNo.Checked = false;
                                radYes.Checked = true;
                                radInherit.Checked = false;
                                txtCacheMinutes.Text = node.NodeCacheMinutes.ToString();
                                break;
                        }
                    }
                    else
                    {
                        switch (node.NodeCacheMinutes)
                        {
                            case -1:
                                // Cache setting is inherited
                                radNo.Checked = false;
                                radYes.Checked = false;
                                radInherit.Checked = true;
                                txtCacheMinutes.Text = "";
                                break;

                            case 0:
                                // Cache is off
                                radNo.Checked = true;
                                radYes.Checked = false;
                                radInherit.Checked = false;
                                txtCacheMinutes.Text = "";
                                break;

                            default:
                                // Cache is enabled
                                radNo.Checked = false;
                                radYes.Checked = true;
                                radInherit.Checked = false;
                                txtCacheMinutes.Text = node.NodeCacheMinutes.ToString();
                                break;
                        }
                    }

                    if (!radYes.Checked)
                    {
                        txtCacheMinutes.Enabled = false;
                    }
                }


                if (!RequestHelper.IsPostBack())
                {
                    if (node.GetValue("DocumentStylesheetID") == null)
                    {
                        // If default site not exist edit is set to -1 - disabled
                        if (CMSContext.CurrentSiteStylesheet != null)
                        {
                            ctrlSiteSelectStyleSheet.Value = "default";
                        }
                        else
                        {
                            ctrlSiteSelectStyleSheet.Value = -1;
                        }
                    }
                    else
                    {
                        // If stylesheet is inherited from parent document
                        if (ValidationHelper.GetInteger(node.GetValue("DocumentStylesheetID"), 0) == -1)
                        {
                            if (!isRoot)
                            {
                                chkCssStyle.Checked = true;

                                // Get parent stylesheet
                                string value = PageInfoProvider.GetParentProperty(CMSContext.CurrentSite.SiteID, node.NodeAliasPath, "(DocumentStylesheetID <> -1 OR DocumentStylesheetID IS NULL) AND DocumentCulture = N'" + SqlHelperClass.GetSafeQueryString(node.DocumentCulture, false) + "'", "DocumentStylesheetID");

                                if (String.IsNullOrEmpty(value))
                                {
                                    // If default site stylesheet not exist edit is set to -1 - disabled
                                    if (CMSContext.CurrentSiteStylesheet != null)
                                    {
                                        ctrlSiteSelectStyleSheet.Value = "default";
                                    }
                                    else
                                    {
                                        ctrlSiteSelectStyleSheet.Value = -1;
                                    }
                                }
                                else
                                {
                                    // Set parent stylesheet to current document
                                    ctrlSiteSelectStyleSheet.Value = value;
                                }
                            }
                        }
                        else
                        {
                            ctrlSiteSelectStyleSheet.Value = node.GetValue("DocumentStylesheetID");
                        }
                    }
                }

                // Disable new button if document inherit stylesheet
                if (!isRoot && chkCssStyle.Checked)
                {
                    ctrlSiteSelectStyleSheet.Enabled = false;
                    ctrlSiteSelectStyleSheet.ButtonNew.Enabled = false;
                }

                // Initialize Rating control
                RefreshCntRatingResult();

                double rating = 0.0f;
                if (node.DocumentRatings > 0)
                {
                    rating = node.DocumentRatingValue / node.DocumentRatings;
                }
                ratingControl.MaxRating = 10;
                ratingControl.CurrentRating = rating;
                ratingControl.Visible = true;
                ratingControl.Enabled = false;

                // Initialize Reset button for rating
                btnResetRating.Text = GetString("general.reset");
                btnResetRating.OnClientClick = "if (!confirm(" + ScriptHelper.GetString(GetString("GeneralProperties.ResetRatingConfirmation")) + ")) return false;";

                object[] param = new object[1];
                param[0] = node.DocumentID;

                // Check ad-hoc forum counts
                hasAdHocForum = (ModuleCommands.ForumsGetDocumentForumsCount(node.DocumentID) > 0);

                // Ad-Hoc message boards check
                hasAdHocBoard = (ModuleCommands.MessageBoardGetDocumentBoardsCount(node.DocumentID) > 0);

                plcAdHocForums.Visible = hasAdHocForum;
                plcAdHocBoards.Visible = hasAdHocBoard;
            }
        }
        else
        {
            btnResetRating.Visible = false;
        }
    }


    /// <summary>
    /// Initializes the label with specified user text.
    /// </summary>
    private void SetUserLabel(Label label, string columnName)
    {
        // Get the user ID
        int userId = ValidationHelper.GetInteger(node.GetValue(columnName), 0);
        if (userId > 0)
        {
            // Get the user object
            UserInfo ui = null;
            string key = "user_" + userId;
            object userObject = RequestStockHelper.GetItem(key);
            if (userObject != null)
            {
                ui = (UserInfo)userObject;
            }
            else
            {
                // Get user object from database
                ui = UserInfoProvider.GetUserInfo(userId);
                RequestStockHelper.Add(key, ui);
            }

            if (ui != null)
            {
                label.Text = HTMLHelper.HTMLEncode(ui.FullName);
            }
        }
        else
        {
            label.Text = GetString("general.selectnone");
        }
    }


    protected void btnClear_Click(object sender, EventArgs e)
    {
        if (node != null)
        {
            // Check modify permissions
            if (CMSContext.CurrentUser.IsAuthorizedPerDocument(node, NodePermissionsEnum.Modify) == AuthorizationResultEnum.Denied)
            {
                return;
            }

            // Clear the output cache with the children
            node.ClearOutputCache(true, true);

            this.lblCacheInfo.Text = GetString("GeneralProperties.CacheCleared");
            this.lblCacheInfo.Visible = true;
        }
    }


    protected void lnkSave_Click(object sender, EventArgs e)
    {
        // Get the document
        TreeProvider tree = new TreeProvider(CMSContext.CurrentUser);
        node = tree.SelectSingleNode(nodeId, CMSContext.PreferredCultureCode);
        if (node != null)
        {
            // Check modify permissions
            if (CMSContext.CurrentUser.IsAuthorizedPerDocument(node, NodePermissionsEnum.Modify) == AuthorizationResultEnum.Denied)
            {
                return;
            }

            bool correct = true;

            // OWNER group is displayed by UI profile
            if (!this.pnlUIOwner.IsHidden)
            {
                // Set owner
                int ownerId = ValidationHelper.GetInteger(usrOwner.Value, 0);
                if (ownerId > 0)
                {
                    node.SetValue("NodeOwner", usrOwner.Value);
                }
                else
                {
                    node.SetValue("NodeOwner", null);
                }
            }

            // Search
            node.DocumentSearchExcluded = chkExcludeFromSearch.Checked;

            // DESIGN group is displayed by UI profile
            if (!this.pnlUIDesign.IsHidden)
            {
                node.SetValue("DocumentStylesheetID", -1);
                if (!chkCssStyle.Checked)
                {
                    // Set stylesheet
                    int selectedCssId = ValidationHelper.GetInteger(ctrlSiteSelectStyleSheet.Value, 0);
                    if (selectedCssId < 1)
                    {
                        node.SetValue("DocumentStylesheetID", null);
                    }
                    else
                    {
                        node.SetValue("DocumentStylesheetID", selectedCssId);
                    }

                    ctrlSiteSelectStyleSheet.CurrentDropDown.Enabled = true;
                }
                else
                {
                    ctrlSiteSelectStyleSheet.CurrentDropDown.Enabled = false;
                }
            }

            // CACHE group is displayed by UI profile
            bool clearCache = false;
            if (!this.pnlUICache.IsHidden)
            {
                // Cache minutes
                int cacheMinutes = 0;
                if (radNo.Checked)
                {
                    cacheMinutes = 0;
                    txtCacheMinutes.Text = "";
                }
                else if (radYes.Checked)
                {
                    cacheMinutes = ValidationHelper.GetInteger(txtCacheMinutes.Text, -5);
                    if (cacheMinutes <= 0)
                    {
                        correct = false;
                    }
                }
                else if (radInherit.Checked)
                {
                    cacheMinutes = -1;
                    txtCacheMinutes.Text = "";
                }

                // Set cache minutes                
                if (cacheMinutes != node.NodeCacheMinutes)
                {
                    node.NodeCacheMinutes = cacheMinutes;
                    clearCache = true;
                }
            }

            if (correct)
            {
                node.DocumentLogVisitActivity = (chkPageVisitInherit.Checked ? (bool?)null : chkLogPageVisit.Checked);
                // Save the data
                node.Update();

                // Update search index for node
                if ((node.PublishedVersionExists) && (SearchIndexInfoProvider.SearchEnabled))
                {
                    SearchTaskInfoProvider.CreateTask(SearchTaskTypeEnum.Update, PredefinedObjectType.DOCUMENT, SearchHelper.ID_FIELD, node.GetSearchID());
                }

                // Log synchronization
                DocumentSynchronizationHelper.LogDocumentChange(node, TaskTypeEnum.UpdateDocument, tree);

                // Clear cache if cache settings changed
                if (clearCache)
                {
                    node.ClearOutputCache(true, true);
                }

                lblInfo.Text = GetString("General.ChangesSaved");
                lblInfo.Visible = true;
                ReloadData();
            }
            else
            {
                // Show error message
                lblError.Text = GetString("GeneralProperties.BadCacheMinutes");
                lblError.Visible = true;
            }
        }
    }


    void btnResetPreviewGuid_Click(object sender, ImageClickEventArgs e)
    {
        if (node != null)
        {
            // Check modify permissions
            if (CMSContext.CurrentUser.IsAuthorizedPerDocument(node, NodePermissionsEnum.Modify) == AuthorizationResultEnum.Denied)
            {
                return;
            }
            
            node.DocumentWorkflowCycleGUID = Guid.NewGuid();
            node.Update();

            lblPreviewLink.Text = ResHelper.GetString("GeneralProperties.PreviewLinkGenerated");
            lblPreviewLink.Visible = true;
            InitPreviewUrl();
        }
    }


    /// <summary>
    /// Disables form editing.
    /// </summary>
    protected void DisableFormEditing()
    {
        canEdit = false;

        // Disable all panels
        pnlDesign.Enabled = false;
        pnlCache.Enabled = false;
        pnlOwner.Enabled = false;
        pnlSearch.Enabled = false;
        pnlOnlineMarketing.Enabled = false;

        // Disable 'save button'
        lnkSave.Enabled = false;
        lnkSave.CssClass = "MenuItemEditDisabled";
        imgSave.ImageUrl = GetImageUrl("CMSModules/CMS_Content/EditMenu/savedisabled.png");

        // Disable rating and owner selector
        btnResetPreviewGuid.Enabled = false;
        btnResetPreviewGuid.CssClass = "Disabled";
        btnResetRating.Enabled = false;
        btnClear.Enabled = false;
        usrOwner.Enabled = false;
        if (fcDocumentGroupSelector != null)
        {
            fcDocumentGroupSelector.Enabled = false;
        }
    }


    protected void radInherit_CheckedChanged(object sender, EventArgs e)
    {
        txtCacheMinutes.Enabled = false;

        // Enable textbox for cache minutes
        if (radYes.Checked)
        {
            txtCacheMinutes.Enabled = true;
        }
    }


    protected void chkCssStyle_CheckedChanged(object sender, EventArgs e)
    {
        if (chkCssStyle.Checked)
        {
            // Set stylesheet to stylesheet selector
            ctrlSiteSelectStyleSheet.CurrentDropDown.Enabled = false;
            ctrlSiteSelectStyleSheet.ButtonNew.Enabled = false;

            string value = PageInfoProvider.GetParentProperty(CMSContext.CurrentSite.SiteID, node.NodeAliasPath, "(DocumentStylesheetID <> -1 OR DocumentStylesheetID IS NULL) AND DocumentCulture = N'" + SqlHelperClass.GetSafeQueryString(node.DocumentCulture, false) + "'", "DocumentStylesheetID");
            if (String.IsNullOrEmpty(value))
            {
                // If default site stylesheet not exist edit is set to -1 - disabled
                if (CMSContext.CurrentSiteStylesheet != null)
                {
                    ctrlSiteSelectStyleSheet.CurrentDropDown.SelectedValue = "default";
                }
                else
                {
                    ctrlSiteSelectStyleSheet.CurrentDropDown.SelectedValue = "-1";
                }
            }
            else
            {
                try
                {
                    ctrlSiteSelectStyleSheet.CurrentDropDown.SelectedValue = value;
                }
                catch
                {
                }
            }
        }
        else
        {
            ctrlSiteSelectStyleSheet.CurrentDropDown.Enabled = true;
            ctrlSiteSelectStyleSheet.ButtonNew.Enabled = true;
        }
    }


    /// <summary>
    /// Refreshes current rating result.
    /// </summary>
    protected void RefreshCntRatingResult()
    {
        string msg = null;

        // Avoid division by zero
        if ((node != null) && (node.DocumentRatings > 0))
        {
            msg = String.Format(GetString("GeneralProperties.ContentRatingResult"), (node.DocumentRatingValue * 10) / node.DocumentRatings, node.DocumentRatings);
        }

        // Document wasn't rated
        if (msg == null)
        {
            msg = GetString("general.na");
        }

        lblContentRatingResult.Text = msg;
    }


    /// <summary>
    /// Resets content rating score.
    /// </summary>
    /// <param name="sender">Sender</param>
    /// <param name="e">Args</param>
    protected void btnResetRating_Click(object sender, EventArgs e)
    {
        if (node != null)
        {
            // Check modify permissions
            if (CMSContext.CurrentUser.IsAuthorizedPerDocument(node, NodePermissionsEnum.Modify) == AuthorizationResultEnum.Denied)
            {
                return;
            }

            // Reset rating
            TreeProvider.ResetRating(node);
            RefreshCntRatingResult();
            ratingControl.CurrentRating = 0.0;
            ratingControl.ReloadData();
        }
    }


    private void InitPreviewUrl()
    {
        if (node.DocumentWorkflowCycleGUID != Guid.Empty)
        {
            lnkPreviewURL.Visible = true;
            lblNoPreviewGuid.Visible = false;
            lnkPreviewURL.Text = ResHelper.GetString("GeneralProperties.ShowPreview");
            bool isFile = string.Equals(node.NodeClassName, "cms.file", StringComparison.InvariantCultureIgnoreCase);
            lnkPreviewURL.NavigateUrl = node.GetPreviewLink(CurrentUser.UserName, isFile);
        }
        else
        {
            lnkPreviewURL.Visible = false;
            lblNoPreviewGuid.Visible = true;
            lblNoPreviewGuid.Text = GetString("GeneralProperties.NoPreviewGuid");
        }
    }

    #endregion
}
