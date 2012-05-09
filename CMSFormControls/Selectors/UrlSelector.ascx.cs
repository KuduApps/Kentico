using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.FormControls;
using CMS.GlobalHelper;
using CMS.ExtendedControls;
using CMS.SiteProvider;
using CMS.CMSHelper;

public partial class CMSFormControls_Selectors_UrlSelector : FormEngineUserControl
{
    #region "Variables"

    private bool mEnableSiteSelection = false;
    private DialogConfiguration mConfig = null;
    private string selectedSiteName = null;
    private bool siteNameIsAll = false;

    #endregion


    #region "Private properties"

    /// <summary>
    /// Gets the configuration dialog.
    /// </summary>
    private DialogConfiguration Config
    {
        get
        {
            if (mConfig == null)
            {
                mConfig = GetDialogConfiguration(FieldInfo);
                mConfig.EditorClientID = txtPath.ClientID;

                if (ControlsHelper.CheckControlContext(this, ControlContext.WIDGET_PROPERTIES) && (!siteNameIsAll))
                {
                    // If used in a widget, site selection is provided by a site selector form control (using HasDependingField/DependsOnAnotherField principle)
                    // therefore the site selector drop-down list in the SelectPath dialog contains only a single site - preselected by the site selector form control
                    mConfig.ContentSites = (String.IsNullOrEmpty(selectedSiteName)) ? AvailableSitesEnum.OnlyCurrentSite : AvailableSitesEnum.OnlySingleSite;
                }

                if (string.IsNullOrEmpty(mConfig.ContentSelectedSite))
                {
                    mConfig.ContentSelectedSite = (String.IsNullOrEmpty(selectedSiteName)) ? CMSContext.CurrentSiteName : selectedSiteName;
                }
                
                mConfig.OutputFormat = OutputFormatEnum.URL;
            }

            return mConfig;
        }
    }

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
            base.Enabled = value;
            txtPath.Enabled = value;
            btnSelectPath.Enabled = value;
        }
    }


    /// <summary>
    /// Gets or sets field value.
    /// </summary>
    public override object Value
    {
        get
        {
            return txtPath.Text;
        }
        set
        {
            txtPath.Text = ValidationHelper.GetString(value, string.Empty);
        }
    }


    /// <summary>
    /// Gets ClientID of the textbox with path.
    /// </summary>
    public override string ValueElementID
    {
        get
        {
            return txtPath.ClientID;
        }
    }


    /// <summary>
    /// Determines whether to enable site selection or not.
    /// </summary>
    public bool EnableSiteSelection
    {
        get
        {
            return mEnableSiteSelection;
        }
        set
        {
            mEnableSiteSelection = value;
            Config.ContentSites = (value ? AvailableSitesEnum.All : AvailableSitesEnum.OnlyCurrentSite);
        }
    }

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptHelper.RegisterDialogScript(Page);
        SetFormSiteName();

        btnSelectPath.Text = GetString("general.select");
        btnSelectPath.OnClientClick = GetDialogScript();
    }


    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (URLHelper.IsPostback() && DependsOnAnotherField)
        {
            if (siteNameIsAll)
            {
                // Refresh the dialog script if the site name was changed to "ALL" (this enables the site selection in the dialog window)
                btnSelectPath.OnClientClick = GetDialogScript();
            }

            pnlUpdate.Update();
        }
    }


    /// <summary>
    /// Gets the javascript which opens the dialog window.
    /// </summary>
    /// <returns></returns>
    private string GetDialogScript()
    {
        return "modalDialog('" + GetDialogUrl() + "', 'URLSelection', '90%', '85%'); return false;";
    }


    /// <summary>
    /// Returns Correct URL dialog.
    /// </summary>
    private string GetDialogUrl()
    {
        string url = CMSDialogHelper.GetDialogUrl(Config, IsLiveSite, false, null, false);
        return url;
    }


    /// <summary>
    /// Sets the site name if the SiteName field is available in the form.
    /// The outcome of this method is used for the configuration of the "Config" property.
    /// </summary>
    private void SetFormSiteName()
    {
        if (DependsOnAnotherField && (Form != null) && Form.IsFieldAvailable("SiteName"))
        {
            string siteName = ValidationHelper.GetString(Form.GetFieldValue("SiteName"), "");

            if (siteName.Equals(string.Empty) || siteName.Equals("##all##", StringComparison.InvariantCultureIgnoreCase))
            {
                selectedSiteName = string.Empty;
                siteNameIsAll = true;
                return;
            }

            if (!String.IsNullOrEmpty(siteName))
            {
                selectedSiteName = siteName;
                return;
            }
        }

        selectedSiteName = null;
    }

    #endregion
}