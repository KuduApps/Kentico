using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Text;

using CMS.PortalEngine;
using CMS.TreeEngine;
using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.SiteProvider;
using CMS.EventLog;
using CMS.WorkflowEngine;
using CMS.UIControls;
using CMS.SettingsProvider;
using CMS.Synchronization;

using TreeNode = CMS.TreeEngine.TreeNode;

public partial class CMSModules_Content_Controls_Security : CMSUserControl
{
    #region "Variables"

    private AclProvider mAclProvider = null;
    private DataSet dsAclItems = null;
    private TreeProvider mTree = null;
    private TreeNode mNode = null;
    private EventLogProvider mEventLog = null;

    private int? mNodeID = null;
    private int? mFilterLimit = null;
    private bool hasModifyPermission = false;
    private bool mAllowEditUsers = true;
    private bool mAllowEditRoles = true;
    private bool isForceReload = false;
    private bool mDisplaySecurityMessage = false;

    private string ipAddress = null;
    private string eventUrl = null;
    
    #endregion


    #region "Private properties"

    /// <summary>
    /// Gets or sets disabled roles in selector.
    /// </summary>
    private string DisabledRoles
    {
        get
        {
            return ValidationHelper.GetString(ViewState["DisabledRoles"], String.Empty);
        }
        set
        {
            ViewState["DisabledRoles"] = value;
        }
    }


    /// <summary>
    /// Gets or sets disabled users in selector.
    /// </summary>
    private string DisabledUsers
    {
        get
        {
            return ValidationHelper.GetString(ViewState["DisabledUsers"], String.Empty);
        }
        set
        {
            ViewState["DisabledUsers"] = value;
        }
    }

    #endregion


    #region "Public properties"

    /// <summary>
    /// Information label.
    /// </summary>
    public Label InfoLabel
    {
        get
        {
            return lblInfo;
        }
    }


    /// <summary>
    /// Error label.
    /// </summary>
    public Label ErrorLabel
    {
        get
        {
            return lblError;
        }
    }


    /// <summary>
    /// Identifier of edited node.
    /// </summary>
    public int NodeID
    {
        get
        {
            if (mNodeID == null)
            {
                mNodeID = ValidationHelper.GetInteger(ViewState["NodeID"], QueryHelper.GetInteger("nodeid", 0));
            }
            return mNodeID.Value;
        }
        set
        {
            ViewState["NodeID"] = value;
            mNodeID = value;
        }
    }


    /// <summary>
    /// Currently edited node.
    /// </summary>
    public TreeNode Node
    {
        get
        {
            return mNode ?? (mNode = Tree.SelectSingleNode(NodeID, TreeProvider.ALL_CULTURES));
        }
        set
        {
            mNode = value;
            if (value != null)
            {
                NodeID = value.NodeID;
            }
        }
    }


    /// <summary>
    /// Whether to allow adding users.
    /// </summary>
    public bool AllowEditUsers
    {
        get
        {
            return mAllowEditUsers;
        }
        set
        {
            addUsers.Enabled = value;
            mAllowEditUsers = value;
        }
    }


    /// <summary>
    /// Whether to allow adding roles.
    /// </summary>
    public bool AllowEditRoles
    {
        get
        {
            return mAllowEditRoles;
        }
        set
        {
            addRoles.Enabled = value;
            mAllowEditRoles = value;
        }
    }


    /// <summary>
    /// Indiactes whether to allow redirection due to lack of permissions.
    /// </summary>
    public bool AllowRedirection
    {
        get
        {
            return ValidationHelper.GetBoolean(ViewState["AllowRedirection"], true);

        }
        set
        {
            ViewState["AllowRedirection"] = value;
        }
    }


    /// <summary>
    /// Gets or sets whether the submit button is visible or hidden.
    /// </summary>
    public bool DisplayButtons
    {
        get
        {
            return ValidationHelper.GetBoolean(ViewState["DisplayButtons"], true);
        }
        set
        {
            ViewState["DisplayButtons"] = value;
            SetButtonsVisibility();
        }
    }


    /// <summary>
    /// Minimal count of entries for display filter.
    /// </summary>
    public int FilterLimit
    {
        get
        {
            if (mFilterLimit == null)
            {
                mFilterLimit = ValidationHelper.GetInteger(SettingsHelper.AppSettings["CMSDefaultListingFilterLimit"], 25);
            }
            return (int)mFilterLimit;
        }
        set
        {
            mFilterLimit = value;
        }
    }


    /// <summary>
    /// Tree provider.
    /// </summary>
    public TreeProvider Tree
    {
        get
        {
            return mTree ?? (mTree = new TreeProvider(CMSContext.CurrentUser));
        }
        set
        {
            mTree = value;
        }
    }


    /// <summary>
    /// Access control list provider.
    /// </summary>
    public AclProvider AclProvider
    {
        get
        {
            return mAclProvider ?? (mAclProvider = new AclProvider(Tree));
        }
        set
        {
            mAclProvider = value;
        }
    }


    /// <summary>
    /// Document name.
    /// </summary>
    protected string DocumentName
    {
        get
        {
            if (Node != null)
            {
                if (string.IsNullOrEmpty(Node.DocumentName))
                {
                    return "/";
                }
                else
                {
                    return Node.DocumentName;
                }
            }
            return string.Empty;
        }
    }


    /// <summary>
    /// Event log provider.
    /// </summary>
    public EventLogProvider EventLog
    {
        get
        {
            return mEventLog ?? (mEventLog = new EventLogProvider());
        }
    }


    /// <summary>
    /// Indicates whether to display security message.
    /// </summary>
    public bool DisplaySecurityMessage
    {
        get
        {
            return mDisplaySecurityMessage;
        }
        set
        {
            mDisplaySecurityMessage = value;
        }
    }


    /// <summary>
    /// Gets or sets group identifier.
    /// </summary>
    public int GroupID
    {
        get
        {
            return ValidationHelper.GetInteger(ViewState["GroupID"], 0);

        }
        set
        {
            ViewState["GroupID"] = value;
        }
    }

