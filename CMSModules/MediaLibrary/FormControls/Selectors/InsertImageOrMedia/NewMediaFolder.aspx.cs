using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.UIControls;
using CMS.GlobalHelper;
using CMS.MediaLibrary;
using CMS.SiteProvider;
using CMS.CMSHelper;


public partial class CMSModules_MediaLibrary_FormControls_Selectors_InsertImageOrMedia_NewMediaFolder : CMSModalPage
{
    #region "Private variables"

    private int mLibraryId = 0;
    private string mFolderPath = "";
    private MediaLibraryInfo mLibrary = null;

    #endregion


    #region "Private properties"

    /// <summary>
    /// Current library ID.
    /// </summary>
    private MediaLibraryInfo Library
    {
        get
        {
            if ((this.mLibrary == null) && (this.mLibraryId > 0))
            {
                this.mLibrary = MediaLibraryInfoProvider.GetMediaLibraryInfo(this.mLibraryId);
            }
            return this.mLibrary;
        }
        set
        {
            this.mLibrary = value;
        }
    }

    #endregion



    protected void Page_Load(object sender, EventArgs e)
    {
        if (QueryHelper.ValidateHash("hash"))
        {
            // Check site availability
            if (!ResourceSiteInfoProvider.IsResourceOnSite("CMS.MediaLibrary", CMSContext.CurrentSiteName))
            {
                RedirectToResourceNotAvailableOnSite("CMS.MediaLibrary");
            }

            // Initialize controls
            SetupControls();
        }
        else
        {
            this.createFolder.Visible = false;
            string url = ResolveUrl("~/CMSMessages/Error.aspx?title=" + GetString("dialogs.badhashtitle") + "&text=" + GetString("dialogs.badhashtext") + "&cancel=1");
            ScriptHelper.RegisterStartupScript(Page, typeof(string), "redirect", ScriptHelper.GetScript("if (window.parent != null) { window.parent.location = '" + url + "' }"));
        }
    }


    /// <summary>
    /// Initializes controls.
    /// </summary>
    private void SetupControls()
    {
        // Get data from query string
        this.mLibraryId = QueryHelper.GetInteger("libraryid", 0);
        this.mFolderPath = QueryHelper.GetString("path", "");
        EditedObject = this.Library;

        if (this.Library != null)
        {
            this.createFolder.OnFolderChange += new CMSModules_MediaLibrary_Controls_MediaLibrary_FolderActions_EditFolder.OnFolderChangeEventHandler(createFolder_OnFolderChange);
            this.createFolder.CancelClick += new CMSModules_MediaLibrary_Controls_MediaLibrary_FolderActions_EditFolder.OnCancelClickEventHandler(createFolder_CancelClick);
            this.createFolder.IsLiveSite = false;

            // Initialize information on library
            this.createFolder.LibraryID = this.mLibraryId;
            this.createFolder.LibraryFolder = this.Library.LibraryFolder;
            this.createFolder.FolderPath = this.mFolderPath;
        }

        this.Page.Header.Title = GetString("dialogs.newfoldertitle");

        this.CurrentMaster.Title.TitleText = GetString("media.folder.new");
        this.CurrentMaster.Title.TitleImage = ResolveUrl(GetImageUrl("CMSModules/CMS_MediaLibrary/librarynew.png", true));

    }


    #region "Event handlers"

    protected void createFolder_CancelClick()
    {
        string cancelScript = "";

        bool postbackOnCancel = QueryHelper.GetBoolean("cancel", true);
        if (postbackOnCancel)
        {
            cancelScript = "wopener.SetAction('cancelfolder', ''); wopener.RaiseHiddenPostBack();";
        }
        cancelScript += " window.close();";

        this.ltlScript.Text += ScriptHelper.GetScript(cancelScript);
    }


    protected void createFolder_OnFolderChange(string pathToSelect)
    {
        this.ltlScript.Text = ScriptHelper.GetScript("wopener.SetAction('newfolder', '" + pathToSelect.Replace('\\', '|').Replace("'", "\\'") + "'); wopener.RaiseHiddenPostBack(); window.close();");
    }

    #endregion
}
