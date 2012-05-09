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
using CMS.SiteProvider;
using CMS.CMSHelper;

public partial class CMSModules_Forums_FormControls_GroupSelector : CMS.FormControls.FormEngineUserControl
{
    #region "Public properties"

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
            base.Enabled = value;
            selectGroup.Enabled = value;
        }
    }


    /// <summary>
    /// Gets or sets field value.
    /// </summary>
    public override object Value
    {
        get
        {
            return selectGroup.Value;
        }
        set
        {
            selectGroup.Value = value;
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
            base.IsLiveSite = value;
            selectGroup.IsLiveSite = value;
        }
    }

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.DependsOnAnotherField)
        {
            selectGroup.DependsOnAnotherField = this.DependsOnAnotherField;
            selectGroup.SiteId = GetFormSiteId();
            selectGroup.Form = this.Form;
        }
    }


    /// <summary>
    /// Gets the SiteId if the SiteName field is available in the form.
    /// </summary>
    private int GetFormSiteId()
    {
        if (this.DependsOnAnotherField
            && (this.Form != null)
            && this.Form.IsFieldAvailable("SiteName"))
        {
            string siteName = ValidationHelper.GetString(this.Form.GetFieldValue("SiteName"), "");
            if (String.IsNullOrEmpty(siteName) || siteName.Equals("##all##", StringComparison.InvariantCultureIgnoreCase))
            {
                return -1;
            }
            else if (siteName.Equals("##currentsite##", StringComparison.InvariantCultureIgnoreCase))
            {
                siteName = CMSContext.CurrentSiteName;
            }

            SiteInfo siteObj = SiteInfoProvider.GetSiteInfo(siteName);
            if (siteObj != null)
            {
                return siteObj.SiteID;
            }
        }

        return CMSContext.CurrentSiteID;
    }
}
