using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.UIControls;
using CMS.GlobalHelper;
using CMS.SettingsProvider;
using CMS.CMSHelper;
using CMS.ExtendedControls;


public partial class CMSModules_DocumentTypes_Pages_Development_HierarchicalTransformations_New : CMSModalDesignPage
{
    private bool modalDialog = false;

    protected override void OnPreInit(EventArgs e)
    {
        base.OnPreInit(e);
        RequireSite = false;

        // Page has been opened from CMSDesk
        modalDialog = QueryHelper.GetBoolean("editonlycode", false);

        if (modalDialog)
        {
            MasterPageFile = "~/CMSMasterPages/UI/Dialogs/ModalDialogPage.master";
            CurrentMaster.Title.TitleText = GetString("documenttype_edit_transformation_edit.newtransformation");
            CurrentMaster.Title.TitleImage = GetImageUrl("Design/Selectors/selecttransformation.png");

            mCurrentMaster.PanelFooter.CssClass += " FloatRight ";
            plcDocTypeFilter.Visible = true;

            // Add close button 
            mCurrentMaster.PanelFooter.Controls.Add(new LocalizedButton
            {
                ID = "btnCancel",
                ResourceString = "general.close",
                EnableViewState = false,
                OnClientClick = "window.close(); return false;",
                CssClass = "SubmitButton"
            });

            ucDocFilter.ShowCustomTableClasses = false;
        }
        else
        {
            // Page opened from Site Manager
            CheckAccessToSiteManager();
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        // Check for UI permissions
        if (!CMSContext.CurrentUser.IsAuthorizedPerUIElement("CMS.Content", new string[] { "Design", "Design.WebPartProperties", "WebPartProperties.General", "WebPartProperties.NewTransformations" }, CMSContext.CurrentSiteName))
        {
            RedirectToCMSDeskUIElementAccessDenied("CMS.Content", "Design.WebPartProperties;WebPartProperties.General;WebPartProperties.NewTransformations");
        }

        ucTransf.OnSaved += new EventHandler(ucTransf_OnSaved);

        this.CurrentMaster.Title.HelpTopicName = "newedit_hierarchical_transformation";
        this.CurrentMaster.Title.HelpName = "helpTopic";

        if (!modalDialog)
        {
            int documentTypeID = QueryHelper.GetInteger("documenttypeid", 0);
            string[,] tabs = new string[2, 4];
            tabs[0, 0] = GetString("documenttype_edit_transformation_list.title");
            tabs[0, 1] = ResolveUrl("~/CMSModules/DocumentTypes/Pages/Development/DocumentType_edit_Transformation_list.aspx?documenttypeid=" + documentTypeID);
            tabs[0, 2] = "";

            tabs[1, 0] = GetString("DocumentType_Edit_Transformation_List.btnHierarchicalNew");
            tabs[1, 1] = "";
            tabs[1, 2] = "";

            ucTransf.DocumentTypeID = documentTypeID;
            this.CurrentMaster.Title.Breadcrumbs = tabs;
        }
        else
        {
            ucTransf.DocumentTypeID = ucDocFilter.ClassId;
            pnlContainer.CssClass = "PageContent";
        }
    }


    /// <summary>
    /// Occurs when new transformation is saved.
    /// </summary>
    protected void ucTransf_OnSaved(object sender, EventArgs e)
    {
        if (!modalDialog)
        {
            //Transfer to transformation list
            URLHelper.Redirect("~/CMSModules/DocumentTypes/Pages/Development/HierarchicalTransformations_Frameset.aspx?transID=" + ucTransf.TransInfo.TransformationID + "&classid=" + ucTransf.DocumentTypeID + "&tabmode=1");
        }
        else
        {
            // Check for selector ID
            string selector = QueryHelper.GetString("selectorid", string.Empty);
            if (!string.IsNullOrEmpty(selector))
            {
                // Add selector refresh
                string script =
                    string.Format(@"var wopener = window.top.opener ? window.top.opener : window.top.dialogArguments;
		                            if (wopener) {{ wopener.US_SelectNewValue_{0}('{1}'); }}",
                                    selector, ucTransf.TransInfo.TransformationFullName);

                script += " window.name='hierarchicalproperties';  window.open('" + ResolveUrl("~/CMSModules/DocumentTypes/Pages/Development/HierarchicalTransformations_Frameset.aspx") + "?transID=" + ucTransf.TransInfo.TransformationID + "&classid=" + ucTransf.DocumentTypeID + "&editonlycode=true','hierarchicalproperties')";

                ScriptHelper.RegisterStartupScript(this, GetType(), "UpdateSelector", script, true);

                pnlContainer.Visible = false;
                plcDocTypeFilter.Visible = false;
            }
        }
    }
}

