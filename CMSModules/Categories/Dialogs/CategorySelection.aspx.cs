using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;

using CMS.UIControls;
using CMS.GlobalHelper;
using CMS.SiteProvider;
using CMS.CMSHelper;

public partial class CMSModules_Categories_Dialogs_CategorySelection : CMSModalPage
{
    #region "Variables"

    private Hashtable parameters = null;
    private SelectionModeEnum selectionMode = SelectionModeEnum.SingleButton;
    private string valuesSeparator = ";";

    #endregion


    #region "Page events"
    
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        LoadParameters();

        CurrentMaster.PanelTitleActions.Visible = true;

        // Init actions images
        imgNewCategory.ImageUrl = GetImageUrl("Objects/CMS_Category/add.png");
        imgDeleteCategory.ImageUrl = GetImageUrl("Objects/CMS_Category/delete.png");
        imgMoveUp.ImageUrl = GetImageUrl("Objects/CMS_Category/up.png");
        imgMoveDown.ImageUrl = GetImageUrl("Objects/CMS_Category/down.png");
        imgEdit.ImageUrl = GetImageUrl("Objects/CMS_Category/edit.png");
        imgExpand.ImageUrl = GetImageUrl("Objects/CMS_Category/expandall.png");
        imgCollapse.ImageUrl = GetImageUrl("Objects/CMS_Category/collapseall.png");

        // Cancel button
        btnCancel.Attributes.Add("onclick", "return US_Cancel();");

        // Ok button
        btnOk.Attributes.Add("onclick", "return US_Submit();");

        lnkNew.Text = GetString("general.new");
        lnkDelete.Text = GetString("general.delete");
        lnkEdit.Text = GetString("general.edit");
        lnkUp.Text = GetString("general.up");
        lnkDown.Text = GetString("general.down");
        lnkExpandAll.Text = GetString("general.expand");
        lnkCollapseAll.Text = GetString("general.collapse");

        lnkDelete.Click += new EventHandler(SelectionElem.lnkDelete_Click);
        lnkUp.Click += new EventHandler(SelectionElem.lnkUp_Click);
        lnkDown.Click += new EventHandler(SelectionElem.lnkDown_Click);
        lnkExpandAll.Click += new EventHandler(SelectionElem.lnkExpandAll_Click);
        lnkCollapseAll.Click += new EventHandler(SelectionElem.lnkCollapseAll_Click);

        bool allowMultiple = false;
        if ((selectionMode == SelectionModeEnum.Multiple) ||
            (selectionMode == SelectionModeEnum.MultipleButton) ||
            (selectionMode == SelectionModeEnum.MultipleTextBox))
        {
            allowMultiple = true;
        }

        string titleText = GetString(allowMultiple ? "categories.selectmultiple" : "categories.select");
        CurrentMaster.Title.TitleText = titleText;
        CurrentMaster.Title.TitleImage = GetImageUrl("/CMSModules/CMS_Categories/module.png");
        Page.Title = titleText;
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        // Register JQuery
        ScriptHelper.RegisterDialogScript(this.Page);
        ScriptHelper.RegisterJQuery(Page);
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        int categoryId = 0;
        int categoryParentId = 0;
        
        // Get selected category and its parent IDs
        CategoryInfo category = SelectionElem.SelectedCategory;
        if (category != null)
        {
            categoryId = category.CategoryID;
            categoryParentId = category.CategoryParentID;

            // Check if user can manage selected category
            bool canModify = SelectionElem.CanModifySelectedCategory;
            lnkDelete.Enabled = canModify;
            lnkUp.Enabled = canModify;
            lnkDown.Enabled = canModify;

            if (!category.CategoryIsPersonal)
            {
                // Display New button when authorized to modify site categories
                lnkNew.Enabled = SelectionElem.CanModifySiteCategories;

                // Additionaly check GlobalModify under global categories 
                if (category.CategoryIsGlobal)
                {
                    lnkNew.Enabled |= SelectionElem.CanModifyGlobalCategories;
                }
            }
        }
        else
        {
            categoryParentId = SelectionElem.SelectedCategoryParentID;

            lnkNew.Enabled = SelectionElem.CategoriesRootSelected ? SelectionElem.CanModifyGlobalCategories || SelectionElem.CanModifySiteCategories : true;
        }

        // Enable/disable actions
        if (categoryId == 0)
        {
            lnkDelete.Enabled = false;
            lnkEdit.Enabled = false;
            lnkUp.Enabled = false;
            lnkDown.Enabled = false;
        }
        else
        {
            lnkEdit.OnClientClick = "modalDialog('" + ResolveUrl("~/CMSModules/Categories/Dialogs/CategoryEdit.aspx?categoryId=" + categoryId) + "', 'CategoryEdit', 500, 450); return false;";
        }

        // Enable/disable actions visualy
        if (!lnkDelete.Enabled)
        {
            lnkDelete.CssClass += " MenuItemDisabled";
            lnkDelete.OnClientClick = "";
        }
        lnkEdit.CssClass = lnkEdit.Enabled ? lnkEdit.CssClass.Replace(" MenuItemDisabled", "") : lnkEdit.CssClass + " MenuItemDisabled";
        lnkUp.CssClass = lnkUp.Enabled ? lnkUp.CssClass.Replace(" MenuItemDisabled", "") : lnkUp.CssClass + " MenuItemDisabled";
        lnkDown.CssClass = lnkDown.Enabled ? lnkDown.CssClass.Replace(" MenuItemDisabled", "") : lnkDown.CssClass + " MenuItemDisabled";
        lnkNew.CssClass = lnkNew.Enabled ? lnkNew.CssClass.Replace(" MenuItemDisabled", "") : lnkNew.CssClass + " MenuItemDisabled";

        string createPersonal = "";
        if ((SelectionElem.CategoriesRootSelected) || ((category != null) && !category.CategoryIsPersonal))
        {
            createPersonal = "&personal=0";
        }

        if (lnkNew.Enabled)
        {
            lnkNew.OnClientClick = "modalDialog('" + ResolveUrl("~/CMSModules/Categories/Dialogs/CategoryEdit.aspx?parentId=" + categoryId + createPersonal) + "', 'CategoryEdit', 500, 450); return false;";
        }
    }

    #endregion


    #region "Methods"

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
            valuesSeparator = ValidationHelper.GetString(parameters["ValuesSeparator"], ";");
        }
    }

    #endregion
}

