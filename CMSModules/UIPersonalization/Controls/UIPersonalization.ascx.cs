using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.GlobalHelper;
using CMS.SiteProvider;
using CMS.CMSHelper;
using CMS.UIControls;
using CMS.SettingsProvider;

public partial class CMSModules_UIPersonalization_Controls_UIPersonalization : CMSUserControl
{
    #region "Variables"

    private int mSiteId = 0;
    private int mResourceId = 0;
    private int mRoleId = 0;
    private int mCurrentSiteID = 0;
    private string mCssClass = "";
    private bool globalRoles = false;
    private bool mHideSiteSelector = false;

    #endregion


    #region "Properties

    /// <summary>
    /// Gets or sets the ID of the module (if set, module selector is hidden).
    /// </summary>
    public int ResourceID
    {
        get
        {
            return this.mResourceId;
        }
        set
        {
            this.mResourceId = value;
        }
    }


    /// <summary>
    /// Gets or sets the ID of the site (if set, site selector is hidden).
    /// </summary>
    public int SiteID
    {
        get
        {
            return this.mSiteId;
        }
        set
        {
            this.mSiteId = value;
        }
    }

    /// <summary>
    /// If false hide site selector in all cases.
    /// </summary>
    public bool HideSiteSelector
    {
        get
        {
            return this.mHideSiteSelector;
        }
        set
        {
            this.mHideSiteSelector = value;
        }
    }


    /// <summary>
    /// Gets or sets the ID of the role (if set, role selector is hidden).
    /// </summary>
    public int RoleID
    {
        get
        {
            return this.mRoleId;
        }
        set
        {
            this.mRoleId = value;
        }
    }


    /// <summary>
    /// Indicates if control is used on live site.
    /// </summary>
    public override bool IsLiveSite
    {
        get
        {
            return base.IsLiveSite;
        }
        set
        {
            base.IsLiveSite = value;
            // Set is livesite
            this.selectSite.IsLiveSite = value;
            this.selectRole.IsLiveSite = value;
            this.treeElem.IsLiveSite = value;
            this.selectModule.IsLiveSite = value;
        }
    }


    /// <summary>
    /// Sets css class for tree.
    /// </summary>
    public string CssClass
    {
        set
        {
            mCssClass = value;
        }
    }


    #endregion


    #region "Private properties"

    /// <summary>
    /// Gets ID of the currently processed site.
    /// </summary>
    private int CurrentSiteID
    {
        get
        {
            if (this.mCurrentSiteID == 0)
            {
                this.mCurrentSiteID = GetSiteID();
                if ((this.mCurrentSiteID == 0) && (this.selectSite != null))
                {
                    try
                    {
                        this.selectSite.AllowEmpty = false;
                        this.selectSite.Reload(true);
                        this.selectSite.DataBind();
                    }
                    catch { }

                    this.mCurrentSiteID = ValidationHelper.GetInteger(selectSite.DropDownSingleSelect.SelectedValue, 0);
                }
            }
            return this.mCurrentSiteID;
        }
    }

    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        // Initialize header actions
        string[,] actions = new string[2, 7];
        actions[0, 0] = HeaderActions.TYPE_LINKBUTTON;
        actions[0, 1] = GetString("uiprofile.expandall");
        actions[0, 4] = GetString("uiprofile.expandall");
        actions[0, 5] = GetImageUrl("Objects/CMS_UIElement/expandall.png");
        actions[0, 6] = "expandall";
        actions[1, 0] = HeaderActions.TYPE_LINKBUTTON;
        actions[1, 1] = GetString("uiprofile.collapseall");
        actions[1, 4] = GetString("uiprofile.collapseall");
        actions[1, 5] = GetImageUrl("Objects/CMS_UIElement/collapseall.png");
        actions[1, 6] = "collapseall";
        this.actionsElem.Actions = actions;
        this.actionsElem.ActionPerformed += new CommandEventHandler(actionsElem_ActionPerformed);

