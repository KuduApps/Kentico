using System;
using System.Text;
using System.Web;
using System.Collections.Specialized;

using CMS.CMSHelper;
using CMS.TreeEngine;
using CMS.GlobalHelper;
using CMS.ExtendedControls;
using CMS.UIControls;

using TreeNode = CMS.TreeEngine.TreeNode;

public partial class CMSModules_Content_CMSDesk_SplitView : CMSContentPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string url = QueryHelper.GetString("splitUrl", null);

        if (!string.IsNullOrEmpty(url) && CMSContext.DisplaySplitMode)
        {
            // Register script files
            ltlScript.Text += ScriptHelper.GetIncludeScript("~/CMSModules/Content/CMSDesk/SplitView.js");

            // Decode url
            url = HttpUtility.UrlDecode(url);

            // Update view mode
            UpdateViewMode(CMSContext.ViewMode);

            // Set frame1 url
            docSplitView.Frame1Url = url;

            // Get the URL of the other frame
            NameValueCollection param = new NameValueCollection();
            param.Add("culture", CMSContext.SplitModeCultureCode);

            // Set frame2 url
            docSplitView.Frame2Url = VirtualContext.GetVirtualContextPath(url, param);

            // Set script prefix
            param.Set("culture", "##c##");
            string scriptPrefix = VirtualContext.GetVirtualContextPrefix(param);

            StringBuilder script = new StringBuilder();
            script.Append(
@"function InitSplitViewSyncScroll(frameElement, body, refreshSameCulture, unbind) {
  if (InitSyncScroll) {
    InitSyncScroll(frameElement, body, refreshSameCulture, unbind);
  }
}
function SplitModeRefreshFrame() {
    if (FSP_SplitModeRefreshFrame) {
        FSP_SplitModeRefreshFrame();
    }
}
var FSP_appPref = '", URLHelper.GetFullApplicationUrl(), @"';
var FSP_cntPref = '", scriptPrefix, "';");

            ScriptHelper.RegisterClientScriptBlock(Page, typeof(string), "splitViewSync_" + Page.ClientID, ScriptHelper.GetScript(script.ToString()));
        }
    }
}
