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
using CMS.Notifications;
using CMS.UIControls;

public partial class CMSModules_Notifications_Development_Templates_Template_Edit_Header : CMSNotificationsPage
{
    #region "Variables"

    protected int templateId = 0;
    protected int siteId = 0;

    #endregion


    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        // get query string
        templateId = QueryHelper.GetInteger("templateid", 0);
        siteId = QueryHelper.GetInteger("siteid", 0);

        // Intialize the control
        SetupControl();
    }

    #endregion


    #region "Private methods"

    /// <summary>
    /// Initializes the controls.
    /// </summary>
    private void SetupControl()
    {
        InitalizeMenu();

        InitializeBreadcrumb();
    }


    /// <summary>
    /// Initializes the breadcrumb header element of the master page.
    /// </summary>
    private void InitializeBreadcrumb()
    {
        string[,] breadcrumbs = new string[2, 3];
        breadcrumbs[0, 0] = GetString("notifications.header.templates");
        breadcrumbs[0, 1] = "~/CMSModules/Notifications/Development/Templates/Template_List.aspx" + ((siteId > 0) ? "?siteid=" + siteId : "");
        breadcrumbs[1, 0] = GetString("notifications.template_edit.new");
        breadcrumbs[0, 2] = "_parent";
        if (templateId > 0)
        {            
            breadcrumbs[1, 0] = NotificationTemplateInfoProvider.GetNotificationTemplateInfo(templateId).TemplateDisplayName;
            breadcrumbs[0, 1] = "~/CMSModules/Notifications/Development/Templates/Template_List.aspx?templateid=" + templateId + ((siteId > 0) ? "&siteid=" + siteId : "");
        }       

        breadcrumbs[1, 1] = "";

        this.CurrentMaster.Title.Breadcrumbs = breadcrumbs;
    }


    /// <summary>
    /// Initialize the tab control on the master page.
    /// </summary>
    private void InitalizeMenu()
    {
        int templateId = QueryHelper.GetInteger("templateid", 0) ;
        int siteId = QueryHelper.GetInteger("siteid", 0) ;

        // Collect tabs data
        string[,] tabs = new string[2, 4];
        tabs[0, 0] = GetString("general.general");
        tabs[0, 1] = "SetHelpTopic('helpTopic', 'notification_template_edit_general');";
        tabs[0, 2] = "Template_Edit_General.aspx?templateid=" + templateId + "&siteid=" + siteId;

        tabs[1, 0] = GetString("general.text");
        tabs[1, 1] = "SetHelpTopic('helpTopic', 'notification_template_edit_text');";
        tabs[1, 2] = "Template_Edit_Text.aspx?templateid=" + templateId + "&siteid=" + siteId  ;
        
        // Set the target iFrame
        this.CurrentMaster.Tabs.UrlTarget = "templatesContent";

        this.CurrentMaster.Title.HelpTopicName = "notification_template_edit_general";
        this.CurrentMaster.Title.HelpName = "helpTopic";

        // Assign tabs data
        this.CurrentMaster.Tabs.Tabs = tabs;
    }

    #endregion   
}
