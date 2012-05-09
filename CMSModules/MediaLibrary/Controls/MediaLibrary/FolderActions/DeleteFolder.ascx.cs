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
using CMS.IO;

public partial class CMSModules_MediaLibrary_Controls_MediaLibrary_FolderActions_DeleteFolder : CMSAdminControl
{
    #region "Delegates & Events"

    /// <summary>
    /// Delegate of event fired when 'Cancel' button of control is clicked.
    /// </summary>
    public delegate void OnCancelClickEventHandler();

    /// <summary>
    /// Delegate of event fired when folder has been deleted.
    /// </summary>
    public delegate void OnFolderDeletedEventHandler(string pathToSelect);

    /// <summary>
    /// Event raised when 'Cancel' button is clicked.
    /// </summary>
    public event OnCancelClickEventHandler CancelClick;

    /// <summary>
    /// Event raised when folder has been deleted.
    /// </summary>
    public event OnFolderDeletedEventHandler OnFolderDeleted;

    #endregion


    #region "Private variables"

    private int mLibraryId = 0;
    private string mFolderPath = null;
    private string mLibraryFolder = null;
    private string mRootFolderPath = null;
    private string mImageFolderPath = "";

    /// <summary>
    /// Path of the parent folder of currently deleted folder.
    /// </summary>
    private string mDeletedFolderParent = "";

    #endregion


    #region "Public properties"

    /// <summary>
    /// Folder path where the images are stored.
    /// </summary>
    public string ImageFolderPath
    {
        get
        {
            if (mImageFolderPath == "")
            {
                mImageFolderPath = GetImageUrl("CMSModules/CMS_MediaLibrary/", IsLiveSite, true);
            }
            return this.mImageFolderPath.TrimEnd('/') + "/";
        }
        set
        {
            this.mImageFolderPath = value;
        }
    }


    /// <summary>
    /// Folder path of the currently processed library.
    /// </summary>
    public string FolderPath
    {
        get
        {
            if (this.mFolderPath == "")
            {
                this.mFolderPath = ValidationHelper.GetString(ViewState["LastKnownDeleteFolderPath"], "");
            }

            return this.mFolderPath;
        }
        set
        {
            this.mFolderPath = value;
            ViewState["LastKnownDeleteFolderPath"] = value;
        }
    }


    /// <summary>
    /// Library folder of the currently processed library.
    /// </summary>
    public string LibraryFolder
    {
        get
        {
            return this.mLibraryFolder;
        }
        set
        {
            this.mLibraryFolder = value;
        }
    }


    /// <summary>
    /// ID of the currently processed media library.
    /// </summary>
    public int LibraryID
    {
        get
        {
            return this.mLibraryId;
        }
        set
        {
            this.mLibraryId = value;
        }
    }


    /// <summary>
    /// Gets or sets library root folder path.
    /// </summary>
    public string RootFolderPath
    {
        get
        {
            return this.mRootFolderPath;
        }
        set
        {
            this.mRootFolderPath = value;
        }
    }

    #endregion


    protected override void CreateChildControls()
    {
        // Initialize controls
        SetupControl();

        base.CreateChildControls();
    }


    protected override void OnLoad(EventArgs e)
    {
        RaiseOnCheckPermissions(CMSAdminControl.PERMISSION_READ, this);

        base.OnLoad(e);
    }


    /// <summary>
    /// Reloads control's content.
    /// </summary>
    public void ReloadControl()
    {
        SetupControl();
    }


    #region "Event handlers"

    protected void btnOk_Click(object sender, EventArgs e)
    {
        MediaLibraryInfo libInfo = MediaLibraryInfoProvider.GetMediaLibraryInfo(this.LibraryID);
        if (libInfo != null)
        {
            // Check 'Folder delete' permission
            if (!MediaLibraryInfoProvider.IsUserAuthorizedPerLibrary(libInfo, "folderdelete"))
            {
                this.lblError.Text = MediaLibraryHelper.GetAccessDeniedMessage("folderdelete");
                this.lblError.Visible = true;
                return;
            }

            try
            {
                // Delete folder and all files within
                MediaLibraryInfoProvider.DeleteMediaLibraryFolder(CMSContext.CurrentSiteName, this.LibraryID, this.FolderPath, false);

                // Get path of the parent folder of the currently deleted folder
                this.FolderPath = DirectoryHelper.CombinePath(this.LibraryFolder, this.FolderPath);
                this.mDeletedFolderParent = this.FolderPath.Remove(this.FolderPath.LastIndexOf("\\"));

                // Let the parent control know the folder was deleted
                if (OnFolderDeleted != null)
                {
                    OnFolderDeleted(this.mDeletedFolderParent);
                }

                this.lblError.Visible = false;
            }
            catch (Exception ex)
            {
                // Display an error to the user
                this.lblError.Text = GetString("general.erroroccurred") + " " + ex.Message;
                this.lblError.Visible = true;
            }
        }
    }


    protected void btnCancel_Click(object sender, EventArgs e)
    {
        // Let the parent control know about the event
        if (CancelClick != null)
        {
            CancelClick();
        }
    }

    #endregion


    #region "Private methods"

    /// <summary>
    /// Initializes the control and all nested controls.
    /// </summary>
    private void SetupControl()
    {
        // Setup labels
        this.lblDeleteInfo.Text = string.Format(GetString("media.folder.delete.confirmation"), this.FolderPath);

        this.imgTitle.ImageUrl = GetImageUrl("CMSModules/CMS_MediaLibrary/librarydelete.png");

        this.btnOk.Text = GetString("general.ok");
        this.btnCancel.Text = GetString("general.cancel");
    }

    #endregion
}
