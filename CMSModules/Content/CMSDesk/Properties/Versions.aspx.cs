using System;

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.TreeEngine;
using CMS.UIControls;
using CMS.WorkflowEngine;
using CMS.SiteProvider;
using CMS.EventLog;

using TreeNode = CMS.TreeEngine.TreeNode;

[RegisterTitle("content.ui.propertiesversions")]
public partial class CMSModules_Content_CMSDesk_Properties_Versions : CMSPropertiesPage
{
    #region "Private variables"

    private WorkflowInfo mWorkflowInfo = null;
    private bool displaySplitMode = CMSContext.DisplaySplitMode;

    #endregion


    #region "Properties"

    /// <summary>
    /// Identifier of edited node.
    /// </summary>
    public int NodeID
    {
        get
        {
            return versionsElem.NodeID;
        }
    }


    /// <summary>
    /// Currently edited node.
    /// </summary>
    public TreeNode Node
    {
        get
        {
            return versionsElem.Node;
        }
    }


    /// <summary>
    /// Tree provider.
    /// </summary>
    public TreeProvider Tree
    {
        get
        {
            return versionsElem.TreeProvider;
        }
    }


    /// <summary>
    /// Version manager.
    /// </summary>
    public VersionManager VersionManager
    {
        get
        {
            return versionsElem.VersionManager;
        }
    }


    /// <summary>
    /// Workflow manager.
    /// </summary>
    public WorkflowManager WorkflowManager
    {
        get
        {
            return versionsElem.WorkflowManager;
        }
    }


    /// <summary>
    /// Returns workflow step information of current node.
    /// </summary>
    public WorkflowInfo WorkflowInfo
    {
        get
        {
            return mWorkflowInfo ?? (mWorkflowInfo = WorkflowManager.GetNodeWorkflow(Node));
        }
        set
        {
            mWorkflowInfo = value;
        }
    }


    /// <summary>
    /// Returns workflow step information of current node.
    /// </summary>
    public WorkflowStepInfo WorkflowStepInfo
    {
        get
        {
            return versionsElem.WorkflowStepInfo;
        }
    }

    #endregion


    #region "Page events"

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        if (!CMSContext.CurrentUser.IsAuthorizedPerUIElement("CMS.Content", "Properties.Versions"))
        {
            RedirectToCMSDeskUIElementAccessDenied("CMS.Content", "Properties.Versions");
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        // Register the scripts
        ScriptHelper.RegisterProgress(Page);

        UIContext.PropertyTab = PropertyTabEnum.Versions;

        // Register the dialog script
        ScriptHelper.RegisterDialogScript(this);

        versionsElem.InfoLabel = lblInfo;
        versionsElem.ErrorLabel = lblError;
        versionsElem.AfterDestroyHistory += versionsElem_AfterDestroyHistory;
        versionsElem.CombineWithDefaultCulture = false;

        // Redirect to page 'New culture version' in split mode. It must be before setting EditedDocument.
        if ((Node == null) && displaySplitMode)
        {
            URLHelper.Redirect("~/CMSModules/Content/CMSDesk/New/NewCultureVersion.aspx" + URLHelper.Url.Query);
        }
        // Set edited document
        EditedDocument = Node;

        if (Node != null)
        {
            // Check read permissions
            if (CMSContext.CurrentUser.IsAuthorizedPerDocument(Node, NodePermissionsEnum.Read) == AuthorizationResultEnum.Denied)
            {
                RedirectToAccessDenied(String.Format(GetString("cmsdesk.notauthorizedtoreaddocument"), Node.NodeAliasPath));
            }

            ReloadData();

            // Register js synchronization script for split mode
            if (displaySplitMode)
            {
                RegisterSplitModeSync(true, false);
            }
        }
        else
        {
            // Hide all if no node is specified
            pnlContent.Visible = false;
        }
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        lblInfo.Visible = !string.IsNullOrEmpty(lblInfo.Text);
        lblError.Visible = !string.IsNullOrEmpty(lblError.Text);
    }

