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
using CMS.UIControls;
using CMS.CMSHelper;

public partial class CMSModules_CssStylesheets_Pages_CssStylesheet_List : SiteManagerPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        UniGridCssStyleSheets.OnAction += new OnActionEventHandler(UniGridRoles_OnAction);
        UniGridCssStyleSheets.ZeroRowsText = GetString("general.nodatafound");

        this.CurrentMaster.Title.TitleText = GetString("CssStylesheet.CssStylesheets");
        this.CurrentMaster.Title.TitleImage = GetImageUrl("Objects/CMS_CSSStylesheet/object.png");
        this.CurrentMaster.Title.HelpTopicName = "stylesheets_list";
        this.CurrentMaster.Title.HelpName = "helpTopic";

        // New item link
        string[,] actions = new string[1, 6];
        actions[0, 0] = HeaderActions.TYPE_HYPERLINK;
        actions[0, 1] = GetString("CssStylesheet.New");
        actions[0, 2] = null;
        actions[0, 3] = ResolveUrl("CssStylesheet_New.aspx?hash=" + QueryHelper.GetHash(""));
        actions[0, 4] = null;
        actions[0, 5] = GetImageUrl("Objects/CMS_CSSStylesheet/add.png");
        this.CurrentMaster.HeaderActions.Actions = actions;
    }


    /// <summary>
    /// Handles the UniGrid's OnAction event.
    /// </summary>
    /// <param name="actionName">Name of item (button) that threw event</param>
    /// <param name="actionArgument">ID (value of Primary key) of corresponding data row</param>
    protected void UniGridRoles_OnAction(string actionName, object actionArgument)
    {
        if (actionName == "edit")
        {
            URLHelper.Redirect("CssStylesheet_Edit.aspx?cssstylesheetid=" + actionArgument.ToString() + "&tabmode=1");
        }
        else if (actionName == "delete")
        {
            CssStylesheetInfoProvider.DeleteCssStylesheetInfo(Convert.ToInt32(actionArgument));
        }
    }
}
