using System;
using System.Data;

using CMS.GlobalHelper;
using CMS.SettingsProvider;
using CMS.SiteProvider;
using CMS.UIControls;

public partial class CMSModules_InlineControls_Pages_Development_Sites : SiteManagerPage
{
    #region "Variables"

    protected static int controlId;

    private string currentValues = string.Empty;

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Get inline control ID from querystring
        controlId = QueryHelper.GetInteger("inlinecontrolid", 0);

        InlineControlInfo inlineControlObj = InlineControlInfoProvider.GetInlineControlInfo(controlId);
        EditedObject = inlineControlObj;

        if (controlId > 0)
        {
            // Get the active sites
            DataSet ds = InlineControlSiteInfoProvider.GetInlineControlSites("SiteID", "ControlID = " + controlId, null, 0);
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
        // Get object
        InlineControlInfo inlineControlObj = InlineControlInfoProvider.GetInlineControlInfo(controlId);
        EditedObject = inlineControlObj;

        // Remove old items
        string newValues = ValidationHelper.GetString(usSites.Value, null);
        string items = DataHelper.GetNewItemsInList(newValues, currentValues);
        if (!string.IsNullOrEmpty(items))
        {
            string[] newItems = items.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            if (newItems != null)
            {
                int siteId = 0;
                foreach (string item in newItems)
                {
                    siteId = ValidationHelper.GetInteger(item, 0);
                    // Remove inline control from the site
                    InlineControlSiteInfoProvider.RemoveInlineControlFromSite(controlId, siteId);
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
                int siteId = 0;
                // Add all new items to site
                foreach (string item in newItems)
                {
                    siteId = ValidationHelper.GetInteger(item, 0);
                    // Add inline control to the site
                    InlineControlSiteInfoProvider.AddInlineControlToSite(controlId, siteId);
                }
            }
        }

        ShowInformation(GetString("General.ChangesSaved"));
    }

    #endregion
}