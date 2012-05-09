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
using CMS.UIControls;
using CMS.SettingsProvider;
using CMS.CMSHelper;

public partial class CMSFormControls_SelectCssStylesheet : CMS.FormControls.FormEngineUserControl
{
    #region "Private variables"

    private string mStylesheetCodeName = String.Empty;
    private int mSiteId = 0;
    private bool mAllowEditButtons = true;
    private string mReturnColumnName = "StylesheetName";

    #endregion


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
            EnsureChildControls();
            base.Enabled = value;
            this.usStyleSheet.Enabled = value;
        }
    }


    /// <summary>
    /// Indicates whether "default" record should be added to the dropdownlist.
    /// </summary>
    public bool AddNoneRecord
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("adddefaultrecord"), true);
        }
        set
        {
            this.SetValue("adddefaultrecord", value);
        }
    }


    /// <summary>
    /// Gets the currene UniSelector instance.
    /// </summary>
    public UniSelector CurrentSelector
    {
        get
        {
            EnsureChildControls();
            return this.usStyleSheet;
        }
    }


    /// <summary>
    /// If true edit buttons are shown.
    /// </summary>
    public bool AllowEditButtons
    {
        get
        {
            return mAllowEditButtons;
        }
        set
        {
            mAllowEditButtons = value;
        }
    }


    /// <summary>
    /// Css stylesheet code name.
    /// </summary>
    public string StylesheetCodeName
    {
        get
        {
            EnsureChildControls();
            return Convert.ToString(usStyleSheet.Value);
        }
        set
        {
            EnsureChildControls();
            usStyleSheet.Value = value;
        }
    }


    /// <summary>
    /// Gets the current drop down control.
    /// </summary>
    public DropDownList CurrentDropDown
    {
        get
        {
            EnsureChildControls();
            return usStyleSheet.DropDownSingleSelect;
        }
    }


    /// <summary>
    /// Gets or sets stylesheet name.
    /// </summary>
    public override object Value
    {
        get
        {
            EnsureChildControls();
            return Convert.ToString(usStyleSheet.Value);
        }
        set
        {
            EnsureChildControls();
            usStyleSheet.Value = value;
        }
    }


    /// <summary>
    /// Gets ClientID of the dropdownlist with stylesheets.
    /// </summary>
    public override string ValueElementID
    {
        get
        {
            EnsureChildControls();
            return usStyleSheet.ClientID;
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
            usStyleSheet.IsLiveSite = value;
        }
    }


    /// <summary>
    /// Gets or sets the site id. If set, only stylesheets of the site are displayed.
    /// </summary>
    public int SiteId
    {
        get
        {
            if ((mSiteId == 0) && !String.IsNullOrEmpty(SiteName))
            {
                string siteName = SiteName.ToLower();
                if (siteName == "##all##")
                {
                    mSiteId = -1;
                }
                else if (siteName == "##currentsite##")
                {
                    mSiteId = CMSContext.CurrentSiteID;
                }
                else
                {
                    // Get site id from site name if sets.
                    mSiteId = SiteInfoProvider.GetSiteID(siteName);
                }
            }
            return mSiteId;
        }
        set
        {
            mSiteId = value;
        }
    }


    /// <summary>
    /// Gets or sets the site name. If set, only stylesheets of the site are displayed.
    /// </summary>
    public string SiteName
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("SiteName"), String.Empty);
        }
        set
        {
            this.SetValue("SiteName", value);
        }
    }


    /// <summary>
    /// New item button.
    /// </summary>
    public Button ButtonNew
    {
        get
        {
            return usStyleSheet.ButtonDropDownNew;
        }
    }


    /// <summary>
    /// Name of the column used for return value.
    /// </summary>
    public string ReturnColumnName
    {
        get
        {
            return mReturnColumnName;
        }
        set
        {
            mReturnColumnName = value;
        }
    }

    #endregion


    #region "Methods"

    /// <summary>
    /// Page load event.
    /// </summary>
    protected void Page_Load(object sender, EventArgs e)
    {
        // Add "none record" if required
        if (this.AddNoneRecord && (usStyleSheet.SpecialFields == null))
        {
            usStyleSheet.SpecialFields = new string[1, 2] { { GetString("SelectCssStylesheet.NoneRecord"), String.Empty } };
        }

        // If site specified, restrict to stylesheets assigned to the site
        if (this.SiteId > 0)
        {
            usStyleSheet.WhereCondition = SqlHelperClass.AddWhereCondition(usStyleSheet.WhereCondition, "StylesheetID IN (SELECT StylesheetID FROM CMS_CssStylesheetSite WHERE SiteID = " + this.SiteId + ")");
        }

        usStyleSheet.ReturnColumnName = ReturnColumnName;

        // Check if user can edit the stylesheet
        CurrentUserInfo currentUser = CMSContext.CurrentUser;
        bool design = currentUser.IsAuthorizedPerResource("CMS.Design", "Design");
        bool uiElement = currentUser.IsAuthorizedPerUIElement("CMS.Content", new string[] { "Properties", "Properties.General", "General.Design" }, CMSContext.CurrentSiteName);


        if ((AllowEditButtons) && (design) && (uiElement) && (this.usStyleSheet.ReturnColumnName.Equals("StylesheetID", StringComparison.InvariantCultureIgnoreCase)))
        {
            bool uiEditStylesheet = currentUser.IsAuthorizedPerUIElement("CMS.Content", new string[] { "Design.EditCSSStylesheets" }, CMSContext.CurrentSiteName);
            bool uiNewStylesheet = currentUser.IsAuthorizedPerUIElement("CMS.Content", new string[] { "Design.NewCSSStylesheets" }, CMSContext.CurrentSiteName);
            usStyleSheet.DropDownSingleSelect.CssClass = "SelectorDropDown";

            // Check UI permissions for editing stylesheet
            if (uiEditStylesheet)
            {
                string url = "~/CMSModules/CssStylesheets/Pages/CssStylesheet_General.aspx?editonlycode=true";
                url = URLHelper.AddParameterToUrl(url, "hash", QueryHelper.GetHash("?editonlycode=true"));
                url = URLHelper.AddParameterToUrl(url, "cssstylesheetid", "##ITEMID##");
                url = URLHelper.AddParameterToUrl(url, "siteid", SiteId.ToString());
                usStyleSheet.EditItemPageUrl = url;
            }

            // Check UI permissions for creating stylesheet
            if (uiNewStylesheet)
            {
                string url = "~/CMSModules/CssStylesheets/Pages/CssStylesheet_New.aspx?usedialog=1";

                if (SiteId > 0)
                {
                    url = URLHelper.AddParameterToUrl(url, "siteid", SiteId.ToString());
                }

                url = URLHelper.AddParameterToUrl(url, "hash", QueryHelper.GetHash(url));
                usStyleSheet.NewItemPageUrl = url;
            }
        }
    }


    /// <summary>
    /// Reloads the selector's data.
    /// </summary>
    /// <param name="forceReload">Indicates whether data should be forcibly reloaded</param>
    public void Reload(bool forceReload)
    {
        usStyleSheet.Reload(forceReload);
    }


    /// <summary>
    /// Creates child controls and loads update panel container if it is required.
    /// </summary>
    protected override void CreateChildControls()
    {
        // If selector is not defined load updat panel container
        if (usStyleSheet == null)
        {
            this.pnlUpdate.LoadContainer();
        }

        // Call base method
        base.CreateChildControls();
    }

    #endregion
}

