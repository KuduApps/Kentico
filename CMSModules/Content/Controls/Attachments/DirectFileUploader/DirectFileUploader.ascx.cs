using System;
using System.Text;
using System.Web.UI.WebControls;

using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.ExtendedControls;
using CMS.CMSHelper;
using System.Web.UI;

public partial class CMSModules_Content_Controls_Attachments_DirectFileUploader_DirectFileUploader : DirectFileUploader
{
    #region "Variables"

    private string mIFrameUrl = null;
    private MediaSourceEnum mSourceType = MediaSourceEnum.DocumentAttachments;

    #endregion


    #region "Properties"

    /// <summary>
    /// Gets or sets type of the content uploaded by the control.
    /// </summary>
    public override MediaSourceEnum SourceType
    {
        get
        {
            return mSourceType;
        }
        set
        {
            mSourceType = value;
            mfuDirectUploader.SourceType = SourceType;
        }
    }


    /// <summary>
    /// Gets or sets node site name.
    /// </summary>
    public override string IFrameUrl
    {
        get
        {
            return mIFrameUrl ?? (mIFrameUrl = GetIframeUrl(containerDiv.ClientID, uploaderFrame.ClientID));
        }
    }


    /// <summary>
    /// Indicates whether the control is displayed.
    /// </summary>
    public override bool Visible
    {
        get
        {
            return base.Visible;
        }
        set
        {
            base.Visible = value;
            uploaderFrame.Visible = value;
        }
    }


    /// <summary>
    /// Indicates if silverlight uploader should be enabled. If false only direft file uploader iFrame will be rendered.
    /// </summary>
    public override bool EnableSilverlightUploader
    {
        get
        {
            return mfuDirectUploader.Enabled;
        }
        set
        {
            mfuDirectUploader.Enabled = value;
        }
    }


    /// <summary>
    /// Control key.
    /// </summary>
    private string ControlKey
    {
        get
        {
            return String.Format("{0}_{1}", ControlGroup, ParentElemID);
        }
    }


    /// <summary>
    /// Iframe CSS class.
    /// </summary>
    private string IFrameCSSClass
    {
        get
        {
            return "DFUframe_" + ControlGroup;
        }
    }


    /// <summary>
    /// GUID of attachment group.
    /// </summary>
    public override Guid AttachmentGroupGUID
    {
        get
        {
            return base.AttachmentGroupGUID;
        }
        set
        {
            base.AttachmentGroupGUID = value;
            mfuDirectUploader.AttachmentGroupGUID = AttachmentGroupGUID;
        }
    }


    /// <summary>
    /// Name of document attachment column.
    /// </summary>
    public override string AttachmentGUIDColumnName
    {
        get
        {
            return base.AttachmentGUIDColumnName;
        }
        set
        {
            base.AttachmentGUIDColumnName = value;
            mfuDirectUploader.AttachmentGUIDColumnName = AttachmentGUIDColumnName;
        }
    }


    /// <summary>
    /// GUID of attachment.
    /// </summary>
    public override Guid AttachmentGUID
    {
        get
        {
            return base.AttachmentGUID;
        }
        set
        {
            base.AttachmentGUID = value;
            mfuDirectUploader.AttachmentGUID = AttachmentGUID;
        }
    }


