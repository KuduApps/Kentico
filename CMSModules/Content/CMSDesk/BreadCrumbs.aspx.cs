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

using CMS.TreeEngine;
using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.SiteProvider;
using CMS.UIControls;
using CMS.WorkflowEngine;
using CMS.DataEngine;

using TreeNode = CMS.TreeEngine.TreeNode;

public partial class CMSModules_Content_CMSDesk_BreadCrumbs : CMSContentPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Current Node ID
        int nodeId = 0;
        if (Request.QueryString["nodeid"] != null)
        {
            nodeId = ValidationHelper.GetInteger(Request.QueryString["nodeid"], 0);
        }

        switch (QueryHelper.GetString("action", "edit").ToLower())
        {
            case "delete":
                // Do not include title upon delete
                this.titleElem.SetWindowTitle = false;
                break;
        }

        // Get the node
        string aliasPath = TreePathUtils.GetAliasPathByNodeId(nodeId);
        if (aliasPath == "/")
        {
            // Set path as site name if empty
            SiteInfo si = CMSContext.CurrentSite;
            if (si != null)
            {
                this.titleElem.CreateStaticBreadCrumbs(HttpUtility.HtmlEncode(si.DisplayName));
            }
        }
        else
        {
            TreeProvider tree = new TreeProvider(CMSContext.CurrentUser);

            // Get the DataSet of nodes
            string where = TreeProvider.GetNodesOnPathWhereCondition(aliasPath, true, true);
            DataSet ds = DocumentHelper.GetDocuments(CMSContext.CurrentSiteName, "/%", TreeProvider.ALL_CULTURES, true, null, where, "NodeLevel ASC", -1, false, tree);
            if (!DataHelper.DataSourceIsEmpty(ds))
            {
                string[,] bc = new string[ds.Tables[0].Rows.Count, 3];
                int index = 0;

                // Build the path
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    string documentName = ValidationHelper.GetString(dr["DocumentName"], "");

                    bc[index, 0] = documentName;
                    bc[index, 1] = string.Empty;
                    bc[index, 2] = string.Empty;

                    index++;
                }

                this.titleElem.Breadcrumbs = bc;
            }
        }
    }
}
