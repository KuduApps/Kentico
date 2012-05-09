using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.FormControls;
using CMS.Reporting;


public partial class CMSModules_Reporting_FormControls_ReportSelector : FormEngineUserControl
{

    /// <summary>
    /// Gets or sets field value.
    /// </summary>
    public override object Value
    {
        get
        {
            return ucItemSelector.Value;
        }
        set
        {
            ucItemSelector.Form = this.Form;
            ucItemSelector.Value = value;
        }
    }


    /// <summary>
    /// Returns true if entered data is valid. If data is invalid, it returns false and displays an error message.
    /// </summary>
    public override bool IsValid()
    {
        return ucItemSelector.IsValid();
    }


    /// <summary>
    /// Returns an array of values of any other fields returned by the control.
    /// </summary>
    /// <remarks>It returns an array where first dimension is attribute name and the second dimension is its value.</remarks>
    public override object[,] GetOtherValues()
    {
        return ucItemSelector.GetOtherValues();
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        ucItemSelector.ReportType = ReportItemType.All;
        ucItemSelector.IsLiveSite = IsLiveSite;     
    }
}
