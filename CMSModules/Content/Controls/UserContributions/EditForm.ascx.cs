using System;
using System.Data;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

using CMS.FormEngine;
using CMS.FormControls;
using CMS.WorkflowEngine;
using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.TreeEngine;
using CMS.SettingsProvider;
using CMS.SiteProvider;
using CMS.ExtendedControls;
using CMS.PortalEngine;
using CMS.EventLog;
using CMS.URLRewritingEngine;
using CMS.Controls;
using CMS.UIControls;
using CMS.LicenseProvider;
using CMS.WebAnalytics;

using TreeNode = CMS.TreeEngine.TreeNode;


public partial class CMSModules_Content_Controls_UserContributions_EditForm : CMSUserControl
{
    #region "Variables"

    /// <summary>
    /// On after aprove event.
    /// </summary>
    public event EventHandler OnAfterApprove = null;

    /// <summary>
    /// On after reject event.
    /// </summary>
    public event EventHandler OnAfterReject = null;


    /// <summary>
    /// On after delete event.
    /// </summary>
    public event EventHandler OnAfterDelete = null;

    /// <summary>
    /// Data properties variable.
    /// </summary>
    private readonly CMSDataProperties mDataProperties = new CMSDataProperties();

    /// <summary>
    /// Indicates if the form has been loaded.
    /// </summary>
    private bool mFormLoaded = false;

    private bool confirmChanges = false;
    protected TreeNode mNode = null;
    protected TreeProvider mTreeProvider = null;
    private WorkflowManager mWorkflowManager = null;
    private WorkflowInfo wi = null;
    private VersionManager mVersionManager = null;
    private DataClassInfo ci = null;

    #endregion


    #region "Document properties"

    /// <summary>
    /// Culture code.
    /// </summary>
    public string CultureCode
    {
        get
        {
            return ValidationHelper.GetString(ViewState["CultureCode"], mDataProperties.CultureCode);
        }
        set
        {
            ViewState["CultureCode"] = value;
        }
    }


    /// <summary>
    /// Site name.
    /// </summary>
    public string SiteName
    {
        get
        {
            return ValidationHelper.GetString(ViewState["SiteName"], mDataProperties.SiteName);
        }
        set
        {
            ViewState["SiteName"] = value;
        }
    }