    #endregion


    #region "Page events"

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        if (StopProcessing)
        {
            ProcessStopProcessing();
        }
    }


    protected void Page_Load(Object sender, EventArgs e)
    {
        if (StopProcessing)
        {
            ProcessStopProcessing();
        }
        else
        {
            ReloadData(false);
        }
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        if (!RequestHelper.IsPostBack() || isForceReload)
        {
            // Hide filter
            if ((FilterLimit > 0) && (lstOperators.Items.Count <= FilterLimit))
            {
                pnlFilter.Visible = false;
            }
            else
            {
                pnlFilter.Visible = true;
            }
        }
        // Set visibility of labels
        lblError.Visible = !String.IsNullOrEmpty(lblError.Text);
        lblInfo.Visible = !String.IsNullOrEmpty(lblInfo.Text);
        infoseparator.Visible = (!String.IsNullOrEmpty(lblError.Text) && !String.IsNullOrEmpty(lblInfo.Text));

        // Set buttons visibility
        SetButtonsVisibility();

        // Render the styles link in live site mode
        if (Visible && (CMSContext.ViewMode == ViewModeEnum.LiveSite))
        {
            CSSHelper.RegisterDesignMode(Page);
        }
    }

    #endregion


    #region "Methods"

    public void ReloadData(bool forceReload)
    {
        ipAddress = HTTPHelper.UserHostAddress;
        eventUrl = HTTPHelper.GetAbsoluteUri();
        isForceReload = forceReload;

        // Set up roles control
        addRoles.Node = Node;
        addRoles.Changed += Selection_Changed;
        addRoles.IsLiveSite = IsLiveSite;
        addRoles.CurrentSelector.IsLiveSite = IsLiveSite;
        addRoles.ShowSiteFilter = false;
        addRoles.Enabled = AllowEditRoles;
        addRoles.GroupID = GroupID;

        // Set up roles control
        addUsers.Node = Node;
        addUsers.Changed += Selection_Changed;
        addUsers.IsLiveSite = IsLiveSite;
        addUsers.CurrentSelector.IsLiveSite = IsLiveSite;
        addUsers.ShowSiteFilter = false;
        addUsers.Enabled = AllowEditUsers;
        addUsers.GroupID = GroupID;

        if (!RequestHelper.IsCallback())
        {
            // Gets the node
            if (Node != null)
            {
                UIContext.PropertyTab = PropertyTabEnum.Security;

                // Check modify and read permissions
                CheckPermissions(false, true);

                // Initialize controls
                SetupControls();

                // Register scripts
                LoadJavascript();
                ScriptHelper.RegisterDialogScript(Page);

                btnFilter.Click += btnFilter_Click;

                LoadOperators(!RequestHelper.IsPostBack() || forceReload);

                // If there is no item in listbox, disables checkboxes
                if (lstOperators.Items.Count == 0)
                {
                    ResetPermissions();
                    DisableAllCheckBoxes();
                }
            }
        }

        addRoles.CurrentSelector.DisabledItems = DisabledRoles;
        addUsers.CurrentSelector.DisabledItems = DisabledUsers;
    }


    public void Save()
    {
        CheckPermissions(true);

        int allowed = 0;
        int denied = 0;
        string operatorID = null;

        if (lstOperators.SelectedItem == null)
        {
            return;
        }
        else
        {
            operatorID = lstOperators.SelectedValue;
            allowed += GetCheckBoxValue(chkReadAllow, NodePermissionsEnum.Read);
            allowed += GetCheckBoxValue(chkModifyAllow, NodePermissionsEnum.Modify);
            allowed += GetCheckBoxValue(chkCreateAllow, NodePermissionsEnum.Create);
            allowed += GetCheckBoxValue(chkDeleteAllow, NodePermissionsEnum.Delete);
            allowed += GetCheckBoxValue(chkDestroyAllow, NodePermissionsEnum.Destroy);
            allowed += GetCheckBoxValue(chkExploreTreeAllow, NodePermissionsEnum.ExploreTree);
            allowed += GetCheckBoxValue(chkManagePermissionsAllow, NodePermissionsEnum.ModifyPermissions);
            denied += GetCheckBoxValue(chkReadDeny, NodePermissionsEnum.Read);
            denied += GetCheckBoxValue(chkModifyDeny, NodePermissionsEnum.Modify);
            denied += GetCheckBoxValue(chkCreateDeny, NodePermissionsEnum.Create);
            denied += GetCheckBoxValue(chkDeleteDeny, NodePermissionsEnum.Delete);
            denied += GetCheckBoxValue(chkDestroyDeny, NodePermissionsEnum.Destroy);
            denied += GetCheckBoxValue(chkExploreTreeDeny, NodePermissionsEnum.ExploreTree);
            denied += GetCheckBoxValue(chkManagePermissionsDeny, NodePermissionsEnum.ModifyPermissions);

            string message = null;
            string operatorName = lstOperators.SelectedItem.Text;
            if (operatorID.StartsWith("U"))
            {
                int userId = int.Parse(operatorID.Substring(1));
                UserInfo ui = UserInfoProvider.GetUserInfo(userId);
                AclProvider.SetUserPermissions(Node, allowed, denied, ui);
                message = "security.documentuserpermissionschange";
            }
            else
            {
                AclProvider.SetRolePermissions(Node, allowed, denied, int.Parse(operatorID.Substring(1)));
                message = "security.documentrolepermissionschange";
            }
            lblInfo.Text = GetString("general.changessaved");
            // Log synchronization task and flush cache
            DocumentSynchronizationHelper.LogDocumentChange(Node, TaskTypeEnum.UpdateDocument, Node.TreeProvider);
            CacheHelper.TouchKeys(TreeProvider.GetDependencyCacheKeys(Node, Node.NodeSiteName));

            // Insert information about this event to eventlog.
            if (Tree.LogEvents)
            {
                EventLog.LogEvent(EventLogProvider.EVENT_TYPE_INFORMATION, DateTime.Now, "Content", "DOCPERMISSIONSMODIFIED", Tree.UserInfo.UserID, Tree.UserInfo.UserName, Node.NodeID, DocumentName, ipAddress, string.Format(ResHelper.GetAPIString(message, "Permissions of the operator '{0}' have been modified for the document."), operatorName), Node.NodeSiteID, eventUrl);
            }
        }

        if (Node != null)
        {
            // Invalidate permission data in current request
            TreeSecurityProvider.InvalidateTreeNodeAuthorizationResults(CMSContext.CurrentUser, NodeID, Node.DocumentCulture);
            AclProvider.InvalidateACLItems(CMSContext.CurrentUser.UserID, Node);
        }
        CheckPermissions(false, true);
        pnlUpdate.Update();
    }


    private void ProcessStopProcessing()
    {
        addUsers.StopProcessing = true;
        addRoles.StopProcessing = true;
    }


    /// <summary>
    /// Checks if current use can modify  the perrmission.
    /// </summary>
    /// <param name="redirect">If true and can't modify the user is redirected to denied page</param>
    private void CheckPermissions(bool redirect)
    {
        CheckPermissions(redirect, false);
    }


    /// <summary>
    /// Checks if current use can modify  the perrmission.
    /// </summary>
    /// <param name="redirect">If true and can't modify the user is redirected to denied page</param>
    /// <param name="checkRead">Indicates whether to check also read permission</param>    
    private void CheckPermissions(bool redirect, bool checkRead)
    {
        if (Node != null)
        {
            bool isGroupAdmin = false;

            // Allow group administrator edit group document permissions
            if (GroupID > 0)
            {
                isGroupAdmin = CMSContext.CurrentUser.IsGroupAdministrator(GroupID);
            }

            if (checkRead)
            {
                // Check permissions, for group document library also group administrator can read the permissions
                bool hasReadPermission = (CMSContext.CurrentUser.IsAuthorizedPerDocument(Node, NodePermissionsEnum.Read) == AuthorizationResultEnum.Allowed) || isGroupAdmin;

                // If hasn't permission and resirect enabled
                if (!hasReadPermission)
                {
                    if (AllowRedirection)
                    {
                        RedirectToAccessDenied(String.Format(GetString("cmsdesk.notauthorizedtoreaddocument"), Node.NodeAliasPath));
                    }
                    else
                    {
                        DisableForm();
                    }
                }
            }
            hasModifyPermission = (CMSContext.CurrentUser.IsAuthorizedPerDocument(Node, NodePermissionsEnum.ModifyPermissions) == AuthorizationResultEnum.Allowed) || isGroupAdmin;

            // If hasn't permission and resirect enabled
            if (!hasModifyPermission)
            {
                if (redirect && AllowRedirection)
                {
                    RedirectToAccessDenied(String.Format(GetString("cmsdesk.notauthorizedtoeditdocument"), Node.NodeAliasPath));
                }
                else
                {
                    DisableForm();
                }
            }
        }
    }


    private void DisableForm()
    {
        addUsers.Enabled = false;
        addRoles.Enabled = false;
        pnlAccessRights.Enabled = false;
        btnRemoveOperator.Enabled = false;
        btnOk.Enabled = false;
        if (DisplaySecurityMessage)
        {
            // Display security message
            plcSecurityMessage.Visible = true;
            lblPermission.Text = String.Format(GetString("cmsdesk.notauthorizedtoeditdocument"), Node.NodeAliasPath);
        }
    }


    /// <summary>
    /// Initializes components.
    /// </summary>
    private void SetupControls()
    {
        // Adds additional events to controls        
        chkFullControlAllow.Attributes.Add("onclick", "CheckAllAllowCheckBoxes();");
        chkFullControlDeny.Attributes.Add("onclick", "CheckAllDenyCheckBoxes();");
        chkCreateAllow.Attributes.Add("onclick", "CheckFullCheck('allow');");
        chkCreateDeny.Attributes.Add("onclick", "CheckFullCheck('deny');");
        chkDeleteAllow.Attributes.Add("onclick", "CheckFullCheck('allow');");
        chkDeleteDeny.Attributes.Add("onclick", "CheckFullCheck('deny');");
        chkDestroyAllow.Attributes.Add("onclick", "CheckFullCheck('allow');");
        chkDestroyDeny.Attributes.Add("onclick", "CheckFullCheck('deny');");
        chkExploreTreeAllow.Attributes.Add("onclick", "CheckFullCheck('allow');");
        chkExploreTreeDeny.Attributes.Add("onclick", "CheckFullCheck('deny');");
        chkManagePermissionsAllow.Attributes.Add("onclick", "CheckFullCheck('allow');");
        chkManagePermissionsDeny.Attributes.Add("onclick", "CheckFullCheck('deny');");
        chkModifyAllow.Attributes.Add("onclick", "CheckFullCheck('allow');");
        chkModifyDeny.Attributes.Add("onclick", "CheckFullCheck('deny');");
        chkReadAllow.Attributes.Add("onclick", "CheckFullCheck('allow');");
        chkReadDeny.Attributes.Add("onclick", "CheckFullCheck('deny');");
    }


    /// <summary>
    /// Loads users and roles listed in the ACL of the selected document and displays them in the listbox.
    /// </summary>
    /// <param name="reload">Forces reload of listbox</param>
    public void LoadOperators(bool reload)
    {
        if (!StopProcessing)
        {
            string lastOperator = "";
            StringBuilder roles = new StringBuilder();
            StringBuilder users = new StringBuilder();

            StringBuilder disabledRoles = new StringBuilder();
            StringBuilder disabledUsers = new StringBuilder();

            if (reload)
            {
                lstOperators.Items.Clear();
            }

            LoadACLItems();
            if ((dsAclItems != null) && (dsAclItems.Tables.Count > 0))
            {
                foreach (DataRow drAclItem in dsAclItems.Tables[0].Rows)
                {
                    int nodeID = ValidationHelper.GetInteger(drAclItem["ACLOwnerNodeID"], 0);                               
                    string op = ValidationHelper.GetString(drAclItem["Operator"], "");
                    if (op != lastOperator)
                    {
                        lastOperator = op;
                        string operName = ValidationHelper.GetString(drAclItem["OperatorName"], String.Empty);
                        operName = CMSContext.ResolveMacros(operName);

                        if (!String.IsNullOrEmpty(op))
                        {
                            switch (op[0])
                            {
                                // Operator starts with 'R' - indicates role
                                case 'R':
                                    string role = op.Substring(1) + ";";
                                    roles.Append(role);

                                    // Test whether ACL owner node id is current node id, if not this ACL is inherited => disable in selector
                                    if (nodeID != NodeID)
                                    {
                                        disabledRoles.Append(role);                                        
                                    }

                                    if (ValidationHelper.GetInteger(drAclItem["RoleGroupID"], 0) > 0)
                                    {
                                        operName += " " + GetString("security.grouprole");
                                    }

                                    // Add global postfix
                                    if (ValidationHelper.GetInteger(drAclItem["SiteID"], 0) == 0)
                                    {
                                        operName += " " + GetString("general.global");
                                    }

                                    break;

                                // Operator starts with 'U' - indicates user
                                case 'U':
                                    string user = op.Substring(1) + ";";
                                    users.Append(user);

                                    // Test whether ACL owner node id is current node id, if not this ACL is inherited => disable in selector
                                    if (nodeID != NodeID)
                                    {
                                        disabledUsers.Append(user);
                                    }

                                    string fullName = ValidationHelper.GetString(drAclItem["OperatorFullName"], String.Empty);
                                    operName = Functions.GetFormattedUserName(operName, fullName);
                                    break;
                            }
                        }

                        if (reload)
                        {
                            lstOperators.Items.Add(new ListItem(operName, op));
                        }
                    }
                    
                }
            }

            if (reload)
            {
                if (lstOperators.Items.Count > 0)
                {
                    lstOperators.SelectedIndex = 0;
                    DisplayOperatorPermissions(lstOperators.SelectedValue);
                }
                else
                {
                    ResetPermissions();
                    DisableAllCheckBoxes();
                }

                // Update selector values on full reload
                addRoles.CurrentSelector.Value = roles.ToString();
                addUsers.CurrentSelector.Value = users.ToString();
            }

            // Set values to selectors (to be able to distinguish new and old items for add/remove action)
            addRoles.CurrentValues = roles.ToString();
            addUsers.CurrentValues = users.ToString();

            DisabledRoles = disabledRoles.ToString();
            DisabledUsers = disabledUsers.ToString();
        }
    }


    /// <summary>
    /// Load ACLItems for the selected document.
    /// </summary>
    private void LoadACLItems()
    {
        if (dsAclItems == null)
        {
            string where = GetWhereCondition();
            dsAclItems = AclProvider.GetACLItems(Node.NodeID, where, "OperatorName, Operator", 0, "Operator,ACLOwnerNodeID,OperatorName,OperatorFullName,Allowed,Denied,RoleGroupID,RoleID,SiteID");
        }
    }


    /// <summary>
    /// Invalidates dataset with acl items.
    /// </summary>
    public void InvalidateAcls()
    {
        dsAclItems = null;
    }


    /// <summary>
    /// Gets where condition for filter.
    /// </summary>
    /// <returns>Where condition for filter</returns>
    private string GetWhereCondition()
    {
        string where = null;
        if (!string.IsNullOrEmpty(txtFilter.Text))
        {
            where = "OperatorName LIKE '%" + SqlHelperClass.GetSafeQueryString(txtFilter.Text, false) + "%'";
        }
        return where;
    }


    /// <summary>
    /// Displays permissions of the selected operator (user or role).
    /// </summary>
    /// <param name="operatorID">OperatorID in format U123 or R123 where U/R indicates user/role and 123 is UserID/RoleID value</param>
    private void DisplayOperatorPermissions(string operatorID)
    {
        if (!StopProcessing)
        {
            ResetPermissions();

            LoadACLItems();
            DataRow[] rows = null;
            bool hasNativePermissions = false;
            bool hasInheritedPermissions = false;

            if ((dsAclItems != null) && (dsAclItems.Tables.Count > 0))
            {
                int i = 0;
                int allowed = 0;
                int denied = 0;
                // Process inherited permissions
                rows = dsAclItems.Tables[0].Select(" Operator = '" + operatorID + "' AND ACLOwnerNodeID <> '" + Node.NodeID + "' ");

                for (i = 0; i <= rows.GetUpperBound(0); i++)
                {
                    hasInheritedPermissions = true;
                    allowed = Convert.ToInt32(rows[i]["Allowed"]);
                    denied = Convert.ToInt32(rows[i]["Denied"]);

                    // Set "allow" check boxes for inherited permissions
                    chkFullControlAllow.Checked = chkFullControlAllow.Checked | (allowed == 127);
                    chkFullControlAllow.Enabled = chkFullControlAllow.Enabled & (allowed != 127);
                    chkReadAllow.Checked = chkReadAllow.Checked | IsPermissionTrue(allowed, NodePermissionsEnum.Read);
                    chkReadAllow.Enabled = chkReadAllow.Enabled & !(IsPermissionTrue(allowed, NodePermissionsEnum.Read));
                    chkModifyAllow.Checked = chkModifyAllow.Checked | IsPermissionTrue(allowed, NodePermissionsEnum.Modify);
                    chkModifyAllow.Enabled = chkModifyAllow.Enabled & !(IsPermissionTrue(allowed, NodePermissionsEnum.Modify));
                    chkCreateAllow.Checked = chkCreateAllow.Checked | IsPermissionTrue(allowed, NodePermissionsEnum.Create);
                    chkCreateAllow.Enabled = chkCreateAllow.Enabled & !(IsPermissionTrue(allowed, NodePermissionsEnum.Create));
                    chkDeleteAllow.Checked = chkDeleteAllow.Checked | IsPermissionTrue(allowed, NodePermissionsEnum.Delete);
                    chkDeleteAllow.Enabled = chkDeleteAllow.Enabled & !(IsPermissionTrue(allowed, NodePermissionsEnum.Delete));
                    chkDestroyAllow.Checked = chkDestroyAllow.Checked | IsPermissionTrue(allowed, NodePermissionsEnum.Destroy);
                    chkDestroyAllow.Enabled = chkDestroyAllow.Enabled & !(IsPermissionTrue(allowed, NodePermissionsEnum.Destroy));
                    chkExploreTreeAllow.Checked = chkExploreTreeAllow.Checked | IsPermissionTrue(allowed, NodePermissionsEnum.ExploreTree);
                    chkExploreTreeAllow.Enabled = chkExploreTreeAllow.Enabled & !(IsPermissionTrue(allowed, NodePermissionsEnum.ExploreTree));
                    chkManagePermissionsAllow.Checked = chkManagePermissionsAllow.Checked | IsPermissionTrue(allowed, NodePermissionsEnum.ModifyPermissions);
                    chkManagePermissionsAllow.Enabled = chkManagePermissionsAllow.Enabled & !(IsPermissionTrue(allowed, NodePermissionsEnum.ModifyPermissions));

                    // Set "deny" checkboxes for inherited permissions
                    chkFullControlDeny.Checked = chkFullControlDeny.Checked | (denied == 127);
                    chkFullControlDeny.Enabled = chkFullControlDeny.Enabled & (denied != 127);
                    chkReadDeny.Checked = chkReadDeny.Checked | IsPermissionTrue(denied, NodePermissionsEnum.Read);
                    chkReadDeny.Enabled = chkReadDeny.Enabled & !(IsPermissionTrue(denied, NodePermissionsEnum.Read));
                    chkModifyDeny.Checked = chkModifyDeny.Checked | IsPermissionTrue(denied, NodePermissionsEnum.Modify);
                    chkModifyDeny.Enabled = chkModifyDeny.Enabled & !(IsPermissionTrue(denied, NodePermissionsEnum.Modify));
                    chkCreateDeny.Checked = chkCreateDeny.Checked | IsPermissionTrue(denied, NodePermissionsEnum.Create);
                    chkCreateDeny.Enabled = chkCreateDeny.Enabled & !(IsPermissionTrue(denied, NodePermissionsEnum.Create));
                    chkDeleteDeny.Checked = chkDeleteDeny.Checked | IsPermissionTrue(denied, NodePermissionsEnum.Delete);
                    chkDeleteDeny.Enabled = chkDeleteDeny.Enabled & !(IsPermissionTrue(denied, NodePermissionsEnum.Delete));
                    chkDestroyDeny.Checked = chkDestroyDeny.Checked | IsPermissionTrue(denied, NodePermissionsEnum.Destroy);
                    chkDestroyDeny.Enabled = chkDestroyDeny.Enabled & !(IsPermissionTrue(denied, NodePermissionsEnum.Destroy));
                    chkExploreTreeDeny.Checked = chkExploreTreeDeny.Checked | IsPermissionTrue(denied, NodePermissionsEnum.ExploreTree);
                    chkExploreTreeDeny.Enabled = chkExploreTreeDeny.Enabled & !(IsPermissionTrue(denied, NodePermissionsEnum.ExploreTree));
                    chkManagePermissionsDeny.Checked = chkManagePermissionsDeny.Checked | IsPermissionTrue(denied, NodePermissionsEnum.ModifyPermissions);
                    chkManagePermissionsDeny.Enabled = chkManagePermissionsDeny.Enabled & !(IsPermissionTrue(denied, NodePermissionsEnum.ModifyPermissions));

                    // Disable fullcontrol checkboxes if needed
                    if (chkFullControlAllow.Enabled)
                    {
                        chkFullControlAllow.Enabled = !(chkReadAllow.Checked && chkModifyAllow.Checked && chkCreateAllow.Checked && chkDeleteAllow.Checked && chkDestroyAllow.Checked && chkExploreTreeAllow.Checked && chkManagePermissionsAllow.Checked);
                    }

                    if (chkFullControlDeny.Enabled)
                    {
                        chkFullControlDeny.Enabled = !(chkReadDeny.Checked && chkModifyDeny.Checked && chkCreateDeny.Checked && chkDeleteDeny.Checked && chkDestroyDeny.Checked && chkExploreTreeDeny.Checked && chkManagePermissionsDeny.Checked);
                    }
                }

                // Process native permissions
                rows = dsAclItems.Tables[0].Select(" Operator = '" + operatorID + "' AND ACLOwnerNodeID = '" + Node.NodeID + "' ");

                for (i = 0; i <= rows.GetUpperBound(0); i++)
                {
                    hasNativePermissions = true;
                    allowed = Convert.ToInt32(rows[i]["Allowed"]);
                    denied = Convert.ToInt32(rows[i]["Denied"]);

                    // Set "allow" check boxes for native permissions
                    SetCheckBoxWithNativePermission(chkReadAllow, IsPermissionTrue(allowed, NodePermissionsEnum.Read));
                    SetCheckBoxWithNativePermission(chkModifyAllow, IsPermissionTrue(allowed, NodePermissionsEnum.Modify));
                    SetCheckBoxWithNativePermission(chkCreateAllow, IsPermissionTrue(allowed, NodePermissionsEnum.Create));
                    SetCheckBoxWithNativePermission(chkDeleteAllow, IsPermissionTrue(allowed, NodePermissionsEnum.Delete));
                    SetCheckBoxWithNativePermission(chkDestroyAllow, IsPermissionTrue(allowed, NodePermissionsEnum.Destroy));
                    SetCheckBoxWithNativePermission(chkExploreTreeAllow, IsPermissionTrue(allowed, NodePermissionsEnum.ExploreTree));
                    SetCheckBoxWithNativePermission(chkManagePermissionsAllow, IsPermissionTrue(allowed, NodePermissionsEnum.ModifyPermissions));

                    // Set "deny" checkboxes for inherited permissions
                    SetCheckBoxWithNativePermission(chkReadDeny, IsPermissionTrue(denied, NodePermissionsEnum.Read));
                    SetCheckBoxWithNativePermission(chkModifyDeny, IsPermissionTrue(denied, NodePermissionsEnum.Modify));
                    SetCheckBoxWithNativePermission(chkCreateDeny, IsPermissionTrue(denied, NodePermissionsEnum.Create));
                    SetCheckBoxWithNativePermission(chkDeleteDeny, IsPermissionTrue(denied, NodePermissionsEnum.Delete));
                    SetCheckBoxWithNativePermission(chkDestroyDeny, IsPermissionTrue(denied, NodePermissionsEnum.Destroy));
                    SetCheckBoxWithNativePermission(chkExploreTreeDeny, IsPermissionTrue(denied, NodePermissionsEnum.ExploreTree));
                    SetCheckBoxWithNativePermission(chkManagePermissionsDeny, IsPermissionTrue(denied, NodePermissionsEnum.ModifyPermissions));
                }
            }

            // Determine whether user can edit current operator
            bool operatorIsEditable = ((operatorID.StartsWith("U") && AllowEditUsers) || !operatorID.StartsWith("U")) && ((operatorID.StartsWith("R") && AllowEditRoles) || !operatorID.StartsWith("R"));

            // Check if disable ok button
            btnOk.Enabled = (chkFullControlAllow.Enabled || chkFullControlDeny.Enabled) && operatorIsEditable && hasModifyPermission;
            pnlAccessRights.Enabled = pnlAccessRights.Enabled && operatorIsEditable;
            btnRemoveOperator.Enabled = (hasModifyPermission && !hasInheritedPermissions && hasNativePermissions) && operatorIsEditable;
            btnRemoveOperator.OnClientClick = "return confirm('" + GetString("security.confirmremove") + "');";

            // Setup 'Full control' checkboxes
            chkFullControlAllow.Checked = chkReadAllow.Checked && chkModifyAllow.Checked && chkCreateAllow.Checked && chkDeleteAllow.Checked && chkDestroyAllow.Checked && chkExploreTreeAllow.Checked && chkManagePermissionsAllow.Checked;
            chkFullControlDeny.Checked = chkReadDeny.Checked && chkModifyDeny.Checked && chkCreateDeny.Checked && chkDeleteDeny.Checked && chkDestroyDeny.Checked && chkExploreTreeDeny.Checked && chkManagePermissionsDeny.Checked;
        }
    }


    /// <summary>
    /// Parses permission value and return true if appropriate bit is 1.
    /// </summary>
    protected bool IsPermissionTrue(int permissionValue, NodePermissionsEnum permission)
    {
        return ((permissionValue >> Convert.ToInt32(permission)) % 2) == 1;
    }


    /// <summary>
    /// Checks checkbox if provided value is true.
    /// </summary>
    protected void SetCheckBoxWithNativePermission(CheckBox chkBox, bool value)
    {
        if (value)
        {
            chkBox.Checked = true;
        }
    }


    /// <summary>
    /// Returns integer value representing permission value of the given checkbox and permission name.
    /// </summary>
    protected int GetCheckBoxValue(CheckBox chkBox, NodePermissionsEnum permission)
    {
        return (chkBox.Enabled && chkBox.Checked) ? Convert.ToInt32(Math.Pow(2, Convert.ToInt32(permission))) : 0;
    }


    /// <summary>
    /// Resets all checkboxes representing permissions.
    /// </summary>
    protected void ResetPermissions()
    {
        ResetCheckBox(chkFullControlAllow);
        ResetCheckBox(chkFullControlDeny);
        ResetCheckBox(chkReadAllow);
        ResetCheckBox(chkReadDeny);
        ResetCheckBox(chkCreateAllow);
        ResetCheckBox(chkCreateDeny);
        ResetCheckBox(chkModifyAllow);
        ResetCheckBox(chkModifyDeny);
        ResetCheckBox(chkDeleteAllow);
        ResetCheckBox(chkDeleteDeny);
        ResetCheckBox(chkDestroyAllow);
        ResetCheckBox(chkDestroyDeny);
        ResetCheckBox(chkExploreTreeAllow);
        ResetCheckBox(chkExploreTreeDeny);
        ResetCheckBox(chkManagePermissionsAllow);
        ResetCheckBox(chkManagePermissionsDeny);
    }


    /// <summary>
    /// Resets given checkbox. Enables set to true, checked to false.
    /// </summary>
    protected void ResetCheckBox(CheckBox chkBox)
    {
        chkBox.Enabled = true;
        chkBox.Checked = false;
    }


    /// <summary>
    /// Sets value to enabled checkbox.
    /// </summary>
    /// <param name="chkBox">Checkbox to use</param>
    /// <param name="checkedIdent">Checked value</param>
    private static void CheckEnabledCheckbox(CheckBox chkBox, bool checkedIdent)
    {
        if (chkBox.Enabled)
        {
            chkBox.Checked = checkedIdent;
        }
    }


    /// <summary>
    /// Disables all checkboxes.
    /// </summary>
    protected void DisableAllCheckBoxes()
    {
        chkCreateAllow.Enabled = false;
        chkCreateDeny.Enabled = false;
        chkDeleteAllow.Enabled = false;
        chkDeleteDeny.Enabled = false;
        chkDestroyAllow.Enabled = false;
        chkDestroyDeny.Enabled = false;
        chkExploreTreeAllow.Enabled = false;
        chkExploreTreeDeny.Enabled = false;
        chkFullControlAllow.Enabled = false;
        chkFullControlDeny.Enabled = false;
        chkManagePermissionsAllow.Enabled = false;
        chkManagePermissionsDeny.Enabled = false;
        chkModifyAllow.Enabled = false;
        chkModifyDeny.Enabled = false;
        chkReadAllow.Enabled = false;
        chkReadDeny.Enabled = false;
    }


    /// <summary>
    /// Loads javacript.
    /// </summary>
    private void LoadJavascript()
    {
        string javascript = @"   

        function CheckAllDenyCheckBoxes()
        {
            if(document.getElementById('" + chkFullControlDeny.ClientID + @"').checked == true)
            {
                if(document.getElementById('" + chkReadDeny.ClientID + @"').disabled == false)
                    document.getElementById('" + chkReadDeny.ClientID + @"').checked = true;
                if(document.getElementById('" + chkCreateDeny.ClientID + @"').disabled == false)
                    document.getElementById('" + chkCreateDeny.ClientID + @"').checked = true;
                if(document.getElementById('" + chkModifyDeny.ClientID + @"').disabled == false)
                    document.getElementById('" + chkModifyDeny.ClientID + @"').checked = true;
                if(document.getElementById('" + chkDeleteDeny.ClientID + @"').disabled == false)
                    document.getElementById('" + chkDeleteDeny.ClientID + @"').checked = true;
                if(document.getElementById('" + chkDestroyDeny.ClientID + @"').disabled == false)
                    document.getElementById('" + chkDestroyDeny.ClientID + @"').checked = true;
                if(document.getElementById('" + chkExploreTreeDeny.ClientID + @"').disabled == false)
                    document.getElementById('" + chkExploreTreeDeny.ClientID + @"').checked = true;
                if(document.getElementById('" + chkManagePermissionsDeny.ClientID + @"').disabled == false)
                    document.getElementById('" + chkManagePermissionsDeny.ClientID + @"').checked = true;
           }
           else
           {
                if(document.getElementById('" + chkReadDeny.ClientID + @"').disabled == false)
                    document.getElementById('" + chkReadDeny.ClientID + @"').checked = false;
                if(document.getElementById('" + chkCreateDeny.ClientID + @"').disabled == false)
                    document.getElementById('" + chkCreateDeny.ClientID + @"').checked = false;
                if(document.getElementById('" + chkModifyDeny.ClientID + @"').disabled == false)
                    document.getElementById('" + chkModifyDeny.ClientID + @"').checked = false;
                if(document.getElementById('" + chkDeleteDeny.ClientID + @"').disabled == false)
                    document.getElementById('" + chkDeleteDeny.ClientID + @"').checked = false;
                if(document.getElementById('" + chkDestroyDeny.ClientID + @"').disabled == false)
                    document.getElementById('" + chkDestroyDeny.ClientID + @"').checked = false;
                if(document.getElementById('" + chkExploreTreeDeny.ClientID + @"').disabled == false)
                    document.getElementById('" + chkExploreTreeDeny.ClientID + @"').checked = false;
                if(document.getElementById('" + chkManagePermissionsDeny.ClientID + @"').disabled == false)
                    document.getElementById('" + chkManagePermissionsDeny.ClientID + @"').checked = false;
           }
        }

        function CheckAllAllowCheckBoxes()
        {
            if(document.getElementById('" + chkFullControlAllow.ClientID + @"').checked == true)
            {
                if(document.getElementById('" + chkReadAllow.ClientID + @"').disabled == false)
                    document.getElementById('" + chkReadAllow.ClientID + @"').checked = true;
                if(document.getElementById('" + chkCreateAllow.ClientID + @"').disabled == false)
                    document.getElementById('" + chkCreateAllow.ClientID + @"').checked = true;
                if(document.getElementById('" + chkModifyAllow.ClientID + @"').disabled == false)
                    document.getElementById('" + chkModifyAllow.ClientID + @"').checked = true;
                if(document.getElementById('" + chkDeleteAllow.ClientID + @"').disabled == false)
                    document.getElementById('" + chkDeleteAllow.ClientID + @"').checked = true;
                if(document.getElementById('" + chkDestroyAllow.ClientID + @"').disabled == false)
                    document.getElementById('" + chkDestroyAllow.ClientID + @"').checked = true;
                if(document.getElementById('" + chkExploreTreeAllow.ClientID + @"').disabled == false)
                    document.getElementById('" + chkExploreTreeAllow.ClientID + @"').checked = true;
                if(document.getElementById('" + chkManagePermissionsAllow.ClientID + @"').disabled == false)
                    document.getElementById('" + chkManagePermissionsAllow.ClientID + @"').checked = true;
            }
            else
            {
                if(document.getElementById('" + chkReadAllow.ClientID + @"').disabled == false)
                    document.getElementById('" + chkReadAllow.ClientID + @"').checked = false;
                if(document.getElementById('" + chkCreateAllow.ClientID + @"').disabled == false)
                    document.getElementById('" + chkCreateAllow.ClientID + @"').checked = false;
                if(document.getElementById('" + chkModifyAllow.ClientID + @"').disabled == false)
                    document.getElementById('" + chkModifyAllow.ClientID + @"').checked = false;
                if(document.getElementById('" + chkDeleteAllow.ClientID + @"').disabled == false)
                    document.getElementById('" + chkDeleteAllow.ClientID + @"').checked = false;
                if(document.getElementById('" + chkDestroyAllow.ClientID + @"').disabled == false)
                    document.getElementById('" + chkDestroyAllow.ClientID + @"').checked = false;
                if(document.getElementById('" + chkExploreTreeAllow.ClientID + @"').disabled == false)
                    document.getElementById('" + chkExploreTreeAllow.ClientID + @"').checked = false;
                if(document.getElementById('" + chkManagePermissionsAllow.ClientID + @"').disabled == false)
                    document.getElementById('" + chkManagePermissionsAllow.ClientID + @"').checked = false;
            }
        }
       
        function CheckFullCheck(which)
        {
            if(which=='allow')
            {
                if((document.getElementById('" + chkReadAllow.ClientID + @"').checked == true) &&
                   (document.getElementById('" + chkCreateAllow.ClientID + @"').checked == true) &&
                   (document.getElementById('" + chkModifyAllow.ClientID + @"').checked == true) &&
                   (document.getElementById('" + chkDeleteAllow.ClientID + @"').checked == true) &&
                   (document.getElementById('" + chkDestroyAllow.ClientID + @"').checked == true) &&
                   (document.getElementById('" + chkExploreTreeAllow.ClientID + @"').checked == true) &&
                   (document.getElementById('" + chkManagePermissionsAllow.ClientID + @"').checked == true))
                {
                        document.getElementById('" + chkFullControlAllow.ClientID + @"').checked = true;
                }
                else
                {
                        document.getElementById('" + chkFullControlAllow.ClientID + @"').checked = false;                            
                }
            }
            else
            {
                if((document.getElementById('" + chkReadDeny.ClientID + @"').checked == true) &&
                   (document.getElementById('" + chkCreateDeny.ClientID + @"').checked == true) &&
                   (document.getElementById('" + chkModifyDeny.ClientID + @"').checked == true) &&
                   (document.getElementById('" + chkDeleteDeny.ClientID + @"').checked == true) &&
                   (document.getElementById('" + chkDestroyDeny.ClientID + @"').checked == true) &&
                   (document.getElementById('" + chkExploreTreeDeny.ClientID + @"').checked == true) &&
                   (document.getElementById('" + chkManagePermissionsDeny.ClientID + @"').checked == true))
                {
                        document.getElementById('" + chkFullControlDeny.ClientID + @"').checked = true;
                }
                else
                {
                        document.getElementById('" + chkFullControlDeny.ClientID + @"').checked = false;                            
                }
            }
        }    
        ";

        javascript = ScriptHelper.GetScript(javascript);
        ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "securityElem_" + ClientID, javascript);
    }


    /// <summary>
    /// Sets visibility of buttons based on DisplayButtons property.
    /// </summary>
    private void SetButtonsVisibility()
    {
        btnOk.Style["visibility"] = DisplayButtons ? "visible" : "hidden";
    }

    #endregion


    #region "Events"

    /// <summary>
    /// On changed roles - update update panel.
    /// </summary>
    protected void Selection_Changed(object sender, EventArgs e)
    {
        if (!StopProcessing)
        {
            dsAclItems = null;
            LoadOperators(true);
            pnlUpdate.Update();
        }
    }


    protected void btnFilter_Click(object sender, EventArgs e)
    {
        if (!StopProcessing)
        {
            LoadOperators(true);
        }
    }


    protected void lstOperators_SelectedIndexChanged(Object sender, EventArgs e)
    {
        if (lstOperators.SelectedItem != null)
        {
            DisplayOperatorPermissions(lstOperators.SelectedValue);
        }
        else
        {
            DisableAllCheckBoxes();
        }
    }


    /// <summary>
    /// Saves data.
    /// </summary>
    protected void btnOK_Click(Object sender, EventArgs e)
    {
        // Check permission
        Save();
    }


    /// <summary>
    /// Removes selected operator from the ACL.
    /// </summary>
    protected void btnRemoveOperator_Click(Object sender, EventArgs e)
    {
        // Check permission
        CheckPermissions(true);

        if (lstOperators.SelectedItem == null)
        {
            return;
        }

        string operatorName = lstOperators.SelectedItem.Text;
        string message = null;
        string operatorID = lstOperators.SelectedValue;
        if (operatorID.StartsWith("U"))
        {
            int userId = int.Parse(operatorID.Substring(1));
            UserInfo ui = UserInfoProvider.GetUserInfo(userId);
            AclProvider.RemoveUser(Node.NodeID, ui);
            message = "security.documentuserpermissionremoved";
        }
        else
        {
            AclProvider.RemoveRole(Node.NodeID, int.Parse(operatorID.Substring(1)));
            message = "security.documentrolepermissionremoved";
        }

        // Log synchronization task and flush cache
        DocumentSynchronizationHelper.LogDocumentChange(TreeHelper.SelectSingleNode(Node.NodeID), TaskTypeEnum.UpdateDocument, Node.TreeProvider);
        CacheHelper.TouchKeys(TreeProvider.GetDependencyCacheKeys(Node, Node.NodeSiteName));

        // Insert information about this event to eventlog.
        if (Tree.LogEvents)
        {
            EventLog.LogEvent(EventLogProvider.EVENT_TYPE_INFORMATION, DateTime.Now, "Content", "DOCPERMISSIONSMODIFIED", Tree.UserInfo.UserID, Tree.UserInfo.UserName, Node.NodeID, DocumentName, ipAddress, string.Format(ResHelper.GetAPIString(message, "Operator '{0}' has been removed from the document permissions."), operatorName), Node.NodeSiteID, eventUrl);
        }

        dsAclItems = null;
        LoadOperators(true);
    }


    protected void chkFullControlAllow_CheckedChanged(Object sender, EventArgs e)
    {
        // Check permission
        CheckPermissions(true);

        // Check all enabled checkboxes
        CheckEnabledCheckbox(chkReadAllow, chkFullControlAllow.Checked);
        CheckEnabledCheckbox(chkCreateAllow, chkFullControlAllow.Checked);
        CheckEnabledCheckbox(chkModifyAllow, chkFullControlAllow.Checked);
        CheckEnabledCheckbox(chkDeleteAllow, chkFullControlAllow.Checked);
        CheckEnabledCheckbox(chkDestroyAllow, chkFullControlAllow.Checked);
        CheckEnabledCheckbox(chkExploreTreeAllow, chkFullControlAllow.Checked);
        CheckEnabledCheckbox(chkManagePermissionsAllow, chkFullControlAllow.Checked);
    }


    protected void chkFullControlDeny_CheckedChanged(Object sender, EventArgs e)
    {
        // Check permission
        CheckPermissions(true);

        // Check all deny checkboxes
        CheckEnabledCheckbox(chkReadDeny, chkFullControlDeny.Checked);
        CheckEnabledCheckbox(chkCreateDeny, chkFullControlDeny.Checked);
        CheckEnabledCheckbox(chkModifyDeny, chkFullControlDeny.Checked);
        CheckEnabledCheckbox(chkDeleteDeny, chkFullControlDeny.Checked);
        CheckEnabledCheckbox(chkDestroyDeny, chkFullControlDeny.Checked);
        CheckEnabledCheckbox(chkExploreTreeDeny, chkFullControlDeny.Checked);
        CheckEnabledCheckbox(chkManagePermissionsDeny, chkFullControlDeny.Checked);
    }

    #endregion
}