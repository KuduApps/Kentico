using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.UIControls;
using CMS.GlobalHelper;

public partial class CMSModules_OnlineMarketing_Pages_Tools_MVTest_Frameset : CMSMVTestPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        frmContent.Attributes["src"] = GetPageURL(QueryHelper.GetString("page", "overview")) + URLHelper.Url.Query;
    }


    /// <summary>
    /// Gets page url from constant.
    /// </summary>
    /// <param name="page">Page constant</param>
    private string GetPageURL(string page)
    {
        switch (page.ToLower())
        {
            case "mvtreport":
                return "MVTReport.aspx";

            default:
                return "overview.aspx";
        }
    }
}

