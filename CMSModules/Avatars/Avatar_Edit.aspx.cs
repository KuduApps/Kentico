using System;
using System.Web.UI;

using CMS.GlobalHelper;
using CMS.SiteProvider;
using CMS.CMSHelper;
using CMS.SettingsProvider;
using CMS.UIControls;
using CMS.IO;

public partial class CMSModules_Avatars_Avatar_Edit : SiteManagerPage, IPostBackEventHandler
{
    #region "Variables"

    protected int avatarId = 0;
    protected AvatarInfo ai = null;

    #endregion


    #region "Events and methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        imgAvatar.Visible = false;

        avatarId = QueryHelper.GetInteger("avatarid", 0);

        if (QueryHelper.GetInteger("saved", 0) == 1)
        {
            lblInfo.Visible = true;
            lblInfo.Text = GetString("general.changessaved");
        }        

        // new avatar
        if (avatarId == 0)
        {
            CurrentMaster.Title.TitleText = GetString("avat.newavatar");
            CurrentMaster.Title.TitleImage = GetImageUrl("Objects/CMS_Avatar/new.png");
            drpAvatarType.AutoPostBack = false;
        }
        // Edit avatar
        else
        {
            CurrentMaster.Title.TitleText = GetString("avat.properties");
            CurrentMaster.Title.TitleImage = GetImageUrl("Objects/CMS_Avatar/object.png");
            drpAvatarType.AutoPostBack = true;
        }

        CurrentMaster.Title.HelpTopicName = "avatars_edit";
        CurrentMaster.Title.HelpName = "helpTopic";

        // initializes breadcrumbs 		
        string[,] pageTitleTabs = new string[2, 3];
        pageTitleTabs[0, 0] = GetString("avat.title");
        pageTitleTabs[0, 1] = "~/CMSModules/Avatars/Avatar_List.aspx";
        pageTitleTabs[0, 2] = "";
        pageTitleTabs[1, 0] = GetString("avat.newavatar");
        pageTitleTabs[1, 1] = "";
        pageTitleTabs[1, 2] = "";        

        lblSharedInfo.Text = String.Format(GetString("avat.convertinfo") + "<br /><br />", "<a href=\"javascript:" + Page.ClientScript.GetPostBackEventReference(this, "shared") + "\">" + GetString("General.clickhere") + "</a>");
          
        valAvatarName.ErrorMessage = GetString("avat.requiresname");

        btnOk.Text = GetString("general.ok");

        if (!RequestHelper.IsPostBack())
        {
            // Fill the drop down list
            DataHelper.FillListControlWithEnum(typeof(AvatarTypeEnum), drpAvatarType, "avat.type", AvatarInfoProvider.GetAvatarTypeString);
        }

        if (avatarId > 0)
        {
            plcImage.Visible = true;

            ai = AvatarInfoProvider.GetAvatarInfoWithoutBinary(avatarId);
            if (ai != null)
            {
                if (ai.AvatarIsCustom)
                {
                    lblSharedInfo.Visible = true;
                }

                pageTitleTabs[1, 0] = HTMLHelper.HTMLEncode(!string.IsNullOrEmpty(ai.AvatarName) ? ai.AvatarName : ai.AvatarFileName.Substring(0, ai.AvatarFileName.LastIndexOf(".")));

                // Load avatars data
                if (!RequestHelper.IsPostBack())
                {
                    txtAvatarName.Text = ai.AvatarName;
                    drpAvatarType.SelectedValue = ai.AvatarType.ToLower();                    
                    chkDefaultUserAvatar.Checked = ai.DefaultUserAvatar;
                    chkDefaultMaleUserAvatar.Checked = ai.DefaultMaleUserAvatar;
                    chkDefaultFemaleUserAvatar.Checked = ai.DefaultFemaleUserAvatar;
                    chkDefaultGroupAvatar.Checked = ai.DefaultGroupAvatar;
                    imgAvatar.AlternateText = HTMLHelper.HTMLEncode(ai.AvatarName);
                }

                imgAvatar.Visible = true;
                imgAvatar.ImageUrl = ResolveUrl("~/CMSModules/Avatars/CMSPages/GetAvatar.aspx?maxsidesize=250&avatarguid=" + ai.AvatarGUID);

                // Display default avatar options, only for global avatars
                if (!ai.AvatarIsCustom)
                {
                    switch (AvatarInfoProvider.GetAvatarTypeEnum(drpAvatarType.SelectedValue))
                    {
                        case AvatarTypeEnum.User:
                            plcDefaultUserAvatar.Visible = true;
                            break;

                        case AvatarTypeEnum.Group:
                            plcDefaultGroupAvatar.Visible = true;
                            break;

                        case AvatarTypeEnum.All:
                            plcDefaultGroupAvatar.Visible = true;
                            plcDefaultUserAvatar.Visible = true;
                            break;
                    }
                }
            }
        }

