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
using CMS.UIControls;
using CMS.CMSHelper;
using CMS.SettingsProvider;
using CMS.ExtendedControls;

public partial class CMSModules_Categories_Controls_Categories : CMSAdminEditControl
{
    #region "Variables and constants"

    private int mSelectedCategoryId = -1;
    private CategoryInfo mSelectedCategory = null;
    private int mSelectedParentId = 0;
    private const int CATEGORIES_ROOT_PARENT_ID = -1;
    private const int PERSONAL_CATEGORIES_ROOT_PARENT_ID = -2;
    private int mUserId = 0;
    private bool mDisplayPersonalCategories = true;
    private bool mDisplaySiteCategories = true;
    private bool mStartInCreatingMode = false;
    private bool mDisplaySiteSelector = true;
    private string mGlobalCategoryIcon = "Objects/CMS_Category/global.png";
    private string mSiteCategoryIcon = "Objects/CMS_Category/category.png";
    private string mPersonalCategoryIcon = "Objects/CMS_Category/category.png";
    private bool canModifySite = false;
    private bool canModifyGlobal = false;
    private bool? mAllowGlobalCategories = null;

    #endregion


    #region "Properties"

    /// <summary>
    /// ID of the user to manage categories for. Default value is ID of the current user.
    /// </summary>
    public int UserID
    {
        get
        {
            if (mUserId > 0)
            {
                return mUserId;
            }

            if (CMSContext.CurrentUser != null)
            {
                return CMSContext.CurrentUser.UserID;
            }

            return 0;
        }
        set
        {
            mUserId = value;
        }
    }


    /// <summary>
    /// Get ID of the selected site.
    /// </summary>
    public int SiteID
    {
        get
        {
            if (DisplaySiteSelector)
            {
                int siteId = SelectSite.SiteID;

                return (siteId < 0) ? 0 : siteId;
            }
            else
            {
                return CMSContext.CurrentSiteID;
            }
        }
    }


    /// <summary>
    /// Indicates whether personal categories are to be displayed.
    /// </summary>
    public bool DisplayPersonalCategories
    {
        get
        {
            return mDisplayPersonalCategories;
        }
        set
        {
            mDisplayPersonalCategories = value;
        }
    }


    /// <summary>
    /// Indicates whether general categories are to be displayed.
    /// </summary>
    public bool DisplaySiteCategories
    {
        get
        {
            return mDisplaySiteCategories;
        }
        set
        {
            mDisplaySiteCategories = value;
        }
    }


    /// <summary>
    /// Indicates whether site selector will be displayed.
    /// </summary>
    public bool DisplaySiteSelector
    {
        get
        {
            return mDisplaySiteSelector;
        }
        set
        {
            mDisplaySiteSelector = value;
        }
    }


    /// <summary>
    /// Partial icon path for personal categories. Default value is Objects/CMS_Category/list.png.
    /// </summary>
    public string PersonalCategoryIcon
    {
        get
        {
            return mPersonalCategoryIcon;
        }
        set
        {
            mPersonalCategoryIcon = value;
        }
    }


    /// <summary>
    /// Partial icon path for site categories. Default value is Objects/CMS_Category/list.png.
    /// </summary>
    public string SiteCategoryIcon
    {
        get
        {
            return mSiteCategoryIcon;
        }
        set
        {
            mSiteCategoryIcon = value;
        }
    }


    /// <summary>
    /// Partial icon path for global categories. Default value is Objects/CMS_Category/global.png.
    /// </summary>
    public string GlobalCategoryIcon
    {
        get
        {
            return mGlobalCategoryIcon;
        }
        set
        {
            mGlobalCategoryIcon = value;
        }
    }


    /// <summary>
    /// Allows to make control start with 'Create new category' form opened.
    /// </summary>
    public bool StartInCreatingMode
    {
        get
        {
            return mStartInCreatingMode;
        }
        set
        {
            mStartInCreatingMode = value;
        }
    }


    /// <summary>
    /// Indicates if the control should perform the operations.
    /// </summary>
    public override bool StopProcessing
    {
        get
        {
            return base.StopProcessing;
        }
        set
        {
            EnsureChildControls();
            base.StopProcessing = value;

            if (treeElemG != null)
            {
                treeElemG.StopProcessing = value;
            }
            if (treeElemP != null)
            {
                treeElemP.StopProcessing = value;
            }
            if (gridDocuments != null)
            {
                gridDocuments.UniGrid.StopProcessing = value;
            }
            if (gridSubCategories != null)
            {
                gridSubCategories.StopProcessing = value;
            }
            if (catEdit != null)
            {
                catEdit.StopProcessing = value;
            }
            if (catNew != null)
            {
                catNew.StopProcessing = value;
            }
        }
    }

    #endregion


    #region "Private properties"

    /// <summary>
    /// ID of currently selected category.
    /// </summary>
    private int SelectedCategoryID
    {
        get
        {
            if (mSelectedCategoryId == -1)
            {
                string[] splits = hidSelectedElem.Value.Split('|');

                if (splits.Length == 2)
                {
                    return ValidationHelper.GetInteger(splits[0], 0);
                }
            }

            return mSelectedCategoryId;
        }
        set
        {
            mSelectedCategoryId = value;
            mSelectedCategory = null;
        }
    }


