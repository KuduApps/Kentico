using System;
using System.Data;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.SiteProvider;

public partial class CMSModules_Membership_Controls_Membership_Edit : CMSAdminEditControl
{
    #region "Variables"

    private MembershipInfo mMembershipObj = null;
    private int mMembershipId = 0;
    private int mSiteID = 0;

    #endregion


    #region "Properties"

    /// <summary>
    /// Membership data.
    /// </summary>
    public MembershipInfo MembershipObj
    {
        get
        {
            if (mMembershipObj == null)
            {
                mMembershipObj = MembershipInfoProvider.GetMembershipInfo(this.MembershipID);
            }

            return mMembershipObj;
        }
        set
        {
            mMembershipObj = value;
            if (value != null)
            {
                mMembershipId = value.MembershipID;
            }
            else
            {
                mMembershipId = 0;
            }
        }
    }


    /// <summary>
    /// SiteID filter - if 0 global membership is used.
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
    /// Membership ID.
    /// </summary>
    public int MembershipID
    {
        get
        {
            return mMembershipId;
        }
        set
        {
            mMembershipId = value;
            mMembershipObj = null;
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
        if (this.MembershipID > 0)
        {
            EditedObject = (MembershipInfo)MembershipObj;
        }

        // Load the form data
        if (!URLHelper.IsPostback())
        {
            ReloadData();
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
        this.rfvMembershipCodeName.ErrorMessage = GetString("general.requirescodename");
        this.rfvMembershipName.ErrorMessage = GetString("general.requiresdisplayname");

        // Display 'Changes were saved' message if required
        if (QueryHelper.GetBoolean("saved", false))
        {
            this.lblInfo.Text = GetString("general.changessaved");
        }
    }


    /// <summary>
    /// Loads the data into the form.
    /// </summary>
    public override void ReloadData()
    {
        // Load the form from the info object
        if (this.MembershipObj != null)
        {
            this.txtMembershipName.Text = this.MembershipObj.MembershipDisplayName;
            this.txtMembershipCodeName.Text = this.MembershipObj.MembershipName;
            this.txtMembershipDescription.Text = this.MembershipObj.MembershipDescription;
        }
    }


    /// <summary>
    // Processes the form - saves the data.
    /// </summary>
    private void Process()
    {
        // Check "modify" permission
        if (!CheckPermissions("CMS.Membership", PERMISSION_MODIFY))
        {
            return;
        }

        // Ensure the info object
        if (this.MembershipObj == null)
        {
            this.MembershipObj = new MembershipInfo();
            this.MembershipObj.MembershipSiteID = SiteID;
        }

        if (this.MembershipObj.MembershipDisplayName != this.txtMembershipName.Text.Trim())
        {
            // Refresh a breadcrumb if used in the tabs layout
            ScriptHelper.RefreshTabHeader(Page, string.Empty);
        }

        // Initialize object
        this.MembershipObj.MembershipDisplayName = this.txtMembershipName.Text.Trim();
        this.MembershipObj.MembershipName = this.txtMembershipCodeName.Text.Trim();
        this.MembershipObj.MembershipDescription = this.txtMembershipDescription.Text.Trim();

        // Validate the form
        if (Validate())
        {
            // Save object data to database
            MembershipInfoProvider.SetMembershipInfo(this.MembershipObj);

            this.ItemID = this.MembershipObj.MembershipID;
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
            .NotEmpty(this.txtMembershipCodeName.Text.Trim(), this.rfvMembershipCodeName.ErrorMessage)
            .NotEmpty(this.txtMembershipName.Text.Trim(), this.rfvMembershipName.ErrorMessage)
            .IsCodeName(this.txtMembershipCodeName.Text.Trim(), GetString("general.invalidcodename")).Result;

        MembershipObj.Generalized.CheckUnique = false;
        MembershipObj.Generalized.ValidateCodeName = false;

        // If not unique codename dont save
        if (!this.MembershipObj.CheckUniqueCodeName())
        {
            lblError.Visible = true;
            lblError.Text = String.Format(GetString("membership.notuniquecodename"), this.MembershipObj.MembershipName);

            return false;
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

