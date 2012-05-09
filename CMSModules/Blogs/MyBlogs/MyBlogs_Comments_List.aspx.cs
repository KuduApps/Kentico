using System;
using System.Data;
using System.Collections;
using System.Web.UI.WebControls;

using CMS.SettingsProvider;
using CMS.UIControls;
using CMS.WorkflowEngine;

public partial class CMSModules_Blogs_MyBlogs_MyBlogs_Comments_List : CMSMyBlogsPage
{

    protected void Page_Load(object sender, EventArgs e)
    {
        // No cms.blog doc. type
        if (DataClassInfoProvider.GetDataClass("cms.blog") == null)
        {
            RedirectToInformation(GetString("blog.noblogdoctype"));
        }
    }
}
