using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using CMS.ExtendedControls;
using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.DataEngine;
using CMS.UIControls;
using CMS.SettingsProvider;

namespace CMS.Ecommerce
{
    /// <summary>
    /// Base class of the option category sku selector control.
    /// </summary>
    public class ProductOptionSelector : CMSUserControl
    {
        #region "Variables"

        private int mOptionCategoryId = 0;
        private bool mShowOptionCategoryName = true;
        private bool mShowOptionCategoryDescription = true;
        private bool mUseDefaultCurrency = false;
        private bool mShowPriceIncludingTax = false;

        private Control mSelectionControl = null;

        private OptionCategoryInfo mOptionCategory = null;
        private ShoppingCartInfo mLocalShoppingCartObj = null;

        private Hashtable mProductOptions= null;

        private int mTextOptionSKUID = 0;

        #endregion


        #region "Public properties"

        /// <summary>
        /// Option category ID.
        /// </summary>
        public int OptionCategoryId
        {
            get
            {
                if ((mOptionCategoryId == 0) && (this.OptionCategory != null))
                {
                    mOptionCategoryId = this.OptionCategory.CategoryID;
                }
                return mOptionCategoryId;
            }
            set
            {
                // Force loading new selection control
                //mSelectionControl = null;

                // Force creating new option category object
                mOptionCategory = null;

                mOptionCategoryId = value;
            }
        }


        /// <summary>
        /// Indicates whether option category name should be displayed to the user.
        /// </summary>
        public bool ShowOptionCategoryName
        {
            get
            {
                return mShowOptionCategoryName;
            }
            set
            {
                mShowOptionCategoryName = value;
            }
        }


        /// <summary>
        /// Indicates whether option category description should be displayed to the user.
        /// </summary>
        public bool ShowOptionCategoryDescription
        {
            get
            {
                return mShowOptionCategoryDescription;
            }
            set
            {
                mShowOptionCategoryDescription = value;
            }
        }


        /// <summary>
        /// Selection control according to the current option category selection type.
        /// </summary>
        public Control SelectionControl
        {
            get
            {
                if (mSelectionControl == null)
                {
                    mSelectionControl = GetSelectionControl();
                }
                return mSelectionControl;
            }
        }


        /// <summary>
        /// Option category object. It is loaded from option category datarow by default, if it is not specified it is loaded by option category id.
        /// </summary>
        public OptionCategoryInfo OptionCategory
        {
            get
            {
                if (mOptionCategory == null)
                {
                    mOptionCategory = OptionCategoryInfoProvider.GetOptionCategoryInfo(mOptionCategoryId);
                }

                return mOptionCategory;
            }
            set
            {
                mOptionCategory = value;
            }
        }


        /// <summary>
        /// Shopping cart object required for price formatting. Use this property when in CMSDesk.
        /// </summary>
        public ShoppingCartInfo LocalShoppingCartObj
        {
            get
            {
                return mLocalShoppingCartObj;
            }
            set
            {
                mLocalShoppingCartObj = value;
            }
        }


        /// <summary>
        /// TRUE - default currency is used for price formatting and no discounts and taxes are applied to price, by default it is set to FALSE.
        /// </summary>
        public bool UseDefaultCurrency
        {
            get
            {
                return mUseDefaultCurrency;
            }
            set
            {
                mUseDefaultCurrency = value;
            }
        }


        /// <summary>
        /// TRUE - product option price is displayed including tax, FALSE - product option price is displayed without tax.
        /// </summary>
        public bool ShowPriceIncludingTax
        {
            get
            {
                return mShowPriceIncludingTax;
            }
            set
            {
                mShowPriceIncludingTax = value;
            }
        }

        /// <summary>
        /// SKU data of all product options of the current option category
        /// </summary>
        public Hashtable ProductOptions
        {
            get
            {
                if (mProductOptions == null)
                {
                    mProductOptions = GetProductOptions(); 
                }

                return mProductOptions;
            }
        }

        #endregion


        #region "Private properties"

        public int TextOptionSKUID
        {
            get
            {
                if (mTextOptionSKUID <= 0)
                {
                    DataSet ds = SKUInfoProvider.GetSKUOptions(this.OptionCategoryId, true);
                    if (!DataHelper.DataSourceIsEmpty(ds))
                    {
                        DataRow dr = ds.Tables[0].Rows[0];
                        mTextOptionSKUID = ValidationHelper.GetInteger(ds.Tables[0].Rows[0]["SKUID"], 0);
                    }
                }

                return mTextOptionSKUID;
            }
            set
            {
                mTextOptionSKUID = value;
            }
        }

