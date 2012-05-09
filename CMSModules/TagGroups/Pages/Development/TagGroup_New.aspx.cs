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

public partial class CMSModules_TagGroups_Pages_Development_TagGroup_New : SiteManagerPage
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
        // Set the page title when new category is being created
        this.CurrentMaster.Title.TitleText = GetString("tags.taggroup_list.newgroup");
        this.CurrentMaster.Title.TitleImage = GetImageUrl("Objects/CMS_TagGroup/new.png");

        this.CurrentMaster.Title.HelpTopicName = "new_taggroup_general";
        this.CurrentMaster.Title.HelpName = "helpTopic";

        // Setup the control
        this.groupEdit.SiteID = QueryHelper.GetInteger("siteid", 0);

        // Initialize breadcrumb
        InitializeBreadcrumb("tags.taggroup_list.title");
    }


    /// <summary>
    /// Sets the page breadcrumb control.
    /// </summary>
    /// <param name="caption">Caption of the breadcrumb item displayed in the page title as resource string key</param>
    /// <param name="backUrl">URL of previous item</param>
    /// <param name="currentUrl">URL of current item</param>
    private void InitializeBreadcrumb(string caption)
    {
        // Initialize the breadcrumb settings
        string[,] breadCrumbInfo = new string[2, 3];

        // Add the base item
        breadCrumbInfo[0, 0] = GetString(caption);
        breadCrumbInfo[0, 1] = "~/CMSModules/TagGroups/Pages/Development/TagGroup_List.aspx?siteid=" + this.groupEdit.SiteID;
        breadCrumbInfo[0, 2] = "";

        // Add the custom item
        breadCrumbInfo[1, 0] = GetString("tags.taggroup_new.new");
        breadCrumbInfo[1, 1] = "";
        breadCrumbInfo[1, 2] = "";

        // Send the breadcrumb settings to the master page
        if (this.CurrentMaster != null)
        {
            this.CurrentMaster.Title.Breadcrumbs = breadCrumbInfo;
        }
    }

    #endregion
}
