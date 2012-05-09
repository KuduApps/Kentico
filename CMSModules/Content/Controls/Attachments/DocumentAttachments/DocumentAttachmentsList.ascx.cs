using System;
using System.Data;
using System.Text;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Collections.Generic;

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.TreeEngine;
using CMS.WorkflowEngine;
using CMS.FormControls;
using CMS.SiteProvider;
using CMS.SettingsProvider;
using CMS.UIControls;
using CMS.ExtendedControls;

public partial class CMSModules_Content_Controls_Attachments_DocumentAttachments_DocumentAttachmentsList : AttachmentsControl
{
    #region "Variables"

    private string mInnerDivClass = "NewAttachment";
    private int? mFilterLimit = null;

    #endregion


    #region "Properties"

    /// <summary>
    /// Minimal count of entries for display filter.
    /// </summary>
    public int FilterLimit
    {
        get
        {
            if (mFilterLimit == null)
            {
                mFilterLimit = ValidationHelper.GetInteger(SettingsHelper.AppSettings["CMSDefaultListingFilterLimit"], 25);
            }
            return (int)mFilterLimit;
        }
        set
        {
            mFilterLimit = value;
        }
    }


    /// <summary>
    /// CSS class of the new attachment link.
    /// </summary>
    public string InnerDivClass
    {
        get
        {
            return mInnerDivClass;
        }
        set
        {
            mInnerDivClass = value;
            if (lblDisabled != null)
            {
                lblDisabled.CssClass = value + "Disabled";
            }
        }
    }


    /// <summary>
    /// Info label.
    /// </summary>
    public Label InfoLabel
    {
        get
        {
            return lblInfo;
        }
    }


    /// <summary>
    /// Indicates whether grouped attachments should be displayed.
    /// </summary>
    public override Guid GroupGUID
    {
        get
        {
            return base.GroupGUID;
        }
        set
        {
            base.GroupGUID = value;
            if ((dsAttachments != null) && (newAttachmentElem != null))
            {
                dsAttachments.AttachmentGroupGUID = value;
                newAttachmentElem.AttachmentGroupGUID = value;
            }
        }
    }


    /// <summary>
    /// Indicates if paging is allowed.
    /// </summary>
    public override bool AllowPaging
    {
        get
        {
            return base.AllowPaging;
        }
        set
        {
            base.AllowPaging = value;
            if (gridAttachments != null)
            {
                gridAttachments.GridView.AllowPaging = value;
            }
        }
    }


    /// <summary>
    /// Defines size of the page for paging.
    /// </summary>
    public override string PageSize
    {
        get
        {
            return base.PageSize;
        }
        set
        {
            base.PageSize = value;
            if (gridAttachments != null)
            {
                gridAttachments.PageSize = value;
            }
        }
    }


    /// <summary>
    /// Default page size.
    /// </summary>
    public override int DefaultPageSize
    {
        get
        {
            return base.DefaultPageSize;
        }
        set
        {
            base.DefaultPageSize = value;
            if (gridAttachments != null)
            {
                gridAttachments.Pager.DefaultPageSize = value;
            }
        }
    }



    /// <summary>
    /// Width of the attachment.
    /// </summary>
    public override int ResizeToWidth
    {
        get
        {
            return base.ResizeToWidth;
        }
        set
        {
            base.ResizeToWidth = value;
            if (newAttachmentElem != null)
            {
                newAttachmentElem.ResizeToWidth = value;
            }
        }
    }


    /// <summary>
    /// Height of the attachment.
    /// </summary>
    public override int ResizeToHeight
    {
        get
        {
            return base.ResizeToHeight;
        }
        set
        {
            base.ResizeToHeight = value;
            if (newAttachmentElem != null)
            {
                newAttachmentElem.ResizeToHeight = value;
            }
        }
    }


    /// <summary>
    /// Maximal side size of the attachment.
    /// </summary>
    public override int ResizeToMaxSideSize
    {
        get
        {
            return base.ResizeToMaxSideSize;
        }
        set
        {
            base.ResizeToMaxSideSize = value;
            if (newAttachmentElem != null)
            {
                newAttachmentElem.ResizeToMaxSideSize = value;
            }
        }
    }


    /// <summary>
    /// List of allowed extensions.
    /// </summary>
    public override string AllowedExtensions
    {
        get
        {
            return base.AllowedExtensions;
        }
        set
        {
            base.AllowedExtensions = value;
            if (newAttachmentElem != null)
            {
                newAttachmentElem.AllowedExtensions = value;
            }
        }
    }


    /// <summary>
    /// Specifies the document for which the attachments should be displayed.
    /// </summary>
    public override int DocumentID
    {
        get
        {
            return base.DocumentID;
        }
        set
        {
            base.DocumentID = value;
            if (newAttachmentElem != null)
            {
                newAttachmentElem.DocumentID = value;
            }
        }
    }


    /// <summary>
    /// Specifies the version of the document (optional).
    /// </summary>
    public override int VersionHistoryID
    {
        get
        {
            int versionHistoryId = base.VersionHistoryID;
            if ((versionHistoryId == 0) && (Node != null))
            {
                versionHistoryId = Node.DocumentCheckedOutVersionHistoryID;
            }
            return versionHistoryId;
        }
        set
        {
            base.VersionHistoryID = value;
            if ((dsAttachments != null) && UsesWorkflow)
            {
                dsAttachments.DocumentVersionHistoryID = value;
            }
        }
    }


