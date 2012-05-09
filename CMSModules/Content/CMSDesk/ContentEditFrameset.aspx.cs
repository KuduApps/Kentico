using System;

using CMS.TreeEngine;
using CMS.CMSHelper;
using CMS.SettingsProvider;
using CMS.GlobalHelper;
using CMS.PortalEngine;
using CMS.UIControls;
using CMS.WorkflowEngine;

using TreeNode = CMS.TreeEngine.TreeNode;

public partial class CMSModules_Content_CMSDesk_ContentEditFrameset : CMSContentPage
{
    protected string viewpage = "about:blank";
    protected string tabspage = "Edit/edittabs.aspx" + URLHelper.Url.Query;

    protected void Page_Load(object sender, EventArgs e)
    {
        // Register script files
        ScriptHelper.RegisterScriptFile(Page, "~/CMSModules/Content/CMSDesk/ContentEditFrameset.js");

        // Current Node ID
        int nodeId = QueryHelper.GetInteger("nodeid", 0);

        ViewModeEnum currentMode = ViewModeEnum.Edit;

        // Get the node
        TreeProvider tree = new TreeProvider(CMSContext.CurrentUser);
        tree.CombineWithDefaultCulture = false;

        TreeNode node = DocumentHelper.GetDocument(nodeId, CMSContext.PreferredCultureCode, tree);
        if (node != null)
        {
            currentMode = CMSContext.ViewMode;

            // Check the product mode
            if (currentMode == ViewModeEnum.Product)
            {
                bool showProductTab = false;

                // Product tab has to be allowed in settings and also the doc. type has to be marked as a product
                DataClassInfo classObj = DataClassInfoProvider.GetDataClass(node.NodeClassName);
                if (classObj != null)
                {
                    showProductTab = SettingsKeyProvider.GetBoolValue(CMSContext.CurrentSiteName + ".ProductTabEnabled") && classObj.ClassIsProduct;
                }

                if (!showProductTab)
                {
                    currentMode = ViewModeEnum.Properties;
                    CMSContext.ViewMode = ViewModeEnum.Properties;
                }
            }

            // Get the name for automatic title
            string name = (node.NodeAliasPath == "/" ? CMSContext.CurrentSite.DisplayName : node.DocumentName);
            if (!String.IsNullOrEmpty(name))
            {
                ScriptHelper.RegisterTitleScript(this, ResHelper.LocalizeString(name));
            }
        }
        else
        {
            // Document does not exist -> redirect to new culture version creation dialog
            URLHelper.Redirect("~/CMSModules/Content/CMSDesk/New/NewCultureVersion.aspx" + URLHelper.Url.Query);
        }

        switch (ValidationHelper.GetString(Request.QueryString["action"], "edit").ToLower())
        {
            case "properties":
                viewpage = "~/CMSModules/Content/CMSDesk/Properties/default.aspx";
                string tab = QueryHelper.GetString("tab", null);
                if (!string.IsNullOrEmpty(tab))
                {
                    tabspage = URLHelper.RemoveParameterFromUrl(tabspage, "tab");
                    tabspage = URLHelper.AddParameterToUrl(tabspage, "tabspecific", tab);
                }
                break;

            case "product":
                viewpage = "~/CMSModules/Ecommerce/Pages/Content/Product/Product_Selection.aspx";
                break;

            default:
                // Set the radio buttons
                switch (currentMode)
                {
                    case ViewModeEnum.Product:
                        viewpage = "~/CMSModules/Ecommerce/Pages/Content/Product/Product_Selection.aspx";
                        break;

                    case ViewModeEnum.Properties:
                        viewpage = "~/CMSModules/Content/CMSDesk/Properties/default.aspx";
                        break;

                    default:
                        viewpage = "~/CMSModules/Content/CMSDesk/Edit/editframeset.aspx";
                        break;
                }
                break;
        }

        viewpage += URLHelper.Url.Query;

        // Split mode enabled
        if (CMSContext.DisplaySplitMode)
        {
            viewpage = GetSplitViewUrl(viewpage);
        }

        viewpage = ResolveUrl(viewpage);
    }
}
