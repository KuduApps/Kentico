using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.GlobalHelper;
using CMS.SiteProvider;
using CMS.DataEngine;
using CMS.UIControls;

public partial class CMSModules_Avatars_Dialogs_AvatarsGallery : CMSModalPage
{
    #region "Events"

    protected void Page_Load(object sender, EventArgs e)
    {
        this.CurrentMaster.Title.TitleText = GetString("avat.selectavatar");
        this.CurrentMaster.Title.TitleImage = GetImageUrl("Objects/CMS_Avatar/object.png");
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        btnOk.Enabled = avatarsGallery.HasData();
    }

    #endregion 
}
