using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.UIControls;
using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.SiteProvider;
using CMS.ExtendedControls;
using CMS.LicenseProvider;
using CMS.SettingsProvider;

public partial class CMSModules_RecycleBin_Pages_RecycleBin_Objects : SiteManagerPage
{
    #region "Constants"

    private const string GLOBAL_OBJECTS = "##global##";

    #endregion


    #region "Page events"

    protected void Page_Init(object sender, EventArgs e)
    {
        // Check the license
        if (DataHelper.GetNotEmpty(URLHelper.GetCurrentDomain(), "") != "")
        {
            LicenseHelper.CheckFeatureAndRedirect(URLHelper.GetCurrentDomain(), FeatureEnum.ObjectVersioning);
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        // Set site selector
        siteSelector.DropDownSingleSelect.AutoPostBack = true;
        siteSelector.OnlyRunningSites = false;
        siteSelector.AllowAll = false;
        siteSelector.UniSelector.SpecialFields = new string[2, 2] { { GetString("RecycleBin.AllSitesAndGlobal"), "0" },
                                                                    { GetString("General.GlobalObjects"), "-1" }
                                                                  };
        siteSelector.UniSelector.OnSelectionChanged += UniSelector_OnSelectionChanged;

        if (!RequestHelper.IsPostBack())
        {
            siteSelector.Value = 0;
        }

        if (!IsCallback)
        {
            lblSite.Text = GetString("Administration-RecycleBin.Site");
        }

        // Set site name to recycle bin control
        int siteId = ValidationHelper.GetInteger(siteSelector.Value, 0);
        SiteInfo si = SiteInfoProvider.GetSiteInfo(siteId);
        if (si != null)
        {
            recycleBin.SiteName = si.SiteName;
        }
        else if (siteId == -1)
        {
            recycleBin.SiteName = GLOBAL_OBJECTS;
        }

        // Set delayed reload if site was changed
        Control pbCtrl = ControlsHelper.GetPostBackControl(this);
        if ((pbCtrl != null) && (pbCtrl == siteSelector.DropDownSingleSelect))
        {
            recycleBin.DelayedLoading = true;
        }
    }


    protected override void OnPreRender(EventArgs e)
    {
        // Hide site selector if there are no sites
        if (!siteSelector.UniSelector.HasData)
        {
            pnlSiteSelector.Visible = false;
        }
        base.OnPreRender(e);
    }

    #endregion


    #region "Control events"

    /// <summary>
    /// Handles site selection change event.
    /// </summary>
    protected void UniSelector_OnSelectionChanged(object sender, EventArgs e)
    {
        recycleBin.ReloadData(true);
    }

    #endregion
}