    /// <summary>
    /// Defines the form GUID; indicates that the temporary attachment will be handled.
    /// </summary>
    public override Guid FormGUID
    {
        get
        {
            return base.FormGUID;
        }
        set
        {
            base.FormGUID = value;
            if ((dsAttachments != null) && (newAttachmentElem != null))
            {
                dsAttachments.AttachmentFormGUID = value;
                newAttachmentElem.FormGUID = value;
            }
        }
    }


    /// <summary>
    /// If true, control does not process the data.
    /// </summary>
    public override bool StopProcessing
    {
        get
        {
            return base.StopProcessing;
        }
        set
        {
            base.StopProcessing = value;
            if ((dsAttachments != null) && (newAttachmentElem != null))
            {
                dsAttachments.StopProcessing = value;
                newAttachmentElem.StopProcessing = value;
            }
        }
    }

    #endregion


    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        if (dsAttachments == null)
        {
            StopProcessing = true;
        }
        Visible = !StopProcessing;

        if (StopProcessing)
        {
            if (dsAttachments != null)
            {
                dsAttachments.StopProcessing = true;
            }
            if (newAttachmentElem != null)
            {
                newAttachmentElem.StopProcessing = true;
            }
            // Do nothing
        }
        else
        {
            // Register script for tooltips
            if (ShowTooltip)
            {
                ScriptHelper.RegisterTooltip(Page);
            }

            // Ensure info message
            if ((Request["__EVENTTARGET"] == hdnPostback.ClientID) || Request["__EVENTTARGET"] == hdnFullPostback.ClientID)
            {
                string action = Request["__EVENTARGUMENT"];

                switch (action)
                {
                    case "insert":
                        lblInfo.Text = GetString("attach.inserted");
                        break;

                    case "update":
                        lblInfo.Text = GetString("attach.updated");
                        break;

                    case "delete":
                        lblInfo.Text = GetString("attach.deleted");
                        break;

                    default:
                        if (action != "")
                        {
                            UpdateEditParameters(action);
                        }
                        break;
                }
            }

            #region "Scripts"

            // Refresh script
            string script = String.Format(@"
function RefreshUpdatePanel_{0}(hiddenFieldID, action) {{
   var hiddenField = document.getElementById(hiddenFieldID);
   if (hiddenField) {{
       __doPostBack(hiddenFieldID, action);
   }}
}}
function FullRefresh(hiddenFieldID, action) {{
   if(PassiveRefresh != null)
   {{
       PassiveRefresh();
   }}

   var hiddenField = document.getElementById(hiddenFieldID);
   if (hiddenField) {{
       __doPostBack(hiddenFieldID, action);
   }}
}}
function FullPageRefresh_{0}(guid) {{
   if(PassiveRefresh != null)
   {{
       PassiveRefresh();
   }}

   var hiddenField = document.getElementById('{1}');if (hiddenField) {{
       __doPostBack('{1}', 'refresh|' + guid);}}
}}
function InitRefresh_{0}(msg, fullRefresh, refreshTree, action)
{{
    if((msg != null) && (msg != '')){{ 
        alert(msg); action='error'; 
    }}
    {3}   
    if(fullRefresh){{ 
        FullRefresh('{1}', action); 
    }}
    else {{ 
        RefreshUpdatePanel_{0}('{2}', action); 
    }}
}}
function DeleteConfirmation() {{
    return confirm({4});;
}}
", ClientID, hdnFullPostback.ClientID, hdnPostback.ClientID, (ActualNode != null ? "if (window.RefreshTree && fullRefresh) { RefreshTree(" + ActualNode.NodeParentID + ", " + ActualNode.NodeID + "); }" : string.Empty), ScriptHelper.GetString(GetString("attach.deleteconfirmation")));

            ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "AttachmentScripts_" + ClientID, ScriptHelper.GetScript(script));

            // Register dialog script
            ScriptHelper.RegisterDialogScript(Page);

            // Register jQuery script for thumbnails updating
            ScriptHelper.RegisterJQuery(Page);

            #endregion


            // Initialize button for adding attachments
            newAttachmentElem.ImageUrl = ResolveUrl(GetImageUrl("Design/Controls/DirectUploader/upload_new.png"));
            newAttachmentElem.ImageWidth = 16;
            newAttachmentElem.ImageHeight = 16;
            newAttachmentElem.SourceType = MediaSourceEnum.DocumentAttachments;
            newAttachmentElem.DocumentID = DocumentID;
            newAttachmentElem.NodeParentNodeID = NodeParentNodeID;
            newAttachmentElem.NodeClassName = NodeClassName;
            newAttachmentElem.ResizeToWidth = ResizeToWidth;
            newAttachmentElem.ResizeToHeight = ResizeToHeight;
            newAttachmentElem.ResizeToMaxSideSize = ResizeToMaxSideSize;
            newAttachmentElem.AttachmentGroupGUID = GroupGUID;
            newAttachmentElem.FormGUID = FormGUID;
            newAttachmentElem.AllowedExtensions = AllowedExtensions;
            newAttachmentElem.ParentElemID = ClientID;
            newAttachmentElem.ForceLoad = true;
            newAttachmentElem.InnerDivHtml = GetString("attach.newattachment");
            newAttachmentElem.InnerDivClass = InnerDivClass;
            newAttachmentElem.InnerLoadingDivHtml = GetString("attach.loading");
            newAttachmentElem.InnerLoadingDivClass = InnerLoadingDivClass;
            newAttachmentElem.IsLiveSite = IsLiveSite;
            newAttachmentElem.CheckPermissions = CheckPermissions;
            lblDisabled.CssClass = InnerDivClass + "Disabled";

            imgDisabled.ImageUrl = ResolveUrl(GetImageUrl("Design/Controls/DirectUploader/upload_newdisabled.png"));

            // Grid initialization
            gridAttachments.OnExternalDataBound += gridAttachments_OnExternalDataBound;
            gridAttachments.OnDataReload += gridAttachments_OnDataReload;
            gridAttachments.OnAction += gridAttachments_OnAction;
            gridAttachments.OnBeforeDataReload += gridAttachments_OnBeforeDataReload;
            gridAttachments.ZeroRowsText = GetString("general.nodatafound");
            gridAttachments.IsLiveSite = IsLiveSite;
            gridAttachments.ShowActionsMenu = true;
            gridAttachments.Columns = "AttachmentID, AttachmentGUID, AttachmentImageWidth, AttachmentImageHeight, AttachmentExtension, AttachmentName, AttachmentSize, AttachmentOrder, AttachmentTitle, AttachmentDescription";

            AttachmentInfo ai = new AttachmentInfo();
            gridAttachments.AllColumns = SqlHelperClass.MergeColumns(ai.ColumnNames.ToArray());

            pnlGrid.Attributes.Add("style", "margin-top: 5px;");
        }
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        if (StopProcessing)
        {
            // Do nothing
        }
        else
        {
            lblError.Visible = (lblError.Text != string.Empty);
            lblInfo.Visible = (lblInfo.Text != string.Empty);

            // Ensure uplaoder button
            plcUploader.Visible = Enabled;
            plcUploaderDisabled.Visible = !Enabled;

            // Hide actions
            gridAttachments.GridView.Columns[0].Visible = !HideActions;
            gridAttachments.GridView.Columns[1].Visible = !HideActions;
            newAttachmentElem.Visible = !HideActions && Enabled;
            plcUploaderDisabled.Visible = !HideActions && !Enabled;

            if (!RequestHelper.IsPostBack())
            {
                // Hide filter
                if ((FilterLimit > 0) && (gridAttachments.RowsCount <= FilterLimit))
                {
                    pnlFilter.Visible = false;
                }
                else
                {
                    pnlFilter.Visible = true;
                }
            }

            // Ensure correct layout
            bool gridHasData = !DataHelper.DataSourceIsEmpty(gridAttachments.DataSource);
            Visible = !HideActions || pnlFilter.Visible;

            lblNoData.Visible = (!gridHasData && pnlFilter.Visible);

            // Dialog for editing attachment
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(String.Format(@"
function Edit_{0}(attachmentGUID, formGUID, versionHistoryID, parentId, hash, image) {{ 
  var form = '';
  if (formGUID != '') {{ 
      form = '&formguid=' + formGUID + '&parentid=' + parentId; 
  }}
  {1}
  if (image) {{
      modalDialog('{2}, 'editorDialog', 905, 670); 
  }}
  else {{
      modalDialog('{3}, 'editorDialog', 500, 350); 
  }}
  return false; 
}}",
            ClientID,
            (((Node != null) ? String.Format("else{{ form = '&siteid=' + {0}; }}", Node.NodeSiteID) : string.Empty)),
            ResolveUrl((IsLiveSite ? "~/CMSFormControls/LiveSelectors/ImageEditor.aspx" : "~/CMSModules/Content/CMSDesk/Edit/ImageEditor.aspx") + "?attachmentGUID=' + attachmentGUID + '&versionHistoryID=' + versionHistoryID + form + '&clientid=" + ClientID + "&refresh=1&hash=' + hash"),
            CMSContext.ResolveDialogUrl(String.Format("{0}?attachmentGUID=' + attachmentGUID + '&versionHistoryID=' + versionHistoryID + form + '&clientid={1}&refresh=1&hash=' + hash", (IsLiveSite ? "~/CMSModules/Content/Attachments/CMSPages/MetaDataEditor.aspx" : "~/CMSModules/Content/Attachments/Dialogs/MetaDataEditor.aspx"), ClientID))));

            // Register script for editing attachment
            ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "AttachmentEditScripts_" + ClientID, ScriptHelper.GetScript(sb.ToString()));
        }
    }

