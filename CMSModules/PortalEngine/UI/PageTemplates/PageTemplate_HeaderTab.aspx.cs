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
using CMS.PortalEngine;
using CMS.UIControls;
using CMS.CMSHelper;

public partial class CMSModules_PortalEngine_UI_PageTemplates_PageTemplate_HeaderTab : CMSEditTemplatePage
{
    #region "Variables"

    protected int templateId = 0;

    #endregion


    #region "Page events"

    /// <summary>
    /// Handles the Load event of the Page control.
    /// </summary>
    protected void Page_Load(object sender, EventArgs e)
    {
        lblTemplateHeader.Text = GetString("pagetemplate_header.addheader");
        btnOk.Text = GetString("General.OK");

        // Get page template id from url
        templateId = QueryHelper.GetInteger("templateid", 0);

        if (!RequestHelper.IsPostBack())
        {
            // Load data to form
            PageTemplateInfo pti = PageTemplateInfoProvider.GetPageTemplateInfo(templateId);
            if (pti != null)
            {
                txtTemplateHeader.Text = pti.PageTemplateHeader;
            }
        }
    }


    /// <summary>
    /// Handles the Click event of the btnOK control.
    /// </summary>
    protected void btnOK_Click(object sender, EventArgs e)
    {
        PageTemplateInfo pti = PageTemplateInfoProvider.GetPageTemplateInfo(templateId);
        if (pti != null)
        {
            pti.PageTemplateHeader = txtTemplateHeader.Text.Trim();
            try
            {
                // Save changes
                PageTemplateInfoProvider.SetPageTemplateInfo(pti);
                lblInfo.Text = GetString("General.ChangesSaved");
                lblInfo.Visible = true;
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
                lblError.Visible = true;
            }
        }
    }

    #endregion
}
