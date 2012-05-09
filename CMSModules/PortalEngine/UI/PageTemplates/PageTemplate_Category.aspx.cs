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

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.SiteProvider;
using CMS.PortalEngine;
using CMS.UIControls;

public partial class CMSModules_PortalEngine_UI_PageTemplates_PageTemplate_Category : SiteManagerPage
{
    #region "Variables"

    protected int templateCategoryId = 0;
    protected int parentCategoryId = 0;

    #endregion


    #region "Page events"

    /// <summary>
    /// Handles the Load event of the Page control.
    /// </summary>
    protected void Page_Load(object sender, EventArgs e)
    {
        lblCategoryDisplayName.Text = GetString("Development-PageTemplate_Category.DisplayName");
        lblCategoryName.Text = GetString("Development-PageTemplate_Category.Name");
        btnOk.Text = GetString("General.OK");
        rfvCategoryDisplayName.ErrorMessage = GetString("General.RequiresDisplayName");
        rfvCategoryName.ErrorMessage = GetString("General.RequiresCodeName");

        this.CurrentMaster.Title.TitleText = GetString("development-pagetemplate_category.title");
        this.CurrentMaster.Title.TitleImage = GetImageUrl("Objects/CMS_PageTemplateCategory/new.png");
        this.CurrentMaster.Title.HelpTopicName = "new_category";
        this.CurrentMaster.Title.HelpName = "helpTopic";

        string templateCategoryDisplayName = "";
        string templateCategoryName = "";
        string currentTemplateCategoryName = GetString("Development-PageTemplate_Category.Title"); 
        string categoryImagePath = "";

        templateCategoryId = QueryHelper.GetInteger("categoryid", 0);
        parentCategoryId = QueryHelper.GetInteger("parentcategoryid", 0);

        if (templateCategoryId > 0)
        {
            // Existing category

            // Hide breadcrumbs and title
            this.CurrentMaster.Title.TitleText = "";
            this.CurrentMaster.Title.Breadcrumbs = null;
            PageTemplateCategoryInfo ptci = PageTemplateCategoryInfoProvider.GetPageTemplateCategoryInfo(templateCategoryId);
            if (ptci != null)
            {
                templateCategoryDisplayName = ptci.DisplayName;
                templateCategoryName = ptci.CategoryName;
                currentTemplateCategoryName = ptci.CategoryName;
                categoryImagePath = ptci.CategoryImagePath;
                parentCategoryId = ptci.ParentId;

                // If it's root category hide category name textbox
                if ((parentCategoryId == 0) || (ptci.CategoryName == "AdHoc"))
                {
                    plcCategoryName.Visible = false;
                }
            }
        }
        else
        {
            // New category
            this.CurrentMaster.Title.HelpName = "helpTopic";
            this.CurrentMaster.Title.HelpTopicName = "page_template_category_general";

            // Load parent category name
            PageTemplateCategoryInfo parentCategoryInfo = PageTemplateCategoryInfoProvider.GetPageTemplateCategoryInfo(parentCategoryId);
            string parentCategoryName = GetString("development.pagetemplates");
            if (parentCategoryInfo != null)
            {
                parentCategoryName = parentCategoryInfo.DisplayName;
            }

            // initializes breadcrumbs		
            string[,] tabs = new string[3, 4];

            tabs[0, 0] = GetString("development.pagetemplates");
            tabs[0, 1] = URLHelper.ResolveUrl("~/CMSModules/PortalEngine/UI/PageTemplates/Category_Frameset.aspx");
            tabs[0, 2] = "";
            tabs[0, 3] = "if (parent.frames['pt_tree']) { parent.frames['pt_tree'].location.href = '" + URLHelper.ResolveUrl("~/CMSModules/PortalEngine/UI/PageTemplates/PageTemplate_Tree.aspx") + "'; }";

            tabs[1, 0] = HTMLHelper.HTMLEncode(parentCategoryName);
            tabs[1, 1] = URLHelper.ResolveUrl("~/CMSModules/PortalEngine/UI/PageTemplates/Category_Frameset.aspx?categoryid=" + parentCategoryId);
            tabs[1, 2] = "";

            tabs[2, 0] = GetString("Development-PageTemplate_Category.TitleNew");
            tabs[2, 1] = "";
            tabs[2, 2] = "";

            // set master page
            this.CurrentMaster.Title.Breadcrumbs = tabs;
            this.CurrentMaster.Title.TitleText = currentTemplateCategoryName;
            this.CurrentMaster.Title.TitleImage = GetImageUrl("Objects/CMS_PageTemplateCategory/new.png");
        }

        if (!RequestHelper.IsPostBack())
        {
            txtCategoryDisplayName.Text = templateCategoryDisplayName;
            txtCategoryName.Text = templateCategoryName;
            txtCategoryImagePath.Text = categoryImagePath;
        }
    }


