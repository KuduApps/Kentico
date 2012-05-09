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
using CMS.Community;

public partial class CMSModules_Groups_Tools_MediaLibrary_Library_New : CMSGroupMediaLibraryPage
{
    private int mGroupId = 0;


    protected void Page_Load(object sender, EventArgs e)
    {
        this.mGroupId = QueryHelper.GetInteger("groupid", 0);
        CheckGroupPermissions(mGroupId, CMSAdminControl.PERMISSION_READ);

        // Init breadcrumbs
        string[,] breadcrumbs = new string[2, 3];
        breadcrumbs[0, 0] = GetString("media.new.librarylistlink");
        breadcrumbs[0, 1] = "~/CMSModules/Groups/Tools/MediaLibrary/Library_List.aspx?groupid=" + this.mGroupId;
        breadcrumbs[0, 2] = "";
        breadcrumbs[1, 0] = GetString("media.new.newlibrary");
        breadcrumbs[1, 1] = "";
        breadcrumbs[1, 2] = "";

        this.CurrentMaster.Title.Breadcrumbs = breadcrumbs;

        if (this.mGroupId > 0)
        {
            elemEdit.MediaLibraryID = QueryHelper.GetInteger("libraryid", 0);
            elemEdit.MediaLibraryGroupID = mGroupId;
            elemEdit.OnSaved += new EventHandler(elemEdit_OnSaved);
        }
        else
        {
            elemEdit.Enable = false;
        }

        this.CurrentMaster.Title.HelpTopicName = "library_new";
        this.CurrentMaster.Title.HelpName = "helpTopic";
        
    }

    void elemEdit_OnSaved(object sender, EventArgs e)
    {
        URLHelper.Redirect(ResolveUrl("Library_Edit.aspx?libraryid=" + elemEdit.MediaLibraryID + "&groupid=" + mGroupId));
    }
}
