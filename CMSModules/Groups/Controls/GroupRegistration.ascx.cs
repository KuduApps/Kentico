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

using CMS.Community;
using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.SiteProvider;
using CMS.TreeEngine;
using CMS.WorkflowEngine;
using CMS.SettingsProvider;
using CMS.UIControls;
using CMS.EmailEngine;
using CMS.Forums;
using CMS.MediaLibrary;

using TreeNode = CMS.TreeEngine.TreeNode;


public partial class CMSModules_Groups_Controls_GroupRegistration : CMSUserControl
{
    #region "Constants"

    private const string FORUM_DOCUMENT_ALIAS = "forums";

    #endregion

    
    #region "Variables"

    private int mSiteId = 0;
    private bool mRequireApproval = true;
    private string mGroupTemplateSourceAliasPath = null;
    private string mGroupTemplateTargetAliasPath = null;
    private string mGroupProfileURLPath = String.Empty;
    private bool mCombineWithDefaultCulture = false;
    private string mRedirectToURL = "";
    private string mGroupNameLabelText = null;
    private string mSuccessfullRegistrationText;
    private string mSuccessfullRegistrationWaitingForApprovalText;
    private bool mHideFormAfterRegistration = true;
    private string codeName = "";
    private bool mCreateForum = true;
    private bool mCreateMediaLibrary = true;
    private bool mCreateSearchIndexes = true;
    private string mSendWaitingForApprovalEmailTo = String.Empty;
    private TreeProvider mTreeProvider = null;

    #endregion


    #region "Public properties"

    /// <summary>
    /// Gets or sets the value that indicates whether form should be hidden after successful registration.
    /// </summary>

    public bool HideFormAfterRegistration
    {
        get
        {
            return mHideFormAfterRegistration;
        }
        set
        {
            mHideFormAfterRegistration = value;
        }
    }


    /// <summary>
    /// Gets or sets text which should be displayed after successful registration.
    /// </summary>
    public string SuccessfullRegistrationText
    {
        get
        {
            return DataHelper.GetNotEmpty(mSuccessfullRegistrationText, GetString("group.group.succreg"));
        }
        set
        {
            mSuccessfullRegistrationText = value;
        }
    }


    /// <summary>
    /// Gets or sets text which should be displayed after successful registration and waiting for approving.
    /// </summary>
    public string SuccessfullRegistrationWaitingForApprovalText
    {
        get
        {
            return DataHelper.GetNotEmpty(mSuccessfullRegistrationWaitingForApprovalText, GetString("group.group.succregapprove"));
        }
        set
        {
            mSuccessfullRegistrationWaitingForApprovalText = value;
        }
    }


    /// <summary>
    /// Gets or sets the label text of group name.
    /// </summary>
    public string GroupNameLabelText
    {
        get
        {
            if (mGroupNameLabelText == null)
            {
                mGroupNameLabelText = GetString("general.displayname") + ResHelper.Colon;
            }
            return mGroupNameLabelText;
        }
        set
        {
            mGroupNameLabelText = value;
            this.lblDisplayName.Text = value;
        }
    }


    /// <summary>
    /// Current site ID.
    /// </summary>
    public int SiteID
    {
        get
        {
            return this.mSiteId;
        }
        set
        {
            this.mSiteId = value;
        }
    }


    /// <summary>
    /// If true, the group must be approved before it can be active.
    /// </summary>
    public bool CombineWithDefaultCulture
    {
        get
        {
            return this.mCombineWithDefaultCulture;
        }
        set
        {
            this.mCombineWithDefaultCulture = value;
        }
    }


    /// <summary>
    /// If true, the group must be approved before it can be active.
    /// </summary>
    public bool RequireApproval
    {
        get
        {
            return this.mRequireApproval;
        }
        set
        {
            this.mRequireApproval = value;
        }
    }


    /// <summary>
    /// Alias path of the document structure which will be copied as the group content.
    /// </summary>
    public string GroupTemplateSourceAliasPath
    {
        get
        {
            return this.mGroupTemplateSourceAliasPath;
        }
        set
        {
            this.mGroupTemplateSourceAliasPath = value;
        }
    }


