using System;
using System.Data;
using System.Web;
using System.Web.UI;

using CMS.SiteProvider;
using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.SettingsProvider;

public partial class CMSModules_CssStylesheets_Pages_CssStylesheet_Sites : SiteManagerPage
{
    protected int cssStylesheetId = 0;
    protected string currentValues = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        lblAvialable.Text = GetString("CssStylesheet_Sites.Available");
        cssStylesheetId = QueryHelper.GetInteger("cssstylesheetid", 0);
        if (cssStylesheetId > 0)
        {
            // Get the active sites
            DataSet ds = CssStylesheetSiteInfoProvider.GetCssStylesheetSites("SiteID", "StylesheetID = " + cssStylesheetId, null, 0);
            if (!DataHelper.DataSourceIsEmpty(ds))
            {
                currentValues = TextHelper.Join(";", SqlHelperClass.GetStringValues(ds.Tables[0], "SiteID"));
            }

            if (!RequestHelper.IsPostBack())
            {
                usSites.Value = currentValues;
            }
        }

        usSites.IconPath = GetImageUrl("Objects/CMS_Site/object.png");
        usSites.OnSelectionChanged += usSites_OnSelectionChanged;
    }


    void usSites_OnSelectionChanged(object sender, EventArgs e)
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
                    CssStylesheetSiteInfoProvider.RemoveCssStylesheetFromSite(cssStylesheetId, siteId);
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
                    CssStylesheetSiteInfoProvider.AddCssStylesheetToSite(cssStylesheetId, siteId);
                }
            }
        }

        lblInfo.Visible = true;
        lblInfo.Text = GetString("General.ChangesSaved");
    }
}
