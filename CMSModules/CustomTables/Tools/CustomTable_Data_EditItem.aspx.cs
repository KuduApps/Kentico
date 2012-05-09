using System;

using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.SettingsProvider;
using CMS.UIControls;

public partial class CMSModules_CustomTables_Tools_CustomTable_Data_EditItem : CMSCustomTablesToolsPage
{
    protected void Page_Init(object sender, EventArgs e)
    {
        this.RequireSite = false;
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        bool accessGranted = true;
        CurrentMaster.Title.HelpTopicName = "custom_tables_edit_item";
        CurrentMaster.Title.HelpName = "helpTopic";

        // Get custom table id from url
        int customTableId = QueryHelper.GetInteger("customtableid", 0);

        if (customTableId > 0)
        {
            // Get custom table item id
            int itemId = QueryHelper.GetInteger("itemid", 0);

            // Running in site manager?
            bool siteManager = QueryHelper.GetInteger("sm", 0) == 1;

            string currentItem = string.Empty;

            DataClassInfo dci = DataClassInfoProvider.GetDataClass(customTableId);
            // Set edited object
            EditedObject = dci;

            // If class exists
            if (dci != null)
            {
                // Edit item
                if (itemId > 0)
                {
                    // Check 'Read' permission
                    if (!CMSContext.CurrentUser.IsAuthorizedPerResource("cms.customtables", "Read") &&
                        !CMSContext.CurrentUser.IsAuthorizedPerClassName(dci.ClassName, "Read"))
                    {
                        lblError.Visible = true;
                        lblError.Text = String.Format(GetString("customtable.permissiondenied.read"), dci.ClassName);
                        plcContent.Visible = false;
                        accessGranted = false;
                    }

                    currentItem = GetString("customtable.data.Edititem");
                    if (!siteManager)
                    {
                        CurrentMaster.Title.TitleText = GetString("customtable.data.edititemtitle");
                        CurrentMaster.Title.TitleImage = GetImageUrl("CMSModules/CMS_CustomTables/edititem.png");
                    }
                }
                // New item
                else
                {
                    currentItem = GetString("customtable.data.NewItem");
                    if (!siteManager)
                    {
                        CurrentMaster.Title.TitleText = GetString("customtable.data.newitemtitle");
                        CurrentMaster.Title.TitleImage = GetImageUrl("CMSModules/CMS_CustomTables/newitem24.png");
                    }
                }

                string listPage = "~/CMSModules/Customtables/Tools/CustomTable_Data_List.aspx";
                string newItemPage = "~/CMSModules/CustomTables/Tools/CustomTable_Data_EditItem.aspx";

                // Set custom pages
                if (dci.ClassListPageURL != String.Empty)
                {
                    listPage = dci.ClassListPageURL;
                }
                else if (dci.ClassNewPageURL != String.Empty)
                {
                    newItemPage = dci.ClassNewPageURL;
                }

                if (QueryHelper.GetString("saved", String.Empty) != String.Empty)
                {
                    // If this was creating of new item show the link again
                    if ((QueryHelper.GetString("new", String.Empty) != String.Empty))
                    {
                        string[,] actions = new string[1, 6];
                        // New item link
                        actions[0, 0] = HeaderActions.TYPE_HYPERLINK;
                        actions[0, 1] = GetString("customtable.data.createanother");
                        actions[0, 2] = null;
                        actions[0, 3] = ResolveUrl(newItemPage + "?new=1&customtableid=" + customTableId + (siteManager ? "&sm=1" : String.Empty));
                        actions[0, 4] = null;
                        actions[0, 5] = GetImageUrl("CMSModules/CMS_CustomTables/newitem.png");

                        CurrentMaster.HeaderActions.Actions = actions;
                    }
                }

                // Initializes page title
                string[,] breadcrumbs = new string[2, 3];
                breadcrumbs[0, 0] = GetString("general.data");
                breadcrumbs[0, 1] = listPage + "?customtableid=" + customTableId + (siteManager ? "&sm=1" : String.Empty);
                breadcrumbs[0, 2] = string.Empty;
                breadcrumbs[1, 0] = currentItem;
                breadcrumbs[1, 1] = string.Empty;
                breadcrumbs[1, 2] = string.Empty;

                CurrentMaster.Title.Breadcrumbs = breadcrumbs;

                // Set edit form
                if (accessGranted)
                {
                    customTableForm.CustomTableId = customTableId;
                    customTableForm.ItemId = itemId;
                    customTableForm.EditItemPageAdditionalParams = (siteManager ? "sm=1" : String.Empty);
                }
            }
        }
    }
}
