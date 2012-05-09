using System;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.UIControls;
using CMS.TreeEngine;
using CMS.GlobalHelper;
using CMS.ExtendedControls;
using CMS.CMSHelper;
using CMS.WorkflowEngine;
using CMS.Synchronization;
using CMS.SiteProvider;
using CMS.SettingsProvider;

using TreeNode = CMS.TreeEngine.TreeNode;

public partial class CMSModules_Content_Controls_Relationships_AddRelatedDocument : CMSUserControl
{
    #region "Protected variables"

    protected int currentNodeId = 0;
    protected TreeNode node = null;
    protected DialogConfiguration mConfig = null;
    protected bool mEnabled = true;
    protected bool mShowButtons = true;
    protected bool mAllowSwitchSides = true;
    protected RelationshipNameInfo relationshipNameInfo = null;

    #endregion


    #region "Private properties"

    /// <summary>
    /// Gets the configuration for dialog.
    /// </summary>
    private DialogConfiguration Config
    {
        get
        {
            if (mConfig == null)
            {
                mConfig = new DialogConfiguration();
                mConfig.HideLibraries = true;
                mConfig.ContentSelectedSite = CMSContext.CurrentSiteName;
                mConfig.HideAnchor = true;
                mConfig.HideAttachments = true;
                mConfig.HideContent = false;
                mConfig.HideEmail = true;
                mConfig.HideLibraries = true;
                mConfig.HideWeb = true;
                mConfig.ContentSelectedSite = CMSContext.CurrentSiteName;
                mConfig.OutputFormat = OutputFormatEnum.Custom;
                mConfig.CustomFormatCode = "relationship";
                mConfig.SelectableContent = SelectableContentEnum.AllContent;
            }
            return mConfig;
        }
    }

    #endregion


    #region "Public properties"

    /// <summary>
    /// Indicates if allow switch sides.
    /// </summary>
    public bool AllowSwitchSides
    {
        get
        {
            return mAllowSwitchSides;
        }
        set
        {
            mAllowSwitchSides = value;
        }
    }


    /// <summary>
    /// Default side (False - left, True - right).
    /// </summary>
    public bool DefaultSide
    {
        get;
        set;
    }


    /// <summary>
    /// Gets or sets relationship name.
    /// </summary>
    public string RelationshipName
    {
        get;
        set;
    }


    /// <summary>
    /// Gets or sets the document.
    /// </summary>
    public TreeNode TreeNode
    {
        get;
        set;
    }


    /// <summary>
    /// Enables or disables controls.
    /// </summary>
    public bool Enabled
    {
        get
        {
            return mEnabled;
        }
        set
        {
            mEnabled = value;

            btnLeftNode.Enabled = mEnabled;
            btnRightNode.Enabled = mEnabled;
            btnOk.Enabled = mEnabled;
            btnSwitchSides.Enabled = mEnabled;
            relNameSelector.Enabled = mEnabled;
            txtLeftNode.Enabled = mEnabled;
            txtRightNode.Enabled = mEnabled;
        }
    }


    /// <summary>
    /// Indicates if show buttons (OK, Close).
    /// </summary>
    public bool ShowButtons
    {
        get
        {
            return mShowButtons;
        }
        set
        {
            mShowButtons = value;
        }
    }

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Register the dialog script
        ScriptHelper.RegisterDialogScript(Page);

        DefaultSide = QueryHelper.GetBoolean("defaultside", DefaultSide);
        AllowSwitchSides = QueryHelper.GetBoolean("allowswitchsides", AllowSwitchSides);
        RelationshipName = QueryHelper.GetString("relationshipname", RelationshipName);

        relNameSelector.IsLiveSite = false;
        btnSwitchSides.Visible = AllowSwitchSides;
        btnOk.Visible = ShowButtons;

        // Initialize dialog scripts
        Config.EditorClientID = txtLeftNode.ClientID + ";" + hdnSelectedNodeId.ClientID;
        string url = CMSDialogHelper.GetDialogUrl(Config, IsLiveSite, false, null, false);
        btnLeftNode.OnClientClick = "modalDialog('" + url + "', 'contentselectnode', '90%', '85%'); return false;";

        Config.EditorClientID = txtRightNode.ClientID + ";" + hdnSelectedNodeId.ClientID;
        url = CMSDialogHelper.GetDialogUrl(Config, IsLiveSite, false, null, false);
        btnRightNode.OnClientClick = "modalDialog('" + url + "', 'contentselectnode', '90%', '85%'); return false;";

        leftCell.Text = GetString("Relationship.leftSideDoc");
        middleCell.Text = GetString("Relationship.RelationshipName");
        rightCell.Text = GetString("Relationship.rightSideDoc");

