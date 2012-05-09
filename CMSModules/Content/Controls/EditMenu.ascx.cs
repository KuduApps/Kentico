using System;

using CMS.skmMenuControl;
using CMS.TreeEngine;
using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.WorkflowEngine;
using CMS.SettingsProvider;
using CMS.UIControls;
using CMS.EventLog;
using CMS.PortalEngine;
using CMS.SiteProvider;

using TreeNode = CMS.TreeEngine.TreeNode;

public partial class CMSModules_Content_Controls_EditMenu : CMSUserControl
{
    #region "Variables"

    private int mNodeId = 0;
    private string mAction = null;
    private string mHelpName = null;
    private string mHelpTopicName = null;

    private bool newDocument = false;
    private bool newLink = false;
    private bool newCulture = false;
    private TreeNode mNode = null;
    private TreeProvider mTree = null;
    private VersionManager mVersionMan = null;
    private WorkflowManager mWorkflowMan = null;
    private bool mAllowEdit = true;
    private bool mAllowSave = true;
    private bool mEnablepasiveRefresh = true;
    private string mWorkflowInfo = "";
    private int mMenuItems = 0;

    public event EventHandler LocalSave = null;

    private bool mShowSave = true;
    private bool mShowCheckOut = true;
    private bool mShowCheckIn = true;
    private bool mShowUndoCheckOut = true;
    private bool mShowApprove = true;
    private bool mShowSubmitToApproval = true;
    private bool mShowReject = true;
    private bool mShowProperties = true;
    private bool mShowSpellCheck = false;
    private bool mShowDelete = false;
    private bool mShowCreateAnother = true;

    private bool mRenderScript = true;

    protected string controlFrame = "editview" + QueryHelper.GetString("frameSuffix", "");

    #endregion


    #region "Events"

    /// <summary>
    /// On before check in event handler.
    /// </summary>
    public event EventHandler OnBeforeCheckIn;


    /// <summary>
    /// On before check out event handler.
    /// </summary>
    public event EventHandler OnBeforeCheckOut;


    /// <summary>
    /// On before undo check out event handler.
    /// </summary>
    public event EventHandler OnBeforeUndoCheckOut;


    /// <summary>
    /// On before approve event handler.
    /// </summary>
    public event EventHandler OnBeforeApprove;


    /// <summary>
    /// On before reject event handler.
    /// </summary>
    public event EventHandler OnBeforeReject;


    /// <summary>
    /// On before save event handler.
    /// </summary>
    public event EventHandler OnBeforeSave;


    /// <summary>
    /// Raises on before check in event.
    /// </summary>
    /// <param name="sender">Sender</param>
    /// <param name="e">Event arguments</param>
    public void RaiseOnBeforeCheckIn(object sender, EventArgs e)
    {
        if (OnBeforeCheckIn != null)
        {
            OnBeforeCheckIn(sender, e);
        }
    }


    /// <summary>
    /// Raises on before check out event.
    /// </summary>
    /// <param name="sender">Sender</param>
    /// <param name="e">Event arguments</param>
    public void RaiseOnBeforeCheckOut(object sender, EventArgs e)
    {
        if (OnBeforeCheckOut != null)
        {
            OnBeforeCheckOut(sender, e);
        }
    }


    /// <summary>
    /// Raises on before undo check out event.
    /// </summary>
    /// <param name="sender">Sender</param>
    /// <param name="e">Event arguments</param>
    public void RaiseOnBeforeUndoCheckOut(object sender, EventArgs e)
    {
        if (OnBeforeUndoCheckOut != null)
        {
            OnBeforeUndoCheckOut(sender, e);
        }
    }


    /// <summary>
    /// Raises on before approve event.
    /// </summary>
    /// <param name="sender">Sender</param>
    /// <param name="e">Event arguments</param>
    public void RaiseOnBeforeApprove(object sender, EventArgs e)
    {
        if (OnBeforeApprove != null)
        {
            OnBeforeApprove(sender, e);
        }
    }


    /// <summary>
    /// Raises on before reject event.
    /// </summary>
    /// <param name="sender">Sender</param>
    /// <param name="e">Event arguments</param>
    public void RaiseOnBeforeReject(object sender, EventArgs e)
    {
        if (OnBeforeReject != null)
        {
            OnBeforeReject(sender, e);
        }
    }


    /// <summary>
    /// Raises on before save event.
    /// </summary>
    /// <param name="sender">Sender</param>
    /// <param name="e">Event arguments</param>
    public void RaiseOnBeforeSave(object sender, EventArgs e)
    {
        if (OnBeforeSave != null)
        {
            OnBeforeSave(sender, e);
        }
    }

    #endregion


    #region "Button display options"

    /// <summary>
    /// Show the Delete button?
    /// </summary>
    public bool ShowDelete
    {
        get
        {
            return mShowDelete;
        }
        set
        {
            mShowDelete = value;
        }
    }


