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

public partial class CMSAdminControls_ContentRating_Controls_RadioButtons : AbstractRatingControl
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
        }
    }

    #endregion


    #region "Public methods"

    /// <summary>
    /// Rerturns current rating.
    /// </summary>
    public override double GetCurrentRating()
    {
        this.CurrentRating = 0;

        // Check for division by zero
        if (this.MaxRating > 0)
        {
            // Loop through entire control collection and find checked radio button
            foreach (Control cntrl in plcContent.Controls)
            {
                if ((cntrl is RadioButton) && (((RadioButton)cntrl).Checked))
                {
                    int tmp = ValidationHelper.GetInteger(cntrl.ID.Substring(7), -1);
                    if (tmp > 0)
                    {
                        this.CurrentRating = (double)tmp / this.MaxRating;
                        break;
                    }
                }
            }
        }

        return this.CurrentRating;
    }

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        ReloadData();
    }


    public override void ReloadData()
    {
        int currPos = Convert.ToInt32(Math.Round(this.CurrentRating * this.MaxRating, MidpointRounding.AwayFromZero));
        plcContent.Controls.Clear();
        plcContent.Controls.Add(new LiteralControl("<table class=\"CntRatingRadioTable\"><tr>\n"));

        // Create radio buttons
        for (int i = 1; i <= this.MaxRating; i++)
        {
            plcContent.Controls.Add(new LiteralControl("<td>"));

            RadioButton radBtn = new RadioButton();
            radBtn.ID = "radBtn_" + Convert.ToString(i);
            radBtn.Enabled = this.Enabled;
            if (!this.Enabled)
            {
                radBtn.Checked = i == currPos;
            }
            radBtn.GroupName = this.ClientID;
            plcContent.Controls.Add(radBtn);

            // WAI validation
            LocalizedLabel lbl = new LocalizedLabel();
            lbl.Display = false;
            lbl.EnableViewState = false;
            lbl.ResourceString = "general.rating";
            lbl.AssociatedControlID = radBtn.ID;

            plcContent.Controls.Add(lbl);
            plcContent.Controls.Add(new LiteralControl("</td>"));
        }
        plcContent.Controls.Add(new LiteralControl("\n</tr>\n<tr>\n"));

        // Create labels
        for (int i = 1; i <= this.MaxRating; i++)
        {
            plcContent.Controls.Add(new LiteralControl("<td>" + i.ToString() + "</td>"));
        }
        plcContent.Controls.Add(new LiteralControl("\n</tr>\n</table>"));

        if (this.Enabled)
        {
            btnSubmit.Text = ResHelper.GetString("general.ok");
            btnSubmit.Click += new EventHandler(btnSubmit_Click);
        }

        // Hide button when control is disabled or external management is used
        btnSubmit.Visible = this.Enabled && !this.ExternalManagement;
    }


    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        // Actualize CurrentRating property
        this.GetCurrentRating();
        // Throw the rating event
        this.OnRating();
    }

    #endregion
}
