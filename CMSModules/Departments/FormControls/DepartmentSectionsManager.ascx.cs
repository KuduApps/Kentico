using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.Collections;

using CMS.FormControls;
using CMS.SettingsProvider;
using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.WorkflowEngine;
using CMS.TreeEngine;
using CMS.SiteProvider;
using CMS.UIControls;
using CMS.Forums;
using CMS.MediaLibrary;
using CMS.ExtendedControls;
using CMS.FormEngine;
using CMS.DataEngine;
using CMS.Synchronization;

using TreeNode = CMS.TreeEngine.TreeNode;

public partial class CMSModules_Departments_FormControls_DepartmentSectionsManager : FormEngineUserControl
{
    #region "Constants"

    private const int ITEMS_PER_ROW = 4;
    private const string FORUM_DOCUMENT_ALIAS = "forums";
    private const string MEDIA_DOCUMENT_ALIAS = "media";
    private const string HIDDEN_DOCUMENT_ALIAS = ";home;search;rss;";

    #endregion


    #region "Variables"

    private DataSet mTemplateDocuments = null;
    private TreeProvider mTreeProvider = null;
    private bool mDocumentSaved = false;
    private ArrayList mCheckboxReferences = null;
    private string mSelectedValue = null;

    #endregion


    #region "Properties"

    /// <summary>
    /// Gets or sets selected departments.
    /// </summary>
    public override object Value
    {
        get
        {
            StringBuilder sb = new StringBuilder();
            if (CheckboxReferences.Count > 0)
            {
                // Get selected values from registered checkboxes
                foreach (CheckBox chk in CheckboxReferences)
                {
                    if (chk.Checked)
                    {
                        sb.Append(chk.Attributes["Value"].ToLower());
                        sb.Append(";");
                    }
                }
                return sb.ToString().TrimEnd(';');
            }
            return null;
        }
        set
        {
            mSelectedValue = ValidationHelper.GetString(value, null);
        }
    }


    /// <summary>
    /// Department template path.
    /// </summary>
    private string DepartmentTemplatePath
    {
        get
        {
            return SettingsKeyProvider.GetStringValue(CMSContext.CurrentSiteName + ".CMSDepartmentTemplatePath");
        }
    }


    /// <summary>
    /// Gets DataSet of first level template documents.
    /// </summary>
    public DataSet TemplateDocuments
    {
        get
        {
            if (mTemplateDocuments == null)
            {
                string templatePath = DepartmentTemplatePath;
                if (!String.IsNullOrEmpty(templatePath))
                {
                    templatePath = TreePathUtils.EnsureSingleNodePath(templatePath).TrimEnd('/') + "/%";
                    mTemplateDocuments = DocumentHelper.GetDocuments(CMSContext.CurrentSiteName, templatePath, null, false, null, "NodeAlias != ''", "NodeOrder ASC", 1, false, 0, "NodeID, DocumentCulture, NodeAlias, NodeClassID", null);
                }
            }
            return mTemplateDocuments;
        }
    }

