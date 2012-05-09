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

public partial class CMSModules_MediaLibrary_Tools_Library_Edit_Security : CMSMediaLibraryPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        MediaLibraryInfo mli = MediaLibraryInfoProvider.GetMediaLibraryInfo(QueryHelper.GetInteger("libraryid", 0));
        EditedObject = mli;

        // Check 'Read' permission
        if (!MediaLibraryInfoProvider.IsUserAuthorizedPerLibrary(mli, "Read"))
        {
            CMSPage.RedirectToCMSDeskAccessDenied("cms.medialibrary", "Read");
        }

        // Check 'Manage' permission
        if (!MediaLibraryInfoProvider.IsUserAuthorizedPerLibrary(mli, "Manage"))
        {
            this.librarySecurity.Enable = false;
        }

        this.librarySecurity.MediaLibraryID = QueryHelper.GetInteger("libraryid", 0);
        this.librarySecurity.OnCheckPermissions += new CMSAdminControl.CheckPermissionsEventHandler(librarySecurity_OnCheckPermissions);
    }

    void librarySecurity_OnCheckPermissions(string permissionType, CMSAdminControl sender)
    {
        MediaLibraryInfo mli = MediaLibraryInfoProvider.GetMediaLibraryInfo(QueryHelper.GetInteger("libraryid", 0));
        if (!MediaLibraryInfoProvider.IsUserAuthorizedPerLibrary(mli, "Read"))
        {
            CMSPage.RedirectToCMSDeskAccessDenied("cms.medialibrary", "Read");
        }
    }
}
