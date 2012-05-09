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
using CMS.CMSHelper;
using CMS.MediaLibrary;
using CMS.IO;
using CMS.SiteProvider;

public partial class CMSModules_MediaLibrary_Controls_MediaLibrary_FolderActions_EditFolder : CMSAdminControl
{
    #region "Delegates & Events"

    /// <summary>
    /// Delegate of event fired when 'Cancel' button of control is clicked.
    /// </summary>
    public delegate void OnCancelClickEventHandler();

    /// <summary>
    /// Delegate of event fired when folder has been deleted.
    /// </summary>
    public delegate void OnFolderChangeEventHandler(string pathToSelect);

    /// <summary>
    /// Event raised when 'Click' button is clicked.
    /// </summary>
    public event OnCancelClickEventHandler CancelClick;

    /// <summary>
    /// Event raised when folder has been deleted.
    /// </summary>
    public event OnFolderChangeEventHandler OnFolderChange;

    #endregion


    #region "Private variables"

    private int mLibraryId = 0;
    private string mLibraryFolder = null;
    private string mAction = null;
    private string mFolderPath = null;
    private string mNewFolderPath = null;
    private string mRootFolderPath = null;
    private string mNewTreePath = null;
    private string mCustomScript = null;
    private bool mCheckAdvancedPermissions = false;
    private bool mErrorOccurred = false;

    #endregion


    #region "Private properties"

    /// <summary>
    /// Indicates whether the error occurred during internal folder action processing.
    /// </summary>
    private bool ErrorOccurred
    {
        get
        {
            return this.mErrorOccurred;
        }
        set
        {
            this.mErrorOccurred = value;
        }
    }

    #endregion


    #region "Public properties"

    /// <summary>
    /// Indicates whether the properties should be stored in ViewState.
    /// </summary>
    public bool UseViewStateProperties
    {
        get
        {
            return ValidationHelper.GetBoolean(ViewState["UseViewStateProperties"], false);
        }
        set
        {
            ViewState["UseViewStateProperties"] = value;
        }
    }


    /// <summary>
    /// Indicates whether the CANCEL button should be displayed.
    /// </summary>
    public bool DisplayCancel
    {
        get
        {
            if (this.UseViewStateProperties)
            {
                return ValidationHelper.GetBoolean(ViewState["DisplayCancel"], true);
            }
            return this.plcCancelArea.Visible;
        }
        set
        {
            this.plcCancelArea.Visible = value;

            if (this.UseViewStateProperties)
            {
                ViewState["DisplayCancel"] = value;
            }
        }
    }


    /// <summary>
    /// Indicates whether the control and all the nested controls are enabled.
    /// </summary>
    public bool Enabled
    {
        get
        {
            if (this.UseViewStateProperties)
            {
                return ValidationHelper.GetBoolean(ViewState["Enabled"], true);
            }
            return this.txtFolderName.Enabled;
        }
        set
        {
            this.txtFolderName.Enabled = value;
            this.btnOk.Enabled = value;
            this.btnCancel.Enabled = value;

            if (this.UseViewStateProperties)
            {
                ViewState["Enabled"] = value;
            }
        }
    }


    /// <summary>
    /// Indicates whether the control is loaded for new folder creation.
    /// </summary>
    public string Action
    {
        get
        {
            if (this.UseViewStateProperties && (this.mAction == null))
            {
                this.mAction = ValidationHelper.GetString(ViewState["Action"], null);
            }
            return this.mAction;
        }
        set
        {
            this.mAction = value;

            if (this.UseViewStateProperties)
            {
                ViewState["Action"] = this.mAction;
            }
        }
    }


    /// <summary>
    /// JavaScript used for OnClick event of OK button.
    /// </summary>
    public string CustomScript
    {
        get
        {
            if (this.UseViewStateProperties && (this.mCustomScript == null))
            {
                this.mCustomScript = ValidationHelper.GetString(ViewState["CustomScript"], null);
            }
            return this.mCustomScript;
        }
        set
        {
            this.mCustomScript = value;

            if (this.UseViewStateProperties)
            {
                ViewState["CustomScript"] = this.mCustomScript;
            }
        }
    }


    /// <summary>
    /// Path of the media library folder.
    /// </summary>
    public string FolderPath
    {
        get
        {
            if (this.UseViewStateProperties && (this.mFolderPath == null))
            {
                this.mFolderPath = ValidationHelper.GetString(ViewState["FolderPath"], null);
            }
            return this.mFolderPath;
        }
        set
        {
            this.mFolderPath = value;

            if (this.UseViewStateProperties)
            {
                ViewState["FolderPath"] = this.mFolderPath;
            }
        }
    }


