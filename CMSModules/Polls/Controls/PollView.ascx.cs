using System;
using System.Text;
using System.Data;
using System.Web.UI;

using CMS.CMSHelper;
using CMS.ExtendedControls;
using CMS.GlobalHelper;
using CMS.Polls;
using CMS.SiteProvider;
using CMS.UIControls;
using CMS.SettingsProvider;
using CMS.WebAnalytics;
using CMS.TreeEngine;
using CMS.PortalEngine;

public partial class CMSModules_Polls_Controls_PollView : CMSUserControl
{
    #region "Events"

    public event EventHandler OnAfterVoted;

    #endregion


    #region "Variables"

    protected bool mShowGraph = true;
    protected CountTypeEnum mCodeType = CountTypeEnum.Absolute;
    protected bool mShowResultsAfterVote = true;
    protected bool mCheckVoted = true;
    protected bool mCheckPermissions = true;
    protected bool mCheckOpen = true;
    protected bool mHideWhenNotAuthorized = false;
    protected bool mHideWhenNotOpened = false;
    protected string mButtonText = null;
    protected int mCacheMinutes = 0;
    protected string errMessage = null;
    protected bool hasPermission = false;
    protected bool isOpened = false;
    protected DataSet answers = null;
    private PollInfo pi = null;

    #endregion


    #region "Properties"

    /// <summary>
    /// Gets or sets the code name of the poll, which should be displayed.
    /// </summary>
    public string PollCodeName
    {
        get
        {
            return ValidationHelper.GetString(ViewState["PollCodeName"], null);
        }
        set
        {
            ViewState["PollCodeName"] = value;
        }
    }


    /// <summary>
    /// Gets or sets the site ID of the poll (optional).
    /// </summary>
    public int PollSiteID
    {
        get
        {
            return ValidationHelper.GetInteger(ViewState["PollSiteID"], 0);
        }
        set
        {
            ViewState["PollSiteID"] = value;
        }
    }


