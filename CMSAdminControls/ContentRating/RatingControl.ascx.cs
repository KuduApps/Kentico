using System;
using System.Web.UI;

using CMS.CMSHelper;
using CMS.ExtendedControls;
using CMS.GlobalHelper;
using CMS.SettingsProvider;
using CMS.SiteProvider;
using CMS.TreeEngine;
using CMS.UIControls;
using CMS.WebAnalytics;
using CMS.PortalEngine;

using TreeNode = CMS.TreeEngine.TreeNode;

public partial class CMSAdminControls_ContentRating_RatingControl : CMSUserControl
{
    #region "Private variables"

    private AbstractRatingControl usrControl = null;
    private int mMaxRatingValue = 5;
    private double mExternalValue = -1.0;
    private string mRatingType = "Stars";
    private bool mShowResultMessage = false;
    private string mResultMessage = null;
    private string mErrorMessage = null;
    private string mMessageAfterRating = null;
    private bool mAllowForPublic = true;
    private bool mCheckIfUserRated = true;
    private bool mHideToUnauthorized = false;
    private bool mCheckPermissions = true;
    private bool mAllowZeroValue = true;
    private bool mEnabled = true;

    #endregion


    #region "Public properties"

    /// <summary>
    /// Gets or sets max value of scale.
    /// </summary>
    public int MaxRatingValue
    {
        get
        {
            return mMaxRatingValue;
        }
        set
        {
            mMaxRatingValue = value;
        }
    }


    /// <summary>
    /// Gets or sets value that indicates whether unrated value is allowed.
    /// </summary>
    public bool AllowZeroValue
    {
        get
        {
            return mAllowZeroValue;
        }
        set
        {
            mAllowZeroValue = value;
        }
    }


    /// <summary>
    /// Gets or sets current value. If value is negative number then document
    /// rating is used.
    /// </summary>
    public string ExternalValue
    {
        get
        {
            return mExternalValue.ToString();
        }
        set
        {
            mExternalValue = ValidationHelper.GetDouble(value, -1.0);
        }
    }


    /// <summary>
    /// Code name of form control that manages rating scale.
    /// </summary>
    public string RatingType
    {
        get
        {
            return mRatingType;
        }
        set
        {
            mRatingType = value;
        }
    }


    /// <summary>
    /// If true the brief result info is shown.
    /// </summary>
    public bool ShowResultMessage
    {
        get
        {
            return mShowResultMessage;
        }
        set
        {
            mShowResultMessage = value;
        }
    }


    /// <summary>
    /// Gets or sets result info message that is displayed after rating.
    /// </summary>
    public string ResultMessage
    {
        get
        {
            return mResultMessage;
        }
        set
        {
            mResultMessage = value;
        }
    }


    /// <summary>
    /// Gets or sets message that is displayed after rating.
    /// </summary>
    public string MessageAfterRating
    {
        get
        {
            return mMessageAfterRating;
        }
        set
        {
            mMessageAfterRating = value;
        }
    }


    /// <summary>
    /// Gets or sets message that is displayed when user forgot to rate.
    /// </summary>
    public string ErrorMessage
    {
        get
        {
            return mErrorMessage;
        }
        set
        {
            mErrorMessage = value;
        }
    }


    /// <summary>
    /// Gets or sets value that indicates whether rating is allowed for public users.
    /// </summary>
    public bool AllowForPublic
    {
        get
        {
            return mAllowForPublic;
        }
        set
        {
            mAllowForPublic = value;
        }
    }


    /// <summary>
    /// Enables/disables checking if user voted.
    /// </summary>
    public bool CheckIfUserRated
    {
        get
        {
            return mCheckIfUserRated;
        }
        set
        {
            mCheckIfUserRated = value;
        }
    }


