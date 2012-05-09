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
using CMS.DataEngine;
using CMS.SiteProvider;
using CMS.Ecommerce;
using CMS.CMSHelper;
using CMS.UIControls;

public partial class CMSModules_Ecommerce_Pages_Tools_ProductOptions_OptionCategory_Edit_General : CMSProductOptionCategoriesPage
{
    protected int categoryID = 0;
    protected int editedSiteId = -1;

    private string SelectionType
    {
        get
        {
            return ValidationHelper.GetString(ViewState["SelectionType"], "");
        }
        set
        {
            ViewState["SelectionType"] = value;
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!CMSContext.CurrentUser.IsAuthorizedPerUIElement("CMS.Ecommerce", "ProductOptions.General"))
        {
            RedirectToCMSDeskUIElementAccessDenied("CMS.Ecommerce", "ProductOptions.General");
        }

        // Field validator error messages initialization
        rfvCategoryName.ErrorMessage = GetString("general.requirescodename");
        rfvDisplayName.ErrorMessage = GetString("general.requiresdisplayname");

        // Control initializations
        lblCategoryName.Text = GetString("general.codename") + ResHelper.Colon;
        lblCategoryDescription.Text = GetString("general.description") + ResHelper.Colon;
        lblDefaultRecord.Text = GetString("OptionCategory_Edit.CategoryDefaultRecord");
        lblNoOptions.Text = GetString("OptionCategory_Edit.NoProductOptions");
        btnOk.Text = GetString("General.OK");        
        
        categoryID = QueryHelper.GetInteger("categoryID", 0);
        editedSiteId = ConfiguredSiteID;

        // Get option category information	
        OptionCategoryInfo categObj = OptionCategoryInfoProvider.GetOptionCategoryInfo(categoryID);
        EditedObject = categObj;
        
        if (categObj == null)
        {
            // Do not process
            return;
        }

        // Set category type specific labels
        switch (categObj.CategorySelectionType)
        {
            case OptionCategorySelectionTypeEnum.TextArea:
            case OptionCategorySelectionTypeEnum.TextBox:

                lblCategorySelectionType.Text = GetString("OptionCategory_Edit.CategoryTextTypeLabel");
                lblCategoryDefaultOptions.Text = GetString("optioncategory_edit.categorydefaulttext");
                lblTextMaxLength.Text = GetString("com.optioncategory.textmaxlength");                
                break;

            default:

                lblCategorySelectionType.Text = GetString("OptionCategory_Edit.CategorySelectionTypeLabel");
                lblCategoryDefaultOptions.Text = GetString("OptionCategory_Edit.CategoryDefaultOptionsLabel");
                break;
        }

        // Use default currency for price formatting
        productOptionSelector.UseDefaultCurrency = true;

        if (SelectionType != "")
        {
            categObj.CategorySelectionType = GetOptionCategoryEnum(SelectionType);
            productOptionSelector.OptionCategory = categObj;
        }
        else
        {
            productOptionSelector.OptionCategoryId = categoryID;
        }

        editedSiteId = categObj.CategorySiteID;

        // Check edited object site ID
        CheckEditedObjectSiteID(editedSiteId);

        // Fill editing form
        if (!RequestHelper.IsPostBack())
        {
            LoadData(categObj);

            // Show that the optionCategory was created or updated successfully
            if (QueryHelper.GetString("saved", "") == "1")
            {
                lblInfo.Visible = true;
                lblInfo.Text = GetString("General.ChangesSaved");
            }
        }        
    }


    protected override void OnPreRender(EventArgs e)
    {
        // Show information label when selection control is empty or contain only 'none'
        lblNoOptions.Visible = !this.productOptionSelector.HasSelectableOptions();
        
        base.OnPreRender(e);
    }


