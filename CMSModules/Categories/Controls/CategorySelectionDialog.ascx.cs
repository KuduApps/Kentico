using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using CMS.UIControls;
using CMS.GlobalHelper;
using CMS.SettingsProvider;
using CMS.CMSHelper;
using CMS.Controls;
using CMS.SiteProvider;
using CMS.ExtendedControls;

public partial class CMSModules_Categories_Controls_CategorySelectionDialog : CMSUserControl
{
    #region "Variables and constants"

    private SelectionModeEnum selectionMode = SelectionModeEnum.SingleButton;
    private string resourcePrefix = "general";
    private string valuesSeparator = ";";

    private bool allowMultiple = false;
    private bool? mAllowGlobalCategories = null;

    private string whereCondition = null;
    private string orderBy = null;

    private string txtClientId = null;
    private string hdnClientId = null;
    private string hdnDrpId = null;

    private string parentClientId = null;
    private string callbackMethod = null;

    private bool allowEditTextBox = false;
    private bool fireOnChanged = false;

    private string returnColumnName = "CategoryID";
    private string disabledItems = "";

    private readonly TextHelper th = new TextHelper();
    private Hashtable parameters = null;

    private int mSelectedCategoryId = -1;
    private CategoryInfo mSelectedCategory = null;
    private int mSelectedParentId = 0;

    private string mGlobalCategoryIcon = "Objects/CMS_Category/global.png";
    private string mSiteCategoryIcon = "Objects/CMS_Category/category.png";
    private string mPersonalCategoryIcon = "Objects/CMS_Category/category.png";

    private const int CATEGORIES_ROOT_PARENT_ID = -1;
    private const int PERSONAL_CATEGORIES_ROOT_PARENT_ID = -2;

    private bool canModifySite = false;
    private bool canModifyGlobal = false;

    #endregion


    #region "Properties"

    /// <summary>
    /// ID of the user to manage categories for. Default value is ID of the current user.
    /// </summary>
    public int UserID
    {
        get
        {
            if (CMSContext.CurrentUser != null)
            {
                return CMSContext.CurrentUser.UserID;
            }

            return 0;
        }
    }


