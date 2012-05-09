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
using CMS.SiteProvider;
using CMS.CMSHelper;
using CMS.Forums;
using CMS.LicenseProvider;
using CMS.UIControls;
using CMS.SettingsProvider;

public partial class CMSModules_Forums_Tools_Forums_Forum_Moderators : CMSForumsPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int forumID = QueryHelper.GetInteger("forumid", 0);
        ForumContext.CheckSite(0, forumID, 0);

        this.forumModerators.ForumID = forumID;
        this.forumModerators.IsLiveSite = false;
    }
}