    #endregion


    #region "Public methods"

    /// <summary>
    /// Indicates if the control contains some data.
    /// </summary>
    public override bool HasData()
    {
        return (dsAttachments != null ? !DataHelper.DataSourceIsEmpty(dsAttachments.DataSource) : false);
    }

    #endregion


    #region "Overridden methods"

    /// <summary>
    /// Reloads data.
    /// </summary>
    public override void ReloadData()
    {
        gridAttachments.ReloadData();
    }

    #endregion


    #region "Private methods"

    /// <summary>
    /// Updates parameters used by Edit button when displaying image editor.
    /// </summary>
    /// <param name="action"></param>
    private void UpdateEditParameters(string action)
    {
        if (ShowTooltip)
        {
            // Try to get attachment Guid
            Guid attGuid = ValidationHelper.GetGuid(action, Guid.Empty);
            if (attGuid != null)
            {
                // Get attachment
                AttachmentInfo attInfo = AttachmentManager.GetAttachmentInfoWithoutBinary(attGuid, SiteName);
                if (attInfo != null)
                {
                    // Get attachment URL
                    string attachmentUrl = null;
                    if (Node != null)
                    {
                        attachmentUrl = CMSContext.ResolveUIUrl(TreePathUtils.GetAttachmentUrl(attGuid, URLHelper.GetSafeFileName(attInfo.AttachmentName, CMSContext.CurrentSiteName), VersionHistoryID, null));
                    }
                    else
                    {
                        attachmentUrl = ResolveUrl(DocumentHelper.GetAttachmentUrl(attGuid, VersionHistoryID));
                    }
                    attachmentUrl = URLHelper.UpdateParameterInUrl(attachmentUrl, "chset", Guid.NewGuid().ToString());

                    // Ensure correct URL
                    if (SiteName != CMSContext.CurrentSiteName)
                    {
                        attachmentUrl = URLHelper.AddParameterToUrl(attachmentUrl, "sitename", SiteName);
                    }

                    // Generate new tooltip command
                    string newToolTip = null;
                    string title = attInfo.AttachmentTitle;
                    string description = attInfo.AttachmentDescription;
                    // Optionally trim attachment name
                    string attachmentName = TextHelper.LimitLength(attInfo.AttachmentName, ATTACHMENT_NAME_LIMIT, "...");
                    int imageWidth = attInfo.AttachmentImageWidth;
                    int imageHeight = attInfo.AttachmentImageHeight;
                    bool isImage = ImageHelper.IsImage(attInfo.AttachmentExtension);

                    int tooltipWidth = 300;
                    string url = isImage ? attachmentUrl : null;

                    string tooltipBody = UIHelper.GetTooltip(url, imageWidth, imageHeight, title, attachmentName, description, null, ref tooltipWidth);
                    if (!string.IsNullOrEmpty(tooltipBody))
                    {
                        newToolTip = String.Format("Tip('{0}', WIDTH, -300)", tooltipBody);
                    }

                    // Get update script
                    string updateScript = String.Format("$j(\"#{0}\").attr('onmouseover', '').unbind('mouseover').mouseover(function(){{ {1} }});", attGuid, newToolTip);

                    // Execute update
                    ScriptHelper.RegisterStartupScript(Page, typeof(Page), "AttachmentUpdateEdit", ScriptHelper.GetScript(updateScript));
                }
            }
        }
    }


