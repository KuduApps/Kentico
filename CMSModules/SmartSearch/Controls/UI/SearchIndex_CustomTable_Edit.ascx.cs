using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.GlobalHelper;
using CMS.FormControls;
using CMS.UIControls;
using CMS.SiteProvider;
using CMS.SettingsProvider;


public partial class CMSModules_SmartSearch_Controls_UI_SearchIndex_CustomTable_Edit : CMSAdminEditControl, IPostBackEventHandler
{
    #region "Variables"

    private string mItemType = null;

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


    protected void Page_Load(object sender, EventArgs e)
    {
        // Show panel with message how to enable indexing
        if (!smartSearchEnabled)
        {
            pnlDisabled.Visible = true;
        }

        // Init controls
        if (!RequestHelper.IsPostBack())
        {
            this.LoadControls();
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
                customTableSelector.Value = sisi.ClassNames;
                txtWhere.TextArea.Text = sisi.WhereCondition;
            }
        }
    }


    /// <summary>
    /// Stores data to database.
    /// </summary>
    protected void btnOK_Click(object sender, EventArgs e)
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
                sisi.ClassNames = Convert.ToString(customTableSelector.Value);
                sisi.WhereCondition = txtWhere.TextArea.Text.Trim();

                DataClassInfo dci = DataClassInfoProvider.GetDataClass(sisi.ClassNames);
                if (dci != null)
                {
                    sisi.SetValue("DisplayName", dci.ClassDisplayName);
                }

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
