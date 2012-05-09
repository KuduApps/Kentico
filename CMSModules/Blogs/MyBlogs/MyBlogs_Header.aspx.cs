using System;

using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.Blogs;

public partial class CMSModules_Blogs_MyBlogs_MyBlogs_Header : CMSMyBlogsPage
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
        CurrentMaster.Title.TitleText = GetString("myblogs.header.myblogs");
        CurrentMaster.Title.TitleImage = GetImageUrl("CMSModules/CMS_Blog/module.png");
        CurrentMaster.Title.HelpTopicName = "my_blogs_list";

        InitalizeMenu();
    }


    /// <summary>
    /// Initialize the tab control on the master page.
    /// </summary>
    private void InitalizeMenu()
    {
        // Collect tabs data
        string[,] tabs = new string[2, 4];
        tabs[0, 0] = GetString("myblogs.header.comments");
        tabs[0, 2] = "MyBlogs_Comments_List.aspx";

        tabs[1, 0] = GetString("myblogs.header.blogs");
        tabs[1, 2] = "MyBlogs_Blogs_List.aspx";

        // Set the target iFrame
        CurrentMaster.Tabs.UrlTarget = "blogsContent";

        // Assign tabs data
        CurrentMaster.Tabs.Tabs = tabs;
    }
}
