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

public partial class CMSDesk_accessdenied : CMSPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Logging out of Facebook
        if (QueryHelper.GetInteger("logout", 0) > 0)
        {
            btnSignOut_Click(this, EventArgs.Empty);
        }
        btnSignOut.OnClientClick = FacebookConnectHelper.FacebookConnectInitForSignOut(CMSContext.CurrentSiteName, ltlScript);

        // Setup page title text and image
        this.CurrentMaster.Title.TitleText = GetString("CMSDesk.AccessDenied");
        this.CurrentMaster.Title.TitleImage = GetImageUrl("Others/Messages/denied.png");

        bool hideLinks = false;

        this.LabelMessage.Text = GetString("CMSDesk.IsNotEditorMsg");

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
                this.CurrentMaster.Title.TitleText = String.Format(GetString("CMSSiteManager.AccessDeniedOnResource"), HTMLHelper.HTMLEncode(ri.ResourceDisplayName));
            }
        }
        else if (nodeId > 0)
        {
            // Access denied to document
            TreeProvider tree = new TreeProvider(CMSContext.CurrentUser);
            TreeNode node = tree.SelectSingleNode(nodeId);
            if (node != null)
            {
                this.CurrentMaster.Title.TitleText = String.Format(GetString("CMSSiteManager.AccessDeniedOnNode"), HTMLHelper.HTMLEncode(node.DocumentName));
            }
        }
        // Add custom message
        else if (!String.IsNullOrEmpty(message))
        {
            this.LabelMessage.Text = message;
            hideLinks = true;
        }

        // Add missing permission name message
        string permission = QueryHelper.GetText("permission", null);
        string uielement = QueryHelper.GetText("uielement", null);
        
        if (!String.IsNullOrEmpty(permission))
        {
            if (permission.Contains("|"))
            {
                permission = String.Join(GetString("CMSSiteManager.AccessDeniedOr"), permission.Split('|'));
            }

            this.LabelMessage.Text = String.Format(GetString("CMSSiteManager.AccessDeniedOnPermissionName"), permission);
            hideLinks = true;
        }
        // Add missing UI element name message
        else if (!String.IsNullOrEmpty(uielement))
        {
            if (uielement.Contains("|"))
            {
                uielement = String.Join(GetString("CMSSiteManager.AccessDeniedOr"), uielement.Split('|'));
            }

            this.LabelMessage.Text = String.Format(GetString("CMSSiteManager.AccessDeniedOnUIElementName"), uielement);
            hideLinks = true;
        }

        if (!hideLinks)
        {
            this.lnkGoBack.Text = GetString("CMSDesk.GoBack");

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
        ltlScript.Text = "<script type=\"text/javascript\">parent.location.href= '../default.aspx';</script> ";
    }
}
