using System;
using System.Web.UI.WebControls;

using CMS.GlobalHelper;
using CMS.SiteProvider;
using CMS.CMSHelper;
using CMS.SettingsProvider;
using CMS.Community;
using CMS.FormControls;
using CMS.IO;

public partial class CMSModules_Groups_FormControls_GroupPictureEdit : FormEngineUserControl
{
    #region "Variables"

    private CurrentSiteInfo site;
    private GroupInfo mGroupInfo;
    private int mMaxSideSize = 100;
    private AvatarTypeEnum avatarType = AvatarTypeEnum.Group;
    private bool isValidated = false;
    private int avatarID = 0;

    public string divId = string.Empty;
    public string placeholderId = string.Empty;

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
            return picGroup.Width;
        }
        set
        {
            picGroup.Width = ValidationHelper.GetInteger(value, 0);
        }
    }


    /// <summary>
    /// Max picture height.
    /// </summary>
    public int MaxPictureHeight
    {
        get
        {
            return picGroup.Height;
        }
        set
        {
            picGroup.Height = ValidationHelper.GetInteger(value, 0);
        }
    }


    /// <summary>
    /// Keep aspect ratio.
    /// </summary>
    public bool KeepAspectRatio
    {
        get
        {
            return picGroup.KeepAspectRatio;
        }
        set
        {
            picGroup.KeepAspectRatio = value;
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
    /// Group information.
    /// </summary>
    public GroupInfo GroupInfo
    {
        get
        {
            return mGroupInfo;
        }
        set
        {
            mGroupInfo = value;
            if (mGroupInfo != null)
            {
                picGroup.GroupID = mGroupInfo.GroupID;
                if (mGroupInfo.GroupAvatarID != 0)
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
            picGroup.Width = value;
            picGroup.Height = value;
            picGroup.KeepAspectRatio = true;
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

            // If group info not specified
            if (GroupInfo == null)
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
                    if ((uplFilePicture.PostedFile != null) && (uplFilePicture.PostedFile.ContentLength > 0) && ImageHelper.IsImage(Path.GetExtension(uplFilePicture.PostedFile.FileName)))
                    {
                        // Change delete to false because file will be replaced
                        pseudoDelete = false;

                        // If some avatar exists and is custom
                        if ((ai != null) && (ai.AvatarIsCustom))
                        {
                            // Delete file and upload new
                            AvatarInfoProvider.DeleteAvatarFile(ai.AvatarGUID.ToString(), ai.AvatarFileExtension, false, false);
                            AvatarInfoProvider.UploadAvatar(ai, uplFilePicture.PostedFile,
                                SettingsKeyProvider.GetIntValue(site.SiteName + ".CMSGroupAvatarWidth"),
                                SettingsKeyProvider.GetIntValue(site.SiteName + ".CMSGroupAvatarHeight"),
                                SettingsKeyProvider.GetIntValue(site.SiteName + ".CMSGroupAvatarMaxSideSize"));
                        }
                        else
                        {
                            // Create new avatar
                            ai = new AvatarInfo(uplFilePicture.PostedFile,
                                SettingsKeyProvider.GetIntValue(site.SiteName + ".CMSGroupAvatarWidth"),
                                SettingsKeyProvider.GetIntValue(site.SiteName + ".CMSGroupAvatarHeight"),
                                SettingsKeyProvider.GetIntValue(site.SiteName + ".CMSGroupAvatarMaxSideSize"));

                            if (CommunityContext.CurrentGroup != null)
                            {
                                ai.AvatarName = AvatarInfoProvider.GetUniqueAvatarName(GetString("avat.custom") + " " + CommunityContext.CurrentGroup.GroupName);
                            }
                            else
                            {
                                ai.AvatarName = AvatarInfoProvider.GetUniqueAvatarName(ai.AvatarFileName.Substring(0, ai.AvatarFileName.LastIndexOf(".")));
                            }
                            ai.AvatarType = AvatarTypeEnum.Group.ToString();
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
                                AvatarInfoProvider.DeleteAvatarInfo(ai);
                            }
                        }

                        ai = null;
                        avatarID = 0;
                        plcImageActions.Visible = false;
                        picGroup.AvatarID = 0;
                    }

                    // Update avatar id
                    if (ai != null)
                    {
                        avatarID = ai.AvatarID;
                    }
                }

                if (avatarID != 0)
                {
                    picGroup.AvatarID = avatarID;
                    plcImageActions.Visible = true;
                }

                // Return
                return avatarID;
            }
            else
            {
                return GroupInfo.GroupAvatarID;
            }
        }
        set
        {
            avatarID = ValidationHelper.GetInteger(value, 0);
            if (GroupInfo != null)
            {
                GroupInfo.GroupAvatarID = avatarID;
            }
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

        // Setup site info        
        site = CMSContext.CurrentSite;

        // Create id for div with selected image preview
        divId = ClientID + "imgDiv";
        placeholderId = plcImageActions.ClientID;

        // Setup delete image properties
        btnDeleteImage.ImageUrl = GetImageUrl("Design/Controls/UniGrid/Actions/delete.png");
        btnDeleteImage.OnClientClick = "return deleteAvatar('" + hiddenDeleteAvatar.ClientID + "', '" + hiddenAvatarGuid.ClientID + "', '" + placeholderId + "' );";
        btnDeleteImage.AlternateText = GetString("general.delete");

        // Setup show gallery button
        btnShowGallery.Text = GetString("avat.selector.select");
        btnShowGallery.Visible = SettingsKeyProvider.GetBoolValue(site.SiteName + ".CMSEnableDefaultAvatars");

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
        ltlScript.Text = ScriptHelper.GetScript("function UpdateForm(){ ; } \n");

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

        // Try to load avatar 
        if ((GroupInfo == null) && (!RequestHelper.IsPostBack()))
        {
            if (avatarID != 0)
            {
                plcImageActions.Visible = true;
                picGroup.AvatarID = avatarID;
            }
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
    /// Updates picture of current group.
    /// </summary>
    /// <param name="gi">Group info object</param>
    public void UpdateGroupPicture(GroupInfo gi)
    {
        AvatarInfo ai = null;

        if (gi != null)
        {
            // Delete avatar if needed
            if (hiddenDeleteAvatar.Value == "true")
            {
                DeleteOldGroupPicture(gi);
            }

            // If some file was uploaded
            if ((uplFilePicture.PostedFile != null) && (uplFilePicture.PostedFile.ContentLength > 0) && ImageHelper.IsImage(Path.GetExtension(uplFilePicture.PostedFile.FileName)))
            {
                // Check if this grou[ has some avatar and if so check if is custom
                ai = AvatarInfoProvider.GetAvatarInfoWithoutBinary(gi.GroupAvatarID);
                bool isCustom = false;
                if ((ai != null) && ai.AvatarIsCustom)
                {
                    isCustom = true;
                }

                if (isCustom)
                {
                    AvatarInfoProvider.UploadAvatar(ai, uplFilePicture.PostedFile,
                        SettingsKeyProvider.GetIntValue(site.SiteName + ".CMSGroupAvatarWidth"),
                        SettingsKeyProvider.GetIntValue(site.SiteName + ".CMSGroupAvatarHeight"),
                        SettingsKeyProvider.GetIntValue(site.SiteName + ".CMSGroupAvatarMaxSideSize"));
                }
                else
                {
                    // Delete old picture
                    DeleteOldGroupPicture(gi);

                    ai = new AvatarInfo(uplFilePicture.PostedFile,
                        SettingsKeyProvider.GetIntValue(site.SiteName + ".CMSGroupAvatarWidth"),
                        SettingsKeyProvider.GetIntValue(site.SiteName + ".CMSGroupAvatarHeight"),
                        SettingsKeyProvider.GetIntValue(site.SiteName + ".CMSGroupAvatarMaxSideSize"));

                    ai.AvatarName = AvatarInfoProvider.GetUniqueAvatarName(GetString("avat.custom") + " " + gi.GroupName);
                    ai.AvatarType = AvatarTypeEnum.Group.ToString();
                    ai.AvatarIsCustom = true;
                    ai.AvatarGUID = Guid.NewGuid();
                }

                AvatarInfoProvider.SetAvatarInfo(ai);

                // Update group info
                gi.GroupAvatarID = ai.AvatarID;
                GroupInfoProvider.SetGroupInfo(gi);

                plcImageActions.Visible = true;
            }
            // If predefined was chosen 
            else if (!string.IsNullOrEmpty(hiddenAvatarGuid.Value))
            {
                // Delete old picture 
                DeleteOldGroupPicture(gi);

                Guid guid = ValidationHelper.GetGuid(hiddenAvatarGuid.Value, Guid.NewGuid());
                ai = AvatarInfoProvider.GetAvatarInfoWithoutBinary(guid);

                // Update group info
                if (ai != null)
                {
                    gi.GroupAvatarID = ai.AvatarID;
                    GroupInfoProvider.SetGroupInfo(gi);
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
    /// Deletes group picture.
    /// </summary>
    /// <param name="gi">GroupInfo</param>
    public static void DeleteOldGroupPicture(GroupInfo gi)
    {
        // Delete old picture if needed
        if (gi.GroupAvatarID != 0)
        {
            // Delete avatar info provider if needed
            AvatarInfo ai = AvatarInfoProvider.GetAvatarInfoWithoutBinary(gi.GroupAvatarID);
            if (ai != null)
            {
                if (ai.AvatarIsCustom)
                {
                    AvatarInfoProvider.DeleteAvatarInfo(ai);
                    AvatarInfoProvider.DeleteAvatarFile(ai.AvatarGUID.ToString(), ai.AvatarFileExtension, false, false);

                }

                gi.GroupAvatarID = 0;
                GroupInfoProvider.SetGroupInfo(gi);
            }
        }
    }

    #endregion
}
