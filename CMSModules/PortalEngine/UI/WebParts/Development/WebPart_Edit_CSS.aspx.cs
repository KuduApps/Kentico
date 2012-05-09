using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

using CMS.UIControls;
using CMS.GlobalHelper;
using CMS.PortalEngine;
using CMS.IO;
using CMS.FormEngine;
using CMS.URLRewritingEngine;

public partial class CMSModules_PortalEngine_UI_WebParts_Development_WebPart_Edit_CSS : SiteManagerPage
{
    /// <summary>
    /// Page load.
    /// </summary>
    protected void Page_Load(object sender, EventArgs e)
    {
        // Initialize controls.        
        btnOk.Text = GetString("General.OK");

        // Get 'webpartid' from querystring.
        int webPartId = QueryHelper.GetInteger("webpartid", 0);

        if (!RequestHelper.IsPostBack())
        {
            // Get WebPartInfo of specified 'webPartId'.
            WebPartInfo wpi = WebPartInfoProvider.GetWebPartInfo(webPartId);
            if (wpi != null)
            {
                etaCSS.Text = wpi.WebPartCSS;
            }
        }
    }


    /// <summary>
    /// Handle btnOK's OnClick event.
    /// </summary>
    protected void btnOK_Click(object sender, EventArgs e)
    {
        string errorMessage = "";

        // Get WebPartInfo.
        WebPartInfo wpi = WebPartInfoProvider.GetWebPartInfo(QueryHelper.GetInteger("webpartid", 0));
        if (wpi != null)
        {
            // Update web part CSS
            try
            {
                wpi.WebPartCSS = etaCSS.Text;
                WebPartInfoProvider.SetWebPartInfo(wpi);
                lblInfo.Text = GetString("General.ChangesSaved");
                lblInfo.Visible = true;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }

            // Show error message
            if (errorMessage != "")
            {
                lblError.Text = errorMessage;
                lblError.Visible = true;
            }
        }
    }
}

