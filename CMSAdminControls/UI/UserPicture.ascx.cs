using System;
using System.Web.UI;

using CMS.GlobalHelper;
using CMS.SiteProvider;
using CMS.UIControls;
using CMS.DataEngine;
using CMS.SettingsProvider;
using CMS.IO;
using CMS.CMSHelper;

public partial class CMSAdminControls_UI_UserPicture : CMSUserControl
{
    #region "Variables"

    private int mWidth = 0;
    private int mHeight = 0;
    private bool mDisplayPicture = true;
    private int mUserId = 0;
    private int mGroupId = 0;
    private int mAvatarID = 0;
    private bool mKeepAspectRatio = false;
    private bool mRenderOuterDiv = false;
    private string mOuterDivCSSClass = "UserPicture";

    protected string imageUrl = "";
    public string width = "";
    public string height = "";

    #endregion


    #region "Public properties"

    /// <summary>
    /// Keep aspect ratio.
    /// </summary>
    public bool KeepAspectRatio
    {
        get
        {
            return mKeepAspectRatio;
        }
        set
        {
            mKeepAspectRatio = value;
        }
    }


    /// <summary>
    /// Max picture width.
    /// </summary>
    public int Width
    {
        get
        {
            return mWidth;
        }
        set
        {
            mWidth = value;
        }
    }


    /// <summary>
    /// Max picture height.
    /// </summary>
    public int Height
    {
        get
        {
            return mHeight;
        }
        set
        {
            mHeight = value;
        }
    }


    /// <summary>
    /// Enable/disable display picture
    /// </summary>
    public bool DisplayPicture
    {
        get
        {
            return mDisplayPicture;
        }
        set
        {
            mDisplayPicture = value;
        }
    }


    /// <summary>
    /// User ID.
    /// </summary>
    public int UserID
    {
        get
        {
            return mUserId;
        }
        set
        {
            mUserId = value;
        }
    }


    /// <summary>
    /// Gets or sets group id.
    /// </summary>
    public int GroupID
    {
        get
        {
            return mGroupId;
        }
        set
        {
            mGroupId = value;
        }
    }


    /// <summary>
    /// Gets or sets avatar id.
    /// </summary>
    public int AvatarID
    {
        get
        {
            return mAvatarID;
        }
        set
        {
            mAvatarID = value;
        }
    }


    /// <summary>
    /// Div tag is rendered around picture if true (default value = 'false').
    /// </summary>
    public bool RenderOuterDiv
    {
        get
        {
            return mRenderOuterDiv;
        }
        set
        {
            mRenderOuterDiv = value;
        }
    }


    /// <summary>
    /// CSS class of outer div (default value = 'UserPicture').
    /// </summary>
    public string OuterDivCSSClass
    {
        get
        {
            return mOuterDivCSSClass;
        }
        set
        {
            mOuterDivCSSClass = value;
        }
    }


    /// <summary>
    /// Use default avatar if any user/group avatar not found
    /// </summary>
    public bool UseDefaultAvatar
    {
        get;
        set;
    }

    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
    }


    /// <summary>
    /// Sets image  url, width and height.
    /// </summary>
    protected void SetImage()
    {
        Visible = false;

        // Only if display picture is allowed
        if (DisplayPicture)
        {
            string imageUrl = ResolveUrl("~/CMSModules/Avatars/CMSPages/GetAvatar.aspx?avatarguid=");

            // Is user id set? => Get user info
            if (mUserId > 0)
            {
                // Get user info
                UserInfo ui = UserInfoProvider.GetUserInfo(mUserId);
                if (ui != null)
                {
                    AvatarID = ui.UserAvatarID;
                    if (AvatarID <= 0)   // Backward compatibility
                    {
                        if (ui.UserPicture != "")
                        {
                            // Get picture filename
                            string filename = ui.UserPicture.Remove(ui.UserPicture.IndexOf('/'));
                            string ext = Path.GetExtension(filename);
                            imageUrl += filename.Substring(0, (filename.Length - ext.Length));
                            imageUrl += "&extension=" + ext;
                            Visible = true;
                        }
                        else if (UseDefaultAvatar)
                        {
                            DefaultAvatarTypeEnum defAvatar = DefaultAvatarTypeEnum.User;

                            // Get default avatar type according to user gender
                            UserGenderEnum gender = (UserGenderEnum)ValidationHelper.GetInteger(ui.GetValue("UserGender"), 0);
                            switch (gender)
                            {
                                case UserGenderEnum.Female:
                                    defAvatar = DefaultAvatarTypeEnum.Female;
                                    break;

                                case UserGenderEnum.Male:
                                    defAvatar = DefaultAvatarTypeEnum.Male;
                                    break;
                            }

                            AvatarInfo ai = AvatarInfoProvider.GetDefaultAvatar(defAvatar);

                            // Avatar not specified for current gender, get user default avatar
                            if (ai == null)
                            {
                                ai = AvatarInfoProvider.GetDefaultAvatar(DefaultAvatarTypeEnum.User);
                            }

                            if (ai != null)
                            {
                                AvatarID = ai.AvatarID;
                            }
                        }
                    }
                }
            }

            // Is group id set? => Get group info
            if (mGroupId > 0)
            {
                // Get group info trough module commands
                GeneralizedInfo gi = ModuleCommands.CommunityGetGroupInfo(mGroupId);
                if (gi != null)
                {
                    AvatarID = ValidationHelper.GetInteger(gi.GetValue("GroupAvatarID"), 0);
                }

                if ((AvatarID <= 0) && UseDefaultAvatar)
                {
                    AvatarInfo ai = AvatarInfoProvider.GetDefaultAvatar(DefaultAvatarTypeEnum.Group);
                    if (ai != null)
                    {
                        AvatarID = ai.AvatarID;
                    }
                }
            }

            if (AvatarID > 0)
            {
                AvatarInfo ai = AvatarInfoProvider.GetAvatarInfoWithoutBinary(AvatarID);
                if (ai != null)
                {
                    imageUrl += ai.AvatarGUID.ToString();
                    Visible = true;
                }
            }

            // If item was found 
            if (Visible)
            {
                if (KeepAspectRatio)
                {
                    imageUrl += "&maxsidesize=" + (Width > Height ? Width : Height);
                }
                else
                {
                    imageUrl += "&width=" + Width + "&height=" + Height;
                }
                imageUrl = URLHelper.EncodeQueryString(imageUrl);
                ltlImage.Text = "<img alt=\"" + GetString("general.avatar") + "\" src=\"" + imageUrl + "\" />";

                // Render outer div with specific CSS class
                if (RenderOuterDiv)
                {
                    ltlImage.Text = "<div class=\"" + OuterDivCSSClass + "\">" + ltlImage.Text + "</div>";
                }
            }
        }
    }


    /// <summary>
    /// Render.
    /// </summary>
    protected override void Render(HtmlTextWriter writer)
    {
        if (DisplayPicture)
        {
            SetImage();
        }
        else
        {
            Visible = false;
        }

        base.Render(writer);
    }
}
