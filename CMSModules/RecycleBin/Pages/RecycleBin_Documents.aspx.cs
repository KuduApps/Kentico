using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.UIControls;
using CMS.SiteProvider;
using CMS.GlobalHelper;
using CMS.CMSHelper;

public partial class CMSModules_RecycleBin_Pages_RecycleBin_Documents : SiteManagerPage
{
    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Get site ID       
        int selectedSite = QueryHelper.GetInteger("toSite", 0);
        if (selectedSite == 0)
        {
            // Set site selector
            siteSelector.DropDownSingleSelect.AutoPostBack = true;
            siteSelector.OnlyRunningSites = false;
            siteSelector.AllowAll = false;
            siteSelector.UniSelector.SpecialFields = new string[1, 2] { { GetString("RecycleBin.AllSites"), "0" } };
            siteSelector.UniSelector.OnSelectionChanged += UniSelector_OnSelectionChanged;

            if (!RequestHelper.IsPostBack())
            {
                selectedSite = 0;
                siteSelector.Value = selectedSite;
            }
        }

        if (!IsCallback)
        {
            lblSite.Text = GetString("Administration-RecycleBin.Site");
        }

        SiteInfo si = SiteInfoProvider.GetSiteInfo(ValidationHelper.GetInteger(siteSelector.Value, 0));
        if (si != null)
        {
            recycleBin.SiteName = si.SiteName;
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
        pnlUpdate.Update();
    }

    #endregion
}

