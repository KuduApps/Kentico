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
using CMS.FormControls;

public partial class CMSFormControls_Inputs_EncodingTextBox : FormEngineUserControl
{
    #region "Public properties"

    /// <summary>
    /// Gets or sets encoded textbox value.
    /// </summary>
    public override object Value
    {
        get
        {
            if (Trim)
            {
                return txtValue.Text.Trim();
            }
            else
            {
                return txtValue.Text;
            }
        }
        set
        {
            txtValue.Text = ValidationHelper.GetString(value, String.Empty);
        }
    }

    #endregion


    #region "Methods"


    protected void Page_Load(object sender, EventArgs e)
    {
        // Set trimming ability from form controls parameters
        this.Trim = ValidationHelper.GetBoolean(GetValue("trim"), false);

        CheckMinMaxLength = true;
        CheckRegularExpression = true;
    }

    #endregion
}
