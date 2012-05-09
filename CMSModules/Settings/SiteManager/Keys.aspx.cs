using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.SiteProvider;
using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.IO;
using CMS.SettingsProvider;

public partial class CMSModules_Settings_SiteManager_Keys : SiteManagerPage, IPostBackEventHandler
{
    #region "Private variables"

    private int mCategoryId;
    private int mSiteId;

    #endregion


    #region "Page Events"


    protected void Page_Load(object sender, EventArgs e)
    {
        // Get query strings
        mCategoryId = QueryHelper.GetInteger("categoryid", 0);
        mSiteId = QueryHelper.GetInteger("siteid", 0);

        // Assign category id, site id
        SettingsGroupViewer.CategoryID = mCategoryId;
        SettingsGroupViewer.SiteID = mSiteId;

        if (SettingsGroupViewer.SettingsCategoryInfo == null)
        {
            // Get root category info
            SettingsCategoryInfo sci = SettingsCategoryInfoProvider.GetRootSettingsCategoryInfo();
            SettingsGroupViewer.CategoryID = sci.CategoryID;
        }

        // Get search text if exist
        string search = QueryHelper.GetString("search", String.Empty).Trim();

        // If root selected show search controls
        if ((SettingsKeyProvider.DevelopmentMode) && (SettingsGroupViewer.CategoryName == "CMS.Settings"))
        {


            pnlSearch.Visible = true;
            lblNoSettings.ResourceString = "Development.Settings.SearchSettings";
            if (!string.IsNullOrEmpty(search))
            {
                // Set searched values
                if (!URLHelper.IsPostback())
                {
                    txtSearch.Text = search;
                    chkDescription.Checked = QueryHelper.GetBoolean("description", true);
                }
            }
            RegisterSearchScript();
        }

        // Set master title
        CurrentMaster.Title.TitleText = HTMLHelper.HTMLEncode(ResHelper.LocalizeString(SettingsGroupViewer.SettingsCategoryInfo.CategoryDisplayName));
        CurrentMaster.Title.TitleImage = GetImageUrlForHeader(SettingsGroupViewer.SettingsCategoryInfo.CategoryName);
        CurrentMaster.Title.HelpTopicName = GetHelpTopicName();

        // Check, if there are any groups
        DataSet ds = SettingsCategoryInfoProvider.GetSettingsCategories("CategoryIsGroup = 1 AND CategoryParentID = " + mCategoryId, null, -1, "CategoryID");
        if ((!DataHelper.DataSourceIsEmpty(ds)) || (!string.IsNullOrEmpty(search)))
        {
            CurrentMaster.HeaderActions.Actions = GetHeaderActions();
            CurrentMaster.HeaderActions.ActionPerformed += HeaderActions_ActionPerformed;
        }
        else
        {
            lblNoSettings.Visible = true;
        }

        ScriptHelper.RegisterSaveShortcut(this, "save", false);
    }

    #endregion


    #region "Private Methods"


    /// <summary>
    /// Script for search parameters for site selector
    /// </summary>
    private void RegisterSearchScript()
    {
        Literal searchScript = new Literal();

        string script = @" 
    function GetSearchValues() {
        var search = document.getElementById('" + txtSearch.ClientID + @"').value;
        var searchSettings = new Array('" + mCategoryId + @"', document.getElementById('" + txtSearch.ClientID + @"').value, document.getElementById('" + chkDescription.ClientID + @"').checked );
        return searchSettings;
    }";

        searchScript.Text = ScriptHelper.GetScript(script);
        this.Controls.Add(searchScript);
    }


    /// <summary>
    /// Returns image URL for category.
    /// </summary>
    /// <param name="categoryName">Category name</param>
    /// <returns>Url of an image.</returns>
    private string GetImageUrlForHeader(string categoryName)
    {
        string imageUrl = String.Empty;

        if (!string.IsNullOrEmpty(categoryName))
        {
            imageUrl = GetImagePath("CMSModules/CMS_Settings/Categories/") + categoryName.Replace(".", "_") + "/module.png";


           
            // Check if category icon exists and then use it
            if (File.Exists(Server.MapPath(imageUrl)))
            {
                imageUrl = ResolveUrl(imageUrl);
            }
            // Otherwise use default icon
            else
            {
                imageUrl = ResolveUrl(GetImageUrl("CMSModules/CMS_Settings/Categories/module.png"));
            }
        }

        return imageUrl;
    }


