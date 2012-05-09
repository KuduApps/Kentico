using System;
using System.Data;
using System.Web.UI.WebControls;

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.SiteProvider;
using CMS.UIControls;
using CMS.WorkflowEngine;
using CMS.TreeEngine;
using CMS.LicenseProvider;
using CMS.SettingsProvider;
using CMS.DataEngine;

using TreeNode = CMS.TreeEngine.TreeNode;
using TimeZoneInfo = CMS.SiteProvider.TimeZoneInfo;

[RegisterTitle("content.ui.propertieslanguages")]
public partial class CMSModules_Content_CMSDesk_Properties_Languages : CMSPropertiesPage
{
    #region "Private variables"

    private UserInfo currentUserInfo = null;
    private SiteInfo currentSiteInfo = null;

    #endregion


    #region "Protected variables"

    protected int nodeId = 0;
    protected DateTime defaultLastModification = DateTimeHelper.ZERO_TIME;
    protected DateTime defaultLastPublished = DateTimeHelper.ZERO_TIME;
    protected string mDefaultSiteCulture = null;

    #endregion


    #region "Properties"

    /// <summary>
    /// Default culture of the site.
    /// </summary>
    protected string DefaultSiteCulture
    {
        get
        {
            if (mDefaultSiteCulture == null)
            {
                mDefaultSiteCulture = CultureHelper.GetDefaultCulture(CMSContext.CurrentSiteName);
            }
            return mDefaultSiteCulture;
        }
    }

    #endregion


    #region "Page events"

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        if (!CMSContext.CurrentUser.IsAuthorizedPerUIElement("CMS.Content", "Properties.Languages"))
        {
            RedirectToCMSDeskUIElementAccessDenied("CMS.Content", "Properties.Languages");
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        // Get ID of node
        nodeId = QueryHelper.GetInteger("nodeid", 0);

        gridLanguages.FilteredZeroRowsText = GetString("transman.nodocumentculture");
        gridLanguages.OnDataReload += gridDocuments_OnDataReload;
        gridLanguages.OnExternalDataBound += gridLanguages_OnExternalDataBound;
        gridLanguages.ShowActionsMenu = true;
        gridLanguages.Columns = "DocumentModifiedWhen, DocumentLastPublished, DocumentName, VersionNumber, Published";

        IDataClass nodeClass = DataClassFactory.NewDataClass("CMS.Tree");
        DocumentInfo di = new DocumentInfo();
        gridLanguages.AllColumns = SqlHelperClass.MergeColumns(SqlHelperClass.MergeColumns(di.ColumnNames.ToArray()), SqlHelperClass.MergeColumns(nodeClass.ColumnNames.ToArray()));

    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        // Check license limitations
        if (!CultureInfoProvider.LicenseVersionCheck())
        {
            LicenseHelper.GetAllAvailableKeys(FeatureEnum.Multilingual);
        }

        // Set selected tab
        UIContext.PropertyTab = PropertyTabEnum.Languages;
    }

    #endregion


    #region "Grid events"