    private string GetWhereConditionInternal()
    {
        return string.IsNullOrEmpty(txtFilter.Text) ? null : String.Format("AttachmentName LIKE '%{0}%'", SqlHelperClass.GetSafeQueryString(txtFilter.Text, false));
    }

    #endregion


    #region "Grid events"

    protected DataSet gridAttachments_OnDataReload(string completeWhere, string currentOrder, int currentTopN, string columns, int currentOffset, int currentPageSize, ref int totalRecords)
    {
        string where = null;
        Guid attachmentFormGUID = Guid.Empty;
        string whereCondition = GetWhereConditionInternal();
        int documentVersionHistoryID = UsesWorkflow ? VersionHistoryID : 0;

        // Grouped attachments
        if (GroupGUID != Guid.Empty)
        {
            // Combine where conditions
            where = String.Format("(AttachmentGroupGUID='{0}')", GroupGUID);
        }
        // Unsorted attachments
        else
        {
            where = "(AttachmentIsUnsorted = 1)";
        }

        // Temporary attachments
        if ((attachmentFormGUID != Guid.Empty) && (documentVersionHistoryID == 0))
        {
            where += String.Format(" AND (AttachmentFormGUID='{0}')", FormGUID);
        }
        // Else document attachments
        else
        {
            if (documentVersionHistoryID == 0)
            {
                // Ensure current site name
                if (string.IsNullOrEmpty(SiteName))
                {
                    SiteName = CMSContext.CurrentSiteName;
                }


                if (Node != null)
                {
                    where += String.Format(" AND (AttachmentDocumentID = {0})", Node.DocumentID);

                    // Get attachments for latest version if not live site
                    if (!IsLiveSite)
                    {
                        WorkflowManager wm = new WorkflowManager(TreeProvider);
                        WorkflowInfo wi = wm.GetNodeWorkflow(Node);
                        if (wi != null)
                        {
                            documentVersionHistoryID = Node.DocumentCheckedOutVersionHistoryID;
                        }
                    }
                }
                else
                {
                    return null;
                }
            }
        }

        // Ensure additional where condition
        whereCondition = SqlHelperClass.AddWhereCondition(whereCondition, where);

        // Ensure [AttachmentID] column
        List<String> cols = new List<string>(columns.Split(new char[] { ',', ';' }));
        if (!cols.Contains("AttachmentID") && !cols.Contains("[AttachmentID]"))
        {
            columns = SqlHelperClass.MergeColumns("AttachmentID", columns);
        }

        DataSet ds = null;

        // Get attachments for published document
        if (documentVersionHistoryID == 0)
        {
            currentOrder = "AttachmentOrder, AttachmentName, AttachmentID";
            ds = AttachmentManager.GetAttachments(whereCondition, currentOrder, true, currentTopN, columns);
        }
        else
        {
            currentOrder = "AttachmentOrder, AttachmentName, AttachmentHistoryID";

            VersionManager vm = new VersionManager(TreeProvider);
            ds = vm.GetVersionAttachments(documentVersionHistoryID, whereCondition, currentOrder, true, currentTopN, columns.Replace("AttachmentID", "AttachmentHistoryID"));
        }

        // Ensure consistent ID column name
        if (!DataHelper.DataSourceIsEmpty(ds))
        {
            ds.Tables[0].Columns[0].ColumnName = "AttachmentID";
        }

        return ds;
    }


