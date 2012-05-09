using System;
using System.Web.UI.WebControls;

using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.Synchronization;
using CMS.UIControls;

public partial class CMSModules_Staging_Tools_Servers_List : CMSStagingServersPage
{
    protected int siteId = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        // check 'Manage servers' permission
        if (!CMSContext.CurrentUser.IsAuthorizedPerResource("cms.staging", "ManageServers"))
        {
            RedirectToAccessDenied("cms.staging", "ManageServers");
        }

        siteId = QueryHelper.GetInteger("siteid", 0);
        if (siteId == 0)
        {
            siteId = CMSContext.CurrentSite.SiteID;
        }

        string[,] actions = new string[1, 6];
        actions[0, 0] = HeaderActions.TYPE_HYPERLINK;
        actions[0, 1] = GetString("Server_List.NewItemCaption");
        actions[0, 2] = null;
        actions[0, 3] = ResolveUrl("Server_Edit.aspx");
        actions[0, 4] = null;
        actions[0, 5] = GetImageUrl("Objects/Staging_Server/add.png");

        CurrentMaster.HeaderActions.Actions = actions;

        UniGrid.OnAction += uniGrid_OnAction;
        UniGrid.OnExternalDataBound += UniGrid_OnExternalDataBound;
        UniGrid.WhereCondition = "ServerSiteID=" + siteId;
        UniGrid.ZeroRowsText = GetString("Server_List.nodatafound");

        if(!String.IsNullOrEmpty(TaskInfoProvider.ServerName))
        {
            lblInfo.Text = String.Format(GetString("staging.currentserver"), TaskInfoProvider.ServerName);
        }
    }


    protected object UniGrid_OnExternalDataBound(object sender, string sourceName, object parameter)
    {
        if (sourceName.ToLower() == "serverenabled")
        {
            if (ValidationHelper.GetBoolean(parameter, false))
            {
                return "<span class=\"ServerStatusEnabled\">Yes</span>";
            }
            else
            {
                return "<span class=\"ServerStatusDisabled\">No</span>";
            }
        }
        return parameter;
    }


    /// <summary>
    /// Handles the UniGrid's OnAction event.
    /// </summary>
    /// <param name="actionName">Name of item (button) that throws event</param>
    /// <param name="actionArgument">ID (value of Primary key) of corresponding data row</param>
    protected void uniGrid_OnAction(string actionName, object actionArgument)
    {
        switch (actionName.ToLower())
        {
            case "edit":
                string detailUrl = "Server_Edit.aspx?serverid=" + Convert.ToString(actionArgument);
                detailUrl = URLHelper.AddParameterToUrl(detailUrl, "hash", QueryHelper.GetHash(detailUrl));

                URLHelper.Redirect(detailUrl);
                break;

            case "delete":
                ServerInfoProvider.DeleteServerInfo(Convert.ToInt32(actionArgument));
                break;
        }
    }
}