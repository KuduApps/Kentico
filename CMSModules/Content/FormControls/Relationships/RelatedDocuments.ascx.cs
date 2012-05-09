using System;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.FormControls;
using CMS.GlobalHelper;
using CMS.TreeEngine;
using CMS.CMSHelper;
using CMS.WorkflowEngine;
using CMS.Synchronization;
using CMS.ExtendedControls;
using CMS.FormEngine;
using CMS.SettingsProvider;
using CMS.SiteProvider;

using TreeNode = CMS.TreeEngine.TreeNode;

public partial class CMSModules_Content_FormControls_Relationships_RelatedDocuments : FormEngineUserControl
{
    #region "Variables"

    private TreeProvider mTreeProvider = null;
    private bool mShowAddRelation = true;
    private string mPageSize = "5,10,25,50,100,##ALL##";
    private int mDefaultPageSize = 5;
    private bool mAllowSwitchSides = true;
    private DialogConfiguration mConfig = null;

    #endregion


    #region "Private properties"

    /// <summary>
    /// Gets the configuration for Copy and Move dialog.
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
                mConfig.ContentSites = AvailableSitesEnum.OnlyCurrentSite;
            }
            return mConfig;
        }
    }

    #endregion


    #region "Properties"

    /// <summary>
    /// Gets or sets relationship name.
    /// </summary>
    public string RelationshipName
    {
        get;
        set;
    }


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
    /// Defalut side (False - left, True - right).
    /// </summary>
    public bool DefaultSide
    {
        get;
        set;
    }


    /// <summary>
    /// Default page size.
    /// </summary>
    public int DefaultPageSize
    {
        get
        {
            return mDefaultPageSize;
        }
        set
        {
            mDefaultPageSize = value;
        }
    }


    /// <summary>
    /// Page size values separated with comma.
    /// </summary>
    public string PageSize
    {
        get
        {
            return mPageSize;
        }
        set
        {
            mPageSize = value;
        }
    }


    /// <summary>
    /// Indicates id show link 'Add relation'.
    /// </summary>
    public bool ShowAddRelation
    {
        get
        {
            return mShowAddRelation;
        }
        set
        {
            mShowAddRelation = value;
        }
    }


    /// <summary>
    /// Gets or sets the document;.
    /// </summary>
    public TreeNode TreeNode
    {
        get;
        set;
    }


    /// <summary>
    /// Gets tree provider.
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
    }


    /// <summary>
    /// Enables or disables control.
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
            UniGridRelationship.GridView.Enabled = value;
            lnkNewRelationship.Enabled = value;
        }
    }

    #endregion


    #region "Methods"

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        // Paging
        UniGridRelationship.PageSize = ValidationHelper.GetString(GetValue("PageSize"), PageSize);
        UniGridRelationship.Pager.DefaultPageSize = ValidationHelper.GetInteger(GetValue("DefaultPageSize"), DefaultPageSize);
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        if (StopProcessing)
        {
            UniGridRelationship.StopProcessing = StopProcessing;
        }
        else
        {
            // Set tree node from Form object
            if ((TreeNode == null) && (Form != null) && (Form.EditedObject != null))
            {
                TreeNode node = Form.EditedObject as TreeNode;
                if ((node != null) && (Form.Mode == FormModeEnum.Update))
                {
                    TreeNode = node;
                }
                else
                {
                    lblError.Text = GetString("relationship.editdocumenterror");
                }
            }

            if (TreeNode != null)
            {
                // Settings
                RelationshipName = ValidationHelper.GetString(GetValue("RelationshipName"), RelationshipName);
                AllowSwitchSides = ValidationHelper.GetBoolean(GetValue("AllowSwitchSides"), AllowSwitchSides);
                DefaultSide = ValidationHelper.GetBoolean(GetValue("DefaultSide"), DefaultSide);

                // Set unigrid
                UniGridRelationship.Columns = "LeftNodeID, RightNodeID, RelationshipNameID, LeftNodeName, RightNodeName, RelationshipDisplayName";
                UniGridRelationship.OnExternalDataBound += UniGridRelationship_OnExternalDataBound;
                UniGridRelationship.OnBeforeDataReload += UniGridRelationship_OnBeforeDataReload;
                UniGridRelationship.OnAction += UniGridRelationship_OnAction;
                UniGridRelationship.ZeroRowsText = GetString("general.nodatafound");

                int nodeId = TreeNode.NodeID;
                bool oneRelationshipName = !string.IsNullOrEmpty(RelationshipName);
                string where = null;
                if (oneRelationshipName)
                {
                    where = SqlHelperClass.AddWhereCondition(where, "RelationshipName = N'" + SqlHelperClass.GetSafeQueryString(RelationshipName, false) + "'");
                }

                // Switch sides is disabled
                if (!AllowSwitchSides)
                {
                    if (DefaultSide)
                    {
                        where = SqlHelperClass.AddWhereCondition(where, "RightNodeID = " + nodeId);
                    }
                    else
                    {
                        where = SqlHelperClass.AddWhereCondition(where, "LeftNodeID = " + nodeId);
                    }
                }
                else
                {
                    where = SqlHelperClass.AddWhereCondition(where, "(LeftNodeID = " + nodeId + ") OR (RightNodeID = " + nodeId + ")");
                }

                UniGridRelationship.WhereCondition = where;

                if (ShowAddRelation)
                {
                    string postbackArgument = null;
                    if (!AllowSwitchSides && !string.IsNullOrEmpty(RelationshipName))
                    {
                        postbackArgument = "insertfromselectdocument";

                        // Register javascript 'postback' function
                        string script = "function RefreshRelatedPanel(elementId) { if (elementId != null) { __doPostBack(elementId, '" + postbackArgument + "'); } } \n";
                        ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "RefreshRelatedPanel", ScriptHelper.GetScript(script));

                        // Dialog 'Select document'
                        Config.EditorClientID = pnlUpdate.ClientID + ";" + hdnSelectedNodeId.ClientID;
                        string url = CMSDialogHelper.GetDialogUrl(Config, IsLiveSite, false, null, false);
                        lnkNewRelationship.Style.Add("cursor", "pointer");
                        lnkNewRelationship.Style.Add("text-decoration", "underline");
                        lnkNewRelationship.Attributes.Add("onclick", "modalDialog('" + url + "', 'contentselectnode', '90%', '85%');");
                    }
                    else
                    {
                        postbackArgument = "insert";

                        // Register javascript 'postback' function
                        ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "RefreshUpdatePanel_" + ClientID, ScriptHelper.GetScript(
                            "function RefreshUpdatePanel_" + ClientID + "(){ " + Page.ClientScript.GetPostBackEventReference(pnlUpdate, postbackArgument) + "; } \n"));

                        // Dialog 'Add related document'
                        string query = "?nodeid=" + nodeId.ToString();
                        query = URLHelper.AddUrlParameter(query, "defaultside", DefaultSide.ToString());
                        query = URLHelper.AddUrlParameter(query, "allowswitchsides", AllowSwitchSides.ToString());
                        query = URLHelper.AddUrlParameter(query, "relationshipname", RelationshipName);
                        query = URLHelper.AddUrlParameter(query, "externalControlID", ClientID);
                        query = URLHelper.AddUrlParameter(query, "hash", QueryHelper.GetHash(query));

                        string url = null;
                        if (IsLiveSite)
                        {
                            url = ResolveUrl("~/CMSFormControls/LiveSelectors/RelatedDocuments.aspx" + query);
                        }
                        else
                        {
                            url = ResolveUrl("~/CMSFormControls/Selectors/RelatedDocuments.aspx" + query);
                        }

                        // Initialize controls
                        lnkNewRelationship.NavigateUrl = url;
                        lnkNewRelationship.Attributes.Add("onclick", "modalDialog('" + url + "', 'AddRelatedDocument', '900', '300'); return false;");
                    }


                    imgNewRelationship.ImageUrl = GetImageUrl("CMSModules/CMS_Content/Properties/addrelationship.png");
                    imgNewRelationship.DisabledImageUrl = GetImageUrl("CMSModules/CMS_Content/Properties/addrelationshipdisabled.png");
                }
                else
                {
                    pnlNewLink.Visible = false;
                }
            }
            else
            {
                UniGridRelationship.StopProcessing = true;
                UniGridRelationship.Visible = false;
                pnlNewLink.Visible = false;
            }

            if (RequestHelper.IsPostBack())
            {
                string target = Request["__EVENTTARGET"];
                if ((target == pnlUpdate.ClientID) || (target == pnlUpdate.UniqueID))
                {
                    string action = Request["__EVENTARGUMENT"];

                    if (!string.IsNullOrEmpty(action))
                    {
                        switch (action.ToLower())
                        {
                            // Insert from 'Add related document' dialog
                            case "insert":
                                lblInfo.Text = GetString("relationship.wasadded");
                                break;

                            // Insert from 'Select document' dialog
                            case "insertfromselectdocument":
                                SaveRelationship();
                                break;

                            // Nothing
                            default:
                                break;
                        }
                    }
                }
            }

            bool inserted = QueryHelper.GetBoolean("inserted", false);
            if (inserted)
            {
                lblInfo.Text = GetString("relationship.wasadded");
            }
        }
    }


    /// <summary>
    /// Perfoms actions before reload grid.
    /// </summary>
    private void UniGridRelationship_OnBeforeDataReload()
    {
        DataControlField rightColumn = UniGridRelationship.NamedColumns["RightNodeName"];

        // Hide columns
        if (!string.IsNullOrEmpty(RelationshipName) && !AllowSwitchSides)
        {
            string headerText = GetString("relationship.relateddocument");
            DataControlField leftColumn = UniGridRelationship.NamedColumns["LeftNodeName"];
            DataControlField relationshipNameColumn = UniGridRelationship.NamedColumns["RelationshipDisplayName"];

            if (DefaultSide)
            {
                leftColumn.HeaderText = headerText;
                leftColumn.HeaderStyle.Width = new Unit("100%");
                rightColumn.Visible = false;
            }
            else
            {
                rightColumn.HeaderText = headerText;
                rightColumn.HeaderStyle.Width = new Unit("100%");
                leftColumn.Visible = false;
            }

            // Hide relatiosnhip name column
            relationshipNameColumn.Visible = false;
        }
        else
        {
            rightColumn.HeaderStyle.Width = new Unit("100%");
        }
    }


    /// <summary>
    /// Fires on the grid action.
    /// </summary>
    /// <param name="actionName">Action name</param>
    /// <param name="actionArgument">Action argument</param>
    private void UniGridRelationship_OnAction(string actionName, object actionArgument)
    {
        // Check modify permissions
        if (CMSContext.CurrentUser.IsAuthorizedPerDocument(TreeNode, NodePermissionsEnum.Modify) == AuthorizationResultEnum.Denied)
        {
            return;
        }

        if (actionName == "delete")
        {
            string[] parameters = ((string)actionArgument).Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            if (parameters.Length == 3)
            {
                // Parse parameters
                int leftNodeId = ValidationHelper.GetInteger(parameters[0], 0);
                int rightNodeId = ValidationHelper.GetInteger(parameters[1], 0);
                int relationshipNameId = ValidationHelper.GetInteger(parameters[2], 0);

                // If parameters are valid
                if ((leftNodeId > 0) && (rightNodeId > 0) && (relationshipNameId > 0))
                {
                    // Remove relationship
                    RelationshipProvider.RemoveRelationship(leftNodeId, rightNodeId, relationshipNameId);

                    // Log synchronization
                    DocumentSynchronizationHelper.LogDocumentChange(CMSContext.CurrentSiteName, TreeNode.NodeAliasPath, TaskTypeEnum.UpdateDocument, TreeProvider);

                    lblInfo.Text = GetString("relationship.wasdeleted");
                }
            }
        }
    }


    /// <summary>
    /// Binds the grid columns.
    /// </summary>
    /// <param name="sender">Sender</param>
    /// <param name="sourceName">Source name</param>
    /// <param name="parameter">Paremeter</param>
    private object UniGridRelationship_OnExternalDataBound(object sender, string sourceName, object parameter)
    {
        switch (sourceName.ToLower())
        {
            case "leftnodename":
            case "rightnodename":
                return HTMLHelper.HTMLEncode(DataHelper.GetNotEmpty(parameter, "/"));

            case "delete":
                ImageButton btn = ((ImageButton)sender);
                btn.PreRender += imgDelete_PreRender;
                break;
        }

        return parameter;
    }


    protected void imgDelete_PreRender(object sender, EventArgs e)
    {
        ImageButton imgDelete = (ImageButton)sender;
        if (!Enabled)
        {
            // Disable delete icon in case that editing is not allowed
            imgDelete.ImageUrl = GetImageUrl("Design/Controls/UniGrid/Actions/deletedisabled.png");
            imgDelete.Enabled = false;
            imgDelete.Style.Add("cursor", "default");
        }
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        lblInfo.Visible = !string.IsNullOrEmpty(lblInfo.Text);
        lblError.Visible = !string.IsNullOrEmpty(lblError.Text);

        if (!Enabled)
        {
            lnkNewRelationship.NavigateUrl = null;
            if (lnkNewRelationship.Attributes["onclick"] != null)
            {
                lnkNewRelationship.Attributes["onclick"] = null;
            }
        }
    }


    /// <summary>
    /// Saves relationship.
    /// </summary>
    public void SaveRelationship()
    {
        if (TreeNode != null)
        {
            // Check modify permissions
            if (CMSContext.CurrentUser.IsAuthorizedPerDocument(TreeNode, NodePermissionsEnum.Modify) == AuthorizationResultEnum.Denied)
            {
                return;
            }

            bool currentNodeIsOnLeftSide = !DefaultSide;
            // Selected node Id
            int selectedNodeId = ValidationHelper.GetInteger(hdnSelectedNodeId.Value, 0);

            // Get relatioshipname
            RelationshipNameInfo relationshipNameInfo = RelationshipNameInfoProvider.GetRelationshipNameInfo(RelationshipName);

            int relationshipNameId = 0;
            if (relationshipNameInfo != null)
            {
                relationshipNameId = relationshipNameInfo.RelationshipNameId;
            }

            if ((selectedNodeId > 0) && (relationshipNameId > 0))
            {
                try
                {
                    // Left side
                    if (currentNodeIsOnLeftSide)
                    {
                        RelationshipProvider.AddRelationship(TreeNode.NodeID, selectedNodeId, relationshipNameId);
                    }
                    // Right side
                    else
                    {
                        RelationshipProvider.AddRelationship(selectedNodeId, TreeNode.NodeID, relationshipNameId);
                    }

                    // Log synchronization
                    DocumentSynchronizationHelper.LogDocumentChange(CMSContext.CurrentSiteName, TreeNode.NodeAliasPath, TaskTypeEnum.UpdateDocument, TreeProvider);

                    lblInfo.Text = GetString("relationship.wasadded");
                }
                catch (Exception ex)
                {
                    lblError.Visible = true;
                    lblError.Text = ex.Message;
                }
            }
        }
    }

    #endregion
}