    protected void gridAttachments_OnBeforeDataReload()
    {
        gridAttachments.IsLiveSite = IsLiveSite;
        gridAttachments.GridView.AllowPaging = AllowPaging;
        if (!AllowPaging)
        {
            gridAttachments.PageSize = "0";
        }
        gridAttachments.GridView.AllowSorting = false;
    }


    /// <summary>
    /// UniGrid action buttons event handler.
    /// </summary>
    protected void gridAttachments_OnAction(string actionName, object actionArgument)
    {
        if (Enabled && !HideActions)
        {
            // Check the permissions
            #region "Check permissions"

            if (CheckPermissions)
            {
                if (FormGUID != Guid.Empty)
                {
                    if (!RaiseOnCheckPermissions("Create", this))
                    {
                        if (!CMSContext.CurrentUser.IsAuthorizedToCreateNewDocument(NodeParentNodeID, NodeClassName))
                        {
                            lblError.Text = GetString("attach.actiondenied");
                            return;
                        }
                    }
                }
                else
                {
                    if (!RaiseOnCheckPermissions("Modify", this))
                    {
                        if (CMSContext.CurrentUser.IsAuthorizedPerDocument(Node, NodePermissionsEnum.Modify) == AuthorizationResultEnum.Denied)
                        {
                            lblError.Text = GetString("attach.actiondenied");
                            return;
                        }
                    }
                }
            }

            #endregion

            Guid attachmentGuid = Guid.Empty;

            // Get action argument (Guid or int)
            if (ValidationHelper.IsGuid(actionArgument))
            {
                attachmentGuid = ValidationHelper.GetGuid(actionArgument, Guid.Empty);
            }

            // Process proper action
            switch (actionName.ToLower())
            {
                case "moveup":
                    if (attachmentGuid != Guid.Empty)
                    {
                        // Move attachment up
                        if (FormGUID == Guid.Empty)
                        {
                            // Ensure automatic check-in/ check-out
                            VersionManager vm = null;

                            // Check out the document
                            if (AutoCheck)
                            {
                                vm = new VersionManager(TreeProvider);
                                vm.CheckOut(Node, Node.IsPublished, true);
                            }

                            DocumentHelper.MoveAttachmentUp(attachmentGuid, Node);

                            // Check in the document
                            if (AutoCheck)
                            {
                                if (vm != null)
                                {
                                    vm.CheckIn(Node, null, null);
                                    VersionHistoryID = Node.DocumentCheckedOutVersionHistoryID;
                                }

                                // Ensure full page refresh
                                ScriptHelper.RegisterStartupScript(Page, typeof(Page), "moveUpRefresh", ScriptHelper.GetScript(String.Format("InitRefresh_{0}('', true, false, 'moveup');", ClientID)));
                            }

                            // Log synchronization task if not under workflow
                            if (!UsesWorkflow)
                            {
                                DocumentSynchronizationHelper.LogDocumentChange(Node, TaskTypeEnum.UpdateDocument, TreeProvider);
                            }
                        }
                        else
                        {
                            AttachmentManager.MoveAttachmentUp(attachmentGuid, 0);
                        }
                    }
                    break;

                case "movedown":
                    if (attachmentGuid != Guid.Empty)
                    {
                        // Move attachment down
                        if (FormGUID == Guid.Empty)
                        {
                            // Ensure automatic check-in/ check-out
                            VersionManager vm = null;

                            // Check out the document
                            if (AutoCheck)
                            {
                                vm = new VersionManager(TreeProvider);
                                vm.CheckOut(Node, Node.IsPublished, true);
                            }

                            DocumentHelper.MoveAttachmentDown(attachmentGuid, Node);

                            // Check in the document
                            if (AutoCheck)
                            {
                                if (vm != null)
                                {
                                    vm.CheckIn(Node, null, null);
                                    VersionHistoryID = Node.DocumentCheckedOutVersionHistoryID;
                                }

                                // Ensure full page refresh
                                ScriptHelper.RegisterStartupScript(Page, typeof(Page), "moveDownRefresh", ScriptHelper.GetScript(String.Format("InitRefresh_{0}('', true, false, 'movedown');", ClientID)));
                            }

                            // Log synchronization task if not under workflow
                            if (!UsesWorkflow)
                            {
                                DocumentSynchronizationHelper.LogDocumentChange(Node, TaskTypeEnum.UpdateDocument, TreeProvider);
                            }
                        }
                        else
                        {
                            AttachmentManager.MoveAttachmentDown(attachmentGuid, 0);
                        }
                    }
                    break;

                case "delete":
                    if (attachmentGuid != Guid.Empty)
                    {
                        // Delete attachment
                        if (FormGUID == Guid.Empty)
                        {
                            // Ensure automatic check-in/ check-out
                            VersionManager vm = null;

                            // Check out the document
                            if (AutoCheck)
                            {
                                vm = new VersionManager(TreeProvider);
                                vm.CheckOut(Node, Node.IsPublished, true);
                            }

                            DocumentHelper.DeleteAttachment(Node, attachmentGuid, TreeProvider);

                            // Check in the document
                            if (AutoCheck)
                            {
                                if (vm != null)
                                {
                                    vm.CheckIn(Node, null, null);
                                    VersionHistoryID = Node.DocumentCheckedOutVersionHistoryID;
                                }

                                // Ensure full page refresh
                                ScriptHelper.RegisterStartupScript(Page, typeof(Page), "deleteRefresh", ScriptHelper.GetScript(String.Format("InitRefresh_{0}('', true, false, 'delete');", ClientID)));
                            }

                            // Log synchronization task if not under workflow
                            if (!UsesWorkflow)
                            {
                                DocumentSynchronizationHelper.LogDocumentChange(Node, TaskTypeEnum.UpdateDocument, TreeProvider);
                            }
                        }
                        else
                        {
                            AttachmentManager.DeleteTemporaryAttachment(attachmentGuid, CMSContext.CurrentSiteName);
                        }

                        lblInfo.Text = GetString("attach.deleted");
                    }
                    break;
            }
        }
    }


