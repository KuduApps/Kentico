using System;
using System.Data;
using System.Collections;

using CMS.GlobalHelper;
using CMS.SettingsProvider;
using CMS.CMSHelper;
using CMS.UIControls;

public partial class CMSModules_CustomTables_Tools_CustomTable_List : CMSCustomTablesToolsPage
{
    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Initialize master page
        CurrentMaster.Title.TitleText = GetString("customtable.list.Title");
        CurrentMaster.Title.TitleImage = GetImageUrl("Objects/CMS_CustomTable/object.png");
        CurrentMaster.Title.HelpTopicName = "custom_tables_tools_list";
        CurrentMaster.Title.HelpName = "helpTopic";

        // Initialize unigrid
        uniGrid.OnAction += uniGrid_OnAction;
        uniGrid.ZeroRowsText = GetString("customtable.notable");
        uniGrid.OnAfterRetrieveData += new OnAfterRetrieveData(uniGrid_OnAfterRetrieveData);
        uniGrid.WhereCondition = "ClassID IN (SELECT ClassID FROM CMS_ClassSite WHERE SiteID = " + CMSContext.CurrentSite.SiteID + ")";
    }

    #endregion


    #region "Unigrid events"

    private DataSet uniGrid_OnAfterRetrieveData(DataSet ds)
    {
        // Check permission to each custom table if user is not autorized to read all (from module)
        if (!CMSContext.CurrentUser.IsAuthorizedPerResource("CMS.CustomTables", "Read"))
        {
            if (!DataHelper.DataSourceIsEmpty(ds))
            {
                ArrayList toDelete = new ArrayList();

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    int customtableid = ValidationHelper.GetInteger(row["ClassID"], 0);
                    DataClassInfo dci = DataClassInfoProvider.GetDataClass(customtableid);
                    if (dci != null)
                    {
                        if (!CMSContext.CurrentUser.IsAuthorizedPerClassName(dci.ClassName, "Read"))
                        {
                            toDelete.Add(row);
                        }
                    }
                }

                // Delete from DataSet
                foreach (DataRow row in toDelete)
                {
                    ds.Tables[0].Rows.Remove(row);
                }

                // Redirect to access denied page if user doesn't have permission to any custom table
                if (ds.Tables[0].Rows.Count == 0)
                {
                    MissingPermissionsRedirect();
                }
            }
            else
            {
                // Redirect to access denied page if user doesn't have permission to any custom table
                MissingPermissionsRedirect();
            }
        }

        return ds;
    }


    /// <summary>
    /// Handles the UniGrid's OnAction event.
    /// </summary>
    /// <param name="actionName">Name of item (button) that throws event</param>
    /// <param name="actionArgument">ID (value of Primary key) of corresponding data row</param>
    protected void uniGrid_OnAction(string actionName, object actionArgument)
    {
        if (actionName == "edit")
        {
            int classId = ValidationHelper.GetInteger(actionArgument, 0);
            DataClassInfo dci = DataClassInfoProvider.GetDataClass(classId);
            if (dci != null)
            {
                // Check if custom table class hasn't set specific listing page
                if (dci.ClassListPageURL != String.Empty)
                {
                    URLHelper.Redirect(dci.ClassListPageURL + "?customtableid=" + classId);
                }
                else
                {
                    URLHelper.Redirect("CustomTable_Data_List.aspx?customtableid=" + classId);
                }
            }
        }
    }

    #endregion


    #region "Other methods"

    /// <summary>
    /// Redirects to access denied page with appropriate message.
    /// </summary>
    private void MissingPermissionsRedirect()
    {
        RedirectToAccessDenied(GetString("customtable.anytablepermiss"));
    }

    #endregion
}
