using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.PortalControls;
using CMS.PortalEngine;

public partial class CMSWebParts_Localization_GoogleTranslator : CMSAbstractWebPart
{
    #region "Page events"

    /// <summary>
    /// Loads the web part content.
    /// </summary>
    public override void OnContentLoaded()
    {
        base.OnContentLoaded();
        SetupControl();
    }

    #endregion


    #region "Other methods"

    /// <summary>
    /// Sets up the control.
    /// </summary>
    protected void SetupControl()
    {
        if (StopProcessing)
        {
            // Do nothing
        }
        else
        {
            if ((CMSContext.ViewMode == ViewModeEnum.LiveSite) || (CMSContext.ViewMode == ViewModeEnum.Preview))
            {
                string culture = CultureHelper.GetShortCultureCode(CMSContext.CurrentDocumentCulture.CultureCode);

                // Registers Google Translator scripts
                string translateScript = "function googleTranslateElementInit() {new google.translate.TranslateElement({pageLanguage: '" + culture + "'}, 'google_translate_element');}";
                ScriptHelper.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "GoogleTranslatorScript", translateScript, true);
                ScriptHelper.RegisterScriptFile(this.Page, "http://translate.google.com/translate_a/element.js?cb=googleTranslateElementInit");
            }
        }
    }

    #endregion
}