    /// <summary>
    /// Currently selected category object.
    /// </summary>
    private CategoryInfo SelectedCategory
    {
        get
        {
            if (mSelectedCategory == null)
            {
                mSelectedCategory = CategoryInfoProvider.GetCategoryInfo(SelectedCategoryID);
            }

            return mSelectedCategory;
        }
    }


    /// <summary>
    /// ID of the parent category of selected category.
    /// </summary>
    private int SelectedCategoryParentID
    {
        get
        {
            if (mSelectedParentId == 0)
            {
                string[] splits = hidSelectedElem.Value.Split('|');

                if (splits.Length == 2)
                {
                    return ValidationHelper.GetInteger(splits[1], CATEGORIES_ROOT_PARENT_ID);
                }
                else
                {
                    return DisplaySiteCategories ? CATEGORIES_ROOT_PARENT_ID : PERSONAL_CATEGORIES_ROOT_PARENT_ID;
                }
            }

            return mSelectedParentId;
        }
        set
        {
            mSelectedParentId = value;
        }
    }


    /// <summary>
    /// Indicates whether root category of personal categories is selected.
    /// </summary>
    private bool CustomCategoriesRootSelected
    {
        get
        {
            return SelectedCategoryParentID == PERSONAL_CATEGORIES_ROOT_PARENT_ID;
        }
    }


    /// <summary>
    /// Indicates whether control is in editing mode.
    /// </summary>
    private bool IsEditing
    {
        get
        {
            return ValidationHelper.GetBoolean(ViewState["IsEditing"], false);
        }
        set
        {
            ViewState["IsEditing"] = value;
        }
    }


    /// <summary>
    /// Indicates whether control is in mode of creating a new category.
    /// </summary>
    private bool IsCreating
    {
        get
        {
            return ValidationHelper.GetBoolean(ViewState["IsCreating"], false);
        }
        set
        {
            ViewState["IsCreating"] = value;
        }
    }


    /// <summary>
    /// Indicates whether global categories are allowed for selected site.
    /// </summary>
    private bool AllowGlobalCategories
    {
        get
        {
            if (!mAllowGlobalCategories.HasValue)
            {
                string siteName = SiteInfoProvider.GetSiteName(SiteID);
                mAllowGlobalCategories = SettingsKeyProvider.GetBoolValue(siteName + ".CMSAllowGlobalCategories");
            }

            return mAllowGlobalCategories ?? false;
        }
    }

    #endregion


    #region "Page Events"

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        // Prepare actions texts
        btnNew.Text = GetString("categories.new");
        btnDelete.Text = GetString("categories.delete");
        btnUp.Text = GetString("general.up");
        btnDown.Text = GetString("general.down");

        // Init actions images
        imgNewCategory.AlternateText = btnNew.Text;
        imgDeleteCategory.AlternateText = btnDelete.Text;
        imgMoveUp.AlternateText = btnUp.Text;
        imgMoveDown.AlternateText = btnDown.Text;
        imgNewCategory.ImageUrl = GetImageUrl("Objects/CMS_Category/add.png");
        imgDeleteCategory.ImageUrl = GetImageUrl("Objects/CMS_Category/delete.png");
        imgMoveUp.ImageUrl = GetImageUrl("Objects/CMS_Category/up.png");
        imgMoveDown.ImageUrl = GetImageUrl("Objects/CMS_Category/down.png");

        // Init grids
        gridDocuments.UniGrid.OnBeforeDataReload += new OnBeforeDataReload(UniGrid_OnBeforeDataReload);
        gridDocuments.UniGrid.OnAfterDataReload += new OnAfterDataReload(UniGrid_OnAfterDataReload);
        gridSubCategories.OnBeforeDataReload += new OnBeforeDataReload(gridSubCategories_OnBeforeDataReload);
        gridSubCategories.OnExternalDataBound += new OnExternalDataBoundEventHandler(gridSubCategories_OnExternalDataBound);
        gridSubCategories.OnAction += new OnActionEventHandler(gridSubCategories_OnAction);

        gridDocuments.IsLiveSite = this.IsLiveSite;
        gridSubCategories.IsLiveSite = this.IsLiveSite;

        // Prepare tabs headings
        tabGeneral.HeaderText = GetString("general.general");
        tabDocuments.HeaderText = GetString("Category_Edit.Documents");
        tabCategories.HeaderText = GetString("Development.Categories");

        // Init editing controls
        catNew.OnSaved += new EventHandler(NewCategoryCreated);
        catEdit.OnSaved += new EventHandler(CategoryUpdated);

        catNew.IsLiveSite = this.IsLiveSite;
        catEdit.IsLiveSite = this.IsLiveSite;

        catNew.StopProcessing = !IsCreating;
        catEdit.StopProcessing = !IsEditing;

