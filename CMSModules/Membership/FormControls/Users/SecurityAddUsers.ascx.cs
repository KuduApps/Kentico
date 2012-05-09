using System;

using CMS.FormControls;
using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.TreeEngine;
using CMS.SettingsProvider;
using CMS.UIControls;
using CMS.SiteProvider;
using CMS.EventLog;
using CMS.WorkflowEngine;

using TreeNode = CMS.TreeEngine.TreeNode;

public partial class CMSModules_Membership_FormControls_Users_SecurityAddUsers : FormEngineUserControl
{
    #region "Private variables"

    private int mNodeID = 0;
    private TreeNode mNode = null;
    private int mBoardID = 0;
    private int mForumID = 0;
    private int mGroupID = 0;
    private string mCurrentValues = String.Empty;
    private TreeProvider mTree = null;
    private EventLogProvider mEventLog = null;

    #endregion


    #region "Public properties"

    /// <summary>
    /// Gets or sets node id.
    /// </summary>
    public int NodeID
    {
        get
        {
            return mNodeID;
        }
        set
        {
            // Clear TreeNode on id change
            if (mNodeID != value)
            {
                mNode = null;
            }

            mNodeID = value;
        }
    }


    /// <summary>
    /// Gets or sets the TreeNode.
    /// </summary>
    public TreeNode Node
    {
        get
        {
            if ((mNode == null) && (NodeID > 0))
            {
                // Get node
                mNode = Tree.SelectSingleNode(NodeID);
            }
            return mNode;
        }
        set
        {
            mNode = value;
            // Update NodeID
            if (mNode != null)
            {
                mNodeID = mNode.NodeID;
            }
        }
    }


    /// <summary>
    /// Gets or sets board id.
    /// </summary>
    public int BoardID
    {
        get
        {
            return mBoardID;
        }
        set
        {
            mBoardID = value;
        }
    }


    /// <summary>
    /// Gets or sets forum id.
    /// </summary>
    public int ForumID
    {
        get
        {
            return mForumID;
        }
        set
        {
            mForumID = value;
        }
    }


    /// <summary>
    /// Gets or sets group id.
    /// </summary>
    public int GroupID
    {
        get
        {
            if (NodeID > 0)
            {
                return mGroupID;
            }
            return usUsers.GroupID;
        }
        set
        {
            if (NodeID > 0)
            {
                mGroupID = value;
            }
            else
            {
                usUsers.GroupID = value;
            }
        }
    }


    /// <summary>
    /// Returns current uniselector.
    /// </summary>
    public UniSelector CurrentSelector
    {
        get
        {
            return usUsers.UniSelector;
        }
    }

    /// <summary>
    /// Gets or sets subscriber.
    /// </summary>
    public string CurrentValues
    {
        get
        {
            return mCurrentValues;
        }
        set
        {
            mCurrentValues = value;
        }
    }


    /// <summary>
    /// Enables or disables the control.
    /// </summary>
    public override bool Enabled
    {
        get
        {
            return base.Enabled;
        }
        set
        {
            base.Enabled = value;
            usUsers.Enabled = value;
        }
    }


    /// <summary>
    /// Enables or disables site filter in uni selector.
    /// </summary>
    public bool ShowSiteFilter
    {
        get
        {
            return usUsers.ShowSiteFilter;
        }
        set
        {
            usUsers.ShowSiteFilter = value;
        }
    }


    /// <summary>
    /// Gets or sets ID of the site. Only users of this site are shown in selector.
    /// Note. SiteID is not used if site filter is enabled
    /// </summary>
    public int SiteID
    {
        get
        {
            return usUsers.SiteID;
        }
        set
        {
            usUsers.SiteID = value;
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
            usUsers.IsLiveSite = value;
        }
    }

    #endregion


    #region "Protected properties"

    /// <summary>
    /// Tree provider.
    /// </summary>
    protected TreeProvider Tree
    {
        get
        {
            return mTree ?? (mTree = new TreeProvider(CMSContext.CurrentUser));
        }
    }


    /// <summary>
    /// Event log provider.
    /// </summary>
    protected EventLogProvider EventLog
    {
        get
        {
            return mEventLog ?? (mEventLog = new EventLogProvider());
        }
    }

    #endregion


    #region "Events"

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        // Add sites filter        
        usUsers.UniSelector.SetValue("FilterMode", "user");
        usUsers.ResourcePrefix = "addusers";
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        // Add event handling
        usUsers.UniSelector.OnItemsSelected += usUsers_OnItemsSelected;
        usUsers.UniSelector.OnSelectionChanged += usUsers_OnItemsSelected;

