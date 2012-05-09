using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using CMS.UIControls;
using CMS.MessageBoard;
using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.SiteProvider;
using CMS.SettingsProvider;

public partial class CMSModules_MessageBoards_Controls_Boards_BoardSubscription : CMSAdminEditControl
{
    #region "Private variables"

    private int mSubscriptionId = 0;
    private int mBoardId = 0;
    private int mGroupID = 0;

    private BoardSubscriptionInfo mCurrentSubscription = null;

    #endregion


    #region "Private properties"

    /// <summary>
    /// Currently edited subscriber.
    /// </summary>
    private BoardSubscriptionInfo CurrentSubscription
    {
        get
        {
            if (mCurrentSubscription == null)
            {
                mCurrentSubscription = BoardSubscriptionInfoProvider.GetBoardSubscriptionInfo(this.SubscriptionID);
            }
            return mCurrentSubscription;
        }
        set
        {
            mCurrentSubscription = value;
        }
    }

    #endregion


    #region "Public properties"

    /// <summary>
    /// ID of the current board.
    /// </summary>
    public int BoardID
    {
        get
        {
            return this.mBoardId;
        }
        set
        {
            this.mBoardId = value;
        }
    }


    /// <summary>
    /// ID of the current subscription.
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
    /// ID of the current subscription.
    /// </summary>
    public int GroupID
    {
        get
        {
            return mGroupID;
        }
        set
        {
            mGroupID = value;
        }
    }

    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        // If control should be hidden save view state memory
        if (this.StopProcessing || !this.Visible)
        {
            this.EnableViewState = false;
        }

        // Initializes the controls
        SetupControls();

        // Reload data if necessary
        if (!URLHelper.IsPostback() && !this.IsLiveSite)
        {
            ReloadData();
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        // Reload data if is live site 
        if (!URLHelper.IsPostback() && this.IsLiveSite)
        {
            ReloadData();
        }
    }


    #region "General methods"

    /// <summary>
    /// Initializes the controls on the page.
    /// </summary>
    private void SetupControls()
    {
        ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "UpdateForm", ScriptHelper.GetScript("function UpdateForm() {return;}"));

        this.userSelector.SiteID = CMSContext.CurrentSiteID;
        this.userSelector.ShowSiteFilter = false;
        this.userSelector.IsLiveSite = this.IsLiveSite;

        int groupId = QueryHelper.GetInteger("groupid", 0);
        if (groupId > 0)
        {
            this.userSelector.GroupID = groupId;
        }
        else
        {
            this.userSelector.GroupID = this.GroupID;
        }

        // Initialize the labels
        this.btnOk.Text = GetString("general.ok");
        this.radAnonymousSubscription.Text = GetString("board.subscription.anonymous");
        this.radRegisteredSubscription.Text = GetString("board.subscription.registered");

        this.lblEmailAnonymous.Text = GetString("general.email") + ResHelper.Colon;
        this.lblEmailRegistered.Text = GetString("general.email") + ResHelper.Colon;
        this.lblUserRegistered.Text = GetString("general.username") + ResHelper.Colon;
        this.rfvEmailAnonymous.ErrorMessage = GetString("board.subscription.noemail");
        this.rfvEmailRegistered.ErrorMessage = GetString("board.subscription.noemail");

        this.radRegisteredSubscription.CheckedChanged += new EventHandler(radRegisteredSubscription_CheckedChanged);
        this.radAnonymousSubscription.CheckedChanged += new EventHandler(radAnonymousSubscription_CheckedChanged);

        this.userSelector.UniSelector.OnSelectionChanged += new EventHandler(UniSelector_OnSelectionChanged);

