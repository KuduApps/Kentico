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
using System.Text.RegularExpressions;

using CMS.UIControls;
using CMS.GlobalHelper;
using CMS.MediaLibrary;
using CMS.CMSHelper;


public partial class CMSModules_MediaLibrary_Tools_FolderActions_SelectFolder_Content : CMSMediaLibraryModalPage
{
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        SetBrowserClass();
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        // Get query values
        this.selectFolder.MediaLibraryID = QueryHelper.GetInteger("libraryid", 0);
        this.selectFolder.Action = QueryHelper.GetString("action", "");
        this.selectFolder.FolderPath = QueryHelper.GetString("folderpath", "").Replace("/", "\\");
        this.selectFolder.Files = QueryHelper.GetString("files", "").Trim('|');
        this.selectFolder.AllFiles = QueryHelper.GetBoolean("allFiles", false);
    }
}
