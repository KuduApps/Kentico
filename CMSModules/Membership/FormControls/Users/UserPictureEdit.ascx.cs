using System;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;

using CMS.GlobalHelper;
using CMS.SiteProvider;
using CMS.CMSHelper;
using CMS.SettingsProvider;
using CMS.FormControls;
using CMS.IO;

public partial class CMSModules_Membership_FormControls_Users_UserPictureEdit : FormEngineUserControl
{
    #region "Variables"

    private UserInfo mUserInfo = null;
    private int mMaxSideSize = 0;
    private AvatarTypeEnum avatarType = AvatarTypeEnum.User;
    private int avatarID = 0;
    private bool isValidated = false;

    public string divId = string.Empty;
    public string placeholderId = string.Empty;

    #endregion


    #region "Private properties"

    /// <summary>
    /// Indicates whether there were already an attempt to load an avatar.
    /// </summary>
    private bool AvatarAlreadyLoaded
    {
        get
        {
            return ValidationHelper.GetBoolean(ViewState["AvatarAlreadyLoaded"], false);
        }
        set
        {
            ViewState["AvatarAlreadyLoaded"] = value;
        }
    }

    #endregion


    #region "Public methods"

    /// <summary>
    /// Gets or sets the enabled state of the control.
    /// </summary>
    public override bool Enabled
    {
        set
        {
            btnDeleteImage.Enabled = value;
            uplFilePicture.Enabled = value;
            base.Enabled = value;
        }
        get
        {
            return base.Enabled;
        }
    }


    /// <summary>
    /// Max picture width.
    /// </summary>
    public int MaxPictureWidth
    {
        get
        {
            return picUser.Width;
        }
        set
        {
            picUser.Width = ValidationHelper.GetInteger(value, 0);
        }
    }


    /// <summary>
    /// Max picture height.
    /// </summary>
    public int MaxPictureHeight
    {
        get
        {
            return picUser.Height;
        }
        set
        {
            picUser.Height = ValidationHelper.GetInteger(value, 0);
        }
    }


    /// <summary>
    /// Keep aspect ratio.
    /// </summary>
    public bool KeepAspectRatio
    {
        get
        {
            return picUser.KeepAspectRatio;
        }
        set
        {
            picUser.KeepAspectRatio = value;
        }
    }


    /// <summary>
    /// Max upload file/picture field width.
    /// </summary>
    public int FileUploadFieldWidth
    {
        get
        {
            return (int)uplFilePicture.Width.Value;
        }
        set
        {
            uplFilePicture.Width = Unit.Pixel(value);
        }
    }


    /// <summary>
    /// User information.
    /// </summary>
    public UserInfo UserInfo
    {
        get
        {
            return mUserInfo;
        }
        set
        {
            mUserInfo = value;
            if (mUserInfo != null)
            {

                picUser.UserID = mUserInfo.UserID;
                if (mUserInfo.UserPicture != "" || mUserInfo.UserAvatarID != 0)
                {
                    plcImageActions.Visible = true;
                }
                else
                {
                    plcImageActions.Visible = false;
                }
            }
        }
    }


    /// <summary>
    /// Maximal side size.
    /// </summary>
    public int MaxSideSize
    {
        get
        {
            return mMaxSideSize;
        }
        set
        {
            picUser.Width = value;
            picUser.Height = value;
            picUser.KeepAspectRatio = true;
            mMaxSideSize = value;
        }
    }


