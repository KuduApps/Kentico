using System;
using System.Web.UI;
using System.Web;

using CMS.TreeEngine;
using CMS.GlobalHelper;
using CMS.SiteProvider;
using CMS.CMSHelper;
using CMS.SettingsProvider;
using CMS.PortalEngine;
using CMS.PortalControls;
using CMS.UIControls;
using CMS.LicenseProvider;
using CMS.WebAnalytics;

using TreeNode = CMS.TreeEngine.TreeNode;

public partial class CMSModules_Content_CMSDesk_Edit_EditTabs : CMSContentPage, ICallbackEventHandler
{
    #region "Variables"

    private ViewModeEnum currentMode;
    private CurrentUserInfo currentUser = null;
    private int nodeId = 0;

    private bool showProductTab = false;
    private bool isMasterPage = false;
    private bool isPortalPage = false;
    private bool designEnabled = false;
    private bool authorizedPerDesign = false;
    private bool designPermissionRequired = false;
    private bool isWireframe = false;

    private int pageTabIndex = -1;
    private int designTabIndex = -1;
    private int formTabIndex = -1;
    private int productTabIndex = -1;
    private int masterTabIndex = -1;
    private int propertiesTabIndex = -1;
    private int analyticsIndex = -1;

    private string tabQuery = null;

    #endregion


    protected override void OnPreInit(EventArgs e)
    {
        base.OnPreInit(e);
        this["TabControl"] = tabsModes;
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!RequestHelper.IsPostBack())
        {
            chkWebParts.Checked = PortalHelper.DisplayContentInDesignMode;
        }

        tabQuery = QueryHelper.GetString("tabspecific", null);

        chkWebParts.Attributes.Add("onclick", "SaveSettings()");
        chkWebParts.Text = GetString("EditTabs.DisplayContent");

        ltlScript.Text += ScriptHelper.GetScript("function SaveSettings() { __theFormPostData = ''; WebForm_InitCallback(); " + ClientScript.GetCallbackEventReference(this, "'save'", "RefreshContent", null) + "; }");

        // Initialize tabs
        tabsModes.OnTabCreated += tabModes_OnTabCreated;
        tabsModes.SelectedTab = 0;
        tabsModes.UrlTarget = "contenteditview";

        // Process the page mode
        currentUser = CMSContext.CurrentUser;

        // Current Node ID
        nodeId = QueryHelper.GetInteger("nodeid", 0);

