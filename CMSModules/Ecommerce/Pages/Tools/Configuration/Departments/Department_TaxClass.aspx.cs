using System;
using System.Data;

using CMS.GlobalHelper;
using CMS.Ecommerce;
using CMS.CMSHelper;
using CMS.UIControls;
using CMS.SettingsProvider;
using CMS.SiteProvider;

public partial class CMSModules_Ecommerce_Pages_Tools_Configuration_Departments_Department_TaxClass : CMSDepartmentsPage
{
    protected int mDepartmentId = 0;
    protected string mCurrentValues = string.Empty;
    protected DepartmentInfo mDepartmentInfoObj = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        // Check permissions for CMS Desk -> Ecommerce
        if (!CMSContext.CurrentUser.IsAuthorizedPerUIElement("CMS.Ecommerce", "Configuration.Departments.TaxClasses"))
        {
            RedirectToCMSDeskUIElementAccessDenied("CMS.Ecommerce", "Configuration.Departments.TaxClasses");
        }

        lblAvialable.Text = GetString("com.department.defaulttaxes");
        mDepartmentId = QueryHelper.GetInteger("departmentid", 0);
        if (mDepartmentId > 0)
        {
            mDepartmentInfoObj = DepartmentInfoProvider.GetDepartmentInfo(mDepartmentId);
            EditedObject = mDepartmentInfoObj;

            if (mDepartmentInfoObj != null)
            {
                CheckEditedObjectSiteID(mDepartmentInfoObj.DepartmentSiteID);

                DataSet ds = TaxClassInfoProvider.GetDepartmentTaxClasses(mDepartmentId);
                if (!DataHelper.DataSourceIsEmpty(ds))
                {
                    mCurrentValues = TextHelper.Join(";", SqlHelperClass.GetStringValues(ds.Tables[0], "TaxClassID"));
                }

                if (!RequestHelper.IsPostBack())
                {
                    uniSelector.Value = mCurrentValues;
                }
            }
        }

        uniSelector.IconPath = GetObjectIconUrl("ecommerce.taxclass", "object.png");
        uniSelector.OnSelectionChanged += uniSelector_OnSelectionChanged;
        uniSelector.WhereCondition = GetSelectorWhereCondition();
    }


    protected void uniSelector_OnSelectionChanged(object sender, EventArgs e)
    {
        SaveItems();
    }


    protected void SaveItems()
    {
        if (mDepartmentInfoObj == null)
        {
            return;
        }

        // Check permissions
        CheckConfigurationModification(mDepartmentInfoObj.DepartmentSiteID);
        
        // Remove old items
        string newValues = ValidationHelper.GetString(uniSelector.Value, null);
        string items = DataHelper.GetNewItemsInList(newValues, mCurrentValues);
        if (!String.IsNullOrEmpty(items))
        {
            string[] newItems = items.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            if (newItems != null)
            {
                // Add all new items to user
                foreach (string item in newItems)
                {
                    int taxClassId = ValidationHelper.GetInteger(item, 0);
                    DepartmentTaxClassInfoProvider.RemoveTaxClassFromDepartment(taxClassId, mDepartmentId);
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
                // Add all new items to user
                foreach (string item in newItems)
                {
                    int taxClassId = ValidationHelper.GetInteger(item, 0);
                    DepartmentTaxClassInfoProvider.AddTaxClassToDepartment(taxClassId, mDepartmentId);
                }
            }
        }

        lblInfo.Visible = true;
        lblInfo.Text = GetString("General.ChangesSaved");
    }


    /// <summary>
    /// Returns where condition for uniselector. This condition filters records contained in currently selected values
    /// and site-specific records according to edited objects site ID.
    /// </summary>
    protected string GetSelectorWhereCondition()
    {
        // Select nothing
        string where = "";

        // Add records which are used by parent object
        if (!string.IsNullOrEmpty(mCurrentValues))
        {
            where = SqlHelperClass.AddWhereCondition(where, "TaxClassID IN (" + mCurrentValues.Replace(';', ',') + ")", "OR");
        }

        int taxSiteId = 0;
        // Add site specific records when editing site shipping option and not using global tax classes
        if ((mDepartmentInfoObj != null) && (mDepartmentInfoObj.DepartmentSiteID > 0))
        {
            if (!ECommerceSettings.UseGlobalTaxClasses(SiteInfoProvider.GetSiteName(mDepartmentInfoObj.DepartmentSiteID)))
            {
                taxSiteId = mDepartmentInfoObj.DepartmentSiteID;
            }
        }

        where = SqlHelperClass.AddWhereCondition(where, "ISNULL(TaxClassSiteID, 0) = " + taxSiteId, "OR");

        return where;
    }
}
