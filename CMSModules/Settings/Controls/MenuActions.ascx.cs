using System;

using CMS.UIControls;
using CMS.GlobalHelper;
using CMS.SiteProvider;

public partial class CMSModules_Settings_Controls_MenuActions : CMSUserControl
{
    #region "Variables"

    private int mTabIndex = 0;
    private bool mWholeSettings = false;

    #endregion


    #region "Constructor"

    public CMSModules_Settings_Controls_MenuActions()
    {
        ResourceID = 0;
        ElementID = 0;
        ParentID = 0;
    }

    #endregion


    #region "Properties"

    /// <summary>
    /// Resource ID.
    /// </summary>
    public int ResourceID
    {
        get;
        set;
    }


    /// <summary>
    /// Element ID.
    /// </summary>
    public int ElementID
    {
        get;
        set;
    }


    /// <summary>
    /// Enables use of whole settings tree. If false, uses only custom settings subtree.
    /// </summary>
    public bool WholeSettings
    {
        get
        {
            return mWholeSettings;
        }
        set
        {
            mWholeSettings = value;
        }
    }



    /// <summary>
    /// Parent element ID.
    /// </summary>
    public int ParentID
    {
        get;
        set;
    }


    /// <summary>
    /// Gets or sets value of hidden field where are stroed ElementID and ParentID separated by |.
    /// </summary>
    public string Value
    {
        get
        {
            return hidSelectedElem.Value;
        }
        set
        {
            hidSelectedElem.Value = value;
        }
    }


    /// <summary>
    /// Event called after menu action is executed.
    /// </summary>
    public event OnActionEventHandler AfterAction = null;

    #endregion


    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!StopProcessing)
        {
            imgNewElem.ImageUrl = GetImageUrl("CMSModules/CMS_Settings/add.png");
            imgDeleteElem.ImageUrl = GetImageUrl("CMSModules/CMS_Settings/delete.png");
            imgMoveUp.ImageUrl = GetImageUrl("CMSModules/CMS_Settings/up.png");
            imgMoveDown.ImageUrl = GetImageUrl("CMSModules/CMS_Settings/down.png");

            btnNewElem.Text = GetString("settings.newelem");
            btnDeleteElem.Text = GetString("settings.deleteelem");
            btnMoveUp.Text = GetString("settings.modeupelem");
            btnMoveDown.Text = GetString("settings.modedownelem");

            btnNewElem.ToolTip = GetString("settings.newelem");
            btnDeleteElem.ToolTip = GetString("settings.deleteelem");
            btnMoveUp.ToolTip = GetString("settings.modeupelem");
            btnMoveDown.ToolTip = GetString("settings.modedownelem");

            // Create new element javascript
            string newScript = "var hidElem = document.getElementById('" + hidSelectedElem.ClientID + "'); var ids = hidElem.value.split('|');";
            newScript += "if ((window.parent != null) && (window.parent.frames['customsettingsmain'] != null)) {";
            newScript += "window.parent.frames['customsettingsmain'].location = '" + ResolveUrl("~/CMSModules/Settings/Development/CustomSettings/CustomSettingsCategory_Edit.aspx") + "?showtitle=1&treeroot=" + (WholeSettings ? "settings" : "customsettings") + "&parentId=' + ids[0];";
            newScript += "} return false;";
            btnNewElem.OnClientClick = newScript;

            // Confirm delete
            btnDeleteElem.OnClientClick = "return deleteConfirm();";

            string script = "var menuHiddenId = '" + hidSelectedElem.ClientID + "';";
            script += "function deleteConfirm() {";
            script += "return confirm(" + ScriptHelper.GetString(GetString("settings.categorydeleteconfirmation")) + ");";
            script += "}";
            ltlScript.Text = ScriptHelper.GetScript(script);
        }
    }

    #endregion


    #region "Control events"

    protected void btnMoveUp_Click(object sender, EventArgs e)
    {
        GetHiddenValues();
        if (ElementID > 0)
        {
            SettingsCategoryInfoProvider.MoveCategoryUp(ElementID);
            if (AfterAction != null)
            {
                AfterAction("moveup", ElementID + "|" + mTabIndex);
            }
        }
    }


    protected void btnMoveDown_Click(object sender, EventArgs e)
    {
        GetHiddenValues();
        if (ElementID > 0)
        {
            SettingsCategoryInfoProvider.MoveCategoryDown(ElementID);
            if (AfterAction != null)
            {
                AfterAction("movedown", ElementID + "|" + mTabIndex);
            }
        }
    }


    protected void btnNewElem_Click(object sender, EventArgs e)
    {
        GetHiddenValues();
        string script = "if ((window.parent != null) && (window.parent.frames['customsettingstree'] != null) && (window.parent.frames['customsettingsmain'] != null)) {";
        script += "window.parent.frames['customsettingstree'].location = '" + ResolveUrl("~/CMSModules/Settings/Development/CustomSettings/CustomSettings_Menu.aspx") + "?path=" + ResourceID + "';";
        script += "window.parent.frames['customsettingsmain'].location = '" + ResolveUrl("~/CMSModules/Modules/Pages/Development/Module_UI_General.aspx") + "?moduleid=" + ResourceID + "&parentId=" + ElementID + "';";
        script += "}";
        ltlScript.Text += ScriptHelper.GetScript(script);
        if (AfterAction != null)
        {
            AfterAction("new", ElementID);
        }
    }


    protected void btnDeleteElem_Click(object sender, EventArgs e)
    {
        GetHiddenValues();
        if ((ElementID > 0) && (ParentID > 0))
        {
            SettingsCategoryInfo categoryObj = SettingsCategoryInfoProvider.GetSettingsCategoryInfo(ElementID);
            if (categoryObj.CategoryName != "CMS.CustomSettings")
            {
                SettingsCategoryInfoProvider.DeleteSettingsCategoryInfo(categoryObj);
                if (AfterAction != null)
                {
                    AfterAction("delete", ParentID);
                }
            }
        }
    }

    #endregion


    #region "Private methods"

    private void GetHiddenValues()
    {
        string hidValue = hidSelectedElem.Value;
        string[] split = hidValue.Split('|');
        if (split.Length >= 2)
        {
            ElementID = ValidationHelper.GetInteger(split[0], 0);
            ParentID = ValidationHelper.GetInteger(split[1], 0);
            if (split.Length == 3)
            {
                mTabIndex = ValidationHelper.GetInteger(split[2], 0);
            }
        }
    }
    #endregion
}
