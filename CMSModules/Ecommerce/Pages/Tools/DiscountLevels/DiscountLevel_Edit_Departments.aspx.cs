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
using CMS.CMSHelper;
using CMS.Ecommerce;
using CMS.UIControls;
using CMS.SettingsProvider;
using CMS.SiteProvider;

public partial class CMSModules_Ecommerce_Pages_Tools_DiscountLevels_DiscountLevel_Edit_Departments : CMSDiscountLevelsPage
{
    protected int mDiscountLevelId = 0;
    protected string mCurrentValues = string.Empty;
    protected DiscountLevelInfo mDiscountLevelInfoObj = null;
    protected int editedSiteId = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!CMSContext.CurrentUser.IsAuthorizedPerUIElement("CMS.Ecommerce", "DiscountLevels.Departments"))
        {
            RedirectToCMSDeskUIElementAccessDenied("CMS.Ecommerce", "DiscountLevels.Departments");
        }

        lblAvialable.Text = GetString("com.discountlevel.departmentsavailable");
        mDiscountLevelId = QueryHelper.GetInteger("discountLevelId", 0);
        editedSiteId = ConfiguredSiteID;

        if (mDiscountLevelId > 0)
        {
            mDiscountLevelInfoObj = DiscountLevelInfoProvider.GetDiscountLevelInfo(mDiscountLevelId);
            EditedObject = mDiscountLevelInfoObj;

            if (mDiscountLevelInfoObj != null)
            {
                editedSiteId = mDiscountLevelInfoObj.DiscountLevelSiteID;
                CheckEditedObjectSiteID(editedSiteId);

                // Get the active departments
                DataSet ds = DepartmentInfoProvider.GetDiscountLevelDepartments(mDiscountLevelId);
                if (!DataHelper.DataSourceIsEmpty(ds))
                {
                    mCurrentValues = TextHelper.Join(";", SqlHelperClass.GetStringValues(ds.Tables[0], "DepartmentID"));
                }

                if (!RequestHelper.IsPostBack())
                {
                    this.uniSelector.Value = mCurrentValues;
                }
            }
        }

        this.uniSelector.IconPath = GetObjectIconUrl("ecommerce.department", "object.png");
        this.uniSelector.OnSelectionChanged += uniSelector_OnSelectionChanged;
        this.uniSelector.WhereCondition = GetWhereCondition();
    }


    protected void uniSelector_OnSelectionChanged(object sender, EventArgs e)
    {
        SaveItems();
    }


    protected void SaveItems()
    {
        if (mDiscountLevelInfoObj == null)
        {
            return;
        }

        // Check module permissions
        if (!ECommerceContext.IsUserAuthorizedToModifyDiscountLevel(mDiscountLevelInfoObj))
        {
            if (mDiscountLevelInfoObj.IsGlobal)
            {
                RedirectToAccessDenied("CMS.Ecommerce", "EcommerceGlobalModify");
            }
            else
            {
                RedirectToAccessDenied("CMS.Ecommerce", "EcommerceModify OR ModifyDiscounts");
            }
        }

        // Remove old items
        string newValues = ValidationHelper.GetString(uniSelector.Value, null);
        string items = DataHelper.GetNewItemsInList(newValues, mCurrentValues);
        if (!String.IsNullOrEmpty(items))
        {
            string[] newItems = items.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            if (newItems != null)
            {
                // Add all new items to department
                foreach (string item in newItems)
                {
                    int departmentId = ValidationHelper.GetInteger(item, 0);
                    DiscountLevelDepartmentInfoProvider.RemoveDiscountLevelFromDepartment(mDiscountLevelId, departmentId);
                }
            }
        }

        // Add new items
        items = DataHelper.GetNewItemsInList(mCurrentValues, newValues);
        if (!String.IsNullOrEmpty(items))
        {
            string[] newItems = items.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            if (newItems != null)
            {
                // Add all new items to department
                foreach (string item in newItems)
                {
                    int departmentId = ValidationHelper.GetInteger(item, 0);
                    DiscountLevelDepartmentInfoProvider.AddDiscountLevelToDepartment(mDiscountLevelId, departmentId);
                }
            }
        }

        lblInfo.Visible = true;
        lblInfo.Text = GetString("General.ChangesSaved");
    }


    /// <summary>
    /// Returns where condition for uniselector. Respects settings of global departments.
    /// </summary>
    public string GetWhereCondition()
    {
        string where = "";
        // Add global items when editing global discount level or site allows global departments
        if ((editedSiteId == 0) || ECommerceSettings.AllowGlobalDepartments(SiteInfoProvider.GetSiteName(editedSiteId)))
        {
            where = SqlHelperClass.AddWhereCondition(where, "DepartmentSiteID IS NULL", "OR");
        }
        // Add site specific items, when editing site specific discount level
        if (editedSiteId > 0)
        {
            where = SqlHelperClass.AddWhereCondition(where, "DepartmentSiteID = " + editedSiteId, "OR");
        }

        // Add items which have to be on the list
        string valuesList = SqlHelperClass.GetSafeQueryString(mCurrentValues, false).Replace(';', ',');
        if (!string.IsNullOrEmpty(valuesList))
        {
            where = SqlHelperClass.AddWhereCondition(where, " DepartmentID IN (" + valuesList + ")", "OR");
        }

        return where;
    }
}
