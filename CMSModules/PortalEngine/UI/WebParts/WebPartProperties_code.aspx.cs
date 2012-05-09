using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Xml;
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

public partial class CMSModules_PortalEngine_UI_WebParts_WebPartProperties_code : CMSWebPartPropertiesPage
{
    #region "Variables"

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

    #endregion


    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        // Check permissions for web part properties UI
        CurrentUserInfo currentUser = CMSContext.CurrentUser;

        if (!currentUser.IsAuthorizedPerUIElement("CMS.Content", "WebPartProperties.Code"))
        {
            RedirectToCMSDeskUIElementAccessDenied("CMS.Content", "WebPartProperties.Code");
        }
                
        if (webpartId != "")
        {
            // Get page info
            pi = GetPageInfo(aliasPath, templateId);
            if (pi == null)
            {
                this.Visible = false;
                return;
            }

            pti = pi.PageTemplateInfo;
            if ((pti != null) && ((pti.TemplateInstance != null)))
            {
                // Get web part instance
                webPart = pti.TemplateInstance.GetWebPart(instanceGuid, zoneVariantId, variantId) ?? pti.GetWebPart(webpartId);

                if (webPart != null)
                {
                    txtCode.Text = ValidationHelper.GetString(webPart.GetValue("WebPartCode"), "");
                }
            }
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!SettingsKeyProvider.UsingVirtualPathProvider)
        {
            this.lblInfo.Text = GetString("WebPartCode.ProviderNotRunning");
            this.lblInfo.CssClass = "ErrorLabel";
            this.txtCode.Enabled = false;
        }
        else
        {
            this.lblInfo.Text = GetString("WebPartCode.Info");
            btnOnOK.Click += new EventHandler(btnOnOK_Click);
            btnOnApply.Click += new EventHandler(btnOnApply_Click);

            ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "ApplyButton", ScriptHelper.GetScript(
                "function SetRefresh(refreshpage) { document.getElementById('" + this.hidRefresh.ClientID + "').value = refreshpage; } \n" +
                "function OnApplyButton(refreshpage) { SetRefresh(refreshpage); " + Page.ClientScript.GetPostBackEventReference(btnOnApply, "") + "} \n" +
                "function OnOKButton(refreshpage) { SetRefresh(refreshpage); " + Page.ClientScript.GetPostBackEventReference(btnOnOK, "") + "} \n"
            ));
        }
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        if (!CMSContext.CurrentUser.IsAuthorizedPerResource("CMS.Design", "EditCode"))
        {
            this.txtCode.ReadOnly = true;
            this.lblInfo.Text = GetString("EditCode.NotAllowedNoHtml");
        }
    }


    /// <summary>
    /// Saves webpart properties.
    /// </summary>
    public void Save()
    {
        if (!CMSContext.CurrentUser.IsAuthorizedPerResource("CMS.Design", "EditCode"))
        {
            return;
        }

        webPart.SetValue("WebPartCode", txtCode.Text);


        bool isWebPartVariant = (variantId > 0) || (zoneVariantId > 0) || isNewVariant;
        if (!isWebPartVariant)
        {
            // Update page template
            PageTemplateInfoProvider.SetPageTemplateInfo(pti);
        }
        else
        {
            // Save the variant properties
            if ((webPart != null)
                && (webPart.ParentZone != null)
                && (webPart.ParentZone.ParentTemplateInstance != null)
                && (webPart.ParentZone.ParentTemplateInstance.ParentPageTemplate != null))
            {
                XmlDocument doc = new XmlDocument();
                XmlNode xmlWebParts = null;

                if (zoneVariantId > 0)
                {
                    // This webpart is in a zone variant therefore save the whole variant webparts
                    xmlWebParts = webPart.ParentZone.GetXmlNode(doc);
                    if (webPart.VariantMode == VariantModeEnum.MVT)
                    {
                        ModuleCommands.OnlineMarketingSaveMVTVariantWebParts(zoneVariantId, xmlWebParts);
                    }
                    else if (webPart.VariantMode == VariantModeEnum.ContentPersonalization)
                    {
                        ModuleCommands.OnlineMarketingSaveContentPersonalizationVariantWebParts(zoneVariantId, xmlWebParts);
                    }
                }
                else if (variantId > 0)
                {
                    // This webpart is a web part variant
                    xmlWebParts = webPart.GetXmlNode(doc);
                    if (webPart.VariantMode == VariantModeEnum.MVT)
                    {
                        ModuleCommands.OnlineMarketingSaveMVTVariantWebParts(variantId, xmlWebParts);
                    }
                    else if (webPart.VariantMode == VariantModeEnum.ContentPersonalization)
                    {
                        ModuleCommands.OnlineMarketingSaveContentPersonalizationVariantWebParts(variantId, xmlWebParts);
                    }
                }
            }
        }

        string parameters = aliasPath + "/" + zoneId + "/" + webpartId;
        string cacheName = "CMSVirtualWebParts|" + parameters.ToLower().TrimStart('/');

        CacheHelper.Remove(cacheName);
    }


    /// <summary>
    /// Saves the webpart properties and closes the window.
    /// </summary>
    protected void btnOnOK_Click(object sender, EventArgs e)
    {
        // Save webpart properties
        Save();

        bool refresh = ValidationHelper.GetBoolean(this.hidRefresh.Value, false);

        string script = "";
        if (refresh)
        {
            script = "RefreshPage(); \n";
        }

        // Close the window
        ltlScript.Text += ScriptHelper.GetScript(script + "top.window.close();");
    }


    /// <summary>
    /// Saves the webpart properties.
    /// </summary>
    protected void btnOnApply_Click(object sender, EventArgs e)
    {
        Save();
    }
}
