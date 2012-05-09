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

public partial class CMSFormControls_Inputs_InternationalPhone : CMS.FormControls.FormEngineUserControl
{
    /// <summary>
    /// Gets or sets the enabled state of the control.
    /// </summary>
    public override bool Enabled
    {
        get
        {
            return base.Enabled;
        }
        set
        {
            base.Enabled = value;
            this.txt1st.Enabled = value;
            this.txt2nd.Enabled = value;
        }
    }


    /// <summary>
    /// Gets or sets field value.
    /// </summary>
    public override object Value
    {
        get
        {
            if (DataHelper.IsEmpty(txt1st.Text) && DataHelper.IsEmpty(txt2nd.Text))
            {
                return "";
            }
            return "+" + txt1st.Text + " " + txt2nd.Text;
        }
        set
        {
            string number = (string)value;
            // Parse numbers from incoming string.
            if ((number != null) && (number != ""))
            {
                txt1st.Text = number.Substring(1, number.IndexOf(" ") - 1);
                txt2nd.Text = number.Substring(number.IndexOf(" ") + 1, number.Length - number.IndexOf(" ") - 1);
            }
        }
    }


    /// <summary>
    /// Page load.
    /// </summary>
    protected void Page_Load(object sender, EventArgs e)
    {
    }


    /// <summary>
    /// Returns true if user control is valid.
    /// </summary>
    public override bool IsValid()
    {
        if ((txt1st.Text != "") || (txt2nd.Text != ""))
        {
            Validator val = new Validator();
            // International phone number must be in the form '+d{1-4} d{1-20}' where 'd' is digit.
            string result = val.IsRegularExp(txt1st.Text, @"\d{1,4}", "error").IsRegularExp(txt2nd.Text, @"\d[0-9\s]{1,18}\d", "error").Result;

            if (result != "")
            {
                this.ValidationError = GetString("InternationalPhone.ValidationError");
                return false;
            }
        }
        return true;
    }
}
