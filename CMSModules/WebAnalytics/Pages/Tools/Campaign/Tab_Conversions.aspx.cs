using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using CMS.UIControls;
using CMS.SiteProvider;
using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.SettingsProvider;
using CMS.WebAnalytics;

[EditedObject(AnalyticsObjectType.CAMPAIGN, "campaignId")]

public partial class CMSModules_WebAnalytics_Pages_Tools_Campaign_Tab_Conversions : CMSWebAnalyticsPage
{
    #region "Variables"

    private string currentValues = string.Empty;
    private int campaignID = 0;
    private CampaignInfo ci = null;

    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        ci = EditedObject as CampaignInfo;
        if (!RequestHelper.IsPostBack())
        {
            if (ci != null)
            {
                rbAllConversions.Checked = ci.CampaignUseAllConversions;
                rbSelectedConversions.Checked = !ci.CampaignUseAllConversions;
            }
        }

        // Validate SiteID for non administrators
        if ((ci != null) && (!CMSContext.CurrentUser.IsGlobalAdministrator))
        {
            if (ci.CampaignSiteID != CMSContext.CurrentSiteID)
            {
                RedirectToAccessDenied(GetString("cmsmessages.accessdenied"));
            }
        }

        plcTable.Visible = !rbAllConversions.Checked;

        lblAvialable.Text = GetString("campaign.availableconversions");
        campaignID = QueryHelper.GetInteger("campaignid", 0);

        // Get the conversions
        currentValues = GetConversions();

        if (!RequestHelper.IsPostBack())
        {
            usConversions.Value = currentValues;
        }
        
        usConversions.WhereCondition = "ConversionSiteID = " + CMSContext.CurrentSiteID;
        usConversions.OnSelectionChanged += usConversions_OnSelectionChanged;
        rbAllConversions.CheckedChanged += ConversionsSelection_changed;
        rbSelectedConversions.CheckedChanged += ConversionsSelection_changed;
    }


    protected void ConversionsSelection_changed(object sender, EventArgs ea)
    {
        // Check "modify" permission
        if (!CMSContext.CurrentUser.IsAuthorizedPerResource("CMS.WebAnalytics", "ManageCampaigns"))
        {
            RedirectToAccessDenied("CMS.WebAnalytics", "Manage campaigns");
        }

        if (ci != null)
        {
            ci.CampaignUseAllConversions = rbAllConversions.Checked;
            if (ci.CampaignUseAllConversions)
            {
                CampaignInfoProvider.RemoveAllConversionsFromCampaign(campaignID);
            }

            CampaignInfoProvider.SetCampaignInfo(ci);

            lblInfo.Visible = true;
            lblInfo.Text = GetString("General.ChangesSaved");
        }
    }


    /// <summary>
    /// Returns string with conversions ids
    /// </summary>    
    private string GetConversions()
    {
        DataSet ds = ConversionCampaignInfoProvider.GetConversionCampaigns("CampaignID = " + campaignID, null, 0, "ConversionID");
        if (!DataHelper.DataSourceIsEmpty(ds))
        {
            return TextHelper.Join(";", SqlHelperClass.GetStringValues(ds.Tables[0], "ConversionID"));
        }

        return String.Empty;
    }


    protected void usConversions_OnSelectionChanged(object sender, EventArgs e)
    {
        SaveConversions();
    }


    /// <summary>
    /// Saves the changes in conversions
    /// </summary>
    protected void SaveConversions()
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
        string newValues = ValidationHelper.GetString(usConversions.Value, null);
        string items = DataHelper.GetNewItemsInList(newValues, currentValues);
        if (!String.IsNullOrEmpty(items))
        {
            string[] newItems = items.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            if (newItems != null)
            {
                // Add all new items to site
                foreach (string item in newItems)
                {
                    int conversionID = ValidationHelper.GetInteger(item, 0);

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
                    int conversionID = ValidationHelper.GetInteger(item, 0);
                    ConversionCampaignInfoProvider.AddConversionToCampaign(conversionID, campaignID);
                }

                // If some of sites could not be assigned reload selector value
                if (falseValues)
                {
                    usConversions.Value = GetConversions();
                    usConversions.Reload(true);
                }
            }
        }

        lblInfo.Visible = true;
        lblInfo.Text = GetString("General.ChangesSaved");
    }
}

