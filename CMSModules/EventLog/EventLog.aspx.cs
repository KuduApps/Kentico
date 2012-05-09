using System;

using CMS.GlobalHelper;
using CMS.SiteProvider;
using CMS.EventLog;
using CMS.CMSHelper;
using CMS.UIControls;

public partial class CMSModules_EventLog_EventLog : CMSEventLogPage
{
    #region "Protectetd variables"

    protected EventLogProvider eventProvider = new EventLogProvider();
    int siteID = -1;

    #endregion


    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        eventLog.OnCheckPermissions += eventLog_OnCheckPermissions;

        // Register the dialog script
        ScriptHelper.RegisterDialogScript(this);

        // Setup page title text and image
        CurrentMaster.Title.TitleText = GetString("EventLogList.Header");
        CurrentMaster.Title.TitleImage = GetImageUrl("Objects/CMS_EventLog/object.png");

        CurrentMaster.Title.HelpTopicName = "list_of_events";
        CurrentMaster.Title.HelpName = "helpTopic";

        lblSite.Text = GetString("EventLogList.Label");

        btnClearLog.Text = GetString("EventLogList.Clear");
        btnClearLog.Attributes.Add("onclick", "return confirm(" + ScriptHelper.GetString(GetString("EventLogList.ClearAllConfirmation")) + ");");

        if (SiteID > 0)
        {
            pnlSites.Visible = false;
            siteID = SiteID;
        }
        else
        {
            // Set site selector
            siteSelector.DropDownSingleSelect.AutoPostBack = true;
            siteSelector.OnlyRunningSites = false;
            siteSelector.AllowAll = true;
            siteSelector.UniSelector.SpecialFields = new string[,] { { GetString("EventLogList.GlobalEvents"), "0" } };
            siteSelector.UniSelector.OnSelectionChanged += UniSelector_OnSelectionChanged;
            if (RequestHelper.IsPostBack())
            {
                siteID = ValidationHelper.GetInteger(siteSelector.Value, 0);
            }
        }
        eventLog.SiteID = siteID;
    }


    protected void Page_PreRender(object sender, EventArgs e)
    {
        // If user is not authorized, hide the whole panel
        pnlTop.Visible = CMSContext.CurrentUser.IsAuthorizedPerResource("CMS.EventLog", "ClearLog");

        if (pnlTop.Visible)
        {
            // If events grid not empty allow clear log button
            btnClearLog.Enabled = (eventLog.EventLogGrid.RowsCount > 0);
            if (btnClearLog.Enabled)
            {
                btnClearLog.Attributes.Add("onclick", "return confirm(" + ScriptHelper.GetString(GetString("EventLogList.ClearConfirmation")) + ");");
            }
        }
    }

    #endregion


    #region "UI Handlers"

    /// <summary>
    /// Handles site selection change event.
    /// </summary>
    protected void UniSelector_OnSelectionChanged(object sender, EventArgs e)
    {
        // Update eventLog
        eventLog.ReloadData();
        eventLog.Update();
    }


    /// <summary>
    /// Deletes event logs from DB.
    /// </summary>
    protected void ClearLogButton_Click(object sender, EventArgs e)
    {
        if (!CMSContext.CurrentUser.IsAuthorizedPerResource("CMS.EventLog", "ClearLog"))
        {
            RedirectToAccessDenied("CMS.EventLog", "ClearLog");
        }

        UserInfo ui = CMSContext.CurrentUser;

        // Deletes event logs of specific site from DB
        eventProvider.ClearEventLog(ui.UserID, ui.UserName, Request.UserHostAddress, siteID);

        eventLog.ReloadData();
    }

    #endregion


    #region "Private methods"

    /// <summary>
    /// OnCheckPermission event handler.
    /// </summary>
    /// <param name="permissionType">Type of the permission</param>
    /// <param name="sender">The sender</param>
    private void eventLog_OnCheckPermissions(string permissionType, CMSAdminControl sender)
    {
        CheckPermissions(true);
    }

    #endregion
}