        // Hide checkboxes with "group." prefix for WYSIWYG Editor
        this.treeElem.GroupPreffix = "group.";

        // Initialize selectors
        if (this.ResourceID > 0)
        {
            this.plcModule.Visible = false;
            this.selectModule.StopProcessing = true;
        }
        else
        {
            this.selectModule.UniSelector.OnSelectionChanged += new EventHandler(selectModule_OnSelectionChanged);
            this.selectModule.DropDownSingleSelect.AutoPostBack = true;
            this.lblModule.AssociatedControlClientID = this.selectModule.DropDownSingleSelect.ClientID;
            if (!URLHelper.IsPostback())
            {
                // Module preselection from query string
                string selectedModule = QueryHelper.GetString("module", null);
                if (!String.IsNullOrEmpty(selectedModule))
                {
                    ResourceInfo ri = ResourceInfoProvider.GetResourceInfo(selectedModule);
                    if (ri != null)
                    {
                        selectModule.Value = ri.ResourceId;
                    }
                }
            }
        }

        this.selectRole.CurrentSelector.SelectionMode = SelectionModeEnum.SingleDropDownList;
        this.selectRole.DropDownSingleSelect.AutoPostBack = true;
        this.selectRole.CurrentSelector.OnSelectionChanged += new EventHandler(selectRole_OnSelectionChanged);
        this.lblRole.AssociatedControlClientID = this.selectRole.DropDownSingleSelect.ClientID;

        if (HideSiteSelector)
        {
            plcSite.Visible = false;
        }
        else
        {
            this.selectSite.AllowGlobal = true;
            this.selectSite.DropDownSingleSelect.AutoPostBack = true;
            this.selectSite.UniSelector.OnSelectionChanged += new EventHandler(selectSite_OnSelectionChanged);
            this.selectSite.IsLiveSite = this.IsLiveSite;
            this.lblSite.AssociatedControlClientID = this.selectSite.DropDownSingleSelect.ClientID;
        }

        if (!URLHelper.IsPostback())
        {
            // Site selector in direct UI personalization
            if (SiteID <= 0)
            {
                this.selectSite.SiteID = ValidationHelper.GetInteger(this.selectSite.GlobalRecordValue, 0);
                this.globalRoles = true;
            }
            else
            {
                this.selectSite.SiteID = CurrentSiteID;
            }

            ReloadRoles();
            if (this.ResourceID <= 0)
            {
                ReloadModules();
            }
        }

        globalRoles = (ValidationHelper.GetString(selectSite.Value, "") == selectSite.GlobalRecordValue);

        if (this.RoleID > 0)
        {
            plcRole.Visible = false;
        }
        else
        {
            this.selectRole.SiteID = this.CurrentSiteID;
        }

        if (this.RoleID > 0 && ((this.SiteID > 0) || HideSiteSelector) && this.ResourceID > 0)
        {
            pnlActions.Visible = false;
        }

        this.selectModule.SiteID = this.CurrentSiteID;

        // Check manage permission
        if (!CMSContext.CurrentUser.IsAuthorizedPerResource("CMS.UIPersonalization", CMSAdminControl.PERMISSION_MODIFY))
        {
            this.treeElem.Enabled = false;
            this.lblInfo.Text = String.Format(GetString("CMSSiteManager.AccessDeniedOnPermissionName"), CMSAdminControl.PERMISSION_MODIFY);
            this.lblInfo.Visible = true;
        }


