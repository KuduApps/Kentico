using System;
using System.Data;

using CMS.GlobalHelper;
using CMS.Forums;
using CMS.CMSHelper;
using CMS.UIControls;

public partial class CMSModules_Forums_Controls_Subscriptions_SubscriptionEdit : CMSAdminEditControl
{
    #region "Variables"

    private int mForumId;
    private int mSubscriptionId;
    private ForumSubscriptionInfo mSubscriptionObj = null;

    #endregion


    #region "Public properties"

    /// <summary>
    /// Gets or sets the ID of the forum to edit.
    /// </summary>
    public int ForumID
    {
        get
        {
            return this.mForumId;
        }
        set
        {
            this.mForumId = value;
        }
    }


    /// <summary>
    /// Gets or sets the ID of the subscription to edit.
    /// </summary>
    public int SubscriptionID
    {
        get
        {
            return this.mSubscriptionId;
        }
        set
        {
            this.mSubscriptionId = value;
        }
    }


    /// <summary>
    /// Returns if checkbox "Send confirmation email" is checked or not.
    /// </summary>
    public bool SendEmailConfirmation
    {
        get
        {
            return this.chkSendConfirmationEmail.Checked;
        }
    }

    #endregion


    #region "Private properties"

    /// <summary>
    /// Gets the subscription obj.
    /// </summary>
    protected ForumSubscriptionInfo SubscriptionObj
    {
        get
        {
            if (mSubscriptionObj == null)
            {
                mSubscriptionObj = ForumSubscriptionInfoProvider.GetForumSubscriptionInfo(this.SubscriptionID);
            }

            return mSubscriptionObj;
        }
        set
        {
            mSubscriptionObj = value;
        }
    }

    #endregion


    #region "Page methods"

    /// <summary>
    /// Handles the Load event of the Page control.
    /// </summary>
    protected void Page_Load(object sender, EventArgs e)
    {
        // Get query keys
        bool saved = QueryHelper.GetBoolean("saved", false);
        bool chkChecked = QueryHelper.GetBoolean("checked", true);

        // Get resource strings        
        this.rfvEmail.ErrorMessage = GetString("ForumSubscription_Edit.emailErrorMsg");
        this.btnOk.Text = GetString("General.OK");

        // Check whether the forum still exists
        if ((ForumID > 0) && (ForumInfoProvider.GetForumInfo(ForumID) == null))
        {
            RedirectToInformation("editedobject.notexists");
        }

        // Set edited object
        if (SubscriptionID > 0)
        {
            EditedObject = SubscriptionObj;
        }

        bool process = true;
        if (!this.Visible || StopProcessing)
        {
            this.EnableViewState = false;
            process = false;
        }

        this.rfvSubscriptionEmail.ErrorMessage = GetString("ForumSubscription_Edit.EnterSomeEmail");
        this.rfvEmail.ValidationExpression = @"^([a-zA-Z0-9_\-\+]+(\.[a-zA-Z0-9_\-\+]+)*@[a-zA-Z0-9_-]+(\.[a-zA-Z0-9_-]+)+)*$";
        
        if (!this.IsLiveSite && !RequestHelper.IsPostBack() && process)
        {
            ReloadData();
        }

        if (!RequestHelper.IsPostBack())
        {
            chkSendConfirmationEmail.Checked = true;
        }

        if (!chkChecked)
        {
            chkSendConfirmationEmail.Checked = false;
        }

        if (saved)
        {
            lblInfo.Visible = true;
            lblInfo.Text = GetString("General.ChangesSaved");
        }
    }


    /// <summary>
    /// Sets data to database.
    /// </summary>
    protected void btnOK_Click(object sender, EventArgs e)
    {
        if (!CheckPermissions("cms.forums", CMSAdminControl.PERMISSION_MODIFY))
        {
            return;
        }

        string errorMessage = new Validator().NotEmpty(txtSubscriptionEmail.Text, GetString("ForumSubscription_Edit.EnterSomeEmail")).Result;
        
        if (errorMessage == "")
        {

            // Check that e-mail is not already subscribed
            if (ForumSubscriptionInfoProvider.IsSubscribed(txtSubscriptionEmail.Text.Trim(), this.mForumId, 0))
            {
                lblError.Visible = true;
                lblError.Text = GetString("ForumSubscibe.SubscriptionExists");
                return;
            }

            if (SubscriptionObj == null)
            {
                SubscriptionObj = new ForumSubscriptionInfo();
                SubscriptionObj.SubscriptionForumID = this.mForumId;
                SubscriptionObj.SubscriptionGUID = Guid.NewGuid();
            }

            SubscriptionObj.SubscriptionEmail = txtSubscriptionEmail.Text.Trim();
            if (ValidationHelper.IsEmail(SubscriptionObj.SubscriptionEmail))
            {
                ForumSubscriptionInfoProvider.SetForumSubscriptionInfo(SubscriptionObj, chkSendConfirmationEmail.Checked, null, null);

                this.SubscriptionID = SubscriptionObj.SubscriptionID;

                lblInfo.Visible = true;
                lblInfo.Text = GetString("General.ChangesSaved");

                this.RaiseOnSaved();
            }
            else
            {
                lblError.Visible = true;
                lblError.Text = GetString("ForumSubscription_Edit.EmailIsNotValid");
            }
        }
        else
        {
            lblError.Visible = true;
            lblError.Text = errorMessage;
        }
    }

    #endregion


    #region "Private methods"

    /// <summary>
    /// Reloads the form data.
    /// </summary>
    public override void ReloadData()
    {
        ClearForm();
        if (SubscriptionObj != null)
        {
            txtSubscriptionEmail.Text = HTMLHelper.HTMLEncode(SubscriptionObj.SubscriptionEmail);
        }
    }


    /// <summary>
    /// Clears the form fields to default values.
    /// </summary>
    public override void ClearForm()
    {
        this.txtSubscriptionEmail.Text = "";
    }
    
    #endregion
}
