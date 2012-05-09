using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.UIControls;
using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.PortalEngine;

public partial class CMSModules_PortalEngine_UI_WebParts_WebPartProperties_layout_frameset_header : CMSWebPartPropertiesPage
{
    #region "Variables"

    string mLayoutCodeName = null;

    #endregion


    #region "Properties"

    /// <summary>
    /// Gets code name of edited layout.
    /// </summary>
    private string LayoutCodeName
    {
        get
        {
            if (mLayoutCodeName == null)
            {
                mLayoutCodeName = QueryHelper.GetString("layoutcodename", String.Empty);
            }
            return mLayoutCodeName;
        }
        set
        {
            mLayoutCodeName = value;
        }
    }

    #endregion


    #region "Page methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Check permissions for web part properties UI
        CurrentUserInfo currentUser = CMSContext.CurrentUser;
        if (!currentUser.IsAuthorizedPerUIElement("CMS.Content", "WebPartProperties.Layout"))
        {
            RedirectToCMSDeskUIElementAccessDenied("CMS.Content", "WebPartProperties.Layout");
        }

        WebPartInstance webPart = null;

        if (webpartId != "")
        {
            // Get pageinfo
            PageInfo pi = GetPageInfo(aliasPath, templateId);
            if (pi != null)
            {
                // Get page template
                PageTemplateInfo pti = pi.PageTemplateInfo;

                // Get web part
                if ((pti != null) && ((pti.TemplateInstance != null)))
                {
                    webPart = pti.TemplateInstance.GetWebPart(instanceGuid, zoneVariantId, variantId) ?? pti.GetWebPart(webpartId);
                }
            }
        }

        // If the web part is not found, do not continue
        if (webPart != null)
        {
            WebPartInfo wpi = WebPartInfoProvider.GetWebPartInfo(webPart.WebPartType);
            if (wpi != null)
            {
                // Get the current layout name
                if (String.IsNullOrEmpty(LayoutCodeName))
                {
                    // Get the current layout name
                    LayoutCodeName = ValidationHelper.GetString(webPart.GetValue("WebPartLayout"), "");
                }

                // Load the web part information
                if (LayoutCodeName != "")
                {
                    WebPartLayoutInfo wpli = WebPartLayoutInfoProvider.GetWebPartLayoutInfo(wpi.WebPartName, LayoutCodeName);
                    SetEditedObject(wpli, null);
                }
            }
        }

        // Set page tabs
        InitTabs(1, "webpartlayoutcontent");
        SetTab(0, GetString("general.general"), "webpartproperties_layout.aspx" + URLHelper.RemoveParameterFromUrl(URLHelper.Url.Query, "saved"), null);
    }

    #endregion
}
