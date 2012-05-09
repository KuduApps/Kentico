using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.GlobalHelper;
using CMS.SettingsProvider;
using CMS.PortalEngine;
using CMS.UIControls;

public partial class CMSModules_PortalEngine_UI_WebContainers_Container_List : SiteManagerPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Set master page title
        InitializeMasterPage();

        UniGrid.OnAction += new OnActionEventHandler(uniGrid_OnAction);
        UniGrid.ZeroRowsText = GetString("general.nodatafound");
    }


    /// <summary>
    /// Initializes the mastre page elements.
    /// </summary>
    private void InitializeMasterPage()
    {
        // Set page title
        this.CurrentMaster.Title.TitleText = GetString("Container_List.HeaderCaption");
        this.CurrentMaster.Title.TitleImage = GetImageUrl("Objects/CMS_WebPartContainer/object.png");
        this.CurrentMaster.Title.HelpTopicName = "web_part_containers_list";
        this.CurrentMaster.Title.HelpName = "helpTopic";

        // Set actions
        string[,] actions = new string[1, 8];
        actions[0, 0] = "HyperLink";
        actions[0, 1] = GetString("Container_List.NewItemCaption");
        actions[0, 3] = ResolveUrl("Container_New.aspx");
        actions[0, 5] = GetImageUrl("Objects/CMS_WebPartContainer/add.png");

        this.CurrentMaster.HeaderActions.Actions = actions;
    }


    /// <summary>
    /// Handles the UniGrid's OnAction event.
    /// </summary>
    /// <param name="actionName">Name of item (button) that throws event</param>
    /// <param name="actionArgument">ID (value of Primary key) of corresponding data row</param>
    protected void uniGrid_OnAction(string actionName, object actionArgument)
    {
        switch (actionName.ToLower())
        {
            case "edit":
                URLHelper.Redirect(string.Format("Container_Edit_Frameset.aspx?containerid={0}&hash={1}&tabmode=1", 
                                                 Convert.ToString(actionArgument), 
                                                 QueryHelper.GetHash(string.Empty)));
                break;

            case "delete":
                WebPartContainerInfoProvider.DeleteWebPartContainerInfo(Convert.ToInt32(actionArgument));
                break;
        }
    }
}