        // Plant some trees
        treeElemG.OnGetImage += treeElem_OnGetImage;
        treeElemP.OnGetImage += treeElem_OnGetImage;
        treeElemG.OnNodeCreated += treeElem_OnNodeCreated;
        treeElemP.OnNodeCreated += treeElem_OnNodeCreated;

        // Get and store permissions
        canModifyGlobal = CMSContext.CurrentUser.IsAuthorizedPerResource("CMS.Categories", "GlobalModify");
        canModifySite = CMSContext.CurrentUser.IsAuthorizedPerResource("CMS.Categories", "Modify");

        // Init site selector
        SelectSite.DropDownSingleSelect.AutoPostBack = true;
        SelectSite.DropDownSingleSelect.SelectedIndexChanged += new EventHandler(Selector_SelectedIndexChanged);
        if (!URLHelper.IsPostback())
        {
            SelectSite.SiteID = CMSContext.CurrentSiteID;
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        // Register JQuery
        ScriptHelper.RegisterJQuery(Page);

        SelectSite.StopProcessing = !DisplaySiteSelector;
        plcSelectSite.Visible = DisplaySiteSelector;

        bool hasSelected = SelectedCategory != null;

        // Check if selection is valid
        CheckSelection();

        // Stop processing grids, when no category selected
        gridDocuments.UniGrid.StopProcessing = !hasSelected;
        gridDocuments.UniGrid.FilterForm.StopProcessing = !hasSelected;
        gridDocuments.UniGrid.Visible = hasSelected;

        gridSubCategories.StopProcessing = !hasSelected;
        gridSubCategories.FilterForm.StopProcessing = !hasSelected;
        gridSubCategories.Visible = hasSelected;

        if (!StopProcessing)
        {
            if (!URLHelper.IsPostback())
            {
                // Start in mode of creating new category when requested
                if (StartInCreatingMode)
                {
                    SwitchToNew();
                }
            }

            // Use images according to culture
            if (CultureHelper.IsUICultureRTL())
            {
                this.treeElemG.LineImagesFolder = GetImageUrl("RTL/Design/Controls/Tree", false, false);
                this.treeElemP.LineImagesFolder = GetImageUrl("RTL/Design/Controls/Tree", false, false);
            }
            else
            {
                this.treeElemG.LineImagesFolder = GetImageUrl("Design/Controls/Tree", false, false);
                this.treeElemP.LineImagesFolder = GetImageUrl("Design/Controls/Tree", false, false);
            }

            treeElemG.StopProcessing = !DisplaySiteCategories;
            treeElemP.StopProcessing = !DisplayPersonalCategories;

            // Prepare node templates
            treeElemP.SelectedNodeTemplate = treeElemG.SelectedNodeTemplate = "<span id=\"node_##NODECODENAME####NODEID##\" class=\"ContentTreeItem ContentTreeSelectedItem\" onclick=\"SelectNode('##NODECODENAME####NODEID##'); if (NodeSelected) { NodeSelected(##NODEID##, ##PARENTID##); return false;}\">##ICON##<span class=\"Name\">##NODECUSTOMNAME##</span></span>";
            treeElemP.NodeTemplate = treeElemG.NodeTemplate = "<span id=\"node_##NODECODENAME####NODEID##\" class=\"ContentTreeItem\" onclick=\"SelectNode('##NODECODENAME####NODEID##'); if (NodeSelected) { NodeSelected(##NODEID##, ##PARENTID##); return false;}\">##ICON##<span class=\"Name\">##NODECUSTOMNAME##</span></span>";

            // Init tree provider objects
            treeElemG.ProviderObject = CreateTreeProvider(SiteID, 0);
            treeElemP.ProviderObject = CreateTreeProvider(0, UserID);

            // Expand first level by default
            treeElemP.ExpandPath = treeElemG.ExpandPath = "/";

            catNew.CategoryID = 0;
            catNew.AllowCreateOnlyGlobal = SiteID == 0;
            catNew.SiteID = SiteID;
            CategoryInfo categoryObj = SelectedCategory;
            if (categoryObj != null)
            {
                catEdit.UserID = categoryObj.CategoryUserID;
                catEdit.CategoryID = categoryObj.CategoryID;

                catNew.UserID = categoryObj.CategoryUserID;
                catNew.ParentCategoryID = categoryObj.CategoryID;

                gridDocuments.SiteName = filterDocuments.SelectedSite;

                PreselectCategory(categoryObj, false);
            }
            else
            {
                catNew.UserID = CustomCategoriesRootSelected ? UserID : 0;
                catNew.SiteID = CustomCategoriesRootSelected ? 0 : SiteID;
                catNew.ParentCategoryID = 0;
            }

            // Create root node for global and site categories
            string rootIcon = "";
            string rootName = "<span class=\"TreeRoot\">" + GetString("categories.rootcategory") + "</span>";
            string rootText = treeElemG.ReplaceMacros(treeElemG.NodeTemplate, 0, 6, rootName, rootIcon, 0, null, null);

            rootText = rootText.Replace("##NODECUSTOMNAME##", rootName);
            rootText = rootText.Replace("##NODECODENAME##", "CategoriesRoot");
            rootText = rootText.Replace("##PARENTID##", CATEGORIES_ROOT_PARENT_ID.ToString());

            treeElemG.SetRoot(rootText, "NULL", GetImageUrl("Objects/CMS_Category/list.png"), null, null);

            // Create root node for personal categories
            rootName = "<span class=\"TreeRoot\">" + GetString("categories.rootpersonalcategory") + "</span>";
            rootText = "";
            rootText = treeElemP.ReplaceMacros(treeElemP.NodeTemplate, 0, 6, rootName, rootIcon, 0, null, null);

            rootText = rootText.Replace("##NODECUSTOMNAME##", rootName);
            rootText = rootText.Replace("##NODECODENAME##", "PersonalCategoriesRoot");
            rootText = rootText.Replace("##PARENTID##", PERSONAL_CATEGORIES_ROOT_PARENT_ID.ToString());
            treeElemP.SetRoot(rootText, "NULL", GetImageUrl("Objects/CMS_Category/list.png"), null, null);

            // Prepare post abck reerence for selecting nodes and confirmation message
            string postBackRef = ControlsHelper.GetPostBackEventReference(hdnButton, "");
            string script = "var menuHiddenId = '" + hidSelectedElem.ClientID + "';";
            script += "function deleteConfirm() {";
            script += "return confirm(" + ScriptHelper.GetString(GetString("general.confirmdelete")) + ");";
            script += "}";
            script += "function RaiseHiddenPostBack(){" + postBackRef + ";}";

            ltlScript.Text = ScriptHelper.GetScript(script);
        }
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        if (!StopProcessing)
        {
            // Prepare values for selection script
            string categoryName = "";
            int categoryId = 0;
            int categoryParentId = 0;
            if (SelectedCategory != null)
            {
                categoryName = SelectedCategory.CategoryName;
                categoryId = SelectedCategory.CategoryID;
                categoryParentId = SelectedCategory.CategoryParentID;

                // Check if user can manage selected category
                bool canModify = CanModifySelectedCategory();

                // Set enabled state of actions
                SetLinkEnabledState(btnDelete, canModify);
                SetLinkEnabledState(btnUp, canModify);
                SetLinkEnabledState(btnDown, canModify);

                if (!SelectedCategory.CategoryIsPersonal)
                {
                    // Display New button when authorized to modify site categories
                    bool canCreate = canModifySite;

                    // Additionaly check GlobalModify under global categories GlobalModify
                    if (SelectedCategory.CategoryIsGlobal)
                    {
                        canCreate |= canModifyGlobal;
                    }

                    SetLinkEnabledState(btnNew, canCreate);
                }
            }
            else
            {
                categoryParentId = SelectedCategoryParentID;
                categoryName = CustomCategoriesRootSelected ? "PersonalCategoriesRoot" : "CategoriesRoot";

                // Set enabled state of new category button
                bool newEnabled = CustomCategoriesRootSelected ? true : (canModifyGlobal || canModifySite);
                SetLinkEnabledState(btnNew, newEnabled);
            }

            ShowForms();

            // Enable/disable actions
            if (categoryId == 0)
            {
                SetLinkEnabledState(btnDelete, false);
                SetLinkEnabledState(btnUp, false);
                SetLinkEnabledState(btnDown, false);
            }

            pnlUpdateActions.Update();
            ScriptHelper.RegisterStartupScript(Page, typeof(string), "CategorySelectionScript", ScriptHelper.GetScript("SelectNode(" + ScriptHelper.GetString(categoryName + categoryId) + ");"));
            this.hidSelectedElem.Value = categoryId.ToString() + '|' + categoryParentId;

            // Use correct css classes for edit/create mode
            pnlHeader.CssClass = IsEditing ? "PageHeader" : "PageHeader SimpleHeader";

            treeElemG.Visible = DisplaySiteCategories;
            treeElemP.Visible = DisplayPersonalCategories;

            // Reload trees
            treeElemG.ReloadData();
            treeElemP.ReloadData();
        }
    }

