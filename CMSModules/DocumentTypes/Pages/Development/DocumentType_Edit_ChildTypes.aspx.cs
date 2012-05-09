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
using CMS.SettingsProvider;
using CMS.SiteProvider;
using CMS.UIControls;

public partial class CMSModules_DocumentTypes_Pages_Development_DocumentType_Edit_ChildTypes : SiteManagerPage
{
    #region "Variables"

    protected static int classId = 0;

    protected string currentValues = "";
    protected string parentValues = "";

    #endregion


    #region "Page Events"

    protected void Page_Load(object sender, EventArgs e)
    {
        classId = QueryHelper.GetInteger("documenttypeid", 0);
        if (classId > 0)
        {
            // Get the active child classes
            DataSet ds = AllowedChildClassInfoProvider.GetAllowedChildClasses("ParentClassID = " + classId, null);
            if (!DataHelper.DataSourceIsEmpty(ds))
            {
                currentValues = TextHelper.Join(";", SqlHelperClass.GetStringValues(ds.Tables[0], "ChildClassID"));
            }

            if (!RequestHelper.IsPostBack())
            {
                this.uniSelector.Value = currentValues;
            }

            uniSelector.ResourcePrefix = "allowedclasscontrol";
            uniSelector.DisplayNameFormat = "{%ClassDisplayName%} ({%ClassName%})";
            uniSelector.OnSelectionChanged += new EventHandler(uniSelector_OnSelectionChanged);

            // Get the active child classes
            ds = AllowedChildClassInfoProvider.GetAllowedChildClasses("ChildClassID = " + classId, null);
            if (!DataHelper.DataSourceIsEmpty(ds))
            {
                parentValues = TextHelper.Join(";", SqlHelperClass.GetStringValues(ds.Tables[0], "ParentClassID"));
            }

            if (!RequestHelper.IsPostBack())
            {
                this.selParent.Value = parentValues;
            }

            selParent.ResourcePrefix = "allowedclasscontrol";
            selParent.DisplayNameFormat = "{%ClassDisplayName%} ({%ClassName%})";
            selParent.OnSelectionChanged += new EventHandler(selParent_OnSelectionChanged);
        }
    }


    protected void uniSelector_OnSelectionChanged(object sender, EventArgs e)
    {
        // Remove old items
        string newValues = ValidationHelper.GetString(uniSelector.Value, null);
        string items = DataHelper.GetNewItemsInList(newValues, currentValues);
        if (!String.IsNullOrEmpty(items))
        {
            string[] newItems = items.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            if (newItems != null)
            {
                // Add all new items to site
                foreach (string item in newItems)
                {
                    int childId = ValidationHelper.GetInteger(item, 0);
                    AllowedChildClassInfoProvider.RemoveAllowedChildClass(classId, childId);
                }
            }
        }

        // Add new items
        items = DataHelper.GetNewItemsInList(currentValues, newValues);
        if (!String.IsNullOrEmpty(items))
        {
            string[] newItems = items.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            if (newItems != null)
            {
                // Add all new items to site
                foreach (string item in newItems)
                {
                    int childId = ValidationHelper.GetInteger(item, 0);
                    AllowedChildClassInfoProvider.AddAllowedChildClass(classId, childId);
                }
            }
        }

        lblInfo.Visible = true;
        lblInfo.Text = GetString("general.changessaved");
    }


    protected void selParent_OnSelectionChanged(object sender, EventArgs e)
    {
        // Remove old items
        string newValues = ValidationHelper.GetString(selParent.Value, null);
        string items = DataHelper.GetNewItemsInList(newValues, parentValues);
        if (!String.IsNullOrEmpty(items))
        {
            string[] newItems = items.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            if (newItems != null)
            {
                // Add all new items to site
                foreach (string item in newItems)
                {
                    int parentId = ValidationHelper.GetInteger(item, 0);
                    AllowedChildClassInfoProvider.RemoveAllowedChildClass(parentId, classId);
                }
            }
        }

        // Add new items
        items = DataHelper.GetNewItemsInList(parentValues, newValues);
        if (!String.IsNullOrEmpty(items))
        {
            string[] newItems = items.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            if (newItems != null)
            {
                // Add all new items to site
                foreach (string item in newItems)
                {
                    int parentId = ValidationHelper.GetInteger(item, 0);
                    AllowedChildClassInfoProvider.AddAllowedChildClass(parentId, classId);
                }
            }
        }

        lblInfo.Visible = true;
        lblInfo.Text = GetString("general.changessaved");
    }
    #endregion
}
