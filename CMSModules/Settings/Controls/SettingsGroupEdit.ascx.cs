using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using CMS.UIControls;
using CMS.GlobalHelper;
using CMS.SiteProvider;
using CMS.SettingsProvider;

public partial class CMSModules_Settings_Controls_SettingsGroupEdit : CMSUserControl
{
    #region "Variables"

    /// <summary>
    /// Code name of displayed category. This category is NOT group (CategoryIsGroup = false).
    /// </summary>
    private string mCategoryName = null;

    /// <summary>
    /// Allows filtering for specific site.
    /// </summary>
    private int mSiteId = 0;

    #endregion


    #region "Properties"

    /// <summary>
    /// Code name of displayed category. This category is NOT group (CategoryIsGroup = false).
    /// </summary>
    public string CategoryName
    {
        get
        {
            return mCategoryName;
        }
        set
        {
            mCategoryName = value;
        }
    }

    /// <summary>
    /// Allows filtering of keys only for specific site. Set SiteID = 0 for all sites (dafault).
    /// </summary>
    public int SiteID
    {
        get
        {
            return mSiteId;
        }
        set
        {
            mSiteId = value;
        }
    }

    #endregion


    #region "Events"

    /// <summary>
    /// Event raised, when edit/delete/moveUp/moveDown category button is clicked.
    /// </summary>
    public event CommandEventHandler ActionPerformed;


    /// <summary>
    /// Event raised, when asked to add new key.
    /// </summary>
    public event CommandEventHandler OnNewKey;


    /// <summary>
    /// Event raised, when unigrid's button is clicked.
    /// </summary>
    public event OnActionEventHandler OnKeyAction;

    #endregion


    #region "Control events"

    /// <summary>
    /// Handles the whole category actions.
    /// </summary>
    /// <param name="sender">Sender of event</param>
    /// <param name="e">Event arguments</param>
    protected void group_ActionPerformed(object sender, CommandEventArgs e)
    {
        if (ActionPerformed != null)
        {
            ActionPerformed(sender, e);
        }
    }


    /// <summary>
    /// Handles request for creating of new settings key.
    /// </summary>
    /// <param name="sender">Sender of event</param>
    /// <param name="e">Event arguments</param>
    protected void group_OnNewKey(object sender, CommandEventArgs e)
    {
        if (OnNewKey != null)
        {
            OnNewKey(sender, e);
        }
    }


    /// <summary>
    /// Handles settings keys actions (delete, edit, move up, move down).
    /// </summary>
    /// <param name="actionName">Name of the action</param>
    /// <param name="argument">Argument of the action</param>
    protected void group_OnKeyAction(string actionName, object actionArgument)
    {
        if (OnKeyAction != null)
        {
            OnKeyAction(actionName, actionArgument);
        }
    }

    #endregion


    #region "Methods"

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        // Reload data;
        ReloadData();
    }


    /// <summary>
    /// Creates SettingsGroup panel for a specified category.
    /// </summary>
    /// <param name="category">Non-group category</param>
    protected CMSUserControl CreatePanelForCategory(SettingsCategoryInfo category)
    {
        if (category == null)
        {
            return null;
        }

        // Create new Category bar and initialize it
        SettingsGroup group = Page.LoadControl("~/CMSModules/Settings/Controls/SettingsGroup.ascx") as SettingsGroup;
        group.Category = category;
        group.SiteID = mSiteId;
        group.ActionPerformed += new CommandEventHandler(group_ActionPerformed);
        group.OnNewKey += new CommandEventHandler(group_OnNewKey);
        group.OnKeyAction += new OnActionEventHandler(group_OnKeyAction);

        return group;
    }


    /// <summary>
    /// Reloads data.
    /// </summary>
    public void ReloadData()
    {
        CMSUserControl catPanel;
        // Get data
        DataSet ds = SettingsCategoryInfoProvider.GetChildSettingsCategories(mCategoryName, "CategoryIsGroup = 1");

        this.Controls.Clear();

        if (!DataHelper.DataSourceIsEmpty(ds))
        {
            DataRowCollection rows = ds.Tables[0].Rows;
            foreach (DataRow row in rows)
            {
                // Create new panel with info about subcategory
                SettingsCategoryInfo sci = new SettingsCategoryInfo(row);
                catPanel = CreatePanelForCategory(sci);
                catPanel.ID = "CategoryPanel_" + sci.CategoryName.Replace(".", "_");
                this.Controls.Add(catPanel);
            }
        }
    }

    #endregion
}
