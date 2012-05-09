using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.WebAnalytics;
using CMS.FormControls;

public partial class CMSModules_WebAnalytics_Pages_Tools_Campaign_New : CMSWebAnalyticsPage
{
    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        EditForm.OnBeforeSave += new EventHandler(EditForm_OnBeforeSave);
        EditForm.OnAfterValidate += new EventHandler(EditForm_OnAfterValidate);
        EditForm.RedirectUrlAfterCreate = "Frameset.aspx?campaignid={%EditedObject.ID%}&saved=1"
            + (QueryHelper.GetBoolean("displayreport", false) ? "&displayreport=true" : "");

        CurrentMaster.Title.HelpTopicName = "campaign_general";
        CurrentMaster.Title.HelpName = "helpTopic";

        String listUrl = "~/CMSModules/WebAnalytics/Pages/Tools/Campaign/List.aspx";
        if (QueryHelper.GetBoolean("DisplayReport", false))
        {
            listUrl += "?displayreport=true";
        }

        // Prepare the breadcrumbs
        string[,] breadcrumbs = new string[2, 3];
        breadcrumbs[0, 0] = GetString("campaign.campaign.list");
        breadcrumbs[0, 1] = listUrl;
        breadcrumbs[1, 0] = GetString("campaign.campaign.new");

        CurrentMaster.Title.Breadcrumbs = breadcrumbs;
    }


    void EditForm_OnBeforeSave(object sender, EventArgs e)
    {
        CampaignInfo ci = (CampaignInfo)EditForm.EditedObject;
        ci.CampaignSiteID = CMSContext.CurrentSiteID;
    }


    void EditForm_OnAfterValidate(object sender, EventArgs e)
    {
        // Validate ToDate > FromDate
        FormEngineUserControl fromField = EditForm.FieldControls["CampaignOpenFrom"] as FormEngineUserControl;
        FormEngineUserControl toField = EditForm.FieldControls["CampaignOpenTo"] as FormEngineUserControl;

        DateTime from = DateTimeHelper.ZERO_TIME;
        DateTime to = DateTimeHelper.ZERO_TIME;

        if (fromField != null)
        {
            from = ValidationHelper.GetDateTime(fromField.Value, DateTimeHelper.ZERO_TIME);
        }

        if (toField != null)
        {
            to = ValidationHelper.GetDateTime(toField.Value, DateTimeHelper.ZERO_TIME);
        }

        if ((from != DateTimeHelper.ZERO_TIME) && (to != DateTimeHelper.ZERO_TIME) && (from > to))
        {
            EditForm.StopProcessing = true;
            // Disable default error label
            EditForm.ErrorLabel = null;

            // Show specific label for this error
            lblError.Visible = true;
            lblError.ResourceString = "campaign.wronginterval";
        }
    }

    #endregion
}
