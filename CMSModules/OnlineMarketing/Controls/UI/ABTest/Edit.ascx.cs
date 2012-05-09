using System;
using System.Data;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.OnlineMarketing;
using CMS.SettingsProvider;
using CMS.WebAnalytics;

public partial class CMSModules_OnlineMarketing_Controls_UI_AbTest_Edit : CMSAdminEditControl
{
    #region "Variables"

    private ABTestInfo mAbTestObj = null;
    private int mAbTestId = 0;
    private string mAliasPath = String.Empty;
    private bool mShowAliasPath = true;

    #endregion


    #region "Properties"

    /// <summary>
    /// Ab test data.
    /// </summary>
    public ABTestInfo AbTestObj
    {
        get
        {
            if (mAbTestObj == null)
            {
                mAbTestObj = ABTestInfoProvider.GetABTestInfo(this.ABTestID);
            }

            return mAbTestObj;
        }
        set
        {
            mAbTestObj = value;
            if (value != null)
            {
                mAbTestId = value.ABTestID;
            }
            else
            {
                mAbTestId = 0;
            }
        }
    }


    /// <summary>
    /// If true show alias path selector.
    /// </summary>
    public bool ShowAliasPath
    {
        get
        {
            // If alias path is set dont show this selector
            if (AliasPath != String.Empty)
            {
                return false;
            }
            return mShowAliasPath;
        }
        set
        {
            mShowAliasPath = value;
        }
    }


    /// <summary>
    /// Alias path of document to which this abtest belongs.
    /// </summary>
    public string AliasPath
    {
        get
        {
            return mAliasPath;
        }
        set
        {
            mAliasPath = value;
        }
    }


    /// <summary>
    /// Ab test ID.
    /// </summary>
    public int ABTestID
    {
        get
        {
            return mAbTestId;
        }
        set
        {
            mAbTestId = value;
            mAbTestObj = null;
        }
    }

