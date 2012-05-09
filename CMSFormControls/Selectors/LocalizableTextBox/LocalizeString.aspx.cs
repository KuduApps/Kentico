using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Data;
using System.Collections;
using System.Text;

using CMS.UIControls;
using CMS.GlobalHelper;
using CMS.ResourceManager;
using CMS.SettingsProvider;
using CMS.DataEngine;
using CMS.FormControls;
using CMS.CMSHelper;
using CMS.Synchronization;

public partial class CMSFormControls_Selectors_LocalizableTextBox_LocalizeString : CMSModalPage
{
    #region "Variables"

    private Hashtable translations = new Hashtable();

    private string parentTextbox = null;
    private string parentHidden = null;
    private string defaultTranslation = null;
    private string defaultCultureName = null;

    private ResourceStringInfo rsi = null;

    private const string NOT_FOUND = "##NOT_FOUND##";

    #endregion


    #region "Events"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Check permissions
        if (CMSContext.CurrentUser.IsAuthorizedPerResource("CMS.Localization", "LocalizeStrings"))
        {
            // Set title
            CurrentMaster.Title.TitleText = GetString("localizable.string");
            CurrentMaster.Title.TitleImage = GetImageUrl("CMSModules/CMS_UICultures/module.png");
            CurrentMaster.Title.HelpTopicName = "localize_string";
            CurrentMaster.Title.HelpName = "helpTopic";

            // Get parent textbox to return changed text
            parentTextbox = QueryHelper.GetString("parentTextbox", "");
            parentHidden = QueryHelper.GetString("hiddenValueControl", "");

            // Prepare controls
            btnOk.Click += new EventHandler(btnOk_Click);
            btnApply.Click += new EventHandler(btnApply_Click);
            btnFilter.Click += new EventHandler(btnFilter_Click);
            // Show 'Is custom' option only in development mode
            plcIsCustom.Visible = SettingsKeyProvider.DevelopmentMode;
            rfvKey.ErrorMessage = GetString("Administration-UICulture_String_New.EmptyKey");
            // Table
            tblGrid.Attributes.Add("style", "border-collapse:collapse;");
            tblHeaderCellFilter.Attributes.Add("style", "white-space:nowrap;");
            tblHeaderRow.Attributes.Add("style", "width: 100%;");
            tblHeaderCellLabel.Attributes.Add("style", "white-space:nowrap;");
            tblHeaderCellLabel.Text = GetString("transman.Translated");

            ReloadData();
        }
        // Dialog is not available for unauthorized user
        else
        {
            lblError.ResourceString = "security.accesspage.onlyglobaladmin";
            lblError.Visible = true;
            pnlControls.Visible = false;
        }
    }


    /// <summary>
    /// Button OK clicked.
    /// </summary>
    void btnOk_Click(object sender, EventArgs e)
    {
        Save();
        if (!lblError.Visible)
        {
            if (String.IsNullOrEmpty(defaultTranslation))
            {
                defaultTranslation = ResHelper.GetString(txtStringKey.Text);
            }

            ScriptHelper.RegisterStartupScript(this, typeof(string), "localizeString", ScriptHelper.GetScript("wopener.SetTranslation('" + parentTextbox + "', " + ScriptHelper.GetString(defaultTranslation) + ", '" + parentHidden + "', " + ScriptHelper.GetString(rsi.StringKey) + "); window.close();"));
        }
    }


    /// <summary>
    /// Button Apply clicked.
    /// </summary>
    void btnApply_Click(object sender, EventArgs e)
    {
        Save();
        if (!lblError.Visible)
        {
            lblInfo.Visible = true;
        }
    }


    /// <summary>
    /// Filters the content.
    /// </summary>
    protected void btnFilter_Click(object sender, EventArgs e)
    {
        this.txtFilter.Focus();
    }

    #endregion


    #region "Methods"

    /// <summary>
    /// Reloads data.
    /// </summary>
    protected void ReloadData()
    {
        if (!RequestHelper.IsPostBack())
        {
            string key = QueryHelper.GetString("stringKey", "").Trim().Replace("'", "''");;

            txtStringKey.Text = key;
            rsi = SqlResourceManager.GetResourceStringInfo(txtStringKey.Text);

            // Ensure the value if not found
            if (rsi == null)
            {
                string currentCulture = CultureHelper.PreferredUICulture;
                string defaultCulture = CultureHelper.DefaultUICulture;

                string value = ResHelper.GetString(key, defaultCulture, NOT_FOUND);
                if (value != NOT_FOUND)
                {
                    // Create the resource string with current culture value
                    rsi = new ResourceStringInfo();
                    rsi.StringKey = key;
                    rsi.StringIsCustom = true;
                    rsi.UICultureCode = defaultCulture;
                    rsi.TranslationText = value;

                    SqlResourceManager.SetResourceStringInfo(rsi);

                    // Impor the current culture
                    if (!currentCulture.Equals(defaultCulture, StringComparison.InvariantCultureIgnoreCase))
                    {
                        rsi.UICultureCode = currentCulture;
                        rsi.TranslationText = ResHelper.GetString(key, currentCulture);

                        SqlResourceManager.SetResourceStringInfo(rsi);
                    }
                }
            }
        }
        else
        {
            rsi = SqlResourceManager.GetResourceStringInfo(ValidationHelper.GetInteger(hdnID.Value, 0));
        }

        if (rsi != null)
        {
            if (!RequestHelper.IsPostBack())
            {
                chkIsCustom.Checked = rsi.StringIsCustom;
                hdnID.Value = rsi.StringId.ToString();
            }

            // Get the cultures
            DataSet result = GetData();
            DataSet defaultCulture = GetDefaultCulture(result);

            if (!DataHelper.DataSourceIsEmpty(result) || !DataHelper.DataSourceIsEmpty(defaultCulture))
            {
                int rowCount = 1;
                TableRow row = null;
                TableCell cellText = null;
                TableCell cellValue = null;
                FormEngineUserControl control = null;
                Control c = null;

                // Add default culture translation as a first record
                if (!DataHelper.DataSourceIsEmpty(defaultCulture))
                {
                    AddRow(true, ref rowCount, row, cellText, cellValue, control, c, defaultCulture.Tables[0].Rows[0]);
                }

                // Add all cultures
                if (!DataHelper.DataSourceIsEmpty(result))
                {
                    foreach (DataRow dr in result.Tables[0].Rows)
                    {
                        AddRow(false, ref rowCount, row, cellText, cellValue, control, c, dr);
                    }
                }

                // Display filter for large number of results
                if ((rowCount > 6) || (txtFilter.Text.Trim().Length != 0))
                {
                    pnlHeaderCell.Visible = true;
                    lblHeaderCell.Visible = false;
                }
                else
                {
                    pnlHeaderCell.Visible = false;
                    lblHeaderCell.Visible = true;
                }
            }
            else
            {
                if (txtFilter.Text.Trim().Length != 0)
                {
                    pnlHeaderCell.Visible = true;
                    lblHeaderCell.Visible = false;
                }
                else
                {
                    pnlHeaderCell.Visible = false;
                    lblHeaderCell.Visible = true;
                }
            }
        }
        else
        {
            lblError.ResourceString = "resourcestring.notfound";
            lblError.Visible = true;
            pnlControls.Visible = false;
            btnApply.Enabled = false;
            btnOk.Enabled = false;
        }
    }


    /// <summary>
    /// Returns DataSet according to filter settings.
    /// </summary>
    private DataSet GetData()
    {
        string filterWhere = null;
        string filterText = txtFilter.Text.Trim();

        // Filter by culture name
        if (!String.IsNullOrEmpty(filterText))
        {
            filterWhere = "UICultureName LIKE '%" + SqlHelperClass.GetSafeQueryString(filterText, false) + "%'";
        }

        // Filter by site cultures
        if (chkSiteCultures.Checked)
        {
            filterWhere = SqlHelperClass.AddWhereCondition(filterWhere, "UICultureCode IN (SELECT CultureCode FROM CMS_Culture WHERE CultureID IN (SELECT CultureID FROM CMS_SiteCulture WHERE SiteID = " + CMSContext.CurrentSiteID + "))");
        }

        // Filter by string key
        filterWhere = SqlHelperClass.AddWhereCondition("(StringID = " + hdnID.Value + ")", filterWhere);

        // Get translated strings
        DataSet result = ConnectionHelper.ExecuteQuery("cms.resourcestring.selecttranslated", null, filterWhere, null, -1, "UICultureName, UICultureCode, TranslationText");

        DataSet missingCultures = null;

        string existingTranslations = TextHelper.Join("', '", SqlHelperClass.GetStringValues(result.Tables[0], "UICultureCode"));
        existingTranslations = "'" + existingTranslations + "'";

        // Add missing site cultures' translations
        if (chkSiteCultures.Checked)
        {
            filterWhere = "UICultureCode IN (SELECT CultureCode FROM CMS_Culture WHERE CultureID IN (SELECT CultureID FROM CMS_SiteCulture WHERE SiteID = " + CMSContext.CurrentSiteID + ")) AND UICultureCode NOT IN (" + existingTranslations + ")";
        }
        // Add all missing cultures' tranlsations
        else
        {
            filterWhere = "UICultureCode NOT IN (" + existingTranslations + ")";
        }

        // Add filter from header
        if (!String.IsNullOrEmpty(filterText))
        {
            filterWhere = SqlHelperClass.AddWhereCondition(filterWhere, "UICultureName LIKE '%" + SqlHelperClass.GetSafeQueryString(filterText, false) + "%'");
        }

        // Get missing translations
        missingCultures = ConnectionHelper.ExecuteQuery("cms.uiculture.selectall", null, filterWhere, null, -1, "UICultureName, UICultureCode");

        // Add missing translations to result
        if (!DataHelper.DataSourceIsEmpty(missingCultures))
        {
            foreach (DataRow dr in missingCultures.Tables[0].Rows)
            {
                result.Tables[0].Rows.Add(dr[0], dr[1], null);
            }
        }

        // Sort and bind dataset
        result.Tables[0].DefaultView.Sort = "UICultureName";
        return result;
    }


    /// <summary>
    /// Returns default culture translation.
    /// </summary>
    private DataSet GetDefaultCulture(DataSet completeResult)
    {
        DataSet result = null;
        string existingTranslations = TextHelper.Join("', '", SqlHelperClass.GetStringValues(completeResult.Tables[0], "UICultureCode"));

        // Get default culture translation
        if (chkDefaultCulture.Checked || existingTranslations.Contains(CultureHelper.DefaultUICulture))
        {
            string filterWhere = null;

            // Set WHERE condition
            filterWhere = "(StringID = " + hdnID.Value + ")";
            filterWhere = SqlHelperClass.AddWhereCondition(filterWhere, "UICultureCode LIKE '" + CultureHelper.DefaultUICulture + "'");

            result = ConnectionHelper.ExecuteQuery("cms.resourcestring.selecttranslated", null, filterWhere, null, -1, "UICultureName, UICultureCode, TranslationText");
        }
        return result;
    }


    /// <summary>
    /// Adds new row into result table.
    /// </summary>
    private void AddRow(bool isDefaultCulture, ref int rowCount, TableRow row, TableCell cellText, TableCell cellValue, FormEngineUserControl control, Control c, DataRow dr)
    {
        string cultureName = HTMLHelper.HTMLEncode(ValidationHelper.GetString(dr["UICultureName"], null));
        string cultureCode = ValidationHelper.GetString(dr["UICultureCode"], null);

        if ((cultureCode.ToLowerInvariant() != CultureHelper.DefaultUICulture) || isDefaultCulture)
        {
            // Create new row
            row = new TableRow();
            row.CssClass = (rowCount % 2 == 0) ? "OddRow" : "EvenRow";
            rowCount++;

            // Create cell with culture name
            cellText = new TableCell();
            cellText.Text = "<img class=\"Image16\" style=\"vertical-align:middle;\" src=\"" + UIHelper.GetFlagIconUrl(this.Page, ValidationHelper.GetString(dr["UICultureCode"], null), "16x16") + "\" alt=\"" + cultureName + "\" />&nbsp;" + cultureName;
            if (isDefaultCulture)
            {
                cellText.Text += " " + GetString("general.defaultchoice");
            }
            row.Cells.Add(cellText);

            // Create cell with translation text
            cellValue = new TableCell();
            control = null;
            c = Page.LoadControl("~/CMSFormControls/Inputs/LargeTextArea.ascx");
            cellValue.Controls.Add(c);
            row.Cells.Add(cellValue);
            tblGrid.Rows.Add(row);
            c.ID = cultureCode;
            if (c.ID == CultureHelper.DefaultUICulture)
            {
                defaultCultureName = cellText.Text;
            }

            if (c is FormEngineUserControl)
            {
                control = (FormEngineUserControl)c;
                control.Value = ValidationHelper.GetString(dr["TranslationText"], null); ;
                translations[dr["UICultureCode"]] = control;
            }
        }
    }

    /// <summary>
    /// Saves resource translations and returns TRUE if save was successful. Returns FALSE if any error ocurred.
    /// </summary>
    private void Save()
    {
        // Check permissions
        if (CMSContext.CurrentUser.IsAuthorizedPerResource("CMS.Localization", "LocalizeStrings"))
        {
            // Change resource key
            string key = txtStringKey.Text.Trim();
            if (key != rsi.StringKey)
            {
                // Validate the key
                string result = new Validator().NotEmpty(key, rfvKey.ErrorMessage).IsCodeName(key, GetString("Administration-UICulture_String_New.InvalidCodeName")).Result;
                if (String.IsNullOrEmpty(result))
                {
                    ResourceStringInfo riNew = SqlResourceManager.GetResourceStringInfo(txtStringKey.Text.Trim());

                    // Check if string with given key is not already defined
                    if ((riNew == null) || (rsi.StringId == riNew.StringId))
                    {
                        // Log deletion of old string
                        if ((!String.Equals(key, rsi.StringKey, StringComparison.OrdinalIgnoreCase)) &&
                            (rsi.Generalized.LogSynchronization == SynchronizationTypeEnum.LogSynchronization))
                        {
                            SynchronizationHelper.LogObjectChange(rsi, TaskTypeEnum.DeleteObject);
                        }

                        rsi.StringKey = key;
                        SqlResourceManager.SetResourceStringInfo(rsi);
                    }
                    // New resource key collides with already existing resource key
                    else
                    {
                        lblError.Text = String.Format(GetString("Administration-UICulture_String_New.StringExists"), key);
                        lblError.Visible = true;
                    }
                }
                // New resource string key is not code name
                else
                {
                    lblError.Visible = true;
                    lblError.Text = result;
                }
            }

            string existingTranslation = null;
            string newTranslation = null;
            FormEngineUserControl control = null;

            // Go through all cultures
            foreach (string cultureCode in translations.Keys)
            {
                // Check if translation in given culture exists
                existingTranslation = SqlResourceManager.GetStringStrictly(txtStringKey.Text, cultureCode);
                // Get control for given culture
                control = (FormEngineUserControl)translations[cultureCode];

                if (control != null)
                {
                    // Translation is not already created
                    if (String.IsNullOrEmpty(existingTranslation))
                    {
                        // Get new translation
                        newTranslation = ValidationHelper.GetString(control.Value, String.Empty).Trim();

                        // Create new translation in given culture
                        if (!String.IsNullOrEmpty(newTranslation))
                        {
                            UpdateString(cultureCode, newTranslation);
                        }
                        // Translation of default culture must exist
                        else if (cultureCode == CultureHelper.DefaultUICulture)
                        {
                            lblError.Text = String.Format(ResHelper.GetString("localizable.deletedefault"), defaultCultureName);
                            lblError.Visible = true;
                        }
                    }
                    // Existing translation is being updated
                    else
                    {
                        newTranslation = ValidationHelper.GetString(control.Value, String.Empty).Trim();

                        // Delete translation if new translation is empty
                        if (String.IsNullOrEmpty(newTranslation))
                        {
                            // Delete translation
                            if (cultureCode != CultureHelper.DefaultUICulture)
                            {
                                SqlResourceManager.DeleteResourceStringInfo(txtStringKey.Text, cultureCode);
                            }
                            // Translation in default culture cannot be deleted or set to empty in Localizable textbox
                            else
                            {
                                lblError.Text = String.Format(ResHelper.GetString("localizable.deletedefault"), defaultCultureName);
                                lblError.Visible = true;
                            }
                        }
                        // Update translation if new translation is not empty
                        else
                        {
                            UpdateString(cultureCode, newTranslation);
                        }
                    }

                    // Set updated translation in current culture
                    if (cultureCode == CultureHelper.PreferredUICulture)
                    {
                        defaultTranslation = newTranslation;
                    }
                }
            }
        }
        // Current user is not global admin
        else
        {
            lblError.ResourceString = "general.actiondenied";
            lblError.Visible = true;
            pnlControls.Visible = false;
        }
    }


    /// <summary>
    /// Updates resource string for given culture.
    /// </summary>
    /// <param name="cultureCode">Culture code</param>
    /// <param name="translationText">Translation text</param>
    private void UpdateString(string cultureCode, string translationText)
    {
        // Update the string
        ResourceStringInfo ri = SqlResourceManager.GetResourceStringInfo(ValidationHelper.GetInteger(hdnID.Value, 0), cultureCode);
        if (ri != null)
        {
            ri.StringIsCustom = chkIsCustom.Checked;
            ri.UICultureCode = cultureCode;
            ri.TranslationText = translationText;
            SqlResourceManager.SetResourceStringInfo(ri);
        }
    }

    #endregion
}
