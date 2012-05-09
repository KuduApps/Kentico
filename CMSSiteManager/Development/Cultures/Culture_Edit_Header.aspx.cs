using System;

using CMS.SiteProvider;
using CMS.GlobalHelper;
using CMS.UIControls;

public partial class CMSSiteManager_Development_Cultures_Culture_Edit_Header : SiteManagerPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string currentCulture = string.Empty;
        int cultureId = QueryHelper.GetInteger("cultureid", 0);

        CultureInfo ci = CultureInfoProvider.GetSafeCulture(cultureId);
        EditedObject = ci;
        currentCulture = ci.CultureName;

        CurrentMaster.Title.TitleText = GetString("culture.edit.properties");
        CurrentMaster.Title.TitleImage = GetImageUrl("Objects/CMS_Culture/object.png");
        CurrentMaster.Title.HelpTopicName = "general_tabnew";
        CurrentMaster.Title.HelpName = "helpTopic";

        string[,] breadcrumbs = new string[2, 3];
        breadcrumbs[0, 0] = GetString("general.cultures");
        breadcrumbs[0, 1] = "~/CMSSiteManager/Development/Cultures/Culture_List.aspx";
        breadcrumbs[0, 2] = "_parent";
        breadcrumbs[1, 0] = currentCulture;
        CurrentMaster.Title.Breadcrumbs = breadcrumbs;

        string[,] tabs = new string[2, 4];
        tabs[0, 0] = GetString("general.general");
        tabs[0, 1] = "SetHelpTopic('helpTopic', 'general_tabnew');";
        tabs[0, 2] = "Culture_Edit_General.aspx?cultureID=" + cultureId;
        tabs[1, 0] = GetString("general.sites");
        tabs[1, 1] = "SetHelpTopic('helpTopic', 'sites_tab4');";
        tabs[1, 2] = "Culture_Edit_Sites.aspx?cultureID=" + cultureId;
        CurrentMaster.Tabs.Tabs = tabs;
        CurrentMaster.Tabs.UrlTarget = "content";
    }
}