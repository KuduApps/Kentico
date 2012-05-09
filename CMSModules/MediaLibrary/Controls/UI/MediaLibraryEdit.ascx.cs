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

using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.MediaLibrary;
using CMS.CMSHelper;
using CMS.SiteProvider;
using CMS.ExtendedControls;
using CMS.SettingsProvider;
using CMS.EventLog;
using CMS.IO;

public partial class CMSModules_MediaLibrary_Controls_UI_MediaLibraryEdit : CMSAdminEditControl
{
    #region "Private variables"

    private int mMediaLibraryID = 0;
    private int mMediaLibraryGroupID = 0;
    private Guid mMediaLibraryGroupGUID = Guid.Empty;
    private bool mEnable = true;

    private MediaLibraryInfo mLibraryInfo = null;

    #endregion


    #region "Private properties"

    /// <summary>
    /// Current library info.
    /// </summary>
    private MediaLibraryInfo LibraryInfo
    {
        get
        {
            if ((this.mLibraryInfo == null) && (this.MediaLibraryID > 0))
            {
                // Get data
                this.mLibraryInfo = MediaLibraryInfoProvider.GetMediaLibraryInfo(this.MediaLibraryID);

                // Check whether library belongs to requested group when entered 
                if ((this.mLibraryInfo != null) && (this.MediaLibraryGroupID > 0) && (this.mLibraryInfo.LibraryGroupID != this.MediaLibraryGroupID))
                {
                    this.mLibraryInfo = null;
                }

                // Check whether library belongs to current site when not global admin
                if ((this.mLibraryInfo != null) && (!CMSContext.CurrentUser.IsGlobalAdministrator) && (this.mLibraryInfo.LibrarySiteID != CMSContext.CurrentSiteID))
                {
                    this.mLibraryInfo = null;
                }
            }

            return this.mLibraryInfo;
        }
    }

    #endregion


    #region "Public properties"

    /// <summary>
    /// Gets or sets media library ID.
    /// </summary>
    public int MediaLibraryID
    {
        get
        {
            return mMediaLibraryID;
        }
        set
        {
            mMediaLibraryID = value;
            mLibraryInfo = null;
        }
    }


    /// <summary>
    /// Gets or sets media library group ID.
    /// </summary>
    public int MediaLibraryGroupID
    {
        get
        {
            return this.mMediaLibraryGroupID;
        }
        set
        {
            this.mMediaLibraryGroupID = value;
        }
    }


    /// <summary>
    /// Gets or sets media library group GUID.
    /// </summary>
    public Guid MediaLibraryGroupGUID
    {
        get
        {
            return this.mMediaLibraryGroupGUID;
        }
        set
        {
            this.mMediaLibraryGroupGUID = value;
        }
    }


    /// <summary>
    /// Indicates whether editing form is enabled.
    /// </summary>
    public bool Enable
    {
        get
        {
            return this.mEnable;
        }
        set
        {
            this.mEnable = value;
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
            txtDisplayName.IsLiveSite = value;
            ucMetaFile.IsLiveSite = value;
        }
    }

    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        txtDisplayName.IsLiveSite = this.IsLiveSite;
        txtDescription.IsLiveSite = this.IsLiveSite;

