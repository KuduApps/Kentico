using System;

using CMS.GlobalHelper;
using CMS.UIControls;

public partial class CMSModules_Polls_Tools_Polls_New : CMSPollsPage
{
    #region "Private variables"

    private int siteId = 0;

    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        siteId = QueryHelper.GetInteger("siteid", 0);
        bool createGlobal = (siteId == UniSelector.US_GLOBAL_RECORD) || (siteId == UniSelector.US_GLOBAL_OR_SITE_RECORD);

        // Init breadcrumbs - new item breadcrumb
        string[,] breadcrumbs = new string[2, 3];
        breadcrumbs[0, 0] = GetString("Polls_New.ItemListLink");
        breadcrumbs[0, 1] = "~/CMSModules/Polls/Tools/Polls_List.aspx?siteid=" + siteId;
        breadcrumbs[0, 2] = "";
        breadcrumbs[1, 0] = GetString("Polls_New.NewItemCaption");
        breadcrumbs[1, 1] = "";
        breadcrumbs[1, 2] = "";

        // New item title
        this.CurrentMaster.Title.TitleText = GetString("Polls_New.HeaderCaption");
        this.CurrentMaster.Title.TitleImage = GetImageUrl("Objects/Polls_Poll/new.png");
        this.CurrentMaster.Title.HelpTopicName = "new_poll";
        this.CurrentMaster.Title.Breadcrumbs = breadcrumbs;

        PollNew.OnSaved += new EventHandler(PollNew_OnSaved);
        PollNew.IsLiveSite = false;
        PollNew.CreateGlobal = createGlobal;
    }


    /// <summary>
    /// OnSave event handler.
    /// </summary>
    void PollNew_OnSaved(object sender, EventArgs e)
    {
        string error = null;
        // Show possible license limitation error
        if (!String.IsNullOrEmpty(PollNew.LicenseError))
        {
            error = "&error=" + PollNew.LicenseError;
        }

        URLHelper.Redirect("Polls_Edit.aspx?pollId=" + PollNew.ItemID + "&saved=1" + error + "&siteid=" + siteId);
    }
}
