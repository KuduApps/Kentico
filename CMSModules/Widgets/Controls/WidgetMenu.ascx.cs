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

public partial class CMSModules_Widgets_Controls_WidgetMenu : CMSAbstractPortalUserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Use UI culture for strings
        string culture = CMSContext.CurrentUser.PreferredUICultureCode;

        // Properties
        this.imgProperties.ImageUrl = GetImageUrl("CMSModules/CMS_PortalEngine/ContextMenu/Properties.png");
        this.lblProperties.Text = ResHelper.GetString("WebPartMenu.IconProperties", culture);
        this.pnlProperties.Attributes.Add("onclick", "ContextConfigureWidget(GetContextMenuParameter('widgetMenu'));");
        
        // Up
        this.imgUp.ImageUrl = GetImageUrl("CMSModules/CMS_PortalEngine/ContextMenu/Up.png");
        this.lblUp.Text = ResHelper.GetString("WebPartMenu.IconUp", culture);
        this.pnlUp.Attributes.Add("onclick", "ContextMoveWidgetUp(GetContextMenuParameter('widgetMenu'));");

        // Down
        this.imgDown.ImageUrl = GetImageUrl("CMSModules/CMS_PortalEngine/ContextMenu/Down.png");
        this.lblDown.Text = ResHelper.GetString("WebPartMenu.IconDown", culture);
        this.pnlDown.Attributes.Add("onclick", "ContextMoveWidgetDown(GetContextMenuParameter('widgetMenu'));");

        // Clone
        this.imgClone.ImageUrl = GetImageUrl("CMSModules/CMS_PortalEngine/ContextMenu/Clonewidget.png");
        this.lblClone.Text = ResHelper.GetString("widgets.widgetMenu.clone", culture);
        this.pnlClone.Attributes.Add("onclick", "ContextCloneWidget(GetContextMenuParameter('widgetMenu'));");

        // Delete
        this.imgDelete.ImageUrl = GetImageUrl("CMSModules/CMS_PortalEngine/ContextMenu/Delete.png");
        this.lblDelete.Text = ResHelper.GetString("general.remove", culture);
        this.pnlDelete.Attributes.Add("onclick", "ContextRemoveWidget(GetContextMenuParameter('widgetMenu'));");

        // Top
        this.lblTop.Text = ResHelper.GetString("UpMenu.IconTop", culture);
        this.pnlTop.Attributes.Add("onclick", "ContextMoveWidgetTop(GetContextMenuParameter('widgetMenu'));");

        // Bottom
        this.lblBottom.Text = ResHelper.GetString("DownMenu.IconBottom", culture);
        this.pnlBottom.Attributes.Add("onclick", "ContextMoveWidgetBottom(GetContextMenuParameter('widgetMenu'));");
    }
}
