using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Text;

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.TreeEngine;
using CMS.WorkflowEngine;
using CMS.FormControls;
using CMS.ExtendedControls;
using CMS.SiteProvider;
using CMS.UIControls;
using CMS.SettingsProvider;

public partial class CMSModules_Content_Controls_Attachments_DocumentAttachments_DirectUploader : AttachmentsControl, IUploaderControl
{
    #region "Variables"

    private string mInnerDivClass = "NewAttachment";
    private Guid attachmentGuid = Guid.Empty;
    private AttachmentInfo innerAttachment = null;
    private bool createTempAttachment = false;

    #endregion


    #region "Properties"

    /// <summary>
    /// CSS class of the new attachment link.
    /// </summary>
    public string InnerDivClass
    {
        get
        {
            return mInnerDivClass;
        }
        set
        {
            mInnerDivClass = value;
        }
    }


    /// <summary>
    /// Last performed action.
    /// </summary>
    public string LastAction
    {
        get
        {
            return ValidationHelper.GetString(ViewState["LastAction"], null);
        }
        set
        {
            ViewState["LastAction"] = value;
        }
    }


    /// <summary>
    /// Info label.
    /// </summary>
    public Label InfoLabel
    {
        get
        {
            return lblInfo;
        }
    }


    /// <summary>
    /// Inner attachment GUID (GUID of temporary attachment created for new culture version).
    /// </summary>
    public override Guid InnerAttachmentGUID
    {
        get
        {
            return ValidationHelper.GetGuid(ViewState["InnerAttachmentGUID"], base.InnerAttachmentGUID);
        }
        set
        {
            ViewState["InnerAttachmentGUID"] = value;
            base.InnerAttachmentGUID = value;
        }
    }


    /// <summary>
    /// Name of document attachment column.
    /// </summary>
    public override string GUIDColumnName
    {
        get
        {
            return base.GUIDColumnName;
        }
        set
        {
            base.GUIDColumnName = value;
            if ((dsAttachments != null) && (newAttachmentElem != null))
            {
                newAttachmentElem.AttachmentGUIDColumnName = value;
            }
        }
    }


    /// <summary>
    /// Width of the attachment.
    /// </summary>
    public override int ResizeToWidth
    {
        get
        {
            return base.ResizeToWidth;
        }
        set
        {
            base.ResizeToWidth = value;
            if (newAttachmentElem != null)
            {
                newAttachmentElem.ResizeToWidth = value;
            }
        }
    }


    /// <summary>
    /// Height of the attachment.
    /// </summary>
    public override int ResizeToHeight
    {
        get
        {
            return base.ResizeToHeight;
        }
        set
        {
            base.ResizeToHeight = value;
            if (newAttachmentElem != null)
            {
                newAttachmentElem.ResizeToHeight = value;
            }
        }
    }


    /// <summary>
    /// Maximal side size of the attachment.
    /// </summary>
    public override int ResizeToMaxSideSize
    {
        get
        {
            return base.ResizeToMaxSideSize;
        }
        set
        {
            base.ResizeToMaxSideSize = value;
            if (newAttachmentElem != null)
            {
                newAttachmentElem.ResizeToMaxSideSize = value;
            }
        }
    }


    /// <summary>
    /// List of allowed extensions.
    /// </summary>
    public override string AllowedExtensions
    {
        get
        {
            return base.AllowedExtensions;
        }
        set
        {
            base.AllowedExtensions = value;
            if (newAttachmentElem != null)
            {
                newAttachmentElem.AllowedExtensions = value;
            }
        }
    }


    /// <summary>
    /// Specifies the document for which the attachments should be displayed.
    /// </summary>
    public override int DocumentID
    {
        get
        {
            return base.DocumentID;
        }
        set
        {
            base.DocumentID = value;
            if (newAttachmentElem != null)
            {
                newAttachmentElem.DocumentID = value;
            }
        }
    }


    /// <summary>
    /// Specifies the version of the document (optional).
    /// </summary>
    public override int VersionHistoryID
    {
        get
        {
            int versionHistoryId = base.VersionHistoryID;
            if ((versionHistoryId == 0) && (Node != null))
            {
                versionHistoryId = Node.DocumentCheckedOutVersionHistoryID;
            }
            return versionHistoryId;
        }
        set
        {
            base.VersionHistoryID = value;
            if ((dsAttachments != null) && UsesWorkflow)
            {
                dsAttachments.DocumentVersionHistoryID = value;
            }
        }
    }


    /// <summary>
    /// Defines the form GUID; indicates that the temporary attachment will be handled.
    /// </summary>
    public override Guid FormGUID
    {
        get
        {
            return base.FormGUID;
        }
        set
        {
            base.FormGUID = value;
            if ((dsAttachments != null) && (newAttachmentElem != null))
            {
                dsAttachments.AttachmentFormGUID = value;
                newAttachmentElem.FormGUID = value;
            }
        }
    }


