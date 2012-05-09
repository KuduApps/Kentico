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

// Do not use CMSPage or MessagePage due license checking
public partial class CMSMessages_LicenseLimit : CMSPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        titleElem.TitleText = GetString("LicenseLimitation.InfoPageTitle");
        titleElem.TitleImage = GetImageUrl("Others/Messages/denied.png");

        lblMessage.Text = GetString("LicenseLimitation.InfoPageMessage");
    }
}
