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
using CMS.UIControls;
using CMS.ExtendedControls;
using CMS.Forums;

public partial class CMSModules_Forums_Tools_Posts_ForumPost_Frameset : CMSForumsPage
{
    protected string postsTreeUrl = "ForumPost_Tree.aspx?forumid=";
    protected string postsEditUrl = "ForumPost_View.aspx?forumid=";
    protected string postFrameTree = "posts_tree";
    protected string postFrameEdit = "posts_edit";

    protected void Page_Load(object sender, EventArgs e)
    {
        int forumId = ValidationHelper.GetInteger(Request.QueryString["forumid"], 0);
        ForumContext.CheckSite(0, forumId, 0);

        if (ValidationHelper.GetInteger(Request.QueryString["saved"], 0) > 0)
        {
            postsTreeUrl += forumId.ToString() + "&saved=1";
            postsEditUrl += forumId.ToString() + "&saved=1";
        }
        else
        {
            postsTreeUrl += forumId.ToString();
            postsEditUrl += forumId.ToString();
        }

        frameTree.Attributes["name"] = postFrameTree;
        frameTree.Attributes["src"] = postsTreeUrl;

        frameEdit.Attributes["name"] = postFrameEdit;
        frameEdit.Attributes["src"] = postsEditUrl;

        if (CultureHelper.IsUICultureRTL())
        {
            ControlsHelper.ReverseFrames(this.colsFrameset);                                  
        }
    }
}
