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

using CMS.UIControls;
using CMS.Community;
using CMS.GlobalHelper;

public partial class CMSModules_Groups_Tools_MediaLibrary_Library_Edit : CMSGroupMediaLibraryPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int groupID = QueryHelper.GetInteger("groupid", 0);
        CheckGroupPermissions(groupID, CMSAdminControl.PERMISSION_READ);
    }
}
