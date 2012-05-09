using System;

using CMS.GlobalHelper;
using CMS.SettingsProvider;
using CMS.UIControls;

// Set title
[Title("Objects/CMS_CustomTable/object.png", "customtable.edit.header", "CustomTable_Edit_General")]

public partial class CMSModules_CustomTables_CustomTable_Edit_Header : CMSCustomTablesPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int customTableId = QueryHelper.GetInteger("customTableId", 0);

        if (customTableId > 0)
        {
            DataClassInfo classInfo = DataClassInfoProvider.GetDataClass(customTableId);

            // Set edited object
            SetEditedObject(classInfo, null);

            // If class exists
            if (classInfo != null)
            {
                // Initializes PageTitle
                InitBreadcrumbs(2);
                SetBreadcrumb(0, GetString("customtable.list.title"), ResolveUrl("~/CMSModules/CustomTables/CustomTable_List.aspx"), "_parent", null);
                SetBreadcrumb(1, classInfo.ClassDisplayName, null, null, null);

                ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "ShowContent", ScriptHelper.GetScript("function ShowContent(contentLocation) { parent.content.location.href = contentLocation; }"));

                // Initialize menu
                InitializeMenu(classInfo);
            }
        }
    }


    /// <summary>
    /// Initializes CustomTable edit menu.
    /// </summary>    
    protected void InitializeMenu(DataClassInfo classObj)
    {
        InitTabs(9, "content");
        SetTab(0, GetString("general.general"), "CustomTable_Edit_General.aspx?customtableid=" + classObj.ClassID, "SetHelpTopic('helpTopic', 'CustomTable_Edit_General');");
        SetTab(1, GetString("general.fields"), "CustomTable_Edit_Fields.aspx?customtableid=" + classObj.ClassID, "SetHelpTopic('helpTopic', 'CustomTable_Edit_Fields');");
        SetTab(2, GetString("general.form"), "CustomTable_Edit_Form.aspx?customtableid=" + classObj.ClassID, "SetHelpTopic('helpTopic', 'CustomTable_Edit_Form');");
        SetTab(3, GetString("general.Transformations"), "CustomTable_Edit_Transformation_List.aspx?customtableid=" + classObj.ClassID, "SetHelpTopic('helpTopic', 'CustomTable_Edit_Transformation_List');");
        SetTab(4, GetString("general.Queries"), "CustomTable_Edit_Query_List.aspx?customtableid=" + classObj.ClassID, "SetHelpTopic('helpTopic', 'CustomTable_Edit_Query_List');");
        SetTab(5, GetString("general.sites"), "CustomTable_Edit_Sites.aspx?customtableid=" + classObj.ClassID, "SetHelpTopic('helpTopic', 'CustomTable_Edit_Sites');");
        SetTab(6, GetString("customtable.header.altforms"), "AlternativeForms/AlternativeForms_List.aspx?classid=" + classObj.ClassID, "SetHelpTopic('helpTopic', 'alternative_forms');");
        SetTab(7, GetString("srch.fields"), "CustomTable_Edit_SearchFields.aspx?classid=" + classObj.ClassID, "SetHelpTopic('helpTopic', 'CustomTable_Edit_SearchFields');");

        string dataListPage = null;
        // If is specific (custom) list page defined, add query parameters for list page (table ID + site manager flag)
        if (!String.IsNullOrEmpty(classObj.ClassListPageURL))
        {
            // Use specific list page
            dataListPage = URLHelper.AddParameterToUrl(classObj.ClassListPageURL.Trim(), "customtableid", classObj.ClassID.ToString());
            dataListPage = URLHelper.AddParameterToUrl(dataListPage, "sm", "1");
        }
        else
        {
            // Use default list page
            dataListPage = "Tools/CustomTable_Data_List.aspx?sm=1&customtableid=" + classObj.ClassID;
        }

        SetTab(8, GetString("customtable.header.data"), dataListPage, "SetHelpTopic('helpTopic', 'custom_tables_data');");
    }
}
