using CMS.UIControls;
using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.TreeEngine;
using CMS.WorkflowEngine;
using CMS.SiteProvider;


/// <summary>
/// Base history version control.
/// </summary>
public abstract class VersionHistoryControl : CMSUserControl
{
    #region "Variables"

    protected TreeProvider mTreeProvider = null;
    protected WorkflowStepInfo mWorkflowStepInfo = null;
    protected WorkflowManager mWorkflowManager = null;
    protected VersionManager mVersionManager = null;
    protected TreeNode mNode = null;
    protected CurrentUserInfo mCurrentUser = null;
    protected SiteInfo mCurrentSiteInfo = null;

    #endregion


    #region "Properties"

    /// <summary>
    /// Identifier of edited node.
    /// </summary>
    public int NodeID
    {
        get
        {
            int mNodeID = ValidationHelper.GetInteger(ViewState["NodeID"], 0);
            if (mNodeID == 0)
            {
                mNodeID = QueryHelper.GetInteger("nodeid", 0);
            }
            return mNodeID;
        }
        set
        {
            ViewState["NodeID"] = value;
        }
    }


    /// <summary>
    /// Indicates if returned nodes should be combined with appropriate nodes of default culture in case they are not localized. It applies only if you're using multilingual support. The default value is false.
    /// </summary>
    public bool CombineWithDefaultCulture
    {
        get
        {
            return ValidationHelper.GetBoolean(ViewState["CombineWithDefaultCulture"], TreeProvider.CombineWithDefaultCulture);
        }
        set
        {
            ViewState["CombineWithDefaultCulture"] = value;
        }
    }


    /// <summary>
    /// Culture of document.
    /// </summary>
    public string DocumentCulture
    {
        get
        {
            return ValidationHelper.GetString(ViewState["DocumentCulture"], CMSContext.PreferredCultureCode);
        }
        set
        {
            ViewState["DocumentCulture"] = value;
        }
    }


    /// <summary>
    /// Currently edited node.
    /// </summary>
    public virtual TreeNode Node
    {
        get
        {
            return mNode ?? (mNode = DocumentHelper.GetDocument(NodeID, DocumentCulture, CombineWithDefaultCulture, TreeProvider));
        }
        set
        {
            mNode = value;
        }
    }


    /// <summary>
    /// Tree provider.
    /// </summary>
    public TreeProvider TreeProvider
    {
        get
        {
            return mTreeProvider ?? (mTreeProvider = new TreeProvider(CurrentUser));
        }
        set
        {
            mTreeProvider = value;
        }
    }


    /// <summary>
    /// Workflow manager.
    /// </summary>
    public WorkflowManager WorkflowManager
    {
        get
        {
            return mWorkflowManager ?? (mWorkflowManager = new WorkflowManager(TreeProvider));
        }
        set
        {
            mWorkflowManager = value;
        }
    }


    /// <summary>
    /// Returns workflow step information of current node.
    /// </summary>
    public WorkflowStepInfo WorkflowStepInfo
    {
        get
        {
            if ((mWorkflowStepInfo == null) && (Node != null))
            {
                mWorkflowStepInfo = (mWorkflowStepInfo = WorkflowManager.GetStepInfo(Node, false) ?? WorkflowManager.GetFirstWorkflowStep(Node));
            }

            return mWorkflowStepInfo;
        }
        set
        {
            mWorkflowStepInfo = value;
        }
    }


    /// <summary>
    /// Version manager.
    /// </summary>
    public VersionManager VersionManager
    {
        get
        {
            return mVersionManager ?? (mVersionManager = new VersionManager(TreeProvider));
        }
        set
        {
            mVersionManager = value;
        }
    }


    /// <summary>
    /// Determines whether current user is allowed to destroy current node.
    /// </summary>
    public bool CanDestroy
    {
        get
        {
            return (CurrentUser.IsAuthorizedPerDocument(Node, NodePermissionsEnum.Destroy) == AuthorizationResultEnum.Allowed);
        }
    }


    /// <summary>
    /// Determines whether current user is allowed to modify given node.
    /// </summary>
    public bool CanModify
    {
        get
        {
            return (CurrentUser.IsAuthorizedPerDocument(Node, NodePermissionsEnum.Modify) == AuthorizationResultEnum.Allowed);
        }
    }


    /// <summary>
    /// Determines whether current user is allowed to approve document.
    /// </summary>
    public bool CanApprove
    {
        get
        {
            bool canApprove = WorkflowManager.CanUserApprove(Node, CurrentUser);
            if (WorkflowStepInfo != null)
            {
                string stepName = WorkflowStepInfo.StepName.ToLower();

                switch (stepName)
                {
                    case "edit":
                    case "published":
                    case "archived":
                        canApprove = true;
                        break;
                }
            }
            return canApprove;
        }
    }


    /// <summary>
    /// Returns identifier of user for whom the node is currently checked out.
    /// </summary>
    public int CheckedOutByUserID
    {
        get
        {
            if (Node != null)
            {
                return Node.DocumentCheckedOutByUserID;
            }

            return 0;
        }
    }


    /// <summary>
    /// Determines whether given node is checked out by another user.
    /// </summary>
    public bool CheckedOutByAnotherUser
    {
        get
        {
            return (CheckedOutByUserID != 0) && (CheckedOutByUserID != CurrentUser.UserID);
        }
    }


    /// <summary>
    /// Determines whether user has permission 'CheckInAll'.
    /// </summary>
    public bool CanCheckIn
    {
        get
        {
            return CurrentUser.IsAuthorizedPerResource("CMS.Content", "CheckInAll");
        }
    }


    /// <summary>
    /// Current user info.
    /// </summary>
    protected CurrentUserInfo CurrentUser
    {
        get
        {
            return mCurrentUser ?? (mCurrentUser = CMSContext.CurrentUser);
        }
    }


    /// <summary>
    /// Current site info.
    /// </summary>
    public SiteInfo CurrentSiteInfo
    {
        get
        {
            return mCurrentSiteInfo ?? (mCurrentSiteInfo = CMSContext.CurrentSite);
        }
    }

    #endregion


    #region "Methods"

    /// <summary>
    /// Invalidates working node.
    /// </summary>
    public void InvalidateNode()
    {
        mNode = null;
    }

    #endregion
}