    /// <summary>
    /// Value of the control.
    /// </summary>
    public override object Value
    {
        get
        {
            return ViewState["Value"];
        }
        set
        {
            ViewState["Value"] = value;
        }
    }


    /// <summary>
    /// Name of the attachment.
    /// </summary>
    public string AttachmentName
    {
        get
        {
            return hdnAttachName.Value;
        }
    }


    /// <summary>
    /// If true, control does not process the data.
    /// </summary>
    public override bool StopProcessing
    {
        get
        {
            return base.StopProcessing;
        }
        set
        {
            base.StopProcessing = value;
            if ((dsAttachments != null) && (newAttachmentElem != null))
            {
                dsAttachments.StopProcessing = value;
                newAttachmentElem.StopProcessing = value;
            }
        }
    }

    #endregion


    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Register script for tooltips
        if (ShowTooltip)
        {
            ScriptHelper.RegisterTooltip(Page);
        }

        // Ensure info message
        if ((Request["__EVENTTARGET"] == hdnPostback.ClientID) || Request["__EVENTTARGET"] == hdnFullPostback.ClientID)
        {
            string action = Request["__EVENTARGUMENT"];

            if (action != null)
            {
                string[] values = action.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                action = values[0];
                if ((values.Length > 1) && ValidationHelper.IsGuid(values[1]))
                {
                    Value = values[1];
                }
            }

            LastAction = action;
        }


        #region "Scripts"

        // Refresh script
        StringBuilder script = new StringBuilder();
        script.AppendLine();
        script.AppendLine("function RefreshUpdatePanel_" + ClientID + @"(hiddenFieldID, action)");
        script.AppendLine("{");
        script.AppendLine("  var hiddenField = document.getElementById(hiddenFieldID);");
        script.AppendLine("  if (hiddenField)");
        script.AppendLine("  {");
        script.AppendLine("    __doPostBack(hiddenFieldID, action);");
        script.AppendLine("  }");
        script.AppendLine("}");

        script.AppendLine("function FullRefresh(hiddenFieldID, action)");
        script.AppendLine("{");
        script.AppendLine("  if (PassiveRefresh != null)");
        script.AppendLine("  {");
        script.AppendLine("    PassiveRefresh();");
        script.AppendLine("  }");
        script.AppendLine("  var hiddenField = document.getElementById(hiddenFieldID);");
        script.AppendLine("  if (hiddenField)");
        script.AppendLine("  {");
        script.AppendLine("    __doPostBack(hiddenFieldID, action);");
        script.AppendLine("  }");
        script.AppendLine("}");

        script.AppendLine("function FullPageRefresh_" + ClientID + @"(guid)");
        script.AppendLine("{");
        script.AppendLine("  if (PassiveRefresh != null)");
        script.AppendLine("  {");
        script.AppendLine("    PassiveRefresh();");
        script.AppendLine("  }");
        script.AppendLine("  var hiddenField = document.getElementById('" + hdnFullPostback.ClientID + "');");
        script.AppendLine("  if (hiddenField)");
        script.AppendLine("  {");
        script.AppendLine("    __doPostBack('" + hdnFullPostback.ClientID + "', 'refresh|' + guid);");
        script.AppendLine("  }");
        script.AppendLine("}");

        // Initialize refresh script for update panel
        script.AppendLine("function InitRefresh_" + ClientID + "(msg, fullRefresh, refreshTree, guid, action)");
        script.AppendLine("{");
        script.AppendLine("  if ((msg != null) && (msg != \"\"))");
        script.AppendLine("  {");
        script.AppendLine("    alert(msg);");
        script.AppendLine("    action='error';");
        script.AppendLine("  }");
        if (ActualNode != null)
        {
            script.AppendLine(" if (window.RefreshTree && (fullRefresh || refreshTree)) { RefreshTree(" + ActualNode.NodeParentID + ", " + ActualNode.NodeID + "); }");
        }
        script.AppendLine("  if (fullRefresh)");
        script.AppendLine("  {");
        script.AppendLine("    FullRefresh('" + hdnFullPostback.ClientID + "', action + '|' + guid);");
        script.AppendLine("  }");
        script.AppendLine("  else");
        script.AppendLine("  {");
        script.AppendLine("    RefreshUpdatePanel_" + ClientID + "('" + hdnPostback.ClientID + "', action + '|' + guid);");
        script.AppendLine("  }");
        script.AppendLine("}");

