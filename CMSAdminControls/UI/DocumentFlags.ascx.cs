using System;
using System.Data;
using System.Text;

using CMS.TreeEngine;
using CMS.UIControls;
using CMS.GlobalHelper;
using CMS.DataEngine;
using CMS.CMSHelper;
using CMS.SettingsProvider;
using CMS.ExtendedControls;
using CMS.SiteProvider;

using TimeZoneInfo = CMS.SiteProvider.TimeZoneInfo;

public partial class CMSAdminControls_UI_DocumentFlags : DocumentFlagsControl
{
    #region "Private variables"

    private int mNodeID = 0;
    private object mDataSource = null;
    private DataSet mSiteCultures = null;
    private string mDefaultSiteCulture = null;
    private int mRepeatColumns = 10;

    #endregion


    #region "Public properties"

    /// <summary>
    /// Gets or sets Node ID.
    /// </summary>
    public override int NodeID
    {
        get
        {
            return mNodeID;

        }
        set
        {
            mNodeID = value;
        }
    }


    /// <summary>
    /// Number of columns for the flags.
    /// </summary>
    public override int RepeatColumns
    {
        get
        {
            return mRepeatColumns;

        }
        set
        {
            mRepeatColumns = value;
        }
    }


    /// <summary>
    /// Data source object.
    /// </summary>
    public override object DataSource
    {
        get
        {
            return mDataSource;

        }
        set
        {
            mDataSource = value;
        }
    }


    /// <summary>
    /// DataSet containing all site cultures.
    /// </summary>
    public override DataSet SiteCultures
    {
        get
        {
            return mSiteCultures;

        }
        set
        {
            mSiteCultures = value;
        }
    }


    /// <summary>
    /// Default culture of the site.
    /// </summary>
    public string DefaultSiteCulture
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

