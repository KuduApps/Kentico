using System;

using CMS.GlobalHelper;
using CMS.UIControls;

public partial class CMSModules_Messaging_MyMessages_MyMessages_Header : CMSMyMessagesPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        CurrentMaster.Title.TitleText = GetString("mydesk.mymessages");
        CurrentMaster.Title.TitleImage = GetImageUrl("CMSModules/CMS_Messaging/module.png");
        CurrentMaster.Title.HelpTopicName = "my_messages_inbox";
        CurrentMaster.Title.HelpName = "helpTopic";

        if (!RequestHelper.IsPostBack())
        {
            InitalizeMenu();
        }
    }


    #region "Private methods"

    /// <summary>
    /// Initializes menu.
    /// </summary>
    protected void InitalizeMenu()
    {
        string[,] tabs = new string[4, 4];
        tabs[0, 0] = GetString("MyMessages.Inbox");
        tabs[0, 1] = "SetHelpTopic('helpTopic', 'my_messages_inbox');";
        tabs[0, 2] = "MyMessages_Inbox.aspx" + URLHelper.Url.Query;

        tabs[1, 0] = GetString("MyMessages.Outbox");
        tabs[1, 1] = "SetHelpTopic('helpTopic', 'my_messages_outbox');";
        tabs[1, 2] = "MyMessages_Outbox.aspx" + URLHelper.Url.Query;

        tabs[2, 0] = GetString("MyMessages.ContactList");
        tabs[2, 1] = "SetHelpTopic('helpTopic', 'my_messages_contactlist');";
        tabs[2, 2] = "MyMessages_ContactList.aspx" + URLHelper.Url.Query;

        tabs[3, 0] = GetString("MyMessages.IgnoreList");
        tabs[3, 1] = "SetHelpTopic('helpTopic', 'my_messages_ignorelist');";
        tabs[3, 2] = "MyMessages_IgnoreList.aspx" + URLHelper.Url.Query;
        CurrentMaster.Tabs.UrlTarget = "messagesContent";
        CurrentMaster.Tabs.Tabs = tabs;
    }

    #endregion
}
