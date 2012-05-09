using System;

using CMS.GlobalHelper;
using CMS.UIControls;

public partial class CMSModules_EmailQueue_EmailQueue_Header : SiteManagerPage
{
    #region "Variables"

    int siteId;


    int selectedTab;

    #endregion


    #region "Methods"

    protected override void OnPreInit(EventArgs e)
    {
        base.OnPreInit(e);

        this["TabControl"] = BasicTabControlMenu;
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!RequestHelper.IsPostBack())
        {
            siteId = QueryHelper.GetInteger("siteid", -1);
            selectedTab = QueryHelper.GetInteger("selectedTab", 0);
        }

        // Set site selector
        siteSelector.OnlyRunningSites = false;
        siteSelector.DropDownSingleSelect.AutoPostBack = true;
        siteSelector.UniSelector.SpecialFields = new string[1, 2] { { GetString("general.global"), "0" } };
        siteSelector.UniSelector.OnSelectionChanged += UniSelector_OnSelectionChanged;

        if (!RequestHelper.IsPostBack())
        {
            siteSelector.Value = siteId;
        }
        else
        {
            siteId = ValidationHelper.GetInteger(siteSelector.Value, -1);
        }


        ScriptHelper.RegisterStartupScript(this, GetType(), "selectedSiteId", string.Format("var selectedSiteId = {0};", siteId), true);

        BasicTabControlMenu.Tabs = new string[3, 4];
        BasicTabControlMenu.Tabs[0, 0] = GetString("emailqueue.queue.title");
        BasicTabControlMenu.Tabs[0, 1] = "SetHelpTopic('helpTopic', 'email_queue'); return false;";
        BasicTabControlMenu.Tabs[0, 2] = "EmailQueue.aspx?siteid=' + selectedSiteId+'";
        BasicTabControlMenu.Tabs[1, 0] = GetString("emailqueue.archive.title");
        BasicTabControlMenu.Tabs[1, 1] = "SetHelpTopic('helpTopic', 'sent_emails'); return false;";
        BasicTabControlMenu.Tabs[1, 2] = "SentEmails.aspx?siteid='+ selectedSiteId+'";
        BasicTabControlMenu.Tabs[2, 0] = GetString("emailqueue.send.title");
        BasicTabControlMenu.Tabs[2, 1] = "SetHelpTopic('helpTopic', 'send_email'); return false;";
        BasicTabControlMenu.Tabs[2, 2] = "SendEmail.aspx?siteid='+ selectedSiteId+'";
        BasicTabControlMenu.UrlTarget = "content";
        BasicTabControlMenu.SelectedTab = selectedTab;

        titleElem.TitleText = GetString("emailqueue.queue.title");
        titleElem.TitleImage = GetImageUrl("CMSModules/CMS_EmailQueue/module.png");

        titleElem.HelpTopicName = "email_queue";
        titleElem.HelpName = "helpTopic";
    }


    /// <summary>
    /// Handles site selection change event.
    /// </summary>
    protected void UniSelector_OnSelectionChanged(object sender, EventArgs e)
    {
        string newVariable = @"
                    function OnSelectedIndex() {
                        selectedTab = document.getElementById('TabControlSelItemNo');
                        if ((selectedTab != null) && (typeof(selectedSiteId) != 'undefined')) {
                            if (selectedTab.value == '0')
                                parent.frames['content'].location = 'EmailQueue.aspx?siteid=' + selectedSiteId;
                            else if (selectedTab.value == '1')
                                parent.frames['content'].location = 'SentEmails.aspx?siteid='+ selectedSiteId;
                            else
                                parent.frames['content'].location = 'SendEmail.aspx?siteid='+ selectedSiteId;                       
                        }
                    }
                    OnSelectedIndex();";

        ScriptHelper.RegisterStartupScript(this, GetType(), "OnSelectedIndex", newVariable, true);
    }

    #endregion
}