    /// <summary>
    /// Width of attachment.
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
            mfuDirectUploader.ResizeToWidth = ResizeToWidth;
        }
    }


    /// <summary>
    /// Height of attachment.
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
            mfuDirectUploader.ResizeToHeight = ResizeToHeight;
        }
    }


    /// <summary>
    /// Maximum side size of attachment.
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
            mfuDirectUploader.ResizeToMaxSideSize = ResizeToMaxSideSize;
        }
    }


    /// <summary>
    /// GUID of form.
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
            mfuDirectUploader.FormGUID = FormGUID;
        }
    }


    /// <summary>
    /// ID of document.
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
            mfuDirectUploader.DocumentID = DocumentID;
        }
    }


    /// <summary>
    /// Parent node ID.
    /// </summary>
    public override int NodeParentNodeID
    {
        get
        {
            return base.NodeParentNodeID;
        }
        set
        {
            base.NodeParentNodeID = value;
            mfuDirectUploader.DocumentParentNodeID = NodeParentNodeID;
            mfuDirectUploader.NodeID = NodeParentNodeID;
        }
    }


    /// <summary>
    /// Document culture short name.
    /// </summary>
    public override string DocumentCulture
    {
        get
        {
            return base.DocumentCulture;
        }
        set
        {
            base.DocumentCulture = value;
            mfuDirectUploader.DocumentCulture = DocumentCulture;
        }
    }


    /// <summary>
    /// Document class name.
    /// </summary>
    public override string NodeClassName
    {
        get
        {
            return base.NodeClassName;
        }
        set
        {
            base.NodeClassName = value;
            mfuDirectUploader.NodeClassName = NodeClassName;
        }
    }


    /// <summary>
    /// Current site name.
    /// </summary>
    public override string NodeSiteName
    {
        get
        {
            return base.NodeSiteName;
        }
        set
        {
            base.NodeSiteName = value;
            mfuDirectUploader.NodeSiteName = NodeSiteName;
        }
    }


    /// <summary>
    /// Indicates if control is in insert mode (only new attachments are added, no update).
    /// </summary>
    public override bool InsertMode
    {
        get
        {
            return base.InsertMode;
        }
        set
        {
            base.InsertMode = value;
            mfuDirectUploader.IsInsertMode = InsertMode;
        }
    }


    /// <summary>
    /// Gets or sets which files with extensions are allowed to be uploaded.
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
            mfuDirectUploader.AllowedExtensions = AllowedExtensions;
        }
    }


    /// <summary>
    /// Indicates if control is used on live site.
    /// </summary>
    public override bool IsLiveSite
    {
        get
        {
            return base.IsLiveSite;
        }
        set
        {
            base.IsLiveSite = value;
            mfuDirectUploader.IsLiveSite = IsLiveSite;
        }
    }


    /// <summary>
    /// Indicates if only images is allowed for upload.
    /// </summary>
    public override bool OnlyImages
    {
        get
        {
            return base.OnlyImages;
        }
        set
        {
            base.OnlyImages = value;
            mfuDirectUploader.OnlyImages = OnlyImages;
        }
    }


    /// <summary>
    /// ID of the current library.
    /// </summary>
    public override int LibraryID
    {
        get
        {
            return base.LibraryID;
        }
        set
        {
            base.LibraryID = value;
            mfuDirectUploader.MediaLibraryID = LibraryID;
        }
    }


    /// <summary>
    /// Folder path of the current library.
    /// </summary>
    public override string LibraryFolderPath
    {
        get
        {
            return base.LibraryFolderPath;
        }
        set
        {
            base.LibraryFolderPath = value;
            mfuDirectUploader.MediaFolderPath = LibraryFolderPath;
        }
    }


    /// <summary>
    /// Media file name.
    /// </summary>
    public override string MediaFileName
    {
        get
        {
            return base.MediaFileName;
        }
        set
        {
            base.MediaFileName = value;
            mfuDirectUploader.MediaFileName = MediaFileName;
        }
    }


    /// <summary>
    /// Determines whether the uploader should upload media file thumbnail or basic media file.
    /// </summary>
    public override bool IsMediaThumbnail
    {
        get
        {
            return base.IsMediaThumbnail;
        }
        set
        {
            base.IsMediaThumbnail = value;
            mfuDirectUploader.IsMediaThumbnail = IsMediaThumbnail;
        }
    }


    /// <summary>
    /// ID of the media file.
    /// </summary>
    public override int MediaFileID
    {
        get
        {
            return base.MediaFileID;
        }
        set
        {
            base.MediaFileID = value;
            mfuDirectUploader.MediaFileID = MediaFileID;
        }
    }


    /// <summary>
    /// JavaScript function name called after save of new file.
    /// </summary>
    public override string AfterSaveJavascript
    {
        get
        {
            return base.AfterSaveJavascript;
        }
        set
        {
            base.AfterSaveJavascript = value;
            mfuDirectUploader.AfterSaveJavascript = AfterSaveJavascript;
        }
    }


    /// <summary>
    /// ID of parent element.
    /// </summary>
    public override string ParentElemID
    {
        get
        {
            return base.ParentElemID;
        }
        set
        {
            base.ParentElemID = value;
            mfuDirectUploader.ParentElemID = ParentElemID;
        }
    }


    /// <summary>
    /// Target folder path, to which physical files will be uploaded.
    /// </summary>
    public override string TargetFolderPath
    {
        get
        {
            return base.TargetFolderPath;
        }
        set
        {
            base.TargetFolderPath = value;
            mfuDirectUploader.TargetFolderPath = TargetFolderPath;
        }
    }


    /// <summary>
    /// Target file name, to which will be used after files will be uploaded.
    /// </summary>
    public override string TargetFileName
    {
        get
        {
            return base.TargetFileName;
        }
        set
        {
            base.TargetFileName = value;
            mfuDirectUploader.TargetFileName = TargetFileName;
        }
    }


    /// <summary>
    /// Indicates whether the permissions should be explicitly checked.
    /// </summary>
    public override bool CheckPermissions
    {
        get
        {
            return base.CheckPermissions;
        }
        set
        {
            base.CheckPermissions = value;
            mfuDirectUploader.CheckPermissions = CheckPermissions;
        }
    }


    /// <summary>
    /// Upload mode for the silverlight uploader application.
    /// </summary>
    public override MultifileUploaderModeEnum UploadMode
    {
        get
        {
            return base.UploadMode;
        }
        set
        {
            base.UploadMode = value;
            mfuDirectUploader.UploadMode = UploadMode;
        }
    }


    /// <summary>
    /// Container width.
    /// </summary>
    public override Unit Width
    {
        get
        {
            return base.Width;
        }
        set
        {
            base.Width = value;
            mfuDirectUploader.Width = Width;
        }
    }


    /// <summary>
    /// Container height.
    /// </summary>
    public override Unit Height
    {
        get
        {
            return base.Height;
        }
        set
        {
            base.Height = value;
            mfuDirectUploader.Height = Height;
        }
    }


    /// <summary>
    /// Indicates whether the post-upload JavaScript function call should include created attachment GUID information.
    /// </summary>
    public override bool IncludeNewItemInfo
    {
        get
        {
            return base.IncludeNewItemInfo;
        }
        set
        {
            base.IncludeNewItemInfo = value;
            mfuDirectUploader.IncludeNewItemInfo = IncludeNewItemInfo;
        }
    }


    /// <summary>
    /// Indicates if supported browser.
    /// </summary>
    public override bool RaiseOnClick
    {
        get
        {
            return base.RaiseOnClick;
        }
        set
        {
            base.RaiseOnClick = value;
            mfuDirectUploader.RaiseOnClick = RaiseOnClick;
        }
    }


    /// <summary>
    /// Value indicating whether multiselect is enabled in the open file dialog window.
    /// </summary>
    public override bool Multiselect
    {
        get
        {
            return base.Multiselect;
        }
        set
        {
            base.Multiselect = value;
            mfuDirectUploader.Multiselect = Multiselect;
        }
    }


    /// <summary>
    /// Max number of possible upload files.
    /// </summary>
    public override int MaxNumberToUpload
    {
        get
        {
            return base.MaxNumberToUpload;
        }
        set
        {
            base.MaxNumberToUpload = value;
            mfuDirectUploader.MaxNumberToUpload = MaxNumberToUpload;
        }
    }


    /// <summary>
    /// Site ID form metafile upload.
    /// </summary>
    public override int SiteID
    {
        get
        {
            return base.SiteID;
        }
        set
        {
            base.SiteID = value;
            mfuDirectUploader.SiteID = SiteID;
        }
    }


    /// <summary>
    /// Category/Group for uploaded metafile.
    /// </summary>
    public override string Category
    {
        get
        {
            return base.Category;
        }
        set
        {
            base.Category = value; ;
            mfuDirectUploader.Category = Category;
        }
    }


    /// <summary>
    /// Metafile ID for reupload.
    /// </summary>
    public override int MetaFileID
    {
        get
        {
            return base.MetaFileID;
        }
        set
        {
            base.MetaFileID = value;
            mfuDirectUploader.MediaFileID = MetaFileID;
        }
    }


    /// <summary>
    /// Object ID for metafile upload.
    /// </summary>
    public override int ObjectID
    {
        get
        {
            return base.ObjectID;
        }
        set
        {
            base.ObjectID = value; ;
            mfuDirectUploader.ObjectID = ObjectID;
        }
    }


    /// <summary>
    /// Object type for metafile upload.
    /// </summary>
    public override string ObjectType
    {
        get
        {
            return base.ObjectType;
        }
        set
        {
            base.ObjectType = value; ;
            mfuDirectUploader.ObjectType = ObjectType;
        }
    }

    #endregion


    #region "Methods"

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        if (StopProcessing)
        {
            // Do nothing
        }
        else
        {
            ScriptHelper.RegisterJQuery(Page);
            ScriptHelper.RegisterScriptFile(Page, "~/CMSModules/Content/Controls/Attachments/DirectFileUploader/DirectFileUploader.js");

            if (!RequestHelper.IsPostBack() || ForceLoad)
            {
                ReloadData();
            }

            if (ControlGroup != null)
            {
                // First instance of the control is loaded
                RequestStockHelper.Add("DFUInstanceLoaded_" + ControlKey, true);

                StringBuilder sb = new StringBuilder();
                sb.Append(
@"
function DFULoadIframes_", ControlKey, @"() {
    var iframe = document.getElementById('", uploaderFrame.ClientID, @"');
    if (iframe!=null) {
        iframe.setAttribute('allowTransparency','true');
        if (window.DFUframes != null) {
            var iframes = $j('iframe.", IFrameCSSClass, @"');
            for(var i = 0; i < iframes.length; i++) {
                var f = iframes[i];
                var p = f.parentNode.parentNode;
                var imgs = p.getElementsByTagName('img');
                if ((imgs != null) && (imgs[0] != null)) {
                    p.removeChild(imgs[0]);
                }
                var o = null;
                var cw = iframe.contentWindow;
                if (cw != null)
                { 
                    var cwd = cw.document;
                    if (cwd != null) {
                        var cn = cwd.childNodes;
                        if ((cn != null) && (cn.length > 0) && (cn[1].innerHTML != null)) {
                            var containerId = DFUframes[f.id].match(/containerid=([^&]+)/i)[1];
                    
                            o = cn[1].innerHTML;
                            o = o.replace(/action=[^\\s]+/, 'action=""' + DFUframes[f.id] + '""').replace('", ERROR_FUNCTION, @"','');
                            o = o.replace(/(\.\.\/)+App_Themes\//ig, '", ResolveUrl("~"), @"App_Themes/');
                            o = o.replace(/OnUploadBegin\('[^']+'\)/ig, 'OnUploadBegin(\'' + containerId + '\')');

                            var cd = f.contentWindow.document;
                            cd.write(o);
                            cd.close();

                            f.style.display = '';
                            f.setAttribute('allowTransparency','true');
                        }
                    }
                }
            }
        }
    }
}"
                );

                string script = sb.ToString();

                ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "DFUIframesLoader_" + ControlKey, ScriptHelper.GetScript(script));

                if (RequestHelper.IsAsyncPostback())
                {
                    ScriptHelper.RegisterStartupScript(this, typeof(string), "DFUIframesLoader_" + ControlKey, ScriptHelper.GetScript(script));
                }
            }
        }
    }


    /// <summary>
    /// Loads data of the control.
    /// </summary>
    public override void ReloadData()
    {
        mIFrameUrl = null;
        LoadIFrame();
        mfuDirectUploader.ContainerID = containerDiv.ClientID;
        mfuDirectUploader.OnUploadBegin = "DFU.OnUploadBegin";
        mfuDirectUploader.OnUploadCompleted = "DFU.OnUploadCompleted";

        // Display progress only for UI
        if (!IsLiveSite || ShowProgress)
        {
            mfuDirectUploader.OnUploadProgressChanged = "DFU.OnUploadProgressChanged";
        }
        mfuDirectUploader.ReloadData();
    }

    #endregion


    #region "Private methods"

    /// <summary>
    /// Loads inner IFrame content.
    /// </summary>
    private void LoadIFrame()
    {

        // Set iframe attributes
        if ((ControlGroup == null) || !ValidationHelper.GetBoolean(RequestStockHelper.GetItem("DFUInstanceLoaded_" + ControlKey), false))
        {
            uploaderFrame.Attributes.Add("src", ResolveUrl(IFrameUrl));

            if (ControlGroup != null)
            {
                uploaderFrame.Attributes.Add("onload", String.Format("(function tryLoadDFU_{0}() {{ if (window.DFULoadIframes_{0}) {{ window.DFULoadIframes_{0}(); }} else {{ setTimeout(tryLoadDFU_{0}, 200); }} }})();", ControlKey));
            }
            else
            {
                if (RequestHelper.IsCallback() && BrowserHelper.IsIE())
                {
                    uploaderFrame.Attributes.Add("allowTransparency", "true");
                }
                else
                {
                    string script = String.Format("var frameElem = document.getElementById('{0}'); if(frameElem) {{ frameElem.setAttribute('allowTransparency','true') }}", uploaderFrame.ClientID);
                    ScriptHelper.RegisterStartupScript(this, typeof(string), "DFUTrans_" + ClientID, ScriptHelper.GetScript(script));
                }
            }
        }
        else
        {
            uploaderFrame.Attributes.Add("style", "display:none;vertical-align:middle;");
            uploaderFrame.Attributes.Add("class", IFrameCSSClass);
            string script = String.Format("if(!window.DFUframes) {{ var DFUframes = new Object(); }}if(!window.DFUpanels) {{ var DFUpanels = new Object(); }}DFUframes['{0}'] = {1};",
                uploaderFrame.ClientID,
                ScriptHelper.GetString(ResolveUrl(IFrameUrl)));
            ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "DFUArrays_" + ClientID, ScriptHelper.GetScript(script));
            if (RequestHelper.IsAsyncPostback())
            {
                ScriptHelper.RegisterStartupScript(this, typeof(string), "DFUArrays_" + ClientID, ScriptHelper.GetScript(script));
            }
        }

        uploaderFrame.Attributes.Add("title", uploaderFrame.ID);
        uploaderFrame.Attributes.Add("name", uploaderFrame.ClientID);

        // Initialize design
        InitDesign();
    }


    /// <summary>
    /// Initialize design.
    /// </summary>
    private void InitDesign()
    {
        // Register css styles for uploader
        CSSHelper.RegisterCSSBlock(Page, CreateCss(containerDiv.ClientID));

        // Prepare loading image
        if (!String.IsNullOrEmpty(LoadingImageUrl))
        {
            string imgFloat = "left";
            if (IsLiveSite)
            {
                if (CultureHelper.IsPreferredCultureRTL())
                {
                    imgFloat = "right";
                }
            }
            else
            {
                if (CultureHelper.IsUICultureRTL())
                {
                    imgFloat = "right";
                }
            }
            imgLoading.Style.Add("float", imgFloat);
            imgLoading.ImageUrl = LoadingImageUrl;
            imgLoading.AlternateText = GetString("tree.loading");
        }
        else
        {
            imgLoading.Visible = false;
        }

        if (String.IsNullOrEmpty(InnerLoadingDivHtml))
        {
            lblLoading.Text = GetString("general.uploading");
        }
        else
        {
            lblLoading.Text = InnerLoadingDivHtml;
        }

        // Loading css class
        lblLoading.CssClass = InnerLoadingDivClass;
        lblProgress.CssClass = InnerLoadingDivClass;

        // Ensure nowrap on loading text
        pnlLoading.Style.Add("white-space", "nowrap;");
        pnlLoading.Style.Add("display", "none");

        // Prepare uploader image
        if (!String.IsNullOrEmpty(ImageUrl))
        {
            ltlUploadImage.Text = String.Format("<img class=\"UploaderImage\" alt=\"\" src=\"{0}\" {1}{2}/>",
                GetImageUrl("General/Transparent.gif"),
                (ImageWidth > 0 ? String.Format("width=\"{0}\" ", ImageWidth) : null),
                (ImageHeight > 0 ? String.Format("height=\"{0}\" ", ImageHeight) : null));
        }
        // Inner div html and design
        if (!String.IsNullOrEmpty(InnerDivHtml))
        {
            ltlInnerDiv.Text = InnerDivHtml;
        }
        if (!String.IsNullOrEmpty(InnerDivClass))
        {
            pnlInnerDiv.CssClass += " " + InnerDivClass;
        }

        // Container div styles
        string style = "position:relative;";
        if (DisplayInline)
        {
            style += "flaot:left;";
        }
        containerDiv.Attributes.Add("style", style);
        if (!String.IsNullOrEmpty(ControlGroup))
        {
            containerDiv.Attributes.Add("class", ControlGroup);
        }

        string initScript = ScriptHelper.GetScript(String.Format("if (typeof(DFU) !== 'undefined') {{ DFU.initializeDesign({0}); }}", ScriptHelper.GetString(containerDiv.ClientID)));

        if (ControlsHelper.IsInAsyncPostback(Page))
        {
            ScriptHelper.RegisterStartupScript(this, typeof(string), "DFUInit_" + ClientID, initScript);
        }
        else
        {
            ltlScript.Text = initScript;
        }
    }

    #endregion
}
