using System;
using System.Web.UI;

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.SiteProvider;
using CMS.TreeEngine;
using CMS.UIControls;
using CMS.WorkflowEngine;

[RegisterTitle("content.ui.propertiesmetadata")]
public partial class CMSModules_Content_CMSDesk_Properties_MetaData : CMSPropertiesPage
{
    #region "Protected variables"

    protected int nodeId = 0;
    protected TreeNode node = null;
    protected TreeProvider mTreeProvider = null;
    private WorkflowManager mWorkflowManager = null;
    private WorkflowInfo wi = null;
    private VersionManager mVersionManager = null;
    protected bool hasModifyPermission = true;
    protected string siteName = null;

    #endregion


    #region "Properties"

    /// <summary>
    /// Indicates whether the asynchronous postback occurs on the page.
    /// </summary>
    private bool IsAsyncPostback
    {
        get
        {
            return ScriptManager.GetCurrent(Page).IsInAsyncPostBack;
        }
    }


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

        // Disable tag selectors
        tagGroupSelectorElem.StopProcessing = pnlUITags.IsHidden;
        tagSelectorElem.StopProcessing = pnlUITags.IsHidden;

        // Check UI element permission for content
        if (!CMSContext.CurrentUser.IsAuthorizedPerUIElement("CMS.Content", "Properties.MetaData"))
        {
            RedirectToCMSDeskUIElementAccessDenied("CMS.Content", "Properties.MetaData");
        }

        // Redirect to information page when no UI elements displayed
        if (pnlUIPage.IsHidden && pnlUITags.IsHidden)
        {
            RedirectToUINotAvailable();
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        UIContext.PropertyTab = PropertyTabEnum.Metadata;

        // Register the scripts
        ScriptHelper.RegisterProgress(Page);

        // Initialize tag group selector
        tagGroupSelectorElem.AddNoneItemsRecord = false;
        tagGroupSelectorElem.UseAutoPostback = true;
        tagGroupSelectorElem.UseGroupNameForSelection = false;
        tagSelectorElem.IsLiveSite = false;

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

        if (node != null)
        {
            // Get site info of current document
            SiteInfo si = SiteInfoProvider.GetSiteInfo(node.NodeSiteID);
            if (si != null)
            {
                siteName = si.SiteName;
            }
        }

        SetCheckBoxes();

        ltlScript.Text = "";

        // Get strings for labels
        lblPageDescription.Text = GetString("PageProperties.Decription");
        lblPageKeywords.Text = GetString("PageProperties.Keywords");
        lblPageTitle.Text = GetString("PageProperties.Title");
        lblTagSelector.Text = GetString("PageProperties.Tags");
        lblTagGroupSelector.Text = GetString("PageProperties.TagGroup");

        // Get string for checkboxes
        chkDescription.Text = GetString("Metadata.Inherit");
        chkKeyWords.Text = GetString("Metadata.Inherit");
        chkTitle.Text = GetString("Metadata.Inherit");
        chkTagGroupSelector.Text = GetString("Metadata.Inherit");

        // Get titles for panels
        pnlPageSettings.GroupingText = GetString("content.metadata.pagesettings");
        pnlTags.GroupingText = GetString("content.metadata.tags");

        btnOk.Text = GetString("general.ok");

        // Add handlers
        menuElem.OnBeforeCheckIn += menuElem_OnBeforeCheckIn;
        menuElem.OnBeforeApprove += menuElem_OnBeforeApprove;
        menuElem.OnBeforeReject += menuElem_OnBeforeReject;

        // Register js synchronization script for split mode
        if (displaySplitMode)
        {
            RegisterSplitModeSync(true, false);
        }
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        // If modify is allowed
        if (menuElem.AllowEdit && menuElem.AllowSave && hasModifyPermission)
        {
            // Enable form
            ScriptHelper.RegisterShortcuts(this);
            pnlForm.Enabled = true;
            tagSelectorElem.Enabled = true;
        }
        else
        {
            // Else disable form
            pnlForm.Enabled = false;
            tagSelectorElem.Enabled = false;
        }

        lblWorkflowInfo.Text = menuElem.WorkflowInfo;
        lblWorkflowInfo.Visible = (lblWorkflowInfo.Text != "");

        // Get the document with valid data if under the workflow
        if (RequestHelper.IsPostBack() && lblWorkflowInfo.Visible)
        {
            menuElem.ReloadMenu();
        }

        if (!IsAsyncPostback)
        {
            ReloadData();
        }

        if (!pnlUITags.IsHidden)
        {
            if (!tagGroupSelectorElem.UniSelector.HasData)
            {
                // Hide tag controls
                lblTagGroupSelector.Visible = false;
                tagGroupSelectorElem.Visible = false;
                chkTagGroupSelector.Visible = false;
                lblTagSelector.Visible = false;
                tagSelectorElem.Visible = false;

                // Show tag info
                lblTagInfo.Text = GetString("PageProperties.TagsInfo");
                lblTagInfo.Visible = true;
            }
            else
            {
                // Set the tag selector
                int tagGroup = ValidationHelper.GetInteger(tagGroupSelectorElem.Value, 0);
                if (tagGroup <= 0)
                {
                    tagSelectorElem.Enabled = false;
                    tagSelectorElem.Value = "";
                }
                else
                {
                    tagSelectorElem.SetValue("GroupID", tagGroup);
                }
            }
        }
    }

