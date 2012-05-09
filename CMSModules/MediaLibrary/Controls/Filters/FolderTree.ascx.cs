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
using CMS.CMSHelper;
using CMS.Controls;
using CMS.MediaLibrary;
using CMS.IO;

public partial class CMSModules_MediaLibrary_Controls_Filters_FolderTree : FolderTree
{
    #region "Private properties"

    /// <summary>
    /// Currently selected path.
    /// </summary>
    override public string SelectedPath
    {
        get
        {
            return ValidationHelper.GetString(ViewState["SelectedPath"], (FilterMethod == 0 ? QueryHelper.GetString(PathQueryStringKey, null) : null));
        }
        set
        {
            ViewState["SelectedPath"] = value;
        }
    }

    #endregion


    protected override void OnPreRender(EventArgs e)
    {
        string path = null;
        // If filter by query parameters
        if (this.FilterMethod == 0)
        {
            path = QueryHelper.GetString(this.PathQueryStringKey, "");
        }
        else
        {
            // Check if media file is set and try get file path
            int fileId = GetFileID();
            if (fileId > 0)
            {
                MediaFileInfo mfi = MediaFileInfoProvider.GetMediaFileInfo(fileId);
                if (mfi != null)
                {
                    // Get folder path from media file info object
                    path = Path.GetDirectoryName(mfi.FilePath);
                }
            }
            else
            {
                path = RemoveRoot(this.SelectedPath);
            }
        }

        if (String.IsNullOrEmpty(path))
        {
            // Select root in tree view
            this.folderTree.SelectPath(this.MediaLibraryFolder, false);
        }
        else
        {
            // Select folder in tree view
            this.folderTree.SelectPath(DirectoryHelper.CombinePath(this.MediaLibraryFolder, path), false);
        }

        base.OnPreRender(e);
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.FilterMethod == 1)
        {
            // Root by default
            if (this.SelectedPath == null)
            {
                this.SelectedPath = this.MediaLibraryFolder;
            }
        }
        // If postback is from Folder tree
        if (ValidationHelper.GetString(Request.Params["__EVENTTARGET"], String.Empty).StartsWith(folderTree.UniqueID))
        {
            // Update information on currently selected path
            string selectedPath = ValidationHelper.GetString(Request.Params["__EVENTARGUMENT"], String.Empty).ToLower();
            if (selectedPath != "")
            {
                // Remove library root
                this.SelectedPath = selectedPath.Remove(0, 1);
            }
            if (GetFileID() > 0)
            {
                string url = URLHelper.RemoveParameterFromUrl(URLHelper.CurrentURL, this.FileIDQueryStringKey);
                if (FilterMethod == 0)
                {
                    url = URLHelper.UpdateParameterInUrl(url, this.PathQueryStringKey, GetPathForQuery(SelectedPath));
                }
                URLHelper.Redirect(url);
            }
        }

