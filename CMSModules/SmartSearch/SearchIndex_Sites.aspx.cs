using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.UIControls;
using CMS.GlobalHelper;
using CMS.SiteProvider;
using CMS.CMSHelper;
using CMS.SettingsProvider;

public partial class CMSModules_SmartSearch_SearchIndex_Sites : SiteManagerPage, IPostBackEventHandler
{
    private int indexId = 0;
    private string currentValues = string.Empty;


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Check read permission
        if (!CMSContext.CurrentUser.IsAuthorizedPerResource("cms.searchindex", CMSAdminControl.PERMISSION_READ))
        {
            RedirectToAccessDenied("cms.searchindex", CMSAdminControl.PERMISSION_READ);
        }

        // Show panel with message how to enable indexing
        if (!SettingsKeyProvider.GetBoolValue("CMSSearchIndexingEnabled"))
        {
            pnlDisabled.Visible = true;
        }

        lblAvialable.Text = GetString("SearchIndex_Sites.Available");

        indexId = QueryHelper.GetInteger("indexid", 0);

        // Get the user sites
        currentValues = GetIndexSites();

        if (!RequestHelper.IsPostBack())
        {
            usSites.Value = currentValues;
        }


        lblInfo.Text = String.Format(GetString("general.changessaved") + " " + GetString("srch.indexrequiresrebuild"), "<a href=\"javascript:" + Page.ClientScript.GetPostBackEventReference(this, "saved") + "\">" + GetString("General.clickhere") + "</a>");

        usSites.OnSelectionChanged += usSites_OnSelectionChanged;
    }


    /// <summary>
    /// Returns string with site ids where user is member.
    /// </summary>    
    private string GetIndexSites()
    {
        DataSet ds = SearchIndexSiteInfoProvider.GetIndexSites("SiteID", "IndexID = " + indexId, null, 0);
        if (!DataHelper.DataSourceIsEmpty(ds))
        {
            return TextHelper.Join(";", SqlHelperClass.GetStringValues(ds.Tables[0], "SiteID"));
        }

        return String.Empty;
    }


    /// <summary>
    /// Handles site selector selection change event.
    /// </summary>
    protected void usSites_OnSelectionChanged(object sender, EventArgs e)
    {
        SaveIndexes();
    }


    /// <summary>
    /// Saves changes in site assignment.
    /// </summary>
    protected void SaveIndexes()
    {
        // Check permissions 
        if (!CMSContext.CurrentUser.IsAuthorizedPerResource("cms.searchindex", CMSAdminControl.PERMISSION_MODIFY))
        {
            RedirectToAccessDenied("cms.searchindex", CMSAdminControl.PERMISSION_MODIFY);
        }

        // Remove old items
        string newValues = ValidationHelper.GetString(usSites.Value, null);
        string items = DataHelper.GetNewItemsInList(newValues, currentValues);
        if (!String.IsNullOrEmpty(items))
        {
            string[] newItems = items.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            if (newItems != null)
            {
                // Add all new items to site
                foreach (string item in newItems)
                {
                    int siteId = ValidationHelper.GetInteger(item, 0);

                    // Unassign site from index
                    SearchIndexSiteInfoProvider.DeleteSearchIndexSiteInfo(indexId, siteId);
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
                // Add all new items to site
                foreach (string item in newItems)
                {
                    int siteId = ValidationHelper.GetInteger(item, 0);

                    // Assign site to index
                    SearchIndexSiteInfoProvider.AddSearchIndexToSite(indexId, siteId);
                }
            }
        }

        // Show saved message with rebuild link
        lblInfo.Visible = true;
    }

    #endregion


    #region "IPostBackEventHandler Members"

    /// <summary>
    /// Handles click on rebuild link (after sites are saved).
    /// </summary>
    /// <param name="eventArgument"></param>
    public void RaisePostBackEvent(string eventArgument)
    {
        if (eventArgument == "saved")
        {
            // Check permissions 
            if (!CMSContext.CurrentUser.IsAuthorizedPerResource("cms.searchindex", CMSAdminControl.PERMISSION_MODIFY))
            {
                RedirectToAccessDenied("cms.searchindex", CMSAdminControl.PERMISSION_MODIFY);
            }

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
