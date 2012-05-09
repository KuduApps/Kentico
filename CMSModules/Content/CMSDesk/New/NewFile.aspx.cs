using System;
using System.Web.UI.WebControls;

using CMS.CMSHelper;
using CMS.FormEngine;
using CMS.GlobalHelper;
using CMS.IO;
using CMS.LicenseProvider;
using CMS.PortalEngine;
using CMS.SettingsProvider;
using CMS.TreeEngine;
using CMS.UIControls;
using CMS.WorkflowEngine;

using TreeNode = CMS.TreeEngine.TreeNode;

public partial class CMSModules_Content_CMSDesk_New_NewFile : CMSContentPage
{
    #region "Variables"

    private int nodeId = 0;


    private bool isDialog = false;


    private int templateId = 0;


    private FormFieldInfo mFieldInfo = null;


    private FormFieldInfo mFileNameFieldInfo = null;


    private DataClassInfo mDataClass = null;


    private int mResizeToWidth = 0;


    private int mResizeToHeight = 0;


    private int mResizeToMaxSideSize = 0;


    private string mAllowedExtensions = null;

    #endregion


    #region "Properties"


    /// <summary>
    /// Class field info.
    /// </summary>
    public FormFieldInfo FieldInfo
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
    /// Class field info for file name field.
    /// </summary>
    public FormFieldInfo FileNameFieldInfo
    {
        get
        {
            if (mFileNameFieldInfo == null)
            {
                FormInfo fi = FormHelper.GetFormInfo(DataClass.ClassName, true);

                // Get valid extensions from form field info
                mFileNameFieldInfo = fi.GetFormField("FileName");
            }
            return mFileNameFieldInfo;
        }
    }


    /// <summary>
    /// Data class info for the cms.file.
    /// </summary>
    public DataClassInfo DataClass
    {
        get
        {
            if (mDataClass == null)
            {
                // Get document type ('cms.file') settings
                mDataClass = DataClassInfoProvider.GetDataClass("cms.file");
                if (mDataClass == null)
                {
                    throw new Exception("[NewFile.aspx]: Class 'cms.file' is missing!");
                }
            }
            return mDataClass;
        }
    }


    /// <summary>
    /// Unique GUID.
    /// </summary>
    public Guid Guid
    {
        get
        {
            Guid guid = ValidationHelper.GetGuid(ViewState["Guid"], Guid.Empty);
            if (guid == Guid.Empty)
            {
                guid = Guid.NewGuid();
                ViewState["Guid"] = guid;
            }
            return guid;
        }
    }


    /// <summary>
    /// Resize to width.
    /// </summary>
    private int ResizeToWidth
    {
        get
        {
            return mResizeToWidth;
        }
        set
        {
            mResizeToWidth = value;
        }
    }


    /// <summary>
    /// Resize to height.
    /// </summary>
    private int ResizeToHeight
    {
        get
        {
            return mResizeToHeight;
        }
        set
        {
            mResizeToHeight = value;
        }
    }


    /// <summary>
    /// Resize to maximal side size.
    /// </summary>
    private int ResizeToMaxSideSize
    {
        get
        {
            return mResizeToMaxSideSize;
        }
        set
        {
            mResizeToMaxSideSize = value;
        }
    }


    /// <summary>
    /// Allowed extensions.
    /// </summary>
    private string AllowedExtensions
    {
        get
        {
            return mAllowedExtensions;
        }
        set
        {
            mAllowedExtensions = value;
        }
    }


    /// <summary>
    /// Indicates if file uploader control should be used.
    /// </summary>
    private bool UseFileUploader
    {
        get
        {
            return FormHelper.IsFieldOfType(FieldInfo, FormFieldControlTypeEnum.UploadControl);
        }
    }

    #endregion


    #region "Methods"

