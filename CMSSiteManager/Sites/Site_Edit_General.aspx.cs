using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Web.UI;

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.SettingsProvider;
using CMS.SiteProvider;
using CMS.UIControls;

public partial class CMSSiteManager_Sites_Site_Edit_General : SiteManagerPage, IPostBackEventHandler, ICallbackEventHandler
{

    #region "Variables"

    protected int siteId = 0;
    protected string siteName = "";
    protected string currentCulture = "";
    private SiteInfo si = null;

    #endregion


    #region "Events and methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        siteId = QueryHelper.GetInteger("siteid", 0);

        RequiredFieldValidatorCodeName.ErrorMessage = GetString("Administration-Site_Edit.RequiresCodeName");
        RequiredFieldValidatorDisplayName.ErrorMessage = GetString("Administration-Site_Edit.RequiresDisplayName");
        RequiredFieldValidatorDomainName.ErrorMessage = GetString("Administration-Site_Edit.RequiresDomainName");

        lblCodeName.Text = GetString("Site_Edit.CodeName");
        lblDescription.Text = GetString("Site_Edit.Description");
        lblDisplayName.Text = GetString("Site_Edit.DisplayName");
        lblDomainName.Text = GetString("Site_Edit.DomainName");
        lblCulture.Text = GetString("Site_Edit.ContentCulture");
        //lblLicenseKey.Text = GetString("Site_Edit.LicenseKey");
        lblCssStyle.Text = GetString("NewSite_SiteDetails.CssStyle");
        lblEditorStyle.Text = GetString("Site_Edit.EditorStyleSheet");
        lblVisitorCulture.Text = GetString("Site_Edit.VisitorCulture");
        btnOk.Text = GetString("general.ok");
        btnChange.Text = GetString("general.change");

        // Set the culture textbox readonly
        this.txtCulture.Attributes.Add("readonly", "readonly");

        // Stylesheet selector
        ctrlEditorSelectStyleSheet.CurrentSelector.SpecialFields = new string[1, 2] { { GetString("administration-site_edit.sitestylesheet"), "0" } };
        ctrlEditorSelectStyleSheet.ReturnColumnName = "StyleSheetID";
        ctrlEditorSelectStyleSheet.SiteId = siteId;

        ctrlSiteSelectStyleSheet.CurrentSelector.SpecialFields = new string[1, 2] { { GetString("general.selectnone"), "0" } };
        ctrlSiteSelectStyleSheet.ReturnColumnName = "StyleSheetID";
        ctrlSiteSelectStyleSheet.SiteId = siteId;

        ltlScript.Text = ScriptHelper.GetScript(
            "var pageChangeUrl='" + ResolveUrl("~/CMSSiteManager/Sites/CultureChange.aspx") + "'; " +
            "function ChangeCulture(documentChanged){ var hiddenElem = document.getElementById('" + hdnDocumentsChangeChecked.ClientID + "');" +
            "hiddenElem.value = documentChanged;" +
            Page.ClientScript.GetPostBackEventReference(btnHidden, "") + "  } "
            );

        // Initialize culture selector
        this.cultureSelector.AddDefaultRecord = false;
        this.cultureSelector.SpecialFields = new string[,] { { GetString("Site_Edit.Automatic"), "" } };
        this.cultureSelector.SiteID = siteId;

        si = SiteInfoProvider.GetSiteInfo(siteId);
        if (si != null)
        {
            if (!RequestHelper.IsPostBack() && (si.SiteName != null))
            {
                siteName = si.SiteName;

                txtCodeName.Text = siteName;
                txtDescription.Text = si.Description;
                txtDisplayName.Text = si.DisplayName;
                txtDomainName.Text = si.DomainName;

                ctrlSiteSelectStyleSheet.Value = si.SiteDefaultStylesheetID;
                ctrlEditorSelectStyleSheet.Value = si.SiteDefaultEditorStylesheet;

                if (CultureHelper.GetDefaultCulture(siteName) != null)
                {
                    CultureInfo ci = CultureInfoProvider.GetCultureInfo(CultureHelper.GetDefaultCulture(siteName));

                    if (ci != null)
                    {
                        txtCulture.Text =  ResHelper.LocalizeString(ci.CultureName);
                        currentCulture = ci.CultureCode;
                    }
                }

                this.cultureSelector.Value = si.DefaultVisitorCulture;

                // Check version limitations
                if (!CultureInfoProvider.LicenseVersionCheck(si.DomainName, FeatureEnum.Multilingual, VersionActionEnum.Edit))
                {
                    lblError.Text = GetString("licenselimitation.siteculturesexceeded");
                    lblError.Visible = true;
                    cultureSelector.Enabled = false;
                    btnOk.Enabled = false;
                }
            }
        }