    #endregion


    #region "Event handlers"

    protected TreeNode treeElem_OnNodeCreated(DataRow itemData, TreeNode defaultNode)
    {
        defaultNode.Selected = false;
        defaultNode.SelectAction = TreeNodeSelectAction.None;
        defaultNode.NavigateUrl = "";

        if (itemData != null)
        {
            // Ensure name
            string catName = ValidationHelper.GetString(itemData["CategoryName"], "");

            // Ensure caption
            string caption = ValidationHelper.GetString(itemData["CategoryDisplayName"], "");

            // Ensure parent category ID
            int catParentId = ValidationHelper.GetInteger(itemData["CategoryParentID"], 0);

            // Ensure category ID
            int catId = ValidationHelper.GetInteger(itemData["CategoryID"], 0);

            if (String.IsNullOrEmpty(caption))
            {
                caption = catName;
            }

            // Set caption
            defaultNode.Text = defaultNode.Text.Replace("##NODECUSTOMNAME##", HTMLHelper.HTMLEncode(ResHelper.LocalizeString(caption)));
            defaultNode.Text = defaultNode.Text.Replace("##NODECODENAME##", HTMLHelper.HTMLEncode(catName));
            defaultNode.Text = defaultNode.Text.Replace("##PARENTID##", catParentId.ToString());

            return defaultNode;
        }

        return null;
    }


