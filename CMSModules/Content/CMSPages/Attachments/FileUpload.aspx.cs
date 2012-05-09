using System;

using CMS.GlobalHelper;
using CMS.ExtendedControls;
using CMS.CMSHelper;
using CMS.UIControls;
using CMS.SettingsProvider;

public partial class CMSModules_Content_CMSPages_Attachments_FileUpload : CMSLiveModalPage
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


    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = GetString("attach.uploadfile");
        CurrentMaster.Title.TitleText = Page.Title;
        CurrentMaster.Title.TitleImage = GetImageUrl("CMSModules/CMS_Content/EditMenu/save.png");
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

            // Get uploader control based on the current source type
            string uploaderPath = "";
            if (SourceType == MediaSourceEnum.MediaLibraries)
            {
                // If media library module is running
                if (ModuleEntry.IsModuleRegistered(ModuleEntry.MEDIALIBRARY) && ModuleEntry.IsModuleLoaded(ModuleEntry.MEDIALIBRARY))
                {
                    uploaderPath = "~/CMSModules/MediaLibrary/Controls/Dialogs/DirectFileUploader/FileUpload.ascx";
                }
            }
            else
            {
                uploaderPath = "~/CMSModules/Content/Controls/Attachments/FileUpload.ascx";
            }

            // Load direct file uploader
            if (uploaderPath != "")
            {
                FileUpload fileUploaderElem = LoadControl(uploaderPath) as FileUpload;
                if (fileUploaderElem != null)
                {

                    // Initialize uploader control properties by query string values
                    fileUploaderElem.ID = "uploader";
                    fileUploaderElem.ImageWidth = QueryHelper.GetInteger("imagewidth", 24);
                    fileUploaderElem.ImageHeight = QueryHelper.GetInteger("imageheight", 24);
                    fileUploaderElem.ImageUrl = QueryHelper.GetString("imageurl", GetImageUrl("Design/Controls/DirectUploader/upload.png"));
                    fileUploaderElem.ImageUrlOver = QueryHelper.GetString("imageurlover", String.Empty);
                    fileUploaderElem.InnerDivHtml = QueryHelper.GetString("innerdivhtml", String.Empty);
                    fileUploaderElem.InnerDivClass = QueryHelper.GetString("innerdivclass", String.Empty);
                    fileUploaderElem.InnerLoadingDivHtml = QueryHelper.GetString("innerloadingdivhtml", String.Empty);
                    fileUploaderElem.InnerLoadingDivClass = QueryHelper.GetString("innerloadingdivclass", String.Empty);
                    fileUploaderElem.LoadingImageUrl = QueryHelper.GetString("loadingimageurl", GetImageUrl("Design/Preloaders/preload16.gif"));
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
                    fileUploaderElem.SourceType = SourceType;

                    // Library info initialization
                    fileUploaderElem.LibraryID = QueryHelper.GetInteger("libraryid", 0);
                    fileUploaderElem.LibraryFolderPath = QueryHelper.GetString("path", "");
                    fileUploaderElem.IncludeNewItemInfo = QueryHelper.GetBoolean("includeinfo", false);

                    string siteName = CMSContext.CurrentSiteName;
                    string allowed = QueryHelper.GetString("allowedextensions", "");
                    if (allowed == "")
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
                    fileUploaderElem.ResizeToWidth = QueryHelper.GetInteger("autoresize_width", 0);
                    fileUploaderElem.ResizeToHeight = QueryHelper.GetInteger("autoresize_height", 0);
                    fileUploaderElem.ResizeToMaxSideSize = QueryHelper.GetInteger("autoresize_maxsidesize", 0);

                    fileUploaderElem.AfterSaveJavascript = QueryHelper.GetString("aftersave", String.Empty);

                    // Insert uploader to parent container
                    pnlUploaderElem.Controls.Add(fileUploaderElem);
                }
            }
        }
    }
}
