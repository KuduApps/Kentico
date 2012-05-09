using System;

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.SettingsProvider;
using CMS.UIControls;
using CMS.SiteProvider;

public partial class CMSModules_DocumentTypes_Pages_Development_DocumentType_Edit_Transformation_Edit : CMSModalDesignPage
{
    #region "Variables"

    private bool mDialogMode;

    #endregion


    #region "Methods"

    protected override void OnPreInit(EventArgs e)
    {
        base.OnPreInit(e);

        RequireSite = false;

        // Page has been opened from CMSDesk
        mDialogMode = QueryHelper.GetBoolean("editonlycode", false);

        if (mDialogMode)
        {
            MasterPageFile = "~/CMSMasterPages/UI/Dialogs/ModalDialogPage.master";

            // Check hash
            if (!QueryHelper.ValidateHash("hash", "saved;name;selectorid;sitemanager;selectedvalue;tabmode"))
            {
                URLHelper.Redirect(ResolveUrl(string.Format("~/CMSMessages/Error.aspx?title={0}&text={1}", GetString("dialogs.badhashtitle"), GetString("dialogs.badhashtext"))));
            }
        }
        else
        {
            CheckAccessToSiteManager();
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        // Check permissions for web part properties UI
        CurrentUserInfo currentUser = CMSContext.CurrentUser;

        UserInfo ui = CMSContext.CurrentUser;

        bool isSiteManager = QueryHelper.GetBoolean("sitemanager", false);

        // Site manager may be used in dialog mode too (hierar. transformations)
        if ((isSiteManager || !mDialogMode) && ui.UserSiteManagerAdmin)
        {
            transformationEdit.IsSiteManager = true;
        }
  
        // Check if existing transformation is edited or created
        if (QueryHelper.Contains("name"))
        {
            // Check for UI permissions
            if (!currentUser.IsAuthorizedPerUIElement("CMS.Content", new string[] { "Design", "Design.WebPartProperties", "WebPartProperties.General", "WebPartProperties.EditTransformations" }, CMSContext.CurrentSiteName))
            {
                RedirectToCMSDeskUIElementAccessDenied("CMS.Content", "Design.WebPartProperties;WebPartProperties.General;WebPartProperties.EditTransformations");
            }
        }
        else
        {
            // Check for UI permissions
            if (!currentUser.IsAuthorizedPerUIElement("CMS.Content", new string[] { "Design", "Design.WebPartProperties", "WebPartProperties.General", "WebPartProperties.NewTransformations" }, CMSContext.CurrentSiteName))
            {
                RedirectToCMSDeskUIElementAccessDenied("CMS.Content", "Design.WebPartProperties;WebPartProperties.General;WebPartProperties.NewTransformations");
            }
        }

        // In case of opening via web part uni selector and transformation is hierarchical - redirect to hierarchical transformation
        string transName = QueryHelper.GetString("name", string.Empty);

        TransformationInfo hti = TransformationInfoProvider.GetTransformation(transName);
        if (hti != null)
        {
            if (hti.TransformationIsHierarchical)
            {
                URLHelper.Redirect(ResolveUrl(string.Format("~/CMSModules/DocumentTypes/Pages/Development/HierarchicalTransformations_Transformations.aspx?transID={0}&editonlycode={1}", hti.TransformationID, QueryHelper.GetInteger("editonlycode", 0))));
            }

            EditedObject = hti;
        }
        else
        {
            int transId = QueryHelper.GetInteger("transformationid", 0);
            hti = TransformationInfoProvider.GetTransformation(transId);
            if (hti != null)
            {
                EditedObject = hti;
            }
        }

        if (hti == null)
        {
            transformationEdit.EditingPage = "DocumentType_Edit_Transformation_Frameset.aspx";
        }
        else
        {
            transformationEdit.EditingPage = "DocumentType_Edit_Transformation_Edit.aspx";
        }

        // Reload header if changes were saved
        if (QueryHelper.GetInteger("saved", 0) == 1)
        {
            ScriptHelper.RefreshTabHeader(Page, null);
        }  
    }

    #endregion
}