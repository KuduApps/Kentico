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
using CMS.TreeEngine;
using CMS.CMSHelper;
using CMS.SiteProvider;
using CMS.SettingsProvider;
using CMS.PortalControls;
using CMS.PortalEngine;
using CMS.Synchronization;
using System.Text;

public partial class CMSModules_PortalEngine_Controls_Layout_PlaceholderMenu : CMSAbstractPortalUserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Use UI culture for strings
        string culture = CMSContext.CurrentUser.PreferredUICultureCode;

        // Register dialog script
        ScriptHelper.RegisterDialogScript(Page);

        // Prepare script to display versions dialog
        StringBuilder script = new StringBuilder();
        script.Append(
@"
function ShowVersionsDialog(objectType, objectId, objectName) {
  modalDialog('", ResolveUrl("~/CMSModules/Objects/Dialogs/ObjectVersionDialog.aspx"), @"' + '?objecttype=' + objectType + '&objectid=' + objectId + '&objectname=' + objectName,'VersionsDialog','800','600');
}"
        );

        ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "ShowVersionDialog", ScriptHelper.GetScript(script.ToString()));

        // Check if template is ASPX one and initialize PageTemplateInfo variable
        bool isAspx = false;
        PageInfo pi = null;
        PageTemplateInfo pti = null;

        if (mPagePlaceholder != null)
        {
            pi = mPagePlaceholder.PageInfo;
        }

        if (pi != null)
        {
            pti = pi.PageTemplateInfo;
            if (pti != null)
            {
                isAspx = pti.IsAspx;
            }
        }

        if ((mPagePlaceholder != null) && (mPagePlaceholder.ViewMode == ViewModeEnum.DesignDisabled))
        {
            // Hide edit layout and edit template if design mode is disabled
            this.pnlLayout.Visible = false;
            this.pnlTemplate.Visible = false;
        }
        else
        {
            if ((mPagePlaceholder != null) && (mPagePlaceholder.LayoutTemplate == null))
            {
                // Edit layout
                this.imgLayout.ImageUrl = GetImageUrl("CMSModules/CMS_PortalEngine/ContextMenu/Layout.png");
                this.lblLayout.Text = ResHelper.GetString("PlaceholderMenu.IconLayout", culture);
                this.imgLayout.AlternateText = this.lblLayout.Text;
                //this.lblLayout.ToolTip = GetString("WebPartMenu.Layout", culture);
                this.pnlLayout.Attributes.Add("onclick", "EditLayout();");

                if ((pti != null) && (pti.LayoutID > 0))
                {
                    LayoutInfo li = LayoutInfoProvider.GetLayoutInfo(pti.LayoutID);

                    // Display layout versions sub-menu
                    if ((li != null) && ObjectVersionManager.DisplayVersionsTab(li))
                    {
                        menuLayout.Visible = true;
                        lblLayout.Text = ResHelper.GetString("PlaceholderMenu.IconLayoutMore", culture);
                        lblSharedLayoutVersions.Text = ResHelper.GetString("PlaceholderMenu.SharedLayoutVersions", culture);
                        pnlSharedLayout.Attributes.Add("onclick", GetVersionsDialog(li.ObjectType, li.LayoutId));
                    }
                }
            }
            else
            {
                this.pnlLayout.Visible = false;
            }

            // Template properties
            this.imgTemplate.ImageUrl = GetImageUrl("CMSModules/CMS_PortalEngine/ContextMenu/Template.png");
            this.lblTemplate.Text = ResHelper.GetString("PlaceholderMenu.IconTemplate", culture);
            this.imgTemplate.AlternateText = this.lblTemplate.Text;
            //this.lblTemplate.ToolTip = GetString("WebPartMenu.Template", culture);
            this.pnlTemplate.Attributes.Add("onclick", "EditTemplate(GetContextMenuParameter('pagePlaceholderMenu'));");

            if (pti != null)
            {
                // Display template versions sub-menu
                if (ObjectVersionManager.DisplayVersionsTab(pti))
                {
                    menuTemplate.Visible = true;
                    lblTemplate.Text = ResHelper.GetString("PlaceholderMenu.IconTemplateMore", culture);
                    lblTemplateVersions.Text = ResHelper.GetString("PlaceholderMenu.TemplateVersions", culture);
                    pnlTemplateVersions.Attributes.Add("onclick", GetVersionsDialog(pti.ObjectType, pti.PageTemplateId));
                }
            }
        }

        if (!isAspx)
        {
            this.imgClone.ImageUrl = GetImageUrl("CMSModules/CMS_PortalEngine/ContextMenu/Clonetemplate.png");
            this.lblClone.Text = ResHelper.GetString("PlaceholderMenu.IconClone", culture);
            this.imgClone.AlternateText = this.lblClone.Text;
            //this.lblClone.ToolTip = GetString("WebPartMenu.Clone", culture);
            this.pnlClone.Attributes.Add("onclick", "CloneTemplate(GetContextMenuParameter('pagePlaceholderMenu'));");
        }
        else
        {
            this.pnlClone.Visible = false;
        }

        this.imgRefresh.ImageUrl = GetImageUrl("CMSModules/CMS_PortalEngine/ContextMenu/Refresh.png");
        this.lblRefresh.Text = ResHelper.GetString("PlaceholderMenu.IconRefresh", culture);
        this.imgRefresh.AlternateText = this.lblRefresh.Text;
        //this.lblRefresh.ToolTip = GetString("WebPartMenu.Refresh", culture);
        this.pnlRefresh.Attributes.Add("onclick", "RefreshPage();");
    }


    /// <summary>
    /// Gets javascript to open modal versions dialog.
    /// </summary>
    /// <param name="objType">Object type</param>
    /// <param name="objId">ID of the object</param>
    private string GetVersionsDialog(string objType, int objId)
    {
        string url = ResolveUrl("~/CMSModules/Objects/Dialogs/ObjectVersionDialog.aspx?objecttype=" + objType + "&objectid=" + objId);
        return "modalDialog('" + URLHelper.AddParameterToUrl(url, "hash", QueryHelper.GetHash(url)) + "','VersionsDialog','900','600');return false;";
    }
}
