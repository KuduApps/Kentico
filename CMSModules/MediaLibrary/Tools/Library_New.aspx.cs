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

public partial class CMSModules_MediaLibrary_Tools_Library_New : CMSMediaLibraryPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Init breadcrumbs
        string[,] breadcrumbs = new string[2, 3];
        breadcrumbs[0, 0] = GetString("media.new.librarylistlink");
        breadcrumbs[0, 1] = "~/CMSModules/MediaLibrary/Tools/Library_List.aspx";
        breadcrumbs[0, 2] = "";
        breadcrumbs[1, 0] = GetString("media.new.newlibrary");
        breadcrumbs[1, 1] = "";
        breadcrumbs[1, 2] = "";
        this.CurrentMaster.Title.Breadcrumbs = breadcrumbs;
        this.CurrentMaster.Title.TitleText = GetString("media.new.newlibrarytitle");
        this.CurrentMaster.Title.TitleImage = ResolveUrl(GetImageUrl("Objects/Media_Library/new.png"));

        this.CurrentMaster.Title.HelpTopicName = "library_new";
        this.CurrentMaster.Title.HelpName = "helpTopic";

        elemEdit.MediaLibraryID = QueryHelper.GetInteger("libraryid", 0);
        elemEdit.OnSaved += new EventHandler(elemEdit_OnSaved);
    }

    void elemEdit_OnSaved(object sender, EventArgs e)
    {
        URLHelper.Redirect(ResolveUrl("Library_Edit.aspx?libraryid=" + elemEdit.MediaLibraryID));
    }
}
