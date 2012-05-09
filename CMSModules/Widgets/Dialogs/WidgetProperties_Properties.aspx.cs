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
using CMS.CMSHelper;

public partial class CMSModules_Widgets_Dialogs_WidgetProperties_Properties : CMSDeskPage
{
    #region "Variables"

    protected string aliasPath = QueryHelper.GetString("aliaspath", "");
    protected int templateId = QueryHelper.GetInteger("templateid", 0);

    #endregion


    #region "Page events"

    /// <summary>
    /// Constructor.
    /// </summary>
    public CMSModules_Widgets_Dialogs_WidgetProperties_Properties()
    {
        this.PreInit += new EventHandler(PreInit_Setting);
    }


    /// <summary>
    /// OnInit override - do not require site.
    /// </summary>
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        RequireSite = false;

        RegisterEscScript();
    }


    /// <summary>
    /// PreInit event handler.
    /// </summary>
    protected void PreInit_Setting(object sender, EventArgs e)
    {
        mBodyClass = BrowserHelper.GetBrowserClass();

        // Initialize the control
        widgetProperties.AliasPath = aliasPath;
        widgetProperties.PageTemplateId = templateId;
        widgetProperties.WidgetId = QueryHelper.GetString("widgetid", "");
        widgetProperties.ZoneId = QueryHelper.GetString("zoneid", "");
        widgetProperties.InstanceGUID = QueryHelper.GetGuid("instanceguid", Guid.Empty);
        widgetProperties.IsNewWidget = QueryHelper.GetBoolean("isnew", false);
        widgetProperties.IsNewVariant = QueryHelper.GetBoolean("isnewvariant", false);
        widgetProperties.IsInline = QueryHelper.GetBoolean("Inline", false);
        widgetProperties.VariantID = QueryHelper.GetInteger("variantid", 0);
        widgetProperties.VariantMode = VariantModeFunctions.GetVariantModeEnum(QueryHelper.GetString("variantmode", string.Empty));
        widgetProperties.ZoneType = WidgetZoneTypeCode.ToEnum(QueryHelper.GetString("zonetype", ""));
        widgetProperties.IsLiveSite = false;

        // Ensure the design mode for the dialog
        if (String.IsNullOrEmpty(aliasPath))
        {
            PortalContext.SetRequestViewMode(ViewModeEnum.Design);
        }

        widgetProperties.OnNotAllowed += new EventHandler(widgetProperties_OnNotAllowed);
    }


    /// <summary>
    /// Handles the OnNotAllowed event of the widgetProperties control.
    /// </summary>
    protected void widgetProperties_OnNotAllowed(object sender, EventArgs e)
    {
        RedirectToAccessDenied(GetString("widgets.security.notallowed"));
    }

    #endregion
}
