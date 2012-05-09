using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using CMS.UIControls;
using CMS.SiteProvider;
using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.SettingsProvider;

public partial class CMSAdminControls_UI_UIProfiles_UIPanelMenu : CMSUserControl
{
    #region "Properties"

    /// <summary>
    /// Module name.
    /// </summary>
    public string ModuleName { get; set; }


    /// <summary>
    /// Number of columns in which categories will be displayed.
    /// </summary>
    public int ColumnsCount { get; set; }

    #endregion


    #region "Events"

    /// <summary>
    /// Category creation delegate.
    /// </summary>
    public delegate void CategoryCreatedEventHandler(object sender, CategoryCreatedEventArgs e);


    /// <summary>
    /// Category creation event.
    /// </summary>
    public event CategoryCreatedEventHandler CategoryCreated;

    #endregion


    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        CurrentUserInfo currentUser = CMSContext.CurrentUser;

        // Fill the menu with UIElement data for specified module
        if (!String.IsNullOrEmpty(this.ModuleName) & (currentUser != null))
        {
            DataSet dsModules = UIElementInfoProvider.GetUIMenuElements(ModuleName);

            List<object[]> categoriesTmp = new List<object[]>();

            if (!DataHelper.DataSourceIsEmpty(dsModules))
            {
                foreach (DataRow drModule in dsModules.Tables[0].Rows)
                {
                    UIElementInfo moduleElement = new UIElementInfo(drModule);

                    // Proceed if user has permissions for this UI element
                    if (currentUser.IsAuthorizedPerUIElement(this.ModuleName, moduleElement.ElementName))
                    {
                        // Category title
                        string categoryTitle = ResHelper.LocalizeString(moduleElement.ElementDisplayName);

                        // Category name
                        string categoryName = ResHelper.LocalizeString(moduleElement.ElementName);

                        // Category URL
                        string categoryUrl = CMSContext.ResolveMacros(URLHelper.EnsureHashToQueryParameters(moduleElement.ElementTargetURL));

                        // Category image URL
                        string categoryImageUrl = this.GetImagePath(moduleElement.ElementIconPath.Replace("list.png", "module.png"));
                        if (!FileHelper.FileExists(categoryImageUrl))
                        {
                            categoryImageUrl = this.GetImagePath("CMSModules/module.png");
                        }

                        // Category tooltip
                        string categoryTooltip = ResHelper.LocalizeString(moduleElement.ElementDescription);

                        // Category actions
                        DataSet dsActions = UIElementInfoProvider.GetChildUIElements(moduleElement.ElementID);

                        List<string[]> actionsTmp = new List<string[]>();

                        foreach (DataRow drAction in dsActions.Tables[0].Rows)
                        {
                            UIElementInfo actionElement = new UIElementInfo(drAction);

                            // Proceed if user has permissions for this UI element
                            if (currentUser.IsAuthorizedPerUIElement(this.ModuleName, actionElement.ElementName))
                            {
                                actionsTmp.Add(new string[] { ResHelper.LocalizeString(actionElement.ElementDisplayName), CMSContext.ResolveMacros(URLHelper.EnsureHashToQueryParameters(actionElement.ElementTargetURL)) });
                            }
                        }

                        int actionsCount = actionsTmp.Count;

                        string[,] categoryActions = new string[actionsCount, 2];

                        for (int i = 0; i < actionsCount; i++)
                        {
                            categoryActions[i, 0] = actionsTmp[i][0];
                            categoryActions[i, 1] = actionsTmp[i][1];
                        }

                        CategoryCreatedEventArgs args = new CategoryCreatedEventArgs(moduleElement, categoryName, categoryTitle, categoryUrl, categoryImageUrl, categoryTooltip, categoryActions);

                        // Raise additional initialization events for this category
                        if (this.CategoryCreated != null)
                        {
                            this.CategoryCreated(this, args);
                        }

                        // Add to categories, if further processing of this category was not cancelled
                        if (!args.Cancel)
                        {
                            categoriesTmp.Add(new object[] { args.CategoryTitle, args.CategoryName, args.CategoryURL, args.CategoryImageURL, args.CategoryTooltip, args.CategoryActions });
                        }
                    }
                }
            }

            int categoriesCount = categoriesTmp.Count;

            object[,] categories = new object[categoriesCount, 6];

            for (int i = 0; i < categoriesCount; i++)
            {
                categories[i, 0] = categoriesTmp[i][0];
                categories[i, 1] = categoriesTmp[i][1];
                categories[i, 2] = categoriesTmp[i][2];
                categories[i, 3] = categoriesTmp[i][3];
                categories[i, 4] = categoriesTmp[i][4];
                categories[i, 5] = categoriesTmp[i][5];
            }
            if (categoriesCount > 0)
            {
                this.panelMenu.Categories = categories;
                this.panelMenu.ColumnsCount = this.ColumnsCount;
            }
            else
            {
                RedirectToUINotAvailable();
            }

            // Add editing icon in development mode
            if (SettingsKeyProvider.DevelopmentMode && currentUser.IsGlobalAdministrator)
            {
                ResourceInfo ri = ResourceInfoProvider.GetResourceInfo(this.ModuleName);
                if (ri != null)
                {
                    ltlAfter.Text += "<div class=\"AlignRight\">" + UIHelper.GetResourceUIElementsLink(this.Page, ri.ResourceId) + "</div>";
                }
            }
        }
    }

    #endregion


    #region "Classes"

    /// <summary>
    /// Event arguments class for UIPanelMenu OnCategoryCreated event.
    /// </summary>
    public class CategoryCreatedEventArgs : EventArgs
    {
        #region "Properties"

        /// <summary>
        /// Base UI element for this category.
        /// </summary>
        public UIElementInfo UIElement { get; protected set; }


        /// <summary>
        /// Category name.
        /// </summary>
        public string CategoryName { get; set; }

        /// <summary>
        /// Category title.
        /// </summary>
        public string CategoryTitle { get; set; }


        /// <summary>
        /// Category URL.
        /// </summary>
        public string CategoryURL { get; set; }


        /// <summary>
        /// Category image url.
        /// </summary>
        public string CategoryImageURL { get; set; }


        /// <summary>
        /// Category tooltip.
        /// </summary>
        public string CategoryTooltip { get; set; }


        /// <summary>
        /// Category actions.
        /// </summary>
        public string[,] CategoryActions { get; protected set; }


        /// <summary>
        /// Indicates if further processing of this category will be done or not.
        /// </summary>
        public bool Cancel { get; set; }

        #endregion


        #region "Constructors"

        /// <summary>
        /// Initiliazes new instance of <see cref="CategoryCreatedEventArgs"/> class.
        /// </summary>
        /// <param name="categoryTitle">Category title</param>
        /// <param name="categoryUrl">Category URL</param>
        /// <param name="categoryImageUrl">Category image URL</param>
        /// <param name="categoryTooltip">Category tooltip</param>
        /// <param name="categoryActions">Category actions</param>
        public CategoryCreatedEventArgs(UIElementInfo element, string categoryName, string categoryTitle, string categoryUrl, string categoryImageUrl, string categoryTooltip, string[,] categoryActions)
        {
            this.UIElement = element;
            this.CategoryName = categoryName;
            this.CategoryTitle = categoryTitle;
            this.CategoryURL = categoryUrl;
            this.CategoryImageURL = categoryImageUrl;
            this.CategoryTooltip = categoryTooltip;
            this.CategoryActions = categoryActions;

            this.Cancel = false;
        }

        #endregion
    }

    #endregion
}