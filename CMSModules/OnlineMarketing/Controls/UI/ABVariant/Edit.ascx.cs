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

public partial class CMSModules_OnlineMarketing_Controls_UI_ABVariant_Edit : CMSAdminEditControl
{
    #region "Variables"

    private ABVariantInfo mVariantObj = null;
    private int mVariantId = 0;
    private int mTestID;

    #endregion


    #region "Properties"

    /// <summary>
    /// Parent test ID.
    /// </summary>
    public int TestID
    {
        get
        {
            return mTestID;
        }
        set
        {
            mTestID = value;
        }
    }

    /// <summary>
    /// Variant data.
    /// </summary>
    public ABVariantInfo VariantObj
    {
        get
        {
            if (mVariantObj == null)
            {
                mVariantObj = ABVariantInfoProvider.GetABVariantInfo(this.ABVariantID);
            }

            return mVariantObj;
        }
        set
        {
            mVariantObj = value;
            if (value != null)
            {
                mVariantId = value.ABVariantID;
            }
            else
            {
                mVariantId = 0;
            }
        }
    }


    /// <summary>
    /// Variant ID.
    /// </summary>
    public int ABVariantID
    {
        get
        {
            return mVariantId;
        }
        set
        {
            mVariantId = value;
            mVariantObj = null;
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

        SetupControls();

        // Set edited object
        if (this.ABVariantID > 0)
        {
            EditedObject = VariantObj;
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

        // Validators
        this.rfvABVariantName.ErrorMessage = GetString("general.requirescodename");
        this.rfvABVariantDisplayName.ErrorMessage = GetString("general.requiresdisplayname");

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
        if (this.VariantObj != null)
        {
            this.txtABVariantDisplayName.Text = this.VariantObj.ABVariantDisplayName;
            this.txtABVariantName.Text = this.VariantObj.ABVariantName;
            this.ucPath.Value = this.VariantObj.ABVariantPath;
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
            // Ensure the info object
            if (this.VariantObj == null)
            {
                this.VariantObj = new ABVariantInfo();
                this.VariantObj.ABVariantTestID = TestID;
                this.VariantObj.ABVariantSiteID = CMSContext.CurrentSiteID;
            }

            // Initialize object
            this.VariantObj.ABVariantDisplayName = this.txtABVariantDisplayName.Text.Trim();
            this.VariantObj.ABVariantName = this.txtABVariantName.Text.Trim();
            this.VariantObj.ABVariantPath = this.ucPath.Value.ToString().Trim();            
            
            // Save object data to database
            ABVariantInfoProvider.SetABVariantInfo(this.VariantObj);

            this.ItemID = this.VariantObj.ABVariantID;
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
        // Validate required fields
        string errorMessage = new Validator()
            .NotEmpty(this.txtABVariantDisplayName.Text.Trim(), this.rfvABVariantDisplayName.ErrorMessage)
            .NotEmpty(this.txtABVariantName.Text.Trim(), this.rfvABVariantName.ErrorMessage)
            .IsCodeName(this.txtABVariantName.Text.Trim(), GetString("general.invalidcodename")).Result;

        string abTestName = string.Empty ;
        string siteName = CMSContext.CurrentSiteName ;

        // Get AB test info object
        ABTestInfo abti = ABTestInfoProvider.GetABTestInfo(TestID) ;
        if(abti != null)
        {
            abTestName = abti.ABTestName ;
        }

        // Test unique codename
        ABVariantInfo info = ABVariantInfoProvider.GetABVariantInfo(txtABVariantName.Text.Trim(), abTestName, siteName);
        if ((info != null) && ((this.VariantObj == null) || (info.ABVariantID != this.VariantObj.ABVariantID)))
        {
            errorMessage = GetString("general.codenameexists");
        }

        if (String.IsNullOrEmpty(ucPath.Value.ToString()))
        {
            errorMessage = GetString("abtesting.entertestpage");
        }
   
        // Set the error message
        if (!String.IsNullOrEmpty(errorMessage))
        {
            this.lblError.Text = errorMessage;         
            return false;
        }

        return true;
    }

    #endregion
}