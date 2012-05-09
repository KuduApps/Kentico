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
using CMS.SiteProvider;
using CMS.PortalEngine;
using CMS.UIControls;
using CMS.CMSHelper;

public partial class CMSModules_PortalEngine_UI_Layout_SaveNewPageTemplate : CMSModalDesignPage
{
    protected int pageTemplateId = 0;
    protected PageTemplateInfo pt = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        pageTemplateId = QueryHelper.GetInteger("templateid", 0);
        if (pageTemplateId > 0)
        {
            pt = PageTemplateInfoProvider.GetPageTemplateInfo(pageTemplateId);
        }

        // Check the authorization per UI element
        CurrentUserInfo currentUser = CMSContext.CurrentUser;
        if (!currentUser.IsAuthorizedPerUIElement("CMS.Content", new string[] { "Properties", "Properties.Template", "Template.SaveAsNew" }, CMSContext.CurrentSiteName))
        {
            RedirectToCMSDeskUIElementAccessDenied("CMS.Content", "Properties;Properties.Template;Template.SaveAsNew");
        }

        CurrentMaster.Title.TitleText = GetString("PortalEngine.SaveNewPageTemplate.PageTitle");
        CurrentMaster.Title.TitleImage = GetImageUrl("CMSModules/CMS_Content/Template/save.png");

        lblTemplateDisplayName.Text = GetString("PortalEngine.SaveNewPageTemplate.DisplayName");
        lblTemplateDescription.Text = GetString("PortalEngine.SaveNewPageTemplate.Description");
        lblTemplateCategory.Text = GetString("PortalEngine.SaveNewPageTemplate.Category");
        lblTemplateCodeName.Text = GetString("PortalEngine.SaveNewPageTemplate.CodeName");

        btnOk.Text = GetString("general.ok");
        btnCancel.Text = GetString("general.cancel");

        rfvTemplateDisplayName.ErrorMessage = GetString("PortalEngine.SaveNewPageTemplate.rfvDisplayNameMsg");
        rfvTemplateCodeName.ErrorMessage = GetString("PortalEngine.SaveNewPageTemplate.rfvCodeNameMsg");

        // Preset category
        if (!RequestHelper.IsPostBack())
        {
            if (pt != null)
            {
                categorySelector.Value = pt.CategoryID.ToString();
            }
        }
    }


    protected void btnOK_Click(object sender, EventArgs e)
    {
        if (pt != null)
        {
            // Limit text length
            txtTemplateCodeName.Text = TextHelper.LimitLength(txtTemplateCodeName.Text.Trim(), 100, "");
            txtTemplateDisplayName.Text = TextHelper.LimitLength(txtTemplateDisplayName.Text.Trim(), 200, "");

            // finds whether required fields are not empty
            string result = new Validator().NotEmpty(txtTemplateDisplayName.Text, GetString("Administration-PageTemplate_General.ErrorEmptyTemplateDisplayName")).NotEmpty(txtTemplateCodeName.Text, GetString("Administration-PageTemplate_General.ErrorEmptyTemplateCodeName"))
                .IsCodeName(txtTemplateCodeName.Text, GetString("general.invalidcodename"))
                .Result;

            if (String.IsNullOrEmpty(result))
            {

                // Check if template with given name already exists            
                if (PageTemplateInfoProvider.PageTemplateNameExists(txtTemplateCodeName.Text))
                {
                    lblError.Text = GetString("general.codenameexists");
                    return;
                }

                if (pt.IsReusable == true)
                {
                    // Clone template with clear
                    pt = pt.Clone(true);
                }
                pt.CodeName = txtTemplateCodeName.Text;
                pt.DisplayName = txtTemplateDisplayName.Text;
                pt.Description = txtTemplateDescription.Text;

                pt.CategoryID = Convert.ToInt32(categorySelector.Value);

                pt.IsReusable = true;
                pt.PageTemplateSiteID = 0;
                try
                {
                    PageTemplateInfoProvider.SetPageTemplateInfo(pt);
                    int siteId = QueryHelper.GetInteger("siteid", 0);
                    if (siteId > 0)
                    {
                        PageTemplateInfoProvider.AddPageTemplateToSite(pt.PageTemplateId, siteId);
                    }
                    ltlScript.Text = ScriptHelper.GetScript("SelectActualData(" + pt.PageTemplateId.ToString() + ", " + pt.IsPortal.ToString().ToLower() + ", " + pt.IsReusable.ToString().ToLower() + ");");
                }
                catch (Exception ex)
                {
                    lblError.Text = ex.Message;
                }
            }
            else
            {
                lblError.Visible = true;
                lblError.Text = result;
            }
        }
    }
}
