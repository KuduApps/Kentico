using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.Controls;
using CMS.MediaLibrary;
using CMS.IO;

public partial class CMSModules_MediaLibrary_Controls_LiveControls_MediaGallery : CMSAdminControl
{
    #region "Variables"

    private MediaLibraryInfo mMediaLibrary = null;
    private string mMediaLibraryName = null;
    private string mMediaLibraryPath = null;
    private bool mHideFolderTree = false;
    private string mTransformationName = null;
    private string mSelectedItemTransformation = null;
    private string mHeaderTransformation = null;
    private string mFooterTransformation = null;
    private string mSeparatorTransformation = null;
    private bool mShowSubfoldersContent = false;
    private string mMediaLibraryRoot = null;
    private string mMediaLibraryUrl = null;
    private int mMediaFileID = 0;
    private string mPreviewSuffix = null;
    private string mIconSet = null;
    private bool mDisplayActiveContent = true;
    private bool? mDisplayDetail = null;
    private bool mAllowUpload = false;
    private bool mAllowUploadPreview = false;
    private bool mDisplayFileCount = false;
    private bool mUseSecureLinks = true;

    private int mFilterMethod = 0;
    private string mFileIDQueryStringKey = null;
    private string mSortQueryStringKey = null;
    private string mPathQueryStringKey = null;

    private int mSelectTopN = 0;
    private bool mHideControlForZeroRows = true;
    private string mZeroRowsText = null;

    private string mPagesTemplate = null;
    private string mCurrentPageTemplate = null;
    private string mSeparatorTemplate = null;
    private string mFirstPageTemplate = null;
    private string mLastPageTemplate = null;
    private string mPreviousPageTemplate = null;
    private string mNextPageTemplate = null;
    private string mPreviousGroupTemplate = null;
    private string mNextGroupTemplate = null;
    private string mLayoutTemplate = null;

    private bool hidden = false;

    #endregion


    #region "Content properties"

