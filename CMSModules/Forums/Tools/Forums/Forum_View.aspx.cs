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
using CMS.CMSHelper;
using CMS.LicenseProvider;
using CMS.UIControls;
using CMS.SettingsProvider;
using CMS.Forums;

public partial class CMSModules_Forums_Tools_Forums_Forum_View : CMSForumsPage
{
    protected int forumId = 0;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        forumId = ValidationHelper.GetInteger(Request.QueryString["forumid"], 0);
        ForumContext.CheckSite(0, forumId, 0);

        ForumFlatView1.ForumID = forumId;
        ForumFlatView1.IsLiveSite = false;
        this.InitializeMasterPage();
    }


    /// <summary>
    /// Initializes master page.
    /// </summary>
    protected void InitializeMasterPage()
    {
        this.Title = "Forums - Forum view";
    }
}
