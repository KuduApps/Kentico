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

public partial class CMSMessages_DiskPermissions : MessagePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string message = GetString("CMSSiteManager.DiskPermissions");

        message = message.Replace("<user>", System.Security.Principal.WindowsIdentity.GetCurrent().Name);
        message = message.Replace("<folder>", Server.MapPath("~/."));

        LabelMessage.Text = message;
        this.titleElem.TitleText = GetString("CMSSiteManager.InvalidDiskPermissions");
        this.titleElem.TitleImage = GetImageUrl("Others/Messages/info.png");
    }
}
