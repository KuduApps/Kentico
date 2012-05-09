using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using CMS.GlobalHelper;
using CMS.FormControls;
using CMS.UIControls;
using CMS.SiteProvider;
using CMS.CMSHelper;
using CMS.SettingsProvider;

public partial class CMSModules_SmartSearch_Controls_UI_SearchIndex_Forum_Edit : CMSAdminEditControl, IPostBackEventHandler
{
    #region "Variables"

    private string mItemType = null;
    FormEngineUserControl selForum = null;
    private bool smartSearchEnabled = SettingsKeyProvider.GetBoolValue("CMSSearchIndexingEnabled");

    #endregion


    #region "Public properties"

    /// <summary>
    /// Gets or sets item type.
    /// </summary>
    public string ItemType
    {
        get
        {
            return mItemType;
        }
        set
        {
            mItemType = value;
        }
    }

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Show panel with message how to enable indexing
        if (!smartSearchEnabled)
        {
            pnlDisabled.Visible = true;
        }

        // Module forums is not available
        if (!(ModuleEntry.IsModuleRegistered(ModuleEntry.FORUMS) && ModuleEntry.IsModuleLoaded(ModuleEntry.FORUMS)))
        {
            return;
        }

        selForum = this.LoadControl("~/CMSModules/Forums/FormControls/ForumSelector.ascx") as FormEngineUserControl;
        if (selForum != null)
        {
            selForum.IsLiveSite = false;
            plcForumSelector.Controls.Add(selForum);
        }

        this.selSite.AllowAll = false;
        this.selSite.UseCodeNameForSelection = true;

        string siteWhere = String.Empty;

        DataSet ds = SearchIndexSiteInfoProvider.GetIndexSites("SiteID", "IndexID = " + ItemID, null, 0);
        if (!DataHelper.DataSourceIsEmpty(ds))
        {
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                siteWhere += dr["SiteID"] + ",";
            }

            siteWhere = "," + siteWhere;

            // Preselect current site if it is assigned to index
            if (!RequestHelper.IsPostBack() && siteWhere.Contains("," + CMSContext.CurrentSiteID + ","))
            {
                this.selSite.Value = CMSContext.CurrentSiteName;
            }

