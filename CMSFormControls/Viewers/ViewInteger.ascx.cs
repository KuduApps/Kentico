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

public partial class CMSFormControls_Viewers_ViewInteger : FormEngineUserControl
{
    private int mValue = 0;

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
            mValue = ValidationHelper.GetInteger(value, 0);
        }
    }

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        lblNumber.Text = mValue.ToString();
    }

    #endregion
}
