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
using CMS.PortalEngine;
using CMS.FormEngine;
using CMS.CMSHelper;
using CMS.SiteProvider;
using CMS.SettingsProvider;
using CMS.UIControls;

public partial class CMSModules_PortalEngine_UI_WebParts_WebPartProperties_header : CMSWebPartPropertiesPage
{
    #region "Variables"

    private bool showCodeTab = ValidationHelper.GetBoolean(SettingsHelper.AppSettings["CMSShowWebPartCodeTab"], false);
    private bool showBindingTab = ValidationHelper.GetBoolean(SettingsHelper.AppSettings["CMSShowWebPartBindingTab"], false);

    #endregion


    protected override void OnPreInit(EventArgs e)
    {
        base.OnPreInit(e);

        this["TabControl"] = tabsElem;
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        // Initialize page title
        PageTitle.TitleText = GetString("WebpartProperties.Title");
        PageTitle.TitleImage = GetImageUrl("CMSModules/CMS_PortalEngine/Webpartproperties.png");

        if (!RequestHelper.IsPostBack())
        {
            InitalizeMenu();
        }

        this.tabsElem.OnTabCreated += new UITabs.TabCreatedEventHandler(tabElem_OnTabCreated);

        ScriptHelper.RegisterClientScriptBlock(this, typeof(string), ScriptHelper.NEWWINDOW_SCRIPT_KEY, ScriptHelper.NewWindowScript);
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        this.tabsElem.DoTabSelection();
    }


    /// <summary>
    /// Initializes menu.
    /// </summary>
    protected void InitalizeMenu()
    {
        if (webpartId != "")
        {
            // get pageinfo
            PageInfo pi = GetPageInfo(aliasPath, templateId);
            if (pi == null)
            {
                this.Visible = false;
                return;
            }

            PageTemplateInfo pti = pi.PageTemplateInfo;
            if (pti != null)
            {
                WebPartInfo wi = null;

                // Get web part
                WebPartInstance webPart = pti.GetWebPart(instanceGuid, webpartId);
                if (webPart != null)
                {
                    wi = WebPartInfoProvider.GetWebPartInfo(webPart.WebPartType);
                    if (ValidationHelper.GetString(webPart.GetValue("WebPartCode"), "").Trim() != "")
                    {
                        showCodeTab = true;
                    }
                    if (webPart.Bindings.Count > 0)
                    {
                        showBindingTab = true;
                    }
                }
                else
                {
                    wi = WebPartInfoProvider.GetWebPartInfo(ValidationHelper.GetInteger(webpartId, 0));
                }

                if (wi != null)
                {
                    // Generate documentation link
                    Literal ltr = new Literal();
                    PageTitle.RightPlaceHolder.Controls.Add(ltr);

                    string docScript = "NewWindow('" + ResolveUrl("~/CMSModules/PortalEngine/UI/WebParts/WebPartDocumentationPage.aspx") + "?webpartid=" + ScriptHelper.GetString(wi.WebPartName, false) + "', 'WebPartPropertiesDocumentation', 800, 800); return false;";

                    ltr.Text = "<table cellpadding=\"0\" cellspacing=\"0\"><tr><td>";
                    ltr.Text += "<a onclick=\"" + docScript + "\" href=\"#\"><img src=\"" + ResolveUrl(GetImageUrl("CMSModules/CMS_PortalEngine/Documentation.png")) + "\" style=\"border-width: 0px;\"></a>";
                    ltr.Text += "</td>";
                    ltr.Text += "<td>";
                    ltr.Text += "<a onclick=\"" + docScript + "\" href=\"#\">" + GetString("WebPartPropertie.DocumentationLink") + "</a>";
                    ltr.Text += "</td></tr></table>";

                    PageTitle.TitleText = GetString("WebpartProperties.Title") + " (" + HTMLHelper.HTMLEncode(ResHelper.LocalizeString(wi.WebPartDisplayName)) + ")";
                }

            }
        }

        CurrentUserInfo currentUser = CMSContext.CurrentUser;

        tabsElem.UrlTarget = "webpartpropertiescontent";
    }


    protected string[] tabElem_OnTabCreated(CMS.SiteProvider.UIElementInfo element, string[] parameters, int tabIndex)
    {
        String script = "SetTabsContext('');";
        String defaultParam = parameters[1];
        switch (element.ElementName.ToLower())
        {
            case "webpartproperties.code":
                if (!showCodeTab || isNew || isNewVariant)
                {
                    return null;
                }
                break;

            case "webpartproperties.variant":
                script = "SetTabsContext('variants');";
                String varName = (variantMode == VariantModeEnum.MVT) ? "mvtvariant_edit" : "cpvariant_edit";
                defaultParam = "if (window.SetHelpTopic) { window.SetHelpTopic('helpTopic', '" + varName + "')}";
                if ((variantId <= 0) || isNew || isNewVariant)
                {
                    return null;
                }
                break;

            case "webpartzoneproperties.variant":
                if ((zoneVariantId <= 0) || isNew)
                {
                    return null;
                }
                break;

            case "webpartproperties.bindings":
                if (!showBindingTab || isNew || isNewVariant)
                {
                    return null;
                }
                break;

            case "webpartproperties.layout":
                if (isNew || isNewVariant)
                {
                    return null;
                }
                break;
        }

        parameters[1] = script + defaultParam;

        return parameters;
    }
}
