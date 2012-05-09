using System;
using System.Data;
using System.Web.UI.WebControls;

using CMS.GlobalHelper;
using CMS.SettingsProvider;
using CMS.SiteProvider;
using CMS.UIControls;

public partial class CMSModules_CustomTables_CustomTable_List : CMSCustomTablesPage
{
    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        CurrentMaster.Title.TitleText = GetString("customtable.list.Title");
        CurrentMaster.Title.TitleImage = GetImageUrl("Objects/CMS_CustomTable/object.png");
        CurrentMaster.Title.HelpTopicName = "custom_tables_list";
        CurrentMaster.Title.HelpName = "helpTopic";

        // New item link
        string[,] actions = new string[1, 6];
        actions[0, 0] = HeaderActions.TYPE_HYPERLINK;
        actions[0, 1] = GetString("customtable.list.NewCustomTable");
        actions[0, 2] = null;
        actions[0, 3] = ResolveUrl("CustomTable_New.aspx");
        actions[0, 4] = null;
        actions[0, 5] = GetImageUrl("Objects/CMS_CustomTable/add.png");
        CurrentMaster.HeaderActions.Actions = actions;

        // Initialize grid
        uniGrid.OnAction += uniGrid_OnAction;
        uniGrid.ZeroRowsText = GetString("general.nodatafound");
    }

    #endregion


    #region "Grid events"


    /// <summary>
    /// Handles the UniGrid's OnAction event.
    /// </summary>
    /// <param name="actionName">Name of item (button) that throws event</param>
    /// <param name="actionArgument">ID (value of Primary key) of corresponding data row</param>
    protected void uniGrid_OnAction(string actionName, object actionArgument)
    {
        if (actionName == "edit")
        {
            URLHelper.Redirect("CustomTable_Edit.aspx?customtableid=" + actionArgument);
        }
        else if (actionName == "delete")
        {
            int classId = ValidationHelper.GetInteger(actionArgument, 0);

            if (classId > 0)
            {
                // If no item depends on the current class
                if (!DataClassInfoProvider.CheckDependencies(classId))
                {
                    // Delete the class
                    DataClassInfoProvider.DeleteDataClass(classId);
                    CustomTableItemProvider.ClearCustomLicHash();
                }
            }
            else
            {
                // Display error on deleting
                lblError.Visible = true;
                lblError.Text = GetString("customtable.delete.hasdependencies");
            }
        }
    }

    #endregion
}
