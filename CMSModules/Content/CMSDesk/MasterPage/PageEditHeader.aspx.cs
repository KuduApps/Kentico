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
using CMS.UIControls;
using CMS.WorkflowEngine;
using CMS.PortalEngine;

using MenuItem = CMS.skmMenuControl.MenuItem;
using TreeNode = CMS.TreeEngine.TreeNode;

public partial class CMSModules_Content_CMSDesk_MasterPage_PageEditHeader : CMSContentPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        CurrentUserInfo user = CMSContext.CurrentUser;

        // Check UIProfile
        if (!user.IsAuthorizedPerUIElement("CMS.Content", "MasterPage"))
        {
            RedirectToCMSDeskUIElementAccessDenied("CMS.Content", "MasterPage");
        }

        // Check "Design" permission
        if (!user.IsAuthorizedPerResource("CMS.Design", "Design"))
        {
            RedirectToAccessDenied("CMS.Design", "Design");
        }

        // Register the scripts
        ScriptHelper.RegisterProgress(this.Page);

        // Register the dialog script
        ScriptHelper.RegisterDialogScript(this.Page);

        // Save button
        MenuItem saveItem = null;

        saveItem = new CMS.skmMenuControl.MenuItem();
        saveItem.ToolTip = GetString("EditMenu.Save");
        saveItem.JavascriptCommand = "SaveMasterPage();";
        saveItem.ImageAltText = saveItem.ToolTip;
        saveItem.Text = GetString("general.save");
        saveItem.Image = GetImageUrl("CMSModules/CMS_Content/EditMenu/save.png");
        saveItem.MouseOverImage = saveItem.Image;
        saveItem.MouseOverCssClass = "MenuItemEdit";
        saveItem.CssClass = "MenuItemEdit";
        menuElem.Items.Add(saveItem);

        // Get document node
        int nodeId = QueryHelper.GetInteger("nodeid", 0);
        TreeNode node = DocumentHelper.GetDocument(nodeId, CMSContext.PreferredCultureCode, null);

        if (node != null)
        {
            PageTemplateInfo pt = PageTemplateInfoProvider.GetPageTemplateInfo(node.DocumentPageTemplateID);
            if (pt != null)
            {
                // Edit page properties button
                MenuItem editTemplate = null;

                editTemplate = new CMS.skmMenuControl.MenuItem();
                editTemplate.Text = GetString("PageProperties.EditTemplateProperties");
                editTemplate.ToolTip = editTemplate.Text;
                editTemplate.JavascriptCommand = "modalDialog('" + ResolveUrl("~/CMSModules/PortalEngine/UI/PageTemplates/PageTemplate_Edit.aspx") + "?templateid=" + pt.PageTemplateId + "&nobreadcrumbs=1&dialog=1', 'TemplateSelection', 850, 680, false);return false;";
                editTemplate.ImageAltText = editTemplate.Text;
                editTemplate.Image = GetImageUrl("CMSModules/CMS_Content/Template/edit.png");
                editTemplate.MouseOverImage = editTemplate.Image;
                editTemplate.MouseOverCssClass = "MenuItemEdit";
                editTemplate.CssClass = "MenuItemEdit";
                menuElem.Items.Add(editTemplate);
            }
        }

        this.menuElem.Layout = CMS.skmMenuControl.MenuLayout.Horizontal;
    }


    protected void lnkSave_Click(object sender, EventArgs e)
    {
    }
}