    /// <summary>
    /// UniGrid external data bound.
    /// </summary>
    protected object gridAttachments_OnExternalDataBound(object sender, string sourceName, object parameter)
    {
        DataRowView rowView = null;
        string attName = null;
        string attachmentExt = null;

        switch (sourceName.ToLower())
        {
            case "update":
                Panel pnlBlock = new Panel() { ID = "pnlBlock" };

                bool isWebDAVEnabled = CMSContext.IsWebDAVEnabled(SiteName);
                bool isWindowsAuthentication = RequestHelper.IsWindowsAuthentication();
                pnlBlock.Style.Add("margin", "0 auto");
                pnlBlock.Width = ((isWebDAVEnabled && isWindowsAuthentication) ? 32 : 16);

                // Add disabled image
                ImageButton imgUpdate = new ImageButton() { ID = "imgUpdate" };
                imgUpdate.PreRender += imgUpdate_PreRender;
                pnlBlock.Controls.Add(imgUpdate);

                // Add update control
                // Dynamically load uploader control
                DirectFileUploader dfuElem = Page.LoadControl("~/CMSModules/Content/Controls/Attachments/DirectFileUploader/DirectFileUploader.ascx") as DirectFileUploader;

                rowView = parameter as DataRowView;

                // Set uploader's properties
                if (dfuElem != null)
                {
                    dfuElem.SourceType = MediaSourceEnum.DocumentAttachments;
                    dfuElem.ID = "dfuElem" + DocumentID;
                    dfuElem.IsLiveSite = IsLiveSite;
                    dfuElem.EnableSilverlightUploader = false;
                    dfuElem.ControlGroup = "update";
                    dfuElem.AttachmentGUID = GetAttachmentGuid(rowView);
                    dfuElem.DisplayInline = true;
                    dfuElem.UploadMode = MultifileUploaderModeEnum.DirectSingle;
                    dfuElem.InnerLoadingDivHtml = "&nbsp;";
                    dfuElem.MaxNumberToUpload = 1;
                    dfuElem.Height = 16;
                    dfuElem.Width = 16;
                    dfuElem.PreRender += dfuElem_PreRender;
                    pnlBlock.Controls.Add(dfuElem);
                }

                attName = GetAttachmentName(rowView);
                attachmentExt = GetAttachmentExtension(rowView);

                int nodeGroupId = (Node != null) ? Node.GetIntegerValue("NodeGroupID") : 0;

                // Check if WebDAV allowed by the form
                bool allowWebDAV = (Form == null) ? true : Form.AllowWebDAV;

                // Add WebDAV edit control
                if (allowWebDAV && isWebDAVEnabled && isWindowsAuthentication && (FormGUID == Guid.Empty) && WebDAVSettings.IsExtensionAllowedForEditMode(attachmentExt, SiteName))
                {
                    // Dynamically load control
                    WebDAVEditControl webdavElem = Page.LoadControl("~/CMSModules/WebDAV/Controls/AttachmentWebDAVEditControl.ascx") as WebDAVEditControl;

                    // Set editor's properties
                    if (webdavElem != null)
                    {
                        webdavElem.Enabled = Enabled;
                        webdavElem.ID = "webdavElem" + DocumentID;

                        // Ensure form identification
                        if ((Form != null) && (Form.Parent != null))
                        {
                            webdavElem.FormID = Form.Parent.ClientID;
                        }
                        webdavElem.SiteName = SiteName;
                        webdavElem.FileName = attName;
                        webdavElem.NodeAliasPath = Node.NodeAliasPath;
                        webdavElem.NodeCultureCode = Node.DocumentCulture;
                        webdavElem.PreRender += webdavElem_PreRender;

                        if (FieldInfo != null)
                        {
                            webdavElem.AttachmentFieldName = FieldInfo.Name;
                        }

                        // Set Group ID for live site
                        webdavElem.GroupID = IsLiveSite ? nodeGroupId : 0;
                        webdavElem.IsLiveSite = IsLiveSite;

                        // Align left if WebDAV is enabled and windows authentication is enabled
                        bool isRTL = (IsLiveSite && CultureHelper.IsPreferredCultureRTL()) || (!IsLiveSite && CultureHelper.IsUICultureRTL());
                        pnlBlock.Style.Add("text-align", isRTL ? "right" : "left");

                        pnlBlock.Controls.Add(webdavElem);
                    }
                }
                return pnlBlock;

            case "edit":
                // Get file extension
                string extension = ValidationHelper.GetString(((DataRowView)((GridViewRow)parameter).DataItem).Row["AttachmentExtension"], string.Empty).ToLower();
                Guid guid = ValidationHelper.GetGuid(((DataRowView)((GridViewRow)parameter).DataItem).Row["AttachmentGUID"], Guid.Empty);
                if (sender is ImageButton)
                {
                    ImageButton img = (ImageButton)sender;
                    img.AlternateText = String.Format("{0}|{1}", extension, guid);
                    img.PreRender += img_PreRender;
                }
                break;

            case "delete":
                if (sender is ImageButton)
                {
                    ImageButton imgDelete = (ImageButton)sender;
                    // Turn off validation
                    imgDelete.CausesValidation = false;
                    imgDelete.PreRender += imgDelete_PreRender;
                    // Explicitly initialize confirmation
                    imgDelete.OnClientClick = "if(DeleteConfirmation() == false){return false;}";
                }
                break;

            case "moveup":
                if (sender is ImageButton)
                {
                    ImageButton imgUp = (ImageButton)sender;
                    // Turn off validation
                    imgUp.CausesValidation = false;
                    imgUp.PreRender += imgUp_PreRender;
                }
                break;

            case "movedown":
                if (sender is ImageButton)
                {
                    ImageButton imgDown = (ImageButton)sender;
                    // Turn off validation
                    imgDown.CausesValidation = false;
                    imgDown.PreRender += imgDown_PreRender;
                }
                break;

            case "attachmentname":
                {
                    rowView = parameter as DataRowView;

                    // Get attachment GUID
                    Guid attachmentGuid = GetAttachmentGuid(rowView);

                    // Get attachment extension
                    attachmentExt = GetAttachmentExtension(rowView);
                    bool isImage = ImageHelper.IsImage(attachmentExt);
                    string iconUrl = GetFileIconUrl(attachmentExt, "List");

                    // Get link for attachment
                    string attachmentUrl = null;
                    attName = ValidationHelper.GetString(rowView["AttachmentName"], string.Empty);
                    int documentId = DocumentID;

                    if (Node != null)
                    {
                        if (IsLiveSite && (documentId > 0))
                        {
                            attachmentUrl = CMSContext.ResolveUIUrl(TreePathUtils.GetAttachmentUrl(attachmentGuid, URLHelper.GetSafeFileName(attName, CMSContext.CurrentSiteName), 0, null));
                        }
                        else
                        {
                            attachmentUrl = CMSContext.ResolveUIUrl(TreePathUtils.GetAttachmentUrl(attachmentGuid, URLHelper.GetSafeFileName(attName, CMSContext.CurrentSiteName), VersionHistoryID, null));
                        }
                    }
                    else
                    {
                        attachmentUrl = ResolveUrl(DocumentHelper.GetAttachmentUrl(attachmentGuid, VersionHistoryID));
                    }
                    attachmentUrl = URLHelper.UpdateParameterInUrl(attachmentUrl, "chset", Guid.NewGuid().ToString());

                    // Ensure correct URL
                    if (SiteName != CMSContext.CurrentSiteName)
                    {
                        attachmentUrl = URLHelper.AddParameterToUrl(attachmentUrl, "sitename", SiteName);
                    }

                    // Add latest version requirement for live site
                    if (IsLiveSite && (documentId > 0))
                    {
                        // Add requirement for latest version of files for current document
                        string newparams = "latestfordocid=" + documentId;
                        newparams += "&hash=" + ValidationHelper.GetHashString("d" + documentId);

                        attachmentUrl += "&" + newparams;
                    }

                    // Optionally trim attachment name
                    string attachmentName = TextHelper.LimitLength(attName, ATTACHMENT_NAME_LIMIT, "...");

                    // Tooltip
                    string tooltip = null;
                    if (ShowTooltip)
                    {
                        string title = ValidationHelper.GetString(DataHelper.GetDataRowViewValue(rowView, "AttachmentTitle"), string.Empty);
                        ;
                        string description = ValidationHelper.GetString(DataHelper.GetDataRowViewValue(rowView, "AttachmentDescription"), string.Empty);
                        int imageWidth = ValidationHelper.GetInteger(DataHelper.GetDataRowViewValue(rowView, "AttachmentImageWidth"), 0);
                        int imageHeight = ValidationHelper.GetInteger(DataHelper.GetDataRowViewValue(rowView, "AttachmentImageHeight"), 0);

                        tooltip = UIHelper.GetTooltipAttributes(attachmentUrl, imageWidth, imageHeight, title, attachmentName, attachmentExt, description, null, 300);
                    }

                    // Icon
                    string imageTag = String.Format("<img class=\"Icon\" src=\"{0}\" alt=\"{1}\" />", iconUrl, attachmentName);

                    if (isImage)
                    {
                        return String.Format("<a href=\"#\" onclick=\"javascript: window.open('{0}'); return false;\"><span id=\"{1}\" {2}>{3}{4}</span></a>", attachmentUrl, attachmentGuid, tooltip, imageTag, attachmentName);
                    }
                    else
                    {
                        return String.Format("<a href=\"{0}\"><span id=\"{1}\" {2}>{3}{4}</span></a>", attachmentUrl, attachmentGuid, tooltip, imageTag, attachmentName);
                    }
                }

            case "attachmentsize":
                long size = ValidationHelper.GetLong(parameter, 0);
                return DataHelper.GetSizeString(size);
        }

