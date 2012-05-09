using System;
using System.Collections;
using System.Security.Principal;
using System.Web;
using System.Web.UI;

using CMS.CMSHelper;
using CMS.CMSImportExport;
using CMS.GlobalHelper;
using CMS.SiteProvider;
using CMS.UIControls;


public partial class CMSSiteManager_Sites_Site_Delete : SiteManagerPage, ICallbackEventHandler
{
    #region "Variables"

    private static Hashtable mManagers = new Hashtable();

    // Site ID
    private int siteId = 0;

    // Site name
    private string siteName = "";

    // Site display name
    private string siteDisplayName = "";

    SiteInfo si = null;

    #endregion


    #region "Public properties"
    
    /// <summary>
    /// Deletion manager.
    /// </summary>
    public SiteDeletionManager DeletionManager
    {
        get
        {
            string key = "delManagers_" + ProcessGUID;
            if (mManagers[key] == null)
            {
                // Restart of the application
                if (ApplicationInstanceGUID != CMSContext.ApplicationInstanceGUID)
                {
                    LogStatusEnum progressLog = DeletionInfo.GetProgressState();
                    if (progressLog != LogStatusEnum.Finish)
                    {
                        DeletionInfo.LogDeletionState(LogStatusEnum.UnexpectedFinish, ResHelper.GetAPIString("Site_Delete.Applicationrestarted", "<strong>Application has been restarted and the logging of the site delete process has been terminated. Please make sure that the site is deleted. If it is not, please repeate the deletion process.</strong><br />"));
                    }
                }

                SiteDeletionManager dm = new SiteDeletionManager(DeletionInfo);
                mManagers[key] = dm;
            }
            return (SiteDeletionManager)mManagers[key];
        }
        set
        {
            string key = "delManagers_" + ProcessGUID;
            mManagers[key] = value;
        }
    }


    /// <summary>
    /// Application instance GUID.
    /// </summary>
    public Guid ApplicationInstanceGUID
    {
        get
        {
            if (ViewState["ApplicationInstanceGUID"] == null)
            {
                ViewState["ApplicationInstanceGUID"] = CMSContext.ApplicationInstanceGUID;
            }

            return ValidationHelper.GetGuid(ViewState["ApplicationInstanceGUID"], Guid.Empty);
        }
    }


    /// <summary>
    /// Import process GUID.
    /// </summary>
    public Guid ProcessGUID
    {
        get
        {
            if (ViewState["ProcessGUID"] == null)
            {
                ViewState["ProcessGUID"] = Guid.NewGuid();
            }

            return ValidationHelper.GetGuid(ViewState["ProcessGUID"], Guid.Empty);
        }
    }


    /// <summary>
    /// Persistent settings key.
    /// </summary>
    public string PersistentSettingsKey
    {
        get
        {
            return "SiteDeletion_" + ProcessGUID + "_Settings";
        }
    }


    /// <summary>
    /// Deletion info.
    /// </summary>
    public DeletionInfo DeletionInfo
    {
        get
        {
            DeletionInfo delInfo = (DeletionInfo)PersistentStorageHelper.GetValue(PersistentSettingsKey);
            if (delInfo == null)
            {
                throw new Exception("[SiteDelete.DeletionInfo]: Deletion info has been lost.");
            }
            return delInfo;
        }
        set
        {
            PersistentStorageHelper.SetValue(PersistentSettingsKey, value);
        }
    }

    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        // Register script for pendingCallbacks repair
        ScriptHelper.FixPendingCallbacks(this.Page);

