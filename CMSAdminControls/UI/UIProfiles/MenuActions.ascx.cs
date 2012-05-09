using System;

using CMS.UIControls;
using CMS.GlobalHelper;
using CMS.SiteProvider;

public partial class CMSAdminControls_UI_UIProfiles_MenuActions : CMSUserControl
{
    #region "Variables"

    private int mTabIndex = 0;

    public CMSAdminControls_UI_UIProfiles_MenuActions()
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
            imgNewElem.ImageUrl = GetImageUrl("CMSModules/CMS_UIProfiles/add.png");
            imgDeleteElem.ImageUrl = GetImageUrl("CMSModules/CMS_UIProfiles/delete.png");
            imgMoveUp.ImageUrl = GetImageUrl("CMSModules/CMS_UIProfiles/up.png");
            imgMoveDown.ImageUrl = GetImageUrl("CMSModules/CMS_UIProfiles/down.png");

            btnNewElem.Text = GetString("resource.ui.newelem");
            btnDeleteElem.Text = GetString("resource.ui.deleteelem");
            btnMoveUp.Text = GetString("resource.ui.modeupelem");
            btnMoveDown.Text = GetString("resource.ui.modedownelem");

            // Create new element javascript
            string newScript = "var hidElem = document.getElementById('" + hidSelectedElem.ClientID + "'); var ids = hidElem.value.split('|');";
            newScript += "if ((window.parent != null) && (window.parent.frames['uicontent'] != null)) {";
            newScript += "window.parent.frames['uicontent'].location = '" + ResolveUrl("~/CMSModules/Modules/Pages/Development/Module_UI_New.aspx") + "?moduleid=" + ResourceID + "&parentId=' + ids[0];";
            newScript += "} return false;";
            btnNewElem.OnClientClick = newScript;

            // Confirm delete
            btnDeleteElem.OnClientClick = "return deleteConfirm();";

            string script = "var menuHiddenId = '" + hidSelectedElem.ClientID + "';";
            script += "function deleteConfirm() {";
            script += "return confirm(" + ScriptHelper.GetString(GetString("resource.ui.confirmdelete")) + ");";
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
            UIElementInfoProvider.MoveUIElementUp(ElementID);
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
            UIElementInfoProvider.MoveUIElementDown(ElementID);
            if (AfterAction != null)
            {
                AfterAction("movedown", ElementID + "|" + mTabIndex);
            }
        }
    }


    protected void btnNewElem_Click(object sender, EventArgs e)
    {
        GetHiddenValues();
        string script = "if ((window.parent != null) && (window.parent.frames['header'] != null) && (window.parent.frames['content'] != null)) {";
        script += "window.parent.frames['header'].location = '" + ResolveUrl("~/CMSModules/Modules/Pages/Development/Module_UI_Header.aspx") + "?moduleid=" + ResourceID + "&new=1';";
        script += "window.parent.frames['content'].location = '" + ResolveUrl("~/CMSModules/Modules/Pages/Development/Module_UI_General.aspx") + "?moduleid=" + ResourceID + "&parentId=" + ElementID + "';";
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
            UIElementInfoProvider.DeleteUIElementInfo(ElementID);
            if (AfterAction != null)
            {
                AfterAction("delete", ParentID);
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
