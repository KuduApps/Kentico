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

using CMS.Ecommerce;
using CMS.GlobalHelper;
using CMS.ExtendedControls;

public partial class CMSModules_Ecommerce_Controls_ProductOptions_ProductOptionSelector : ProductOptionSelector
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LoadSelector();
    }


    /// <summary>
    /// Loads selector's data.
    /// </summary>
    private void LoadSelector()
    {
        if (this.SelectionControl != null)
        {
            // Add selection control to the collection            
            this.pnlSelector.Controls.Add(this.SelectionControl);

            if (this.IsSelectionControlEmpty())
            {
                // Load selection control data according to the optiong category data
                this.ReloadData();
            }

            // There is no choice -> hide control
            if (!this.HasSelectableOptions())
            {
                pnlContainer.Visible = false;
            }
            // Option category is not empty -> display option category details
            else if (this.OptionCategory != null)
            {
                // Show / hide option category name
                if (this.ShowOptionCategoryName)
                {
                    lblCategName.Text = HTMLHelper.HTMLEncode(ResHelper.LocalizeString(this.OptionCategory.CategoryDisplayName));
                }
                else
                {
                    lblCategName.Visible = false;
                }

                // Show / hide option category description
                if (this.ShowOptionCategoryDescription)
                {
                    lblCategDescription.Text = ResHelper.LocalizeString(this.OptionCategory.CategoryDescription);
                }
                else
                {
                    lblCategDescription.Visible = false;
                }
            }

            // WAI validation
            if (this.OptionCategory.CategorySelectionType == OptionCategorySelectionTypeEnum.Dropdownlist)
            {
                lblCategName.AssociatedControlClientID = SelectionControl.ClientID;
            }
        }
    }


    // Reloads selector's data
    public void ReloadSelector()
    {
        pnlSelector.Controls.Clear();

        this.LoadCategorySelectionControl();

        LoadSelector();
    }


    /// <summary>
    /// Validates selected/entered product option. If it is valid, returns true, otherwise returns false.
    /// </summary> 
    public override bool IsValid()
    {
        // Check length of the text product option
        if ((this.OptionCategory.CategorySelectionType == OptionCategorySelectionTypeEnum.TextBox ||
            this.OptionCategory.CategorySelectionType == OptionCategorySelectionTypeEnum.TextArea) &&
            this.OptionCategory.CategoryTextMaxLength > 0)
        {
            // Get text length
            int textLength = this.GetSelectedSKUOptions().Length;

            // Validate text length
            if (textLength > this.OptionCategory.CategoryTextMaxLength)
            {
                lblError.Text = string.Format(GetString("com.optioncategory.maxtextlengthexceeded"), this.OptionCategory.CategoryTextMaxLength);
                plnError.Visible = true;
                return false;
            }
        }

        return true;
    }
}
