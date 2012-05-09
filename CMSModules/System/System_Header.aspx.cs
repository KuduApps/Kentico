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

public partial class CMSModules_System_System_Header : SiteManagerPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Pagetitle
        this.CurrentMaster.Title.TitleText = GetString("Administration-System.Header");
        this.CurrentMaster.Title.TitleImage = GetImageUrl("CMSModules/CMS_System/module.png");
        this.CurrentMaster.Title.HelpTopicName = "general_tab10";
        this.CurrentMaster.Title.HelpName = "helpTopic";

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
        string[,] tabs = new string[5, 4];

        tabs[0, 0] = GetString("general.general");
        tabs[0, 1] = "SetHelpTopic('helpTopic', 'general_tab10');";
        tabs[0, 2] = "System.aspx";
        
        tabs[1, 0] = GetString("general.email");
        tabs[1, 1] = "SetHelpTopic('helpTopic', 'email_tab');";
        tabs[1, 2] = "System_Email.aspx";
        
        tabs[2, 0] = GetString("Administration-System.Files");
        tabs[2, 1] = "SetHelpTopic('helpTopic', 'files_tab');";
        tabs[2, 2] = "Files/System_FilesFrameset.aspx";
        
        tabs[3, 0] = GetString("Administration-System.Deployment");
        tabs[3, 1] = "SetHelpTopic('helpTopic', 'deployment_tab');";
        tabs[3, 2] = "System_Deployment.aspx";

        // Debug tab
        tabs[4, 0] = GetString("Administration-System.Debug");
        tabs[4, 1] = "SetHelpTopic('helpTopic', 'debug_tab');";
        tabs[4, 2] = "Debug/System_DebugFrameset.aspx";

        this.CurrentMaster.Tabs.UrlTarget = "systemContent";
        this.CurrentMaster.Tabs.Tabs = tabs;
    }
}
