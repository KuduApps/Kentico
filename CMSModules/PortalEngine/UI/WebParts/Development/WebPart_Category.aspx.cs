using System;

using CMS.GlobalHelper;
using CMS.SiteProvider;
using CMS.PortalEngine;
using CMS.UIControls;

public partial class CMSModules_PortalEngine_UI_WebParts_Development_WebPart_Category : SiteManagerPage
{
    protected int categoryId;
    protected int parentCategoryId;

    string[,] pageTitleTabs = new string[2, 3];

    protected void Page_Load(object sender, EventArgs e)
    {
        // Initialize
        lblCategoryName.Text = GetString("General.CategoryName");
        lblCategoryDisplayName.Text = GetString("General.CategoryDisplayName");
        btnOk.Text = GetString("general.ok");
        rfvCategoryName.ErrorMessage = GetString("General.RequiresCodeName");
        rfvCategoryDisplayName.ErrorMessage = GetString("General.RequiresDisplayName");

        pageTitleTabs[0, 0] = GetString("Development-WebPart_Category.Category");
        pageTitleTabs[0, 1] = URLHelper.ResolveUrl("~/CMSModules/PortalEngine/UI/WebParts/Development/WebPart_Frameset.aspx");
        pageTitleTabs[0, 2] = "_parent";

        pageTitleTabs[1, 0] = GetString("Development-WebPart_Category.New");
        pageTitleTabs[1, 1] = "";
        pageTitleTabs[1, 2] = "";        

        // Get category id
        categoryId = QueryHelper.GetInteger("categoryid", 0);
        parentCategoryId = QueryHelper.GetInteger("parentid", 0);

        string categoryName = "";
        string categoryDisplayName = "";
        string currentWebPartCategoryName = GetString("objecttype.cms_webpartcategory"); 
        string categoryImagePath = "" ;
        string pageTitleText = "";
        string pageTitleImage = "";

        if (categoryId > 0)
        {
            // Existing category

            // Hide breadcrumbs and title
            this.CurrentMaster.Title.TitleText = "";
            this.CurrentMaster.Title.Breadcrumbs = null;

            WebPartCategoryInfo ci = WebPartCategoryInfoProvider.GetWebPartCategoryInfoById(categoryId);
            if (ci != null)
            {
                categoryName = ci.CategoryName;
                categoryDisplayName = ci.CategoryDisplayName;
                categoryImagePath = ci.CategoryImagePath ;
                parentCategoryId = ci.CategoryParentID;
                currentWebPartCategoryName = ci.CategoryName;

                // If it's root category hide category name textbox
                if (parentCategoryId == 0)
                {
                    plcCategoryName.Visible = false;
                }

                pageTitleTabs[1, 0] = HTMLHelper.HTMLEncode(ci.CategoryDisplayName);
                pageTitleText = GetString("Development-WebPart_Category.Title");
                pageTitleImage = GetImageUrl("Objects/CMS_WebPartCategory/object.png");                                  
            }
        }
        else
        {
            // New category
            this.CurrentMaster.Title.HelpName = "helpTopic";
            this.CurrentMaster.Title.HelpTopicName = "web_part_category_general";

            // Load parent category name
            WebPartCategoryInfo parentCategoryInfo = WebPartCategoryInfoProvider.GetWebPartCategoryInfoById(parentCategoryId);
            string parentCategoryName = GetString("development-webpart_header.webparttitle");
            if (parentCategoryInfo != null)
            {
                parentCategoryName = parentCategoryInfo.CategoryDisplayName;
            }

            // Initializes breadcrumbs		
            string[,] tabs = new string[3, 4];

            tabs[0, 0] = GetString("development-webpart_header.webparttitle");
            tabs[0, 1] = URLHelper.ResolveUrl("~/CMSModules/PortalEngine/UI/WebParts/Development/Category_Frameset.aspx");
            tabs[0, 2] = "";
            tabs[0, 3] = "if (parent.frames['webparttree']) { parent.frames['webparttree'].location.href = 'WebPart_Tree.aspx'; }";

            tabs[1, 0] = HTMLHelper.HTMLEncode(parentCategoryName);
            tabs[1, 1] = URLHelper.ResolveUrl("~/CMSModules/PortalEngine/UI/WebParts/Development/Category_Frameset.aspx?categoryid=" + parentCategoryId);
            tabs[1, 2] = "";

            tabs[2, 0] = GetString("development-webpart_category.titlenew");
            tabs[2, 1] = "";
            tabs[2, 2] = "";

            // Set master page
            this.CurrentMaster.Title.Breadcrumbs = tabs;
            this.CurrentMaster.Title.TitleText = HTMLHelper.HTMLEncode(currentWebPartCategoryName);
            this.CurrentMaster.Title.TitleImage = GetImageUrl("Objects/CMS_WebPartCategory/new.png");
        }

        if (!RequestHelper.IsPostBack())
        {
            txtCategoryDisplayName.Text = HTMLHelper.HTMLEncode(categoryDisplayName);
            txtCategoryName.Text = HTMLHelper.HTMLEncode(categoryName);
            txtCategoryImagePath.Text = HTMLHelper.HTMLEncode(categoryImagePath);
        }

    }