    /// <summary>
    /// Load data of editing optionCategory.
    /// </summary>
    /// <param name="optionCategoryObj">OptionCategory object</param>
    protected void LoadData(OptionCategoryInfo optionCategoryObj)
    {
        // Load data to controls
        txtDisplayName.Text = optionCategoryObj.CategoryDisplayName;
        txtCategoryName.Text = optionCategoryObj.CategoryName;

        // Add text types only to text category or to category without options
        if ((optionCategoryObj.CategorySelectionType == OptionCategorySelectionTypeEnum.TextBox) ||
            (optionCategoryObj.CategorySelectionType == OptionCategorySelectionTypeEnum.TextArea))
        {
            plcDefaultRecordText.Visible = false;

            // Add text types
            drpCategorySelectionType.Items.Add(new ListItem(GetString("optioncategory_selectiontype.textbox"), OptionCategorySelectionTypeEnum.TextBox.ToString()));
            drpCategorySelectionType.Items.Add(new ListItem(GetString("optioncategory_selectiontype.textarea"), OptionCategorySelectionTypeEnum.TextArea.ToString()));
        }
        else
        {
            plcTextMaxLength.Visible = false;

            // Add standard types
            drpCategorySelectionType.Items.Add(new ListItem(GetString("optioncategory_selectiontype.dropdownlist"), OptionCategorySelectionTypeEnum.Dropdownlist.ToString()));
            drpCategorySelectionType.Items.Add(new ListItem(GetString("optioncategory_selectiontype.radiobuttonsvertical"), OptionCategorySelectionTypeEnum.RadioButtonsVertical.ToString()));
            drpCategorySelectionType.Items.Add(new ListItem(GetString("optioncategory_selectiontype.radiobuttonshorizontal"), OptionCategorySelectionTypeEnum.RadioButtonsHorizontal.ToString()));
            drpCategorySelectionType.Items.Add(new ListItem(GetString("optioncategory_selectiontype.checkboxesvertical"), OptionCategorySelectionTypeEnum.CheckBoxesVertical.ToString()));
            drpCategorySelectionType.Items.Add(new ListItem(GetString("optioncategory_selectiontype.checkboxeshorizontal"), OptionCategorySelectionTypeEnum.CheckBoxesHorizontal.ToString()));
        }

        drpCategorySelectionType.SelectedValue = optionCategoryObj.CategorySelectionType.ToString();
        chkCategoryDisplayPrice.Checked = optionCategoryObj.CategoryDisplayPrice;

        txtCategoryDecription.Text = optionCategoryObj.CategoryDescription;
        txtDefaultRecord.Text = optionCategoryObj.CategoryDefaultRecord;        
        txtTextMaxLength.Text = (optionCategoryObj.CategoryTextMaxLength > 0) ? optionCategoryObj.CategoryTextMaxLength.ToString() : "";
        chkCategoryEnabled.Checked = optionCategoryObj.CategoryEnabled;
    }