    /// <summary>
    /// Method for obtaining image url for given tree node (category).
    /// </summary>
    /// <param name="node">Node (category)</param>
    protected string treeElem_OnGetImage(UniTreeNode node)
    {
        DataRow dr = node.ItemData as DataRow;
        string imgUrl = string.Empty;

        if (dr != null)
        {
            int userId = ValidationHelper.GetInteger(dr["CategoryUserID"], 0);
            int siteId = ValidationHelper.GetInteger(dr["CategorySiteID"], 0);

            if (userId > 0)
            {
                // Use icon for personal category
                imgUrl = PersonalCategoryIcon;
            }
            else
            {
                if (siteId > 0)
                {
                    // Use icon for site category
                    imgUrl = SiteCategoryIcon;
                }
                else
                {
                    // Use icon for global category
                    imgUrl = GlobalCategoryIcon;
                }
            }
        }
        else
        {
            // Use default icon
            imgUrl = "Objects/CMS_Category/list.png";
        }

        return GetImageUrl(imgUrl);
    }


    /// <summary>
    /// Handles the SiteSelector's selection changed event.
    /// </summary>
    void Selector_SelectedIndexChanged(object sender, EventArgs e)
    {
        // Check if any category selected
        if (SelectedCategory != null)
        {
            bool selectRoot = false;

            // Preselect root category when site category is selected
            if (!SelectedCategory.CategoryIsGlobal)
            {
                selectRoot = true;
            }
            else
            {
                // Select root when global categories are not allowed
                selectRoot = !SelectedCategory.CategoryIsPersonal && !AllowGlobalCategories;
            }

            // Decite whether to select root
            if (selectRoot)
            {
                SelectedCategoryID = 0;
                SelectedCategoryParentID = CATEGORIES_ROOT_PARENT_ID;
                SwitchToInfo();
            }
            else
            {
                SwitchToEdit();
            }
        }
        else
        {
            // Switch to info message when root is selected
            SwitchToInfo();
        }

        // Update trees and content
        pnlUpdateContent.Update();
        pnlUpdateTree.Update();
    }


    /// <summary>
    /// Ensures filtering documents assigned to the selected category.
    /// </summary>
    protected void UniGrid_OnBeforeDataReload()
    {
        string where = "(DocumentID IN (SELECT CMS_DocumentCategory.DocumentID FROM CMS_DocumentCategory WHERE CategoryID = " + SelectedCategoryID + "))";
        where = SqlHelperClass.AddWhereCondition(where, filterDocuments.WhereCondition);
        gridDocuments.UniGrid.WhereCondition = SqlHelperClass.AddWhereCondition(gridDocuments.UniGrid.WhereCondition, where);
    }


    /// <summary>
    /// Ensures filtering ancestor categories.
    /// </summary>
    protected void gridSubCategories_OnBeforeDataReload()
    {
        if (SelectedCategory != null)
        {
            gridSubCategories.WhereCondition = "CategoryParentID = " + SelectedCategory.CategoryID + " AND (ISNULL(CategorySiteID, 0) = " + SelectedCategory.CategorySiteID + " OR ISNULL(CategorySiteID, 0) = " + SiteID + ") AND ISNULL(CategoryUserID, 0) = " + SelectedCategory.CategoryUserID;
        }
    }


    protected object gridSubCategories_OnExternalDataBound(object sender, string sourceName, object parameter)
    {
        switch (sourceName.ToLower())
        {
            case "edit":
            case "delete":
                if (sender is ImageButton)
                {
                    ImageButton button = (ImageButton)sender;
                    DataRowView data = UniGridFunctions.GetDataRowView(button.Parent as DataControlFieldCell);

                    int userId = ValidationHelper.GetInteger(data["CategoryUserID"], 0);
                    int siteId = ValidationHelper.GetInteger(data["CategorySiteID"], 0);

                    // Hide action when can not modify
                    button.Visible = CanModifyCategory(userId > 0, siteId == 0);
                }
                break;
        }

        return parameter;
    }


    /// <summary>
    /// Ensures hiding of document filer.
    /// </summary>
    protected void UniGrid_OnAfterDataReload()
    {
        plcFilter.Visible = gridDocuments.UniGrid.DisplayExternalFilter(filterDocuments.FilterIsSet);
    }


