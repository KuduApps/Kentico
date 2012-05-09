using System;

using CMS.GlobalHelper;
using CMS.ExtendedControls;
using CMS.UIControls;
using CMS.SettingsProvider;
using CMS.CMSHelper;

public partial class CMSModules_Content_Attachments_DirectFileUploader_FileUploader : LivePage
{
    #region "Private variables"

    private MediaSourceEnum mSourceType = MediaSourceEnum.DocumentAttachments;

    #endregion


    #region "Private properties"

    /// <summary>
    /// Gets or sets current source type.
    /// </summary>
    private MediaSourceEnum SourceType
    {
        get
        {
            return mSourceType;
        }
        set
        {
            mSourceType = value;
        }
    }

    #endregion


    #region "Page events"

    protected override void OnPreInit(EventArgs e)
    {
        base.OnPreInit(e);
        if (QueryHelper.GetBoolean("islive", true))
        {
            Page.MasterPageFile = "~/CMSMasterPages/LiveSite/EmptyPage.master";
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        // Validate query string
        if (!QueryHelper.ValidateHash("hash"))
        {
            // Do nothing
        }
        else
        {
            // Get information on current source type
            string sourceType = QueryHelper.GetString("source", "attachments");
            SourceType = CMSDialogHelper.GetMediaSource(sourceType);

            // Ensure additional styles
            CurrentMaster.HeadElements.Visible = true;
            CurrentMaster.HeadElements.Text += CSSHelper.GetStyle("*{direction:ltr !important;}body{background:transparent !important;}input,input:focus,input:hover,input:active{border:none;border-color:transparent;outline:none;}");

            // Get uploader control based on the current source type
            string uploaderPath = "";
            if (SourceType == MediaSourceEnum.MediaLibraries)
            {
                // If media library module is running
                if (ModuleEntry.IsModuleRegistered(ModuleEntry.MEDIALIBRARY) && ModuleEntry.IsModuleLoaded(ModuleEntry.MEDIALIBRARY))
                {
                    uploaderPath = "~/CMSModules/MediaLibrary/Controls/Dialogs/DirectFileUploader/DirectMediaFileUploaderControl.ascx";
                }
            }
            else
            {
                uploaderPath = "~/CMSModules/Content/Controls/Attachments/DirectFileUploader/DirectFileUploaderControl.ascx";
            }

            // Load direct file uploader
            if (uploaderPath != "")
            {
                DirectFileUploader fileUploaderElem = LoadControl(uploaderPath) as DirectFileUploader;
                if (fileUploaderElem != null)
                {
                    // Insert uploader to parent container
                    pnlUploaderElem.Controls.Add(fileUploaderElem);

                    // Initialize uploader control properties by query string values
                    fileUploaderElem.AttachmentGUID = QueryHelper.GetGuid("attachmentguid", Guid.Empty);
                    fileUploaderElem.AttachmentGroupGUID = QueryHelper.GetGuid("attachmentgroupguid", Guid.Empty);
                    fileUploaderElem.AttachmentGUIDColumnName = QueryHelper.GetString("attachmentguidcolumnname", null);
                    fileUploaderElem.FormGUID = QueryHelper.GetGuid("formguid", Guid.Empty);
                    fileUploaderElem.DocumentID = QueryHelper.GetInteger("documentid", 0);
                    fileUploaderElem.NodeParentNodeID = QueryHelper.GetInteger("parentid", 0);
                    fileUploaderElem.NodeClassName = QueryHelper.GetString("classname", "");
                    fileUploaderElem.InsertMode = QueryHelper.GetBoolean("insertmode", false);
                    fileUploaderElem.OnlyImages = QueryHelper.GetBoolean("onlyimages", false);
                    fileUploaderElem.ParentElemID = QueryHelper.GetString("parentelemid", String.Empty);
                    fileUploaderElem.CheckPermissions = QueryHelper.GetBoolean("checkperm", true);
                    fileUploaderElem.IsLiveSite = QueryHelper.GetBoolean("islive", true);
                    fileUploaderElem.RaiseOnClick = QueryHelper.GetBoolean("click", false);
                    fileUploaderElem.NodeSiteName = QueryHelper.GetString("sitename", null);
                    fileUploaderElem.SourceType = SourceType;
                    fileUploaderElem.NodeGroupID = QueryHelper.GetInteger("nodegroupid", 0);

                    // Metafile upload
                    fileUploaderElem.SiteID = QueryHelper.GetInteger("siteid", 0);
                    fileUploaderElem.Category = QueryHelper.GetString("category", String.Empty);
                    fileUploaderElem.ObjectID = QueryHelper.GetInteger("objectid", 0);
                    fileUploaderElem.ObjectType = QueryHelper.GetString("objecttype", String.Empty);
                    fileUploaderElem.MetaFileID = QueryHelper.GetInteger("metafileid", 0);

                    // Library info initialization;
                    fileUploaderElem.LibraryID = QueryHelper.GetInteger("libraryid", 0);
                    fileUploaderElem.MediaFileID = QueryHelper.GetInteger("mediafileid", 0);
                    fileUploaderElem.MediaFileName = QueryHelper.GetString("filename", null);
                    fileUploaderElem.IsMediaThumbnail = QueryHelper.GetBoolean("ismediathumbnail", false);
                    fileUploaderElem.LibraryFolderPath = QueryHelper.GetString("path", "");
                    fileUploaderElem.IncludeNewItemInfo = QueryHelper.GetBoolean("includeinfo", false);

                    string siteName = CMSContext.CurrentSiteName;
                    string allowed = QueryHelper.GetString("allowedextensions", null);
                    if (allowed == null)
                    {
                        if (fileUploaderElem.SourceType == MediaSourceEnum.MediaLibraries)
                        {
                            allowed = SettingsKeyProvider.GetStringValue(siteName + ".CMSMediaFileAllowedExtensions");
                        }
                        else
                        {
                            allowed = SettingsKeyProvider.GetStringValue(siteName + ".CMSUploadExtensions");
                        }
                    }
                    fileUploaderElem.AllowedExtensions = allowed;

                    // Auto resize width
                    int autoResizeWidth = QueryHelper.GetInteger("autoresize_width", -1);
                    if (autoResizeWidth == -1)
                    {
                        autoResizeWidth = SettingsKeyProvider.GetIntValue(siteName + ".CMSAutoResizeImageWidth");
                    }
                    fileUploaderElem.ResizeToWidth = autoResizeWidth;

                    // Auto resize height
                    int autoResizeHeight = QueryHelper.GetInteger("autoresize_height", -1);
                    if (autoResizeHeight == -1)
                    {
                        autoResizeHeight = SettingsKeyProvider.GetIntValue(siteName + ".CMSAutoResizeImageHeight");
                    }
                    fileUploaderElem.ResizeToHeight = autoResizeHeight;

                    // Auto resize max side size
                    int autoResizeMaxSideSize = QueryHelper.GetInteger("autoresize_maxsidesize", -1);
                    if (autoResizeMaxSideSize == -1)
                    {
                        autoResizeMaxSideSize = SettingsKeyProvider.GetIntValue(siteName + ".CMSAutoResizeImageMaxSideSize");
                    }
                    fileUploaderElem.ResizeToMaxSideSize = autoResizeMaxSideSize;

                    fileUploaderElem.AfterSaveJavascript = QueryHelper.GetString("aftersave", String.Empty);
                    fileUploaderElem.TargetFolderPath = QueryHelper.GetString("targetfolder", String.Empty);
                    fileUploaderElem.TargetFileName = QueryHelper.GetString("targetfilename", String.Empty);
                }
            }
        }
    }

    #endregion
}
