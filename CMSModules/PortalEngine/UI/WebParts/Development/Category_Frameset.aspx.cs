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

public partial class CMSModules_PortalEngine_UI_WebParts_Development_Category_Frameset : SiteManagerPage
{
    #region "Variables"

    protected string categoryContentUrl = URLHelper.ResolveUrl("~/CMSModules/PortalEngine/UI/WebParts/Development/WebPart_List.aspx?categoryid=");
    protected string categoryHeaderUrl = URLHelper.ResolveUrl("~/CMSModules/PortalEngine/UI/WebParts/Development/Category_Header.aspx?categoryid=");

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
            categoryHeaderUrl += categoryId.ToString() + "&saved=1";
            // Set empty because the header frame will refresh the content frame automatically
            categoryContentUrl = "";
        }
        else
        {
            categoryContentUrl += categoryId.ToString();
            categoryHeaderUrl += categoryId.ToString();
        }
    }

    #endregion
}
