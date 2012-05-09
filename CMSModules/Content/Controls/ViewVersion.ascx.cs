using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Data;
using System.Text;

using CMS.GlobalHelper;
using CMS.TreeEngine;
using CMS.CMSHelper;
using CMS.WorkflowEngine;
using CMS.DataEngine;
using CMS.SettingsProvider;
using CMS.FormEngine;
using CMS.SiteProvider;
using CMS.UIControls;
using CMS.ExtendedControls;
using CMS.Controls;
using CMS.EventLog;

using TreeNode = CMS.TreeEngine.TreeNode;
using TimeZoneInfo = CMS.SiteProvider.TimeZoneInfo;

public partial class CMSModules_Content_Controls_ViewVersion : VersionHistoryControl, IPostBackEventHandler
{
    #region "Variables"

    private TreeNode mCompareNode = null;
    private FormInfo fi = null;

    private int versionHistoryId = 0;
    private int versionCompare = 0;

    private AttachmentsDataSource dsAttachments = null;
    private AttachmentsDataSource dsAttachmentsCompare = null;

    private Hashtable attachments = null;
    private Hashtable attachmentsCompare = null;

    private bool noCompare = false;
    private bool even = true;

    private TimeZoneInfo usedTimeZone = null;
    private TimeZoneInfo serverTimeZone = null;

    /// <summary>
    /// Column marked as unsorted - default string.
    /// </summary>
    private const string UNSORTED = "##UNSORTED##";

    /// <summary>
    /// Predefined DateTime format.
    /// </summary>
    private const string DATE_FORMAT = " GMT{0: + 00.00; - 00.00}";

    #endregion


    #region "Properties"

    /// <summary>
    /// Current edited node.
    /// </summary>
    public override TreeNode Node
    {
        get
        {
            return mNode;
        }
        set
        {
            mNode = value;
        }
    }


    /// <summary>
    /// Compare node.
    /// </summary>
    public TreeNode CompareNode
    {
        get
        {
            return mCompareNode;
        }
        set
        {
            mCompareNode = value;
        }
    }

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Register WOpener script
        ScriptHelper.RegisterWOpenerScript(Page);

        if (QueryHelper.GetBoolean("rollbackok", false))
        {
            lblInfo.Visible = true;
            lblInfo.Text = GetString("VersionProperties.RollbackOK");
        }

        noCompare = QueryHelper.GetBoolean("noCompare", false);
        versionHistoryId = QueryHelper.GetInteger("versionhistoryid", 0);
        versionCompare = QueryHelper.GetInteger("compareHistoryId", 0);

        // Converting modified time to correct time zone
        TimeZoneHelper.GetCurrentTimeZoneDateTimeString(DateTime.Now, CurrentUser, CMSContext.CurrentSite, out usedTimeZone);
        serverTimeZone = TimeZoneHelper.ServerTimeZone;

        // No comparing available in Recycle bin
        pnlControl.Visible = !noCompare;

