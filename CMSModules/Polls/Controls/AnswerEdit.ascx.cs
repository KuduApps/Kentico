using System;

using CMS.GlobalHelper;
using CMS.Polls;
using CMS.UIControls;

public partial class CMSModules_Polls_Controls_AnswerEdit : CMSAdminEditControl
{
    #region "Variables"

    private PollAnswerInfo pollAnswerObj = null;

    #endregion


    #region "Properties"

    /// <summary>
    /// Gets or sets the answer ID.
    /// </summary>
    public int PollId
    {
        get
        {
            return ValidationHelper.GetInteger(this.ViewState["pollid"], 0);
        }
        set
        {
            this.ViewState["pollid"] = (object)value;
        }
    }


    /// <summary>
    /// Gets or sets saved property.
    /// </summary>
    public bool Saved
    {
        get
        {
            return ValidationHelper.GetBoolean(this.ViewState["saved"], false);
        }
        set
        {
            this.ViewState["saved"] = (object)value;
        }
    }

    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        // Required field validator error messages initialization
        rfvAnswerText.ErrorMessage = GetString("Polls_Answer_Edit.AnswerTextError");

        // Controls initializations				
        lblAnswerText.Text = GetString("Polls_Answer_Edit.AnswerTextLabel");
        lblVotes.Text = GetString("Polls_Answer_Edit.Votes");
        btnOk.Text = GetString("General.OK");

        // Set if it is live site
        txtAnswerText.IsLiveSite = this.IsLiveSite;

        if (!RequestHelper.IsPostBack() && !IsLiveSite)
        {
            this.LoadData();
        }
    }


    /// <summary>
    /// Loads new data for this control.
    /// </summary>
    public void LoadData()
    {
        // If working with existing record
        if (this.ItemID > 0)
        {
            pollAnswerObj = PollAnswerInfoProvider.GetPollAnswerInfo(this.ItemID);
            EditedObject = pollAnswerObj;

            if (pollAnswerObj != null)
            {
                // Fill editing form
                if (!RequestHelper.IsPostBack())
                {
                    this.ReloadData();
                }

                // When saved, display info message
                if (this.Saved)
                {
                    lblInfo.Visible = true;
                    lblInfo.Text = GetString("General.ChangesSaved");
                    this.Saved = false;
                }
                // Otherwise hide info message
                else
                {
                    lblInfo.Visible = false;
                }

                this.PollId = pollAnswerObj.AnswerPollID;
            }
        }
        // If creating new record
        else
        {
            plcVotes.Visible = false;
            txtVotes.Text = "0";
        }
    }


    /// <summary>
    /// Clears data.
    /// </summary>
    public override void ClearForm()
    {
        base.ClearForm();
        txtAnswerText.Text = null;
        txtVotes.Text = null;
    }


    /// <summary>
    /// Reloads answer data.
    /// </summary>
    public override void ReloadData()
    {
        this.ClearForm();
        if (pollAnswerObj == null)
        {
            pollAnswerObj = PollAnswerInfoProvider.GetPollAnswerInfo(this.ItemID);
        }

        if (pollAnswerObj != null)
        {
            // Load the fields
            txtAnswerText.Text = pollAnswerObj.AnswerText;
            chkAnswerEnabled.Checked = pollAnswerObj.AnswerEnabled;
            txtVotes.Text = pollAnswerObj.AnswerCount.ToString();
            plcVotes.Visible = true;
        }
        else
        {
            txtAnswerText.Text = String.Empty;
            plcVotes.Visible = false;
        }
    }


    /// <summary>
    /// Sets data to database.
    /// </summary>
    protected void btnOK_Click(object sender, EventArgs e)
    {
        // Load current answer object
        if (pollAnswerObj == null)
        {
            pollAnswerObj = PollAnswerInfoProvider.GetPollAnswerInfo(this.ItemID);
        }
        // Check permission for answer object (global/site poll)
        if (!CheckModifyPermission(this.PollId))
        {
            return;
        }

        string errorMessage = null;
        // Validate the input
        if (txtVotes.Visible)
        {
            errorMessage = new Validator().NotEmpty(txtAnswerText.Text, rfvAnswerText.ErrorMessage)
                .IsPositiveNumber(txtVotes.Text, GetString("Polls_Answer_Edit.VotesNotNumber"), true)
                .IsInteger(txtVotes.Text, GetString("Polls_Answer_Edit.VotesNotNumber")).Result;
        }
        else
        {
            errorMessage = new Validator().NotEmpty(txtAnswerText.Text, rfvAnswerText.ErrorMessage).Result;
        }

        if (String.IsNullOrEmpty(errorMessage))
        {
            // If pollAnswer doesn't already exist, create new one
            if (pollAnswerObj == null)
            {
                pollAnswerObj = new PollAnswerInfo();
                pollAnswerObj.AnswerOrder = PollAnswerInfoProvider.GetLastAnswerOrder(this.PollId) + 1;
                pollAnswerObj.AnswerCount = 0;
                pollAnswerObj.AnswerPollID = this.PollId;
            }

            // Set the fields
            pollAnswerObj.AnswerEnabled = chkAnswerEnabled.Checked;
            pollAnswerObj.AnswerText = txtAnswerText.Text.Trim();
            pollAnswerObj.AnswerCount = ValidationHelper.GetInteger(this.txtVotes.Text, 0);

            // Save the data
            PollAnswerInfoProvider.SetPollAnswerInfo(pollAnswerObj);
            this.Saved = true;
            this.ItemID = pollAnswerObj.AnswerID;

            // Raise event;
            RaiseOnSaved();
        }
        else
        {
            // Error message - Validation
            lblError.Visible = true;
            lblError.Text = errorMessage;
        }
    }


    /// <summary>
    /// Checks modify permission. Returns false if checking failed.
    /// </summary>
    /// <param name="pollId">Poll ID</param>
    private bool CheckModifyPermission(int pollId)
    {
        // Get parent of answer object and see if it is global poll or site poll
        PollInfo pi = null;
        if (pollId > 0)                              // non-zero value when creating new poll
        {
            pi = PollInfoProvider.GetPollInfo(pollId);
        }
        else if (pollAnswerObj != null)              // not null when modifying existing answer
        {
            pi = PollInfoProvider.GetPollInfo(pollAnswerObj.AnswerPollID);
        }
        if (pi != null)
        {
            return (pi.PollSiteID > 0) && CheckPermissions("cms.polls", CMSAdminControl.PERMISSION_MODIFY) ||
                (pi.PollSiteID <= 0) && CheckPermissions("cms.polls", CMSAdminControl.PERMISSION_GLOBALMODIFY);
        }
        return false;
    }
}
