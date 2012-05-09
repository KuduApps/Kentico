using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

using CMS.UIControls;
using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.MediaLibrary;
using CMS.SiteProvider;
using CMS.IO;
using CMS.SettingsProvider;


public partial class CMSModules_MediaLibrary_Controls_MediaLibrary_MediaFileSingleImport : CMSAdminControl
{
    #region "Event & delegates"

    /// <summary>
    /// Delegate used to describe handler of the event required on new file being saved.
    /// </summary>
    /// <param name="file">Info on saved file</param>
    /// <param name="title">New file title</param>
    /// <param name="desc">New file description</param>
    /// <param name="name">New file name</param>
    /// <param name="filePath">Path to the file physical location</param>
    public delegate MediaFileInfo OnSaveRequired(FileInfo file, string title, string desc, string name, string filePath);


    /// <summary>
    /// Event fired when new file should be saved.
    /// </summary>
    public event OnSaveRequired SaveRequired;


    /// <summary>
    /// Event fired after saved succeeded.
    /// </summary>
    public event OnActionEventHandler Action;

    #endregion


    #region "Private variables"

    private SiteInfo mLibrarySiteInfo = null;
    private MediaLibraryInfo mLibraryInfo = null;
    private bool mErrorOccurred = false;

    #endregion


    #region "Public properties"

    /// <summary>
    /// Gets or sets library info.
    /// </summary>
    public MediaLibraryInfo LibraryInfo
    {
        get
        {
            if ((this.mLibraryInfo == null) && (this.LibraryID > 0))
            {
                this.mLibraryInfo = MediaLibraryInfoProvider.GetMediaLibraryInfo(this.LibraryID);
            }
            return this.mLibraryInfo;
        }
        set
        {
            this.mLibraryInfo = value;
        }
    }


    /// <summary>
    /// ID of the library file is imported to.
    /// </summary>
    public int LibraryID
    {
        get
        {
            return ValidationHelper.GetInteger(ViewState["LibraryID"], 0);
        }
        set
        {
            ViewState["LibraryID"] = value;
            this.LibraryInfo = null;
            this.LibrarySiteInfo = null;
        }
    }


    /// <summary>
    /// Gets or sets library info.
    /// </summary>
    public SiteInfo LibrarySiteInfo
    {
        get
        {
            if ((this.mLibrarySiteInfo == null) && (this.LibraryInfo != null))
            {
                this.mLibrarySiteInfo = SiteInfoProvider.GetSiteInfo(this.LibraryInfo.LibrarySiteID);
            }
            return this.mLibrarySiteInfo;
        }
        set
        {
            this.mLibrarySiteInfo = value;
        }
    }


    /// <summary>
    /// Current file path.
    /// </summary>
    public string FilePath
    {
        get
        {
            return ValidationHelper.GetString(ViewState["FilePath"], "");
        }
        set
        {
            ViewState["FilePath"] = value;
        }
    }


