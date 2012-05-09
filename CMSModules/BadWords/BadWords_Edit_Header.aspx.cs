using System;

using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.SiteProvider;
using CMS.UIControls;

public partial class CMSModules_BadWords_BadWords_Edit_Header : SiteManagerPage
{
    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        int badWordId = QueryHelper.GetInteger("badWordId", 0);
        bool badWordIsSelected = (badWordId != 0);
        
        // Initialize PageTitle breadcrumbs
        string[,] pageTitleTabs = new string[2, 3];
        pageTitleTabs[0, 0] = GetString("badwords_edit.itemlistlink");
        pageTitleTabs[0, 1] = "~/CMSModules/BadWords/BadWords_List.aspx";
        pageTitleTabs[0, 2] = "_parent";
        string badWord = string.Empty;
        
        // Get bad word name
        if (!badWordIsSelected)
        {
            badWord = GetString("badwords_list.newitemcaption");
        }
        else
        {
            BadWordInfo badWordInfo = BadWordInfoProvider.GetBadWordInfo(badWordId);
            if (badWordInfo != null)
            {
                badWord = badWordInfo.WordExpression;
            }
        }
        pageTitleTabs[1, 0] = badWord;
        pageTitleTabs[1, 1] = string.Empty;
        pageTitleTabs[1, 2] = string.Empty;

        // Initialize masterpage properties
        CurrentMaster.Title.Breadcrumbs = pageTitleTabs;
        CurrentMaster.Title.TitleText = !badWordIsSelected ? GetString("BadWords_List.HeaderCaption") : GetString("BadWords_Edit.badwordproperties");
        CurrentMaster.Title.TitleImage = GetImageUrl("Objects/Badwords_Word/object.png");
        CurrentMaster.Title.HelpTopicName = "general_badwords";
        CurrentMaster.Title.HelpName = "helpTopic";

        if (!RequestHelper.IsPostBack())
        {
            InitalizeMenu();
        }
    }

    #endregion


    #region "Private methods"

    /// <summary>
    /// Initializes menu.
    /// </summary>
    protected void InitalizeMenu()
    {
        string[,] tabs = new string[2, 4];
        tabs[0, 0] = GetString("general.general");
        tabs[0, 1] = "SetHelpTopic('helpTopic', 'general_badwords');";
        tabs[0, 2] = "BadWords_Edit_General.aspx" + URLHelper.Url.Query;
        int badWordId = QueryHelper.GetInteger("badwordid", 0);
        
        if (!(badWordId == 0))
        {
            tabs[1, 0] = GetString("administration-site_edit.cultures");
            tabs[1, 1] = "SetHelpTopic('helpTopic', 'general_badwords');";
            tabs[1, 2] = "BadWords_Edit_Cultures.aspx?badwordid=" + badWordId;
        }
        CurrentMaster.Tabs.UrlTarget = "badwordsContent";
        CurrentMaster.Tabs.Tabs = tabs;
    }

    #endregion
}