    #endregion


    #region "Methods"

    /// <summary>
    /// Reloads the page data.
    /// </summary>
    private void ReloadData()
    {
        // If no workflow set for node, hide the data  
        if (WorkflowInfo == null)
        {
            lblInfo.Text = GetString("properties.scopenotset");
            DisableForm();
            pnlVersions.Visible = false;
        }
        else
        {
            string stepName = WorkflowStepInfo.StepName.ToLower();

            if ((stepName != "edit") && (stepName != "published") && (stepName != "archived") && !versionsElem.CanApprove)
            {
                lblApprove.Visible = true;
                lblApprove.Text = GetString("EditContent.NotAuthorizedToApprove") + "<br />";
            }
        }

        bool useCheckInCheckOut = false;
        if (WorkflowInfo != null)
        {
            useCheckInCheckOut = WorkflowInfo.UseCheckInCheckOut(CMSContext.CurrentSiteName);
        }

        // Check modify permissions
        if (!versionsElem.CanModify)
        {
            DisableForm();
            plcForm.Visible = false;
            lblInfo.Text = String.Format(GetString("cmsdesk.notauthorizedtoeditdocument"), Node.NodeAliasPath);
        }
        else if (useCheckInCheckOut || (versionsElem.CheckedOutByUserID != 0))
        {
            btnCheckout.Visible = false;
            btnCheckout.Enabled = true;
            btnCheckin.Visible = false;
            btnCheckin.Enabled = true;
            btnUndoCheckout.Visible = false;
            btnUndoCheckout.Enabled = true;
            txtComment.Enabled = true;
            txtVersion.Enabled = true;
            lblComment.Enabled = true;
            lblVersion.Enabled = true;

            // Check whether to check out or in
            if (WorkflowInfo == null)
            {
                btnCheckout.Visible = true;
                lblCheckInfo.Text = GetString("VersionsProperties.CheckOut");
                lblInfo.Text = GetString("properties.scopenotset");
                DisableForm();
            }
            else if (!Node.IsCheckedOut)
            {
                lblCheckInfo.Text = GetString("VersionsProperties.CheckOut");
                lblInfo.Text = GetString("VersionsProperties.InfoCheckedIn");
                DisableForm();
                btnCheckout.Visible = true;
                btnCheckout.Enabled = true;
            }
            else
            {
                // If checked out by current user, allow to check-in
                if (versionsElem.CheckedOutByUserID == CMSContext.CurrentUser.UserID)
                {
                    btnCheckin.Visible = true;
                    btnUndoCheckout.Visible = true;
                    lblCheckInfo.Text = GetString("VersionsProperties.CheckIn");
                    lblInfo.Text = GetString("VersionsProperties.InfoCheckedOut");
                }
                else
                {
                    // Else checked out by somebody else
                    btnCheckout.Visible = true;
                    btnCheckin.Visible = true;
                    btnCheckout.Visible = false;
                    lblCheckInfo.Text = GetString("VersionsProperties.CheckIn");

                    // Get checked out message
                    string userName = UserInfoProvider.GetUserNameById(versionsElem.CheckedOutByUserID);
                    lblInfo.Text = String.Format(GetString("editcontent.documentcheckedoutbyanother"), userName);

                    btnUndoCheckout.Visible = versionsElem.CanCheckIn;
                    btnUndoCheckout.Enabled = versionsElem.CanCheckIn;
                    btnCheckin.Enabled = versionsElem.CanCheckIn;
                    txtComment.Enabled = versionsElem.CanCheckIn;
                    txtVersion.Enabled = versionsElem.CanCheckIn;
                }
            }

            if (!versionsElem.CanApprove)
            {
                DisableForm();
            }
        }
        else
        {
            plcForm.Visible = false;
            if (WorkflowInfo != null)
            {
                lblInfo.Text = String.Empty;
            }
        }
    }


