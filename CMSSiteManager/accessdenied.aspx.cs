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
using CMS.UIControls;
using CMS.MembershipProvider;

using TreeNode = CMS.TreeEngine.TreeNode;

public partial class CMSSiteManager_accessdenied : MessagePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Logging out of Facebook
        if (QueryHelper.GetInteger("logout", 0) > 0)
        {
            btnSignOut_Click(this, EventArgs.Empty);
        }
        btnSignOut.OnClientClick = FacebookConnectHelper.FacebookConnectInitForSignOut(CMSContext.CurrentSiteName, ltlScript);

        bool hideLinks = false;

        // Default message
        this.lblMessage.Text = GetString("CMSSiteManager.IsNotAdminMsg");

        CurrentMaster.Title.TitleText = GetString("CMSSiteManager.AccessDenied");
        CurrentMaster.Title.TitleImage = GetImageUrl("Others/Messages/denied.png");

        // Resource access denied
        string resourceName = QueryHelper.GetString("resource", null);
        if (resourceName != null)
        {
            switch (resourceName.ToLower())
            {
                // Not enabled admin interface
                case "cms.adminui":
                    {
                        this.lblMessage.Text = GetString("CMSSiteManager.AdminUINotEnabled");
                    }
                    break;

                // Standard resource permission
                default:
                    {
                        ResourceInfo ri = ResourceInfoProvider.GetResourceInfo(resourceName);
                        if (ri != null)
                        {
                            CurrentMaster.Title.TitleText = String.Format(GetString("CMSSiteManager.AccessDeniedOnResource"), ri.ResourceDisplayName);
                        }
                    }
                    break;
            }
        }

        // Access denied to document
        int nodeId = QueryHelper.GetInteger("nodeid", 0);
        if (nodeId > 0)
        {
            TreeProvider tree = new TreeProvider(CMSContext.CurrentUser);
            TreeNode node = tree.SelectSingleNode(nodeId);
            if (node != null)
            {
                CurrentMaster.Title.TitleText = String.Format(GetString("CMSSiteManager.AccessDeniedOnNode"), HTMLHelper.HTMLEncode(node.DocumentName));
            }
        }

        // Custom message
        string message = QueryHelper.GetText("message", null);
        if (message != null)
        {
            this.lblMessage.Text = ResHelper.LocalizeString(message);
            hideLinks = true;
        }

        // Add missing permission name message
        string permission = QueryHelper.GetText("permission", null);
        if (permission != null)
        {
            this.lblMessage.Text = String.Format(GetString("CMSSiteManager.AccessDeniedOnPermissionName"), permission);
            hideLinks = true;
        }

        // Override displaying of links
        hideLinks = QueryHelper.GetBoolean("hidelinks", hideLinks);

        if (!hideLinks)
        {
            this.lnkGoBack.Text = GetString("CMSSiteManager.GoBack");

            // Hide for windows authentication
            if (RequestHelper.IsWindowsAuthentication())
            {
                btnSignOut.Visible = false;
            }
        }
        else
        {
            btnSignOut.Visible = false;
            lnkGoBack.Visible = false;
        }
    }


    protected void btnSignOut_Click(object sender, EventArgs e)
    {
        this.SignOut();
        ltlScript.Text = ScriptHelper.GetScript("parent.location.href='../default.aspx';");
    }


    /// <summary>
    /// Disable handler base tag.
    /// </summary>
    protected override void OnInit(EventArgs e)
    {
        UseBaseTagForHandlerPage = false;
        base.OnInit(e);
    }


}
