using System;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
using System.Web;
using System.Web.UI;

using CMS.GlobalHelper;
using CMS.SettingsProvider;
using CMS.CMSHelper;
using CMS.SiteProvider;
using CMS.UIControls;
using CMS.LicenseProvider;

public partial class CMSModules_MyDesk_mainmenu : CMSMyDeskPage
{
    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Filter unavailable modules
        ucUIToolbar.OnButtonFiltered += new CMSAdminControls_UI_UIProfiles_UIToolbar.ButtonFilterEventHandler(ucUIToolbar_OnButtonFiltered);
    }


    /// <summary>
    /// On button filtered handler.
    /// </summary>
    bool ucUIToolbar_OnButtonFiltered(UIElementInfo uiElement)
    {
        bool moduleOnSite = true;
        string siteName = CMSContext.CurrentSiteName;

        // Check whether modules are assigned to current site
        switch (uiElement.ElementName.ToLower())
        {
            case "myfriends":
                moduleOnSite = ResourceSiteInfoProvider.IsResourceOnSite("CMS.Friends", siteName);
                break;

            case "mymessages":
                moduleOnSite = ResourceSiteInfoProvider.IsResourceOnSite("CMS.Messaging", siteName);
                break;

            case "myblogs":
                moduleOnSite = ResourceSiteInfoProvider.IsResourceOnSite("CMS.Blog", siteName);
                break;

            case "myprojects":
                moduleOnSite = ResourceSiteInfoProvider.IsResourceOnSite("CMS.ProjectManagement", siteName);
                break;
        }

        // Check whether separable modules are loaded
        return moduleOnSite && IsMyDeskUIElementAvailable(uiElement.ElementName);
    }


    /// <summary>
    /// Register first page script.
    /// </summary>
    protected override void OnPreRender(EventArgs e)
    {
        string url = null;

        // Preselect the item in the frame
        UIElementInfo selected = this.ucUIToolbar.SelectedUIElement;
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
            script = "try {parent.frames['frameMain'].location.href='" + url + "'} catch (err) {}\n";
        }
        else
        {
            script = "try {parent.frames['frameMain'].location.href='" + URLHelper.ResolveUrl("~/CMSMessages/Information.aspx") + "?message=" + HttpUtility.UrlPathEncode(GetString("uiprofile.uinotavailable")) + "'} catch (err) {}\n";
        }

        litScript.Text = ScriptHelper.GetScript(script);


        base.OnPreRender(e);
    }

    #endregion
}
