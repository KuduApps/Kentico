using System;

using CMS.SiteProvider;
using CMS.GlobalHelper;
using CMS.UIControls;

public partial class CMSModules_InlineControls_Pages_Development_Header : SiteManagerPage
{
    protected int inlinecontrolId;


    protected void Page_Load(object sender, EventArgs e)
    {
        // Get inline control ID from querystring
        inlinecontrolId = QueryHelper.GetInteger("inlinecontrolid", 0);

        if (!RequestHelper.IsPostBack())
        {
            InitalizeMenu();
        }

        string currentInlineControlName = null;
        InlineControlInfo iControl = InlineControlInfoProvider.GetInlineControlInfo(inlinecontrolId);
        if (iControl != null)
        {
            currentInlineControlName = iControl.ControlDisplayName;
        }

        // Initializes page title
        string[,] pageTitleTabs = new string[2, 3];
        pageTitleTabs[0, 0] = GetString("InlineControl_Header.Inlinecontrols");
        pageTitleTabs[0, 1] = "~/CMSModules/InlineControls/Pages/Development/List.aspx";
        pageTitleTabs[0, 2] = "_parent";
        pageTitleTabs[1, 0] = currentInlineControlName;
        pageTitleTabs[1, 1] = string.Empty;
        pageTitleTabs[1, 2] = string.Empty;

        this.CurrentMaster.Title.Breadcrumbs = pageTitleTabs;
        this.CurrentMaster.Title.TitleText = GetString("InlineControl_Header.Title");
        this.CurrentMaster.Title.TitleImage = GetImageUrl("Objects/CMS_InlineControl/object.png");
        this.CurrentMaster.Title.HelpTopicName = "general_tab16";
        this.CurrentMaster.Title.HelpName = "helpTopic";
    }


    /// <summary>
    /// Initializes edit menu.
    /// </summary>
    protected void InitalizeMenu()
    {
        string[,] tabs = new string[3, 4];
        tabs[0, 0] = GetString("General.General");
        tabs[0, 1] = "SetHelpTopic('helpTopic', 'general_tab16');";
        tabs[0, 2] = "General.aspx?inlinecontrolID=" + inlinecontrolId;

        tabs[2, 0] = GetString("general.sites");
        tabs[2, 1] = "SetHelpTopic('helpTopic', 'sites_tab5');";
        tabs[2, 2] = "Sites.aspx?inlinecontrolID=" + inlinecontrolId;

        this.CurrentMaster.Tabs.Tabs = tabs;
        this.CurrentMaster.Tabs.UrlTarget = "content";
    }
}
