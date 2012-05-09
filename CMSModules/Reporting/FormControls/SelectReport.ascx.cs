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
using CMS.CMSHelper;
using CMS.DataEngine;
using CMS.FormControls;
using CMS.UIControls;
using CMS.SettingsProvider;

public partial class CMSModules_Reporting_FormControls_SelectReport : FormEngineUserControl
{
    #region "Variables"

    private string mResourcePrefix = String.Empty;

    #endregion


    #region "Public properties"

    /// <summary>
    /// Gets or sets the text displayed if there are no data.
    /// </summary>
    public string ZeroRowsText
    {
        get
        {
            EnsureChildControls();
            return usReports.ZeroRowsText;
        }
        set
        {
            EnsureChildControls();
            usReports.ZeroRowsText = value;
        }
    }



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
            EnsureChildControls();

            base.Enabled = value;
            usReports.Enabled = value;
        }
    }


    ///<summary>
    /// Gets or sets field value.
    ///</summary>
    public override object Value
    {
        get
        {
            EnsureChildControls();
            return usReports.Value;
        }
        set
        {
            EnsureChildControls();
            usReports.Value = value;
        }
    }


    /// <summary>
    /// Gets the current UniSelector instance.
    /// </summary>
    public UniSelector UniSelector
    {
        get
        {
            EnsureChildControls();
            return usReports;
        }
    }


    /// <summary>
    /// Gets or sets the resource prefix of uni selector. If not set default values are used.
    /// </summary>
    public string ResourcePrefix
    {
        get
        {
            return mResourcePrefix;
        }
        set
        {
            mResourcePrefix = value;
            usReports.ResourcePrefix = value;
        }
    }


    /// <summary>
    /// Indicates if control is used on live site.
    /// </summary>
    public override bool IsLiveSite
    {
        get
        {
            return base.IsLiveSite;
        }
        set
        {
            EnsureChildControls();
            base.IsLiveSite = value;
            usReports.IsLiveSite = value;
        }
    }


    /// <summary>
    /// Gets ClientID of the dropdownlist with reports.
    /// </summary>
    public override string ValueElementID
    {
        get
        {
            return usReports.ClientID;
        }
    }

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Set prefix if not set
        if (ResourcePrefix == String.Empty)
        {
            usReports.ResourcePrefix = "selectreport";
        }

        usReports.FilterControl = "~/CMSModules/Reporting/FormControls/FilterReportCategory.ascx";        
    }


    /// <summary>
    /// Creates child controls and loads update panle container if it is required.
    /// </summary>
    protected override void CreateChildControls()
    {
        // If selector is not defined load updat panel container
        if (usReports == null)
        {
            pnlUpdate.LoadContainer();
        }
        // Call base method
        base.CreateChildControls();
    }


    /// <summary>
    /// Reloads the data of the UniSelector.
    /// </summary>
    public void ReloadData()
    {
        usReports.Reload(true);
    }

    #endregion
}