        #endregion



        #region "Public methods"

        /// <summary>
        /// Reloads selection control according to the option category selection type.
        /// </summary>
        public void LoadCategorySelectionControl()
        {
            mSelectionControl = GetSelectionControl();
        }


        /// <summary>
        /// Reloads selection control data according to the option category data.
        /// </summary>
        public void ReloadData()
        {
            DebugHelper.SetContext("ProductOptionSelector");

            // Load actual product options
            this.LoadSKUOptions();

            // Mark default product options
            this.SetDefaultSKUOptions();

            DebugHelper.ReleaseContext();
        }


        /// <summary>
        /// Gets selected product options from the selection control.
        /// </summary>
        public string GetSelectedSKUOptions()
        {
            if (this.SelectionControl != null)
            {
                // Dropdown list, Radiobutton list - single selection
                if ((this.SelectionControl.GetType() == typeof(LocalizedDropDownList)) ||
                    (this.SelectionControl.GetType() == typeof(LocalizedRadioButtonList)))
                {
                    return ((ListControl)this.SelectionControl).SelectedValue;
                }
                // Checkbox list - multiple selection
                else if (this.SelectionControl.GetType() == typeof(LocalizedCheckBoxList))
                {
                    string result = "";
                    foreach (ListItem item in ((CheckBoxList)this.SelectionControl).Items)
                    {
                        if (item.Selected)
                        {
                            result += item.Value + ",";
                        }
                    }
                    return result.TrimEnd(',');
                }
                else if (this.SelectionControl is TextBox)
                {
                    return ((TextBox)(this.SelectionControl)).Text;
                }
            }

            return null;
        }


        public List<ShoppingCartItemParameters> GetSelectedOptionsParameters()
        {
            List<ShoppingCartItemParameters> options = new List<ShoppingCartItemParameters>();
            ShoppingCartItemParameters param = null;

            if (this.SelectionControl != null)
            {
                // Dropdown list, Radiobutton list - single selection
                if ((this.SelectionControl.GetType() == typeof(LocalizedDropDownList)) ||
                    (this.SelectionControl.GetType() == typeof(LocalizedRadioButtonList)))
                {
                    param = new ShoppingCartItemParameters();
                    param.SKUID = ValidationHelper.GetInteger(((ListControl)this.SelectionControl).SelectedValue, 0);

                    if (param.SKUID > 0)
                    {
                        options.Add(param);
                    }
                }
                // Checkbox list - multiple selection
                else if (this.SelectionControl.GetType() == typeof(LocalizedCheckBoxList))
                {
                    foreach (ListItem item in ((CheckBoxList)this.SelectionControl).Items)
                    {
                        if (item.Selected)
                        {
                            param = new ShoppingCartItemParameters();
                            param.SKUID = ValidationHelper.GetInteger(item.Value, 0);

                            if (param.SKUID > 0)
                            {
                                options.Add(param);
                            }
                        }
                    }
                }
                else if (this.SelectionControl is TextBox)
                {
                    // Bind data
                    if (this.SelectionControl is TextBoxWithLabel)
                    {
                        if (TextOptionSKUID > 0)
                        {
                            param = new ShoppingCartItemParameters();
                            param.SKUID = TextOptionSKUID;
                            param.Text = ((TextBox)(this.SelectionControl)).Text;
                            if (param.SKUID > 0)
                            {
                                options.Add(param);
                            }
                        }
                    }
                }
            }

            return options;
        }


        /// <summary>
        /// Returns TRUE when selection control is empty or only '(none)' record is included, otherwise returns FALSE.
        /// </summary>
        public bool IsSelectionControlEmpty()
        {
            if (this.SelectionControl != null)
            {
                // Text type
                TextBox tb = this.SelectionControl as TextBox;
                if (tb != null)
                {
                    return string.IsNullOrEmpty(tb.Text);
                }

                // Other types
                ListControl list = this.SelectionControl as ListControl;
                if (list != null)
                {
                    bool noItems = (list.Items.Count == 0);
                    bool onlyNoneRecord = ((list.Items.Count == 1) && (list.Items.FindByValue("0") != null));

                    return (noItems || onlyNoneRecord);
                }

                return false;
            }

            return true;
        }


        /// <summary>
        /// Returns true if there is a choice of values in selection control.
        /// </summary>
        public bool HasSelectableOptions()
        {
            if (this.SelectionControl != null)
            {
                // Text type
                if (this.SelectionControl is TextBox)
                {
                    // Text box has always options
                    return true;
                }

                return !IsSelectionControlEmpty();
            }

            return true;
        }