    /// <summary>
    /// ID of the currently processed media library.
    /// </summary>
    public int LibraryID
    {
        get
        {
            if (this.UseViewStateProperties && (this.mLibraryId == 0))
            {
                this.mLibraryId = ValidationHelper.GetInteger(ViewState["LibraryID"], 0);
            }
            return this.mLibraryId;
        }
        set
        {
            this.mLibraryId = value;

            if (this.UseViewStateProperties)
            {
                ViewState["LibraryID"] = this.mLibraryId;
            }
        }
    }


    /// <summary>
    /// Folder path of the currently processed library.
    /// </summary>
    public string LibraryFolder
    {
        get
        {
            if (this.UseViewStateProperties && (this.mLibraryFolder == null))
            {
                this.mLibraryFolder = ValidationHelper.GetString(ViewState["LibraryFolder"], null);
            }
            return this.mLibraryFolder;
        }
        set
        {
            this.mLibraryFolder = value;

            if (this.UseViewStateProperties)
            {
                ViewState["LibraryFolder"] = this.mLibraryFolder;
            }
        }
    }


    /// <summary>
    /// Gets or sets library root folder path.
    /// </summary>
    public string RootFolderPath
    {
        get
        {
            if (this.UseViewStateProperties && (this.mRootFolderPath == null))
            {
                this.mRootFolderPath = ValidationHelper.GetString(ViewState["RootFolderPath"], null);
            }
            return this.mRootFolderPath;
        }
        set
        {
            this.mRootFolderPath = value;

            if (this.UseViewStateProperties)
            {
                ViewState["RootFolderPath"] = this.mRootFolderPath;
            }
        }
    }


    /// <summary>
    /// Indicates whether the advanced permissions should be checked.
    /// </summary>
    public bool CheckAdvancedPermissions
    {
        get
        {
            return this.mCheckAdvancedPermissions;
        }
        set
        {
            this.mCheckAdvancedPermissions = value;
        }
    }


    #endregion


    protected override void OnLoad(EventArgs e)
    {
        RaiseOnCheckPermissions(CMSAdminControl.PERMISSION_READ, this);

        base.OnLoad(e);
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        if (!this.StopProcessing)
        {
            // Initialize control
            SetupControl();
        }
        else
        {
            this.Visible = false;
        }
    }


    /// <summary>
    /// Clears form controls content.
    /// </summary>
    public override void ClearForm()
    {
        this.txtFolderName.Text = "";
    }


    /// <summary>
    /// Reloads control.
    /// </summary>
    public override void ReloadData()
    {
        SetupControl();
    }


    /// <summary>
    /// Handles folder actions.
    /// </summary>
    public string ProcessFolderAction()
    {
        MediaLibraryInfo libInfo = MediaLibraryInfoProvider.GetMediaLibraryInfo(this.LibraryID);
        if (libInfo != null)
        {
            if (this.Action.ToLower().Trim() == "new")
            {
                if (this.CheckAdvancedPermissions)
                {
                    CurrentUserInfo currUser = CMSContext.CurrentUser;

                    // Not a global admin
                    if (!currUser.IsGlobalAdministrator)
                    {
                        // Group library
                        bool isGroupLibrary = (libInfo.LibraryGroupID > 0);
                        if (!(isGroupLibrary && currUser.IsGroupAdministrator(libInfo.LibraryGroupID)))
                        {
                            // Checked resource name
                            string resource = (isGroupLibrary) ? "CMS.Groups" : "CMS.MediaLibrary";

                            // Check 'CREATE' & 'MANAGE' permissions
                            if (!(currUser.IsAuthorizedPerResource(resource, CMSAdminControl.PERMISSION_MANAGE) || MediaLibraryInfoProvider.IsUserAuthorizedPerLibrary(libInfo, "foldercreate")))
                            {
                                this.lblError.Text = MediaLibraryHelper.GetAccessDeniedMessage("foldercreate");
                                this.lblError.Visible = true;
                                return null;
                            }
                        }
                    }
                }
                // Check 'Folder create' permission
                else if (!MediaLibraryInfoProvider.IsUserAuthorizedPerLibrary(libInfo, "foldercreate"))
                {
                    this.lblError.Text = MediaLibraryHelper.GetAccessDeniedMessage("foldercreate");
                    this.lblError.Visible = true;
                    return null;
                }
            }
            else
            {
                // Check 'Folder modify' permission
                if (!MediaLibraryInfoProvider.IsUserAuthorizedPerLibrary(libInfo, "foldermodify"))
                {
                    this.lblError.Text = MediaLibraryHelper.GetAccessDeniedMessage("foldermodify");
                    this.lblError.Visible = true;
                    return null;
                }
            }

            SiteInfo si = SiteInfoProvider.GetSiteInfo(libInfo.LibrarySiteID);
            if (si != null)
            {
                // Validate form entry
                string errMsg = ValidateForm(this.Action, si.SiteName);
                this.ErrorOccurred = !string.IsNullOrEmpty(errMsg);

                // If validation suceeded
                if (errMsg == "")
                {
                    try
                    {
                        // Update info only if folder was renamed
                        if (MediaLibraryHelper.EnsurePath(FolderPath) != MediaLibraryHelper.EnsurePath(mNewFolderPath))
                        {
                            if (this.Action.ToLower().Trim() == "new")
                            {
                                // Create/Update folder according to action
                                MediaLibraryInfoProvider.CreateMediaLibraryFolder(si.SiteName, LibraryID, mNewFolderPath, false);
                            }
                            else
                            {
                                // Create/Update folder according to action
                                MediaLibraryInfoProvider.RenameMediaLibraryFolder(si.SiteName, LibraryID, FolderPath, mNewFolderPath, false);
                            }

                            // Inform the user on success
                            this.lblInfo.Text = GetString("general.changessaved");
                            this.lblInfo.Visible = true;

                            // Refresh folder name
                            this.FolderPath = mNewFolderPath;
                            UpdateFolderName();

                            // Reload media library
                            if (OnFolderChange != null)
                            {
                                OnFolderChange(this.mNewTreePath);
                            }

                        }
                    }
                    catch (Exception ex)
                    {
                        // Display an error to the user
                        this.lblError.Text = GetString("general.erroroccurred") + " " + ex.Message;
                        this.lblError.Visible = true;

                        this.mNewTreePath = null;
                    }
                }
                else
                {
                    // Display an error to the user
                    this.lblError.Text = errMsg;
                    this.lblError.Visible = true;
                    this.mNewTreePath = null;
                }
            }
        }

        return this.mNewTreePath;
    }


