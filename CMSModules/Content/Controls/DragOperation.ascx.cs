using System;
using System.Collections;
using System.Security.Principal;
using System.Threading;

using CMS.GlobalHelper;
using CMS.TreeEngine;

using TreeNode = CMS.TreeEngine.TreeNode;

public partial class CMSModules_Content_Controls_DragOperation : ContentActionsControl
{
    #region "Private variables"

    protected int nodeId = 0;
    protected int targetNodeId = 0;
    private string canceledString = null;

    protected string action = null;

    protected static Hashtable mInfos = new Hashtable();

    protected TreeNode node = null;
    protected TreeNode targetNode = null;
    protected bool childNodes = false;

    #endregion


    #region "Properties"

    /// <summary>
    /// Current log context.
    /// </summary>
    public LogContext CurrentLog
    {
        get
        {
            return EnsureLog();
        }
    }


    /// <summary>
    /// Current Error.
    /// </summary>
    public string CurrentError
    {
        get
        {
            return ValidationHelper.GetString(mInfos["CopyMoveError_" + ctlAsync.ProcessGUID], string.Empty);
        }
        set
        {
            mInfos["CopyMoveError_" + ctlAsync.ProcessGUID] = value;
        }
    }


    /// <summary>
    /// Current Info.
    /// </summary>
    public string CurrentInfo
    {
        get
        {
            return ValidationHelper.GetString(mInfos["CopyMoveInfo" + ctlAsync.ProcessGUID], string.Empty);
        }
        set
        {
            mInfos["CopyMoveInfo_" + ctlAsync.ProcessGUID] = value;
        }
    }


    /// <summary>
    /// Gets the document node which is moved / copied / linked
    /// </summary>
    public TreeNode Node
    {
        get
        {
            return node;
        }
    }

    #endregion


    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Initialize events
        ctlAsync.OnFinished += ctlAsync_OnFinished;
        ctlAsync.OnError += ctlAsync_OnError;
        ctlAsync.OnRequestLog += ctlAsync_OnRequestLog;
        ctlAsync.OnCancel += ctlAsync_OnCancel;

        // Get the data
        nodeId = QueryHelper.GetInteger("nodeid", 0);
        targetNodeId = QueryHelper.GetInteger("targetnodeid", 0);
        action = QueryHelper.GetString("action", "");