    /// <summary>
    /// Alias where the group content will be created by copying the source template.
    /// </summary>
    public string GroupTemplateTargetAliasPath
    {
        get
        {
            return this.mGroupTemplateTargetAliasPath;
        }
        set
        {
            this.mGroupTemplateTargetAliasPath = value;
        }
    }


    /// <summary>
    /// Gets or sets the document URL under which will be accessible the profile of newly created group.
    /// </summary>
    public string GroupProfileURLPath
    {
        get
        {
            return mGroupProfileURLPath;
        }
        set
        {
            mGroupProfileURLPath = value;
        }
    }


    /// <summary>
    /// Gets or sets the url, where is user redirected after registration.
    /// </summary>
    public string RedirectToURL
    {
        get
        {
            return this.mRedirectToURL;
        }
        set
        {
            this.mRedirectToURL = value;
        }
    }


    /// <summary>
    /// Emails of admins capable of approving the group.
    /// </summary>
    public string SendWaitingForApprovalEmailTo
    {
        get
        {
            return this.mSendWaitingForApprovalEmailTo;
        }
        set
        {
            this.mSendWaitingForApprovalEmailTo = value;
        }
    }


    /// <summary>
    /// Indicates if group forum should be created.
    /// </summary>
    public bool CreateForum
    {
        get
        {
            return mCreateForum;
        }
        set
        {
            mCreateForum = value;
        }
    }


    /// <summary>
    /// Indicates if search indexes should be created.
    /// </summary>
    public bool CreateSearchIndexes
    {
        get
        {
            return mCreateSearchIndexes;
        }
        set
        {
            mCreateSearchIndexes = value;
        }
    }


    /// <summary>
    /// Indicates if group media libraries should be created.
    /// </summary>
    public bool CreateMediaLibrary
    {
        get
        {
            return mCreateMediaLibrary;
        }
        set
        {
            mCreateMediaLibrary = value;
        }
    }


