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
using System.Text;

using CMS.GlobalHelper;
using CMS.SiteProvider;
using CMS.SettingsProvider;
using CMS.UIControls;

public partial class CMSModules_Settings_SiteManager_GetSettings : SiteManagerPage
{
    #region "Constants"

    public const string GROUP_SEPARATOR = "--------------------------------------------------";

    #endregion


    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        int siteId = QueryHelper.GetInteger("siteid", 0);
        int categoryId = QueryHelper.GetInteger("categoryid", 0);
        string searchText = QueryHelper.GetString("search", "");
        bool searchInDescription = QueryHelper.GetBoolean("description", false);


        // Get the category
        SettingsCategoryInfo ci = SettingsCategoryInfoProvider.GetSettingsCategoryInfo(categoryId);

        // If category is set or search is used
        if ((ci != null) || (string.IsNullOrEmpty(searchText)))
        {
            StringBuilder sb = new StringBuilder();

            // Prepare the header of the file
            SiteInfo si = SiteInfoProvider.GetSiteInfo(siteId);
            if (si == null)
            {
                sb.Append("Global settings, ");
            }
            else
            {
                sb.Append("Settings for site \"" + si.DisplayName + "\", ");
            }

            DataSet groups = new DataSet();

            // Get right DataSet of settings category groups
            if (!string.IsNullOrEmpty(searchText))
            {
                sb.Append("search text \"" + searchText + "\"" + Environment.NewLine + Environment.NewLine);
                groups = SettingsCategoryInfoProvider.GetSettingsCategories("CategoryIsGroup = 1", "CategoryName", -1, " CategoryID, CategoryDisplayName, CategoryIDPath");
            }
            else
            {
                sb.Append("category \"" + ResHelper.LocalizeString(GetCategoryPathNames(ci)) + "\"" + Environment.NewLine + Environment.NewLine);
                groups = SettingsCategoryInfoProvider.GetSettingsCategories(string.Format("CategoryParentID = {0} AND CategoryIsGroup = 1", ci.CategoryID), "CategoryOrder", -1, " CategoryID, CategoryDisplayName");

            }

            // Iterate over all groups under selected category
            foreach (DataRow groupRow in groups.Tables[0].Rows)
            {
                string groupName = ResHelper.LocalizeString(groupRow["CategoryDisplayName"].ToString());
                int groupCategoryId = SqlHelperClass.GetInteger(groupRow["CategoryID"], -1);

                // Get all settings keys in specified group
                DataSet ds = SettingsKeyProvider.GetSettingsKeysOrdered(siteId, groupCategoryId);
                if (!DataHelper.DataSourceIsEmpty(ds))
                {
                    string result = GetGroupKeys(ds.Tables[0], siteId > 0, searchText, searchInDescription);

                    if (!string.IsNullOrEmpty(result))
                    {
                        if (!string.IsNullOrEmpty(searchText))
                        {
                            // Get parent categories names
                            string parentName = ResHelper.LocalizeString(GetCategoryNames(groupRow["CategoryIDPath"].ToString()));

                            // Print group name with parent
                            sb.Append(Environment.NewLine + parentName + " - " + groupName + Environment.NewLine + GROUP_SEPARATOR + Environment.NewLine + Environment.NewLine);

                        }
                        else
                        {
                            // Print group name
                            sb.Append(Environment.NewLine + groupName + Environment.NewLine + GROUP_SEPARATOR + Environment.NewLine + Environment.NewLine);
                        }

                        // Print SettingsKey names and values
                        sb.Append(result);
                    }
                }
            }

            // Send the file to the user
            string siteName = (siteId > 0 ? si.SiteName : "Global");

            Response.AddHeader("Content-disposition", "attachment; filename=" + siteName + "_" + ValidationHelper.GetIdentifier(ci.CategoryName, "_") + ".txt");
            Response.ContentType = "text/plain";
            Response.Write(sb.ToString());

            RequestHelper.EndResponse();
        }
    }

    #endregion


    #region "Private methods"

    /// <summary>
    /// Gets category names for given settings groupIDPath
    /// </summary>
    /// <param name="groupIDPath">Settings group IDPath</param>
    /// <returns>String with parent category names</returns>
    private string GetCategoryNames(string groupIDPath)
    {
        // Get parent category names
        DataSet parents = SettingsCategoryInfoProvider.GetSettingsCategories(SettingsCategoryInfoProvider.GetCategoriesOnPathWhereCondition(groupIDPath, true) + " AND (CategoryLevel > 0)", "CategoryLevel", -1, "CategoryDisplayName");

        if (!DataHelper.DataSourceIsEmpty(parents))
        {
            string result = "";
            foreach (DataRow parent in parents.Tables[0].Rows)
            {
                result += parent["CategoryDisplayName"] + " > ";
            }

            return result.Substring(0, result.LastIndexOf(">")).Trim();
        }
        return String.Empty;
    }


    /// <summary>
    /// Formats informations about provided keys into a string.
    /// </summary>
    /// <param name="keys">Datatable containing keys for one group</param>
    /// <param name="specificSite">True, if keys belong to specific site (not global)</param>
    /// <param name="searchText">Search text</param>
    /// <param name="searchInDescription">True, if search in key description</param>
    private string GetGroupKeys(DataTable keys, bool specificSite, string searchText, bool searchInDescription)
    {
        StringBuilder sb = new StringBuilder("");

        foreach (DataRow dr in keys.Rows)
        {
            string name = ResHelper.LocalizeString(dr["KeyDisplayName"].ToString());
            string description = ResHelper.LocalizeString(dr["KeyDescription"].ToString());

            // Return only wanted keys
            if ((searchText == "") || ((name.Contains(searchText)) || (searchInDescription && (description.Contains(searchText)))))
            {
                string codeName = dr["KeyName"].ToString();
                bool isBool = dr["KeyType"].ToString() == "boolean";
                object value = dr["KeyValue"];
                bool isCustom = SqlHelperClass.GetBoolean(dr["KeyIsCustom"], false);

                sb.Append(name + (name.EndsWith(":") ? " " : ": "));

                if ((value == DBNull.Value) && specificSite)
                {
                    // Key is inherited

                    if (isBool)
                    {
                        sb.Append(SettingsKeyProvider.GetBoolValue(codeName) + " (inherited)");
                    }
                    else
                    {
                        sb.Append(SettingsKeyProvider.GetStringValue(codeName) + " (inherited)");
                    }
                }
                else
                {
                    if (isBool)
                    {
                        sb.Append(ValidationHelper.GetBoolean(value, false));
                    }
                    else
                    {
                        sb.Append(value.ToString());
                    }
                }

                // Key is custom
                if (isCustom)
                {
                    sb.Append(" (custom)");
                }

                sb.Append(Environment.NewLine);
                sb.Append(Environment.NewLine);
            }
        }
        return sb.ToString();
    }


    /// <summary>
    /// Gets path containing display names of all predecessors of given category and name of category itself.
    /// </summary>
    /// <param name="child">Category to get path from</param>
    /// <returns>String containing path to given Category</returns>
    private string GetCategoryPathNames(SettingsCategoryInfo child)
    {
        // Convert IDPath to be suitable for IN-clause of SQL query
        string inClause = child.CategoryIDPath.Remove(0, 1).Replace('/', ',');

        StringBuilder sb = new StringBuilder("");
        if (!string.IsNullOrEmpty(inClause))
        {
            // Get display names of all categories from path, order by nesting level
            DataSet ds = SettingsCategoryInfoProvider.GetSettingsCategories(string.Format("CategoryID IN ({0}) AND CategoryLevel > 0", inClause), "CategoryLevel", -1, "CategoryDisplayName");

            if (!DataHelper.DataSourceIsEmpty(ds))
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    // No slash before first name
                    if (sb.Length > 0)
                    {
                        sb.Append(" / ");
                    }
                    sb.Append(SqlHelperClass.GetString(row["CategoryDisplayName"], ""));
                }
            }
        }

        return sb.ToString();
    }

    #endregion
}