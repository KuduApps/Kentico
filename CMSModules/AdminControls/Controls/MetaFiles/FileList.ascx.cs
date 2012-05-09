using System;
using System.Data;
using System.Web.UI.WebControls;
using System.ComponentModel;

using CMS.GlobalHelper;
using CMS.SiteProvider;
using CMS.CMSHelper;
using CMS.UIControls;
using CMS.SettingsProvider;
using CMS.ExtendedControls;

public partial class CMSModules_AdminControls_Controls_MetaFiles_FileList : CMSUserControl
{
    #region "Variables"

    protected string colActions = "";
    protected string colFileName = "";
    protected string colFileSize = "";
    protected string colURL = "";

    protected string btnDeleteImageUrl = "";
    protected string btnDeleteToolTip = "";

    protected string btnPasteImageUrl = "";
    protected string btnPasteToolTip = "";

    protected string btnImageEditorImageUrl = "";
    protected string btnImageEditorToolTip = "";

    private bool mCheckObjectPermissions = true;

    #endregion


    #region "Public Properties"

    /// <summary>
    /// Object id.
    /// </summary>
    public int ObjectID
    {
        get
        {
            return ValidationHelper.GetInteger(ViewState["ObjectID"], 0);
        }
        set
        {
            ViewState["ObjectID"] = value;
        }
    }


    /// <summary>
    /// Object type.
    /// </summary>
    public string ObjectType
    {
        get
        {
            return ValidationHelper.GetString(ViewState["ObjectType"], "");
        }
        set
        {
            ViewState["ObjectType"] = value;
        }
    }


    /// <summary>
    /// Site id of uploaded files.
    /// </summary>
    public int SiteID
    {
        get
        {
            return ValidationHelper.GetInteger(ViewState["SiteID"], 0);
        }
        set
        {
            ViewState["SiteID"] = value;
        }
    }


    /// <summary>
    /// Attachment category/group
    /// </summary>
    public string Category
    {
        get
        {
            return ValidationHelper.GetString(ViewState["Category"], "");
        }
        set
        {
            ViewState["Category"] = value;
        }
    }


    /// <summary>
    /// Where condition.
    /// </summary>
    public string Where
    {
        get
        {
            return ValidationHelper.GetString(ViewState["Where"], "");
        }
        set
        {
            ViewState["Where"] = value;
        }
    }


    /// <summary>
    /// Order by.
    /// </summary>
    public string OrderBy
    {
        get
        {
            return ValidationHelper.GetString(ViewState["OrderBy"], "MetaFileName");
        }
        set
        {
            ViewState["OrderBy"] = value;
        }
    }


    /// <summary>
    /// Allows pasting file URLs into editable areas.
    /// </summary>
    public bool AllowPasteAttachments
    {
        get
        {
            return ValidationHelper.GetBoolean(ViewState["AllowPasteAttachments"], false);
        }
        set
        {
            ViewState["AllowPasteAttachments"] = value;
        }
    }


    /// <summary>
    /// Allow edit flag.
    /// </summary>
    public bool AllowEdit
    {
        get
        {
            return ValidationHelper.GetBoolean(ViewState["AllowEdit"], true);
        }
        set
        {
            ViewState["AllowEdit"] = value;
        }
    }


    /// <summary>
    /// Allow modify flag.
    /// </summary>
    protected bool AllowModify
    {
        get
        {
            return (this.AllowEdit && (!CheckObjectPermissions || UserInfoProvider.IsAuthorizedPerObject(this.ObjectType, this.ObjectID, PermissionsEnum.Modify, CMSContext.CurrentSiteName, CMSContext.CurrentUser)));
        }
    }


    /// <summary>
    /// Indicates whether object permissions should be checked. It is TRUE by default.
    /// </summary>
    public bool CheckObjectPermissions
    {
        get
        {
            return mCheckObjectPermissions;
        }
        set
        {
            mCheckObjectPermissions = value;
        }
    }


