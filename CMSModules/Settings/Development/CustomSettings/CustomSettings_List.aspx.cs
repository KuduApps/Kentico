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

using CMS.SiteProvider;
using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.SettingsProvider;

public partial class CMSModules_Settings_Development_CustomSettings_CustomSettings_List : SiteManagerPage
{
    #region "Variables"

    /// <summary>
    /// Displayed category.
    /// </summary>
    private SettingsCategoryInfo mCategory = null;

    /// <summary>
    /// Root category name of displayed tree.
    /// </summary>
    private string mTreeRoot = "customsettings";

    /// <summary>
    /// Id of the site;.
    /// </summary>
    private int mSiteId = 0;

    #endregion


    #region "Page events"

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        int categoryId = QueryHelper.GetInteger("categoryid", -1);
        // Get root of tree
        mTreeRoot = QueryHelper.GetString("treeroot", "customsettings");
        // Get site id
        mSiteId = QueryHelper.GetInteger("siteid", 0);

        // Find category
        if (categoryId >= 0)
        {
            mCategory = SettingsCategoryInfoProvider.GetSettingsCategoryInfo(categoryId);
        }

        // Use root category for CustomSettings or Settings if category not found or specified
        if((categoryId == -1) || (mCategory == null))
        {
            switch (mTreeRoot)
            {
                case ("customsettings"):
                    grpEdit.CategoryName = "CMS.CustomSettings";
                    break;
                case ("settings"):
                default:
                    grpEdit.CategoryName = "CMS.Settings";
                    break;
            }

            mCategory = SettingsCategoryInfoProvider.GetSettingsCategoryInfoByName(grpEdit.CategoryName);
        }
        
        grpEdit.CategoryName = mCategory.CategoryName;
        grpEdit.SiteID = mSiteId;
        grpEdit.ActionPerformed += new CommandEventHandler(grpEdit_ActionPerformed);
        grpEdit.OnNewKey += new CommandEventHandler(grpEdit_OnNewKey);
        grpEdit.OnKeyAction += new OnActionEventHandler(grpEdit_OnKeyAction);

        // Read data
        grpEdit.ReloadData();
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        if (mCategory != null)
        {
            // Header actions
            string[,] actions = new string[4, 13];

            // Disable inserting groups under root category
            if (mCategory.CategoryName != "CMS.Settings")
            {
                // Edit button
                actions[0, 0] = HeaderActions.TYPE_HYPERLINK;
                actions[0, 1] = ResHelper.GetString("Development.CustomSettings.NewGroup");
                actions[0, 3] = URLHelper.ResolveUrl("~/CMSModules/Settings/Development/CustomSettings/CustomSettingsCategory_Edit.aspx?showtitle=1&treeroot=" + mTreeRoot + "&isgroup=1&parentid=" + mCategory.CategoryID);
                actions[0, 5] = GetImageUrl("Objects/CMS_CustomSettings/add.png");
                actions[0, 7] = "";
                actions[0, 11] = "";
                actions[0, 12] = "true";

                this.CurrentMaster.HeaderActions.Actions = actions;
            }
        }
    }

    #endregion


    #region "Events handling"

    /// <summary>
    /// Handles the whole category actions.
    /// </summary>
    /// <param name="sender">Sender of event</param>
    /// <param name="e">Event arguments</param>
    protected void grpEdit_ActionPerformed(object sender, CommandEventArgs e)
    {
        int categoryId = ValidationHelper.GetInteger(e.CommandArgument, 0);
        switch (e.CommandName.ToLower())
        {
            case ("edit"):               
                SettingsCategoryInfo sci = SettingsCategoryInfoProvider.GetSettingsCategoryInfo(categoryId);
                if (sci != null)
                {
                    URLHelper.Redirect("~/CMSModules/Settings/Development/CustomSettings/CustomSettingsCategory_Edit.aspx?treeroot=" + mTreeRoot + "&isgroup=1&categoryid=" + categoryId);
                }
                break;
               
            case ("delete"):
                try
                {
                    SettingsCategoryInfo categoryObj = SettingsCategoryInfoProvider.GetSettingsCategoryInfo(categoryId);
                     if (categoryObj.CategoryName != "CMS.CustomSettings")
                     {
                         SettingsCategoryInfoProvider.DeleteSettingsCategoryInfo(categoryObj);
                     }
                }
                catch
                {
                    lblError.Text = GetString("settings.group.deleteerror");
                    lblError.Visible = true;
                }
                grpEdit.ReloadData();
                break;

            case ("moveup"):
                SettingsCategoryInfoProvider.MoveCategoryUp(categoryId);
                grpEdit.ReloadData();
                break;

            case ("movedown"):
                SettingsCategoryInfoProvider.MoveCategoryDown(categoryId);
                grpEdit.ReloadData();
                break;
        }
    }


    /// <summary>
    /// Handles creation of new settings key.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    void grpEdit_OnNewKey(object sender, CommandEventArgs e)
    {
        URLHelper.Redirect("~/CMSModules/Settings/Development/CustomSettings/SettingsKey_Edit.aspx?treeroot="+mTreeRoot+"&" + e.CommandArgument);
    }
    

    /// <summary>
    /// Handles the settings key action event.
    /// </summary>
    /// <param name="actionName">Name of item (button) that throws event</param>
    /// <param name="actionArgument">ID (value of Primary key) of corresponding data row</param>
    protected void grpEdit_OnKeyAction(string actionName, object actionArgument)
    {
        int keyId = ValidationHelper.GetInteger(actionArgument, 0);
        SettingsKeyInfo ski = SettingsKeyProvider.GetSettingsKeyInfo(keyId);

        switch (actionName.ToLower())
        {
            case ("edit"):
                // Redirect to key-editing page
                if (ski != null)
                {
                    URLHelper.Redirect("~/CMSModules/Settings/Development/CustomSettings/SettingsKey_Edit.aspx?treeroot=" + mTreeRoot + "&keyname=" + ski.KeyName + "&siteid=" + mSiteId);
                }
                break;

            case ("delete"):
                try
                {
                    // Delete all keys
                    DataSet ds = SiteInfoProvider.GetSites(null, null, "[SiteName]");
                    if (!DataHelper.DataSourceIsEmpty(ds))
                    {
                        DataTable tbl = ds.Tables[0];
                        foreach (DataRow row in tbl.Rows)
                        {
                            string siteName = ValidationHelper.GetString(row[0], "");
                            if (!string.IsNullOrEmpty(siteName))
                            {
                                SettingsKeyProvider.DeleteKey(string.Format("{0}.{1}", siteName, ski.KeyName));
                            }
                        }
                    }
                    SettingsKeyProvider.DeleteKey(ski.KeyName);
                }
                catch
                {
                    lblError.Text = GetString("settingsedit.settingskey_edit.errordelete");
                    lblError.Visible = true;
                }
                break;

            case ("moveup"):
                SettingsKeyProvider.MoveSettingsKeyUp(ski.KeyName);
                break;

            case ("movedown"):
                SettingsKeyProvider.MoveSettingsKeyDown(ski.KeyName);
                break;
        }
    }

    #endregion
}

