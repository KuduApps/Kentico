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

public partial class CMSModules_PortalEngine_UI_PageTemplates_PageTemplate_WebParts : SiteManagerPage
{
    /// <summary>
    /// PageTemplateID.
    /// </summary>
    protected int templateId = 0;


    /// <summary>
    /// Page load.
    /// </summary>
    protected void Page_Load(object sender, EventArgs e)
    {
        // Initialize controls.        
        lblWarning.Text = GetString("PageTemplate_WebParts.Warning");
        lblWPConfig.Text = GetString("PageTemplate_WebParts.WebPartsConfiguration");
        btnOk.Text = GetString("General.OK");

        // Get 'templateid' from querystring.
        templateId = QueryHelper.GetInteger("templateId", 0);

        if (!RequestHelper.IsPostBack())
        {
            // Get PageTemplateInfo of specified 'templateid'.
            PageTemplateInfo pti = PageTemplateInfoProvider.GetPageTemplateInfo(templateId);
            if (pti != null)
            {
                txtWebParts.Text = HTMLHelper.ReformatHTML(pti.WebParts, "  ");
            }            
        }        
    }


    /// <summary>
    /// Handle btnOK's OnClick event.
    /// </summary>
    protected void btnOK_Click(object sender, EventArgs e)
    {
        string errorMessage = "";

        // Get PageTemplateInfo.
        PageTemplateInfo pti = PageTemplateInfoProvider.GetPageTemplateInfo(templateId);
        if (pti != null)
        {            
            // Update WebParts configuration in PageTemplate.
            try
            {
                pti.WebParts = txtWebParts.Text;
                PageTemplateInfoProvider.SetPageTemplateInfo(pti);
                lblInfo.Text = GetString("General.ChangesSaved");
                lblInfo.Visible = true;

                // Update textbox value
                txtWebParts.Text = HTMLHelper.ReformatHTML(pti.WebParts, "  ");
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
