using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.UIControls;
using CMS.GlobalHelper;
using CMS.Controls;
using CMS.SettingsProvider;
using CMS.CMSHelper;
using CMS.ExtendedControls;
using CMS.SiteProvider;

[EditedObject("cms.transformation", "transid")]

public partial class CMSModules_DocumentTypes_Pages_Development_HierarchicalTransformations_Transformations : CMSModalDesignPage
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

            // Add close button every time
            CurrentMaster.PanelFooter.Controls.Add(new LocalizedButton
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

        this.CurrentMaster.Title.HelpTopicName = "partialhierarchicaltransformation_tab";
        this.CurrentMaster.Title.HelpName = "helpTopic";
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        // Check for UI permissions
        if (!CMSContext.CurrentUser.IsAuthorizedPerUIElement("CMS.Content", new string[] { "Design", "Design.WebPartProperties", "WebPartProperties.General", "WebPartProperties.EditTransformations" }, CMSContext.CurrentSiteName))
        {
            RedirectToCMSDeskUIElementAccessDenied("CMS.Content", "Design.WebPartProperties;WebPartProperties.General;WebPartProperties.EditTransformations");
        }
        UserInfo ui = CMSContext.CurrentUser;
        ucNewTransf.IsSiteManager = !mDialogMode && ui.UserSiteManagerAdmin;
        CurrentMaster.Title.Visible = true;

        int transformationID = QueryHelper.GetInteger("transID", 0);
        bool add = QueryHelper.GetBoolean("add", false);
        string templateType = QueryHelper.GetString("templatetype", "all");

        if (!RequestHelper.IsPostBack())
        {
            // Load transformations
            drpTransformations.Items.Add(new ListItem(GetString("hiertransf.all"), "all"));
            drpTransformations.Items.Add(new ListItem(GetString("hiertransf.item"), "item"));
            drpTransformations.Items.Add(new ListItem(GetString("hiertransf.alternatingitem"), "alternatingitem"));
            drpTransformations.Items.Add(new ListItem(GetString("hiertransf.firstitem"), "firstitem"));
            drpTransformations.Items.Add(new ListItem(GetString("hiertransf.lastitem"), "lastitem"));
            drpTransformations.Items.Add(new ListItem(GetString("hiertransf.header"), "header"));
            drpTransformations.Items.Add(new ListItem(GetString("hiertransf.footer"), "footer"));
            drpTransformations.Items.Add(new ListItem(GetString("hiertransf.singleitem"), "singleitem"));
            drpTransformations.Items.Add(new ListItem(GetString("hiertransf.separator"), "separator"));
            drpTransformations.Items.Add(new ListItem(GetString("hiertransf.currentitem"), "currentitem"));
        }

        // Try to load hierarchical transformations
        TransformationInfo ti = TransformationInfoProvider.GetTransformation(transformationID);

        ucNewTransf.TransInfo = ti;
        ucTransf.TransInfo = ti;
        ucTransf.DialogMode = mDialogMode;

        if (!String.IsNullOrEmpty(drpTransformations.SelectedValue))
        {
            templateType = drpTransformations.SelectedValue;
        }
        else
        {
            drpTransformations.SelectedValue = templateType;
        }

        ucTransf.TemplateType = templateType;

        if (!add)
        {
            // Show new transformation link
            string[,] actions = new string[2, 6];
            actions[0, 0] = HeaderActions.TYPE_HYPERLINK;
            actions[0, 1] = GetString("DocumentType_Edit_Transformation_List.btnAdd");
            actions[0, 2] = null;
            actions[0, 3] = ResolveUrl("HierarchicalTransformations_Transformations.aspx?add=true&transID=" + transformationID.ToString() + "&templatetype=" + templateType + "&editonlycode=" + mDialogMode + "&tabmode=" + QueryHelper.GetInteger("tabmode", 0));
            actions[0, 5] = GetImageUrl("Objects/CMS_Transformation/hierarchicalTransformation.png");

            this.CurrentMaster.HeaderActions.Actions = actions;
            this.CurrentMaster.DisplayControlsPanel = true;
        }
        else
        {
            // Set breadcrumbs
            string[,] tabs = new string[2, 4];
            tabs[0, 0] = GetString("documenttype_edit_transformation_list.titlelist");
            tabs[0, 1] = ResolveUrl("HierarchicalTransformations_Transformations.aspx?transID=" + transformationID.ToString() + "&templatetype=" + templateType + "&editonlycode=" + mDialogMode + "&tabmode=" + QueryHelper.GetInteger("tabmode", 0));
            tabs[0, 2] = "";

            tabs[1, 0] = GetString("documenttype_edit_transformation_list.btnAdd");
            tabs[1, 1] = "";
            tabs[1, 2] = "";

            ucNewTransf.Visible = true;
            ucTransf.Visible = false;

            ucNewTransf.OnSaved += new EventHandler(ucNewTransf_OnSaved);
            this.CurrentMaster.Title.Breadcrumbs = tabs;
        }

        // Set proper position for templates list
        if (!RequestHelper.IsPostBack())
        {
            if (templateType != String.Empty)
            {
                ucTransf.TemplateType = templateType;
            }
        }

        if (mDialogMode)
        {
            pnlContainer.CssClass = "PageContent";
        }
    }


    /// <summary>
    /// Occurs when new hierarchical transformation is saved.
    /// </summary>    
    void ucNewTransf_OnSaved(object sender, EventArgs e)
    {
        // Redirect to edit page whit param show info true
        URLHelper.Redirect("HierarchicalTransformations_Transformations_Edit.aspx?showinfo=true&guid=" + ucNewTransf.HierarchicalID + "&transid=" + ucNewTransf.TransInfo.TransformationID + "&templatetype=" + ucTransf.TemplateType + "&editonlycode=" + mDialogMode + "&tabmode=" + QueryHelper.GetInteger("tabmode", 0));
    }

    #endregion
}