        ProcessDisabling(this.radAnonymousSubscription.Checked);
    }


    protected void UniSelector_OnSelectionChanged(object sender, EventArgs e)
    {
        int userId = ValidationHelper.GetInteger(this.userSelector.Value, 0);
        if (userId > 0)
        {
            UserInfo ui = UserInfoProvider.GetUserInfo(userId);
            if (ui != null)
            {
                this.txtEmailRegistered.Text = ui.Email;
                
            }
        }
    }


    public override void ReloadData()
    {
        ClearForm();

        this.lblInfo.Visible = false;
        this.lblError.Visible = false;

        // Get current subscription ID
        if (this.SubscriptionID > 0)
        {
            // Get current subscription info
            this.CurrentSubscription = BoardSubscriptionInfoProvider.GetBoardSubscriptionInfo(this.SubscriptionID);
            EditedObject = this.CurrentSubscription;

            // Load existing subscription data
            if (this.CurrentSubscription != null)
            {
                // If the subscription is related to the registered user
                if (this.CurrentSubscription.SubscriptionUserID > 0)
                {
                    // Load data
                    this.userSelector.Value = this.CurrentSubscription.SubscriptionUserID;
                    this.txtEmailRegistered.Text = this.CurrentSubscription.SubscriptionEmail;

                    this.radRegisteredSubscription.Checked = true;
                    this.radAnonymousSubscription.Checked = false;

                    ProcessDisabling(false);
                }
                else
                {
                    // Load data
                    this.txtEmailAnonymous.Text = this.CurrentSubscription.SubscriptionEmail;

                    this.radAnonymousSubscription.Checked = true;
                    this.radRegisteredSubscription.Checked = false;

                    ProcessDisabling(true);
                }
            }
        }
        else
        {
            this.radAnonymousSubscription.Checked = true;
            this.radRegisteredSubscription.Checked = false;

            ProcessDisabling(true);
        }

        if (QueryHelper.GetBoolean("saved", false))
        {
            // Display info on success if subscription is edited
            this.lblInfo.Text = GetString("general.changessaved");
            this.lblInfo.Visible = true;
        }
    }


    /// <summary>
    /// Clears the form entries.
    /// </summary>
    public override void ClearForm()
    {
        this.radAnonymousSubscription.Checked = true;
        this.userSelector.Value = "";
        this.txtEmailAnonymous.Text = "";
        this.txtEmailRegistered.Text = "";
    }

    #endregion


    #region "Private methods"

    /// <summary>
    /// 
    /// </summary>
    private string ValidateForm()
    {
        string errMsg = "";
        string where = "";

        // Check if the entered e-mail is non-empty string and valid e-mail address
        if (this.radAnonymousSubscription.Checked)
        {
            errMsg = new Validator().NotEmpty(this.txtEmailAnonymous.Text, GetString("board.subscription.emailnotvalid")).IsEmail(this.txtEmailAnonymous.Text, GetString("board.subscription.emailnotvalid")).Result;
            where = "SubscriptionEmail ='" + SqlHelperClass.GetSafeQueryString(this.txtEmailAnonymous.Text, false) + "'";
        }
        else
        {
            errMsg = new Validator().NotEmpty(this.txtEmailRegistered.Text, GetString("board.subscription.emailnotvalid")).IsEmail(this.txtEmailRegistered.Text, GetString("board.subscription.emailnotvalid")).NotEmpty(this.userSelector.Value, GetString("board.subscription.emptyuser")).Result;
            where = "SubscriptionEmail ='" + SqlHelperClass.GetSafeQueryString(this.txtEmailRegistered.Text, false) + "'";
        }

        // Check if there is not the subscription for specified e-mail yet
        if (string.IsNullOrEmpty(errMsg))
        {
            DataSet ds = BoardSubscriptionInfoProvider.GetSubscriptions(where, null);
            if (!DataHelper.DataSourceIsEmpty(ds))
            {
                // If existing subscription is the current one
                if ((ValidationHelper.GetInteger(ds.Tables[0].Rows[0]["SubscriptionID"], 0) != this.SubscriptionID) &&
                    (ValidationHelper.GetInteger(ds.Tables[0].Rows[0]["SubscriptionBoardID"], 0) == this.BoardID))
                {
                    errMsg = GetString("board.subscription.emailexists");
                }
            }
        }

        return errMsg;
    }


    /// <summary>
    /// Handles enabling/disabling appropriate controls
    /// </summary>
    private void ProcessDisabling(bool isTrue)
    {
        // Process registered user subscription displaying            
        this.lblEmailAnonymous.Enabled = isTrue;
        this.txtEmailAnonymous.Enabled = isTrue;
        this.rfvEmailAnonymous.Enabled = isTrue;

        this.lblUserRegistered.Enabled = !isTrue;
        this.lblEmailRegistered.Enabled = !isTrue;
        this.txtEmailRegistered.Enabled = !isTrue;
        this.rfvEmailRegistered.Enabled = !isTrue;
        this.userSelector.Enabled = !isTrue;
    }

    #endregion


    #region "Event handling"

    protected void radRegisteredSubscription_CheckedChanged(object sender, EventArgs e)
    {
        ProcessDisabling(!this.radRegisteredSubscription.Checked);
    }


    protected void radAnonymousSubscription_CheckedChanged(object sender, EventArgs e)
    {
        ProcessDisabling(this.radAnonymousSubscription.Checked);
    }


    protected void btnOk_Click(object sender, EventArgs e)
    {
        if (!CheckPermissions("cms.messageboards", CMSAdminControl.PERMISSION_MODIFY))
        {
            return;
        }

        string errMsg = ValidateForm();

        // If entered form was validated successfully
        if (string.IsNullOrEmpty(errMsg))
        {
            BoardSubscriptionInfo bsi = null;

            // If existing subscription is edited
            if (this.SubscriptionID > 0)
            {
                bsi = this.CurrentSubscription;
            }
            else
            {
                bsi = new BoardSubscriptionInfo();
            }

            // Get data according the selected type
            if (this.radAnonymousSubscription.Checked)
            {
                bsi.SubscriptionEmail = this.txtEmailAnonymous.Text;
                bsi.SubscriptionUserID = 0;
            }
            else
            {
                bsi.SubscriptionEmail = this.txtEmailRegistered.Text;
                bsi.SubscriptionUserID = ValidationHelper.GetInteger(this.userSelector.Value, 0);
            }

            bsi.SubscriptionBoardID = this.BoardID;

            // Save information on user
            BoardSubscriptionInfoProvider.SetBoardSubscriptionInfo(bsi);

            this.SubscriptionID = bsi.SubscriptionID;

            this.RaiseOnSaved();

            // Display info on success if subscription is edited
            this.lblInfo.Text = GetString("general.changessaved");
            this.lblInfo.Visible = true;
        }
        else
        {
            // Inform user on error
            this.lblError.Text = errMsg;
            this.lblError.Visible = true;

            this.lblInfo.Visible = false;
        }
    }

    #endregion
}
