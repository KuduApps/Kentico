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

public partial class CMSModules_Notifications_Development_Notifications_Header : CMSNotificationsPage
{
    protected void Page_Load(object sender, EventArgs e)
    {       
        // Intialize the control
        SetupControl();
    }


    #region "Private methods"

    /// <summary>
    /// Initializes the controls.
    /// </summary>
    private void SetupControl()
    {
        // Set the page title
        this.CurrentMaster.Title.TitleText = GetString("notifications.header.title");
        this.CurrentMaster.Title.TitleImage = GetImageUrl("Objects/Notification_Gateway/object.png");
        this.CurrentMaster.Title.HelpTopicName = "notification_gateways";
        this.CurrentMaster.Title.HelpName = "helpTopic";

        InitalizeMenu();
    }


    /// <summary>
    /// Initialize the tab control on the master page.
    /// </summary>
    private void InitalizeMenu()
    {
        // Collect tabs data
        string[,] tabs = new string[2, 4];
        tabs[0, 0] = GetString("notifications.header.gateways");
        tabs[0, 1] = "SetHelpTopic('helpTopic', 'notification_gateways');";
        tabs[0, 2] = "Gateways/Gateway_List.aspx";

        tabs[1, 0] = GetString("notifications.header.templates");
        tabs[1, 1] = "SetHelpTopic('helpTopic', 'notification_templates');";
        tabs[1, 2] = "Templates/Template_List.aspx";

        // Set the target iFrame
        this.CurrentMaster.Tabs.UrlTarget = "notificationsContent";

        // Assign tabs data
        this.CurrentMaster.Tabs.Tabs = tabs;
    }

    #endregion   
}