    /// <summary>
    /// Handles sub categories grid actions.
    /// </summary>
    /// <param name="actionName">Action name</param>
    /// <param name="actionArgument">Parameter</param>
    protected void gridSubCategories_OnAction(string actionName, object actionArgument)
    {
        int categoryId = ValidationHelper.GetInteger(actionArgument, 0);

        // Get category
        CategoryInfo categoryObj = CategoryInfoProvider.GetCategoryInfo(categoryId);

        if (categoryObj != null)
        {
            switch (actionName.ToLower())
            {
                case "edit":
                    // Switch to editing mode
                    SwitchToEdit();
                    catEdit.UserID = categoryObj.CategoryUserID;
                    catEdit.CategoryID = categoryObj.CategoryID;
                    catEdit.ReloadData();

                    // Preselect category
                    SelectedCategoryID = categoryObj.CategoryID;
                    PreselectCategory(categoryObj, false);
                    pnlUpdateTree.Update();

                    // Show general tab
                    pnlTabs.SelectedTab = tabGeneral;

                    break;

                case "delete":
                    // Delete the category
                    DeleteCategory(categoryObj);

                    break;
            }
        }
    }


    #region "Actions"

    /// <summary>
    /// Handles selection of category in the tree.
    /// </summary>
    /// <param name="sender">Sender object</param>
    /// <param name="e">Arguments</param>
    protected void hdnButton_Click(object sender, EventArgs e)
    {
        SwitchToEdit();
        catEdit.ReloadData();

        pnlUpdateContent.Update();
    }


    /// <summary>
    /// Handles New category button click.
    /// </summary>
    /// <param name="sender">Sender object</param>
    /// <param name="e">Arguments</param>
    protected void btnNewElem_Click(object sender, EventArgs e)
    {
        SwitchToNew();
        catNew.ReloadData();

        pnlUpdateContent.Update();
    }


    /// <summary>
    /// Handles Delete category button click.
    /// </summary>
    /// <param name="sender">Sender object</param>
    /// <param name="e">Arguments</param>
    protected void btnDeleteElem_Click(object sender, EventArgs e)
    {
        DeleteCategory(SelectedCategory);
    }


    /// <summary>
    /// Handles Move category up button click.
    /// </summary>
    /// <param name="sender">Sender object</param>
    /// <param name="e">Arguments</param>
    protected void btnUpElem_Click(object sender, EventArgs e)
    {
        int catId = SelectedCategoryID;
        if (catId > 0)
        {
            CategoryInfoProvider.MoveCategoryUp(catId);
        }

        pnlUpdateTree.Update();
    }


    /// <summary>
    /// Handles Move actegory down button click.
    /// </summary>
    /// <param name="sender">Sender object</param>
    /// <param name="e">Arguments</param>
    protected void btnDownElem_Click(object sender, EventArgs e)
    {
        int catId = SelectedCategoryID;
        if (catId > 0)
        {
            CategoryInfoProvider.MoveCategoryDown(catId);
        }

        pnlUpdateTree.Update();
    }

    #endregion


    /// <summary>
    /// Invoked after category update.
    /// </summary>
    /// <param name="sender">Sender object</param>
    /// <param name="e">Arguments</param>
    protected void CategoryUpdated(object sender, EventArgs e)
    {
        PreselectCategory(catEdit.Category, false);

        pnlUpdateTree.Update();
    }


    /// <summary>
    /// Invoked after category created.
    /// </summary>
    /// <param name="sender">Sender object</param>
    /// <param name="e">Arguments</param>
    protected void NewCategoryCreated(object sender, EventArgs e)
    {
        // Set created categoy as selected
        SelectedCategoryID = catNew.CategoryID;

        if (catNew.Category != null)
        {
            SwitchToEdit();
            catEdit.UserID = catNew.Category.CategoryUserID;
            catEdit.CategoryID = catNew.Category.CategoryID;
            catEdit.WasSaved = true;
        }

        PreselectCategory(catNew.Category, false);

        // Open general tab after the new category is created
        pnlTabs.SelectedTab = tabGeneral;

        // Reload data
        catEdit.ReloadData();
        gridDocuments.UniGrid.ReloadData();
        gridSubCategories.ReloadData();

        pnlUpdateTree.Update();
    }

    #endregion


    #region "Methods"

    public override void SetValue(string propertyName, object value)
    {
        base.SetValue(propertyName, value);
        switch (propertyName.ToLower())
        {
            case "displaypersonalcategories":
                DisplayPersonalCategories = ValidationHelper.GetBoolean(value, true);
                break;
            case "displaysitecategories":
                DisplaySiteCategories = ValidationHelper.GetBoolean(value, false);
                break;
            case "displaysiteselector":
                DisplaySiteSelector = ValidationHelper.GetBoolean(value, false);
                break;
        }
    }


    /// <summary>
    /// Preselects category in the tree.
    /// </summary>
    /// <param name="categoryObj">Category to be selected.</param>
    /// <param name="expandLast">Indicates, if selected ategori is to be expanded.</param>
    private void PreselectCategory(CategoryInfo categoryObj, bool expandLast)
    {
        if (categoryObj != null)
        {
            // Decide which tree will be affected
            if (categoryObj.CategoryIsPersonal)
            {
                treeElemP.SelectPath = categoryObj.CategoryIDPath;
                treeElemP.SelectedItem = categoryObj.CategoryName;
                treeElemP.ExpandPath = categoryObj.CategoryIDPath + (expandLast ? "/" : "");
            }
            else
            {
                treeElemG.SelectPath = categoryObj.CategoryIDPath;
                treeElemG.SelectedItem = categoryObj.CategoryName;
                treeElemG.ExpandPath = categoryObj.CategoryIDPath + (expandLast ? "/" : "");
            }
        }
    }