        /// <summary>
        /// Validates selected/entered product option. If it is valid, returns true, otherwise returns false.
        /// </summary>        
        public virtual bool IsValid()
        {
            return true;
        }

        #endregion


        #region "Private methods"

        /// <summary>
        /// Returns initialized selection control according to the specified selection type.
        /// </summary>
        private Control GetSelectionControl()
        {
            if (this.OptionCategory != null)
            {
                switch (this.OptionCategory.CategorySelectionType)
                {
                    // Dropdownlist                     
                    case OptionCategorySelectionTypeEnum.Dropdownlist:

                        LocalizedDropDownList dropDown = new LocalizedDropDownList();
                        dropDown.ID = "dropdown";
                        dropDown.DataTextField = "SKUName";
                        dropDown.DataValueField = "SKUID";
                        dropDown.DataBound += new EventHandler(SelectionControl_DataBound);
                        return dropDown;

                    // Checkbox list with horizontal direction
                    case OptionCategorySelectionTypeEnum.CheckBoxesHorizontal:

                        LocalizedCheckBoxList boxListHorizontal = new LocalizedCheckBoxList();
                        boxListHorizontal.ID = "chkHorizontal";
                        boxListHorizontal.RepeatDirection = RepeatDirection.Horizontal;
                        boxListHorizontal.DataTextField = "SKUName";
                        boxListHorizontal.DataValueField = "SKUID";
                        boxListHorizontal.DataBound += new EventHandler(SelectionControl_DataBound);
                        return boxListHorizontal;

                    // Checkbox list with vertical direction
                    case OptionCategorySelectionTypeEnum.CheckBoxesVertical:

                        LocalizedCheckBoxList boxListVertical = new LocalizedCheckBoxList();
                        boxListVertical.ID = "chkVertical";
                        boxListVertical.RepeatDirection = RepeatDirection.Vertical;
                        boxListVertical.DataTextField = "SKUName";
                        boxListVertical.DataValueField = "SKUID";
                        boxListVertical.DataBound += new EventHandler(SelectionControl_DataBound);
                        return boxListVertical;

                    // Radiobuton list with horizontal direction
                    case OptionCategorySelectionTypeEnum.RadioButtonsHorizontal:

                        LocalizedRadioButtonList buttonListHorizontal = new LocalizedRadioButtonList();
                        buttonListHorizontal.ID = "radHorizontal";
                        buttonListHorizontal.RepeatDirection = RepeatDirection.Horizontal;
                        buttonListHorizontal.DataTextField = "SKUName";
                        buttonListHorizontal.DataValueField = "SKUID";
                        buttonListHorizontal.DataBound += new EventHandler(SelectionControl_DataBound);
                        return buttonListHorizontal;

                    // Radiobuton list with vertical direction
                    case OptionCategorySelectionTypeEnum.RadioButtonsVertical:

                        LocalizedRadioButtonList buttonListVertical = new LocalizedRadioButtonList();
                        buttonListVertical.ID = "radVertical";
                        buttonListVertical.RepeatDirection = RepeatDirection.Vertical;
                        buttonListVertical.DataTextField = "SKUName";
                        buttonListVertical.DataValueField = "SKUID";
                        buttonListVertical.DataBound += new EventHandler(SelectionControl_DataBound);
                        return buttonListVertical;

                    // Text box
                    case OptionCategorySelectionTypeEnum.TextBox:

                        TextBox text = new TextBoxWithLabel();
                        text.ID = "txtText";
                        text.CssClass = "TextBoxField";
                        return text;

                    // Text area
                    case OptionCategorySelectionTypeEnum.TextArea:

                        TextBox textarea = new TextBoxWithLabel();
                        textarea.ID = "txtArea";
                        textarea.CssClass = "TextAreaField";
                        textarea.TextMode = TextBoxMode.MultiLine;
                        return textarea;
                }
            }

            return null;
        }


        private void SelectionControl_DataBound(object sender, EventArgs e)
        {
            if (this.OptionCategory.CategoryDisplayPrice)
            {
                if (!DataHelper.DataSourceIsEmpty(((ListControl)sender).DataSource))
                {
                    foreach (DataRow row in ((DataSet)((ListControl)sender).DataSource).Tables[0].Rows)
                    {
                        int skuId = ValidationHelper.GetInteger(row["SKUId"], 0);
                        ListItem item = ((ListControl)sender).Items.FindByValue(skuId.ToString());

                        if (item != null)
                        {
                            item.Text += GetPrice(row);
                        }
                    }
                }
            }
        }


