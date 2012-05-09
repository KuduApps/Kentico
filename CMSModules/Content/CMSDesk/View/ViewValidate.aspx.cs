using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.UIControls;
using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.ExtendedControls;

public partial class CMSModules_Content_CMSDesk_View_ViewValidate : CMSContentPage, ICallbackEventHandler
{
    protected string viewpage = "../blank.htm";
    protected string validatepage = "../Validation/Default.aspx";
    protected string loadScript = "FocusFrame();";

    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptHelper.RegisterJQuery(Page);

        if (!RequestHelper.IsPostBack())
        {
            pnlTabs.SelectedTabIndex = ViewTabCode.FromEnum(UIContext.ViewTab);
        }

        pnlTabs.CssClass = "Dialog_Tabs LightTabs";

        tabValidate.HeaderText = ResHelper.GetString("general.validate");
        tabPreview.HeaderText = ResHelper.GetString("general.view");

        validatepage = URLHelper.AppendQuery(validatepage, URLHelper.Url.Query);

        viewpage = QueryHelper.GetString("viewpage", "");
        if (!String.IsNullOrEmpty(viewpage))
        {
            viewpage = URLHelper.AppendQuery(viewpage, URLHelper.Url.Query);

            // Split mode enabled
            if (CMSContext.DisplaySplitMode)
            {
                viewpage = GetSplitViewUrl(viewpage);
            }
        }

        string script = @"$j(""#tabPreview_head a"").bind(""click"",function(){var elem = document.getElementById('pageview');elem.src = elem.src;})";
        ScriptHelper.RegisterStartupScript(this, typeof(string), "RefreshPageViewFrame", ScriptHelper.GetScript(script));

        pnlTabs.OnClientTabClick = Page.ClientScript.GetCallbackEventReference(this, "ui.index", "$j.noop", "null");
    }


    #region "ICallbackEventHandler Members"

    public string GetCallbackResult()
    {
        return null;
    }

    public void RaiseCallbackEvent(string eventArgument)
    {
        UIContext.ViewTab = ViewTabCode.ToEnum(ValidationHelper.GetInteger(eventArgument,0));
    }

    #endregion
}
