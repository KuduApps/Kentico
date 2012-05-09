using System;

using CMS.GlobalHelper;
using CMS.Synchronization;
using CMS.CMSHelper;
using CMS.UIControls;

public partial class CMSModules_Staging_Tools_Servers_Server_Edit : CMSStagingServersPage
{
    #region "Protected variables"

    protected int serverID = 0;
    protected ServerInfo serverObj = null;

    #endregion


    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Check 'Manage servers' permission
        if (!CMSContext.CurrentUser.IsAuthorizedPerResource("cms.staging", "ManageServers"))
        {
            RedirectToAccessDenied("cms.staging", "ManageServers");
        }

        rfvServerDisplayName.ErrorMessage = GetString("Server_Edit.ErrorEmptyServerDisplayName");
        rfvServerName.ErrorMessage = GetString("Server_Edit.ErrorEmptyServerCodeName");
        rfvServerURL.ErrorMessage = GetString("Server_Edit.ErrorEmptyServerURL");

        radUserName.CheckedChanged += Authentication_CheckedChanged;
        radX509.CheckedChanged += Authentication_CheckedChanged;
        ScriptHelper.RegisterStartupScript(this, typeof(string), "setHelpTopic", ScriptHelper.GetScript("parent.frames['stagingHeader'].SetHelpTopic('helpTopic', 'new_server');"));

        string currentServer = GetString("Server_Edit.NewItemCaption");

        serverID = QueryHelper.GetInteger("serverID", 0);
        if (serverID > 0)
        {
            // Check hash
            if (!QueryHelper.ValidateHash("hash"))
            {
                RedirectToAccessDenied(ResHelper.GetString("dialogs.badhashtitle"));
            }

            if (!RequestHelper.IsPostBack() && (Request.QueryString["saved"] != null))
            {
                lblInfo.Visible = true;
                lblInfo.Text = GetString("General.ChangesSaved");
            }

            serverObj = ServerInfoProvider.GetServerInfo(serverID);
            // Set edited object
            EditedObject = serverObj;
            currentServer = serverObj.ServerDisplayName;

            // Fill editing form
            if (!RequestHelper.IsPostBack())
            {
                LoadData(serverObj);
            }
        }

        // Associate server checker control
        serverChecker.TextBoxControlID = txtServerURL.ID;

        // Initializes page title control		
        InitializeBreadcrumbs(currentServer);