    /// <summary>
    /// Creates tree provider object for categories assigned to specified site or user.
    /// </summary>
    /// <param name="siteId">ID of the site.</param>
    /// <param name="userId">ID of the user.</param>
    private UniTreeProvider CreateTreeProvider(int siteId, int userId)
    {
        // Create and set category provider
        UniTreeProvider provider = new UniTreeProvider();
        provider.UseCustomRoots = true;
        provider.RootLevelOffset = -1;
        provider.ObjectType = "cms.category";
        provider.DisplayNameColumn = "CategoryDisplayName";
        provider.IDColumn = "CategoryID";
        provider.LevelColumn = "CategoryLevel";
        provider.OrderColumn = "CategoryOrder";
        provider.ParentIDColumn = "CategoryParentID";
        provider.PathColumn = "CategoryIDPath";
        provider.ValueColumn = "CategoryID";
        provider.ChildCountColumn = "CategoryChildCount";

        // Prepare the parameters
        provider.Parameters = new QueryDataParameters();
        provider.Parameters.Add("SiteID", siteId);
        provider.Parameters.Add("IncludeGlobal", AllowGlobalCategories);
        provider.Parameters.Add("UserID", userId);

        provider.Columns = "CategoryID, CategoryName, CategoryDisplayName, CategoryLevel, CategoryOrder, CategoryParentID, CategoryIDPath, CategoryUserID, CategorySiteID, (SELECT COUNT(C.CategoryID) FROM CMS_Category AS C WHERE (C.CategoryParentID = CMS_Category.CategoryID) AND (ISNULL(C.CategorySiteID, 0) = @SiteID OR (C.CategorySiteID IS NULL AND @IncludeGlobal = 1)) AND (ISNULL(C.CategoryUserID, 0) = @UserID)) AS CategoryChildCount";
        provider.OrderBy = "CategoryUserID, CategorySiteID, CategoryOrder";
        provider.WhereCondition = "ISNULL(CategoryUserID, 0) = " + userId + " AND (ISNULL(CategorySiteID, 0) = " + siteId;
        if (AllowGlobalCategories && (siteId > 0))
        {
            provider.WhereCondition += " OR CategorySiteID IS NULL";
        }
        provider.WhereCondition += ")";

        return provider;
    }


    /// <summary>
    /// Switches control to editing mode.
    /// </summary>
    private void SwitchToEdit()
    {
        IsEditing = true;
        IsCreating = false;

        catEdit.StopProcessing = false;
        catEdit.ReloadData(true);

        gridDocuments.UniGrid.StopProcessing = false;
        gridDocuments.UniGrid.FilterForm.StopProcessing = false;
        gridDocuments.UniGrid.Visible = true;

        gridSubCategories.StopProcessing = false;
        gridSubCategories.FilterForm.StopProcessing = false;
        gridSubCategories.Visible = true;

        pnlUpdateContent.Update();
    }


    /// <summary>
    /// Switches control to creating mode.
    /// </summary>
    private void SwitchToNew()
    {
        IsCreating = true;
        IsEditing = false;

        catNew.StopProcessing = false;
        catNew.ReloadData(true);
    }

    /// <summary>
    /// Switches control to show information.
    /// </summary>
    private void SwitchToInfo()
    {
        IsCreating = false;
        IsEditing = false;
    }


    /// <summary>
    /// Shows forms acording to mode of control and initializes breadcrumbs.
    /// </summary>
    private void ShowForms()
    {
        plcNew.Visible = IsCreating;
        plcEdit.Visible = IsEditing && SelectedCategory != null;
        plcInfo.Visible = !plcNew.Visible && !plcEdit.Visible;

        if (plcNew.Visible || plcEdit.Visible)
        {
            string[] idSplits = { };

            // Figure out breadcrumbs count
            int breadcrumbsCount = plcNew.Visible ? 2 : 1;
            if (SelectedCategory != null)
            {
                idSplits = SelectedCategory.CategoryIDPath.Trim('/').Split('/');
                breadcrumbsCount += idSplits.Length;
            }

            // Init breadcrumbs
            string[,] breadcrumbs = new string[breadcrumbsCount, 4];
            int bi = 0;

            // Prepare root item
            if (CustomCategoriesRootSelected || ((SelectedCategory != null) && (SelectedCategory.CategoryUserID > 0)))
            {
                breadcrumbs[bi, 0] = GetString("categories.rootpersonalcategory");
                breadcrumbs[bi, 3] = "SelectNode('PersonalCategoriesRoot'); if (NodeSelected) { NodeSelected(0, -2);} return false;";
            }
            else
            {
                breadcrumbs[bi, 0] = GetString("categories.rootcategory");
                breadcrumbs[bi, 3] = "SelectNode('CategoriesRoot'); if (NodeSelected) { NodeSelected(0, -1);} return false;";
            }
            breadcrumbs[bi, 1] = " ";
            breadcrumbs[bi, 2] = "";
            bi++;

            // Create bradcrumbs for whole path
            if (SelectedCategory != null)
            {
                CategoryInfo currentCategory = null;
                int[] ids = ValidationHelper.GetIntegers(idSplits, 0);

                foreach (int id in ids)
                {
                    currentCategory = CategoryInfoProvider.GetCategoryInfo(id);
                    if ((currentCategory != null) && (bi < breadcrumbsCount))
                    {
                        breadcrumbs[bi, 0] = ResHelper.LocalizeString(currentCategory.CategoryDisplayName);
                        breadcrumbs[bi, 1] = " ";
                        breadcrumbs[bi, 2] = "";
                        breadcrumbs[bi, 3] = GetCategorySelectionScript(currentCategory);
                        bi++;
                    }
                }
            }

            // Add new category item
            if (plcNew.Visible)
            {
                breadcrumbs[bi, 0] = GetString("categories.new");
                breadcrumbs[bi, 1] = "";
                breadcrumbs[bi, 2] = "";
            }

            titleElem.Breadcrumbs = breadcrumbs;
        }

        // Display title when creating a new category
        if (plcNew.Visible)
        {
            titleElem.TitleText = GetString("categories.new");
            titleElem.TitleImage = GetImageUrl("Objects/CMS_Category/new.png");
        }
    }


