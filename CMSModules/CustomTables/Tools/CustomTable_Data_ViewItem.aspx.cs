using System;

using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.SettingsProvider;
using CMS.SiteProvider;
using CMS.UIControls;

public partial class CMSModules_CustomTables_Tools_CustomTable_Data_ViewItem : CMSCustomTablesModalPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        CurrentMaster.Title.TitleText = GetString("customtable.data.viewitemtitle");
        Page.Title = CurrentMaster.Title.TitleText;

        CurrentMaster.Title.TitleImage = GetImageUrl("CMSModules/CMS_CustomTables/viewitem.png");

        // Get custom table id from url
        int customTableId = QueryHelper.GetInteger("customtableid", 0);
        // Get custom table item id
        int itemId = QueryHelper.GetInteger("itemid", 0);

        DataClassInfo dci = DataClassInfoProvider.GetDataClass(customTableId);
        // Set edited object
        EditedObject = dci;

        if (dci != null)
        {
            // Check 'Read' permission
            if (!CMSContext.CurrentUser.IsAuthorizedPerResource("cms.customtables", "Read") &&
                !CMSContext.CurrentUser.IsAuthorizedPerClassName(dci.ClassName, "Read"))
            {
                RedirectToAccessDenied("cms.customtables", "Read");
            }

            CustomTableItemProvider ctProvider = new CustomTableItemProvider(CMSContext.CurrentUser);
            CustomTableItem item = ctProvider.GetItem(itemId, dci.ClassName);
            customTableViewItem.CustomTableItem = item;
        }
    }
}
