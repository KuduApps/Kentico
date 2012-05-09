using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.UIControls;
using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.TreeEngine;

using TreeNode = CMS.TreeEngine.TreeNode;

public partial class CMSModules_PortalEngine_UI_Layout_PageTemplateSelector : CMSModalPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Check the authorization per UI element
        CurrentUserInfo currentUser = CMSContext.CurrentUser;
        if (!currentUser.IsAuthorizedPerUIElement("CMS.Content", new string[] { "Properties", "Properties.Template" }, CMSContext.CurrentSiteName))
        {
            RedirectToCMSDeskUIElementAccessDenied("CMS.Content", "Properties;Properties.Template");
        }

        string selectorid = QueryHelper.GetString("selectorid", "");
        string selectedItem = QueryHelper.GetString("selectedPageTemplateId", "");        
        selectElem.DocumentID = QueryHelper.GetInteger("documentid", 0);

        selectElem.ShowOnlySiteTemplates = ValidationHelper.GetBoolean(WindowHelper.GetItem("ShowOnlySiteTemplates"), selectElem.ShowOnlySiteTemplates);

        // If document id is not defined try get id from nodeid if is available
        if (selectElem.DocumentID <= 0)
        {
            int nodeId = QueryHelper.GetInteger("nodeid", 0);
            if (nodeId > 0)
            {
                TreeProvider tp = new TreeProvider(CMSContext.CurrentUser);
                TreeNode tn = tp.SelectSingleNode(nodeId);
                if (tn != null)
                {
                    selectElem.DocumentID = tn.DocumentID;
                }
            }
        }
        selectElem.IsNewPage = QueryHelper.GetBoolean("isnewpage", false);

        // Proceeds the current item selection
        string javascript = @"
            function SelectCurrentPageTemplate()
            {                      
                SelectPageTemplate(selectedValue);                
            }
            function SelectPageTemplate(value)
            {                                
                if (value != null)
                {                                                            
                    if (wopener.OnSelectPageTemplate)
                    {                       
                        // Get selecten item name for this selector
                        var name = $j('.FlatSelectedItem .SelectorFlatText').text().trim();                        
                        if (name == '') {
                            name = selectedItemName;                      
                        }
                        var portal = ($j('#selectedTemplateIsPortal').val() == 'true');                         
                        var reusable = ($j('#selectedTemplateIsReusable').val() == 'true'); 
                        
                        wopener.OnSelectPageTemplate(value, name, '" + ScriptHelper.GetString(selectorid,false) + @"', portal, reusable);
                    }
                    window.close();            
		        }
		        else
		        {
                    alert(""" + GetString("PageTemplateSelection.NoPageTemplateSelected") + @""");		    
		        }                
            }            
            // Cancel action
            function Cancel()
            {
                window.close();
            } ";

        ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "PageTemplateSelector", ScriptHelper.GetScript(javascript));

        // Set name of selection function for double click
        selectElem.SelectFunction = "SelectPageTemplate";        

        // Preset item
        if (!RequestHelper.IsPostBack())
        {
            selectElem.SelectedItem = selectedItem;
        }

        // Set the title and icon
        this.Page.Title = GetString("portalengine-PageTemplateSelection.title");
        this.CurrentMaster.Title.TitleText = this.Page.Title;
        this.CurrentMaster.Title.TitleImage = GetImageUrl("Objects/CMS_PageTemplate/object.png");

        // Remove default css class
        if (this.CurrentMaster.PanelBody != null)
        {
            Panel pnl = this.CurrentMaster.PanelBody.FindControl("pnlContent") as Panel;
            if (pnl != null)
            {
                pnl.CssClass = String.Empty;
            }
        }
    }
}