    /// <summary>
    /// Creates new or updates category name.
    /// </summary>
    protected void btnOK_Click(object sender, EventArgs e)
    {
        string displayName = txtCategoryDisplayName.Text;
        string codeName = txtCategoryName.Text;
        string imagePath = txtCategoryImagePath.Text;

        string result = new Validator().NotEmpty(displayName, GetString("General.RequiresDisplayName")).NotEmpty(codeName, GetString("General.RequiresCodeName")).Result;

        // If it's root category don't validate codename
        if (parentCategoryId != 0)
        {
            // Validate the codename
            if (!ValidationHelper.IsCodeName(codeName))
            {
                result = GetString("General.ErrorCodeNameInIdentificatorFormat");
            }
        }

        // Check codename uniqness
        if ((categoryId == 0) && (WebPartCategoryInfoProvider.GetWebPartCategoryInfoByCodeName(codeName) != null))
        {
            result = GetString("General.CodeNameExists");
        }

        if (result == "")
        {
            WebPartCategoryInfo ci;

            lblInfo.Visible = true;
            if (categoryId > 0)
            {
                ci = WebPartCategoryInfoProvider.GetWebPartCategoryInfoById(categoryId);
                ci.CategoryDisplayName = displayName;                
                ci.CategoryName = codeName;                
                ci.CategoryImagePath = imagePath;

                try
                {
                    WebPartCategoryInfoProvider.SetWebPartCategoryInfo(ci);
                    string script = "parent.parent.frames['webparttree'].location.href = '" + URLHelper.ResolveUrl("~/CMSModules/PortalEngine/UI/WebParts/Development/WebPart_Tree.aspx?categoryid=" + categoryId) + "';";
                    script += "parent.frames['categoryHeader'].location.href = '" + URLHelper.ResolveUrl("~/CMSModules/PortalEngine/UI/WebParts/Development/Category_Header.aspx?categoryid=" + categoryId + "&saved=1") + "';";
                    ltlScript.Text += ScriptHelper.GetScript(script);
                }
                catch(Exception ex)
                {
                    lblInfo.Visible = false;
                    lblError.Visible = true;
                    lblError.Text = ex.Message.Replace("%%name%%", displayName);
                }
            }
            else
            {
                ci = new WebPartCategoryInfo();
                ci.CategoryDisplayName = displayName;
                ci.CategoryName = codeName;
                ci.CategoryParentID = parentCategoryId;
                ci.CategoryImagePath = imagePath;

                try
                {
                    WebPartCategoryInfoProvider.SetWebPartCategoryInfo(ci);
                    string script = "parent.frames['webparttree'].location.href = '" + URLHelper.ResolveUrl("~/CMSModules/PortalEngine/UI/WebParts/Development/WebPart_Tree.aspx?categoryid=" + ci.CategoryID) + "';";
                    script += "this.location.href = '" + URLHelper.ResolveUrl("~/CMSModules/PortalEngine/UI/WebParts/Development/Category_Frameset.aspx?categoryid=" + ci.CategoryID) + "';";
                    ltlScript.Text += ScriptHelper.GetScript(script);
                }
                catch(Exception ex)
                {
                    lblInfo.Visible = false;
                    lblError.Visible = true;
                    lblError.Text = ex.Message.Replace("%%name%%", displayName);
                }
            }

            lblInfo.Text = GetString("General.ChangesSaved");
            pageTitleTabs[1, 0] = HTMLHelper.HTMLEncode(ci.CategoryDisplayName);
        }
        else
        {
            lblInfo.Visible = false;
            lblError.Visible = true;
            lblError.Text = HTMLHelper.HTMLEncode(result);
        }
    }
}
