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
using CMS.SiteProvider;
using CMS.CMSHelper;
using CMS.UIControls;

public partial class CMSSiteManager_Sites_Site_Edit_DomainAlias_Edit : SiteManagerPage, ICallbackEventHandler
{
    #region "Variables"

    private int siteId = 0;
    private SiteDomainAliasInfo alias = null;
    private SiteInfo si = null;

    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        siteId = QueryHelper.GetInteger("siteid", 0);
        si = SiteInfoProvider.GetSiteInfo(siteId);

        int domainAliasId = QueryHelper.GetInteger("domainaliasid", 0);

        if (!RequestHelper.IsPostBack())
        {
            // Display error from previous save if occurred
            string runError = QueryHelper.GetString("runerror", String.Empty);
            string[] collisionSite = runError.Split(';');
            if ((collisionSite.Length == 2) && (si != null))
            {
                lblError.Text = String.Format(GetString("SiteDomain.RunError"), HTMLHelper.HTMLEncode(collisionSite[0]), HTMLHelper.HTMLEncode(collisionSite[1]), HTMLHelper.HTMLEncode(si.DisplayName));
                lblError.Visible = true;
            }
        }

        InitializeComponents();

        if (QueryHelper.GetString("saved", String.Empty) != String.Empty)
        {
            lblInfo.Text = GetString("General.ChangesSaved");
            lblInfo.Visible = true;
        }

        alias = SiteDomainAliasInfoProvider.GetSiteDomainAliasInfo(domainAliasId);
        if (!RequestHelper.IsPostBack())
        {
            LoadData();
        }

        string currentAlias;
        // Update breadcrumb label
        if (alias != null)
        {
            currentAlias = alias.SiteDomainAliasName;
        }
        else
        {
            // New domain alias
            currentAlias = GetString("SiteDomain.NewAlias");
        }

        // initializes page title control		
        string[,] breadcrumbs = new string[2, 3];
        breadcrumbs[0, 0] = GetString("SiteDomain.ItemListLink");
        breadcrumbs[0, 1] = ResolveUrl("~/CMSSiteManager/Sites/Site_Edit_DomainAliases.aspx?siteid=" + siteId);
        breadcrumbs[0, 2] = "";
        breadcrumbs[1, 0] = currentAlias;
        breadcrumbs[1, 1] = "";
        breadcrumbs[1, 2] = "";

