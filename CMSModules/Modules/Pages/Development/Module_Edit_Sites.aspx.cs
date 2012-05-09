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
using System.Data.SqlClient;

using CMS.GlobalHelper;
using CMS.SiteProvider;
using CMS.DataEngine;
using CMS.CMSHelper;
using CMS.UIControls;
using CMS.SettingsProvider;

public partial class CMSModules_Modules_Pages_Development_Module_Edit_Sites : SiteManagerPage
{
    protected int moduleId = 0;
    private string currentValues = String.Empty;


    protected void Page_Load(object sender, EventArgs e)
    {
        lblAvialable.Text = GetString("Administration-Module_Edit_Sites.SiteTitle");

        moduleId = QueryHelper.GetInteger("moduleId", 0);

        if (moduleId > 0)
        {
            // Get the active sites
            DataSet ds = ResourceSiteInfoProvider.GetResourceSites("ResourceID = " + moduleId, null, 0, "SiteID");
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
                    ResourceSiteInfoProvider.RemoveResourceFromSite(moduleId, siteId);
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
                    ResourceSiteInfoProvider.AddResourceToSite(moduleId, siteId);
                }
            }
        }

        lblInfo.Visible = true;
        lblInfo.Text = GetString("General.ChangesSaved");
    }
}
