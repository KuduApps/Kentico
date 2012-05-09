using System;
using System.Data;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.OnlineMarketing;
using CMS.SettingsProvider;

public partial class CMSModules_OnlineMarketing_Controls_UI_MVTest_Edit : CMSAdminEditControl
{
    #region "Variables"

    private MVTestInfo mMvtestObj = null;
    private int mMvtestId = 0;
    private string mAliasPath = string.Empty;
    private bool mShowAliasPath = true;
    private int siteId = CMSContext.CurrentSiteID;

    #endregion


    #region "Properties"

    /// <summary>
    /// Mvtest data.
    /// </summary>
    public MVTestInfo MvtestObj
    {
        get
        {
            if (mMvtestObj == null)
            {
                mMvtestObj = MVTestInfoProvider.GetMVTestInfo(this.MVTestID);
            }

            return mMvtestObj;
        }
        set
        {
            mMvtestObj = value;
            if (value != null)
            {
                mMvtestId = value.MVTestID;
            }
            else
            {
                mMvtestId = 0;
            }
        }
    }


    /// <summary>
    /// Mvtest ID.
    /// </summary>
    public int MVTestID
    {
        get
        {
            return mMvtestId;
        }
        set
        {
            mMvtestId = value;
            mMvtestObj = null;
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
    /// Gets or sets the alias path.
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
            plcMVTestPage.Visible = false;
        }

        SetupControls();

        // Set edited object
        if (this.MVTestID > 0)
        {
            EditedObject = MvtestObj;
        }
        else
        {
            pnlConversions.Visible = false;
        }

        // Disabled field - load not only on postback
        if (this.MvtestObj != null)
        {
            this.txtMVTestConversions.Text = this.MvtestObj.MVTestConversions.ToString();
        }

        // Empty textbox for zero values
        if (this.txtMVTestMaxConversions.Text.Trim() == "0")
        {
            this.txtMVTestMaxConversions.Text = String.Empty;
        }

        // Load the form data
        if (!URLHelper.IsPostback())
        {
            LoadData();
        }
    }


    /// <summary>
    /// PreRender event handler.
    /// </summary>
    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        // Show or hide messages
        this.lblError.Visible = !string.IsNullOrEmpty(this.lblError.Text);
        this.lblInfo.Visible = !string.IsNullOrEmpty(this.lblInfo.Text);

        // Set status
        this.lblStatusValue.ResourceString = "mvtest.status.";

        // Disabled by default
        MVTestStatusEnum status = MVTestStatusEnum.Disabled;
        if (MvtestObj != null)
        {
            // Set correct status has been
            status = MVTestInfoProvider.GetMVTestStatus(MvtestObj);
        }

        // Set the current status resource string
        this.lblStatusValue.ResourceString += status;

