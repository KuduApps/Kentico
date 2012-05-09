using System;
using System.Data;
using System.Web.UI.WebControls;

using CMS.FormControls;
using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.Ecommerce;
using CMS.TreeEngine;
using CMS.SiteProvider;
using CMS.CMSHelper;
using CMS.SettingsProvider;

public partial class CMSModules_Ecommerce_Controls_UI_ProductDocuments : FormEngineUserControl
{
    #region "Private variables"

    private int mProductId = 0;
    private SKUInfo product = null;

    #endregion


    #region "Public properties"

    /// <summary>
    /// ID of the current product.
    /// </summary>
    public int ProductID
    {
        get
        {
            return mProductId;
        }
        set
        {
            mProductId = value;
        }
    }

    #endregion


    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Get current product info
        product = SKUInfoProvider.GetSKUInfo(ProductID);
        if (product != null)
        {
            // Allow site selector for global admins
            if (!CMSContext.CurrentUser.IsGlobalAdministrator)
            {
                filterDocuments.LoadSites = false;
                filterDocuments.SitesPlaceHolder.Visible = false;
            }

            // Get no data message text
            string productNameLocalized = ResHelper.LocalizeString(product.SKUName);
            string noDataMessage = string.Format(GetString("ProductDocuments.Documents.nodata"), HTMLHelper.HTMLEncode(productNameLocalized));
            if (filterDocuments.FilterIsSet)
            {
                noDataMessage = GetString("ProductDocuments.Documents.noresults");
            }
            else if (filterDocuments.SelectedSite != TreeProvider.ALL_SITES)
            {
                SiteInfo si = SiteInfoProvider.GetSiteInfo(filterDocuments.SelectedSite);
                if (si != null)
                {
                    noDataMessage = string.Format(GetString("ProductDocuments.Documents.nodataforsite"), HTMLHelper.HTMLEncode(productNameLocalized), HTMLHelper.HTMLEncode(si.DisplayName));
                }
            }
            docElem.ZeroRowsText = noDataMessage;

            docElem.SiteName = filterDocuments.SelectedSite;
            docElem.UniGrid.OnBeforeDataReload += new OnBeforeDataReload(UniGrid_OnBeforeDataReload);
            docElem.UniGrid.OnAfterDataReload += new OnAfterDataReload(UniGrid_OnAfterDataReload);
        }
    }

    #endregion


    #region "Grid events"

    protected void UniGrid_OnBeforeDataReload()
    {
        string where = "(SKUID=" + ProductID + " AND DocumentNodeID IN (SELECT NodeID FROM CMS_Tree WHERE NodeSKUID=" + ProductID + "))";
        where = SqlHelperClass.AddWhereCondition(where, filterDocuments.WhereCondition);
        docElem.UniGrid.WhereCondition = SqlHelperClass.AddWhereCondition(docElem.UniGrid.WhereCondition, where);
    }


    protected void UniGrid_OnAfterDataReload()
    {
        plcFilter.Visible = docElem.UniGrid.DisplayExternalFilter(filterDocuments.FilterIsSet);
    }

    #endregion
}