        ReloadTree();
    }


    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (this.SiteID > 0)
        {
            SiteInfo si = SiteInfoProvider.GetSiteInfo(SiteID);
            if (si != null)
            {
                this.lblDisabled.Visible = !SettingsKeyProvider.GetBoolValue(si.SiteName + ".CMSPersonalizeUserInterface");
            }
        }
        else
        {
            this.lblDisabled.Visible = !SettingsKeyProvider.GetBoolValue(this.selectSite.SiteName + ".CMSPersonalizeUserInterface");
        }
        if (!this.selectRole.CurrentSelector.HasData)
        {
            this.pnlTree.Visible = false;
            this.pnlAdditionalControls.Visible = false;
            this.lblNoRoleInfo.Visible = true;
        }
        else
        {
            if (plcModule.Visible && !this.selectModule.UniSelector.HasData)
            {
                this.pnlTree.Visible = false;
                this.pnlAdditionalControls.Visible = false;
                this.lblNoModuleInfo.Visible = true;
            }
            else
            {
                this.pnlTree.Visible = true;
                if (this.ResourceID > 0)
                {
                    if (!string.IsNullOrEmpty(mCssClass))
                    {
                        this.pnlTree.CssClass = mCssClass;
                    }
                    else
                    {
                        if (this.SiteID > 0)
                        {
                            this.pnlTree.CssClass = "UIPersonalizationTreeSmall";
                        }
                        else
                        {
                            this.pnlTree.CssClass = "UIPersonalizationTreeMedium";
                        }
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(mCssClass))
                    {
                        this.pnlTree.CssClass = mCssClass;
                    }
                    else
                    {
                        if (this.SiteID > 0)
                        {
                            this.pnlTree.CssClass = "UIPersonalizationTreeMedium";
                        }
                        else
                        {
                            this.pnlTree.CssClass = "UIPersonalizationTreeBig";
                        }
                    }
                }
            }
        }
    }


    #region "Event handlers"

    protected void actionsElem_ActionPerformed(object sender, CommandEventArgs e)
    {
        switch (e.CommandName.ToLower())
        {
            case "expandall":
                this.treeElem.CollapseAll = false;
                this.treeElem.ExpandAll = true;
                break;

            case "collapseall":
                this.treeElem.CollapseAll = true;
                this.treeElem.ExpandAll = false;
                break;
        }
        ReloadTree();
    }


    protected void selectSite_OnSelectionChanged(object sender, EventArgs e)
    {
        ReloadRoles();
        ReloadModules();
        ReloadTree();
    }


    protected void selectRole_OnSelectionChanged(object sender, EventArgs e)
    {
        ReloadModules();
        ReloadTree();
    }


    protected void selectModule_OnSelectionChanged(object sender, EventArgs e)
    {
        ReloadTree();
    }


    #endregion


    #region "Private methods"

    /// <summary>
    /// Reloads the tree.
    /// </summary>
    private void ReloadRoles()
    {
        this.selectRole.GlobalRoles = globalRoles;
        this.selectRole.SiteRoles = !globalRoles;

        this.selectRole.SiteID = this.CurrentSiteID;
        this.selectRole.Reload(true);
    }


    /// <summary>
    /// Reloads the modules.
    /// </summary>
    private void ReloadModules()
    {
        this.selectModule.DisplayOnlyForGivenSite = globalRoles ? false : true;
        this.selectModule.SiteID = globalRoles ? 0 : this.CurrentSiteID;
        this.selectModule.ReloadData(true);
    }


    /// <summary>
    /// Reloads the tree.
    /// </summary>
    private void ReloadTree()
    {
        this.treeElem.SiteID = globalRoles ? 0 : this.CurrentSiteID;

        // Use gievn RoleID if explicitly given
        this.treeElem.RoleID = this.RoleID > 0 ? this.RoleID : ValidationHelper.GetInteger(this.selectRole.Value, 0);
        if (this.ResourceID > 0)
        {
            this.treeElem.ModuleID = this.ResourceID;
        }
        else
        {
            this.treeElem.ModuleID = ValidationHelper.GetInteger(this.selectModule.Value, 0);
        }
        if ((this.treeElem.RoleID > 0) && (this.treeElem.ModuleID > 0))
        {
            this.treeElem.ReloadData();
        }
    }


    /// <summary>
    /// Gets the site ID from the selector.
    /// </summary>
    private int GetSiteID()
    {
        if (this.SiteID > 0)
        {
            return this.SiteID;
        }
        else
        {
            return (URLHelper.IsPostback() ? this.selectSite.SiteID : CMSContext.CurrentSiteID);
        }
    }

    #endregion
}
