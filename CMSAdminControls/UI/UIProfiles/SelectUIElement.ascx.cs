using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;

using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.FormControls;
using CMS.SiteProvider;
using CMS.DataEngine;
using CMS.SettingsProvider;

public partial class CMSAdminControls_UI_UIProfiles_SelectUIElement : CMSUserControl
{
    #region "Variables"

    private int mRootElementID = 0;
    private string mSubItemPrefix = "--";
    private int mModuleId = 0;
    private string mWhereCondition = null;

    GroupedDataSource gds = null;

    #endregion


    #region "Properties"

    /// <summary>
    /// Gets or sets the value (ID of the module) which will be set as a root.
    /// </summary>
    public int ModuleID
    {
        get
        {
            return this.mModuleId;
        }
        set
        {
            this.mModuleId = value;
        }
    }


    /// <summary>
    /// Gets or sets the enabled state of the control.
    /// </summary>
    public bool Enabled
    {
        get
        {
            return this.drpElements.Enabled;
        }
        set
        {
            this.drpElements.Enabled = value;
        }
    }


    /// <summary>
    /// Gets or sets the Element ID.
    /// </summary>
    public int ElementID
    {
        get
        {
            return ValidationHelper.GetInteger(this.drpElements.SelectedValue, 0);
        }
        set
        {
            this.drpElements.SelectedValue = value.ToString();
        }
    }


    /// <summary>
    /// Gets or sets the string which will be used as a prefix in order to achieve tree structure.
    /// </summary>
    public string SubItemPrefix
    {
        get
        {
            return this.mSubItemPrefix;
        }
        set
        {
            this.mSubItemPrefix = value;
        }
    }


    /// <summary>
    /// Gets or sets ID of the UIElement which will be the root of the tree structure.
    /// </summary>
    public int RootElementID
    {
        get
        {
            return this.mRootElementID;
        }
        set
        {
            this.mRootElementID = value;
        }
    }


    /// <summary>
    /// Where condition.
    /// </summary>
    public string WhereCondition
    {
        get
        {
            return this.mWhereCondition;
        }
        set
        {
            this.mWhereCondition = value;
        }
    }


    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!StopProcessing && !URLHelper.IsPostback())
        {
            int shift = -1;
            string where = "ElementResourceID = " + this.ModuleID;
            if (!String.IsNullOrEmpty(WhereCondition))
            {
                where += " AND " + WhereCondition;
            }

            // Get the data
            DataSet ds = UIElementInfoProvider.GetUIElements(where, "ElementOrder", 0, "ElementID, ElementParentID, ElementDisplayName, ElementOrder, ElementLevel");
            if (!DataHelper.DataSourceIsEmpty(ds))
            {
                gds = new GroupedDataSource(ds, "ElementParentID");

                FillDropDownList(shift, 0);
            }
        }
    }


    #region "Private methods"

    /// <summary>
    /// Fills existing UIElements in the drop down list as a tree structure.
    /// </summary>
    /// <param name="shift">Subelement offset in the DDL</param>
    /// <param name="parentCategoryID">ID of the parent element</param>
    protected void FillDropDownList(int shift, int parentElementId)
    {
        ArrayList items = null;
        if (parentElementId > 0)
        {
            items = gds.GetGroupView(parentElementId);
        }
        else
        {
            items = gds.GetGroupView(DBNull.Value);
        }
        if (items != null)
        {
            shift++;

            foreach (DataRowView dr in items)
            {
                ListItem item = new ListItem();

                // Append prefix
                for (int i = 0; i < shift; i++)
                {
                    item.Text += this.SubItemPrefix;
                }

                int elementId = ValidationHelper.GetInteger(dr["ElementID"], 0);
                string elementName = ValidationHelper.GetString(dr["ElementDisplayName"], "");
                string elementDisplayName = ValidationHelper.GetString(dr["ElementDisplayName"], "");

                item.Text += ResHelper.LocalizeString(elementDisplayName);
                item.Value = elementId.ToString();

                // Add item to the DLL
                this.drpElements.Items.Add(item);

                // Call to add the subitems
                FillDropDownList(shift, elementId);
            }
        }
    }

    #endregion
}
