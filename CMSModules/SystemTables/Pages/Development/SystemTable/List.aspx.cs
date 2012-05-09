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

public partial class CMSModules_SystemTables_Pages_Development_SystemTable_List : SiteManagerPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.CurrentMaster.Title.HelpTopicName = "system_tables_list";
        this.CurrentMaster.Title.HelpName = "helpTopic";

        // This invisible blank action button allows adding context help
        string[,] actions = new string[1, 11];
        actions[0, 0] = HeaderActions.TYPE_HYPERLINK;
        actions[0, 10] = "false";
        this.CurrentMaster.HeaderActions.Actions = actions;
        this.CurrentMaster.HeaderActions.HelpTopicName = "system_tables_list";
        this.CurrentMaster.HeaderActions.HelpName = "helpTopic";

        UniGridSysTables.OnAction += new OnActionEventHandler(UniGridSysTables_OnAction);
        UniGridSysTables.ZeroRowsText = GetString("general.nodatafound");
    }


    /// <summary>
    /// Handles the UniGrid's OnAction event.
    /// </summary>
    /// <param name="actionName">Name of item (button) that threw event</param>
    /// <param name="actionArgument">ID (value of Primary key) of corresponding data row</param>
    protected void UniGridSysTables_OnAction(string actionName, object actionArgument)
    {
        switch (actionName.ToLower())
        {
            case "edit":
                URLHelper.Redirect("Frameset.aspx?classid=" + Convert.ToString(actionArgument));
                break;
        }
    }
}
