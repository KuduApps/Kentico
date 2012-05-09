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

using CMS.UIControls;
using CMS.MediaLibrary;
using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.IO;

public partial class CMSModules_MediaLibrary_Controls_MediaLibrary_SelectFolder : CMSAdminControl
{
    #region "Variables"

    private int mMediaLibraryID = 0;
    private string mAction = null;
    private string mFolderPath = null;
    private string mFiles = null;
    private bool mAllFiles = false;

    #endregion


    #region "Public properties"

    /// <summary>
    /// ID of the media library to display.
    /// </summary>
    public int MediaLibraryID
    {
        get
        {
            return this.mMediaLibraryID;
        }
        set
        {
            this.mMediaLibraryID = value;
        }
    }


    /// <summary>
    /// Action control is displayed for.
    /// </summary>
    public string Action
    {
        get
        {
            return this.mAction;
        }
        set
        {
            this.mAction = value;
        }
    }


    /// <summary>
    /// Folder path of the files action is related to.
    /// </summary>
    public string FolderPath
    {
        get
        {
            return this.mFolderPath;
        }
        set
        {
            this.mFolderPath = value;
        }
    }


    /// <summary>
    /// Sets of file names action is related to.
    /// </summary>
    public string Files
    {
        get
        {
            return this.mFiles;
        }
        set
        {
            this.mFiles = value;
        }
    }


    /// <summary>
    /// Indicates whether all available files should be processed.
    /// </summary>
    public bool AllFiles
    {
        get
        {
            return this.mAllFiles;
        }
        set
        {
            this.mAllFiles = value;
        }
    }

    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        if (QueryHelper.ValidateHash("hash"))
        {
            SetupControls();
        }
        else
        {
            this.mediaLibrary.StopProcessing = true;
            this.mediaLibrary.ShouldProcess = false;
            string url = ResolveUrl("~/CMSMessages/Error.aspx?title=" + GetString("dialogs.badhashtitle") + "&text=" + GetString("dialogs.badhashtext") + "&cancel=1");
            ScriptHelper.RegisterStartupScript(Page, typeof(string), "redirect", ScriptHelper.GetScript("if (window.parent != null) { window.parent.location = '" + url + "' }"));
        }
    }


    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (IsLiveSite)
        {
            // Register custom CSS
            CSSHelper.RegisterCSSLink(Page, CMSContext.CurrentDocumentStylesheetName, "Skin.css");            
        }
    }


    /// <summary>
    /// Initializes all the nested controls.
    /// </summary>
    private void SetupControls()
    {
        // Setup title
        InitializeTitle();

        this.mediaLibrary.IsLiveSite = this.IsLiveSite;
        this.mediaLibrary.ShouldProcess = true;
        this.mediaLibrary.LibraryID = this.MediaLibraryID;
        this.mediaLibrary.Action = this.Action;
        this.mediaLibrary.CopyMovePath = this.FolderPath;
        this.mediaLibrary.Files = this.Files;
        this.mediaLibrary.AllFiles = this.AllFiles;
    }


    #region "Private methods"

    /// <summary>
    /// Setup title according to action.
    /// </summary>
    private void InitializeTitle()
    {
        if ((this.Files != "") || this.AllFiles)
        {
            if (this.Action == "copy")
            {
                this.titleElem.TitleText = GetString("media.tree.copyfiles");
                this.titleElem.TitleImage = ResolveUrl(GetImageUrl("CMSModules/CMS_MediaLibrary/filecopy.png", IsLiveSite));
            }
            else
            {
                this.titleElem.TitleText = GetString("media.tree.movefiles");
                this.titleElem.TitleImage = ResolveUrl(GetImageUrl("CMSModules/CMS_MediaLibrary/filemove.png", IsLiveSite));
            }
        }
        else
        {
            if (this.Action == "copy")
            {
                this.titleElem.TitleText = GetString("media.tree.copyfolder");
                this.titleElem.TitleImage = ResolveUrl(GetImageUrl("CMSModules/CMS_MediaLibrary/foldercopy.png", IsLiveSite));
            }
            else
            {
                this.titleElem.TitleText = GetString("media.tree.movefolder");
                this.titleElem.TitleImage = ResolveUrl(GetImageUrl("CMSModules/CMS_MediaLibrary/foldermove.png", IsLiveSite));
            }
        }
    }

    #endregion
}