        if (versionHistoryId > 0)
        {
            try
            {
                // Get original version of document
                Node = VersionManager.GetVersion(versionHistoryId, null);
                CompareNode = VersionManager.GetVersion(versionCompare, null);

                if (Node != null)
                {
                    // Check read permissions
                    if (CMSContext.CurrentUser.IsAuthorizedPerDocument(Node, NodePermissionsEnum.Read) != AuthorizationResultEnum.Allowed)
                    {
                        RedirectToAccessDenied(String.Format(GetString("cmsdesk.notauthorizedtoreaddocument"), Node.NodeAliasPath));
                    }

                    if (!RequestHelper.IsPostBack())
                    {
                        LoadDropDown(Node.DocumentID, versionHistoryId);
                        ReloadData();
                    }

                    drpCompareTo.SelectedIndexChanged += drpCompareTo_SelectedIndexChanged;
                }
                else
                {
                    lblInfo.Text = GetString("editeddocument.notexists");
                    plcLabels.Visible = true;
                    lblInfo.Visible = true;
                    pnlAdditionalControls.Visible = false;
                }
            }
            catch (Exception ex)
            {
                if (Node == null)
                {
                    RedirectToInformation(GetString("editeddocument.notexists"));
                }
                else
                {
                    lblError.Text = GetString("document.viewversionerror") + " " + ex.Message;
                }

                EventLogProvider.LogException("Content", "VIEWVERSION", ex);
            }
        }
    }


    /// <summary>
    /// Reloads control with new data.
    /// </summary>
    private void ReloadData()
    {
        tblDocument.Rows.Clear();

        DataClassInfo ci = DataClassInfoProvider.GetDataClass(Node.NodeClassName);
        if (ci != null)
        {
            fi = FormHelper.GetFormInfo(ci.ClassName, false);

            TableHeaderCell labelCell = new TableHeaderCell();
            TableHeaderCell valueCell = null;
            TableHeaderCell valueCompare = null;

            // Add header column with version number
            if (CompareNode == null)
            {
                labelCell.Text = GetString("General.FieldName");
                labelCell.EnableViewState = false;
                valueCell = new TableHeaderCell();
                valueCell.EnableViewState = false;
                valueCell.Text = GetString("General.Value");

                // Add table header
                AddRow(labelCell, valueCell, "UniGridHead", false);
            }
            else
            {
                labelCell.Text = GetString("lock.versionnumber");
                valueCell = GetRollbackTableHeaderCell("source", Node.DocumentID, versionHistoryId);
                valueCompare = GetRollbackTableHeaderCell("compare", CompareNode.DocumentID, versionCompare);

                // Add table header
                AddRow(labelCell, valueCell, valueCompare, true, "UniGridHead", false);
            }

            if (ci.ClassIsCoupledClass)
            {
                // Add coupled class fields
                IDataClass coupleClass = DataClassFactory.NewDataClass(Node.NodeClassName);
                if (coupleClass != null)
                {
                    foreach (string col in coupleClass.StructureInfo.ColumnNames)
                    {
                        // If comparing with other version and current coupled column is not versioned do not display it
                        if (!((CompareNode != null) && !(VersionManager.IsVersionedCoupledColumn(Node.NodeClassName, col))))
                        {
                            AddField(Node, CompareNode, col);
                        }
                    }
                }
            }

            // Add versioned document class fields
            IDataClass docClass = DataClassFactory.NewDataClass("cms.document");
            if (docClass != null)
            {
                foreach (string col in docClass.StructureInfo.ColumnNames)
                {
                    // If comparing with other version and current document column is not versioned do not display it
                    // One exception is DocumentNamePath column which will be displayed even if it is not marked as a versioned column
                    if (!((CompareNode != null) && (!(VersionManager.IsVersionedDocumentColumn(col) || (col.ToLower() == "documentnamepath")))))
                    {
                        AddField(Node, CompareNode, col);
                    }
                }
            }

            // Add versioned document class fields
            IDataClass treeClass = DataClassFactory.NewDataClass("cms.tree");
            if (treeClass != null)
            {
                foreach (string col in treeClass.StructureInfo.ColumnNames)
                {
                    // Do not display cms_tree columns when comparing with other version
                    // cms_tree columns are not versioned
                    if (CompareNode == null)
                    {
                        AddField(Node, CompareNode, col);
                    }
                }
            }

            // Add unsorted attachments to the table
            AddField(Node, CompareNode, UNSORTED);
        }
    }


    /// <summary>
    /// Gets new table header cell which contains label and rollback image.
    /// </summary>
    /// <param name="suffixID">ID suffix</param>
    /// <param name="documentID">Document ID</param>
    /// <param name="versionID">Version history ID</param>
    /// <param name="action">Action</param>
    private TableHeaderCell GetRollbackTableHeaderCell(string suffixID, int documentID, int versionID)
    {
        TableHeaderCell tblHeaderCell = new TableHeaderCell();
        tblHeaderCell.EnableViewState = false;

        string tooltip = GetString("history.versionrollbacktooltip");

        // Label
        Label lblValue = new Label();
        lblValue.ID = "lbl" + suffixID;
        lblValue.Text = HTMLHelper.HTMLEncode(GetVersionNumber(documentID, versionID));
        lblValue.EnableViewState = false;

        // Panel
        Panel pnlLabel = new Panel();
        pnlLabel.ID = "pnlLabel" + suffixID;
        pnlLabel.CssClass = "LeftAlign";
        pnlLabel.Controls.Add(lblValue);
        pnlLabel.EnableViewState = false;

        // Rollback image
        Image imgRollback = new Image();
        imgRollback.ID = "imgRollback" + suffixID;
        imgRollback.AlternateText = tooltip;
        imgRollback.ToolTip = tooltip;
        imgRollback.EnableViewState = false;

        // Disable buttons according to permissions
        if (!CanApprove || !CanModify || (CheckedOutByAnotherUser && !CanCheckIn))
        {
            imgRollback.ImageUrl = GetImageUrl("Design/Controls/UniGrid/Actions/undodisabled.png");
            imgRollback.Enabled = false;
        }
        else
        {
            imgRollback.ImageUrl = GetImageUrl("Design/Controls/UniGrid/Actions/undo.png");
            imgRollback.Style.Add("cursor", "pointer");
        }

        // Prepare onclick script
        string confirmScript = "if (confirm(" + ScriptHelper.GetString(GetString("Unigrid.VersionHistory.Actions.Rollback.Confirmation")) + ")) { ";
        confirmScript += Page.ClientScript.GetPostBackEventReference(this, versionID.ToString()) + "; return false; }";
        imgRollback.Attributes.Add("onclick", confirmScript);

        // Rollback panel 
        Panel pnlImage = new Panel();
        pnlImage.EnableViewState = false;
        pnlImage.ID = "pnlRollback" + suffixID;
        pnlImage.CssClass = "RightAlign";
        pnlImage.Controls.Add(imgRollback);

        tblHeaderCell.Controls.Add(pnlLabel);
        tblHeaderCell.Controls.Add(pnlImage);

        return tblHeaderCell;
    }


    /// <summary>
    /// Loads dropdown list with versions.
    /// </summary>
    /// <param name="documentId">ID of current document</param>
    /// <param name="versionHistoryId">ID of current version history</param>
    private void LoadDropDown(int documentId, int versionHistoryId)
    {
        DataSet ds = VersionManager.GetDocumentHistory(documentId, "VersionHistoryID !=" + versionHistoryId, "ModifiedWhen DESC, VersionNumber DESC", -1, "VersionHistoryID, VersionNumber, ModifiedWhen");
        string version = null;

        // Converting modified time to corect time zone
        if (!DataHelper.DataSourceIsEmpty(ds))
        {
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                version = ValidationHelper.GetString(dr["VersionNumber"], null);
                version += " (" + GetUserDate(dr["ModifiedWhen"]) + ")";

                drpCompareTo.Items.Add(new ListItem(version, ValidationHelper.GetString(dr["VersionHistoryID"], "0")));
            }
        }

        // If history to be compared is available
        if (drpCompareTo.Items.Count > 0)
        {
            drpCompareTo.Items.Insert(0, GetString("history.select"));
        }
        // Otherwise hide dropdown list
        else
        {
            pnlControl.Visible = false;
        }

        // Pre-select dropdown list
        if (drpCompareTo.Items.FindByValue(versionCompare.ToString()) != null)
        {
            drpCompareTo.SelectedValue = versionCompare.ToString();
        }
    }


    /// <summary>
    /// Returns string with converted DateTime parameters according to specific user timezone settings.
    /// </summary>
    private string GetUserDate(object dateTimeVal)
    {
        DateTime date = ValidationHelper.GetDateTime(dateTimeVal, DataHelper.DATETIME_NOT_SELECTED);
        return TimeZoneHelper.ConvertTimeZoneDateTime(date, serverTimeZone, usedTimeZone) + TimeZoneHelper.GetGMTStringOffset(DATE_FORMAT, usedTimeZone);
    }


    /// <summary>
    /// Returns version number with date for given document.
    /// </summary>
    private string GetVersionNumber(int documentId, int version)
    {
        DataSet ds = VersionManager.GetDocumentHistory(documentId, "VersionHistoryID =" + version, null, 1, "VersionNumber, ModifiedWhen");

        if (DataHelper.DataSourceIsEmpty(ds))
        {
            return null;
        }
        else
        {
            string versionNumber = ValidationHelper.GetString(ds.Tables[0].Rows[0]["VersionNumber"], null);
            versionNumber += " (" + GetUserDate(ds.Tables[0].Rows[0]["ModifiedWhen"]) + ")";
            return versionNumber;
        }
    }


    /// <summary>
    /// Adds the field to the form.
    /// </summary>
    /// <param name="node">Document node</param>
    /// <param name="compareNode">Document compare node</param>
    /// <param name="columnName">Column name</param>
    private void AddField(TreeNode node, TreeNode compareNode, string columnName)
    {
        FormFieldInfo ffi = null;
        if (fi != null)
        {
            ffi = fi.GetFormField(columnName);
        }

        TableCell valueCell = new TableCell();
        valueCell.EnableViewState = false;
        TableCell valueCompare = new TableCell();
        valueCompare.EnableViewState = false;
        TableCell labelCell = new TableCell();
        labelCell.EnableViewState = false;
        TextComparison comparefirst = null;
        TextComparison comparesecond = null;
        bool switchSides = true;
        bool loadValue = true;
        bool empty = true;
        bool allowLabel = true;

        // Get the caption
        if ((columnName == UNSORTED) || (ffi != null))
        {
            AttachmentInfo aiCompare = null;

            // Compare attachments
            if ((columnName == UNSORTED) || (ffi.DataType == FormFieldDataTypeEnum.DocumentAttachments))
            {
                allowLabel = false;

                string title = null;
                if (columnName == UNSORTED)
                {
                    title = GetString("attach.unsorted") + ":";
                }
                else
                {
                    title = (String.IsNullOrEmpty(ffi.Caption) ? ffi.Name : ffi.Caption) + ":";
                }

                // Prepare DataSource for original node
                loadValue = false;

                dsAttachments = new AttachmentsDataSource();
                dsAttachments.DocumentVersionHistoryID = versionHistoryId;
                if (columnName != UNSORTED)
                {
                    dsAttachments.AttachmentGroupGUID = ffi.Guid;
                }
                dsAttachments.Path = node.NodeAliasPath;
                dsAttachments.CultureCode = CMSContext.PreferredCultureCode;
                SiteInfo si = SiteInfoProvider.GetSiteInfo(node.NodeSiteID);
                dsAttachments.SiteName = si.SiteName;
                dsAttachments.OrderBy = "AttachmentOrder, AttachmentName, AttachmentHistoryID";
                dsAttachments.SelectedColumns = "AttachmentHistoryID, AttachmentGUID, AttachmentImageWidth, AttachmentImageHeight, AttachmentExtension, AttachmentName, AttachmentSize, AttachmentOrder, AttachmentTitle, AttachmentDescription";
                dsAttachments.IsLiveSite = false;
                dsAttachments.DataSource = null;
                dsAttachments.DataBind();

                // Get the attachments table
                attachments = GetAttachmentsTable((DataSet)dsAttachments.DataSource, versionHistoryId, node.DocumentID);

                // Prepare datasource for compared node
                if (compareNode != null)
                {
                    dsAttachmentsCompare = new AttachmentsDataSource();
                    dsAttachmentsCompare.DocumentVersionHistoryID = versionCompare;
                    if (columnName != UNSORTED)
                    {
                        dsAttachmentsCompare.AttachmentGroupGUID = ffi.Guid;
                    }
                    dsAttachmentsCompare.Path = compareNode.NodeAliasPath;
                    dsAttachmentsCompare.CultureCode = CMSContext.PreferredCultureCode;
                    dsAttachmentsCompare.SiteName = si.SiteName;
                    dsAttachmentsCompare.OrderBy = "AttachmentOrder, AttachmentName, AttachmentHistoryID";
                    dsAttachmentsCompare.SelectedColumns = "AttachmentHistoryID, AttachmentGUID, AttachmentImageWidth, AttachmentImageHeight, AttachmentExtension, AttachmentName, AttachmentSize, AttachmentOrder, AttachmentTitle, AttachmentDescription";
                    dsAttachmentsCompare.IsLiveSite = false;
                    dsAttachmentsCompare.DataSource = null;
                    dsAttachmentsCompare.DataBind();

                    // Get the table to compare
                    attachmentsCompare = GetAttachmentsTable((DataSet)dsAttachmentsCompare.DataSource, versionCompare, node.DocumentID);

                    // Switch the sides if older version is on the right
                    if (versionHistoryId > versionCompare)
                    {
                        Hashtable dummy = attachmentsCompare;
                        attachmentsCompare = attachments;
                        attachments = dummy;
                    }

                    // Add comparison
                    AddTableComparison(attachments, attachmentsCompare, "<strong>" + title + "</strong>", true, true);
                }
                else
                {
                    // Normal display
                    if (attachments.Count != 0)
                    {
                        bool first = true;
                        string itemValue = null;

                        foreach (DictionaryEntry item in attachments)
                        {
                            itemValue = ValidationHelper.GetString(item.Value, null);
                            if (!String.IsNullOrEmpty(itemValue))
                            {
                                valueCell = new TableCell();
                                labelCell = new TableCell();

                                if (first)
                                {
                                    labelCell.Text = "<strong>" + String.Format(title, item.Key) + "</strong>";
                                    first = false;
                                }
                                valueCell.Text = itemValue;

                                AddRow(labelCell, valueCell, null, even);
                                even = !even;
                            }
                        }
                    }
                }
            }
            // Compare single file attachment
            else if (ffi.DataType == FormFieldDataTypeEnum.File)
            {
                // Get the attachment
                AttachmentInfo ai = DocumentHelper.GetAttachment(ValidationHelper.GetGuid(node.GetValue(columnName), Guid.Empty), versionHistoryId, TreeProvider, false);

                if (compareNode != null)
                {
                    aiCompare = DocumentHelper.GetAttachment(ValidationHelper.GetGuid(compareNode.GetValue(columnName), Guid.Empty), versionCompare, TreeProvider, false);
                }

                loadValue = false;
                empty = true;

                // Prepare text comparison controls
                if ((ai != null) || (aiCompare != null))
                {
                    string textorig = null;
                    if (ai != null)
                    {
                        textorig = CreateAttachment(ai.Generalized.DataClass, versionHistoryId);
                    }
                    string textcompare = null;
                    if (aiCompare != null)
                    {
                        textcompare = CreateAttachment(aiCompare.Generalized.DataClass, versionCompare);
                    }

                    comparefirst = new TextComparison();
                    comparefirst.SynchronizedScrolling = false;
                    comparefirst.IgnoreHTMLTags = true;
                    comparefirst.ConsiderHTMLTagsEqual = true;
                    comparefirst.BalanceContent = false;

                    comparesecond = new TextComparison();
                    comparesecond.SynchronizedScrolling = false;
                    comparesecond.IgnoreHTMLTags = true;

                    // Source text must be older version
                    if (versionHistoryId < versionCompare)
                    {
                        comparefirst.SourceText = textorig;
                        comparefirst.DestinationText = textcompare;
                    }
                    else
                    {
                        comparefirst.SourceText = textcompare;
                        comparefirst.DestinationText = textorig;
                    }

                    comparefirst.PairedControl = comparesecond;
                    comparesecond.RenderingMode = TextComparisonTypeEnum.DestinationText;

                    valueCell.Controls.Add(comparefirst);
                    valueCompare.Controls.Add(comparesecond);
                    switchSides = false;

                    // Add both cells
                    if (compareNode != null)
                    {
                        AddRow(labelCell, valueCell, valueCompare, switchSides, null, even);
                    }
                    // Add one cell only
                    else
                    {
                        valueCell.Controls.Clear();
                        Literal ltl = new Literal();
                        ltl.Text = textorig;
                        valueCell.Controls.Add(ltl);
                        AddRow(labelCell, valueCell, null, even);
                    }
                    even = !even;
                }
            }
        }

        if (allowLabel && (labelCell.Text == ""))
        {
            labelCell.Text = "<strong>" + columnName + ":</strong>";
        }

        if (loadValue)
        {
            string textcompare = null;

            switch (columnName.ToLower())
            {
                // Document content - display content of editable regions and editable web parts
                case "documentcontent":
                    EditableItems ei = new EditableItems();
                    ei.LoadContentXml(ValidationHelper.GetString(node.GetValue(columnName), ""));

                    // Add text comparison control
                    if (compareNode != null)
                    {
                        EditableItems eiCompare = new EditableItems();
                        eiCompare.LoadContentXml(ValidationHelper.GetString(compareNode.GetValue(columnName), ""));

                        // Create editable regions comparison
                        Hashtable hashtable;
                        Hashtable hashtableCompare;

                        // Source text must be older version
                        if (versionHistoryId < versionCompare)
                        {
                            hashtable = ei.EditableRegions;
                            hashtableCompare = eiCompare.EditableRegions;
                        }
                        else
                        {
                            hashtable = eiCompare.EditableRegions;
                            hashtableCompare = ei.EditableRegions;
                        }

                        // Add comparison
                        AddTableComparison(hashtable, hashtableCompare, "<strong>" + columnName + " ({0}):</strong>", false, false);

                        // Create editable webparts comparison
                        // Source text must be older version
                        if (versionHistoryId < versionCompare)
                        {
                            hashtable = ei.EditableWebParts;
                            hashtableCompare = eiCompare.EditableWebParts;
                        }
                        else
                        {
                            hashtable = eiCompare.EditableWebParts;
                            hashtableCompare = ei.EditableWebParts;
                        }

                        // Add comparison
                        AddTableComparison(hashtable, hashtableCompare, "<strong>" + columnName + " ({0}):</strong>", false, false);
                    }
                    // No compare node
                    else
                    {
                        // Editable regions
                        Hashtable hashtable = ei.EditableRegions;
                        if (hashtable.Count != 0)
                        {
                            string regionValue = null;
                            string regionKey = null;

                            foreach (DictionaryEntry region in hashtable)
                            {
                                regionValue = ValidationHelper.GetString(region.Value, null);
                                if (!String.IsNullOrEmpty(regionValue))
                                {
                                    regionKey = ValidationHelper.GetString(region.Key, null);

                                    valueCell = new TableCell();
                                    labelCell = new TableCell();

                                    labelCell.Text = "<strong>" + columnName + " (" + MultiKeyHashtable.GetFirstKey(regionKey) + "):</strong>";
                                    valueCell.Text = HttpUtility.HtmlDecode(HTMLHelper.StripTags(regionValue, false));

                                    AddRow(labelCell, valueCell, null, even);
                                    even = !even;
                                }
                            }
                        }

                        // Editable web parts
                        hashtable = ei.EditableWebParts;
                        if (hashtable.Count != 0)
                        {
                            string partValue = null;
                            string partKey = null;

                            foreach (DictionaryEntry part in hashtable)
                            {
                                partValue = ValidationHelper.GetString(part.Value, null);
                                if (!String.IsNullOrEmpty(partValue))
                                {
                                    partKey = ValidationHelper.GetString(part.Key, null);
                                    valueCell = new TableCell();
                                    labelCell = new TableCell();

                                    labelCell.Text = "<strong>" + columnName + " (" + MultiKeyHashtable.GetFirstKey(partKey) + "):</strong>";
                                    valueCell.Text = HttpUtility.HtmlDecode(HTMLHelper.StripTags(partValue, false));

                                    AddRow(labelCell, valueCell, null, even);
                                    even = !even;
                                }
                            }
                        }
                    }

                    break;

                // Others, display the string value
                default:
                    // Shift date time values to user time zone
                    object origobject = node.GetValue(columnName);
                    string textorig = null;
                    if (origobject is DateTime)
                    {
                        TimeZoneInfo usedTimeZone = null;
                        textorig = TimeZoneHelper.GetCurrentTimeZoneDateTimeString(ValidationHelper.GetDateTime(origobject, DateTimeHelper.ZERO_TIME), CurrentUser, CMSContext.CurrentSite, out usedTimeZone);
                    }
                    else
                    {
                        textorig = ValidationHelper.GetString(origobject, "");
                    }

                    // Add text comparison control
                    if (compareNode != null)
                    {
                        // Shift date time values to user time zone
                        object compareobject = compareNode.GetValue(columnName);
                        if (compareobject is DateTime)
                        {
                            TimeZoneInfo usedTimeZone = null;
                            textcompare = TimeZoneHelper.GetCurrentTimeZoneDateTimeString(ValidationHelper.GetDateTime(compareobject, DateTimeHelper.ZERO_TIME), CurrentUser, CMSContext.CurrentSite, out usedTimeZone);
                        }
                        else
                        {
                            textcompare = ValidationHelper.GetString(compareobject, "");
                        }

                        comparefirst = new TextComparison();
                        comparefirst.SynchronizedScrolling = false;

                        comparesecond = new TextComparison();
                        comparesecond.SynchronizedScrolling = false;
                        comparesecond.RenderingMode = TextComparisonTypeEnum.DestinationText;

                        // Source text must be older version
                        if (versionHistoryId < versionCompare)
                        {
                            comparefirst.SourceText = HttpUtility.HtmlDecode(HTMLHelper.StripTags(textorig, false));
                            comparefirst.DestinationText = HttpUtility.HtmlDecode(HTMLHelper.StripTags(textcompare, false));
                        }
                        else
                        {
                            comparefirst.SourceText = HttpUtility.HtmlDecode(HTMLHelper.StripTags(textcompare, false));
                            comparefirst.DestinationText = HttpUtility.HtmlDecode(HTMLHelper.StripTags(textorig, false));
                        }

                        comparefirst.PairedControl = comparesecond;

                        if (Math.Max(comparefirst.SourceText.Length, comparefirst.DestinationText.Length) < 100)
                        {
                            comparefirst.BalanceContent = false;
                        }

                        valueCell.Controls.Add(comparefirst);
                        valueCompare.Controls.Add(comparesecond);
                        switchSides = false;
                    }
                    else
                    {
                        valueCell.Text = HttpUtility.HtmlDecode(HTMLHelper.StripTags(textorig, false));
                    }

                    empty = (String.IsNullOrEmpty(textorig)) && (String.IsNullOrEmpty(textcompare));
                    break;
            }
        }

        if (!empty)
        {
            if (compareNode != null)
            {
                AddRow(labelCell, valueCell, valueCompare, switchSides, null, even);
                even = !even;
            }
            else
            {
                AddRow(labelCell, valueCell, null, even);
                even = !even;
            }
        }
    }


    /// <summary>
    /// Gets the hashtable of attachments from the DataSet.
    /// </summary>
    /// <param name="ds">Source DataSet</param>
    /// <param name="versionId">Version history ID</param>
    /// <param name="documentId">Document ID</param>
    protected Hashtable GetAttachmentsTable(DataSet ds, int versionId, int documentId)
    {
        Hashtable result = new Hashtable();
        if (!DataHelper.DataSourceIsEmpty(ds))
        {
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                // Get attachment GUID
                Guid attachmentGuid = ValidationHelper.GetGuid(dr["AttachmentGUID"], Guid.Empty);
                result[attachmentGuid.ToString()] = CreateAttachment(new DataRowContainer(dr), versionId);
            }
        }

        return result;
    }


    /// <summary>
    /// Adds the table comparisson for matching objects.
    /// </summary>
    /// <param name="hashtable">Left side table</param>
    /// <param name="hashtableCompare">Right side table</param>
    /// <param name="titleFormat">Title format string</param>
    /// <param name="attachments">If true, the HTML code is kept (attachment comparison)</param>
    /// <param name="renderOnlyFirstTitle">If true, only first title is rendered</param>
    protected void AddTableComparison(Hashtable hashtable, Hashtable hashtableCompare, string titleFormat, bool attachments, bool renderOnlyFirstTitle)
    {
        TableCell valueCell = new TableCell();
        TableCell valueCompare = new TableCell();
        TableCell labelCell = new TableCell();
        TextComparison comparefirst = null;
        TextComparison comparesecond = null;

        bool firstTitle = true;

        // Go through left column regions
        if (hashtable != null)
        {
            foreach (DictionaryEntry entry in hashtable)
            {
                object value = entry.Value;
                if (value != null)
                {
                    // Initialize comparators
                    comparefirst = new TextComparison();
                    comparefirst.SynchronizedScrolling = false;

                    comparesecond = new TextComparison();
                    comparesecond.SynchronizedScrolling = false;
                    comparesecond.RenderingMode = TextComparisonTypeEnum.DestinationText;

                    if (attachments)
                    {
                        comparefirst.IgnoreHTMLTags = true;
                        comparefirst.ConsiderHTMLTagsEqual = true;

                        comparesecond.IgnoreHTMLTags = true;
                    }

                    comparefirst.PairedControl = comparesecond;

                    // Initialize cells
                    valueCell = new TableCell();
                    valueCompare = new TableCell();
                    labelCell = new TableCell();

                    string key = ValidationHelper.GetString(entry.Key, null);
                    string strValue = ValidationHelper.GetString(value, null);
                    if (firstTitle || !renderOnlyFirstTitle)
                    {
                        labelCell.Text = String.Format(titleFormat, MultiKeyHashtable.GetFirstKey(key));
                        firstTitle = false;
                    }

                    if (attachments)
                    {
                        comparefirst.SourceText = strValue;
                    }
                    else
                    {
                        comparefirst.SourceText = HttpUtility.HtmlDecode(HTMLHelper.StripTags(strValue, false));
                    }

                    if ((hashtableCompare != null) && hashtableCompare.Contains(key))
                    {
                        // Compare to the existing other version
                        string compareKey = ValidationHelper.GetString(hashtableCompare[key], null);
                        if (attachments)
                        {
                            comparefirst.DestinationText = compareKey;
                        }
                        else
                        {
                            comparefirst.DestinationText = HttpUtility.HtmlDecode(HTMLHelper.StripTags(compareKey, false));
                        }
                        hashtableCompare.Remove(key);
                    }
                    else
                    {
                        // Compare to an empty string
                        comparefirst.DestinationText = "";
                    }

                    // Do not balance content if too short
                    if (attachments)
                    {
                        comparefirst.BalanceContent = false;
                    }
                    else if (Math.Max(comparefirst.SourceText.Length, comparefirst.DestinationText.Length) < 100)
                    {
                        comparefirst.BalanceContent = false;
                    }

                    // Create cell comparison
                    valueCell.Controls.Add(comparefirst);
                    valueCompare.Controls.Add(comparesecond);

                    AddRow(labelCell, valueCell, valueCompare, false, null, even);
                    even = !even;
                }
            }
        }

        // Go through right column regions which left
        if (hashtableCompare != null)
        {
            foreach (DictionaryEntry entry in hashtableCompare)
            {
                object value = entry.Value;
                if (value != null)
                {
                    // Initialize comparators
                    comparefirst = new TextComparison();
                    comparefirst.SynchronizedScrolling = false;

                    comparesecond = new TextComparison();
                    comparesecond.SynchronizedScrolling = false;
                    comparesecond.RenderingMode = TextComparisonTypeEnum.DestinationText;

                    comparefirst.PairedControl = comparesecond;

                    if (attachments)
                    {
                        comparefirst.IgnoreHTMLTags = true;
                        comparefirst.ConsiderHTMLTagsEqual = true;

                        comparesecond.IgnoreHTMLTags = true;
                    }

                    // Initialize cells
                    valueCell = new TableCell();
                    valueCompare = new TableCell();
                    labelCell = new TableCell();

                    if (firstTitle || !renderOnlyFirstTitle)
                    {
                        labelCell.Text = String.Format(titleFormat, MultiKeyHashtable.GetFirstKey(ValidationHelper.GetString(entry.Key, null)));
                        firstTitle = false;
                    }

                    comparefirst.SourceText = "";
                    string strValue = ValidationHelper.GetString(value, null);
                    if (attachments)
                    {
                        comparefirst.DestinationText = strValue;
                    }
                    else
                    {
                        comparefirst.DestinationText = HttpUtility.HtmlDecode(HTMLHelper.StripTags(strValue, false));
                    }

                    if (attachments)
                    {
                        comparefirst.BalanceContent = false;
                    }
                    else if (Math.Max(comparefirst.SourceText.Length, comparefirst.DestinationText.Length) < 100)
                    {
                        comparefirst.BalanceContent = false;
                    }

                    // Create cell comparison
                    valueCell.Controls.Add(comparefirst);
                    valueCompare.Controls.Add(comparesecond);

                    AddRow(labelCell, valueCell, valueCompare, false, null, even);
                    even = !even;
                }
            }
        }
    }


    /// <summary>
    /// Returns new TableRow with CSS class set according to even/odd position in table.
    /// </summary>
    private TableRow CreateRow(string cssClass, bool even, TableCell labelCell)
    {
        TableRow newRow = new TableRow();

        // Set CSS
        if (!String.IsNullOrEmpty(cssClass))
        {
            newRow.CssClass = cssClass;
        }
        else if (even)
        {
            newRow.CssClass = "EvenRow";
        }
        else
        {
            newRow.CssClass = "OddRow";
        }

        labelCell.Wrap = false;
        newRow.Cells.Add(labelCell);


        return newRow;
    }


    /// <summary>
    /// Creates 2 column table.
    /// </summary>
    /// <param name="labelCell">Cell with label</param>
    /// <param name="valueCell">Cell with content</param>
    /// <returns>Returns TableRow object</returns>
    private TableRow AddRow(TableCell labelCell, TableCell valueCell, string cssClass, bool even)
    {
        TableRow newRow = CreateRow(cssClass, even, labelCell);

        valueCell.Width = new Unit(100, UnitType.Percentage);
        newRow.Cells.Add(valueCell);

        tblDocument.Rows.Add(newRow);
        return newRow;
    }


    /// <summary>
    /// Creates 3 column table. Older version must be always in the left column.
    /// </summary>
    /// <param name="labelCell">Cell with label</param>
    /// <param name="valueCell">Cell with content</param>
    /// <param name="compareCell">Cell with compare content</param>
    /// <param name="switchSides">Indicates if cells should be switched to match corresponding version</param>
    /// <param name="cssClass">CSS class</param>
    /// <param name="even">Determines whether row is odd or even</param>
    /// <returns>Returns TableRow object</returns>
    private TableRow AddRow(TableCell labelCell, TableCell valueCell, TableCell compareCell, bool switchSides, string cssClass, bool even)
    {
        TableRow newRow = CreateRow(cssClass, even, labelCell);

        const int cellWidth = 40;

        // Switch sides to match version
        if (switchSides)
        {
            // Older version must be in the left column
            if (versionHistoryId < versionCompare)
            {
                valueCell.Width = new Unit(cellWidth, UnitType.Percentage);
                newRow.Cells.Add(valueCell);

                compareCell.Width = new Unit(cellWidth, UnitType.Percentage);
                newRow.Cells.Add(compareCell);
            }
            else
            {
                compareCell.Width = new Unit(cellWidth, UnitType.Percentage);
                newRow.Cells.Add(compareCell);

                valueCell.Width = new Unit(cellWidth, UnitType.Percentage);
                newRow.Cells.Add(valueCell);
            }
        }
        // Do not switch sides
        else
        {
            valueCell.Width = new Unit(cellWidth, UnitType.Percentage);
            newRow.Cells.Add(valueCell);

            compareCell.Width = new Unit(cellWidth, UnitType.Percentage);
            newRow.Cells.Add(compareCell);
        }

        tblDocument.Rows.Add(newRow);
        return newRow;
    }


    /// <summary>
    /// Creates attachment string.
    /// </summary>
    private string CreateAttachment(IDataContainer dc, int versionId)
    {
        string result = null;
        if (dc != null)
        {
            // Get attachment GUID
            Guid attachmentGuid = ValidationHelper.GetGuid(dc.GetValue("AttachmentGUID"), Guid.Empty);

            // Get attachment extension
            string attachmentExt = ValidationHelper.GetString(dc.GetValue("AttachmentExtension"), null);
            string iconUrl = GetFileIconUrl(attachmentExt, "List");

            // Get link for attachment
            string attachmentUrl = null;
            string attName = ValidationHelper.GetString(dc.GetValue("AttachmentName"), null);
            attachmentUrl = CMSContext.ResolveUIUrl(TreePathUtils.GetAttachmentUrl(attachmentGuid, URLHelper.GetSafeFileName(attName, CMSContext.CurrentSiteName), versionId, null));

            // Ensure correct URL
            attachmentUrl = URLHelper.AddParameterToUrl(attachmentUrl, "sitename", CMSContext.CurrentSiteName);

            // Optionally trim attachment name
            string attachmentName = TextHelper.LimitLength(attName, 90);
            bool isImage = ImageHelper.IsImage(attachmentExt);

            // Tooltip
            string tooltip = null;
            if (isImage)
            {
                int attachmentWidth = ValidationHelper.GetInteger(dc.GetValue("AttachmentImageWidth"), 0);
                if (attachmentWidth > 300)
                {
                    attachmentWidth = 300;
                }
                tooltip = "onmouseout=\"UnTip()\" onmouseover=\"TipImage(" + attachmentWidth + ", '" + URLHelper.AddParameterToUrl(attachmentUrl, "width", "300") + "', " + ScriptHelper.GetString(HTMLHelper.HTMLEncode(attachmentName)) + ")\"";
            }

            string attachmentSize = SqlHelperClass.GetSizeString(ValidationHelper.GetLong(dc.GetValue("AttachmentSize"), 0));
            string title = ValidationHelper.GetString(dc.GetValue("AttachmentTitle"), string.Empty);
            string description = ValidationHelper.GetString(dc.GetValue("AttachmentDescription"), string.Empty);

            // Icon
            StringBuilder sb = new StringBuilder();
            sb.Append("<img class=\"Icon\" style=\"cursor: pointer;\"  src=\"", HTMLHelper.HTMLEncode(iconUrl), "\" alt=\"", HTMLHelper.HTMLEncode(attachmentName), "\" ", tooltip, " onclick=\"javascript: window.open(", ScriptHelper.GetString(attachmentUrl), "); return false;\" />");
            sb.Append(" ", HTMLHelper.HTMLEncode(attachmentName), " (", HTMLHelper.HTMLEncode(attachmentSize), ")<br />");
            sb.Append("<table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" class=\"NoBorderTable\" ><tr><td style=\"padding:1px;\"><strong>", GetString("general.title"), ":</strong></td><td style=\"padding:1px;\">", HTMLHelper.HTMLEncode(title), "</td></tr>");
            sb.Append("<tr><td style=\"padding:1px;\"><strong>", GetString("general.description"), ":</strong></td><td style=\"padding:1px;\">", HTMLHelper.HTMLEncode(description), "</td></tr></table><br/>"); result = sb.ToString();
        }

        return result;
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        if (Node != null)
        {
            plcLabels.Visible = !(string.IsNullOrEmpty(lblError.Text) && string.IsNullOrEmpty(lblInfo.Text));
        }
    }

    #endregion


    #region "Events"

    /// <summary>
    /// Dropdown list selection changed.
    /// </summary>
    private void drpCompareTo_SelectedIndexChanged(object sender, EventArgs e)
    {
        string url = URLHelper.CurrentURL;

        url = URLHelper.RemoveParameterFromUrl(url, "rollbackok");

        if (drpCompareTo.SelectedIndex == 0)
        {
            url = URLHelper.RemoveParameterFromUrl(url, "compareHistoryId");
        }
        else
        {
            url = URLHelper.AddParameterToUrl(url, "compareHistoryId", drpCompareTo.SelectedValue);
        }
        URLHelper.Redirect(url);
    }

    #endregion


    #region "IPostBackEventHandler Members"

    /// <summary>
    /// Raises event postback event.
    /// </summary>
    /// <param name="eventArgument">Argument</param>
    public void RaisePostBackEvent(string eventArgument)
    {
        int rollbackVersionId = ValidationHelper.GetInteger(eventArgument, 0);

        if (rollbackVersionId > 0)
        {
            if (Node == null)
            {
                return;
            }

            if (CheckedOutByUserID > 0)
            {
                // Document is checked out
                lblError.Text = GetString("VersionProperties.CannotRollbackCheckedOut");
            }
            else
            {
                // Check permissions
                if (!CanApprove || !CanModify)
                {
                    lblError.Text = String.Format(GetString("cmsdesk.notauthorizedtoeditdocument"), Node.NodeAliasPath);
                }
                else
                {
                    try
                    {
                        // Rollback version
                        int newVersionHistoryId = VersionManager.RollbackVersion(rollbackVersionId);

                        lblInfo.Text = GetString("VersionProperties.RollbackOK");

                        string url = URLHelper.CurrentURL;

                        // Add URL parameters
                        url = URLHelper.AddParameterToUrl(url, "versionHistoryId", newVersionHistoryId.ToString());
                        url = URLHelper.AddParameterToUrl(url, "compareHistoryId", versionCompare.ToString());
                        url = URLHelper.AddParameterToUrl(url, "rollbackok", "1");

                        // Prepare URL
                        url = ScriptHelper.GetString(URLHelper.ResolveUrl(url), true);

                        // Prepare script for refresh parent window and this dialog
                        StringBuilder builder = new StringBuilder();
                        builder.Append("if (wopener != null) {\n");
                        builder.Append("wopener.document.location.replace(wopener.document.location);}\n");
                        builder.Append("window.document.location.replace(" + url + ");");

                        string script = ScriptHelper.GetScript(builder.ToString());
                        ScriptHelper.RegisterStartupScript(this, typeof(string), "RefreshAndReload", script);
                    }
                    catch (Exception ex)
                    {
                        lblError.Text += GetString("versionproperties.rollbackerror");
                        EventLogProvider.LogException("Content", "ROLLBACKVERSION", ex);
                    }
                }
            }

            // Display form if error occurs
            if (!string.IsNullOrEmpty(lblError.Text))
            {
                ReloadData();
            }
        }
    }

    #endregion
}
