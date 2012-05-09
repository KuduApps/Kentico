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
using CMS.SiteProvider;
using CMS.DataEngine;
using CMS.SettingsProvider;
using CMS.FormControls;

public partial class CMSFormControls_System_CacheDependencies : FormEngineUserControl
{
    /// <summary>
    /// Gets or sets field value.
    /// </summary>
    public override object Value
    {
        get
        {
            if (this.chkDependencies.Checked)
            {
                // Return together with default ones
                return CacheHelper.DEFAULT_CACHE_DEPENDENCIES + "\n" + this.txtDependencies.Value;
            }
            else
            {
                if ((string)this.txtDependencies.Value == "")
                {
                    // No dependencies
                    return CacheHelper.NO_CACHE_DEPENDENCIES;
                }
                else
                {
                    // Only specific dependencies
                    return this.txtDependencies.Value;
                }
            }
        }
        set
        {
            string val = (string)value;
            if (val == null)
            {
                // Default cache dependencies
                this.chkDependencies.Checked = true;
                this.txtDependencies.TextArea.Text = "";
            }
            else if (val == CacheHelper.NO_CACHE_DEPENDENCIES)
            {
                // No cache dependencies
                this.chkDependencies.Checked = false;
                this.txtDependencies.TextArea.Text = "";
            }
            else
            {
                // Check if default is applied
                if (val.Contains(CacheHelper.DEFAULT_CACHE_DEPENDENCIES))
                {
                    this.chkDependencies.Checked = true;
                    val = val.Replace(CacheHelper.DEFAULT_CACHE_DEPENDENCIES, "").Trim();
                }

                this.txtDependencies.TextArea.Text = val;
            }
        }
    }


    /// <summary>
    /// Gets ClientID of the text area.
    /// </summary>
    public override string ValueElementID
    {
        get
        {
            return txtDependencies.ValueElementID;
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        this.chkDependencies.Text = GetString("CacheDependencies.UseCacheDependencies");
    }
}
