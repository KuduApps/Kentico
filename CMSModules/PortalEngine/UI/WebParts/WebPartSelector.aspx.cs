using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.UIControls;
using CMS.GlobalHelper;
using CMS.CMSHelper;

public partial class CMSModules_PortalEngine_UI_WebParts_WebPartSelector : CMSModalDesignPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Check UI elements for web part zone
        CurrentUserInfo currentUser = CMSContext.CurrentUser;
        if (!currentUser.IsAuthorizedPerUIElement("CMS.Content", new string[] { "Design", "Design.AddWebParts" }, CMSContext.CurrentSiteName))
        {
            RedirectToCMSDeskUIElementAccessDenied("CMS.Content", "Design;Design.AddWebParts");
        }

        // Set dialog body class
        CurrentMaster.PanelBody.CssClass = "DialogPageBody";
        selectElem.ShowInheritedWebparts = true;

        // Proceeds the current item selection
        string javascript = @"
            function SelectCurrentWebPart() 
            {
                SelectWebPart(selectedValue) ;
            }
            function SelectWebPart(value)
            {
                if( value != null )
                {
                    window.close();
                    if ( wopener.OnSelectWebPart )
                    {
                        wopener.OnSelectWebPart(value);
                    }	            
		        }
		        else
		        {
                    alert(document.getElementById('" + hdnMessage.ClientID + @"').value);		    
		        }                
            }            
            // Cancel action
            function Cancel()
            {
                window.close();
            } ";

        ScriptHelper.RegisterStartupScript(this, typeof(string), "WebPartSelector", ScriptHelper.GetScript(javascript));

        // Set name of selection function
        selectElem.SelectFunction = "SelectWebPart";
        selectElem.ShowWireframeOnlyWebparts = QueryHelper.GetBoolean("wireframe", false);

        // Set the title and icon
        string title = GetString("portalengine-webpartselection.title");
        this.Page.Title = title;
        this.CurrentMaster.Title.TitleText = title;
        this.CurrentMaster.Title.TitleImage = GetImageUrl("Objects/CMS_WebPart/object.png");
        
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
