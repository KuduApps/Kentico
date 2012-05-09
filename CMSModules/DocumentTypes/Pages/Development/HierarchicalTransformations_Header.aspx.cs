using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.SettingsProvider;
using CMS.CMSHelper;
using CMS.SiteProvider;

// Set edited object
[EditedObject("cms.transformation", "transid")]

// Set help
[Help("hierarchicalTransformation_tab", "helpTopic")]

// Set number of tabs
[Tabs(2, "content")]

public partial class CMSModules_DocumentTypes_Pages_Development_HierarchicalTransformations_Header : SiteManagerPage
{
    bool mDialogMode = false;

    protected override void OnPreInit(EventArgs e)
    {
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
        ScriptHelper.RegisterDialogScript(this);

        TransformationInfo ti = EditedObject as TransformationInfo;
        if (ti != null)
        {
            UserInfo ui = CMSContext.CurrentUser;
            bool isSiteManager = QueryHelper.GetBoolean("sitemanager", false) && ui.UserSiteManagerAdmin;
            string isSiteManagerStr = isSiteManager ? "&sitemanager=true" : String.Empty;
            SetTab(0, GetString("general.transformations"), "HierarchicalTransformations_Transformations.aspx?transid=" + ti.TransformationID + "&editonlycode=" + mDialogMode + "&tabmode=" + QueryHelper.GetInteger("tabmode", 0) + isSiteManagerStr, "SetHelpTopic('helpTopic','hierarchicalTransformation_tab')");
            if (!mDialogMode)
            {
                SetTab(1, GetString("general.general"), "HierarchicalTransformations_General.aspx?transid=" + ti.TransformationID, "SetHelpTopic('helpTopic','newedit_hierarchical_transformation')");

                InitBreadcrumbs(2);
                SetBreadcrumb(0, GetString("documenttype_edit_transformation_list.title"), ResolveUrl("~/CMSModules/DocumentTypes/Pages/Development/DocumentType_edit_Transformation_list.aspx?documenttypeid=" + ti.TransformationClassID), "content", null);
                SetBreadcrumb(1, ti.TransformationName + " (" + GetString("transformation.hierarchical") + ")", null, null, null);
            }
            else
            {
                SetTitle("Design/Selectors/selecttransformation.png", GetString("transformationedit.title"), "hierarchicaltransformation_tab", "helpTopic");
            }
        }
    }
}

