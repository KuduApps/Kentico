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
using CMS.FormEngine;
using CMS.SettingsProvider;
using CMS.LicenseProvider;
using CMS.PortalEngine;
using CMS.CMSHelper;

public partial class CMSFormControls_VisibilityControls_RadioButtonsHorizontalVisibility : FormEngineUserControl
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
            rblVisibility.Enabled = value;
        }
    }


    /// <summary>
    /// Gets or sets field value.
    /// </summary>
    public override object Value
    {
        get
        {
            return ValidationHelper.GetString(rblVisibility.SelectedValue, null);
        }
        set
        {
            this.EnsureChildControls();
            rblVisibility.SelectedValue = ValidationHelper.GetString(value, null);
        }
    }

    #endregion


    #region "Methods"

    protected override void CreateChildControls()
    {
        base.CreateChildControls();
        if (!StopProcessing)
        {
            ReloadData();
        }
    }


    /// <summary>
    /// Loads the child controls at run-time.
    /// </summary>
    private void ReloadData()
    {
        rblVisibility.Items.Add(new ListItem(GetString("visibilityrad.nobody"), FormFieldVisibilityTypeCode.NONE));
        rblVisibility.Items.Add(new ListItem(GetString("visibilityrad.all"), FormFieldVisibilityTypeCode.ALL));
        rblVisibility.Items.Add(new ListItem(GetString("visibilityrad.sitemembers"), FormFieldVisibilityTypeCode.AUTHENTICATED));

        // Add friends if friends feature is available
        if (LicenseHelper.CheckFeature(URLHelper.GetCurrentDomain(), FeatureEnum.Friends))
        {
            if (CMSContext.ViewMode != ViewModeEnum.LiveSite || UIHelper.IsFriendsModuleEnabled(CMSContext.CurrentSiteName))
            {
                rblVisibility.Items.Add(new ListItem(GetString("visibilityrad.friends"), FormFieldVisibilityTypeCode.FRIENDS));
            }
        }
    }

    #endregion
}