    /// <summary>
    /// Sets data to database.
    /// </summary>
    protected void btnOK_Click(object sender, EventArgs e)
    {
        // Check module permissions
        bool global = (editedSiteId <= 0);
        if (!ECommerceContext.IsUserAuthorizedToModifyOptionCategory(global))
        {
            // Check module permissions
            if (global)
            {
                RedirectToAccessDenied("CMS.Ecommerce", "EcommerceGlobalModify");
            }
            else
            {
                RedirectToAccessDenied("CMS.Ecommerce", "EcommerceModify OR ModifyProducts");
            }
        }

        // Validate the form
        string errorMessage = ValidateForm();

        if (errorMessage == "")
        {
            // Category code name must be unique
            OptionCategoryInfo optionCategoryObj = null;
            string siteWhere = (editedSiteId > 0) ? " AND (CategorySiteID = " + editedSiteId + " OR CategorySiteID IS NULL)" : "";
            DataSet ds = OptionCategoryInfoProvider.GetOptionCategories("CategoryName = '" + txtCategoryName.Text.Trim().Replace("'", "''") + "'" + siteWhere, null, 1, null);
            if (!DataHelper.DataSourceIsEmpty(ds))
            {
                optionCategoryObj = new OptionCategoryInfo(ds.Tables[0].Rows[0]);
            }

            // If category code name value is unique														
            if ((optionCategoryObj == null) || (optionCategoryObj.CategoryID == categoryID))
            {
                // If optionCategory doesn't already exist, create new one
                if (optionCategoryObj == null)
                {
                    optionCategoryObj = OptionCategoryInfoProvider.GetOptionCategoryInfo(categoryID);
                    if (optionCategoryObj == null)
                    {
                        optionCategoryObj = new OptionCategoryInfo();
                        optionCategoryObj.CategorySiteID = editedSiteId;
                    }
                }

                // Set category properties
                optionCategoryObj.CategoryID = categoryID;
                optionCategoryObj.CategoryDisplayName = txtDisplayName.Text.Trim();
                optionCategoryObj.CategoryName = txtCategoryName.Text.Trim();
                optionCategoryObj.CategorySelectionType = GetOptionCategoryEnum(drpCategorySelectionType.SelectedValue);
                optionCategoryObj.CategoryDefaultOptions = productOptionSelector.GetSelectedSKUOptions();
                optionCategoryObj.CategoryDescription = txtCategoryDecription.Text.Trim();
                optionCategoryObj.CategoryDefaultRecord = txtDefaultRecord.Text.Trim();
                optionCategoryObj.CategoryEnabled = chkCategoryEnabled.Checked;
                optionCategoryObj.CategoryDisplayPrice = chkCategoryDisplayPrice.Checked;
                optionCategoryObj.CategoryTextMaxLength = ValidationHelper.GetInteger(txtTextMaxLength.Text.Trim(),0);

                using (CMSTransactionScope tran = new CMSTransactionScope())
                {
                    // Save changes
                    OptionCategoryInfoProvider.SetOptionCategoryInfo(optionCategoryObj);

                    // Add text option to text category only if it is empty
                    if (((optionCategoryObj.CategorySelectionType == OptionCategorySelectionTypeEnum.TextBox) ||
                        (optionCategoryObj.CategorySelectionType == OptionCategorySelectionTypeEnum.TextArea)) &&
                        (SKUInfoProvider.GetSKUOptionsCount(categoryID) == 0))
                    {
                        // Create default text product option
                        SKUInfo option = new SKUInfo();
                        option.SKUName = optionCategoryObj.CategoryDisplayName;
                        option.SKUDescription = optionCategoryObj.CategoryDescription;
                        option.SKUSiteID =  optionCategoryObj.CategorySiteID;
                        option.SKUPrice = 0;
                    }

                    tran.Commit();
                }

                // If hidebreadcrumbs (is in modal window)
                if (QueryHelper.GetBoolean("hidebreadcrumbs", false))
                {
                    // Close window and refresh opener
                    ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "CloseAndRefresh", ScriptHelper.GetScript("parent.wopener.Refresh(); parent.window.close();"));
                }
                else
                {
                    // Normal save
                    //URLHelper.Redirect("OptionCategory_Edit_General.aspx?CategoryID=" + Convert.ToString(optionCategoryObj.CategoryID) + "&saved=1&siteId=" + this.SiteID);
                    ScriptHelper.RefreshTabHeader(this, "general");

                    lblInfo.Visible = true;
                    lblInfo.Text = GetString("General.ChangesSaved");
                }
            }
            else
            {
                lblError.Visible = true;
                lblError.Text = GetString("optioncategory_new.errorExistingCodeName");
            }
        }
        else
        {
            lblError.Visible = true;
            lblError.Text = errorMessage;
        }
    }

    /// <summary>
    /// Validates form. If validation fails, specified error message is returned, otherwise an empty string is returned.
    /// </summary>    
    private string ValidateForm()
    {
        // Check input value from general textboxes
        string errorMessage = new Validator().NotEmpty(txtDisplayName.Text.Trim(), GetString("general.requiresdisplayname"))
            .NotEmpty(txtCategoryName.Text.Trim(), GetString("general.requirescodename"))
            .IsIdentificator(txtCategoryName.Text, GetString("optioncategory_new.errorNotIdentificator")).Result;

        
        if (string.IsNullOrEmpty(errorMessage) && plcTextMaxLength.Visible && (txtTextMaxLength.Text.Trim() != ""))
        {
            // Check if text length is valid number
            int textLengthNumber = ValidationHelper.GetInteger(txtTextMaxLength.Text.Trim(), 0);
            if (textLengthNumber <= 0)
            {
                errorMessage = GetString("com.optioncategory.errortextmaxlength");
            }
            // Check text length
            else 
            {
                int textLength = productOptionSelector.GetSelectedSKUOptions().Length;
                if (textLength > textLengthNumber)
                {
                    errorMessage = string.Format(GetString("com.optioncategory.maxtextlengthexceeded"), textLengthNumber);
                }
            }
        }

        return errorMessage;
    }


    /// <summary>
    /// DrpCategorySelectionType SelectedIndexChanged event handler.
    /// </summary>
    protected void drpCategorySelectionType_SelectedIndexChanged(object sender, EventArgs e)
    {
        OptionCategoryInfo categObj = OptionCategoryInfoProvider.GetOptionCategoryInfo(categoryID);
        if (categObj != null)
        {
            // Clone option category data
            OptionCategoryInfo tempCategObj = new OptionCategoryInfo(categObj, false);

            // Set new selection type from selection control
            tempCategObj.CategorySelectionType = GetOptionCategoryEnum(drpCategorySelectionType.SelectedValue);

            // Set previously selected options
            tempCategObj.CategoryDefaultOptions = productOptionSelector.GetSelectedSKUOptions();

            // Set display price option
            tempCategObj.CategoryDisplayPrice = this.chkCategoryDisplayPrice.Checked;

            // Remember selection type in viewstate
            this.SelectionType = drpCategorySelectionType.SelectedValue;

            // Reload selector
            productOptionSelector.OptionCategory = tempCategObj;
            productOptionSelector.ReloadSelector();
        }
    }


    /// <summary>
    /// Handle display price selection made.
    /// </summary>
    protected void chkCategoryDisplayPrice_CheckedChanged(object sender, EventArgs e)
    {
        OptionCategoryInfo categObj = OptionCategoryInfoProvider.GetOptionCategoryInfo(categoryID);
        if (categObj != null)
        {
            // Clone option category data
            OptionCategoryInfo tempCategObj = new OptionCategoryInfo(categObj, false);

            // Set new selection type from selection control
            tempCategObj.CategorySelectionType = GetOptionCategoryEnum(drpCategorySelectionType.SelectedValue);

            // Set previously selected options
            tempCategObj.CategoryDefaultOptions = productOptionSelector.GetSelectedSKUOptions();

            // Set display price option
            tempCategObj.CategoryDisplayPrice = this.chkCategoryDisplayPrice.Checked;

            // Remember selection type in viewstate
            this.SelectionType = drpCategorySelectionType.SelectedValue;

            // Reload selector
            productOptionSelector.OptionCategory = tempCategObj;
            productOptionSelector.ReloadSelector();
        }
    }


    /// <summary>
    /// Returns option category enumeration type according to the string value.
    /// </summary>
    /// <param name="value">Option category string identifier</param>
    private OptionCategorySelectionTypeEnum GetOptionCategoryEnum(string value)
    {
        if (value == OptionCategorySelectionTypeEnum.CheckBoxesHorizontal.ToString())
        {
            return OptionCategorySelectionTypeEnum.CheckBoxesHorizontal;
        }
        else if (value == OptionCategorySelectionTypeEnum.CheckBoxesVertical.ToString())
        {
            return OptionCategorySelectionTypeEnum.CheckBoxesVertical;
        }
        else if (value == OptionCategorySelectionTypeEnum.RadioButtonsHorizontal.ToString())
        {
            return OptionCategorySelectionTypeEnum.RadioButtonsHorizontal;
        }
        else if (value == OptionCategorySelectionTypeEnum.RadioButtonsVertical.ToString())
        {
            return OptionCategorySelectionTypeEnum.RadioButtonsVertical;
        }
        else if (value == OptionCategorySelectionTypeEnum.TextBox.ToString())
        {
            return OptionCategorySelectionTypeEnum.TextBox;
        }
        else if (value == OptionCategorySelectionTypeEnum.TextArea.ToString())
        {
            return OptionCategorySelectionTypeEnum.TextArea;
        }
        else
        {
            return OptionCategorySelectionTypeEnum.Dropdownlist;
        }
    }
}