    /// <summary>
    /// Indicates whether the error occurred during new file processing.
    /// </summary>
    public bool ErrorOccurred
    {
        get
        {
            return this.mErrorOccurred;
        }
        private set
        {
            this.mErrorOccurred = value;
        }
    }

    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.StopProcessing)
        {
            this.Visible = false;
        }
        else
        {
            SetupTexts();
        }
    }


    /// <summary>
    /// Initializes nested controls.
    /// </summary>
    public void SetupTexts()
    {
        // Initialize labels
        this.lblNewInfo.Text = GetString("media.file.infonew");
        this.imgNewInfo.ImageUrl = ResolveUrl(GetImageUrl("Others/Messages/warning.png", this.IsLiveSite));
        this.imgNewInfo.AlternateText = GetString("media.file.infonew");
        this.rfvNewFileName.ErrorMessage = GetString("general.requiresvalue");
        this.tabImport.HeaderText = GetString("media.file.import");
    }


    /// <summary>
    /// Displays new file form.
    /// </summary>
    /// <param name="file">File the form is displayed for</param>
    public void DisplayForm(FileInfo file)
    {
        if (file != null)
        {
            string ext = file.Extension;

            if (!MediaLibraryHelper.IsExtensionAllowed(ext.TrimStart('.')))
            {
                this.lblErrorNew.Text = string.Format(GetString("attach.notallowedextension"), ext, MediaLibraryHelper.GetAllowedExtensions(CMSContext.CurrentSiteName).TrimEnd(';').Replace(";", ", "));
                this.lblErrorNew.Visible = true;
                SetFormVisible(false);
            }
            else
            {
                // If is not in DB fill new file form and show it
                SetFormVisible(true);
                this.txtNewDescripotion.Text = "";
                this.txtNewFileTitle.Text = Path.GetFileNameWithoutExtension(file.Name);
                this.txtNewFileName.Text = URLHelper.GetSafeFileName(Path.GetFileNameWithoutExtension(file.Name), CMSContext.CurrentSiteName, false);
            }
        }
    }


    /// <summary>
    /// Sets default values and clear textboxes.
    /// </summary>
    public void SetDefault()
    {
        this.txtNewFileName.Text = "";
        this.txtNewFileTitle.Text = "";
        this.txtNewDescripotion.Text = "";
    }


    /// <summary>
    /// New file event handler.
    /// </summary>
    protected void btnNew_Click(object sender, EventArgs e)
    {
        MediaFileInfo mfi = null;

        try
        {
            string newFileName = this.txtNewFileName.Text.Trim();
            // Check if the filename is in correct format
            if (!ValidationHelper.IsFileName(newFileName))
            {
                this.lblErrorNew.Text = GetString("media.rename.wrongformat");
                ErrorOccurred = true;
            }
            else
            {
                // Check 'File create' permission
                if (MediaLibraryInfoProvider.IsUserAuthorizedPerLibrary(this.LibraryInfo, "filecreate"))
                {
                    if (this.LibraryInfo != null)
                    {
                        // Get file and library info
                        FileInfo fi = FileInfo.New(MediaFileInfoProvider.GetMediaFilePath(this.LibrarySiteInfo.SiteName, this.LibraryInfo.LibraryFolder, this.FilePath));
                        if (fi != null)
                        {
                            if (File.Exists(fi.FullName))
                            {
                                // Save new file in the DB
                                if (SaveRequired != null)
                                {
                                    mfi = SaveRequired(fi, this.txtNewFileTitle.Text.Trim(), this.txtNewDescripotion.Text.Trim(), URLHelper.GetSafeFileName(newFileName, CMSContext.CurrentSiteName, false), this.FilePath);
                                    if (mfi != null)
                                    {
                                        mfi.RelatedData = fi.FullName;
                                    }
                                }
                            }
                            else
                            {
                                this.lblErrorNew.Text = GetString("media.newfile.notexist");
                                ErrorOccurred = true;
                            }
                        }
                    }
                }
                else
                {
                    this.lblErrorNew.Text = MediaLibraryHelper.GetAccessDeniedMessage("filecreate");
                    ErrorOccurred = true;
                }
            }
        }
        catch (Exception ex)
        {
            ErrorOccurred = true;
            this.lblErrorNew.Text = (ex.InnerException != null ? ex.InnerException.Message : ex.Message);
        }

        // Display user with error encountered
        if (ErrorOccurred)
        {
            this.lblErrorNew.Visible = true;
            RaiseOnAction("error");
        }
        else
        {
            RaiseOnAction("save", mfi);
        }
    }


    /// <summary>
    /// BreadCrumbs in new file form.
    /// </summary>
    protected void lnkNewList_Click(object sender, EventArgs e)
    {
        RaiseOnAction("showlist");
    }


    #region "Event methods"

    /// <summary>
    /// Raises action event of specified type.
    /// </summary>
    /// <param name="name">Action name</param>
    private void RaiseOnAction(string name)
    {
        RaiseOnAction(name, null);
    }


    /// <summary>
    /// Raises action event of specified type.
    /// </summary>
    /// <param name="name">Action name</param>
    private void RaiseOnAction(string name, object argument)
    {
        if (Action != null)
        {
            // Hide new file form and show unigrid
            Action(name, argument);
        }
    }


    /// <summary>
    /// Sets forms visible property.
    /// </summary>
    /// <param name="visible">Visible flag</param>
    private void SetFormVisible(bool visible)
    {
        txtNewFileName.Visible = visible;
        lblNewFileName.Visible = visible;
        txtNewFileTitle.Visible = visible;
        lnlNewFileTitle.Visible = visible;
        txtNewDescripotion.Visible = visible;
        lblNewDescription.Visible = visible;
        btnNew.Visible = visible;
    }

    #endregion
}
