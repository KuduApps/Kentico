using System;
using System.Data;
using System.Web.UI.WebControls;

using CMS.SettingsProvider;
using CMS.UIControls;
using CMS.GlobalHelper;
using CMS.TreeEngine;
using CMS.CMSHelper;
using CMS.DataEngine;

using TreeNode = CMS.TreeEngine.TreeNode;

public partial class CMSModules_Staging_Tools_Tasks_DocumentsList : CMSStagingTasksPage
{
    #region "Variables"

    private int nodeId = 0;
    private int serverId = 0;
    private TreeProvider mTreeProvider = null;

    #endregion


    #region "Preperties"

    /// <summary>
    /// Tree provider used for current page.
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
        set
        {
            mTreeProvider = value;
        }
    }

    #endregion


    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        uniGrid.OnDataReload += uniGrid_OnDataReload;
        uniGrid.OnExternalDataBound += uniGrid_OnExternalDataBound;
        uniGrid.ShowActionsMenu = true;
        uniGrid.Columns = "NodeID, DocumentName, DocumentNamePath, DocumentCulture, DocumentModifiedWhen, ClassDisplayName, NodeChildNodesCount";
        uniGrid.OnBeforeDataReload += uniGrid_OnBeforeDataReload;

        IDataClass nodeClass = DataClassFactory.NewDataClass("CMS.Tree");
        DocumentInfo di = new DocumentInfo();
        uniGrid.AllColumns = SqlHelperClass.MergeColumns(SqlHelperClass.MergeColumns(di.ColumnNames.ToArray()), SqlHelperClass.MergeColumns(nodeClass.ColumnNames.ToArray())); ;

        nodeId = QueryHelper.GetInteger("nodeid", 0);
        serverId = QueryHelper.GetInteger("serverid", 0);
        if (nodeId > 0)
        {
            TreeNode node = TreeProvider.SelectSingleNode(nodeId);
            if (node != null)
            {
                if (node.NodeParentID > 0)
                {
                    lnkUpperDoc.Attributes["onclick"] = "parent.frames['tasksTree'].RefreshNode(" + node.NodeParentID + "," + node.NodeParentID + ");window.location.href='" +
                        ResolveUrl("~/CMSModules/Staging/Tools/Tasks/DocumentsList.aspx?serverid=") + serverId +
                        "&nodeid=" + node.NodeParentID + "'; return false;";
                    imgUpperDoc.ImageUrl = GetImageUrl("Design/Controls/Tree/folderup.png");
                }
                else
                {
                    lnkUpperDoc.Attributes["onclick"] = "return false";
                    imgUpperDoc.ImageUrl = GetImageUrl("Design/Controls/Tree/folderupdisabled.png");
                }
                string closeLink = "<a href=\"#\"><span class=\"ListingClose\" style=\"cursor: pointer;\" " +
                    "onclick=\"parent.frames['tasksHeader'].selectDocuments = false; window.location.href='" +
                    ResolveUrl("~/CMSModules/Staging/Tools/Tasks/Tasks.aspx?serverid=") + serverId +
                    "&nodeid=" + nodeId + "';" +
                    "var completeObj = parent.frames['tasksHeader'].document.getElementById('pnlComplete');" +
                    "if (completeObj != null){ completeObj.style.display = 'block'; }" +
                    "return false;\">" + GetString("general.close") +
                    "</span></a>";
                string docNamePath = "<span class=\"ListingPath\">" + node.DocumentNamePath + "</span>";

                lblListingInfo.Text = String.Format(GetString("synchronization.listinginfo"), docNamePath, closeLink);
            }
        }
    }

    #endregion


    #region "Protected methods"

    protected object uniGrid_OnExternalDataBound(object sender, string sourceName, object parameter)
    {
        DataRowView data = null;
        switch (sourceName.ToLower())
        {
            case "select":
                ImageButton btnImg = (ImageButton)sender;
                btnImg.OnClientClick = "parent.frames['tasksHeader'].selectDocuments = false;parent.frames['tasksTree'].SelectTree(" + btnImg.CommandArgument + ");window.location.href='" + ResolveUrl("~/CMSModules/Staging/Tools/Tasks/Tasks.aspx?serverid=") + serverId + "&nodeid=" + btnImg.CommandArgument + "'; return false;";
                return btnImg;

            case "showsubdocuments":
                ImageButton btnSubImg = (ImageButton)sender;
                int childNodesCount = ValidationHelper.GetInteger(((DataRowView)((GridViewRow)parameter).DataItem).Row["NodeChildNodesCount"], 0);
                if (childNodesCount > 0)
                {
                    btnSubImg.OnClientClick = "parent.frames['tasksTree'].RefreshNode(" + btnSubImg.CommandArgument + "," + btnSubImg.CommandArgument + ");window.location.href='" + ResolveUrl("~/CMSModules/Staging/Tools/Tasks/DocumentsList.aspx?serverid=") + serverId + "&nodeid=" + btnSubImg.CommandArgument + "'; return false;";
                }
                else
                {
                    string noSubDocuments = GetString("synchronization.nosubdocuments");
                    btnSubImg.ToolTip = noSubDocuments;
                    btnSubImg.ImageUrl = uniGrid.DefaultImageDirectoryPath + "subdocumentdisabled.png";
                    btnSubImg.AlternateText = noSubDocuments;
                    btnSubImg.OnClientClick = "return false;";
                }
                return btnSubImg;

            case "documentname":
                data = (DataRowView)parameter;
                string name = ValidationHelper.GetString(data["DocumentName"], string.Empty);
                return "<span>" + HTMLHelper.HTMLEncode(name) + "</span>";

            case "documentnametooltip":
                data = (DataRowView)parameter;
                return UniGridFunctions.DocumentNameTooltip(data);
        }
        return parameter;
    }


    protected void uniGrid_OnBeforeDataReload()
    {
        string searchText = txtSearch.Text.Trim();
        if (!String.IsNullOrEmpty(searchText))
        {
            uniGrid.WhereClause = "(DocumentName LIKE N'%" + SqlHelperClass.GetSafeQueryString(searchText, false) + "%')";
        }
        else
        {
            uniGrid.WhereClause = null;
        }
    }


    protected DataSet uniGrid_OnDataReload(string completeWhere, string currentOrder, int currentTopN, string columns, int currentOffset, int currentPageSize, ref int totalRecords)
    {
        string searchText = txtSearch.Text.Trim();
        int parentNodeID = nodeId;

        string where = "(NodeParentID = " + parentNodeID + ")";

        where = SqlHelperClass.AddWhereCondition(where, completeWhere);

        DataSet nodes = TreeProvider.SelectNodes(TreeProvider.ALL_SITES, "/%", null, true, null, where, null, TreeProvider.ALL_LEVELS, false, currentTopN, columns);
        if (DataHelper.DataSourceIsEmpty(nodes))
        {
            if (String.IsNullOrEmpty(searchText))
            {
                pnlSearch.Visible = false;
                lblInfo.ResourceString = "synchronization.nochilddocuments";
            }
            else
            {
                lblInfo.ResourceString = "synchronization.nodocumentsfound";
            }
            pnlUniGrid.Visible = false;
            pnlInfo.Visible = true;
        }
        else
        {
            pnlUniGrid.Visible = true;
        }
        totalRecords = DataHelper.GetItemsCount(nodes);
        return nodes;
    }

    #endregion
}