        return parameter;
    }

    #endregion


    #region "Grid actions' events"

    protected void dfuElem_PreRender(object sender, EventArgs e)
    {
        DirectFileUploader dfuElem = (DirectFileUploader)sender;
        if (Enabled)
        {
            dfuElem.ForceLoad = true;
            dfuElem.FormGUID = FormGUID;
            dfuElem.AttachmentGroupGUID = GroupGUID;
            dfuElem.DocumentID = DocumentID;
            dfuElem.NodeParentNodeID = NodeParentNodeID;
            dfuElem.NodeClassName = NodeClassName;
            dfuElem.ResizeToWidth = ResizeToWidth;
            dfuElem.ResizeToHeight = ResizeToHeight;
            dfuElem.ResizeToMaxSideSize = ResizeToMaxSideSize;
            dfuElem.AllowedExtensions = AllowedExtensions;
            dfuElem.ImageUrl = ResolveUrl(GetImageUrl("Design/Controls/DirectUploader/upload.png"));
            dfuElem.ImageHeight = 16;
            dfuElem.ImageWidth = 16;
            dfuElem.InsertMode = false;
            dfuElem.ParentElemID = ClientID;
            dfuElem.CheckPermissions = CheckPermissions;
            dfuElem.IsLiveSite = IsLiveSite;
            dfuElem.UploadMode = MultifileUploaderModeEnum.DirectSingle;
            dfuElem.MaxNumberToUpload = 1;
            dfuElem.Height = 16;
            dfuElem.Width = 16;
        }
        else
        {
            dfuElem.Visible = false;
        }
    }