        TreeProvider tree = new TreeProvider(CMSContext.CurrentUser);
        TreeNode node = tree.SelectSingleNode(nodeId);
        if (node != null)
        {
            // Product tab
            DataClassInfo classObj = DataClassInfoProvider.GetDataClass(node.NodeClassName);
            if (classObj != null)
            {
                showProductTab = SettingsKeyProvider.GetBoolValue(CMSContext.CurrentSiteName + ".ProductTabEnabled") && classObj.ClassIsProduct;
            }

            // Initialize required variables
            authorizedPerDesign = currentUser.IsAuthorizedPerResource("CMS.Design", "Design");
            isWireframe = node.NodeClassName.Equals("CMS.Wireframe", StringComparison.InvariantCultureIgnoreCase);

            // Get page template information
            PageInfo pi = PageInfoProvider.GetPageInfo(CMSContext.CurrentSiteName, node.NodeAliasPath, node.DocumentCulture, node.DocumentUrlPath, false);
            if ((pi != null) && (pi.PageTemplateInfo != null))
            {
                PageTemplateInfo pti = pi.PageTemplateInfo;

                isPortalPage = pti.IsPortal;
                isMasterPage = isPortalPage && ((node.NodeAliasPath == "/") || pti.ShowAsMasterTemplate);
                designEnabled = ((pti.PageTemplateType == PageTemplateTypeEnum.Portal) || (pti.PageTemplateType == PageTemplateTypeEnum.AspxPortal));
            }

            // Do not show design tab for CMS.File
            if (node.NodeClassName.Equals("CMS.File", StringComparison.InvariantCultureIgnoreCase))
            {
                designEnabled = false;
            }

            // Update view mode
            UpdateViewMode(ViewModeEnum.Edit);

            // Get the page mode
            currentMode = CMSContext.ViewMode;
        }
    }


    string[] tabModes_OnTabCreated(UIElementInfo element, string[] parameters, int tabIndex)
    {
        bool splitViewSupported = false;
        
        // Script for refresh 'display webpart content' button
        String script = "SetTabsContext('');";            

        switch (element.ElementName.ToLower())
        {
            case "page":
                // Page tab is not allowed when wireframe
                if (isWireframe)
                {
                    return null;
                }

                pageTabIndex = tabIndex;
                splitViewSupported = true;
                break;

            case "wireframe":
                // Check if the wireframe mode is enabled
                if (!isWireframe || !designEnabled)
                {
                    return null;
                }

                if (authorizedPerDesign)
                {
                    designTabIndex = tabIndex;
                    splitViewSupported = true;
                }
                else
                {
                    if (!authorizedPerDesign)
                    {
                        designPermissionRequired = true;
                    }
                    return null;
                }
                break;

            case "design":
                // Check if the design mode is enabled
                if (isWireframe || !designEnabled)
                {
                    return null;
                }
                
                script = "SetTabsContext('design');";         

                if (authorizedPerDesign)
                {
                    designTabIndex = tabIndex;
                    splitViewSupported = true;
                }
                else
                {
                    if (!authorizedPerDesign)
                    {
                        designPermissionRequired = true;
                    }
                    return null;
                }
                break;

            case "editform":
                formTabIndex = tabIndex;
                splitViewSupported = true;
                break;

            case "product":
                // Check if the product tab is enabled
                if (showProductTab && LicenseHelper.IsFeatureAvailableInUI(FeatureEnum.Ecommerce, ModuleEntry.ECOMMERCE))
                {
                    productTabIndex = tabIndex;
                }
                else
                {
                    return null;
                }
                break;

            case "masterpage":
                // Check if master page tab is enabled
                if (isMasterPage && authorizedPerDesign)
                {
                    masterTabIndex = tabIndex;
                    splitViewSupported = true;
                }
                else
                {
                    if (!authorizedPerDesign)
                    {
                        designPermissionRequired = true;
                    }
                    return null;
                }
                break;

            case "properties":
                parameters[2] += (tabQuery != null ? "&tab=" + tabQuery : "");
                propertiesTabIndex = tabIndex;
                break;

            case "analytics":
                analyticsIndex = tabIndex;
                break;

            case "validation":
                return null;
        }

        // Ensure split view mode
        if (splitViewSupported && CMSContext.DisplaySplitMode)
        {
            parameters[2] = GetSplitViewUrl(parameters[2]);
        }

        // Add script for refresh webpartcontent button (displayed only when design mode)
        parameters[1] = script + parameters[1];

        return parameters;
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        SelectTab();
    }


    private void SelectTab()
    {
        switch (QueryHelper.GetString("action", "edit").ToLower())
        {
            // New dialog / new page form
            case "new":
            case "newculture":
                tabsModes.SelectedTab = pageTabIndex;
                break;

            case "properties":
                // New document culture
                tabsModes.SelectedTab = propertiesTabIndex;
                break;

            case "product":
                tabsModes.SelectedTab = productTabIndex;
                break;

            default:
                // Set the radio buttons
                switch (currentMode)
                {
                    case ViewModeEnum.Edit:
                        tabsModes.SelectedTab = pageTabIndex;
                        break;

                    case ViewModeEnum.Design:
                        tabsModes.SelectedTab = designTabIndex;
                        ltlScript.Text += ScriptHelper.GetScript("SetTabsContext('design');");
                        break;

                    case ViewModeEnum.EditForm:
                        tabsModes.SelectedTab = formTabIndex;
                        break;

                    case ViewModeEnum.MasterPage:
                        tabsModes.SelectedTab = masterTabIndex;
                        break;

                    case ViewModeEnum.Product:
                        tabsModes.SelectedTab = productTabIndex;
                        break;

                    case ViewModeEnum.Properties:
                        tabsModes.SelectedTab = propertiesTabIndex;
                        break;

                    case ViewModeEnum.Analytics:
                        tabsModes.SelectedTab = analyticsIndex;
                        break;
                }
                break;
        }

        // Selected tab (for first load)
        if (tabsModes.SelectedTab == -1)
        {
            tabsModes.SelectedTab = 0;
        }

        tabsModes.DoTabSelection(designPermissionRequired, "CMS.Content", "Design");
    }


    #region "Callback handling"

    public void RaiseCallbackEvent(string eventArgument)
    {
        if (eventArgument == "save")
        {
            PortalHelper.DisplayContentInDesignMode = chkWebParts.Checked;
        }
    }


    public string GetCallbackResult()
    {
        return "";
    }

    #endregion
}
