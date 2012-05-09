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
using CMS.ExtendedControls;
using CMS.UIControls;

public partial class CMSModules_MyDesk_Default : CMSMyDeskPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptHelper.RegisterTitleScript(this, GetString("Header.MyDesk"));

        frameMenu.Attributes.Add("src", "mainMenu.aspx" + URLHelper.Url.Query);
    }
}
