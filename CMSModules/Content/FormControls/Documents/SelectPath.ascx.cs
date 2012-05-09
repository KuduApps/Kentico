using System;

using CMS.FormControls;
using CMS.GlobalHelper;
using CMS.ExtendedControls;
using CMS.CMSHelper;
using CMS.SiteProvider;

public partial class CMSModules_Content_FormControls_Documents_SelectPath : FormEngineUserControl
{
    #region "Variables"

    private bool mEnableSiteSelection = false;
    private DialogConfiguration mConfig = null;
    private string selectedSiteName = null;
    private bool siteNameIsAll = false;

    #endregion


    #region "Private properties"

    /// <summary>
    /// Gets the configuration for Copy and Move dialog.
    /// </summary>
    private DialogConfiguration Config
    {
        get
        {
            if (this.mConfig == null)
            {
                this.mConfig = new DialogConfiguration();
                this.mConfig.HideLibraries = true;
                this.mConfig.HideAnchor = true;
                this.mConfig.HideAttachments = true;
                this.mConfig.HideContent = false;
                this.mConfig.HideEmail = true;
                this.mConfig.HideLibraries = true;
                this.mConfig.HideWeb = true;
                this.mConfig.EditorClientID = this.txtPath.ClientID;

                if (ControlsHelper.CheckControlContext(this, ControlContext.WIDGET_PROPERTIES)
                    && (!siteNameIsAll))
                {
                    // If used in a widget, site selection is provided by a site selector form control (using HasDependingField/DependsOnAnotherField principle)
                    // therefore the site selector drop-down list in the SelectPath dialog contains only a single site - preselected by the site selector form control
                    this.mConfig.ContentSites = (String.IsNullOrEmpty(selectedSiteName)) ? AvailableSitesEnum.OnlyCurrentSite : AvailableSitesEnum.OnlySingleSite;
                }
                else
                {
                    this.mConfig.ContentSites = AvailableSitesEnum.All;
                }

                this.mConfig.ContentSelectedSite = (String.IsNullOrEmpty(selectedSiteName)) ? CMSContext.CurrentSiteName : selectedSiteName;

                this.mConfig.OutputFormat = OutputFormatEnum.Custom;
                this.mConfig.CustomFormatCode = "selectpath";
                this.mConfig.SelectableContent = SelectableContentEnum.AllContent;
            }
            return this.mConfig;
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
            this.txtPath.Enabled = value;
            this.btnSelectPath.Enabled = value;
        }
    }


    /// <summary>
    /// Gets or sets field value.
    /// </summary>
    public override object Value
    {
        get
        {
            return this.txtPath.Text;
        }
        set
        {
            this.txtPath.Text = (string)value;
        }
    }


    /// <summary>
    /// Gets ClientID of the textbox with path.
    /// </summary>
    public override string ValueElementID
    {
        get
        {
            return this.txtPath.ClientID;
        }
    }


    /// <summary>
    /// Determines whether to enable site selection or not.
    /// </summary>
    public bool EnableSiteSelection
    {
        get
        {
            return this.mEnableSiteSelection;
        }
        set
        {
            this.mEnableSiteSelection = value;
            this.Config.ContentSites = (value ? AvailableSitesEnum.All : AvailableSitesEnum.OnlyCurrentSite);
        }
    }

    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptHelper.RegisterDialogScript(this.Page);
        SetFormSiteName();

        this.btnSelectPath.Text = GetString("general.select");
        this.btnSelectPath.OnClientClick = GetDialogScript();

    }


    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (URLHelper.IsPostback()
            && this.DependsOnAnotherField)
        {
            if (siteNameIsAll)
            {
                // Refresh the dialog script if the site name was changed to "ALL" (this enables the site selection in the dialog window)
                this.btnSelectPath.OnClientClick = GetDialogScript();
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
        return "modalDialog('" + GetDialogUrl() + "','PathSelection', '90%', '85%'); return false;";
    }


    /// <summary>
    /// Returns Correct URL of the copy or move dialog.
    /// </summary>
    private string GetDialogUrl()
    {
        string url = CMSDialogHelper.GetDialogUrl(this.Config, this.IsLiveSite, false, null, false);
        return url;
    }


    /// <summary>
    /// Sets the site name if the SiteName field is available in the form.
    /// The outcome of this method is used for the configuration of the "Config" property
    /// </summary>
    private void SetFormSiteName()
    {
        if (this.DependsOnAnotherField
            && (this.Form != null)
            && this.Form.IsFieldAvailable("SiteName"))
        {
            string siteName = ValidationHelper.GetString(this.Form.GetFieldValue("SiteName"), "");

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
}
