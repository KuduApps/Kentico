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

using CMS.FormControls;
using CMS.GlobalHelper;

public partial class CMSFormControls_Viewers_ViewSecureText : FormEngineUserControl
{
    private string mValue = String.Empty;

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
            mValue = ValidationHelper.GetString(value, string.Empty);
        }
    }

    #endregion


    #region "Methods"

    protected void Page_PreRender(object sender, EventArgs e)
    {
        lblText.Text = HTMLHelper.HTMLEncode(ValidationHelper.GetString(this.Value, null));
    }

    #endregion
}