        if (!IsCallback)
        {
            if (!RequestHelper.IsPostBack())
            {
                // Initialize deletion info
                DeletionInfo = new DeletionInfo();
                DeletionInfo.PersistentSettingsKey = PersistentSettingsKey;
            }

            DeletionManager.DeletionInfo = DeletionInfo;

            // Register the script to perform get flags for showing buttons retrieval callback
            ScriptHelper.RegisterClientScriptBlock(this, GetType(), "GetState", ScriptHelper.GetScript("function GetState(cancel){ return " + Page.ClientScript.GetCallbackEventReference(this, "cancel", "SetStateMssg", null) + " } \n"));

            // Setup page title text and image
            CurrentMaster.Title.TitleText = GetString("Site_Edit.DeleteSite");
            CurrentMaster.Title.TitleImage = GetImageUrl("CMSModules/CMS_Sites/deletesite.png");

            //initialize PageTitle
            string[,] pageTitleTabs = new string[2, 3];
            pageTitleTabs[0, 0] = GetString("general.sites");
            pageTitleTabs[0, 1] = "~/CMSSiteManager/Sites/site_list.aspx";
            pageTitleTabs[0, 2] = "cmsdesktop";
            pageTitleTabs[1, 0] = GetString("Site_Edit.DeleteSite");
            pageTitleTabs[1, 1] = "";
            CurrentMaster.Title.Breadcrumbs = pageTitleTabs;
            CurrentMaster.Title.HelpTopicName = "site_deletion";
            CurrentMaster.Title.HelpName = "helpTopic";

            // Get site ID
            siteId = ValidationHelper.GetInteger(Request.QueryString["siteId"], 0);

            si = SiteInfoProvider.GetSiteInfo(siteId);
            if (si != null)
            {
                siteName = si.SiteName;
                siteDisplayName = HTMLHelper.HTMLEncode(ResHelper.LocalizeString(si.DisplayName));

                ucHeader.Header = string.Format(GetString("Site_Delete.Header"), siteDisplayName);
                ucHeaderConfirm.Header = GetString("Site_Delete.HeaderConfirm");

                // Initialize web root path
                DeletionInfo.WebRootFullPath = HttpContext.Current.Server.MapPath("~/");

                DeletionInfo.DeletionLog = string.Format("I" + SiteDeletionManager.SEPARATOR + DeletionManager.DeletionInfo.GetAPIString("Site_Delete.DeletingSite", "Initializing deletion of the site") + SiteDeletionManager.SEPARATOR + SiteDeletionManager.SEPARATOR, siteName);

                lblConfirmation.Text = string.Format(GetString("Site_Edit.Confirmation"), siteDisplayName);
                btnYes.Text = GetString("General.Yes");
                btnNo.Text = GetString("General.No");
                btnOk.Text = GetString("General.OK");
                lblLog.Text = string.Format(GetString("Site_Delete.DeletingSite"), siteDisplayName);
            }

            btnYes.Click += btnYes_Click;
            btnNo.Click += btnNo_Click;
            btnOk.Click += btnOK_Click;

            // Javascript functions
            string script =
                        "function SetStateMssg(rValue, context) \n" +
                        "{\n" +
                        "   var values = rValue.split('<#>');\n" +
                        "   if((values[0]=='E') || (values[0]=='F') || values=='')\n" +
                        "   {\n" +
                        "       StopStateTimer();\n" +
                        "       BTN_Enable('" + btnOk.ClientID + "');\n" +
                        "   }\n" +
                        "   if(values[0]=='E')\n" +
                        "   {\n" +
                        "       document.getElementById('" + lblError.ClientID + "').innerHTML = values[2];\n" +
                        "   }\n" +
                        "   else if(values[0]=='I')\n" +
                        "   {\n" +
                        "       document.getElementById('" + lblLog.ClientID + "').innerHTML = values[1];\n" +
                        "   }\n" +
                        "   else if((values=='') || (values[0]=='F'))\n" +
                        "   {\n" +
                        "       document.getElementById('" + lblLog.ClientID + "').innerHTML = values[1];\n" +
                        "   }\n" +
                        "   document.getElementById('" + lblWarning.ClientID + "').innerHTML = values[3];\n" +
                        "}\n";

            // Register the script to perform get flags for showing buttons retrieval callback
            ScriptHelper.RegisterClientScriptBlock(this, GetType(), "GetDeletionState", ScriptHelper.GetScript(script));
        }
    }


    protected void btnOK_Click(object sender, EventArgs e)
    {
        URLHelper.Redirect("~/CMSSiteManager/Sites/site_list.aspx");
    }


    protected void btnNo_Click(object sender, EventArgs e)
    {
        URLHelper.Redirect("~/CMSSiteManager/Sites/site_list.aspx");
    }


    void btnYes_Click(object sender, EventArgs e)
    {
        pnlConfirmation.Visible = false;
        pnlDeleteSite.Visible = true;

        // Start the timer for the callbacks
        ltlScript.Text = ScriptHelper.GetScript("StartStateTimer();");

        // Initilaize web root path
        AttachmentHelper.WebRootFullPath = HttpContext.Current.Server.MapPath("~/");

        // Deletion info initialization
        DeletionInfo.DeleteAttachments = chkDeleteDocumentAttachments.Checked;
        DeletionInfo.DeleteMediaFiles = chkDeleteMediaFiles.Checked;
        DeletionInfo.DeleteMetaFiles = chkDeleteMetaFiles.Checked;
        DeletionInfo.SiteName = siteName;
        DeletionInfo.SiteDisplayName = siteDisplayName;
        DeletionManager.CurrentUser = CMSContext.CurrentUser;
        DeletionManager.DeletionInfo = DeletionInfo;

        AsyncWorker worker = new AsyncWorker();
        worker.RunAsync(DeletionManager.DeleteSite, WindowsIdentity.GetCurrent());
    }


    /// <summary>
    /// Callback event handler.
    /// </summary>
    /// <param name="argument">Callback argument</param>
    public void RaiseCallbackEvent(string argument)
    {
        hdnLog.Value = DeletionManager.DeletionInfo.DeletionLog;
    }


    /// <summary>
    /// Callback result retrieving handler.
    /// </summary>
    public string GetCallbackResult()
    {
        return hdnLog.Value;
    }
}
