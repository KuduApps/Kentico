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

public partial class CMSAdminControls_ContentRating_Controls_DropDown : AbstractRatingControl
{
    #region "Public properties"

    /// <summary>
    /// Enables/disables rating scale
    /// </summary>
    public override bool Enabled
    {
        get
        {
            return btnSubmit.Enabled;
        }
        set
        {
            btnSubmit.Enabled = value;
            drpRatings.Enabled = value;
        }
    }

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        ReloadData();
    }


    /// <summary>
    /// Returns current rating.
    /// </summary>
    public override double GetCurrentRating()
    {
        if (this.MaxRating <= 0)
        {
            this.CurrentRating = 0;
        }
        else
        {
            this.CurrentRating = (double)ValidationHelper.GetInteger(drpRatings.SelectedValue, 0) / this.MaxRating;
        }

        return this.CurrentRating;
    }


    public override void ReloadData()
    {
        drpRatings.Items.Clear();

        // Insert '(none)' when external management is used (the ability to send message without rating)
        if (this.ExternalManagement)
        {
            drpRatings.Items.Add(new ListItem(ResHelper.GetString("general.selectnone"), "0"));
        }

        int currPos = Convert.ToInt32(Math.Round(this.CurrentRating * this.MaxRating, MidpointRounding.AwayFromZero));
        for (int i = 1; i <= this.MaxRating; i++)
        {
            drpRatings.Items.Add(new ListItem(i.ToString(), i.ToString()));
            if (i == currPos)
            {
                drpRatings.SelectedIndex = drpRatings.Items.Count - 1;
            }
        }

        if (this.Enabled)
        {
            btnSubmit.Text = ResHelper.GetString("general.ok");
            btnSubmit.Click += new EventHandler(btnSubmit_Click);
        }

        // Hide button when control is disabled or external management is used
        btnSubmit.Visible = this.Enabled && !this.ExternalManagement;
    }


    private void btnSubmit_Click(object sender, EventArgs e)
    {
        // Actualize CurrentRating property
        this.GetCurrentRating();
        // Throw the rating event
        this.OnRating();
    }

    #endregion
}
