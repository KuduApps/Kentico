using System;

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.TreeEngine;
using CMS.UIControls;
using CMS.WorkflowEngine;

using TreeNode = CMS.TreeEngine.TreeNode;

[RegisterTitle("content.ui.propertiesmenu")]
public partial class CMSModules_Content_CMSDesk_Properties_Menu : CMSPropertiesPage
{
    #region "Variables"

    protected int nodeId = 0;
    protected bool hasModifyPermission = true;
    private TreeNode node = null;
    protected TreeProvider mTreeProvider = null;
    private WorkflowManager mWorkflowManager = null;
    private WorkflowInfo wi = null;
    private VersionManager mVersionManager = null;

    #endregion


    #region "Protected properties"

    /// <summary>
    /// Indicates if check-in/check-out functionality is automatic
    /// </summary>
    protected bool AutoCheck
    {
        get
        {
            if (node != null)
            {
                // Get workflow info
                wi = WorkflowManager.GetNodeWorkflow(node);

                // Check if the document uses workflow
                if (wi != null)
                {
                    return !wi.UseCheckInCheckOut(CMSContext.CurrentSiteName);
                }
            }
            return false;
        }
    }


    /// <summary>
    /// Gets Workflow manager instance.
    /// </summary>
    protected WorkflowManager WorkflowManager
    {
        get
        {
            return mWorkflowManager ?? (mWorkflowManager = new WorkflowManager(TreeProvider));
        }
    }


    /// <summary>
    /// Gets Version manager instance.
    /// </summary>
    protected VersionManager VersionManager
    {
        get
        {
            return mVersionManager ?? (mVersionManager = new VersionManager(TreeProvider));
        }
    }


    /// <summary>
    /// Tree provider instance.
    /// </summary>
    protected TreeProvider TreeProvider
    {
        get
        {
            return mTreeProvider ?? (mTreeProvider = new TreeProvider(CMSContext.CurrentUser));
        }
    }

    #endregion


    #region "Page events"

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        if (!CMSContext.CurrentUser.IsAuthorizedPerUIElement("CMS.Content", "Properties.Menu"))
        {
            RedirectToCMSDeskUIElementAccessDenied("CMS.Content", "Properties.Menu");
        }

        // Redirect to information page when no UI elements displayed
        if (pnlUIActions.IsHidden && pnlUIBasicProperties.IsHidden && pnlUIDesign.IsHidden)
        {
            RedirectToUINotAvailable();
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        // Register the scripts
        ScriptHelper.RegisterProgress(Page);

        UIContext.PropertyTab = PropertyTabEnum.Menu;

        // Get the document
        nodeId = QueryHelper.GetInteger("nodeid", 0);
        node = DocumentHelper.GetDocument(nodeId, CMSContext.PreferredCultureCode, TreeProvider);
        bool displaySplitMode = CMSContext.DisplaySplitMode;
        if ((node == null) && displaySplitMode)
        {
            URLHelper.Redirect("~/CMSModules/Content/CMSDesk/New/NewCultureVersion.aspx" + URLHelper.Url.Query);
        }
        menuElem.Node = node;
        // Set edited document
        EditedDocument = node;

        radInactive.Attributes.Add("onclick", "enableTextBoxes('inactive')");
        radStandard.Attributes.Add("onclick", "enableTextBoxes('')");
        radUrl.Attributes.Add("onclick", "enableTextBoxes('url')");
        radJavascript.Attributes.Add("onclick", "enableTextBoxes('java')");

        pnlBasicProperties.GroupingText = GetString("content.menu.basic");
        pnlActions.GroupingText = GetString("content.menu.actions");
        pnlDesign.GroupingText = GetString("content.menu.design");

        menuElem.OnBeforeCheckIn += menuElem_OnBeforeCheckIn;
        menuElem.OnBeforeApprove += menuElem_OnBeforeApprove;
        menuElem.OnBeforeReject += menuElem_OnBeforeReject;

        // Register js synchronization script for split mode
        if (displaySplitMode)
        {
            RegisterSplitModeSync(true, false);
        }
    }


    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (menuElem.AllowEdit && menuElem.AllowSave && hasModifyPermission)
        {
            ScriptHelper.RegisterShortcuts(this);
            pnlForm.Enabled = true;
        }
        else
        {
            pnlForm.Enabled = false;
        }

        lblWorkflowInfo.Text = menuElem.WorkflowInfo;
        lblWorkflowInfo.Visible = (lblWorkflowInfo.Text != string.Empty);