    protected override void OnPreInit(EventArgs e)
    {
        ((Panel)CurrentMaster.PanelBody.FindControl("pnlContent")).CssClass = "";
        base.OnPreInit(e);
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        // Check if required fields exist ("FileName", "FileAttachment")
        if ((FieldInfo == null) || (FileNameFieldInfo == null))
        {
            lblError.Visible = true;
            lblError.Text = GetString("NewFile.SomeOfRequiredFieldsMissing");
            pnlForm.Visible = false;
            return;
        }

        // Register progrees script
        ScriptHelper.RegisterShortcuts(this);
        ScriptHelper.RegisterProgress(Page);
        ScriptHelper.RegisterSpellChecker(this);

        ltlSpellScript.Text = GetSpellCheckDialog();

        if (!LicenseHelper.LicenseVersionCheck(URLHelper.GetCurrentDomain(), FeatureEnum.Documents, VersionActionEnum.Insert))
        {
            RedirectToAccessDenied(String.Format(GetString("cmsdesk.documentslicenselimits"), ""));
        }

        // Check if need template selection, if so, then redirect to template selection page
        if ((DataClass.ClassShowTemplateSelection) && (templateId == 0))
        {
            URLHelper.Redirect("~/CMSModules/Content/CMSDesk/TemplateSelection.aspx" + URLHelper.Url.Query);
        }

        nodeId = ValidationHelper.GetInteger(Request.QueryString["nodeid"], 0);

        // Get the node
        TreeProvider tree = new TreeProvider(CMSContext.CurrentUser);
        TreeNode node = tree.SelectSingleNode(nodeId, CMSContext.PreferredCultureCode, true);
        if (node != null)
        {
            if (!DataClassInfoProvider.IsChildClassAllowed(ValidationHelper.GetInteger(node.GetValue("NodeClassID"), 0), DataClass.ClassID))
            {
                lblError.Text = GetString("Content.ChildClassNotAllowed");
                pnlForm.Visible = false;
                return;
            }
        }

        if (!CMSContext.CurrentUser.IsAuthorizedToCreateNewDocument(node, DataClass.ClassName))
        {
            lblError.Text = GetString("Content.NotAuthorizedFile");
            pnlForm.Visible = false;
            return;
        }

        // Set request timeout
        Server.ScriptTimeout = AttachmentHelper.ScriptTimeout;

        // Indicates whether new file is added from the dialog window (HTML editor -> InsertImage -> BrowseServer -> Upload)
        isDialog = (ValidationHelper.GetString(Request.QueryString["dialog"], "") == "1");

        plcDirect.Visible = !UseFileUploader;
        plcUploader.Visible = UseFileUploader;

        InitializeProperties();

        // Init direct uploader
        if (!UseFileUploader)
        {
            ucDirectUploader.GUIDColumnName = FieldInfo.Name;
            ucDirectUploader.AllowDelete = FieldInfo.AllowEmpty;
            ucDirectUploader.FormGUID = Guid;
            ucDirectUploader.ResizeToHeight = ResizeToHeight;
            ucDirectUploader.ResizeToWidth = ResizeToWidth;
            ucDirectUploader.ResizeToMaxSideSize = ResizeToMaxSideSize;
            ucDirectUploader.AllowedExtensions = AllowedExtensions;
            ucDirectUploader.NodeParentNodeID = (node != null) ? node.NodeParentID : 0;
            ucDirectUploader.IsLiveSite = false;
        }

        lblFileDescription.Text = GetString("NewFile.FileDescription");
        lblUploadFile.Text = GetString("NewFile.UploadFile");

        ltlScript.Text = ScriptHelper.GetScript("function SaveDocument(nodeId, createAnother) { document.getElementById('hidAnother').value = createAnother; " + ClientScript.GetPostBackEventReference(btnOk, null) + "; }");
    }