    protected void webdavElem_PreRender(object sender, EventArgs e)
    {
        WebDAVEditControl wdc = sender as WebDAVEditControl;
        if (wdc != null)
        {
            if (!Enabled)
            {
                wdc.Enabled = false;
            }
        }
    }


    protected void imgUpdate_PreRender(object sender, EventArgs e)
    {
        ImageButton imgUpdate = (ImageButton)sender;
        if (!Enabled)
        {
            imgUpdate.ImageUrl = ResolveUrl(GetImageUrl("Design/Controls/DirectUploader/uploaddisabled.png"));
            imgUpdate.Style.Add("cursor", "default");
            imgUpdate.CssClass = "InlineIcon";
            imgUpdate.Enabled = false;
        }
        else
        {
            imgUpdate.Visible = false;
        }
    }


    protected void img_PreRender(object sender, EventArgs e)
    {
        ImageButton img = (ImageButton)sender;

        if (CMSContext.CurrentUser.IsAuthenticated())
        {
            if (!Enabled)
            {
                // Disable edit icon
                img.ImageUrl = GetImageUrl("Design/Controls/UniGrid/Actions/editdisabled.png");
                img.Enabled = false;
                img.Style.Add("cursor", "default");
            }
            else
            {
                string[] args = img.AlternateText.Split('|');
                string strForm = (FormGUID == Guid.Empty) ? string.Empty : FormGUID.ToString();
                string form = null;

                if (!String.IsNullOrEmpty(strForm))
                {
                    form = String.Format("&formguid={0}&parentid={1}", strForm, NodeParentNodeID);
                }
                else
                {
                    if (Node != null)
                    {
                        form += "&siteid=" + Node.NodeSiteID;
                    }
                }

                string isImage = ImageHelper.IsSupportedByImageEditor(args[0]) ? "true" : "false";
                // Prepare parameters
                string parameters = String.Format("?attachmentGUID={0}&versionHistoryID={1}{2}&clientid={3}&refresh=1", args[1], VersionHistoryID, form, ClientID);
                // Create security hash
                string validationHash = QueryHelper.GetHash(parameters);

                img.Attributes.Add("onclick", String.Format("Edit_{0}('{1}', '{2}', '{3}', {4}, '{5}', {6});return false;", ClientID, args[1], strForm, VersionHistoryID, NodeParentNodeID, validationHash, isImage));
            }

            img.AlternateText = GetString("general.edit");
        }
        else
        {
            img.Visible = false;
        }
    }


    protected void imgDown_PreRender(object sender, EventArgs e)
    {
        ImageButton imgDown = (ImageButton)sender;
        if (!Enabled || !AllowChangeOrder)
        {
            // Disable move down icon in case that editing is not allowed
            imgDown.ImageUrl = GetImageUrl("Design/Controls/UniGrid/Actions/downdisabled.png");
            imgDown.Enabled = false;
            imgDown.Style.Add("cursor", "default");
        }
    }


    protected void imgUp_PreRender(object sender, EventArgs e)
    {
        ImageButton imgUp = (ImageButton)sender;
        if (!Enabled || !AllowChangeOrder)
        {
            // Disable move up icon in case that editing is not allowed
            imgUp.ImageUrl = GetImageUrl("Design/Controls/UniGrid/Actions/updisabled.png");
            imgUp.Enabled = false;
            imgUp.Style.Add("cursor", "default");
        }
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

    #endregion


    #region "Helper methods"

    /// <summary>
    /// Gets GUID value from DataRowView.
    /// </summary>
    /// <param name="drv">Row cell</param>
    /// <returns>GUID of attachment</returns>
    protected static Guid GetAttachmentGuid(DataRowView drv)
    {
        // Get GUID of attachment
        return ValidationHelper.GetGuid(DataHelper.GetDataRowViewValue(drv, "AttachmentGUID"), Guid.Empty);
    }


    /// <summary>
    /// Gets attachment name from DataRowView.
    /// </summary>
    /// <param name="drv">Row cell</param>
    /// <returns>Attachment name</returns>
    protected static string GetAttachmentName(DataRowView drv)
    {
        // Get GUID of attachment
        return ValidationHelper.GetString(DataHelper.GetDataRowViewValue(drv, "AttachmentName"), string.Empty);
    }


    /// <summary>
    /// Gets extension value from DataRowView.
    /// </summary>
    /// <param name="drv">Row view</param>
    /// <returns>Extension of attachment</returns>
    protected static string GetAttachmentExtension(DataRowView drv)
    {
        // Get extension of attachment
        return ValidationHelper.GetString(DataHelper.GetDataRowViewValue(drv, "AttachmentExtension"), string.Empty);
    }

    #endregion
}
