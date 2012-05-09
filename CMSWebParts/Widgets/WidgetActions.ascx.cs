using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Collections.Generic;

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.PortalControls;
using CMS.PortalEngine;
using CMS.TreeEngine;
using CMS.WorkflowEngine;
using CMS.URLRewritingEngine;
using CMS.SettingsProvider;

public partial class CMSWebParts_Widgets_WidgetActions : CMSAbstractWebPart
{
    #region "Variables"

    private WidgetZoneTypeEnum zoneType = WidgetZoneTypeEnum.None;
    private PageInfo pi = null;
    private TreeProvider mTreeProvider = null;
    private bool resetAllowed = true;
    private WebPartZoneInstance zoneInstance = null;
    List<WebPartZoneInstance> zoneInstances = new List<WebPartZoneInstance>();

    #endregion


    #region "Public properties"

    /// <summary>
    /// Gets or sets widget zone type.
    /// </summary>
    public string WidgetZoneType
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("WidgetZoneType"), String.Empty);
        }
        set
        {
            this.SetValue("WidgetZoneType", value);
        }
    }


    /// <summary>
    /// Gets or sets widget zone type.
    /// </summary>
    public string WidgetZoneID
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("WidgetZoneID"), String.Empty);
        }
        set
        {
            this.SetValue("WidgetZoneID", value);
        }
    }


    /// <summary>
    /// Gets or sets text for add button.
    /// </summary>
    public string AddButtonText
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("AddButtonText"), String.Empty);
        }
        set
        {
            this.SetValue("AddButtonText", value);
        }
    }


    /// <summary>
    /// Gets or sets text for reset button.
    /// </summary>
    public string ResetButtonText
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("ResetButtonText"), String.Empty);
        }
        set
        {
            this.SetValue("ResetButtonText", value);
        }
    }


    /// <summary>
    /// Enables or disables reset button.
    /// </summary>
    public bool DisplayResetButton
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("DisplayResetButton"), true);
        }
        set
        {
            this.SetValue("DisplayResetButton", value);
        }
    }


    /// <summary>
    /// Enables or disables add widget button.
    /// </summary>
    public bool DisplayAddButton
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("DisplayAddButton"), true);
        }
        set
        {
            this.SetValue("DisplayAddButton", value);
        }
    }


    /// <summary>
    /// Enables or disables confirmation for reset button.
    /// </summary>
    public bool ResetConfirmationRequired
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("ResetConfirmationRequired"), true);
        }
        set
        {
            this.SetValue("ResetConfirmationRequired", value);
        }
    }


    /// <summary>
    /// Returns instance of tree provider.
    /// </summary>
    public TreeProvider TreeProvider
    {
        get
        {
            if (mTreeProvider == null)
            {
                mTreeProvider = new TreeProvider(CMSContext.CurrentUser);
            }
            return mTreeProvider;
        }
        set
        {
            mTreeProvider = value;
        }
    }

    #endregion


    #region "Methods"

    /// <summary>
    /// Content loaded event handler.
    /// </summary>
    public override void OnContentLoaded()
    {
        base.OnContentLoaded();
        SetupControl();
    }


    /// <summary>
    /// Initializes the control properties.
    /// </summary>
    protected void SetupControl()
    {
        if (this.StopProcessing)
        {
            // Do nothing
        }
        else
        {
            pi = CMSContext.CurrentPageInfo;
            if (pi != null)
            {
                CMSPagePlaceholder parentPlaceHolder = PortalHelper.FindParentPlaceholder(this);

                // Nothing to render, nothing to do
                if ((!this.DisplayAddButton && !this.DisplayResetButton) || ((parentPlaceHolder != null) && (parentPlaceHolder.UsingDefaultPageTemplate)))
                {
                    this.Visible = false;
                    return;
                }

                CurrentUserInfo currentUser = CMSContext.CurrentUser;
                zoneType = WidgetZoneTypeCode.ToEnum(this.WidgetZoneType);


                // Check security
                if (((zoneType == WidgetZoneTypeEnum.Group) && !currentUser.IsGroupAdministrator(pi.NodeGroupId))
                    || ((zoneType == WidgetZoneTypeEnum.User || zoneType == WidgetZoneTypeEnum.Dashboard) && !currentUser.IsAuthenticated()))
                {
                    this.Visible = false;
                    resetAllowed = false;
                    return;
                }

                // Displaying - Editor zone only in edit mode, User/Group zone only in Live site/Preview mode
                if (((zoneType == WidgetZoneTypeEnum.Editor) && (CMSContext.ViewMode != ViewModeEnum.Edit)) ||
                    (((zoneType == WidgetZoneTypeEnum.User) || (zoneType == WidgetZoneTypeEnum.Group)) && ((CMSContext.ViewMode != ViewModeEnum.LiveSite) && (CMSContext.ViewMode != ViewModeEnum.Preview))) || ((zoneType == WidgetZoneTypeEnum.Dashboard) && ((CMSContext.ViewMode != ViewModeEnum.DashboardWidgets) || (String.IsNullOrEmpty(PortalContext.DashboardName)))))
                {
                    this.Visible = false;
                    resetAllowed = false;
                    return;
                }

                // Get current document
                TreeNode currentNode = DocumentHelper.GetDocument(pi.DocumentId, this.TreeProvider);
                if (((zoneType == WidgetZoneTypeEnum.Editor) && (!currentUser.IsEditor || (currentUser.IsAuthorizedPerDocument(currentNode, NodePermissionsEnum.Modify) == AuthorizationResultEnum.Denied))))
                {
                    this.Visible = false;
                    resetAllowed = false;
                    return;
                }

                // If use checkin checkout enabled, check if document is checkout by current user
                if (zoneType == WidgetZoneTypeEnum.Editor)
                {
                    if (currentNode != null)
                    {
                        WorkflowManager wm = new WorkflowManager(this.TreeProvider);
                        // Get workflow info
                        WorkflowInfo wi = wm.GetNodeWorkflow(currentNode);

                        // Check if node is under workflow and if use checkin checkout enabled
                        if ((wi != null) && (wi.UseCheckInCheckOut(CMSContext.CurrentSiteName)))
                        {
                            int checkedOutBy = currentNode.DocumentCheckedOutByUserID;

                            // Check if document is checkout by current user
                            if (checkedOutBy != CMSContext.CurrentUser.UserID)
                            {
                                this.Visible = false;
                                resetAllowed = false;
                                return;
                            }
                        }
                    }
                }

                // Find widget zone
                PageTemplateInfo pti = pi.PageTemplateInfo;

                // ZodeID specified directly
                if (!String.IsNullOrEmpty(this.WidgetZoneID))
                {
                    zoneInstance = pti.GetZone(this.WidgetZoneID);
                }

                // Zone not find or specified zone is not of correct type
                if ((zoneInstance != null) && (zoneInstance.WidgetZoneType != zoneType))
                {
                    zoneInstance = null;
                }

                // For delete all variants all zones are necessairy
                if (parentPlaceHolder != null)
                {
                    ArrayList zones = parentPlaceHolder.WebPartZones;
                    if (zones != null)
                    {
                        foreach (CMSWebPartZone zone in zones)
                        {
                            if ((zone.ZoneInstance != null) && (zone.ZoneInstance.WidgetZoneType == zoneType))
                            {
                                zoneInstances.Add(zone.ZoneInstance);
                                if (zoneInstance == null)
                                {
                                    zoneInstance = zone.ZoneInstance;
                                }
                            }
                        }
                    }
                }

                // No suitable zones on the page, nothing to do
                if (zoneInstance == null)
                {
                    this.Visible = false;
                    resetAllowed = false;
                    return;
                }

                // Adding is enabled
                if (this.DisplayAddButton)
                {
                    pnlAdd.Visible = true;
                    lnkAddWidget.Visible = true;
                    lnkAddWidget.Text = DataHelper.GetNotEmpty(this.AddButtonText, GetString("widgets.addwidget"));

                    int templateId = 0;
                    if (pi.PageTemplateInfo != null)
                    {
                        templateId = pi.PageTemplateInfo.PageTemplateId;
                    }

                    string script = "NewWidget('" + HttpUtility.UrlEncode(zoneInstance.ZoneID) + "', '" + HttpUtility.UrlEncode(pi.NodeAliasPath) + "', '" + templateId + "'); return false;";
                    lnkAddWidget.Attributes.Add("onclick", script);
                }

                // Reset is enabled
                if (this.DisplayResetButton)
                {
                    pnlReset.Visible = true;
                    btnReset.Text = DataHelper.GetNotEmpty(this.ResetButtonText, GetString("widgets.resettodefault"));
                    btnReset.Click += new EventHandler(btnReset_Click);

                    // Add confirmation if required
                    if (this.ResetConfirmationRequired)
                    {
                        btnReset.Attributes.Add("onclick", "if (!confirm('" + GetString("widgets.resetzoneconfirmtext") + "')) return false;");
                    }
                }

                // Set the panel css clas with dependence on actions zone type
                switch (zoneType)
                {
                    // Editor
                    case WidgetZoneTypeEnum.Editor:
                        pnlWidgetActions.CssClass = "EditorWidgetActions";
                        break;

                    // User
                    case WidgetZoneTypeEnum.User:
                        pnlWidgetActions.CssClass = "UserWidgetActions";
                        break;

                    // Group
                    case WidgetZoneTypeEnum.Group:
                        pnlWidgetActions.CssClass = "GroupWidgetActions";
                        break;

                    // Dashboard
                    case WidgetZoneTypeEnum.Dashboard:
                        pnlContextHelp.Visible = true;
                        break;
                }
            }
        }
    }


    /// <summary>
    /// Handles reset button click. Resets zones of specified type to default settings.
    /// </summary>
    protected void btnReset_Click(object sender, EventArgs e)
    {
        // Security check
        if (!DisplayResetButton || !resetAllowed)
        {
            return;
        }

        if (pi == null)
        {
            return;
        }

        if ((zoneType == WidgetZoneTypeEnum.Editor) || (zoneType == WidgetZoneTypeEnum.Group))
        {
            // Clear document webparts/group webparts
            TreeNode node = DocumentHelper.GetDocument(pi.DocumentId, this.TreeProvider);

            if (node != null)
            {
                if (zoneType == WidgetZoneTypeEnum.Editor)
                {
                    node.SetValue("DocumentWebParts", String.Empty);

                    // Delete all variants 
                    if (pi.PageTemplateInfo != null)
                    {
                        foreach (WebPartZoneInstance zoneInstance in zoneInstances)
                        {
                            if (zoneInstance.WebPartsContainVariants)
                            {
                                ModuleCommands.OnlineMarketingResetMVTWidgetZone(zoneInstance.ZoneID, pi.PageTemplateInfo.PageTemplateId, node.DocumentID);
                                ModuleCommands.OnlineMarketingResetContentPersonalizationWidgetZone(zoneInstance.ZoneID, pi.PageTemplateInfo.PageTemplateId, node.DocumentID);
                            }
                        }
                    }
                }
                else if (zoneType == WidgetZoneTypeEnum.Group)
                {
                    node.SetValue("DocumentGroupWebParts", String.Empty);
                }

                // Save the document
                DocumentHelper.UpdateDocument(node, this.TreeProvider);
            }
        }
        else if (zoneType == WidgetZoneTypeEnum.User)
        {
            // Delete user personalization info
            PersonalizationInfo up = PersonalizationInfoProvider.GetUserPersonalization(CMSContext.CurrentUser.UserID, pi.DocumentId);
            PersonalizationInfoProvider.DeletePersonalizationInfo(up);

            // Clear cached values
            TreeNode node = DocumentHelper.GetDocument(pi.DocumentId, this.TreeProvider);
            if (node != null)
            {
                CacheHelper.TouchKeys(TreeProvider.GetDependencyCacheKeys(node, CMSContext.CurrentSiteName));
            }
        }
        else if (zoneType == WidgetZoneTypeEnum.Dashboard)
        {
            // Delete user personalization info
            PersonalizationInfo up = PersonalizationInfoProvider.GetDashBoardPersonalization(CMSContext.CurrentUser.UserID, PortalContext.DashboardName, PortalContext.DashboardSiteName);
            PersonalizationInfoProvider.DeletePersonalizationInfo(up);

            // Clear cached page template
            if (pi.PageTemplateInfo != null)
            {
                CacheHelper.TouchKey("cms.pagetemplate|byid|" + pi.PageTemplateInfo.PageTemplateId);
            }
        }

        // Make redirect to see changes after load
        string url = URLRewriter.CurrentURL;
        URLHelper.Redirect(url);
    }


    /// <summary>
    /// Reloads the control data.
    /// </summary>
    public override void ReloadData()
    {
        SetupControl();
        base.ReloadData();
    }

    #endregion
}