        if (serverID > 0)
        {
            CurrentMaster.Title.TitleImage = GetImageUrl("Objects/Staging_Server/object.png");
        }
        else
        {
            CurrentMaster.Title.TitleImage = GetImageUrl("Objects/Staging_Server/new.png");
        }
    }

    #endregion


    #region "Protected methods"

    protected void InitializeBreadcrumbs(string currentServer)
    {
        string[,] breadcrumbs = new string[2, 4];
        breadcrumbs[0, 0] = GetString("Server_Edit.ItemListLink");
        breadcrumbs[0, 1] = "~/CMSModules/Staging/Tools/Servers/List.aspx";
        breadcrumbs[0, 2] = "";
        breadcrumbs[0, 3] = "parent.frames['stagingHeader'].SetHelpTopic('helpTopic', 'cms_staging_servers');";
        breadcrumbs[1, 0] = currentServer;
        breadcrumbs[1, 1] = "";
        breadcrumbs[1, 2] = "";
        CurrentMaster.Title.Breadcrumbs = breadcrumbs;
    }


    /// <summary>
    /// Load data of editing server.
    /// </summary>
    /// <param name="serverInfo">Server object</param>
    protected void LoadData(ServerInfo serverInfo)
    {
        txtServerDisplayName.Text = serverInfo.ServerDisplayName;
        txtServerName.Text = serverInfo.ServerName;
        txtServerURL.Text = serverInfo.ServerURL;
        chkServerEnabled.Checked = serverInfo.ServerEnabled;

        txtServerX509ServerKeyID.Text = serverInfo.ServerX509ServerKeyID;
        txtServerX509ClientKeyID.Text = serverInfo.ServerX509ClientKeyID;
        txtServerUsername.Text = serverInfo.ServerUsername;
        encryptedPassword.Value = serverInfo.ServerPassword;

        if (serverInfo.ServerAuthentication == ServerAuthenticationEnum.X509)
        {
            radX509.Checked = true;
            radUserName.Checked = false;
        }
        else
        {
            radUserName.Checked = true;
            radX509.Checked = false;
        }

        SetAuthenticationModeControls(serverInfo.ServerAuthentication);
    }


    protected void SetAuthenticationModeControls(ServerAuthenticationEnum authentication)
    {
        txtServerX509ClientKeyID.Enabled = (authentication == ServerAuthenticationEnum.X509);
        txtServerX509ServerKeyID.Enabled = (authentication == ServerAuthenticationEnum.X509);
        txtServerUsername.Enabled = (authentication != ServerAuthenticationEnum.X509);
        encryptedPassword.Enabled = (authentication != ServerAuthenticationEnum.X509);
    }

    #endregion


    #region "Control events"

    protected void Authentication_CheckedChanged(object sender, EventArgs e)
    {
        if (radX509.Checked)
        {
            SetAuthenticationModeControls(ServerAuthenticationEnum.X509);
        }
        else
        {
            SetAuthenticationModeControls(ServerAuthenticationEnum.UserName);
        }
    }


    /// <summary>
    /// Sets data to database.
    /// </summary>
    protected void btnOK_Click(object sender, EventArgs e)
    {
        // Check 'Manage servers' permission
        if (!CMSContext.CurrentUser.IsAuthorizedPerResource("cms.staging", "ManageServers"))
        {
            RedirectToAccessDenied("cms.staging", "ManageServers");
        }

        string errorMessage = new Validator().NotEmpty(txtServerDisplayName, GetString("General.requiresDisplayName")).NotEmpty(txtServerName, GetString("General.RequiresCodeName")).
            IsCodeName(txtServerName.Text, GetString("general.invalidcodename"))
            .Result;
        if (errorMessage == "")
        {
            // Server name must be unique
            serverObj = ServerInfoProvider.GetServerInfo(txtServerName.Text.Trim(), CMSContext.CurrentSite.SiteID);

            // If server name is unique														
            if ((serverObj == null) || (serverObj.ServerID == serverID))
            {
                // If server name is unique -> determine whether it is update or insert 
                if ((serverObj == null))
                {
                    // Get ServerInfo object by primary key
                    serverObj = ServerInfoProvider.GetServerInfo(serverID) ?? new ServerInfo();
                }

                serverObj.ServerSiteID = CMSContext.CurrentSite.SiteID;
                serverObj.ServerX509ServerKeyID = txtServerX509ServerKeyID.Text.Trim();
                serverObj.ServerPassword = encryptedPassword.Value.ToString();
                serverObj.ServerAuthentication = (radX509.Checked) ? ServerAuthenticationEnum.X509 : ServerAuthenticationEnum.UserName;
                serverObj.ServerDisplayName = txtServerDisplayName.Text.Trim();
                serverObj.ServerURL = txtServerURL.Text.Trim();
                serverObj.ServerX509ClientKeyID = txtServerX509ClientKeyID.Text.Trim();
                serverObj.ServerName = txtServerName.Text.Trim();
                serverObj.ServerUsername = txtServerUsername.Text.Trim();
                serverObj.ServerEnabled = chkServerEnabled.Checked;

                ServerInfoProvider.SetServerInfo(serverObj);

                // Refresh breadcrumbs
                InitializeBreadcrumbs(serverObj.ServerDisplayName);

                lblInfo.Visible = true;
                lblInfo.Text = GetString("General.ChangesSaved");

                if (serverID <= 0)
                {
                    string detailUrl = "Server_Edit.aspx?serverid=" + serverObj.ServerID + "&saved=1";
                    detailUrl = URLHelper.AddParameterToUrl(detailUrl, "hash", QueryHelper.GetHash(detailUrl));

                    URLHelper.Redirect(detailUrl);
                }
            }
            else
            {
                lblError.Visible = true;
                lblError.Text = GetString("Server_Edit.ServerNameExists");
            }
        }
        else
        {
            lblError.Visible = true;
            lblError.Text = errorMessage;
        }
    }

    #endregion
}
