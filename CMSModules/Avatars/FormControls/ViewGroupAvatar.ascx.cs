using System;

using CMS.FormControls;
using CMS.GlobalHelper;
using CMS.SiteProvider;

public partial class CMSModules_Avatars_FormControls_ViewGroupAvatar : FormEngineUserControl
{
    private int mValue;
    private int mMaxSideSize = 200;

    #region "Public properties"

    /// <summary>
    /// Gets or sets field value.
    /// </summary>
    public override object Value
    {
        get
        {
            return mValue;
        }
        set
        {
            // get avatar id
            mValue = ValidationHelper.GetInteger(value, 0);
        }
    }


    /// <summary>
    /// Gets or sets max side size of avatar image.
    /// </summary>
    public int MaxSideSize
    {
        get
        {
            return mMaxSideSize;
        }
        set
        {
            mMaxSideSize = value;   
        }
    }

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        AvatarInfo ai = null;

        if (mValue > 0)
        {
            ai = AvatarInfoProvider.GetAvatarInfo(mValue);
        }
        if (ai == null)
        {
            ai = AvatarInfoProvider.GetDefaultAvatar(DefaultAvatarTypeEnum.Group);
        }

        if (ai != null)
        {
            imgAvatar.ImageUrl = ResolveUrl("~/CMSModules/Avatars/CMSPages/GetAvatar.aspx?avatarguid=" + ai.AvatarGUID + "&maxsidesize=" + MaxSideSize);
            imgAvatar.AlternateText = GetString("general.avatar");
            return;
        }

        imgAvatar.Visible = false;
    }

    #endregion
}