    /// <summary>
    /// Gets or sets javascript for upload button.
    /// </summary>
    public string UploadOnClickScript
    {
        get
        {
            return btnUpload.OnClientClick;
        }
        set
        {
            btnUpload.OnClientClick = value;
        }
    }


    /// <summary>
    /// Currently handled (uploaded or being deleted) meta file.
    /// </summary>
    public MetaFileInfo CurrentlyHandledMetaFile
    {
        get;
        set;
    }


    /// <summary>
    /// Indicates if uploader is enabled.
    /// </summary>
    public bool Enabled
    {
        get
        {
            return uploader.Enabled;
        }
        set
        {
            uploader.Enabled = value;
            plcUploader.Visible = value;
            plcUploaderDisabled.Visible = !value;
        }
    }

    #endregion


    #region "Events"

    /// <summary>
    /// Event fired before upload processing.
    /// </summary>
    public event CancelEventHandler OnBeforeUpload;


    /// <summary>
    /// Event fired after upload processing.
    /// </summary>
    public event EventHandler OnAfterUpload;


    /// <summary>
    /// Event fired before delete processing.
    /// </summary>
    public event CancelEventHandler OnBeforeDelete;


    /// <summary>
    /// Event fired after delete processing.
    /// </summary>
    public event EventHandler OnAfterDelete;

    #endregion


    #region "Page methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.StopProcessing)
        {
            gridFiles.StopProcessing = StopProcessing;
            return;
        }
        else
        {
            gridFiles.Visible = true;
        }

        RegisterScripts();

        btnDeleteImageUrl = GetImageUrl("Design/Controls/UniGrid/Actions/Delete.png");
        btnPasteImageUrl = GetImageUrl("Design/Controls/UniGrid/Actions/pasteimg.png");
        btnImageEditorImageUrl = GetImageUrl("Design/Controls/UniGrid/Actions/Edit.png");
        imgDisabled.ImageUrl = GetImageUrl("Design/Controls/DirectUploader/upload_newdisabled.png");

        btnDeleteToolTip = GetString("general.delete");
        btnPasteToolTip = GetString("checkoutprocess.btnpastetooltip");
        btnImageEditorToolTip = GetString("general.editimage");

        colActions = GetString("general.action");
        colFileName = GetString("general.filename");
        colFileSize = GetString("filelist.unigrid.colfilesize");
        colURL = GetString("filelist.unigrid.colurl");

        ControlsHelper.RegisterPostbackControl(btnUpload);
        btnUpload.Text = GetString("filelist.btnupload");
        btnUpload.Enabled = AllowModify;

