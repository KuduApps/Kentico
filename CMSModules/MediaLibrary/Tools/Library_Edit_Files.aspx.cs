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
using CMS.MediaLibrary;


public partial class CMSModules_MediaLibrary_Tools_Library_Edit_Files : CMSMediaLibraryPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        libraryElem.LibraryID = QueryHelper.GetInteger("libraryid", 0);
        libraryElem.OnCheckPermissions += new CMSAdminControl.CheckPermissionsEventHandler(libraryElem_OnCheckPermissions);
    }
    
    
    private void libraryElem_OnCheckPermissions(string permissionType, CMSAdminControl sender)
    {
        MediaLibraryInfo MediaLibrary = MediaLibraryInfoProvider.GetMediaLibraryInfo(QueryHelper.GetInteger("libraryid", 0));
        if (permissionType.ToLower() == "read")
        {
            // Check 'Read' permission
            if (!MediaLibraryInfoProvider.IsUserAuthorizedPerLibrary(MediaLibrary, permissionType))
            {
                RedirectToAccessDenied("cms.medialibrary", "Read");
            }
        }
    }
}
