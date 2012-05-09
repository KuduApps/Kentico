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

public partial class CMSMessages_Information : MessagePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Get localization string from URL
        string msgResString = QueryHelper.GetText("message", "");

        this.lblMessage.Text = GetString(msgResString);

        // Initialize title
        this.titleElem.TitleText = GetString("CMSDesk.Information");
        this.titleElem.TitleImage = GetImageUrl("Others/Messages/info.png");
    }
}
