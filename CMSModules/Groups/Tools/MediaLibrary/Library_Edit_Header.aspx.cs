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
using CMS.MediaLibrary;
using CMS.Community;

public partial class CMSModules_Groups_Tools_MediaLibrary_Library_Edit_Header : CMSGroupMediaLibraryPage
{
    protected int libraryId;
    protected int groupId;

    protected void Page_Load(object sender, EventArgs e)
    {
        // Get the media library ID
        libraryId = QueryHelper.GetInteger("libraryId", 0);
        groupId = QueryHelper.GetInteger("groupid", 0);

        // Get library info
        MediaLibraryInfo mli = MediaLibraryInfoProvider.GetMediaLibraryInfo(libraryId);
        if (mli != null)
        {
            InitalizeMenu();

            // Initialize the page title
            string[,] breadcrumbs = new string[2, 3];
            breadcrumbs[0, 0] = GetString("media.header.itemlistlink");
            breadcrumbs[0, 1] = "~/CMSModules/Groups/Tools/MediaLibrary/Library_List.aspx?groupid=" + groupId;
            breadcrumbs[0, 2] = "_parent";
            breadcrumbs[1, 0] = mli.LibraryDisplayName;
            breadcrumbs[1, 1] = "";
            breadcrumbs[1, 2] = "";
            this.CurrentMaster.Title.Breadcrumbs = breadcrumbs;

            this.CurrentMaster.Title.HelpTopicName = "library_files";
            this.CurrentMaster.Title.HelpName = "helpTopic";
        }
    }


    /// <summary>
    /// Initializes edit menu.
    /// </summary>
    protected void InitalizeMenu()
    {
        string[,] tabs = new string[5, 4];
        tabs[0, 0] = GetString("general.files");
        tabs[0, 1] = "SetHelpTopic('helpTopic', 'library_files');";
        tabs[0, 2] = String.Format("Library_Edit_Files.aspx?libraryid={0}&groupid={1}", libraryId, groupId);
        tabs[1, 0] = GetString("general.general");
        tabs[1, 1] = "SetHelpTopic('helpTopic', 'library_general');";
        tabs[1, 2] = String.Format("Library_Edit_General.aspx?libraryid={0}&groupid={1}", libraryId, groupId);
        tabs[2, 0] = GetString("general.security");
        tabs[2, 1] = "SetHelpTopic('helpTopic', 'library_security');";
        tabs[2, 2] = String.Format("Library_Edit_Security.aspx?libarryid={0}&groupid={1}", libraryId, groupId);

        this.CurrentMaster.Tabs.Tabs = tabs;
        this.CurrentMaster.Tabs.UrlTarget = "mediaContent";
    }
}