        gridFiles.OnAction += gridFiles_OnAction;
        gridFiles.OnExternalDataBound += gridFiles_OnExternalDataBound;
        gridFiles.OnBeforeDataReload += gridFiles_OnBeforeDataReload;
        gridFiles.IsLiveSite = IsLiveSite;
        gridFiles.WhereCondition = MetaFileInfoProvider.GetWhereCondition(ObjectID, ObjectType, Category, Where);
        gridFiles.OrderBy = OrderBy;
    }


    protected override void OnPreRender(EventArgs e)
    {
        if (AllowModify)
        {
            if (ObjectID > 0)
            {
                // Initialize button for adding metafile
                newMetafileElem.ImageUrl = ResolveUrl(GetImageUrl("Design/Controls/DirectUploader/upload_new.png"));
                newMetafileElem.ImageWidth = 16;
                newMetafileElem.ImageHeight = 16;
                newMetafileElem.ObjectID = ObjectID;
                newMetafileElem.ObjectType = ObjectType;
                newMetafileElem.Category = Category;
                newMetafileElem.SiteID = SiteID;
                newMetafileElem.ParentElemID = ClientID;
                newMetafileElem.ForceLoad = true;
                newMetafileElem.InnerDivHtml = GetString("attach.newattachment");
                newMetafileElem.InnerDivClass = "NewAttachment";
                newMetafileElem.InnerLoadingDivHtml = GetString("attach.loading");
                newMetafileElem.InnerLoadingDivClass = "NewAttachmentLoading";
                newMetafileElem.IsLiveSite = IsLiveSite;
                newMetafileElem.SourceType = MediaSourceEnum.MetaFile;

                plcDirectUploder.Visible = Enabled;
                plcUploaderDisabled.Visible = !Enabled;
                plcUploader.Visible = false;
            }
            else
            {
                plcDirectUploder.Visible = false;
                plcUploaderDisabled.Visible = false;
                plcUploader.Visible = true;
            }
        }
        else
        {
            plcDirectUploder.Visible = false;
            plcUploaderDisabled.Visible = true;
            plcUploader.Visible = false;
        }

        // Hide grid placeholder if unigird is hidden
        plcGridFiles.Visible = gridFiles.Visible;

        base.OnPreRender(e);
    }


    /// <summary>
    /// BtnUpload click event handler.
    /// </summary>
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        if (!AllowModify || (uploader.PostedFile == null) || (uploader.PostedFile.FileName.Trim() == ""))
        {
            return;
        }

        try
        {
            // Fire before upload event
            CancelEventArgs beforeUploadArgs = new CancelEventArgs();
            if (OnBeforeUpload != null)
            {
                OnBeforeUpload(this, beforeUploadArgs);
            }

            // If upload was not cancelled
            if (!beforeUploadArgs.Cancel)
            {
                // Create new meta file
                MetaFileInfo mfi = new MetaFileInfo(uploader.PostedFile, ObjectID, ObjectType, Category)
                {
                    MetaFileSiteID = SiteID
                };

                // Save meta file
                MetaFileInfoProvider.SetMetaFileInfo(mfi);

                // Set currently handled meta file
                CurrentlyHandledMetaFile = mfi;

                // Fire after upload event
                if (OnAfterUpload != null)
                {
                    OnAfterUpload(this, EventArgs.Empty);
                }

                // Reload grid data
                gridFiles.ReloadData();
            }
        }
        catch (Exception ex)
        {
            lblError.Visible = true;
            lblError.Text = ex.Message;
        }
    }

    protected void hdnPostback_Click(object sender, EventArgs e)
    {
        try
        {
            int fileId = ValidationHelper.GetInteger(hdnField.Value, 0);
            if (fileId > 0)
            {
                // Get uploaded meta file
                MetaFileInfo mfi = MetaFileInfoProvider.GetMetaFileInfo(fileId);

                // Set currently handled meta file
                CurrentlyHandledMetaFile = mfi;

                // Fire after upload event
                if (OnAfterUpload != null)
                {
                    OnAfterUpload(this, EventArgs.Empty);
                }

                // Reload grid data
                gridFiles.ReloadData();
            }
        }
        catch (Exception ex)
        {
            lblError.Visible = true;
            lblError.Text = ex.Message;
        }
    }

    #endregion


    #region "Private methods"

    private void RegisterScripts()
    {
        // Register tool tip script
        ScriptHelper.RegisterTooltip(Page);

        // Register dialog script for Image Editor
        ScriptHelper.RegisterDialogScript(Page);
        ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "OpenImageEditor",
            ScriptHelper.GetScript(String.Format(@"
function OpenImageEditor(query) {{ 
    modalDialog('{0}/CMSModules/Content/CMSDesk/Edit/ImageEditor.aspx' + query, 'EditImage', 905, 670); 
    return false; 
}}", URLHelper.GetFullApplicationUrl())));
        ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "OpenEditor",
           ScriptHelper.GetScript(String.Format(@"
function OpenEditor(query) {{ 
    modalDialog('{0}/CMSModules/AdminControls/Controls/MetaFiles/MetaDataEditor.aspx' + query, 'EditMetadata', 500, 350); 
    return false; 
}} ", URLHelper.GetFullApplicationUrl())));

        // Register javascript 'postback' function
        ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "PostBack", ScriptHelper.GetScript(String.Format(@"
function UpdatePage(){{ 
    {0}; 
}}", Page.ClientScript.GetPostBackEventReference(hdnPostback, ""))));

        // Refresh script
        string script = String.Format(@"
function InitRefresh_{0}(msg, fullRefresh, action, fileId)
{{
    if((msg != null) && (msg != '')){{ 
        alert(msg); action='error'; 
    }}
    var hidden = document.getElementById('{1}');
    if (hidden) {{
        hidden.value = fileId;
    }}
    if(fullRefresh){{
        {2}
    }}
    else {{
        {3}
    }}
}}
function ConfirmDelete() {{
    return confirm({4});;
}}
 ",
            ClientID,
            hdnField.ClientID,
            ControlsHelper.GetPostBackEventReference(hdnFullPostback, ""),
            ControlsHelper.GetPostBackEventReference(hdnPostback, ""),
            ScriptHelper.GetString(GetString("general.confirmdelete")));

        ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "MetafileScripts_" + ClientID, ScriptHelper.GetScript(script));
    }

    #endregion


    #region "UniGrid Events"

    protected object gridFiles_OnExternalDataBound(object sender, string sourceName, object parameter)
    {
        GridViewRow gvr = null;
        DataRowView drv = null;
        string fileGuid = null;

        switch (sourceName.ToLower())
        {
            case "edit":
                if (sender is ImageButton)
                {
                    gvr = (GridViewRow)parameter;
                    drv = (DataRowView)gvr.DataItem;

                    fileGuid = ValidationHelper.GetString(drv["MetaFileGUID"], "");
                    string fileExtension = ValidationHelper.GetString(drv["MetaFileExtension"], "");

                    // Initialize properties
                    ImageButton btnImageEditor = (ImageButton)sender;
                    btnImageEditor.Visible = true;

                    // Display button only if 'Modify' is allowed
                    if (AllowModify)
                    {
                        string query = String.Format("?metafileguid={0}&clientid={1}", fileGuid, ClientID);
                        query = URLHelper.AddUrlParameter(query, "hash", QueryHelper.GetHash(query));

                        // Display button only if metafile is in supported image format
                        if (ImageHelper.IsSupportedByImageEditor(fileExtension))
                        {
                            // Initialize button with script
                            btnImageEditor.Attributes.Add("onclick", String.Format("OpenImageEditor({0}); return false;", ScriptHelper.GetString(query)));
                        }
                        // Non-image metafile
                        else
                        {
                            // Initialize button with script
                            btnImageEditor.Attributes.Add("onclick", String.Format("OpenEditor({0}); return false;", ScriptHelper.GetString(query)));
                        }
                    }
                    else
                    {
                        btnImageEditor.ImageUrl = GetImageUrl("Design/Controls/UniGrid/Actions/editdisabled.png");
                        btnImageEditor.Enabled = false;
                    }
                }
                break;

            case "paste":
                if (sender is ImageButton)
                {
                    gvr = (GridViewRow)parameter;
                    drv = (DataRowView)gvr.DataItem;

                    fileGuid = ValidationHelper.GetString(drv["MetaFileGUID"], "");
                    int fileWidth = ValidationHelper.GetInteger(drv["MetaFileImageWidth"], 0);
                    int fileHeight = ValidationHelper.GetInteger(drv["MetaFileImageHeight"], 0);

                    ImageButton btnPaste = (ImageButton)sender;
                    btnPaste.Visible = AllowPasteAttachments;
                    if (AllowPasteAttachments)
                    {
                        if ((fileWidth > 0) && (fileHeight > 0))
                        {
                            string appPath = URLHelper.ApplicationPath;
                            if ((appPath == null) || (appPath == "/"))
                            {
                                appPath = String.Empty;
                            }
                            btnPaste.OnClientClick = String.Format("PasteImage('{0}/CMSPages/GetMetaFile.aspx?fileguid={1}'); return false", appPath, fileGuid);
                        }
                        else
                        {
                            btnPaste.Visible = false;
                        }
                    }
                    else
                    {
                        btnPaste.Visible = false;
                    }
                }
                break;

            case "delete":
                if (sender is ImageButton)
                {
                    ImageButton btnDelete = ((ImageButton)sender);
                    btnDelete.Visible = AllowModify;
                    if (!AllowModify)
                    {
                        btnDelete.ImageUrl = GetImageUrl("Design/Controls/UniGrid/Actions/deletedisabled.png");
                    }
                }
                break;

            case "name":
                drv = (DataRowView)parameter;

                string fileName = ValidationHelper.GetString(DataHelper.GetDataRowViewValue(drv, "MetaFileName"), string.Empty);
                fileGuid = ValidationHelper.GetString(DataHelper.GetDataRowViewValue(drv, "MetaFileGUID"), string.Empty);
                string fileExt = ValidationHelper.GetString(DataHelper.GetDataRowViewValue(drv, "MetaFileExtension"), string.Empty);
                string iconUrl = GetFileIconUrl(fileExt, "List");

                bool isImage = ImageHelper.IsImage(fileExt);
                string fileUrl = String.Format("{0}?fileguid={1}&chset={2}", URLHelper.GetAbsoluteUrl("~/CMSPages/GetMetaFile.aspx"), fileGuid, Guid.NewGuid());

                // Tooltip
                string title = ValidationHelper.GetString(DataHelper.GetDataRowViewValue(drv, "MetaFileTitle"), string.Empty); ;
                string description = ValidationHelper.GetString(DataHelper.GetDataRowViewValue(drv, "MetaFileDescription"), string.Empty);
                int imageWidth = ValidationHelper.GetInteger(DataHelper.GetDataRowViewValue(drv, "MetaFileImageWidth"), 0);
                int imageHeight = ValidationHelper.GetInteger(DataHelper.GetDataRowViewValue(drv, "MetaFileImageHeight"), 0);
                string tooltip = UIHelper.GetTooltipAttributes(fileUrl, imageWidth, imageHeight, title, fileName, fileExt, description, null, 300);

                // Icon
                string imageTag = String.Format("<img class=\"Icon\" src=\"{0}\" alt=\"{1}\" />", iconUrl, fileName);
                if (isImage)
                {
                    return String.Format("<a href=\"#\" onclick=\"javascript: window.open('{0}'); return false;\"><span id=\"{1}\" {2}>{3}{4}</span></a>", fileUrl, fileGuid, tooltip, imageTag, fileName);
                }
                else
                {
                    return String.Format("<a href=\"{0}\"><span id=\"{1}\" {2}>{3}{4}</span></a>", fileUrl, fileGuid, tooltip, imageTag, fileName);
                }

            case "size":
                return DataHelper.GetSizeString(ValidationHelper.GetLong(parameter, 0));

            case "update":
                {
                    // Display buttons only if 'Modify' is allowed
                    if (AllowModify)
                    {
                        drv = (DataRowView)parameter;
                        Panel pnlBlock = new Panel()
                        {
                            ID = "pnlBlock"
                        };

                        string fileExtension = ValidationHelper.GetString(drv["MetaFileExtension"], null);
                        string siteName = null;

                        if (SiteID > 0)
                        {
                            SiteInfo si = SiteInfoProvider.GetSiteInfo(SiteID);
                            if (si != null)
                            {
                                siteName = si.SiteName;
                            }
                        }
                        else
                        {
                            siteName = CMSContext.CurrentSiteName;
                        }

                        // Add update control
                        // Dynamically load uploader control
                        DirectFileUploader dfuElem = Page.LoadControl("~/CMSModules/Content/Controls/Attachments/DirectFileUploader/DirectFileUploader.ascx") as DirectFileUploader;

                        // Set uploader's properties
                        if (dfuElem != null)
                        {
                            dfuElem.ID = "dfuElem" + ObjectID;
                            dfuElem.SourceType = MediaSourceEnum.MetaFile;
                            dfuElem.ControlGroup = "Uploader_" + ObjectID;
                            dfuElem.DisplayInline = true;
                            dfuElem.ForceLoad = true;
                            dfuElem.MetaFileID = ValidationHelper.GetInteger(drv["MetaFileID"], 0);
                            dfuElem.ObjectID = ObjectID;
                            dfuElem.ObjectType = ObjectType;
                            dfuElem.Category = Category;
                            dfuElem.ParentElemID = ClientID;
                            dfuElem.ImageUrl = ResolveUrl(GetImageUrl("Design/Controls/DirectUploader/upload.png"));
                            dfuElem.ImageHeight = 16;
                            dfuElem.ImageWidth = 16;
                            dfuElem.InsertMode = false;
                            dfuElem.ParentElemID = ClientID;
                            dfuElem.IncludeNewItemInfo = true;
                            dfuElem.SiteID = SiteID;
                            dfuElem.IsLiveSite = IsLiveSite;
                            // Setting of the direct single mode
                            dfuElem.UploadMode = MultifileUploaderModeEnum.DirectSingle;
                            dfuElem.Width = 16;
                            dfuElem.Height = 16;
                            dfuElem.MaxNumberToUpload = 1;
                            dfuElem.EnableSilverlightUploader = false;

                            pnlBlock.Controls.Add(dfuElem);
                        }

                        // If the WebDAV is enabled and windows authentication
                        if (CMSContext.IsWebDAVEnabled(siteName) && RequestHelper.IsWindowsAuthentication()
                            && WebDAVSettings.IsExtensionAllowedForEditMode(fileExtension, siteName))
                        {
                            // Dynamically load control
                            WebDAVEditControl webDAVElem = Page.LoadControl("~/CMSModules/WebDAV/Controls/MetaFileWebDAVEditControl.ascx") as WebDAVEditControl;

                            // Set editor's properties
                            if (webDAVElem != null)
                            {
                                // Initialize WebDAV control
                                fileGuid = ValidationHelper.GetString(drv["MetaFileGUID"], "");
                                webDAVElem.MetaFileGUID = ValidationHelper.GetGuid(fileGuid, Guid.Empty);
                                webDAVElem.FileName = ValidationHelper.GetString(drv["MetaFileName"], null);

                                webDAVElem.Enabled = AllowModify;

                                // Initialize general info
                                webDAVElem.ID = "webDAVElem";
                                webDAVElem.IsLiveSite = IsLiveSite;
                                // Add to panel
                                pnlBlock.Controls.Add(webDAVElem);
                            }
                        }

                        return pnlBlock;
                    }
                    return null;
                }
        }
        return parameter;
    }


    protected void gridFiles_OnAction(string actionName, object actionArgument)
    {
        switch (actionName.ToLower())
        {
            case "delete":
                try
                {
                    // Get meta file ID
                    int metaFileId = ValidationHelper.GetInteger(actionArgument, 0);

                    // Get meta file
                    MetaFileInfo mfi = MetaFileInfoProvider.GetMetaFileInfo(metaFileId);

                    // Set currently handled meta file
                    CurrentlyHandledMetaFile = mfi;

                    // Fire before delete event
                    CancelEventArgs beforeDeleteArgs = new CancelEventArgs();
                    if (OnBeforeDelete != null)
                    {
                        OnBeforeDelete(this, beforeDeleteArgs);
                    }

                    // If delete was not cancelled
                    if (!beforeDeleteArgs.Cancel)
                    {
                        // Delete meta file
                        MetaFileInfoProvider.DeleteMetaFileInfo(metaFileId);

                        // Fire after delete event
                        if (OnAfterDelete != null)
                        {
                            OnAfterDelete(this, EventArgs.Empty);
                        }
                    }
                }
                catch (Exception ex)
                {
                    lblError.Visible = true;
                    lblError.Text = ex.Message;
                }
                break;
        }
    }


    protected void gridFiles_OnBeforeDataReload()
    {
        gridFiles.NamedColumns["update"].Visible = AllowModify;
        gridFiles.NamedColumns["filename"].Visible = false;
    }

    #endregion
}