    /// <summary>
    /// Show the Save button?
    /// </summary>
    public bool ShowSave
    {
        get
        {
            return mShowSave;
        }
        set
        {
            mShowSave = value;
        }
    }


    /// <summary>
    /// Show the Check Out button?
    /// </summary>
    public bool ShowCheckOut
    {
        get
        {
            return mShowCheckOut;
        }
        set
        {
            mShowCheckOut = value;
        }
    }


    /// <summary>
    /// Show the Check In button?
    /// </summary>
    public bool ShowCheckIn
    {
        get
        {
            return mShowCheckIn;
        }
        set
        {
            mShowCheckIn = value;
        }
    }


    /// <summary>
    /// Show the Undo CheckOut button?
    /// </summary>
    public bool ShowUndoCheckOut
    {
        get
        {
            return mShowUndoCheckOut;
        }
        set
        {
            mShowUndoCheckOut = value;
        }
    }


    /// <summary>
    /// Show the Approve button?
    /// </summary>
    public bool ShowApprove
    {
        get
        {
            return mShowApprove;
        }
        set
        {
            mShowApprove = value;
        }
    }


    /// <summary>
    /// Show the Reject button?
    /// </summary>
    public bool ShowReject
    {
        get
        {
            return mShowReject;
        }
        set
        {
            mShowReject = value;
        }
    }


    /// <summary>
    /// Show the Submit To Approval button?
    /// </summary>
    public bool ShowSubmitToApproval
    {
        get
        {
            return mShowSubmitToApproval;
        }
        set
        {
            mShowSubmitToApproval = value;
        }
    }


    /// <summary>
    /// Show the Properties button?
    /// </summary>
    public bool ShowProperties
    {
        get
        {
            return mShowProperties;
        }
        set
        {
            mShowProperties = value;
        }
    }


    /// <summary>
    /// Show spell check button.
    /// </summary>
    public bool ShowSpellCheck
    {
        get
        {
            return mShowSpellCheck;
        }
        set
        {
            mShowSpellCheck = value;
        }
    }


    /// <summary>
    /// If true, create another button can be displayed.
    /// </summary>
    public bool ShowCreateAnother
    {
        get
        {
            return mShowCreateAnother;
        }
        set
        {
            mShowCreateAnother = value;
        }
    }

    #endregion


    #region "Public properties"

    /// <summary>
    /// Tree provider.
    /// </summary>
    public TreeProvider Tree
    {
        get
        {
            if (mTree == null)
            {
                mTree = new TreeProvider(CMSContext.CurrentUser);
                mTree.CombineWithDefaultCulture = false;
            }
            return mTree;
        }
    }


    /// <summary>
    /// Version manager.
    /// </summary>
    public VersionManager VersionMan
    {
        get
        {
            return mVersionMan ?? (mVersionMan = new VersionManager(Tree));
        }
    }


    /// <summary>
    /// Worklfow manager.
    /// </summary>
    public WorkflowManager WorkflowMan
    {
        get
        {
            return mWorkflowMan ?? (mWorkflowMan = new WorkflowManager(Tree));
        }
    }


    /// <summary>
    /// Enable passive refresh.
    /// </summary>
    public bool EnablePassiveRefresh
    {
        get
        {
            return mEnablepasiveRefresh;
        }
        set
        {
            mEnablepasiveRefresh = value;
        }
    }


    /// <summary>
    /// Returns true if the document editing is allowed.
    /// </summary>
    public bool AllowEdit
    {
        get
        {
            return mAllowEdit;
        }
    }


    /// <summary>
    /// Save action allowed.
    /// </summary>
    public bool AllowSave
    {
        get
        {
            return mAllowSave;
        }
        set
        {
            mAllowSave = value;
        }
    }


    /// <summary>
    /// If true, the access permissions to the items are checked.
    /// </summary>
    public bool CheckPermissions
    {
        get
        {
            return ValidationHelper.GetBoolean(ViewState["CheckPermissions"], true);
        }
        set
        {
            ViewState["CheckPermissions"] = value;
        }
    }


    /// <summary>
    /// Returns the.
    /// </summary>
    public string WorkflowInfo
    {
        get
        {
            return mWorkflowInfo;
        }
    }


    /// <summary>
    /// Count of the menu items currently displayed.
    /// </summary>
    public int MenuItems
    {
        get
        {
            return mMenuItems;
        }
    }


    /// <summary>
    /// Node ID.
    /// </summary>
    public int NodeID
    {
        get
        {
            return mNodeId;
        }
        set
        {
            mNodeId = value;
        }
    }


    /// <summary>
    /// Culture code.
    /// </summary>
    public string CultureCode
    {
        get
        {
            return ValidationHelper.GetString(ViewState["CultureCode"], CMSContext.PreferredCultureCode);
        }
        set
        {
            ViewState["CultureCode"] = value;
        }
    }