        btnChange.OnClientClick = "OpenCultureChanger('" + siteId + "','" + currentCulture + "'); return false;";
    }


    protected void Page_PreRender(object sender, EventArgs e)
    {
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
    }


    /// <summary>
    /// Save current site's data.
    /// </summary>
    protected void btnOk_Click(object sender, EventArgs e)
    {
        // Finds whether required fields are not empty
        string result = new Validator().NotEmpty(txtDisplayName.Text, GetString("Administration-Site_Edit.RequiresDisplayName")).NotEmpty(txtCodeName.Text, GetString("Administration-Site_Edit.RequiresCodeName")).NotEmpty(txtDomainName.Text, GetString("Administration-Site_Edit.RequiresDomainName")).IsCodeName(txtCodeName.Text, GetString("Administration-Site_Edit.NotValidCodeName")).Result;

        // Get resource string
        lblInfo.Text = GetString("General.ChangesSaved");

        if (result == "")
        {
            SiteInfo si = null;
            // Finds whether edited site code name is unique
            si = SiteInfoProvider.GetSiteInfo(txtCodeName.Text.Trim());

            if ((si == null) || (si.SiteID == siteId))
            {
                if (si == null)
                {
                    // Get siteinfo by primary key
                    si = SiteInfoProvider.GetSiteInfo(siteId);
                }

                // Update SiteInfo
                if (SaveSite(si))
                {
                    // Display save information
                    lblInfo.Visible = true;
                }
            }
            else
            {
                lblError.Visible = true;
                lblError.Text = GetString("Administration-Site_Edit.SiteExists");
            }
        }
        else
        {
            lblError.Visible = true;
            lblError.Text = result;
        }
    }


    /// <summary>
    /// Save edited site.
    /// </summary>
    private bool SaveSite(SiteInfo oldSi)
    {
        // Correct domain name and return it to textbox
        txtDomainName.Text = URLHelper.RemoveProtocol(txtDomainName.Text);

        // Get SiteInfo with the given siteID
        SiteInfo si = oldSi.Clone();
        if (siteId > 0)
        {
            try
            {
                // Keep information about running status
                bool runSite = (si.Status == SiteStatusEnum.Running);

                if (si.DisplayName != txtDisplayName.Text)
                {
                    // Refresh the breadcrumb
                    ScriptHelper.RefreshTabHeader(Page, null);
                }

                // Update SiteInfo parameters
                si.SiteName = txtCodeName.Text;
                si.Description = txtDescription.Text;
                si.DisplayName = txtDisplayName.Text;
                si.DomainName = txtDomainName.Text;
                si.DefaultVisitorCulture = ValidationHelper.GetString(cultureSelector.Value, "");
                si.SiteID = siteId;
                si.SiteDefaultStylesheetID = ValidationHelper.GetInteger(ctrlSiteSelectStyleSheet.Value, 0);
                si.SiteDefaultEditorStylesheet = ValidationHelper.GetInteger(ctrlEditorSelectStyleSheet.Value, 0);
                si.Status = SiteStatusEnum.Stopped;

                // Save SiteInfo
                SiteInfoProvider.SetSiteInfo(si);

                // Remove cached cultures for site
                CultureInfoProvider.ClearSiteCultures(true);

                // Clear settings if sitename changes
                if (si.SiteName.ToLower() != txtCodeName.Text.Trim().ToLower())
                {
                    SettingsKeyProvider.Clear(true);
                }

                // Reindex hashtable with sessions if SiteName changes and change info message                
                if (oldSi.SiteName != txtCodeName.Text)
                {
                    SessionManager.ReindexSessionsInfosHashtable(oldSi.SiteName, txtCodeName.Text);
                    if (SearchIndexInfoProvider.SearchEnabled)
                    {
                        lblInfo.Text = String.Format(GetString("general.changessaved") + " " + GetString("srch.indexrequiresrebuild"), "<a href=\"javascript:" + Page.ClientScript.GetPostBackEventReference(this, "saved") + "\">" + GetString("General.clickhere") + "</a>");
                    }
                }

                if (runSite)
                {
                    DataSet ds = SiteInfoProvider.CheckDomainNameForCollision(txtDomainName.Text.Trim(), si.SiteID);
                    if (!DataHelper.DataSourceIsEmpty(ds))
                    {
                        SiteInfo runningsi = SiteInfoProvider.GetSiteInfo(ValidationHelper.GetInteger(ds.Tables[0].Rows[0]["SiteID"], 0));
                        if (runningsi != null)
                        {
                            string collisionSite = HTMLHelper.HTMLEncode(runningsi.DisplayName);
                            string collisionDomain = HTMLHelper.HTMLEncode(ValidationHelper.GetString(ds.Tables[0].Rows[0]["SiteDomainAliasName"], ""));

                            lblError.Text = String.Format(GetString("SiteDomain.RunError"), collisionSite, collisionDomain, HTMLHelper.HTMLEncode(si.DisplayName));
                            lblError.Visible = true;
                        }
                    }
                    else
                    {
                        // Run current site
                        SiteInfoProvider.RunSite(si.SiteName);
                    }
                }
            }
            catch (RunningSiteException exc)
            {
                lblError.Visible = true;
                lblError.Text = exc.Message;
            }
            return true;
        }
        else
        {
            return false;
        }
    }


    /// <summary>
    /// On default culture change.
    /// </summary>
    protected void btnHidden_Click(object sender, EventArgs e)
    {
        SiteInfo si = SiteInfoProvider.GetSiteInfo(siteId);

        if (si != null)
        {
            if (CultureHelper.GetDefaultCulture(si.SiteName) != null)
            {
                CultureInfo ci = CultureInfoProvider.GetCultureInfo(CultureHelper.GetDefaultCulture(si.SiteName));
                if (ci != null)
                {
                    // Rebuild info message
                    if ((txtCulture.Text != ci.CultureName) && ValidationHelper.GetBoolean(hdnDocumentsChangeChecked.Value, false) && SearchIndexInfoProvider.SearchEnabled)
                    {
                        lblInfo.Text = String.Format(GetString("general.changessaved") + " " + GetString("srch.indexrequiresrebuild"), "<a href=\"javascript:" + Page.ClientScript.GetPostBackEventReference(this, "saved") + "\">" + GetString("General.clickhere") + "</a>");
                        lblInfo.Visible = true;
                    }

                    txtCulture.Text = ci.CultureName;
                    btnChange.OnClientClick = "OpenCultureChanger('" + siteId + "','" + ci.CultureCode + "'); return false;";
                }
            }
        }

    }

    #endregion


    #region IPostBackEventHandler Members

    public void RaisePostBackEvent(string eventArgument)
    {
        // Rebuild search index
        if (SearchIndexInfoProvider.SearchEnabled)
        {
            SiteInfo si = SiteInfoProvider.GetSiteInfo(siteId);
            if (si != null)
            {

                // Get all indexes depending on given site
                DataSet result = SearchIndexSiteInfoProvider.GetSiteSearchIndexes(si.SiteID);

                if (!DataHelper.DataSourceIsEmpty(result))
                {
                    List<string> items = new List<string>();
                    SearchIndexInfo sii = null;

                    // Add all indexes to rebuild queue
                    foreach (DataRow dr in result.Tables[0].Rows)
                    {
                        sii = SearchIndexInfoProvider.GetSearchIndexInfo((int)dr["IndexID"]);
                        if (sii != null)
                        {
                            items.Add(sii.IndexName);
                        }
                    }

                    // Rebuild all indexes
                    SearchTaskInfoProvider.CreateMultiTask(SearchTaskTypeEnum.Rebuild, null, null, items, true);

                }
            }

            lblInfo.Text = GetString("srch.index.rebuildstarted");
            lblInfo.Visible = true;
        }
    }

    #endregion


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