    protected void btnOk_Click(object sender, EventArgs e)
    {
        // Check allowed cultures
        if (!CMSContext.CurrentUser.IsCultureAllowed(CMSContext.PreferredCultureCode, CMSContext.CurrentSiteName))
        {
            lblError.Text = GetString("Content.NotAuthorizedFile");
            pnlForm.Visible = false;
            return;
        }

        TreeNode node = null;
        TreeProvider tree = new TreeProvider(CMSContext.CurrentUser);

        if ((UseFileUploader && !FileUpload.HasFile) || (!UseFileUploader && !ucDirectUploader.HasData()))
        {
            lblError.Text = GetString("NewFile.ErrorEmpty");
        }
        else
        {
            // Get file extension
            string fileExtension = UseFileUploader ? FileUpload.FileName : ucDirectUploader.AttachmentName;
            fileExtension = Path.GetExtension(fileExtension).TrimStart('.');

            // Check file extension
            if (IsExtensionAllowed(fileExtension))
            {
                bool newDocumentCreated = false;

                try
                {
                    if (UseFileUploader)
                    {
                        // Process file using file upload
                        node = ProcessFileUploader(tree);
                    }
                    else
                    {
                        // Process file using direct uploader
                        node = ProcessDirectUploader(tree);

                        // Save temporary attachments
                        DocumentHelper.SaveTemporaryAttachments(node, Guid, CMSContext.CurrentSiteName, tree);
                    }

                    newDocumentCreated = true;

                    // Create default SKU if configured
                    if (ModuleEntry.CheckModuleLicense(ModuleEntry.ECOMMERCE, URLHelper.GetCurrentDomain(), FeatureEnum.Ecommerce, VersionActionEnum.Insert))
                    {
                        bool? skuCreated = node.CreateDefaultSKU();
                        if (skuCreated.HasValue && !skuCreated.Value)
                        {
                            lblError.Text = GetString("com.CreateDefaultSKU.Error");
                        }
                    }

                    // Set additional values
                    if (!string.IsNullOrEmpty(fileExtension))
                    {
                        // Update document extensions if no custom are used
                        if (!node.DocumentUseCustomExtensions)
                        {
                            node.DocumentExtensions = "." + fileExtension;
                        }
                        node.SetValue("DocumentType", "." + fileExtension);
                    }

                    // Update the document
                    DocumentHelper.UpdateDocument(node, tree);

                    WorkflowManager workflowManager = new WorkflowManager(tree);
                    // Get workflow info
                    WorkflowInfo workflowInfo = workflowManager.GetNodeWorkflow(node);

                    // Check if auto publish changes is allowed
                    if ((workflowInfo != null) && workflowInfo.WorkflowAutoPublishChanges && !workflowInfo.UseCheckInCheckOut(CMSContext.CurrentSiteName))
                    {
                        // Automatically publish document
                        workflowManager.AutomaticallyPublish(node, workflowInfo, null);
                    }

                    bool createAnother = ValidationHelper.GetBoolean(Request.Params["hidAnother"], false);

                    // Added from CMSDesk->Content
                    if (!isDialog)
                    {
                        if (createAnother)
                        {
                            ltlScript.Text += ScriptHelper.GetScript("PassiveRefresh(" + node.NodeParentID + ", " + node.NodeParentID + "); CreateAnother();");
                        }
                        else
                        {
                            ltlScript.Text += ScriptHelper.GetScript(
                                "   RefreshTree(" + node.NodeID + ", " + node.NodeID + "); SelectNode(" + node.NodeID + "); \n"
                            );
                        }
                    }
                    // Added from dialog window
                    else
                    {
                        if (createAnother)
                        {
                            txtFileDescription.Text = "";
                            ltlScript.Text += ScriptHelper.GetScript("FileCreated(" + node.NodeID + ", " + node.NodeParentID + ", false);");
                        }
                        else
                        {
                            ltlScript.Text += ScriptHelper.GetScript("FileCreated(" + node.NodeID + ", " + node.NodeParentID + ", true);");
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Delete the document if something failed
                    if (newDocumentCreated && (node != null) && (node.DocumentID > 0))
                    {
                        DocumentHelper.DeleteDocument(node, tree, false, true, true);
                    }
                    lblError.Text = GetString("NewFile.Failed") + ": " + ex.Message;
                }
            }
            else
            {
                lblError.Text = String.Format(GetString("NewFile.ExtensionNotAllowed"), fileExtension);
            }
        }
    }


    protected void InitializeProperties()
    {
        string autoresize = ValidationHelper.GetString(FieldInfo.Settings["autoresize"], "").ToLower();

        // Set custom settings
        if (autoresize == "custom")
        {
            if (FieldInfo.Settings["autoresize_width"] != null)
            {
                ResizeToWidth = ValidationHelper.GetInteger(FieldInfo.Settings["autoresize_width"], 0);
            }
            if (FieldInfo.Settings["autoresize_height"] != null)
            {
                ResizeToHeight = ValidationHelper.GetInteger(FieldInfo.Settings["autoresize_height"], 0);
            }
            if (FieldInfo.Settings["autoresize_maxsidesize"] != null)
            {
                ResizeToMaxSideSize = ValidationHelper.GetInteger(FieldInfo.Settings["autoresize_maxsidesize"], 0);
            }
        }
        // Set site settings
        else if (autoresize == "")
        {
            string siteName = CMSContext.CurrentSiteName;
            ResizeToWidth = ImageHelper.GetAutoResizeToWidth(siteName);
            ResizeToHeight = ImageHelper.GetAutoResizeToHeight(siteName);
            ResizeToMaxSideSize = ImageHelper.GetAutoResizeToMaxSideSize(siteName);
        }

        if (UseFileUploader)
        {
            string siteName = CMSContext.CurrentSiteName;
            string siteExtensions = SettingsKeyProvider.GetStringValue(siteName + ".CMSUploadExtensions");
            string allExtensions = siteExtensions;
            string customExtensions = ValidationHelper.GetString(FieldInfo.Settings["allowed_extensions"], "");
            if (!string.IsNullOrEmpty(customExtensions))
            {
                allExtensions += ";" + customExtensions;
            }
            AllowedExtensions = allExtensions;
        }
        else
        {
            if (ValidationHelper.GetString(FieldInfo.Settings["extensions"], "") == "custom")
            {
                // Load allowed extensions
                AllowedExtensions = ValidationHelper.GetString(FieldInfo.Settings["allowed_extensions"], "");
            }
            else
            {
                // Use site settings
                string siteName = CMSContext.CurrentSiteName;
                AllowedExtensions = SettingsKeyProvider.GetStringValue(siteName + ".CMSUploadExtensions");
            }
        }
    }


    protected TreeNode ProcessFileUploader(TreeProvider tree)
    {
        TreeNode node = null;

        // Create new document
        string fileName = Path.GetFileNameWithoutExtension(FileUpload.FileName);

        int maxFileNameLength = FileNameFieldInfo.Size;
        if (fileName.Length > maxFileNameLength)
        {
            fileName = fileName.Substring(0, maxFileNameLength);
        }

        node = TreeNode.New("CMS.File", tree);
        node.DocumentCulture = CMSContext.PreferredCultureCode;
        node.DocumentName = fileName;

        // Load default values
        FormHelper.LoadDefaultValues(node);

        if (node.ContainsColumn("FileDescription"))
        {
            node.SetValue("FileDescription", txtFileDescription.Text);
        }
        //node.SetValue("FileName", fileName);
        node.SetValue("FileAttachment", Guid.Empty);

        // Set default template ID
        if (templateId > 0)
        {
            node.DocumentPageTemplateID = templateId;
        }
        else
        {
            node.DocumentPageTemplateID = DataClass.ClassDefaultPageTemplateID;
        }

        // Insert the document
        DocumentHelper.InsertDocument(node, nodeId, tree);

        // Add the file
        DocumentHelper.AddAttachment(node, "FileAttachment", FileUpload.PostedFile, tree, ResizeToWidth, ResizeToHeight, ResizeToMaxSideSize);

        return node;
    }


    protected TreeNode ProcessDirectUploader(TreeProvider tree)
    {
        TreeNode node = null;

        // Create new document
        string fileName = Path.GetFileNameWithoutExtension(ucDirectUploader.AttachmentName);

        int maxFileNameLength = FileNameFieldInfo.Size;
        if (fileName.Length > maxFileNameLength)
        {
            fileName = fileName.Substring(0, maxFileNameLength);
        }

        node = TreeNode.New("CMS.File", tree);
        node.DocumentCulture = CMSContext.PreferredCultureCode;
        node.DocumentName = fileName;

        // Load default values
        FormHelper.LoadDefaultValues(node);

        if (node.ContainsColumn("FileDescription"))
        {
            node.SetValue("FileDescription", txtFileDescription.Text);
        }
        //node.SetValue("FileName", fileName);
        node.SetValue("FileAttachment", Guid.Empty);

        // Set default template ID
        if (templateId > 0)
        {
            node.DocumentPageTemplateID = templateId;
        }
        else
        {
            node.DocumentPageTemplateID = DataClass.ClassDefaultPageTemplateID;
        }

        // Insert the document
        DocumentHelper.InsertDocument(node, nodeId, tree);

        // Set the attachment GUID later - important when document is under workflow and  using check-in/check-out
        node.SetValue("FileAttachment", ucDirectUploader.Value);

        return node;
    }


    /// <summary>
    /// Determines whether file with specified extension can be uploaded.
    /// </summary>
    /// <param name="extension">File extension to check</param>
    protected bool IsExtensionAllowed(string extension)
    {
        if (string.IsNullOrEmpty(AllowedExtensions))
        {
            return true;
        }


        // Remove starting dot from tested extension
        extension = extension.TrimStart('.').ToLower();

        string extensions = ";" + AllowedExtensions.ToLower() + ";";
        return ((extensions.Contains(";" + extension + ";")) || (extensions.Contains(";." + extension + ";")));
    }

    #endregion
}