        switch (status)
        {
            case MVTestStatusEnum.Running:
                this.lblStatusValue.CssClass = "StatusEnabled";
                break;

            case MVTestStatusEnum.Disabled:
                this.lblStatusValue.CssClass = "StatusDisabled";
                break;
        }
    }


    /// <summary>
    /// Handles the Click event of the btnOk control.
    /// </summary>
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
        ucPath.SiteID = siteId;

        // Validators
        this.rfvMVTestCodeName.ErrorMessage = GetString("general.requirescodename");
        this.rfvMVTestDisplayName.ErrorMessage = GetString("general.requiresdisplayname");
        this.rfvMVTestMaxConversions.MaximumValue = Int32.MaxValue.ToString();
        this.rfvMVTestMaxConversions.MinimumValue = "0";
        this.rfvMVTestMaxConversions.ErrorMessage = String.Format(GetString("general.outofrange"), this.rfvMVTestMaxConversions.MinimumValue, this.rfvMVTestMaxConversions.MaximumValue);

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
        if (this.MvtestObj != null)
        {
            this.txtMVTestCodeName.Text = this.MvtestObj.MVTestName;
            this.txtMVTestDisplayName.Text = this.MvtestObj.MVTestDisplayName;
            this.txtMVTestDescription.Text = this.MvtestObj.MVTestDescription;
            this.ucPath.Value = this.MvtestObj.MVTestPage;
            this.ucCultureSelector.Value = this.MvtestObj.MVTestCulture;

            // For 0 - empty textbox
            if (MvtestObj.MVTestMaxConversions != 0)
            {
                this.txtMVTestMaxConversions.Text = this.MvtestObj.MVTestMaxConversions.ToString();
            }

            this.dtpMVTestOpenFrom.SelectedDateTime = this.MvtestObj.MVTestOpenFrom;
            this.dtpMVTestOpenTo.SelectedDateTime = this.MvtestObj.MVTestOpenTo;
            this.chkMVTestEnabled.Checked = this.MvtestObj.MVTestEnabled;

            switch (this.MvtestObj.MVTestTargetConversionType)
            {
                case MVTTargetConversionTypeEnum.AnyCombination:
                    radAnyVariant.Checked = true;
                    break;

                case MVTTargetConversionTypeEnum.Total:
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
        if (CheckPermissions("CMS.MVTest", CMSAdminControl.PERMISSION_MANAGE))
        {
            // Validate the form
            if (Validate())
            {
                // Ensure the info object
                if (this.MvtestObj == null)
                {
                    this.MvtestObj = new MVTestInfo();
                    this.MvtestObj.MVTestSiteID = siteId;
                    this.MvtestObj.MVTestPage = this.AliasPath;
                }

                // Initialize object
                String newCodeName = this.txtMVTestCodeName.Text.Trim();
                this.MvtestObj.MVTestDisplayName = this.txtMVTestDisplayName.Text.Trim();
                this.MvtestObj.MVTestDescription = this.txtMVTestDescription.Text;
                this.MvtestObj.OriginalCulture = this.MvtestObj.MVTestCulture;
                this.MvtestObj.MVTestCulture = this.ucCultureSelector.Value.ToString();
                this.MvtestObj.MVTestMaxConversions = ValidationHelper.GetInteger(this.txtMVTestMaxConversions.Text, 0);
                this.MvtestObj.MVTestOpenFrom = this.dtpMVTestOpenFrom.SelectedDateTime;
                this.MvtestObj.MVTestOpenTo = this.dtpMVTestOpenTo.SelectedDateTime;
                this.MvtestObj.MVTestEnabled = chkMVTestEnabled.Checked;

                // Name has changed. Change analytics statistics data for existing object
                if ((this.MvtestObj.MVTestID != 0) && (this.MvtestObj.MVTestName != newCodeName))
                {
                    MVTestInfoProvider.RenameMVTestStatistics(this.MvtestObj.MVTestName, newCodeName, CMSContext.CurrentSiteID);
                }
                this.MvtestObj.MVTestName = newCodeName;

                if (radTotal.Checked)
                {
                    this.MvtestObj.MVTestTargetConversionType = MVTTargetConversionTypeEnum.Total;
                }
                else if (radAnyVariant.Checked)
                {
                    this.MvtestObj.MVTestTargetConversionType = MVTTargetConversionTypeEnum.AnyCombination;
                }

                if (ShowAliasPath)
                {
                    this.MvtestObj.MVTestPage = this.ucPath.Value.ToString().Trim();
                }

                // Save object data to database
                MVTestInfoProvider.SetMVTestInfo(this.MvtestObj);

                this.ItemID = this.MvtestObj.MVTestID;
                this.RaiseOnSaved();

                // Set the info message
                this.lblInfo.Text = GetString("general.changessaved");
            }
        }
    }


    /// <summary>
    /// Validates the form. If validation succeeds returns true, otherwise returns false.
    /// </summary>
    private bool Validate()
    {
        string codename = this.txtMVTestCodeName.Text.Trim();

        // Validate required fields
        string errorMessage = new Validator()
            .NotEmpty(txtMVTestDisplayName.Text.Trim(), this.rfvMVTestDisplayName.ErrorMessage)
            .NotEmpty(codename, this.rfvMVTestCodeName.ErrorMessage)
            .IsCodeName(codename, GetString("general.invalidcodename")).Result;

        // Prepare the properties required for validation (codename + siteID)
        MVTestInfo tempMvtTest = new MVTestInfo();
        tempMvtTest.MVTestName = codename;
        tempMvtTest.MVTestSiteID = siteId;
        if (MvtestObj != null)
        {
            tempMvtTest.MVTestID = MvtestObj.MVTestID;
        }

        // Check the uniqueness of the codename
        if (!tempMvtTest.CheckUniqueCodeName())
        {
            errorMessage = GetString("general.codenameexists");
        }

        if (!dtpMVTestOpenFrom.IsValidRange() || !dtpMVTestOpenTo.IsValidRange())
        {
            errorMessage = GetString("general.errorinvaliddatetimerange");
        }

        if ((dtpMVTestOpenFrom.SelectedDateTime != DateTimeHelper.ZERO_TIME)
            && (dtpMVTestOpenTo.SelectedDateTime != DateTimeHelper.ZERO_TIME)
            && (dtpMVTestOpenFrom.SelectedDateTime > dtpMVTestOpenTo.SelectedDateTime))
        {
            errorMessage = GetString("om.wrongtimeinterval");
        }

        string currentConversions = txtMVTestConversions.Text.Trim();
        string maxConversions = txtMVTestMaxConversions.Text.Trim();
        if (!String.IsNullOrEmpty(currentConversions) && (String.IsNullOrEmpty(errorMessage)))
        {
            errorMessage = new Validator().IsInteger(currentConversions, GetString("om.currentconversionrequiresinteger")).IsPositiveNumber(currentConversions, GetString("om.currentconversionrequiresinteger")).Result;
        }

        if (!String.IsNullOrEmpty(maxConversions) && (String.IsNullOrEmpty(errorMessage)))
        {
            errorMessage = new Validator().IsInteger(maxConversions, GetString("om.targetconversionrequiresinteger")).IsPositiveNumber(maxConversions, GetString("om.targetconversionrequiresinteger")).Result;
        }


        if (string.IsNullOrEmpty(this.ucPath.Value.ToString().Trim()) && (AliasPath == String.Empty))
        {
            errorMessage = GetString("mvtest.pagerequired");
        }

        // Test if there is no enabled test for same page
        if (chkMVTestEnabled.Checked && TestToValidate())
        {
            QueryDataParameters parameters = null;
            string testPage = ((AliasPath != String.Empty) && !ShowAliasPath) ? AliasPath : ucPath.Value.ToString();
            string where = MVTestInfoProvider.GetRunningCondition(MVTestID, testPage, siteId, ucCultureSelector.Value.ToString(), dtpMVTestOpenFrom.SelectedDateTime, dtpMVTestOpenTo.SelectedDateTime, out parameters);
            DataSet ds = MVTestInfoProvider.GetMVTests(where, null, parameters);
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
        if (chkMVTestEnabled.Checked)
        {
            int max = ValidationHelper.GetInteger(this.txtMVTestMaxConversions.Text.Trim(), 0);
            int conversions = (MvtestObj != null) ? MvtestObj.MVTestConversions : 0;
            if ((max == 0) || (max > conversions))
            {
                DateTime dtTo = ValidationHelper.GetDateTime(this.dtpMVTestOpenTo.SelectedDateTime, DateTimeHelper.ZERO_TIME);
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

