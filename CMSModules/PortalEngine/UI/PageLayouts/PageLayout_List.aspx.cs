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
using CMS.SettingsProvider;
using CMS.UIControls;
using CMS.PortalEngine;

public partial class CMSModules_PortalEngine_UI_PageLayouts_PageLayout_List : SiteManagerPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!SettingsKeyProvider.UsingVirtualPathProvider)
        {
            this.lblError.Text = GetString("Layouts.VirtualPathProviderNotRunning");
            this.lblError.Visible = true;
        }

        this.CurrentMaster.Title.TitleText = GetString("Administration-PageLayout_List.Title");
        this.CurrentMaster.Title.TitleImage = GetImageUrl("Objects/CMS_Layout/object.png");
        this.CurrentMaster.Title.HelpTopicName = "page_layouts_list";
        this.CurrentMaster.Title.HelpName = "helpTopic";

        // New item link
        string[,] actions = new string[1, 6];
        actions[0, 0] = HeaderActions.TYPE_HYPERLINK;
        actions[0, 1] = GetString("Administration-PageLayout_List.NewLayout");
        actions[0, 2] = null;
        actions[0, 3] = ResolveUrl("PageLayout_Edit.aspx");
        actions[0, 4] = null;
        actions[0, 5] = GetImageUrl("Objects/CMS_Layout/add.png");
        this.CurrentMaster.HeaderActions.Actions = actions;

        gridLayouts.OnAction += new OnActionEventHandler(UniGridModules_OnAction);
        gridLayouts.ZeroRowsText = GetString("general.nodatafound");
    }


    /// <summary>
    /// Handles the UniGrid's OnAction event.
    /// </summary>
    /// <param name="actionName">Name of item (button) that threw event</param>
    /// <param name="actionArgument">ID (value of Primary key) of corresponding data row</param>
    protected void UniGridModules_OnAction(string actionName, object actionArgument)
    {
        if (actionName == "edit")
        {
            URLHelper.Redirect("PageLayout_Frameset.aspx?layoutId=" + actionArgument.ToString());
        }
        else if (actionName == "delete")
        {
            LayoutInfoProvider.DeleteLayoutInfo(Convert.ToInt32(actionArgument));
        }
    }
}
