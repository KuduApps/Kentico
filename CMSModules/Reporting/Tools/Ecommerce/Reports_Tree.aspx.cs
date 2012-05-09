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
using System.Text;

using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.UIControls;
using CMS.SiteProvider;
using CMS.LicenseProvider;
using CMS.SettingsProvider;

public partial class CMSModules_Reporting_Tools_Ecommerce_Reports_Tree : CMSEcommerceReportingPage
{
    #region "Variables"

    private string firstChildNodeName = null;
    private string firstChildNodeUrl = null;

    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        AddMenuButtonSelectScript("ECReports", "", "ecommerceMenu");

        this.treeElem.EnableRootSelect = false;
        this.treeElem.OnNodeCreated += new CMSAdminControls_UI_UIProfiles_UIMenu.NodeCreatedEventHandler(treeElem_OnNodeCreated);
        this.treeElem.JavaScriptHandler = "ShowDesktopContent";
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        // Preselect first child node
        if (!String.IsNullOrEmpty(this.firstChildNodeName))
        {
            this.ltlNodePreselectScript.Text = ScriptHelper.GetScript("SelectNode('" + this.firstChildNodeName + "'); parent.frames['ecommreports'].location.href = '" + this.firstChildNodeUrl + "'");
        }

        // Call the script for tab which is selected
        if (this.treeElem.MenuEmpty)
        {
            ScriptHelper.RegisterStartupScript(this.Page, typeof(string), "FirstTabSelection", ScriptHelper.GetScript(" parent.frames['ecommreports'].location.href = '" + URLHelper.ResolveUrl("~/CMSMessages/Information.aspx") + "?message=" + HttpUtility.UrlPathEncode(GetString("uiprofile.uinotavailable")) + "'; "));
        }
    }


    protected TreeNode treeElem_OnNodeCreated(UIElementInfo uiElement, TreeNode defaultNode)
    {
        if (uiElement != null)
        {
            defaultNode.NavigateUrl = URLHelper.ResolveUrl(uiElement.ElementTargetURL);
            defaultNode.Target = "ecommreports";

            // Set first child node to be preselected
            if (String.IsNullOrEmpty(this.firstChildNodeName))
            {
                this.firstChildNodeName = uiElement.ElementName;
                this.firstChildNodeUrl = defaultNode.NavigateUrl;
                defaultNode.Selected = true;
            }
        }
        return defaultNode;
    }
}
