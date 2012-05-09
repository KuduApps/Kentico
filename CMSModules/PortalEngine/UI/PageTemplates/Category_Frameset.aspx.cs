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
using CMS.UIControls;

public partial class CMSModules_PortalEngine_UI_PageTemplates_Category_Frameset : SiteManagerPage
{
    #region "Variables"

    protected string categoryContentUrl = "PageTemplate_List.aspx?categoryid=";
    protected string categoryHeaderUrl = "Category_Header.aspx?categoryid=";

    #endregion


    #region "Page events"

    /// <summary>
    /// Handles the Load event of the Page control.
    /// </summary>
    protected void Page_Load(object sender, EventArgs e)
    {
        int categoryId = QueryHelper.GetInteger("categoryid", 0);
        if (QueryHelper.GetInteger("saved", 0) > 0)
        {
            categoryContentUrl +=  categoryId.ToString() + "&saved=1";
            categoryHeaderUrl += categoryId.ToString() + "&saved=1";
        }
        else
        {
            categoryContentUrl += categoryId.ToString();
            categoryHeaderUrl += categoryId.ToString();
        }
    }

    #endregion
}
