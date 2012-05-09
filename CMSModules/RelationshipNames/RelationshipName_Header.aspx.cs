using System;

using CMS.SiteProvider;
using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.UIControls;

public partial class CMSModules_RelationshipNames_RelationshipName_Header : SiteManagerPage
{
    #region "Protected variables"

    protected int relationshipNameId = 0;

    #endregion


    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Get ID of relationship name
        relationshipNameId = QueryHelper.GetInteger("relationshipnameid", 0);

        // Initialize menu
        if (!RequestHelper.IsPostBack())
        {
            InitalizeMenu();
        }

        // Get info object
        RelationshipNameInfo relationshipNameInfo = RelationshipNameInfoProvider.GetRelationshipNameInfo(relationshipNameId);

        // Initializes page title
        string[,] pageTitleTabs = new string[2, 3];
        pageTitleTabs[0, 0] = GetString("RelationshipNames.RelationshipNames");
        pageTitleTabs[0, 1] = "~/CMSModules/RelationshipNames/RelationshipName_List.aspx";
        pageTitleTabs[0, 2] = "_parent";
        pageTitleTabs[1, 0] = (relationshipNameInfo != null) ? relationshipNameInfo.RelationshipDisplayName : string.Empty;
        pageTitleTabs[1, 1] = string.Empty;
        pageTitleTabs[1, 2] = string.Empty;

        CurrentMaster.Title.Breadcrumbs = pageTitleTabs;
        CurrentMaster.Title.TitleText = GetString("RelationshipNames.RelationshipNameProperties");
        CurrentMaster.Title.TitleImage = GetImageUrl("Objects/CMS_RelationshipName/object.png");
        CurrentMaster.Title.HelpTopicName = "new_namegeneral_tab";
        CurrentMaster.Title.HelpName = "helpTopic";
    }

    #endregion


    #region "Protected methods"

    /// <summary>
    /// Initializes relationship name edit menu.
    /// </summary>
    protected void InitalizeMenu()
    {
        string[,] tabs = new string[2, 4];
        tabs[0, 0] = GetString("General.General");
        tabs[0, 1] = "SetHelpTopic('helpTopic', 'new_namegeneral_tab');";
        tabs[0, 2] = "RelationshipName_General.aspx?relationshipnameid=" + relationshipNameId;
        tabs[1, 0] = GetString("general.sites");
        tabs[1, 1] = "SetHelpTopic('helpTopic', 'sites_tab6');";
        tabs[1, 2] = "RelationshipName_Sites.aspx?relationshipnameid=" + relationshipNameId;

        CurrentMaster.Tabs.Tabs = tabs;
        CurrentMaster.Tabs.UrlTarget = "relationshipNameContent";
    }

    #endregion
}
