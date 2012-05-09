using System;
using System.Data;
using System.Web;
using System.Web.UI;

using CMS.SettingsProvider;
using CMS.SiteProvider;
using CMS.GlobalHelper;
using CMS.UIControls;

public partial class CMSModules_RelationshipNames_RelationshipName_Sites : SiteManagerPage
{
    #region "Protected variables"

    protected int relationshipNameId = 0;
    protected string currentValues = string.Empty;

    #endregion


    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        relationshipNameId = QueryHelper.GetInteger("relationshipnameid", 0);
        if (relationshipNameId > 0)
        {
            // Get the active sites
            DataSet ds = RelationshipNameSiteInfoProvider.GetRelationshipNameSites("SiteID", "RelationshipNameID = " + relationshipNameId, null, 0);

            if (!DataHelper.DataSourceIsEmpty(ds))
            {
                currentValues =  TextHelper.Join(";", SqlHelperClass.GetStringValues(ds.Tables[0], "SiteID"));
            }

            if (!RequestHelper.IsPostBack())
            {
                usRelNames.Value = currentValues;
            }
        }

        usRelNames.OnSelectionChanged += usSites_OnSelectionChanged;
    }

    #endregion


    #region "Control events"

    protected void usSites_OnSelectionChanged(object sender, EventArgs e)
    {
        SaveSites();
    }

    #endregion


    #region "Protected methods"

    protected void SaveSites()
    {
        // Remove old items
        string newValues = ValidationHelper.GetString(usRelNames.Value, null);
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
                    RelationshipNameSiteInfoProvider.RemoveRelationshipNameFromSite(relationshipNameId, siteId);
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
                    RelationshipNameSiteInfoProvider.AddRelationshipNameToSite(relationshipNameId, siteId);
                }
            }
        }

        lblInfo.Visible = true;
        lblInfo.Text = GetString("General.ChangesSaved");
    }

    #endregion
}