        this.CurrentMaster.Title.Breadcrumbs = breadcrumbs;
        this.CurrentMaster.Title.HelpTopicName = "edit_and_new_alias";
    }


    /// <summary>
    /// Initialization of UI components.
    /// </summary>
    private void InitializeComponents()
    {
        // Set the page selector
        pageSelector.IsLiveSite = false;
        lblDefaultAliasPath.AssociatedControlClientID = pageSelector.ValueElementID;

        // Set label strings
        lblDomainName.ResourceString = "SiteDomain.DomainName";
        lblVisitorCulture.ResourceString = "SiteDomain.VisitorCulture";
        lblDefaultAliasPath.ResourceString = "sitedomain.defaultaliaspath";
        lblRedirectUrl.ResourceString = "sitedomain.redirecturl";
        rfvDomainName.Text = GetString("Site_Edit.AliasRequired");

        btnOk.ResourceString = "general.ok";

        // Prepare callback check for running site - notofication before site is stopped
        if ((si != null) && (si.Status == SiteStatusEnum.Running))
        {
            ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "GetDomainName", ScriptHelper.GetScript("function GetDomainName() { return document.getElementById('" + txtDomainName.ClientID + "').value; }"));
            ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "ReceiveServerData",
                ScriptHelper.GetScript("function ReceiveServerData(value) { " +
            "if ((value == 'ok') || confirm('" + GetString("sitedomain.proceedwithcollision") + "')) { " + this.ClientScript.GetPostBackEventReference(btnOk, null) + "; } " +
            "else { return false; } }"));

            btnOk.OnClientClick = this.Page.ClientScript.GetCallbackEventReference(this, "GetDomainName()", "ReceiveServerData", null) + ";return false;";
        }


        // Initialize culture selector
        this.cultureSelector.AddDefaultRecord = false;
        this.cultureSelector.SpecialFields = new string[,] { { GetString("Site_Edit.Automatic"), "" } };
        this.cultureSelector.SiteID = siteId;

        // Initialize the page selector
        pageSelector.SiteID = siteId;
    }


    /// <summary>
    /// Load data from SiteDomainAliasInfo object to UI.
    /// </summary>    
    private void LoadData()
    {
        if (alias != null)
        {
            pageSelector.Value = alias.SiteDomainDefaultAliasPath;
            txtDomainName.Text = alias.SiteDomainAliasName;
            txtRedirectUrl.Text = alias.SiteDomainRedirectUrl;

            this.cultureSelector.Value = alias.SiteDefaultVisitorCulture;
        }
    }


    /// <summary>
    /// Save or update domain alias.
    /// </summary>
    protected void btnOk_Click(object sender, EventArgs e)
    {
        string result = new Validator().NotEmpty(txtDomainName.Text, GetString("Site_Edit.AliasRequired")).Result;
        if (result == "")
        {
            // Check site of this domain alias
            if (si == null)
            {
                return;
            }

            // Stop the site before adding alias
            bool wasRunning = false;
            if (si.Status == SiteStatusEnum.Running)
            {
                SiteInfoProvider.StopSite(si.SiteName);
                si.Status = SiteStatusEnum.Stopped;
                wasRunning = true;
            }

            string redirect = String.Empty;

            // Correct domain name and return it to textbox
            txtDomainName.Text = URLHelper.RemoveProtocol(txtDomainName.Text);

            // Insert new
            if (alias == null)
            {
                // Check duplicity
                if (!SiteDomainAliasInfoProvider.DomainAliasExists(txtDomainName.Text, siteId))
                {
                    SiteDomainAliasInfo sdai = new SiteDomainAliasInfo();
                    sdai.SiteID = si.SiteID;
                    sdai.SiteDomainAliasName = txtDomainName.Text.Trim();
                    sdai.SiteDefaultVisitorCulture = ValidationHelper.GetString(this.cultureSelector.Value, "");
                    sdai.SiteDomainDefaultAliasPath = pageSelector.Value.ToString().Trim();
                    sdai.SiteDomainRedirectUrl = txtRedirectUrl.Text.Trim();

                    SiteDomainAliasInfoProvider.SetSiteDomainAliasInfo(sdai);

                    redirect = "Site_Edit_DomainAlias_Edit.aspx?siteId=" + sdai.SiteID + "&domainaliasid=" + sdai.SiteDomainAliasID + "&saved=1";
                }
                else
                {
                    lblError.Visible = true;
                    lblError.Text = GetString("Site_Edit.AliasExists");
                }
            }
            // Update
            else
            {
                // Check duplicity
                SiteDomainAliasInfo existing = SiteDomainAliasInfoProvider.GetSiteDomainAliasInfo(txtDomainName.Text, siteId);
                if ((existing == null) || (existing.SiteDomainAliasID == alias.SiteDomainAliasID))
                {
                    string originalDomainAlias = alias.SiteDomainAliasName;
                    alias.SiteDomainAliasName = txtDomainName.Text.Trim();
                    alias.SiteDefaultVisitorCulture = ValidationHelper.GetString(this.cultureSelector.Value, "");
                    alias.SiteDomainDefaultAliasPath = pageSelector.Value.ToString().Trim();
                    alias.SiteDomainRedirectUrl = txtRedirectUrl.Text.Trim();

                    SiteDomainAliasInfoProvider.SetSiteDomainAliasInfo(alias, originalDomainAlias);

                    // Update breadcrumbs
                    this.CurrentMaster.Title.Breadcrumbs[1, 0] = alias.SiteDomainAliasName;
                    

                    lblInfo.Text = GetString("General.ChangesSaved");
                    lblInfo.Visible = true;
                }
                else
                {
                    lblError.Visible = true;
                    lblError.Text = GetString("Site_Edit.AliasExists");
                }
            }

            // Run site again
            if (wasRunning)
            {
                DataSet ds = SiteInfoProvider.CheckDomainNameForCollision(txtDomainName.Text.Trim(), si.SiteID);
                if (!DataHelper.DataSourceIsEmpty(ds))
                {
                    SiteInfo runningsi = SiteInfoProvider.GetSiteInfo(ValidationHelper.GetInteger(ds.Tables[0].Rows[0]["SiteID"], 0));
                    if (runningsi != null)
                    {
                        string collisionSite = runningsi.DisplayName;
                        string collisionDomain = ValidationHelper.GetString(ds.Tables[0].Rows[0]["SiteDomainAliasName"], "");

                        if (String.IsNullOrEmpty(redirect))
                        {
                            redirect = "Site_Edit_DomainAlias_Edit.aspx?siteId=" + alias.SiteID + "&domainaliasid=" + alias.SiteDomainAliasID + "&saved=1";
                        }

                        // Add parameter indicating run problem
                        redirect = URLHelper.AddParameterToUrl(redirect, "runerror", collisionSite + ";" + collisionDomain);
                    }
                }
                else
                {
                    // Seems to be ok, run the site
                    try
                    {
                        SiteInfoProvider.RunSite(si.SiteName);
                    }
                    catch (Exception ex)
                    {
                        lblError.Visible = true;
                        lblError.Text = ex.Message;

                        redirect = "";
                    }
                }
            }

            // Redirect
            if (redirect != "")
            {
                URLHelper.Redirect(redirect);
            }
        }
        else
        {
            lblError.Visible = true;
            lblError.Text = result;
        }
    }


    #region ICallbackEventHandler Members

    string domainName = string.Empty;

    public string GetCallbackResult()
    {
        if (si != null)
        {
            DataSet ds = SiteInfoProvider.CheckDomainNameForCollision(domainName, si.SiteID);
            if (!DataHelper.DataSourceIsEmpty(ds))
            {
                return "running";
            }
        }

        return "ok";
    }

    public void RaiseCallbackEvent(string eventArgument)
    {
        domainName = eventArgument;
    }

    #endregion
}


