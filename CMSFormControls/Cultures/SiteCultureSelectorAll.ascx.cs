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
using CMS.PortalControls;

public partial class CMSFormControls_Cultures_SiteCultureSelectorAll : FormEngineUserControl
{
    /// <summary>
    /// Gets or sets the field value.
    /// </summary>
    public override object Value
    {
        get
        {
            return this.SiteCultureSelector.Value;
        }
        set
        {
            this.SiteCultureSelector.Value = value;
        }
    }


    /// <summary>
    /// Page load.
    /// </summary>
    protected void Page_Load(object sender, EventArgs e)
    {
        // Ensure the script manager
        PortalHelper.EnsureScriptManager(this.Page);

        if (this.StopProcessing)
        {
            this.SiteCultureSelector.StopProcessing = true;
        }
        else
        {
            SiteCultureSelector.ReloadData();            
        }
    }
}
