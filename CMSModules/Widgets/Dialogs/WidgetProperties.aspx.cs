using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using CMS.ExtendedControls;
using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.PortalEngine;
using CMS.CMSHelper;
using CMS.TreeEngine;
using CMS.PortalControls;

public partial class CMSModules_Widgets_Dialogs_WidgetProperties : CMSModalPage
{

    /// <summary>
    /// Handles the Load event of the Page control.
    /// </summary>
    protected void Page_Load(object sender, EventArgs e)
    {
        TreeProvider tree = new TreeProvider(CMSContext.CurrentUser);

        string widgetId = QueryHelper.GetString("widgetid", String.Empty);
        string aliasPath = QueryHelper.GetString("aliasPath", String.Empty);
        int templateId = QueryHelper.GetInteger("templateid", 0);
        string zoneId = QueryHelper.GetString("zoneid", String.Empty);
        Guid instanceGUID = QueryHelper.GetGuid("instanceguid", Guid.Empty);
        
        bool isNewWidget = QueryHelper.GetBoolean("isnew", false);
        bool inline = QueryHelper.GetBoolean("inline", false);
        int variantId = QueryHelper.GetInteger("variantid", 0);

        // Set page title 
        if (isNewWidget)
        {
            this.Page.Title = GetString("widgets.propertiespage.titlenew");
        }
        else
        {
            this.Page.Title = GetString("widgets.propertiespage.title");
        }

        // Resize the header (enlarge) to make a space for the tabs header when displaying a widget variant
        if (variantId > 0)
        {
            rowsFrameset.Attributes.Add("rows", "67, *");
        }

        // Ensure correct view mode
        if (String.IsNullOrEmpty(aliasPath))
        {
            // Ensure the dashboard mode for the dialog
            if (QueryHelper.Contains("dashboard"))
            {
                PortalContext.SetRequestViewMode(ViewModeEnum.DashboardWidgets);
                PortalContext.DashboardName = QueryHelper.GetString("dashboard", String.Empty);
                PortalContext.DashboardSiteName = QueryHelper.GetString("sitename", String.Empty);
            }
            // Ensure the design mode for the dialog
            else
            {
                PortalContext.SetRequestViewMode(ViewModeEnum.Design);
            }
        }

        if (widgetId != "")
        {
            // Get pageinfo
            PageInfo pi = null;
            try
            {
                pi = CMSWebPartPropertiesPage.GetPageInfo(aliasPath, templateId);
            }
            catch (PageNotFoundException)
            {
                // Do not throw exception if page info not found (e.g. bad alias path)
            }

            if (pi == null)
            {
                return;
            }

            // Get template
            PageTemplateInfo pti = pi.PageTemplateInfo;

            // Get template instance
            PageTemplateInstance templateInstance = CMSPortalManager.GetTemplateInstanceForEditing(pi);

            // Get widget from instance
            WidgetInfo wi = null;
            if (!isNewWidget)
            {
                // Get the instance of widget
                WebPartInstance widgetInstance = templateInstance.GetWebPart(instanceGUID, widgetId);
                if (widgetInstance == null)
                {
                    return;
                }

                // Get widget info by widget name(widget type)
                wi = WidgetInfoProvider.GetWidgetInfo(widgetInstance.WebPartType);
            }
            // Widget instance hasn't created yet
            else
            {
                wi = WidgetInfoProvider.GetWidgetInfo(ValidationHelper.GetInteger(widgetId, 0));
            }
            
            if (wi != null)
            {
                WebPartZoneInstance zone = templateInstance.GetZone(zoneId);
                if (zone != null)
                {
                    CurrentUserInfo currentUser = CMSContext.CurrentUser;

                    switch (zone.WidgetZoneType)
                    {
                        // Group zone => Only group widgets and group admin
                        case WidgetZoneTypeEnum.Group:
                            // Should always be, only group widget are allowed in group zone
                            if (!wi.WidgetForGroup || (!currentUser.IsGroupAdministrator(pi.NodeGroupId) && ((CMSContext.ViewMode != ViewModeEnum.Design) || ((CMSContext.ViewMode == ViewModeEnum.Design) && (!currentUser.IsAuthorizedPerResource("CMS.Design", "Design"))))))
                            {
                                RedirectToAccessDenied(GetString("widgets.security.notallowed"));
                            }
                            break;

                        // Widget must be allowed for editor zones
                        case WidgetZoneTypeEnum.Editor:
                            if (!wi.WidgetForEditor)
                            {
                                RedirectToAccessDenied(GetString("widgets.security.notallowed"));
                            }
                            break;

                        // Widget must be allowed for user zones
                        case WidgetZoneTypeEnum.User:
                            if (!wi.WidgetForUser)
                            {
                                RedirectToAccessDenied(GetString("widgets.security.notallowed"));
                            }
                            break;
                    }

                    if ((zone.WidgetZoneType != WidgetZoneTypeEnum.Group) && !WidgetRoleInfoProvider.IsWidgetAllowed(wi, currentUser.UserID, currentUser.IsAuthenticated()))
                    {
                        RedirectToAccessDenied(GetString("widgets.security.notallowed"));
                    }
                }

                // If all ok, set up frames
                this.frameHeader.Attributes.Add("src", "widgetproperties_header.aspx" + URLHelper.Url.Query);
                this.frameContent.Attributes.Add("src", "widgetproperties_properties_frameset.aspx" + URLHelper.Url.Query);
            }
        }

        this.frameHeader.Attributes.Add("src", "widgetproperties_header.aspx" + URLHelper.Url.Query);
        if (inline && !isNewWidget)
        {
            this.frameContent.Attributes.Add("src", ResolveUrl("~/CMSPages/Blank.htm"));
        }
        else 
        {
            this.frameContent.Attributes.Add("src", "widgetproperties_properties_frameset.aspx" + URLHelper.Url.Query);
        }
    }
}