    private string GetCategorySelectionScript(CategoryInfo category)
    {
        string script = string.Empty;
        if (category != null)
        {
            script = string.Format("SelectNode('{0}{1}'); if (NodeSelected) {{ NodeSelected({1}, {2});}} return false;", category.CategoryName, category.CategoryID, category.CategoryParentID);
        }

        return script;
    }


    /// <summary>
    /// Returns true if current user can modify selected category.
    /// </summary>
    private bool CanModifySelectedCategory()
    {
        if (SelectedCategory != null)
        {
            return CanModifyCategory(SelectedCategory.CategoryIsPersonal, SelectedCategory.CategoryIsGlobal);
        }

        return false;
    }


    /// <summary>
    /// Returns true if current user can modify given category.
    /// </summary>
    private bool CanModifyCategory(bool personal, bool global)
    {
        if (!personal)
        {
            return global ? canModifyGlobal : canModifySite;
        }

        // Personal categories can be modified.
        return true;
    }


    /// <summary>
    /// Checks whether category belongs to current site, current user and whether it is allowed (in case of global category).
    /// </summary>
    private void CheckSelection()
    {
        if (CMSContext.CurrentUser.IsGlobalAdministrator)
        {
            return;
        }

        bool valid = true;

        if (SelectedCategory != null)
        {
            if (SelectedCategory.CategoryIsPersonal)
            {
                // Can not access personal categories from another user
                valid = (SelectedCategory.CategoryUserID == UserID);
            }
            else
            {
                // Global categories have to be allowed
                if (SelectedCategory.CategoryIsGlobal)
                {
                    valid = AllowGlobalCategories;
                }
                else
                {
                    // Site categories have to belong to selected site
                    valid = (SelectedCategory.CategorySiteID == SiteID);
                }
            }
        }

        // Select root when invalid
        if (!valid)
        {
            SelectedCategoryID = 0;
            SelectedCategoryParentID = DisplaySiteCategories ? CATEGORIES_ROOT_PARENT_ID : PERSONAL_CATEGORIES_ROOT_PARENT_ID;
            SwitchToInfo();
        }
    }


    private void SetLinkEnabledState(LinkButton link, bool enabled)
    {
        if (!enabled)
        {
            link.PostBackUrl = "javascript:void(0);";
            link.OnClientClick = "return false;";
            link.CssClass = "MenuItemDisabled";
        }
    }


    private void DeleteCategory(CategoryInfo categoryObj)
    {
        // Check if category
        if ((categoryObj != null) && CanModifyCategory(categoryObj.CategoryIsPersonal, categoryObj.CategoryIsGlobal))
        {
            CategoryInfo parentCategory = CategoryInfoProvider.GetCategoryInfo(categoryObj.CategoryParentID);

            // Check if deleted category has parent
            if (parentCategory != null)
            {
                // Switch to editing of parent category
                SwitchToEdit();
                catEdit.UserID = parentCategory.CategoryUserID;
                catEdit.CategoryID = parentCategory.CategoryID;
                catEdit.ReloadData();

                SelectedCategoryID = parentCategory.CategoryID;
                PreselectCategory(parentCategory, false);
            }
            else
            {
                SelectedCategoryID = 0;
                SelectedCategoryParentID = categoryObj.CategoryIsPersonal ? PERSONAL_CATEGORIES_ROOT_PARENT_ID : CATEGORIES_ROOT_PARENT_ID;
                SwitchToInfo();
            }

            // Delete category
            CategoryInfoProvider.DeleteCategoryInfo(categoryObj);

            pnlUpdateTree.Update();
            pnlUpdateContent.Update();
        }
    }

    #endregion
}