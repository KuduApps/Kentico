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
using CMS.SettingsProvider;
using CMS.CMSHelper;
using CMS.SiteProvider;
using CMS.UIControls;

// Set title
[Title("Objects/CMS_DocumentType/objectproperties.png", "DocumentType_Edit_Header.Title", "general_tab18")]

public partial class CMSModules_DocumentTypes_Pages_Development_DocumentType_Edit_Header : SiteManagerPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int documentTypeId = QueryHelper.GetInteger("documenttypeid", 0);
        DataClassInfo classInfo = DataClassInfoProvider.GetDataClass(documentTypeId);

        // Set edited object
        SetEditedObject(classInfo, null);

        // Document type exists
        if (classInfo != null)
        {
            // Set breadcrumbs
            InitBreadcrumbs(2);
            SetBreadcrumb(0, GetString("DocumentType_Edit_Header.DocumentTypes"), ResolveUrl("~/CMSModules/DocumentTypes/Pages/Development/DocumentType_List.aspx"), "_parent", null);
            SetBreadcrumb(1, classInfo.ClassDisplayName, null, null, null);

            ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "ShowContent", ScriptHelper.GetScript("function ShowContent(contentLocation) { parent.content.location.href = contentLocation; }"));

            // Initialize menu
            InitializeMenu(classInfo);
        }
    }


    /// <summary>
    /// Initializes DocumentType edit menu.
    /// </summary>
    /// <param name="docTypeId">DocumentTypeID</param>
    protected void InitializeMenu(DataClassInfo classObj)
    {
        int i = 0;
        InitTabs(11, "content");
        SetTab(i++, GetString("general.general"), "DocumentType_Edit_General.aspx?documenttypeid=" + classObj.ClassID, "SetHelpTopic('helpTopic', 'general_tab18');");

        // If document type is conatiner
        if (classObj.ClassIsCoupledClass)
        {
            SetTab(i++, GetString("general.fields"), "DocumentType_Edit_Fields.aspx?documenttypeid=" + classObj.ClassID, "SetHelpTopic('helpTopic', 'fields_tab2');");
        }

        SetTab(i++, GetString("general.form"), "DocumentType_Edit_Form.aspx?documenttypeid=" + classObj.ClassID, "SetHelpTopic('helpTopic', 'form_tab3');");
        SetTab(i++, GetString("DocumentType_Edit_Header.Transformations"), "DocumentType_Edit_Transformation_List.aspx?documenttypeid=" + classObj.ClassID, "SetHelpTopic('helpTopic', 'transformations_tab');");
        SetTab(i++, GetString("DocumentType_Edit_Header.Queries"), "DocumentType_Edit_Query_List.aspx?documenttypeid=" + classObj.ClassID, "SetHelpTopic('helpTopic', 'queries_tab');");
        SetTab(i++, GetString("DocumentType_Edit_Header.ChildTypes"), "DocumentType_Edit_ChildTypes.aspx?documenttypeid=" + classObj.ClassID, "SetHelpTopic('helpTopic', 'child_types_tab');");
        SetTab(i++, GetString("general.sites"), "DocumentType_Edit_Sites.aspx?documenttypeid=" + classObj.ClassID, "SetHelpTopic('helpTopic', 'sites_tab7');");

        if (classObj.ClassIsProduct && ModuleEntry.IsModuleLoaded(ModuleEntry.ECOMMERCE))
        {
            SetTab(i++, GetString("DocumentType_Edit_Header.Ecommerce"), ResolveUrl("~/CMSModules/Ecommerce/Pages/Development/DocumentTypes/DocumentType_Edit_Ecommerce.aspx") + "?documenttypeid=" + classObj.ClassID, "SetHelpTopic('helpTopic', 'e_commerce_tab');");
        }

        SetTab(i++, GetString("doc.header.altforms"), ResolveUrl("~/CMSModules/DocumentTypes/Pages/AlternativeForms/AlternativeForms_List.aspx?classid=" + classObj.ClassID), "SetHelpTopic('helpTopic', 'alternative_forms');");

        // If document type is conatiner
        if (classObj.ClassIsCoupledClass)
        {
            SetTab(i++, GetString("srch.fields"), "DocumentType_Edit_SearchFields.aspx?documenttypeid=" + classObj.ClassID, "SetHelpTopic('helpTopic', 'search_fields');");
        }

        SetTab(i++, GetString("general.documents"), "DocumentType_Edit_Documents.aspx?documenttypeid=" + classObj.ClassID, "SetHelpTopic('helpTopic', 'doctype_documents');");
    }
}