    #endregion


    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.StopProcessing)
        {
            return;
        }

        if (!ShowAliasPath)
        {
            plcOriginalPage.Visible = false;
        }

        SetupControls();

        // Set edited object
        if (this.ABTestID > 0)
        {
            EditedObject = AbTestObj;
        }
        else
        {
            // Hide conversions textbox for new ABTests
            pnlConversions.Visible = false;
        }

        // Disabled field - load not only on postback
        if (this.AbTestObj != null)
        {
            this.txtABTestConversions.Text = this.AbTestObj.ABTestConversions.ToString();
        }

        // Empty textbox for zero values
        if (this.txtABTestMaxConversions.Text.Trim() == "0")
        {
            this.txtABTestMaxConversions.Text = String.Empty;
        }

        // Load the form data
        if (!URLHelper.IsPostback())
        {
            LoadData();
        }
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        // Show or hide messages
        this.lblError.Visible = !string.IsNullOrEmpty(this.lblError.Text);
        this.lblInfo.Visible = !string.IsNullOrEmpty(this.lblInfo.Text);

        string status = string.Empty;

        // Set status
        if (AbTestObj != null)
        {
            if (!AbTestObj.ABTestEnabled)
            {
                status = "<span class=\"StatusDisabled\">" + GetString("general.disabled") + "</span>";
            }
            else
            {
                if (ABTestInfoProvider.ABTestIsRunning(AbTestObj))
                {
                    // Display disabled information
                    if (!AnalyticsHelper.AnalyticsEnabled(CMSContext.CurrentSiteName)
                        || !AnalyticsHelper.TrackConversionsEnabled(CMSContext.CurrentSiteName))
                    {
                        status = GetString("abtesting.statusNone");
                    }
                    else
                    {
                        if (!ABTestInfoProvider.ContainsVariants(AbTestObj))
                        {
                            // Display warning when the test does not contain any variant
                            status = "<img src=\"" + GetImageUrl("Design/Controls/UniGrid/Actions/Warning.png") + "\" alt=\"" + GetString("abtest.novariants") + "\" title=\""
                                + GetString("abtest.novariants") + "\" />&nbsp;&nbsp;"
                                + GetString("abtesting.statusNone");
                        }
                        else
                        {
                            status = "<span class=\"StatusEnabled\">" + GetString("abtesting.status" + ABTestInfoProvider.GetABTestStatus(AbTestObj)) + "</span>";
                        }
                    }
                }
                else
                {
                    status = GetString("abtesting.status" + ABTestInfoProvider.GetABTestStatus(AbTestObj));
                }
            }
        }
        else
        {
            status = GetString("general.none");
        }

        ltrStatusValue.Text = status;
    }


    protected void btnOk_Click(object sender, EventArgs e)
    {
        // Validate and save the data
        Process();
    }

    #endregion


    #region "Private methods"

    /// <summary>
    /// Initializes form controls.
    /// </summary>
    private void SetupControls()
    {
        // Button
        btnOk.Text = GetString("general.ok");

        // Path selector
        ucPath.SiteID = CMSContext.CurrentSiteID;

        // Validators
        this.rfvABTestName.ErrorMessage = GetString("general.requirescodename");
        this.rfvABTestDisplayName.ErrorMessage = GetString("general.requiresdisplayname");
        this.rfvABTestMaxConversions.MaximumValue = Int32.MaxValue.ToString();
        this.rfvABTestMaxConversions.MinimumValue = "0";
        this.rfvABTestMaxConversions.ErrorMessage = String.Format(GetString("general.outofrange"), this.rfvABTestMaxConversions.MinimumValue, this.rfvABTestMaxConversions.MaximumValue);

        // Display 'Changes were saved' message if required
        if (QueryHelper.GetBoolean("saved", false))
        {
            this.lblInfo.Text = GetString("general.changessaved");
        }
    }


    /// <summary>
    /// Loads the data into the form.
    /// </summary>
    private void LoadData()
    {
        // Load the form from the info object
        if (this.AbTestObj != null)
        {
            this.txtABTestName.Text = this.AbTestObj.ABTestName;
            this.txtABTestDisplayName.Text = this.AbTestObj.ABTestDisplayName;
            this.txtABTestDescription.Text = this.AbTestObj.ABTestDescription;

            // Show empty textbox for zero value
            if (AbTestObj.ABTestMaxConversions != 0)
            {
                this.txtABTestMaxConversions.Text = this.AbTestObj.ABTestMaxConversions.ToString();
            }
            this.dtpABTestOpenFrom.SelectedDateTime = this.AbTestObj.ABTestOpenFrom;
            this.dtpABTestOpenTo.SelectedDateTime = this.AbTestObj.ABTestOpenTo;
            this.ucPath.Value = this.AbTestObj.ABTestOriginalPage;
            this.chkABTestEnabled.Checked = this.AbTestObj.ABTestEnabled;
            this.ucCultureSelector.Value = this.AbTestObj.ABTestCulture;

            switch (this.AbTestObj.ABTestTargetConversionType)
            {
                case TargetConversionType.Total:
                    radTotal.Checked = true;
                    break;

                case TargetConversionType.AnyVariant:
                    radAnyVariant.Checked = true;
                    break;

                default:
                    radTotal.Checked = true;
                    break;
            }
        }
    }


    /// <summary>
    // Processes the form - saves the data.
    /// </summary>
    private void Process()
    {
        // Check manage permissions
        if (!CheckPermissions("CMS.ABTest", CMSAdminControl.PERMISSION_MANAGE))
        {
            return;
        }

        // Validate the form
        if (Validate())
        {
            bool isNew = false;

            // Ensure the info object
            if (this.AbTestObj == null)
            {
                this.AbTestObj = new ABTestInfo();
                if (AliasPath != String.Empty)
                {
                    this.AbTestObj.ABTestOriginalPage = AliasPath;
                }

                isNew = true;
            }

            // Initialize object
            String newName = this.txtABTestName.Text.Trim();
            this.AbTestObj.ABTestDisplayName = this.txtABTestDisplayName.Text.Trim();
            this.AbTestObj.ABTestDescription = this.txtABTestDescription.Text.Trim();
            this.AbTestObj.ABTestMaxConversions = ValidationHelper.GetInteger(this.txtABTestMaxConversions.Text.Trim(), 0);
            this.AbTestObj.ABTestOpenFrom = this.dtpABTestOpenFrom.SelectedDateTime;
            this.AbTestObj.ABTestOpenTo = this.dtpABTestOpenTo.SelectedDateTime;
            this.AbTestObj.ABTestCulture = this.ucCultureSelector.Value.ToString();
            this.AbTestObj.ABTestEnabled = this.chkABTestEnabled.Checked;
            this.AbTestObj.ABTestSiteID = CMSContext.CurrentSiteID;

            // Name has changed. Change analytics statistics data for existing object
            if ((this.AbTestObj.ABTestID != 0) && (this.AbTestObj.ABTestName != newName))
            {
                ABTestInfoProvider.RenameABTestStatistics(this.AbTestObj.ABTestName, newName, CMSContext.CurrentSiteID);
            }
            this.AbTestObj.ABTestName = newName;

            // Store conversion type
            TargetConversionType conversionType = TargetConversionType.Total;
            if (radAnyVariant.Checked)
            {
                conversionType = TargetConversionType.AnyVariant;
            }
            this.AbTestObj.ABTestTargetConversionType = conversionType;

            if (ShowAliasPath)
            {
                this.AbTestObj.ABTestOriginalPage = this.ucPath.Value.ToString().Trim();
            }

            // Save object data to database
            ABTestInfoProvider.SetABTestInfo(this.AbTestObj);

            this.ItemID = this.AbTestObj.ABTestID;

            // For new A/B test create default variant
            if (isNew)
            {
                // Create instance of ab variant
                ABVariantInfo variant = new ABVariantInfo();

                // Set properties
                variant.ABVariantPath = this.AbTestObj.ABTestOriginalPage;
                variant.ABVariantTestID = this.AbTestObj.ABTestID;
                variant.ABVariantDisplayName = this.AbTestObj.ABTestDisplayName + " " + GetString("om.variant");
                variant.ABVariantName = this.AbTestObj.ABTestName;
                variant.ABVariantSiteID = this.AbTestObj.ABTestSiteID;

                // Save to the storage
                ABVariantInfoProvider.SetABVariantInfo(variant);
            }

            this.RaiseOnSaved();

            // Set the info message
            this.lblInfo.Text = GetString("general.changessaved");

        }
    }


    /// <summary>
    /// Validates the form. If validation succeeds returns true, otherwise returns false.
    /// </summary>
    private bool Validate()
    {
        string codename = this.txtABTestName.Text.Trim();

        // Validate required fields
        string errorMessage = new Validator()
            .NotEmpty(txtABTestDisplayName.Text.Trim(), this.rfvABTestDisplayName.ErrorMessage)
            .NotEmpty(codename, this.rfvABTestName.ErrorMessage)
            .IsCodeName(codename, GetString("general.invalidcodename")).Result;

        if (!dtpABTestOpenFrom.IsValidRange() || !dtpABTestOpenTo.IsValidRange())
        {
            errorMessage = GetString("general.errorinvaliddatetimerange");
        }

        if ((dtpABTestOpenFrom.SelectedDateTime != DateTimeHelper.ZERO_TIME)
            && (dtpABTestOpenTo.SelectedDateTime != DateTimeHelper.ZERO_TIME)
            && (dtpABTestOpenFrom.SelectedDateTime > dtpABTestOpenTo.SelectedDateTime))
        {
            errorMessage = GetString("om.wrongtimeinterval");
        }

        string maxConversions = txtABTestMaxConversions.Text.Trim();
        if (!String.IsNullOrEmpty(maxConversions) && (String.IsNullOrEmpty(errorMessage)))
        {
            errorMessage = new Validator().IsInteger(maxConversions, GetString("om.targetconversionrequiresinteger")).IsPositiveNumber(maxConversions, GetString("om.targetconversionrequiresinteger")).Result;
        }

        // Check the uniqueness of the codename
        ABTestInfo info = ABTestInfoProvider.GetABTestInfo(txtABTestName.Text.Trim(), CMSContext.CurrentSiteName);
        if ((info != null) && ((this.AbTestObj == null) || (info.ABTestID != this.AbTestObj.ABTestID)))
        {
            errorMessage = GetString("general.codenameexists");
        }

        if (String.IsNullOrEmpty(ucPath.Value.ToString()) && (AliasPath == String.Empty))
        {
            errorMessage = GetString("abtesting.enteroriginalpage");
        }

        // Test if there is no enabled test for same page
        if (chkABTestEnabled.Checked && TestToValidate())
        {
            QueryDataParameters parameters = null;
            string testPage = ((AliasPath != String.Empty) && !ShowAliasPath) ? AliasPath : ucPath.Value.ToString();
            string where = ABTestInfoProvider.GetRunningCondition(ABTestID, testPage, CMSContext.CurrentSiteID, ucCultureSelector.Value.ToString(), dtpABTestOpenFrom.SelectedDateTime, dtpABTestOpenTo.SelectedDateTime, out parameters);
            DataSet ds = ABTestInfoProvider.GetABTests(where, null, -1, null, parameters);
            if (!DataHelper.DataSourceIsEmpty(ds))
            {
                errorMessage = GetString("om.twotestsonepageerror");
            }
        }

        // Set the error message
        if (!String.IsNullOrEmpty(errorMessage))
        {
            this.lblError.Text = errorMessage;
            return false;
        }

        return true;
    }


    /// <summary>
    /// Returns true, if test should be validated for test page
    /// </summary>
    private bool TestToValidate()
    {
        if (chkABTestEnabled.Checked)
        {
            int max = ValidationHelper.GetInteger(this.txtABTestMaxConversions.Text.Trim(), 0);
            int conversions = (AbTestObj != null) ? AbTestObj.ABTestConversions : 0;
            if ((max == 0) || (max > conversions))
            {
                DateTime dtTo = ValidationHelper.GetDateTime(this.dtpABTestOpenTo.SelectedDateTime, DateTimeHelper.ZERO_TIME);
                if ((dtTo == DateTimeHelper.ZERO_TIME) || (dtTo > DateTime.Now))
                {
                    return true;
                }
            }
        }

        return false;
    }

    #endregion
}