    #region "Event handlers"

    protected void btnOk_Click(object sender, EventArgs e)
    {
        ProcessFolderAction();
    }


    protected void btnCancel_Click(object sender, EventArgs e)
    {
        // Let the parent control know about 'Cancel' button click
        if (CancelClick != null)
        {
            CancelClick();
        }
    }

    #endregion


    #region "Private methods"

    /// <summary>
    /// Initializes the control.
    /// </summary>
    private void SetupControl()
    {
        if (!String.IsNullOrEmpty(this.Action))
        {
            this.Visible = true;

            // Setup labels
            if (this.Action.ToLower().Trim() == "new")
            {
                this.pnlFolderEdit.CssClass = "MediaLibraryNewFolder";
                this.ltlScript.Text = GetFocusScript();
                this.divButtons.Attributes["class"] = "PageFooterLine FloatRight";
                this.pnlTabs.ShowTabs = false;
            }
            else
            {
                if (!this.ErrorOccurred)
                {
                    // Fill the text box with the name of the currently processed folder
                    UpdateFolderName();
                }
                this.tabGeneral.HeaderText = GetString("general.general");
            }

            this.btnOk.Text = GetString("general.ok");

            // Remove OnClick event when JavaScript is used to refresh page
            if (!string.IsNullOrEmpty(this.CustomScript))
            {
                this.btnOk.Attributes["onclick"] = this.CustomScript;
                this.txtFolderName.Attributes["onkeydown"] = "try { if (event.keyCode == 13) { " + this.CustomScript + " return false; } } catch (e) {}";
            }
            else
            {
                this.pnlContent.DefaultButton = "btnOk";
            }

            if (!this.Enabled)
            {
                DisableControls();
            }

            this.plcCancelArea.Visible = this.DisplayCancel;
            this.btnCancel.Text = GetString("general.cancel");
            this.btnCancel.Click += new EventHandler(btnCancel_Click);
            this.rfvFolderName.ErrorMessage = GetString("media.folder.foldernameempty");
        }
        else
        {
            // Hide control when action wasn't specified
            this.Visible = false;
        }
    }


    /// <summary>
    /// Updates folder name in the folder text box.
    /// </summary>
    private void UpdateFolderName()
    {
        string safeFolderPath = this.FolderPath.Replace("/", "\\");
        int folderNameStartIndex = safeFolderPath.LastIndexOf('\\') + 1;
        this.txtFolderName.Text = this.FolderPath.Substring(folderNameStartIndex);
    }


    /// <summary>
    /// Disables controls.
    /// </summary>
    private void DisableControls()
    {
        this.txtFolderName.Enabled = false;
        this.btnOk.Enabled = false;
        this.btnCancel.Enabled = false;
    }


