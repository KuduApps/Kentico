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
using CMS.GlobalHelper;

public partial class CMSModules_MediaLibrary_Controls_MediaLibrary_FolderActions_FolderActions : CMSAdminItemsControl
{
    /// <summary>
    /// Delegate for the events fired whenever some action occurs.
    /// </summary>
    public event OnActionEventHandler OnAction;


    #region "Variables"

    private string mDeleteScript = "";

    #endregion


    #region "Public properties"

    /// <summary>
    /// Path to the folder currently processed.
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
    /// Currently processed library ID.
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
        }
    }


    /// <summary>
    /// Indicates whether the DELETE action should be displayed.
    /// </summary>
    public bool DisplayDelete
    {
        get
        {
            return this.plcDelete.Visible;
        }
        set
        {
            this.plcDelete.Visible = value;
        }
    }


    /// <summary>
    /// Indicates whether the COPY action should be displayed.
    /// </summary>
    public bool DisplayCopy
    {
        get
        {
            return this.plcCopy.Visible;
        }
        set
        {
            this.plcCopy.Visible = value;
        }
    }


    /// <summary>
    /// Indicates whether the copy action is enabled.
    /// </summary>
    public bool CopyEnabled
    {
        get
        {
            return this.lnkCopy.Enabled;
        }
        set
        {
            this.lnkCopy.Enabled = value;
        }
    }


    /// <summary>
    /// Indicates whether the MOVE action should be displayed.
    /// </summary>
    public bool DisplayMove
    {
        get
        {
            return this.plcMove.Visible;
        }
        set
        {
            this.plcMove.Visible = value;
        }
    }


    /// <summary>
    /// JavaScript called when Delete button is clicked. If specified no postback is raised.
    /// </summary>
    public string DeleteScript
    {
        get
        {
            return this.mDeleteScript;
        }
        set
        {
            this.mDeleteScript = value;
        }
    }

    #endregion


    protected override void OnLoad(EventArgs e)
    {
        RaiseOnCheckPermissions(CMSAdminControl.PERMISSION_READ, this);

        base.OnLoad(e);
    }


    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (!this.StopProcessing)
        {
            // Initialize nested controls
            SetupControl();
        }
        else
        {
            this.Visible = false;
        }
    }


    #region "Event handlers"

    protected void lnkRename_Click(object sender, EventArgs e)
    {
        RaiseOnActionEvent("rename");
    }


    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        RaiseOnActionEvent("delete");
    }


    protected void lnkNew_Click(object sender, EventArgs e)
    {
        RaiseOnActionEvent("new");
    }

    #endregion


    #region "Private methods"

    /// <summary>
    /// Initializes all the nested controls and control itself.
    /// </summary>
    private void SetupControl()
    {
        // Register modal dialog handling script
        ScriptHelper.RegisterDialogScript(this.Page);

        // Setup copy action
        this.lnkCopy.Text = GetString("general.copy");
        this.lnkCopy.ToolTip = GetString("media.tree.copyfolder");
        this.imgCopy.ToolTip = GetString("media.tree.copyfolder");

        if (this.lnkCopy.Enabled)
        {
            this.imgCopy.ImageUrl = ResolveUrl(GetImageUrl("CMSModules/CMS_MediaLibrary/librarycopy.png", IsLiveSite));
        }
        else
        {
            this.imgCopy.ImageUrl = ResolveUrl(GetImageUrl("CMSModules/CMS_MediaLibrary/librarycopydisabled.png", IsLiveSite));
            this.lnkCopy.Attributes["style"] = "cursor: default;";
            this.lnkCopy.OnClientClick = "";
        }

        // Setup move action
        this.lnkMove.Text = GetString("general.move");
        this.lnkMove.ToolTip = GetString("media.tree.movefolder");
        this.imgMove.ToolTip = GetString("media.tree.movefolder");

        // Setup delete action
        this.lnkDelete.Text = GetString("general.delete");
        this.lnkDelete.ToolTip = GetString("media.folder.delete");
        this.imgDelete.ToolTip = GetString("media.folder.delete");

        // If delete script is set
        if (!string.IsNullOrEmpty(this.DeleteScript))
        {
            // Register delete script
            this.lnkDelete.OnClientClick = this.DeleteScript.Replace("##FOLDERPATH##", this.FolderPath.Replace("\\", "/").Replace("'", "\\'"));
            this.lnkDelete.Attributes["href"] = "#";
        }

        // If folder path is set
        if (String.IsNullOrEmpty(this.FolderPath))
        {
            // Disable delete action
            this.imgDelete.ImageUrl = ResolveUrl(GetImageUrl("CMSModules/CMS_MediaLibrary/librarydeletedisabled.png", IsLiveSite));
            this.lnkDelete.Enabled = false;
            this.lnkDelete.Attributes["style"] = "cursor: default;";
            this.lnkDelete.OnClientClick = "";

            // Disable move action
            this.imgMove.ImageUrl = ResolveUrl(GetImageUrl("CMSModules/CMS_MediaLibrary/librarymovedisabled.png", IsLiveSite));
            this.lnkMove.Enabled = false;
            this.lnkMove.Attributes["style"] = "cursor: default;";
            this.lnkMove.OnClientClick = "";
        }
        else
        {
            // Set enabled images
            this.imgDelete.ImageUrl = ResolveUrl(GetImageUrl("CMSModules/CMS_MediaLibrary/librarydelete.png", IsLiveSite));
            this.imgMove.ImageUrl = ResolveUrl(GetImageUrl("CMSModules/CMS_MediaLibrary/librarymove.png", IsLiveSite));
        }
    }


    /// <summary>
    /// Fires the OnAction event.
    /// </summary>
    /// <param name="actionName">Name of the action that takes place</param>
    private void RaiseOnActionEvent(string actionName)
    {
        // Let other controls now the action takes place
        if (this.OnAction != null)
        {
            OnAction(actionName, this.FolderPath);
        }
    }


    protected override void OnPreRender(EventArgs e)
    {
        string url = (this.IsLiveSite) ? "~/CMSModules/MediaLibrary/CMSPages/SelectFolder.aspx" :
            "~/CMSModules/MediaLibrary/Tools/FolderActions/SelectFolder.aspx";
        string folderPath = HttpUtility.UrlPathEncode(this.FolderPath.Replace("\\", "\\\\")).Replace("'", "%27").Replace("&", "%26").Replace("#", "%23").Replace("+", "%2B").Replace("{", "%7B").Replace("}", "%7D");

        // Add query into url
        url += "?action={0}&folderpath=" + folderPath + "&libraryid=" + this.LibraryID;

        // Create copy and move url
        string copyUrl = String.Format(url, "copy");
        string moveUrl = String.Format(url, "move");

        // Add security hash to urls
        copyUrl = URLHelper.AddParameterToUrl(copyUrl, "hash", QueryHelper.GetHash(copyUrl, false));
        moveUrl = URLHelper.AddParameterToUrl(moveUrl, "hash", QueryHelper.GetHash(moveUrl, false));

        // Register modal dialogs
        this.lnkCopy.OnClientClick = "modalDialog('" + ResolveUrl(copyUrl) + "', 'CopyFolder', '90%', '70%'); return false;";
        if (!String.IsNullOrEmpty(this.FolderPath))
        {
            this.lnkMove.OnClientClick = "modalDialog('" + ResolveUrl(moveUrl) + "', 'MoveFolder', '90%', '70%'); return false;";
        }

        base.OnPreRender(e);
    }

    #endregion
}