    #endregion


    #region "Protected methods"

    protected void SetCheckBoxes()
    {
        if (RequestHelper.IsPostBack())
        {
            return;
        }

        // Hide 'inherit' checkboxes for root document
        if ((node != null) && (node.NodeAliasPath == "/"))
        {
            chkDescription.Visible = false;
            chkKeyWords.Visible = false;
            chkTitle.Visible = false;
            chkTagGroupSelector.Visible = false;
        }
    }


    private void ReloadData()
    {
        if (node != null)
        {
            bool isRoot = (node.NodeAliasPath == "/");

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
                tagSelectorElem.Enabled = false;
                lblWorkflowInfo.Text = String.Format(GetString("cmsdesk.notauthorizedtoeditdocument"), node.NodeAliasPath);
            }

            TreeNode tmpNode = node.Clone();

            // If values are inherited set nulls
            tmpNode.SetValue("DocumentPageTitle", DBNull.Value);
            tmpNode.SetValue("DocumentPageKeyWords", DBNull.Value);
            tmpNode.SetValue("DocumentPageDescription", DBNull.Value);
            tmpNode.SetValue("DocumentTagGroupID", DBNull.Value);

            // Load the inherited values
            SiteInfo si = SiteInfoProvider.GetSiteInfo(node.NodeSiteID);
            if (si != null)
            {
                tmpNode.LoadInheritedValues(new string[] { "DocumentPageTitle", "DocumentPageKeyWords", "DocumentPageDescription", "DocumentTagGroupID" }, SiteInfoProvider.CombineWithDefaultCulture(si.SiteName));
            }

            if (!pnlUIPage.IsHidden)
            {
                // Page title
                if (node.GetValue("DocumentPageTitle") != null)
                {
                    txtTitle.Text = node.GetValue("DocumentPageTitle").ToString();
                }
                else
                {
                    if (!isRoot)
                    {
                        txtTitle.Enabled = false;
                        chkTitle.Checked = true;
                        txtTitle.Text = ValidationHelper.GetString(tmpNode.GetValue("DocumentPageTitle"), "");
                    }
                }

                // Page key words
                if (node.GetValue("DocumentPageKeyWords") != null)
                {
                    txtKeywords.Text = node.GetValue("DocumentPageKeyWords").ToString();
                }
                else
                {
                    if (!isRoot)
                    {
                        txtKeywords.Enabled = false;
                        chkKeyWords.Checked = true;
                        txtKeywords.Text = ValidationHelper.GetString(tmpNode.GetValue("DocumentPageKeyWords"), "");
                    }
                }

                // Page description
                if (node.GetValue("DocumentPageDescription") != null)
                {
                    txtDescription.Text = node.GetValue("DocumentPageDescription").ToString();
                }
                else
                {
                    if (!isRoot)
                    {
                        txtDescription.Enabled = false;
                        chkDescription.Checked = true;
                        txtDescription.Text = ValidationHelper.GetString(tmpNode.GetValue("DocumentPageDescription"), "");
                    }
                }
            }

            if (!pnlUITags.IsHidden)
            {
                // Tag group
                if (node.GetValue("DocumentTagGroupID") != null)
                {
                    object tagGroupId = node.GetValue("DocumentTagGroupID");
                    tagGroupSelectorElem.Value = tagGroupId;
                }
                else
                {
                    if (!isRoot)
                    {
                        // Get the inherited tag group
                        int tagGroup = ValidationHelper.GetInteger(tmpNode.GetValue("DocumentTagGroupID"), 0);
                        if (tagGroup > 0)
                        {
                            tagGroupSelectorElem.Value = tagGroup;
                        }
                        else
                        {
                            tagGroupSelectorElem.AddNoneItemsRecord = true;
                        }
                        tagGroupSelectorElem.Enabled = false;
                        chkTagGroupSelector.Checked = true;
                    }
                    else
                    {
                        // Add 'none' option to Root document
                        tagGroupSelectorElem.AddNoneItemsRecord = true;
                    }
                }

                // Tags
                tagSelectorElem.Value = node.DocumentTags;
            }
        }
    }

    #endregion


    #region "Control handling"

    void menuElem_OnBeforeCheckIn(object sender, EventArgs e)
    {
        SaveDocument();

        // Add Checkin info to label
        lblInfo.Text += "<br />" + GetString("ContentEdit.WasCheckedIn");
        lblInfo.Visible = true;
    }


    void menuElem_OnBeforeReject(object sender, EventArgs e)
    {
        SaveDocument();
    }


    void menuElem_OnBeforeApprove(object sender, EventArgs e)
    {
        SaveDocument();
    }


    /// <summary>
    /// Updates node properties in database.
    /// </summary>
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
                if (DocumentIsArchived(node) || DocumentIsPublished(node))
                {
                    ltlScript.Text += ScriptHelper.GetScript("if (window.RefreshTree) { RefreshTree(" + node.NodeParentID + ", " + node.NodeID + "); }");
                }

                // Check out
                VersionManager.CheckOut(node, node.IsPublished, true);
            }

            SaveDocument();

            // Update document
            if (!pnlUIPage.IsHidden || !pnlUITags.IsHidden)
            {
                // Check in the document
                if (AutoCheck)
                {
                    VersionManager.CheckIn(node, null, null);
                }
            }

            menuElem.Node = node;
            menuElem.ReloadMenu();

            lblInfo.Visible = true;
            lblInfo.Text = GetString("General.ChangesSaved");

            // Reload tags
            tagSelectorElem.Value = node.DocumentTags;
        }
    }


    private void SaveDocument()
    {
        // Update the data
        if (!pnlUIPage.IsHidden)
        {

            // Set Page title property
            node.SetValue("DocumentPageTitle", null);
            if (!chkTitle.Checked)
            {
                node.SetValue("DocumentPageTitle", txtTitle.Text);
            }

            // Set Page key words property
            node.SetValue("DocumentPageKeyWords", null);
            if (!chkKeyWords.Checked)
            {
                node.SetValue("DocumentPageKeyWords", txtKeywords.Text);
            }

            // Set Page description property
            node.SetValue("DocumentPageDescription", null);
            if (!chkDescription.Checked)
            {
                node.SetValue("DocumentPageDescription", txtDescription.Text);
            }
        }

        // Update the tags related data
        if (!pnlUITags.IsHidden)
        {
            node.SetValue("DocumentTagGroupID", null);
            if (!chkTagGroupSelector.Checked)
            {
                // If is tag group id
                node.SetValue("DocumentTagGroupID", (ValidationHelper.GetInteger(tagGroupSelectorElem.Value, 0) > 0) ? tagGroupSelectorElem.Value : DBNull.Value);
            }

            node.SetValue("DocumentTags", (ValidationHelper.GetInteger(tagGroupSelectorElem.Value, 0) > 0) ? tagSelectorElem.Value : DBNull.Value);
        }

        // Update document
        if (!pnlUIPage.IsHidden || !pnlUITags.IsHidden)
        {
            DocumentHelper.UpdateDocument(node, TreeProvider);
        }
    }


    protected void chkTitle_CheckedChanged(object sender, EventArgs e)
    {
        if (chkTitle.Checked)
        {
            // Value is inherited
            txtTitle.Enabled = false;
            if (!String.IsNullOrEmpty(siteName))
            {
                txtTitle.Text = ValidationHelper.GetString(node.GetInheritedValue("DocumentPageTitle", SiteInfoProvider.CombineWithDefaultCulture(siteName)), "");
            }
        }
        else
        {
            // Textbox is enabled
            txtTitle.Enabled = true;
        }
    }


    protected void chkDescription_CheckedChanged(object sender, EventArgs e)
    {
        if (chkDescription.Checked)
        {
            // Value is inherited
            txtDescription.Enabled = false;
            if (!String.IsNullOrEmpty(siteName))
            {
                txtDescription.Text = ValidationHelper.GetString(node.GetInheritedValue("DocumentPageDescription", SiteInfoProvider.CombineWithDefaultCulture(siteName)), "");
            }
        }
        else
        {
            // Textarea is enabled
            txtDescription.Enabled = true;
        }
    }


    protected void chkKeyWords_CheckedChanged(object sender, EventArgs e)
    {
        if (chkKeyWords.Checked)
        {
            // Value is inherited
            txtKeywords.Enabled = false;
            if (!String.IsNullOrEmpty(siteName))
            {
                txtKeywords.Text = ValidationHelper.GetString(node.GetInheritedValue("DocumentPageKeyWords", SiteInfoProvider.CombineWithDefaultCulture(siteName)), "");
            }
        }
        else
        {
            // Textbox is enabled
            txtKeywords.Enabled = true;
        }
    }


    protected void chkTagGroupSelector_CheckedChanged(object sender, EventArgs e)
    {
        if (chkTagGroupSelector.Checked)
        {
            // Value is inherited
            tagGroupSelectorElem.Enabled = false;

            if (!String.IsNullOrEmpty(siteName))
            {
                // Load parent value to tag group selector
                int value = ValidationHelper.GetInteger(node.GetInheritedValue("DocumentTagGroupID", SiteInfoProvider.CombineWithDefaultCulture(siteName)), 0);
                if (value == 0)
                {
                    tagGroupSelectorElem.AddNoneItemsRecord = true;
                }
                tagGroupSelectorElem.Value = value;
                tagGroupSelectorElem.ReloadData();
            }
        }
        else
        {
            tagGroupSelectorElem.Enabled = true;
        }
    }

    #endregion
}