    /// <summary>
    /// Saves data of edited workflow scope from TextBoxes into DB.
    /// </summary>
    protected void btnOK_Click(object sender, EventArgs e)
    {
        // Check whether required fields are not empty
        string displayName = txtCategoryDisplayName.Text.Trim();
        string codeName = txtCategoryName.Text.Trim();
        string imagePath = txtCategoryImagePath.Text.Trim();

        string result = new Validator().NotEmpty(displayName, GetString("General.RequiresDisplayName")).NotEmpty(codeName, GetString("General.RequiresCodeName")).Result;

        // Validate the codename
        if (parentCategoryId != 0)
        {
            if (!ValidationHelper.IsCodeName(codeName))
            {
                result = GetString("General.ErrorCodeNameInIdentificatorFormat");
            }
        }

        // Check codename uniqness
        if ((templateCategoryId == 0) && (PageTemplateCategoryInfoProvider.GetPageTemplateCategoryInfoByCodeName(codeName) != null))
        {
            result = GetString("General.CodeNameExists");
        }

        if (result == "")
        {
            if (templateCategoryId > 0)
            {
                PageTemplateCategoryInfo ptci = PageTemplateCategoryInfoProvider.GetPageTemplateCategoryInfo(templateCategoryId);
                if (ptci != null)
                {
                    ptci.DisplayName = displayName;
                    ptci.CategoryName = codeName;
                    ptci.CategoryImagePath = imagePath;
                    PageTemplateCategoryInfoProvider.SetPageTemplateCategoryInfo(ptci);
                    lblInfo.Visible = true;
                    lblInfo.ResourceString = "General.ChangesSaved";

                    // Reload tree
                    string script = "parent.parent.frames['pt_tree'].location.href = '" + URLHelper.ResolveUrl("~/CMSModules/PortalEngine/UI/PageTemplates/PageTemplate_Tree.aspx?categoryid=" + templateCategoryId) + "';";
                    script += "parent.frames['categoryHeader'].location.href = '" + URLHelper.ResolveUrl("~/CMSModules/PortalEngine/UI/PageTemplates/Category_Header.aspx?categoryid=" + templateCategoryId + "&saved=1") + "';";
                    ltlScript.Text += ScriptHelper.GetScript(script);
                }
            }
            else
            {
                PageTemplateCategoryInfo ptci = new PageTemplateCategoryInfo();
                ptci.DisplayName = displayName;
                ptci.CategoryName = codeName;
                ptci.CategoryImagePath = imagePath;
                ptci.ParentId = parentCategoryId;
                try
                {
                    PageTemplateCategoryInfoProvider.SetPageTemplateCategoryInfo(ptci);
                    string script = "parent.frames['pt_tree'].location.href = '" + URLHelper.ResolveUrl("~/CMSModules/PortalEngine/UI/PageTemplates/PageTemplate_Tree.aspx?categoryid=" + ptci.CategoryId) + "';";
                    script += "this.location.href = '" + URLHelper.ResolveUrl("~/CMSModules/PortalEngine/UI/PageTemplates/Category_Frameset.aspx?categoryid=" + ptci.CategoryId) + "';";
                    ltlScript.Text += ScriptHelper.GetScript(script);
                }
                catch (Exception ex)
                {
                    lblError.Text = ex.Message;
                    lblError.Visible = true;
                }
            }
        }
        else
        {
            lblError.Visible = true;
            lblError.Text = result;
        }
    }

    #endregion
}
