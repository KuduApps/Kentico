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
using CMS.SiteProvider;
using CMS.UIControls;

public partial class CMSModules_TagGroups_Pages_Development_TagGroup_Edit_Header : SiteManagerPage
{
    #region "Private fields"

    private int mGroupID = 0;
    private int mSiteId = 0;

    #endregion


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
        // Set the page title when existing group is being edited
        this.CurrentMaster.Title.TitleText = GetString("tags.general.editgroup");
        this.CurrentMaster.Title.TitleImage = GetImageUrl("Objects/CMS_TagGroup/object.png");

        this.mGroupID = QueryHelper.GetInteger("groupid", 0);
        this.mSiteId = QueryHelper.GetInteger("siteid", 0);

        InitalizeMenu();

        InitializeBreadcrumb();
    }


    /// <summary>
    /// Initializes the breadcrumb master page element.
    /// </summary>
    private void InitializeBreadcrumb()
    {
        string[,] breadcrumbs = new string[2, 3];
        breadcrumbs[0, 0] = GetString("Development.TagGroups");

        string backUrl = "~/CMSModules/TagGroups/Pages/Development/TagGroup_List.aspx";

        // If the ID of the selected site on tag group list page was supplied use it
        if (this.mSiteId != 0)
        {
            breadcrumbs[0, 1] = backUrl + "?siteid=" + this.mSiteId;
        }
        else
        {
            breadcrumbs[0, 1] = backUrl;
        }

        breadcrumbs[0, 2] = "_parent";

        TagGroupInfo tgi = TagGroupInfoProvider.GetTagGroupInfo(this.mGroupID);
        breadcrumbs[1, 0] = tgi.TagGroupDisplayName;

        this.CurrentMaster.Title.Breadcrumbs = breadcrumbs;
    }


    /// <summary>
    /// Initialize the tab control on the master page.
    /// </summary>
    private void InitalizeMenu()
    {
        // Collect tabs data
        string[,] tabs = new string[2, 4];
        tabs[0, 0] = GetString("general.general");

        string urlEditGeneral = "TagGroup_Edit_General.aspx";
        string urlEditTags = "TagGroup_Edit_Tags.aspx";

        // If the ClassID was specified pass it through
        if (this.mGroupID > 0)
        {
            urlEditGeneral += "?groupid=" + this.mGroupID.ToString();
            urlEditTags += "?groupid=" + this.mGroupID.ToString();
        }

        if (this.mSiteId > 0) 
        {
            urlEditGeneral += "&siteid=" + this.mSiteId;
            urlEditTags += "&siteid=" + this.mSiteId;
        }

        tabs[0, 2] = urlEditGeneral;
        tabs[0, 1] = "SetHelpTopic('helpTopic', 'new_taggroup_general');";
        tabs[1, 0] = GetString("general.tags");
        tabs[1, 1] = "SetHelpTopic('helpTopic', 'taggroup_tags');";
        tabs[1, 2] = urlEditTags;

        // Set the target iFrame
        this.CurrentMaster.Tabs.UrlTarget = "groupContent";

        // Assign tabs data
        this.CurrentMaster.Tabs.Tabs = tabs;

        this.CurrentMaster.Title.HelpTopicName = "new_taggroup_general";
        this.CurrentMaster.Title.HelpName = "helpTopic";
    }

    #endregion   
}