        // Set up page title control
        CurrentMaster.Title.Breadcrumbs = pageTitleTabs;
    }


    /// <summary>
    /// OK click event handler.
    /// </summary>
    protected void btnOK_Click(object sender, EventArgs e)
    {
        if ((uploadAvatar.PostedFile == null) && (ai == null))
        {
            lblError.Visible = true;
            lblError.Text = GetString("avat.fileinputerror");
        }
        else
        {
            CurrentSiteInfo site = CMSContext.CurrentSite;

            int width = 0;
            int height = 0;
            int sidesize = 0;
            
            // Get resize values
            if (drpAvatarType.SelectedValue != "all")
            {
                // Get right settings key
                string siteName = ((site != null) ? (site.SiteName + ".") : "");
                string prefix = "CMSAvatar";

                if (drpAvatarType.SelectedValue == "group")
                {
                    prefix = "CMSGroupAvatar";
                }

                width = SettingsKeyProvider.GetIntValue(siteName + prefix + "Width");
                height = SettingsKeyProvider.GetIntValue(siteName + prefix + "Height");
                sidesize = SettingsKeyProvider.GetIntValue(siteName + prefix + "MaxSideSize");
            }

            // Check if avatar name is unique
            string newAvatarName = txtAvatarName.Text.Trim();
            AvatarInfo avatarWithSameName = AvatarInfoProvider.GetAvatarInfoWithoutBinary(newAvatarName);
            if (avatarWithSameName != null)
            {
                if (ai != null)
                {
                    // Check unique avatar name of existing avatar
                    if (avatarWithSameName.AvatarID != ai.AvatarID)
                    {
                        lblInfo.Visible = false;
                        lblError.Visible = true;
                        lblError.Text = GetString("avat.uniqueavatarname");
                        return;
                    }
                }
                // Check unique avatar name of new avatar 
                else
                {
                    lblInfo.Visible = false;
                    lblError.Visible = true;
                    lblError.Text = GetString("avat.uniqueavatarname");
                    return;
                }
            }

            // Process form in these cases:
            // 1 - creating new avatar and uploaded file is not empty and it is image file
            // 2 - updating existing avatar and not uploading new image file
            // 3 - updating existing avatar and uploading image file
            if (((ai == null) && (uploadAvatar.PostedFile != null) && (uploadAvatar.PostedFile.ContentLength > 0) && (ImageHelper.IsImage(Path.GetExtension(uploadAvatar.PostedFile.FileName))))
                || ((ai != null) && ((uploadAvatar.PostedFile == null) || (uploadAvatar.PostedFile.ContentLength == 0)))
                || ((ai != null) && (uploadAvatar.PostedFile != null) && (uploadAvatar.PostedFile.ContentLength > 0) && (ImageHelper.IsImage(Path.GetExtension(uploadAvatar.PostedFile.FileName)))))
            {
                if (ai == null)
                {
                    switch (drpAvatarType.SelectedValue)
                    {
                        case "user":
                            ai = new AvatarInfo(uploadAvatar.PostedFile, width, height, sidesize);
                            break;

                        case "group":
                            ai = new AvatarInfo(uploadAvatar.PostedFile, width, height, sidesize);
                            break;

                        case "all":
                            ai = new AvatarInfo(uploadAvatar.PostedFile, 0, 0, 0);
                            break;
                        default:
                            ai = new AvatarInfo(uploadAvatar.PostedFile, 0, 0, 0);
                            break;
                    }

                    ai.AvatarIsCustom = false;
                    ai.AvatarGUID = Guid.NewGuid();
                }
                else if ((uploadAvatar.PostedFile != null) && (uploadAvatar.PostedFile.ContentLength > 0) && (ImageHelper.IsMimeImage(uploadAvatar.PostedFile.ContentType)))
                {
                    AvatarInfoProvider.DeleteAvatarFile(ai.AvatarGUID.ToString(), ai.AvatarFileExtension, false, false);
                    AvatarInfoProvider.UploadAvatar(ai, uploadAvatar.PostedFile, width, height, sidesize);
                }

                // Set new avatar name
                ai.AvatarName = newAvatarName;

                imgAvatar.Visible = true;
                imgAvatar.ImageUrl = ResolveUrl("~/CMSModules/Avatars/CMSPages/GetAvatar.aspx?maxsidesize=250&avatarguid=" + ai.AvatarGUID);

                // If there was avatar type change clear possible default avatar settings
                if (ai.AvatarType != drpAvatarType.SelectedValue)
                {
                    if (drpAvatarType.SelectedValue == "group")
                    {
                        // Clear default user's avatar
                        ClearDefaultUserAvatars(ai);
                    }
                    else if (drpAvatarType.SelectedValue == "user")
                    {
                        //Clear group avatar
                        ClearDefaultGroupAvatar(ai);
                    }
                    // "all" option is doesn't have to clear anything
                }

                // Set new type
                ai.AvatarType = drpAvatarType.SelectedValue;

                // If user uncheck
                if (ai.DefaultUserAvatar && (!chkDefaultUserAvatar.Checked || ai.AvatarIsCustom))
                {
                    AvatarInfoProvider.ClearDefaultAvatar(DefaultAvatarTypeEnum.User, true);
                }
                // If male uncheck
                if (ai.DefaultMaleUserAvatar && (!chkDefaultMaleUserAvatar.Checked || ai.AvatarIsCustom))
                {
                    AvatarInfoProvider.ClearDefaultAvatar(DefaultAvatarTypeEnum.Male, true);
                }
                // If female uncheck
                if (ai.DefaultFemaleUserAvatar && (!chkDefaultFemaleUserAvatar.Checked || ai.AvatarIsCustom))
                {
                    AvatarInfoProvider.ClearDefaultAvatar(DefaultAvatarTypeEnum.Female, true);
                }
                // If group uncheck
                if (ai.DefaultGroupAvatar && (!chkDefaultGroupAvatar.Checked || ai.AvatarIsCustom))
                {
                    AvatarInfoProvider.ClearDefaultAvatar(DefaultAvatarTypeEnum.Group, true);
                }

                // If avatar is not global, can't be default any default avatar
                if (ai.AvatarIsCustom)
                {
                    // Set all default avatar options to false
                    ai.DefaultUserAvatar = ai.DefaultMaleUserAvatar = ai.DefaultFemaleUserAvatar = ai.DefaultGroupAvatar = false;
                }
                else
                {
                    // Set new default avatar settings
                    ai.DefaultUserAvatar = chkDefaultUserAvatar.Checked;
                    ai.DefaultMaleUserAvatar = chkDefaultMaleUserAvatar.Checked;
                    ai.DefaultFemaleUserAvatar = chkDefaultFemaleUserAvatar.Checked;
                    ai.DefaultGroupAvatar = chkDefaultGroupAvatar.Checked;


                    // If this avatar is becoming to be new default avatar. Clear the mark from others avatar 
                    ClearDefaultUserAvatars(ai);
                    ClearDefaultGroupAvatar(ai);
                }

                AvatarInfoProvider.SetAvatarInfo(ai);

                avatarId = ai.AvatarID;
                URLHelper.Redirect("Avatar_Edit.aspx?saved=1&avatarid=" + avatarId );
            }
            else
            {
                lblInfo.Visible = false;
                lblError.Visible = true;
                // If given file is not valid
                if ((uploadAvatar.PostedFile != null) && (uploadAvatar.PostedFile.ContentLength > 0) && !ImageHelper.IsImage(Path.GetExtension(uploadAvatar.PostedFile.FileName)))
                {
                    lblError.Text = GetString("avat.filenotvalid");
                }
                else
                {
                    // If posted file is not given
                    lblError.Text = GetString("avat.fileinputerror");
                }
            }
        }
    }


    /// <summary>
    /// Clears default user avatars which is assigned to avatarinfo.
    /// </summary>    
    private static void ClearDefaultUserAvatars(AvatarInfo ai)
    {
        if (ai.DefaultUserAvatar)
        {
            AvatarInfoProvider.ClearDefaultAvatar(DefaultAvatarTypeEnum.User);
        }

        if (ai.DefaultMaleUserAvatar)
        {
            AvatarInfoProvider.ClearDefaultAvatar(DefaultAvatarTypeEnum.Male);
        }

        if (ai.DefaultFemaleUserAvatar)
        {
            AvatarInfoProvider.ClearDefaultAvatar(DefaultAvatarTypeEnum.Female);
        }
    }


    /// <summary>
    /// Clears default group avatar which is assigned to avatarinfo.
    /// </summary>    
    private static void ClearDefaultGroupAvatar(AvatarInfo ai)
    {
        if (ai.DefaultGroupAvatar)
        {
            AvatarInfoProvider.ClearDefaultAvatar(DefaultAvatarTypeEnum.Group);
        }
    }

    #endregion


    #region "IPostBackEventHandler Members"

    public void RaisePostBackEvent(string eventArgument)
    {
        if (eventArgument == "shared")
        {
            if ((ai != null) && (ai.AvatarIsCustom))
            {
                ai.AvatarIsCustom = false;
                AvatarInfoProvider.SetAvatarInfo(ai);
                URLHelper.Redirect(URLHelper.AddParameterToUrl(URLHelper.CurrentURL, "saved", "1"));
            }
        }

    }

    #endregion
}