    /// <summary>
    /// Validates form entries.
    /// </summary>    
    /// <param name="action">Action type</param>
    /// <param name="siteName">Site name</param>
    public string ValidateForm(string action, string siteName)
    {
        string errMsg = null;

        string newFolderName = this.txtFolderName.Text.Trim();

        errMsg = new Validator().NotEmpty(newFolderName, GetString("media.folder.foldernameempty")).
            IsFolderName(newFolderName, GetString("media.folder.foldernameerror")).Result;

        if (String.IsNullOrEmpty(errMsg))
        {
            // Check special folder names
            if ((newFolderName == ".") || (newFolderName == ".."))
            {
                errMsg = GetString("media.folder.foldernameerror");
            }

            if (String.IsNullOrEmpty(errMsg))
            {
                bool mustExist = true;

                // Make a note that we are renaming existing folder
                if ((!String.IsNullOrEmpty(this.Action)) && (this.Action.ToLower().Trim() == "new"))
                {
                    mustExist = false;
                }

                // Check if folder with specified name exists already if required
                if (mustExist)
                {
                    // Existing folder is being renamed
                    if (!Directory.Exists(MediaLibraryInfoProvider.GetMediaLibraryFolderPath(siteName, DirectoryHelper.CombinePath(this.LibraryFolder, this.FolderPath))))
                    {
                        errMsg = GetString("media.folder.folderdoesntexist");
                    }
                }

                if (String.IsNullOrEmpty(errMsg))
                {
                    if ((newFolderName == MediaLibraryHelper.GetMediaFileHiddenFolder(siteName)) || ValidationHelper.IsSpecialFolderName(newFolderName))
                    {
                        errMsg = GetString("media.folder.folderrestricted");
                    }

                    if (String.IsNullOrEmpty(errMsg))
                    {
                        // Get new folder path
                        GetNewFolderPath(mustExist);

                        if (MediaLibraryHelper.EnsurePath(FolderPath) != MediaLibraryHelper.EnsurePath(mNewFolderPath))
                        {
                            // Check if new folder doesn't exist yet
                            if (Directory.Exists(MediaLibraryInfoProvider.GetMediaLibraryFolderPath(siteName, DirectoryHelper.CombinePath(this.LibraryFolder, this.mNewFolderPath))))
                            {
                                errMsg = GetString("media.folder.folderexist");
                            }
                        }
                    }
                }
            }
        }

        return errMsg;
    }


    /// <summary>
    /// Sets the new folder path.
    /// </summary>    
    private void GetNewFolderPath(bool mustExist)
    {
        string trimFolderName = this.txtFolderName.Text.Trim();

        if (mustExist)
        {
            string folderPath = this.FolderPath.Replace('/', '\\');

            this.mNewFolderPath = GetParentPath(folderPath) + trimFolderName;
            if (folderPath.LastIndexOf("\\") > 0)
            {
                // Folder is in library tree
                this.mNewTreePath = DirectoryHelper.CombinePath(this.LibraryFolder, GetParentPath(folderPath), trimFolderName);
            }
            else
            {
                // Folder is in library root
                this.mNewTreePath = DirectoryHelper.CombinePath(this.LibraryFolder, trimFolderName);
            }
        }
        else
        {
            this.mNewFolderPath = DirectoryHelper.CombinePath(this.FolderPath, trimFolderName);
            if (this.FolderPath != "")
            {
                // Folder is in library tree
                this.mNewTreePath = DirectoryHelper.CombinePath(this.LibraryFolder, this.FolderPath, trimFolderName);
            }
            else
            {
                // Folder is in library root
                this.mNewTreePath = DirectoryHelper.CombinePath(this.LibraryFolder, trimFolderName);
            }
        }

        // Ensure paths are in correct format
        this.mNewFolderPath = this.mNewFolderPath.TrimStart('\\');
        this.mNewTreePath = this.mNewTreePath.Replace("\\\\", "\\").Replace('/', '\\');
    }


    /// <summary>
    /// Returns path of the parent folder of the specified folder.
    /// </summary>
    /// <param name="folderPath"></param>
    private static string GetParentPath(string folderPath)
    {
        if ((!string.IsNullOrEmpty(folderPath)) && (folderPath.LastIndexOf("\\") > -1))
        {
            return folderPath.Remove(folderPath.LastIndexOf("\\")) + "\\";
        }

        return "";
    }


    /// <summary>
    /// Returns script for focus folder name textbox.
    /// </summary>
    private string GetFocusScript()
    {
        string script = "function FocusFolderName(){\n" +
            "var txtBox = document.getElementById('" + this.txtFolderName.ClientID + "');\n" +
            "if (txtBox != null) { \n" +
            "   try {\n" +
            "       txtBox.focus();\n" +
            "   } catch (e) {\n" +
            "       setTimeout('FocusFolderName()',50);\n" +
            "   }\n" +
            "}\n" +
            "}\n";

        return ScriptHelper.GetScript(script);
    }

    #endregion
}