    /// <summary>
    /// Indicates if control is used on live site.
    /// </summary>
    public override bool IsLiveSite
    {
        get
        {
            return base.IsLiveSite;
        }
        set
        {
            treeElemG.IsLiveSite = value;
            treeElemP.IsLiveSite = value;

            base.IsLiveSite = value;
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
    /// ID of the selected category.
    /// </summary>
    public int SelectedCategoryID
    {
        get
        {
            if (mSelectedCategoryId == -1)
            {
                string[] splits = hidSelectedElem.Value.Split('|');

                if (splits.Length == 2)
                {
                    return ValidationHelper.GetInteger(splits[0], -1);
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
    /// Selected category object.
    /// </summary>
    public CategoryInfo SelectedCategory
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
    public int SelectedCategoryParentID
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

                return CATEGORIES_ROOT_PARENT_ID;
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
    public bool PersonalCategoriesRootSelected
    {
        get
        {
            return SelectedCategoryParentID == PERSONAL_CATEGORIES_ROOT_PARENT_ID;
        }
    }


    /// <summary>
    /// Indicates whether root category of site/global categories is selected.
    /// </summary>
    public bool CategoriesRootSelected
    {
        get
        {
            return SelectedCategoryParentID == CATEGORIES_ROOT_PARENT_ID;
        }
    }


    /// <summary>
    /// Indicates if current user can modify global categories.
    /// </summary>
    public bool CanModifyGlobalCategories
    {
        get
        {
            return canModifyGlobal;
        }
    }


    /// <summary>
    /// Indicates if current user can modify site categories.
    /// </summary>
    public bool CanModifySiteCategories
    {
        get
        {
            return canModifySite;
        }
    }


    /// <summary>
    /// Indicates whether global categories are allowed for selected site.
    /// </summary>
    public bool AllowGlobalCategories
    {
        get
        {
            if (!mAllowGlobalCategories.HasValue)
            {
                mAllowGlobalCategories = SettingsKeyProvider.GetBoolValue(CMSContext.CurrentSiteName + ".CMSAllowGlobalCategories");
            }

            return mAllowGlobalCategories ?? false;
        }
    }

    #endregion


    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        // Init trees handlers
        treeElemG.OnGetImage += treeElem_OnGetImage;
        treeElemP.OnGetImage += treeElem_OnGetImage;
        treeElemG.OnNodeCreated += treeElem_OnNodeCreated;
        treeElemP.OnNodeCreated += treeElem_OnNodeCreated;

        // Load parameters
        LoadParameters();

        // Stop personal category tree when disabled
        if (!string.IsNullOrEmpty(disabledItems))
        {
            if (disabledItems.ToLower().Contains("personal"))
            {
                treeElemP.StopProcessing = true;
            }
            if (disabledItems.ToLower().Contains("globalandsite"))
            {
                treeElemG.StopProcessing = true;
            }
        }

        // Get and store permissions
        canModifyGlobal = AllowGlobalCategories && CMSContext.CurrentUser.IsAuthorizedPerResource("CMS.Categories", "GlobalModify");
        canModifySite = CMSContext.CurrentUser.IsAuthorizedPerResource("CMS.Categories", "Modify");

        // Expand and preselect selected category when in single select mode
        if (!RequestHelper.IsPostBack() && !allowMultiple)
        {
            string value = hidItem.Value;
            if (!string.IsNullOrEmpty(value))
            {
                string[] values = value.Split(new string[] { valuesSeparator }, StringSplitOptions.RemoveEmptyEntries);
                if (values.Length == 1)
                {
                    int catId = 0;

                    if (returnColumnName == "CategoryID")
                    {
                        catId = ValidationHelper.GetInteger(values[0], 0);
                    }
                    else
                    {
                        CategoryInfo cat = CategoryInfoProvider.GetCategoryInfo(values[0], CMSContext.CurrentSiteName);
                        catId = (cat != null) ? cat.CategoryID : 0;
                    }

                    if (catId > 0)
                    {
                        // Select category
                        SelectedCategoryID = catId;
                    }
                }
            }
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        // Get control IDs from parent window
        txtClientId = QueryHelper.GetString("txtElem", string.Empty);
        hdnClientId = QueryHelper.GetString("hidElem", string.Empty);
        hdnDrpId = QueryHelper.GetString("selectElem", string.Empty);
        parentClientId = QueryHelper.GetString("clientId", string.Empty);

        // Check if selection is valid
        CheckSelection();

        // Buttons scripts
        string buttonsScript = "function US_Cancel(){ Cancel();  return false; }";

        switch (selectionMode)
        {
            // Button modes
            case SelectionModeEnum.SingleButton:
            case SelectionModeEnum.MultipleButton:
                buttonsScript += "function US_Submit(){ SelectItems(ItemsElem().value); return false; }";
                break;

            // Textbox modes
            case SelectionModeEnum.SingleTextBox:
            case SelectionModeEnum.MultipleTextBox:
                if (allowEditTextBox)
                {
                    buttonsScript += "function US_Submit(){ SelectItems(ItemsElem().value, ItemsElem().value.replace(/^" + valuesSeparator + "+|" + valuesSeparator + "+$/g, ''), " + ScriptHelper.GetString(hdnClientId) + ", " + ScriptHelper.GetString(txtClientId) + ", " + ScriptHelper.GetString(hdnDrpId) + "); return false; }";
                }
                else
                {
                    buttonsScript += "function US_Submit(){ SelectItemsReload(ItemsElem().value, nameElem.value, " + ScriptHelper.GetString(hdnClientId) + ", " + ScriptHelper.GetString(txtClientId) + ", " + ScriptHelper.GetString(hdnDrpId) + "); return false; }";
                }
                break;

            // Other modes
            default:
                buttonsScript += "function US_Submit(){ SelectItemsReload(ItemsElem().value, nameElem.value, " + ScriptHelper.GetString(hdnClientId) + ", " + ScriptHelper.GetString(txtClientId) + ", " + ScriptHelper.GetString(hdnDrpId) + "); return false; }";
                break;
        }

        string script = null;

        switch (selectionMode)
        {
            // Button modes
            case SelectionModeEnum.SingleButton:
            case SelectionModeEnum.MultipleButton:
                {
                    // Register javascript code
                    if (callbackMethod == null)
                    {
                        script = "function SelectItems(items) { wopener.US_SelectItems_" + parentClientId + "(items); window.close(); }";
                    }
                    else
                    {
                        script = "function SelectItems(items) { wopener." + callbackMethod + "(items.replace(/^;+|;+$/g, '')); window.close(); }";
                    }
                }
                break;

            // Selector modes
            default:
                {
                    // Register javascript code
                    script =
                        @"function SelectItems(items, names, hiddenFieldId, txtClientId) { 
                            if(items.length > 0) { 
                                wopener.US_SetItems(items, names, hiddenFieldId, txtClientId); 
                            } else {
                                wopener.US_SetItems('','', hiddenFieldId, txtClientId, null); 
                            }" +
                            (fireOnChanged ? "wopener.US_SelectionChanged_" + parentClientId + "();" : "")
                            + @"window.close(); 
                        }

                        function SelectItemsReload(items, names, hiddenFieldId, txtClientId, hidValue) {
                            if (items.length > 0) { 
                                wopener.US_SetItems(items, names, hiddenFieldId, txtClientId, hidValue); 
                            } else {
                                wopener.US_SetItems('','', hiddenFieldId, txtClientId, hidValue); 
                            }
                            window.close();
                            wopener.US_ReloadPage_" + parentClientId + @"(); 
                            return false; 
                        }";
                }
                break;
        }

        script += @"
            var nameElem = document.getElementById('" + hidName.ClientID + @"');
            
            function ItemsElem()
            {
                return document.getElementById('" + hidItem.ClientID + @"');
            }

            function Refresh(param) {
                var parameElem = $j('#' + paramElemId);
                if (parameElem.length) {
                    parameElem[0].value = param;
                }
                RaiseHiddenPostBack();
            }

function disableParents(id, disable)
{
  while(id > 0)
  {
      var chkbox = $j('#chk'+id);
      id = 0;
      if(chkbox.length)
      {
        var continueToParent=true;
        var parentId = 0;
        var nameSplits = chkbox[0].name.split('_');
        if(nameSplits.length == 2)
        {
           parentId=nameSplits[1];
           if(!disable)
           {
             var siblings = $j('input[name$=\'_'+parentId+'\']:checked');
             continueToParent = (siblings.length === 0);
           }
        }
        if(continueToParent)
        {
            var parentChkbox = $j('#chk'+parentId);
            if(parentChkbox.length)
            {
              if(disable)
              {
                parentChkbox.attr('disabled', 'disabled');
              } else {
                parentChkbox.removeAttr('disabled');
              }

              if (ItemsElem().value.toLowerCase().indexOf('" + valuesSeparator + @"' + parentId + '" + valuesSeparator + @"') < 0)
              {
                parentChkbox[0].checked = disable;
              } else {
                parentChkbox[0].checked = true;
              }
      
              id = parentId;
            }
        }
      }
  }
}

            function ProcessItem(chkbox, changeChecked) {   
                if (chkbox != null) {
                    var itemsElem = ItemsElem();
                    var items = itemsElem.value; 
                    var item = chkbox.id.substr(3);

                    if (changeChecked)
                    {
                        chkbox.checked = !chkbox.checked;
                    }
                    if (chkbox.checked)
                    {
                        if (items == '')
                        {
                            itemsElem.value = '" + valuesSeparator + @"' + escape(item) + '" + valuesSeparator + @"';
                        }
                        else if (items.toLowerCase().indexOf('" + valuesSeparator + @"' + escape(item).toLowerCase() + '" + valuesSeparator + @"') < 0)
                        {
                            itemsElem.value += escape(item) + '" + valuesSeparator + @"';
                        }
disableParents(item, true);
                    }
                    else
                    {
                        var re = new RegExp('" + valuesSeparator + "' + escape(item) + '" + valuesSeparator + @"', 'i');
                        itemsElem.value = items.replace(re, '" + valuesSeparator + @"');    
disableParents(item, false);
                    }
                }
            }
            
            function Cancel() { window.close(); }

            function SelectAllItems(checkbox)
            {
                var checkboxes = document.getElementsByTagName('input');
                for(var i = 0; i < checkboxes.length; i++)
                {
                    var chkbox = checkboxes[i];
                    if (chkbox.className == 'chckbox')
                    {
                        if(checkbox.checked) { chkbox.checked = true; }
                        else { chkbox.checked = false; }

                        ProcessItem(chkbox);
                    }
                }
            }";

        ltlScript.Text = ScriptHelper.GetScript(script + buttonsScript);

        bool rtl = IsLiveSite ? CultureHelper.IsPreferredCultureRTL() : CultureHelper.IsUICultureRTL();

        // Use images according to culture
        if (rtl)
        {
            this.treeElemG.LineImagesFolder = GetImageUrl("RTL/Design/Controls/Tree", false, false);
            this.treeElemP.LineImagesFolder = GetImageUrl("RTL/Design/Controls/Tree", false, false);
        }
        else
        {
            this.treeElemG.LineImagesFolder = GetImageUrl("Design/Controls/Tree", false, false);
            this.treeElemP.LineImagesFolder = GetImageUrl("Design/Controls/Tree", false, false);
        }

        // Prepare node templates
        treeElemP.SelectedNodeTemplate = treeElemG.SelectedNodeTemplate = "<span id=\"node_##NODECODENAME####NODEID##\" name=\"treeNode\" class=\"ContentTreeItem ContentTreeSelectedItem\" onclick=\"SelectNode('##NODECODENAME####NODEID##'); if (NodeSelected) { NodeSelected(##NODEID##, ##PARENTID##); ##ONCLICK## return false;}\">##ICON##<span class=\"Name\">##NODECUSTOMNAME##</span></span>";
        treeElemP.NodeTemplate = treeElemG.NodeTemplate = "<span id=\"node_##NODECODENAME####NODEID##\" name=\"treeNode\" class=\"ContentTreeItem\" onclick=\"SelectNode('##NODECODENAME####NODEID##'); if (NodeSelected) { NodeSelected(##NODEID##, ##PARENTID##); ##ONCLICK## return false;}\">##ICON##<span class=\"Name\">##NODECUSTOMNAME##</span></span>";

        treeElemP.UsePostBack = treeElemG.UsePostBack = false;
        treeElemG.ProviderObject = CreateTreeProvider(CMSContext.CurrentSiteID, 0);
        treeElemP.ProviderObject = CreateTreeProvider(0, UserID);

        treeElemP.ExpandPath = treeElemG.ExpandPath = "/";
        CategoryInfo categoryObj = SelectedCategory;
        if (categoryObj != null)
        {
            PreselectCategory(categoryObj);
        }

        // Create root node for global and site categories
        string rootIcon = "";
        string rootName = "<span class=\"TreeRoot\">" + GetString("categories.rootcategory") + "</span>";
        string rootText = treeElemG.ReplaceMacros(treeElemG.NodeTemplate, 0, 6, rootName, rootIcon, 0, null, null);

        rootText = rootText.Replace("##NODECUSTOMNAME##", rootName);
        rootText = rootText.Replace("##NODECODENAME##", "CategoriesRoot");
        rootText = rootText.Replace("##PARENTID##", CATEGORIES_ROOT_PARENT_ID.ToString());
        rootText = rootText.Replace("##ONCLICK##", "");

        treeElemG.SetRoot(rootText, "NULL", GetImageUrl("Objects/CMS_Category/list.png"), URLHelper.Url + "#", null);

        // Create root node for personal categories
        rootName = "<span class=\"TreeRoot\">" + GetString("categories.rootpersonalcategory") + "</span>";
        rootText = "";
        rootText = treeElemP.ReplaceMacros(treeElemP.NodeTemplate, 0, 6, rootName, rootIcon, 0, null, null);

        rootText = rootText.Replace("##NODECUSTOMNAME##", rootName);
        rootText = rootText.Replace("##NODECODENAME##", "PersonalCategoriesRoot");
        rootText = rootText.Replace("##PARENTID##", PERSONAL_CATEGORIES_ROOT_PARENT_ID.ToString());
        rootText = rootText.Replace("##ONCLICK##", "");

        treeElemP.SetRoot(rootText, "NULL", GetImageUrl("Objects/CMS_Category/list.png"), URLHelper.Url + "#", null);

        // Prepare postback reference
        string postBackRef = ControlsHelper.GetPostBackEventReference(hdnButton, "");
        string treeScript = "var menuHiddenId = '" + hidSelectedElem.ClientID + "';";
        treeScript += "var paramElemId = '" + hidParam.ClientID + "';";
        // Prepare delete confirmation
        treeScript += "function deleteConfirm() {";
        treeScript += "return confirm(" + ScriptHelper.GetString(GetString("general.confirmdelete")) + ");";
        treeScript += "}";
        treeScript += "function RaiseHiddenPostBack(){" + postBackRef + ";}";

        ltlTreeScript.Text = ScriptHelper.GetScript(treeScript);
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        CategoryInfo categoryObj = SelectedCategory;

        string categoryName = "";
        int categoryId = 0;
        int categoryParentId = 0;
        // Mark selected category
        if (categoryObj != null)
        {
            categoryName = categoryObj.CategoryName;
            categoryId = categoryObj.CategoryID;
            categoryParentId = categoryObj.CategoryParentID;
        }
        else
        {
            // Mark root when no category selected
            categoryParentId = SelectedCategoryParentID;
            categoryName = PersonalCategoriesRootSelected ? "PersonalCategoriesRoot" : "CategoriesRoot";
        }

        ScriptHelper.RegisterStartupScript(Page, typeof(string), "CategorySelectionScript", ScriptHelper.GetScript("SelectNode(" + ScriptHelper.GetString(categoryName + categoryId) + ");"));
        this.hidSelectedElem.Value = categoryId.ToString() + '|' + categoryParentId;

        // Reload trees
        treeElemG.ReloadData();
        treeElemP.ReloadData();
    }


    protected TreeNode treeElem_OnNodeCreated(DataRow itemData, TreeNode defaultNode)
    {
        defaultNode.Selected = false;
        if (itemData != null)
        {
            // Ensure name
            string catName = ValidationHelper.GetString(itemData["CategoryName"], "");
            // Ensure caption
            string caption = ValidationHelper.GetString(itemData["CategoryDisplayName"], "");
            // Get parent category ID
            int catParentId = ValidationHelper.GetInteger(itemData["CategoryParentID"], 0);
            // Get category ID
            int catId = ValidationHelper.GetInteger(itemData["CategoryID"], 0);
            // Get count of child categories
            bool catHasCheckedChilds = ValidationHelper.GetInteger(itemData["ChildChecked"], 0) > 0;

            if (String.IsNullOrEmpty(caption))
            {
                caption = catName;
            }

            // Set caption
            defaultNode.Text = defaultNode.Text.Replace("##NODECUSTOMNAME##", HTMLHelper.HTMLEncode(ResHelper.LocalizeString(caption)));
            defaultNode.Text = defaultNode.Text.Replace("##NODECODENAME##", HTMLHelper.HTMLEncode(catName));
            defaultNode.Text = defaultNode.Text.Replace("##PARENTID##", catParentId.ToString());

            string onclick = "";
            string checkBox = "";
            if (allowMultiple)
            {
                // Prepare checbox when in multiple selection mode
                checkBox = "<input id=\"chk" + catId + "\" type=\"checkbox\" onclick=\"ProcessItem(this);\" class=\"chckbox\" ";
                if (catHasCheckedChilds || (hidItem.Value.IndexOf(valuesSeparator + catId + valuesSeparator, StringComparison.CurrentCultureIgnoreCase) >= 0))
                {
                    checkBox += "checked=\"checked\" ";
                }
                if (catHasCheckedChilds)
                {
                    checkBox += "disabled=\"disabled\" ";
                }
                checkBox += "name=\"" + catId + "_" + catParentId + "\"";
                checkBox += "/>";
            }
            else
            {
                if (returnColumnName == "CategoryID")
                {
                    onclick = "ItemsElem().value = '" + valuesSeparator + catId + valuesSeparator + "';";
                }
                else
                {
                    onclick = "ItemsElem().value = '" + valuesSeparator + ScriptHelper.GetString(catName, false) + valuesSeparator + "';";
                }
            }

            defaultNode.Text = defaultNode.Text.Replace("##ONCLICK##", onclick);
            defaultNode.Text = checkBox + defaultNode.Text;

            // Expand selected categories
            if (catHasCheckedChilds && !URLHelper.IsPostback())
            {
                defaultNode.Expand();
            }

            return defaultNode;
        }

        return null;
    }


    /// <summary>
    /// Method for obtaining image url for given tree node (category).
    /// </summary>
    /// <param name="node">Node (category)</param>
    /// <returns>Image URL</returns>
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

        return URLHelper.ResolveUrl(UIHelper.GetImagePath(Page, imgUrl, false, false));
    }


    /// <summary>
    /// Handles actions risen from javascript.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void hdnButton_Click(object sender, EventArgs e)
    {
        string param = hidParam.Value;

        // Check if action was saving of existing category
        if (param.StartsWith("edit"))
        {
            // Check if category was disabled during editing
            if ((SelectedCategory != null) && !SelectedCategory.CategoryEnabled)
            {
                // Select coresponding root element
                bool personal = SelectedCategory.CategoryIsPersonal;
                SelectedCategoryID = 0;
                SelectedCategoryParentID = personal ? PERSONAL_CATEGORIES_ROOT_PARENT_ID : CATEGORIES_ROOT_PARENT_ID;
            }

            pnlUpdateTrees.Update();
        }
        // Check if action was creation of new category
        else if (param.StartsWith("new"))
        {
            string[] splits = param.Split('|');
            if (splits.Length == 2)
            {
                int id = ValidationHelper.GetInteger(splits[1], 0);
                if (id > 0)
                {
                    // Select created category
                    CategoryInfo category = CategoryInfoProvider.GetCategoryInfo(id);
                    if (category != null)
                    {
                        SelectedCategoryID = category.CategoryID;
                        PreselectCategory(category, false);

                        if (!allowMultiple)
                        {
                            if (returnColumnName == "CategoryID")
                            {
                                hidItem.Value = valuesSeparator + id + valuesSeparator;
                            }
                            else
                            {
                                hidItem.Value = valuesSeparator + ScriptHelper.GetString(category.CategoryName, false) + valuesSeparator;
                            }
                            pnlHidden.Update();
                        }
                    }
                }
            }

            pnlUpdateTrees.Update();
        }

        // Remove parameter
        hidParam.Value = "";
    }


    /// <summary>
    /// Handles request for deleting category.
    /// </summary>
    /// <param name="sender">Sender object.</param>
    /// <param name="e">Arguments.</param>
    public void lnkDelete_Click(object sender, EventArgs e)
    {
        CategoryInfo categoryObj = SelectedCategory;

        if ((categoryObj != null) && CanModifySelectedCategory)
        {
            // Remove deleted category from selection
            if (!string.IsNullOrEmpty(hidItem.Value))
            {
                hidItem.Value = hidItem.Value.Replace(valuesSeparator + categoryObj.CategoryID + valuesSeparator, valuesSeparator);
                pnlHidden.Update();
            }

            // Preselect parent category
            CategoryInfo parentCategory = CategoryInfoProvider.GetCategoryInfo(categoryObj.CategoryParentID);
            if (parentCategory != null)
            {
                SelectedCategoryID = parentCategory.CategoryID;
                PreselectCategory(parentCategory, true);
            }
            else
            {
                SelectedCategoryID = 0;
                SelectedCategoryParentID = categoryObj.CategoryIsPersonal ? PERSONAL_CATEGORIES_ROOT_PARENT_ID : CATEGORIES_ROOT_PARENT_ID;
            }

            // Delete category
            CategoryInfoProvider.DeleteCategoryInfo(categoryObj);

            pnlUpdateTrees.Update();
        }
    }


    /// <summary>
    /// Handles request for moving category up.
    /// </summary>
    /// <param name="sender">Sender object.</param>
    /// <param name="e">Arguments.</param>
    public void lnkUp_Click(object sender, EventArgs e)
    {
        // Move selected category up
        int catId = SelectedCategoryID;
        if ((catId > 0) && CanModifySelectedCategory)
        {
            CategoryInfoProvider.MoveCategoryUp(catId);
        }

        pnlUpdateTrees.Update();
    }


    /// <summary>
    /// Handles request for moving category down.
    /// </summary>
    /// <param name="sender">Sender object.</param>
    /// <param name="e">Arguments.</param>
    public void lnkDown_Click(object sender, EventArgs e)
    {
        // Move selected category down
        int catId = SelectedCategoryID;
        if ((catId > 0) && CanModifySelectedCategory)
        {
            CategoryInfoProvider.MoveCategoryDown(catId);
        }

        pnlUpdateTrees.Update();
    }


    /// <summary>
    /// Handles request for expanding trees.
    /// </summary>
    /// <param name="sender">Sender object.</param>
    /// <param name="e">Arguments.</param>
    public void lnkExpandAll_Click(object sender, EventArgs e)
    {
        // Expand trees
        treeElemG.ExpandAll = true;
        treeElemP.ExpandAll = true;
        pnlUpdateTrees.Update();
    }


    /// <summary>
    /// Handles request for collapsing trees.
    /// </summary>
    /// <param name="sender">Sender object.</param>
    /// <param name="e">Arguments.</param>
    public void lnkCollapseAll_Click(object sender, EventArgs e)
    {
        // Collapse trees
        treeElemG.ExpandAll = false;
        treeElemP.ExpandAll = false;
        pnlUpdateTrees.Update();
    }


    /// <summary>
    /// Preselects category without expanding it.
    /// </summary>
    /// <param name="categoryObj">Category to be selected.</param>
    private void PreselectCategory(CategoryInfo categoryObj)
    {
        PreselectCategory(categoryObj, false);
    }


    /// <summary>
    /// Preselects category allowing to expand its childs.
    /// </summary>
    /// <param name="categoryObj">Category to be selected.</param>
    /// <param name="expandLast">When true, childs of selected category will be expanded.</param>
    private void PreselectCategory(CategoryInfo categoryObj, bool expandLast)
    {
        if (categoryObj != null)
        {
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
    /// Loads control parameters.
    /// </summary>
    private void LoadParameters()
    {
        string identificator = QueryHelper.GetString("params", null);
        parameters = (Hashtable)WindowHelper.GetItem(identificator);

        if (parameters != null)
        {
            // Load values from session
            selectionMode = (SelectionModeEnum)parameters["SelectionMode"];
            resourcePrefix = ValidationHelper.GetString(parameters["ResourcePrefix"], "general");
            valuesSeparator = ValidationHelper.GetString(parameters["ValuesSeparator"], ";");
            whereCondition = ValidationHelper.GetString(parameters["WhereCondition"], null);
            orderBy = ValidationHelper.GetString(parameters["OrderBy"], null);
            callbackMethod = ValidationHelper.GetString(parameters["CallbackMethod"], null);
            allowEditTextBox = ValidationHelper.GetBoolean(parameters["AllowEditTextBox"], false);
            fireOnChanged = ValidationHelper.GetBoolean(parameters["FireOnChanged"], false);
            returnColumnName = ValidationHelper.GetString(parameters["ReturnColumnName"], null);
            disabledItems = ValidationHelper.GetString(parameters["DisabledItems"], null);

            switch (selectionMode)
            {
                case SelectionModeEnum.Multiple:
                case SelectionModeEnum.MultipleButton:
                case SelectionModeEnum.MultipleTextBox:
                    allowMultiple = true;
                    break;

                case SelectionModeEnum.SingleButton:
                case SelectionModeEnum.SingleDropDownList:
                case SelectionModeEnum.SingleTextBox:
                    allowMultiple = false;
                    break;
            }

            // Pre-select unigrid values passed from parent window
            if (!RequestHelper.IsPostBack())
            {
                string values = (string)parameters["Values"];
                if (!String.IsNullOrEmpty(values))
                {
                    hidItem.Value = values;
                    parameters["Values"] = null;
                }
            }
        }
    }


    /// <summary>
    /// Creates tree provider.
    /// </summary>
    /// <param name="siteId">ID of the site to create provider for.</param>
    /// <param name="userId">ID of the user to create provider for.</param>
    /// <returns></returns>
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

        string selected = "";
        string colName = "CategoryID";
        if (!string.IsNullOrEmpty(hidItem.Value))
        {
            // Selecting by ID
            if (returnColumnName == "CategoryID")
            {
                int[] valIds = ValidationHelper.GetIntegers(hidItem.Value.Split(new string[] { valuesSeparator }, StringSplitOptions.RemoveEmptyEntries), 0);
                selected = "0";
                foreach (int i in valIds)
                {
                    selected += "," + i;
                }
            }
            else
            {
                // Selecting by code name
                colName = "CategoryName";
                string[] valNames = hidItem.Value.Split(new string[] { valuesSeparator }, StringSplitOptions.RemoveEmptyEntries);
                selected = "N''";
                foreach (string name in valNames)
                {
                    selected += ", N'" + SqlHelperClass.GetSafeQueryString(name, false) + "'";
                }
            }
        }

        if (string.IsNullOrEmpty(selected))
        {
            selected = (returnColumnName == "CategoryID") ? "0" : "N''";
        }

        // Subquery to obtain count of enabled child categories for specified user, site and 'use global categories' setting
        string ChildCountColumn = "(SELECT COUNT(C.CategoryID) FROM CMS_Category AS C WHERE (C.CategoryEnabled = 1) AND (C.CategoryParentID = CMS_Category.CategoryID) AND (ISNULL(C.CategorySiteID, 0) = @SiteID OR (C.CategorySiteID IS NULL AND @IncludeGlobal = 1)) AND (ISNULL(C.CategoryUserID, 0) = @UserID)) AS CategoryChildCount";

        // Subquery to obtain count of selected enabled child categories with no disabled parent.
        string CheckedChildCountColumn = "(SELECT COUNT(CategoryID) FROM CMS_Category AS cc WHERE (cc.CategoryEnabled = 1) AND (cc." + colName + " IN (" + selected + ")) AND (cc.CategoryIDPath LIKE CMS_Category.CategoryIDPath + '/%')  AND (NOT EXISTS(SELECT CategoryID FROM CMS_Category AS pc WHERE (pc.CategoryEnabled = 0) AND (cc.CategoryIDPath like pc.CategoryIDPath+'/%')))) AS ChildChecked";

        // Prepare 
        provider.Columns = string.Format("CategoryID, CategoryName, CategoryDisplayName, CategoryLevel, CategoryOrder, CategoryParentID, CategoryIDPath, CategoryUserID, CategorySiteID, {0}, {1}", ChildCountColumn, CheckedChildCountColumn);
        provider.OrderBy = "CategoryUserID, CategorySiteID, CategoryOrder";
        provider.WhereCondition = "ISNULL(CategoryUserID, 0) = " + userId + " AND (CategoryEnabled = 1) AND (ISNULL(CategorySiteID, 0) = " + siteId;
        if (AllowGlobalCategories && (siteId > 0))
        {
            provider.WhereCondition += " OR CategorySiteID IS NULL";
        }
        provider.WhereCondition += ")";

        // Append explicit where condition
        provider.WhereCondition = SqlHelperClass.AddWhereCondition(provider.WhereCondition, whereCondition);

        return provider;
    }


    /// <summary>
    /// Returns string safe for inserting to javascript as parameter.
    /// </summary>
    /// <param name="param">Parameter</param>    
    private string GetSafe(string param)
    {
        // Replace + char for %20 to make it compatible with client side decodeURIComponent
        return ScriptHelper.GetString(Server.UrlEncode(param).Replace("+", "%20"));
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
                    valid = (SelectedCategory.CategorySiteID == CMSContext.CurrentSiteID);
                }
            }
        }

        // Select root when invalid
        if (!valid)
        {
            SelectedCategoryID = 0;
            SelectedCategoryParentID = CATEGORIES_ROOT_PARENT_ID;
        }
    }


    /// <summary>
    /// Returns true if current user can modify selected category.
    /// </summary>
    public bool CanModifySelectedCategory
    {
        get
        {
            CategoryInfo category = SelectedCategory;

            if (category != null)
            {
                if (!category.CategoryIsPersonal)
                {
                    return category.CategoryIsGlobal ? CanModifyGlobalCategories : CanModifySiteCategories;
                }

                // Personal categories can be modified.
                return true;
            }

            // No category selected
            return false;
        }
    }
}
