using System;
using System.Data;
using System.Collections;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using CMS.FormControls;
using CMS.GlobalHelper;
using CMS.SettingsProvider;

public partial class CMSFormControls_Inputs_OrderBy : SqlFormControl
{
    /// <summary>
    /// Editing textbox
    /// </summary>
    protected override TextBox TextBoxControl
    {
        get
        {
            return txtOrder;
        }
    }


    /// <summary>
    /// Gets the regular expression for the safe value
    /// </summary>
    protected override Regex GetSafeRegEx()
    {
        return SecurityHelper.OrderByRegex;
    }


    protected new void Page_Load(object sender, EventArgs e)
    {
        CheckMinMaxLength = true;
        CheckRegularExpression = true;
    }
}