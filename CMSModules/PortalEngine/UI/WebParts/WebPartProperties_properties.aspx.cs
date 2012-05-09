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
using CMS.UIControls;
using CMS.PortalControls;
using CMS.PortalEngine;
using CMS.CMSHelper;

public partial class CMSModules_PortalEngine_UI_WebParts_WebPartProperties_properties : CMSWebPartPropertiesPage
{
    /// <summary>
    /// PreInit event handler.
    /// </summary>
    protected override void OnPreInit(EventArgs e)
    {
        base.OnPreInit(e);

        // Check permissions for web part properties UI
        CurrentUserInfo currentUser = CMSContext.CurrentUser;
        if (!currentUser.IsAuthorizedPerUIElement("CMS.Content", "WebPartProperties.General"))
        {
            RedirectToCMSDeskUIElementAccessDenied("CMS.Content", "WebPartProperties.General");
        }

        // Initialize the control
        webPartProperties.AliasPath = aliasPath;
        webPartProperties.WebpartId = webpartId;
        webPartProperties.ZoneId = zoneId;
        webPartProperties.InstanceGUID = instanceGuid;
        webPartProperties.PageTemplateId = templateId;
        webPartProperties.IsNewWebPart = isNew;
        webPartProperties.IsNewVariant = isNewVariant;
        webPartProperties.VariantID = variantId;
        webPartProperties.ZoneVariantID = zoneVariantId;
        webPartProperties.VariantMode = variantMode;
    }
}