    /// <summary>
    /// If true, the control hides when user is not authorized.
    /// </summary>
    public bool HideToUnauthorizedUsers
    {
        get
        {
            return mHideToUnauthorized;
        }
        set
        {
            mHideToUnauthorized = value;
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
    /// Enables/disables rating control 
    /// </summary>
    public bool Enabled
    {
        get
        {
            return mEnabled;
        }
        set
        {
            mEnabled = value;
        }
    }
    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        ReloadData();
    }


    /// <summary>
    /// Occures on rating event.
    /// </summary>
    protected void usrControl_RatingEvent(AbstractRatingControl sender)
    {
        // Check if control is enabled
        if (!(this.Enabled && HasPermissions() && !(this.CheckIfUserRated && TreeProvider.HasRated(CMSContext.CurrentDocument))))
        {
            return;
        }

        // Check banned ip
        if (!BannedIPInfoProvider.IsAllowed(CMSContext.CurrentSiteName, BanControlEnum.AllNonComplete))
        {
            pnlError.Visible = true;
            lblError.Text = GetString("general.bannedip");
            return;
        }

        // Check null value
        if (!this.AllowZeroValue && usrControl.CurrentRating <= 0)
        {
            pnlError.Visible = true;
            lblError.Text = this.ErrorMessage;
            return;
        }

        if (CMSContext.CurrentDocument != null)
        {
            // Check whether user has already rated
            if (this.CheckIfUserRated && TreeProvider.HasRated(CMSContext.CurrentDocument))
            {
                return;
            }

            // Update document rating, remember rating in cookie if required
            TreeProvider.AddRating(CMSContext.CurrentDocument, usrControl.CurrentRating, this.CheckIfUserRated);
            // Get absolute rating value of the current rating
            double currRating = usrControl.MaxRating * usrControl.CurrentRating;
            // Reload rating control
            ReloadData();
            // Show message after rating if enabled or set
            if (!string.IsNullOrEmpty(this.MessageAfterRating))
            {
                pnlMessage.Visible = true;
                // Merge message text with rating values
                lblMessage.Text = String.Format(this.MessageAfterRating,
                    Convert.ToInt32(currRating), usrControl.CurrentRating * usrControl.MaxRating, CMSContext.CurrentDocument.DocumentRatings);
            }
            else
            {
                pnlMessage.Visible = false;
            }

            // log activity
            LogActivity(usrControl.CurrentRating);
        }
    }


    /// <summary>
    /// Reload all values.
    /// </summary>
    public void ReloadData()
    {
        if (this.StopProcessing)
        {
            return;
        }

        // Check permissions
        if (this.HideToUnauthorizedUsers && !HasPermissions())
        {
            this.Visible = false;
            return;
        }

        if (CMSContext.CurrentDocument != null)
        {
            try
            {
                // Insert rating control to page
                usrControl = (AbstractRatingControl)(this.Page.LoadControl(AbstractRatingControl.GetRatingControlUrl(this.RatingType + ".ascx")));
            }
            catch (Exception e)
            {
                this.Controls.Add(new LiteralControl(e.Message));
                return;
            }

            double rating = 0.0f;
            
            // Use current document rating if external value is not used
            if (mExternalValue < 0)
            {
                if (CMSContext.CurrentDocument.DocumentRatings > 0)
                {
                    rating = CMSContext.CurrentDocument.DocumentRatingValue / CMSContext.CurrentDocument.DocumentRatings;
                }
            }
            else
            {
                rating = mExternalValue;
            }

            // Check allowed interval 0.0-1.0
            if ((rating < 0.0) || (rating > 1.0))
            {
                rating = 0.0;
            }

            // Init values
            usrControl.ID = "RatingControl";
            usrControl.MaxRating = this.MaxRatingValue;
            usrControl.CurrentRating = rating;
            usrControl.Visible = true;
            usrControl.Enabled = this.Enabled && HasPermissions() && !(this.CheckIfUserRated && TreeProvider.HasRated(CMSContext.CurrentDocument));

            RefreshResultMessage();

            usrControl.RatingEvent += new AbstractRatingControl.OnRatingEventHandler(usrControl_RatingEvent);
            pnlRating.Controls.Clear();
            pnlRating.Controls.Add(usrControl);
        }
    }


    /// <summary>
    /// Refreshes result info message.
    /// </summary>
    private void RefreshResultMessage()
    {
        if (this.ShowResultMessage && (!string.IsNullOrEmpty(this.ResultMessage)))
        {
            pnlResult.Visible = true;
            // Merge result text with rating values
            lblResult.Text = String.Format(this.ResultMessage, usrControl.CurrentRating * usrControl.MaxRating, CMSContext.CurrentDocument.DocumentRatings);
        }
        else
        {
            pnlResult.Visible = false;
        }
    }


    /// <summary>
    /// Returns true if user has permissions to access to the rating control.
    /// </summary>
    private bool HasPermissions()
    {
        if (!this.CheckPermissions || CMSContext.CurrentUser.IsGlobalAdministrator)
        {
            return true;
        }

        if (this.AllowForPublic && CMSContext.CurrentUser.IsPublic())
        {
            return true;
        }

        if (!CMSContext.CurrentUser.IsPublic())
        {
            return true;
        }

        return false;
    }


    /// <summary>
    /// Logs rating activity
    /// </summary>
    /// <param name="value">Rating value</param>
    private void LogActivity(double value)
    {
        if ((CMSContext.ViewMode != ViewModeEnum.LiveSite) || !ActivitySettingsHelper.ActivitiesEnabledForThisUser(CMSContext.CurrentUser))
        {
            return;
        }

        string siteName = CMSContext.CurrentSiteName;
        if (!ActivitySettingsHelper.ActivitiesEnabledAndModuleLoaded(siteName) || !ActivitySettingsHelper.ContentRatingEnabled(siteName))
        {
            return;
        }

        bool logActivity = false;
        TreeNode currentDoc = CMSContext.CurrentDocument;
        if (currentDoc != null)
        {
            if (CMSContext.CurrentDocument.DocumentLogVisitActivity == null)
            {
                logActivity = ValidationHelper.GetBoolean(currentDoc.GetInheritedValue("DocumentLogVisitActivity", SiteInfoProvider.CombineWithDefaultCulture(siteName)), false);
            }
            else
            {
                logActivity = currentDoc.DocumentLogVisitActivity == true;
            }

            if (logActivity)
            {
                var data = new ActivityData()
                {
                    ContactID = ModuleCommands.OnlineMarketingGetCurrentContactID(),
                    SiteID = CMSContext.CurrentSiteID,
                    Type = PredefinedActivityType.RATING,
                    TitleData = String.Format("{0} ({1})", value.ToString(), currentDoc.DocumentName),
                    URL = URLHelper.CurrentRelativePath,
                    NodeID = currentDoc.NodeID,
                    Value = value.ToString(),
                    Culture = currentDoc.DocumentCulture,
                    Campaign = CMSContext.Campaign
                };
                ActivityLogProvider.LogActivity(data);
            }
        }
    }

    #endregion
}
