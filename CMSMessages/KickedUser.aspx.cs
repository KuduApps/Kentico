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
using CMS.UIControls;
using CMS.SettingsProvider;

public partial class CMSMessages_KickedUser : MessagePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.titleElem.TitleText = GetString("kicked.header");
        this.Page.Title = GetString("kicked.header");
        this.titleElem.TitleImage = GetImageUrl("Others/Messages/denied.png");

        this.lblInfo.Text = String.Format(GetString("kicked.info"), SettingsKeyProvider.GetIntValue("CMSDenyLoginInterval"));
        
        // Back link
        this.lnkBack.Text = GetString("general.Back");
        this.lnkBack.NavigateUrl = "~/";
    }
}
