using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.GlobalHelper;
using CMS.SettingsProvider;
using CMS.Controls;
using CMS.UIControls;
using CMS.CMSHelper;
using CMS.ExtendedControls;
using CMS.SiteProvider;

public partial class CMSModules_DocumentTypes_Pages_Development_HierarchicalTransformations_Transformations_Edit : CMSModalDesignPage
{
    #region "Variables"

    private bool mDialogMode = false;

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
            CurrentMaster.PanelFooter.CssClass += " FloatRight ";

            // Add close button 
            mCurrentMaster.PanelFooter.Controls.Add(new LocalizedButton
            {
                ID = "btnCancel",
                ResourceString = "general.close",
                EnableViewState = false,
                OnClientClick = "window.top.close(); return false;",
                CssClass = "SubmitButton"
            });
        }
        else
        {
            // Page opened from Site Manager
            CheckAccessToSiteManager();
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        // Check security
        CurrentUserInfo currentUser = CMSContext.CurrentUser;
        if (!currentUser.IsAuthorizedPerUIElement("CMS.Content", new string[] { "Design", "Design.WebPartProperties", "WebPartProperties.General", "WebPartProperties.EditTransformations" }, CMSContext.CurrentSiteName))
        {
            RedirectToCMSDeskUIElementAccessDenied("CMS.Content", "Design.WebPartProperties;WebPartProperties.General;WebPartProperties.EditTransformations");
        }

        UserInfo ui = CMSContext.CurrentUser;

        // If site manager set directly (or window not in dialog mode) -  set site manager flag to unigrid
        // In some cases dialog mode may be used in site manager (hier. transformation)
        bool isSiteManager = QueryHelper.GetBoolean("sitemanager", false);
        if ((isSiteManager || !mDialogMode) && ui.UserSiteManagerAdmin)
        {
            ucTransf.IsSiteManager = true;
        }

        this.CurrentMaster.Title.Visible = true;

        int transformationID = QueryHelper.GetInteger("transID", 0);
        string selectedTemplate = QueryHelper.GetString("templatetype", String.Empty);
        if (!String.IsNullOrEmpty(ucTransf.SelectedItemType))
        {
            selectedTemplate = ucTransf.SelectedItemType;
        }
        Guid guid = QueryHelper.GetGuid("guid", Guid.Empty);
        bool showInfoLabel = QueryHelper.GetBoolean("showinfo", false);

        TransformationInfo ti = TransformationInfoProvider.GetTransformation(transformationID);

        ucTransf.ShowInfoLabel = showInfoLabel;
        ucTransf.TransInfo = ti;
        ucTransf.HierarchicalID = guid;

        //Set breadcrumbs
        string[,] tabs = new string[2, 4];
        tabs[0, 0] = GetString("documenttype_edit_transformation_list.titlelist");
        tabs[0, 1] = ResolveUrl("~/CMSModules/DocumentTypes/Pages/Development/HierarchicalTransformations_Transformations.aspx?transID=" + ti.TransformationID + "&templatetype=" + selectedTemplate + "&editonlycode=" + mDialogMode + "&tabmode=" + QueryHelper.GetInteger("tabmode", 0));
        tabs[0, 2] = "";

        tabs[1, 0] = GetString("documenttype_edit_transformation_list.edit");
        tabs[1, 1] = "";
        tabs[1, 2] = "";

        this.CurrentMaster.Title.HelpTopicName = "partialhierarchicalTransformation_tab";
        this.CurrentMaster.Title.HelpName = "helpTopic";

        this.CurrentMaster.Title.Breadcrumbs = tabs;

        if (mDialogMode)
        {
            pnlContainer.CssClass = "PageContent";
        }
    }

    #endregion
}

