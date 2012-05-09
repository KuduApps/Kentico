using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.GlobalHelper;
using CMS.SiteProvider;
using CMS.UIControls;

public partial class CMSModules_OnlineMarketing_Pages_Header : CMSDeskPage
{
    protected void Page_PreRender(object sender, EventArgs e)
    {
        string url = null;

        // Preselect the item in the frame
        UIElementInfo selected = uiToolbarElem.SelectedUIElement;
        if (selected != null)
        {
            url = selected.ElementTargetURL;
            url = URLHelper.EnsureHashToQueryParameters(url);
            url = URLHelper.ResolveUrl(url);
        }

        // Prepare the script
        string script = null;
        if (!String.IsNullOrEmpty(url))
        {
            script = "try {parent.frames['content'].location.href='" + url + "'; SelectItem('" + selected.ElementName + "','',true);} catch (err) {}\n";
        }
        else
        {
            script = "try {parent.frames['content'].location.href='" + URLHelper.ResolveUrl("~/CMSMessages/Information.aspx") + "?message=" + HttpUtility.UrlPathEncode(GetString("uiprofile.uinotavailable")) + "'} catch (err) {}\n";
        }

        ScriptHelper.RegisterStartupScript(Page, typeof(string), "TabSelection", ScriptHelper.GetScript(script));
        ScriptHelper.RegisterTitleScript(this, GetString("cmsdesk.ui.marketing"));
    }
}