        // Reload data
        ReloadData();
    }

    #endregion


    #region "Private methods"

    /// <summary>
    /// Reload data.
    /// </summary>
    private void ReloadData()
    {
        if (node != null)
        {
            // Check read permissions
            if (CMSContext.CurrentUser.IsAuthorizedPerDocument(node, NodePermissionsEnum.Read) == AuthorizationResultEnum.Denied)
            {
                RedirectToAccessDenied(String.Format(GetString("cmsdesk.notauthorizedtoreaddocument"), node.NodeAliasPath));
            }
            // Check modify permissions
            else if (CMSContext.CurrentUser.IsAuthorizedPerDocument(node, NodePermissionsEnum.Modify) == AuthorizationResultEnum.Denied)
            {
                hasModifyPermission = false;
                pnlForm.Enabled = false;
                lblWorkflowInfo.Text = String.Format(GetString("cmsdesk.notauthorizedtoeditdocument"), node.NodeAliasPath);
            }

            txtMenuCaption.Text = node.DocumentMenuCaption;
            txtMenuItemStyle.Text = node.DocumentMenuStyle;
            txtMenuItemImage.Text = node.DocumentMenuItemImage;
            txtMenuItemLeftImage.Text = node.DocumentMenuItemLeftImage;
            txtMenuItemRightImage.Text = node.DocumentMenuItemRightImage;

            if (node.GetValue("DocumentMenuItemHideInNavigation") != null)
            {
                chkShowInNavigation.Checked = !(Convert.ToBoolean(node.GetValue("DocumentMenuItemHideInNavigation")));
            }
            else
            {
                chkShowInNavigation.Checked = false;
            }


            chkShowInSitemap.Checked = Convert.ToBoolean(node.GetValue("DocumentShowInSiteMap"));

            txtCssClass.Text = ValidationHelper.GetString(node.GetValue("DocumentMenuClass"), "");

            txtMenuItemStyleMouseOver.Text = ValidationHelper.GetString(node.GetValue("DocumentMenuStyleOver"), "");
            txtCssClassMouseOver.Text = ValidationHelper.GetString(node.GetValue("DocumentMenuClassOver"), "");
            txtMenuItemImageMouseOver.Text = ValidationHelper.GetString(node.GetValue("DocumentMenuItemImageOver"), "");
            txtMenuItemLeftImageMouseOver.Text = ValidationHelper.GetString(node.GetValue("DocumentMenuItemLeftImageOver"), "");
            txtMenuItemRightImageMouseOver.Text = ValidationHelper.GetString(node.GetValue("DocumentMenuItemRightImageOver"), "");

            txtMenuItemStyleHighlight.Text = ValidationHelper.GetString(node.GetValue("DocumentMenuStyleHighlighted"), "");
            txtCssClassHighlight.Text = ValidationHelper.GetString(node.GetValue("DocumentMenuClassHighlighted"), "");
            txtMenuItemImageHighlight.Text = ValidationHelper.GetString(node.GetValue("DocumentMenuItemImageHighlighted"), "");
            txtMenuItemLeftImageHighlight.Text = ValidationHelper.GetString(node.GetValue("DocumentMenuItemLeftImageHighlighted"), "");
            txtMenuItemRightImageHighlight.Text = ValidationHelper.GetString(node.GetValue("DocumentMenuItemRightImageHighlighted"), "");


            //Menu Action
            SetRadioActions(0);

            // Menu action priority low to high !
            if (ValidationHelper.GetString(node.GetValue("DocumentMenuJavascript"), "") != "")
            {
                txtJavaScript.Text = ValidationHelper.GetString(node.GetValue("DocumentMenuJavascript"), "");
                SetRadioActions(2);
            }

            if (ValidationHelper.GetString(node.GetValue("DocumentMenuRedirectUrl"), "") != "")
            {
                txtUrlInactive.Text = ValidationHelper.GetString(node.GetValue("DocumentMenuRedirectUrl"), "");
                txtUrl.Text = ValidationHelper.GetString(node.GetValue("DocumentMenuRedirectUrl"), "");
                SetRadioActions(3);
            }

            if (ValidationHelper.GetBoolean(node.GetValue("DocumentMenuItemInactive"), false))
            {
                SetRadioActions(1);
            }
        }
    }


    /// <summary>
    /// Sets radio buttons for menu action.
    /// </summary>
    private void SetRadioActions(int action)
    {
        radInactive.Checked = false;
        radStandard.Checked = false;
        radUrl.Checked = false;
        radJavascript.Checked = false;

        txtJavaScript.Enabled = false;
        txtUrl.Enabled = false;

        switch (action)
        {
            case 1:
                {
                    ltlScript.Text = ScriptHelper.GetScript("enableTextBoxes('inactive');");
                    radInactive.Checked = true;
                    break;
                }
            case 2:
                {
                    ltlScript.Text = ScriptHelper.GetScript("enableTextBoxes('java');");
                    radJavascript.Checked = true;
                    txtJavaScript.Enabled = true;
                    break;
                }
            case 3:
                {
                    ltlScript.Text = ScriptHelper.GetScript("enableTextBoxes('url');");
                    radUrl.Checked = true;
                    txtUrl.Enabled = true;
                    break;
                }
            default:
                {
                    ltlScript.Text = ScriptHelper.GetScript("enableTextBoxes('');");
                    radStandard.Checked = true;
                    break;
                }
        }
    }

    #endregion


    #region "Button handling"

    protected void menuElem_OnBeforeCheckIn(object sender, EventArgs e)
    {
        SaveDocument();
        lblInfo.Text += "<br />" + GetString("ContentEdit.WasCheckedIn");
        lblInfo.Visible = true;
    }


    protected void menuElem_OnBeforeReject(object sender, EventArgs e)
    {
        SaveDocument();
    }


    protected void menuElem_OnBeforeApprove(object sender, EventArgs e)
    {
        SaveDocument();
    }


    protected void btnOK_Click(object sender, EventArgs e)
    {
        if (node != null)
        {
            // Check modify permissions
            if (CMSContext.CurrentUser.IsAuthorizedPerDocument(node, NodePermissionsEnum.Modify) == AuthorizationResultEnum.Denied)
            {
                return;
            }

            // If not using check-in/check-out, check out automatically
            if (AutoCheck)
            {
                // Check out
                VersionManager.CheckOut(node, node.IsPublished, true);
            }

            SaveDocument();

            // Check in the document
            if (AutoCheck)
            {
                VersionManager.CheckIn(node, null, null);
            }

            menuElem.Node = node;
            menuElem.ReloadMenu();

            // Refresh tree
            string refreshMenuScript = "RefreshTree(" + node.NodeParentID + ", " + nodeId + ");";
            ScriptHelper.RegisterStartupScript(this, typeof(string), "refreshMenu", ScriptHelper.GetScript(refreshMenuScript));

            lblInfo.Text = GetString("General.ChangesSaved");
            lblInfo.Visible = true;
        }
    }


    private void SaveDocument()
    {
        // Update the data
        if (!pnlUIBasicProperties.IsHidden)
        {
            node.DocumentMenuCaption = txtMenuCaption.Text.Trim();
            node.SetValue("DocumentMenuItemHideInNavigation", !chkShowInNavigation.Checked);
            node.SetValue("DocumentShowInSiteMap", chkShowInSitemap.Checked);
        }

        if (!pnlUIDesign.IsHidden)
        {
            node.DocumentMenuItemImage = txtMenuItemImage.Text.Trim();
            node.DocumentMenuItemLeftImage = txtMenuItemLeftImage.Text.Trim();
            node.DocumentMenuItemRightImage = txtMenuItemRightImage.Text.Trim();
            node.DocumentMenuStyle = txtMenuItemStyle.Text.Trim();
            node.SetValue("DocumentMenuClass", txtCssClass.Text.Trim());

            node.SetValue("DocumentMenuStyleOver", txtMenuItemStyleMouseOver.Text.Trim());
            node.SetValue("DocumentMenuClassOver", txtCssClassMouseOver.Text.Trim());
            node.SetValue("DocumentMenuItemImageOver", txtMenuItemImageMouseOver.Text.Trim());
            node.SetValue("DocumentMenuItemLeftImageOver", txtMenuItemLeftImageMouseOver.Text.Trim());
            node.SetValue("DocumentMenuItemRightImageOver", txtMenuItemRightImageMouseOver.Text.Trim());

            node.SetValue("DocumentMenuStyleHighlighted", txtMenuItemStyleHighlight.Text.Trim());
            node.SetValue("DocumentMenuClassHighlighted", txtCssClassHighlight.Text.Trim());
            node.SetValue("DocumentMenuItemImageHighlighted", txtMenuItemImageHighlight.Text.Trim());
            node.SetValue("DocumentMenuItemLeftImageHighlighted", txtMenuItemLeftImageHighlight.Text.Trim());
            node.SetValue("DocumentMenuItemRightImageHighlighted", txtMenuItemRightImageHighlight.Text.Trim());
        }

        if (!pnlUIActions.IsHidden)
        {
            // Menu action
            txtJavaScript.Enabled = false;
            txtUrl.Enabled = false;

            if (radStandard.Checked)
            {
                if (node != null)
                {
                    node.SetValue("DocumentMenuRedirectUrl", "");
                    node.SetValue("DocumentMenuJavascript", "");
                    node.SetValue("DocumentMenuItemInactive", false);
                }
            }

            if (radInactive.Checked)
            {
                ltlScript.Text = ScriptHelper.GetScript("enableTextBoxes('inactive');");
                if (node != null)
                {
                    node.SetValue("DocumentMenuRedirectUrl", txtUrlInactive.Text);
                    node.SetValue("DocumentMenuJavascript", txtJavaScript.Text);
                    node.SetValue("DocumentMenuItemInactive", true);
                }
            }

            if (radJavascript.Checked)
            {
                txtJavaScript.Enabled = true;
                txtUrl.Enabled = false;
                if (node != null)
                {
                    node.SetValue("DocumentMenuRedirectUrl", "");
                    node.SetValue("DocumentMenuJavascript", txtJavaScript.Text);
                    node.SetValue("DocumentMenuItemInactive", false);
                }
            }

            if (radUrl.Checked)
            {
                txtJavaScript.Enabled = false;
                txtUrl.Enabled = true;
                if (node != null)
                {
                    node.SetValue("DocumentMenuRedirectUrl", txtUrl.Text);
                    node.SetValue("DocumentMenuJavascript", "");
                    node.SetValue("DocumentMenuItemInactive", false);
                }
            }
        }

        DocumentHelper.UpdateDocument(node, TreeProvider);
    }

    #endregion
}