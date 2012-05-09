using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.UI;

using CMS.PortalControls;
using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.FormControls;
using CMS.SiteProvider;
using CMS.TreeEngine;
using CMS.LicenseProvider;

public partial class CMSWebParts_ContentRating_ContentRating : CMSAbstractWebPart
{
    #region "Properties"

    /// <summary>
    /// Gets or sets max value of scale.
    /// </summary>
    public int MaxRatingValue
    {
        get
        {
            return ValidationHelper.GetInteger(this.GetValue("MaxRatingValue"), elemRating.MaxRatingValue);
        }
        set
        {
            this.SetValue("MaxRatingValue", value);
        }
    }


    /// <summary>
    /// Gets or sets value that indicates whether unrated value is allowed.
    /// </summary>
    public bool AllowZeroValue
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("AllowZeroValue"), elemRating.AllowZeroValue);
        }
        set {
            this.SetValue("AllowZeroValue", value);
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
            return ValidationHelper.GetString(this.GetValue("ExternalValue"), elemRating.ExternalValue);
        }
        set
        {
            this.SetValue("ExternalValue", value);
        }
    }


    /// <summary>
    /// Code name of control that manages rating scale.
    /// </summary>
    public string RatingType
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("RatingType"), elemRating.RatingType);
        }
        set
        {
            this.SetValue("RatingType", value);
        }
    }


    /// <summary>
    /// If true the brief result info is shown.
    /// </summary>
    public bool ShowResultMessage
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("ShowResultMessage"), elemRating.ShowResultMessage);
        }
        set
        {
            this.SetValue("ShowResultMessage", value);
        }
    }


    /// <summary>
    /// Gets or sets result info message.
    /// </summary>
    public string ResultMessage
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("ResultMessage"), elemRating.ResultMessage);
        }
        set
        {
            this.SetValue("ResultMessage", value);
        }
    }


    /// <summary>
    /// Gets or sets message that is displayed after rating.
    /// </summary>
    public string MessageAfterRating
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("MessageAfterRating"), elemRating.MessageAfterRating);
        }
        set
        {
            this.SetValue("MessageAfterRating", value);
        }
    }


    /// <summary>
    /// Gets or sets message that is displayed when user forgot to rate.
    /// </summary>
    public string ErrorMessage
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("ErrorMessage"), elemRating.ErrorMessage);
        }
        set
        {
            this.SetValue("ErrorMessage", value);
        }
    }


    /// <summary>
    /// Gets or sets value that indicates wheter rating is allowed for public users.
    /// </summary>
    public bool AllowForPublic
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("AllowForPublic"), elemRating.AllowForPublic);
        }
        set
        {
            this.SetValue("AllowForPublic", value);
        }
    }


    /// <summary>
    /// Enables/disables checking if user rated.
    /// </summary>
    public bool CheckIfUserRated
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("CheckIfUserRated"), elemRating.CheckIfUserRated);
        }
        set
        {
            this.SetValue("CheckIfUserRated", value);
        }
    }


    /// <summary>
    /// If true, the control is hidden when user is not authorized.
    /// </summary>
    public bool HideToUnauthorizedUsers
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("HideToUnauthorizedUsers"), elemRating.HideToUnauthorizedUsers);
        }
        set
        {
            this.SetValue("HideToUnauthorizedUsers", value);
        }
    }


    /// <summary>
    /// Gets or sets the value that determines whether permissions are checked.
    /// </summary>
    public bool CheckPermissions
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("CheckPermissions"), elemRating.CheckPermissions);
        }
        set
        {
            this.SetValue("CheckPermissions", value);
            elemRating.CheckPermissions = value;
        }
    }

    #endregion


    #region "Methods"

    /// <summary>
    /// Content loaded event handler.
    /// </summary>
    public override void OnContentLoaded()
    {
        base.OnContentLoaded();
        SetupControl();
    }


    /// <summary>
    /// Initializes the control properties.
    /// </summary>
    protected void SetupControl()
    {
        if (this.StopProcessing)
        {
            elemRating.StopProcessing = true;
        }
        else
        {
            elemRating.MaxRatingValue = this.MaxRatingValue;
            elemRating.RatingType = this.RatingType;
            elemRating.ShowResultMessage = this.ShowResultMessage;
            elemRating.ResultMessage = this.ResultMessage;
            elemRating.MessageAfterRating = this.MessageAfterRating;
            elemRating.AllowForPublic = this.AllowForPublic;
            elemRating.CheckIfUserRated = this.CheckIfUserRated;
            elemRating.HideToUnauthorizedUsers = this.HideToUnauthorizedUsers;
            elemRating.CheckPermissions = this.CheckPermissions;
            elemRating.ExternalValue = this.ExternalValue;
            elemRating.AllowZeroValue = this.AllowZeroValue;
            elemRating.ErrorMessage = this.ErrorMessage;
        }
    }


    /// <summary>
    /// Reload data.
    /// </summary>
    public override void ReloadData()
    {
        base.ReloadData();
        SetupControl();
    }

    #endregion
}
