using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.Newsletter;

public partial class CMSModules_Newsletters_FormControls_NewsletterTemplateSelector : CMS.FormControls.FormEngineUserControl
{
    #region "Properties"

    /// <summary>
    /// Gets or sets the enabled state of the control.
    /// </summary>
    public override bool Enabled
    {
        get
        {
            return this.uniNewsletterTemplate.Enabled;
        }
        set
        {
            this.uniNewsletterTemplate.Enabled = value;
        }
    }


    /// <summary>
    /// Gets or sets field value.
    /// </summary>
    public override object Value
    {
        get
        {
            return uniNewsletterTemplate.Value;
        }
        set
        {
            this.EnsureChildControls();
            this.uniNewsletterTemplate.Value = value;
        }
    }


    /// <summary>
    /// Gets or sets WHERE condition.
    /// </summary>
    public string WhereCondition
    {
        get
        {
            return this.uniNewsletterTemplate.WhereCondition;
        }
        set
        {
            this.uniNewsletterTemplate.WhereCondition = value;
        }
    }

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        this.uniNewsletterTemplate.OrderBy = "TemplateDisplayName";
    }


    /// <summary>
    /// Reloads the selector's data.
    /// </summary>
    /// <param name="forceReload">Indicates whether data should be forcibly reloaded</param>
    public void Reload(bool forceReload)
    {
        uniNewsletterTemplate.Reload(forceReload);
    }

    #endregion
}
