using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Globalization;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using CMS.FormControls;
using CMS.GlobalHelper;
using CMS.CMSHelper;

public partial class CMSFormControls_Viewers_ViewDate : FormEngineUserControl
{
    private DateTime mValue;

    #region "Public properties"

    /// <summary>
    /// Gets or sets field value.
    /// </summary>
    public override object Value
    {
        get
        {
            return mValue;
        }
        set
        {
            mValue = ValidationHelper.GetDateTime(value, DateTime.MinValue);
        }
    }

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Check if datetime is avaible
        if (mValue != DateTime.MinValue)
        {
            if (CMSContext.CurrentUser != null)
            {
                try
                {                    
                    // Try to print the date in user culture
                    CultureInfo ci = CultureInfo.CreateSpecificCulture(CMSContext.CurrentUser.PreferredCultureCode);                    
                    lblDate.Text = mValue.ToString("d", ci);
                    return;
                }
                catch (ArgumentException)
                {
                    // Can't get culture
                }
            }

            // Default print
            lblDate.Text = mValue.ToString("d");
        }
        else
        {
            lblDate.Text = GetString("general.na");
        }
    }

    #endregion
}