    /// <summary>
    /// Gets string array representing header actions.
    /// </summary>
    /// <returns>Array of strings</returns>
    private string[,] GetHeaderActions()
    {
        string[,] actions = new string[2, 9];

        actions[0, 0] = HeaderActions.TYPE_SAVEBUTTON;
        actions[0, 1] = GetString("Header.Settings.SaveChanged");
        actions[0, 2] = "";
        actions[0, 3] = null;
        actions[0, 4] = null;
        actions[0, 5] = GetImageUrl("CMSModules/CMS_Content/EditMenu/save.png");
        actions[0, 6] = "lnkSaveChanges_Click";
        actions[0, 7] = String.Empty;
        actions[0, 8] = true.ToString();

        actions[1, 0] = HeaderActions.TYPE_SAVEBUTTON;
        actions[1, 1] = GetString("Header.Settings.ResetToDefault");
        actions[1, 2] = string.Format(@" if (confirm({0})) {{ return true; }} return false;", ScriptHelper.GetString(GetString("Header.Settings.ResetToDefaultConfirmation")));
        actions[1, 3] = null;
        actions[1, 4] = null;
        actions[1, 5] = GetImageUrl("Objects/CMS_SettingsKey/object.png");
        actions[1, 6] = "lnkResetToDefault_Click";
        actions[1, 7] = String.Empty;

        return actions;
    }


    /// <summary>
    /// Gets help topic name for the current settings category name.
    /// </summary>
    /// <returns>String representing help topic name.</returns>
    private string GetHelpTopicName()
    {
        string helpTopicName = string.Empty;

        if ((SettingsGroupViewer.SettingsCategoryInfo != null) && !string.IsNullOrEmpty(SettingsGroupViewer.SettingsCategoryInfo.CategoryName))
        {
            string lowCatName = SettingsGroupViewer.SettingsCategoryInfo.CategoryName.ToLower();

            if (lowCatName.StartsWith("cms."))
            {
                helpTopicName = "settings_" + lowCatName.Remove(0, 4).Replace(".", "");
            }
        }

        return helpTopicName;
    }


    /// <summary>
    /// Saves the settings.
    /// </summary>
    private void SaveSettings()
    {
        SettingsGroupViewer.SaveChanges();

        // Special case for Debug category - debug settings have to be reloaded to take effect
        if (SettingsGroupViewer.CategoryName.ToLower() == "cms.system.debug")
        {
            CMSFunctions.ResetDebugSettings();
        }
        else if (SettingsGroupViewer.CategoryName.ToLower() == "cms.system.performance")
        {
            RequestHelper.ResetPerformanceSettings();
        }
    }

    #endregion


    #region "Event Handlers"

    /// <summary>
    /// Handles actions performed on the master header.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void HeaderActions_ActionPerformed(object sender, CommandEventArgs e)
    {
        switch (e.CommandName.ToLower())
        {
            case "lnksavechanges_click":
                SaveSettings();
                break;

            case "lnkresettodefault_click":
                if ((mSiteId >= 0) && (mCategoryId > 0))
                {
                    SettingsGroupViewer.ResetToDefault();
                    URLHelper.Redirect(string.Format(@"Keys.aspx?resettodefault=1&categoryid={0}&siteid={1}&search={2}&description={3}", mCategoryId, mSiteId, txtSearch.Text, chkDescription.Checked));
                }
                break;
        }
    }

    /// <summary>
    /// Handles search button action
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSearch_OnClick(object sender, EventArgs e)
    {
        URLHelper.Redirect(string.Format(@"Keys.aspx?categoryid={0}&siteid={1}&search={2}&description={3}", mCategoryId, mSiteId, txtSearch.Text, chkDescription.Checked));
    }


    #endregion


    #region "IPostBackEventHandler Members"

    /// <summary>
    /// Handles postback events.
    /// </summary>
    /// <param name="eventArgument">Postback argument</param>
    public void RaisePostBackEvent(string eventArgument)
    {
        if (eventArgument.ToLower() == "save")
        {
            SaveSettings();
        }
    }

    #endregion
}