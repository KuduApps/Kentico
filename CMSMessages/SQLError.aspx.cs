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

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.LicenseProvider;
using CMS.UIControls;

public partial class CMSMessages_SQLError : MessagePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.titleElem.TitleText = "SQL troubleshooting help";
        this.titleElem.TitleImage = GetImageUrl("Others/Messages/info.png");
    }
}
