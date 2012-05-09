using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.SiteProvider;
using CMS.TreeEngine;
using CMS.WorkflowEngine;
using CMS.UIControls;
using CMS.SettingsProvider;
using CMS.DataEngine;

using TreeNode = CMS.TreeEngine.TreeNode;

public partial class CMSModules_Content_CMSDesk_Properties_LinkedDocs : CMSPropertiesPage
{
    #region "Protected variables"

    protected int nodeId = 0;
    protected string currentSiteName = null;
    protected int nodeParentId = 0;

    protected TreeNode node = null;
    protected TreeProvider tree = null;

    private CurrentUserInfo currentUser = null;

    #endregion


    #region "Methods"

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        currentUser = CMSContext.CurrentUser;
        if (!currentUser.IsAuthorizedPerUIElement("CMS.Content", "Properties.LinkedDocs"))
        {
            RedirectToCMSDeskUIElementAccessDenied("CMS.Content", "Properties.LinkedDocs");
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        UIContext.PropertyTab = PropertyTabEnum.LinkedDocs;

        nodeId = QueryHelper.GetInteger("nodeid", 0);
        currentSiteName = CMSContext.CurrentSiteName.ToLower();

        tree = new TreeProvider(currentUser);
        node = tree.SelectSingleNode(nodeId, TreeProvider.ALL_CULTURES, tree.CombineWithDefaultCulture, false);
        // Set edited document
        EditedDocument = node;

        gridDocs.OnExternalDataBound += gridDocuments_OnExternalDataBound;
        gridDocs.OnAction += gridDocs_OnAction;
        gridDocs.OnDataReload += gridDocs_OnDataReload;
        gridDocs.ShowActionsMenu = true;
        gridDocs.Columns = "NodeAliasPath, SiteName, NodeParentID, DocumentName, DocumentNamePath, ClassDisplayName";

        // Get all possible columns to retrieve
        IDataClass nodeClass = DataClassFactory.NewDataClass("CMS.Tree");
        DocumentInfo di = new DocumentInfo();
        gridDocs.AllColumns = SqlHelperClass.MergeColumns(SqlHelperClass.MergeColumns(di.ColumnNames.ToArray()), SqlHelperClass.MergeColumns(nodeClass.ColumnNames.ToArray()));

    }


    protected DataSet gridDocs_OnDataReload(string completeWhere, string currentOrder, int currentTopN, string columns, int currentOffset, int currentPageSize, ref int totalRecords)
    {
        if (nodeId > 0)
        {
            if (node != null)
            {
                nodeParentId = node.NodeParentID;
                int linkedNodeId = nodeId;

                if (node.IsLink)
                {
                    linkedNodeId = ValidationHelper.GetInteger(node.GetValue("NodeLinkedNodeID"), 0);
                }

                // Get the documents
                columns = SqlHelperClass.MergeColumns(TreeProvider.SELECTNODES_REQUIRED_COLUMNS, columns);
                DataSet nodes = tree.SelectNodes(TreeProvider.ALL_SITES, "/%", TreeProvider.ALL_CULTURES, true, null, "(NodeID = " + linkedNodeId + " AND NodeLinkedNodeID IS NULL) OR NodeLinkedNodeID = " + linkedNodeId, "NodeAliasPath ASC", -1, false, gridDocs.TopN, columns);
                if (!DataHelper.DataSourceIsEmpty(nodes) && (nodes.Tables[0].Rows.Count > 1))
                {
                    gridDocs.Visible = true;

                    lblInfo.Text = GetString("LinkedDocs.LinkedDocs");

                    gridDocs.DataSource = nodes;
                    return nodes;
                }
                else
                {
                    lblInfo.Text = GetString("LinkedDocs.NoLinkedDocs");
                    gridDocs.Visible = false;
                }
            }
        }
        return null;
    }


