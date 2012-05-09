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
using CMS.Community;

public partial class CMSModules_Groups_Tools_Forums_Groups_Group_Frameset : CMSGroupForumPage
{
    protected string groupsContentUrl = "../Forums/Forums_List.aspx?groupid=";
    protected string groupsHeaderUrl = "ForumGroup_Header.aspx?groupid=";

    protected void Page_Load(object sender, EventArgs e)
    {
        int groupId = ValidationHelper.GetInteger(Request.QueryString["groupid"], 0);
        if (ValidationHelper.GetInteger(Request.QueryString["saved"], 0) > 0)
        {
            groupsContentUrl +=  groupId.ToString() + "&saved=1";
            groupsHeaderUrl += groupId.ToString() + "&saved=1";
        }
        else
        {
            groupsContentUrl += groupId.ToString();
            groupsHeaderUrl += groupId.ToString();
        }
    }
}
