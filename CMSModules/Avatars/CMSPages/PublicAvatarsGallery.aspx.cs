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
using CMS.SiteProvider;
using CMS.DataEngine;
using CMS.UIControls;

public partial class CMSModules_Avatars_CMSPages_PublicAvatarsGallery : CMSLiveModalPage
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