        private string GetPrice(DataRow row)
        {
            SKUInfo sku = new SKUInfo(row);
            CurrencyInfo currency = null; 
            double price = 0;
            
            // Use product option currency
            if (this.UseDefaultCurrency)
            {
                // Get site main currency
                currency = CurrencyInfoProvider.GetMainCurrency(sku.SKUSiteID);

                // Get product price
                price = sku.SKUPrice;
            }
            // Use cart currency 
            else
            {   
                // Get cart currency
                currency = this.LocalShoppingCartObj.CurrencyInfoObj;

                // Get price in site main currency
                price = SKUInfoProvider.GetSKUPrice(sku, this.LocalShoppingCartObj, true, this.ShowPriceIncludingTax);

                // Get price in cart currency
                price = this.LocalShoppingCartObj.ApplyExchangeRate(price);
            }

            string preffix = (price >= 0) ? "+ " : "- ";
            price = Math.Abs(price);

            // Round price
            price = CurrencyInfoProvider.RoundTo(price, currency);

            // Format price
            string formattedPrice = CurrencyInfoProvider.GetFormattedPrice(price, currency);

            return " (" + preffix + formattedPrice + ")";
        }


        /// <summary>
        /// Sets category default options as 'Selected' in selection control.
        /// </summary>        
        private void SetDefaultSKUOptions()
        {
            if ((this.SelectionControl != null) && (this.OptionCategory != null))
            {
                // Dropdown list - single selection
                // Radiobutton list - single selection
                if ((this.SelectionControl is LocalizedDropDownList) ||
                    (this.SelectionControl is LocalizedRadioButtonList))
                {
                    try
                    {
                        ((ListControl)this.SelectionControl).SelectedValue = this.OptionCategory.CategoryDefaultOptions;
                    }
                    catch
                    {
                    }
                }
                // Checkbox list - multiple selection
                else if (this.SelectionControl is LocalizedCheckBoxList)
                {
                    try
                    {
                        if (this.OptionCategory.CategoryDefaultOptions != "")
                        {
                            foreach (string skuId in this.OptionCategory.CategoryDefaultOptions.Split(','))
                            {
                                // Ensure value is not empty
                                string value = (skuId != "") ? skuId : "0";

                                ListItem item = ((CheckBoxList)this.SelectionControl).Items.FindByValue(value);
                                if (item != null)
                                {
                                    item.Selected = true;
                                }
                            }
                        }
                    }
                    catch
                    {
                    }
                }
                // Text type
                else if (this.SelectionControl is TextBoxWithLabel)
                {
                    TextBoxWithLabel tb = this.SelectionControl as TextBoxWithLabel;
                    tb.Text = this.OptionCategory.CategoryDefaultOptions;
                }
            }
        }


        /// <summary>
        /// Loads data (SKU options) to the selection control.
        /// </summary>
        private void LoadSKUOptions()
        {
            // Only for none-text types
            if (this.SelectionControl != null)
            {
                // Bind data
                DataSet ds = SKUInfoProvider.GetSKUOptions(this.OptionCategoryId, true);

                if (this.SelectionControl is TextBoxWithLabel)
                {
                    if (!DataHelper.DataSourceIsEmpty(ds))
                    {
                        DataRow dr = ds.Tables[0].Rows[0];
                        TextOptionSKUID = ValidationHelper.GetInteger(ds.Tables[0].Rows[0]["SKUID"], 0);

                        if (this.OptionCategory.CategoryDisplayPrice)
                        {
                            TextBoxWithLabel tb = this.SelectionControl as TextBoxWithLabel;
                            tb.LabelText = GetPrice(dr);
                        }
                    }
                }
                else
                {
                    ((ListControl)this.SelectionControl).DataSource = ds;
                    this.SelectionControl.DataBind();

                    // Add '(none)' record when it is allowed
                    if ((this.OptionCategory != null) && (this.OptionCategory.CategoryDefaultRecord != ""))
                    {
                        ListItem noneRecord = new ListItem(this.OptionCategory.CategoryDefaultRecord, "0");
                        ((ListControl)this.SelectionControl).Items.Insert(0, noneRecord);
                    }
                }
            }
        }


        /// <summary>
        /// Returns hash table with product options.
        /// </summary>
        private Hashtable GetProductOptions()
        {
            Hashtable optionsTable = new Hashtable();

            // Get options and load them to the hashtable
            var options = SKUInfoProvider.GetSKUOptions(this.OptionCategoryId, true);
            foreach (SKUInfo sku in options)
            {
                optionsTable.Add(sku.SKUID, sku);
            }

            return optionsTable;
        }

        #endregion
    }
}
