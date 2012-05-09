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
using CMS.CMSHelper;
using CMS.ExtendedControls;
using CMS.PortalEngine;

public partial class CMSModules_PortalEngine_UI_WebParts_WebPartZoneProperties : CMSModalDesignPage
{
    #region "Variables"

    private Control currentControl = null;
    private bool isVariantTab = false;
    protected Guid instanceGuid = QueryHelper.GetGuid("instanceguid", Guid.Empty);
    protected int templateId = QueryHelper.GetInteger("templateid", 0);
    protected int variantId = QueryHelper.GetInteger("variantid", 0);
    protected int zoneVariantId = QueryHelper.GetInteger("zonevariantid", 0);
    protected VariantModeEnum variantMode = VariantModeFunctions.GetVariantModeEnum(QueryHelper.GetString("variantmode", string.Empty));

    #endregion


    #region "Page methods"

    /// <summary>
    /// PreInit event handler
    /// </summary>
    protected override void OnPreInit(EventArgs e)
    {
        base.OnPreInit(e);

        // When displaying an existing variant of a web part, get the variant mode for its original web part
        if ((variantId > 0) || (zoneVariantId > 0))
        {
            PageTemplateInfo pti = PageTemplateInfoProvider.GetPageTemplateInfo(templateId);
            if ((pti != null) && ((pti.TemplateInstance != null)))
            {
                // Get the original webpart and retrieve its variant mode
                WebPartInstance webpartInstance = pti.TemplateInstance.GetWebPart(instanceGuid, zoneVariantId, 0);
                if (webpartInstance != null)
                {
                    variantMode = webpartInstance.VariantMode;
                }
            }
        }
    }


    /// <summary>
    /// Handles the Load event of the Page control.
    /// </summary>
    protected void Page_Load(object sender, EventArgs e)
    {
        // Check UI elements for web part zone
        CurrentUserInfo currentUser = CMSContext.CurrentUser;
        if (!currentUser.IsAuthorizedPerUIElement("CMS.Content", new string[] { "Design", "Design.WebPartZoneProperties" }, CMSContext.CurrentSiteName))
        {
            RedirectToCMSDeskUIElementAccessDenied("CMS.Content", "Design;Design.WebPartZoneProperties");
        }

        // Show the tabs when displaying a zone variant
        if (variantId > 0)
        {
            tabsElem.JavaScriptHandler = "TabSelect";
            tabsElem.OnTabCreated += new UITabs.TabCreatedEventHandler(tabElem_OnTabCreated);
            pnlTabsContainer.Visible = true;
        }

        string postBackReference = ControlsHelper.GetPostBackEventReference(pnlUpdate, "");
        ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "TabSelect", ScriptHelper.GetScript("function TabSelect(name){currSelElem = document.getElementById('" + hdnSelectedTab.ClientID + "'); if ((currSelElem != null)) { origVal = currSelElem.value; currSelElem.value = name; if (origVal != '') {" + postBackReference + " }}}"));

        // Title
        this.CurrentMaster.Title.TitleText = GetString("webpartzone.propertiesheader");
        this.CurrentMaster.Title.TitleImage = GetImageUrl("CMSModules/CMS_PortalEngine/WebpartZoneProperties/title.png");
        this.CurrentMaster.Title.HelpTopicName = "webpartzoneproperties";
        this.CurrentMaster.Title.HelpName = "helpTopic";

        // Tabs header css class
        this.CurrentMaster.PanelHeader.CssClass = "WebpartTabsPageHeader";

        // UI Strings
        this.btnOk.Text = GetString("general.ok");
        this.btnApply.Text = GetString("general.apply");
        this.btnCancel.Text = GetString("general.cancel");
        this.chkRefresh.Text = GetString("webpartzone.refresh");

        // Default control path 
        string controlPath = "~/CMSModules/PortalEngine/Controls/WebParts/WebPartZoneProperties.ascx";
        string ctrlId = "wpzp";

        // Set personalized control path if selected
        switch (hdnSelectedTab.Value.ToLower())
        {
            case "webpartzoneproperties.variant":
                controlPath = "~/CMSModules/OnlineMarketing/Controls/WebParts/WebPartZonePersonalized.ascx";
                ctrlId = "pers";
                isVariantTab = true;
                break;
        }

