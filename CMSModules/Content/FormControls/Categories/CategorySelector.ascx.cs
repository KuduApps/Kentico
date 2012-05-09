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
using CMS.FormControls;
using CMS.UIControls;
using CMS.CMSHelper;

public partial class CMSModules_Content_FormControls_Categories_CategorySelector : FormEngineUserControl
{
    #region "Variables"

    private bool mUseCategoryNameForSelection = true;

    #endregion


    #region "Properties"

    /// <summary>
    /// Gets or sets the enabled state of the control.
    /// </summary>
    public override bool Enabled
    {
        get
        {
            return base.Enabled;
        }
        set
        {
            base.Enabled = value;

        }
    }


    /// <summary>
    /// Gets or sets the field value.
    /// </summary>
    public override object Value
    {
        get
        {
            // Decide which property to get
            if (this.mUseCategoryNameForSelection)
            {
                return this.CategoryName;
            }
            else
            {
                return this.CategoryID;
            }
        }
        set
        {
            // Decide which property to set
            if (this.mUseCategoryNameForSelection)
            {
                this.CategoryName = ValidationHelper.GetString(value, "");
            }
            else
            {
                this.CategoryID = ValidationHelper.GetInteger(value, 0);
            }
        }
    }


    /// <summary>
    /// Gets or sets the Category ID.
    /// </summary>
    public int CategoryID
    {
        get
        {
            if (this.mUseCategoryNameForSelection)
            {
                // Convert ID to name
                string name = ValidationHelper.GetString(this.selectCategory.Value, "");
                CategoryInfo ngi = CategoryInfoProvider.GetCategoryInfo(name, CMSContext.CurrentSiteName);
                if (ngi != null)
                {
                    return ngi.CategoryID;
                }
                return 0;
            }
            else
            {
                return ValidationHelper.GetInteger(this.selectCategory.Value, 0);
            }
        }
        set
        {
            if (selectCategory == null)
            {
                this.pnlUpdate.LoadContainer();
            }

            if (this.mUseCategoryNameForSelection)
            {
                // Covnert ID to name
                CategoryInfo ngi = CategoryInfoProvider.GetCategoryInfo(value);
                if (ngi != null)
                {
                    this.selectCategory.Value = ngi.CategoryName;
                }
            }
            else
            {
                this.selectCategory.Value = value;
            }
        }
    }


    /// <summary>
    /// Gets or sets the Category code name.
    /// </summary>
    public string CategoryName
    {
        get
        {
            if (this.mUseCategoryNameForSelection)
            {
                return ValidationHelper.GetString(this.selectCategory.Value, "");
            }
            else
            {
                // Convert id to name
                int id = ValidationHelper.GetInteger(this.selectCategory.Value, 0);
                CategoryInfo ngi = CategoryInfoProvider.GetCategoryInfo(id);
                if (ngi != null)
                {
                    return ngi.CategoryName;
                }
                return "";
            }
        }
        set
        {
            if (selectCategory == null)
            {
                this.pnlUpdate.LoadContainer();
            }

            if (this.mUseCategoryNameForSelection)
            {
                this.selectCategory.Value = value;
            }
            else
            {
                // Convert name to ID
                CategoryInfo ngi = CategoryInfoProvider.GetCategoryInfo(value, CMSContext.CurrentSiteName);
                if (ngi != null)
                {
                    this.selectCategory.Value = ngi.CategoryID;
                }
            }
        }
    }


    /// <summary>
    ///  If true, selected value is CategoryName, if false, selected value is CategoryID.
    /// </summary>
    public bool UseCategoryNameForSelection
    {
        get
        {
            return mUseCategoryNameForSelection;
        }
        set
        {
            mUseCategoryNameForSelection = value;
            if (this.selectCategory != null)
            {
                this.selectCategory.ReturnColumnName = (value ? "CategoryName" : "CategoryID");
            }
        }
    }


    public bool DisplayPersonalCategories
    {
        get
        {
            return ValidationHelper.GetBoolean(GetValue("DisplayPersonalCategories"), true);
        }
        set
        {
            SetValue("DisplayPersonalCategories", value);

            if (!string.IsNullOrEmpty(selectCategory.DisabledItems))
            {
                selectCategory.DisabledItems = selectCategory.DisabledItems.Replace("personal", "");
            }
            selectCategory.DisabledItems += value ? "" : "personal";
        }
    }


    public bool DisplayGeneralCategories
    {
        get
        {
            return ValidationHelper.GetBoolean(GetValue("DisplayGeneralCategories"), true);
        }
        set
        {
            SetValue("DisplayGeneralCategories", value);

            if (!string.IsNullOrEmpty(selectCategory.DisabledItems))
            {
                selectCategory.DisabledItems = selectCategory.DisabledItems.Replace("globalandsite", "");
            }
            selectCategory.DisabledItems += value ? "" : "globalandsite";
        }
    }

    #endregion


    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.StopProcessing)
        {
            this.selectCategory.StopProcessing = true;
        }
        else
        {
            ReloadData();
        }
    }

    #endregion


    #region "Method"

    /// <summary>
    /// Reloads the data in the selector.
    /// </summary>
    public void ReloadData()
    {
        if (!this.StopProcessing)
        {
            // Propagate options
            selectCategory.IsLiveSite = this.IsLiveSite;
            selectCategory.Enabled = Enabled;
            selectCategory.GridName = "~/CMSModules/Categories/Controls/Categories.xml";
            selectCategory.OnAdditionalDataBound += new CMSAdminControls_UI_UniSelector_UniSelector.AdditionalDataBoundEventHandler(selectCategory_OnAdditionalDataBound);

            // Init disabled items property
            selectCategory.DisabledItems = selectCategory.DisabledItems ?? "";
            
            if (!string.IsNullOrEmpty(selectCategory.DisabledItems))
            {
                selectCategory.DisabledItems = selectCategory.DisabledItems.Replace("personal", "");
            }
            selectCategory.DisabledItems += DisplayPersonalCategories ? "" : "personal";

            if (!string.IsNullOrEmpty(selectCategory.DisabledItems))
            {
                selectCategory.DisabledItems = selectCategory.DisabledItems.Replace("globalandsite", "");
            }
            selectCategory.DisabledItems += DisplayGeneralCategories ? "" : "globalandsite";


            // Select appropriate dialog window
            if (this.IsLiveSite)
            {
                selectCategory.SelectItemPageUrl = "~/CMSModules/Categories/CMSPages/LiveCategorySelection.aspx";
            }
            else
            {
                selectCategory.SelectItemPageUrl = "~/CMSModules/Categories/Dialogs/CategorySelection.aspx";
            }
        }
    }


    protected object selectCategory_OnAdditionalDataBound(object sender, string sourceName, object parameter, string value)
    {
        switch (sourceName.ToLowerInvariant())
        {
            // Localize category name
            case "name":
                string namePath = parameter as string;
                if (!string.IsNullOrEmpty(namePath))
                {
                    value = HTMLHelper.HTMLEncode(ResHelper.LocalizeString(namePath));
                }

                break;
        }

        return value;
    }

    #endregion
}
