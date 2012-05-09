using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using CMS.UIControls;
using CMS.GlobalHelper;
using CMS.WebAnalytics;
using CMS.SettingsProvider;
using CMS.CMSHelper;
using CMS.SiteProvider;

public partial class CMSModules_WebAnalytics_Pages_Tools_Conversion_Tab_Campaigns : CMSWebAnalyticsPage
{
    #region "Variables"

    private string currentValues = string.Empty;
    private int conversionID = 0;

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        lblAvialable.Text = GetString("conversion.avaiblecampaign");
        conversionID = QueryHelper.GetInteger("campaignid", 0);

        ConversionInfo ci = ConversionInfoProvider.GetConversionInfo(conversionID);
        if (ci != null)
        {
            int siteID = CMSContext.CurrentSiteID;
            if (ci.ConversionSiteID != siteID)
            {
                if (!CMSContext.CurrentUser.IsInSite(SiteInfoProvider.GetSiteName(ci.ConversionSiteID)))
                {
                    RedirectToAccessDenied(GetString("conversion.currentsite"));
                    return;
                }
            }
        }

        // Get the conversions
        currentValues = GetConversions();

        if (!RequestHelper.IsPostBack())
        {
            usCampaigns.Value = currentValues;
        }

        usCampaigns.WhereCondition = "CampaignSiteID = " + CMSContext.CurrentSiteID;
        usCampaigns.OnSelectionChanged += usConversions_OnSelectionChanged;
    }


    /// <summary>
    /// Returns string with campaign ids.
    /// </summary>    
    private string GetConversions()
    {
        DataSet ds = ConversionCampaignInfoProvider.GetConversionCampaigns("ConversionID = " + conversionID, null, 0, "CampaignID");
        if (!DataHelper.DataSourceIsEmpty(ds))
        {
            return TextHelper.Join(";", SqlHelperClass.GetStringValues(ds.Tables[0], "CampaignID"));
        }

        return String.Empty;
    }


    protected void usConversions_OnSelectionChanged(object sender, EventArgs e)
    {
        SaveCampaigns();
    }


    /// <summary>
    /// Saves the selected (removed) campaigns
    /// </summary>
    protected void SaveCampaigns()
    {
        // Check permissions
        if (!CMSContext.CurrentUser.IsAuthorizedPerResource("CMS.WebAnalytics", "ManageConversions"))
        {
            RedirectToAccessDenied("CMS.WebAnalytics", "Manage conversions");
        }

        if (!CMSContext.CurrentUser.IsAuthorizedPerResource("CMS.WebAnalytics", "ManageCampaigns"))
        {
            RedirectToAccessDenied("CMS.WebAnalytics", "Manage campaigns");
        }

        // Remove old items
        string newValues = ValidationHelper.GetString(usCampaigns.Value, null);
        string items = DataHelper.GetNewItemsInList(newValues, currentValues);
        if (!String.IsNullOrEmpty(items))
        {
            string[] newItems = items.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            if (newItems != null)
            {
                // Add all new items to site
                foreach (string item in newItems)
                {
                    int campaignID = ValidationHelper.GetInteger(item, 0);

                    // remove conversion
                    ConversionCampaignInfoProvider.RemoveConversionFromCampaign(conversionID, campaignID);
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
                bool falseValues = false;

                // Add all new items to site
                foreach (string item in newItems)
                {
                    int campaignID = ValidationHelper.GetInteger(item, 0);
                    ConversionCampaignInfoProvider.AddConversionToCampaign(conversionID, campaignID);
                }

                // If some of sites could not be assigned reload selector value
                if (falseValues)
                {
                    usCampaigns.Value = GetConversions();
                    usCampaigns.Reload(true);
                }
            }
        }

        lblInfo.Visible = true;
        lblInfo.Text = GetString("General.ChangesSaved");
    }

    #endregion
}