    /// <summary>
    /// Disables the editing form.
    /// </summary>
    private void DisableForm()
    {
        txtComment.Enabled = false;
        txtVersion.Enabled = false;

        btnCheckin.Enabled = false;
        btnCheckout.Enabled = false;
        btnUndoCheckout.Enabled = false;
    }


    /// <summary>
    /// Add javascript for refresh tree view.
    /// </summary>
    private void AddRefreshTreeScript()
    {
        if (Node != null)
        {
            AddScript(ScriptHelper.GetScript("RefreshTree(" + Node.NodeParentID + ", " + Node.NodeID + ");"));
        }
    }


    public override void AddScript(string script)
    {
        ScriptHelper.RegisterStartupScript(this, typeof(string), script.GetHashCode().ToString(), script);
    }

    #endregion


    #region "Button handling"

    protected void versionsElem_AfterDestroyHistory(object sender, EventArgs e)
    {
        AddRefreshTreeScript();
        ReloadData();
    }


    protected void btnCheckout_Click(object sender, EventArgs e)
    {
        try
        {
            VersionManager.EnsureVersion(Node, Node.IsPublished);

            // Check out the document
            VersionManager.CheckOut(Node);

            // Refresh tree if icon checked out should be displayed
            if (UIHelper.DisplayCheckedOutIcon(CMSContext.CurrentSiteName))
            {
                AddRefreshTreeScript();
            }

            ReloadData();
            versionsElem.ReloadData();
        }
        catch (WorkflowException)
        {
            lblError.Text += GetString("EditContent.DocumentCannotCheckOut");
        }
        catch (Exception ex)
        {
            // Log exception
            EventLogProvider ep = new EventLogProvider();
            ep.LogEvent("Content", "CHECKOUT", ex);
            lblError.Text += ex.Message;
        }
    }


    protected void btnCheckin_Click(object sender, EventArgs e)
    {
        try
        {
            // Check in the document        
            string version = null;
            if (txtVersion.Text.Trim() != string.Empty)
            {
                version = txtVersion.Text.Trim();
            }
            string comment = null;
            if (txtComment.Text.Trim() != string.Empty)
            {
                comment = txtComment.Text.Trim();
            }

            VersionManager.CheckIn(Node, version, comment);

            txtComment.Text = "";
            txtVersion.Text = "";

            // Refresh tree if icon checked out was displayed
            if (UIHelper.DisplayCheckedOutIcon(CMSContext.CurrentSiteName))
            {
                AddRefreshTreeScript();
            }

            ReloadData();
            versionsElem.ReloadData();
        }
        catch (WorkflowException)
        {
            lblError.Text += GetString("EditContent.DocumentCannotCheckIn");
        }
        catch (Exception ex)
        {
            // Log exception
            EventLogProvider ep = new EventLogProvider();
            ep.LogEvent("Content", "CHECKIN", ex);
            lblError.Text += ex.Message;
        }
    }


    protected void btnUndoCheckout_Click(object sender, EventArgs e)
    {
        try
        {
            // Undo check out
            VersionManager.UndoCheckOut(Node);

            txtComment.Text = "";
            txtVersion.Text = "";

            // Refresh tree if icon checked out was displayed
            if (UIHelper.DisplayCheckedOutIcon(CMSContext.CurrentSiteName))
            {
                AddRefreshTreeScript();
            }

            ReloadData();
            versionsElem.ReloadData();
        }
        catch (WorkflowException)
        {
            lblError.Text += GetString("EditContent.DocumentCannotCheckIn");
        }
        catch (Exception ex)
        {
            // Log exception
            EventLogProvider ep = new EventLogProvider();
            ep.LogEvent("Content", "UNDOCHECKOUT", ex);
            lblError.Text += ex.Message;
        }
    }

    #endregion
}

