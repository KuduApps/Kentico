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
using CMS.CMSHelper;
using CMS.SiteProvider;
using CMS.TreeEngine;
using CMS.SettingsProvider;
using CMS.UIControls;
using CMS.MembershipProvider;

using TreeNode = CMS.TreeEngine.TreeNode;

public partial class CMSMessages_AccessDenied : MessagePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Logging out of Facebook
        if (QueryHelper.GetInteger("logout", 0) > 0)
        {
            btnSignOut_Click(this, EventArgs.Empty);
        }
        btnSignOut.OnClientClick = FacebookConnectHelper.FacebookConnectInitForSignOut(CMSContext.CurrentSiteName, LiteralScript);

        this.LabelMessage.Text = GetString("CMSMessages.AccessDenied");
        this.titleElem.TitleText = GetString("CMSDesk.AccessDenied");
        this.titleElem.TitleImage = GetImageUrl("Others/Messages/denied.png");

        // If specification parameters given, display custom message
        string resourceName = QueryHelper.GetString("resource", null);
        int nodeId = QueryHelper.GetInteger("nodeid", 0);
        string message = QueryHelper.GetText("message", null);

        if (!String.IsNullOrEmpty(resourceName))
        {
            // Access denied to resource
            ResourceInfo ri = ResourceInfoProvider.GetResourceInfo(resourceName);
            if (ri != null)
            {
                this.titleElem.TitleText = String.Format(GetString("CMSSiteManager.AccessDeniedOnResource"), ri.ResourceDisplayName);
            }
        }
        else if (nodeId > 0)
        {
            // Access denied to document
            TreeProvider tree = new TreeProvider(CMSContext.CurrentUser);
            TreeNode node = tree.SelectSingleNode(nodeId);
            if (node != null)
            {
                this.titleElem.TitleText = String.Format(GetString("CMSSiteManager.AccessDeniedOnNode"), HTMLHelper.HTMLEncode(node.DocumentName));
            }
        }
        else if (!String.IsNullOrEmpty(message))
        {
            // Custom message
            this.LabelMessage.Text = ResHelper.LocalizeString(message);
        }

        // Add missing permission name message
        string permission = QueryHelper.GetText("permission", null);
        if (!String.IsNullOrEmpty(permission))
        {
            this.LabelMessage.Text = String.Format(GetString("CMSSiteManager.AccessDeniedOnPermissionName"), permission);
        }

        // Display SignOut button
        if (CMSContext.CurrentUser.IsAuthenticated())
        {
            if (!RequestHelper.IsWindowsAuthentication())
            {
                this.btnSignOut.Visible = true;
            }
        }
        else
        {
            this.btnLogin.Visible = true;
        }

        this.lnkGoBack.Text = GetString("CMSDesk.GoBack");
    }


    protected void btnSignOut_Click(object sender, EventArgs e)
    {
        this.SignOut();
        LiteralScript.Text = ScriptHelper.GetScript("window.top.location.href= '../default.aspx';");
    }


    protected void btnLogin_Click(object sender, EventArgs e)
    {
        // Get the logon page URL
        string logonPage = SettingsKeyProvider.GetStringValue(CMSContext.CurrentSiteName + ".CMSSecuredAreasLogonPage");
        if (logonPage == "")
        {
            logonPage = "../CMSPages/Logon.aspx";
        }

        LiteralScript.Text = ScriptHelper.GetScript("window.top.location.href = '" + ResolveUrl(logonPage) + "';");
    }
}
