using System;
using System.Data;

using CMS.GlobalHelper;
using CMS.SiteProvider;
using CMS.UIControls;

public partial class CMSSiteManager_Development_Cultures_Culture_Edit_General : SiteManagerPage
{
    #region "Variables"

    private CultureInfo culture;

    #endregion


    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        rfvCultureName.ErrorMessage = GetString("Culture_Edit_General.ErrorEmptyCultureName");
        rfvCultureCode.ErrorMessage = GetString("Culture_Edit_General.ErrorEmptyCultureCode");
        rfvCultureShortName.ErrorMessage = GetString("Culture_Edit_General.ErrorEmptyCultureShortName");

        int cultureId = QueryHelper.GetInteger("cultureId", 0);
        culture = CultureInfoProvider.GetSafeCulture(cultureId);

        EditedObject = culture;

        if (!RequestHelper.IsPostBack())
        {
            LoadData();

            if (QueryHelper.GetBoolean("saved", false))
            {
                ShowInformation(GetString("General.ChangesSaved"));
            }
        }
    }

    #endregion


    #region "Events"

    /// <summary>
    /// Handles btnOK's OnClick event - Update resource info.
    /// </summary>
    protected void btnOK_Click(object sender, EventArgs e)
    {
        // finds whether required fields are not empty
        string result = new Validator()
            .NotEmpty(txtCultureName.Text.Trim(), rfvCultureName.ErrorMessage)
            .NotEmpty(txtCultureCode.Text.Trim(), rfvCultureCode.ErrorMessage)
            .Result;

        if (txtCultureCode.Text.Trim().Length > 10)
        {
            result = GetString("Culture.MaxLengthError");
        }

        try
        {
            // Chech if global culture exists
            if (new System.Globalization.CultureInfo(txtCultureCode.Text.Trim()) == null)
            {
                result = GetString("Culture.ErrorNoGlobalCulture");
            }
        }
        catch
        {
            result = GetString("Culture.ErrorNoGlobalCulture");
        }

        txtCultureAlias.Text = URLHelper.GetSafeUrlPart(txtCultureAlias.Text.Trim(), String.Empty);
        string cultureAlias = txtCultureAlias.Text.Trim().Replace("'", "''");

        // Check whether culture alias is unique
        if (!string.IsNullOrEmpty(cultureAlias))
        {
            string where = string.Format("(CultureCode = N'{0} 'OR CultureAlias = N'{0}') AND CultureID <> {1}", cultureAlias, culture.CultureID);
            DataSet cultures = CultureInfoProvider.GetCultures(where, null, 1, "CultureID");
            if ((!DataHelper.DataSourceIsEmpty(cultures)) ||
                (string.Equals(cultureAlias, txtCultureCode.Text.Trim(), StringComparison.OrdinalIgnoreCase)))
            {
                result = GetString("Culture.AliasNotUnique");
            }
        }

        if (result != string.Empty)
        {
            ShowError(result);
            return;
        }

        // finds if the culture code is unique
        CultureInfo uniqueCulture = CultureInfoProvider.GetCultureInfoForCulture(txtCultureCode.Text.Trim());

        // if culture code already exists and it is just editing culture -> update
        if ((uniqueCulture == null) || (uniqueCulture.CultureID == culture.CultureID))
        {
            UpdateCulture();
        }
        // if culture code already exists and it is another culture -> error
        else
        {
            ShowError(GetString("Culture_New.CultureExists"));
        }
    }

    #endregion


    #region "Methods"

    /// <summary>
    /// Loads data of edited culture into textboxes.
    /// </summary>
    protected void LoadData()
    {
        txtCultureName.Text = culture.CultureName;
        txtCultureCode.Text = culture.CultureCode;
        txtCultureShortName.Text = culture.CultureShortName;
        txtCultureAlias.Text = culture.CultureAlias;
    }


    /// <summary>
    /// Update just edited culture.
    /// </summary>
    protected void UpdateCulture()
    {
        if (culture != null)
        {
            culture.CultureName = txtCultureName.Text.Trim();
            culture.CultureCode = txtCultureCode.Text.Trim();
            culture.CultureShortName = txtCultureShortName.Text.Trim();
            culture.CultureAlias = txtCultureAlias.Text.Trim();

            CultureInfoProvider.SetCultureInfo(culture);
        }

        ShowInformation(GetString("General.ChangesSaved"));

        // Refresh header
        ScriptHelper.RefreshTabHeader(this, "general");
    }

    #endregion
}