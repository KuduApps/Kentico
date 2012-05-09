using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Data;

using CMS.UIControls;
using CMS.GlobalHelper;
using CMS.SiteProvider;
using CMS.CMSHelper;
using CMS.SettingsProvider;

public partial class CMSModules_SmartSearch_Controls_UI_SearchIndex_Cultures : CMSAdminListControl, IPostBackEventHandler
{
    #region "Variables"

    private int indexId = 0;
    private string currentValues = "";

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Show panel with message how to enable indexing
        if (!SettingsKeyProvider.GetBoolValue("CMSSearchIndexingEnabled"))
        {
            pnlDisabled.Visible = true;
        }

        indexId = QueryHelper.GetInteger("indexid", 0);

        // Add sites filter
        uniSelector.FilterControl = "~/CMSFormControls/Filters/SiteFilter.ascx";
        uniSelector.SetValue("DefaultFilterValue", CMSContext.CurrentSiteID);
        uniSelector.SetValue("FilterMode", "cultures");
        
        // Get the active sites
        DataSet ds = SearchIndexCultureInfoProvider.GetSearchIndexCultures("IndexID = " + indexId, null, 0, "IndexID, IndexCultureID");
        if (!DataHelper.DataSourceIsEmpty(ds))
        {
            currentValues = TextHelper.Join(";", SqlHelperClass.GetStringValues(ds.Tables[0], "IndexCultureID"));
        }

        if (!URLHelper.IsPostback())
        {
            uniSelector.Value = currentValues;
        }

        lblInfo.Text = String.Format(GetString("general.changessaved") + " " + GetString("srch.indexrequiresrebuild"), "<a href=\"javascript:" + Page.ClientScript.GetPostBackEventReference(this, "saved") + "\">" + GetString("General.clickhere") + "</a>");
        lblInfo.Visible = false;
    }


    protected void uniSelector_OnSelectionChanged(object sender, EventArgs e)
    {
        // Remove old items
        string newValues = ValidationHelper.GetString(uniSelector.Value, null);
        string items = DataHelper.GetNewItemsInList(newValues, currentValues);
        if (!String.IsNullOrEmpty(items))
        {
            string[] newItems = items.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            if (newItems != null)
            {
                // Remove all new items
                foreach (string item in newItems)
                {
                    int cultureId = ValidationHelper.GetInteger(item, 0);
                    SearchIndexCultureInfo sici = SearchIndexCultureInfoProvider.GetSearchIndexCultureInfo(indexId, cultureId);
                    SearchIndexCultureInfoProvider.DeleteSearchIndexCultureInfo(sici);
                }
            }
        }

        // Add new items
        items = DataHelper.GetNewItemsInList(currentValues, newValues);
        if (!String.IsNullOrEmpty(items))
        {
            string[] newItems = items.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            if (newItems != null)
            {
                // Add all new items
                foreach (string item in newItems)
                {
                    int cultureId = ValidationHelper.GetInteger(item, 0);
                    SearchIndexCultureInfoProvider.AddSearchIndexCulture(indexId, cultureId);
                }
            }
        }

        // Show saved message with rebuild link
        lblInfo.Visible = true;
    }

    #endregion


    #region "IPostBackEventHandler Members"

    public void RaisePostBackEvent(string eventArgument)
    {
        if (eventArgument == "saved")
        {
            // Get search index info
            SearchIndexInfo sii = null;
            if (this.indexId > 0)
            {
                sii = SearchIndexInfoProvider.GetSearchIndexInfo(this.indexId);
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