    protected object gridLanguages_OnExternalDataBound(object sender, string sourceName, object parameter)
    {
        TranslationStatusEnum status = TranslationStatusEnum.NotAvailable;
        DataRowView drv = null;
        sourceName = sourceName.ToLower();
        switch (sourceName)
        {
            case "action":
                ImageButton img = new ImageButton();
                if (sender != null)
                {
                    // Get datarowview
                    drv = UniGridFunctions.GetDataRowView(sender as DataControlFieldCell);

                    if ((drv != null) && (drv.Row["TranslationStatus"] != DBNull.Value))
                    {
                        // Get translation status
                        status = (TranslationStatusEnum)drv.Row["TranslationStatus"];
                    }
                    else
                    {
                        status = TranslationStatusEnum.NotAvailable;
                    }

                    // Set appropriate icon
                    switch (status)
                    {
                        case TranslationStatusEnum.NotAvailable:
                            img.ImageUrl = GetImageUrl("CMSModules/CMS_Content/Properties/addculture.png");
                            img.ToolTip = GetString("transman.createnewculture");
                            break;

                        default:
                            img.ImageUrl = GetImageUrl("CMSModules/CMS_Content/Properties/editculture.png");
                            img.ToolTip = GetString("transman.editculture");
                            break;
                    }

                    string culture = (drv != null) ? ValidationHelper.GetString(drv.Row["DocumentCulture"], string.Empty) : string.Empty;

                    // Register redirect script
                    img.OnClientClick = "RedirectItem(" + nodeId + ", '" + culture + "');";
                    img.ID = "imgAction";
                }
                return img;

            case "translationstatus":
                if (parameter == DBNull.Value)
                {
                    status = TranslationStatusEnum.NotAvailable;
                }
                else
                {
                    status = (TranslationStatusEnum)parameter;
                }
                string statusName = GetString("transman." + status);
                string statusHtml = "<span class=\"" + status + "\">" + statusName + "</span>";
                // .Outdated
                return statusHtml;

            case "documentculturedisplayname":
                drv = (DataRowView)parameter;
                // Add icon
                return UniGridFunctions.DocumentCultureFlag(drv, this.Page);

            case "documentmodifiedwhen":
            case "documentmodifiedwhentooltip":
                if (string.IsNullOrEmpty(parameter.ToString()))
                {
                    return "-";
                }
                else
                {
                    DateTime modifiedWhen = ValidationHelper.GetDateTime(parameter, DateTimeHelper.ZERO_TIME);
                    if (currentUserInfo == null)
                    {
                        currentUserInfo = CMSContext.CurrentUser;
                    }
                    if (currentSiteInfo == null)
                    {
                        currentSiteInfo = CMSContext.CurrentSite;
                    }

                    bool displayGMT = (sourceName == "documentmodifiedwhentooltip");
                    return TimeZoneHelper.ConvertToUserTimeZone(modifiedWhen, displayGMT, currentUserInfo, currentSiteInfo);
                }

            case "versionnumber":
                if (string.IsNullOrEmpty(parameter.ToString()))
                {
                    return "-";
                }
                break;

            case "documentname":
                if (string.IsNullOrEmpty(parameter.ToString()))
                {
                    parameter = "-";
                }
                return HTMLHelper.HTMLEncode(parameter.ToString());

            case "published":
                bool published = ValidationHelper.GetBoolean(parameter, false);
                if (published)
                {
                    return "<span class=\"DocumentPublishedYes\">" + GetString("General.Yes") + "</span>";
                }
                else
                {
                    return "<span class=\"DocumentPublishedNo\">" + GetString("General.No") + "</span>";
                }
        }
        return parameter;
    }

    #endregion


    #region "Protected methods"

