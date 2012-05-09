using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.UIControls;
using CMS.GlobalHelper;
using CMS.SettingsProvider;
using CMS.IO;

// Set page title
[Title("", "", "newedit_transformation")]

public partial class CMSModules_DocumentTypes_Pages_Development_DocumentType_Edit_Transformation_Header : CMSModalDesignPage
{
    #region "Variables"

    private bool mDialogMode = false;

    #endregion


    #region "Page methods"

    protected override void OnPreInit(EventArgs e)
    {
        RequireSite = false;

        mDialogMode = QueryHelper.GetBoolean("editonlycode", false);

        if (mDialogMode)
        {
            MasterPageFile = "~/CMSMasterPages/UI/Dialogs/TabsHeader.master";
        }
        else
        {
            // Page opened from Site Manager
            CheckAccessToSiteManager();
        }

        base.OnPreInit(e);
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        TransformationInfo ti = null;

        if (PersistentEditedObject == null)
        {
            // Get transformation depending on whether this was invoked from a dialog or site manager
            if (mDialogMode)
            {
                string transformationName = QueryHelper.GetString("name", string.Empty);

                // Get the transformation
                PersistentEditedObject = TransformationInfoProvider.GetTransformation(transformationName);
            }
            else
            {
                int transformationId = QueryHelper.GetInteger("transformationid", 0);

                // Get the transformation
                PersistentEditedObject = TransformationInfoProvider.GetTransformation(transformationId);
            }

        }

        ti = PersistentEditedObject as TransformationInfo;

        // Initialize breadcrumbs and tabs
        if (ti != null)
        {
            if (!mDialogMode)
            {
                InitBreadcrumbs(2);
                SetBreadcrumb(0, GetString("DocumentType_Edit_Transformation_Edit.Transformations"), ResolveUrl("~/CMSModules/DocumentTypes/Pages/Development/DocumentType_Edit_Transformation_List.aspx?documenttypeid=" + ti.TransformationClassID), "_parent", null);
                SetBreadcrumb(1, ti.TransformationName, null, null, null);
            }
            else
            {
                SetTitle("Design/Selectors/selecttransformation.png", GetString("TransformationEdit.Title"), null, "helpTopic");

                string selector = QueryHelper.GetString("selectorid", string.Empty);
                if (!string.IsNullOrEmpty(selector) && RequestHelper.IsPostBack())
                {
                    // Add selector refresh
                    string script =
                        string.Format(@"var wopener = window.top.opener ? window.top.opener : window.top.dialogArguments;
		                                if (wopener && wopener.US_SelectNewValue_{0}) {{ wopener.US_SelectNewValue_{0}('{1}'); }}",
                                        selector, ti.TransformationFullName);

                    ScriptHelper.RegisterStartupScript(this, GetType(), "UpdateSelector", script, true);
                }
            }
            string helpTopic = ti.TransformationIsHierarchical ? "hierarchicaltransformation_tab" : "newedit_transformation";

            // Set page title
            DataClassInfo classObj = DataClassInfoProvider.GetDataClass(ti.TransformationClassID);
            if ((classObj != null) && classObj.ClassIsCustomTable)
            {
                helpTopic = "customtable_edit_newedit_transformation";
            }

            SetHelp(helpTopic, "helpTopic");

            // Set tabs number and ensure additional tab
            InitTabs(2, "t_edit_content");

            string url = "~/CMSModules/DocumentTypes/Pages/Development/DocumentType_Edit_Transformation_Edit.aspx" + URLHelper.Url.Query;
            if (mDialogMode)
            {
                url = URLHelper.AddParameterToUrl(url, "name", ti.TransformationFullName);
                url = URLHelper.AddParameterToUrl(url, "hash", QueryHelper.GetHash("?editonlycode=1"));
            }

            SetTab(0, (mDialogMode && ti.TransformationIsHierarchical) ? GetString("documenttype_edit_transformation_list.title") : GetString("general.general"), ResolveUrl(url), "SetHelpTopic('helpTopic', '" + helpTopic + "');");

            if (!mDialogMode && !StorageHelper.IsExternalStorage)
            {
                SetTab(1, GetString("stylesheet.theme"), ResolveUrl("~/CMSModules/DocumentTypes/Pages/Development/DocumentType_Edit_Transformation_Theme.aspx" + URLHelper.Url.Query), "SetHelpTopic('helpTopic', '" + helpTopic + "');");
            }
        }
    }

    #endregion
}