        if (!this.StopProcessing)
        {
            // Initialize only when visible
            if (this.Visible)
            {
                InitializeControl();
            }

            if (!URLHelper.IsPostback() && !this.IsLiveSite)
            {
                LoadLibrary();
            }
        }
    }


    protected void Page_PreRender(object sender, EventArgs e)
    {
        // Reload data for live site management
        if (this.Visible && this.IsLiveSite)
        {
            ReloadData();
        }
    }


    #region "Public methods"

    /// <summary>
    /// Reloads control's content.
    /// </summary>
    public override void ReloadData()
    {
        InitializeControl();

        LoadLibrary();
    }


    /// <summary>
    /// 
    /// </summary>
    private void InitializeControl()
    {
        RaiseOnCheckPermissions(CMSAdminControl.PERMISSION_READ, this);

        // Hide code name edit for simple mode
        if (DisplayMode == ControlDisplayModeEnum.Simple)
        {
            this.plcCodeName.Visible = false;
        }

        // Initialize controls
        SetupControls();

        SetupTexts();

        if (!this.Enable)
        {
            DisableForm();
        }

        ControlsHelper.RegisterPostbackControl(this.btnOk);
    }


    /// <summary>
    /// Clears the content of editing form - used by Live control.
    /// </summary>
    public override void ClearForm()
    {
        this.txtCodeName.Text = "";
        this.txtDescription.Text = "";
        this.txtDisplayName.Text = "";
        this.txtFolder.Text = "";
        this.ucMetaFile.ClearControl();
    }

    #endregion


    #region "Private methods"

    /// <summary>
    /// Loads library data if available.
    /// </summary>
    private void LoadLibrary()
    {
        // Get info and load controls            
        if (this.LibraryInfo != null)
        {
            FillForm(this.LibraryInfo);
        }
        else if (this.MediaLibraryID > 0)
        {
            plcProperties.Visible = false;
            lblError.Visible = true;
            lblError.Text = GetString("general.invalidid");
            EditedObject = this.LibraryInfo;
        }
        else
        {
            plcProperties.Visible = true;
        }
    }


    /// <summary>
    /// Initializes all the nested controls.
    /// </summary>
    private void SetupControls()
    {
        // Get media library info        
        if (this.LibraryInfo != null)
        {
            ucMetaFile.ObjectID = this.LibraryInfo.LibraryID;
            this.txtFolder.Enabled = false;

            if (!MediaLibraryInfoProvider.IsUserAuthorizedPerLibrary(this.LibraryInfo, CMSAdminControl.PERMISSION_MANAGE))
            {
                // Disable MetaFile uploader
                this.ucMetaFile.Enabled = false;
            }
        }
        else
        {
            ucMetaFile.ObjectID = 0;
            this.txtFolder.Enabled = true;
        }
        ucMetaFile.ObjectType = MediaLibraryObjectType.MEDIALIBRARY;
        ucMetaFile.Category = MetaFileInfoProvider.OBJECT_CATEGORY_IMAGE;
        ucMetaFile.OnAfterDelete += new EventHandler(ucMetaFile_OnAfterDelete);
        ucMetaFile.ReloadData();
    }


    /// <summary>
    /// Initializes inner control labels.
    /// </summary>
    private void SetupTexts()
    {
        lblDisplayName.ResourceString = "general.displayname";
        lblDisplayName.DisplayColon = true;
        lblCodeName.ResourceString = "general.codename";
        lblCodeName.DisplayColon = true;
        lblDescription.ResourceString = "general.description";
        lblDescription.DisplayColon = true;
        lblFolder.ResourceString = "general.foldername";
        lblFolder.DisplayColon = true;
        btnOk.ResourceString = "general.ok";
        lblTeaser.DisplayColon = true;
        lblTeaser.ResourceString = "media.general.teaser";

        rfvCodeName.ErrorMessage = GetString("general.requirescodename");
        rfvDisplayName.ErrorMessage = GetString("general.requiresdisplayname");
        if (txtFolder.Enabled)
        {
            rfvFolder.ErrorMessage = GetString("media.general.requiresfolder");
        }
        else
        {
            rfvFolder.Enabled = false;
        }
    }


    /// <summary>
    /// Fills editing form with selected library data.
    /// </summary>
    /// <param name="library">Library info holding data</param>
    private void FillForm(MediaLibraryInfo library)
    {
        txtDisplayName.Text = library.LibraryDisplayName;
        txtCodeName.Text = library.LibraryName;
        txtDescription.Text = library.LibraryDescription;
        txtFolder.Text = library.LibraryFolder;
        txtFolder.Enabled = false;
    }


    /// <summary>
    /// Validates input data and returns true if input is valid.
    /// </summary>
    /// <param name="codeName">Code name</param>
    protected bool ValidateForm(string codeName)
    {
        txtDisplayName.Text = txtDisplayName.Text.Trim();
        txtDescription.Text = txtDescription.Text.Trim();
        txtFolder.Text = URLHelper.GetSafeFileName(txtFolder.Text.Trim(), CMSContext.CurrentSiteName);

        string result = new Validator().NotEmpty(txtDisplayName.Text, rfvDisplayName.ErrorMessage)
            .NotEmpty(txtDisplayName.Text, rfvCodeName.ErrorMessage)
            .IsCodeName(codeName, GetString("general.invalidcodename")).Result;

        // Folder name is enabled check it
        if ((String.IsNullOrEmpty(result)) && (this.txtFolder.Enabled))
        {
            result = new Validator().NotEmpty(txtFolder.Text, rfvFolder.ErrorMessage)
            .IsFolderName(txtFolder.Text, GetString("media.invalidfoldername")).Result;

            if (String.IsNullOrEmpty(result))
            {
                // Check special folder names
                if ((this.txtFolder.Text == ".") || (this.txtFolder.Text == ".."))
                {
                    result = GetString("media.folder.foldernameerror");
                }
            }
        }

        // Check for duplicit records within current site
        MediaLibraryInfo mli = null;
        if (this.MediaLibraryGroupID > 0)
        {
            mli = MediaLibraryInfoProvider.GetMediaLibraryInfo(codeName, CMSContext.CurrentSiteID, this.MediaLibraryGroupID);
        }
        else
        {
            mli = MediaLibraryInfoProvider.GetMediaLibraryInfo(codeName, CMSContext.CurrentSiteName);
        }
        if ((mli != null) && (mli.LibraryID != this.MediaLibraryID))
        {
            result = GetString("general.codenameexists");
        }

        // Check for duplicit library root folder
        DataSet ds = MediaLibraryInfoProvider.GetMediaLibraries("LibraryFolder = '" + txtFolder.Text.Trim().Replace("'", "''") + "' AND LibrarySiteID = " + CMSContext.CurrentSiteID, null);
        if (!DataHelper.DataSourceIsEmpty(ds))
        {
            int libraryId = ValidationHelper.GetInteger(ds.Tables[0].Rows[0]["LibraryID"], 0);
            if (this.MediaLibraryID != libraryId)
            {
                result = GetString("media.folderexists");
            }
        }

        // If meta files should be stored in filesystem for given site
        if ((ucMetaFile.PostedFile != null) && MetaFileInfoProvider.StoreFilesInFileSystem(CMSContext.CurrentSiteName))
        {
            // Get image path for site
            string path = MetaFileInfoProvider.GetFilesFolderPath(CMSContext.CurrentSiteName);

            // Ensure meta files folder
            if (!Directory.Exists(path))
            {
                DirectoryHelper.EnsureDiskPath(path, SettingsKeyProvider.WebApplicationPhysicalPath);
            }

            // Check permission for image folder
            if (!DirectoryHelper.CheckPermissions(path))
            {
                result = String.Format(GetString("media.AccessDeniedToPath"), path);
            }
        }

        if (result != String.Empty)
        {
            lblError.Visible = true;
            lblError.Text = HTMLHelper.HTMLEncode(result);
            return false;
        }

        return true;
    }


    /// <summary>
    /// Updates metafile image path.
    /// </summary>
    private void UpdateImagePath(MediaLibraryInfo mli)
    {
        // Update image path according to its meta file
        DataSet ds = MetaFileInfoProvider.GetMetaFiles(ucMetaFile.ObjectID, mli.TypeInfo.ObjectType);
        if (!DataHelper.DataSourceIsEmpty(ds))
        {
            MetaFileInfo metaFile = new MetaFileInfo(ds.Tables[0].Rows[0]);
            mli.LibraryTeaserPath = MetaFileInfoProvider.GetMetaFileUrl(metaFile.MetaFileGUID, metaFile.MetaFileName);
        }
        else
        {
            mli.LibraryTeaserPath = "";
        }
    }


    /// <summary>
    /// Disable editing form.
    /// </summary>
    private void DisableForm()
    {
        this.lblCodeName.Enabled = false;
        this.lblDescription.Enabled = false;
        this.lblDisplayName.Enabled = false;
        this.lblFolder.Enabled = false;
        this.lblTeaser.Enabled = false;

        this.txtCodeName.Enabled = false;
        this.txtDescription.Enabled = false;
        this.txtDisplayName.Enabled = false;
        this.txtFolder.Enabled = false;

        this.ucMetaFile.Enabled = false;
        this.btnOk.Enabled = false;
    }

    #endregion


    #region "Event handlers"

    protected void btnOK_Click(object sender, EventArgs e)
    {
        bool isAuthorized = false;

        // Check 'Manage' permission for user        
        if (this.LibraryInfo != null)
        {
            isAuthorized = MediaLibraryInfoProvider.IsUserAuthorizedPerLibrary(this.LibraryInfo, "Manage");
        }
        else
        {
            if (CMSContext.CurrentUser.IsAuthorizedPerResource("CMS.MediaLibrary", "Manage"))
            {
                isAuthorized = true;
            }
            else
            {
                if ((this.MediaLibraryGroupID > 0) && (CMSContext.CurrentUser.IsGroupAdministrator(this.MediaLibraryGroupID)))
                {
                    isAuthorized = true;
                }
            }
        }

        // If user isn't authorized let her now
        if (!isAuthorized)
        {
            lblError.Text = MediaLibraryHelper.GetAccessDeniedMessage("Manage");
            lblError.Visible = true;
            return;
        }
        string codeName = txtCodeName.Text;
        if (DisplayMode == ControlDisplayModeEnum.Simple)
        {
            if (this.MediaLibraryID == 0)
            {
                codeName = ValidationHelper.GetCodeName(txtDisplayName.Text, null, "_group_" + Guid.NewGuid());
            }
            else
            {
                codeName = this.LibraryInfo.LibraryName;
            }
        }
        codeName = codeName.Trim();

        // Validate input boxes
        if (!ValidateForm(codeName))
        {
            return;
        }

        // Create new object (record) if needed
        MediaLibraryInfo mli = null;
        if (this.MediaLibraryID > 0)
        {
            mli = this.LibraryInfo;
        }
        else
        {
            mli = new MediaLibraryInfo();
        }

        mli.LibraryDisplayName = txtDisplayName.Text;

        if (txtFolder.Enabled)
        {
            mli.LibraryFolder = txtFolder.Text;
        }

        mli.LibraryDescription = txtDescription.Text;
        mli.LibraryName = codeName;

        // If the library is group related
        if (this.MediaLibraryGroupID > 0)
        {
            mli.LibraryGroupID = this.MediaLibraryGroupID;
            // If creating new group library setup default security

            if (this.MediaLibraryID == 0)
            {
                // Set default group media library security
                mli.FileCreate = SecurityAccessEnum.GroupMembers;
                mli.FileDelete = SecurityAccessEnum.Nobody;
                mli.FileModify = SecurityAccessEnum.Nobody;
                mli.FolderCreate = SecurityAccessEnum.Nobody;
                mli.FolderDelete = SecurityAccessEnum.Nobody;
                mli.FolderModify = SecurityAccessEnum.Nobody;
                mli.Access = SecurityAccessEnum.GroupMembers;
            }
        }

        mli.LibrarySiteID = CMSContext.CurrentSiteID;

        try
        {
            MediaLibraryInfoProvider.SetMediaLibraryInfo(mli);
        }
        catch
        {
            lblError.Text = GetString("general.errorsaving");
            lblError.Visible = true;
            return;
        }

        if ((mli != null) && (mli.LibraryID != 0))
        {
            // Add teaser image to media library
            ucMetaFile.ObjectID = mli.LibraryID;
            ucMetaFile.UploadFile();
            UpdateImagePath(mli);

            try
            {
                MediaLibraryInfoProvider.SetMediaLibraryInfo(mli);
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
                lblError.Visible = true;
                return;
            }
            // Update current media library id
            this.MediaLibraryID = mli.LibraryID;

            lblInfo.Visible = true;
            lblInfo.Text = GetString("general.changessaved");

            FillForm(mli);

            this.RaiseOnSaved();
        }

        // Reload header if changes were saved
        ScriptHelper.RefreshTabHeader(Page, GetString("general.general"));
    }


    private void ucMetaFile_OnAfterDelete(object sender, EventArgs e)
    {
        MediaLibraryInfo mli = null;

        if (this.MediaLibraryID > 0)
        {
            mli = this.LibraryInfo;
        }

        if (mli != null)
        {
            // Delete teaser path
            mli.LibraryTeaserPath = null;
            MediaLibraryInfoProvider.SetMediaLibraryInfo(mli);
        }
    }


    #endregion
}