    /// <summary>
    /// Action.
    /// </summary>
    public string Action
    {
        get
        {
            return mAction ?? (mAction = QueryHelper.GetString("action", string.Empty).ToLower());
        }
        set
        {
            mAction = value;
        }
    }


    /// <summary>
    /// Help name to identify the help within the javascript.
    /// </summary>
    public string HelpName
    {
        get
        {
            return mHelpName ?? (mHelpName = QueryHelper.GetText("helpname", string.Empty).ToLower());
        }
        set
        {
            mHelpName = value;
        }
    }


    /// <summary>
    /// Help topic name.
    /// </summary>
    public string HelpTopicName
    {
        get
        {
            return mHelpTopicName ?? (mHelpTopicName = QueryHelper.GetText("helptopicname", string.Empty).ToLower());
        }
        set
        {
            mHelpTopicName = value;
        }
    }


    /// <summary>
    /// Render the menu scripts?
    /// </summary>
    public bool RenderScript
    {
        get
        {
            return mRenderScript;
        }
        set
        {
            mRenderScript = value;
        }
    }


    /// <summary>
    /// Prefix of the action functions.
    /// </summary>
    public string FunctionsPrefix
    {
        get
        {
            return ValidationHelper.GetString(ViewState["FunctionsPrefix"], "");
        }
        set
        {
            ViewState["FunctionsPrefix"] = value;
        }
    }


    /// <summary>
    /// Document node.
    /// </summary>
    public TreeNode Node
    {
        get
        {
            return mNode ?? (mNode = DocumentHelper.GetDocument(NodeID, CultureCode, Tree));
        }
        set
        {
            mNode = value;
            NodeID = (mNode == null) ? NodeID : mNode.NodeID;
        }
    }