    /// <summary>
    /// Gets instance of tree provider.
    /// </summary>
    private TreeProvider TreeProvider
    {
        get
        {
            if (mTreeProvider == null)
            {
                mTreeProvider = new TreeProvider(CMSContext.CurrentUser);
            }
            return mTreeProvider;
        }
    }

    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        InitializeForm();
    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        SaveData();
    }


    #region "Public methods"

    /// <summary>
    /// Updates the current Group or creates new if no GroupID is present.
    /// </summary>
    public void SaveData()
    {
        // Check banned ip
        if (!BannedIPInfoProvider.IsAllowed(CMSContext.CurrentSiteName, BanControlEnum.AllNonComplete))
        {
            lblError.Visible = true;
            lblError.Text = GetString("General.BannedIP");
            return;
        }

        // Validate form entries
        string errorMessage = ValidateForm();
        if (errorMessage == "")
        {
            try
            {
                codeName = GetSafeCodeName();
                codeName = GetUniqueCodeName(codeName);

                GroupInfo group = new GroupInfo();
                group.GroupDisplayName = this.txtDisplayName.Text;
                group.GroupName = codeName;
                group.GroupDescription = this.txtDescription.Text;
                group.GroupAccess = GetGroupAccess();
                group.GroupSiteID = this.mSiteId;
                group.GroupApproveMembers = GetGroupApproveMembers();

                // Set columns GroupCreatedByUserID and GroupApprovedByUserID to current user
                CurrentUserInfo user = CMSContext.CurrentUser;

                if (user != null)
                {
                    group.GroupCreatedByUserID = user.UserID;

                    if ((!this.RequireApproval) || (CurrentUserIsAdmin()))
                    {
                        group.GroupApprovedByUserID = user.UserID;
                        group.GroupApproved = true;
                    }
                }

                // Save Group in the database
                GroupInfoProvider.SetGroupInfo(group);

                // Create group admin role
                RoleInfo roleInfo = new RoleInfo();
                roleInfo.DisplayName = "Group admin";
                roleInfo.RoleName = group.GroupName + "_groupadmin";
                roleInfo.RoleGroupID = group.GroupID;
                roleInfo.RoleIsGroupAdministrator = true;
                roleInfo.SiteID = this.mSiteId;
                // Save group admin role
                RoleInfoProvider.SetRoleInfo(roleInfo);

                if (user != null)
                {
                    // Set user as member of group
                    GroupMemberInfo gmi = new GroupMemberInfo();
                    gmi.MemberUserID = user.UserID;
                    gmi.MemberGroupID = group.GroupID;
                    gmi.MemberJoined = DateTime.Now;
                    gmi.MemberStatus = GroupMemberStatus.Approved;
                    gmi.MemberApprovedWhen = DateTime.Now;
                    gmi.MemberApprovedByUserID = user.UserID;

                    // Save user as member of group
                    GroupMemberInfoProvider.SetGroupMemberInfo(gmi);

                    // Set user as member of admin group role
                    UserRoleInfo userRole = new UserRoleInfo();
                    userRole.UserID = user.UserID;
                    userRole.RoleID = roleInfo.RoleID;

                    // Save user as member of admin group role
                    UserRoleInfoProvider.SetUserRoleInfo(userRole);
                }

                // Clear user session a request
                CMSContext.CurrentUser.Invalidate();
                CMSContext.CurrentUser = null;

                string culture = CultureHelper.EnglishCulture.ToString();
                if (CMSContext.CurrentDocument != null)
                {
                    culture = CMSContext.CurrentDocument.DocumentCulture;
                }

                // Copy document
                errorMessage = GroupInfoProvider.CopyGroupDocument(group, CMSContext.ResolveCurrentPath(GroupTemplateSourceAliasPath), CMSContext.ResolveCurrentPath(GroupTemplateTargetAliasPath), GroupProfileURLPath, culture, this.CombineWithDefaultCulture, CMSContext.CurrentUser, roleInfo);

                if (errorMessage != "")
                {
                    // Display error message
                    this.lblError.Text = errorMessage;
                    this.lblError.Visible = true;
                    return;
                }

                // Create group forum
                if (CreateForum)
                {
                    CreateGroupForum(group);

                    // Create group forum search index 
                    if (CreateSearchIndexes)
                    {
                        CreateGroupForumSearchIndex(group);
                    }
                }

                // Create group media library
                if (CreateMediaLibrary)
                {
                    CreateGroupMediaLibrary(group);
                }

                // Create search index for group documents
                if (CreateSearchIndexes)
                {
                    CreateGroupContentSearchIndex(group);
                }

                // Display information on success
                this.lblInfo.Text = GetString("group.group.createdinfo");
                this.lblInfo.Visible = true;

                // If URL is set, redirect user to specified page
                if (!String.IsNullOrEmpty(this.RedirectToURL))
                {
                    URLHelper.Redirect(ResolveUrl(CMSContext.GetUrl(this.RedirectToURL)));
                }

                // After registration message
                if ((this.RequireApproval) && (!CurrentUserIsAdmin()))
                {
                    this.lblInfo.Text = this.SuccessfullRegistrationWaitingForApprovalText;

                    // Send approval email to admin
                    if (!String.IsNullOrEmpty(SendWaitingForApprovalEmailTo))
                    {
                        // Create the message
                        EmailTemplateInfo eti = EmailTemplateProvider.GetEmailTemplate("Groups.WaitingForApproval", CMSContext.CurrentSiteName);
                        if (eti != null)
                        {
                            EmailMessage message = new EmailMessage();
                            if (String.IsNullOrEmpty(eti.TemplateFrom))
                            {
                                message.From = SettingsKeyProvider.GetStringValue(CMSContext.CurrentSiteName + ".CMSSendEmailNotificationsFrom");
                            }
                            else
                            {
                                message.From = eti.TemplateFrom;
                            }

                            MacroResolver resolver = CMSContext.CurrentResolver;
                            resolver.SourceData = new object[] { group };
                            resolver.SetNamedSourceData("Group", group);

                            message.Recipients = SendWaitingForApprovalEmailTo;
                            message.Subject = resolver.ResolveMacros(eti.TemplateSubject);
                            message.Body = resolver.ResolveMacros(eti.TemplateText);

                            resolver.EncodeResolvedValues = false;
                            message.PlainTextBody = resolver.ResolveMacros(eti.TemplatePlainText);

                            // Send the message using email engine
                            EmailSender.SendEmail(message);
                        }
                    }
                }
                else
                {
                    string groupPath = SettingsKeyProvider.GetStringValue(CMSContext.CurrentSiteName + ".CMSGroupProfilePath");
                    string url = String.Empty;

                    if (!String.IsNullOrEmpty(groupPath))
                    {
                        url = TreePathUtils.GetUrl(groupPath.Replace("{GroupName}", group.GroupName));
                    }
                    this.lblInfo.Text = String.Format(this.SuccessfullRegistrationText, url);
                }

                // Hide form
                if (this.HideFormAfterRegistration)
                {
                    this.plcForm.Visible = false;
                }
                else
                {
                    ClearForm();
                }
            }
            catch (Exception ex)
            {
                // Display error message
                this.lblError.Text = GetString("general.erroroccurred") + ex.Message;
                this.lblError.Visible = true;
            }
        }
        else
        {
            // Display error message
            this.lblError.Text = errorMessage;
            this.lblError.Visible = true;
        }
    }

    #endregion


    #region "Private methods"


    /// <summary>
    /// Clears the fields of the form to default values.
    /// </summary>
    private void ClearForm()
    {
        this.txtDescription.Text = "";
        this.txtDisplayName.Text = "";
        this.radGroupMembers.Checked = false;
        this.radSiteMembers.Checked = false;
        this.radMembersApproved.Checked = false;
        this.radMembersInvited.Checked = false;
        this.radMembersAny.Checked = true;
        this.radAnybody.Checked = true;
    }


    /// <summary>
    /// Returns true iff current user is Global administrator or Community administrator.
    /// </summary>
    private bool CurrentUserIsAdmin()
    {
        CurrentUserInfo ui = CMSContext.CurrentUser;
        if (ui != null)
        {
            SiteInfo si = SiteInfoProvider.GetSiteInfo(this.SiteID);
            if (si != null)
            {
                return ui.IsInRole("CMSCommunityAdmin", si.SiteName) || ui.IsGlobalAdministrator;
            }
        }
        return false;
    }


    /// <summary>
    /// Initializes the contols in the form.
    /// </summary>
    private void InitializeForm()
    {
        // Intialize labels
        this.lblDisplayName.Text = this.GroupNameLabelText;
        this.lblDescription.Text = GetString("general.description") + ResHelper.Colon;
        this.lblApproveMembers.Text = GetString("group.group.approvemembers") + ResHelper.Colon;
        this.lblContentAccess.Text = GetString("group.group.contentaccess") + ResHelper.Colon;

        // Initialize radiobuttons
        this.radAnybody.Text = GetString("group.group.accessanybody");
        this.radGroupMembers.Text = GetString("group.group.accessgroupmembers");
        this.radSiteMembers.Text = GetString("group.group.accesssitemembers");
        this.radMembersAny.Text = GetString("group.group.approveany");
        this.radMembersApproved.Text = GetString("group.group.approveapproved");
        this.radMembersInvited.Text = GetString("group.group.approveinvited");

        // Initialize errors
        this.rfvDisplayName.ErrorMessage = GetString("general.requiresdisplayname");

        // Initialize buttons
        this.btnSave.Text = GetString("general.ok");
        this.txtDisplayName.IsLiveSite = this.IsLiveSite;
    }


    /// <summary>
    /// Returns correct number according to radiobutton selection.
    /// </summary>
    private SecurityAccessEnum GetGroupAccess()
    {
        if (this.radSiteMembers.Checked)
        {
            return SecurityAccessEnum.AuthenticatedUsers;
        }
        else if (this.radGroupMembers.Checked)
        {
            return SecurityAccessEnum.GroupMembers;
        }
        else
        {
            return SecurityAccessEnum.AllUsers;
        }
    }


    /// <summary>
    /// Returns correct number according to radiobutton selection.
    /// </summary>
    private GroupApproveMembersEnum GetGroupApproveMembers()
    {
        if (this.radMembersApproved.Checked)
        {
            return GroupApproveMembersEnum.ApprovedCanJoin;
        }
        else if (this.radMembersInvited.Checked)
        {
            return GroupApproveMembersEnum.InvitedWithoutApproval;
        }
        else
        {
            return GroupApproveMembersEnum.AnyoneCanJoin;
        }
    }


    /// <summary>
    /// Returns safe codename.
    /// </summary>
    private string GetSafeCodeName()
    {
        return URLHelper.GetSafeUrlPart(ValidationHelper.GetCodeName(this.txtDisplayName.Text.Trim(), null, 89, true, true), CMSContext.CurrentSiteName);
    }


    /// <summary>
    /// Returns safe and unique codename for specified display name.
    /// </summary>
    private string GetUniqueCodeName(string codeName)
    {
        int maxLength = 89;
        codeName = SqlHelperClass.GetSafeQueryString(codeName, false);
        string originalCodename = codeName;

        int postfixValue = 1;

        // Loop until unique code name is not found
        while (true)
        {
            string where = "(GroupName = N'" + codeName + "'";
            if (this.mSiteId > 0)
            {
                where += " AND GroupSiteID = " + this.mSiteId;
            }
            where += ")";

            DataSet ds = GroupInfoProvider.GetGroups(where, null, 0, "groupid");
            // Default codename is unique
            if (DataHelper.DataSourceIsEmpty(ds))
            {
                return codeName;
            }

            codeName = GenerateCodeName(originalCodename, maxLength, "_" + postfixValue);
            postfixValue++;
        }
    }


    /// <summary>
    /// Generates unique codename with dependenc on maximal length and postfix value.
    /// </summary>
    /// <param name="codeName">Codename value</param>
    /// <param name="maxLength">Maximal length</param>
    /// <param name="postFix">Postfix value</param>
    private string GenerateCodeName(string codeName, int maxLength, string postFix)
    {
        if ((codeName.Length + postFix.Length) < maxLength)
        {
            return codeName + postFix;
        }

        codeName = codeName.Substring(0, codeName.Length - postFix.Length);
        return codeName + postFix;
    }


    /// <summary>
    /// Validates the form entries.
    /// </summary>
    /// <returns>Empty string if validation passed otherwise error message is returned</returns>
    private string ValidateForm()
    {
        string errorMessage = new Validator().NotEmpty(this.txtDisplayName.Text, this.rfvDisplayName.ErrorMessage).Result;

        if (errorMessage != "")
        {
            return errorMessage;
        }
        return errorMessage;
    }


    /// <summary>
    /// Creates group forum.
    /// </summary>
    /// <param name="group">Particular group info object</param>
    private void CreateGroupForum(GroupInfo group)
    {
        #region "Create forum group"

        // Get forum group code name
        string forumGroupCodeName = "Forums_group_" + group.GroupGUID;

        // Check if forum group with given name already exists
        if (ForumGroupInfoProvider.GetForumGroupInfo(forumGroupCodeName, CMSContext.CurrentSiteID) != null)
        {
            return;
        }

        // Create forum base URL
        string baseUrl = null;
        TreeNode groupDocument = TreeProvider.SelectSingleNode(group.GroupNodeGUID, CMSContext.CurrentDocumentCulture.CultureCode, CMSContext.CurrentSiteName);
        if (groupDocument != null)
        {
            baseUrl = CMSContext.GetUrl(groupDocument.NodeAliasPath + "/" + FORUM_DOCUMENT_ALIAS);

        }

        ForumGroupInfo forumGroupObj = new ForumGroupInfo();
        string suffix = " forums";
        forumGroupObj.GroupDisplayName = TextHelper.LimitLength(group.GroupDisplayName, 200 - suffix.Length, "") + suffix;
        forumGroupObj.GroupName = forumGroupCodeName;
        forumGroupObj.GroupOrder = 0;
        forumGroupObj.GroupEnableQuote = true;
        forumGroupObj.GroupGroupID = group.GroupID;
        forumGroupObj.GroupSiteID = CMSContext.CurrentSiteID;
        forumGroupObj.GroupBaseUrl = baseUrl;

        // Additional settings
        forumGroupObj.GroupEnableCodeSnippet = true;
        forumGroupObj.GroupEnableFontBold = true;
        forumGroupObj.GroupEnableFontColor = true;
        forumGroupObj.GroupEnableFontItalics = true;
        forumGroupObj.GroupEnableFontStrike = true;
        forumGroupObj.GroupEnableFontUnderline = true;
        forumGroupObj.GroupEnableQuote = true;
        forumGroupObj.GroupEnableURL = true;
        forumGroupObj.GroupEnableImage = true;

        // Set forum group info
        ForumGroupInfoProvider.SetForumGroupInfo(forumGroupObj);

        #endregion

        #region "Create forum"

        string codeName = "General_discussion_group_" + group.GroupGUID;

        // Check if forum with given name already exists
        if (ForumInfoProvider.GetForumInfo(codeName, CMSContext.CurrentSiteID, group.GroupID) != null)
        {
            return;
        }

        // Create new forum object
        ForumInfo forumObj = new ForumInfo();
        forumObj.ForumSiteID = CMSContext.CurrentSiteID;
        forumObj.ForumIsLocked = false;
        forumObj.ForumOpen = true;
        forumObj.ForumDisplayEmails = false;
        forumObj.ForumRequireEmail = false;
        forumObj.ForumDisplayName = "General discussion";
        forumObj.ForumName = codeName;
        forumObj.ForumGroupID = forumGroupObj.GroupID;
        forumObj.ForumModerated = false;
        forumObj.ForumAccess = 40000;
        forumObj.ForumPosts = 0;
        forumObj.ForumThreads = 0;
        forumObj.ForumPostsAbsolute = 0;
        forumObj.ForumThreadsAbsolute = 0;
        forumObj.ForumOrder = 0;
        forumObj.ForumUseCAPTCHA = false;
        forumObj.SetValue("ForumHTMLEditor", null);

        // Set security
        forumObj.AllowAccess = SecurityAccessEnum.GroupMembers;
        forumObj.AllowAttachFiles = SecurityAccessEnum.GroupMembers;
        forumObj.AllowMarkAsAnswer = SecurityAccessEnum.GroupMembers;
        forumObj.AllowPost = SecurityAccessEnum.GroupMembers;
        forumObj.AllowReply = SecurityAccessEnum.GroupMembers;
        forumObj.AllowSubscribe = SecurityAccessEnum.GroupMembers;

        if (ForumInfoProvider.LicenseVersionCheck(URLHelper.GetCurrentDomain(), FeatureEnum.Forums, VersionActionEnum.Insert))
        {
            ForumInfoProvider.SetForumInfo(forumObj);
        }

        #endregion
    }


    /// <summary>
    /// Creates group media library.
    /// </summary>
    /// <param name="group">Particular group info object</param>
    private void CreateGroupMediaLibrary(GroupInfo group)
    {
        // Set general values
        string codeName = "Library_group_" + group.GroupGUID;

        // Check if library with same name already exists
        MediaLibraryInfo mlInfo = MediaLibraryInfoProvider.GetMediaLibraryInfo(codeName, CMSContext.CurrentSiteName);
        if (mlInfo != null)
        {
            return;
        }
        else
        {
            // Create new object (record) if needed
            mlInfo = new MediaLibraryInfo();
            string suffix = " media";
            mlInfo.LibraryDisplayName = TextHelper.LimitLength(group.GroupDisplayName, 200 - suffix.Length, "") + suffix;
            mlInfo.LibraryFolder = group.GroupName;
            mlInfo.LibraryName = codeName;
            mlInfo.LibraryDescription = "";
            mlInfo.LibraryGroupID = group.GroupID;
            mlInfo.LibrarySiteID = CMSContext.CurrentSiteID;

            // Set security
            mlInfo.FileCreate = SecurityAccessEnum.GroupMembers;
            mlInfo.FileDelete = SecurityAccessEnum.GroupMembers;
            mlInfo.FileModify = SecurityAccessEnum.GroupMembers;
            mlInfo.FolderCreate = SecurityAccessEnum.GroupMembers;
            mlInfo.FolderDelete = SecurityAccessEnum.GroupMembers;
            mlInfo.FolderModify = SecurityAccessEnum.GroupMembers;
            mlInfo.Access = SecurityAccessEnum.GroupMembers;

            try
            {
                MediaLibraryInfoProvider.SetMediaLibraryInfo(mlInfo);
            }
            catch
            {
                return;
            }

            // Create additional folders
            //MediaLibraryInfoProvider.CreateMediaLibraryFolder(CMSContext.CurrentSiteName, mlInfo.LibraryID, "Videos", false);
            //MediaLibraryInfoProvider.CreateMediaLibraryFolder(CMSContext.CurrentSiteName, mlInfo.LibraryID, "Other", false);
            //MediaLibraryInfoProvider.CreateMediaLibraryFolder(CMSContext.CurrentSiteName, mlInfo.LibraryID, "Photos & Images", false);
        }
    }


    /// <summary>
    /// Creates content search index.
    /// </summary>
    /// <param name="group">Particular group info object</param>
    private void CreateGroupContentSearchIndex(GroupInfo group)
    {
        string codeName = "default_group_" + group.GroupGUID;

        SearchIndexInfo sii = SearchIndexInfoProvider.GetSearchIndexInfo(codeName);
        if (sii == null)
        {

            // Create search index info
            sii = new SearchIndexInfo();
            sii.IndexName = codeName;
            string suffix = " - Default";
            sii.IndexDisplayName = TextHelper.LimitLength(group.GroupDisplayName, 200 - suffix.Length, "") + suffix;
            sii.IndexAnalyzerType = AnalyzerTypeEnum.StandardAnalyzer;
            sii.IndexType = PredefinedObjectType.DOCUMENT;
            sii.IndexIsCommunityGroup = false;

            // Create search index settings info
            SearchIndexSettingsInfo sisi = new SearchIndexSettingsInfo();
            sisi.ID = Guid.NewGuid();
            sisi.Path = mGroupTemplateTargetAliasPath + "/" + group.GroupName + "/%";
            sisi.SiteName = CMSContext.CurrentSiteName;
            sisi.Type = SearchIndexSettingsInfo.TYPE_ALLOWED;
            sisi.ClassNames = "";

            // Create settings item
            SearchIndexSettings sis = new SearchIndexSettings();

            // Update settings item
            sis.SetSearchIndexSettingsInfo(sisi);

            // Update xml value
            sii.IndexSettings = sis;
            SearchIndexInfoProvider.SetSearchIndexInfo(sii);

            // Assing to current website and current culture
            SearchIndexSiteInfoProvider.AddSearchIndexToSite(sii.IndexID, CMSContext.CurrentSiteID);
            CultureInfo ci = CMSContext.CurrentDocumentCulture;
            if (ci != null)
            {
                SearchIndexCultureInfoProvider.AddSearchIndexCulture(sii.IndexID, ci.CultureID);
            }

            // Register rebuild index action
            SearchTaskInfoProvider.CreateTask(SearchTaskTypeEnum.Rebuild, sii.IndexType, null, sii.IndexName);
        }

    }


    /// <summary>
    /// Creates forum search index.
    /// </summary>
    /// <param name="group">Particular group info object</param>
    private void CreateGroupForumSearchIndex(GroupInfo group)
    {
        string codeName = "forums_group_" + group.GroupGUID;

        SearchIndexInfo sii = SearchIndexInfoProvider.GetSearchIndexInfo(codeName);
        if (sii == null)
        {
            // Create search index info
            sii = new SearchIndexInfo();
            sii.IndexName = codeName;
            string suffix = " - Forums";
            sii.IndexDisplayName = TextHelper.LimitLength(group.GroupDisplayName, 200 - suffix.Length, "") + suffix;
            sii.IndexAnalyzerType = AnalyzerTypeEnum.StandardAnalyzer;
            sii.IndexType = PredefinedObjectType.FORUM;
            sii.IndexIsCommunityGroup = false;

            // Create search index settings info
            SearchIndexSettingsInfo sisi = new SearchIndexSettingsInfo();
            sisi.ID = Guid.NewGuid();
            sisi.ForumNames = "*_group_" + group.GroupGUID;
            sisi.Type = SearchIndexSettingsInfo.TYPE_ALLOWED;
            sisi.SiteName = CMSContext.CurrentSiteName;

            // Create settings item
            SearchIndexSettings sis = new SearchIndexSettings();

            // Update settings item
            sis.SetSearchIndexSettingsInfo(sisi);

            // Update xml value
            sii.IndexSettings = sis;
            SearchIndexInfoProvider.SetSearchIndexInfo(sii);

            // Assing to current website and current culture
            SearchIndexSiteInfoProvider.AddSearchIndexToSite(sii.IndexID, CMSContext.CurrentSiteID);
            CultureInfo ci = CMSContext.CurrentDocumentCulture;
            if (ci != null)
            {
                SearchIndexCultureInfoProvider.AddSearchIndexCulture(sii.IndexID, ci.CultureID);
            }
        }
    }

    #endregion
}
