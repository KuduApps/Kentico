using System;

using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.UIControls;

[Tabs("CMS.Newsletter", "", "newslettersContent")]
public partial class CMSModules_Newsletters_Tools_Header : CMSNewsletterPage
{
    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        this.CurrentMaster.Title.TitleText = GetString("Newsletters.HeaderCaption");
        this.CurrentMaster.Title.TitleImage = GetImageUrl("Objects/Newsletter_Newsletter/object.png");
        this.CurrentMaster.Title.HelpName = "helpTopic";
        this.CurrentMaster.Title.HelpTopicName = "CMS_Newsletter_Newsletters";

        // Load action page directly, if set by URL
        switch (QueryHelper.GetString("action", null))
        {
            case "newnewsletter":
                this.CurrentMaster.Tabs.StartPageURL = URLHelper.ResolveUrl("Newsletters/Newsletter_New.aspx");
                break;
        }

        // Register script for unimenu button selection
        CMSDeskPage.AddMenuButtonSelectScript(this, "Newsletters", null, "menu");
    }

    #endregion
}
