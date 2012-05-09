using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.UIControls;
using CMS.GlobalHelper;
using CMS.WebAnalytics;
using CMS.FormControls;
using CMS.CMSHelper;

// Edited object
[EditedObject(AnalyticsObjectType.CAMPAIGN, "campaignId")]
public partial class CMSModules_WebAnalytics_Pages_Tools_Campaign_Tab_Goals : CMSWebAnalyticsPage
{
    #region "Variables"

    protected string mSave = null;

    #endregion


    #region "Methods"

    protected override void OnInit(EventArgs e)
    {
        CampaignInfo ci = EditedObject as CampaignInfo;
        // Validate SiteID for non administrators
        if ((ci != null) && (!CMSContext.CurrentUser.IsGlobalAdministrator))
        {
            if (ci.CampaignSiteID != CMSContext.CurrentSiteID)
            {
                RedirectToAccessDenied(GetString("cmsmessages.accessdenied"));
            }
        }

        if (ci != null)
        {
            if ((ci.CampaignImpressions == 0) || (ci.CampaignTotalCost == 0))
            {
                lblInfo.Text = GetString("campaign.noimpressionsorcost");
                lblInfo.Visible = true;
            }            
        }

        string options = "<item value=\"False\" text=\"" + GetString("campaign.absolutevalue") + "\" /><item value=\"True\" text=\"{0}\" />";

        rbVisitorsPercent.SetValue("repeatdirection", "horizontal");
        rbVisitorsPercent.SetValue("options", String.Format(options, GetString("campaign.percofimpress")));

        rbConversionsPercent.SetValue("repeatdirection", "horizontal");
        rbConversionsPercent.SetValue("options", String.Format(options, GetString("campaign.percofimpress")));

        rbValuePercent.SetValue("repeatdirection", "horizontal");
        rbValuePercent.SetValue("options", String.Format(options, GetString("campaign.percoftotalcost")));

        rbPerVisitorPercent.SetValue("repeatdirection", "horizontal");
        rbPerVisitorPercent.SetValue("options", String.Format(options, GetString("campaign.percofpervisitor")));

        base.OnInit(e);
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        // Set save icon 
        imgSave.ImageUrl = GetImageUrl("CMSModules/CMS_Content/EditMenu/save.png");

        // Save text
        mSave = GetString("general.save");
        EditForm.OnBeforeValidate += new EventHandler(EditForm_OnBeforeValidate);
    }


    void EditForm_OnBeforeValidate(object sender, EventArgs e)
    {
        String errorMsg = String.Empty;

        // Test whether all goal values are higher then the min values
        int visitorsMin = ValidationHelper.GetInteger(((FormEngineUserControl)EditForm.FieldControls["CampaignGoalVisitorsMin"]).Value, 0);
        int visitorsGoal = ValidationHelper.GetInteger(((FormEngineUserControl)EditForm.FieldControls["CampaignGoalVisitors"]).Value, 0);

        int conversionsMin = ValidationHelper.GetInteger(((FormEngineUserControl)EditForm.FieldControls["CampaignGoalConversionsMin"]).Value, 0);
        int conversionsGoal = ValidationHelper.GetInteger(((FormEngineUserControl)EditForm.FieldControls["CampaignGoalConversions"]).Value, 0);

        int goalValueMin = ValidationHelper.GetInteger(((FormEngineUserControl)EditForm.FieldControls["CampaignGoalValueMin"]).Value, 0);
        int goalValue = ValidationHelper.GetInteger(((FormEngineUserControl)EditForm.FieldControls["CampaignGoalValue"]).Value, 0);

        int goalPerVisitorMin = ValidationHelper.GetInteger(((FormEngineUserControl)EditForm.FieldControls["CampaignGoalPerVisitorMin"]).Value, 0);
        int goalPerVisitor = ValidationHelper.GetInteger(((FormEngineUserControl)EditForm.FieldControls["CampaignGoalPerVisitor"]).Value, 0);

        bool visitorsAsPercent = ValidationHelper.GetBoolean(((FormEngineUserControl)EditForm.FieldControls["CampaignGoalVisitorsPercent"]).Value, false);
        bool conversionsAsPercent = ValidationHelper.GetBoolean(((FormEngineUserControl)EditForm.FieldControls["CampaignGoalConversionsPercent"]).Value, false);

        if (visitorsMin > visitorsGoal)
        {
            errorMsg = GetString("campaign.error.goal");
        }

        if (conversionsMin > conversionsGoal)
        {
            errorMsg = GetString("campaign.error.goal");
        }

        if (goalValueMin > goalValue)
        {
            errorMsg = GetString("campaign.error.goal");
        }

        if (goalPerVisitorMin > goalPerVisitor)
        {
            errorMsg = GetString("campaign.error.goal");
        }

        // Percent of impressions may not be higher then 100
        if ((conversionsAsPercent) && (conversionsGoal > 100))
        {
            errorMsg = GetString("campaign.error.goalspercent");
        }

        if ((visitorsAsPercent) && (visitorsGoal > 100))
        {
            errorMsg = GetString("campaign.error.goalspercent");
        }

        // Test zero values
        if (visitorsMin < 0)
        {
            errorMsg = GetString("campaign.valuegreaterzero");
        }

        if (visitorsGoal < 0)
        {
            errorMsg = GetString("campaign.valuegreaterzero");
        }

        if (conversionsMin < 0)
        {
            errorMsg = GetString("campaign.valuegreaterzero");
        }

        if (conversionsGoal < 0)
        {
            errorMsg = GetString("campaign.valuegreaterzero");
        }

        if (goalValueMin < 0)
        {
            errorMsg = GetString("campaign.valuegreaterzero");
        }

        if (goalValue < 0)
        {
            errorMsg = GetString("campaign.valuegreaterzero");
        }

        if (goalPerVisitorMin < 0)
        {
            errorMsg = GetString("campaign.valuegreaterzero");
        }

        if (goalPerVisitor < 0)
        {
            errorMsg = GetString("campaign.valuegreaterzero");
        }

        if (errorMsg != String.Empty)
        {
            EditForm.ErrorLabel.Text = errorMsg;
            EditForm.ErrorLabel.Visible = true;
            EditForm.StopProcessing = true;
        }
    }


    protected override void OnPreRender(EventArgs e)
    {
        // Hide some default error labels  - we want them on specific positions 
        // They destroy design otherwise
        Label lblErr = EditForm.FieldErrorLabels["CampaignConversionsCount"] as Label;
        Label lblVis = EditForm.FieldErrorLabels["CampaignVisitors"] as Label;

        if (lblErr != null)
        {
            lblErr.Visible = false;
        }

        if (lblVis != null)
        {
            lblVis.Visible = false;
        }

        base.OnPreRender(e);
    }


    protected void lnkSave_Click(object sender, EventArgs ea)
    {
        EditForm.SaveData("");
    }

    #endregion
}

