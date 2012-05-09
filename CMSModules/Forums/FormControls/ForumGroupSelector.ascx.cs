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

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.Forums;
using CMS.UIControls;
using CMS.SettingsProvider;
using CMS.SiteProvider;

public partial class CMSModules_Forums_FormControls_ForumGroupSelector : CMS.FormControls.FormEngineUserControl
{
    private int mSiteId = 0;


    #region "Properties"

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
            uniSelector.Enabled = value;
        }
    }


    /// <summary>
    /// Gets or sets field value.
    /// </summary>
    public override object Value
    {
        get
        {
            EnsureChildControls();
            return uniSelector.Value;
        }
        set
        {
            EnsureChildControls();
            uniSelector.Value = value;

        }
    }


    /// <summary>
    /// Gets or sets the value which determines whether to allow more than one user to select.
    /// </summary>
    public SelectionModeEnum SelectionMode
    {
        get
        {
            EnsureChildControls();
            return uniSelector.SelectionMode;
        }
        set
        {
            EnsureChildControls();
            uniSelector.SelectionMode = value;
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
            uniSelector.IsLiveSite = value;
        }
    }


    /// <summary>
    /// Gets the current uniselector instance.
    /// </summary>
    public UniSelector UniSelector
    {
        get
        {
            return this.uniSelector;
        }
    }


    /// <summary>
    /// Gets the single select drop down field.
    /// </summary>
    public DropDownList DropDownSingleSelect
    {
        get
        {
            return uniSelector.DropDownSingleSelect;
        }
    }


    /// <summary>
    /// Gets or sets the site id of site which forum groups will be displayed.
    /// </summary>
    public int SiteId
    {
        get
        {
            return mSiteId;
        }
        set
        {
            mSiteId = value;
        }
    }

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Set uniselector
        uniSelector.DisplayNameFormat = "{%GroupDisplayName%}";
        if (String.IsNullOrEmpty(uniSelector.ReturnColumnName))
        {
            uniSelector.ReturnColumnName = "GroupName";
        }
        uniSelector.AllowEmpty = false;
        uniSelector.AllowAll = false;

        // Set resource prefix based on mode
        if ((this.SelectionMode == SelectionModeEnum.Multiple) || (this.SelectionMode == SelectionModeEnum.MultipleButton) || (this.SelectionMode == SelectionModeEnum.MultipleTextBox))
        {
            uniSelector.ResourcePrefix = "forumgroupssel";
        }
        else
        {
            uniSelector.ResourcePrefix = "forumgroupsel";
        }

        SetupWhereCondition();
    }


    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (SiteId < 0)
        {
            this.Enabled = false;
        }

        if (URLHelper.IsPostback()
            && this.DependsOnAnotherField)
        {
            SetupWhereCondition();
            uniSelector.Reload(true);
            pnlUpdate.Update();
        }
    }


    /// <summary>
    /// Creates child controls and loads update panel container if it is required.
    /// </summary>
    protected override void CreateChildControls()
    {
        // If selector is not defined load updat panel container
        if (uniSelector == null)
        {
            this.pnlUpdate.LoadContainer();
        }
        // Call base method
        base.CreateChildControls();
    }


    /// <summary>
    /// Reloads the selector.
    /// </summary>
    /// <param name="forceLoad">Indicates whether data should be always loaded</param>
    public void Reload(bool forceLoad)
    {
        uniSelector.Reload(forceLoad);
    }


    private void SetupWhereCondition()
    {
        SetFormSiteId();

        // If SiteId not ste use current site
        int siteId = this.SiteId;
        if (siteId == 0)
        {
            siteId = CMSContext.CurrentSiteID;
        }

        if (siteId < 0)
        {
            uniSelector.Value = "";
        }

        // Set where condition - do not show groups forumgroups and adhoc forumsgroups
        uniSelector.WhereCondition = "GroupSiteID = " + siteId + " AND GroupGroupID IS NULL AND GroupName NOT LIKE 'AdHoc%'";
    }


    /// <summary>
    /// Sets the SiteId if the SiteName field is available in the form.
    /// </summary>
    private void SetFormSiteId()
    {
        if (this.DependsOnAnotherField
            && (this.Form != null)
            && this.Form.IsFieldAvailable("SiteName"))
        {
            string siteName = ValidationHelper.GetString(this.Form.GetFieldValue("SiteName"), null);
            if (!String.IsNullOrEmpty(siteName))
            {
                SiteInfo siteObj = SiteInfoProvider.GetSiteInfo(siteName);
                if (siteObj != null)
                {
                    SiteId = siteObj.SiteID;
                }
            }
            else
            {
                if (SiteId > 0)
                {
                    SiteId = 0;
                }
            }
        }
    }

    #endregion
}
