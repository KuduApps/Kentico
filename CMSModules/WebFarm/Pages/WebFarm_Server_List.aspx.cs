using System;
using System.Web.UI.WebControls;

using CMS.GlobalHelper;
using CMS.SettingsProvider;
using CMS.WebFarmSync;
using CMS.CMSHelper;
using CMS.UIControls;

public partial class CMSModules_WebFarm_Pages_WebFarm_Server_List : SiteManagerPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // New item link
        string[,] actions = new string[1, 6];
        actions[0, 0] = HeaderActions.TYPE_HYPERLINK;
        actions[0, 1] = GetString("WebFarmServers_List.NewItemCaption");
        actions[0, 2] = null;
        actions[0, 3] = ResolveUrl("WebFarm_Server_Edit.aspx");
        actions[0, 4] = null;
        actions[0, 5] = GetImageUrl("Objects/CMS_WebFarmServer/add.png");

        this.CurrentMaster.HeaderActions.Actions = actions;

        UniGrid.OnAction += new OnActionEventHandler(uniGrid_OnAction);
        UniGrid.OnExternalDataBound += new OnExternalDataBoundEventHandler(UniGrid_OnExternalDataBound);
        UniGrid.ZeroRowsText = GetString("general.nodatafound");

        if (WebSyncHelperClass.WebFarmInstanceEnabled && !String.IsNullOrEmpty(WebSyncHelperClass.ServerName))
        {
            if (AzureHelper.IsRunningOnAzure)
            {
                lblInfo.Text = String.Format(GetString("WebFarm.EnabledAzure"), WebSyncHelperClass.ServerName);
            }
            else
            {
                lblInfo.Text = String.Format(GetString("WebFarm.Enabled"), WebSyncHelperClass.ServerName);
            }
        }
        else
        {
            lblInfo.Text = GetString("WebFarm.Disabled");
        }
    }


    /// <summary>
    /// Handles the UniGrid's OnExternalDataBound.
    /// </summary>
    /// <param name="sender">Sender</param>
    /// <param name="sourceName">Source name</param>
    /// <param name="parameter">Value</param>
    static object UniGrid_OnExternalDataBound(object sender, string sourceName, object parameter)
    {
        switch (sourceName.ToLower())
        {
            case "serverenabled":
                return UniGridFunctions.ColoredSpanYesNo(parameter);
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
        if (actionName == "edit")
        {
            URLHelper.Redirect("WebFarm_Server_Edit.aspx?serverid=" + Convert.ToString(actionArgument));
        }
        else if (actionName == "delete")
        {
            // delete WebFarmServerInfo object from database
            WebFarmServerInfoProvider.DeleteWebFarmServerInfo(Convert.ToInt32(actionArgument));
        }
    }
}
