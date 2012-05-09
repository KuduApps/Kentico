using System;

using CMS.GlobalHelper;
using CMS.SiteProvider;
using CMS.UIControls;

public partial class CMSModules_UICultures_Pages_Development_UICulture_Tab_General : SiteManagerPage
{
    #region "Variables"

    protected int UIcultureID;

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        rfvUICultureName.ErrorMessage = GetString("UICulture_Edit_General.ErrorEmptyCultureName");
        rfvUICultureCode.ErrorMessage = GetString("UICulture_Edit_General.ErrorEmptyCultureCode");

        // Get UIcultureID
        UIcultureID = QueryHelper.GetInteger("uicultureid", 0);
        if (UIcultureID != 0)
        {
            UICultureInfo myUICultureInfo = UICultureInfoProvider.GetSafeUICulture(UIcultureID);
            EditedObject = myUICultureInfo;

            if (!RequestHelper.IsPostBack())
            {
                txtUICultureName.Text = myUICultureInfo.UICultureName;
                txtUICultureCode.Text = myUICultureInfo.UICultureCode;
            }
        }
    }


    /// <summary>
    /// Handles btnOK's OnClick event - Update resource info.
    /// </summary>
    protected void btnOK_Click(object sender, EventArgs e)
    {
        // Check if input is valid 
        string cultureCode = txtUICultureCode.Text.Trim();
        string result = new Validator()
            .NotEmpty(txtUICultureName.Text, rfvUICultureName.ErrorMessage)
            .NotEmpty(cultureCode, rfvUICultureCode.ErrorMessage)
            .Result;

        // Check if requested culture exists
        try
        {
            System.Globalization.CultureInfo obj = new System.Globalization.CultureInfo(cultureCode);
        }
        catch
        {
            result = GetString("UICulture.ErrorNoGlobalCulture");
        }

        if (!string.IsNullOrEmpty(result))
        {
            ShowError(result);
        }
        else
        {
            SaveUICulture(cultureCode);
        }
    }


    /// <summary>
    /// Save UI culture that is being edited.
    /// </summary>
    /// <returns>ID of the saved culture</returns>
    protected void SaveUICulture(string cultureCode)
    {
        if (!String.IsNullOrEmpty(cultureCode))
        {
            UICultureInfo culture = UICultureInfoProvider.GetUICultureInfo(cultureCode, false);
            string cultureName = txtUICultureName.Text.Trim();

            // Update UI culture
            if ((UIcultureID > 0) && (((culture != null) && (culture.UICultureID == UIcultureID)) || (culture == null)))
            {
                if (culture == null)
                {
                    culture = UICultureInfoProvider.GetSafeUICulture(UIcultureID);
                }

                if (culture != null)
                {
                    culture.UICultureCode = cultureCode;
                    culture.UICultureName = cultureName;
                    culture.UICultureLastModified = DateTime.Now;
                    UICultureInfoProvider.SetUICultureInfo(culture);
                    ShowInformation(GetString("General.ChangesSaved"));
                }
            }
            // Create new UI culture
            else if ((UIcultureID <= 0) && (culture == null))
            {
                culture = new UICultureInfo();
                culture.UICultureGUID = Guid.NewGuid();
                culture.UICultureCode = cultureCode;
                culture.UICultureName = cultureName;
                culture.UICultureLastModified = DateTime.Now;
                UICultureInfoProvider.SetUICultureInfo(culture);
                ShowInformation(GetString("General.ChangesSaved"));
            }
            // Culture with specified code already exists
            else
            {
                ShowError(GetString("UICulture_New.CultureExists"));
            }
        }
        // Provided culture code is empty
        else
        {
            ShowError(GetString("UICulture.ErrorNoGlobalCulture"));
        }
    }

    #endregion
}