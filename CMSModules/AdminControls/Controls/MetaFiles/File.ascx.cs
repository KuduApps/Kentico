using System;
using System.Data;
using System.Web;

using CMS.GlobalHelper;
using CMS.SiteProvider;
using CMS.CMSHelper;
using CMS.UIControls;
using CMS.IO;
using CMS.SettingsProvider;
using System.Web.UI.WebControls;
using CMS.ExtendedControls;

public partial class CMSModules_AdminControls_Controls_MetaFiles_File : CMSUserControl
{
    #region "Variables"

    private bool mAlreadyUploadedDontDelete = false;
    private string baseUrl = null;
    private int width = 0;
    private int height = 0;

    protected bool columnUpdateVisible = false;

    #endregion


    #region "Properties"

    /// <summary>
    /// After metafile delete event.
    /// </summary>
    public event EventHandler OnAfterDelete;


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
    /// Site name.
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
    /// Returns true if saving of the file failed.
    /// </summary>
    public bool SavingFailed
    {
        get
        {
            return ValidationHelper.GetBoolean(ViewState["SavingFailed"], false);
        }
    }


    /// <summary>
    /// Returns true if deleting of the file failed.
    /// </summary>
    public bool DeletingFailed
    {
        get
        {
            return ValidationHelper.GetBoolean(ViewState["DeletingFailed"], false);
        }
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


    /// <summary>
    /// Returns the currently posted file or null when no file posted.
    /// </summary>
    public HttpPostedFile PostedFile
    {
        get
        {
            return uploader.PostedFile;
        }
    }


    /// <summary>
    /// Currently handled meta file.
    /// </summary>
    public MetaFileInfo CurrentlyHandledMetaFile
    {
        get;
        set;
    }


    /// <summary>
    /// Allow modify flag.
    /// </summary>
    protected bool AllowModify
    {
        get
        {
            return (this.Enabled && UserInfoProvider.IsAuthorizedPerObject(this.ObjectType, this.ObjectID, PermissionsEnum.Modify, CMSContext.CurrentSiteName, CMSContext.CurrentUser));
        }
    }

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Register dialog script for Image Editor
        ScriptHelper.RegisterDialogScript(Page);
        ScriptHelper.RegisterTooltip(Page);
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
    if(fullRefresh){{
        {1}
    }}
    else {{
        {2}
    }}
}}
function ConfirmDelete() {{
    return confirm({3});;
}}
 ",
            ClientID,
            ControlsHelper.GetPostBackEventReference(hdnFullPostback, ""),
            ControlsHelper.GetPostBackEventReference(hdnPostback, ""),
            ScriptHelper.GetString(GetString("general.confirmdelete")));

        ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "MetafileScripts_" + ClientID, ScriptHelper.GetScript(script));

        SetupControls();
    }


    private void SetupControls()
    {
        if (this.Enabled)
        {
            uploader.OnUploadFile += uploader_OnUploadFile;
            uploader.OnDeleteFile += uploader_OnDeleteFile;
        }

        // Initialize UniGrid only if ObjectID is present
        if (ObjectID > 0)
        {
            gridFile.OnAction += gridFile_OnAction;
            gridFile.OnExternalDataBound += gridFile_OnExternalDataBound;
            gridFile.IsLiveSite = IsLiveSite;
            gridFile.WhereCondition = MetaFileInfoProvider.GetWhereCondition(ObjectID, ObjectType, Category, null);
            gridFile.StopProcessing = StopProcessing;
            pnlGrid.Visible = true;
            pnlAttachmentList.CssClass = "AttachmentsList";

            // Hide update column if modify not enabled
            gridFile.GridColumns.Columns[0].Visible = this.AllowModify;
        }
        else
        {
            pnlGrid.Visible = false;
        }

        uploader.CurrentFileName = String.Empty;
        uploader.CurrentFileUrl = String.Empty;

        if ((ObjectID > 0) && (ObjectType != "") && (Category != ""))
        {
            gridFile.ReloadData();
        }

        if (ObjectID > 0)
        {
            if (this.AllowModify)
            {
                // Initialize button for adding metafile
                newMetafileElem.ImageUrl = ResolveUrl(GetImageUrl("Design/Controls/DirectUploader/upload_new.png"));
                newMetafileElem.ImageWidth = 16;
                newMetafileElem.ImageHeight = 16;
                newMetafileElem.ObjectID = ObjectID;
                newMetafileElem.ObjectType = ObjectType;
                newMetafileElem.Category = Category;
                newMetafileElem.ParentElemID = ClientID;
                newMetafileElem.SiteID = SiteID;
                newMetafileElem.ForceLoad = true;
                newMetafileElem.InnerDivHtml = GetString("attach.uploadfile");
                newMetafileElem.InnerDivClass = "NewAttachment";
                newMetafileElem.InnerLoadingDivHtml = GetString("attach.loading");
                newMetafileElem.InnerLoadingDivClass = "NewAttachmentLoading";
                newMetafileElem.IsLiveSite = IsLiveSite;
                newMetafileElem.SourceType = MediaSourceEnum.MetaFile;
                newMetafileElem.Visible = true;
            }
            else
            {
                lblDisabled.CssClass = "NewAttachmentDisabled";
                imgDisabled.ImageUrl = ResolveUrl(GetImageUrl("Design/Controls/DirectUploader/upload_newdisabled.png"));
            }
            plcUploader.Visible = this.AllowModify;
            plcUploaderDisabled.Visible = !this.AllowModify;
            plcOldUploader.Visible = false;
        }
        else
        {
            newMetafileElem.Visible = false;
            plcUploader.Visible = false;
            plcUploaderDisabled.Visible = false;
            plcOldUploader.Visible = true;
        }
    }


    /// <summary>
    /// Reloads file uploader.
    /// </summary>
    public void ReloadData()
    {
        SetupControls();
    }


    /// <summary>
    /// Inits uploaded file name based on file name and GUID.
    /// </summary>
    /// <param name="fileName">File name</param>
    /// <param name="fileGuid">File GUID</param>
    public void InitUploader(string fileName, Guid fileGuid)
    {
        uploader.CurrentFileName = Path.GetFileName(fileName);
        uploader.CurrentFileUrl = "~/CMSPages/GetMetaFile.aspx?fileguid=" + fileGuid;
    }


    protected object gridFile_OnExternalDataBound(object sender, string sourceName, object parameter)
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
                    if (this.AllowModify)
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

            case "delete":
                if (sender is ImageButton)
                {
                    ImageButton btnDelete = ((ImageButton)sender);
                    if (!this.AllowModify)
                    {
                        btnDelete.ImageUrl = GetImageUrl("Design/Controls/Uploader/deletedisabled.png");
                        btnDelete.Enabled = false;
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

                            webDAVElem.Enabled = true;

                            // Initialize general info
                            webDAVElem.ID = "webDAVElem";
                            webDAVElem.IsLiveSite = IsLiveSite;
                            // Add to panel
                            pnlBlock.Controls.Add(webDAVElem);

                            //columnUpdateVisible = true;
                        }
                    }

                    return pnlBlock;
                }
        }
        return parameter;
    }

    protected void gridFile_OnAction(string actionName, object actionArgument)
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

                    // Delete meta file
                    MetaFileInfoProvider.DeleteMetaFileInfo(metaFileId);

                    // Execute after delete event
                    if (OnAfterDelete != null)
                    {
                        OnAfterDelete(this, null);
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

    protected void uploader_OnUploadFile(object sender, EventArgs e)
    {
        UploadFile();
    }


    protected void uploader_OnDeleteFile(object sender, EventArgs e)
    {
        // Careful with upload and delete in on postback - ignore delete request
        if (mAlreadyUploadedDontDelete)
        {
            return;
        }

        try
        {
            using (DataSet ds = MetaFileInfoProvider.GetMetaFiles(ObjectID, ObjectType, Category, null, null))
            {
                if (!DataHelper.DataSourceIsEmpty(ds))
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        MetaFileInfo mfi = new MetaFileInfo(dr);
                        if ((mfi != null) && (mfi.MetaFileName.ToLower() == uploader.CurrentFileName.ToLower()))
                        {
                            MetaFileInfoProvider.DeleteMetaFileInfo(mfi.MetaFileID);
                        }
                    }
                }
            }

            // Execute after delete event
            if (OnAfterDelete != null)
            {
                OnAfterDelete(this, null);
            }

            SetupControls();
        }
        catch (Exception ex)
        {
            ViewState["DeletingFailed"] = true;
            lblErrorUploader.Visible = true;
            lblErrorUploader.Text = ex.Message;
            SetupControls();
        }
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        if (!string.IsNullOrEmpty(baseUrl))
        {
            ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "OpenEditor",
                            ScriptHelper.GetScript(String.Format(@"
function OpenEditor(queryString) {{ 
    modalDialog('{0}{1}' + queryString, 'editorDialog', {2}, {3}); 
    return false; 
}}", URLHelper.GetFullApplicationUrl(), baseUrl, width, height)));
        }

        if (ObjectID > 0)
        {
            bool gridHasData = !DataHelper.DataSourceIsEmpty(gridFile.GridView.DataSource);

            // Ensure uploader button
            plcUploader.Visible = (this.AllowModify && !gridHasData);
            plcUploaderDisabled.Visible = (!this.AllowModify && !gridHasData);
        }
    }


    /// <summary>
    /// Uploads file.
    /// </summary>
    public void UploadFile()
    {
        if ((uploader.PostedFile != null) && (uploader.PostedFile.ContentLength > 0) && (ObjectID > 0))
        {
            try
            {
                MetaFileInfo existing = null;

                // Check if uploaded file already exists and delete it
                DataSet ds = MetaFileInfoProvider.GetMetaFiles(ObjectID, ObjectType, Category, null, null);
                if (!DataHelper.DataSourceIsEmpty(ds))
                {
                    // Get existing record ID and delete it
                    existing = new MetaFileInfo(ds.Tables[0].Rows[0]);
                    MetaFileInfoProvider.DeleteMetaFileInfo(existing);
                }

                // Create new meta file
                MetaFileInfo mfi = new MetaFileInfo(uploader.PostedFile, ObjectID, ObjectType, Category);
                if (existing != null)
                {
                    // Preserve GUID
                    mfi.MetaFileGUID = existing.MetaFileGUID;
                    mfi.MetaFileTitle = existing.MetaFileTitle;
                    mfi.MetaFileDescription = existing.MetaFileDescription;
                }
                mfi.MetaFileSiteID = SiteID;

                // Save to the database
                MetaFileInfoProvider.SetMetaFileInfo(mfi);

                // Set currently handled meta file
                this.CurrentlyHandledMetaFile = mfi;

                SetupControls();
            }
            catch (Exception ex)
            {
                lblErrorUploader.Visible = true;
                lblErrorUploader.Text = ex.Message;
                ViewState["SavingFailed"] = true;
                SetupControls();
            }

            // File was uploaded, do not delete in one postback
            mAlreadyUploadedDontDelete = true;
        }
    }


    /// <summary>
    /// Clears the content (file name & file URL) of the control.
    /// </summary>
    public void ClearControl()
    {
        uploader.CurrentFileName = "";
        uploader.CurrentFileUrl = "";
    }

    #endregion
}
