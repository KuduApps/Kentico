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
using CMS.DataEngine;
using CMS.PortalEngine;
using CMS.CMSHelper;
using CMS.UIControls;
using CMS.SettingsProvider;

public partial class CMSModules_PortalEngine_UI_WebContainers_Container_Edit_Sites : SiteManagerPage
{
    protected int containerId = 0;    
    private string currentValues = String.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        lblAvialable.Text = GetString("WebContainers_Edit_Sites.SiteTitle");

        containerId = QueryHelper.GetInteger("containerId", 0);

        if (containerId > 0)
        {
            // Get the active sites
            DataSet ds = SiteInfoProvider.GetSites("SiteID IN (SELECT SiteID FROM CMS_WebPartContainerSite WHERE ContainerID = " + containerId + ")", null, "SiteID", 0);
            if (!DataHelper.DataSourceIsEmpty(ds))
            {
                currentValues = TextHelper.Join(";", SqlHelperClass.GetStringValues(ds.Tables[0], "SiteID"));
            }

            if (!RequestHelper.IsPostBack())
            {
                usSites.Value = currentValues;
            }
        }

        usSites.OnSelectionChanged += usSites_OnSelectionChanged;
    }


    protected void usSites_OnSelectionChanged(object sender, EventArgs e)
    {
        SaveSites();
    }


    protected void SaveSites()
    {
        // Remove old items
        string newValues = ValidationHelper.GetString(usSites.Value, null);
        string items = DataHelper.GetNewItemsInList(newValues, currentValues);
        if (!String.IsNullOrEmpty(items))
        {
            string[] newItems = items.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            if (newItems != null)
            {
                // Add all new items to site
                foreach (string item in newItems)
                {
                    int siteId = ValidationHelper.GetInteger(item, 0);

                    // Remove
                    WebPartContainerSiteInfoProvider.RemoveContainerFromSite(containerId, siteId);
                }
            }
        }

        // Add new items
        items = DataHelper.GetNewItemsInList(currentValues, newValues);
        if (!String.IsNullOrEmpty(items))
        {
            string[] newItems = items.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            if (newItems != null)
            {
                // Add all new items to site
                foreach (string item in newItems)
                {
                    int siteId = ValidationHelper.GetInteger(item, 0);

                    // Add
                    WebPartContainerSiteInfoProvider.AddContainerToSite(containerId, siteId);
                }
            }
        }

        lblInfo.Visible = true;
        lblInfo.Text = GetString("General.ChangesSaved");
    }
}
