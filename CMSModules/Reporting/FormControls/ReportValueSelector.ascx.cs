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
using CMS.Reporting;

public partial class CMSModules_Reporting_FormControls_ReportValueSelector : FormEngineUserControl
{
    #region "Public properties"

    /// <summary>
    /// Gets or sets field value.
    /// </summary>
    public override object Value
    {
        get
        {
            return brsItems.Value;
        }
        set
        {
            brsItems.Form = this.Form;
            brsItems.Value = value;
        }
    }

    #endregion


    #region "Methods"
 
    protected void Page_Load(object sender, EventArgs e)
    {
        brsItems.ReportType = ReportItemType.Value;
        brsItems.IsLiveSite = IsLiveSite;        
    }


    /// <summary>
    /// Returns true if entered data is valid. If data is invalid, it returns false and displays an error message.
    /// </summary>
    public override bool IsValid()
    {
        return brsItems.IsValid();
    }


    /// <summary>
    /// Returns an array of values of any other fields returned by the control.
    /// </summary>
    /// <remarks>It returns an array where first dimension is attribute name and the second dimension is its value.</remarks>
    public override object[,] GetOtherValues()
    {
        return brsItems.GetOtherValues();
    }

    #endregion
}