    protected DataSet gridDocuments_OnDataReload(string completeWhere, string currentOrder, int currentTopN, string columns, int currentOffset, int currentPageSize, ref int totalRecords)
    {
        if (nodeId != 0)
        {
            string currentSiteName = CMSContext.CurrentSiteName;
            // Get node
            TreeProvider tree = new TreeProvider(CMSContext.CurrentUser);
            TreeNode node = tree.SelectSingleNode(nodeId);
            // Set edited document
            EditedDocument = node;

            // Check if node is not null
            if (node != null)
            {
                // Get documents
                int topN = gridLanguages.GridView.PageSize * (gridLanguages.GridView.PageIndex + 1 + gridLanguages.GridView.PagerSettings.PageButtonCount);
                columns = SqlHelperClass.MergeColumns(SqlHelperClass.MergeColumns(TreeProvider.SELECTNODES_REQUIRED_COLUMNS, columns), "DocumentLastPublished");
                DataSet documentsDS = DocumentHelper.GetDocuments(currentSiteName, node.NodeAliasPath, TreeProvider.ALL_CULTURES, false, null, null, null, -1, false, topN, columns, tree);
                DataTable documents = documentsDS.Tables[0];

                if (!DataHelper.DataSourceIsEmpty(documents))
                {
                    // Get site cultures
                    DataSet allSiteCultures = CultureInfoProvider.GetSiteCultures(currentSiteName).Copy();

                    // Rename culture column to enable row transfer
                    allSiteCultures.Tables[0].Columns[2].ColumnName = "DocumentCulture";

                    // Create where condition for row transfer
                    string where = "DocumentCulture NOT IN (";
                    foreach (DataRow row in documents.Rows)
                    {
                        where += "'" + row["DocumentCulture"] + "',";
                    }
                    where = where.TrimEnd(',') + ")";

                    // Transfer missing cultures, keep original list of site cultures
                    DataHelper.TransferTableRows(documents, allSiteCultures.Copy().Tables[0], where, null);
                    DataHelper.EnsureColumn(documents, "DocumentCultureDisplayName", typeof(string));

                    // Ensure culture names
                    foreach (DataRow cultDR in documents.Rows)
                    {
                        string cultureCode = cultDR["DocumentCulture"].ToString();
                        DataRow[] cultureRow = allSiteCultures.Tables[0].Select("DocumentCulture='" + cultureCode + "'");
                        if (cultureRow.Length > 0)
                        {
                            cultDR["DocumentCultureDisplayName"] = cultureRow[0]["CultureName"].ToString();
                        }
                    }

                    // Ensure default culture to be first
                    DataRow[] culturreDRs = documents.Select("DocumentCulture='" + DefaultSiteCulture + "'");
                    if (culturreDRs.Length <= 0)
                    {
                        throw new Exception("[ReloadData]: Default site culture '" + DefaultSiteCulture + "' is not assigned to the current site.");
                    }

                    DataRow defaultCultureRow = culturreDRs[0];

                    DataRow dr = documents.NewRow();
                    dr.ItemArray = defaultCultureRow.ItemArray;
                    documents.Rows.InsertAt(dr, 0);
                    documents.Rows.Remove(defaultCultureRow);

                    // Get last modification date of default culture
                    defaultCultureRow = documents.Select("DocumentCulture='" + DefaultSiteCulture + "'")[0];
                    defaultLastModification = ValidationHelper.GetDateTime(defaultCultureRow["DocumentModifiedWhen"], DateTimeHelper.ZERO_TIME);
                    defaultLastPublished = ValidationHelper.GetDateTime(defaultCultureRow["DocumentLastPublished"], DateTimeHelper.ZERO_TIME);

                    // Add column containing translation status
                    documents.Columns.Add("TranslationStatus", typeof(TranslationStatusEnum));

                    // Get proper translation status and store it to datatable
                    foreach (DataRow document in documents.Rows)
                    {
                        TranslationStatusEnum status = TranslationStatusEnum.NotAvailable;
                        int documentId = ValidationHelper.GetInteger(document["DocumentID"], 0);
                        if (documentId == 0)
                        {
                            status = TranslationStatusEnum.NotAvailable;
                        }
                        else
                        {
                            string versionNumber = ValidationHelper.GetString(DataHelper.GetDataRowValue(document, "VersionNumber"), null);
                            DateTime lastModification = DateTimeHelper.ZERO_TIME;

                            // Check if document is outdated
                            if (versionNumber != null)
                            {
                                lastModification = ValidationHelper.GetDateTime(document["DocumentLastPublished"], DateTimeHelper.ZERO_TIME);
                                status = (lastModification < defaultLastPublished) ? TranslationStatusEnum.Outdated : TranslationStatusEnum.Translated;
                            }
                            else
                            {
                                lastModification = ValidationHelper.GetDateTime(document["DocumentModifiedWhen"], DateTimeHelper.ZERO_TIME);
                                status = (lastModification < defaultLastModification) ? TranslationStatusEnum.Outdated : TranslationStatusEnum.Translated;
                            }
                        }
                        document["TranslationStatus"] = status;
                    }

                    // Bind datasource
                    DataSet filteredDocuments = documentsDS.Clone();
                    DataRow[] filteredDocs = documents.Select(gridLanguages.GetDataTableFilter());
                    foreach (DataRow row in filteredDocs)
                    {
                        filteredDocuments.Tables[0].ImportRow(row);
                    }

                    gridLanguages.DataSource = filteredDocuments;
                    return filteredDocuments;
                }
            }
        }

        return null;
    }

    #endregion
}