        if (TreeNode != null)
        {
            currentNodeId = TreeNode.NodeID;

            // Check modify permissions
            if (CMSContext.CurrentUser.IsAuthorizedPerDocument(TreeNode, NodePermissionsEnum.Modify) == AuthorizationResultEnum.Denied)
            {
                Enabled = false;
                lblInfo.Visible = true;
                lblInfo.Text = String.Format(GetString("cmsdesk.notauthorizedtoeditdocument"), TreeNode.NodeAliasPath);
            }

            string nodeDocumentName = TreeNode.DocumentName;
            lblRightNode.Text = lblLeftNode.Text = (string.IsNullOrEmpty(nodeDocumentName)) ? "/" : HTMLHelper.HTMLEncode(nodeDocumentName);
        }
        else
        {
            Enabled = false;
        }

        // All relationship names for current site
        if (string.IsNullOrEmpty(RelationshipName))
        {
            relNameSelector.Visible = true;
            lblRelName.Visible = false;
        }
        else
        {
            relationshipNameInfo = RelationshipNameInfoProvider.GetRelationshipNameInfo(RelationshipName);
            if (relationshipNameInfo != null)
            {
                lblRelName.Text = relationshipNameInfo.RelationshipDisplayName;
            }

            relNameSelector.Visible = false;
            lblRelName.Visible = true;
        }

        // Register switching js
        if (btnSwitchSides.Enabled)
        {
            RegisterScript();
        }

        if (!RequestHelper.IsPostBack())
        {
            hdnCurrentOnLeft.Value = !DefaultSide ? "true" : "false";
        }

        bool isLeftSide = ValidationHelper.GetBoolean(hdnCurrentOnLeft.Value, false);

