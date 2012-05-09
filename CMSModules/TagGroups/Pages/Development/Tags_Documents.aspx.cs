using System;
using System.Data;
using System.Web.UI.WebControls;

using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.SettingsProvider;
using CMS.SiteProvider;
using CMS.UIControls;
using CMS.TreeEngine;

public partial class CMSModules_TagGroups_Pages_Development_Tags_Documents : SiteManagerPage
{
    #region "Variables"

    private int mTagId = 0;

    #endregion


    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Get current tag ID
        mTagId = QueryHelper.GetInteger("tagid", 0);
        TagInfo ti = TagInfoProvider.GetTagInfo(mTagId);
        EditedObject = ti;

        if (ti != null)
        {
            int groupId = QueryHelper.GetInteger("groupid", 0);
            int siteId = QueryHelper.GetInteger("siteid", 0);

            string[,] pageTitleTabs = new string[2, 3];
            pageTitleTabs[0, 0] = GetString("taggroup_edit.itemlistlink");
            pageTitleTabs[0, 1] = "~/CMSModules/TagGroups/Pages/Development/TagGroup_Edit_Tags.aspx?groupid=" + groupId + "&siteid=" + siteId;
            pageTitleTabs[0, 2] = "groupContent";
            pageTitleTabs[1, 0] = ti.TagName;
            pageTitleTabs[1, 1] = string.Empty;
            pageTitleTabs[1, 2] = string.Empty;

            CurrentMaster.Title.Breadcrumbs = pageTitleTabs;

            docElem.SiteName = filterDocuments.SelectedSite;
            docElem.UniGrid.OnBeforeDataReload += new OnBeforeDataReload(UniGrid_OnBeforeDataReload);
            docElem.UniGrid.OnAfterDataReload += new OnAfterDataReload(UniGrid_OnAfterDataReload);
        }
    }

    #endregion


    #region "Grid events"

    protected void UniGrid_OnBeforeDataReload()
    {
        string where = "(DocumentID IN (SELECT CMS_DocumentTag.DocumentID FROM CMS_DocumentTag WHERE TagID = " + mTagId + "))";
        where = SqlHelperClass.AddWhereCondition(where, filterDocuments.WhereCondition);
        docElem.UniGrid.WhereCondition = SqlHelperClass.AddWhereCondition(docElem.UniGrid.WhereCondition, where);
    }


    protected void UniGrid_OnAfterDataReload()
    {
        plcFilter.Visible = docElem.UniGrid.DisplayExternalFilter(filterDocuments.FilterIsSet);
    }

    #endregion
}
