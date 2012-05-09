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

public partial class CMSModules_PortalEngine_UI_PageTemplates_PageTemplate_New : SiteManagerPage
{
    #region "Variables"

    protected int parentCategoryId = 0;

    #endregion


    #region "Page events"

    /// <summary>
    /// Handles the Load event of the Page control.
    /// </summary>
    protected void Page_Load(object sender, EventArgs e)
    {
        parentCategoryId = QueryHelper.GetInteger("parentcategoryid", 0);
        
        if (!RequestHelper.IsPostBack())
        {
            lblTemplateDisplayName.Text = GetString("Administration-PageTemplate_General.TemplateDisplayName");
            lblTemplateDescription.Text = GetString("Administration-PageTemplate_General.TemplateDescription");
            lblTemplateCodeName.Text = GetString("Administration-PageTemplate_General.TemplateCodeName");
            btnOk.Text = GetString("General.OK");

            rfvTemplateDisplayName.ErrorMessage = GetString("Administration-PageTemplate_General.ErrorEmptyTemplateDisplayName");
            rfvTemplateCodeName.ErrorMessage = GetString("Administration-PageTemplate_General.ErrorEmptyTemplateCodeName");
            txtTemplateDisplayName.Focus();
        }
        string currentPageTemplate = GetString("Administration-PageTemplate_Header.NewPageTemplate");
        // Initialize page title
        string templates = GetString("Administration-PageTemplate_Header.Templates");
        string title = GetString("PageTemplate.NewTitle");
        
        string[,] pageTitleTabs = new string[3, 4];
        int i = 0;

        pageTitleTabs[i, 0] = templates;
        pageTitleTabs[i, 1] = URLHelper.ResolveUrl("~/CMSModules/PortalEngine/UI/PageTemplates/Category_Frameset.aspx");
        pageTitleTabs[i, 2] = "";
        pageTitleTabs[i, 3] = "if (parent.frames['pt_tree']) { parent.frames['pt_tree'].location.href = '" + URLHelper.ResolveUrl("~/CMSModules/PortalEngine/UI/PageTemplates/PageTemplate_Tree.aspx") + "'; }";
        i++;

        PageTemplateCategoryInfo categoryInfo = PageTemplateCategoryInfoProvider.GetPageTemplateCategoryInfo(parentCategoryId);

        // Check if the parent category is a root category, if not => display both (root + parent)
        if ((categoryInfo != null) && (categoryInfo.ParentId != 0))
        {
            // Add a cetegory tab
            pageTitleTabs[i, 0] = HTMLHelper.HTMLEncode(categoryInfo.DisplayName);
            pageTitleTabs[i, 1] = URLHelper.ResolveUrl("~/CMSModules/PortalEngine/UI/PageTemplates/Category_Frameset.aspx") + "?categoryid=" + categoryInfo.CategoryId;
            pageTitleTabs[i, 2] = "";
            i++;
        }

        pageTitleTabs[i, 0] = HTMLHelper.HTMLEncode(currentPageTemplate);
        pageTitleTabs[i, 1] = "";
        pageTitleTabs[i, 2] = "";
        i++;

        this.CurrentMaster.Title.Breadcrumbs = pageTitleTabs;
        this.CurrentMaster.Title.TitleText = title;
        this.CurrentMaster.Title.TitleImage = GetImageUrl("CMSModules/CMS_PageTemplates/addpagetemplate.png");
        this.CurrentMaster.Title.HelpTopicName = "new_page_template";
        this.CurrentMaster.Title.HelpName = "helpTopic";
    }
    
    
    protected void btnOK_Click(object sender, EventArgs e)
    {
        // Limit text length
        txtTemplateCodeName.Text = TextHelper.LimitLength(txtTemplateCodeName.Text.Trim(), 100, "");
        txtTemplateDisplayName.Text = TextHelper.LimitLength(txtTemplateDisplayName.Text.Trim(), 200, "");

        // finds whether required fields are not empty
        string result = new Validator().NotEmpty(txtTemplateDisplayName.Text, GetString("Administration-PageTemplate_General.ErrorEmptyTemplateDisplayName")).NotEmpty(txtTemplateCodeName.Text, GetString("Administration-PageTemplate_General.ErrorEmptyTemplateCodeName"))
            .IsCodeName(txtTemplateCodeName.Text, GetString("general.invalidcodename"))
            .Result;

        if (result == "")
        {
            if (parentCategoryId > 0)
            {
                if (!PageTemplateInfoProvider.PageTemplateNameExists(txtTemplateCodeName.Text))
                {
                    //Insert page template info
                    PageTemplateInfo pi = new PageTemplateInfo();
                    pi.DisplayName = txtTemplateDisplayName.Text;
                    pi.CodeName = txtTemplateCodeName.Text;
                    pi.FileName = "";
                    pi.WebParts = "";
                    pi.Description = txtTemplateDescription.Text;
                    pi.CategoryID = parentCategoryId;
                    pi.IsReusable = true; //set default value for isReusable
                    PageTemplateInfoProvider.SetPageTemplateInfo(pi);

                    string script = "parent.frames['pt_tree'].location.href = 'PageTemplate_Tree.aspx?templateid=" + pi.PageTemplateId + "';";
                    script += "this.location.href = 'PageTemplate_Edit.aspx?templateid=" + pi.PageTemplateId + "';";
                    ltlScript.Text += ScriptHelper.GetScript(script);
                }
                else
                {
                    lblError.Visible = true;
                    lblError.Text = GetString("Administration-PageTemplate_New.TemplateExistsAlready");
                }
            }
            else
            {
                lblError.Visible = true;
                lblError.Text = GetString("Administration-PageTemplate_New.NoParentIdGiven");
            }
        }
        else
        {
            rfvTemplateDisplayName.Visible = false;
            rfvTemplateCodeName.Visible = false;
            lblError.Visible = true;
            lblError.Text = result;
        }
    }

    #endregion
}
