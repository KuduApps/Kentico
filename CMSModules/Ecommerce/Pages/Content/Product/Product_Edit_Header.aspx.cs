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
using CMS.Ecommerce;
using CMS.FormEngine;
using CMS.UIControls;
using CMS.SiteProvider;
using CMS.WorkflowEngine;
using CMS.SettingsProvider;
using CMS.CMSHelper;

public partial class CMSModules_Ecommerce_Pages_Content_Product_Product_Edit_Header : CMSContentProductPage
{
    private int productId = 0;

    protected void Page_PreInit(object sender, EventArgs e)
    {
        // Get product name ane option category ID
        this.NodeID = QueryHelper.GetInteger("nodeid", 0);
        productId = QueryHelper.GetInteger("productId", 0);
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.Node != null)
        {
            if (!IsAuthorizedToModifyDocument())
            {
                // Disable form editing                                                            
                chkMarkDocAsProd.Enabled = false;
            }
        }

        // Ensure page with changes saved message is loaded initially if required
        if (QueryHelper.GetInteger("saved", 0) == 1)
        {
            this.CurrentMaster.Tabs.StartPageURL = "Product_Edit_General.aspx" + URLHelper.Url.Query;
        }

        CMSMasterPage master = (CMSMasterPage)this.CurrentMaster;

        master.Tabs.OnTabCreated += new UITabs.TabCreatedEventHandler(Tabs_OnTabCreated);
        master.Tabs.UrlTarget = "ProductContent";
        master.Tabs.ModuleName = "CMS.Content";
        master.Tabs.ElementName = "Product";

        master.DisplaySiteSelectorPanel = true;

        string confirmationScript = "if (confirm(" + ScriptHelper.GetString(GetString("com.product.contentconfirmation")) + ")) { " + this.Page.ClientScript.GetPostBackEventReference(this.chkMarkDocAsProd, null) + " } return false;";
        this.chkMarkDocAsProd.Attributes.Add("onclick", confirmationScript);
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        this.chkMarkDocAsProd.Visible = !this.CurrentMaster.Tabs.TabsEmpty;
    }


    protected string[] Tabs_OnTabCreated(CMS.SiteProvider.UIElementInfo element, string[] parameters, int tabIndex)
    {
        if (element.ElementName.ToLower() == "contentproduct.customfields")
        {
            // Check if SKU has any custom fields
            FormInfo formInfo = FormHelper.GetFormInfo("ecommerce.sku", false);
            if (formInfo.GetFormElements(true, false, true).Count <= 0)
            {
                return null;
            }
        }

        return parameters;
    }


    protected void chkMarkDocAsProd_CheckedChanged(object sender, EventArgs e)
    {
        if (!IsAuthorizedToModifyDocument())
        {
            RedirectToAccessDenied("CMS.Content", "Modify");
        }

        this.Node.NodeSKUID = 0;
        this.Node.Update();

        // Update search index for node
        if ((this.Node.PublishedVersionExists) && (SearchIndexInfoProvider.SearchEnabled))
        {
            SearchTaskInfoProvider.CreateTask(SearchTaskTypeEnum.Update, PredefinedObjectType.DOCUMENT, SearchHelper.ID_FIELD, this.Node.GetSearchID());
        }

        // Log synchronization
        DocumentSynchronizationHelper.LogDocumentChange(this.Node, TaskTypeEnum.UpdateDocument, this.Node.TreeProvider);

        ScriptHelper.RegisterStartupScript(Page, typeof(string), "FirstTabSelection", ScriptHelper.GetScript(" parent.window.location = '" + URLHelper.ResolveUrl("~/CMSModules/Ecommerce/Pages/Content/Product/Product_Selection.aspx") + "?nodeid=" + this.NodeID + "&productid=" + productId + "' "));
    }
}