    /// <summary>
    /// Indicates if check-in/check-out functionality is automatic
    /// </summary>
    protected bool AutoCheck
    {
        get
        {
            if (Node != null)
            {
                // Get workflow info
                wi = WorkflowManager.GetNodeWorkflow(Node);

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


    #region "Public properties"

    /// <summary>
    /// Returns true if new document mode.
    /// </summary>
    public bool NewDocument
    {
        get
        {
            return (Action.ToLower() == "new");
        }
    }


    /// <summary>
    /// Returns true if new culture mode.
    /// </summary>
    public bool NewCulture
    {
        get
        {
            return (Action.ToLower() == "newculture");
        }
    }


    /// <summary>
    /// Returns true in delete mode.
    /// </summary>
    public bool Delete
    {
        get
        {
            return (Action.ToLower() == "delete");
        }
    }


    /// <summary>
    /// Returns true in edit mode.
    /// </summary>
    public bool Edit
    {
        get
        {
            return (Action.ToLower() == "edit");
        }
    }


    /// <summary>
    /// Node ID.
    /// </summary>
    public int NodeID
    {
        get
        {
            return ValidationHelper.GetInteger(ViewState["NodeID"], 0);
        }
        set
        {
            ViewState["NodeID"] = value;
            menuElem.NodeID = value;
        }
    }


    /// <summary>
    /// Document node.
    /// </summary>
    public TreeNode Node
    {
        get
        {
            return mNode ?? (mNode = DocumentHelper.GetDocument(NodeID, CultureCode, TreeProvider));
        }
        set
        {
            mNode = value;
            NodeID = (mNode == null) ? NodeID : mNode.NodeID;
        }
    }


    /// <summary>
    /// Form Action (mode).
    /// </summary>
    public string Action
    {
        get
        {
            return ValidationHelper.GetString(ViewState["Action"], "");
        }
        set
        {
            ViewState["Action"] = value;
        }
    }


    /// <summary>
    /// Class ID.
    /// </summary>
    public int ClassID
    {
        get
        {
            return ValidationHelper.GetInteger(ViewState["ClassID"], 0);
        }
        set
        {
            ViewState["ClassID"] = value;
        }
    }


    /// <summary>
    /// Alternative form name.
    /// </summary>
    public string AlternativeFormName
    {
        get
        {
            return ValidationHelper.GetString(ViewState["AlternativeFormName"], null);
        }
        set
        {
            ViewState["AlternativeFormName"] = value;
        }
    }


    /// <summary>
    /// Form validation error message.
    /// </summary>
    public string ValidationErrorMessage
    {
        get
        {
            return ValidationHelper.GetString(ViewState["ValidationErrorMessage"], null);
        }
        set
        {
            ViewState["ValidationErrorMessage"] = value;
        }
    }


    /// <summary>
    /// Template ID.
    /// </summary>
    public int TemplateID
    {
        get
        {
            return ValidationHelper.GetInteger(ViewState["TemplateID"], 0);
        }
        set
        {
            ViewState["TemplateID"] = value;
        }
    }


    /// <summary>
    /// Owner ID.
    /// </summary>
    public int OwnerID
    {
        get
        {
            return ValidationHelper.GetInteger(ViewState["OwnerID"], 0);
        }
        set
        {
            ViewState["OwnerID"] = value;
        }
    }


    /// <summary>
    /// New item page template code name.
    /// </summary>
    public string NewItemPageTemplate
    {
        get
        {
            return ValidationHelper.GetString(ViewState["NewItemPageTemplate"], "");
        }
        set
        {
            ViewState["NewItemPageTemplate"] = value;

            // Get template and set template ID
            PageTemplateInfo pti = PageTemplateInfoProvider.GetPageTemplateInfo(value);
            if (pti != null)
            {
                TemplateID = pti.PageTemplateId;
            }
        }
    }


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


    /// <summary>
    /// List of allowed child classes separated by semicolon.
    /// </summary>
    public string AllowedChildClasses
    {
        get
        {
            return ValidationHelper.GetString(ViewState["AllowedChildClasses"], "");
        }
        set
        {
            ViewState["AllowedChildClasses"] = value;
        }
    }


    /// <summary>
    /// Document ID to use for default data of new culture version.
    /// </summary>
    public int CopyDefaultDataFromDocumentID
    {
        get
        {
            return ValidationHelper.GetInteger(ViewState["CopyDefaultDataFromDocumentID"], 0);
        }
        set
        {
            ViewState["CopyDefaultDataFromDocumentID"] = value;
        }
    }


    /// <summary>
    /// If true, form allows deleting the document.
    /// </summary>
    public bool AllowDelete
    {
        get
        {
            return ValidationHelper.GetBoolean(ViewState["AllowDelete"], true);
        }
        set
        {
            ViewState["AllowDelete"] = value;
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether document permissions are checked.
    /// </summary>
    public bool CheckPermissions
    {
        get
        {
            return ValidationHelper.GetBoolean(ViewState["CheckPermissions"], false);
        }
        set
        {
            ViewState["CheckPermissions"] = value;
            menuElem.CheckPermissions = value;
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether document type permissions are required to create new document.
    /// </summary>
    public bool CheckDocPermissionsForInsert
    {
        get
        {
            return ValidationHelper.GetBoolean(ViewState["CheckDocPermissionsForInsert"], true);
        }
        set
        {
            ViewState["CheckDocPermissionsForInsert"] = value;
        }
    }


    /// <summary>
    /// Prefix of the action functions.
    /// </summary>
    public string FunctionsPrefix
    {
        get
        {
            return ValidationHelper.GetString(ViewState["FunctionsPrefix"], "");
        }
        set
        {
            ViewState["FunctionsPrefix"] = value;
            menuElem.FunctionsPrefix = value;
        }
    }


    /// <summary>
    /// Determines whether to use progress script.
    /// </summary>
    public bool UseProgressScript
    {
        get
        {
            return ValidationHelper.GetBoolean(ViewState["UseProgressScript"], true);
        }
        set
        {
            ViewState["UseProgressScript"] = value;
        }
    }


    /// <summary>
    /// Editing form.
    /// </summary>
    public CMSForm CMSForm
    {
        get
        {
            return formElem;
        }
    }


    /// <summary>
    /// Determines whether the save is allowed (form have to be loaded first).
    /// </summary>
    public bool AllowSave
    {
        get
        {
            return menuElem.AllowSave;
        }
    }


    /// <summary>
    /// Indicates whether activity logging is enabled.
    /// </summary>
    public bool LogActivity
    {
        get;
        set;
    }

    #endregion


    #region "Methods"

    protected override void CreateChildControls()
    {
        // Reload data
        ReloadData(false);
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        // Register external data bound event handler for UniGrid
        gridClass.OnExternalDataBound += gridClass_OnExternalDataBound;
    }


    /// <summary>
    /// Reloads control.
    /// </summary>
    /// <param name="forceReload">Forces nested CMSForm to reload if true</param>
    public void ReloadData(bool forceReload)
    {
        if (!mFormLoaded || forceReload)
        {
            // Check License
            LicenseHelper.CheckFeatureAndRedirect(URLHelper.GetCurrentDomain(), FeatureEnum.UserContributions);

            if (StopProcessing)
            {
                formElem.StopProcessing = true;
            }
            else
            {
                ScriptHelper.RegisterDialogScript(this.Page);
                confirmChanges = SettingsKeyProvider.GetBoolValue(SiteName + ".CMSConfirmChanges");

                formElem.TreeProvider = TreeProvider;
                formElem.StopProcessing = false;

                titleElem.TitleImage = String.Empty;
                titleElem.TitleText = String.Empty;

                if (!confirmChanges)
                {
                    AddScript("confirmChanges = false;");
                }

                // Get the node
                if (NewCulture || NewDocument)
                {
                    Node = DocumentHelper.GetDocument(NodeID, null, TreeProvider);
                    if (NewCulture && (Node != null))
                    {
                        DocumentHelper.ClearWorkflowInformation(Node);
                    }
                }
                else
                {
                    Node = DocumentHelper.GetDocument(NodeID, CultureCode, TreeProvider);
                }

                pnlSelectClass.Visible = false;
                pnlEdit.Visible = false;
                pnlInfo.Visible = false;
                pnlNewCulture.Visible = false;
                pnlDelete.Visible = false;

                // If node found, init the form
                if (Node != null)
                {
                    if (Delete)
                    {
                        // Delete document
                        pnlDelete.Visible = true;

                        titleElem.TitleText = GetString("Content.DeleteTitle");
                        titleElem.TitleImage = GetImageUrl("CMSModules/CMS_Content/Menu/delete.png");

                        chkAllCultures.Text = GetString("ContentDelete.AllCultures");
                        chkDestroy.Text = GetString("ContentDelete.Destroy");

                        lblQuestion.Text = GetString("ContentDelete.Question");
                        btnYes.Text = GetString("general.yes");
                        btnNo.Text = GetString("general.no");

                        DataSet culturesDS = CultureInfoProvider.GetSiteCultures(SiteName);
                        if ((DataHelper.DataSourceIsEmpty(culturesDS)) || (culturesDS.Tables[0].Rows.Count <= 1))
                        {
                            chkAllCultures.Visible = false;
                            chkAllCultures.Checked = true;
                        }

                        if (Node.IsLink)
                        {
                            titleElem.TitleText = GetString("Content.DeleteTitleLink") + " \"" + HTMLHelper.HTMLEncode(Node.NodeName) + "\"";
                            lblQuestion.Text = GetString("ContentDelete.QuestionLink");
                            chkAllCultures.Checked = true;
                            plcCheck.Visible = false;
                        }
                        else
                        {
                            titleElem.TitleText = GetString("Content.DeleteTitle") + " \"" + HTMLHelper.HTMLEncode(Node.NodeName) + "\"";
                        }
                    }
                    else
                    {
                        if (NewDocument)
                        {
                            titleElem.TitleImage = GetImageUrl("CMSModules/CMS_Content/Menu/new.png");
                            titleElem.TitleText = GetString("Content.NewTitle");
                        }

                        if (NewDocument && (ClassID <= 0))
                        {
                            // Select document type
                            pnlSelectClass.Visible = true;

                            // Get the allowed child classes
                            DataSet ds = DataClassInfoProvider.GetAllowedChildClasses(ValidationHelper.GetInteger(Node.GetValue("NodeClassID"), 0), ValidationHelper.GetInteger(SiteInfoProvider.GetSiteInfo(SiteName).SiteID, 0), "ClassName, ClassDisplayName, ClassID", -1);

                            ArrayList deleteRows = new ArrayList();

                            if (!DataHelper.DataSourceIsEmpty(ds))
                            {
                                // Get the unwanted classes
                                string allowed = AllowedChildClasses.Trim().ToLower();
                                if (!string.IsNullOrEmpty(allowed))
                                {
                                    allowed = String.Format(";{0};", allowed);
                                }

                                CurrentUserInfo userInfo = CMSContext.CurrentUser;
                                string className = null;
                                // Check if the user has 'Create' permission per Content
                                bool isAuthorizedToCreateInContent = userInfo.IsAuthorizedPerResource("CMS.Content", "Create");
                                bool hasNodeAllowCreate = (userInfo.IsAuthorizedPerTreeNode(Node, NodePermissionsEnum.Create) != AuthorizationResultEnum.Allowed);
                                foreach (DataRow dr in ds.Tables[0].Rows)
                                {
                                    className = ValidationHelper.GetString(DataHelper.GetDataRowValue(dr, "ClassName"), String.Empty).ToLower();
                                    // Document type is not allowed or user hasn't got permission, remove it from the data set
                                    if ((!string.IsNullOrEmpty(allowed) && (!allowed.Contains(";" + className + ";"))) ||
                                        (CheckPermissions && CheckDocPermissionsForInsert && !isAuthorizedToCreateInContent && !userInfo.IsAuthorizedPerClassName(className, "Create") && (!userInfo.IsAuthorizedPerClassName(className, "CreateSpecific") || !hasNodeAllowCreate)))
                                    {
                                        deleteRows.Add(dr);
                                    }
                                }

                                // Remove the rows
                                foreach (DataRow dr in deleteRows)
                                {
                                    ds.Tables[0].Rows.Remove(dr);
                                }
                            }

                            // Check if some classes are available
                            if (!DataHelper.DataSourceIsEmpty(ds))
                            {
                                // If number of classes is more than 1 display them in grid
                                if (ds.Tables[0].Rows.Count > 1)
                                {
                                    ds.Tables[0].DefaultView.Sort = "ClassDisplayName";
                                    lblError.Visible = false;
                                    lblInfo.Visible = true;
                                    lblInfo.Text = GetString("Content.NewInfo");

                                    DataSet sortedResult = new DataSet();
                                    sortedResult.Tables.Add(ds.Tables[0].DefaultView.ToTable());
                                    gridClass.DataSource = sortedResult;
                                    gridClass.ReloadData();
                                }
                                // else show form of the only class
                                else
                                {
                                    ClassID = ValidationHelper.GetInteger(DataHelper.GetDataRowValue(ds.Tables[0].Rows[0], "ClassID"), 0);
                                    ReloadData(true);
                                    return;
                                }
                            }
                            else
                            {
                                // Display error message
                                lblError.Visible = true;
                                lblError.Text = GetString("Content.NoAllowedChildDocuments");
                                lblInfo.Visible = false;
                                gridClass.Visible = false;
                            }
                        }
                        else
                        {
                            // Display the form
                            pnlEdit.Visible = true;

                            // Try to get GroupID if group context exists
                            int currentGroupId = ModuleCommands.CommunityGetCurrentGroupID();

                            btnApprove.Attributes.Add("style", "display: none;");
                            btnCheckIn.Attributes.Add("style", "display: none;");
                            btnCheckOut.Attributes.Add("style", "display: none;");
                            btnReject.Attributes.Add("style", "display: none;");
                            btnSave.Attributes.Add("style", "display: none;");
                            btnUndoCheckOut.Attributes.Add("style", "display: none;");
                            btnDelete.Attributes.Add("style", "display: none;");
                            btnRefresh.Attributes.Add("style", "display: none;");

                            // CMSForm initialization
                            formElem.NodeId = Node.NodeID;
                            formElem.SiteName = SiteName;
                            formElem.CultureCode = CultureCode;
                            formElem.ValidationErrorMessage = ValidationErrorMessage;
                            formElem.IsLiveSite = IsLiveSite;
                            // Set group ID if group context exists
                            formElem.GroupID = currentGroupId;
                            // WebDAV is allowed for live site only if the permissions are checked or user is global administrator or for group context - user is group administrator
                            formElem.AllowWebDAV = !IsLiveSite || CheckPermissions || CMSContext.CurrentUser.IsGlobalAdministrator || CMSContext.CurrentUser.IsGroupAdministrator(currentGroupId);

                            // Set the form mode
                            if (NewDocument)
                            {
                                ci = DataClassInfoProvider.GetDataClass(ClassID);
                                if (ci == null)
                                {
                                    throw new Exception(String.Format("[CMSAdminControls/EditForm.aspx]: Class ID '{0}' not found.", ClassID));
                                }

                                string classDisplayName = HTMLHelper.HTMLEncode(ResHelper.LocalizeString(ci.ClassDisplayName));
                                titleElem.TitleText = GetString("Content.NewTitle") + ": " + classDisplayName;

                                // Set default template ID
                                formElem.DefaultPageTemplateID = TemplateID > 0 ? TemplateID : ci.ClassDefaultPageTemplateID;

                                // Set document owner
                                formElem.OwnerID = OwnerID;
                                formElem.FormMode = FormModeEnum.Insert;
                                string newClassName = ci.ClassName;
                                string newFormName = newClassName + ".default";
                                if (!String.IsNullOrEmpty(AlternativeFormName))
                                {
                                    // Set the alternative form full name
                                    formElem.AlternativeFormFullName = GetAltFormFullName(ci.ClassName);
                                }
                                if (newFormName.ToLower() != formElem.FormName.ToLower())
                                {
                                    formElem.FormName = newFormName;
                                }
                            }
                            else if (NewCulture)
                            {
                                formElem.FormMode = FormModeEnum.InsertNewCultureVersion;
                                // Default data document ID
                                formElem.CopyDefaultDataFromDocumentId = CopyDefaultDataFromDocumentID;

                                ci = DataClassInfoProvider.GetDataClass(Node.NodeClassName);
                                formElem.FormName = Node.NodeClassName + ".default";
                                if (!String.IsNullOrEmpty(AlternativeFormName))
                                {
                                    // Set the alternative form full name
                                    formElem.AlternativeFormFullName = GetAltFormFullName(ci.ClassName);
                                }
                            }
                            else
                            {
                                formElem.FormMode = FormModeEnum.Update;
                                ci = DataClassInfoProvider.GetDataClass(Node.NodeClassName);
                                formElem.FormName = String.Empty;
                                if (!String.IsNullOrEmpty(AlternativeFormName))
                                {
                                    // Set the alternative form full name
                                    formElem.AlternativeFormFullName = GetAltFormFullName(ci.ClassName);
                                }

                                // Initialize the CMSForm
                                formElem.LoadForm(forceReload);
                            }

                            // Display the CMSForm
                            formElem.Visible = true;

                            ReloadForm();
                        }
                    }
                }
                else
                {
                    // Try to get any culture of the document
                    Node = DocumentHelper.GetDocument(NodeID, TreeProvider.ALL_CULTURES, TreeProvider);
                    if (Node != null)
                    {
                        // Offer a new culture creation
                        pnlNewCulture.Visible = true;

                        titleElem.TitleText = GetString("Content.NewCultureVersionTitle") + " (" + HTMLHelper.HTMLEncode(CMSContext.CurrentUser.PreferredCultureCode) + ")";
                        titleElem.TitleImage = GetImageUrl("CMSModules/CMS_Content/Menu/new.png");

                        lblNewCultureInfo.Text = GetString("ContentNewCultureVersion.Info");
                        radCopy.Text = GetString("ContentNewCultureVersion.Copy");
                        radEmpty.Text = GetString("ContentNewCultureVersion.Empty");

                        radCopy.Attributes.Add("onclick", "ShowSelection();");
                        radEmpty.Attributes.Add("onclick", "ShowSelection()");

                        AddScript(
                            "function ShowSelection() { \n" +
                            "   if (document.getElementById('" + radCopy.ClientID + "').checked) { document.getElementById('divCultures').style.display = 'block'; } \n" +
                            "   else { document.getElementById('divCultures').style.display = 'none'; } \n" +
                            "} \n"
                        );

                        btnOk.Text = GetString("ContentNewCultureVersion.Create");

                        // Load culture versions
                        SiteInfo si = SiteInfoProvider.GetSiteInfo(Node.NodeSiteID);
                        if (si != null)
                        {
                            lstCultures.Items.Clear();

                            DataSet nodes = TreeProvider.SelectNodes(si.SiteName, Node.NodeAliasPath, TreeProvider.ALL_CULTURES, false, null, null, null, 1, false);
                            foreach (DataRow nodeCulture in nodes.Tables[0].Rows)
                            {
                                ListItem li = new ListItem();
                                li.Text = CultureInfoProvider.GetCultureInfo(nodeCulture["DocumentCulture"].ToString()).CultureName;
                                li.Value = nodeCulture["DocumentID"].ToString();
                                lstCultures.Items.Add(li);
                            }
                            if (lstCultures.Items.Count > 0)
                            {
                                lstCultures.SelectedIndex = 0;
                            }
                        }
                    }
                    else
                    {
                        pnlInfo.Visible = true;
                        lblFormInfo.Text = GetString("EditForm.DocumentNotFound");
                        formElem.StopProcessing = true;
                    }
                }
            }
            // Set flag that the form is loaded
            mFormLoaded = true;
        }
    }


    /// <summary>
    /// Unigrid external databound.
    /// </summary>
    protected object gridClass_OnExternalDataBound(object sender, string sourceName, object parameter)
    {
        switch (sourceName.ToLower())
        {
            // Display link to class type
            case "classdisplayname":
                {
                    DataRowView row = (DataRowView)parameter;

                    LinkButton btn = new LinkButton();
                    btn.CssClass = "UserContributionNewClass";
                    btn.CommandArgument = ValidationHelper.GetString(row["ClassID"], "0");
                    btn.Command += btnClass_Command;

                    Image img = new Image();
                    img.ImageUrl = GetDocumentTypeIconUrl(Convert.ToString(row["ClassName"]));

                    Label lbl = new Label();
                    string classDisplayName = Convert.ToString(row["ClassDisplayName"]);
                    lbl.Text = HTMLHelper.HTMLEncode(ResHelper.LocalizeString(classDisplayName));

                    btn.Controls.Add(img);
                    btn.Controls.Add(lbl);
                    return btn;
                }
        }

        return null;
    }


    private void ReloadForm()
    {
        lblWorkflowInfo.Text = String.Empty;

        // Enable the CMSForm
        formElem.Enabled = true;

        if ((Node != null) && !NewDocument && !NewCulture)
        {
            bool authorized = true;

            // Check the permissions
            if (CheckPermissions)
            {
                // Check read permissions
                if (CMSContext.CurrentUser.IsAuthorizedPerDocument(Node, NodePermissionsEnum.Read) == AuthorizationResultEnum.Denied)
                {
                    RedirectToAccessDenied(String.Format(GetString("cmsdesk.notauthorizedtoreaddocument"), Node.NodeAliasPath));
                }
                // Check modify permissions
                else if (CMSContext.CurrentUser.IsAuthorizedPerDocument(Node, NodePermissionsEnum.Modify) == AuthorizationResultEnum.Denied)
                {
                    formElem.Enabled = false;
                    lblWorkflowInfo.Text = String.Format(GetString("cmsdesk.notauthorizedtoeditdocument"), Node.NodeAliasPath);
                    authorized = false;
                }
            }

            // If authorized
            if (authorized)
            {
                // Setup the workflow information
                wi = WorkflowManager.GetNodeWorkflow(Node);
                if ((wi != null) && (!NewCulture))
                {
                    // Get current step info, do not update document
                    WorkflowStepInfo si = null;
                    if (Node.IsPublished && (Node.DocumentCheckedOutVersionHistoryID <= 0))
                    {
                        si = WorkflowStepInfoProvider.GetWorkflowStepInfo("published", wi.WorkflowID);
                    }
                    else
                    {
                        si = WorkflowManager.GetStepInfo(Node, false) ??
                             WorkflowManager.GetFirstWorkflowStep(Node, wi);
                    }

                    bool allowApproval = true;
                    bool canApprove = WorkflowManager.CanUserApprove(Node, CMSContext.CurrentUser);
                    string stepName = si.StepName.ToLower();

                    if (!(canApprove || (stepName == "edit") || (stepName == "published") || (stepName == "archived")))
                    {
                        formElem.Enabled = false;
                    }

                    bool useCheckInCheckOut = wi.UseCheckInCheckOut(SiteName);

                    if (!Node.IsCheckedOut)
                    {
                        // Check-in, Check-out
                        if (useCheckInCheckOut)
                        {
                            // If not checked out, add the check-out information
                            if (canApprove || (stepName == "edit") || (stepName == "published") || (stepName == "archived"))
                            {
                                lblWorkflowInfo.Text = GetString("EditContent.DocumentCheckedIn");
                            }
                            formElem.Enabled = NewCulture;
                        }
                    }
                    else
                    {
                        // If checked out by current user, add the check-in button
                        int checkedOutBy = Node.DocumentCheckedOutByUserID;
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

                            formElem.Enabled = NewCulture;
                        }
                        allowApproval = false;
                    }

                    // Document approval
                    if (allowApproval)
                    {
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

        // Reload edit menu
        menuElem.ShowDelete = AllowDelete && Edit;
        menuElem.Node = Node;
        menuElem.Action = Action.ToLower();
        menuElem.CheckPermissions = CheckPermissions;
        menuElem.ReloadMenu();
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
    /// Adds the script to the output request window.
    /// </summary>
    /// <param name="script">Script to add</param>
    private void AddScript(string script)
    {
        ScriptHelper.RegisterStartupScript(this, typeof(string), script.GetHashCode().ToString(), ScriptHelper.GetScript(script));
    }


    /// <summary>
    /// Save button click event handler.
    /// </summary>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (SaveDocument())
        {
            ReloadForm();
        }
    }


    /// <summary>
    /// Save new or existing document.
    /// </summary>
    public bool SaveDocument()
    {
        if (IsBannedIP())
        {
            return false;
        }

        if (NodeID > 0)
        {
            // Validate the form first
            if (formElem.BasicForm.ValidateData())
            {
                if (!NewDocument && !NewCulture)
                {
                    // Check the permissions
                    if (CheckPermissions)
                    {
                        // Check modify permissions
                        if (CMSContext.CurrentUser.IsAuthorizedPerDocument(Node, NodePermissionsEnum.Modify) == AuthorizationResultEnum.Denied)
                        {
                            return false;
                        }
                    }
                }

                try
                {
                    // If not using check-in/check-out, check out automatically
                    if ((Node != null) && !NewDocument && !NewCulture && AutoCheck)
                    {
                        // Check out
                        VersionManager.CheckOut(Node, Node.IsPublished, true);
                    }

                    // Store old form mode (will be used to
                    FormModeEnum formModebeforeSave = formElem.FormMode;
                    // Tree node is returned from CMSForm.Save method
                    if ((Node != null) && !NewDocument && !NewCulture)
                    {
                        formElem.Save(Node);
                    }
                    else
                    {
                        Node = formElem.Save();

                        // Clear cache if current document is blogpost or blog
                        if (ci != null)
                        {
                            if ((ci.ClassName.ToLower() == "cms.blogpost") || (ci.ClassName.ToLower() == "cms.blog"))
                            {
                                // Clear cache
                                if (CMSControlsHelper.CurrentPageManager != null)
                                {
                                    CMSControlsHelper.CurrentPageManager.ClearCache();
                                }
                            }
                        }

                        // Set the edit mode
                        if (Node != null)
                        {
                            NodeID = Node.NodeID;
                            Action = "edit";
                            ReloadData(true);
                            CMSForm.lblInfo.Text = GetString("general.changessaved");
                        }
                    }

                    if (Node != null)
                    {
                        // Check in the document
                        if (AutoCheck)
                        {
                            VersionManager.CheckIn(Node, null, null);
                        }

                        // Log insert/update activity
                        switch (formModebeforeSave)
                        {
                            case FormModeEnum.Insert:
                            case FormModeEnum.InsertNewCultureVersion:
                                LogInsertActivity(Node);
                                break;
                            case FormModeEnum.Update:
                                LogUpdateActivity(Node);
                                break;
                        }
                    }

                    AddScript("changed=false;");
                }
                catch (Exception ex)
                {
                    AddAlert(GetString("ContentRequest.SaveFailed") + ": " + ex.Message);
                }
                return true;
            }
        }
        return false;
    }


    /// <summary>
    /// Approve button click event handler.
    /// </summary>
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        if (IsBannedIP())
        {
            return;
        }

        if (Node != null)
        {
            // Validate the form first
            if (formElem.BasicForm.ValidateData())
            {
                if (AutoCheck)
                {
                    if (SaveChanges)
                    {
                        SaveDocument();
                    }
                    else
                    {
                        formElem.BasicForm.LoadControlValues();
                    }
                }

                // Approve the document - Go to next workflow step
                // Get original step
                WorkflowStepInfo originalStep = WorkflowManager.GetStepInfo(Node);

                // Approve the document
                WorkflowStepInfo nextStep = WorkflowManager.MoveToNextStep(Node, "");
                if (nextStep != null)
                {
                    string nextStepName = nextStep.StepName.ToLower();
                    string originalStepName = originalStep.StepName.ToLower();

                    // Send workflow e-mails
                    if (SettingsKeyProvider.GetBoolValue(SiteName + ".CMSSendWorkflowEmails"))
                    {
                        WorkflowManager.SendWorkflowEmails(Node, CMSContext.CurrentUser, originalStep, nextStep, (nextStepName == "published") ? WorkflowActionEnum.Published : WorkflowActionEnum.Approved, "");
                    }
                    AddScript("if (window.PassiveRefresh) {\n window.PassiveRefresh(" + NodeID + ", " + Node.NodeParentID + ");\n }");

                    // Ensure correct message is displayed
                    if (nextStepName == "published")
                    {
                        formElem.lblInfo.Text += " " + GetString("workflowstep.customtopublished");
                    }
                    else if (originalStepName == "edit" && nextStepName != "published")
                    {
                        formElem.lblInfo.Text += " " + GetString("workflowstep.edittocustom");
                    }
                    else if ((originalStepName != "edit") && (nextStepName != "published") && (nextStepName != "archived"))
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
                    AddScript("     SelectNode(" + NodeID + ");");
                    formElem.lblInfo.Text += " " + GetString("ContentEdit.WasApproved");
                }
            }

            ReloadForm();

            RaiseOnAfterApprove();
        }
    }


    /// <summary>
    /// Reject button click event handler.
    /// </summary>
    protected void btnReject_Click(object sender, EventArgs e)
    {
        if (IsBannedIP())
        {
            return;
        }

        // Reject the document - Go to previous workflow step
        if (Node != null)
        {
            // Validate the form first
            if (formElem.BasicForm.ValidateData())
            {
                // Save the document first
                if (AutoCheck)
                {
                    if (SaveChanges)
                    {
                        SaveDocument();
                    }
                    else
                    {
                        formElem.BasicForm.LoadControlValues();
                    }
                }

                // Get original step
                WorkflowStepInfo originalStep = WorkflowManager.GetStepInfo(Node);

                // Reject the document
                WorkflowStepInfo previousStep = WorkflowManager.MoveToPreviousStep(Node, "");

                // Send workflow e-mails
                if (SettingsKeyProvider.GetBoolValue(SiteName + ".CMSSendWorkflowEmails"))
                {
                    WorkflowManager.SendWorkflowEmails(Node, CMSContext.CurrentUser, originalStep, previousStep, WorkflowActionEnum.Rejected, "");
                }

                formElem.lblInfo.Text += " " + GetString("ContentEdit.WasRejected");
            }

            ReloadForm();

            RaiseOnAfterReject();
        }
    }


    /// <summary>
    /// CheckIn button click event handler.
    /// </summary>
    protected void btnCheckIn_Click(object sender, EventArgs e)
    {
        if (IsBannedIP())
        {
            return;
        }

        if (Node != null)
        {
            // Validate the form first
            if (formElem.BasicForm.ValidateData())
            {
                // Save the document first
                if (SaveChanges)
                {
                    SaveDocument();
                }
                else
                {
                    formElem.BasicForm.LoadControlValues();
                }

                // Check in the document        
                VersionManager.CheckIn(Node, null, null);
                formElem.lblInfo.Text += " " + GetString("ContentEdit.WasCheckedIn");
            }

            ReloadForm();
        }
    }


    /// <summary>
    /// CheckOut button click event handler.
    /// </summary>
    protected void btnCheckOut_Click(object sender, EventArgs e)
    {
        if (IsBannedIP())
        {
            return;
        }

        // Check out the document
        if (Node != null)
        {
            VersionManager.EnsureVersion(Node, Node.IsPublished);

            // Check out the document
            VersionManager.CheckOut(Node);
        }

        ReloadForm();
    }


    /// <summary>
    /// Refresh button click event handler.
    /// </summary>
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        if (Node != null)
        {
            // Check permission to modify document
            if (!CheckPermissions || (CMSContext.CurrentUser.IsAuthorizedPerDocument(Node, NodePermissionsEnum.Modify) == AuthorizationResultEnum.Allowed))
            {
                // Move to edit step
                DocumentHelper.MoveDocumentToEditStep(Node, Node.TreeProvider);

                // Reload form
                ReloadForm();
                if (SaveChanges)
                {
                    ScriptHelper.RegisterStartupScript(this, typeof(string), "moveToEditStepChange", ScriptHelper.GetScript("Changed();"));
                }
            }
        }
    }


    /// <summary>
    /// UndoCheckOut click event handler.
    /// </summary>
    protected void btnUndoCheckOut_Click(object sender, EventArgs e)
    {
        if (IsBannedIP())
        {
            return;
        }

        // Undo check out the document
        if (Node != null)
        {
            // Undo check out
            VersionManager.UndoCheckOut(Node);
        }

        ReloadForm();
        formElem.LoadForm(true);

        // Reload the values if the form
        formElem.BasicForm.LoadControlValues();
    }


    /// <summary>
    /// New class selection click event handler.
    /// </summary>
    protected void btnClass_Command(object sender, CommandEventArgs e)
    {
        int newClassId = ValidationHelper.GetInteger(e.CommandArgument, 0);
        if (newClassId > 0)
        {
            ClassID = newClassId;
            ReloadData(true);
        }
    }


    /// <summary>
    /// OK button click event handler.
    /// </summary>
    protected void btnOK_Click(object sender, EventArgs e)
    {
        if (IsBannedIP())
        {
            return;
        }

        Action = "newculture";
        CopyDefaultDataFromDocumentID = radCopy.Checked ? ValidationHelper.GetInteger(lstCultures.SelectedValue, 0) : 0;

        ReloadData(true);
    }


    /// <summary>
    /// Yes button click event handler.
    /// </summary>
    protected void btnYes_Click(object sender, EventArgs e)
    {
        if (IsBannedIP())
        {
            return;
        }

        // Prepare the where condition
        string where = "NodeID = " + NodeID;

        // Get the documents
        DataSet ds = null;
        if (chkAllCultures.Checked)
        {
            ds = TreeProvider.SelectNodes(SiteName, "/%", TreeProvider.ALL_CULTURES, true, null, where, null, -1, false);
        }
        else
        {
            ds = TreeProvider.SelectNodes(SiteName, "/%", CultureCode, false, null, where, null, -1, false);
        }

        if (!DataHelper.DataSourceIsEmpty(ds))
        {
            // Get node alias
            string nodeAlias = ValidationHelper.GetString(DataHelper.GetDataRowValue(ds.Tables[0].Rows[0], "NodeAlias"), "");
            // Get parent alias path
            string parentAliasPath = TreePathUtils.GetParentPath(ValidationHelper.GetString(DataHelper.GetDataRowValue(ds.Tables[0].Rows[0], "NodeAliasPath"), ""));

            // Delete the documents
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                string aliasPath = ValidationHelper.GetString(dr["NodeAliasPath"], "");
                string culture = ValidationHelper.GetString(dr["DocumentCulture"], "");
                string className = ValidationHelper.GetString(dr["ClassName"], "");

                // Get the node
                TreeNode treeNode = TreeProvider.SelectSingleNode(SiteName, aliasPath, culture, false, className, false);

                if (treeNode != null)
                {
                    // Check delete permissions
                    bool hasUserDeletePermission = !CheckPermissions || IsUserAuthorizedToDeleteDocument(treeNode, chkDestroy.Checked);

                    if (hasUserDeletePermission)
                    {
                        // Delete the document
                        try
                        {
                            LogDeleteActivity(treeNode);
                            DocumentHelper.DeleteDocument(treeNode, TreeProvider, chkAllCultures.Checked, chkDestroy.Checked, true);

                            Action = "edit";
                            ReloadData(true);
                        }
                        catch (Exception ex)
                        {
                            EventLogProvider log = new EventLogProvider();
                            log.LogEvent(EventLogProvider.EVENT_TYPE_ERROR, DateTime.Now, "Content", "DELETEDOC", CMSContext.CurrentUser.UserID, CMSContext.CurrentUser.UserName, treeNode.NodeID, treeNode.DocumentName, HTTPHelper.UserHostAddress, EventLogProvider.GetExceptionLogMessage(ex), CMSContext.CurrentSite.SiteID, HTTPHelper.GetAbsoluteUri());
                            AddAlert(GetString("ContentRequest.DeleteFailed") + ": " + ex.Message);
                            return;
                        }
                    }
                    // Access denied - not authorized to delete the document
                    else
                    {
                        AddAlert(String.Format(GetString("cmsdesk.notauthorizedtodeletedocument"), treeNode.NodeAliasPath));
                        return;
                    }
                }
                else
                {
                    AddAlert(GetString("ContentRequest.ErrorMissingSource"));
                    return;
                }
            }

            RaiseOnAfterDelete();

            string rawUrl = URLRewriter.RawUrl;
            if ((nodeAlias != "") && (rawUrl.Substring(rawUrl.LastIndexOf('/')).Contains(nodeAlias)))
            {
                // Redirect to the parent url when current url belongs to deleted document
                URLHelper.Redirect(CMSContext.GetUrl(parentAliasPath));
            }
            else
            {
                // Redirect to current url
                URLHelper.Redirect(rawUrl);
            }
        }
        else
        {
            AddAlert(GetString("DeleteDocument.CultureNotExists"));
            return;
        }
    }


    /// <summary>
    /// No button click event handler.
    /// </summary>
    protected void btnNo_Click(object sender, EventArgs e)
    {
        Action = "edit";
        ReloadData(true);
    }


    /// <summary>
    /// Delete button click event handler.
    /// </summary>
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (IsBannedIP())
        {
            return;
        }

        Action = "delete";
        ReloadData(true);
    }


    protected override void OnPreRender(EventArgs e)
    {
        if (Visible)
        {
            // Render the styles link if live site mode
            if (CMSContext.ViewMode == ViewModeEnum.LiveSite)
            {
                CSSHelper.RegisterDesignMode(Page);
            }

            if (pnlEdit.Visible && (formElem.BasicForm != null))
            {
                // Register other scripts which are necessary in edit mode
                if (UseProgressScript)
                {
                    ScriptHelper.RegisterProgress(Page);
                }
                ScriptHelper.RegisterSpellChecker(Page);
                ScriptHelper.RegisterShortcuts(Page);
                ScriptHelper.RegisterSaveChanges(Page);

                // Register full postback if an uploader is used
                if (formElem.BasicForm.UploaderInUse)
                {
                    ControlsHelper.RegisterPostbackControl(btnApprove);
                    ControlsHelper.RegisterPostbackControl(btnSave);
                    ControlsHelper.RegisterPostbackControl(btnCheckIn);
                }

                // Register script
                StringBuilder sb = new StringBuilder();
                if (btnSave.Enabled && menuElem.AllowSave)
                {
                    sb.AppendLine("function " + FunctionsPrefix + "SaveDocument(NodeID) { if (window.AllowSubmit) { AllowSubmit(); } " + Page.ClientScript.GetPostBackEventReference(btnSave, null) + "; } \n");
                }
                else
                {
                    sb.AppendLine("function " + FunctionsPrefix + "SaveDocument(NodeID) {}");
                }
                sb.AppendLine("function " + FunctionsPrefix + "Approve(NodeID) { if (window.SubmitAction) { SubmitAction(); } " + Page.ClientScript.GetPostBackEventReference(btnApprove, null) + "; } \n");
                sb.AppendLine("function " + FunctionsPrefix + "Reject(NodeID) { if (window.SubmitAction) { SubmitAction(); } " + Page.ClientScript.GetPostBackEventReference(btnReject, null) + "; } \n");
                sb.AppendLine("function " + FunctionsPrefix + "CheckIn(NodeID) { if (window.SubmitAction) { SubmitAction(); } " + Page.ClientScript.GetPostBackEventReference(btnCheckIn, null) + "; } \n");
                sb.AppendLine("var confirmLeave='" + GetString("Content.ConfirmLeave") + "'; \n");
                sb.AppendLine("function " + FunctionsPrefix + "CheckOut(NodeID) { " + Page.ClientScript.GetPostBackEventReference(btnCheckOut, null) + "; } \n");
                sb.AppendLine("function " + FunctionsPrefix + "UndoCheckOut(NodeID) { if(!confirm(" + ScriptHelper.GetString(GetString("EditMenu.UndoCheckOutConfirmation")) + ")) return false; " + Page.ClientScript.GetPostBackEventReference(btnUndoCheckOut, null) + "; } \n");
                sb.AppendLine("function " + FunctionsPrefix + "Delete(NodeID) { " + Page.ClientScript.GetPostBackEventReference(btnDelete, null) + "; } \n");
                if (!string.IsNullOrEmpty(FunctionsPrefix))
                {
                    sb.AppendLine("function SaveDocument(NodeID) { " + FunctionsPrefix + "SaveDocument(NodeID); } \n");
                    sb.AppendLine("\n function " + FunctionsPrefix + "SpellCheck() { checkSpelling(spellURL); } \n");
                }
                sb.Append("\n var spellURL = '");

                if (CMSContext.ViewMode == ViewModeEnum.LiveSite)
                {
                    sb.Append(CMSContext.ResolveDialogUrl("~/CMSFormControls/LiveSelectors/SpellCheck.aspx"));
                }
                else
                {
                    sb.Append(CMSContext.ResolveDialogUrl("~/CMSModules/Content/CMSDesk/Edit/SpellCheck.aspx"));
                }
                sb.AppendLine("'; \n");

                sb.AppendLine("function PassiveRefresh(nodeId, selectNodeId) {} \n");
                sb.AppendLine("function " + formElem.ClientID + "_RefreshForm(){" + Page.ClientScript.GetPostBackEventReference(btnRefresh, "") + " }");

                // Register the scripts
                AddScript(sb.ToString());

                // Disable maximize plugin on HTML editors                
                FormFieldInfo[] htmlControls = formElem.BasicForm.FormInformation.GetFields(FormFieldControlTypeEnum.HtmlAreaControl);
                if (htmlControls.Length > 0)
                {
                    foreach (FormFieldInfo field in htmlControls)
                    {
                        Control control = formElem.BasicForm.FieldControls[field.Name] as Control;
                        CMSHtmlEditor htmlEditor = ControlsHelper.GetChildControl(control, typeof(CMSHtmlEditor)) as CMSHtmlEditor;
                        if (htmlEditor != null)
                        {
                            htmlEditor.RemovePlugin("maximize");
                        }
                    }
                }
            }
        }

        base.OnPreRender(e);
    }


    /// <summary>
    /// Checks whether the user is authorized to delete document.
    /// </summary>
    /// <param name="treeNode">Document node</param>
    /// <param name="deleteDocHistory">Delete document history?</param>
    protected bool IsUserAuthorizedToDeleteDocument(TreeNode treeNode, bool deleteDocHistory)
    {
        bool isAuthorized = true;

        CurrentUserInfo currentUser = CMSContext.CurrentUser;

        // Check delete permission
        if (currentUser.IsAuthorizedPerDocument(treeNode, new NodePermissionsEnum[] { NodePermissionsEnum.Delete, NodePermissionsEnum.Read }) == AuthorizationResultEnum.Allowed)
        {
            if (deleteDocHistory)
            {
                // Check destroy permisson
                if (currentUser.IsAuthorizedPerDocument(treeNode, NodePermissionsEnum.Destroy) != AuthorizationResultEnum.Allowed)
                {
                    isAuthorized = false;
                }
            }
        }
        else
        {
            isAuthorized = false;
        }

        return isAuthorized;
    }


    /// <summary>
    /// Check if user IP is banned.
    /// </summary>
    private bool IsBannedIP()
    {
        // Check banned ip
        if (!BannedIPInfoProvider.IsAllowed(CMSContext.CurrentSiteName, BanControlEnum.AllNonComplete))
        {
            AddAlert(GetString("General.BannedIP"));
            return true;
        }
        return false;
    }


    /// <summary>
    /// Returns alternative form name in full version - 'ClassName.AltFormCodeName'.
    /// </summary>
    /// <param name="className">Class name</param>
    private string GetAltFormFullName(string className)
    {
        if (!string.IsNullOrEmpty(AlternativeFormName) && !string.IsNullOrEmpty(className) && !AlternativeFormName.StartsWith(className))
        {
            if (AlternativeFormName.Contains("."))
            {
                // Remove class name if it is different from class name in parameter
                AlternativeFormName = AlternativeFormName.Remove(0, AlternativeFormName.LastIndexOf(".") + 1);
            }
            return className + "." + AlternativeFormName;
        }
        else
        {
            return AlternativeFormName;
        }
    }


    /// <summary>
    /// Raises the OnAfterApprove event.
    /// </summary>
    private void RaiseOnAfterApprove()
    {
        if (OnAfterApprove != null)
        {
            OnAfterApprove(this, null);
        }
    }


    /// <summary>
    /// Raises the OnAfterReject event.
    /// </summary>
    private void RaiseOnAfterReject()
    {
        if (OnAfterReject != null)
        {
            OnAfterReject(this, null);
        }
    }


    /// <summary>
    /// Raises the OnAfterDelete event.
    /// </summary>
    private void RaiseOnAfterDelete()
    {
        if (OnAfterDelete != null)
        {
            OnAfterDelete(this, null);
        }
    }


    /// <summary>
    /// Logs activity of given type and for given node .
    /// </summary>
    /// <param name="node">Node</param>
    /// <param name="type">Activity type</param>
    /// <param name="siteId">Site ID</param>
    /// <param name="siteName">Site name</param>
    private void LogUserContribActivity(TreeNode node, string type, int siteId, string siteName)
    {
        var data = new ActivityData()
        {
            ContactID = ModuleCommands.OnlineMarketingGetCurrentContactID(),
            SiteID = siteId,
            Type = type,
            TitleData = node.DocumentName,
            NodeID = node.NodeID,
            URL = URLHelper.CurrentRelativePath,
            Culture = node.DocumentCulture,
            Campaign = CMSContext.Campaign
        };
        ActivityLogProvider.LogActivity(data);
    }



    /// <summary>
    /// Logs "insert" activity
    /// </summary>
    /// <param name="node">Node</param>
    private void LogInsertActivity(TreeNode node)
    {
        string siteName = CMSContext.CurrentSiteName;
        if ((node == null) || !this.LogActivity || (CMSContext.ViewMode != ViewModeEnum.LiveSite)
            || !ActivitySettingsHelper.ActivitiesEnabledAndModuleLoaded(siteName) || !ActivitySettingsHelper.ActivitiesEnabledForThisUser(CMSContext.CurrentUser))
        {
            return;
        }

        if (ActivitySettingsHelper.WikiContributionInsertEnabled(siteName))
        {
            LogUserContribActivity(node, PredefinedActivityType.USER_CONTRIB_INSERT, CMSContext.CurrentSiteID, siteName);
        }
    }


    /// <summary>
    /// Logs "update" activity
    /// </summary>
    /// <param name="node"></param>
    private void LogUpdateActivity(TreeNode node)
    {
        string siteName = CMSContext.CurrentSiteName;
        if ((node == null) || !this.LogActivity || (CMSContext.ViewMode != ViewModeEnum.LiveSite)
            || !ActivitySettingsHelper.ActivitiesEnabledAndModuleLoaded(siteName) || !ActivitySettingsHelper.ActivitiesEnabledForThisUser(CMSContext.CurrentUser))
        {
            return;
        }

        if (ActivitySettingsHelper.WikiContributionUpdateEnabled(siteName))
        {
            LogUserContribActivity(node, PredefinedActivityType.USER_CONTRIB_UPDATE, CMSContext.CurrentSiteID, siteName);
        }
    }


    /// <summary>
    /// Logs "delete" activity
    /// </summary>
    /// <param name="node"></param>
    private void LogDeleteActivity(TreeNode node)
    {
        string siteName = CMSContext.CurrentSiteName;
        if ((node == null) || !this.LogActivity || (CMSContext.ViewMode != ViewModeEnum.LiveSite)
            || !ActivitySettingsHelper.ActivitiesEnabledAndModuleLoaded(siteName) || !ActivitySettingsHelper.ActivitiesEnabledForThisUser(CMSContext.CurrentUser))
        {
            return;
        }

        if (ActivitySettingsHelper.WikiContributionDeleteEnabled(siteName))
        {
            LogUserContribActivity(node, PredefinedActivityType.USER_CONTRIB_DELETE, CMSContext.CurrentSiteID, siteName);
        }
    }

    #endregion
}
