using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.PortalControls;
using CMS.PortalEngine;
using CMS.IO;

public partial class CMSModules_Widgets_LiveDialogs_WidgetProperties_Properties : LivePage
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public CMSModules_Widgets_LiveDialogs_WidgetProperties_Properties()
    {
        this.PreInit += new EventHandler(PreInit_Setting);        
    }


    /// <summary>
    /// PreInit event handler.
    /// </summary>
    protected void PreInit_Setting(object sender, EventArgs e)
    {
        mBodyClass = BrowserHelper.GetBrowserClass();
        // Initialize the control
        string aliasPath = QueryHelper.GetString("aliaspath", "");
        widgetProperties.AliasPath = aliasPath;
        widgetProperties.PageTemplateId = QueryHelper.GetInteger("templateid", 0);
        widgetProperties.WidgetId = QueryHelper.GetString("widgetid", "");
        widgetProperties.ZoneId = QueryHelper.GetString("zoneid", "");
        widgetProperties.InstanceGUID = QueryHelper.GetGuid("instanceguid", Guid.Empty);
        widgetProperties.IsNewWidget = QueryHelper.GetBoolean("isnew", false);
        widgetProperties.IsNewVariant = QueryHelper.GetBoolean("isnewvariant", false);
        widgetProperties.IsInline = QueryHelper.GetBoolean("Inline", false);
        widgetProperties.VariantID = QueryHelper.GetInteger("variantid", 0);
        widgetProperties.ZoneType = WidgetZoneTypeCode.ToEnum(QueryHelper.GetString("zonetype", ""));

        widgetProperties.IsLiveSite = true;

        widgetProperties.OnNotAllowed += new EventHandler(widgetProperties_OnNotAllowed);
    }


    protected override void OnPreRenderComplete(EventArgs e)
    {
        base.OnPreRenderComplete(e);

        RegisterDialogCSSLink();
        SetLiveDialogClass();
    }


    protected void widgetProperties_OnNotAllowed(object sender, EventArgs e)
    {
        RedirectToAccessDenied(GetString("widgets.security.notallowed"));
    }
}
