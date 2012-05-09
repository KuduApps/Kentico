using System;
using System.Text;
using System.ComponentModel;

using CMS.CMSHelper;
using CMS.ExtendedControls;
using CMS.GlobalHelper;
using CMS.IO;
using CMS.UIControls;
using System.Web.UI;

public partial class CMSModules_Content_Controls_Dialogs_FileSystemSelector_Menu : CMSUserControl
{
    #region "Public properties"

    /// <summary>
    /// Target folder path for physical files.
    /// </summary>
    public string TargetFolderPath { get; set; }


    /// <summary>
    /// Gets or sets which files with extensions are allowed to be uploaded.
    /// </summary>
    public string AllowedExtensions { get; set; }


    /// <summary>
    /// Delete folder button
    /// </summary>
    public bool EnableDeleteFolder
    {
        get
        {
            return menuBtnDeleteFolder.Enabled;
        }
        set
        {
            menuBtnDeleteFolder.Enabled = value;
        }
    }

    #endregion


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        if (!StopProcessing)
        {
            // Initialize actions menu
            InitializeActionsMenu();
        }
        else
        {
            Visible = false;
        }
    }


    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);

        if (!StopProcessing)
        {
            // Initialize controls
            SetupControls();
        }
    }


    /// <summary>
    /// Reloads part of the menu providing file related actions.
    /// </summary>
    public void UpdateActionsMenu()
    {
        // Initialize actions menu
        InitializeActionsMenu();

        // Apply updated information
        fileUploader.ReloadData();

        pnlUpdateActionsMenu.Update();
    }


    #region "Private methods"

    /// <summary>
    /// Initializes all the nested controls.
    /// </summary>
    private void SetupControls()
    {
        menuBtnDeleteFolder.OnClick += new EventHandler(menuBtnDeleteFolder_OnClick);

        if (!IsLiveSite)
        {
            // Initialize help
            helpElem.TopicName = "dialogs_filesystem";
        }
        else
        {
            helpElem.StopProcessing = true;
            pnlHelp.Visible = false;
        }
    }


    /// <summary>
    /// Initiliazes menu with view mode selection.
    /// </summary>
    private void InitializeActionsMenu()
    {
        string uploaderImg = GetImageUrl("CMSModules/CMS_Content/Dialogs/addfile.png");
        string uploaderImgOver = GetImageUrl("CMSModules/CMS_Content/Dialogs/addfileover.png");

        if (IsLiveSite)
        {
            // For live site must get image from App_Themes folder from current stylesheet
            string liveUploderImg = ResolveUrl("~/App_Themes/" + CMSContext.CurrentDocumentStylesheetName + "/Images/MediaLibrary/addfile.png");
            string liveUploderImgOver = ResolveUrl("~/App_Themes/" + CMSContext.CurrentDocumentStylesheetName + "/Images/MediaLibrary/addfileover.png");
            if (FileHelper.FileExists(liveUploderImg))
            {
                uploaderImg = liveUploderImg;
            }
            if (File.Exists(Server.MapPath(liveUploderImgOver)))
            {
                uploaderImgOver = liveUploderImgOver;
            }
        }

        fileUploader.ImageUrl = uploaderImg;
        fileUploader.ImageUrlOver = uploaderImgOver;

        string url = null;

        // New folder button
        url = "~/CMSModules/Content/Controls/Dialogs/FileSystemSelector/NewFolder.aspx?targetpath=" + Server.UrlEncode(TargetFolderPath).Replace("'", "%27");
        url = URLHelper.AddParameterToUrl(url, "hash", QueryHelper.GetHash(url, false));

        menuBtnNewFolder.Tooltip = GetString("dialogs.actions.newfolder.desc");
        menuBtnNewFolder.OnClickJavascript = "modalDialog('" + URLHelper.ResolveUrl(url) + "', 'NewFolder', 500, 350, null, true); return false;";
        menuBtnNewFolder.Text = "<div style=\"overflow:hidden; width:66px; white-space:nowrap\">" + GetString("dialogs.actions.newfolder") + "</div>";

        // Delete folder button
        menuBtnDeleteFolder.Tooltip = GetString("media.folder.delete");
        menuBtnDeleteFolder.OnClickJavascript = "if (!confirm('" + GetString("General.ConfirmDelete") + "')) { return false; } ";
        menuBtnDeleteFolder.Text = "<div style=\"overflow:hidden; width:71px; white-space:nowrap\">" + GetString("media.folder.delete") + "</div>";

        // Initialize disabled button
        imgUploaderDisabled.EnableViewState = false;
        if (IsLiveSite)
        {
            imgUploaderDisabled.Src = ResolveUrl(GetImageUrl("CMSModules/CMS_Content/Dialogs/addfiledisabledlife.png"));
        }
        else
        {
            imgUploaderDisabled.Src = ResolveUrl(GetImageUrl("CMSModules/CMS_Content/Dialogs/addfiledisabled.png"));
        }
        imgUploaderDisabled.Alt = GetString("dialogs.actions.newfile");

        // Initialize file uploader
        fileUploader.SourceType = MediaSourceEnum.PhysicalFile;
        fileUploader.TargetFolderPath = TargetFolderPath;
        fileUploader.IsLiveSite = IsLiveSite;
        fileUploader.InnerDivClass = "DialogMenuInnerDiv";
        fileUploader.InnerDivHtml = GetString("dialogs.actions.newfile");
        fileUploader.LoadingImageUrl = GetImageUrl("Design/Preloaders/preload16.gif");
        fileUploader.UploadMode = MultifileUploaderModeEnum.DirectMultiple;
        fileUploader.AllowedExtensions = AllowedExtensions;
        fileUploader.AfterSaveJavascript = "SetRefreshAction";
        fileUploader.InnerDivHtml = String.Format("<span>{0}</span>", GetString("dialogs.actions.newfile"));
    }


    void menuBtnDeleteFolder_OnClick(object sender, EventArgs e)
    {
        // Delete the folder
        Directory.Delete(this.TargetFolderPath, true);

        // Refresh the tree
        string parentPath = Path.GetDirectoryName(this.TargetFolderPath);

        string script = "SetParentAction('" + ScriptHelper.GetString(parentPath, false) + "')";

        ScriptHelper.RegisterStartupScript(this.Page, typeof(string), "DeleteRefresh", ScriptHelper.GetScript(script));

        pnlUpdateActionsMenu.Update();
    }

    #endregion
}