            siteWhere = siteWhere.Trim(',');
            siteWhere = "SiteID IN (" + siteWhere + ")";
            this.selSite.UniSelector.WhereCondition = siteWhere;
        }
        else
        {
            selSite.Enabled = false;
            selForum.Enabled = false;
            btnOk.Enabled = false;

            lblInfo.Visible = true;
            lblInfo.Text = GetString("srch.index.nositeselected");
        }

        // Set default vaules for forum selector
        this.selForum.SetValue("selectionmode", SelectionModeEnum.MultipleTextBox);
        this.selForum.SetValue("DisplayAdHocOption", false);
        this.selForum.SetValue("SiteName", Convert.ToString(selSite.Value));

        // Set events and default values for site selector
        this.selSite.UniSelector.OnSelectionChanged += new EventHandler(UniSelector_OnSelectionChanged);
        this.selSite.DropDownSingleSelect.AutoPostBack = true;

        this.LoadControls();

        if (ItemType == SearchIndexSettingsInfo.TYPE_ALLOWED)
        {
            this.selSite.AllowAll = true;
        }

        // Init controls
        if (!RequestHelper.IsPostBack())
        {
            selForum.Enabled = true;

            string siteName = ValidationHelper.GetString(selSite.Value, String.Empty);
            if (String.IsNullOrEmpty(siteName) || (siteName == "-1"))
            {
                selForum.Enabled = false;
            }
            else
            {
                selForum.SetValue("SiteName", siteName);
                SetControlsStatus(false);
            }
        }
    }


    void UniSelector_OnSelectionChanged(object sender, EventArgs e)
    {
        SetControlsStatus(true);
    }


    /// <summary>
    /// Enable or disable controls with dependece on current settings.
    /// </summary>
    /// <param name="clear">Indicates whether selector value should be cleared</param>
    protected void SetControlsStatus(bool clear)
    {
        if (clear)
        {
            selForum.Value = String.Empty;
        }

        selForum.Enabled = true;

        string siteName = ValidationHelper.GetString(selSite.Value, String.Empty);
        if (String.IsNullOrEmpty(siteName) || (siteName == "-1"))
        {
            selForum.Enabled = false;
        }
        else
        {
            selForum.SetValue("SiteName", siteName);
        }
    }


    /// <summary>
    /// Resets all boxes.
    /// </summary>
    public void LoadControls()
    {
        SearchIndexInfo sii = SearchIndexInfoProvider.GetSearchIndexInfo(this.ItemID);

        // If we are editing existing search index
        if (sii != null)
        {
            SearchIndexSettings sis = new SearchIndexSettings();
            sis.LoadData(sii.IndexSettings.GetData());
            SearchIndexSettingsInfo sisi = sis.GetSearchIndexSettingsInfo(this.ItemGUID);
            if (sisi != null)
            {
                if (!RequestHelper.IsPostBack())
                {
                    selSite.Value = sisi.SiteName;
                    selForum.SetValue("SiteName", sisi.SiteName);
                    selForum.Value = sisi.ForumNames;
                }
                this.ItemType = sisi.Type;
            }
        }

        plcForumsInfo.Visible = true;
        if (this.ItemType == SearchIndexSettingsInfo.TYPE_EXLUDED)
        {
            plcForumsInfo.Visible = false;
        }
    }

    #endregion


    #region "Events"

    /// <summary>
    /// Stores data to database.
    /// </summary>
    protected void btnOK_Click(object sender, EventArgs e)
    {
        // Perform validation
        string errorMessage = new Validator().NotEmpty(selSite.Value, GetString("srch.err.emptysite")).Result;

        if (String.IsNullOrEmpty(errorMessage) && (this.ItemType == SearchIndexSettingsInfo.TYPE_EXLUDED) && String.IsNullOrEmpty(Convert.ToString(selForum.Value)))
        {
            errorMessage = GetString("srch.err.emptyforum");
        }

        if (String.IsNullOrEmpty(errorMessage))
        {
            SearchIndexInfo sii = SearchIndexInfoProvider.GetSearchIndexInfo(this.ItemID);
            if (sii != null)
            {
                SearchIndexSettings sis = sii.IndexSettings;
                SearchIndexSettingsInfo sisi;

                // If we are updating existing Search Index Settings Info
                if (this.ItemGUID != Guid.Empty)
                {
                    sisi = sis.GetSearchIndexSettingsInfo(this.ItemGUID);

                }
                // If we are creating new Search Index Settings Info
                else
                {
                    sisi = new SearchIndexSettingsInfo();
                    sisi.ID = Guid.NewGuid();
                    sisi.Type = this.ItemType;
                }

                // Save values
                if (sisi != null)
                {
                    string siteName = selSite.Value.ToString();
                    if (siteName == "-1")
                    {
                        siteName = String.Empty;
                    }

                    sisi.SiteName = siteName;
                    sisi.ForumNames = selForum.Value.ToString();

                    // Update settings item
                    sis.SetSearchIndexSettingsInfo(sisi);
                    // Update xml value
                    sii.IndexSettings = sis;
                    SearchIndexInfoProvider.SetSearchIndexInfo(sii);
                    this.ItemGUID = sisi.ID;

                    // Redirect to edit mode
                    lblInfo.Visible = true;
                    lblInfo.Text = GetString("general.changessaved");
                    if (smartSearchEnabled)
                    {
                        lblInfo.Text += " " + String.Format(GetString("srch.indexrequiresrebuild"), "<a href=\"javascript:" + Page.ClientScript.GetPostBackEventReference(this, "saved") + "\">" + GetString("General.clickhere") + "</a>");
                    }
                }
                // Error loading SearchIndexSettingsInfo
                else
                {
                    lblError.Text = GetString("srch.err.loadingsisi");
                    lblError.Visible = true;
                }
            }
            // Error loading SearchIndexInfo
            else
            {
                lblError.Text = GetString("srch.err.loadingsii");
                lblError.Visible = true;
            }
        }
        else
        {
            lblError.Text = errorMessage;
            lblError.Visible = true;
        }
    }

    #endregion


    #region "IPostBackEventHandler Members"

    public void RaisePostBackEvent(string eventArgument)
    {
        if (eventArgument == "saved")
        {
            // Get search index info
            SearchIndexInfo sii = null;
            if (this.ItemID > 0)
            {
                sii = SearchIndexInfoProvider.GetSearchIndexInfo(this.ItemID);
            }

            // Create rebuild task
            if (sii != null)
            {
                SearchTaskInfoProvider.CreateTask(SearchTaskTypeEnum.Rebuild, sii.IndexType, null, sii.IndexName);
            }

            lblInfo.Text = GetString("srch.index.rebuildstarted");
            lblInfo.Visible = true;
        }
    }

    #endregion
}
