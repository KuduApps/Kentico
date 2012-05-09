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

using CMS.GlobalHelper;
using CMS.ExtendedControls;


public partial class CMSAdminControls_ContentRating_Controls_Stars : AbstractRatingControl
{
    /// <summary>
    /// Enables/disables rating scale.
    /// </summary>
    public override bool Enabled
    {
        get
        {
            return !elemRating.ReadOnly;
        }
        set
        {
            elemRating.Enabled = value;
            elemRating.ReadOnly = !value;
        }
    }


    /// <summary>
    /// Returns current rating.
    /// </summary>
    public override double GetCurrentRating()
    {
        if (elemRating.MaxRating <= 0)
        {
            this.CurrentRating = 0;
        }
        else
        {
            this.CurrentRating = ValidationHelper.GetDouble(elemRating.CurrentRating, 0) / elemRating.MaxRating;
        }
        return this.CurrentRating;
    }


    /// <summary>
    /// Page load.
    /// </summary>
    protected void Page_Load(object sender, EventArgs e)
    {
        ReloadData();
    }


    public override void ReloadData()
    {
        // Avoid exception if max value is less or equal to zero
        if (this.MaxRating <= 0)
        {
            elemRating.Visible = false;
            return;
        }

        elemRating.MaxRating = this.MaxRating;
        // Clear stars to enable further rating
        if (this.Enabled && !this.ExternalManagement)
        {
            elemRating.CurrentRating = 0;
        }
        else
        {
            // Display rating result if in readonly mode
            elemRating.CurrentRating = Convert.ToInt32(Math.Round(this.CurrentRating * this.MaxRating, MidpointRounding.AwayFromZero));
        }

        elemRating.Changed += new AjaxControlToolkit.RatingEventHandler(elemRating_Changed);
        elemRating.AutoPostBack = true;

        // Switch RTL or LTR layout
        if (CultureHelper.IsPreferredCultureRTL())
        {
            elemRating.RatingDirection = AjaxControlToolkit.RatingDirection.RightToLeftBottomToTop;
        }
        else
        {
            elemRating.RatingDirection = AjaxControlToolkit.RatingDirection.LeftToRightTopToBottom;
        }
    }


    protected void elemRating_Changed(object sender, AjaxControlToolkit.RatingEventArgs e)
    {
        // Actualize CurrentRating properties
        elemRating.CurrentRating = ValidationHelper.GetInteger(e.Value, 0);
        this.GetCurrentRating();
        // Throw the rating event
        this.OnRating();
    }
}