    protected void gridDocs_OnAction(string actionName, object actionArgument)
    {
        switch (actionName.ToLower())
        {
            case "delete":
                int deleteNodeId = ValidationHelper.GetInteger(actionArgument, 0);
                if (deleteNodeId > 0)
                {
                    // Get the document
                    if (tree == null)
                    {
                        tree = new TreeProvider(currentUser);
                    }
                    TreeNode deleteNode = tree.SelectSingleNode(deleteNodeId, TreeProvider.ALL_CULTURES);
                    if ((deleteNode != null) && (node != null))
                    {
                        try
                        {
                            // Check user permissions
                            if (IsUserAuthorizedToDeleteDocument(deleteNode))
                            {
                                // Delete the document
                                DocumentHelper.DeleteDocument(deleteNode, tree, false, false, false);

                                if ((deleteNode.NodeSiteID == node.NodeSiteID) && (node.NodeAliasPath.StartsWith(deleteNode.NodeAliasPath, StringComparison.CurrentCulture)))
                                {
                                    ltlScript.Text += ScriptHelper.GetScript("SelectItem(" + deleteNode.NodeParentID + ", " + deleteNode.NodeParentID + ");");
                                }
                                else
                                {
                                    gridDocs.ReloadData();

                                    ltlScript.Text += ScriptHelper.GetScript("RefreshTree(" + nodeParentId + ", " + nodeId + ");");
                                }
                            }
                            else
                            {
                                lblError.Text = String.Format(GetString("cmsdesk.notauthorizedtodeletedocument"), HTMLHelper.HTMLEncode(deleteNode.NodeAliasPath));
                                lblError.Visible = true;
                            }
                        }
                        catch (Exception ex)
                        {
                            lblError.Text = GetString("ContentRequest.DeleteFailed") + " : " + ex.Message;
                            lblError.Visible = true;
                        }

                        // If node no longer present, refresh
                        node = tree.SelectSingleNode(nodeId, TreeProvider.ALL_CULTURES);
                        if (node == null)
                        {
                            if (lblError.Text != "")
                            {
                                ltlScript.Text += ScriptHelper.GetAlertScript(lblError.Text);
                                lblError.Text = "";
                                lblError.Visible = false;
                            }
                            ltlScript.Text += ScriptHelper.GetScript("SelectItem(" + nodeId + ", " + nodeId + ");");
                        }
                    }
                }
                break;
        }
    }


    /// <summary>
    /// External data binding handler.
    /// </summary>
    protected object gridDocuments_OnExternalDataBound(object sender, string sourceName, object parameter)
    {
        DataRowView data = null;
        switch (sourceName.ToLower())
        {
            case "documentname":
                {
                    data = (DataRowView)parameter;

                    string name = ValidationHelper.GetString(data["NodeAliasPath"], "");
                    string siteName = ValidationHelper.GetString(data["SiteName"], "");
                    int currentNodeId = ValidationHelper.GetInteger(data["NodeID"], 0);
                    int currentNodeParentId = ValidationHelper.GetInteger(data["NodeParentID"], 0);

                    string result = null;
                    if (currentSiteName == siteName.ToLower())
                    {
                        result = "<a href=\"javascript: SelectItem(" + currentNodeId + ", " + currentNodeParentId + ");\">" + HTMLHelper.HTMLEncode(TextHelper.LimitLength(name, 50)) + "</a>";
                    }
                    else
                    {
                        result = "<span>" + HTMLHelper.HTMLEncode(TextHelper.LimitLength(name, 50)) + "</span>";
                    }

                    bool isLink = (data["NodeLinkedNodeID"] != DBNull.Value);
                    if (isLink)
                    {
                        result += UIHelper.GetDocumentMarkImage(this, DocumentMarkEnum.Link);
                    }

                    return result;
                }

            case "documentnametooltip":
                data = (DataRowView)parameter;
                return UniGridFunctions.DocumentNameTooltip(data);

            case "type":
                {
                    data = (DataRowView)parameter;

                    int currentNodeId = ValidationHelper.GetInteger(data["NodeID"], 0);
                    int linkedNodeId = ValidationHelper.GetInteger(data["NodeLinkedNodeID"], 0);
                    if (linkedNodeId == 0)
                    {
                        return GetString("LinkedDocs.Original");
                    }
                    else if (currentNodeId == nodeId)
                    {
                        return GetString("LinkedDocs.Current");
                    }
                    else
                    {
                        return string.Empty;
                    }
                }

            case "sitename":
                {
                    string siteName = (string)parameter;
                    SiteInfo si = SiteInfoProvider.GetSiteInfo(siteName);
                    if (si != null)
                    {
                        return si.DisplayName;
                    }
                    else
                    {
                        return parameter;
                    }
                }

            case "deleteaction":
                {
                    GridViewRow container = (GridViewRow)parameter;
                    int currentNodeId = ValidationHelper.GetInteger(((DataRowView)container.DataItem)["NodeID"], 0);

                    bool current = (nodeId == currentNodeId);
                    const bool parent = false;

                    ((Control)sender).Visible = ((((DataRowView)container.DataItem)["NodeLinkedNodeID"] != DBNull.Value) && !current && !parent);
                    ((ImageButton)sender).CommandArgument = currentNodeId.ToString();
                    break;
                }
        }
        return parameter;
    }


    /// <summary>
    /// Checks whether the user is authorized to delete document.
    /// </summary>
    /// <param name="node">Document node</param>
    protected bool IsUserAuthorizedToDeleteDocument(TreeNode node)
    {
        // Check delete permission for document
        return (currentUser.IsAuthorizedPerDocument(node, new NodePermissionsEnum[] { NodePermissionsEnum.Delete, NodePermissionsEnum.Read }) == AuthorizationResultEnum.Allowed);
    }

    #endregion
}
