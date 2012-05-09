using System;
using System.Web.UI.WebControls;

using CMS.UIControls;
using CMS.GlobalHelper;
using CMS.EmailEngine;
using System.Data;

[Title("Objects/CMS_SMTPServer/object.png", "SMTPServer_List.HeaderCaption", "smtpservers_list")]
[Actions(1)]
[Action(0, "Objects/CMS_SMTPServer/add.png", "SMTPServer_List.NewItemCaption", "New.aspx")]
public partial class CMSModules_SMTPServers_Pages_Administration_List : CMSSMTPServersPage
{
    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        ShowInformation(GetString("smtpserver.editdefaultservers"));

        UniGrid.OnAction += uniGrid_OnAction;
        UniGrid.OnExternalDataBound += UniGrid_OnExternalDataBound;
    }


    protected void uniGrid_OnAction(string actionName, object actionArgument)
    {
        switch (actionName)
        {
            case "delete":
                // Delete SMTP server
                SMTPServerInfoProvider.DeleteSMTPServerInfo(ValidationHelper.GetInteger(actionArgument, 0));
                break;

            case "enable":
                // Enable SMTP server
                SMTPServerInfoProvider.EnableSMTPServer(ValidationHelper.GetInteger(actionArgument, 0));
                break;

            case "disable":
                // Disable SMTP server
                SMTPServerInfoProvider.DisableSMTPServer(ValidationHelper.GetInteger(actionArgument, 0));
                break;
        }
    }


    protected object UniGrid_OnExternalDataBound(object sender, string sourceName, object parameter)
    {
        switch (sourceName)
        {
            // Display/hide buttons for enabling/disabling SMTP servers
            case "enable":
                (sender as ImageButton).Visible = !ServerEnabled(parameter);
                return parameter;

            case "disable":
                (sender as ImageButton).Visible = ServerEnabled(parameter);
                return parameter;

            default:
                return parameter;
        }
    }


    /// <summary>
    /// Check if SMTP server is enabled.
    /// </summary>
    /// <param name="parameter">GridView row with SMTP server data</param>
    private static bool ServerEnabled(object parameter)
    {
        DataRowView rowView = ((parameter as GridViewRow).DataItem) as DataRowView;
        return ValidationHelper.GetBoolean(rowView["ServerEnabled"], false);
    }

    #endregion
}