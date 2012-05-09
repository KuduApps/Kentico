using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.UIControls;
using CMS.PortalEngine;
using CMS.GlobalHelper;
using CMS.SettingsProvider;

public partial class CMSModules_PortalEngine_UI_WebParts_WebPartProperties_layout_menu : CMSWebPartPropertiesPage
{
    #region "Variables"

    WebPartInfo webPartInfo = null;

    /// <summary>
    /// Current page info.
    /// </summary>
    PageInfo pi = null;

    /// <summary>
    /// Page template info.
    /// </summary>
    PageTemplateInfo pti = null;


    /// <summary>
    /// Current web part.
    /// </summary>
    WebPartInstance webPart = null;

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
        this.CurrentMaster.DisplaySiteSelectorPanel = true;

        if (webpartId != "")
        {
            // Get pageinfo
            pi = GetPageInfo(aliasPath, templateId);
            if (pi == null)
            {
                RedirectToInformation(GetString("WebPartProperties.WebPartNotFound"));
                return;
            }

            // Get page template
            pti = pi.PageTemplateInfo;
            if ((pti != null) && ((pti.TemplateInstance != null)))
            {
                webPart = pti.TemplateInstance.GetWebPart(instanceGuid, zoneVariantId, variantId) ?? pti.GetWebPart(webpartId);
            }
        }

        // If the web part is not found, do not continue
        if (webPart == null)
        {
            RedirectToInformation(GetString("WebPartProperties.WebPartNotFound"));
            return;
        }
        else
        {
            // Get the current layout name
            if (String.IsNullOrEmpty(LayoutCodeName))
            {
                mLayoutCodeName = ValidationHelper.GetString(webPart.GetValue("WebPartLayout"), "");
            }
        }

        // Strings
        lblLayouts.Text = GetString("WebPartPropertise.LayoutList");

        if (!RequestHelper.IsPostBack())
        {
            LoadLayouts();
        }

        // Add default drop down items
        selectLayout.ShowDefaultItem = true;

        // Add new item
        if (SettingsKeyProvider.UsingVirtualPathProvider && CurrentUser.IsAuthorizedPerResource("CMS.Design", "EditCode"))
        {
            selectLayout.ShowNewItem = true;
        }

        webPartInfo = WebPartInfoProvider.GetWebPartInfo(webPart.WebPartType);

        // Where condition
        selectLayout.WhereCondition = "WebPartLayoutWebPartID =" + webPartInfo.WebPartID;

        if (QueryHelper.GetBoolean("reload", false))
        {
            SetContentPage();
        }
    }

    /// <summary>
    /// Load layouts.
    /// </summary>
    private void LoadLayouts()
    {
        if (!RequestHelper.IsPostBack() && (LayoutCodeName != ""))
        {
            selectLayout.Value = LayoutCodeName;
        }
    }


    /// <summary>
    /// Selected index changed.
    /// </summary>
    protected void drpLayouts_Changed(object sender, EventArgs ea)
    {
        if (webPartInfo != null)
        {
            SetContentPage();
        }
    }


    /// <summary>
    /// Reload content frame.
    /// </summary>
    private void SetContentPage()
    {
        // Register script for load layout form to other frame
        string query = URLHelper.Url.Query;
        string selectedLayout = selectLayout.Value.ToString();

        query = URLHelper.AddParameterToUrl(query, "layoutcodename", selectedLayout);
        query = URLHelper.AddParameterToUrl(query, "noreload", "true");
        if (selectedLayout == "|new|")
        {
            query = URLHelper.RemoveParameterFromUrl(query, "saved");
        }

        string scriptText = ScriptHelper.GetScript(@"parent.frames['webpartpropertiescontent'].location = 'webpartproperties_layout.aspx" + query + "';");

        ScriptHelper.RegisterStartupScript(this.Page, typeof(string), "SetWebPartLayout", scriptText);
    }

    #endregion
}
