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

using CMS.Forums;
using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.UIControls;
using CMS.Community;

public partial class CMSModules_Groups_Tools_Forums_Forums_Forum_Frameset : CMSGroupForumPage
{
    #region "Variables"

    protected string forumsContentUrl = "../Posts/ForumPost_Frameset.aspx?forumid=";
    protected string forumsHeaderUrl = "Forum_Header.aspx?forumid=";

    #endregion


    #region "Page events"

    /// <summary>
    /// Handles the Load event of the Page control.
    /// </summary>
    protected void Page_Load(object sender, EventArgs e)
    {
        int forumId = ValidationHelper.GetInteger(Request.QueryString["forumid"], 0);
        if (QueryHelper.GetInteger("saved", 0) > 0)
        {
            forumsContentUrl += forumId.ToString() + "&saved=1";
            forumsHeaderUrl += forumId.ToString() + "&saved=1";
        }
        else
        {
            forumsContentUrl += forumId.ToString();
            forumsHeaderUrl += forumId.ToString();
        }

        string changeMasterStr = "&changemaster=" + QueryHelper.GetInteger("changemaster", 0).ToString();

        forumsHeaderUrl += changeMasterStr;
        forumsContentUrl += changeMasterStr;
    }

    #endregion
}
