using System;

using CMS.GlobalHelper;
using CMS.WebFarmSync;
using CMS.CMSHelper;
using CMS.UIControls;
using CMS.LicenseProvider;
using CMS.SettingsProvider;
using CMS.EventLog;

public partial class CMSModules_WebFarm_Pages_WebFarm_Server_Edit : SiteManagerPage
{
    int serverid;

    protected void Page_Load(object sender, EventArgs e)
    {
        this.CurrentMaster.Title.HelpTopicName = "webfarm_server_list";

        // Initialize breadcrumbs 		
        string[,] pageTitleTabs = new string[2, 3];
        pageTitleTabs[0, 0] = GetString("WebFarmServers_Edit.WebFarmServers");
        pageTitleTabs[0, 1] = "~/CMSModules/WebFarm/Pages/WebFarm_Server_List.aspx";
        pageTitleTabs[0, 2] = "";
        pageTitleTabs[1, 1] = "";
        pageTitleTabs[1, 2] = "";
        pageTitleTabs[1, 0] = GetString("WebFarmServers_Edit.New");

        btnOk.Text = GetString("general.ok");
        rfvCodeName.ErrorMessage = GetString("general.requirescodename");
        rfvURL.ErrorMessage = GetString("WebFarmServers_Edit.UrlEmpty");
        rfvDisplayName.ErrorMessage = GetString("general.requiresdisplayname");

        // Get server ID
        serverid = QueryHelper.GetInteger("serverid", 0);

        if (serverid > 0)
        {
            WebFarmServerInfo wi = WebFarmServerInfoProvider.GetWebFarmServerInfo(serverid);

            if (wi == null)
            {
                lblError.Visible = true;
                lblError.Text = GetString("WebFarmServers_Edit.InvalidServerID");
                plcEditForm.Visible = false;
            }
            else
            {
                pageTitleTabs[1, 0] = HTMLHelper.HTMLEncode(wi.ServerDisplayName);
                if (!RequestHelper.IsPostBack())
                {
                    txtCodeName.Text = wi.ServerName;
                    txtDisplayName.Text = wi.ServerDisplayName;
                    txtURL.Text = wi.ServerURL;
                    chkEnabled.Checked = wi.ServerEnabled;
                }
            }
        }

        this.CurrentMaster.Title.Breadcrumbs = pageTitleTabs;

        // Associate server checker control
        serverChecker.TextBoxControlID = txtURL.ID;

        if (ValidationHelper.GetString(Request.QueryString["saved"], "") != "")
        {
            lblInfo.Text = GetString("General.ChangesSaved");
            lblInfo.Visible = true;
        }
    }


    protected void btnOK_Click(object sender, EventArgs e)
    {
        string result = new Validator().NotEmpty(rfvDisplayName, rfvDisplayName.ErrorMessage).NotEmpty(rfvCodeName, rfvCodeName.ErrorMessage).NotEmpty(rfvURL, rfvURL.ErrorMessage)
            .IsCodeName(txtCodeName.Text, GetString("general.invalidcodename"))
            .Result;

        // Get the object
        WebFarmServerInfo wi = WebFarmServerInfoProvider.GetWebFarmServerInfo(serverid) ?? new WebFarmServerInfo();

        // Check license web farm server limit
        if (String.IsNullOrEmpty(result))
        {
            LicenseKeyInfo lki = LicenseHelper.CurrentLicenseInfo;
            if (lki == null)
            {
                return;
            }

            // Only if server is enabled
            if (chkEnabled.Checked)
            {
                // Enabling or new server as action insert
                VersionActionEnum action = ((wi.ServerID > 0) && wi.ServerEnabled) ? VersionActionEnum.Edit : VersionActionEnum.Insert;
                if (!lki.CheckServerCount(WebSyncHelperClass.ServerCount, action))
                {
                    result = GetString("licenselimitation.infopagemessage");
                }

                // Log the message
                if (!String.IsNullOrEmpty(result))
                {
                    EventLogProvider eventLog = new EventLogProvider();
                    string message = GetString("licenselimitation.serversexceeded");
                    eventLog.LogEvent(EventLogProvider.EVENT_TYPE_WARNING, DateTime.Now, "WebFarms", LicenseHelper.LICENSE_LIMITATION_EVENTCODE, URLHelper.CurrentURL, message);
                }
            }
        }


        if (result == "")
        {
            wi.ServerID = serverid;
            wi.ServerDisplayName = txtDisplayName.Text;
            wi.ServerName = txtCodeName.Text;
            wi.ServerURL = txtURL.Text;
            wi.ServerEnabled = chkEnabled.Checked;
            try
            {
                WebFarmServerInfoProvider.SetWebFarmServerInfo(wi);
                // Clear server list
                URLHelper.Redirect("WebFarm_Server_Edit.aspx?serverid=" + wi.ServerID + "&saved=1");
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message.Replace("%%name%%", wi.ServerName);
                lblError.Visible = true;
                lblInfo.Visible = false;
            }
        }
        else
        {
            lblError.Text = result;
            lblError.Visible = true;
            lblInfo.Visible = false;
        }
    }
}