    protected void Page_Load(object sender, EventArgs e)
    {
        if (StopProcessing)
        {
            // Do nothing
        }
        else
        {
            ReloadData();
        }
    }



    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        if (!ScriptHelper.IsClientScriptBlockRegistered("FlagActions"))
        {
            StringBuilder sb = new StringBuilder();

            string strCulture = GetString("transman.language");
            string strStatus = GetString("transman.status");
            string strModified = GetString("transman.modified");
            string strVersion = GetString("transman.version");

            // Check if RTL culture
            bool isRTL = CultureHelper.IsUICultureRTL();

            sb.Append(
@"function DF_Tip(icon, culture, status, version, modif) {
    var code = '<div class=""FlagTooltip""><div style=""text-align:center;""><img class=""Icon"" src=""' + icon + '"" alt=""' + culture + '"" /></div><div class=""Text"">", string.Format(strCulture, "' + culture + '&nbsp;"), @"<br />", string.Format(strStatus, "' + status + '&nbsp;"), @"';
    if (version != '') {
        code += '<br />", string.Format(strVersion, "' + version + '&nbsp;"), @"';
    }
    if (modif != '') {
        code += '<br />", string.Format(strModified, "' + modif + '&nbsp;"), @"';
    }
    code += '</div></div>';
    Tip(code);
}

function DF_Redir(nodeId, culture) {
    if (RedirectItem != null) {
        RedirectItem(nodeId, culture);
    }
}
");

            ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "FlagActions", ScriptHelper.GetScript(sb.ToString()));
        }
    }

    #endregion


    #region "Public methods"

    override public void ReloadData()
    {
        // Hide control for one culture version
        if ((DataSource == null) || DataHelper.DataSourceIsEmpty(SiteCultures) || (SiteCultures.Tables[0].Rows.Count <= 1))
        {
            Visible = false;
        }
        else
        {
            // Check the data source
            if (!(DataSource is GroupedDataSource))
            {
                throw new Exception("[DocumentFlags]: Only GroupedDataSource is supported as a data source.");
            }

            // Register tooltip script
            ScriptHelper.RegisterTooltip(Page);

            // Get appropriate table from the data source
            GroupedDataSource gDS = (GroupedDataSource)DataSource;
            DataTable table = gDS.GetGroupDataTable(NodeID);

            // Get document in the default site culture
            DateTime defaultLastModification = DateTimeHelper.ZERO_TIME;
            DateTime defaultLastPublished = DateTimeHelper.ZERO_TIME;
            bool defaultCultureExists = false;
            DataRow[] rows = null;
            if (table != null)
            {
                rows = table.Select("DocumentCulture='" + DefaultSiteCulture + "'");
                defaultCultureExists = (rows.Length > 0);
            }

            if (defaultCultureExists)
            {
                defaultLastModification = ValidationHelper.GetDateTime(rows[0]["DocumentModifiedWhen"], DateTimeHelper.ZERO_TIME);
                defaultLastPublished = ValidationHelper.GetDateTime(rows[0]["DocumentLastPublished"], DateTimeHelper.ZERO_TIME);
            }

            // Build the content
            StringBuilder sb = new StringBuilder();

            sb.Append("<table cellpadding=\"1\" cellspacing=\"0\" class=\"DocumentFlags\">\n<tr>\n");
            int colsInRow = 0;
            int cols = 0;
            int colsCount = SiteCultures.Tables[0].Rows.Count;
            if (colsCount < RepeatColumns)
            {
                RepeatColumns = colsCount;
            }

            // Helper variable
            TimeZoneInfo ti = null;

            foreach (DataRow dr in SiteCultures.Tables[0].Rows)
            {
                ++cols;
                ++colsInRow;
                DateTime lastModification = DateTimeHelper.ZERO_TIME;
                string versionNumber = null;
                TranslationStatusEnum status = TranslationStatusEnum.NotAvailable;
                string cultureName = ValidationHelper.GetString(DataHelper.GetDataRowValue(dr, "CultureName"), "-");
                string cultureCode = ValidationHelper.GetString(DataHelper.GetDataRowValue(dr, "CultureCode"), "-");

                // Get document for given culture
                if (table != null)
                {
                    rows = table.Select("DocumentCulture='" + cultureCode + "'");
                    // Document doesn't exist
                    if (rows.Length != 0)
                    {
                        versionNumber = ValidationHelper.GetString(DataHelper.GetDataRowValue(rows[0], "VersionNumber"), null);

                        // Check if document is outdated
                        if (versionNumber != null)
                        {
                            lastModification = ValidationHelper.GetDateTime(rows[0]["DocumentLastPublished"], DateTimeHelper.ZERO_TIME);
                            status = (lastModification < defaultLastPublished) ? TranslationStatusEnum.Outdated : TranslationStatusEnum.Translated;
                        }
                        else
                        {
                            lastModification = ValidationHelper.GetDateTime(rows[0]["DocumentModifiedWhen"], DateTimeHelper.ZERO_TIME);
                            status = (lastModification < defaultLastModification) ? TranslationStatusEnum.Outdated : TranslationStatusEnum.Translated;
                        }
                    }
                }

                sb.Append("<td class=\"", GetStatusCSSClass(status), "\">");
                sb.Append("<img onmouseout=\"UnTip()\" style=\"cursor:pointer;\" onclick=\"DF_Redir('", NodeID, "','", cultureCode, "')\" onmouseover=\"DF_Tip('", GetFlagIconUrl(cultureCode, "48x48"), "', '", cultureName, "', '", GetStatusString(status), "', '");

                sb.Append((versionNumber != null) ? versionNumber : "");
                sb.Append("', '");
                sb.Append((lastModification != DateTimeHelper.ZERO_TIME) ? TimeZoneHelper.GetCurrentTimeZoneDateTimeString(lastModification,
                                                                           CMSContext.CurrentUser, CMSContext.CurrentSite, out ti)
                                                                         : "");
                sb.Append("')\" src=\"");
                sb.Append(GetFlagIconUrl(cultureCode, "16x16"));
                sb.Append("\" alt=\"");
                sb.Append(cultureName);
                sb.Append("\" /></td>\n");

                // Ensure repeat columns
                if (((colsInRow % RepeatColumns) == 0) && (cols != colsCount))
                {
                    sb.Append("</tr><tr>\n");
                    colsInRow = 0;
                }
            }

            // Ensure rest of the table cells
            if ((colsInRow != RepeatColumns) && (colsInRow != 0))
            {
                while (colsInRow < RepeatColumns)
                {
                    sb.Append("<td>&nbsp;</td>\n");
                    ++colsInRow;
                }
            }
            sb.Append("</tr>\n</table>\n");
            ltlFlags.Text = sb.ToString();
        }
    }


    /// <summary>
    /// Returns resolved path to the flag image for the specified culture.
    /// </summary>
    /// <param name="cultureCode">Culture code</param>
    /// <param name="iconSet">Name of the subfolder where icon images are located</param>
    public override string GetFlagIconUrl(string cultureCode, string iconSet)
    {
        // Short path to the icon
        if (ControlsExtensions.RenderShortIDs)
        {
            if (iconSet == "48x48")
            {
                return UIHelper.GetShortImageUrl(UIHelper.FLAG_ICONS_48, cultureCode + ".png");
            }
            else
            {
                return UIHelper.GetShortImageUrl(UIHelper.FLAG_ICONS, cultureCode + ".png");
            }
        }

        return base.GetFlagIconUrl(cultureCode, iconSet);
    }

    #endregion


    #region "Private methods"

    private static string GetStatusCSSClass(TranslationStatusEnum status)
    {
        switch (status)
        {
            case TranslationStatusEnum.NotAvailable:
                return "NotAvailable";

            case TranslationStatusEnum.Outdated:
                return "Outdated";

            default:
                return "Translated";
        }
    }


    private static string GetStatusString(TranslationStatusEnum status)
    {
        switch (status)
        {
            case TranslationStatusEnum.NotAvailable:
                return ResHelper.GetString("transman.NotAvailable");

            case TranslationStatusEnum.Outdated:
                return ResHelper.GetString("transman.Outdated");

            default:
                return ResHelper.GetString("transman.Translated");
        }
    }

    #endregion
}
