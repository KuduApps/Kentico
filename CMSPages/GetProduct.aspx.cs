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
using CMS.WorkflowEngine;
using CMS.SiteProvider;
using CMS.CMSHelper;
using CMS.UIControls;

public partial class CMSPages_GetProduct : CMSPage
{
    #region "Variables"

    protected Guid skuGuid = Guid.Empty;
    protected int productId = 0;
    protected SiteInfo currentSite = null;
    protected string url = "";

    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        // Initialization
        productId = QueryHelper.GetInteger("productId", 0);
        skuGuid = QueryHelper.GetGuid("skuguid", Guid.Empty);
        currentSite = CMSContext.CurrentSite;

        string where = null;
        if (productId > 0)
        {
            where = "NodeSKUID = " + productId;
        }
        else if (skuGuid != Guid.Empty)
        {
            where = "SKUGUID = '" + skuGuid.ToString() + "'";
        }

        if ((where != null) && (currentSite != null))
        {
            TreeProvider tree = new TreeProvider(CMSContext.CurrentUser);
            DataSet ds = tree.SelectNodes(currentSite.SiteName, "/%", TreeProvider.ALL_CULTURES, true, "", where);
            if (!DataHelper.DataSourceIsEmpty(ds))
            {
                // Ger specified product url 
                url = CMSContext.GetUrl(Convert.ToString(ds.Tables[0].Rows[0]["NodeAliasPath"]));
            }
        }

        if (url != "")
        {
            // Redirect to specified product 
            URLHelper.Redirect(url);
        }
        else
        {
            // Display error message
            lblInfo.Visible = true;
            lblInfo.Text = GetString("GetProduct.NotFound");
        }
    }
}
