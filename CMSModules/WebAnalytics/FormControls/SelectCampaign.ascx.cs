using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.FormControls;
using CMS.UIControls;
using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.WebAnalytics;
using CMS.SiteProvider;
using CMS.LicenseProvider;
using CMS.SettingsProvider;

public partial class CMSModules_WebAnalytics_FormControls_SelectCampaign : FormEngineUserControl
{
    #region "Variables"

    private bool mPostbackOnChange = false;
    private int mSiteID = CMSContext.CurrentSiteID;
    private bool mCreateOnUnknownName = true;

    #endregion


    #region "Properties"

    /// <summary>
    /// Gets or sets the value of campaign.
    /// </summary>
    public override object Value
    {
        get
        {
            return usCampaign.Value;
        }
        set
        {
            usCampaign.Value = value;
        }
    }


    /// <summary>
    /// Return uniselector control.
    /// </summary>
    public UniSelector UniSelector
    {
        get
        {
            return usCampaign;
        }
    }


    /// <summary>
    /// Gets or sets site ID.
    /// </summary>
    public int SiteID
    {
        get
        {
            return mSiteID;
        }
        set
        {
            mSiteID = value;
        }
    }


    /// <summary>
    /// If true, full postback is raised when item changed.
    /// </summary>
    public bool PostbackOnChange
    {
        get
        {
            return mPostbackOnChange;
        }
        set
        {
            mPostbackOnChange = value;
        }
    }


    /// <summary>
    /// Selection mode of control (dropdown,multiselect...).
    /// </summary>
    public SelectionModeEnum SelectionMode
    {
        get
        {
            return usCampaign.SelectionMode;
        }
        set
        {
            usCampaign.SelectionMode = value;
        }
    }


    /// <summary>
    /// If true, allow all is enabled.
    /// </summary>
    public bool AllowAll
    {
        get
        {
            return usCampaign.AllowAll;
        }
        set
        {
            usCampaign.AllowAll = value;
        }
    }


    /// <summary>
    /// Gets or sets AllowEmpty value of uniselector.
    /// </summary>
    public bool AllowEmpty
    {
        get
        {
            return usCampaign.AllowEmpty;
        }
        set
        {
            usCampaign.AllowEmpty = value;
        }
    }


    /// <summary>
    /// Value for all record item.
    /// </summary>
    public string AllRecordValue
    {
        get
        {
            return usCampaign.AllRecordValue;
        }
    }


    /// <summary>
    /// Gets or sets value for (none) record.
    /// </summary>
    public string NoneRecordValue
    {
        set
        {
            usCampaign.NoneRecordValue = value;
        }
        get
        {
            return usCampaign.NoneRecordValue;
        }
    }


    /// <summary>
    /// Indicates whether selector should try to create a new row if unknown selected or not.
    /// </summary>
    public bool CreateOnUnknownName
    {
        get
        {
            return mCreateOnUnknownName;
        }
        set
        {
            mCreateOnUnknownName = value;
        }
    }

    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        usCampaign.IsLiveSite = IsLiveSite;
        usCampaign.AllowEditTextBox = true;

        if ((SelectionMode == SelectionModeEnum.SingleTextBox) && CMSContext.CurrentUser.IsAuthorizedPerResource("CMS.WebAnalytics", "managecampaigns"))
        {
            usCampaign.EditDialogWindowWidth = 580;
            usCampaign.EditDialogWindowHeight = 620;

            string url = "~/CMSModules/WebAnalytics/Pages/Tools/Campaign/Tab_General.aspx?campaignName=##ITEMID##&modaldialog=true";
            this.usCampaign.EditItemPageUrl = url;

            url = "~/CMSModules/WebAnalytics/Pages/Tools/Campaign/Tab_General.aspx?modaldialog=true";
            this.usCampaign.NewItemPageUrl = url;
        }

        if (PostbackOnChange)
        {
            usCampaign.DropDownSingleSelect.AutoPostBack = true;
            ScriptManager scr = ScriptManager.GetCurrent(Page);
            scr.RegisterPostBackControl(usCampaign);
        }

        usCampaign.WhereCondition = "CampaignSiteID = " + this.SiteID;
        if (!RequestHelper.IsPostBack())
        {
            ReloadData();
        }
    }


    /// <summary>
    /// Reloads uniselector.
    /// </summary>
    public void ReloadData()
    {
        usCampaign.WhereCondition = "CampaignSiteID = " + this.SiteID;
        usCampaign.Reload(true);
    }


    /// <summary>
    /// Test if campaign is valid.
    /// </summary>    
    public override bool IsValid()
    {
        String value = ValidationHelper.GetString(usCampaign.Value, String.Empty).Trim();
        if (value != String.Empty)
        {
            String domain = URLHelper.GetCurrentDomain();
            if (DataHelper.GetNotEmpty(domain, "") != "")
            {
                string parsedDomain = LicenseKeyInfoProvider.ParseDomainName(domain);
                if (!LicenseKeyInfoProvider.IsFeatureAvailable(parsedDomain, FeatureEnum.CampaignAndConversions))
                {
                    ValidationError = GetString("campaignnselector.nolicence");
                    return false;
                }
            }

            if (!ValidationHelper.IsCodeName(value))
            {
                this.ValidationError = GetString("campaign.validcodename");
                return false;
            }

            // If campaign object not found
            CampaignInfo ci = CampaignInfoProvider.GetCampaignInfo(value, SiteInfoProvider.GetSiteName(this.SiteID));

            // Handle info not found
            if (ci == null)
            {
                if (!mCreateOnUnknownName)
                {
                    // Selector is not set to create new campaigns so error out
                    this.ValidationError = GetString("campaign.validcodename");
                    return false;
                }

                // Or check permissions ..
                if (!CMSContext.CurrentUser.IsAuthorizedPerResource("CMS.WebAnalytics", "ManageCampaigns"))
                {
                    this.ValidationError = GetString("campaign.notallowedcreate");
                    return false;
                }

                // .. and try to create a new one.
                ci = new CampaignInfo();
                ci.CampaignName = value;
                ci.CampaignDisplayName = value;
                ci.CampaignEnabled = true;
                ci.CampaignSiteID = this.SiteID;

                CampaignInfoProvider.SetCampaignInfo(ci);
            }
        }

        return true;
    }
}
