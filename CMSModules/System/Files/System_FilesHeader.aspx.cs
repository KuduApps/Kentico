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
using CMS.DataEngine;
using CMS.UIControls;
using CMS.Controls;
using CMS.SiteProvider;
using CMS.OutputFilter;
using CMS.SettingsProvider;
using CMS.IO;

public partial class CMSModules_System_Files_System_FilesHeader : SiteManagerPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Pagetitle
        this.CurrentMaster.Title.HelpTopicName = "filetest_tab";
        this.CurrentMaster.Title.HelpName = "helpTopic";

        // Initializes PageTitle
        string[,] pageTitleTabs = new string[2, 3];

        pageTitleTabs[0, 0] = GetString("Administration-System.Header");
        pageTitleTabs[0, 1] = null;
        pageTitleTabs[0, 2] = null;

        pageTitleTabs[1, 0] = GetString("Administration-System.Files");
        pageTitleTabs[1, 1] = null;
        pageTitleTabs[1, 2] = null;

        this.CurrentMaster.Title.Breadcrumbs = pageTitleTabs;

        if (!RequestHelper.IsPostBack())
        {
            InitalizeMenu();
        }
    }


    /// <summary>
    /// Initializes menu.
    /// </summary>
    protected void InitalizeMenu()
    {
        string[,] tabs = new string[14, 5];
        int index = 0;

        tabs[index, 0] = GetString("Administration-System.FilesTest");
        tabs[index, 1] = "SetHelpTopic('helpTopic', 'filetest_tab');";
        tabs[index, 2] = "System_FilesTest.aspx";
        index++;

        tabs[index, 0] = GetString("Administration-System.FilesAttachments");
        tabs[index, 1] = "SetHelpTopic('helpTopic', 'filesattachments_tab');";
        tabs[index, 2] = "System_FilesAttachments.aspx";
        index++;

        tabs[index, 0] = GetString("Administration-System.FilesMetafiles");
        tabs[index, 1] = "SetHelpTopic('helpTopic', 'filesmetafiles_tab');";
        tabs[index, 2] = "System_FilesMetafiles.aspx";
        index++;

        this.CurrentMaster.Tabs.UrlTarget = "systemFiles";
        this.CurrentMaster.Tabs.Tabs = tabs;
    }
}
