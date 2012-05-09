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
using CMS.PortalControls;
using CMS.Controls;
using CMS.MediaLibrary;

public partial class CMSWebParts_MediaLibrary_MediaGallery : CMSAbstractWebPart
{
    #region "Content properties"

    /// <summary>
    /// Gets or sets the number which indicates how many files should be displayed.
    /// </summary>
    public int SelectTopN
    {
        get
        {
            return ValidationHelper.GetInteger(this.GetValue("SelectTopN"), this.gallery.SelectTopN);
        }
        set
        {
            this.SetValue("SelectTopN", value);
            this.gallery.SelectTopN = value;
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether control should be hidden if no data found.
    /// </summary>
    public bool HideControlForZeroRows
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("HideControlForZeroRows"), this.gallery.HideControlForZeroRows);
        }
        set
        {
            this.SetValue("HideControlForZeroRows", value);
            this.gallery.HideControlForZeroRows = value;
        }
    }


    /// <summary>
    /// Gets or sets the text which is displayed for zero rows result.
    /// </summary>
    public string ZeroRowsText
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("ZeroRowsText"), this.gallery.ZeroRowsText);
        }
        set
        {
            this.SetValue("ZeroRowsText", value);
            this.gallery.ZeroRowsText = value;
        }
    }

    #endregion


    #region "UniPager properties"

    /// <summary>
    /// Gets or sets the value that indicates whether pager should be hidden for single page.
    /// </summary>
    public bool HidePagerForSinglePage
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("HidePagerForSinglePage"), this.gallery.HidePagerForSinglePage);
        }
        set
        {
            this.SetValue("HidePagerForSinglePage", value);
            this.gallery.HidePagerForSinglePage = value;
        }
    }


    /// <summary>
    /// Gets or sets the number of records to display on a page.
    /// </summary>
    public int PageSize
    {
        get
        {
            return ValidationHelper.GetInteger(this.GetValue("PageSize"), this.gallery.PageSize);
        }
        set
        {
            this.SetValue("PageSize", value);
            this.gallery.PageSize = value;
        }
    }


    /// <summary>
    /// Gets or sets the number of pages displayed for current page range.
    /// </summary>
    public int GroupSize
    {
        get
        {
            return ValidationHelper.GetInteger(this.GetValue("GroupSize"), this.gallery.GroupSize);
        }
        set
        {
            this.SetValue("GroupSize", value);
            this.gallery.GroupSize = value;
        }
    }


    /// <summary>
    /// Gets or sets the querysting parameter.
    /// </summary>
    public string QueryStringKey
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("QueryStringKey"), this.gallery.QueryStringKey);
        }
        set
        {
            this.SetValue("QueryStringKey", value);
            this.gallery.QueryStringKey = value;
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether first and last item template are displayed dynamically based on current view.
    /// </summary>
    public bool DisplayFirstLastAutomatically
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("DisplayFirstLastAutomatically"), this.gallery.DisplayFirstLastAutomatically);
        }
        set
        {
            this.SetValue("DisplayFirstLastAutomatically", value);
            this.gallery.DisplayFirstLastAutomatically = value;
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether first and last item template are displayed dynamically based on current view.
    /// </summary>
    public bool DisplayPreviousNextAutomatically
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("DisplayPreviousNextAutomatically"), this.gallery.DisplayPreviousNextAutomatically);
        }
        set
        {
            this.SetValue("DisplayPreviousNextAutomatically", value);
            this.gallery.DisplayPreviousNextAutomatically = value;
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
            return ValidationHelper.GetString(this.GetValue("Pages"), "");
        }
        set
        {
            this.SetValue("Pages", value);
        }
    }


    /// <summary>
    /// Gets or sets the current page template.
    /// </summary>
    public string CurrentPageTemplate
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("CurrentPage"), "");
        }
        set
        {
            this.SetValue("CurrentPage", value);
        }
    }


    /// <summary>
    /// Gets or sets the separator template.
    /// </summary>
    public string SeparatorTemplate
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("PageSeparator"), "");
        }
        set
        {
            this.SetValue("PageSeparator", value);
            gallery.SeparatorTemplate = value;
        }
    }


    /// <summary>
    /// Gets or sets the first page template.
    /// </summary>
    public string FirstPageTemplate
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("FirstPage"), "");
        }
        set
        {
            this.SetValue("FirstPage", value);
        }
    }


    /// <summary>
    /// Gets or sets the last page template.
    /// </summary>
    public string LastPageTemplate
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("LastPage"), "");
        }
        set
        {
            this.SetValue("LastPage", value);
        }
    }


    /// <summary>
    /// Gets or sets the previous page template.
    /// </summary>
    public string PreviousPageTemplate
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("PreviousPage"), "");
        }
        set
        {
            this.SetValue("PreviousPage", value);
        }
    }


    /// <summary>
    /// Gets or sets the next page template.
    /// </summary>
    public string NextPageTemplate
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("NextPage"), "");
        }
        set
        {
            this.SetValue("NextPage", value);
        }
    }


    /// <summary>
    /// Gets or sets the previous group template.
    /// </summary>
    public string PreviousGroupTemplate
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("PreviousGroup"), "");
        }
        set
        {
            this.SetValue("PreviousGroup", value);
        }
    }


    /// <summary>
    /// Gets or sets the next group template.
    /// </summary>
    public string NextGroupTemplate
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("NextGroup"), "");
        }
        set
        {
            this.SetValue("NextGroup", value);
        }
    }


    /// <summary>
    /// Gets or sets the layout template.
    /// </summary>
    public string LayoutTemplate
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("PagerLayout"), "");
        }
        set
        {
            this.SetValue("PagerLayout", value);
            gallery.LayoutTemplate = value;
        }
    }

    #endregion


    #region "Library properties"

    /// <summary>
    /// Gets or sets the name of the transforamtion which is used for displaying the results.
    /// </summary>
    public string TransformationName
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("TransformationName"), this.gallery.TransformationName);
        }
        set
        {
            this.SetValue("TransformationName", value);
            this.gallery.TransformationName = value;
        }
    }


    /// <summary>
    /// Gets or sets the name of the transforamtion which is used for displaying selected file.
    /// </summary>
    public string SelectedItemTransformation
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("SelectedItemTransformation"), this.gallery.SelectedItemTransformation);
        }
        set
        {
            this.SetValue("SelectedItemTransformation", value);
            this.gallery.SelectedItemTransformation = value;
        }
    }


    /// <summary>
    /// Gets or sets the name of the transforamtion which is used for displaying file list header.
    /// </summary>
    public string HeaderTransformation
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("HeaderTransformation"), this.gallery.HeaderTransformation);
        }
        set
        {
            this.SetValue("HeaderTransformation", value);
            this.gallery.HeaderTransformation = value;
        }
    }


    /// <summary>
    /// Gets or sets the name of the transforamtion which is used for displaying file list footer.
    /// </summary>
    public string FooterTransformation
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("FooterTransformation"), this.gallery.FooterTransformation);
        }
        set
        {
            this.SetValue("FooterTransformation", value);
            this.gallery.FooterTransformation = value;
        }
    }


    /// <summary>
    /// Gets or sets the name of the transforamtion which is used for item separator.
    /// </summary>
    public string SeparatorTransformation
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("SeparatorTransformation"), this.gallery.SeparatorTransformation);
        }
        set
        {
            this.SetValue("SeparatorTransformation", value);
            this.gallery.SeparatorTransformation = value;
        }
    }


    /// <summary>
    /// Gets or sets media library path to display files from.
    /// </summary>
    public string MediaLibraryPath
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("MediaLibraryPath"), this.gallery.MediaLibraryPath);
        }
        set
        {
            this.SetValue("MediaLibraryPath", value);
            this.gallery.MediaLibraryPath = value;
        }
    }


    /// <summary>
    /// Gets or sets media library name.
    /// </summary>
    public string MediaLibraryName
    {
        get
        {
            string libraryName = ValidationHelper.GetString(this.GetValue("MediaLibraryName"), this.gallery.MediaLibraryName);
            if ((string.IsNullOrEmpty(libraryName) || libraryName == MediaLibraryInfoProvider.CURRENT_LIBRARY) && (MediaLibraryContext.CurrentMediaLibrary != null))
            {
                return MediaLibraryContext.CurrentMediaLibrary.LibraryName;
            }
            return libraryName;
        }
        set
        {
            this.SetValue("MediaLibraryName", value);
            this.gallery.MediaLibraryName = value;
        }
    }


    /// <summary>
    /// Hide folder tree.
    /// </summary>
    public bool HideFolderTree
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("HideFolderTree"), this.gallery.HideFolderTree);
        }
        set
        {
            this.SetValue("HideFolderTree", value);
            this.gallery.HideFolderTree = value;
        }
    }


    /// <summary>
    /// Indicates whether the links to media file should be processed in a secure way.
    /// </summary>
    public bool UseSecureLinks
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("UseSecureLinks"), true);
        }
        set
        {
            this.SetValue("UseSecureLinks", value);
            this.gallery.UseSecureLinks = value;
        }
    }


    /// <summary>
    /// Gets or sets media library name.
    /// </summary>
    public int FilterMethod
    {
        get
        {
            return ValidationHelper.GetInteger(this.GetValue("FilterMethod"), this.gallery.FilterMethod);
        }
        set
        {
            this.SetValue("FilterMethod", value);
            this.gallery.FilterMethod = value;
        }
    }

    /// <summary>
    /// Gets or sets the file id querysting parameter.
    /// </summary>
    public string FileIDQueryStringKey
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("FileIDQueryStringKey"), String.Empty);
        }
        set
        {
            this.SetValue("FileIDQueryStringKey", value);
            this.gallery.FileIDQueryStringKey = value;
        }
    }


    /// <summary>
    /// Gets or sets the sort querysting parameter.
    /// </summary>
    public string SortQueryStringKey
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("SortQueryStringKey"), String.Empty);
        }
        set
        {
            this.SetValue("SortQueryStringKey", value);
            this.gallery.SortQueryStringKey = value;
        }
    }


    /// <summary>
    /// Gets or sets the path querysting parameter.
    /// </summary>
    public string PathQueryStringKey
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("PathQueryStringKey"), String.Empty);
        }
        set
        {
            this.SetValue("PathQueryStringKey", value);
            this.gallery.PathQueryStringKey = value;
        }
    }


    /// <summary>
    /// Preview preffix for identification preview file.
    /// </summary>
    public string PreviewSuffix
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("PreviewSuffix"), String.Empty);
        }
        set
        {
            this.SetValue("PreviewSuffix", value);
            this.gallery.PreviewSuffix = value;
        }
    }


    /// <summary>
    /// Icon set name.
    /// </summary>
    public string IconSet
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("IconSet"), String.Empty);
        }
        set
        {
            this.SetValue("IconSet", value);
            this.gallery.IconSet = value;
        }
    }


    /// <summary>
    /// Indicates if active content (video, flash etc.) should be displayed.
    /// </summary>
    public bool DisplayActiveContent
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("DisplayActiveContent"), this.gallery.DisplayActiveContent);
        }
        set
        {
            this.SetValue("DisplayActiveContent", value);
            this.gallery.DisplayActiveContent = value;
        }
    }


    /// <summary>
    /// Indicates if subfolders content should be displayed.
    /// </summary>
    public bool ShowSubfoldersContent
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("ShowSubfoldersContent"), this.gallery.ShowSubfoldersContent);
        }
        set
        {
            this.SetValue("ShowSubfoldersContent", value);
            this.gallery.ShowSubfoldersContent = value;
        }
    }


    /// <summary>
    /// Indicates if files count should be displayed.
    /// </summary>
    public bool DisplayFileCount
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("DisplayFileCount"), this.gallery.DisplayFileCount);
        }
        set
        {
            this.SetValue("DisplayFileCount", value);
            this.gallery.DisplayFileCount = value;
        }
    }


    /// <summary>
    /// Indicates if file upload form should be displayed.
    /// </summary>
    public bool AllowUpload
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("AllowUpload"), this.gallery.AllowUpload);
        }
        set
        {
            this.SetValue("AllowUpload", value);
            this.gallery.AllowUpload = value;
        }
    }


    /// <summary>
    /// Indicates if preview file upload should be displayed in upload form.
    /// </summary>
    public bool AllowUploadPreview
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("AllowUploadPreview"), this.gallery.AllowUploadPreview);
        }
        set
        {
            this.SetValue("AllowUploadPreview", value);
            this.gallery.AllowUploadPreview = value;
        }
    }


    #endregion


    public override void OnContentLoaded()
    {
        base.OnContentLoaded();
        SetupControl();
    }


    /// <summary>
    /// Initializes the control properties.
    /// </summary>
    protected void SetupControl()
    {
        this.gallery.OnNotAllowed += new CMSAdminControl.NotAllowedEventHandler(gallery_OnNotAllowed);

        // Library properties
        this.gallery.MediaLibraryName = this.MediaLibraryName;
        this.gallery.MediaLibraryPath = this.MediaLibraryPath;
        this.gallery.TransformationName = this.TransformationName;
        this.gallery.SelectedItemTransformation = this.SelectedItemTransformation;
        this.gallery.HeaderTransformation = this.HeaderTransformation;
        this.gallery.FooterTransformation = this.FooterTransformation;
        this.gallery.SeparatorTransformation = this.SeparatorTransformation;
        this.gallery.HideFolderTree = this.HideFolderTree;
        this.gallery.PreviewSuffix = this.PreviewSuffix;
        this.gallery.IconSet = this.IconSet;
        this.gallery.DisplayActiveContent = this.DisplayActiveContent;
        this.gallery.DisplayFileCount = this.DisplayFileCount;
        this.gallery.ShowSubfoldersContent = this.ShowSubfoldersContent;
        this.gallery.ZeroRowsText = this.ZeroRowsText;
        this.gallery.HideControlForZeroRows = this.HideControlForZeroRows;
        this.gallery.UseSecureLinks = this.UseSecureLinks;

        // Filters properties
        this.gallery.FilterMethod = this.FilterMethod;
        this.gallery.FileIDQueryStringKey = this.FileIDQueryStringKey;
        this.gallery.PathQueryStringKey = this.PathQueryStringKey;
        this.gallery.SortQueryStringKey = this.SortQueryStringKey;

        // Content properties
        this.gallery.SelectTopN = this.SelectTopN;

        // Uploader properties
        this.gallery.AllowUpload = this.AllowUpload;
        this.gallery.AllowUploadPreview = this.AllowUploadPreview;

        // UniPager properties
        this.gallery.PageSize = this.PageSize;
        this.gallery.GroupSize = this.GroupSize;
        this.gallery.QueryStringKey = this.QueryStringKey;
        this.gallery.DisplayFirstLastAutomatically = this.DisplayFirstLastAutomatically;
        this.gallery.DisplayPreviousNextAutomatically = this.DisplayPreviousNextAutomatically;
        this.gallery.HidePagerForSinglePage = this.HidePagerForSinglePage;

        switch (this.FilterMethod)
        {
            case 1:
                this.gallery.PagerMode = UniPagerMode.PostBack;
                break;

            default:
                this.gallery.PagerMode = UniPagerMode.Querystring;
                break;
        }

        // UniPager template properties
        this.gallery.PagesTemplate = this.PagesTemplate;
        this.gallery.CurrentPageTemplate = this.CurrentPageTemplate;
        this.gallery.SeparatorTemplate = this.SeparatorTemplate;
        this.gallery.FirstPageTemplate = this.FirstPageTemplate;
        this.gallery.LastPageTemplate = this.LastPageTemplate;
        this.gallery.PreviousPageTemplate = this.PreviousPageTemplate;
        this.gallery.NextPageTemplate = this.NextPageTemplate;
        this.gallery.PreviousGroupTemplate = this.PreviousGroupTemplate;
        this.gallery.NextGroupTemplate = this.NextGroupTemplate;
        this.gallery.LayoutTemplate = this.LayoutTemplate;
    }


    private void gallery_OnNotAllowed(string permissionType, CMSAdminControl sender)
    {
        if (sender != null)
        {
            sender.StopProcessing = true;
        }

        gallery.StopProcessing = true;
        gallery.Visible = false;
        gallery.UniPager.PageControl = null;
        messageElem.ErrorMessage = MediaLibraryHelper.GetAccessDeniedMessage(permissionType);
        messageElem.DisplayMessage = true;
    }


    /// <summary>
    /// Reload data.
    /// </summary>
    public override void ReloadData()
    {
        base.ReloadData();
        this.SetupControl();
        this.gallery.ReloadData();
    }
}
