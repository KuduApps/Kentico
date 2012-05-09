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

public partial class CMSModules_Groups_Tools_MediaLibrary_Library_Edit_Security : CMSGroupMediaLibraryPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.librarySecurity.MediaLibraryID = QueryHelper.GetInteger("libarryid",0);
    }
}