        // Load selected control
        currentControl = this.LoadControl(controlPath);
        currentControl.ID = ctrlId;
        // Add to control collection
        plcDynamicContent.Controls.Add(currentControl);
    }


    /// <summary>
    /// Saves the webpart zone properties and closes the window.
    /// </summary>
    protected void btnOK_Click(object sender, EventArgs e)
    {
        string script = string.Empty;
        bool saved = false;

        // Save webpart properties
        if (currentControl != null)
        {
            if (!isVariantTab)
            {
                CMSModules_PortalEngine_Controls_WebParts_WebPartZoneProperties webPartZonePropertiesElem = currentControl as CMSModules_PortalEngine_Controls_WebParts_WebPartZoneProperties;
                if ((webPartZonePropertiesElem != null) && webPartZonePropertiesElem.Save())
                {
                    saved = true;
                    if (this.chkRefresh.Checked)
                    {
                        if (webPartZonePropertiesElem.IsNewVariant && (webPartZonePropertiesElem.WebPartZoneInstance != null))
                        {
                            // Display the new variant by default
                            script = "UpdateVariantPosition('" + "Variant_Zone_" + webPartZonePropertiesElem.WebPartZoneInstance.ZoneID + "', -1); ";
                        }

                        script += "wopener.location.replace(wopener.location); ";
                    }
                }
            }
            else
            {
                IWebPartZoneProperties webPartZoneVariantElem = currentControl as IWebPartZoneProperties;
                if (webPartZoneVariantElem != null)
                {
                    saved = webPartZoneVariantElem.Save();
                    tabsElem.SelectedTab = 1;
                }
            }
        }

        // Close the window
        if (saved)
        {
            script += "window.close();";
            ltlScript.Text += ScriptHelper.GetScript(script);
        }
    }


    /// <summary>
    /// Saves the webpart zone properties.
    /// </summary>
    protected void btnApply_Click(object sender, EventArgs e)
    {
        // Save webpart properties
        if (currentControl != null)
        {
            if (!isVariantTab)
            {
                CMSModules_PortalEngine_Controls_WebParts_WebPartZoneProperties webPartZonePropertiesElem = currentControl as CMSModules_PortalEngine_Controls_WebParts_WebPartZoneProperties;
                if (webPartZonePropertiesElem != null)
                {
                    webPartZonePropertiesElem.Save();
                }
            }
            else
            {
                IWebPartZoneProperties webPartZoneVariantElem = currentControl as IWebPartZoneProperties;
                if (webPartZoneVariantElem != null)
                {
                    webPartZoneVariantElem.Save();
                    tabsElem.SelectedTab = 1;
                }
            }
        }
    }


    /// <summary>
    /// Tabs the elem_ on tab created.
    /// </summary>
    protected string[] tabElem_OnTabCreated(CMS.SiteProvider.UIElementInfo element, string[] parameters, int tabIndex)
    {
        if (!String.IsNullOrEmpty(tabsElem.JavaScriptHandler))
        {
            parameters[1] = "if (" + tabsElem.JavaScriptHandler + ") { " + tabsElem.JavaScriptHandler + "(" + ScriptHelper.GetString(element.ElementName) + "); } ";
        }

        switch (element.ElementName.ToLower())
        {
            case "webpartzoneproperties.variant":
                String helpText = (variantMode == VariantModeEnum.MVT) ? "mvtvariant_edit" : "cpvariant_edit";
                parameters[1] += "if (window.SetHelpTopic) { window.SetHelpTopic('helpTopic', '" + helpText + "')}; ";
                bool isNewVariant = QueryHelper.GetBoolean("isnewvariant", false);
                int variantId = QueryHelper.GetInteger("variantid", 0);

                if ((variantId <= 0) || isNewVariant)
                {
                    return null;
                }
                break;

            case "webpartzoneproperties.general":
                parameters[1] += "if (window.SetHelpTopic) { window.SetHelpTopic('helpTopic', 'webpartzoneproperties')}; ";
                break;
        }

        return parameters;
    }

    #endregion
}