        // Initialize deletion confirmation
        script.AppendLine("function DeleteConfirmation()");
        script.AppendLine("{");
        script.AppendLine("  return confirm('" + GetString("attach.deleteconfirmation") + "');");
        script.AppendLine("}");

        #endregion

        ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "AttachmentScripts_" + ClientID, ScriptHelper.GetScript(script.ToString()));

        // Register dialog script
        ScriptHelper.RegisterDialogScript(Page);

        // Register jQuery script for thumbnails updating
        ScriptHelper.RegisterJQuery(Page);

        // Grid initialization
        gridAttachments.OnExternalDataBound += GridDocsOnExternalDataBound;
        gridAttachments.OnAction += GridAttachmentsOnAction;
        gridAttachments.IsLiveSite = IsLiveSite;
        gridAttachments.Pager.PageSizeOptions = "10";
        gridAttachments.Pager.DefaultPageSize = 10;
        pnlGrid.Attributes.Add("style", "padding-top: 2px;");

        // Ensure to raise the events
        if (RequestHelper.IsPostBack())
        {
            switch (LastAction)
            {
                case "delete":
                    RaiseDeleteFile(this, e);
                    break;

                case "update":
                    RaiseUploadFile(this, e);
                    break;
            }

            InnerAttachmentGUID = ValidationHelper.GetGuid(Value, Guid.Empty);
        }

        // Load data
        ReloadData(false);
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        if (StopProcessing)
        {
            // Do nothing
        }
        else
        {
            lblError.Visible = (lblError.Text != "");
            lblInfo.Visible = (lblInfo.Text != "");

            // Ensure uplaoder button
            plcUploader.Visible = Enabled;
            plcUploaderDisabled.Visible = !Enabled;

            // Hide actions
            gridAttachments.GridView.Columns[0].Visible = !HideActions;
            gridAttachments.GridView.Columns[1].Visible = !HideActions;
            newAttachmentElem.Visible = !HideActions && Enabled;
            plcUploaderDisabled.Visible = !HideActions && !Enabled && (attachmentGuid == Guid.Empty);

            // Ensure correct layout
            bool gridHasData = !DataHelper.DataSourceIsEmpty(gridAttachments.DataSource);
            Visible = gridHasData || !HideActions;
            pnlGrid.Visible = gridHasData;

            // Initialize button for adding attachments
            plcUploader.Visible = (attachmentGuid == Guid.Empty) || !gridHasData;

            // Dialog for editing attachment
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("function Edit_" + ClientID + "(attachmentGUID, formGUID, versionHistoryID, parentId, hash, image) { ");
            sb.AppendLine("var form = '';");
            sb.AppendLine("if (formGUID != '') { form = '&formguid=' + formGUID + '&parentid=' + parentId; }");
            sb.AppendLine(((Node != null) ? "else{form = '&siteid=' + " + Node.NodeSiteID + ";}" : ""));
            sb.AppendLine("if (image) {");
            sb.AppendLine("modalDialog('" + ResolveUrl((IsLiveSite ? "~/CMSFormControls/LiveSelectors/ImageEditor.aspx" : "~/CMSModules/Content/CMSDesk/Edit/ImageEditor.aspx") + "?attachmentGUID=' + attachmentGUID + '&refresh=1&versionHistoryID=' + versionHistoryID + form + '&clientid=" + ClientID + "&hash=' + hash") + ", 'editorDialog', 905, 670); }");
            sb.AppendLine("else {");
            sb.AppendLine("modalDialog('" + CMSContext.ResolveDialogUrl((IsLiveSite ? "~/CMSModules/Content/Attachments/CMSPages/MetaDataEditor.aspx" : "~/CMSModules/Content/Attachments/Dialogs/MetaDataEditor.aspx") + "?attachmentGUID=' + attachmentGUID + '&refresh=1&versionHistoryID=' + versionHistoryID + form + '&clientid=" + ClientID + "&hash=' + hash") + ", 'editorDialog', 500, 350); }");
            sb.AppendLine("return false; }");

            // Register script for editing attachment
            ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "AttachmentEditScripts_" + ClientID, ScriptHelper.GetScript(sb.ToString()));
        }
    }

    #endregion


    #region "Public methods"

    /// <summary>
    /// Indicates if the control contains some data.
    /// </summary>
    public override bool HasData()
    {
        return !DataHelper.DataSourceIsEmpty(dsAttachments.DataSource);
    }


    /// <summary>
    /// Clears data.
    /// </summary>
    public void Clear()
    {
        Value = Guid.Empty;
        ReloadData(true);
    }

    #endregion


    #region "Private & protected methods"

    public override void ReloadData(bool forceReload)
    {
        Visible = !StopProcessing;
        if (StopProcessing)
        {
            dsAttachments.StopProcessing = true;
            newAttachmentElem.StopProcessing = true;
            // Do nothing
        }
        else
        {
            if (Node != null)
            {
                dsAttachments.Path = Node.NodeAliasPath;
                dsAttachments.CultureCode = Node.DocumentCulture;

                SiteInfo si = SiteInfoProvider.GetSiteInfo(Node.NodeSiteID);
                SiteName = si.SiteName;
            }

            // For new culture version temporary attachments are always used
            if ((Form != null) && (Form.Mode == CMS.FormEngine.FormModeEnum.InsertNewCultureVersion) && ((LastAction == "update") || (LastAction == "insert")))
            {
                VersionHistoryID = 0;
                SiteName = null;
            }

            // Ensure correct site name
            dsAttachments.SiteName = SiteName;

            // Get attachment GUID
            attachmentGuid = ValidationHelper.GetGuid(Value, Guid.Empty);
            if (attachmentGuid == Guid.Empty)
            {
                dsAttachments.StopProcessing = true;
            }

            if ((Node == null) || UsesWorkflow)
            {
                dsAttachments.DocumentVersionHistoryID = VersionHistoryID;
            }
            dsAttachments.AttachmentFormGUID = FormGUID;
            dsAttachments.AttachmentGUID = attachmentGuid;

            // Force reload datasource
            if (forceReload)
            {
                dsAttachments.DataSource = null;
                dsAttachments.DataBind();
            }

            // Ensure right column name (for attachments under workflow)
            if (!DataHelper.DataSourceIsEmpty(dsAttachments.DataSource))
            {
                DataSet ds = (DataSet)dsAttachments.DataSource;
                if (ds != null)
                {
                    DataTable dt = (ds).Tables[0];
                    if (!dt.Columns.Contains("AttachmentFormGUID"))
                    {
                        dt.Columns.Add("AttachmentFormGUID");
                    }

                    // Get inner attachment
                    innerAttachment = new AttachmentInfo(dt.Rows[0]);
                    Value = innerAttachment.AttachmentGUID;
                    hdnAttachName.Value = innerAttachment.AttachmentName;

                    // Check if temporary attachment should be created
                    createTempAttachment = ((DocumentID == 0) && (DocumentID != innerAttachment.AttachmentDocumentID));
                }
            }

            // Initialize button for adding attachments
            newAttachmentElem.ImageUrl = ResolveUrl(GetImageUrl("Design/Controls/DirectUploader/upload_new.png"));
            newAttachmentElem.ImageWidth = 16;
            newAttachmentElem.ImageHeight = 16;
            newAttachmentElem.SourceType = MediaSourceEnum.Attachment;
            newAttachmentElem.DocumentID = DocumentID;
            newAttachmentElem.NodeParentNodeID = NodeParentNodeID;
            newAttachmentElem.NodeClassName = NodeClassName;
            newAttachmentElem.ResizeToWidth = ResizeToWidth;
            newAttachmentElem.ResizeToHeight = ResizeToHeight;
            newAttachmentElem.ResizeToMaxSideSize = ResizeToMaxSideSize;
            newAttachmentElem.FormGUID = FormGUID;
            newAttachmentElem.AttachmentGUIDColumnName = GUIDColumnName;
            newAttachmentElem.AllowedExtensions = AllowedExtensions;
            newAttachmentElem.ParentElemID = ClientID;
            newAttachmentElem.ForceLoad = true;
            newAttachmentElem.InnerDivHtml = GetString("attach.uploadfile");
            newAttachmentElem.InnerDivClass = InnerDivClass;
            newAttachmentElem.InnerLoadingDivHtml = GetString("attach.loading");
            newAttachmentElem.InnerLoadingDivClass = InnerLoadingDivClass;
            newAttachmentElem.IsLiveSite = IsLiveSite;
            newAttachmentElem.IncludeNewItemInfo = true;
            newAttachmentElem.CheckPermissions = CheckPermissions;
            newAttachmentElem.NodeSiteName = SiteName;

            imgDisabled.ImageUrl = ResolveUrl(GetImageUrl("Design/Controls/DirectUploader/upload_newdisabled.png"));

            // Bind UniGrid to DataSource
            gridAttachments.DataSource = dsAttachments.DataSource;
            gridAttachments.LoadGridDefinition();
            gridAttachments.ReloadData();
        }
    }


    /// <summary>
    /// UniGrid action buttons event handler.
    /// </summary>
    protected void GridAttachmentsOnAction(string actionName, object actionArgument)
    {
        if (Enabled && !HideActions)
        {
            // Check the permissions
            #region "Check permissions"

            if (CheckPermissions)
            {
                if (FormGUID != Guid.Empty)
                {
                    if (!RaiseOnCheckPermissions("Create", this))
                    {
                        if (!CMSContext.CurrentUser.IsAuthorizedToCreateNewDocument(NodeParentNodeID, NodeClassName))
                        {
                            lblError.Text = GetString("attach.actiondenied");
                            return;
                        }
                    }
                }
                else
                {
                    if (!RaiseOnCheckPermissions("Modify", this))
                    {
                        if (CMSContext.CurrentUser.IsAuthorizedPerDocument(Node, NodePermissionsEnum.Modify) == AuthorizationResultEnum.Denied)
                        {
                            lblError.Text = GetString("attach.actiondenied");
                            return;
                        }
                    }
                }
            }

            #endregion

            Guid attachmentGuid = Guid.Empty;

            // Get action argument (Guid or int)
            if (ValidationHelper.IsGuid(actionArgument))
            {
                attachmentGuid = ValidationHelper.GetGuid(actionArgument, Guid.Empty);
            }

            // Process proper action
            switch (actionName.ToLower())
            {
                case "delete":
                    if (!createTempAttachment)
                    {
                        if (attachmentGuid != Guid.Empty)
                        {
                            // Delete attachment
                            if (FormGUID == Guid.Empty)
                            {
                                // Ensure automatic check-in/ check-out
                                VersionManager vm = null;

                                // Check out the document
                                if (AutoCheck)
                                {
                                    vm = new VersionManager(TreeProvider);
                                    vm.CheckOut(Node, Node.IsPublished, true);
                                }

                                // If the GUID column is set, use it to process additional actions for field attachments
                                if (GUIDColumnName != null)
                                {
                                    DocumentHelper.DeleteAttachment(Node, GUIDColumnName, TreeProvider);
                                }
                                else
                                {
                                    DocumentHelper.DeleteAttachment(Node, attachmentGuid, TreeProvider);
                                }
                                DocumentHelper.UpdateDocument(Node, TreeProvider);

                                // Ensure full page refresh
                                if (AutoCheck)
                                {
                                    ScriptHelper.RegisterStartupScript(Page, typeof(Page), "deleteRefresh", ScriptHelper.GetScript("InitRefresh_" + ClientID + "('', true, true, '" + attachmentGuid + "', 'delete');"));
                                }
                                else
                                {
                                    string script = "if (window.RefreshTree) { RefreshTree(" + ActualNode.NodeParentID + ", " + ActualNode.NodeID + "); }";
                                    ScriptHelper.RegisterStartupScript(Page, typeof(Page), "refreshTree", ScriptHelper.GetScript(script));
                                }

                                // Check in the document
                                if (AutoCheck)
                                {
                                    if (vm != null)
                                    {
                                        vm.CheckIn(Node, null, null);
                                        VersionHistoryID = Node.DocumentCheckedOutVersionHistoryID;
                                    }
                                }

                                // Log synchronization task if not under workflow
                                if (!UsesWorkflow)
                                {
                                    DocumentSynchronizationHelper.LogDocumentChange(Node, TaskTypeEnum.UpdateDocument, TreeProvider);
                                }
                            }
                            else
                            {
                                AttachmentManager.DeleteTemporaryAttachment(attachmentGuid, CMSContext.CurrentSiteName);
                            }
                        }
                    }

                    LastAction = "delete";
                    Value = Guid.Empty;
                    break;
            }

            // Force reload data
            ReloadData(true);
        }
    }


    /// <summary>
    /// Overloaded ReloadData.
    /// </summary>
    public override void ReloadData()
    {
        ReloadData(false);
    }


    /// <summary>
    /// UniGrid external data bound.
    /// </summary>
    protected object GridDocsOnExternalDataBound(object sender, string sourceName, object parameter)
    {
        string attName = null;
        string attachmentExt = null;
        DataRowView drv = null;

        switch (sourceName.ToLower())
        {
            case "update":
                drv = parameter as DataRowView;
                PlaceHolder plcUpd = new PlaceHolder();
                plcUpd.ID = "plcUdateAction";
                Panel pnlBlock = new Panel();
                pnlBlock.ID = "pnlBlock";

                plcUpd.Controls.Add(pnlBlock);

                // Add disabled image
                ImageButton imgUpdate = new ImageButton();
                imgUpdate.ID = "imgUpdate";
                imgUpdate.PreRender += imgUpdate_PreRender;
                pnlBlock.Controls.Add(imgUpdate);

                // Add update control
                // Dynamically load uploader control
                DirectFileUploader dfuElem = Page.LoadControl("~/CMSModules/Content/Controls/Attachments/DirectFileUploader/DirectFileUploader.ascx") as DirectFileUploader;

                // Set uploader's properties
                if (dfuElem != null)
                {
                    dfuElem.ID = "dfuElem" + DocumentID;
                    dfuElem.SourceType = MediaSourceEnum.Attachment;
                    dfuElem.DisplayInline = true;
                    if (!createTempAttachment)
                    {
                        dfuElem.AttachmentGUID = ValidationHelper.GetGuid(drv["AttachmentGUID"], Guid.Empty);
                    }

                    dfuElem.ForceLoad = true;
                    dfuElem.FormGUID = FormGUID;
                    dfuElem.AttachmentGUIDColumnName = GUIDColumnName;
                    dfuElem.DocumentID = DocumentID;
                    dfuElem.NodeParentNodeID = NodeParentNodeID;
                    dfuElem.NodeClassName = NodeClassName;
                    dfuElem.ResizeToWidth = ResizeToWidth;
                    dfuElem.ResizeToHeight = ResizeToHeight;
                    dfuElem.ResizeToMaxSideSize = ResizeToMaxSideSize;
                    dfuElem.AllowedExtensions = AllowedExtensions;
                    dfuElem.ImageUrl = ResolveUrl(GetImageUrl("Design/Controls/DirectUploader/upload.png"));
                    dfuElem.ImageHeight = 16;
                    dfuElem.ImageWidth = 16;
                    dfuElem.InsertMode = false;
                    dfuElem.ParentElemID = ClientID;
                    dfuElem.IncludeNewItemInfo = true;
                    dfuElem.CheckPermissions = CheckPermissions;
                    dfuElem.NodeSiteName = SiteName;
                    dfuElem.IsLiveSite = this.IsLiveSite;
                    // Setting of the direct single mode
                    dfuElem.UploadMode = MultifileUploaderModeEnum.DirectSingle;
                    dfuElem.Width = 16;
                    dfuElem.Height = 16;
                    dfuElem.MaxNumberToUpload = 1;

                    dfuElem.PreRender += dfuElem_PreRender;
                    pnlBlock.Controls.Add(dfuElem);
                }

                attName = ValidationHelper.GetString(drv["AttachmentName"], string.Empty);
                attachmentExt = ValidationHelper.GetString(drv["AttachmentExtension"], string.Empty);

                int nodeGroupId = (Node != null) ? Node.GetIntegerValue("NodeGroupID") : 0;
                bool displayGroupAdmin = true;

                // Check group admin for live site
                if (IsLiveSite && (nodeGroupId > 0))
                {
                    displayGroupAdmin = CMSContext.CurrentUser.IsGroupAdministrator(nodeGroupId);
                }

                // Check if WebDAV allowed by the form
                bool allowWebDAV = (Form == null) ? true : Form.AllowWebDAV;

                // Add WebDAV edit control
                if (allowWebDAV && CMSContext.IsWebDAVEnabled(SiteName) && RequestHelper.IsWindowsAuthentication() && (FormGUID == Guid.Empty) && WebDAVSettings.IsExtensionAllowedForEditMode(attachmentExt, SiteName) && displayGroupAdmin)
                {
                    // Dynamically load control
                    WebDAVEditControl webdavElem = Page.LoadControl("~/CMSModules/WebDAV/Controls/AttachmentWebDAVEditControl.ascx") as WebDAVEditControl;

                    // Set editor's properties
                    if (webdavElem != null)
                    {
                        webdavElem.ID = "webdavElem" + DocumentID;

                        // Ensure form identification
                        if ((Form != null) && (Form.Parent != null))
                        {
                            webdavElem.FormID = Form.Parent.ClientID;
                        }
                        webdavElem.PreRender += webdavElem_PreRender;
                        webdavElem.SiteName = SiteName;
                        webdavElem.FileName = attName;
                        webdavElem.NodeAliasPath = Node.NodeAliasPath;
                        webdavElem.NodeCultureCode = Node.DocumentCulture;
                        if (FieldInfo != null)
                        {
                            webdavElem.AttachmentFieldName = FieldInfo.Name;
                        }

                        // Set Group ID for live site
                        webdavElem.GroupID = IsLiveSite ? nodeGroupId : 0;
                        webdavElem.IsLiveSite = IsLiveSite;

                        // Align left if WebDAV is enabled and windows authentication is enabled
                        bool isRTL = (IsLiveSite && CultureHelper.IsPreferredCultureRTL()) || (!IsLiveSite && CultureHelper.IsUICultureRTL());
                        pnlBlock.Style.Add("text-align", isRTL ? "right" : "left");

                        pnlBlock.Controls.Add(webdavElem);
                    }
                }

                return plcUpd;

            case "edit":
                // Get file extension
                string extension = ValidationHelper.GetString(((DataRowView)((GridViewRow)parameter).DataItem).Row["AttachmentExtension"], string.Empty).ToLower();
                // Get attachment GUID
                attachmentGuid = ValidationHelper.GetGuid(((DataRowView)((GridViewRow)parameter).DataItem).Row["AttachmentGUID"], Guid.Empty);
                if (sender is ImageButton)
                {
                    ImageButton img = (ImageButton)sender;
                    if (createTempAttachment)
                    {
                        img.Visible = false;
                    }
                    else
                    {
                        img.AlternateText = extension;
                        img.ToolTip = attachmentGuid.ToString();
                        img.PreRender += img_PreRender;
                    }
                }
                break;

            case "delete":
                if (sender is ImageButton)
                {
                    ImageButton imgDelete = (ImageButton)sender;
                    // Turn off validation
                    imgDelete.CausesValidation = false;
                    imgDelete.PreRender += imgDelete_PreRender;
                    // Explicitly initialize confirmation
                    imgDelete.OnClientClick = "if(DeleteConfirmation() == false){return false;}";
                }
                break;

            case "attachmentname":
                {
                    drv = parameter as DataRowView;
                    // Get attachment GUID
                    attachmentGuid = ValidationHelper.GetGuid(drv["AttachmentGUID"], Guid.Empty);

                    // Get attachment extension
                    attachmentExt = ValidationHelper.GetString(drv["AttachmentExtension"], string.Empty);
                    bool isImage = ImageHelper.IsImage(attachmentExt);
                    string iconUrl = GetFileIconUrl(attachmentExt, "List");

                    // Get link for attachment
                    string attachmentUrl = null;
                    attName = ValidationHelper.GetString(drv["AttachmentName"], string.Empty);
                    int documentId = DocumentID;

                    if (Node != null)
                    {
                        if (IsLiveSite && (documentId > 0))
                        {
                            attachmentUrl = CMSContext.ResolveUIUrl(TreePathUtils.GetAttachmentUrl(attachmentGuid, URLHelper.GetSafeFileName(attName, CMSContext.CurrentSiteName), 0, null));
                        }
                        else
                        {
                            attachmentUrl = CMSContext.ResolveUIUrl(TreePathUtils.GetAttachmentUrl(attachmentGuid, URLHelper.GetSafeFileName(attName, CMSContext.CurrentSiteName), VersionHistoryID, null));
                        }
                    }
                    else
                    {
                        attachmentUrl = ResolveUrl(DocumentHelper.GetAttachmentUrl(attachmentGuid, VersionHistoryID));
                    }
                    attachmentUrl = URLHelper.UpdateParameterInUrl(attachmentUrl, "chset", Guid.NewGuid().ToString());

                    // Ensure correct URL
                    if (SiteName != CMSContext.CurrentSiteName)
                    {
                        attachmentUrl = URLHelper.AddParameterToUrl(attachmentUrl, "sitename", SiteName);
                    }

                    // Add latest version requirement for live site
                    if (IsLiveSite && (documentId > 0))
                    {
                        // Add requirement for latest version of files for current document
                        string newparams = "latestfordocid=" + documentId;
                        newparams += "&hash=" + ValidationHelper.GetHashString("d" + documentId);

                        attachmentUrl += "&" + newparams;
                    }

                    // Optionally trim attachment name
                    string attachmentName = TextHelper.LimitLength(attName, ATTACHMENT_NAME_LIMIT, "...");

                    // Tooltip
                    string tooltip = null;
                    if (ShowTooltip)
                    {
                        string title = ValidationHelper.GetString(drv["AttachmentTitle"], string.Empty); ;
                        string description = ValidationHelper.GetString(drv["AttachmentDescription"], string.Empty);
                        int imageWidth = ValidationHelper.GetInteger(drv["AttachmentImageWidth"], 0);
                        int imageHeight = ValidationHelper.GetInteger(drv["AttachmentImageHeight"], 0);
                        tooltip = UIHelper.GetTooltipAttributes(attachmentUrl, imageWidth, imageHeight, title, attachmentName, attachmentExt, description, null, 300);
                    }

                    // Icon
                    string imageTag = "<img class=\"Icon\" src=\"" + iconUrl + "\" alt=\"" + attachmentName + "\" />";
                    if (isImage)
                    {
                        return "<a href=\"#\" onclick=\"javascript: window.open('" + attachmentUrl + "'); return false;\"><span " + tooltip + ">" + imageTag + attachmentName + "</span></a>";
                    }
                    else
                    {
                        return "<a href=\"" + attachmentUrl + "\"><span id=\"" + attachmentGuid + "\" " + tooltip + ">" + imageTag + attachmentName + "</span></a>";
                    }
                }

            case "attachmentsize":
                long size = ValidationHelper.GetLong(parameter, 0);
                return DataHelper.GetSizeString(size);
        }

        return parameter;
    }


    #region "Grid actions events"

    protected void webdavElem_PreRender(object sender, EventArgs e)
    {
        WebDAVEditControl webdavElem = sender as WebDAVEditControl;
        if (webdavElem != null)
        {
            webdavElem.Enabled = Enabled;
        }
    }


    protected void dfuElem_PreRender(object sender, EventArgs e)
    {
        DirectFileUploader dfuElem = (DirectFileUploader)sender;
        if (Enabled)
        {
            dfuElem.ForceLoad = true;
            dfuElem.FormGUID = FormGUID;
            dfuElem.AttachmentGUIDColumnName = GUIDColumnName;
            dfuElem.DocumentID = DocumentID;
            dfuElem.NodeParentNodeID = NodeParentNodeID;
            dfuElem.NodeClassName = NodeClassName;
            dfuElem.ResizeToWidth = ResizeToWidth;
            dfuElem.ResizeToHeight = ResizeToHeight;
            dfuElem.ResizeToMaxSideSize = ResizeToMaxSideSize;
            dfuElem.AllowedExtensions = AllowedExtensions;
            dfuElem.ImageUrl = ResolveUrl(GetImageUrl("Design/Controls/DirectUploader/upload.png"));
            dfuElem.ImageHeight = 16;
            dfuElem.ImageWidth = 16;
            dfuElem.InsertMode = false;
            dfuElem.ParentElemID = ClientID;
            dfuElem.IncludeNewItemInfo = true;
            dfuElem.CheckPermissions = CheckPermissions;
            dfuElem.NodeSiteName = SiteName;
            dfuElem.IsLiveSite = this.IsLiveSite;
            // Setting of the direct single mode
            dfuElem.UploadMode = MultifileUploaderModeEnum.DirectSingle;
            dfuElem.Width = 16;
            dfuElem.Height = 16;
            dfuElem.MaxNumberToUpload = 1;
        }
        else
        {
            dfuElem.Visible = false;
        }
    }


    protected void imgUpdate_PreRender(object sender, EventArgs e)
    {
        ImageButton imgUpdate = (ImageButton)sender;
        if (!Enabled)
        {
            imgUpdate.ImageUrl = ResolveUrl(GetImageUrl("Design/Controls/DirectUploader/uploaddisabled.png"));
            imgUpdate.Style.Add("cursor", "default");
            imgUpdate.CssClass = "InlineIcon";
            imgUpdate.Enabled = false;
        }
        else
        {
            imgUpdate.Visible = false;
        }
    }


    protected void img_PreRender(object sender, EventArgs e)
    {
        ImageButton img = (ImageButton)sender;

        if (CMSContext.CurrentUser.IsAuthenticated())
        {
            if (!Enabled)
            {
                // Disable edit icon
                img.ImageUrl = GetImageUrl("Design/Controls/UniGrid/Actions/editdisabled.png");
                img.Enabled = false;
                img.Style.Add("cursor", "default");
            }
            else
            {
                string strForm = (FormGUID == Guid.Empty) ? "" : FormGUID.ToString();

                // Create security hash
                string form = null;
                if (!String.IsNullOrEmpty(strForm))
                {
                    form = "&formguid=" + strForm + "&parentid=" + NodeParentNodeID;
                }
                else if (Node != null)
                {
                    form += "&siteid=" + Node.NodeSiteID;
                }
                string parameters = "?attachmentGUID=" + img.ToolTip + "&refresh=1&versionHistoryID=" + VersionHistoryID + form + "&clientid=" + ClientID;
                string validationHash = QueryHelper.GetHash(parameters);

                string isImage = ImageHelper.IsSupportedByImageEditor(img.AlternateText) ? "true" : "false";

                img.Attributes.Add("onclick", "Edit_" + ClientID + "('" + img.ToolTip + "', '" + strForm + "', '" + VersionHistoryID + "', " + NodeParentNodeID + ", '" + validationHash + "', " + isImage + ");return false;");
            }

            img.AlternateText = GetString("general.edit");
            img.ToolTip = "";
        }
        else
        {
            img.Visible = false;
        }
    }


    void imgDelete_PreRender(object sender, EventArgs e)
    {
        ImageButton imgDelete = (ImageButton)sender;
        if (!Enabled || !AllowDelete)
        {
            // Disable delete icon in case that editing is not allowed
            imgDelete.ImageUrl = GetImageUrl("Design/Controls/UniGrid/Actions/deletedisabled.png");
            imgDelete.Enabled = false;
            imgDelete.Style.Add("cursor", "default");
        }
    }

    #endregion

    #endregion
}