        // Left side
        if (isLeftSide)
        {
            pnlLeftCurrentNode.Style.Add("display", "block");
            pnlLeftSelectedNode.Style.Add("display", "none");
            pnlLeftSelectButton.Style.Add("display", "none");
            pnlRightCurrentNode.Style.Add("display", "none");
            pnlRightSelectedNode.Style.Add("display", "block");
            pnlRightSelectButton.Style.Add("display", "block");
        }
        // Right side
        else
        {
            pnlLeftCurrentNode.Style.Add("display", "none");
            pnlLeftSelectedNode.Style.Add("display", "block");
            pnlLeftSelectButton.Style.Add("display", "block");
            pnlRightCurrentNode.Style.Add("display", "block");
            pnlRightSelectedNode.Style.Add("display", "none");
            pnlRightSelectButton.Style.Add("display", "none");
        }
    }


    /// <summary>
    /// Handles OK button event.
    /// </summary>
    /// <param name="sender">Sender</param>
    /// <param name="e">Argument</param>
    protected void btnOk_Click(object sender, EventArgs e)
    {
        if (SaveRelationship())
        {
            URLHelper.Redirect("~/CMSModules/Content/CMSDesk/Properties/Relateddocs_List.aspx?nodeid=" + currentNodeId + "&inserted=1");
        }
    }


    /// <summary>
    /// Saves relationship.
    /// </summary>
    /// <returns>True, if relatioship was successfully saved.</returns>
    public bool SaveRelationship()
    {
        bool saved = false;

        // Check modify permissions
        if (CMSContext.CurrentUser.IsAuthorizedPerDocument(TreeNode, NodePermissionsEnum.Modify) == AuthorizationResultEnum.Denied)
        {
            return saved;
        }

        bool currentNodeIsOnLeftSide = ValidationHelper.GetBoolean(Request.Params[hdnCurrentOnLeft.UniqueID], false);
        int selectedNodeId = ValidationHelper.GetInteger(hdnSelectedNodeId.Value, 0);

        // Try to get by path if not selected
        if (selectedNodeId <= 0)
        {
            string aliaspath = currentNodeIsOnLeftSide ? txtRightNode.Text.Trim() : txtLeftNode.Text.Trim();

            if (aliaspath != string.Empty)
            {
                TreeProvider tree = new TreeProvider(CMSContext.CurrentUser);
                node = tree.SelectSingleNode(CMSContext.CurrentSiteName, aliaspath, TreeProvider.ALL_CULTURES);
                if (node != null)
                {
                    selectedNodeId = node.NodeID;
                }
                else
                {
                    lblError.Visible = true;
                    lblError.Text = GetString("relationship.selectcorrectrelateddoc");
                }
            }
            else
            {
                lblError.Visible = true;
                lblError.Text = GetString("relationship.selectrelateddoc");
            }
        }

        int selectedValue = 0;
        // Only one reletionship name in textbox
        if ((relationshipNameInfo != null) && (lblRelName.Visible))
        {
            selectedValue = relationshipNameInfo.RelationshipNameId;
        }
        // Value from relationship name selector
        else if (relNameSelector.Visible)
        {
            selectedValue = ValidationHelper.GetInteger(relNameSelector.Value, 0);
        }

        if ((currentNodeId > 0) && (selectedNodeId > 0) && (selectedValue > 0))
        {
            int relationshipNameId = selectedValue;

            try
            {
                // Left side
                if (currentNodeIsOnLeftSide)
                {
                    RelationshipProvider.AddRelationship(currentNodeId, selectedNodeId, relationshipNameId);
                }
                // Right side
                else
                {
                    RelationshipProvider.AddRelationship(selectedNodeId, currentNodeId, relationshipNameId);
                }

                string aliasPath = (node == null) ? TreePathUtils.GetAliasPathByNodeId(currentNodeId) : node.NodeAliasPath;

                // Log synchronization
                DocumentSynchronizationHelper.LogDocumentChange(CMSContext.CurrentSiteName, aliasPath, TaskTypeEnum.UpdateDocument, null);

                saved = true;

                lblInfo.Text = GetString("general.changessaved");
                lblInfo.Visible = true;
            }
            catch (Exception ex)
            {
                lblError.Visible = true;
                lblError.Text = ex.Message;
            }
        }

        return saved;
    }

    /// <summary>
    /// Registers javascript functions.
    /// </summary>
    private void RegisterScript()
    {
        string leftCurentNodeClientID = pnlLeftCurrentNode.ClientID;
        string leftSelectedNodeClientID = pnlLeftSelectedNode.ClientID;
        string rightCurrentNodeClientID = pnlRightCurrentNode.ClientID;
        string rightSelectedNodeClientID = pnlRightSelectedNode.ClientID;
        string leftSelectButtonClientID = pnlLeftSelectButton.ClientID;
        string rigthSelectButtonClientID = pnlRightSelectButton.ClientID;
        string txtRightNodeClientID = txtRightNode.ClientID;
        string txtLeftNodeClientID = txtLeftNode.ClientID;
        string currentOnLeftClienID = hdnCurrentOnLeft.ClientID;

        StringBuilder sb = new StringBuilder();
        sb.AppendLine("function SwitchSides() {");
        sb.AppendLine("  var leftSide = document.getElementById('" + currentOnLeftClienID + "').value;");
        sb.AppendLine("  if (leftSide == 'true') {");
        sb.AppendLine("    document.getElementById('" + leftCurentNodeClientID + "').style.display = 'none';");
        sb.AppendLine("    document.getElementById('" + leftSelectedNodeClientID + "').style.display = 'block';");
        sb.AppendLine("    document.getElementById('" + leftSelectButtonClientID + "').style.display = 'block';");
        sb.AppendLine("    document.getElementById('" + rightCurrentNodeClientID + "').style.display = 'block';");
        sb.AppendLine("    document.getElementById('" + rightSelectedNodeClientID + "').style.display = 'none';");
        sb.AppendLine("    document.getElementById('" + rigthSelectButtonClientID + "').style.display = 'none';");
        sb.AppendLine("    document.getElementById('" + currentOnLeftClienID + "').value = 'false';");
        sb.AppendLine("    document.getElementById('" + txtLeftNodeClientID + "').value = document.getElementById('" + txtRightNodeClientID + "').value;");
        sb.AppendLine("  }");
        sb.AppendLine("  else if (leftSide == 'false') {");
        sb.AppendLine("    document.getElementById('" + leftCurentNodeClientID + "').style.display = 'block';");
        sb.AppendLine("    document.getElementById('" + leftSelectedNodeClientID + "').style.display = 'none';");
        sb.AppendLine("    document.getElementById('" + leftSelectButtonClientID + "').style.display = 'none';");
        sb.AppendLine("    document.getElementById('" + rightCurrentNodeClientID + "').style.display = 'none';");
        sb.AppendLine("    document.getElementById('" + rightSelectedNodeClientID + "').style.display = 'block';");
        sb.AppendLine("    document.getElementById('" + rigthSelectButtonClientID + "').style.display = 'block';");
        sb.AppendLine("    document.getElementById('" + currentOnLeftClienID + "').value = 'true';");
        sb.AppendLine("    document.getElementById('" + txtRightNodeClientID + "').value = document.getElementById('" + txtLeftNodeClientID + "').value;");
        sb.AppendLine("  }");
        sb.AppendLine("  return false;");
        sb.AppendLine("}");

        // Register script
        ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "SwitchSides", ScriptHelper.GetScript(sb.ToString()));
    }

    #endregion
}