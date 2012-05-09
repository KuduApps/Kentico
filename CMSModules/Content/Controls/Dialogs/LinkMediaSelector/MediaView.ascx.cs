using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;

using CMS.UIControls;
using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.SettingsProvider;
using CMS.SiteProvider;
using CMS.TreeEngine;
using CMS.ExtendedControls;
using CMS.IO;

using TreeNode = CMS.TreeEngine.TreeNode;

public partial class CMSModules_Content_Controls_Dialogs_LinkMediaSelector_MediaView : MediaView
{
    #region "Private variables"

    private int mNodeParentNodeId = 0;
    private SiteInfo mSiteObj = null;
    private TreeNode mTreeNodeObj = null;

    protected string mSaveText = null;

    #endregion


    #region "Public properties"

    /// <summary>
    /// Gets or sets the value which determineds whtether to show the Parent button or not.
    /// </summary>
    public bool ShowParentButton
    {
        get
        {
            return plcParentButton.Visible;
        }
        set
        {
            plcParentButton.Visible = value;
        }
    }


    /// <summary>
    /// Gets or sets a view mode used to display files.
    /// </summary>
    public override DialogViewModeEnum ViewMode
    {
        get
        {
            return base.ViewMode;
        }
        set
        {
            base.ViewMode = value;
            innermedia.ViewMode = value;
        }
    }

    /// <summary>
    /// Gets or sets the OutputFormat (needed for correct dialog type reckognition).
    /// </summary>
    public OutputFormatEnum OutputFormat
    {
        get
        {
            return innermedia.OutputFormat;
        }
        set
        {
            innermedia.OutputFormat = value;
        }
    }


    /// <summary>
    /// Gets or sets text of the information label.
    /// </summary>
    public string InfoText
    {
        get
        {
            return innermedia.InfoText;
        }
        set
        {
            innermedia.InfoText = value;
        }
    }


    /// <summary>
    /// Gets currently selected page size.
    /// </summary>
    public int CurrentTopN
    {
        get
        {
            return innermedia.CurrentTopN;
        }
    }


    /// <summary>
    /// Gets or sets ID of the parent node.
    /// </summary>
    public int AtachmentNodeParentID
    {
        get
        {
            return mNodeParentNodeId;
        }
        set
        {
            mNodeParentNodeId = value;
        }
    }


    /// <summary>
    /// Gets or sets ID of the parent of the curently selected node.
    /// </summary>
    public int NodeParentID
    {
        get
        {
            return ValidationHelper.GetInteger(hdnLastNodeParentID.Value, 0);
        }
        set
        {
            hdnLastNodeParentID.Value = value.ToString();
        }
    }


    /// <summary>
    /// Gets the node attachments are related to.
    /// </summary>
    public TreeNode TreeNodeObj
    {
        get
        {
            return mTreeNodeObj;
        }
        set
        {
            mTreeNodeObj = value;
            innermedia.TreeNodeObj = value;
        }
    }


    /// <summary>
    /// Gets the site attachments are related to.
    /// </summary>
    public SiteInfo SiteObj
    {
        get
        {
            if (mSiteObj == null)
            {
                mSiteObj = TreeNodeObj != null ? SiteInfoProvider.GetSiteInfo(TreeNodeObj.NodeSiteID) : CMSContext.CurrentSite;
            }
            return mSiteObj;
        }
        set
        {
            mSiteObj = value;
        }
    }


    /// <summary>
    /// Indicates whether the content tree is displaying more than max tree nodes.
    /// </summary>
    public bool IsFullListingMode
    {
        get
        {
            return innermedia.IsFullListingMode;
        }
        set
        {
            innermedia.IsFullListingMode = value;
        }
    }

    #endregion


