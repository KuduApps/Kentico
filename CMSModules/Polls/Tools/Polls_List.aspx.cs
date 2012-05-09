using System;

using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.CMSHelper;
using CMS.SettingsProvider;

public partial class CMSModules_Polls_Tools_Polls_List : CMSPollsPage
{
    private bool globAndSite = false;

    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        InitMaster();

        // Set poll list control
        PollsList.OnEdit += new EventHandler(PollsList_OnEdit);
        PollsList.WhereCondition = fltSite.GetWhereCondition();
        PollsList.IsLiveSite = false;

        // Disables creating of new poll when "global and sites objects" is selected
        globAndSite = (fltSite.SiteID == UniSelector.US_GLOBAL_OR_SITE_RECORD);
        PollsList.DisplayGlobalColumn = globAndSite;
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        // Disables creating of new poll when "global and sites objects" is selected
        hdrActions.Enabled = !globAndSite;
        lblWarnNew.Visible = globAndSite;
    }

    #endregion


    #region "Event handlers"

    /// <summary>
    /// Edit poll click handler.
    /// </summary>
    void PollsList_OnEdit(object sender, EventArgs e)
    {
        // Propagate selected item from ddlist to breadcrumbs on edit page
        URLHelper.Redirect(ResolveUrl("Polls_Edit.aspx?pollId=" + this.PollsList.SelectedItemID + "&siteid=" + fltSite.SiteID));
    }

    #endregion


    #region "Private methods"

    /// <summary>
    /// Initializes master page and header actions.
    /// </summary>
    private void InitMaster()
    {
        // Init filter for the first time according to user permissions
        if (!RequestHelper.IsPostBack())
        {
            if (AuthorizedForGlobalPolls && AuthorizedForSitePolls)
            {
                fltSite.SiteID = QueryHelper.GetInteger("siteid", CMSContext.CurrentSiteID);
            }
            else if (AuthorizedForSitePolls)
            {
                // User is authorized for site polls => select site polls
                fltSite.SiteID = CMSContext.CurrentSiteID;
            }
            else
            {
                // User is authorized for global polls => select global polls only
                fltSite.SiteID = UniSelector.US_GLOBAL_RECORD;
            }
        }

        // New item link
        string[,] actions = new string[1, 6];
        actions[0, 0] = HeaderActions.TYPE_HYPERLINK;
        actions[0, 1] = GetString("Polls_List.NewItemCaption");
        actions[0, 2] = null;
        actions[0, 3] = ResolveUrl("Polls_New.aspx?siteid=" + fltSite.SiteID);
        actions[0, 4] = null;
        actions[0, 5] = GetImageUrl("Objects/Polls_Poll/add.png");

        hdrActions.Actions = actions;

        // Set the page title
        this.CurrentMaster.Title.TitleText = GetString("Polls_List.HeaderCaption");
        this.CurrentMaster.Title.TitleImage = GetImageUrl("Objects/Polls_Poll/object.png");
        this.CurrentMaster.Title.HelpName = "helpTopic";
        this.CurrentMaster.Title.HelpTopicName = "polls_list";
        this.CurrentMaster.DisplaySiteSelectorPanel = AuthorizedForSitePolls && AuthorizedForGlobalPolls;
    }

    #endregion
}