        SetupControls();
    }


    /// <summary>
    /// Setup controls.
    /// </summary>
    private void SetupControls()
    {
        if (this.StopProcessing)
        {
            this.folderTree.Visible = false;
            this.folderTree.StopProcessing = true;
            return;
        }

        if (SourceFilterControl != null)
        {
            SourceFilterControl.OnFilterChanged += new ActionEventHandler(FilterControl_OnFilterChanged);
        }
        this.SourceFilterName = this.SourceFilterName;
        this.folderTree.MediaLibraryFolder = this.MediaLibraryFolder;
        this.folderTree.MediaLibraryPath = this.MediaLibraryPath;
        this.folderTree.ImageFolderPath = this.ImageFolderPath;
        this.folderTree.RootFolderPath = this.RootFolderPath;
        this.folderTree.ExpandedPath = this.ExpandPath;
        this.folderTree.DisplayFilesCount = this.DisplayFileCount;
        this.folderTree.OnFolderSelected += new CMSModules_MediaLibrary_Controls_MediaLibrary_FolderTree.OnFolderSelectedHandler(folderTree_OnFolderSelected);

        if (!string.IsNullOrEmpty(this.SelectedPath))
        {
            this.folderTree.SelectPath(this.SelectedPath, true);
        }

        int fid = GetFileID();
        if (fid == 0)
        {
            SetFilter();
        }
        if (!RequestHelper.IsPostBack())
        {
            // Filter changed event
            this.RaiseOnFilterChanged();
        }
    }

    /// <summary>
    /// OnFilterChange handler.
    /// </summary>
    private void FilterControl_OnFilterChanged()
    {
        this.OrderBy = this.SourceFilterControl.OrderBy;
        this.WhereCondition = this.Where;
        // Raise change event
        this.RaiseOnFilterChanged();
    }


    /// <summary>
    /// Returns FileID from query string.
    /// </summary>
    private int GetFileID()
    {
        return QueryHelper.GetInteger(this.FileIDQueryStringKey, 0);
    }


    /// <summary>
    /// Sets filters where condition according to selected path in folder tree.
    /// </summary>
    private void SetFilter()
    {
        string path = null;
        if (this.FilterMethod != 0)
        {
            // Filter by postback
            path = RemoveRoot(this.SelectedPath);
        }
        else
        {
            // Filter by query parameter
            path = QueryHelper.GetString(this.PathQueryStringKey, "");
        }

        // If in library root
        if (String.IsNullOrEmpty(this.MediaLibraryPath))
        {
            if (String.IsNullOrEmpty(path))
            {
                // Select only files from root folder
                this.WhereCondition = "(Filepath LIKE N'%')";
                this.CurrentFolder = "";

                if (!ShowSubfoldersContent)
                {
                    // Select only files from root folder
                    this.WhereCondition += " AND (Filepath NOT LIKE N'%/%')";
                }
            }
            else
            {
                // Escape ' and [ (spacial character for LIKE condition)
                string wPath = MediaLibraryHelper.EnsurePath(path).Replace("'", "''").Replace("[", "[[]");
                // Get files from path
                this.WhereCondition = "(FilePath LIKE N'" + wPath + "/%')";
                this.CurrentFolder = MediaLibraryHelper.EnsurePath(path);

                if (!ShowSubfoldersContent)
                {
                    // But no from subfolders
                    this.WhereCondition += " AND (FilePath NOT LIKE N'" + wPath + "/%/%')";
                }
            }
        }
        else
        {
            if (String.IsNullOrEmpty(path))
            {
                // Escape ' and [ (spacial character for LIKE condition)
                string wPath = MediaLibraryHelper.EnsurePath(this.MediaLibraryPath).Replace("'", "''").Replace("[", "[[]");
                // Select files from path folder
                this.WhereCondition = "(Filepath LIKE N'" + wPath + "/%')";
                this.CurrentFolder = MediaLibraryHelper.EnsurePath(this.MediaLibraryPath);

                if (!ShowSubfoldersContent)
                {
                    // Select only files from path folder
                    this.WhereCondition += " AND (Filepath NOT LIKE N'" + wPath + "/%/%')";
                }
            }
            else
            {
                // Escape ' and [ (spacial character for LIKE condition)
                string wPath = MediaLibraryHelper.EnsurePath(this.MediaLibraryPath + "/" + path).Replace("'", "''").Replace("[", "[[]");
                // Get files from path
                this.WhereCondition = "(FilePath LIKE N'" + wPath + "/%')";
                this.CurrentFolder = MediaLibraryHelper.EnsurePath(this.MediaLibraryPath) + "/" + MediaLibraryHelper.EnsurePath(path);
                if (!ShowSubfoldersContent)
                {
                    // But no from subfolders
                    this.WhereCondition += " AND (FilePath NOT LIKE N'" + wPath + "/%/%')";
                }
            }
        }
        this.Where = this.WhereCondition;
    }


    private void folderTree_OnFolderSelected()
    {
        if (this.FilterMethod == 0)
        {
            string url = URLHelper.RemoveParameterFromUrl(URLHelper.CurrentURL, this.FileIDQueryStringKey);
            string path = GetPathForQuery(this.SelectedPath);
            url = URLHelper.UpdateParameterInUrl(url, this.PathQueryStringKey, path);

            URLHelper.Redirect(url);
        }
        else
        {
            SetFilter();
            RaiseOnFilterChanged();
        }
    }


    /// <summary>
    /// Gets path without root folder.
    /// </summary>
    /// <param name="path">Path to be unrooted</param>
    private string RemoveRoot(string path)
    {
        if (!string.IsNullOrEmpty(path))
        {
            int rootEnd = path.IndexOf('\\');
            return ((rootEnd > -1) ? path.Substring(rootEnd + 1) : "");
        }

        return path;
    }


    /// <summary>
    /// Returns folder path encoded for query string.
    /// </summary>
    /// <param name="path">Folder path</param>
    private string GetPathForQuery(string path)
    {
        string noRootPath = RemoveRoot(path);

        if (!String.IsNullOrEmpty(noRootPath))
        {
            noRootPath = HttpUtility.UrlEncode(noRootPath);

            // Escape special characters from path
            noRootPath = noRootPath.Replace("&", "%26").Replace("#", "%23");
        }

        return noRootPath;
    }
}
