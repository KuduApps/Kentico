using System;
using System.Data;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

using CMS.PortalControls;
using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.SiteProvider;

public partial class CMSWebParts_TaggingCategories_CategoryBreadcrumbs : CMSAbstractWebPart
{
    #region "Variables"

    private CategoryInfo mCategory = null;
    private CategoryInfo mStartingCategoryObj = null;

    #endregion


    #region "Properties"

    /// <summary>
    /// Breadcrumbs root
    /// </summary>
    public string BreadcrumbsRoot
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("BreadcrumbsRoot"), "");
        }
        set
        {
            this.SetValue("BreadcrumbsRoot", value);
        }
    }


    /// <summary>
    /// Breadcrumbs separator.
    /// </summary>
    public string BreadcrumbsSeparator
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("BreadcrumbsSeparator"), "/");
        }
        set
        {
            this.SetValue("BreadcrumbsSeparator", value);
        }
    }


    /// <summary>
    /// Breadcrumbs separator RTL.
    /// </summary>
    public string BreadcrumbsSeparatorRTL
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("BreadcrumbsSeparatorRTL"), "\\");
        }
        set
        {
            this.SetValue("BreadcrumbsSeparatorRTL", value);
        }
    }


    /// <summary>
    /// Render link title.
    /// </summary>
    public bool RenderLinkTitle
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("RenderLinkTitle"), false);
        }
        set
        {
            this.SetValue("RenderLinkTitle", value);
        }
    }


    /// <summary>
    /// Categories page target.
    /// </summary>
    public string CategoriesPageTarget
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("CategoriesPageTarget"), "");
        }
        set
        {
            this.SetValue("CategoriesPageTarget", value);
        }
    }


    /// <summary>
    /// Starting category.
    /// </summary>
    public string BreadcrumbsStartingCategory
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("BreadcrumbsStartingCategory"), "");
        }
        set
        {
            this.SetValue("BreadcrumbsStartingCategory", value);
        }
    }


    /// <summary>
    /// Breadcrumb content before.
    /// </summary>
    public string BreadcrumbContentBefore
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("BreadcrumbContentBefore"), "");
        }
        set
        {
            this.SetValue("BreadcrumbContentBefore", value);
        }
    }


    /// <summary>
    /// Breadcrumb content after.
    /// </summary>
    public string BreadcrumbContentAfter
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("BreadcrumbContentAfter"), "");
        }
        set
        {
            this.SetValue("BreadcrumbContentAfter", value);
        }
    }


    /// <summary>
    /// Show current item.
    /// </summary>
    public bool ShowCurrentItem
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("ShowCurrentItem"), true);
        }
        set
        {
            this.SetValue("ShowCurrentItem", value);
        }
    }


    /// <summary>
    /// Show current item as link.
    /// </summary>
    public bool ShowCurrentItemAsLink
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("ShowCurrentItemAsLink"), false);
        }
        set
        {
            this.SetValue("ShowCurrentItemAsLink", value);
        }
    }


    /// <summary>
    /// Categories page path.
    /// </summary>
    public string CategoriesPagePath
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("CategoriesPagePath"), "");
        }
        set
        {
            this.SetValue("CategoriesPagePath", value);
        }
    }


    /// <summary>
    /// Current category info object.
    /// </summary>
    public CategoryInfo Category
    {
        get
        {
            if (mCategory == null)
            {
                mCategory = SiteContext.CurrentCategory;
            }

            return mCategory;
        }
        set
        {
            mCategory = value;
        }
    }


    /// <summary>
    /// Starting category info object.
    /// </summary>
    private CategoryInfo StartingCategoryObj
    {
        get
        {
            if (mStartingCategoryObj == null)
            {
                mStartingCategoryObj = CategoryInfoProvider.GetCategoryInfo(BreadcrumbsStartingCategory, CMSContext.CurrentSiteName);
            }

            return mStartingCategoryObj;
        }
    }


    #endregion


    #region "Methods"

    /// <summary>
    /// Content loaded event handler.
    /// </summary>
    public override void OnContentLoaded()
    {
        base.OnContentLoaded();
        SetupControl();
    }


    /// <summary>
    /// Initializes the control properties.
    /// </summary>
    protected void SetupControl()
    {
        if (!this.StopProcessing)
        {
            if (CheckCurrentCategory())
            {
                // Get processed category paths
                string idPath = Category.CategoryIDPath;
                string namePath = Category.CategoryNamePath;

                // Handle custom starting category
                if (!string.IsNullOrEmpty(BreadcrumbsStartingCategory) && (StartingCategoryObj != null))
                {
                    // Check if category from other parent selected
                    if (!idPath.StartsWith(StartingCategoryObj.CategoryIDPath) || !namePath.StartsWith(StartingCategoryObj.CategoryNamePath))
                    {
                        return;
                    }

                    // Shorten paths
                    namePath = namePath.Substring(StartingCategoryObj.CategoryNamePath.Length);
                    idPath = idPath.Replace(StartingCategoryObj.CategoryIDPath, "");
                }

                ltlBreadcrumbs.Text = CreateCategoryLine(idPath, namePath);
            }
        }
    }


    /// <summary>
    /// Reloads the control data.
    /// </summary>
    public override void ReloadData()
    {
        base.ReloadData();

        SetupControl();
    }


    /// <summary>
    /// Checks if current category exists and if can be used on current site by current user.
    /// </summary>
    protected bool CheckCurrentCategory()
    {
        if (Category != null)
        {
            if (Category.CategoryIsPersonal)
            {
                // Check if personal category belongs to current user.
                if ((CMSContext.CurrentUser != null) && (Category.CategoryUserID == CMSContext.CurrentUser.UserID))
                {
                    return true;
                }
            }
            else
            {
                return true;
            }
        }

        return false;
    }


    /// <summary>
    /// Creates html entry for one category.
    /// </summary>
    /// <param name="categoryIdPath">ID path of rendered category.</param>
    /// <param name="categoryNamePath">Name path of rendered category.</param>
    protected string CreateCategoryLine(string categoryIdPath, string categoryNamePath)
    {
        // Split paths
        string[] idSplits = categoryIdPath.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
        string[] nameSplits = categoryNamePath.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);

        bool separator = false;

        // Need rtl rendering
        bool jrtl = false;

        // Set rtl rendering
        jrtl = CultureHelper.IsPreferredCultureRTL();

        StringBuilder line = new StringBuilder();

        int count = idSplits.Length;

        count -= ShowCurrentItem ? 0 : 1;

        if ((count <= nameSplits.Length) && (count > 0))
        {
            // Convert string to ints
            int[] intIdSplits = ValidationHelper.GetIntegers(idSplits, -1);

            // Append prefix if any
            if (!string.IsNullOrEmpty(BreadcrumbsRoot))
            {
                line.Append(CreateCategoryPartLink(BreadcrumbsRoot, 0, false));
                separator = true;
            }

            // Append whole category path
            for (int i = 0; i < count; i++)
            {
                // Do not append the separator at the beginning
                if (separator)
                {
                    line.Append(jrtl ? BreadcrumbsSeparatorRTL : BreadcrumbsSeparator);
                }

                // Don't create link for current category item (last part) when disabled
                if ((i == count - 1) && ShowCurrentItem && !ShowCurrentItemAsLink)
                {
                    // Append display name part as text
                    line.Append(FormatCategoryDisplayName(nameSplits[i], true));
                }
                else
                {
                    // Append display name part as link
                    line.Append(CreateCategoryPartLink(nameSplits[i], intIdSplits[i], true));
                }

                separator = true;
            }
        }

        return line.ToString();
    }


    /// <summary>
    /// Creates HTML code for category link.
    /// </summary>
    /// <param name="categoryDisplayName">Category display name.</param>
    /// <param name="categoryId">ID of the category.</param>
    protected string CreateCategoryPartLink(string categoryDisplayName, int categoryId, bool encode)
    {
        // Get target url
        string url = (String.IsNullOrEmpty(CategoriesPagePath) ? URLHelper.CurrentURL : CMSContext.GetUrl(CategoriesPagePath));

        if (categoryId > 0)
        {
            // Append category parameter
            url = URLHelper.AddParameterToUrl(url, "categoryId", categoryId.ToString());
        }

        StringBuilder attrs = new StringBuilder();

        // Append target attribute
        if (!string.IsNullOrEmpty(CategoriesPageTarget))
        {
            attrs.Append(" target=\"").Append(CategoriesPageTarget).Append("\"");
        }

        // Append title attribute
        if (RenderLinkTitle && encode)
        {
            // Encode category name
            attrs.Append(" title=\"").Append(HTMLHelper.HTMLEncode(categoryDisplayName)).Append("\"");
        }
        
        return string.Format("<a href=\"{0}\"{1}>{2}</a>", url, attrs, FormatCategoryDisplayName(categoryDisplayName, encode));
    }


    /// <summary>
    /// Formats category display name. Applies CategoryContentBefore and CategoryContentAfter.
    /// </summary>
    /// <param name="categoryDisplayName">Category display name.</param>
    /// <param name="encode">Indicates whether category name will be encoded.</param>
    protected string FormatCategoryDisplayName(string categoryDisplayName, bool encode)
    {
        // Localize category display name
        categoryDisplayName = ResHelper.LocalizeString(categoryDisplayName);

        if (encode)
        {
            // Encode category display name
            categoryDisplayName = HTMLHelper.HTMLEncode(categoryDisplayName);
        }

        // Format name
        return string.Format("{0}{1}{2}", BreadcrumbContentBefore, categoryDisplayName, BreadcrumbContentAfter);
    }

    #endregion
}




