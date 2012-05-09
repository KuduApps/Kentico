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
using System.Text.RegularExpressions;

using CMS.UIControls;
using CMS.GlobalHelper;
using CMS.CMSHelper;

public partial class CMSModules_MediaLibrary_CMSPages_SelectFolder_Footer : CMSLiveModalPage
{
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        SetBrowserClass();
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        if (QueryHelper.ValidateHash("hash"))
        {
            // Register custom css if exists
            RegisterDialogCSSLink();
        }
        else
        {
            string url = ResolveUrl("~/CMSMessages/Error.aspx?title=" + GetString("dialogs.badhashtitle") + "&text=" + GetString("dialogs.badhashtext") + "&cancel=1");
            ScriptHelper.RegisterStartupScript(Page, typeof(string), "redirect", ScriptHelper.GetScript("if (window.parent != null) { window.parent.location = '" + url + "' }"));
        }

    }
}
