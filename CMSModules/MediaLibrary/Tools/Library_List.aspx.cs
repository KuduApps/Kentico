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
using CMS.CMSHelper;

public partial class CMSModules_MediaLibrary_Tools_Library_List : CMSMediaLibraryPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string[,] actions = new string[1, 6];

        // New item link
        actions[0, 0] = HeaderActions.TYPE_HYPERLINK;
        actions[0, 1] = GetString("media.list.newlibrary");
        actions[0, 2] = null;
        actions[0, 3] = ResolveUrl("Library_New.aspx");
        actions[0, 4] = null;
        actions[0, 5] = ResolveUrl(GetImageUrl("Objects/Media_Library/add.png"));
        this.CurrentMaster.HeaderActions.Actions = actions;
        this.CurrentMaster.Title.TitleText = GetString("media.list.medialibrary");
        this.CurrentMaster.Title.TitleImage = ResolveUrl(GetImageUrl("Objects/Media_Library/object.png"));

        this.CurrentMaster.Title.HelpTopicName = "library_list";
        this.CurrentMaster.Title.HelpName = "helpTopic";

        elemList.OnEdit += new EventHandler(elemList_OnEdit);
    }


    void elemList_OnEdit(object sender, EventArgs e)
    {
        URLHelper.Redirect(ResolveUrl("Library_Edit.aspx?libraryid=" + elemList.SelectedItemID));
    }
}