        // Check node permissions
        if (Node != null)
        {
            // Set group filter
            if (GroupID > 0)
            {
                usUsers.UniSelector.FilterControl = "~/CMSFormControls/Filters/SiteGroupFilter.ascx";
                usUsers.UniSelector.SetValue("GroupID", GroupID.ToString());
            }

            // Allow group administrator edit group document permissions on live site
            if ((GroupID > 0) && IsLiveSite)
            {
                if (CMSContext.CurrentUser.IsGroupAdministrator(GroupID))
                {
                    usUsers.Enabled = true;
                    return;
                }
            }

            if (CMSContext.CurrentUser.IsAuthorizedPerDocument(Node, NodePermissionsEnum.ModifyPermissions) != AuthorizationResultEnum.Allowed)
            {
                usUsers.Enabled = false;
                return;
            }
        }

        // Check message board permission
        if (BoardID > 0)
        {
            GeneralizedInfo boardObj = ModuleCommands.MessageBoardGetMessageBoardInfo(BoardID);
            if (boardObj != null)
            {
                int boardGroupId = ValidationHelper.GetInteger(boardObj.GetValue("BoardGroupID"), 0);
                if (boardGroupId > 0)
                {
                    if (!CMSContext.CurrentUser.IsGroupAdministrator(boardGroupId))
                    {
                        usUsers.Enabled = false;
                        return;
                    }
                }
                else if (!CMSContext.CurrentUser.IsAuthorizedPerResource("CMS.MessageBoards", "Modify"))
                {
                    usUsers.Enabled = false;
                    return;
                }
            }
        }

        // Check forum permission
        if (ForumID > 0)
        {
            if (GroupID > 0)
            {
                if (!CMSContext.CurrentUser.IsGroupAdministrator(GroupID))
                {
                    usUsers.Enabled = false;
                    return;
                }
            }
            else if (!CMSContext.CurrentUser.IsAuthorizedPerResource("cms.forums", CMSAdminControl.PERMISSION_MODIFY))
            {
                usUsers.Enabled = false;
                return;
            }
        }
    }


    protected void usUsers_OnItemsSelected(object sender, EventArgs e)
    {
        AclProvider aclProv = null;

        // Create Acl provider to current treenode
        if (Node != null)
        {
            aclProv = new AclProvider(Tree);
        }

        // Remove old items
        string newValues = ValidationHelper.GetString(usUsers.Value, null);
        string items = DataHelper.GetNewItemsInList(newValues, CurrentValues);
        if (!String.IsNullOrEmpty(items))
        {
            string[] newItems = items.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            if (newItems != null)
            {
                // Add all new items to site
                foreach (string item in newItems)
                {
                    int userID = ValidationHelper.GetInteger(item, 0);

                    if (BoardID > 0)
                    {
                        // Remove message board from board
                        ModuleCommands.MessageBoardRemoveModeratorFromBoard(userID, BoardID);
                    }
                    else if (Node != null)
                    {
                        if (aclProv != null)
                        {
                            UserInfo ui = UserInfoProvider.GetUserInfo(userID);
                            if (ui != null)
                            {
                                // Remove user from treenode
                                aclProv.RemoveUser(NodeID, ui);
                            }
                        }
                    }
                    else if (ForumID > 0)
                    {
                        // Remove user from forum moderators
                        ModuleCommands.ForumsRemoveForumModerator(userID, ForumID);
                    }
                }
            }
        }

        // Add new items
        items = DataHelper.GetNewItemsInList(CurrentValues, newValues);
        if (!String.IsNullOrEmpty(items))
        {
            string[] newItems = items.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            if (newItems != null)
            {
                // Add all new items to site
                foreach (string item in newItems)
                {
                    int userID = ValidationHelper.GetInteger(item, 0);

                    if (BoardID > 0)
                    {
                        // Add user to the message board
                        ModuleCommands.MessageBoardAddModeratorToBoard(userID, BoardID);
                    }
                    else if (Node != null)
                    {
                        // Add user to treenode
                        if (aclProv != null)
                        {
                            UserInfo ui = UserInfoProvider.GetUserInfo(userID);
                            if (ui != null)
                            {
                                // Remove user from treenode
                                aclProv.SetUserPermissions(Node, 0, 0, ui);
                            }
                        }
                    }
                    else if (ForumID > 0)
                    {
                        // Add user to the forum moderators
                        ModuleCommands.ForumsAddForumModerator(userID, ForumID);
                    }
                }
            }
        }

        // Log synchronization task
        if (Node != null)
        {
            DocumentSynchronizationHelper.LogDocumentChange(Node, TaskTypeEnum.UpdateDocument, Node.TreeProvider);
        }

        RaiseOnChanged();
    }

    #endregion


    /// <summary>
    /// Reloads the data of the UniSelector.
    /// </summary>
    public void ReloadData()
    {
        usUsers.Value = CurrentValues;
        usUsers.ReloadData();
    }
}