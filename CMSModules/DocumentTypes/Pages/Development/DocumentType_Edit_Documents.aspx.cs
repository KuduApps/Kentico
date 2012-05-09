using System;
using System.Collections;
using System.Data;
using System.Web.UI.WebControls;

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.SettingsProvider;
using CMS.SiteProvider;
using CMS.TreeEngine;
using CMS.UIControls;

public partial class CMSModules_DocumentTypes_Pages_Development_DocumentType_Edit_Documents : SiteManagerPage
{
    #region "Private variables"

    private int documentTypeId = 0;

    #endregion


    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        filterDocuments.ClassPlaceHolder.Visible = false;

        // Get current page template ID
        documentTypeId = QueryHelper.GetInteger("documenttypeid", 0);

        docElem.SiteName = filterDocuments.SelectedSite;
        docElem.UniGrid.OnBeforeDataReload += new OnBeforeDataReload(UniGrid_OnBeforeDataReload);
        docElem.UniGrid.OnAfterDataReload += new OnAfterDataReload(UniGrid_OnAfterDataReload);
    }

    #endregion


    #region "Grid events"

    protected void UniGrid_OnBeforeDataReload()
    {
        string where = "ClassName IN (SELECT ClassName FROM CMS_Class WHERE ClassID =" + documentTypeId + ")";
        where = SqlHelperClass.AddWhereCondition(where, filterDocuments.WhereCondition);
        docElem.UniGrid.WhereCondition = SqlHelperClass.AddWhereCondition(docElem.UniGrid.WhereCondition, where);
    }


    protected void UniGrid_OnAfterDataReload()
    {
        plcFilter.Visible = docElem.UniGrid.DisplayExternalFilter(filterDocuments.FilterIsSet);
    }

    #endregion
}
