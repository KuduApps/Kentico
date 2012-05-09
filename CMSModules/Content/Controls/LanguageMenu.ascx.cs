using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.SiteProvider;
using CMS.UIControls;

public partial class CMSModules_Content_Controls_LanguageMenu : CMSUserControl
{
    #region "Variables"

    protected string currentUserPreferredCultureCode = null;
    protected string currentSiteName = null;

    #endregion


    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        currentUserPreferredCultureCode = CMSContext.CurrentUser.PreferredCultureCode;
        currentSiteName = CMSContext.CurrentSiteName;
        DataSet siteCulturesDS = CultureInfoProvider.GetSiteCultures(currentSiteName);
        if (!DataHelper.DataSourceIsEmpty(siteCulturesDS))
        {
            // Register jQuery cookie script
            ScriptHelper.RegisterJQueryCookie(Page);

            string defaultCulture = CultureHelper.GetDefaultCulture(currentSiteName);
            DataTable siteCultures = siteCulturesDS.Tables[0];
            int culturesCount = siteCultures.Rows.Count;
            if ((culturesCount <= 3) && (culturesCount > 1))
            {
                // Disable culture uniselector
                cultureSelector.StopProcessing = true;
                pnlLang.Visible = false;

                string[,] bigButtons = new string[culturesCount, 9];

                for (int i = 0; i < culturesCount; i++)
                {
                    string cultureCode = siteCultures.Rows[i]["CultureCode"].ToString();
                    string cultureShortName = siteCultures.Rows[i]["CultureShortName"].ToString();
                    string cultureLongName = ResHelper.LocalizeString(siteCultures.Rows[i]["CultureName"].ToString());

                    if (string.Compare(cultureCode, defaultCulture, true) == 0)
                    {
                        cultureLongName += " " + GetString("general.defaultchoice");
                    }

                    bigButtons[i, 0] = HTMLHelper.HTMLEncode(cultureShortName);
                    bigButtons[i, 1] = cultureLongName;
                    bigButtons[i, 2] = "BigButton";
                    bigButtons[i, 3] = "ChangeLanguageByCode('" + cultureCode + "')";
                    bigButtons[i, 4] = null;
                    bigButtons[i, 5] = GetFlagIconUrl(cultureCode, "48x48");
                    bigButtons[i, 6] = cultureLongName;
                    bigButtons[i, 7] = ImageAlign.Top.ToString();
                    bigButtons[i, 8] = "48";
                    if (currentUserPreferredCultureCode.ToLower() == cultureCode.ToLower())
                    {
                        buttons.SelectedIndex = i;
                    }
                }

                buttons.Buttons = bigButtons;

            }
            else
            {
                // Do not show culture selection buttons
                buttons.StopProcessing = true;

                // Initialize culture selector
                cultureSelector.AddDefaultRecord = false;
                cultureSelector.SiteID = CMSContext.CurrentSiteID;
                cultureSelector.DropDownCultures.CssClass = "ContentMenuLangDrop";
                cultureSelector.UpdatePanel.RenderMode = UpdatePanelRenderMode.Inline;
                cultureSelector.DropDownCultures.AutoPostBack = true;
                cultureSelector.UniSelector.OnSelectionChanged += UniSelector_OnSelectionChanged;

                if (!URLHelper.IsPostback())
                {
                    cultureSelector.Value = currentUserPreferredCultureCode;
                }
            }

            string compare = GetString("SplitMode.Compare");
            // Split mode button
            string[,] splitButton = new string[1, 9];
            splitButton[0, 0] = compare;
            splitButton[0, 1] = GetString("SplitMode.CompareLangVersions");
            splitButton[0, 2] = "BigButton";
            splitButton[0, 3] = "ChangeSplitMode()";
            splitButton[0, 4] = null;
            splitButton[0, 5] = GetImageUrl("CMSModules/CMS_Content/Menu/Compare.png");
            splitButton[0, 6] = compare;
            splitButton[0, 7] = ImageAlign.Top.ToString();
            splitButton[0, 8] = "48";
            splitView.Buttons = splitButton;
            splitView.SelectedIndex = CMSContext.DisplaySplitMode ? 0 : -1;
        }
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        // Hide Culture selector if there is only one culture or don't have license for multiple languages
        if ((cultureSelector.UniSelector.HasData && (cultureSelector.DropDownCultures.Items.Count < 2)) ||
            !CultureInfoProvider.LicenseVersionCheck())
        {
            Control languageMenu = pnlLang.Parent;

            if (languageMenu != null)
            {
                Control contentPanel = languageMenu.Parent;
                if (contentPanel != null)
                {
                    Control groupPanel = contentPanel.Parent;
                    if (groupPanel != null)
                    {
                        groupPanel.Visible = false;
                    }
                }
            }
        }
    }

    #endregion


    #region "Selector events"

    protected void UniSelector_OnSelectionChanged(object sender, EventArgs e)
    {
        CMSContext.CurrentUser.PreferredCultureCode = ValidationHelper.GetString(cultureSelector.Value, string.Empty);

        ScriptHelper.RegisterStartupScript(Page, typeof(string), "ChangeCultureRefresh", ScriptHelper.GetScript("ChangeLanguage(document.getElementById('" + cultureSelector.DropDownCultures.ClientID + "'));"));
    }

    #endregion
}
