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

using CMS.PortalControls;
using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.UIControls;
using CMS.Controls;
using CMS.MediaLibrary;

public partial class CMSWebParts_MediaLibrary_MediaGalleryFileList : CMSAbstractWebPart
{
    #region "Variables"

    // Indicates whether control was binded
    private bool binded = false;
    // Datasource instance
    private CMSBaseDataSource mDataSourceControl = null;

    private string mMediaLibraryRoot = null;
    private string mMediaLibraryUrl = null;
    private bool? mDisplayDetail = null;

    #endregion


    #region "Properties"

    /// <summary>
    /// Gets or sets the name of the media library name.
    /// </summary>
    public string MediaLibraryName
    {
        get
        {
            string libraryName = ValidationHelper.GetString(this.GetValue("MediaLibraryName"), "");
            if ((string.IsNullOrEmpty(libraryName) || libraryName == MediaLibraryInfoProvider.CURRENT_LIBRARY) && (MediaLibraryContext.CurrentMediaLibrary != null))
            {
                return MediaLibraryContext.CurrentMediaLibrary.LibraryName;
            }
            return libraryName;
        }
        set
        {
            this.SetValue("MediaLibraryName", value);
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
        }
    }


    /// <summary>
    /// Gets or sets the name of the transforamtion which is used for displaying the results.
    /// </summary>
    public string TransformationName
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("TransformationName"), String.Empty);
        }
        set
        {
            this.SetValue("TransformationName", value);
        }
    }


    /// <summary>
    /// Gets or sets the name of the transforamtion which is used for displaying selected file.
    /// </summary>
    public string SelectedItemTransformationName
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("SelectedItemTransformationName"), String.Empty);
        }
        set
        {
            this.SetValue("SelectedItemTransformationName", value);
        }
    }


    /// <summary>
    /// Gets or sets FooterTemplate property.
    /// </summary>
    public string FooterTransformationName
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("FooterTransformationName"), String.Empty);
        }
        set
        {
            this.SetValue("FooterTransformationName", value);
        }
    }


    /// <summary>
    /// Gets or sets HeaderTemplate property.
    /// </summary>
    public string HeaderTransformationName
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("HeaderTransformationName"), String.Empty);
        }
        set
        {
            this.SetValue("HeaderTransformationName", value);
        }
    }


    /// <summary>
    /// Gets or sets SeparatorTemplate property.
    /// </summary>
    public string SeparatorTransformationName
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("SeparatorTransformationName"), String.Empty);
        }
        set
        {
            this.SetValue("SeparatorTransformationName", value);
        }
    }


    /// <summary>
    /// Gets or sets HideControlForZeroRows property.
    /// </summary>
    public bool HideControlForZeroRows
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("HideControlForZeroRows"), true);
        }
        set
        {
            this.SetValue("HideControlForZeroRows", value);
        }
    }


    /// <summary>
    /// Gets or sets ZeroRowsText property.
    /// </summary>
    public string ZeroRowsText
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("ZeroRowsText"), String.Empty);
        }
        set
        {
            this.SetValue("ZeroRowsText", value);
        }
    }


    /// <summary>
    /// Gets or sets the name of the transforamtion which is used for displaying selected file.
    /// </summary>
    public string DataSourceName
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("DataSourceName"), String.Empty);
        }
        set
        {
            this.SetValue("DataSourceName", value);
        }
    }


    /// <summary>
    /// Control with data source.
    /// </summary>
    public CMSBaseDataSource DataSourceControl
    {
        get
        {
            // Check if control is empty and load it with the data
            if (this.mDataSourceControl == null)
            {
                if (!String.IsNullOrEmpty(this.DataSourceName))
                {
                    this.mDataSourceControl = CMSControlsHelper.GetFilter(this.DataSourceName) as CMSBaseDataSource;
                }
            }

            return this.mDataSourceControl;
        }
        set
        {
            this.mDataSourceControl = value;
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
        }
    }


    /// <summary>
    /// Indicates if active content (video, flash etc.) should be displayed.
    /// </summary>
    public bool DisplayActiveContent
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("DisplayActiveContent"), false);
        }
        set
        {
            this.SetValue("DisplayActiveContent", value);
        }
    }

    #endregion


    /// <summary>
    /// OnContentLoaded override.
    /// </summary>
    public override void OnContentLoaded()
    {
        base.OnContentLoaded();
        SetupControl();
    }


    /// <summary>
    /// Page_Load event handler.
    /// </summary>
    protected void Page_Load(object sender, EventArgs e)
    {
        SetupControlLoad();
    }


    /// <summary>
    /// OnPreRender override.
    /// </summary>
    protected override void OnPreRender(EventArgs e)
    {
        if ((this.DataSourceControl != null) && (!DataHelper.DataSourceIsEmpty(this.DataSourceControl.DataSource)) && (!binded))
        {
            this.repItems.DataSource = this.DataSourceControl.DataSource;
            this.repItems.DataBind();
        }

        base.OnPreRender(e);
    }


    /// <summary>
    /// Initializes the control properties.
    /// </summary>
    protected void SetupControlLoad()
    {
        // Handle filter change event
        if (this.DataSourceControl != null)
        {
            this.DataSourceControl.OnFilterChanged += new ActionEventHandler(DataSourceControl_OnFilterChanged);
        }

        MediaLibraryInfo mli = MediaLibraryInfoProvider.GetMediaLibraryInfo(this.MediaLibraryName, CMSContext.CurrentSiteName);
        if (mli != null)
        {
            this.mMediaLibraryRoot = MediaLibraryHelper.GetMediaRootFolderPath(CMSContext.CurrentSiteName) + mli.LibraryFolder;
            this.mMediaLibraryUrl = URLHelper.GetAbsoluteUrl("~/" + CMSContext.CurrentSiteName + "/media/" + mli.LibraryFolder);
        }
        this.repItems.ItemDataBound += new RepeaterItemEventHandler(repItems_ItemDataBound);
    }


    /// <summary>
    /// Initializes the control properties.
    /// </summary>
    protected void SetupControl()
    {
        if (this.StopProcessing)
        {
            // Do nothing
        }
        else
        {

            MediaLibraryInfo mli = MediaLibraryInfoProvider.GetMediaLibraryInfo(this.MediaLibraryName, CMSContext.CurrentSiteName);

            if (mli != null)
            {
                // If dont have 'Manage' permission
                if (!MediaLibraryInfoProvider.IsUserAuthorizedPerLibrary(mli, "read"))
                {
                    // Check 'File create' permission
                    if (!MediaLibraryInfoProvider.IsUserAuthorizedPerLibrary(mli, "libraryaccess"))
                    {
                        repItems.Visible = false;

                        messageElem.ErrorMessage = MediaLibraryHelper.GetAccessDeniedMessage("libraryaccess");
                        messageElem.DisplayMessage = true;
                        return;
                    }
                }

                int fid = QueryHelper.GetInteger(this.FileIDQueryStringKey, 0);
                if (fid > 0)
                {
                    if (!String.IsNullOrEmpty(this.SelectedItemTransformationName))
                    {
                        this.repItems.ItemTemplate = CMSDataProperties.LoadTransformation(this, this.SelectedItemTransformationName, false);
                    }
                }
                else
                {
                    if (!String.IsNullOrEmpty(this.TransformationName))
                    {
                        this.repItems.ItemTemplate = CMSDataProperties.LoadTransformation(this, this.TransformationName, false);
                    }
                }
                if (!String.IsNullOrEmpty(this.FooterTransformationName))
                {
                    this.repItems.FooterTemplate = CMSDataProperties.LoadTransformation(this, this.FooterTransformationName, false);
                }
                if (!String.IsNullOrEmpty(this.HeaderTransformationName))
                {
                    this.repItems.HeaderTemplate = CMSDataProperties.LoadTransformation(this, this.HeaderTransformationName, false);
                }
                if (!String.IsNullOrEmpty(this.SeparatorTransformationName))
                {
                    this.repItems.SeparatorTemplate = CMSDataProperties.LoadTransformation(this, this.SeparatorTransformationName, false);
                }

                this.repItems.DataBindByDefault = false;
                this.repItems.OnPageChanged += new EventHandler<EventArgs>(repItems_OnPageChanged);

                // Add repeater to the filter collection
                CMSControlsHelper.SetFilter(ValidationHelper.GetString(this.GetValue("WebPartControlID"), this.ClientID), this.repItems);

                this.repItems.HideControlForZeroRows = this.HideControlForZeroRows;
                this.repItems.ZeroRowsText = this.ZeroRowsText;
            }
        }
    }


    /// <summary>
    /// OnFilterChanged event handler.
    /// </summary>
    void DataSourceControl_OnFilterChanged()
    {
        // Reload data
        if (this.DataSourceControl != null)
        {
            this.repItems.DataSource = this.DataSourceControl.DataSource;
            this.repItems.DataBind();
            binded = true;
        }
    }


    void repItems_OnPageChanged(object sender, EventArgs e)
    {
        // Reload data
        if (this.DataSourceControl != null)
        {
            this.repItems.DataSource = this.DataSourceControl.DataSource;
            this.repItems.DataBind();
            binded = true;
        }
    }


    void repItems_ItemDataBound(object sender, RepeaterItemEventArgs e)
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


    /// <summary>
    /// Reload data.
    /// </summary>
    public override void ReloadData()
    {
        base.ReloadData();
        SetupControl();
        SetupControlLoad();
        this.repItems.ReloadData(true);
    }
}