    #endregion


    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(HelpTopicName))
        {
            helpElem.TopicName = HelpTopicName;
            pnlHelp.Visible = true;
        }

        if (!string.IsNullOrEmpty(HelpName))
        {
            helpElem.HelpName = HelpName;
        }

        btnApprove.Attributes.Add("style", "display: none;");
        btnCheckIn.Attributes.Add("style", "display: none;");
        btnCheckOut.Attributes.Add("style", "display: none;");
        btnReject.Attributes.Add("style", "display: none;");
        btnSave.Attributes.Add("style", "display: none;");
        btnUndoCheckout.Attributes.Add("style", "display: none;");

        // Get the document ID
        if (NodeID <= 0)
        {
            NodeID = QueryHelper.GetInteger("nodeid", 0);
        }

        ReloadMenu();

        menuElem.Layout = MenuLayout.Horizontal;
        menuElem.ItemPadding = 0;
        menuElem.ItemSpacing = 0;
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        if (Visible)
        {
            ScriptHelper.RegisterScriptFile(Page, @"~/CMSModules/Content/CMSDesk/Edit/EditMenu.js");

            if (RenderScript)
            {
                string script = "function " + FunctionsPrefix + "LocalSave(nodeId) { " + Page.ClientScript.GetPostBackEventReference(btnSave, null) + "; } \n" +
                                "function " + FunctionsPrefix + "LocalApprove(nodeId) { " + Page.ClientScript.GetPostBackEventReference(btnApprove, null) + "; } \n" +
                                "function " + FunctionsPrefix + "LocalReject(nodeId) { " + Page.ClientScript.GetPostBackEventReference(btnReject, null) + "; } \n" +
                                "function " + FunctionsPrefix + "CheckOut(nodeId) { " + Page.ClientScript.GetPostBackEventReference(btnCheckOut, null) + "; } \n" +
                                "function " + FunctionsPrefix + "LocalCheckIn(nodeId) { " + Page.ClientScript.GetPostBackEventReference(btnCheckIn, null) + "; } \n" +
                                "function " + FunctionsPrefix + "UndoCheckOut(nodeId) { if(!confirm(" + ScriptHelper.GetString(GetString("EditMenu.UndoCheckOutConfirmation")) + ")) return false; " + Page.ClientScript.GetPostBackEventReference(btnUndoCheckout, null) + "; } \n";

                // Add control frame name
                script += "var controlFrame = 'editview'; \n";
                script += "var spellURL = '" + ((CMSContext.ViewMode == ViewModeEnum.LiveSite) ?
                    CMSContext.ResolveDialogUrl("~/CMSFormControls/LiveSelectors/SpellCheck.aspx") :
                    CMSContext.ResolveDialogUrl("~/CMSModules/Content/CMSDesk/Edit/SpellCheck.aspx")) + "'; \n";
                script += "var controlFrame = '" + controlFrame + "';\n";

                AddScript(script);
            }
        }
    }

    #endregion


    #region "Methods"

    public void ReloadMenu()
    {
        newLink = (Action == "newlink");
        newDocument = ((Action == "new") || newLink || Action == "newvariant");
        newCulture = (Action == "newculture");

        if (Action == "newvariant")
        {
            ShowSpellCheck = false;
        }

        MenuItem newItem = null;

        // Create the edit menu
        menuElem.Items.Clear();

        mMenuItems = 0;

        // Save button
        MenuItem saveItem = null;
        if (mShowSave)
        {
            saveItem = new MenuItem();
            saveItem.ToolTip = GetString("EditMenu.Save");
            saveItem.JavascriptCommand = FunctionsPrefix + "SaveDocument(" + NodeID + ");";
            saveItem.ImageAltText = saveItem.ToolTip;
            saveItem.Text = GetString("general.save");
            saveItem.Image = GetImageUrl("CMSModules/CMS_Content/EditMenu/save.png");
            saveItem.MouseOverImage = saveItem.Image;
            saveItem.MouseOverCssClass = "MenuItemEdit";
            saveItem.CssClass = "MenuItemEdit";
            menuElem.Items.Add(saveItem);
            mMenuItems += 1;
        }

        mWorkflowInfo = "";

        mAllowEdit = true;
        mAllowSave = true;
        bool showDelete = false;

        if (!newDocument)
        {
            if (Node != null && !newCulture)
            {
                bool authorized = true;

                // If the perrmisions should be checked
                if (CheckPermissions)
                {
                    // Check read permissions
                    if (CMSContext.CurrentUser.IsAuthorizedPerDocument(Node, NodePermissionsEnum.Read) == AuthorizationResultEnum.Denied)
                    {
                        RedirectToAccessDenied(String.Format(GetString("cmsdesk.notauthorizedtoreaddocument"), Node.NodeAliasPath));
                    }
                    // Check modify permissions
                    else if (CMSContext.CurrentUser.IsAuthorizedPerDocument(Node, NodePermissionsEnum.Modify) == AuthorizationResultEnum.Denied)
                    {
                        mAllowSave = false;
                        mWorkflowInfo = String.Format(GetString("cmsdesk.notauthorizedtoeditdocument"), Node.NodeAliasPath);
                        authorized = false;
                    }
                }

                // If authorized
                if (authorized)
                {
                    WorkflowInfo wi = WorkflowMan.GetNodeWorkflow(Node);
                    // If workflow not null, process the workflow information to display the items
                    if (wi != null)
                    {
                        // Get current step info, do not update document
                        WorkflowStepInfo si = null;
                        if (Node.IsPublished && (Node.DocumentCheckedOutVersionHistoryID <= 0))
                        {
                            si = WorkflowStepInfoProvider.GetWorkflowStepInfo("published", wi.WorkflowID);
                        }
                        else
                        {
                            si = WorkflowMan.GetStepInfo(Node, false) ??
                                 WorkflowMan.GetFirstWorkflowStep(Node, wi);
                        }

                        bool allowApproval = true;
                        bool canApprove = true;
                        bool autoPublishChanges = wi.WorkflowAutoPublishChanges;

                        // If license does not allow custom steps, can approve check is useless
                        if (WorkflowInfoProvider.IsCustomStepAllowed())
                        {
                            canApprove = WorkflowMan.CanUserApprove(Node, CMSContext.CurrentUser);
                        }

                        string stepName = si.StepName.ToLower();
                        if (!(canApprove || (stepName == "edit") || (stepName == "published") || (stepName == "archived")))
                        {
                            mAllowSave = false;
                        }

                        bool useCheckInCheckOut = wi.UseCheckInCheckOut(CMSContext.CurrentSiteName);

                        // Check-in, Check-out
                        if (useCheckInCheckOut)
                        {
                            if (!Node.IsCheckedOut)
                            {
                                mAllowSave = false;
                                mAllowEdit = false;

                                // If not checked out, add the check-out button
                                if (mShowCheckOut && (canApprove || (stepName == "edit") || (stepName == "published") || (stepName == "archived")))
                                {
                                    newItem = new MenuItem();
                                    newItem.ToolTip = GetString("EditMenu.CheckOut");
                                    newItem.JavascriptCommand = FunctionsPrefix + "CheckOut(" + NodeID + ");";
                                    newItem.ImageAltText = newItem.ToolTip;
                                    newItem.Text = GetString("EditMenu.IconCheckOut");
                                    newItem.Image = GetImageUrl("CMSModules/CMS_Content/EditMenu/checkout.png");
                                    newItem.MouseOverImage = newItem.Image;
                                    newItem.MouseOverCssClass = "MenuItemEdit";
                                    newItem.CssClass = "MenuItemEdit";
                                    menuElem.Items.Add(newItem);
                                    mMenuItems += 1;
                                }
                                // Set workflow info
                                // If not checked out, add the check-out information
                                if (canApprove || (stepName == "edit") || (stepName == "published") || (stepName == "archived"))
                                {
                                    mWorkflowInfo += GetString("EditContent.DocumentCheckedIn");
                                }
                            }
                        }
                        if (Node.IsCheckedOut)
                        {
                            // If checked out by current user, add the check-in button
                            int checkedOutBy = Node.DocumentCheckedOutByUserID;
                            if (checkedOutBy == CMSContext.CurrentUser.UserID)
                            {
                                if (mShowUndoCheckOut)
                                {
                                    newItem = new MenuItem();
                                    newItem.ToolTip = GetString("EditMenu.UndoCheckout");
                                    newItem.JavascriptCommand = FunctionsPrefix + "UndoCheckOut(" + NodeID + ");";
                                    newItem.ImageAltText = newItem.ToolTip;
                                    newItem.Text = GetString("EditMenu.IconUndoCheckout");
                                    newItem.Image = GetImageUrl("CMSModules/CMS_Content/EditMenu/undocheckout.png");
                                    newItem.MouseOverImage = newItem.Image;
                                    newItem.MouseOverCssClass = "MenuItemEdit";
                                    newItem.CssClass = "MenuItemEdit";
                                    menuElem.Items.Add(newItem);
                                    mMenuItems += 1;
                                }

                                if (mShowCheckIn)
                                {
                                    newItem = new MenuItem();
                                    newItem.ToolTip = GetString("EditMenu.CheckIn");
                                    newItem.JavascriptCommand = FunctionsPrefix + "CheckIn(" + NodeID + ");";
                                    newItem.ImageAltText = newItem.ToolTip;
                                    newItem.Text = GetString("EditMenu.IconCheckIn");
                                    newItem.Image = GetImageUrl("CMSModules/CMS_Content/EditMenu/checkin.png");
                                    newItem.MouseOverImage = newItem.Image;
                                    newItem.MouseOverCssClass = "MenuItemEdit";
                                    newItem.CssClass = "MenuItemEdit";
                                    menuElem.Items.Add(newItem);
                                    mMenuItems += 1;
                                }

                                // Set workflow info
                                mWorkflowInfo += GetString("EditContent.DocumentCheckedOut");
                            }
                            else
                            {
                                mAllowSave = false;
                                mAllowEdit = false;

                                string userName = UserInfoProvider.GetUserNameById(checkedOutBy);
                                // Set workflow info
                                mWorkflowInfo += String.Format(GetString("EditContent.DocumentCheckedOutByAnother"), userName);
                            }
                            allowApproval = false;
                        }

                        // Document approval
                        if (allowApproval)
                        {
                            MenuItem approveItem = null;

                            switch (stepName)
                            {
                                case "edit":
                                    // When edit step and not allowed 'auto publish changes', display submit to approval
                                    if (mShowSubmitToApproval && !autoPublishChanges)
                                    {
                                        approveItem = new MenuItem();
                                        approveItem.ToolTip = GetString("EditMenu.SubmitToApproval");
                                        approveItem.JavascriptCommand = FunctionsPrefix + "Approve(" + NodeID + ");";
                                        approveItem.ImageAltText = approveItem.ToolTip;
                                        approveItem.Text = GetString("EditMenu.IconSubmitToApproval");
                                        approveItem.Image = GetImageUrl("CMSModules/CMS_Content/EditMenu/approve.png");
                                        approveItem.MouseOverImage = approveItem.Image;
                                        approveItem.MouseOverCssClass = "MenuItemEdit";
                                        approveItem.CssClass = "MenuItemEdit";
                                        menuElem.Items.Add(approveItem);
                                        mMenuItems += 1;
                                    }
                                    break;

                                case "published":
                                case "archived":
                                    // No workflow options in these steps
                                    break;

                                default:
                                    // If the user is authorized to perform the step, display the approve and reject buttons
                                    if (canApprove)
                                    {
                                        if (mShowApprove && !autoPublishChanges)
                                        {
                                            approveItem = new MenuItem();
                                            approveItem.ToolTip = GetString("EditMenu.Approve");
                                            approveItem.JavascriptCommand = FunctionsPrefix + "Approve(" + NodeID + ");";
                                            approveItem.ImageAltText = approveItem.ToolTip;
                                            approveItem.Text = GetString("general.approve");
                                            approveItem.Image = GetImageUrl("CMSModules/CMS_Content/EditMenu/approve.png");
                                            approveItem.MouseOverImage = approveItem.Image;
                                            approveItem.MouseOverCssClass = "MenuItemEdit";
                                            approveItem.CssClass = "MenuItemEdit";
                                            menuElem.Items.Add(approveItem);
                                            mMenuItems += 1;
                                        }

                                        if (mShowReject && !autoPublishChanges)
                                        {
                                            newItem = new MenuItem();
                                            newItem.ToolTip = GetString("EditMenu.Reject");
                                            newItem.JavascriptCommand = FunctionsPrefix + "Reject(" + NodeID + ");";
                                            newItem.ImageAltText = newItem.ToolTip;
                                            newItem.Text = GetString("general.reject");
                                            newItem.Image = GetImageUrl("CMSModules/CMS_Content/EditMenu/reject.png");
                                            newItem.MouseOverImage = newItem.Image;
                                            newItem.MouseOverCssClass = "MenuItemEdit";
                                            newItem.CssClass = "MenuItemEdit";
                                            menuElem.Items.Add(newItem);
                                            mMenuItems += 1;
                                        }
                                    }
                                    break;
                            }

                            if (approveItem != null)
                            {
                                // Get next step info
                                WorkflowStepInfo nsi = WorkflowMan.GetNextStepInfo(Node);
                                if (nsi.StepName.ToLower() == "published")
                                {
                                    approveItem.ToolTip = GetString("EditMenu.Publish");
                                    approveItem.Text = GetString("EditMenu.IconPublish");
                                }
                            }

                            // Set workflow info
                            if (!(canApprove || (stepName == "edit") || (stepName == "published") || (stepName == "archived")))
                            {
                                if (!string.IsNullOrEmpty(mWorkflowInfo))
                                {
                                    mWorkflowInfo += "<br />";
                                }
                                mWorkflowInfo += GetString("EditContent.NotAuthorizedToApprove");
                            }
                            if (!string.IsNullOrEmpty(mWorkflowInfo))
                            {
                                mWorkflowInfo += "<br />";
                            }
                            // If workflow isn't auto publish or step name isn't 'published' or 'check-in/check-out' is allowed then show current step name
                            if (!wi.WorkflowAutoPublishChanges || (stepName != "published") || useCheckInCheckOut)
                            {
                                mWorkflowInfo += String.Format(GetString("EditContent.CurrentStepInfo"), HTMLHelper.HTMLEncode(ResHelper.LocalizeString(si.StepDisplayName)));
                            }
                        }
                    }
                }

                if (mShowProperties)
                {
                    newItem = new MenuItem();
                    newItem.ToolTip = GetString("EditMenu.Properties");
                    newItem.JavascriptCommand = FunctionsPrefix + "Properties(" + NodeID + ");";
                    newItem.ImageAltText = newItem.ToolTip;
                    newItem.Text = GetString("EditMenu.IconProperties");
                    newItem.Image = GetImageUrl("CMSModules/CMS_Content/EditMenu/properties.png");
                    newItem.MouseOverImage = newItem.Image;
                    newItem.MouseOverCssClass = "MenuItemEdit";
                    newItem.CssClass = "MenuItemEdit";
                    menuElem.Items.Add(newItem);
                    mMenuItems += 1;
                }

                if (mShowDelete)
                {
                    showDelete = true;
                }
            }

            // If not allowed to save, disable the save item
            if (!mAllowSave && mShowSave && (saveItem != null))
            {
                saveItem.JavascriptCommand = "";
                saveItem.MouseOverCssClass = "MenuItemEditDisabled";
                saveItem.CssClass = "MenuItemEditDisabled";
                saveItem.Image = GetImageUrl("CMSModules/CMS_Content/EditMenu/savedisabled.png");
                saveItem.MouseOverImage = saveItem.Image;
            }
        }
        else if (mAllowSave && mShowCreateAnother)
        {
            newItem = new MenuItem();
            newItem.ToolTip = GetString("EditMenu.SaveAndAnother");
            newItem.JavascriptCommand = FunctionsPrefix + "SaveDocument(" + NodeID + ", true);";
            newItem.ImageAltText = newItem.ToolTip;
            newItem.Text = GetString("editmenu.iconsaveandanother");
            newItem.Image = GetImageUrl("CMSModules/CMS_Content/EditMenu/saveandanother.png");
            newItem.MouseOverImage = newItem.Image;
            newItem.MouseOverCssClass = "MenuItemEdit";
            newItem.CssClass = "MenuItemEdit";
            menuElem.Items.Add(newItem);
            mMenuItems += 1;
        }

        if (mAllowSave && mShowSave && mShowSpellCheck && !newLink)
        {
            newItem = new MenuItem();
            newItem.ToolTip = GetString("EditMenu.SpellCheck");
            newItem.JavascriptCommand = FunctionsPrefix + "SpellCheck(spellURL);";
            newItem.ImageAltText = newItem.ToolTip;
            newItem.Text = GetString("EditMenu.IconSpellCheck");
            newItem.Image = GetImageUrl("CMSModules/CMS_Content/EditMenu/spellcheck.png");
            newItem.MouseOverImage = newItem.Image;
            newItem.MouseOverCssClass = "MenuItemEdit";
            newItem.CssClass = "MenuItemEdit";
            menuElem.Items.Add(newItem);
            mMenuItems += 1;
        }

        if (showDelete)
        {
            newItem = new MenuItem();
            newItem.ToolTip = GetString("EditMenu.Delete");
            newItem.JavascriptCommand = FunctionsPrefix + "Delete(" + NodeID + ");";
            newItem.ImageAltText = newItem.ToolTip;
            newItem.Text = GetString("general.delete");
            newItem.Image = GetImageUrl("CMSModules/CMS_Content/EditMenu/delete.png");
            newItem.MouseOverImage = newItem.Image;
            newItem.MouseOverCssClass = "MenuItemEdit";
            newItem.CssClass = "MenuItemEdit";
            menuElem.Items.Add(newItem);
            mMenuItems += 1;
        }
    }


    /// <summary>
    /// Adds the alert message to the output request window.
    /// </summary>
    /// <param name="message">Message to display</param>
    private void AddAlert(string message)
    {
        ScriptHelper.RegisterStartupScript(this, typeof(string), message.GetHashCode().ToString(), ScriptHelper.GetAlertScript(message));
    }


    /// <summary>
    /// Adds the script to the output request window.
    /// </summary>
    /// <param name="script">Script to add</param>
    private void AddScript(string script)
    {
        ScriptHelper.RegisterStartupScript(this, typeof(string), script.GetHashCode().ToString(), ScriptHelper.GetScript(script));
    }


    private void PassiveRefresh(int nodeId)
    {
        ScriptHelper.RegisterStartupScript(this, typeof(string), "passiveRefresh_" + nodeId, ScriptHelper.GetScript("if (window.PassiveRefresh) { window.PassiveRefresh(" + nodeId + ");}"));
    }


    private void RefreshTree(int parentNodeId, int nodeId)
    {
        ScriptHelper.RegisterStartupScript(this, typeof(string), "refreshTree_" + nodeId, ScriptHelper.GetScript("if (window.RefreshTree) { window.RefreshTree(" + parentNodeId + ", " + nodeId + ");}"));
    }

    #endregion


    #region "Button handling"

    protected void btnApprove_Click(object sender, EventArgs e)
    {
        bool nextStepIsPublished = false;

        if (Node != null)
        {
            // Check modify permissions
            if (CheckPermissions && (CMSContext.CurrentUser.IsAuthorizedPerDocument(Node, NodePermissionsEnum.Modify) == AuthorizationResultEnum.Denied))
            {
                return;
            }

            // Raise event
            RaiseOnBeforeApprove(sender, e);

            // Approve the document - Go to next workflow step
            // Get original step
            WorkflowStepInfo originalStep = WorkflowMan.GetStepInfo(Node);

            // Approve the document
            WorkflowStepInfo nextStep = WorkflowMan.MoveToNextStep(Node, "");

            if (nextStep != null)
            {
                nextStepIsPublished = (nextStep.StepName.ToLower() == "published");

                // Send workflow e-mails
                if (SettingsKeyProvider.GetBoolValue(CMSContext.CurrentSiteName + ".CMSSendWorkflowEmails"))
                {
                    WorkflowMan.SendWorkflowEmails(Node, CMSContext.CurrentUser, originalStep, nextStep, nextStepIsPublished ? WorkflowActionEnum.Published : WorkflowActionEnum.Approved, string.Empty);
                }
            }

            ReloadMenu();

            string siteName = CMSContext.CurrentSiteName;

            // Refresh content tree when step is 'Published' or scope has been removed and icons should be displayed
            if ((nextStepIsPublished || (nextStep == null)) && (UIHelper.DisplayPublishedIcon(siteName) || UIHelper.DisplayVersionNotPublishedIcon(siteName) || UIHelper.DisplayNotPublishedIcon(siteName)))
            {
                RefreshTree(Node.NodeParentID, NodeID);
            }
        }

        if (EnablePassiveRefresh)
        {
            PassiveRefresh(NodeID);
        }
    }


    protected void btnReject_Click(object sender, EventArgs e)
    {
        // Reject the document - Go to previous workflow step
        if (Node != null)
        {
            // Check modify permissions
            if (CheckPermissions && (CMSContext.CurrentUser.IsAuthorizedPerDocument(Node, NodePermissionsEnum.Modify) == AuthorizationResultEnum.Denied))
            {
                return;
            }

            // Raise event
            RaiseOnBeforeReject(sender, e);

            // Get original step
            WorkflowStepInfo originalStep = WorkflowMan.GetStepInfo(Node);

            // Reject the document
            WorkflowStepInfo previousStep = WorkflowMan.MoveToPreviousStep(Node, "");

            // Send workflow e-mails
            if (SettingsKeyProvider.GetBoolValue(CMSContext.CurrentSiteName + ".CMSSendWorkflowEmails"))
            {
                WorkflowMan.SendWorkflowEmails(Node, CMSContext.CurrentUser, originalStep, previousStep, WorkflowActionEnum.Rejected, "");
            }

            ReloadMenu();

            string siteName = CMSContext.CurrentSiteName;

            if (UIHelper.DisplayCheckedOutIcon(siteName) || UIHelper.DisplayNotPublishedIcon(siteName))
            {
                RefreshTree(Node.NodeParentID, NodeID);
            }
        }

        if (EnablePassiveRefresh)
        {
            PassiveRefresh(NodeID);
        }
    }


    protected void btnCheckIn_Click(object sender, EventArgs e)
    {
        try
        {
            // Check in the document
            if (Node != null)
            {
                // Check modify permissions
                if (CheckPermissions && (CMSContext.CurrentUser.IsAuthorizedPerDocument(Node, NodePermissionsEnum.Modify) == AuthorizationResultEnum.Denied))
                {
                    return;
                }

                // Raise event
                RaiseOnBeforeCheckIn(sender, e);

                // Check in the document        
                VersionMan.CheckIn(Node, null, null);

                ReloadMenu();

                if (UIHelper.DisplayCheckedOutIcon(CMSContext.CurrentSiteName))
                {
                    RefreshTree(Node.NodeParentID, NodeID);
                }
            }
        }
        catch (WorkflowException)
        {
            AddAlert(GetString("EditContent.DocumentCannotCheckIn"));
        }
        catch (Exception ex)
        {
            // Log exception
            EventLogProvider ep = new EventLogProvider();
            ep.LogEvent("Content", "CHECKIN", ex);
            AddAlert(ex.Message);
        }

        if (EnablePassiveRefresh)
        {
            PassiveRefresh(NodeID);
        }
    }


    protected void btnCheckOut_Click(object sender, EventArgs e)
    {
        try
        {
            // Check out the document
            if (Node != null)
            {
                // Check modify permissions
                if (CheckPermissions && (CMSContext.CurrentUser.IsAuthorizedPerDocument(Node, NodePermissionsEnum.Modify) == AuthorizationResultEnum.Denied))
                {
                    return;
                }

                // Raise event
                RaiseOnBeforeCheckOut(sender, e);

                VersionMan.EnsureVersion(Node, Node.IsPublished);

                // Check out the document
                VersionMan.CheckOut(Node);

                ReloadMenu();

                if (UIHelper.DisplayCheckedOutIcon(CMSContext.CurrentSiteName))
                {
                    RefreshTree(Node.NodeParentID, NodeID);
                }
            }
        }
        catch (WorkflowException)
        {
            AddAlert(GetString("EditContent.DocumentCannotCheckOut"));
        }
        catch (Exception ex)
        {
            // Log exception
            EventLogProvider ep = new EventLogProvider();
            ep.LogEvent("Content", "CHECKOUT", ex);
            AddAlert(ex.Message);
        }

        // Ensure js script for split mode
        EnsureSplitModeScript();

        if (EnablePassiveRefresh)
        {
            PassiveRefresh(NodeID);
        }
    }


    protected void btnUndoCheckout_Click(object sender, EventArgs e)
    {
        // Check out the document
        if (Node != null)
        {
            try
            {
                // Check modify permissions
                if (CheckPermissions && (CMSContext.CurrentUser.IsAuthorizedPerDocument(Node, NodePermissionsEnum.Modify) == AuthorizationResultEnum.Denied))
                {
                    return;
                }

                // Raise event
                RaiseOnBeforeUndoCheckOut(sender, e);

                // Undo check out
                VersionMan.UndoCheckOut(Node);

                ReloadMenu();

                if (UIHelper.DisplayCheckedOutIcon(CMSContext.CurrentSiteName))
                {
                    RefreshTree(Node.NodeParentID, NodeID);
                }
            }
            catch (WorkflowException)
            {
                AddAlert(GetString("EditContent.DocumentCannotUndoCheckOut"));
            }
            catch (Exception ex)
            {
                // Log exception
                EventLogProvider ep = new EventLogProvider();
                ep.LogEvent("Content", "UNDOCHECKOUT", ex);
                AddAlert(ex.Message);
            }
        }

        // Ensure js script for split mode
        EnsureSplitModeScript();

        if (EnablePassiveRefresh)
        {
            PassiveRefresh(NodeID);
        }
    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        // Check modify permissions
        if (CheckPermissions && (CMSContext.CurrentUser.IsAuthorizedPerDocument(Node, NodePermissionsEnum.Modify) == AuthorizationResultEnum.Denied))
        {
            return;
        }

        if (LocalSave != null)
        {
            // Raise event
            RaiseOnBeforeSave(sender, e);

            LocalSave(sender, e);
        }
    }


    /// <summary>
    /// Ensures js script for split mode.
    /// </summary>
    private void EnsureSplitModeScript()
    {
        if (URLHelper.IsPostback() && CMSContext.DisplaySplitMode)
        {
            // Indicates if is postback and original and split mode cultures are same
            bool refresh = (string.Compare(CultureHelper.GetOriginalPreferredCulture(), CMSContext.SplitModeCultureCode, StringComparison.InvariantCultureIgnoreCase) == 0);

            // Register js script
            ScriptHelper.RegisterSplitModeSync(Page, true, false, refresh, true);
        }
    }

    #endregion
}