        if (!Page.IsCallback)
        {
            // Register the main CMS script
            ScriptHelper.RegisterCMS(this.Page);

            // Get the node
            node = TreeProvider.SelectSingleNode(nodeId);
            targetNode = TreeProvider.SelectSingleNode(targetNodeId, TreeProvider.ALL_CULTURES);

            // Set visibility of panels
            pnlContent.Visible = true;
            pnlLog.Visible = false;

            if ((node != null) && (targetNode != null))
            {
                string targetName = targetNode.DocumentName;
                bool isRoot = targetNode.NodeClassName.Equals("cms.root", StringComparison.InvariantCultureIgnoreCase);

                // Get the real target node
                if (!isRoot && (action.IndexOf("position", StringComparison.InvariantCultureIgnoreCase) >= 0))
                {
                    // Get the target order and real parent ID
                    int newTargetId = targetNode.NodeParentID;
                    TreeNode realTargetNode = TreeProvider.SelectSingleNode(newTargetId);
                    if (realTargetNode != null)
                    {
                        targetName = realTargetNode.DocumentName;
                    }
                }

                // Root node
                if (String.IsNullOrEmpty(targetName))
                {
                    targetName = "/";
                }

                // Initialize resource strings, images
                btnCancel.OnClientClick = ctlAsync.GetCancelScript(true) + "return false;";
                btnNo.OnClientClick = "DisplayDocument(); return false;";

                lblTarget.Text = GetString("ContentOperation.TargetDocument") + " <strong>" + HTMLHelper.HTMLEncode(targetName) + "</strong>";

                switch (action.ToLower())
                {
                    case "movenode":
                    case "movenodeposition":
                    case "movenodefirst":
                        // Setup page title text and image
                        titleElemAsync.TitleText = GetString("ContentRequest.StartMove");
                        titleElemAsync.TitleImage = GetImageUrl("CMSModules/CMS_Content/Dialogs/titlemove.png");

                        canceledString = "ContentRequest.MoveCanceled";
                        lblQuestion.Text = GetString("ContentMove.Question");
                        chkCopyPerm.Text = GetString("contentrequest.preservepermissions");
                        break;

                    case "copynode":
                    case "copynodeposition":
                    case "copynodefirst":
                        // Setup page title text and image
                        titleElemAsync.TitleText = GetString("ContentRequest.StartCopy");
                        titleElemAsync.TitleImage = GetImageUrl("CMSModules/CMS_Content/Dialogs/titlecopy.png");

                        canceledString = "ContentRequest.CopyingCanceled";
                        childNodes = chkChild.Checked;
                        plcCopyCheck.Visible = (node.NodeChildNodesCount > 0);
                        chkChild.ResourceString = "contentrequest.copyunderlying";

                        lblQuestion.Text = GetString("ContentCopy.Question");
                        chkCopyPerm.Text = GetString("contentrequest.copypermissions");
                        break;

                    case "linknode":
                    case "linknodeposition":
                    case "linknodefirst":
                        // Setup page title text and image
                        titleElemAsync.TitleText = GetString("ContentRequest.StartLink");
                        titleElemAsync.TitleImage = GetImageUrl("CMSModules/CMS_Content/Dialogs/titlelink.png");

                        canceledString = "ContentRequest.LinkCanceled";
                        childNodes = chkChild.Checked;
                        plcCopyCheck.Visible = (node.NodeChildNodesCount > 0);
                        chkChild.ResourceString = "contentrequest.linkunderlying";

                        lblQuestion.Text = GetString("ContentLink.Question");
                        chkCopyPerm.Text = GetString("contentrequest.copypermissions");
                        break;

                    default:
                        lblError.Text = GetString("error.notsupported");
                        pnlAction.Visible = false;
                        break;
                }
            }
            else
            {
                // Hide everything
                pnlContent.Visible = false;
            }
        }
    }


    protected override void OnPreRender(EventArgs e)
    {
        lblError.Visible = (lblError.Text != string.Empty);

        base.OnPreRender(e);
    }

    #endregion


    #region "Button actions"

    protected void btnOK_Click(object sender, EventArgs e)
    {
        pnlLog.Visible = true;
        pnlContent.Visible = false;

        EnsureLog();
        CurrentError = string.Empty;
        CurrentInfo = string.Empty;

        // Perform the action
        ctlAsync.RunAsync(DoAction, WindowsIdentity.GetCurrent());
    }

    #endregion


    #region "Action methods"

    /// <summary>
    /// Deletes document(s).
    /// </summary>
    private void DoAction(object parameter)
    {
        // Get the target node
        if (targetNode == null)
        {
            return;
        }

        if (node == null)
        {
            return;
        }

        try
        {
            switch (action.ToLower())
            {
                case "movenode":
                case "movenodeposition":
                case "movenodefirst":
                    {
                        AddLog(GetString("ContentRequest.StartMove"));
                    }
                    break;

                case "copynode":
                case "copynodeposition":
                case "copynodefirst":
                    {
                        AddLog(GetString("ContentRequest.StartCopy"));
                    }
                    break;

                case "linknode":
                case "linknodeposition":
                case "linknodefirst":
                    {
                        AddLog(GetString("ContentRequest.StartLink"));
                    }
                    break;
            }

            // Process the action
            TreeNode newNode = ProcessAction(node, targetNode, action, childNodes, true, chkCopyPerm.Checked);
            if (newNode != null)
            {
                int refreshId = newNode.NodeID;

                // Refresh tree
                ctlAsync.Parameter = "RefreshTree(" + refreshId + ", " + refreshId + "); \n" + "DisplayDocument(" + refreshId + ");";
            }
        }
        catch (ThreadAbortException)
        {
        }
    }


    /// <summary>
    /// Adds the alert error message to the response.
    /// </summary>
    /// <param name="message">Message</param>
    protected override void AddError(string message)
    {
        CurrentError = message;
    }

    #endregion


    #region "Help methods"

    /// <summary>
    /// Adds the script to the output request window.
    /// </summary>
    /// <param name="script">Script to add</param>
    public void AddScript(string script)
    {
        ltlScript.Text += ScriptHelper.GetScript(script);
    }

    #endregion


    #region "Handling async thread"

    private void ctlAsync_OnCancel(object sender, EventArgs e)
    {
        CurrentError = GetString(canceledString);
        ltlScript.Text += ScriptHelper.GetScript("var __pendingCallbacks = new Array();");
        lblError.Text = CurrentError;
        CurrentLog.Close();
    }


    private void ctlAsync_OnRequestLog(object sender, EventArgs e)
    {
        ctlAsync.Log = CurrentLog.Log;
    }


    private void ctlAsync_OnError(object sender, EventArgs e)
    {
        lblError.Text = CurrentError;
        CurrentLog.Close();
    }


    private void ctlAsync_OnFinished(object sender, EventArgs e)
    {
        lblError.Text = CurrentError;
        CurrentLog.Close();

        if (ctlAsync.Parameter != null)
        {
            AddScript(ctlAsync.Parameter.ToString());
        }
    }


    /// <summary>
    /// Ensures the logging context.
    /// </summary>
    protected LogContext EnsureLog()
    {
        LogContext log = LogContext.EnsureLog(ctlAsync.ProcessGUID);

        log.Reversed = true;
        log.LineSeparator = "<br />";

        return log;
    }


    /// <summary>
    /// Adds the log information.
    /// </summary>
    /// <param name="newLog">New log information</param>
    protected override void AddLog(string newLog)
    {
        EnsureLog();

        LogContext.AppendLine(newLog);
    }

    #endregion
}