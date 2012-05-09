using System;

using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.PortalEngine;
using CMS.URLRewritingEngine;
using CMS.UIControls;

public partial class CMSModules_Messaging_Controls_MyMessages : CMSAdminControl
{
    #region "Private variables"

    private bool mDisplayInbox = true;
    private bool mDisplayOutbox = true;
    private bool mDisplayIgnoreList = true;
    private bool mDisplayContactList = true;
    private string mParameterName = "subpage";
    private CMSAdminControls_UI_PageElements_PageTitle title = null;
    private string mNotAuthenticatedMessage = String.Empty;

    #endregion


    #region "Constants"

    private const string TAB_INBOX = "inbox";
    private const string TAB_OUTBOX = "outbox";
    private const string TAB_IGNORE = "ignor";
    private const string TAB_CONTACT = "contact";

    #endregion


    #region "Public properties"

    /// <summary>
    /// Gets or sets the message which should be displayed for public users.
    /// </summary>
    public string NotAuthenticatedMessage
    {
        get
        {
            return mNotAuthenticatedMessage;
        }
        set
        {
            mNotAuthenticatedMessage = value;
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether 'inbox' is displayed.
    /// </summary>
    public bool DisplayInbox
    {
        get
        {
            return mDisplayInbox;
        }
        set
        {
            mDisplayInbox = value;
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether 'outbox' is displayed.
    /// </summary>
    public bool DisplayOutbox
    {
        get
        {
            return mDisplayOutbox;
        }
        set
        {
            mDisplayOutbox = value;
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether 'contact list' is displayed.
    /// </summary>
    public bool DisplayContactList
    {
        get
        {
            return mDisplayContactList;
        }
        set
        {
            mDisplayContactList = value;
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether 'ignore list' is displayed.
    /// </summary>
    public bool DisplayIgnoreList
    {
        get
        {
            return mDisplayIgnoreList;
        }
        set
        {
            mDisplayIgnoreList = value;
        }
    }


    /// <summary>
    /// Gets or sets the WebPart CSS class value.
    /// </summary>
    public string CssClass
    {
        get
        {
            return pnlBody.CssClass;
        }
        set
        {
            pnlBody.CssClass = value;
        }
    }


    /// <summary>
    /// Gets or sets the query string parameter name.
    /// </summary>
    public string ParameterName
    {
        get
        {
            return mParameterName;
        }
        set
        {
            mParameterName = value;
        }
    }

    #endregion


    #region "Page events"

    /// <summary>
    /// Page load.
    /// </summary>
    /// <param name="sender">Sender</param>
    /// <param name="e">Event arguments</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        ucContactList.IsLiveSite = IsLiveSite;
        ucIgnoreList.IsLiveSite = IsLiveSite;
        ucInbox.IsLiveSite = IsLiveSite;
        ucOutbox.IsLiveSite = IsLiveSite;

        title = (CMSAdminControls_UI_PageElements_PageTitle)Page.FindControl("titleElem");

        ReloadData();
    }

    #endregion


    #region "Overridden methods"

    /// <summary>
    /// Reload data.
    /// </summary>
    public override void ReloadData()
    {
        if (StopProcessing)
        {
            // Do nothing
            ucContactList.StopProcessing = true;
            ucIgnoreList.StopProcessing = true;
            ucInbox.StopProcessing = true;
            ucOutbox.StopProcessing = true;
        }
        else
        {
            // Show content only for authenticated users
            if (CMSContext.CurrentUser.IsAuthenticated())
            {
                // Remove 'saved' parameter from querystring
                string absoluteUri = URLHelper.RemoveParameterFromUrl(URLRewriter.CurrentURL, "saved");

                // Selected page url
                string selectedPage = string.Empty;

                // Menu initialization
                tabMenu.TabControlIdPrefix = "MyMessages";
                tabMenu.UrlTarget = "_self";

                int tabCount = Convert.ToInt32(DisplayOutbox) + Convert.ToInt32(DisplayContactList) + Convert.ToInt32(DisplayIgnoreList) + Convert.ToInt32(DisplayInbox);
                tabMenu.Tabs = new string[tabCount, 5];

                int tabIndex = 0;
                int inboxTabIndex = 0;
                int outboxTabIndex = 0;
                int contactTabIndex = 0;
                int ignoreTabIndex = 0;

                if (DisplayInbox)
                {
                    tabMenu.Tabs[tabIndex, 0] = GetString("MyMessages.Inbox");
                    tabMenu.Tabs[tabIndex, 2] = HTMLHelper.HTMLEncode(URLHelper.AddParameterToUrl(absoluteUri, ParameterName, TAB_INBOX));
                    inboxTabIndex = tabIndex;
                    tabIndex++;
                    selectedPage = string.IsNullOrEmpty(selectedPage) ? TAB_INBOX : selectedPage;
                }

                if (DisplayOutbox)
                {
                    tabMenu.Tabs[tabIndex, 0] = GetString("MyMessages.Outbox");
                    tabMenu.Tabs[tabIndex, 2] = HTMLHelper.HTMLEncode(URLHelper.AddParameterToUrl(absoluteUri, ParameterName, TAB_OUTBOX));
                    outboxTabIndex = tabIndex;
                    tabIndex++;
                    selectedPage = string.IsNullOrEmpty(selectedPage) ? TAB_OUTBOX : selectedPage;
                }

                if (DisplayContactList)
                {
                    tabMenu.Tabs[tabIndex, 0] = GetString("MyMessages.ContactList");
                    tabMenu.Tabs[tabIndex, 2] = HTMLHelper.HTMLEncode(URLHelper.AddParameterToUrl(absoluteUri, ParameterName, TAB_CONTACT));
                    contactTabIndex = tabIndex;
                    tabIndex++;
                    selectedPage = string.IsNullOrEmpty(selectedPage) ? TAB_CONTACT : selectedPage;
                }

                if (DisplayIgnoreList)
                {
                    tabMenu.Tabs[tabIndex, 0] = GetString("MyMessages.IgnoreList");
                    tabMenu.Tabs[tabIndex, 2] = HTMLHelper.HTMLEncode(URLHelper.AddParameterToUrl(absoluteUri, ParameterName, TAB_IGNORE));
                    ignoreTabIndex = tabIndex;
                    selectedPage = string.IsNullOrEmpty(selectedPage) ? TAB_IGNORE : selectedPage;
                }

                // Set css class
                pnlBody.CssClass = CssClass;

                // Change tabs header css class in dashboard
                if (PortalContext.GetViewMode() == ViewModeEnum.DashboardWidgets)
                {
                    pnlHeader.CssClass = "TabsHeader LightTabs";
                }

                // Get page url
                selectedPage = QueryHelper.GetText(ParameterName, selectedPage);

                // Set controls visibility
                ucInbox.Visible = false;
                ucInbox.StopProcessing = true;
                ucInbox.EnableViewState = false;

                ucOutbox.Visible = false;
                ucOutbox.StopProcessing = true;
                ucOutbox.EnableViewState = false;

                ucIgnoreList.Visible = false;
                ucIgnoreList.StopProcessing = true;
                ucIgnoreList.EnableViewState = false;

                ucContactList.Visible = false;
                ucContactList.StopProcessing = true;
                ucContactList.EnableViewState = false;

                // Select current page
                switch (selectedPage)
                {
                    default:
                    case TAB_INBOX:
                        tabMenu.SelectedTab = inboxTabIndex;
                        ucInbox.Visible = true;
                        ucInbox.StopProcessing = false;
                        ucInbox.EnableViewState = true;
                        if (title != null)
                        {
                            title.HelpName = "helpTopic";
                            title.HelpTopicName = "my_messages_inbox";
                        }
                        break;

                    case TAB_OUTBOX:
                        tabMenu.SelectedTab = outboxTabIndex;
                        ucOutbox.Visible = true;
                        ucOutbox.StopProcessing = false;
                        ucOutbox.EnableViewState = true;
                        if (title != null)
                        {
                            title.HelpName = "helpTopic";
                            title.HelpTopicName = "my_messages_outbox";
                        }
                        break;

                    case TAB_CONTACT:
                        tabMenu.SelectedTab = contactTabIndex;
                        ucContactList.Visible = true;
                        ucContactList.StopProcessing = false;
                        ucContactList.EnableViewState = true;
                        if (title != null)
                        {
                            title.HelpName = "helpTopic";
                            title.HelpTopicName = "my_messages_contact_list";
                        }
                        break;

                    case TAB_IGNORE:
                        tabMenu.SelectedTab = ignoreTabIndex;
                        ucIgnoreList.Visible = true;
                        ucIgnoreList.StopProcessing = false;
                        ucIgnoreList.EnableViewState = true;
                        if (title != null)
                        {
                            title.HelpName = "helpTopic";
                            title.HelpTopicName = "my_messages_ignore_list";
                        }
                        break;
                }
            }
            else
            {
                if (!String.IsNullOrEmpty(NotAuthenticatedMessage))
                {
                    litMessage.Text = NotAuthenticatedMessage;
                    litMessage.Visible = true;
                    pnlBody.Visible = false;
                }
                else
                {
                    Visible = false;
                }

                ucContactList.StopProcessing = true;
                ucIgnoreList.StopProcessing = true;
                ucInbox.StopProcessing = true;
                ucOutbox.StopProcessing = true;
            }
        }
    }

    #endregion
}