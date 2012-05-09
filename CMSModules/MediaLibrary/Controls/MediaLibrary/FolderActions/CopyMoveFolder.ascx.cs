using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Principal;

using CMS.UIControls;
using CMS.GlobalHelper;
using CMS.ExtendedControls;

public partial class CMSModules_MediaLibrary_Controls_MediaLibrary_FolderActions_CopyMoveFolder_Control : CMSUserControl
{
    #region "Variables"

    private int mMediaLibraryID = 0;
    private bool mAllFiles = false;
    private bool mIsLoad = true;
    private bool mPerformAction = false;
    private string mAction = null;
    private string mFiles = null;
    private string mRefreshScript = null;

    #endregion


    #region "Properties"

    /// <summary>
    /// ID of the media library.
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
    /// Type of the action.
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
    /// Media library Folder path.
    /// </summary>
    public string FolderPath
    {
        get
        {
            return ValidationHelper.GetString(ViewState["FolderPath"], "");
        }
        set
        {
            ViewState["FolderPath"] = value;
        }
    }


    /// <summary>
    /// Path where the item(s) should be copied/moved.
    /// </summary>
    public string NewPath
    {
        get
        {
            return ValidationHelper.GetString(ViewState["NewPath"], "");
        }
        set
        {
            ViewState["NewPath"] = value;
        }
    }


    /// <summary>
    /// List of files to copy/move.
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
    /// Refresh script which is processed when the action is finished.
    /// </summary>
    public string RefreshScript
    {
        get
        {
            return this.mRefreshScript;
        }
        set
        {
            this.mRefreshScript = value;
        }
    }


    /// <summary>
    /// Determines whether all files should be copied.
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


    /// <summary>
    /// Indicates whether the control is just loaded.
    /// </summary>
    public bool IsLoad
    {
        get
        {
            return this.mIsLoad;
        }
        set
        {
            this.mIsLoad = value;
        }
    }


    /// <summary>
    /// Dialog control identifier.
    /// </summary>
    public string Identifier
    {
        get
        {
            string identifier = ValidationHelper.GetString(ViewState["Identifier"], null);
            if (identifier == null)
            {
                identifier = Guid.NewGuid().ToString("N");
                ViewState["Identifier"] = identifier;
            }

            return identifier;
        }
    }

    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        this.innerFrame.Attributes.Add("src", GetFrameUrl());
    }


    public void PerformAction()
    {
        mPerformAction = true;
        this.innerFrame.Attributes["src"] = GetFrameUrl();
    }


    /// <summary>
    /// Reloads control and its data.
    /// </summary>
    public void ReloadData()
    {
        this.innerFrame.Attributes["src"] = GetFrameUrl();
    }


    /// <summary>
    /// Returns correct URL for IFrame.
    /// </summary>
    private string GetFrameUrl()
    {
        string frameUrl = ResolveUrl("~/CMSModules/MediaLibrary/Controls/MediaLibrary/FolderActions/CopyMoveFolder.aspx");

        Hashtable properties = new Hashtable();
        // Fill properties table
        properties.Add("folderpath", FolderPath);
        properties.Add("libraryid", MediaLibraryID);
        properties.Add("newpath", NewPath);
        properties.Add("files", Files);
        properties.Add("allFiles", AllFiles);
        properties.Add("load", IsLoad);
        properties.Add("performaction", mPerformAction);
        properties.Add("action", Action);

        WindowHelper.Add(Identifier, properties);

        frameUrl = URLHelper.AddParameterToUrl(frameUrl, "params", Identifier);
        frameUrl = URLHelper.AddParameterToUrl(frameUrl, "hash", QueryHelper.GetHash(frameUrl));
        return frameUrl;
    }
}
