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

[Tabs("CMS.Blog", "", "blogsContent")]
public partial class CMSModules_Blogs_Tools_Blogs_Header : CMSBlogsPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Intialize the control
        SetupControl();
    }


    /// <summary>
    /// Initializes the controls.
    /// </summary>
    private void SetupControl()
    {
        // Set the page title when existing category is being edited
        this.CurrentMaster.Title.TitleText = GetString("blogs.header.title");
        this.CurrentMaster.Title.TitleImage = GetImageUrl("Objects/CMS_Blog/object.png");
        this.CurrentMaster.Title.HelpName = "helpTopic";
        this.CurrentMaster.Title.HelpTopicName = "CMS_Blog_Comments";
    }
}