    #region "Page methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        // If processing the request should not continue
        if (StopProcessing)
        {
            Visible = false;
        }
        else
        {
            Visible = true;

            // Initialize controls
            SetupControls();
        }
    }

    #endregion


    #region "Public methods"

    /// <summary>
    /// Loads control's content.
    /// </summary>
    public void Reload()
    {
        // Initialize controls
        SetupControls();
        ReloadData();
    }


    /// <summary>
    /// Displays listing info message.
    /// </summary>
    /// <param name="infoMsg">Info message to display</param>
    public void DisplayListingInfo(string infoMsg)
    {
        if (!string.IsNullOrEmpty(infoMsg))
        {
            plcListingInfo.Visible = true;
            lblListingInfo.Text = infoMsg;
        }
    }

    #endregion

    public override bool Visible
    {
        get
        {
            return base.Visible;
        }
        set
        {
            base.Visible = value;
        }
    }


    #region "Private methods"

    /// <summary>
    /// Initializes all nested controls.
    /// </summary>
    private void SetupControls()
    {
        InitializeControlScripts();

        // Initialize inner view control
        innermedia.ViewMode = ViewMode;
        innermedia.DataSource = DataSource;
        innermedia.SelectableContent = SelectableContent;
        innermedia.SourceType = SourceType;
        innermedia.IsLiveSite = IsLiveSite;
        innermedia.NodeParentID = AtachmentNodeParentID;

        innermedia.ResizeToHeight = ResizeToHeight;
        innermedia.ResizeToMaxSideSize = ResizeToMaxSideSize;
        innermedia.ResizeToWidth = ResizeToWidth;

        // Set grid definition according source type
        string gridName = "";
        if (SourceType == MediaSourceEnum.DocumentAttachments)
        {
            gridName = "~/CMSModules/Content/Controls/Dialogs/LinkMediaSelector/AttachmentsListView.xml";
        }
        else
        {
            if ((OutputFormat == OutputFormatEnum.HTMLLink) || (OutputFormat == OutputFormatEnum.BBLink))
            {
                gridName = "~/CMSModules/Content/Controls/Dialogs/LinkMediaSelector/ContentListView_Link.xml";
            }
            else
            {
                gridName = "~/CMSModules/Content/Controls/Dialogs/LinkMediaSelector/ContentListView.xml";
            }
        }
        innermedia.ListViewControl.GridName = gridName;
        ((CMSAdminControls_UI_UniGrid_UniGrid)(innermedia.ListViewControl)).OnPageChanged += ListViewControl_OnPageChanged;

        // Set inner control binding columns
        innermedia.FileIdColumn = "AttachmentGUID";
        innermedia.FileNameColumn = (SourceType == MediaSourceEnum.DocumentAttachments) ? "AttachmentName" : "DocumentName";
        innermedia.FileExtensionColumn = "AttachmentExtension";
        innermedia.FileSizeColumn = "AttachmentSize";
        innermedia.FileWidthColumn = "AttachmentImageWidth";
        innermedia.FileHeightColumn = "AttachmentImageHeight";

        // Register for inner media events
        innermedia.GetArgumentSet += innermedia_GetArgumentSet;
        innermedia.GetListItemUrl += innermedia_GetListItemUrl;
        innermedia.GetTilesThumbsItemUrl += innermedia_GetTilesThumbsItemUrl;

        // Parent directory button
        if ((SourceType == MediaSourceEnum.Content) && ShowParentButton)
        {
            plcParentButton.Visible = true;
            imgParent.ImageUrl = GetImageUrl("Design/Controls/Dialogs/parent.png");
            mSaveText = GetString("dialogs.mediaview.parentdocument");
            btnParent.OnClientClick = String.Format("SetParentAction('{0}'); return false;", NodeParentID);
        }
    }


    /// <summary>
    /// Initializes scrips used by the control.
    /// </summary>
    private void InitializeControlScripts()
    {
        ScriptHelper.RegisterStartupScript(this, GetType(), "DialogsSelectAction", ScriptHelper.GetScript(@"
function SetSelectAction(argument) {
    // Raise select action
    SetAction('select', argument);
    RaiseHiddenPostBack();
}
function SetParentAction(argument) {
    // Raise select action
    SetAction('parentselect', argument);
    RaiseHiddenPostBack();
}"));
    }


    /// <summary>
    /// Loads data from data source property.
    /// </summary>
    private void ReloadData()
    {
        innermedia.Reload(true);
    }

    #endregion


    #region "Inner media view event handlers"

    /// <summary>
    /// Returns argument set according passed DataRow and flag indicating whether the set is obtained for selected item.
    /// </summary>
    /// <param name="data">DataRow with all the item data</param>
    /// <param name="isSelected">Indicates whether the set is required for an selected item</param>
    string innermedia_GetArgumentSet(IDataContainer data)
    {
        // Return required argument set
        return GetArgumentSet(data);
    }


    string innermedia_GetListItemUrl(IDataContainer data, bool isPreview, bool notAttachment)
    {
        // Get set of important information
        string arg = GetArgumentSet(data);

        // Get URL of the list item image
        return GetItemUrl(arg, 0, 0, 0, notAttachment);
    }


    string innermedia_GetTilesThumbsItemUrl(IDataContainer data, bool isPreview, int height, int width, int maxSideSize, bool notAttachment)
    {
        string url = "";

        string ext = data.GetValue("AttachmentExtension").ToString();
        string arg = GetArgumentSet(data);

        // If image is requested for preview
        if (isPreview)
        {
            if (!ImageHelper.IsImage(ext) || notAttachment)
            {
                string className = (SourceType == MediaSourceEnum.Content) ? data.GetValue("ClassName").ToString().ToLower() : "";
                if (className == "cms.file")
                {
                    // File isn't image and no preview exists - get default file icon
                    url = GetFileIconUrl(ext, "");
                }
                else if (((SourceType == MediaSourceEnum.DocumentAttachments) || ((SourceType == MediaSourceEnum.Attachment))) && !String.IsNullOrEmpty(ext))
                {
                    // Get file icon for attachment
                    url = GetFileIconUrl(ext, "");
                }
                else
                {
                    url = GetDocumentTypeIconUrl(className, "48x48");
                }
            }
            else
            {
                // Try to get preview or image itself
                url = GetItemUrl(arg, height, width, maxSideSize, notAttachment);
            }
        }
        else
        {
            url = GetItemUrl(arg, 0, 0, 0, notAttachment);
        }

        return url;
    }


    void ListViewControl_OnPageChanged(object sender, EventArgs e)
    {
        RaiseListReloadRequired();
    }

    #endregion


    #region "Helper methods"

    /// <summary>
    /// Returns argument set for the passed file data row.
    /// </summary>
    /// <param name="data">Data row object holding all the data on current file</param>
    public string GetArgumentSet(IDataContainer data)
    {
        string className = ValidationHelper.GetString(data.GetValue("ClassName"), String.Empty).ToLower();
        string name = (SourceType == MediaSourceEnum.DocumentAttachments) ?
            AttachmentHelper.GetFullFileName(Path.GetFileNameWithoutExtension(data.GetValue("AttachmentName").ToString()), data.GetValue("AttachmentExtension").ToString()) : data.GetValue("DocumentName").ToString();

        StringBuilder sb = new StringBuilder();

        // Common information for both content & attachments
        sb.Append("name|" + CMSDialogHelper.EscapeArgument(name));

        // Load attachment info only for CMS.File document type
        if ((SourceType != MediaSourceEnum.Content) || (className == "cms.file"))
        {
            sb.Append("|AttachmentExtension|" + CMSDialogHelper.EscapeArgument(data.GetValue("AttachmentExtension")));
            sb.Append("|AttachmentImageWidth|" + CMSDialogHelper.EscapeArgument(data.GetValue("AttachmentImageWidth")));
            sb.Append("|AttachmentImageHeight|" + CMSDialogHelper.EscapeArgument(data.GetValue("AttachmentImageHeight")));
            sb.Append("|AttachmentSize|" + CMSDialogHelper.EscapeArgument(data.GetValue("AttachmentSize")));
            sb.Append("|AttachmentGUID|" + CMSDialogHelper.EscapeArgument(data.GetValue("AttachmentGUID")));
        }
        else
        {
            sb.Append("|AttachmentExtension||AttachmentImageWidth||AttachmentImageHeight||AttachmentSize||AttachmentGUID|");
        }

        // Get source type specific information
        if (SourceType == MediaSourceEnum.Content)
        {
            sb.Append("|NodeSiteID|" + CMSDialogHelper.EscapeArgument(data.GetValue("NodeSiteID")));
            sb.Append("|SiteName|" + CMSDialogHelper.EscapeArgument(data.GetValue("SiteName")));
            sb.Append("|NodeGUID|" + CMSDialogHelper.EscapeArgument(data.GetValue("NodeGUID")));
            sb.Append("|NodeID|" + CMSDialogHelper.EscapeArgument(data.GetValue("NodeID")));
            sb.Append("|NodeAlias|" + CMSDialogHelper.EscapeArgument(data.GetValue("NodeAlias")));
            sb.Append("|NodeAliasPath|" + CMSDialogHelper.EscapeArgument(data.GetValue("NodeAliasPath")));
            sb.Append("|DocumentUrlPath|" + CMSDialogHelper.EscapeArgument(data.GetValue("DocumentUrlPath")));
            sb.Append("|DocumentExtensions|" + CMSDialogHelper.EscapeArgument(data.GetValue("DocumentExtensions")));
            sb.Append("|ClassName|" + CMSDialogHelper.EscapeArgument(data.GetValue("ClassName")));
            sb.Append("|NodeLinkedNodeID|" + CMSDialogHelper.EscapeArgument(data.GetValue("NodeLinkedNodeID")));
        }
        else
        {
            string formGuid = data.ContainsColumn("AttachmentFormGUID") ? data.GetValue("AttachmentFormGUID").ToString() : Guid.Empty.ToString();
            string siteId = data.ContainsColumn("AttachmentSiteID") ? data.GetValue("AttachmentSiteID").ToString() : "0";

            sb.Append("|SiteID|" + CMSDialogHelper.EscapeArgument(siteId));
            sb.Append("|FormGUID|" + CMSDialogHelper.EscapeArgument(formGuid));
            sb.Append("|AttachmentDocumentID|" + CMSDialogHelper.EscapeArgument(data.GetValue("AttachmentDocumentID")));
        }

        return sb.ToString();
    }


    /// <summary>
    /// Returns arguments table for the passed argument.
    /// </summary>
    /// <param name="argument">Argument containing information on current media item</param>
    public static Hashtable GetArgumentsTable(string argument)
    {
        Hashtable table = new Hashtable();

        string[] argArr = argument.Split('|');
        try
        {
            // Fill table
            for (int i = 0; i < argArr.Length; i = i + 2)
            {
                table[argArr[i].ToLower()] = CMSDialogHelper.UnEscapeArgument(argArr[i + 1]);
            }
        }
        catch
        {
            throw new Exception("[Media view]: Error loading arguments table.");
        }

        return table;
    }


    /// <summary>
    /// Returns URL of the media item according site settings.
    /// </summary>
    /// <param name="argument">Argument containing information on current media item</param>
    /// <param name="maxSideSize">Maximum dimension for images displayed for tile and thumbnails view</param>
    public string GetItemUrl(string argument, int height, int width, int maxSideSize, bool notAttachment)
    {
        Hashtable argTable = GetArgumentsTable(argument);
        if (argTable.Count >= 2)
        {
            string url = "";

            // Get image URL
            if (SourceType == MediaSourceEnum.Content)
            {
                // Get information from argument
                Guid nodeGuid = ValidationHelper.GetGuid(argTable["nodeguid"], Guid.Empty);
                string documentUrlPath = argTable["documenturlpath"].ToString();
                string nodeAlias = argTable["nodealias"].ToString();
                string nodeAliasPath = argTable["nodealiaspath"].ToString();
                string documentExtensions = argTable["documentextensions"].ToString();
                string documentClass = argTable["classname"].ToString();
                bool nodeIsLink = (ValidationHelper.GetInteger(argTable["nodelinkednodeid"], 0) != 0);

                // Get default url extension for current item
                string fileExt = !String.IsNullOrEmpty(documentClass) && (documentClass.ToLower() == "cms.file") ? TreePathUtils.GetFilesUrlExtension() : TreePathUtils.GetUrlExtension();

                // If extensionless try to get custom document extension
                if (String.IsNullOrEmpty(fileExt))
                {
                    fileExt = (!String.IsNullOrEmpty(documentExtensions) ? documentExtensions.Split(';')[0] : String.Empty);
                }

                // Get content item URL
                url = GetContentItemUrl(nodeGuid, documentUrlPath, nodeAlias, nodeAliasPath, nodeIsLink, height, width, maxSideSize, notAttachment, fileExt);
            }
            else
            {
                // Get information from argument
                Guid attachmentGuid = ValidationHelper.GetGuid(argTable["attachmentguid"], Guid.Empty);
                string attachmentName = argTable["name"].ToString();
                string nodeAliasPath = "";
                if (TreeNodeObj != null)
                {
                    nodeAliasPath = TreeNodeObj.NodeAliasPath;
                }

                // Get item URL                
                url = GetAttachmentItemUrl(attachmentGuid, attachmentName, nodeAliasPath, height, width, maxSideSize);
            }

            return url;
        }

        return "";
    }


    /// <summary>
    /// Ensures no item is selected.
    /// </summary>
    public void ResetSearch()
    {
        dialogSearch.ResetSearch();
    }


    /// <summary>
    /// Ensures first page is displayed in the control displaying the content.
    /// </summary>
    public void ResetPageIndex()
    {
        innermedia.ResetPageIndex();
    }


    /// <summary>
    /// Ensure no item is selected in list view.
    /// </summary>
    public void ResetListSelection()
    {
        innermedia.ResetListSelection();
    }

    #endregion


    #region "Content methods"

    /// <summary>
    /// Returns URL of the media item according site settings.
    /// </summary>
    /// <param name="nodeGuid">Node GUID of the current attachment node</param>    
    /// <param name="documentUrlPath">URL path of the current attachment document</param>
    /// <param name="maxSideSize">Maximum dimension for images displayed for tile and thumbnails view</param>
    /// <param name="nodeAlias">Node alias of the current attachment node</param>
    /// <param name="nodeAliasPath">Node alias path of the current attachment node</param>
    /// <param name="nodeIsLink">Indicates if node is linked node.</param>
    public string GetContentItemUrl(Guid nodeGuid, string documentUrlPath, string nodeAlias, string nodeAliasPath, bool nodeIsLink, int height, int width, int maxSideSize, bool notAttachment, string documentExtension)
    {
        string result = "";

        if (documentExtension.Contains(";"))
        {
            documentExtension = documentExtension.Split(';')[0];
        }

        // Generate URL
        if (UsePermanentUrls)
        {
            bool isLink = ((OutputFormat == OutputFormatEnum.BBLink) || (OutputFormat == OutputFormatEnum.HTMLLink)) ||
                ((OutputFormat == OutputFormatEnum.URL) && (SelectableContent == SelectableContentEnum.AllContent));

            if (String.IsNullOrEmpty(nodeAlias))
            {
                nodeAlias = "default";
            }

            if (notAttachment || isLink)
            {
                result = TreePathUtils.GetPermanentDocUrl(nodeGuid, nodeAlias, SiteObj.SiteName, null, documentExtension);
            }
            else
            {
                result = AttachmentManager.GetPermanentAttachmentUrl(nodeGuid, nodeAlias, documentExtension);
            }
        }
        else
        {
            string docUrlPath = nodeIsLink ? null : documentUrlPath;
            result = TreePathUtils.GetUrl(nodeAliasPath, docUrlPath, null, null, documentExtension);
        }

        // Make URL absolute if required
        int currentSiteId = CMSContext.CurrentSiteID;
        if (Config.UseFullURL || (currentSiteId != SiteObj.SiteID) || (currentSiteId != GetCurrentSiteId()))
        {
            result = URLHelper.GetAbsoluteUrl(result, SiteObj.DomainName, URLHelper.GetApplicationUrl(SiteObj.DomainName), null);
        }

        // Image dimensions to URL
        if (maxSideSize > 0)
        {
            result = URLHelper.AddParameterToUrl(result, "maxsidesize", maxSideSize.ToString());
        }
        if (height > 0)
        {
            result = URLHelper.AddParameterToUrl(result, "height", height.ToString());
        }
        if (width > 0)
        {
            result = URLHelper.AddParameterToUrl(result, "width", width.ToString());
        }

        // Media selctor should returns non-resolved URL in all cases
        bool isMediaSelector = (OutputFormat == OutputFormatEnum.URL) && (SelectableContent == SelectableContentEnum.OnlyMedia);

        return (isMediaSelector ? result : URLHelper.ResolveUrl(result, true, false));
    }

    #endregion


    #region "Attachment methods"

    /// <summary>
    /// Returns URL for the attachment specified by arguments.
    /// </summary>
    /// <param name="attachmentGuid">GUID of the attachment</param>
    /// <param name="attachmentName">Name of the attachment</param>
    /// <param name="attachmentNodeAlias"></param>
    /// <param name="maxSideSize">Maximum size of the item if attachment is image</param>
    public string GetAttachmentItemUrl(Guid attachmentGuid, string attachmentName, string attachmentNodeAlias, int height, int width, int maxSideSize)
    {
        string result = "";

        if (UsePermanentUrls || string.IsNullOrEmpty(attachmentNodeAlias))
        {
            result = AttachmentManager.GetAttachmentUrl(attachmentGuid, attachmentName);
        }
        else
        {
            //string safeFileName = URLHelper.GetSafeFileName(attachmentName, attachmentSite.SiteName);
            string safeFileName = URLHelper.GetSafeFileName(attachmentName, SiteObj.SiteName);

            result = AttachmentManager.GetAttachmentUrl(safeFileName, attachmentNodeAlias);
        }

        // If current site is different from attachment site make URL absolute (domain included)
        if (Config.UseFullURL || (CMSContext.CurrentSiteID != SiteObj.SiteID) || (CMSContext.CurrentSiteID != GetCurrentSiteId()))
        {
            result = URLHelper.GetAbsoluteUrl(result, SiteObj.DomainName, URLHelper.GetApplicationUrl(SiteObj.DomainName), null);
        }


        // If image dimensions are specified
        if (maxSideSize > 0)
        {
            result = URLHelper.AddParameterToUrl(result, "maxsidesize", maxSideSize.ToString());
        }
        if (height > 0)
        {
            result = URLHelper.AddParameterToUrl(result, "height", height.ToString());
        }
        if (width > 0)
        {
            result = URLHelper.AddParameterToUrl(result, "width", width.ToString());
        }

        // Media selctor should returns non-resolved URL in all cases
        bool isMediaSelector = (OutputFormat == OutputFormatEnum.URL) && (SelectableContent == SelectableContentEnum.OnlyMedia);

        return (isMediaSelector ? result : URLHelper.ResolveUrl(result, true, false));
    }

    #endregion
}
