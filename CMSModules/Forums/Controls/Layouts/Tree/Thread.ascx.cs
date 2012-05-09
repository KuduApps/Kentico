using System;

using CMS.Forums;
using CMS.GlobalHelper;

public partial class CMSModules_Forums_Controls_Layouts_Tree_Thread : ForumViewer
{
    protected void Page_Load(object sender, EventArgs e)
    {
        URLHelper.Redirect(URLHelper.RemoveParameterFromUrl(URLHelper.CurrentURL, "threadid"));
    }
}