    /// <summary>
    /// Gets or sets value - AvatarID.
    /// </summary>
    public override object Value
    {
        get
        {
            bool pseudoDelete = false;

            // If user info not specified
            if (UserInfo == null)
            {
                if (isValidated)
                {
                    // Check if some file was deleted
                    if (hiddenDeleteAvatar.Value.ToLower() == "true")
                    {
                        pseudoDelete = true;
                    }

                    AvatarInfo ai = null;

                    // Try to get avatar info
                    if (avatarID != 0)
                    {
                        ai = AvatarInfoProvider.GetAvatarInfoWithoutBinary(avatarID);
                    }

                    // If some new picture was uploaded
                    if ((uplFilePicture.PostedFile != null) && (uplFilePicture.PostedFile.ContentLength > 0))
                    {
                        // Change delete to false because file will be replaced
                        pseudoDelete = false;

                        // If some avatar exists and is custom
                        if ((ai != null) && (ai.AvatarIsCustom))
                        {
                            // Delete file and upload new
                            AvatarInfoProvider.DeleteAvatarFile(ai.AvatarGUID.ToString(), ai.AvatarFileExtension, false, false);
                            AvatarInfoProvider.UploadAvatar(ai, uplFilePicture.PostedFile,
                                SettingsKeyProvider.GetIntValue(CMSContext.CurrentSiteName + ".CMSAvatarWidth"),
                                SettingsKeyProvider.GetIntValue(CMSContext.CurrentSiteName + ".CMSAvatarHeight"),
                                SettingsKeyProvider.GetIntValue(CMSContext.CurrentSiteName + ".CMSAvatarMaxSideSize"));
                        }
                        else
                        {
                            // Create new avatar
                            ai = new AvatarInfo(uplFilePicture.PostedFile,
                                SettingsKeyProvider.GetIntValue(CMSContext.CurrentSiteName + ".CMSAvatarWidth"),
                                SettingsKeyProvider.GetIntValue(CMSContext.CurrentSiteName + ".CMSAvatarHeight"),
                                SettingsKeyProvider.GetIntValue(CMSContext.CurrentSiteName + ".CMSAvatarMaxSideSize"));

                            if ((CMSContext.CurrentUser != null) && CMSContext.CurrentUser.IsAuthenticated() &&
                                (CMSContext.ViewMode == CMS.PortalEngine.ViewModeEnum.LiveSite))
                            {
                                ai.AvatarName = AvatarInfoProvider.GetUniqueAvatarName(GetString("avat.custom") + " " + CMSContext.CurrentUser.UserName);
                            }
                            else
                            {
                                ai.AvatarName = AvatarInfoProvider.GetUniqueAvatarName(ai.AvatarFileName.Substring(0, ai.AvatarFileName.LastIndexOf(".")));
                            }
                            ai.AvatarType = AvatarTypeEnum.User.ToString();
                            ai.AvatarIsCustom = true;
                            ai.AvatarGUID = Guid.NewGuid();
                        }

                        // Update database
                        AvatarInfoProvider.SetAvatarInfo(ai);
                    }
                    // If some predefined avatar was selected
                    else if (!string.IsNullOrEmpty(hiddenAvatarGuid.Value))
                    {
                        // Change delete to false because file will be replaced
                        pseudoDelete = false;

                        // If some avatar exists and is custom
                        if ((ai != null) && (ai.AvatarIsCustom))
                        {
                            AvatarInfoProvider.DeleteAvatarFile(ai.AvatarGUID.ToString(), ai.AvatarFileExtension, false, false);
                            AvatarInfoProvider.DeleteAvatarInfo(ai);
                        }

                        Guid guid = ValidationHelper.GetGuid(hiddenAvatarGuid.Value, Guid.NewGuid());
                        ai = AvatarInfoProvider.GetAvatarInfoWithoutBinary(guid);
                    }

                    // If file was deleted - not replaced
                    if (pseudoDelete)
                    {
                        // Delete it
                        ai = AvatarInfoProvider.GetAvatarInfoWithoutBinary(avatarID);
                        if (ai != null)
                        {
                            if (ai.AvatarIsCustom)
                            {
                                AvatarInfoProvider.DeleteAvatarInfo(ai);
                            }
                        }

                        ai = null;
                        avatarID = 0;
                        plcImageActions.Visible = false;
                        picUser.AvatarID = 0;
                    }

                    // Update avatar id
                    if (ai != null)
                    {
                        avatarID = ai.AvatarID;
                    }
                }

                // Show avatar
                if (avatarID != 0)
                {
                    picUser.AvatarID = avatarID;
                    plcImageActions.Visible = true;
                }

                // Return
                return avatarID;
            }
            else
            {
                return UserInfo.UserAvatarID;
            }
        }
        set
        {
            if (UserInfo != null)
            {
                UserInfo.UserAvatarID = ValidationHelper.GetInteger(value, 0);
            }

            avatarID = ValidationHelper.GetInteger(value, 0);
            picUser.AvatarID = avatarID;
        }
    }