    /// <summary>
    /// ArrayList with checkbox references.
    /// </summary>
    private ArrayList CheckboxReferences
    {
        get
        {
            if (mCheckboxReferences == null)
            {
                mCheckboxReferences = new ArrayList();
            }
            return mCheckboxReferences;
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


    #region "Control methods"

    /// <summary>
    /// Page load.
    /// </summary>
    protected void Page_Load(object sender, EventArgs e)
    {
        GenerateContent();

        // Attach on after save event
        Form.OnAfterSave += ProcessDepartment;

        //Atach Pre Render event
        Page.PreRender += Page_PreRender;
    }


    /// <summary>
    /// PreRender action on which security settings are set.
    /// </summary>
    void Page_PreRender(object sender, EventArgs e)
    {
        if (mDocumentSaved)
        {
            TreeNode editedNode = Form.EditedObject as TreeNode;

            // Create or rebuild department content index
            CreateDepartmentContentSearchIndex(editedNode);

            AclProvider aclProv = new AclProvider(editedNode.TreeProvider);
            if (aclProv.HasOwnACL(editedNode))
            {
                ForumInfo fi = ForumInfoProvider.GetForumInfo("Default_department_" + editedNode.NodeGUID, CMSContext.CurrentSiteID);
                MediaLibraryInfo mi = MediaLibraryInfoProvider.GetMediaLibraryInfo("Department_" + editedNode.NodeGUID, CMSContext.CurrentSiteName);

                // Check if forum of media library exists
                if ((fi != null) || (mi != null))
                {
                    // Get allowed roles ID
                    int aclID = ValidationHelper.GetInteger(editedNode.GetValue("NodeACLID"), 0);
                    DataSet listRoles = aclProv.GetAllowedRoles(aclID, NodePermissionsEnum.Read, "RoleID");
                    string roleIDs = null;

                    if (!DataHelper.DataSourceIsEmpty(listRoles))
                    {
                        IList<string> roles = SqlHelperClass.GetStringValues(listRoles.Tables[0], "RoleID");
                        roleIDs = TextHelper.Join(";", roles);
                    }

                    // Set permissions for forum
                    if (fi != null)
                    {
                        // Get resource object
                        ResourceInfo resForums = ResourceInfoProvider.GetResourceInfo("CMS.Forums");

                        // Get permissions IDs
                        DataSet dsForumPerm = PermissionNameInfoProvider.GetPermissionNames("ResourceID = " + resForums.ResourceId + " AND (PermissionName != '" + CMSAdminControl.PERMISSION_READ + "' AND PermissionName != '" + CMSAdminControl.PERMISSION_MODIFY + "')", null, 0, "PermissionID");
                        string forumPermissions = null;
                        if (!DataHelper.DataSourceIsEmpty(dsForumPerm))
                        {
                            foreach (DataRow drForumPerm in dsForumPerm.Tables[0].Rows)
                            {
                                forumPermissions += drForumPerm["PermissionID"] + ";";
                            }
                            forumPermissions = forumPermissions.TrimEnd(';');
                        }

                        // Delete old permissions apart attach file permission
                        ForumRoleInfoProvider.DeleteAllRoles("ForumID = " + fi.ForumID + " AND PermissionID IN (" + forumPermissions.Replace(";", ", ") + ")");

                        // Set forum permissions
                        ForumRoleInfoProvider.SetPermissions(fi.ForumID, roleIDs, forumPermissions);

                        // Log staging task
                        SynchronizationHelper.LogObjectChange(fi, TaskTypeEnum.UpdateObject);
                    }

                    // Set permissions for media library
                    if (mi != null)
                    {
                        // Get resource object
                        ResourceInfo resMediaLibs = ResourceInfoProvider.GetResourceInfo("CMS.MediaLibrary");

                        // Get permissions IDs
                        DataSet dsMediaLibPerm = PermissionNameInfoProvider.GetPermissionNames("ResourceID = " + resMediaLibs.ResourceId + " AND (PermissionName = 'LibraryAccess' OR PermissionName = 'FileCreate')", null, 0, "PermissionID");
                        string mediaLibPermissions = null;
                        if (!DataHelper.DataSourceIsEmpty(dsMediaLibPerm))
                        {
                            foreach (DataRow drMediaLibPerm in dsMediaLibPerm.Tables[0].Rows)
                            {
                                mediaLibPermissions += drMediaLibPerm["PermissionID"] + ";";
                            }
                            mediaLibPermissions = mediaLibPermissions.TrimEnd(';');
                        }

                        // Delete old permissions only for Create file and See library content permissions
                        MediaLibraryRolePermissionInfoProvider.DeleteAllRoles("LibraryID = " + mi.LibraryID + " AND PermissionID IN (" + mediaLibPermissions.Replace(";", ", ") + ")");

                        // Set media library permissions
                        MediaLibraryRolePermissionInfoProvider.SetPermissions(mi.LibraryID, roleIDs, mediaLibPermissions);

                        // Log staging task
                        SynchronizationHelper.LogObjectChange(mi, TaskTypeEnum.UpdateObject);
                    }
                }
            }
        }
    }


    /// <summary>
    /// Generate control output.
    /// </summary>
    public void GenerateContent()
    {
        string templatePath = DepartmentTemplatePath;
        if (String.IsNullOrEmpty(templatePath))
        {
            ltlContent.Text = ResHelper.GetString("departmentsectionsmanager.notemplate");
            ltlContent.Visible = true;
        }
        else
        {
            if (!DataHelper.DataSourceIsEmpty(TemplateDocuments))
            {
                int counter = 0;
                Table tb = new Table();

                string value = ValidationHelper.GetString(mSelectedValue, "");
                string docAliases = ";" + value.ToLower() + ";";

                TreeNode editedNode = Form.EditedObject as TreeNode;
                int targetClassId = ValidationHelper.GetInteger(editedNode.GetValue("NodeClassID"), 0);

                TableRow tr = new TableRow();
                foreach (DataRow drDoc in TemplateDocuments.Tables[0].Rows)
                {
                    // For each section td element is generated 
                    TableCell td = new TableCell();
                    CheckBox cbCell = new CheckBox();
                    cbCell.ID = "chckSection" + counter;
                    cbCell.Text = drDoc["NodeAlias"].ToString();
                    cbCell.Attributes.Add("Value", drDoc["NodeAlias"].ToString());

                    // Check if child allowed
                    int sourceClassId = ValidationHelper.GetInteger(drDoc["NodeClassID"], 0);
                    if (!DataClassInfoProvider.IsChildClassAllowed(targetClassId, sourceClassId))
                    {
                        cbCell.Enabled = false;
                        cbCell.ToolTip = ResHelper.GetString("departmentsectionsmanager.notallowedchild");
                    }

                    // Disable default hidden documents
                    if (HIDDEN_DOCUMENT_ALIAS.Contains(";" + drDoc["NodeAlias"].ToString().ToLower() + ";"))
                    {
                        cbCell.Checked = cbCell.Enabled;
                        cbCell.Enabled = false;
                    }


                    // Check for selected value
                    string docAlias = ValidationHelper.GetString(drDoc["NodeAlias"], "");

                    if (docAliases.Contains(";" + docAlias.ToLower() + ";") || ((mSelectedValue == null) && (Form.Mode == FormModeEnum.Insert)))
                    {
                        if (cbCell.Enabled)
                        {
                            cbCell.Checked = true;
                        }
                    }

                    // If editing existing document register alert script
                    if ((Form.Mode != FormModeEnum.Insert) && cbCell.Checked)
                    {
                        cbCell.Attributes.Add("OnClick", "if(!this.checked){alert(" + ScriptHelper.GetString(ResHelper.GetString("departmentsectionsmanagerformcontrol.removesectionwarning")) + ");}");
                    }

                    // Add reference to checkbox arraylist
                    CheckboxReferences.Add(cbCell);
                    counter++;

                    td.Controls.Add(cbCell);
                    tr.Cells.Add(td);

                    // If necessary create new row
                    if ((counter % ITEMS_PER_ROW) == 0)
                    {
                        tr.CssClass = "Row";
                        tb.Rows.Add(tr);
                        tr = new TableRow();
                    }
                }
                // If last row contains data add it
                if (counter > 0)
                {
                    tb.Rows.Add(tr);
                }
                pnlContent.Controls.Add(tb);
            }
            else
            {
                ltlContent.Text = ResHelper.GetString("departmentsectionsmanager.notemplate");
                ltlContent.Visible = true;
            }
        }
    }

    #endregion


    #region "Additional department acctions"

    /// <summary>
    /// Copy documents section.
    /// </summary>
    /// <param name="source">Source document</param>
    /// <param name="targetParentId">ID of target parent document</param>
    /// <param name="tree">Tree provider</param>
    private void CopyDocumentSection(TreeNode source, int targetParentId, TreeProvider tree)
    {
        DocumentHelper.CopyDocument(source, targetParentId, true, tree);
    }


    /// <summary>
    /// Delete documents section.
    /// </summary>
    /// <param name="node">Node to be deleted</param>
    /// <param name="tree">Tree provider</param>
    private void DeleteDocumentSection(TreeNode node, TreeProvider tree)
    {
        // Delete all document cultures
        DocumentHelper.DeleteDocument(node, tree, true, false, false);
    }


    /// <summary>
    /// Creates department forum group.
    /// </summary>
    /// <param name="departmentNode">Department node</param>
    private void CreateDepartmentForumGroup(TreeNode departmentNode)
    {
        // Set general values
        string departmentName = departmentNode.DocumentName;
        string suffix = "";

        #region "Create forum group"

        // Get forum group code name
        string groupCodeName = "Department_" + departmentNode.NodeGUID;

        // Check if forum group with given name already exists
        if (ForumGroupInfoProvider.GetForumGroupInfo(groupCodeName, CMSContext.CurrentSiteID) != null)
        {
            return;
        }

        // Create base URL for forums
        string baseUrl = CMSContext.GetUrl(departmentNode.NodeAliasPath + "/" + FORUM_DOCUMENT_ALIAS);

        ForumGroupInfo forumGroupObj = new ForumGroupInfo();
        forumGroupObj.GroupDescription = "Forum group for " + departmentName + " department.";
        suffix = " forum group";
        forumGroupObj.GroupDisplayName = TextHelper.LimitLength(departmentName, 200 - suffix.Length, "") + suffix;
        forumGroupObj.GroupName = groupCodeName;
        forumGroupObj.GroupOrder = 0;
        forumGroupObj.GroupEnableQuote = true;
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

        ForumGroupInfoProvider.SetForumGroupInfo(forumGroupObj);

        #endregion

        #region "Create forum"

        string codeName = "Default_department_" + departmentNode.NodeGUID;

        // Check if forum with given name already exists
        if (ForumInfoProvider.GetForumInfo(codeName, CMSContext.CurrentSite.SiteID) != null)
        {
            return;
        }

        ForumInfo forumObj = new ForumInfo();
        forumObj.ForumSiteID = CMSContext.CurrentSiteID;
        forumObj.ForumIsLocked = false;
        forumObj.ForumOpen = true;
        forumObj.ForumDisplayEmails = false;
        forumObj.ForumDescription = "Forum for " + departmentName + " department.";
        forumObj.ForumRequireEmail = false;
        suffix = " forum";
        forumObj.ForumDisplayName = TextHelper.LimitLength(departmentName, 200 - suffix.Length, "") + suffix;
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
        forumObj.AllowAccess = SecurityAccessEnum.AuthorizedRoles;
        forumObj.AllowAttachFiles = SecurityAccessEnum.AuthorizedRoles;
        forumObj.AllowMarkAsAnswer = SecurityAccessEnum.AuthorizedRoles;
        forumObj.AllowPost = SecurityAccessEnum.AuthorizedRoles;
        forumObj.AllowReply = SecurityAccessEnum.AuthorizedRoles;
        forumObj.AllowSubscribe = SecurityAccessEnum.AuthorizedRoles;

        if (ForumInfoProvider.LicenseVersionCheck(URLHelper.GetCurrentDomain(), FeatureEnum.Forums, VersionActionEnum.Insert))
        {
            ForumInfoProvider.SetForumInfo(forumObj);
        }

        #endregion
    }


    /// <summary>
    /// Creates department media library.
    /// </summary>
    /// <param name="departmentNode">Department node</param>
    private void CreateDepartmentMediaLibrary(TreeNode departmentNode)
    {
        // Set general values
        string departmentName = departmentNode.DocumentName;
        string codeName = "Department_" + departmentNode.NodeGUID;
        string suffix = "";

        // Check if library with same name already exists
        MediaLibraryInfo mlInfo = MediaLibraryInfoProvider.GetMediaLibraryInfo(codeName, CMSContext.CurrentSiteName);
        if (mlInfo != null)
        {
            return;
        }

        // Create new object (record) if needed
        mlInfo = new MediaLibraryInfo();

        suffix = " media library";
        mlInfo.LibraryDisplayName = TextHelper.LimitLength(departmentName, 200 - suffix.Length, "") + suffix;
        mlInfo.LibraryFolder = departmentNode.NodeAlias;
        mlInfo.LibraryDescription = "Media library for " + departmentName + " department.";
        mlInfo.LibraryName = codeName;
        mlInfo.LibrarySiteID = CMSContext.CurrentSiteID;

        // Set security
        mlInfo.FileCreate = SecurityAccessEnum.AuthorizedRoles;
        mlInfo.FileDelete = SecurityAccessEnum.AuthorizedRoles;
        mlInfo.FileModify = SecurityAccessEnum.AuthorizedRoles;
        mlInfo.FolderCreate = SecurityAccessEnum.AuthorizedRoles;
        mlInfo.FolderDelete = SecurityAccessEnum.AuthorizedRoles;
        mlInfo.FolderModify = SecurityAccessEnum.AuthorizedRoles;
        mlInfo.Access = SecurityAccessEnum.AuthorizedRoles;

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


    /// <summary>
    /// Creates or rebuild department content search index.
    /// </summary>
    /// <param name="departmentNode">Department node</param>
    private void CreateDepartmentContentSearchIndex(TreeNode departmentNode)
    {
        string codeName = "default_department_" + departmentNode.NodeGUID;
        string departmentName = departmentNode.DocumentName;

        SearchIndexInfo sii = SearchIndexInfoProvider.GetSearchIndexInfo(codeName);
        if (sii == null)
        {
            // Create search index info
            sii = new SearchIndexInfo();
            sii.IndexName = codeName;
            string suffix = " - Default";
            sii.IndexDisplayName = TextHelper.LimitLength(departmentName, 200 - suffix.Length, "") + suffix;
            sii.IndexAnalyzerType = AnalyzerTypeEnum.StandardAnalyzer;
            sii.IndexType = PredefinedObjectType.DOCUMENT;
            sii.IndexIsCommunityGroup = false;

            // Create search index settings info
            SearchIndexSettingsInfo sisi = new SearchIndexSettingsInfo();
            sisi.ID = Guid.NewGuid();
            sisi.Path = departmentNode.NodeAliasPath + "/%";
            sisi.Type = SearchIndexSettingsInfo.TYPE_ALLOWED;
            sisi.ClassNames = "";

            // Create settings item
            SearchIndexSettings sis = new SearchIndexSettings();

            // Update settings item
            sis.SetSearchIndexSettingsInfo(sisi);

            // Update xml value
            sii.IndexSettings = sis;
            SearchIndexInfoProvider.SetSearchIndexInfo(sii);

            // Assing to current website
            SearchIndexSiteInfoProvider.AddSearchIndexToSite(sii.IndexID, CMSContext.CurrentSiteID);

        }

        // Add curent culture to search index
        CultureInfo ci = CultureInfoProvider.GetCultureInfo(departmentNode.DocumentCulture);
        SearchIndexCultureInfoProvider.AddSearchIndexCulture(sii.IndexID, ci.CultureID);

        // Rebuild search index
        SearchTaskInfoProvider.CreateTask(SearchTaskTypeEnum.Rebuild, sii.IndexType, null, sii.IndexName);
    }


    /// <summary>
    /// Creates forum search index.
    /// </summary>
    /// <param name="departmentNode">Department node</param>
    private void CreateDepartmentForumSearchIndex(TreeNode departmentNode)
    {
        string codeName = "forums_department_" + departmentNode.NodeGUID;
        string departmentName = departmentNode.DocumentName;

        SearchIndexInfo sii = SearchIndexInfoProvider.GetSearchIndexInfo(codeName);
        if (sii == null)
        {
            // Create search index info
            sii = new SearchIndexInfo();
            sii.IndexName = codeName;
            string suffix = " - Forums";
            sii.IndexDisplayName = TextHelper.LimitLength(departmentName, 200 - suffix.Length, "") + suffix;
            sii.IndexAnalyzerType = AnalyzerTypeEnum.StandardAnalyzer;
            sii.IndexType = PredefinedObjectType.FORUM;
            sii.IndexIsCommunityGroup = false;

            // Create search index settings info
            SearchIndexSettingsInfo sisi = new SearchIndexSettingsInfo();
            sisi.ID = Guid.NewGuid();
            sisi.Type = SearchIndexSettingsInfo.TYPE_ALLOWED;
            sisi.SiteName = CMSContext.CurrentSiteName;
            sisi.ForumNames = "*_department_" + departmentNode.NodeGUID;

            // Create settings item
            SearchIndexSettings sis = new SearchIndexSettings();

            // Update settings item
            sis.SetSearchIndexSettingsInfo(sisi);

            // Update xml value
            sii.IndexSettings = sis;
            SearchIndexInfoProvider.SetSearchIndexInfo(sii);

            // Assing to current website and current culture
            SearchIndexSiteInfoProvider.AddSearchIndexToSite(sii.IndexID, CMSContext.CurrentSiteID);
            CultureInfo ci = CultureInfoProvider.GetCultureInfo(departmentNode.DocumentCulture);
            SearchIndexCultureInfoProvider.AddSearchIndexCulture(sii.IndexID, ci.CultureID);
        }
    }


    /// <summary>
    /// Process additional department tasks.
    /// </summary>
    public void ProcessDepartment(object sender, EventArgs e)
    {
        TreeNode editedNode = Form.EditedObject as TreeNode;

        // Get department template source document
        TreeNode sourceNode = DocumentHelper.GetDocument(CMSContext.CurrentSiteName, DepartmentTemplatePath, null, true, null, null, null, 0, false, null, TreeProvider);

        // Copy relevant template data to department document
        if (sourceNode != null)
        {
            DocumentHelper.CopyNodeData(sourceNode, editedNode, true, true, false, true, true, false, false, "DocumentName;NodeAlias;DocumentTagGroupID;DocumentStylesheetID;DocumentPublishFrom;DocumentPublishTo");
            DocumentHelper.UpdateDocument(editedNode, TreeProvider);
        }

        #region "Create department tag group"

        // Get tag group info
        TagGroupInfo tgi = TagGroupInfoProvider.GetTagGroupInfo(editedNode.DocumentTagGroupID);

        // If not exist, create new tag group and set it to document
        if (tgi == null)
        {
            // Populate tag group info fields 
            tgi = new TagGroupInfo();
            tgi.TagGroupDisplayName = editedNode.DocumentName;
            tgi.TagGroupName = editedNode.NodeGUID.ToString();
            tgi.TagGroupDescription = "";
            tgi.TagGroupSiteID = CMSContext.CurrentSiteID;
            tgi.TagGroupIsAdHoc = false;

            // Store tag group info to DB
            TagGroupInfoProvider.SetTagGroupInfo(tgi);

            // Update document Tag group ID
            editedNode.DocumentTagGroupID = tgi.TagGroupID;
            DocumentHelper.UpdateDocument(editedNode, TreeProvider);
        }

        #endregion

        if (!DataHelper.DataSourceIsEmpty(TemplateDocuments))
        {
            // List of selected documents
            string selectedDocs = ";" + Value + ";";

            // Get already created documents under edited document
            DataSet dsExistingDocs = DocumentHelper.GetDocuments(CMSContext.CurrentSiteName, editedNode.NodeAliasPath + "/%", editedNode.DocumentCulture, true, null, null, null, 1, false, 0, "NodeAlias, " + TreeProvider.SELECTNODES_REQUIRED_COLUMNS, null);
            StringBuilder sbExistDocs = new StringBuilder();

            // Process existing documents to obtain list of aliases
            foreach (DataRow drExistDoc in dsExistingDocs.Tables[0].Rows)
            {
                sbExistDocs.Append(";");
                sbExistDocs.Append(drExistDoc["NodeAlias"].ToString().ToLower());
            }
            sbExistDocs.Append(";");
            string existingDocs = sbExistDocs.ToString();

            // Set same ordering as for original template documents
            bool orgUseAutomaticOrdering = TreeProvider.UseAutomaticOrdering;
            TreeProvider.UseAutomaticOrdering = false;

            // Process template documents
            foreach (DataRow drDoc in TemplateDocuments.Tables[0].Rows)
            {
                string nodeAlias = drDoc["NodeAlias"].ToString().ToLower();
                string contNodeAlias = ";" + nodeAlias + ";";

                // Set marks
                bool existing = existingDocs.Contains(contNodeAlias);
                bool selected = selectedDocs.Contains(contNodeAlias);

                int nodeId = ValidationHelper.GetInteger(drDoc["NodeID"], 0);
                string docCulture = ValidationHelper.GetString(drDoc["DocumentCulture"], "");
                TreeNode srcNode = DocumentHelper.GetDocument(nodeId, docCulture, editedNode.TreeProvider);

                // Check if section exists
                if (srcNode != null)
                {
                    // Copy or remove marked document sections
                    if (selected)
                    {
                        if (!existing)
                        {
                            CopyDocumentSection(srcNode, editedNode.NodeID, TreeProvider);
                        }
                    }
                    else
                    {
                        if (existing)
                        {
                            // Select node to delete
                            TreeNode delNode = TreeHelper.SelectSingleNode(editedNode.NodeAliasPath + "/" + nodeAlias);
                            if (delNode != null)
                            {
                                DeleteDocumentSection(delNode, TreeProvider);
                            }
                        }
                    }

                    // Process additional operations
                    if (selected && !existing)
                    {
                        switch (nodeAlias)
                        {
                            // Create department forum
                            case FORUM_DOCUMENT_ALIAS:
                                CreateDepartmentForumGroup(editedNode);
                                CreateDepartmentForumSearchIndex(editedNode);
                                break;

                            // Create media library
                            case MEDIA_DOCUMENT_ALIAS:
                                CreateDepartmentMediaLibrary(editedNode);
                                break;
                        }
                    }
                }
            }

            // Set previous ordering
            TreeProvider.UseAutomaticOrdering = orgUseAutomaticOrdering;
        }
        mDocumentSaved = true;
    }

    #endregion
}