    /// <summary>
    /// Gets or sets the number which indicates how many files should be displayed.
    /// </summary>
    public int SelectTopN
    {
        get
        {
            return this.mSelectTopN;
        }
        set
        {
            this.mSelectTopN = value;
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether control should be hidden if no data found.
    /// </summary>
    public bool HideControlForZeroRows
    {
        get
        {
            return this.mHideControlForZeroRows;
        }
        set
        {
            this.mHideControlForZeroRows = value;
        }
    }


    /// <summary>
    /// Gets or sets the text which is displayed for zero rows result.
    /// </summary>
    public string ZeroRowsText
    {
        get
        {
            return this.mZeroRowsText;
        }
        set
        {
            this.mZeroRowsText = value;
        }
    }


    /// <summary>
    /// Returns true if file ID query string is pressent.
    /// </summary>
    public bool? DisplayDetail
    {
        get
        {
            if (this.mDisplayDetail == null)
            {
                if (QueryHelper.GetInteger(this.FileIDQueryStringKey, 0) > 0)
                {
                    this.mDisplayDetail = true;
                }
                else
                {
                    this.mDisplayDetail = false;
                }
            }
            return mDisplayDetail;
        }
    }

    #endregion


    #region "UniPager properties"

    /// <summary>
    /// Gets or sets the unipager control.
    /// </summary>
    public UniPager UniPager
    {
        get
        {
            return this.UniPagerControl;
        }
        set
        {
            this.UniPagerControl = value;
        }
    }

    /// <summary>
    /// Gets or sets the value that indicates whether pager should be hidden for single page.
    /// </summary>
    public bool HidePagerForSinglePage
    {
        get
        {
            return this.UniPagerControl.HidePagerForSinglePage;
        }
        set
        {
            this.UniPagerControl.HidePagerForSinglePage = value;
        }
    }


    /// <summary>
    /// Gets or sets the number of records to display on a page.
    /// </summary>
    public int PageSize
    {
        get
        {
            return this.UniPagerControl.PageSize;
        }
        set
        {
            this.UniPagerControl.PageSize = value;
        }
    }


    /// <summary>
    /// Gets or sets the number of pages displayed for current page range.
    /// </summary>
    public int GroupSize
    {
        get
        {
            return this.UniPagerControl.GroupSize;
        }
        set
        {
            this.UniPagerControl.GroupSize = value;
        }
    }


    /// <summary>
    /// Gets or sets the pager mode ('querystring' or 'postback').
    /// </summary>
    public UniPagerMode PagerMode
    {
        get
        {
            return this.UniPagerControl.PagerMode;
        }
        set
        {
            UniPagerControl.PagerMode = value;
        }
    }


    /// <summary>
    /// Gets or sets the querysting parameter.
    /// </summary>
    public string QueryStringKey
    {
        get
        {
            return this.UniPagerControl.QueryStringKey;
        }
        set
        {
            UniPagerControl.QueryStringKey = value;
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether first and last item template are displayed dynamically based on current view.
    /// </summary>
    public bool DisplayFirstLastAutomatically
    {
        get
        {
            return this.UniPagerControl.DisplayFirstLastAutomatically;
        }
        set
        {
            UniPagerControl.DisplayFirstLastAutomatically = value;
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether first and last item template are displayed dynamically based on current view.
    /// </summary>
    public bool DisplayPreviousNextAutomatically
    {
        get
        {
            return this.UniPagerControl.DisplayPreviousNextAutomatically;
        }
        set
        {
            UniPagerControl.DisplayPreviousNextAutomatically = value;
        }
    }

    #endregion


    #region "UniPager Template properties"

    /// <summary>
    /// Gets or sets the pages template.
    /// </summary>
    public string PagesTemplate
    {
        get
        {
            return this.mPagesTemplate;
        }
        set
        {
            this.mPagesTemplate = value;
        }
    }


    /// <summary>
    /// Gets or sets the current page template.
    /// </summary>
    public string CurrentPageTemplate
    {
        get
        {
            return this.mCurrentPageTemplate;
        }
        set
        {
            this.mCurrentPageTemplate = value;
        }
    }


    /// <summary>
    /// Gets or sets the separator template.
    /// </summary>
    public string SeparatorTemplate
    {
        get
        {
            return this.mSeparatorTemplate;
        }
        set
        {
            this.mSeparatorTemplate = value;
        }
    }


    /// <summary>
    /// Gets or sets the first page template.
    /// </summary>
    public string FirstPageTemplate
    {
        get
        {
            return this.mFirstPageTemplate;
        }
        set
        {
            this.mFirstPageTemplate = value;
        }
    }


    /// <summary>
    /// Gets or sets the last page template.
    /// </summary>
    public string LastPageTemplate
    {
        get
        {
            return this.mLastPageTemplate;
        }
        set
        {
            this.mLastPageTemplate = value;
        }
    }


    /// <summary>
    /// Gets or sets the previous page template.
    /// </summary>
    public string PreviousPageTemplate
    {
        get
        {
            return this.mPreviousPageTemplate;
        }
        set
        {
            this.mPreviousPageTemplate = value;
        }
    }


    /// <summary>
    /// Gets or sets the next page template.
    /// </summary>
    public string NextPageTemplate
    {
        get
        {
            return this.mNextPageTemplate;
        }
        set
        {
            this.mNextPageTemplate = value;
        }
    }


    /// <summary>
    /// Gets or sets the previous group template.
    /// </summary>
    public string PreviousGroupTemplate
    {
        get
        {
            return this.mPreviousGroupTemplate;
        }
        set
        {
            this.mPreviousGroupTemplate = value;
        }
    }


    /// <summary>
    /// Gets or sets the next group template.
    /// </summary>
    public string NextGroupTemplate
    {
        get
        {
            return this.mNextGroupTemplate;
        }
        set
        {
            this.mNextGroupTemplate = value;
        }
    }


    /// <summary>
    /// Gets or sets the layout template.
    /// </summary>
    public string LayoutTemplate
    {
        get
        {
            return this.mLayoutTemplate;
        }
        set
        {
            this.mLayoutTemplate = value;
        }
    }

    #endregion


    #region "Public properties"

    /// <summary>
    /// Gets or sets the name of the transforamtion which is used for displaying the results.
    /// </summary>
    public string TransformationName
    {
        get
        {
            return this.mTransformationName;
        }
        set
        {
            this.mTransformationName = value;
        }
    }


    /// <summary>
    /// Gets or sets the name of the transforamtion which is used for displaying selected file.
    /// </summary>
    public string SelectedItemTransformation
    {
        get
        {
            return this.mSelectedItemTransformation;
        }
        set
        {
            this.mSelectedItemTransformation = value;
        }
    }


    /// <summary>
    /// Gets or sets the name of the transforamtion which is used for displaying file list header.
    /// </summary>
    public string HeaderTransformation
    {
        get
        {
            return mHeaderTransformation;
        }
        set
        {
            mHeaderTransformation = value;
        }
    }


    /// <summary>
    /// Gets or sets the name of the transforamtion which is used for displaying file list footer.
    /// </summary>
    public string FooterTransformation
    {
        get
        {
            return mFooterTransformation;
        }
        set
        {
            mFooterTransformation = value;
        }
    }


    /// <summary>
    /// Gets or sets the name of the transforamtion which is used for item separator.
    /// </summary>
    public string SeparatorTransformation
    {
        get
        {
            return mSeparatorTransformation;
        }
        set
        {
            mSeparatorTransformation = value;
        }
    }


    /// <summary>
    /// Media library name.
    /// </summary>
    public string MediaLibraryName
    {
        get
        {
            return this.mMediaLibraryName;
        }
        set
        {
            this.mMediaLibraryName = value;
        }
    }


    /// <summary>
    /// Gets or sets media library path to display files from.
    /// </summary>
    public string MediaLibraryPath
    {
        get
        {
            if (this.mMediaLibraryPath != null)
            {
                return this.mMediaLibraryPath.Trim('/');
            }
            return this.mMediaLibraryPath;
        }
        set
        {
            this.mMediaLibraryPath = value;
        }
    }


    /// <summary>
    /// Indicates if subfolders content should be displayed.
    /// </summary>
    public bool ShowSubfoldersContent
    {
        get
        {
            return this.mShowSubfoldersContent;
        }
        set
        {
            this.mShowSubfoldersContent = value;
        }
    }


    /// <summary>
    /// File list folder path.
    /// </summary>
    public string FolderPath
    {
        get
        {
            return ValidationHelper.GetString(this.ViewState["FolderPath"], "");
        }
        set
        {
            this.ViewState["FolderPath"] = value;
        }
    }


    /// <summary>
    /// Gets or sets the file id querystring parameter.
    /// </summary>
    public string FileIDQueryStringKey
    {
        get
        {
            return mFileIDQueryStringKey;
        }
        set
        {
            mFileIDQueryStringKey = value;
        }
    }


    /// <summary>
    /// Gets or sets the sort querystring parameter.
    /// </summary>
    public string SortQueryStringKey
    {
        get
        {
            return mSortQueryStringKey;
        }
        set
        {
            mSortQueryStringKey = value;
        }
    }


    /// <summary>
    /// Gets or sets the path querystring parameter.
    /// </summary>
    public string PathQueryStringKey
    {
        get
        {
            return mPathQueryStringKey;
        }
        set
        {
            mPathQueryStringKey = value;
        }
    }


    /// <summary>
    /// Gets or setsfilter method.
    /// </summary>
    public int FilterMethod
    {
        get
        {
            return mFilterMethod;
        }
        set
        {
            mFilterMethod = value;
        }
    }


    /// <summary>
    /// Media library info object.
    /// </summary>
    public MediaLibraryInfo MediaLibrary
    {
        get
        {
            if (this.mMediaLibrary == null)
            {
                this.mMediaLibrary = MediaLibraryInfoProvider.GetMediaLibraryInfo(this.MediaLibraryName, CMSContext.CurrentSiteName);
            }
            return this.mMediaLibrary;
        }
    }


    /// <summary>
    /// Hide folder tree.
    /// </summary>
    public bool HideFolderTree
    {
        get
        {
            return this.mHideFolderTree;
        }
        set
        {
            this.mHideFolderTree = value;
        }
    }


    /// <summary>
    /// Preview preffix for identification preview file.
    /// </summary>
    public string PreviewSuffix
    {
        get
        {
            return mPreviewSuffix;
        }
        set
        {
            mPreviewSuffix = value;
        }
    }


    /// <summary>
    /// Icon set name.
    /// </summary>
    public string IconSet
    {
        get
        {
            return mIconSet;
        }
        set
        {
            mIconSet = value;
        }
    }


    /// <summary>
    /// Indicates if active content (video, flash etc.) should be displayed.
    /// </summary>
    public bool DisplayActiveContent
    {
        get
        {
            return mDisplayActiveContent;
        }
        set
        {
            mDisplayActiveContent = value;
        }
    }


    /// <summary>
    /// Indicates if file count in directory should be displayed in folder tree.
    /// </summary>
    public bool DisplayFileCount
    {
        get
        {
            return mDisplayFileCount;
        }
        set
        {
            mDisplayFileCount = value;
        }
    }



    /// <summary>
    /// Indicates if file upload form should be displayed.
    /// </summary>
    public bool AllowUpload
    {
        get
        {
            return mAllowUpload;
        }
        set
        {
            mAllowUpload = value;
        }
    }


    /// <summary>
    /// Indicates if preview file upload should be displayed in upload form.
    /// </summary>
    public bool AllowUploadPreview
    {
        get
        {
            return mAllowUploadPreview;
        }
        set
        {
            mAllowUploadPreview = value;
        }
    }


    /// <summary>
    /// Indicates whether the links to media file should be processed in a secure way.
    /// </summary>
    public bool UseSecureLinks
    {
        get
        {
            return this.mUseSecureLinks;
        }
        set
        {
            this.mUseSecureLinks = value;
        }
    }

    #endregion


    #region "Life cycle methods"

    protected override void CreateChildControls()
    {
        // Hide the control if there is no MediaLibrary
        if (this.MediaLibrary == null)
        {
            hidden = true;
            this.Visible = false;
            this.StopProcessing = true;
            return;
        }

        if (this.StopProcessing)
        {
            this.folderTree.StopProcessing = true;
            this.fileDataSource.StopProcessing = true;
            this.UniPagerControl.PageControl = null;
        }
        else
        {
            if (!MediaLibraryInfoProvider.IsUserAuthorizedPerLibrary(MediaLibrary, "Read"))
            {
                // Check 'Media gallery access' permission
                if (!MediaLibraryInfoProvider.IsUserAuthorizedPerLibrary(MediaLibrary, "libraryaccess"))
                {
                    RaiseOnNotAllowed("libraryaccess");
                    return;
                }
            }

            base.CreateChildControls();
            InitializeInnerControls();
        }
    }


    protected override void OnPreRender(EventArgs e)
    {
        if (this.HideFolderTree)
        {
            this.folderTree.Visible = false;
            this.folderTreeContainer.Visible = false;
        }
        
        int fileId = QueryHelper.GetInteger(FileIDQueryStringKey, 0);
        bool hasFilter = false;
        if (fileId > 0)
        {
            hasFilter = true;
            fileDataSource.WhereCondition = "FileID = " + fileId.ToString("D", System.Globalization.CultureInfo.InvariantCulture);
            fileDataSource.OrderBy = null;
            fileDataSource.FilePath = null;
            // Hide uploader
            fileUploader.Visible = false;
        }
        else
        {
            if (MediaLibrary != null)
            {
                hasFilter = true;
                fileDataSource.OrderBy = mediaLibrarySort.OrderBy;
                fileDataSource.LibraryName = MediaLibraryName;
                fileDataSource.WhereCondition = folderTree.WhereCondition;
            }
        }
        // Bind data into fileList
        if (hasFilter)
        {
            fileList.DataSource = fileDataSource.DataSource;
            fileList.DataBind();
        }

        base.OnPreRender(e);
    }

    #endregion


    #region "Public methods"

    /// <summary>
    /// Reloads the data in the control.
    /// </summary>
    public override void ReloadData()
    {
        fileDataSource.InvalidateLoadedData();
    }

    #endregion


    #region "Private methods"

    private void FilterChanged()
    {
        // Set uploader if upload is allowed
        if (AllowUpload)
        {
            fileUploader.DestinationPath = folderTree.CurrentFolder;
        }
        fileDataSource.InvalidateLoadedData();
    }


    private void fileList_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.Controls.Count > 0)
        {
            // Try find control with id 'filePreview'
            MediaFilePreview ctrlFilePreview = e.Item.Controls[0].FindControl("filePreview") as MediaFilePreview;
            if (ctrlFilePreview != null)
            {
                //Set control
                ctrlFilePreview.IconSet = this.IconSet;
                ctrlFilePreview.PreviewSuffix = this.PreviewSuffix;
                ctrlFilePreview.UseSecureLinks = this.UseSecureLinks;
                if (this.DisplayDetail == true)
                {
                    // If showing detail show active control
                    ctrlFilePreview.DisplayActiveContent = true;
                }
                else
                {
                    ctrlFilePreview.DisplayActiveContent = this.DisplayActiveContent;
                }
            }
        }
    }


    private void InitializeInnerControls()
    {
        if (this.MediaLibrary != null)
        {
            // If the control was hidden because there were no data on init, show the control and process it
            if (hidden)
            {
                this.Visible = true;
                this.StopProcessing = false;
                this.folderTree.StopProcessing = false;
                this.fileDataSource.StopProcessing = false;
            }

            if (string.IsNullOrEmpty(this.MediaLibraryPath))
            {
                // If there is no path set
                this.folderTree.RootFolderPath = MediaLibraryHelper.GetMediaRootFolderPath(CMSContext.CurrentSiteName);
                this.folderTree.MediaLibraryFolder = this.MediaLibrary.LibraryFolder;
            }
            else
            {
                // Set root folder with library path
                this.folderTree.RootFolderPath = MediaLibraryHelper.GetMediaRootFolderPath(CMSContext.CurrentSiteName) + this.MediaLibrary.LibraryFolder;
                this.folderTree.MediaLibraryFolder = Path.GetFileName(this.MediaLibraryPath);
                this.folderTree.MediaLibraryPath = MediaLibraryHelper.EnsurePath(this.MediaLibraryPath);
            }

            this.folderTree.FileIDQueryStringKey = this.FileIDQueryStringKey;
            this.folderTree.PathQueryStringKey = this.PathQueryStringKey;
            this.folderTree.FilterMethod = this.FilterMethod;
            this.folderTree.ShowSubfoldersContent = this.ShowSubfoldersContent;
            this.folderTree.DisplayFileCount = this.DisplayFileCount;

            // Get media file id from query
            this.mMediaFileID = QueryHelper.GetInteger(this.FileIDQueryStringKey, 0);

            // Media library sort
            this.mediaLibrarySort.OnFilterChanged += new ActionEventHandler(FilterChanged);
            this.mediaLibrarySort.FileIDQueryStringKey = this.FileIDQueryStringKey;
            this.mediaLibrarySort.SortQueryStringKey = this.SortQueryStringKey;
            this.mediaLibrarySort.FilterMethod = this.FilterMethod;

            // File upload properties
            this.fileUploader.Visible = this.AllowUpload;
            this.fileUploader.EnableUploadPreview = this.AllowUploadPreview;
            this.fileUploader.PreviewSuffix = this.PreviewSuffix;
            this.fileUploader.LibraryID = this.MediaLibrary.LibraryID;
            this.fileUploader.DestinationPath = this.folderTree.SelectedPath;
            this.fileUploader.OnNotAllowed += new NotAllowedEventHandler(fileUploader_OnNotAllowed);
            this.fileUploader.OnAfterFileUpload += new CMSModules_MediaLibrary_Controls_LiveControls_MediaFileUploader.OnAfterFileUploadEventHandler(fileUploader_OnAfterFileUpload);

            // Data properties
            this.fileDataSource.TopN = this.SelectTopN;
            this.fileDataSource.SiteName = CMSContext.CurrentSiteName;
            this.fileDataSource.GroupID = this.MediaLibrary.LibraryGroupID;

            // UniPager properties
            this.UniPagerControl.PageSize = this.PageSize;
            this.UniPagerControl.GroupSize = this.GroupSize;
            this.UniPagerControl.QueryStringKey = this.QueryStringKey;
            this.UniPagerControl.DisplayFirstLastAutomatically = this.DisplayFirstLastAutomatically;
            this.UniPagerControl.DisplayPreviousNextAutomatically = this.DisplayPreviousNextAutomatically;
            this.UniPagerControl.HidePagerForSinglePage = this.HidePagerForSinglePage;
            this.UniPagerControl.PagerMode = this.PagerMode;

            this.mMediaLibraryRoot = MediaLibraryHelper.GetMediaRootFolderPath(CMSContext.CurrentSiteName) + this.MediaLibrary.LibraryFolder;
            this.mMediaLibraryUrl = URLHelper.GetAbsoluteUrl("~/" + CMSContext.CurrentSiteName + "/media/" + this.MediaLibrary.LibraryFolder);

            // List properties
            this.fileList.HideControlForZeroRows = this.HideControlForZeroRows;
            this.fileList.ZeroRowsText = this.ZeroRowsText;
            this.fileList.ItemDataBound += new RepeaterItemEventHandler(fileList_ItemDataBound);

            // Initialize templates for FileList and UniPager
            InitTemplates();
        }

        // Append filter changed evet if folder is hidden or path query string id is set
        if (!HideFolderTree || !String.IsNullOrEmpty(PathQueryStringKey))
        {
            this.folderTree.OnFilterChanged += new ActionEventHandler(FilterChanged);
        }

        // Folder tree
        if (!this.HideFolderTree)
        {
            if (CultureHelper.IsPreferredCultureRTL())
            {
                this.folderTree.ImageFolderPath = GetImageUrl("RTL/Design/Controls/Tree", IsLiveSite, true);
            }
            else
            {
                this.folderTree.ImageFolderPath = GetImageUrl("Design/Controls/Tree", IsLiveSite, true);
            }
        }
    }


    private void InitTemplates()
    {
        // If is media file id sets use SelectedItemTransformation and hide paging and sorting
        if (this.mMediaFileID > 0)
        {
            this.fileList.ItemTemplate = CMSDataProperties.LoadTransformation(this, this.SelectedItemTransformation, false);
            this.UniPagerControl.Visible = false;
            this.mediaLibrarySort.StopProcessing = true;
            this.mediaLibrarySort.Visible = false;
        }
        else
        {
            // Else use transformation name
            this.fileList.ItemTemplate = CMSDataProperties.LoadTransformation(this, this.TransformationName, false);
        }

        if (!String.IsNullOrEmpty(this.HeaderTransformation))
        {
            this.fileList.HeaderTemplate = CMSDataProperties.LoadTransformation(this, this.HeaderTransformation, false);
        }

        if (!String.IsNullOrEmpty(this.FooterTransformation))
        {
            this.fileList.FooterTemplate = CMSDataProperties.LoadTransformation(this, this.FooterTransformation, false);
        }

        if (!String.IsNullOrEmpty(this.SeparatorTransformation))
        {
            this.fileList.SeparatorTemplate = CMSDataProperties.LoadTransformation(this, this.SeparatorTransformation, false);
        }

        if (!String.IsNullOrEmpty(this.PagesTemplate))
        {
            this.UniPagerControl.PageNumbersTemplate = CMSDataProperties.LoadTransformation(this.UniPagerControl, this.PagesTemplate, false);
        }

        if (!String.IsNullOrEmpty(this.CurrentPageTemplate))
        {
            this.UniPagerControl.CurrentPageTemplate = CMSDataProperties.LoadTransformation(this.UniPagerControl, this.CurrentPageTemplate, false);
        }

        if (!String.IsNullOrEmpty(this.SeparatorTemplate))
        {
            this.UniPagerControl.PageNumbersSeparatorTemplate = CMSDataProperties.LoadTransformation(this.UniPagerControl, this.SeparatorTemplate, false);
        }

        if (!String.IsNullOrEmpty(this.FirstPageTemplate))
        {
            this.UniPagerControl.FirstPageTemplate = CMSDataProperties.LoadTransformation(this.UniPagerControl, this.FirstPageTemplate, false);
        }

        if (!String.IsNullOrEmpty(this.LastPageTemplate))
        {
            this.UniPagerControl.LastPageTemplate = CMSDataProperties.LoadTransformation(this.UniPagerControl, this.LastPageTemplate, false);
        }

        if (!String.IsNullOrEmpty(this.PreviousPageTemplate))
        {
            this.UniPagerControl.PreviousPageTemplate = CMSDataProperties.LoadTransformation(this.UniPagerControl, this.PreviousPageTemplate, false);
        }

        if (!String.IsNullOrEmpty(this.NextPageTemplate))
        {
            this.UniPagerControl.NextPageTemplate = CMSDataProperties.LoadTransformation(this.UniPagerControl, this.NextPageTemplate, false);
        }

        if (!String.IsNullOrEmpty(this.PreviousGroupTemplate))
        {
            this.UniPagerControl.PreviousGroupTemplate = CMSDataProperties.LoadTransformation(this.UniPagerControl, this.PreviousGroupTemplate, false);
        }

        if (!String.IsNullOrEmpty(this.NextGroupTemplate))
        {
            this.UniPagerControl.NextGroupTemplate = CMSDataProperties.LoadTransformation(this.UniPagerControl, this.NextGroupTemplate, false);
        }

        if (!String.IsNullOrEmpty(this.LayoutTemplate))
        {
            this.UniPagerControl.LayoutTemplate = CMSDataProperties.LoadTransformation(this.UniPagerControl, this.LayoutTemplate, false);
        }
    }


    private void fileUploader_OnNotAllowed(string permissionType, CMSAdminControl sender)
    {
        RaiseOnNotAllowed(permissionType);
    }


    private void fileUploader_OnAfterFileUpload()
    {
        fileDataSource.InvalidateLoadedData();
    }

    #endregion
}
