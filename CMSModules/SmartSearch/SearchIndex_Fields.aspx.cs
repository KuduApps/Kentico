using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.UIControls;
using CMS.GlobalHelper;
using CMS.SiteProvider;
using CMS.SettingsProvider;
using CMS.ExtendedControls;

public partial class CMSModules_SmartSearch_SearchIndex_Fields : SiteManagerPage, IPostBackEventHandler
{
    #region "Variables"

    private bool smartSearchEnabled = SettingsKeyProvider.GetBoolValue("CMSSearchIndexingEnabled");

    #endregion


    #region "Public properties"

    /// <summary>
    /// Gets or sets the IndexID.
    /// </summary>
    public int IndexID
    {
        get
        {
            return ValidationHelper.GetInteger(ViewState["IndexID"], 0);
        }
        set
        {
            ViewState["IndexID"] = value;
        }
    }

    #endregion


    #region "Page methods"

    /// <summary>
    /// Handles the Load event of the Page control.
    /// </summary>
    protected void Page_Load(object sender, EventArgs e)
    {
        int indexId = QueryHelper.GetInteger("indexid", 0);
        IndexID = 0;

        // Get the IndexInfo in order to obtain the ClassID which the index contains.
        SearchIndexInfo sii = SearchIndexInfoProvider.GetSearchIndexInfo(indexId);
        if ((sii != null) && (sii.IndexSettings != null))
        {
            // Get the index settings
            Dictionary<Guid, SearchIndexSettingsInfo> settingsItems = sii.IndexSettings.Items;
            if (settingsItems.ContainsKey(SearchHelper.SIMPLE_ITEM_ID))
            {
                // Get the ClassInfo and set the ClassID to the searchFields control
                string className = settingsItems[SearchHelper.SIMPLE_ITEM_ID].ClassNames.ToLower(SqlHelperClass.EnglishCulture);
                DataClassInfo classInfo = DataClassInfoProvider.GetDataClass(className);
                if (classInfo != null)
                {
                    IndexID = sii.IndexID;
                    searchFields.ItemID = classInfo.ClassID;
                }
            }
            else
            {
                // ClassName not defined yet, display a message directing the user to the Index tab
                lblInfo.ResourceString = "srch.index.required";
                lblInfo.Visible = true;
                searchFields.Visible = false;
            }
        }

        // Setup the searchFields control
        searchFields.LoadActualValues = true;
        searchFields.OnSaved += new EventHandler(searchFields_OnSaved);
    }


    protected void searchFields_OnSaved(object sender, EventArgs e)
    {
        if (smartSearchEnabled)
        {
            searchFields.RebuildIndexResourceString = " " + String.Format(GetString("srch.indexrequiresrebuild"), "<a href=\"javascript:" + ControlsHelper.GetPostBackEventReference(this, "saved") + "\">" + GetString("General.clickhere") + "</a>");
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
            if (IndexID > 0)
            {
                sii = SearchIndexInfoProvider.GetSearchIndexInfo(IndexID);
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
