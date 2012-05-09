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
using CMS.SiteProvider;
using CMS.PortalEngine;
using CMS.UIControls;
using CMS.SettingsProvider;

public partial class CMSModules_PortalEngine_UI_WebParts_Development_WebPart_Edit_Layout : SiteManagerPage
{
    private int webPartId = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Title = "Web part layout";

        webPartId = QueryHelper.GetInteger("webpartId", 0);

        // Script for UniGri's edit action 
        ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "EditAttendee",
            ScriptHelper.GetScript("function EditLayout(layoutId){" +
            "location.replace('WebPart_Edit_Layout_Frameset.aspx?layoutId=' + layoutId + '&webpartId=" + webPartId + "'); }"));

        // Initialize master page elements
        InitializeMasterPage();

        // Setup the grid
        UniGrid.WhereCondition = "WebPartLayoutWebPartID = " + webPartId;
        UniGrid.OnAction += new OnActionEventHandler(UniGrid_OnAction);
        UniGrid.ZeroRowsText = GetString("general.nodatafound");

        if (!SettingsKeyProvider.UsingVirtualPathProvider)
        {
            this.lblError.Text = GetString("WebPartLayouts.VirtualPathProviderNotRunning");
            this.lblError.Visible = true;
        }
    }


    /// <summary>
    /// 
    /// </summary>
    private void InitializeMasterPage()
    {
        // New item link
        string[,] actions = new string[1, 8];
        actions[0, 0] = "HyperLink";
        actions[0, 1] = GetString("WebPartEditLayout_List.NewItemCaption");
        actions[0, 3] = ResolveUrl("WebPart_Edit_Layout_New.aspx?webPartID=" + webPartId);
        actions[0, 5] = GetImageUrl("Objects/CMS_WebPartLayout/add.png");

        this.CurrentMaster.HeaderActions.Actions = actions;
    }


    /// <summary>
    /// Handles only delete action.
    /// </summary>
    protected void UniGrid_OnAction(string actionName, object actionArgument)
    {
        if (actionName == "delete")
        {
            WebPartLayoutInfoProvider.DeleteWebPartLayoutInfo(ValidationHelper.GetInteger(actionArgument, 0));
            UniGrid.ReloadData();
        }
    }
}
