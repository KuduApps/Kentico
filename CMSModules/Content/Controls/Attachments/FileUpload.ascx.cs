using System;
using System.Web;
using System.Web.UI;

using CMS.GlobalHelper;
using CMS.ExtendedControls;
using CMS.TreeEngine;
using CMS.WorkflowEngine;
using CMS.CMSHelper;
using CMS.SettingsProvider;
using CMS.LicenseProvider;
using CMS.FormEngine;
using CMS.UIControls;
using CMS.SiteProvider;
using CMS.IO;

public partial class CMSModules_Content_Controls_Attachments_FileUpload : FileUpload
{
    #region "Properties"

    /// <summary>
    /// File upload user control.
    /// </summary>
    public override CMSFileUpload FileUploadControl
    {
        get
        {
            return ucFileUpload;
        }
    }


    /// <summary>
    /// Uploaded file.
    /// </summary>
    public override HttpPostedFile PostedFile
    {
        get
        {
            return ucFileUpload.PostedFile;
        }
    }

    #endregion


    #region "Button handling"

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        lblError.Visible = (lblError.Text != "");
    }

    protected void btnOK_Click(object senderObject, EventArgs e)
    {
        if (!ucFileUpload.HasFile)
        {
            lblError.Text = GetString("attach.selectfile");
        }
        else
        {
            switch (this.SourceType)
            {
                case MediaSourceEnum.Attachment:
                    HandleAttachmentUpload(true);
                    break;

                case MediaSourceEnum.DocumentAttachments:
                    HandleAttachmentUpload(false);
                    break;

                case MediaSourceEnum.Content:
                    HandleContentUpload();
                    break;

                case MediaSourceEnum.PhysicalFile:
                    HandlePhysicalFilesUpload();
                    break;

                default:
                    break;
            }
        }
    }


    /// <summary>
    /// Provides operations necessary to create and store new attachment.
    /// </summary>
    private void HandleAttachmentUpload(bool fieldAttachment)
    {
        TreeProvider tree = null;
        TreeNode node = null;

        // New attachment
        AttachmentInfo newAttachment = null;

        string message = string.Empty;
        bool fullRefresh = false;

        try
        {
            // Get the existing document
            if (DocumentID != 0)
            {
                // Get document
                tree = new TreeProvider(CMSContext.CurrentUser);
                node = DocumentHelper.GetDocument(DocumentID, tree);
                if (node == null)
                {
                    throw new Exception("Given document doesn't exist!");
                }
            }

            #region "Check permissions"

            if (this.CheckPermissions)
            {
                CheckNodePermissions(node);
            }

            #endregion

            // Check the allowed extensions
            CheckAllowedExtensions();

            // Standard attachments
            if (DocumentID != 0)
            {
                // Ensure automatic check-in/check-out
                bool useWorkflow = false;
                bool autoCheck = false;
                WorkflowManager workflowMan = new WorkflowManager(tree);
                VersionManager vm = null;

                // Get workflow info
                WorkflowInfo wi = workflowMan.GetNodeWorkflow(node);

                // Check if the document uses workflow
                if (wi != null)
                {
                    useWorkflow = true;
                    autoCheck = !wi.UseCheckInCheckOut(CMSContext.CurrentSiteName);
                }

                // Check out the document
                if (autoCheck)
                {
                    // Get original step Id
                    int originalStepId = node.DocumentWorkflowStepID;

                    // Get current step info
                    WorkflowStepInfo si = workflowMan.GetStepInfo(node);
                    if (si != null)
                    {
                        // Decide if full refresh is needed
                        bool automaticPublish = wi.WorkflowAutoPublishChanges;
                        string stepName = si.StepName.ToLower();
                        // Document is published or archived or uses automatic publish or step is different than original (document was updated)
                        fullRefresh = (stepName == "published") || (stepName == "archived") || (automaticPublish && (stepName != "published")) || (originalStepId != node.DocumentWorkflowStepID);
                    }

                    vm = new VersionManager(tree);
                    vm.CheckOut(node, node.IsPublished, true);
                }

                // Handle field attachment
                if (fieldAttachment)
                {
                    newAttachment = DocumentHelper.AddAttachment(node, AttachmentGUIDColumnName, Guid.Empty, Guid.Empty, ucFileUpload.PostedFile, tree, ResizeToWidth, ResizeToHeight, ResizeToMaxSideSize);
                    // Update attachment field
                    DocumentHelper.UpdateDocument(node, tree);
                }
                // Handle grouped and unsorted attachments
                else
                {
                    // Grouped attachment
                    if (AttachmentGroupGUID != Guid.Empty)
                    {
                        newAttachment = DocumentHelper.AddGroupedAttachment(node, AttachmentGUID, AttachmentGroupGUID, ucFileUpload.PostedFile, tree, ResizeToWidth, ResizeToHeight, ResizeToMaxSideSize);
                    }
                    // Unsorted attachment
                    else
                    {
                        newAttachment = DocumentHelper.AddUnsortedAttachment(node, AttachmentGUID, ucFileUpload.PostedFile, tree, ResizeToWidth, ResizeToHeight, ResizeToMaxSideSize);
                    }
                }

                // Check in the document
                if (autoCheck)
                {
                    if (vm != null)
                    {
                        vm.CheckIn(node, null, null);
                    }
                }

                // Log synchronization task if not under workflow
                if (!useWorkflow)
                {
                    DocumentSynchronizationHelper.LogDocumentChange(node, TaskTypeEnum.UpdateDocument, tree);
                }
            }

            // Temporary attachments
            if (FormGUID != Guid.Empty)
            {
                newAttachment = AttachmentManager.AddTemporaryAttachment(FormGUID, AttachmentGUIDColumnName, AttachmentGUID, AttachmentGroupGUID, ucFileUpload.PostedFile, CMSContext.CurrentSiteID, ResizeToWidth, ResizeToHeight, ResizeToMaxSideSize);
            }

            // Ensure properties update
            if ((newAttachment != null) && !InsertMode)
            {
                AttachmentGUID = newAttachment.AttachmentGUID;
            }

            if (newAttachment == null)
            {
                throw new Exception("The attachment hasn't been created since no DocumentID or FormGUID was supplied.");
            }
        }
        catch (Exception ex)
        {
            message = ex.Message;
            lblError.Text = message;
        }
        finally
        {
            // Call aftersave javascript if exists
            if ((string.IsNullOrEmpty(message)) && (newAttachment != null))
            {
                string closeString = "window.close();";
                // Register wopener script
                ScriptHelper.RegisterWOpenerScript(Page);

                if (!String.IsNullOrEmpty(AfterSaveJavascript))
                {
                    string url = null;
                    string saveName = URLHelper.GetSafeFileName(newAttachment.AttachmentName, CMSContext.CurrentSiteName);
                    if (node != null)
                    {
                        SiteInfo si = SiteInfoProvider.GetSiteInfo(node.NodeSiteID);
                        if (si != null)
                        {
                            bool usePermanent = SettingsKeyProvider.GetBoolValue(si.SiteName + ".CMSUsePermanentURLs");
                            if (usePermanent)
                            {
                                url = ResolveUrl(AttachmentManager.GetAttachmentUrl(newAttachment.AttachmentGUID, saveName));
                            }
                            else
                            {
                                url = ResolveUrl(AttachmentManager.GetAttachmentUrl(saveName, node.NodeAliasPath));
                            }
                        }
                    }
                    else
                    {
                        url = ResolveUrl(AttachmentManager.GetAttachmentUrl(newAttachment.AttachmentGUID, saveName));
                    }
                    // Calling javascript function with parameters attachments url, name, width, height
                    string jsParams = "('" + url + "', '" + newAttachment.AttachmentName + "', '" + newAttachment.AttachmentImageWidth + "', '" + newAttachment.AttachmentImageHeight + "');";
                    string script = "if ((wopener.parent != null) && (wopener.parent." + AfterSaveJavascript + " != null)){wopener.parent." + AfterSaveJavascript + jsParams + "}";
                    script += "else if (wopener." + AfterSaveJavascript + " != null){wopener." + AfterSaveJavascript + jsParams + "}";

                    ScriptHelper.RegisterStartupScript(this.Page, typeof(Page), "refreshAfterSave", ScriptHelper.GetScript(script + closeString));
                }

                // Create attachment info string
                string attachmentInfo = "";
                if ((newAttachment != null) && (newAttachment.AttachmentGUID != Guid.Empty) && (this.IncludeNewItemInfo))
                {
                    attachmentInfo = newAttachment.AttachmentGUID.ToString();
                }

                // Ensure message text
                message = HTMLHelper.EnsureLineEnding(message, " ");

                // Call function to refresh parent window
                ScriptHelper.RegisterStartupScript(this.Page, typeof(Page), "refresh", ScriptHelper.GetScript("if ((wopener.parent != null) && (wopener.parent.InitRefresh_" + ParentElemID + " != null)){wopener.parent.InitRefresh_" + ParentElemID + "(" + ScriptHelper.GetString(message.Trim()) + ", " + (fullRefresh ? "true" : "false") + ", false" + ((attachmentInfo != "") ? ", '" + attachmentInfo + "'" : "") + (InsertMode ? ", 'insert'" : ", 'update'") + ");}" + closeString));
            }
        }
    }


    /// <summary>
    /// Provides operations necessary to create and store new content file.
    /// </summary>
    private void HandleContentUpload()
    {
        TreeNode node = null;
        TreeProvider tree = null;

        string message = string.Empty;
        bool newDocumentCreated = false;

        try
        {
            if (this.NodeParentNodeID == 0)
            {
                throw new Exception(GetString("dialogs.document.parentmissing"));
            }

            // Check license limitations
            if (!LicenseHelper.LicenseVersionCheck(URLHelper.GetCurrentDomain(), FeatureEnum.Documents, VersionActionEnum.Insert))
            {
                throw new Exception(GetString("cmsdesk.documentslicenselimits"));
            }

            // Check if class exists
            DataClassInfo ci = DataClassInfoProvider.GetDataClass("CMS.File");
            if (ci == null)
            {
                throw new Exception("Class 'CMS.File' not found.");
            }

            #region "Check permissions"

            // Get the node
            tree = new TreeProvider(CMSContext.CurrentUser);
            TreeNode parentNode = tree.SelectSingleNode(this.NodeParentNodeID, TreeProvider.ALL_CULTURES);
            if (parentNode != null)
            {
                if (!DataClassInfoProvider.IsChildClassAllowed(ValidationHelper.GetInteger(parentNode.GetValue("NodeClassID"), 0), ci.ClassID))
                {
                    throw new Exception(GetString("Content.ChildClassNotAllowed"));
                }
            }

            // Check user permissions
            if (!RaiseOnCheckPermissions("Create", this))
            {
                if (!CMSContext.CurrentUser.IsAuthorizedToCreateNewDocument(NodeParentNodeID, NodeClassName))
                {
                    throw new Exception("You aren not allowed to create document of type '" + NodeClassName + "' in required location.");
                }
            }

            #endregion

            // Check the allowed extensions
            CheckAllowedExtensions();

            // Create new document
            string fileName = Path.GetFileNameWithoutExtension(ucFileUpload.FileName);

            node = TreeNode.New("CMS.File", tree);
            node.DocumentCulture = CMSContext.PreferredCultureCode;
            node.DocumentName = fileName;

            // Load default values
            FormHelper.LoadDefaultValues(node);

            node.SetValue("FileDescription", "");
            node.SetValue("FileName", fileName);
            node.SetValue("FileAttachment", Guid.Empty);

            // Set default template ID                
            node.DocumentPageTemplateID = ci.ClassDefaultPageTemplateID;

            // Insert the document
            DocumentHelper.InsertDocument(node, parentNode, tree);

            newDocumentCreated = true;

            // Add the file
            DocumentHelper.AddAttachment(node, "FileAttachment", ucFileUpload.PostedFile, tree, this.ResizeToWidth, this.ResizeToHeight, this.ResizeToMaxSideSize);

            // Create default SKU if configured
            if (ModuleEntry.CheckModuleLicense(ModuleEntry.ECOMMERCE, URLHelper.GetCurrentDomain(), FeatureEnum.Ecommerce, VersionActionEnum.Insert))
            {
                node.CreateDefaultSKU();
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
        }
        catch (Exception ex)
        {
            // Delete the document if something failed
            if (newDocumentCreated && (node != null) && (node.DocumentID > 0))
            {
                DocumentHelper.DeleteDocument(node, tree, false, true, true);
            }

            message = ex.Message;
            lblError.Text = message;
        }
        finally
        {
            if (string.IsNullOrEmpty(message))
            {
                // Create node info string
                string nodeInfo = "";
                if ((node != null) && (node.NodeID > 0) && (this.IncludeNewItemInfo))
                {
                    nodeInfo = node.NodeID.ToString();
                }

                // Ensure message text
                message = HTMLHelper.EnsureLineEnding(message, " ");

                // Register wopener script
                ScriptHelper.RegisterWOpenerScript(Page);

                // Call function to refresh parent window                                                     
                ScriptHelper.RegisterStartupScript(this.Page, typeof(Page), "refresh", ScriptHelper.GetScript("if ((wopener.parent != null) && (wopener.parent.InitRefresh_" + ParentElemID + " != null)){wopener.parent.InitRefresh_" + ParentElemID + "(" + ScriptHelper.GetString(message.Trim()) + ", false, false" + ((nodeInfo != "") ? ", '" + nodeInfo + "'" : "") + (InsertMode ? ", 'insert'" : ", 'update'") + ");}window.close();"));
            }
        }
    }


    /// <summary>
    /// Check permissions.
    /// </summary>
    /// <param name="node">Tree node</param>
    private void CheckNodePermissions(TreeNode node)
    {
        // For new document
        if (FormGUID != Guid.Empty)
        {
            if (NodeParentNodeID == 0)
            {
                throw new Exception(GetString("attach.document.parentmissing"));
            }

            if (!RaiseOnCheckPermissions("Create", this))
            {
                if (!CMSContext.CurrentUser.IsAuthorizedToCreateNewDocument(NodeParentNodeID, NodeClassName))
                {
                    throw new Exception(GetString("attach.actiondenied"));
                }
            }
        }
        // For existing document
        else if (DocumentID > 0)
        {
            if (!RaiseOnCheckPermissions("Modify", this))
            {
                if (CMSContext.CurrentUser.IsAuthorizedPerDocument(node, NodePermissionsEnum.Modify) == AuthorizationResultEnum.Denied)
                {
                    throw new Exception(GetString("attach.actiondenied"));
                }
            }
        }
    }


    /// <summary>
    /// Provides operations necessary to create and store new physical file.
    /// </summary>
    private void HandlePhysicalFilesUpload()
    {
        string message = string.Empty;

        try
        {
            // Check the allowed extensions
            CheckAllowedExtensions();

            // Create new document
            string fileName = Path.GetFileName(ucFileUpload.FileName);
            if (String.IsNullOrEmpty(TargetFolderPath))
            {
                TargetFolderPath = "~/";
            }

            string filePath = TargetFolderPath;

            // Try to map virtual and relative path to server
            try
            {
                if (!Path.IsPathRooted(filePath))
                {
                    filePath = Server.MapPath(filePath);
                }
            }
            catch
            {
            }

            // Upload file
            //ucFileUpload.SaveAs();
            File.WriteAllBytes(DirectoryHelper.CombinePath(filePath, fileName), ucFileUpload.FileBytes);
        }
        catch (Exception ex)
        {
            message = ex.Message;

        }
        finally
        {
            if (!String.IsNullOrEmpty(message))
            {
                lblError.Text = message;
            }
            else
            {
                // Register wopener script
                ScriptHelper.RegisterWOpenerScript(Page);

                string script = "if (wopener." + AfterSaveJavascript + " != null){wopener." + AfterSaveJavascript + "()}";
                script += "else if ((wopener.parent != null) && (wopener.parent." + AfterSaveJavascript + " != null)){wopener.parent." + AfterSaveJavascript + "()}";

                // Register script
                ScriptHelper.RegisterStartupScript(this.Page, typeof(Page), "refresh", ScriptHelper.GetScript(script + "window.close();"));
            }
        }
    }

    #endregion
}
