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

using CMS.UIControls;
using CMS.CMSHelper;
using CMS.GlobalHelper;

public partial class CMSModules_PortalEngine_UI_PageTemplates_PageTemplate_Edit : CMSEditTemplatePage
{
    protected int mHeight = 0;

    protected override void OnPreInit(EventArgs e)
    {
        base.OnPreInit(e);

        if (QueryHelper.GetBoolean("dialog", false))
        {
            mHeight = TabsBreadHeadFrameHeight;
        }
        else
        {
            mHeight = TabsBreadFrameHeight;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        // Register script
        string refreshScript = ScriptHelper.GetScript(
@"
function Refresh() {
    if ((parent != null) && (parent.frames['pt_tree'] != null)) parent.frames['pt_tree'].ReloadPage(" + pageTemplateId + @");
    if (window.frames['pt_edit_menu'] != null) window.frames['pt_edit_menu'].location.href = 'PageTemplate_Header.aspx?templateid=" + pageTemplateId + ((ValidationHelper.GetInteger(Request.QueryString["dialog"], 0) > 0) ? "&dialog=1" : "") + @"';
}
"
        );

        ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "pageTemplateRefreshScript", refreshScript);
    }
}