    /// <summary>
    /// Gets or sets the group ID of the poll (optional).
    /// </summary>
    public int PollGroupID
    {
        get
        {
            return ValidationHelper.GetInteger(ViewState["PollGroupID"], 0);
        }
        set
        {
            ViewState["PollGroupID"] = value;
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether the graph of the poll is displayed.
    /// </summary>
    public bool ShowGraph
    {
        get
        {
            return mShowGraph;
        }
        set
        {
            mShowGraph = value;
        }
    }


    /// <summary>
    /// Gets or sets the type of the representation of the answers' count in the graph.
    /// </summary>
    public CountTypeEnum CountType
    {
        get
        {
            return mCodeType;
        }
        set
        {
            mCodeType = value;
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether graph is displayed after answering the poll.
    /// </summary>
    public bool ShowResultsAfterVote
    {
        get
        {
            return mShowResultsAfterVote;
        }
        set
        {
            mShowResultsAfterVote = value;
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether current user has voted is checked.
    /// </summary>
    public bool CheckVoted
    {
        get
        {
            return mCheckVoted;
        }
        set
        {
            mCheckVoted = value;
        }
    }


    /// <summary>
    /// Gets or sets the value that indicates whether permissions are checked.
    /// </summary>
    public bool CheckPermissions
    {
        get
        {
            return mCheckPermissions;
        }
        set
        {
            mCheckPermissions = value;
        }
    }


    /// <summary>
    /// Gets or stes the value that indicates whether open from/to time is checked
    /// </summary>
    public bool CheckOpen
    {
        get
        {
            return mCheckOpen;
        }
        set
        {
            mCheckOpen = value;
        }
    }


    /// <summary>
    /// If true, the control hides when not authorized, 
    /// otherwise the control displays the message and does not allow to vote.
    /// </summary>
    public bool HideWhenNotAuthorized
    {
        get
        {
            return mHideWhenNotAuthorized;
        }
        set
        {
            mHideWhenNotAuthorized = value;
        }
    }


    /// <summary>
    /// If true, the control hides when not opened, 
    /// otherwise the control does not allow to vote.
    /// </summary>
    public bool HideWhenNotOpened
    {
        get
        {
            return mHideWhenNotOpened;
        }
        set
        {
            mHideWhenNotOpened = value;
        }
    }


    /// <summary>
    /// Gets or sets the text of the vote button.
    /// </summary>
    public string ButtonText
    {
        get
        {
            return ValidationHelper.GetString(mButtonText, GetString("Polls.Vote"));
        }
        set
        {
            mButtonText = value;
        }
    }


    /// <summary>
    /// Vote button.
    /// </summary>
    public LocalizedButton VoteButton
    {
        get
        {
            return btnVote;
        }
    }


    /// <summary>
    /// Gets or sets the WebPart cache minutes.
    /// </summary>
    public int CacheMinutes
    {
        get
        {
            return mCacheMinutes;
        }
        set
        {
            mCacheMinutes = value;
        }
    }


    /// <summary>
    /// Gets or sets the poll answers dataset.
    /// </summary>
    public DataSet Answers
    {
        get
        {
            if (DataHelper.DataSourceIsEmpty(answers))
            {
                // Try to get data from cache
                using (CachedSection<DataSet> cs = new CachedSection<DataSet>(ref answers, this.CacheMinutes, true, null, "pollanswers", this.PollCodeName))
                {
                    if (cs.LoadData)
                    {
                        // Get from database
                        if (pi != null)
                        {
                            answers = PollAnswerInfoProvider.GetAnswers(pi.PollID, -1, "AnswerID, AnswerText, AnswerCount, AnswerEnabled");
                        }

                        // Add to the cache
                        cs.Data = answers;
                    }
                }
            }

            return answers;
        }
        set
        {
            // Remove the data from cache
            if (this.CacheMinutes > 0)
            {
                string useCacheItemName = CacheHelper.GetCacheItemName(null, "pollanswers", this.PollCodeName);
                CacheHelper.Remove(useCacheItemName);
            }

            answers = null;
        }
    }

    #endregion


    #region "Methods"

    /// <summary>
    /// Page load.
    /// </summary>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.Visible)
        {
            ReloadData(false);
        }
    }


    protected void Page_PreRender(object sender, EventArgs e)
    {
        // Display info messages if not empty
        lblInfo.Visible = !string.IsNullOrEmpty(lblInfo.Text);
        lblResult.Visible = !string.IsNullOrEmpty(lblResult.Text);
    }


    /// <summary>
    /// Loads data.
    /// </summary>
    public void ReloadData(bool forceReload)
    {
        if (!this.StopProcessing)
        {
            this.SetContext();

            lblInfo.Text = string.Empty;
            lblInfo.Visible = false;

            if (pi == null)
            {
                pi = PollInfoProvider.GetPollInfo(this.PollCodeName, this.PollSiteID, this.PollGroupID);
                hasPermission = HasPermission();
                isOpened = IsOpened();
            }

            // Show poll if current user has permission or if poll should be displayed even if user is not authorized
            // and if poll is opened or if poll should be opened even if it is not opened
            // ... and show group poll if it is poll of current group
            bool showPoll = (pi != null) && (hasPermission || !this.HideWhenNotAuthorized) && (isOpened || !this.HideWhenNotOpened);
            // Show site poll only if it is poll of current site
            if (showPoll && (pi.PollSiteID > 0) && (pi.PollSiteID != CMSContext.CurrentSiteID)) 
            {
                showPoll = false;
            }

            // Show global poll only if it is allowed for current site
            if (showPoll && (pi.PollSiteID == 0))
            {
                showPoll = SettingsKeyProvider.GetBoolValue(CMSContext.CurrentSiteName + ".CMSPollsAllowGlobal");
            }

            if (showPoll)
            {
                this.Visible = true;

                // Load title
                lblTitle.Text = HTMLHelper.HTMLEncode(pi.PollTitle);
                // Load question
                lblQuestion.Text = HTMLHelper.HTMLEncode(pi.PollQuestion);

                if ((!forceReload) || ((forceReload) && (this.ShowResultsAfterVote)))
                {
                    // Add answer section
                    CreateAnswerSection(forceReload, this.CheckVoted && PollInfoProvider.HasVoted(pi.PollID));
                }
                else
                {
                    // Hide answer panel
                    pnlAnswer.Visible = false;
                }

                if ((forceReload) && (isOpened))
                {
                    // Hide footer with vote button
                    pnlFooter.Visible = false;

                    // Add poll response after voting
                    if ((errMessage != null) && (errMessage.Trim() != ""))
                    {
                        // Display message if error occurs
                        lblInfo.Text = errMessage;
                        lblInfo.CssClass = "ErrorMessage";
                    }
                    else
                    {
                        // Display poll response message
                        lblResult.Text = HTMLHelper.HTMLEncode(pi.PollResponseMessage);
                    }
                }
                else if (isOpened)
                {
                    if (hasPermission && !(this.CheckVoted && (PollInfoProvider.HasVoted(pi.PollID))))
                    {
                        // Display footer wiht vote button
                        pnlFooter.Visible = true;
                        btnVote.Text = this.ButtonText;
                    }
                    else
                    {
                        pnlFooter.Visible = false;
                    }
                }
                else
                {
                    pnlFooter.Visible = false;
                    lblInfo.Text = GetString("Polls.Closed");
                }
            }
            else
            {
                this.Visible = false;
            }

            this.ReleaseContext();
        }
    }


    /// <summary>
    /// Creates poll answer section.
    /// </summary>
    /// <param name="reload">Indicates postback</param>
    /// <param name="hasVoted">Indicates if user has voted</param>
    protected void CreateAnswerSection(bool reload, bool hasVoted)
    {
        pnlAnswer.Controls.Clear();

        if (pi != null)
        {
            // Get poll's answers
            DataSet ds = Answers;
            if (!DataHelper.DataSourceIsEmpty(ds))
            {
                int count = 0;
                int maxCount = 0;
                long sumCount = 0;

                // Sum answer counts and get highest
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    if (ValidationHelper.GetBoolean(row["AnswerEnabled"], true))
                    {
                        count = ValidationHelper.GetInteger(row["AnswerCount"], 0);
                        sumCount += count;
                        if (count > maxCount)
                        {
                            maxCount = count;
                        }
                    }
                }

                LocalizedCheckBox chkItem = null;
                LocalizedRadioButton radItem = null;
                LocalizedLabel lblItem = null;
                int index = 0;
                bool enabled = false;

                pnlAnswer.Controls.Add(new LiteralControl("<table cellpadding=\"0\" cellspacing=\"0\" width=\"100%\">"));

                // Create the answers
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    enabled = ValidationHelper.GetBoolean(row["AnswerEnabled"], true);
                    if (enabled)
                    {
                        pnlAnswer.Controls.Add(new LiteralControl("<tr><td class=\"PollAnswer\" colspan=\"2\">"));

                        if (((reload) && (this.ShowResultsAfterVote)) || (!hasPermission && !this.HideWhenNotAuthorized)
                            || (!isOpened && !this.HideWhenNotOpened) || (this.CheckVoted && PollInfoProvider.HasVoted(pi.PollID)))
                        {
                            // Add label
                            lblItem = new LocalizedLabel();
                            lblItem.ID = "lbl" + ValidationHelper.GetInteger(row["AnswerID"], 0);
                            lblItem.EnableViewState = false;
                            lblItem.Text = HTMLHelper.HTMLEncode(ValidationHelper.GetString(row["AnswerText"], ""));
                            lblItem.CssClass = "PollAnswerText";

                            pnlAnswer.Controls.Add(lblItem);
                        }
                        else
                        {
                            if (pi.PollAllowMultipleAnswers)
                            {
                                // Add checkboxes for multiple answers
                                chkItem = new LocalizedCheckBox();
                                chkItem.ID = "chk" + ValidationHelper.GetInteger(row["AnswerID"], 0);
                                chkItem.AutoPostBack = false;
                                chkItem.Text = HTMLHelper.HTMLEncode(ValidationHelper.GetString(row["AnswerText"], ""));
                                chkItem.Checked = false;
                                chkItem.CssClass = "PollAnswerCheck";

                                pnlAnswer.Controls.Add(chkItem);
                            }
                            else
                            {
                                // Add radiobuttons
                                radItem = new LocalizedRadioButton();
                                radItem.ID = "rad" + ValidationHelper.GetInteger(row["AnswerID"], 0);
                                radItem.AutoPostBack = false;
                                radItem.GroupName = pi.PollCodeName + "Group";
                                radItem.Text = HTMLHelper.HTMLEncode(ValidationHelper.GetString(row["AnswerText"], ""));
                                radItem.Checked = false;
                                radItem.CssClass = "PollAnswerRadio";

                                pnlAnswer.Controls.Add(radItem);
                            }
                        }

                        pnlAnswer.Controls.Add(new LiteralControl("</td></tr>"));

                        if (this.ShowGraph || (hasVoted || reload) && this.ShowResultsAfterVote)
                        {
                            // Create graph under the answer
                            CreateGraph(maxCount, ValidationHelper.GetInteger(row["AnswerCount"], 0), sumCount, index);
                        }

                        index++;
                    }
                }

                pnlAnswer.Controls.Add(new LiteralControl("</table>"));
            }
        }
    }


    /// <summary>
    /// Creates graph bar for the answer.
    /// </summary>
    /// <param name="maxValue">Max answers' count</param>
    /// <param name="currentValue">Current answer count</param>
    /// <param name="countSummary">Count summary of all answers</param>
    /// <param name="index">Index</param>
    protected void CreateGraph(int maxValue, int currentValue, long countSummary, int index)
    {
        long ratio = 0;
        if (maxValue != 0)
        {
            ratio = Math.BigMul(100, currentValue) / (long)maxValue;
        }
        // Begin PollAnswerGraph
        pnlAnswer.Controls.Add(new LiteralControl("<tr><td style=\"width: 100%;\"><div class=\"PollGraph\">"));
        if (ratio != 0)
        {
            // PollAnswerItemGraph
            pnlAnswer.Controls.Add(new LiteralControl("<div class=\"PollGraph" + index +
                "\" style=\"width:" + ratio + "%\">&nbsp;</div>"));
        }
        else
        {
            pnlAnswer.Controls.Add(new LiteralControl("&nbsp;"));
        }

        // End PollAnswerGraph
        pnlAnswer.Controls.Add(new LiteralControl("</div></td><td style=\"white-space: nowrap;\" class=\"PollCount\">"));

        // Create lable with answer count
        if (this.CountType == CountTypeEnum.Absolute)
        {
            // Absolute count
            pnlAnswer.Controls.Add(new LiteralControl(currentValue.ToString()));
        }
        else if (this.CountType == CountTypeEnum.Percentage)
        {
            // Percentage count
            long percent = 0;
            if (countSummary != 0)
            {
                percent = Math.BigMul(100, currentValue) / countSummary;
            }
            pnlAnswer.Controls.Add(new LiteralControl(percent.ToString() + "%"));
        }

        // End PollAnswerGraph
        pnlAnswer.Controls.Add(new LiteralControl("</td></tr>"));
    }


    /// <summary>
    /// On btnVote click event handler.
    /// </summary>
    protected void btnVote_OnClick(object sender, EventArgs e)
    {
        // Check banned ip
        if (!BannedIPInfoProvider.IsAllowed(CMSContext.CurrentSiteName, BanControlEnum.AllNonComplete))
        {
            lblInfo.CssClass = "ErrorMessage";
            lblInfo.Text = GetString("General.BannedIP");
            return;
        }

        if (pi != null)
        {
            // Indicates whether user voted or not
            bool voted = false;

            // Check if user has already voted
            if ((this.CheckVoted) && (PollInfoProvider.HasVoted(pi.PollID)))
            {
                errMessage = GetString("Polls.UserHasVoted");
                voted = true;
            }
            else if (isOpened)
            {
                // Get poll answers
                DataSet ds = Answers;
                if (!DataHelper.DataSourceIsEmpty(ds))
                {
                    DataRowCollection rows = ds.Tables[0].Rows;

                    LocalizedCheckBox chkItem = null;
                    LocalizedRadioButton radItem = null;
                    bool selected = false;
                    PollAnswerInfo pai = null;

                    // List of poll answers (in case of multiple answers) for activity logging
                    StringBuilder pollAnswerIDs = new StringBuilder();

                    foreach (DataRow row in rows)
                    {
                        pai = new PollAnswerInfo(row);

                        if ((pai != null) && (pai.AnswerEnabled))
                        {
                            selected = false;

                            // Find specific controls and update pollanswerinfo if controls are checked
                            if (pi.PollAllowMultipleAnswers)
                            {
                                // Find checkbox
                                chkItem = (LocalizedCheckBox)this.pnlAnswer.FindControl("chk" + pai.AnswerID);

                                if (chkItem != null)
                                {
                                    selected = chkItem.Checked;
                                }
                            }
                            else
                            {
                                // Find radiobutton
                                radItem = (LocalizedRadioButton)this.pnlAnswer.FindControl("rad" + pai.AnswerID);

                                if (radItem != null)
                                {
                                    selected = radItem.Checked;
                                }
                            }

                            if ((selected) && (pai.AnswerCount < Int32.MaxValue))
                            {
                                // Set the vote
                                PollAnswerInfoProvider.Vote(pai.AnswerID);
                                voted = true;

                                // Save all selected answers (for activity logging)
                                pollAnswerIDs.Append(pai.AnswerID);
                                pollAnswerIDs.Append(ActivityLogProvider.POLL_ANSWER_SEPARATOR);
                            }
                        }
                    }

                    if (voted)
                    {
                        LogActivity(pi, pollAnswerIDs.ToString());
                    }

                    if ((this.CheckVoted) && (voted))
                    {
                        // Create cookie about user's voting
                        PollInfoProvider.SetVoted(pi.PollID);
                    }
                }
            }

            if (voted)
            {
                // Clear cache if it's used
                Answers = null;
                // Reload poll control
                ReloadData(true);

                if (OnAfterVoted != null)
                {
                    OnAfterVoted(this, EventArgs.Empty);
                }
            }
            else
            {
                lblInfo.CssClass = "ErrorMessage";
                lblInfo.Text = GetString("Polls.DidNotVoted");
            }
        }
    }


    /// <summary>
    /// Returns true if user has permissions.
    /// </summary>
    protected bool HasPermission()
    {
        if ((!this.CheckPermissions) || (CMSContext.CurrentUser.IsGlobalAdministrator))
        {
            return true;
        }

        bool toReturn = false;

        if (pi != null)
        {
            // Access to all users
            if (pi.PollAccess == SecurityAccessEnum.AllUsers)
            {
                toReturn = true;
            }

            // Access to authenticated user
            if ((pi.PollAccess == SecurityAccessEnum.AuthenticatedUsers) && (CMSContext.CurrentUser.IsAuthenticated()))
            {
                toReturn = true;
            }

            // Access to group members
            if ((pi.PollAccess == SecurityAccessEnum.GroupMembers) && (CMSContext.CurrentUser.IsGroupMember(this.PollGroupID)))
            {
                toReturn = true;
            }

            // Access to roles
            if ((pi.PollAccess == SecurityAccessEnum.AuthorizedRoles) && (CMSContext.CurrentUser != null))
            {
                foreach (String role in pi.AllowedRoles.Keys)
                {
                    if (CMSContext.CurrentUser.IsInRole(role, CMSContext.CurrentSiteName))
                    {
                        toReturn = true;
                        break;
                    }
                }
            }
        }

        return toReturn;
    }


    /// <summary>
    /// Return true if actual time is inside the poll's opening time or the poll's open from/to are not set.
    /// </summary>
    protected bool IsOpened()
    {
        if (!this.CheckOpen)
        {
            return true;
        }

        bool toReturn = false;

        if (pi != null)
        {
            DateTime now = DateTime.Now;
            if ((pi.PollOpenFrom == DataHelper.DATETIME_NOT_SELECTED) && (pi.PollOpenTo == DataHelper.DATETIME_NOT_SELECTED))
            {
                toReturn = true;
            }
            else if ((pi.PollOpenFrom != DataHelper.DATETIME_NOT_SELECTED) && (pi.PollOpenTo == DataHelper.DATETIME_NOT_SELECTED) && (pi.PollOpenFrom < now))
            {
                toReturn = true;
            }
            else if ((pi.PollOpenFrom == DataHelper.DATETIME_NOT_SELECTED) && (pi.PollOpenTo != DataHelper.DATETIME_NOT_SELECTED) && (pi.PollOpenTo > now))
            {
                toReturn = true;
            }
            else if ((pi.PollOpenFrom != DataHelper.DATETIME_NOT_SELECTED) && (pi.PollOpenTo != DataHelper.DATETIME_NOT_SELECTED) && (pi.PollOpenFrom < now) && (pi.PollOpenTo > now))
            {
                toReturn = true;
            }
        }

        return toReturn;
    }


    /// <summary>
    /// Logs activity
    /// </summary>
    /// <param name="pollId">Poll</param>
    /// <param name="answers">Answers</param>
    private void LogActivity(PollInfo pi, string answers)
    {
        string siteName = CMSContext.CurrentSiteName;
        if ((CMSContext.ViewMode != ViewModeEnum.LiveSite) || !IsLiveSite || (pi == null) || !pi.PollLogActivity || !ActivitySettingsHelper.ActivitiesEnabledForThisUser(CMSContext.CurrentUser)
            || !ActivitySettingsHelper.ActivitiesEnabledAndModuleLoaded(siteName) || !ActivitySettingsHelper.PollVotingEnabled(siteName))
        {
            return;
        }

        TreeNode currentDoc = CMSContext.CurrentDocument;
        var data = new ActivityData()
        {
            ContactID = ModuleCommands.OnlineMarketingGetCurrentContactID(),
            SiteID = CMSContext.CurrentSiteID,
            Type = PredefinedActivityType.POLL_VOTING,
            TitleData = pi.PollQuestion,
            ItemID = pi.PollID,
            URL = URLHelper.CurrentRelativePath,
            NodeID = (currentDoc != null ? currentDoc.NodeID : 0),
            Culture = (currentDoc != null ? currentDoc.DocumentCulture : null),
            Value = answers,
            Campaign = CMSContext.Campaign
        };
        ActivityLogProvider.LogActivity(data);
    }

    #endregion
}