    #endregion


    #region "Events"

    /// <summary>
    /// Page load.
    /// </summary>
    protected void Page_Load(object sender, EventArgs e)
    {
        // Get resource strings
        lblUploader.Text = GetString("filelist.btnupload") + ResHelper.Colon;

        // Create id for div with selected image preview
        divId = ClientID + "imgDiv";
        placeholderId = plcImageActions.ClientID;

        // Setup delete image properties
        btnDeleteImage.ImageUrl = GetImageUrl("Design/Controls/UniGrid/Actions/delete.png");
        btnDeleteImage.OnClientClick = "return deleteAvatar('" + hiddenDeleteAvatar.ClientID + "', '" + hiddenAvatarGuid.ClientID + "', '" + placeholderId + "' );";
        btnDeleteImage.ToolTip = GetString("general.delete");
        btnDeleteImage.AlternateText = btnDeleteImage.ToolTip;


        // Setup show gallery button
        btnShowGallery.Text = GetString("avat.selector.select");
        btnShowGallery.Visible = SettingsKeyProvider.GetBoolValue(CMSContext.CurrentSiteName + ".CMSEnableDefaultAvatars");

        // Register dialog script
        string resolvedAvatarsPage = string.Empty;
        if (IsLiveSite)
        {
            if (CMSContext.CurrentUser.IsAuthenticated())
            {
                resolvedAvatarsPage = CMSContext.ResolveDialogUrl("~/CMSModules/Avatars/CMSPages/AvatarsGallery.aspx");
            }
            else
            {
                resolvedAvatarsPage = CMSContext.ResolveDialogUrl("~/CMSModules/Avatars/CMSPages/PublicAvatarsGallery.aspx");
            }
        }
        else
        {
            resolvedAvatarsPage = ResolveUrl("~/CMSModules/Avatars/Dialogs/AvatarsGallery.aspx");
        }

        ScriptHelper.RegisterDialogScript(this.Page);
        ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "SelectAvatar",
        ScriptHelper.GetScript("function SelectAvatar(avatarType, clientId) { " +
            "modalDialog('" + resolvedAvatarsPage + "?avatartype=' + avatarType + '&clientid=' + clientId, 'permissionDialog', 600, 270); return false;}"));
        ltlScript.Text = ScriptHelper.GetScript("function UpdateForm(){ ; } \n ");

        // Setup btnShowGallery action
        btnShowGallery.Attributes.Add("onclick", "SelectAvatar('" + AvatarInfoProvider.GetAvatarTypeString(avatarType) + "', '" + ClientID + "'); return false;");

        // Get image size param(s) for preview
        string sizeParams = string.Empty;
        // Keep aspect ratio is set - property was set directly or indirectly by max side size property.  
        if (KeepAspectRatio)
        {
            sizeParams += "&maxsidesize=" + (MaxPictureWidth > MaxPictureHeight ? MaxPictureWidth : MaxPictureHeight);
        }
        else
        {
            sizeParams += "&width=" + MaxPictureWidth + "&height=" + MaxPictureHeight;
        }

        // Javascript which creates selected image preview and saves image guid  to hidden field
        string getAvatarPath = ResolveUrl("~/CMSModules/Avatars/CMSPages/GetAvatar.aspx");

