using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.WebAnalytics;
using CMS.ExtendedControls;

public partial class CMSModules_WebAnalytics_Pages_Tools_Campaign_Tab_General : CMSModalPage
{
    #region "Variables"

    private bool modalDialog = false;
    protected string mSave = null;

    // Help variable for set info label of UI form
    private string infoText = String.Empty;

    #endregion


    #region "Methods"

    protected override void OnPreInit(EventArgs e)
    {
        // Checks all permissions for web analytics
        CMSWebAnalyticsPage.CheckAllPermissions();

        string campaignName = QueryHelper.GetString("campaignName", String.Empty);
        int campaignID = QueryHelper.GetInteger("campaignID", 0);
        CampaignInfo ci = null;

        if (campaignName != String.Empty)
        {
            // Try to check dialog mode
            ci = CampaignInfoProvider.GetCampaignInfo(campaignName, CMSContext.CurrentSiteName);
        }

        if ((campaignName != String.Empty) && (ci == null))
        {
            // Set warning text
            infoText = String.Format(GetString("campaign.editedobjectnotexits"), campaignName);
            
            // Create campaign info based on campaign name
            ci = new CampaignInfo();
            ci.CampaignDisplayName = campaignName;
            ci.CampaignName = campaignName;
        }

        if (campaignID != 0)
        {
            ci = CampaignInfoProvider.GetCampaignInfo(campaignID);
        }

        // Validate SiteID for non administrators
        if ((ci != null) && (!CMSContext.CurrentUser.IsGlobalAdministrator))
        {
            if (ci.CampaignSiteID != CMSContext.CurrentSiteID)
            {
                RedirectToAccessDenied(GetString("cmsmessages.accessdenied"));
            }
        }

        modalDialog = QueryHelper.GetBoolean("modalDialog", false);
        if (modalDialog)
        {
            MasterPageFile = "~/CMSMasterPages/UI/Dialogs/ModalDialogPage.master";
            if (ci != null)
            {
                this.CurrentMaster.Title.TitleText = GetString("analytics.campaign");
                this.CurrentMaster.Title.TitleImage = GetImageUrl("Objects/Analytics_Campaign/object.png");
            }
            else
            {
                this.CurrentMaster.Title.TitleText = GetString("campaign.campaign.new");
                this.CurrentMaster.Title.TitleImage = GetImageUrl("Objects/Analytics_Campaign/new.png");
            }
            SetDialogButtons();
        }

        if (ci != null)
        {
            EditedObject = ci;
        }

        CurrentMaster.Title.HelpTopicName = "campaign_general";
        CurrentMaster.Title.HelpName = "helpTopic";

        base.OnPreInit(e);
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        // Set save icon 
        imgSave.ImageUrl = GetImageUrl("CMSModules/CMS_Content/EditMenu/save.png");

        // Save text
        mSave = GetString("general.save");

        // Hide default UI Form submit button
        editElem.UIFormControl.SubmitButton.Visible = false;

        // Set proper css style for modal dialog
        if (modalDialog)
        {
            pnlActions.Visible = false;
            pnlContent.CssClass = "PageContent";
        }
        
        // Set info label text created in preinit
        if (infoText != String.Empty)
        {
            editElem.UIFormControl.InfoLabel.Text = infoText;
        }
    }


    protected void lnkSave_Click(object sender, EventArgs ea)
    {
        editElem.Save(false);
    }


    private void UpdateUniSelector(bool closeOnSave)
    {
        string selector = QueryHelper.GetString("selectorid", string.Empty);
        CampaignInfo info = editElem.UIFormControl.EditedObject as CampaignInfo;
        if (!string.IsNullOrEmpty(selector) && (info != null))
        {
            // Add selector refresh
            string script =
                string.Format(@"var wopener = window.top.opener ? window.top.opener : window.top.dialogArguments;
		                            if (wopener) {{ wopener.US_SelectNewValue_{0}('{1}'); }}", selector, info.CampaignName);

            if (closeOnSave)
            {
                script += "window.top.close();";
            }

            ScriptHelper.RegisterStartupScript(this, GetType(), "UpdateSelector", script, true);
        }
    }


    protected override void OnPreRender(EventArgs e)
    {
        if (QueryHelper.GetBoolean("saved", false) && !URLHelper.IsPostback())
        {
            UpdateUniSelector(false);
        }

        base.OnPreRender(e);
    }


    private void SetDialogButtons()
    {
        CurrentMaster.PanelFooter.CssClass = "FloatRight";

        // Add save button
        LocalizedButton btnSave = new LocalizedButton
        {
            ID = "btnSave",
            ResourceString = "general.save",
            EnableViewState = false,
            CssClass = "SubmitButton"
        };
        btnSave.Click += (sender, e) => editElem.Save(true);
        mCurrentMaster.PanelFooter.Controls.Add(btnSave);

        // Add save & close button
        LocalizedButton btnSaveAndCancel = new LocalizedButton
        {
            ID = "btnSaveCancel",
            ResourceString = "general.saveandclose",
            EnableViewState = false,
            CssClass = "LongSubmitButton"
        };
        btnSaveAndCancel.Click += (sender, e) =>
        {
            if (editElem.Save(false))
            {
                // Register script for save value to uniselector and close the window
                UpdateUniSelector(true);
            }
        };
        mCurrentMaster.PanelFooter.Controls.Add(btnSaveAndCancel);

        // Add close button every time
        mCurrentMaster.PanelFooter.Controls.Add(new LocalizedButton
        {
            ID = "btnCancel",
            ResourceString = "general.close",
            EnableViewState = false,
            OnClientClick = "window.top.close(); return false;",
            CssClass = "SubmitButton"
        });
    }

    #endregion
}