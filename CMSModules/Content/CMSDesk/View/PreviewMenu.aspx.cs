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

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.SettingsProvider;
using CMS.TreeEngine;
using CMS.WorkflowEngine;
using CMS.UIControls;

using TreeNode = CMS.TreeEngine.TreeNode;

public partial class CMSModules_Content_CMSDesk_View_PreviewMenu : CMSContentPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Register the dialog script
        ScriptHelper.RegisterDialogScript(this);

        // Get the document ID
        int nodeId = ValidationHelper.GetInteger(Request.QueryString["nodeid"], 0);
        TreeProvider tree = new TreeProvider(CMSContext.CurrentUser);
        TreeNode node = tree.SelectSingleNode(nodeId, TreeProvider.ALL_CULTURES);

        if (node == null)
        {
            this.menuElem.Visible = false;
        }
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        if (this.menuElem.MenuItems <= 0)
        {
            this.ltlScript.Text += ScriptHelper.GetScript("parent.document.getElementById('previewframeset').rows = '0, *';");
        }
    }
}