        string updateHiddenScript = ScriptHelper.GetScript("function " + ClientID + "updateHidden(guidPrefix, clientId)" +
        "{" +
        "if ( clientId == '" + ClientID + "')" +
        "{" +
        "avatarGuid = guidPrefix.substring(4);" +
        "if ( avatarGuid != '')" +
        "{" +
        "hidden = document.getElementById('" + hiddenAvatarGuid.ClientID + "');" +
        "hidden.value = avatarGuid ;" +
        "div = document.getElementById('" + divId + "');" +
        "div.style.display='';" +
        "div.innerHTML = '<img src=\"" + getAvatarPath + "?avatarguid=" + "'+ avatarGuid + '" + sizeParams + "\" />" +
        "&#13;&#10;&nbsp;<img src=\"" + btnDeleteImage.ImageUrl + "\" border=\"0\" onclick=\"deleteImagePreview(\\'" + hiddenAvatarGuid.ClientID + "\\',\\'" + divId + "\\')\" style=\"cursor:pointer\"/>';" +
        "placeholder = document.getElementById('" + plcImageActions.ClientID + "');" +
        "if ( placeholder != null)" +
        "{" +
        "placeholder.style.display='none';" +
        "}" +
        "}" +
        "}" +
        "}");

        ScriptHelper.RegisterClientScriptBlock(this, typeof(string), ClientID + "updateHidden", updateHiddenScript);

