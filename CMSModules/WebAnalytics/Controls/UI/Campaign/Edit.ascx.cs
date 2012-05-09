using System;
using System.Data;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.CMSHelper;
using CMS.FormControls;
using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.WebAnalytics;

public partial class CMSModules_WebAnalytics_Controls_UI_Campaign_Edit : CMSAdminEditControl
{
    #region "Variables"

    String formerCodeName = String.Empty;

    #endregion


    #region "Properties"

    /// <summary>
    /// UIForm control used for editing objects properties.
    /// </summary>
    public UIForm UIFormControl
    {
        get
        {
            return this.EditForm;
        }
    }


    /// <summary>
    /// Indicates if the control should perform the operations.
    /// </summary>
    public override bool StopProcessing
    {
        get
        {
            return base.StopProcessing;
        }
        set
        {
            base.StopProcessing = value;
            this.EditForm.StopProcessing = value;
        }
    }


    /// <summary>
    /// Indicates if the control is used on the live site.
    /// </summary>
    public override bool IsLiveSite
    {
        get
        {
            return base.IsLiveSite;
        }
        set
        {
            base.IsLiveSite = value;
            EditForm.IsLiveSite = value;
        }
    }

    #endregion


    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        pnlAdvanced.GroupingText = GetString("campaign.advanced");
        pnlBasic.GroupingText = GetString("campaign.basic");

        // For new items (via dialog) - enable set true
        CampaignInfo ci = EditForm.EditedObject as CampaignInfo;
        if (((ci == null) || (ci.CampaignID == 0)) && !URLHelper.IsPostback())
        {
            chkEnabled.Value = true;
        }

        EditForm.OnAfterValidate += new EventHandler(EditForm_OnAfterValidate);

        if (QueryHelper.GetBoolean("modaldialog", false))
        {
            EditForm.SubmitButton.Visible = false;
            EditForm.RedirectUrlAfterCreate = "";
        }

        EditForm.OnBeforeSave += new EventHandler(EditForm_OnBeforeSave);
        EditForm.OnAfterSave += new EventHandler(EditForm_OnAfterSave);
    }


    void EditForm_OnAfterSave(object sender, EventArgs e)
    {
        CampaignInfo ci = EditForm.EditedObject as CampaignInfo;
        // If code name has changed (on existing object) => Rename all analytics statistics data.
        if ((ci != null) && (ci.CampaignName != formerCodeName) && (formerCodeName != String.Empty))
        {
            CampaignInfoProvider.RenameCampaignStatistics(formerCodeName, ci.CampaignName, CMSContext.CurrentSiteID);
        }
    }


    void EditForm_OnBeforeSave(object sender, EventArgs e)
    {
        CampaignInfo ci = EditForm.EditedObject as CampaignInfo;
        if (ci != null)
        {
            if (ci.CampaignSiteID == 0)
            {
                ci.CampaignSiteID = CMSContext.CurrentSiteID;
            }

            // Init goals values for new item
            if (ci.CampaignID == 0)
            {
                ci.CampaignGoalValuePercent = false;
                ci.CampaignGoalVisitorsPercent = false;
                ci.CampaignGoalPerVisitorPercent = false;
                ci.CampaignGoalConversionsPercent = false;
            }

            // Save old codename
            formerCodeName = ci.CampaignName;
        }
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


    /// <summary>
    /// Saves the data
    /// </summary>
    /// <param name="redirect">If true, use server redirect after successfull save</param>
    public bool Save(bool redirect)
    {
        string selectorID = QueryHelper.GetString("selectorID", String.Empty);

        bool ret = EditForm.SaveData("");

        // If saved - redirect with campaign ID parameter
        if ((ret) && (redirect))
        {
            CampaignInfo ci = (CampaignInfo)EditForm.EditedObject;
            if (ci != null)
            {
                URLHelper.Redirect("tab_general.aspx?campaignid=" + ci.CampaignID + "&saved=1&modaldialog=true&selectorID=" + selectorID);
            }
        }

        return ret;
    }

    #endregion
}