        // Javascript which deletes image preview
        string deleteImagePreviewScript = ScriptHelper.GetScript("function deleteImagePreview(hiddenId, divId)" +
        "{" +
        "if( confirm(" + ScriptHelper.GetString(GetString("myprofile.pictdeleteconfirm")) + "))" +
        "{" +
        "hidden = document.getElementById(hiddenId);" +
        "hidden.value = '' ;" +
        "div = document.getElementById(divId);" +
        "div.style.display='none';" +
        "div.innerHTML = ''; " +
        "}" +
        "}");

        ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "deleteImagePreviewScript", deleteImagePreviewScript);

        // Javascript which pseudo deletes avatar 
        string deleteAvatarScript = ScriptHelper.GetScript("function deleteAvatar(hiddenDeleteId, hiddenGuidId, placeholderId)" +
        "{" +
        "if( confirm(" + ScriptHelper.GetString(GetString("myprofile.pictdeleteconfirm")) + "))" +
        "{" +
        "hidden = document.getElementById(hiddenDeleteId);" +
        "hidden.value = 'true' ;" +
        "placeholder = document.getElementById(placeholderId);" +
        "placeholder.style.display='none';" +
        "hidden = document.getElementById(hiddenGuidId);" +
        "hidden.value = '' ;" +
        "}" +
        "return false; " +
        "}");
        ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "deleteAvatar", deleteAvatarScript);

        // Try to load avatar either on first load or when it is first attempt to load an avatar
        if ((UserInfo == null) && (!RequestHelper.IsPostBack() || !AvatarAlreadyLoaded) && (avatarID != 0))
        {
            AvatarAlreadyLoaded = true;

            plcImageActions.Visible = true;
            picUser.AvatarID = avatarID;
        }
    }

    #endregion


    #region "Methods"

    /// <summary>
    /// Is valid override.
    /// </summary>
    public override bool IsValid()
    {
        isValidated = true;
        if ((uplFilePicture.PostedFile != null) && (uplFilePicture.PostedFile.ContentLength > 0) && !ImageHelper.IsImage(Path.GetExtension(uplFilePicture.PostedFile.FileName)))
        {
            ErrorMessage = GetString("avat.filenotvalid");
            return false;
        }
        return true;
    }


    /// <summary>
    /// Updates picture of current user.
    /// </summary>
    /// <param name="ui">User info object</param>
    public void UpdateUserPicture(UserInfo ui)
    {
        AvatarInfo ai = null;

        if (ui != null)
        {
            // Check if some avatar should be deleted
            if (hiddenDeleteAvatar.Value == "true")
            {
                DeleteOldUserPicture(ui);
            }

            // If some file was uploaded
            if ((uplFilePicture.PostedFile != null) && (uplFilePicture.PostedFile.ContentLength > 0) && ImageHelper.IsImage(Path.GetExtension(uplFilePicture.PostedFile.FileName)))
            {
                // Check if this user has some avatar and if so check if is custom
                ai = AvatarInfoProvider.GetAvatarInfoWithoutBinary(ui.UserAvatarID);
                bool isCustom = false;
                if ((ai != null) && ai.AvatarIsCustom)
                {
                    isCustom = true;
                }

                if (isCustom)
                {
                    AvatarInfoProvider.UploadAvatar(ai, uplFilePicture.PostedFile,
                        SettingsKeyProvider.GetIntValue(CMSContext.CurrentSiteName + ".CMSAvatarWidth"),
                        SettingsKeyProvider.GetIntValue(CMSContext.CurrentSiteName + ".CMSAvatarHeight"),
                        SettingsKeyProvider.GetIntValue(CMSContext.CurrentSiteName + ".CMSAvatarMaxSideSize"));
                }
                // Old avatar is not custom, so crate new custom avatar
                else
                {

                    ai = new AvatarInfo(uplFilePicture.PostedFile,
                        SettingsKeyProvider.GetIntValue(CMSContext.CurrentSiteName + ".CMSAvatarWidth"),
                        SettingsKeyProvider.GetIntValue(CMSContext.CurrentSiteName + ".CMSAvatarHeight"),
                        SettingsKeyProvider.GetIntValue(CMSContext.CurrentSiteName + ".CMSAvatarMaxSideSize"));

                    ai.AvatarName = AvatarInfoProvider.GetUniqueAvatarName(GetString("avat.custom") + " " + ui.UserName);
                    ai.AvatarType = AvatarTypeEnum.User.ToString();
                    ai.AvatarIsCustom = true;
                    ai.AvatarGUID = Guid.NewGuid();
                }

                AvatarInfoProvider.SetAvatarInfo(ai);

                // Update user info
                ui.UserAvatarID = ai.AvatarID;
                UserInfoProvider.SetUserInfo(ui);

                plcImageActions.Visible = true;
            }
            // If predefined was chosen 
            else if (!string.IsNullOrEmpty(hiddenAvatarGuid.Value))
            {
                // Delete old picture 
                DeleteOldUserPicture(ui);

                Guid guid = ValidationHelper.GetGuid(hiddenAvatarGuid.Value, Guid.NewGuid());
                ai = AvatarInfoProvider.GetAvatarInfoWithoutBinary(guid);

                // Update user info
                if (ai != null)
                {
                    ui.UserAvatarID = ai.AvatarID;
                    UserInfoProvider.SetUserInfo(ui);
                }

                plcImageActions.Visible = true;
            }
            else
            {
                plcImageActions.Visible = false;
            }
        }
    }


    /// <summary>
    /// Deletes user picture.
    /// </summary>
    /// <param name="ui">UserInfo</param>
    public static void DeleteOldUserPicture(UserInfo ui)
    {
        // Delete old picture if needed
        if (ui.UserAvatarID != 0)
        {
            // Delete avatar info provider if needed
            AvatarInfo ai = AvatarInfoProvider.GetAvatarInfoWithoutBinary(ui.UserAvatarID);
            if (ai != null)
            {
                if (ai.AvatarIsCustom)
                {
                    AvatarInfoProvider.DeleteAvatarFile(ai.AvatarGUID.ToString(), ai.AvatarFileExtension, false, false);
                    AvatarInfoProvider.DeleteAvatarInfo(ai);
                }

                ui.UserAvatarID = 0;
                UserInfoProvider.SetUserInfo(ui);
                CMSContext.CurrentUser.UserAvatarID = 0;
            }
        }
        // Backward compatibility
        else if (ui.UserPicture != "")
        {
            try
            {
                // Remove from HDD
                string jDirectory = HttpContext.Current.Server.MapPath("~/CMSGlobalFiles/UserPictures/");
                string filename = ui.UserPicture.Remove(ui.UserPicture.IndexOf('/'));
                if (File.Exists(jDirectory + filename))
                {
                    File.Delete(jDirectory + filename);
                }
            }
            catch
            {
            }

            ui.UserPicture = "";
            UserInfoProvider.SetUserInfo(ui);
            CMSContext.CurrentUser.UserPicture = "";
        }
    }

    #endregion
}
