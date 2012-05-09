using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
using System.Collections.Generic;

using CMS.UIControls;
using CMS.Controls;
using CMS.GlobalHelper;
using CMS.ExtendedControls;
using CMS.TreeEngine;
using CMS.WorkflowEngine;
using CMS.CMSHelper;
using CMS.SettingsProvider;
using CMS.SiteProvider;
using CMS.IO;

using TreeNode = CMS.TreeEngine.TreeNode;

public partial class CMSModules_Content_Controls_Dialogs_LinkMediaSelector_InnerMediaView : CMSUserControl
{
    #region "Constants"

    private const double MaxThumbImgWidth = 160.0;
    private const double MaxThumbImgHeight = 95.0;
    private const int PAGER_GROUP_SIZE = 10;

    #endregion


    #region "Private variables"

    private OutputFormatEnum mOutputFormat = OutputFormatEnum.HTMLMedia;
    private DialogConfiguration mConfig = null;
    private DialogViewModeEnum mViewMode = DialogViewModeEnum.ListView;
    private SelectableContentEnum mSelectableContent = SelectableContentEnum.AllContent;
    private MediaSourceEnum mSourceType = MediaSourceEnum.Content;

    private string mImagesPath = "";
    private string mFileIdColumn = "";
    private string mFileNameColumn = "";
    private string mFileExtensionColumn = "";
    private string mFileSizeColumn = "";
    private string mFileWidthColumn = "";
    private string mFileHeightColumn = "";
    private string mAllowedExtensions = "";
    private string mInfoText = "";

    private int mVersionHistoryId = -1;
    private int mLastSiteId = 0;

    private bool columnUpdateVisible = false;

    #endregion


    #region "Events & delegates"

    /// <summary>
    /// Delegate for an event occurring when argument set is required.
    /// </summary>
    /// <param name="data">DataRow holding information on currently processed file</param>    
    public delegate string OnGetArgumentSet(IDataContainer data);

    /// <summary>
    /// Event occurring when argument set is required.
    /// </summary>
    public event OnGetArgumentSet GetArgumentSet;

    /// <summary>
    /// Delegate for the event fired when URL for list image is required.
    /// </summary>
    /// <param name="data">DataRow holding information on currently processed file</param>   
    /// <param name="isPreview">Indicates whether the image is generated as part of preview</param>
    /// <param name="notAttachment">Indicates whether the URL is required for non-attachment item</param>
    public delegate string OnGetListItemUrl(IDataContainer data, bool isPreview, bool notAttachment);

    /// <summary>
    /// Event occurring when URL for list item image is required.
    /// </summary>
    public event OnGetListItemUrl GetListItemUrl;

    /// <summary>
    /// Delegate for the event fired when URL for tiles & thumbnails image is required.
    /// </summary>
    /// <param name="data">DataRow holding information on currently processed file</param>  
    /// <param name="isPreview">Indicates whether the image is generated as part of preview</param>
    /// <param name="width">Width of preview image</param>
    /// <param name="maxSideSize">Maximum size of the preview image. If full-size required parameter gets zero value</param>
    /// <param name="notAttachment">Indicates whether the URL is required for non-attachment item</param>
    /// <param name="height">Height of preview image</param>
    public delegate string OnGetTilesThumbsItemUrl(IDataContainer data, bool isPreview, int height, int width, int maxSideSize, bool notAttachment);

    /// <summary>
    /// Event occurring when URL for tiles & thumbnails image is required.
    /// </summary>
    public event OnGetTilesThumbsItemUrl GetTilesThumbsItemUrl;

    /// <summary>
    /// Delegate for the event occurring when information on file import status is required.
    /// </summary>
    /// <param name="type">Type of the required information</param>
    /// <param name="parameter">Parameter related</param>
    public delegate object OnGetInformation(string type, object parameter);

    /// <summary>
    /// Event occurring when information on file import status is required.
    /// </summary>
    public event OnGetInformation GetInformation;

    /// <summary>
    /// Delegate for the event occurring when permission modify is required.
    /// </summary>
    /// <param name="data">DataRow holding information on currently processed file</param>
    public delegate bool OnGetModifyPermission(IDataContainer data);

    /// <summary>
    /// Event occurring when permission modify is required.
    /// </summary>
    public event OnGetModifyPermission GetModifyPermission;

    #endregion


    #region "Public properties"

    /// <summary>
    /// Gets or sets the OutputFormat (needed for correct dialog type recognition).
    /// </summary>
    public OutputFormatEnum OutputFormat
    {
        get
        {
            return mOutputFormat;
        }
        set
        {
            mOutputFormat = value;
        }
    }


    /// <summary>
    /// Gets current dialog configuration.
    /// </summary>
    public DialogConfiguration Config
    {
        get
        {
            return mConfig ?? (mConfig = DialogConfiguration.GetDialogConfiguration());
        }
    }


    /// <summary>
    /// Gets a UniGrid control used to display files in LIST view mode.
    /// </summary>
    public UniGrid ListViewControl
    {
        get
        {
            return gridList;
        }
    }


    /// <summary>
    /// Gets a repeater control used to display files in TILES view mode.
    /// </summary>
    public BasicRepeater TilesViewControl
    {
        get
        {
            return repTilesView;
        }
    }


    /// <summary>
    /// Gets a repeater control used to display files in THUMBNAILS view mode.
    /// </summary>
    public BasicRepeater ThumbnailsViewControl
    {
        get
        {
            return repThumbnailsView;
        }
    }


    /// <summary>
    /// Gets list of names of selected files.
    /// </summary>
    public ArrayList SelectedItems
    {
        get
        {
            return GetSelectedItems();
        }
    }


    /// <summary>
    /// Gets or sets a view mode used to display files.
    /// </summary>
    public DialogViewModeEnum ViewMode
    {
        get
        {
            return mViewMode;
        }
        set
        {
            mViewMode = value;
        }
    }

    private DataSet mDataSource = null;

    /// <summary>
    /// Gets or sets source of the data for view controls.
    /// </summary>
    public DataSet DataSource
    {
        get
        {
            return mDataSource;
        }
        set
        {
            mDataSource = value;
        }
    }


    /// <summary>
    /// Type of the content which can be selected.
    /// </summary>
    public SelectableContentEnum SelectableContent
    {
        get
        {
            return mSelectableContent;
        }
        set
        {
            mSelectableContent = value;
        }
    }


    /// <summary>
    /// Gets or sets name of the column holding information on the file identifier.
    /// </summary>
    public string FileIdColumn
    {
        get
        {
            return mFileIdColumn;
        }
        set
        {
            mFileIdColumn = value;
        }
    }

    /// <summary>
    /// Gets or sets name of the column holding information on file name.
    /// </summary>
    public string FileNameColumn
    {
        get
        {
            return mFileNameColumn;
        }
        set
        {
            mFileNameColumn = value;
        }
    }


    /// <summary>
    /// Gets or sets name of the column holding information on file extension.
    /// </summary>
    public string FileExtensionColumn
    {
        get
        {
            return mFileExtensionColumn;
        }
        set
        {
            mFileExtensionColumn = value;
        }
    }


    /// <summary>
    /// Gets or sets name of the column holding information on file width.
    /// </summary>
    public string FileWidthColumn
    {
        get
        {
            return mFileWidthColumn;
        }
        set
        {
            mFileWidthColumn = value;
        }
    }


    /// <summary>
    /// Gets or sets name of the column holding information on file height.
    /// </summary>
    public string FileHeightColumn
    {
        get
        {
            return mFileHeightColumn;
        }
        set
        {
            mFileHeightColumn = value;
        }
    }


    /// <summary>
    /// Gets or sets name of the column holding information on file size.
    /// </summary>
    public string FileSizeColumn
    {
        get
        {
            return mFileSizeColumn;
        }
        set
        {
            mFileSizeColumn = value;
        }
    }


    /// <summary>
    /// Gets or sets text of the information label.
    /// </summary>
    public string InfoText
    {
        get
        {
            return mInfoText;
        }
        set
        {
            mInfoText = value;
        }
    }


    /// <summary>
    /// Gets the node attachments are related to.
    /// </summary>
    public TreeNode TreeNodeObj
    {
        get;
        set;
    }


    /// <summary>
    /// Gets or sets information on source type.
    /// </summary>
    public MediaSourceEnum SourceType
    {
        get
        {
            return mSourceType;
        }
        set
        {
            mSourceType = value;
        }
    }


    /// <summary>
    /// Height of attachment.
    /// </summary>
    public int ResizeToHeight
    {
        get;
        set;
    }


    /// <summary>
    /// Width of attachment.
    /// </summary>
    public int ResizeToWidth
    {
        get;
        set;
    }


    /// <summary>
    /// Max side size of attachment.
    /// </summary>
    public int ResizeToMaxSideSize
    {
        get;
        set;
    }


    /// <summary>
    /// Currently topN value for selection.
    /// </summary>
    public int CurrentTopN
    {
        get
        {
            int pageSize = 0;
            int pageIndex = 0;
            int pageCount = 0;
            switch (ViewMode)
            {
                case DialogViewModeEnum.TilesView:
                    pageSize = (pageSizeTiles.SelectedValue.ToUpper() == "-1") ? 0 : ValidationHelper.GetInteger(pageSizeTiles.SelectedValue, 20);
                    pageIndex = pagerElemTiles.CurrentPage;
                    pageCount = pagerElemTiles.PageCount;
                    break;

                case DialogViewModeEnum.ThumbnailsView:
                    pageSize = (pageSizeThumbs.SelectedValue.ToUpper() == "-1") ? 0 : ValidationHelper.GetInteger(pageSizeThumbs.SelectedValue, 12);
                    pageIndex = pagerElemThumbnails.CurrentPage;
                    pageCount = pagerElemThumbnails.PageCount;
                    break;

                case DialogViewModeEnum.ListView:
                    pageSize = (gridList.Pager.CurrentPageSize == -1) ? 0 : ValidationHelper.GetInteger(gridList.Pager.CurrentPageSize, 10);
                    pageIndex = gridList.Pager.CurrentPage - 1;
                    pageCount = gridList.Pager.CurrentPagesGroupSize;
                    break;
            }
            if (pageSize > 0)
            {
                pageCount = (pageCount > PAGER_GROUP_SIZE) ? pageCount : PAGER_GROUP_SIZE;

                return (pageSize * (pageIndex + 1 + pageCount + (PAGER_GROUP_SIZE - (pageCount % PAGER_GROUP_SIZE))));
            }
            else
            {
                return 0;
            }
        }
    }


    /// <summary>
    /// Indicates if full listing mode is enabled. This mode enables navigation to child and parent folders/documents from current view.
    /// </summary>
    public bool IsFullListingMode
    {
        get
        {
            return ValidationHelper.GetBoolean(ViewState["IsFullListingMode"], false);
        }
        set
        {
            ViewState["IsFullListingMode"] = value;
        }
    }



    /// <summary>
    /// Indicates whether the control is displayed as part of the copy/move dialog.
    /// </summary>
    public bool IsCopyMoveLinkDialog
    {
        get;
        set;
    }


    /// <summary>
    /// ID of the related node version history.
    /// </summary>
    private int VersionHistoryID
    {
        get
        {
            if (mVersionHistoryId < 0)
            {
                mVersionHistoryId = 0;

                // Load the version history
                if (TreeNodeObj != null)
                {
                    // Get the node workflow
                    WorkflowManager wm = new WorkflowManager(TreeNodeObj.TreeProvider);
                    WorkflowInfo wi = wm.GetNodeWorkflow(TreeNodeObj);
                    if (wi != null)
                    {
                        // Ensure the document version
                        VersionManager vm = new VersionManager(TreeNodeObj.TreeProvider);
                        VersionHistoryID = vm.EnsureVersion(TreeNodeObj, TreeNodeObj.IsPublished);
                    }
                }
            }

            return mVersionHistoryId;
        }
        set
        {
            mVersionHistoryId = value;
        }
    }

    #endregion]


    #region "Private properties"

    /// <summary>
    /// Image relative path.
    /// </summary>
    private string ImagesPath
    {
        get
        {
            if (mImagesPath == "")
            {
                mImagesPath = GetImageUrl("Design/Controls/UniGrid/Actions/", IsLiveSite, true);
            }
            return mImagesPath;
        }
    }

    #endregion


    #region "Attachment properties"

    /// <summary>
    /// Gets all allowed extensions.
    /// </summary>
    public string AllowedExtensions
    {
        get
        {
            if (mAllowedExtensions == "")
            {
                mAllowedExtensions = QueryHelper.GetString("allowedextensions", "");
                if (mAllowedExtensions == "")
                {
                    mAllowedExtensions = SettingsKeyProvider.GetStringValue(CMSContext.CurrentSiteName + ".CMSUploadExtensions");
                }
            }
            return mAllowedExtensions;
        }
        private set
        {
            mAllowedExtensions = value;
        }
    }


    /// <summary>
    /// Gets or sets ID of the parent node.
    /// </summary>
    public int NodeParentID
    {
        get;
        set;
    }

    #endregion


    #region "Constructors"

    public CMSModules_Content_Controls_Dialogs_LinkMediaSelector_InnerMediaView()
    {
        NodeParentID = 0;
        IsCopyMoveLinkDialog = false;
        ResizeToWidth = 0;
        ResizeToHeight = 0;
        TreeNodeObj = null;
        DataSource = null;
        ResizeToMaxSideSize = 0;
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

    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        Visible = !StopProcessing;
        if (!StopProcessing)
        {
            gridList.IsLiveSite = IsLiveSite;
            if (URLHelper.IsPostback())
            {
                Reload(true);
            }
        }
    }


    protected override void OnPreRender(EventArgs e)
    {
        // Display information on empty data
        bool isEmpty = DataHelper.DataSourceIsEmpty(DataSource);
        if (isEmpty)
        {
            plcViewArea.Visible = false;
        }
        else
        {
            lblInfo.Visible = false;
            plcViewArea.Visible = true;
        }

        // If info text is set display it
        if (!string.IsNullOrEmpty(InfoText))
        {
            lblInfo.Text = InfoText;
            lblInfo.Visible = true;
        }
        else if (isEmpty)
        {
            lblInfo.Text = (IsCopyMoveLinkDialog ? GetString("media.copymove.empty") : CMSDialogHelper.GetNoItemsMessage(Config, SourceType));
            lblInfo.Visible = true;
        }

        // Reset pager control
        switch (ViewMode)
        {
            case DialogViewModeEnum.TilesView:
                pagerElemTiles.CurrentPage = 0;
                break;

            case DialogViewModeEnum.ThumbnailsView:
                pagerElemThumbnails.CurrentPage = 0;
                break;
        }

        // Hide column 'Update' in media libraries
        if (((SourceType == MediaSourceEnum.MediaLibraries) || (SourceType == MediaSourceEnum.Content)) && !columnUpdateVisible)
        {
            if (gridList.NamedColumns.ContainsKey("webdav"))
            {
                gridList.NamedColumns["webdav"].Visible = false;
            }
        }

        base.OnPreRender(e);
    }

    #endregion


    #region "Public methods"

    /// <summary>
    /// Loads view controls according currently set view mode.
    /// </summary>
    public void LoadViewControls()
    {
        InfoText = "";

        // Select mode according required
        switch (ViewMode)
        {
            case DialogViewModeEnum.ListView:
                plcListView.Visible = true;

                // Stop processing                
                plcTilesView.Visible = false;
                plcThumbnailsView.Visible = false;
                break;

            case DialogViewModeEnum.TilesView:
                plcTilesView.Visible = true;
                repTilesView.DataBindByDefault = false;
                pagerElemTiles.PageControl = repTilesView.ID;

                // Stop processing
                plcListView.Visible = false;
                plcThumbnailsView.Visible = false;
                break;

            case DialogViewModeEnum.ThumbnailsView:
                plcThumbnailsView.Visible = true;
                repThumbnailsView.DataBindByDefault = false;
                pagerElemThumbnails.PageControl = repThumbnailsView.ID;

                // Stop processing
                plcListView.Visible = false;
                plcTilesView.Visible = false;
                break;
        }

        // Display mass actions drop-down list if displayed for MediaLibrary UI
        if (!IsCopyMoveLinkDialog && (DisplayMode == ControlDisplayModeEnum.Simple))
        {
            plcMassAction.Visible = true;

            InitializeMassActions();
        }
        else
        {
            plcMassAction.Visible = false;
        }
    }

    #endregion


    #region "Mass actions methods"

    /// <summary>
    /// Initializes mass actions drop-down list with available actions.
    /// </summary>
    private void InitializeMassActions()
    {
        const string actionScript = @"
function RaiseMassAction(drpActionsClientId, drpActionFilesClientId) {
    var drpActions = document.getElementById(drpActionsClientId);
    var drpActionFiles = document.getElementById(drpActionFilesClientId);
    if((drpActions != null) && (drpActionFiles != null)) {
        var selectedFiles = drpActionFiles.options[drpActionFiles.selectedIndex];
        var selectedAction = drpActions.options[drpActions.selectedIndex];
        if((selectedAction != null) && (selectedFiles != null)) {
            var argument = selectedAction.value + '|' + selectedFiles.value;
            SetAction('massaction', argument);
            RaiseHiddenPostBack();
        }                   
    } 
}";

        ScriptHelper.RegisterStartupScript(Page, typeof(Page), "LibraryActionScript", ScriptHelper.GetScript(actionScript));

        if (drpActionFiles.Items.Count == 0)
        {
            // Actions dropdown
            drpActionFiles.Items.Add(new ListItem(GetString("media.file.list.lblactions"), "selected"));
            drpActionFiles.Items.Add(new ListItem(GetString("media.file.list.filesall"), "all"));
        }

        if (drpActions.Items.Count == 0)
        {
            // Actions dropdown
            drpActions.Items.Add(new ListItem(GetString("General.SelectAction"), ""));
            drpActions.Items.Add(new ListItem(GetString("media.file.copy"), "copy"));
            drpActions.Items.Add(new ListItem(GetString("media.file.move"), "move"));
            drpActions.Items.Add(new ListItem(GetString("General.Delete"), "delete"));
            drpActions.Items.Add(new ListItem(GetString("media.file.import"), "import"));
        }

        btnActions.OnClientClick = String.Format("if(MassConfirm('{0}', '{1}')) {{ RaiseMassAction('{0}', '{2}'); }} return false;", drpActions.ClientID, GetString("General.ConfirmGlobalDelete"), drpActionFiles.ClientID);
    }


    /// <summary>
    /// Returns list of names of selected files.
    /// </summary>
    private ArrayList GetSelectedItems()
    {
        switch (ViewMode)
        {
            case DialogViewModeEnum.ListView:
                return gridList.SelectedItems;

            case DialogViewModeEnum.TilesView:
            case DialogViewModeEnum.ThumbnailsView:
                return GetTilesThumbsSelectedItems();
        }

        return null;
    }


    /// <summary>
    /// Returns list of names of files selected in tiles view mode.
    /// </summary>
    private ArrayList GetTilesThumbsSelectedItems()
    {
        ArrayList result = new ArrayList();
        BasicRepeater repElem = (ViewMode == DialogViewModeEnum.TilesView) ? repTilesView : repThumbnailsView;

        // Go through all repeater items and look for selected ones
        foreach (RepeaterItem item in repElem.Items)
        {
            LocalizedCheckBox chkSelected = item.FindControl("chkSelected") as LocalizedCheckBox;
            if ((chkSelected != null) && chkSelected.Checked)
            {
                HiddenField hdnItemName = item.FindControl("hdnItemName") as HiddenField;
                if (hdnItemName != null)
                {
                    string alt = hdnItemName.Value;
                    result.Add(alt);
                }
            }
        }

        return result;
    }


    /// <summary>
    /// Ensures given file name in the way it is usable as ID.
    /// </summary>
    /// <param name="fileName">Name of the file to ensure</param>
    private static string EnsureFileName(string fileName)
    {
        if (!string.IsNullOrEmpty(fileName))
        {
            char[] specialChars = "#;&,.+*~':\"!^$[]()=>|/\\-%@`{}".ToCharArray();
            foreach (char specialChar in specialChars)
            {
                fileName = fileName.Replace(specialChar, '_');
            }
            return fileName.Replace(" ", "").ToLower();
        }

        return fileName;
    }

    #endregion


    #region "Private methods"

    /// <summary>
    /// Initializes all nested controls.
    /// </summary>
    private void SetupControls()
    {
        // Initialize displaying controls according view mode
        LoadViewControls();

        InitializeControlScripts();

        // Select mode according required
        switch (ViewMode)
        {
            case DialogViewModeEnum.ListView:
                InitializeListView();
                ListViewControl.OnExternalDataBound += ListViewControl_OnExternalDataBound;
                ListViewControl.GridView.RowDataBound += GridView_RowDataBound;
                break;

            case DialogViewModeEnum.TilesView:
                InitializeTilesView();
                TilesViewControl.ItemDataBound += TilesViewControl_ItemDataBound;
                break;

            case DialogViewModeEnum.ThumbnailsView:
                InitializeThumbnailsView();
                ThumbnailsViewControl.ItemDataBound += ThumbnailsViewControl_ItemDataBound;
                break;
        }

        // Register delete confirmation script
        ltlScript.Text = ScriptHelper.GetScript(String.Format("function DeleteConfirmation(){{ return confirm('{0}'); }} function DeleteMediaFileConfirmation(){{ return confirm('{1}'); }}",
            GetString("attach.deleteconfirmation"),
            GetString("general.deleteconfirmation")));
    }


    /// <summary>
    /// Initializes all the necessary JavaScript blocks.
    /// </summary>
    private void InitializeControlScripts()
    {
        const string activeBackgroundPath = "CMSModules/CMS_Content/Dialogs/";
        // Dialog for editing image and non-image
        string urlImage = "";
        string urlMeta = "";
        if (SourceType == MediaSourceEnum.MediaLibraries)
        {
            if (IsLiveSite)
            {
                if (CMSContext.CurrentUser.IsAuthenticated())
                {
                    urlImage = ResolveUrl("~/CMSModules/MediaLibrary/CMSPages/ImageEditor.aspx");
                    urlMeta = CMSContext.ResolveDialogUrl("~/CMSModules/MediaLibrary/CMSPages/MetaDataEditor.aspx");
                }
                else
                {
                    urlImage = ResolveUrl("~/CMSModules/MediaLibrary/CMSPages/PublicImageEditor.aspx");
                    urlMeta = CMSContext.ResolveDialogUrl("~/CMSModules/MediaLibrary/CMSPages/MetaDataEditor.aspx");
                }
            }
            else
            {
                urlImage = ResolveUrl("~/CMSModules/MediaLibrary/Controls/MediaLibrary/ImageEditor.aspx");
                urlMeta = CMSContext.ResolveDialogUrl("~/CMSModules/MediaLibrary/Dialogs/MetaDataEditor.aspx");
            }
        }
        else
        {
            if (IsLiveSite)
            {
                if (CMSContext.CurrentUser.IsAuthenticated())
                {
                    urlImage = ResolveUrl("~/CMSFormControls/LiveSelectors/ImageEditor.aspx");
                    urlMeta = CMSContext.ResolveDialogUrl("~/CMSModules/Content/Attachments/CMSPages/MetaDataEditor.aspx");
                }
                else
                {
                    urlImage = ResolveUrl("~/CMSFormControls/LiveSelectors/PublicImageEditor.aspx");
                    urlMeta = CMSContext.ResolveDialogUrl("~/CMSModules/Content/Attachments/CMSPages/MetaDataEditor.aspx");
                }
            }
            else
            {
                urlImage = ResolveUrl("~/CMSModules/Content/CMSDesk/Edit/ImageEditor.aspx");
                urlMeta = CMSContext.ResolveDialogUrl("~/CMSModules/Content/Attachments/Dialogs/MetaDataEditor.aspx");
            }
        }
        string script = String.Format(@"
var activeBackgroundList = 'url({0})';
var activeBackgroundTiles = 'url({1})';
var activeBackgroundThumbs = 'url({2})';
var attemptNo = 0;
function ColorizeRow(itemId) {{
    if (itemId != null)
    {{
        var hdnField = document.getElementById('{3}');
        if (hdnField != null)
        {{
            // If some item was previously selected
            if ((hdnField.value != null) && (hdnField.value != ''))
            {{
                // Get selected item and reset its selection
                var lastColorizedElem = document.getElementById(hdnField.value);
                if (lastColorizedElem != null)
                {{
                    ColorizeElement(lastColorizedElem, '', itemId);
                }}
             }}            
             // Update field value
             hdnField.value = itemId;
        }}
        // Colorize currently selected item
        var elem = document.getElementById(itemId);
        if (elem != null)
        {{
            ColorizeElement(elem, activeBackgroundList, itemId);
            attemptNo = 0;
        }}
        else
        {{
            if(attemptNo < 1)
            {{
                setTimeout('ColorizeRow(\'' + itemId + '\')', 300);
                attemptNo = attemptNo + 1;
            }}
            else
            {{
                attemptNo = 0;
            }}
        }}
     }}
 }}
 function ColorizeLastRow() {{
    var hdnField = document.getElementById('{3}');
    if (hdnField != null)
    {{
        // If some item was previously selected
        if ((hdnField.value != null) && (hdnField.value != ''))
        {{               
            // Get selected item and reset its selection
            var lastColorizedElem = document.getElementById(hdnField.value);
            if (lastColorizedElem != null)
            {{
                ColorizeElement(lastColorizedElem, activeBackgroundList);
            }}
        }}
    }}
}}
function ColorizeElement(elem, bgImage, itemId) {{
    if((bgImage != null) && (bgImage != '')){{
        if (elem.className == 'DialogTileItem') {{
           bgImage = activeBackgroundTiles; 
        }}
        else if (elem.className == 'DialogThumbnailItem') {{
           bgImage = activeBackgroundThumbs; 
        }}
        else {{
           bgImage = activeBackgroundList; 
        }}
        elem.className += ' Selected';
    }}
    else {{
        elem.className = elem.className.replace(' Selected','');
    }}
    elem.style.backgroundImage = bgImage;
}}
function ClearColorizedRow()
{{
    var hdnField = document.getElementById('{3}');
    if (hdnField != null) 
    {{
        // If some item was previously selected
        if ((hdnField.value != null) && (hdnField.value != '')) 
        {{               
            // Get selected item and reset its selection
            var lastColorizedElem = document.getElementById(hdnField.value);
            if (lastColorizedElem != null) 
            {{   
                ColorizeElement(lastColorizedElem, '');

                // Update field value
                hdnField.value = '';                                    
            }}
        }}
    }}                                
}}
function EditImage(param) {{
    var form = '';
    if (param.indexOf('?') != 0) {{ 
        param = '?'+param; 
    }}
    modalDialog('{4}' + param, 'imageEditorDialog', 905, 670, undefined, true); 
    return false; 
}}
function Edit(param) {{
    var form = '';
    if (param.indexOf('?') != 0) {{ 
        param = '?'+param; 
    }}
    modalDialog('{5}' + param, 'editorDialog', 500, 350, undefined, true); 
    return false; 
}}",
        ResolveUrl(GetImageUrl(activeBackgroundPath + "highlightline.png")),
        ResolveUrl(GetImageUrl(activeBackgroundPath + "highlighttiles.png")),
        ResolveUrl(GetImageUrl(activeBackgroundPath + "highlightthumbs.png")),
        hdnItemToColorize.ClientID,
        urlImage,
        urlMeta);

        ScriptHelper.RegisterStartupScript(this, GetType(), "DialogsScript", script, true);
    }


    /// <summary>
    /// Loads data from the data source property.
    /// </summary>
    public void ReloadData()
    {
        // Select mode according required
        switch (ViewMode)
        {
            case DialogViewModeEnum.ListView:
                ReloadListView();
                break;

            case DialogViewModeEnum.TilesView:
                ReloadTilesView();
                break;

            case DialogViewModeEnum.ThumbnailsView:
                ReloadThumbnailsView();
                break;
        }
    }


    /// <summary>
    /// Reloads control with data.
    /// </summary>
    /// <param name="forceSetup">Indicates whether the inner controls should be re-setuped</param>
    public void Reload(bool forceSetup)
    {
        Visible = !StopProcessing;
        if (Visible)
        {
            if (forceSetup)
            {
                // Initialize controls
                SetupControls();
            }

            // Load passed data
            ReloadData();
        }
    }


    /// <summary>
    /// Returns the sitename according to item info.
    /// </summary>
    /// <param name="data">Row containing information on the current item</param>
    /// <param name="isMediaFile">Indicates whether the file is media file</param>
    private string GetSiteName(IDataContainer data, bool isMediaFile)
    {
        int siteId = 0;
        string result = "";

        if (isMediaFile)
        {
            if (data.ContainsColumn("FileSiteID"))
            {
                // Imported media file
                siteId = ValidationHelper.GetInteger(data.GetValue("FileSiteID"), 0);
            }
            else
            {
                // Npt imported yet
                siteId = RaiseOnSiteIdRequired();
            }
        }
        else
        {
            if (data.ContainsColumn("SiteName"))
            {
                // Content file
                result = ValidationHelper.GetString(data.GetValue("SiteName"), "");
            }
            else
            {
                if (data.ContainsColumn("AttachmentSiteID"))
                {
                    // Non-versioned attachment
                    siteId = ValidationHelper.GetInteger(data.GetValue("AttachmentSiteID"), 0);
                }
                else if (TreeNodeObj != null)
                {
                    // Versioned attachment
                    siteId = TreeNodeObj.NodeSiteID;
                }
            }
        }

        if (result == "")
        {
            if (!ContextStockHelper.Contains("DialogsSiteName") || (mLastSiteId != siteId))
            {
                SiteInfo si = SiteInfoProvider.GetSiteInfo(siteId);
                if (si != null)
                {
                    mLastSiteId = si.SiteID;
                    result = si.SiteName;
                    ContextStockHelper.Add("DialogsSiteName", result);
                }
            }
            else
            {
                result = ValidationHelper.GetString(ContextStockHelper.GetItem("DialogsSiteName"), "");
            }
        }

        return result;
    }


    /// <summary>
    /// Gets domain name of the site items are related to.
    /// </summary>
    /// <param name="attachmentDocumentId">Document identifier of attachment</param>
    private static object GetLastModified(int attachmentDocumentId)
    {
        object result = "";

        // Make sure information on site domain name is available in the context
        if (!ContextStockHelper.Contains("DialogsAttachmentLastModified"))
        {
            if (attachmentDocumentId > 0)
            {
                TreeProvider tree = new TreeProvider(CMSContext.CurrentUser);
                TreeNode node = DocumentHelper.GetDocument(attachmentDocumentId, tree);
                if (node != null)
                {
                    result = node.GetValue("DocumentModifiedWhen");
                }
            }

            // Save site name information for future use
            ContextStockHelper.Add("DialogsAttachmentLastModified", result);
        }
        else
        {
            // Get it from stock
            result = ContextStockHelper.GetItem("DialogsAttachmentLastModified");
        }

        return result;
    }


    /// <summary>
    /// Initializes attachment update control according current attachment data.
    /// </summary>
    /// <param name="dfuElem">Direct file uploader</param>
    /// <param name="data">Data container holding attachment data</param>
    private void GetAttachmentUpdateControl(ref DirectFileUploader dfuElem, IDataContainer data)
    {
        if (dfuElem != null)
        {
            string refreshType = CMSDialogHelper.GetMediaSource(SourceType);
            Guid formGuid = Guid.Empty;
            int documentId = ValidationHelper.GetInteger(data.GetValue("AttachmentDocumentID"), 0);

            // If attachment is related to the workflow 'AttachmentFormGUID' information isn't present
            if (data.ContainsColumn("AttachmentFormGUID"))
            {
                formGuid = ValidationHelper.GetGuid(data.GetValue("AttachmentFormGUID"), Guid.Empty);
            }

            dfuElem.ID = "dfuElem";
            dfuElem.SourceType = MediaSourceEnum.DocumentAttachments;
            dfuElem.ForceLoad = true;
            dfuElem.FormGUID = formGuid;
            dfuElem.DocumentID = documentId;
            dfuElem.EnableSilverlightUploader = false;

            if (TreeNodeObj != null)
            {
                // if attachment node exists
                dfuElem.NodeParentNodeID = TreeNodeObj.NodeParentID;
                dfuElem.NodeClassName = TreeNodeObj.NodeClassName;
            }
            else
            {
                // if attachment node doesn't exist
                dfuElem.NodeParentNodeID = NodeParentID;
                dfuElem.NodeClassName = "cms.file";
            }

            dfuElem.CheckPermissions = true;
            dfuElem.ControlGroup = "MediaView";
            dfuElem.AttachmentGUID = ValidationHelper.GetGuid(data.GetValue("AttachmentGUID"), Guid.Empty);
            dfuElem.ResizeToWidth = ResizeToWidth;
            dfuElem.ResizeToHeight = ResizeToHeight;
            dfuElem.ResizeToMaxSideSize = ResizeToMaxSideSize;
            dfuElem.AllowedExtensions = AllowedExtensions;
            dfuElem.ImageUrl = ResolveUrl(GetImageUrl("Design/Controls/DirectUploader/upload.png"));
            dfuElem.LoadingImageUrl = GetImageUrl("Design/Preloaders/preload16.gif");
            dfuElem.ImageHeight = 16;
            dfuElem.ImageWidth = 16;
            dfuElem.InsertMode = false;
            dfuElem.ParentElemID = refreshType;
            dfuElem.IncludeNewItemInfo = true;
            dfuElem.IsLiveSite = IsLiveSite;

            // Setting of the direct single mode
            dfuElem.UploadMode = MultifileUploaderModeEnum.DirectSingle;
            dfuElem.Width = 16;
            dfuElem.Height = 16;
            dfuElem.MaxNumberToUpload = 1;
        }
    }


    /// <summary>
    /// Initializes upload control.
    /// </summary>
    /// <param name="dfuElem">Upload control to initialize</param>
    /// <param name="data">Data row with data on related media file</param>
    public void GetLibraryUpdateControl(ref DirectFileUploader dfuElem, IDataContainer data)
    {
        if (dfuElem != null)
        {
            string siteName = GetSiteName(data, true);
            int fileId = ValidationHelper.GetInteger(data.GetValue("FileID"), 0);
            string fileName = EnsureFileName(Path.GetFileName(ValidationHelper.GetString(data.GetValue("FilePath"), "")));
            string folderPath = Path.GetDirectoryName(ValidationHelper.GetString(data.GetValue("FilePath"), ""));
            int libraryId = ValidationHelper.GetInteger(data.GetValue("FileLibraryID"), 0);

            AllowedExtensions = SettingsKeyProvider.GetStringValue(siteName + ".CMSMediaFileAllowedExtensions");

            // Initialize library info
            dfuElem.LibraryID = libraryId;
            dfuElem.MediaFileID = fileId;
            dfuElem.MediaFileName = fileName;
            dfuElem.LibraryFolderPath = folderPath;

            // Initialize general info
            dfuElem.EnableSilverlightUploader = false;
            dfuElem.CheckPermissions = true;
            dfuElem.SourceType = MediaSourceEnum.MediaLibraries;
            dfuElem.ID = "dfuElemLib";
            dfuElem.ForceLoad = true;
            dfuElem.DisplayInline = true;
            dfuElem.ControlGroup = "MediaView";
            dfuElem.ResizeToWidth = ResizeToWidth;
            dfuElem.ResizeToHeight = ResizeToHeight;
            dfuElem.ResizeToMaxSideSize = ResizeToMaxSideSize;
            dfuElem.AllowedExtensions = AllowedExtensions;
            dfuElem.ImageUrl = GetImageUrl("Design/Controls/DirectUploader/upload.png");
            dfuElem.LoadingImageUrl = GetImageUrl("Design/Preloaders/preload16.gif");
            dfuElem.ImageHeight = 16;
            dfuElem.ImageWidth = 16;
            dfuElem.InsertMode = false;
            dfuElem.ParentElemID = "LibraryUpdate";
            dfuElem.IncludeNewItemInfo = true;
            dfuElem.RaiseOnClick = true;
            dfuElem.IsLiveSite = IsLiveSite;

            // Setting of the direct single mode
            dfuElem.UploadMode = MultifileUploaderModeEnum.DirectSingle;
            dfuElem.Width = 16;
            dfuElem.Height = 16;
            dfuElem.MaxNumberToUpload = 1;
        }
    }


    /// <summary>
    /// Initializes WebDAV edit control.
    /// </summary>
    /// <param name="webDAVElem">WebDAV edit control to initialize</param>
    /// <param name="data">Data row with data on related media file</param>
    public void GetWebDAVEditControl(ref WebDAVEditControl webDAVElem, IDataContainer data)
    {
        if (webDAVElem != null)
        {
            if (webDAVElem.ControlType == WebDAVControlTypeEnum.Media)
            {
                webDAVElem.MediaFileLibraryID = ValidationHelper.GetInteger(data.GetValue("FileLibraryID"), 0);
                webDAVElem.MediaFilePath = ValidationHelper.GetString(data.GetValue("FilePath"), null);
                webDAVElem.Enabled = true;
            }
            else if (webDAVElem.ControlType == WebDAVControlTypeEnum.Attachment)
            {
                webDAVElem.Enabled = (CMSContext.CurrentUser.IsAuthorizedPerDocument(TreeNodeObj, NodePermissionsEnum.Modify) == AuthorizationResultEnum.Allowed);

                string className = ValidationHelper.GetString(data.GetValue("ClassName"), "");

                if (className.ToLower() == "cms.file")
                {
                    webDAVElem.FileName = ValidationHelper.GetString(data.GetValue("AttachmentName"), null);
                    webDAVElem.AttachmentFieldName = "FileAttachment";
                    webDAVElem.NodeAliasPath = ValidationHelper.GetString(data.GetValue("NodeAliasPath"), null);
                    webDAVElem.NodeCultureCode = ValidationHelper.GetString(data.GetValue("DocumentCulture"), null);
                }
                else
                {
                    webDAVElem.FileName = ValidationHelper.GetString(data.GetValue("AttachmentName"), null);
                    webDAVElem.NodeAliasPath = TreeNodeObj.NodeAliasPath;
                    webDAVElem.NodeCultureCode = TreeNodeObj.DocumentCulture;
                }

                // Set Group ID for live site
                webDAVElem.GroupID = IsLiveSite ? TreeNodeObj.GetIntegerValue("NodeGroupID") : 0;
            }

            webDAVElem.IsLiveSite = IsLiveSite;
            webDAVElem.UseImageButton = false;
            webDAVElem.Visible = true;
        }
    }


    /// <summary>
    /// Returns correct ID for the item (for colorizing the item when selected).
    /// </summary>
    /// <param name="dataItem">Container.DataItem</param>
    protected string GetID(object dataItem)
    {
        DataRowView dr = dataItem as DataRowView;
        if (dr != null)
        {
            IDataContainer data = new DataRowContainer(dr);
            return GetColorizeID(data);
        }

        return "";
    }


    /// <summary>
    /// Returns correct ID for the given item (for colorizing the item when selected).
    /// </summary>
    /// <param name="data">Item to get the ID of</param>
    protected string GetColorizeID(IDataContainer data)
    {
        string id = data.ContainsColumn(FileIdColumn) ? data.GetValue(FileIdColumn).ToString() : EnsureFileName(data.GetValue("FileName").ToString());

        if (String.IsNullOrEmpty(id))
        {
            // Content file
            id = data.GetValue("NodeGUID").ToString();
        }

        return id.ToLower();
    }


    /// <summary>
    /// Gets file name according available columns.
    /// </summary>
    /// <param name="data">DataRow containing data</param>
    private string GetFileName(IDataContainer data)
    {
        string fileName = "";

        if (data != null)
        {
            if (data.ContainsColumn("FileExtension"))
            {
                fileName = String.Concat(data.GetValue("FileName"), data.GetValue("FileExtension"));
            }
            else
            {
                fileName = data.GetValue(FileNameColumn).ToString();
            }
        }

        return fileName;
    }


    /// <summary>
    /// Ensures correct format of extension being displayed.
    /// </summary>
    /// <param name="extension">Extension to normalize</param>
    private static string NormalizeExtenison(string extension)
    {
        if (!string.IsNullOrEmpty(extension))
        {
            if (extension.Trim() != "<dir>")
            {
                extension = "." + extension.ToLower().TrimStart('.');
            }
            else
            {
                extension = HTMLHelper.HTMLEncode("<DIR>");
            }
        }

        return extension;
    }


    /// <summary>
    /// Gets title text.
    /// </summary>
    /// <param name="drv">Source data row</param>
    private string GetTitle(IDataContainer data, bool isContentFile)
    {
        string title = null;
        switch (SourceType)
        {
            case MediaSourceEnum.Attachment:
            case MediaSourceEnum.DocumentAttachments:
                title = ValidationHelper.GetString(data.GetValue("AttachmentTitle"), null);
                break;

            case MediaSourceEnum.Content:
                if (isContentFile)
                {
                    title = ValidationHelper.GetString(data.GetValue("AttachmentTitle"), null);
                }
                break;

            case MediaSourceEnum.MediaLibraries:
                title = ValidationHelper.GetString(data.GetValue("FileTitle"), null);
                break;
        }
        return title;
    }


    /// <summary>
    /// Gets description text.
    /// </summary>
    /// <param name="drv">Source data row</param>
    private string GetDescription(IDataContainer data, bool isContentFile)
    {
        string desc = null;
        switch (SourceType)
        {
            case MediaSourceEnum.Attachment:
            case MediaSourceEnum.DocumentAttachments:
                desc = ValidationHelper.GetString(data.GetValue("AttachmentDescription"), null);
                break;

            case MediaSourceEnum.Content:
                if (isContentFile)
                {
                    desc = ValidationHelper.GetString(data.GetValue("AttachmentDescription"), null);
                }
                break;

            case MediaSourceEnum.MediaLibraries:
                desc = ValidationHelper.GetString(data.GetValue("FileDescription"), null);
                break;
        }
        return desc;
    }

    #endregion


    #region "List view methods"

    /// <summary>
    /// Initializes list view controls.
    /// </summary>
    private void InitializeListView()
    {
        switch (SourceType)
        {
            case MediaSourceEnum.Content:
                gridList.OrderBy = "NodeOrder";
                break;

            case MediaSourceEnum.MediaLibraries:
                gridList.OrderBy = "FileName";
                break;

            case MediaSourceEnum.DocumentAttachments:
                gridList.OrderBy = "AttachmentOrder, AttachmentName, AttachmentID";
                break;

            default:
                break;
        }

        gridList.OnBeforeDataReload += gridList_OnBeforeDataReload;
    }


    void gridList_OnBeforeDataReload()
    {
        gridList.PagerForceNumberOfResults = DataHelper.GetItemsCount(DataSource);
        gridList.DataSource = DataSource;
    }


    /// <summary>
    /// Reloads list view according source type.
    /// </summary>
    private void ReloadListView()
    {
        // Fill the grid data source
        if (!DataHelper.DataSourceIsEmpty(DataSource))
        {
            // Disable sorting if is copy/move dialog
            if ((IsCopyMoveLinkDialog) && (DisplayMode == ControlDisplayModeEnum.Simple))
            {
                gridList.GridView.AllowSorting = false;
            }
            gridList.ReloadData();
        }
    }


    /// <summary>
    /// Ensures no item is selected.
    /// </summary>
    public void ResetListSelection()
    {
        if (gridList != null)
        {
            gridList.ResetSelection();
        }
    }


    /// <summary>
    /// Ensures first page is displayed in the control displaying the content.
    /// </summary>
    public void ResetPageIndex()
    {
        if (ViewMode == DialogViewModeEnum.ListView)
        {
            ListViewControl.Pager.UniPager.CurrentPage = 1;
        }
    }


    /// <summary>
    /// Returns panel with image according extension of the processed file.
    /// </summary>
    /// <param name="ext">Extension of the file used to determine icon</param>
    /// <param name="className">Class name</param>
    /// <param name="url">Url of the original image</param>
    /// <param name="item">Control inserted as a file name</param>
    /// <param name="previewUrl">Preview URL</param>
    /// <param name="width">Image width</param>
    /// <param name="height">Image height</param>
    /// <param name="isSelectable">Determines whether it can be selected</param>
    /// <param name="generateTooltip">Determines if tooltip should be generated</param>
    /// <param name="title">File title</param>
    /// <param name="description">File description</param>
    /// <param name="name">File name without extension</param>
    private Panel GetListItem(string ext, string className, string url, string previewUrl, int width, int height, Control item, bool isSelectable, bool generateTooltip, string title, string description, string name)
    {
        Panel pnl = new Panel()
        {
            CssClass = "DialogListItem" + (isSelectable ? "" : "Unselectable")
        };
        pnl.Controls.Add(new LiteralControl("<div class=\"DialogListItemNameRow\">"));

        // Cast media library folder as 'CMS.Folder'
        if ((SourceType == MediaSourceEnum.MediaLibraries) && ext.ToLower() == "<dir>")
        {
            className = "cms.folder";
        }

        // Prepare image
        Image docImg = new Image();
        docImg.Attributes["style"] = "width: 16px; height: 16px;";
        docImg.CssClass = "FloatLeft";

        if ((className == "cms.file") || (className == ""))
        {
            docImg.ImageUrl = GetFileIconUrl(ext, "List");
            // Tooltip
            if (generateTooltip)
            {
                UIHelper.EnsureTooltip(docImg, previewUrl, width, height, title, name, ext, description, null, 300);
            }
        }
        else
        {
            docImg.ImageUrl = GetDocumentTypeIconUrl(className);
        }
        pnl.Controls.Add(docImg);

        if ((isSelectable) && (item is LinkButton))
        {
            // Create clickabe compelte panel
            pnl.Attributes["onclick"] = ((LinkButton)item).Attributes["onclick"];
            ((LinkButton)item).Attributes["onclick"] = null;
        }

        // Add file name                  
        pnl.Controls.Add(new LiteralControl(String.Format("&nbsp;<span class=\"DialogListItemName\" {0}>", (!isSelectable ? "style=\"cursor:default;\"" : ""))));
        pnl.Controls.Add(item);
        pnl.Controls.Add(new LiteralControl("</span></div>"));
        return pnl;
    }


    protected void GridView_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView dr = (e.Row.DataItem as DataRowView);
            if (dr != null)
            {
                IDataContainer data = new DataRowContainer(dr);
                e.Row.Attributes["id"] = GetColorizeID(data);
            }
        }
    }


    protected object ListViewControl_OnExternalDataBound(object sender, string sourceName, object parameter)
    {
        object result = null;
        string argument = "";
        string url = "";

        bool isContent = (SourceType == MediaSourceEnum.Content);
        bool notAttachment = false;
        bool isContentFile = false;
        bool isWebDAVEnabled = CMSContext.IsWebDAVEnabled(CMSContext.CurrentSiteName);
        bool isWindowsAuthentication = RequestHelper.IsWindowsAuthentication();

        sourceName = sourceName.ToLower();

        // Prepare the data
        IDataContainer data = null;
        if (parameter is DataRowView)
        {
            data = new DataRowContainer((DataRowView)parameter);
        }
        else
        {
            // Get the data row view from parameter
            GridViewRow gvr = (parameter as GridViewRow);
            if (gvr != null)
            {
                DataRowView dr = (DataRowView)gvr.DataItem;
                if (dr != null)
                {
                    data = new DataRowContainer(dr);
                }
            }
        }

        ImageButton btn = null;
        DirectFileUploader dfuElem = null;

        string ext = null;
        string fileName = null;
        bool libraryFolder = false;
        string className = "";

        // Get site id
        int siteId = 0;
        if (data != null)
        {
            if (data.ContainsColumn("NodeSiteID"))
            {
                siteId = ValidationHelper.GetInteger(data.GetValue("NodeSiteID"), 0);
            }
            else if (data.ContainsColumn("AttachmentSiteID"))
            {
                siteId = ValidationHelper.GetInteger(data.GetValue("AttachmentSiteID"), 0);
            }
            else
            {
                siteId = ValidationHelper.GetInteger(data.GetValue("FileSiteID"), 0);
            }
        }

        // Check if site is running
        bool siteIsRunning = true;
        SiteInfo siteInfo = CMSContext.CurrentSite;
        if (siteId > 0)
        {
            siteInfo = SiteInfoProvider.GetSiteInfo(siteId);
            if (siteInfo != null)
            {
                siteIsRunning = siteInfo.Status == SiteStatusEnum.Running;
            }
        }

        switch (sourceName)
        {
            #region "Select"

            case "select":
                if (sender is ImageButton)
                {
                    btn = ((ImageButton)sender);

                    // Is current item CMS.File or attachment ?
                    isContentFile = isContent ? (data.GetValue("ClassName").ToString().ToLower() == "cms.file") : false;
                    notAttachment = isContent && !(isContentFile && (ValidationHelper.GetGuid(data.GetValue("AttachmentGUID"), Guid.Empty) != Guid.Empty));
                    if (notAttachment)
                    {
                        className = DataClassInfoProvider.GetDataClass((int)data.GetValue("NodeClassID")).ClassDisplayName;
                    }

                    ext = HTMLHelper.HTMLEncode(notAttachment ? className : data.GetValue(FileExtensionColumn).ToString().TrimStart('.'));

                    // Get file name
                    fileName = GetFileName(data);
                    libraryFolder = ((SourceType == MediaSourceEnum.MediaLibraries) && IsFullListingMode && (data.GetValue(FileExtensionColumn).ToString().ToLower() == "<dir>"));

                    if ((SourceType == MediaSourceEnum.MediaLibraries) && ((RaiseOnFileIsNotInDatabase(fileName) == null) && (DisplayMode == ControlDisplayModeEnum.Simple)))
                    {
                        // If folders are displayed as well show SELECT button icon
                        if (libraryFolder && !IsCopyMoveLinkDialog)
                        {
                            btn.ImageUrl = ResolveUrl(ImagesPath + "subdocument.png");
                            btn.ToolTip = GetString("dialogs.list.actions.showsubfolders");
                            btn.Attributes["onclick"] = String.Format("SetAction('morefolderselect', {0}); RaiseHiddenPostBack(); return false;", ScriptHelper.GetString(fileName));
                        }
                        else if (libraryFolder && IsCopyMoveLinkDialog)
                        {
                            btn.ImageUrl = ResolveUrl(ImagesPath + "next.png");
                            btn.ToolTip = GetString("general.select");
                            btn.Attributes["onclick"] = String.Format("SetAction('copymoveselect', {0}); RaiseHiddenPostBack(); return false;", ScriptHelper.GetString(fileName));
                        }
                        else
                        {
                            // If media file not imported yet - display warning sign
                            btn.ImageUrl = ResolveUrl(ImagesPath + "warning.png");
                            btn.ToolTip = GetString("media.file.import");
                            btn.Attributes["onclick"] = String.Format("SetAction('importfile', {0}); RaiseHiddenPostBack(); return false;", ScriptHelper.GetString(data.GetValue("FileName").ToString()));
                        }
                    }
                    else
                    {
                        // Check if item is selectable, if not remove select action button
                        bool isSelectable = CMSDialogHelper.IsItemSelectable(SelectableContent, ext, isContentFile);
                        if (!isSelectable)
                        {
                            btn.ImageUrl = ResolveUrl(ImagesPath + "transparent.png");
                            btn.ToolTip = "";
                            btn.Attributes["style"] = "margin:0px 3px;cursor:default;";
                            btn.Enabled = false;
                        }
                        else
                        {
                            argument = RaiseOnGetArgumentSet(data);

                            // Get item URL
                            url = RaiseOnGetListItemUrl(data, false, notAttachment);

                            // Initialize command
                            btn.Attributes["onclick"] = String.Format("ColorizeRow({0}); SetSelectAction({1}); return false;", ScriptHelper.GetString(GetColorizeID(data)), ScriptHelper.GetString(argument + "|URL|" + url));

                            result = btn;
                        }
                    }
                }
                break;

            #endregion

            #region "Select sub docs"

            case "selectsubdocs":
                if (sender is ImageButton)
                {
                    btn = ((ImageButton)sender);

                    int nodeId = ValidationHelper.GetInteger(data.GetValue("NodeID"), 0);

                    if (IsFullListingMode)
                    {
                        // Check if item is selectable, if not remove select action button
                        // Initialize command
                        btn.Attributes["onclick"] = String.Format("SetParentAction('{0}'); return false;", nodeId);
                    }
                    else
                    {
                        btn.Visible = false;
                    }
                }
                break;

            #endregion

            #region "Select sub folders"

            case "selectsubfolders":
                if (sender is ImageButton)
                {
                    btn = ((ImageButton)sender);
                    if (btn != null)
                    {
                        string folderName = ValidationHelper.GetString(data.GetValue("FileName"), "");
                        ext = ValidationHelper.GetString(data.GetValue(FileExtensionColumn), "");

                        if (IsCopyMoveLinkDialog || (IsFullListingMode && (DisplayMode == ControlDisplayModeEnum.Default) && (ext == "<dir>")))
                        {
                            // Initialize command
                            btn.Attributes["onclick"] = String.Format("SetLibParentAction({0}); return false;", ScriptHelper.GetString(folderName));
                        }
                        else
                        {
                            btn.Visible = false;
                        }
                    }
                }
                break;

            #endregion

            #region "View"

            case "view":
                if (sender is ImageButton)
                {
                    btn = ((ImageButton)sender);

                    // Check if current item is library folder
                    fileName = GetFileName(data);
                    libraryFolder = ((SourceType == MediaSourceEnum.MediaLibraries) && IsFullListingMode && (data.GetValue(FileExtensionColumn).ToString().ToLower() == "<dir>"));

                    // Is current item CMS.File or attachment ?
                    isContentFile = isContent ? (data.GetValue("ClassName").ToString().ToLower() == "cms.file") : false;
                    notAttachment = isContent && !(isContentFile && (ValidationHelper.GetGuid(data.GetValue("AttachmentGUID"), Guid.Empty) != Guid.Empty));
                    if (!notAttachment && !libraryFolder && siteIsRunning)
                    {
                        // Get item URL
                        url = RaiseOnGetListItemUrl(data, false, false);

                        if (String.IsNullOrEmpty(url))
                        {
                            btn.ImageUrl = GetImageUrl("/Design/Controls/UniGrid/Actions/Viewdisabled.png");
                            btn.OnClientClick = "return false;";
                            btn.Attributes["style"] = "margin:0px 3px;cursor:default;";
                            btn.Enabled = false;
                        }
                        else
                        {
                            // Add latest version requirement for live site
                            if (IsLiveSite)
                            {
                                // Check version history ID
                                int versionHistoryId = VersionHistoryID;
                                if (versionHistoryId > 0)
                                {
                                    // Add requirement for latest version of files for current document
                                    string newparams = "latestforhistoryid=" + versionHistoryId;
                                    newparams += "&hash=" + ValidationHelper.GetHashString("h" + versionHistoryId);

                                    url = URLHelper.AppendQuery(url, newparams);
                                }
                            }

                            string finalUrl = URLHelper.ResolveUrl(url);
                            btn.OnClientClick = String.Format("javascript: window.open({0}); return false;", ScriptHelper.GetString(finalUrl));
                        }
                    }
                    else
                    {
                        btn.Visible = false;
                    }
                }
                break;

            #endregion

            #region "Edit"

            case "edit":
                if (sender is ImageButton)
                {
                    // Is current item CMS.File or attachment ?
                    isContentFile = isContent ? (data.GetValue("ClassName").ToString().ToLower() == "cms.file") : false;
                    notAttachment = isContent && !(isContentFile && (ValidationHelper.GetGuid(data.GetValue("AttachmentGUID"), Guid.Empty) != Guid.Empty));

                    // Get file extension
                    ext = (notAttachment ? "" : data.GetValue(FileExtensionColumn).ToString().TrimStart('.'));
                    Guid guid = Guid.Empty;
                    ImageButton imgEdit = (ImageButton)sender;
                    if ((SourceType == MediaSourceEnum.MediaLibraries) && siteIsRunning)
                    {
                        libraryFolder = (IsFullListingMode && (data.GetValue(FileExtensionColumn).ToString().ToLower() == "<dir>"));

                        if (!libraryFolder)
                        {
                            guid = ValidationHelper.GetGuid(data.GetValue("FileGUID"), Guid.Empty);
                            imgEdit.AlternateText = String.Format("{0}|MediaFileGUID={1}&sitename={2}", ext, guid, GetSiteName(data, true));
                            imgEdit.PreRender += img_PreRender;
                            imgEdit.ImageUrl = ResolveUrl(ImagesPath + "edit.png");
                        }
                        else
                        {
                            imgEdit.Visible = false;
                        }
                    }
                    else if (!notAttachment && siteIsRunning)
                    {
                        string nodeIdQuery = "";
                        if (SourceType == MediaSourceEnum.Content)
                        {
                            nodeIdQuery = "&nodeId=" + data.GetValue("NodeID");
                        }

                        // Get the node workflow
                        VersionHistoryID = ValidationHelper.GetInteger(data.GetValue("DocumentCheckedOutVersionHistoryID"), 0);

                        guid = ValidationHelper.GetGuid(data.GetValue("AttachmentGUID"), Guid.Empty);
                        imgEdit.AlternateText = String.Format("{0}|AttachmentGUID={1}&sitename={2}{3}&versionHistoryId={4}", ext, guid, GetSiteName(data, false), nodeIdQuery, VersionHistoryID);
                        imgEdit.PreRender += img_PreRender;
                        imgEdit.ImageUrl = ResolveUrl(ImagesPath + "edit.png");
                    }
                    else
                    {
                        imgEdit.Visible = false;
                    }
                }
                break;

            #endregion

            #region "WebDAV edit"

            case "webdavedit":
                {
                    // Prepare the data
                    DataRowView dr = parameter as DataRowView;
                    if (dr != null)
                    {
                        data = new DataRowContainer(dr);
                    }

                    if ((SourceType == MediaSourceEnum.MediaLibraries) && siteIsRunning)
                    {
                        PlaceHolder plcUpd = new PlaceHolder();
                        plcUpd.ID = "plcUdateColumn";

                        Panel pnlBlock = new Panel();
                        pnlBlock.ID = "pnlBlock";

                        pnlBlock.CssClass = "TableCell";
                        plcUpd.Controls.Add(pnlBlock);

                        string fileExtension = ValidationHelper.GetString(data.GetValue("FileExtension"), null);

                        // If the WebDAV is enabled and windows authentication and extension is allowed
                        if (isWebDAVEnabled && isWindowsAuthentication && WebDAVSettings.IsExtensionAllowedForEditMode(fileExtension, CMSContext.CurrentSiteName))
                        {
                            // Dynamically load control
                            WebDAVEditControl webDAVElem = Page.LoadControl("~/CMSModules/MediaLibrary/Controls/WebDAV/MediaFileWebDAVEditControl.ascx") as WebDAVEditControl;

                            // Set editor's properties
                            if (webDAVElem != null)
                            {
                                GetWebDAVEditControl(ref webDAVElem, data);
                                // Add to panel
                                pnlBlock.Controls.Add(webDAVElem);

                                columnUpdateVisible = true;
                            }
                        }

                        return plcUpd;
                    }
                    else if ((SourceType == MediaSourceEnum.Content) && (OutputFormat != OutputFormatEnum.Custom) && siteIsRunning)
                    {
                        PlaceHolder plcUpd = new PlaceHolder();
                        plcUpd.ID = "plcUdateColumn";
                        Panel pnlBlock = new Panel();
                        pnlBlock.ID = "pnlBlock";

                        pnlBlock.CssClass = "TableCell";
                        plcUpd.Controls.Add(pnlBlock);

                        string fileExtension = ValidationHelper.GetString(data.GetValue("AttachmentExtension"), null);

                        // If the WebDAV is enabled and windows authentication and extension is allowed
                        if (isWebDAVEnabled && isWindowsAuthentication && WebDAVSettings.IsExtensionAllowedForEditMode(fileExtension, CMSContext.CurrentSiteName))
                        {
                            // Dynamically load control
                            WebDAVEditControl webDAVElem = Page.LoadControl("~/CMSModules/WebDAV/Controls/AttachmentWebDAVEditControl.ascx") as WebDAVEditControl;

                            // Set editor's properties
                            if (webDAVElem != null)
                            {
                                webDAVElem.Visible = false;
                                GetWebDAVEditControl(ref webDAVElem, data);
                                // Add to panel
                                pnlBlock.Controls.Add(webDAVElem);

                                columnUpdateVisible = true;
                            }
                        }
                        return plcUpd;
                    }
                }
                break;

            #endregion

            #region "Edit library ui"

            case "editlibraryui":
                if (sender is ImageButton)
                {
                    ImageButton imgEdit = (ImageButton)sender;
                    if (imgEdit != null)
                    {
                        // Get file name
                        fileName = GetFileName(data);

                        bool notInDatabase = ((RaiseOnFileIsNotInDatabase(fileName) == null) && (DisplayMode == ControlDisplayModeEnum.Simple));

                        // Check if current item is library folder
                        libraryFolder = ((SourceType == MediaSourceEnum.MediaLibraries) && IsFullListingMode && notInDatabase && (data.GetValue(FileExtensionColumn).ToString().ToLower() == "<dir>"));

                        ext = data.ContainsColumn("FileExtension") ? data.GetValue("FileExtension").ToString() : data.GetValue("Extension").ToString();

                        if (libraryFolder)
                        {
                            imgEdit.Visible = false;
                        }
                        else if (notInDatabase)
                        {
                            imgEdit.ImageUrl = ResolveUrl(ImagesPath + "editdisabled.png");
                            imgEdit.Attributes["style"] = "margin:0px 3px;cursor:default;";
                            imgEdit.Enabled = false;
                        }
                        else
                        {
                            imgEdit.OnClientClick = String.Format("$j('#hdnFileOrigName').attr('value', {0}); SetAction('editlibraryui', {1}); RaiseHiddenPostBack(); return false;", ScriptHelper.GetString(EnsureFileName(fileName)), ScriptHelper.GetString(fileName));
                        }
                    }
                }
                break;

            #endregion

            #region "Delete"

            case "delete":
                if (sender is ImageButton)
                {
                    // Get file name
                    fileName = GetFileName(data);

                    // Check if current item is library folder
                    libraryFolder = ((SourceType == MediaSourceEnum.MediaLibraries) && IsFullListingMode && (data.GetValue(FileExtensionColumn).ToString().ToLower() == "<dir>"));

                    ImageButton btnDelete = (ImageButton)sender;
                    if (btnDelete != null)
                    {
                        //btnDelete.ImageUrl = ResolveUrl(ImagesPath + "Delete.png");
                        btnDelete.ToolTip = GetString("general.delete");

                        if (((RaiseOnFileIsNotInDatabase(fileName) == null) && (DisplayMode == ControlDisplayModeEnum.Simple)))
                        {
                            if (libraryFolder)
                            {
                                btnDelete.Visible = false;
                            }
                            else
                            {
                                btnDelete.Attributes["onclick"] = String.Format("if(DeleteMediaFileConfirmation() == false){{return false;}} SetAction('deletefile',{0}); RaiseHiddenPostBack(); return false;", ScriptHelper.GetString(fileName));
                            }
                        }
                        else
                        {
                            btnDelete.Attributes["onclick"] = String.Format("if(DeleteMediaFileConfirmation() == false){{return false;}} SetAction('deletefile',{0}); RaiseHiddenPostBack(); return false;", ScriptHelper.GetString(fileName));
                        }
                    }
                }
                break;

            #endregion

            #region "Name"

            case "name":
                {
                    // Is current item CMS.File or attachment ?
                    isContentFile = isContent ? (data.GetValue("ClassName").ToString().ToLower() == "cms.file") : false;
                    notAttachment = isContent && !(isContentFile && (ValidationHelper.GetGuid(data.GetValue("AttachmentGUID"), Guid.Empty) != Guid.Empty));

                    // Get name and extension                
                    string fileNameColumn = FileNameColumn;
                    if (notAttachment)
                    {
                        fileNameColumn = "DocumentName";
                    }
                    string name = HTMLHelper.HTMLEncode(data.GetValue(fileNameColumn).ToString());
                    ext = (notAttachment ? "" : data.GetValue(FileExtensionColumn).ToString().TrimStart('.'));

                    if (SourceType == MediaSourceEnum.DocumentAttachments)
                    {
                        name = Path.GetFileNameWithoutExtension(name);
                    }

                    // Width & height
                    int width = (data.ContainsColumn(FileWidthColumn) ? ValidationHelper.GetInteger(data.GetValue(FileWidthColumn), 0) : 0);
                    int height = (data.ContainsColumn(FileHeightColumn) ? ValidationHelper.GetInteger(data.GetValue(FileHeightColumn), 0) : 0);

                    string cmpltExt = (notAttachment ? "" : "." + ext);
                    fileName = (cmpltExt != "") ? name.Replace(cmpltExt, "") : name;
                    if (isContent && !notAttachment)
                    {
                        string attachmentName = Path.GetFileNameWithoutExtension(data.GetValue("AttachmentName").ToString());
                        if (fileName.ToLower() != attachmentName.ToLower())
                        {
                            fileName += String.Format(" ({0})", HTMLHelper.HTMLEncode(data.GetValue("AttachmentName").ToString()));
                        }
                    }

                    className = isContent ? HTMLHelper.HTMLEncode(data.GetValue("ClassName").ToString().ToLower()) : "";
                    isContentFile = (className == "cms.file");

                    // Check if item is selectable
                    if (!CMSDialogHelper.IsItemSelectable(SelectableContent, ext, isContentFile))
                    {
                        LiteralControl ltlName = new LiteralControl(fileName);

                        // Get final panel
                        result = GetListItem(ext, className, "", "", width, height, ltlName, false, false, GetTitle(data, isContentFile), GetDescription(data, isContentFile), name);
                    }
                    else
                    {
                        // Make a file name link
                        LinkButton lnkBtn = new LinkButton()
                        {
                            ID = "n",
                            Text = HTMLHelper.HTMLEncode(fileName)
                        };

                        // Is current item CMS.File or attachment ?
                        isContentFile = isContent ? (data.GetValue("ClassName").ToString().ToLower() == "cms.file") : false;
                        notAttachment = isContent && !(isContentFile && (ValidationHelper.GetGuid(data.GetValue("AttachmentGUID"), Guid.Empty) != Guid.Empty));

                        fileName = GetFileName(data);

                        // Try to get imported file row
                        bool isImported = true;
                        IDataContainer importedData = null;
                        if (DisplayMode == ControlDisplayModeEnum.Simple)
                        {
                            importedData = RaiseOnFileIsNotInDatabase(fileName);
                            isImported = (importedData != null);
                        }
                        else
                        {
                            importedData = data;
                        }

                        if (!isImported)
                        {
                            importedData = data;
                        }
                        else
                        {
                            // Update WIDTH
                            if (importedData.ContainsColumn(FileWidthColumn))
                            {
                                width = ValidationHelper.GetInteger(importedData[FileWidthColumn], 0);
                            }

                            // Update HEIGHT
                            if (importedData.ContainsColumn(FileHeightColumn))
                            {
                                height = ValidationHelper.GetInteger(importedData[FileHeightColumn], 0);
                            }
                        }

                        argument = RaiseOnGetArgumentSet(data);
                        url = RaiseOnGetListItemUrl(importedData, false, notAttachment);

                        string previewUrl = RaiseOnGetListItemUrl(importedData, true, notAttachment);
                        if (!String.IsNullOrEmpty(previewUrl))
                        {
                            // Add chset
                            string chset = Guid.NewGuid().ToString();
                            previewUrl = URLHelper.AddParameterToUrl(previewUrl, "chset", chset);
                        }

                        // Add latest version requirement for live site
                        if (!String.IsNullOrEmpty(previewUrl) && IsLiveSite)
                        {
                            int versionHistoryId = VersionHistoryID;
                            if (versionHistoryId > 0)
                            {
                                // Add requirement for latest version of files for current document
                                string newparams = "latestforhistoryid=" + versionHistoryId;
                                newparams += "&hash=" + ValidationHelper.GetHashString("h" + versionHistoryId);

                                //url = URLHelper.AppendQuery(url, newparams);
                                previewUrl = URLHelper.AppendQuery(previewUrl, newparams);
                            }
                        }

                        // Check if current item is library folder
                        libraryFolder = ((SourceType == MediaSourceEnum.MediaLibraries) && IsFullListingMode &&
                            (data.GetValue(FileExtensionColumn).ToString().ToLower() == "<dir>"));

                        if ((SourceType == MediaSourceEnum.MediaLibraries) && (!isImported && (DisplayMode == ControlDisplayModeEnum.Simple)))
                        {
                            if (libraryFolder && !IsCopyMoveLinkDialog)
                            {
                                lnkBtn.Attributes["onclick"] = String.Format("SetAction('morefolderselect', {0}); RaiseHiddenPostBack(); return false;", ScriptHelper.GetString(fileName));
                            }
                            else if (libraryFolder && IsCopyMoveLinkDialog)
                            {
                                lnkBtn.Attributes["onclick"] = String.Format("SetAction('copymoveselect', {0}); RaiseHiddenPostBack(); return false;", ScriptHelper.GetString(fileName));
                            }
                            else
                            {
                                lnkBtn.Attributes["onclick"] = String.Format("ColorizeRow({0}); SetAction('importfile', {1}); RaiseHiddenPostBack(); return false;", ScriptHelper.GetString(GetColorizeID(data)), ScriptHelper.GetString(fileName));
                            }
                        }
                        else
                        {
                            // Initialize command
                            lnkBtn.Attributes["onclick"] = String.Format("ColorizeRow({0}); SetSelectAction({1}); return false;", ScriptHelper.GetString(GetColorizeID(data)), ScriptHelper.GetString(String.Format("{0}|URL|{1}", argument, url)));
                        }

                        // Get final panel
                        result = GetListItem(ext, className, url, previewUrl, width, height, lnkBtn, true, isImported && siteIsRunning, GetTitle(data, isContentFile), GetDescription(data, isContentFile), name);
                    }
                }
                break;

            #endregion

            #region "Type"

            case "type":
                {
                    if (isContent)
                    {
                        // Is current item CMS.File or attachment ?
                        isContentFile = (data.GetValue("ClassName").ToString().ToLower() == "cms.file");
                        notAttachment = !(isContentFile && (ValidationHelper.GetGuid(data.GetValue("AttachmentGUID"), Guid.Empty) != Guid.Empty));

                        if (notAttachment || (OutputFormat == OutputFormatEnum.HTMLLink) || (OutputFormat == OutputFormatEnum.BBLink))
                        {
                            return HTMLHelper.HTMLEncode(ResHelper.LocalizeString(data.GetValue("ClassDisplayName").ToString()));
                        }
                    }
                    result = NormalizeExtenison(data.GetValue(FileExtensionColumn).ToString());
                }
                break;

            #endregion

            #region "Size"

            case "size":
                {
                    // Is current item CMS.File or attachment ?
                    isContentFile = isContent ? (data.GetValue("ClassName").ToString().ToLower() == "cms.file") : false;
                    notAttachment = isContent && !(isContentFile && (ValidationHelper.GetGuid(data.GetValue("AttachmentGUID"), Guid.Empty) != Guid.Empty));

                    if (!notAttachment)
                    {
                        long size = 0;
                        if (data.GetValue(FileExtensionColumn).ToString() != "<dir>")
                        {
                            if (data.ContainsColumn(FileSizeColumn))
                            {
                                size = ValidationHelper.GetLong(data.GetValue(FileSizeColumn), 0);
                            }
                            else if (data.ContainsColumn("Size"))
                            {
                                IDataContainer importedData = RaiseOnFileIsNotInDatabase(GetFileName(data));
                                size = ValidationHelper.GetLong((importedData != null) ? importedData["FileSize"] : data.GetValue("Size"), 0);
                            }
                        }
                        else
                        {
                            return "";
                        }
                        result = DataHelper.GetSizeString(size);
                    }
                }
                break;

            #endregion

            #region "Attachment modified"

            case "attachmentmodified":
            case "attachmentmodifiedtooltip":
                {
                    // If attachment is related to the workflow 'AttachmentLastModified' information isn't present
                    if (!data.ContainsColumn("AttachmentLastModified"))
                    {
                        int attachmentDocumentId = ValidationHelper.GetInteger(data.GetValue("AttachmentDocumentID"), 0);

                        result = GetLastModified(attachmentDocumentId);
                    }
                    else
                    {
                        result = data.GetValue("AttachmentLastModified").ToString();
                    }

                    bool displayGMT = (sourceName == "attachmentmodifiedtooltip");
                    result = TimeZoneHelper.ConvertToUserTimeZone(ValidationHelper.GetDateTime(result, DataHelper.DATETIME_NOT_SELECTED), displayGMT, CMSContext.CurrentUser, siteInfo);
                }
                break;

            #endregion

            #region "Attachment update"

            case "attachmentupdate":
                {
                    // Dynamically load uploader control
                    dfuElem = Page.LoadControl("~/CMSModules/Content/Controls/Attachments/DirectFileUploader/DirectFileUploader.ascx") as DirectFileUploader;

                    Panel updatePanel = new Panel()
                    {
                        ID = "updatePanel",
                        Width = ((isWebDAVEnabled && isWindowsAuthentication) ? 32 : 16)
                    };
                    updatePanel.Style.Add("margin", "0 auto");

                    // Initialize update control
                    GetAttachmentUpdateControl(ref dfuElem, data);

                    dfuElem.DisplayInline = true;
                    updatePanel.Controls.Add(dfuElem);

                    // Add Attachment WebDAV edit control
                    string attachmentExtension = ValidationHelper.GetString(data.GetValue(FileExtensionColumn), null);
                    Guid formGUID = ValidationHelper.GetGuid(data.GetValue("AttachmentFormGUID"), Guid.Empty);

                    // If the WebDAV is enabled and windows authentication and extension is allowed and form GUID is empty
                    if (isWebDAVEnabled && isWindowsAuthentication && (formGUID == Guid.Empty) && WebDAVSettings.IsExtensionAllowedForEditMode(attachmentExtension, CMSContext.CurrentSiteName))
                    {
                        // Dynamically load control
                        WebDAVEditControl webDAVElem = Page.LoadControl("~/CMSModules/WebDAV/Controls/AttachmentWebDAVEditControl.ascx") as WebDAVEditControl;

                        GetWebDAVEditControl(ref webDAVElem, data);

                        updatePanel.Controls.Add(webDAVElem);
                    }

                    result = updatePanel;
                }
                break;

            #endregion

            #region "Library update"

            case "libraryupdate":
                {
                    Panel updatePanel = new Panel();
                    updatePanel.Style.Add("margin", "0 auto");
                    updatePanel.Width = ((isWebDAVEnabled && isWindowsAuthentication) ? 32 : 16);

                    libraryFolder = ((SourceType == MediaSourceEnum.MediaLibraries) && IsFullListingMode &&
                            (data.GetValue(FileExtensionColumn).ToString().ToLower() == "<dir>"));

                    // Get info on imported file
                    fileName = GetFileName(data);

                    IDataContainer existingData = RaiseOnFileIsNotInDatabase(fileName);
                    bool hasModifyPermission = RaiseOnGetModifyPermission(data);

                    ImageButton imgBtn = new ImageButton()
                    {
                        Enabled = false,
                        ImageUrl = ResolveUrl(GetImageUrl("Design/Controls/DirectUploader/uploaddisabled.png"))
                    };
                    imgBtn.Attributes["style"] = "cursor: default;";
                    updatePanel.Controls.Add(imgBtn);

                    if (hasModifyPermission && (existingData != null))
                    {
                        // Dynamically load uploader control
                        dfuElem = Page.LoadControl("~/CMSModules/Content/Controls/Attachments/DirectFileUploader/DirectFileUploader.ascx") as DirectFileUploader;
                        if (dfuElem != null)
                        {
                            // Initialize update control
                            GetLibraryUpdateControl(ref dfuElem, existingData);

                            updatePanel.Controls.Add(dfuElem);
                        }

                        imgBtn.Style.Add("display", "none");
                    }
                    else
                    {
                        updatePanel.Visible = !libraryFolder || !hasModifyPermission;
                    }

                    // Add WebDAV icon
                    if (existingData != null)
                    {
                        string siteName = GetSiteName(existingData, true);
                        string extension = ValidationHelper.GetString(existingData["FileExtension"], null);

                        // If the WebDAV is enabled and windows authentication and extension is allowed
                        if (isWebDAVEnabled && isWindowsAuthentication && WebDAVSettings.IsExtensionAllowedForEditMode(extension, siteName))
                        {
                            // Dynamically load control
                            WebDAVEditControl webdavElem = Page.LoadControl("~/CMSModules/MediaLibrary/Controls/WebDAV/MediaFileWebDAVEditControl.ascx") as WebDAVEditControl;

                            // Set editor's properties
                            if (webdavElem != null)
                            {
                                // Initialize update control
                                GetWebDAVEditControl(ref webdavElem, existingData);

                                updatePanel.Controls.Add(webdavElem);
                            }
                        }
                    }

                    result = updatePanel;
                }
                break;

            #endregion

            #region "Attachment delete"

            case "attachmentdelete":
                if (sender is ImageButton)
                {
                    // Initialize DELETE button
                    btn = ((ImageButton)sender);
                    btn.OnClientClick = String.Format("if(DeleteConfirmation() == false){{return false;}} SetAction('attachmentdelete', '{0}'); RaiseHiddenPostBack(); return false;", data.GetValue(FileIdColumn));
                }
                break;

            #endregion

            #region "Attachment moveup"

            case "attachmentmoveup":
                if (sender is ImageButton)
                {
                    // Initialize MOVE UP button
                    btn = ((ImageButton)sender);

                    // Get attachment ID
                    Guid attachmentGuid = ValidationHelper.GetGuid(data.GetValue("AttachmentGUID"), Guid.Empty);
                    btn.OnClientClick = String.Format("SetAction('attachmentmoveup', '{0}'); RaiseHiddenPostBack(); return false;", attachmentGuid);
                }
                break;
            #endregion

            #region "Attachment movedown"

            case "attachmentmovedown":
                if (sender is ImageButton)
                {
                    // Initialize MOVE DOWN button
                    btn = ((ImageButton)sender);

                    // Get attachment ID
                    Guid attachmentGuid = ValidationHelper.GetGuid(data.GetValue("AttachmentGUID"), Guid.Empty);
                    btn.OnClientClick = String.Format("SetAction('attachmentmovedown', '{0}'); RaiseHiddenPostBack(); return false;", attachmentGuid);
                }
                break;

            #endregion

            #region "Attachment edit"

            case "attachmentedit":
                if (sender is ImageButton)
                {
                    // Get file extension
                    string attExtension = ValidationHelper.GetString(data.GetValue("AttachmentExtension"), "").ToLower();
                    Guid attGuid = ValidationHelper.GetGuid(data.GetValue("AttachmentGUID"), Guid.Empty);

                    ImageButton attImg = (ImageButton)sender;
                    attImg.AlternateText = String.Format("{0}|AttachmentGUID={1}&sitename={2}&versionHistoryId={3}", attExtension, attGuid, GetSiteName(data, false), VersionHistoryID);
                    attImg.PreRender += img_PreRender;
                }
                break;

            #endregion

            #region "Library extension"

            case "extension":
                {
                    result = data.ContainsColumn("FileExtension") ? data.GetValue("FileExtension").ToString() : data.GetValue("Extension").ToString();
                    result = NormalizeExtenison(result.ToString());
                }
                break;

            #endregion

            #region "Modified"

            case "modified":
            case "modifiedtooltip":
                {
                    bool displayGMT = (sourceName == "attachmentmodifiedtooltip");
                    result = data.ContainsColumn("FileModifiedWhen") ? data.GetValue("FileModifiedWhen").ToString() : data.GetValue("Modified").ToString();
                    result = TimeZoneHelper.ConvertToUserTimeZone(ValidationHelper.GetDateTime(result, DataHelper.DATETIME_NOT_SELECTED), displayGMT, CMSContext.CurrentUser, siteInfo);
                }
                break;

            #endregion

            #region "Document modified when"

            case "documentmodifiedwhen":
            case "filemodifiedwhen":
            case "documentmodifiedwhentooltip":
            case "filemodifiedwhentooltip":
                {
                    bool displayGMT = ((sourceName == "documentmodifiedwhentooltip") || (sourceName == "filemodifiedwhentooltip"));
                    result = TimeZoneHelper.ConvertToUserTimeZone(ValidationHelper.GetDateTime(parameter, DataHelper.DATETIME_NOT_SELECTED), displayGMT, CMSContext.CurrentUser, siteInfo);
                }
                break;

            #endregion
        }

        return result;
    }


    protected void img_PreRender(object sender, EventArgs e)
    {
        ImageButton img = (ImageButton)sender;

        string[] args = img.AlternateText.Split('|');
        if (args.Length == 2)
        {
            string refreshType = CMSDialogHelper.GetMediaSource(SourceType);
            int parentId = QueryHelper.GetInteger("parentId", 0);
            Guid formGuid = QueryHelper.GetGuid("formGuid", Guid.Empty);
            string query = String.Format("?clientid={0}{1}&refresh=1&refaction=0{2}&{3}", HTMLHelper.HTMLEncode(refreshType), ((parentId > 0) ? "&parentId=" + parentId : ""), ((formGuid != Guid.Empty) ? "&formGuid=" + formGuid : ""), args[1]);
            // Get validation hash for current image
            query = URLHelper.AddUrlParameter(query, "hash", QueryHelper.GetHash(query));

            img.Attributes["onclick"] = ImageHelper.IsSupportedByImageEditor(args[0]) ?
                String.Format("if(!($j(this).hasClass('Edited'))){{ EditImage(\"{0}\"); }} return false;", query) :
                String.Format("if(!($j(this).hasClass('Edited'))){{ Edit(\"{0}\"); }} return false;", query);

            img.ToolTip = GetString("general.edit");
            img.AlternateText = GetString("general.edit");
        }
    }

    #endregion


    #region "Tiles view methods"

    /// <summary>
    /// Initializes controls for the tiles view mode.
    /// </summary>
    private void InitializeTilesView()
    {
        // Initialize page size
        bool isAttachmentTab = SourceType == MediaSourceEnum.DocumentAttachments;
        pageSizeTiles.Items = isAttachmentTab ? new string[] { "20", "40", "100" } : new string[] { "16", "32", "100" };
        pagerElemTiles.PageSize = (pageSizeTiles.SelectedValue == "-1") ? 0 : ValidationHelper.GetInteger(pageSizeTiles.SelectedValue, (isAttachmentTab ? 20 : 16));

        // Basic control properties
        repTilesView.HideControlForZeroRows = true;

        // UniPager properties        
        pagerElemTiles.GroupSize = PAGER_GROUP_SIZE;
        pagerElemTiles.DisplayFirstLastAutomatically = false;
        pagerElemTiles.DisplayPreviousNextAutomatically = false;
        pagerElemTiles.HidePagerForSinglePage = true;
        pagerElemTiles.PagerMode = UniPagerMode.PostBack;
    }


    /// <summary>
    /// Loads content for media libraries tile view element.
    /// </summary>
    private void ReloadTilesView()
    {
        // Connects repeater with data source
        if (!DataHelper.DataSourceIsEmpty(DataSource))
        {
            repTilesView.DataSource = DataSource;
            repTilesView.DataBind();
        }
    }


    protected void TilesViewControl_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        #region "Load item data"

        string className = "";
        string fileNameColumn = FileNameColumn;

        IDataContainer data = new DataRowContainer((DataRowView)e.Item.DataItem);

        bool isContent = (SourceType == MediaSourceEnum.Content);

        bool isContentFile = isContent ? (data.GetValue("ClassName").ToString().ToLower() == "cms.file") : false;
        bool notAttachment = isContent && !(isContentFile && (data.GetValue("AttachmentGUID") != DBNull.Value));
        if (notAttachment)
        {
            className = DataClassInfoProvider.GetDataClass((int)data.GetValue("NodeClassID")).ClassDisplayName;

            fileNameColumn = "DocumentName";
        }
        else
        {
            fileNameColumn = "AttachmentName";
        }

        // Get basic information on file (use field properties for CMS.File | attachment, document columns otherwise)
        string fileName = HTMLHelper.HTMLEncode((data.ContainsColumn(fileNameColumn) ? data.GetValue(fileNameColumn).ToString() : data.GetValue(FileNameColumn).ToString()));
        string ext = notAttachment ? className : data.GetValue(FileExtensionColumn).ToString().TrimStart('.');
        string argument = RaiseOnGetArgumentSet(data);

        // Get full media library file name
        bool isInDatabase = true;
        string fullFileName = GetFileName(data);
        IDataContainer importedMediaData = null;

        if ((SourceType == MediaSourceEnum.MediaLibraries) && (DisplayMode == ControlDisplayModeEnum.Simple))
        {
            importedMediaData = RaiseOnFileIsNotInDatabase(fullFileName);
            isInDatabase = (importedMediaData != null);
        }

        string selectUrl = RaiseOnGetTilesThumbsItemUrl(data, false, 0, 0, 0, notAttachment);
        bool isSelectable = CMSDialogHelper.IsItemSelectable(SelectableContent, ext, isContentFile);

        bool libraryFolder = ((SourceType == MediaSourceEnum.MediaLibraries) && IsFullListingMode && (data.GetValue(FileExtensionColumn).ToString().ToLower() == "<dir>"));
        bool libraryUiFolder = libraryFolder && !((DisplayMode == ControlDisplayModeEnum.Simple) && isInDatabase);

        int width = 0;
        int height = 0;
        // Width
        if (data.ContainsColumn(FileWidthColumn))
        {
            width = ValidationHelper.GetInteger(data.GetValue(FileWidthColumn), 0);
        }
        else if (isInDatabase && (DisplayMode == ControlDisplayModeEnum.Simple) && importedMediaData.ContainsColumn(FileWidthColumn))
        {
            width = ValidationHelper.GetInteger(importedMediaData.GetValue(FileWidthColumn), 0);
        }
        // Height
        if (data.ContainsColumn(FileHeightColumn))
        {
            height = ValidationHelper.GetInteger(data.GetValue(FileHeightColumn), 0);
        }
        else if (isInDatabase && (DisplayMode == ControlDisplayModeEnum.Simple) && importedMediaData.ContainsColumn(FileHeightColumn))
        {
            height = ValidationHelper.GetInteger(importedMediaData.GetValue(FileHeightColumn), 0);
        }

        // Get item image URL from the parent set
        string previewUrl = "";
        if ((SourceType == MediaSourceEnum.MediaLibraries) && isInDatabase && (DisplayMode == ControlDisplayModeEnum.Simple))
        {
            previewUrl = RaiseOnGetTilesThumbsItemUrl(importedMediaData, true, 0, 0, 48, notAttachment);
        }
        else
        {
            previewUrl = RaiseOnGetTilesThumbsItemUrl(data, true, 0, 0, 48, notAttachment);
        }

        #endregion


        #region "Standard controls and actions"

        bool hideDocName = false;

        Label lblDocumentName = null;
        if (isContent && !notAttachment)
        {
            string docName = Path.GetFileNameWithoutExtension(data.GetValue(FileNameColumn).ToString());
            fileName = Path.GetFileNameWithoutExtension(fileName);

            hideDocName = (docName.ToLower() == fileName.ToLower());
            if (!hideDocName)
            {
                fileName += "." + ext;

                lblDocumentName = e.Item.FindControl("lblDocumentName") as Label;
                if (lblDocumentName != null)
                {
                    lblDocumentName.Attributes["class"] = "DialogTileTitleBold";
                    lblDocumentName.Text = HTMLHelper.HTMLEncode(docName);
                }
            }
            else
            {
                fileName = docName;
            }
        }

        // Do not display document name if the same as the file name
        if (hideDocName || (lblDocumentName == null) || string.IsNullOrEmpty(lblDocumentName.Text))
        {
            PlaceHolder plcDocumentName = e.Item.FindControl("plcDocumentName") as PlaceHolder;
            if (plcDocumentName != null)
            {
                plcDocumentName.Visible = false;
            }
        }

        // Load file name
        Label lblFileName = e.Item.FindControl("lblFileName") as Label;
        if (lblFileName != null)
        {
            if (SourceType == MediaSourceEnum.DocumentAttachments)
            {
                fileName = Path.GetFileNameWithoutExtension(fileName);
            }
            lblFileName.Text = fileName;
        }

        // Load file type
        Label lblType = e.Item.FindControl("lblTypeValue") as Label;
        if (lblType != null)
        {
            lblType.Text = notAttachment ? ResHelper.LocalizeString(ext) : NormalizeExtenison(ext);
        }

        if (!notAttachment && !libraryFolder)
        {
            // Load file size
            Label lblSize = e.Item.FindControl("lblSizeValue") as Label;
            if (lblSize != null)
            {
                long size = 0;
                if (data.ContainsColumn(FileSizeColumn))
                {
                    size = ValidationHelper.GetLong(data.GetValue(FileSizeColumn), 0);
                }
                // Library files
                else if (data.ContainsColumn("Size"))
                {
                    size = ValidationHelper.GetLong((importedMediaData != null) ? importedMediaData["FileSize"] : data.GetValue("Size"), 0);
                }
                lblSize.Text = DataHelper.GetSizeString(size);
            }
        }

        // Initialize SELECT button
        ImageButton btnSelect = e.Item.FindControl("btnSelect") as ImageButton;
        if (btnSelect != null)
        {
            // Check if item is selectable, if not remove select action button
            if (!isSelectable)
            {
                btnSelect.ImageUrl = ResolveUrl(ImagesPath + "transparent.png");
                btnSelect.ToolTip = "";
                btnSelect.Attributes.Remove("onclick");
                btnSelect.Attributes["style"] = "cursor:default;";
                btnSelect.Enabled = false;
            }
            else
            {
                // If media file not imported yet - display warning sign
                if ((SourceType == MediaSourceEnum.MediaLibraries) && ((DisplayMode == ControlDisplayModeEnum.Simple) && !isInDatabase && !libraryFolder))
                {
                    btnSelect.ImageUrl = ResolveUrl(ImagesPath + "warning.png");
                    btnSelect.ToolTip = GetString("media.file.import");
                    btnSelect.Attributes["onclick"] = String.Format("ColorizeRow({0}); SetAction('importfile',{1}); RaiseHiddenPostBack(); return false;", ScriptHelper.GetString(GetColorizeID(data)), ScriptHelper.GetString(fullFileName));
                }
                else
                {
                    // Initialize command
                    if (libraryFolder)
                    {
                        btnSelect.ImageUrl = ResolveUrl(ImagesPath + "subdocument.png");
                        btnSelect.ToolTip = GetString("dialogs.list.actions.showsubfolders");
                        btnSelect.Attributes["onclick"] = String.Format("SetAction('morefolderselect', {0}); RaiseHiddenPostBack(); return false;", ScriptHelper.GetString(fileName));
                    }
                    else
                    {
                        btnSelect.ImageUrl = ResolveUrl(ImagesPath + "next.png");
                        btnSelect.ToolTip = GetString("dialogs.list.actions.select");
                        btnSelect.Attributes["onclick"] = String.Format("ColorizeRow({0}); SetSelectAction({1}); return false;", ScriptHelper.GetString(GetColorizeID(data)), ScriptHelper.GetString(String.Format("{0}|URL|{1}", argument, selectUrl)));
                    }
                }
            }
        }

        // Initialize SELECTSUBDOCS button
        ImageButton btnSelectSubDocs = e.Item.FindControl("btnSelectSubDocs") as ImageButton;
        if (btnSelectSubDocs != null)
        {
            btnSelectSubDocs.ToolTip = GetString("dialogs.list.actions.showsubdocuments");

            if (IsFullListingMode && (SourceType == MediaSourceEnum.Content))
            {
                int nodeId = ValidationHelper.GetInteger(data.GetValue("NodeID"), 0);

                // Check if item is selectable, if not remove select action button
                // Initialize command
                btnSelectSubDocs.Attributes["onclick"] = String.Format("SetParentAction('{0}'); return false;", nodeId);
                btnSelectSubDocs.ImageUrl = ResolveUrl(ImagesPath + "subdocument.png");
            }
            else
            {
                PlaceHolder plcSelectSubDocs = e.Item.FindControl("plcSelectSubDocs") as PlaceHolder;
                if (plcSelectSubDocs != null)
                {
                    plcSelectSubDocs.Visible = false;
                }
            }
        }

        // Initialize VIEW button
        // Get media file URL according system settings
        argument = RaiseOnGetArgumentSet(data);

        ImageButton btnView = e.Item.FindControl("btnView") as ImageButton;
        if (btnView != null)
        {
            if (!notAttachment && !libraryFolder && !libraryUiFolder)
            {
                if (String.IsNullOrEmpty(selectUrl))
                {
                    btnView.ImageUrl = ResolveUrl(ImagesPath + "viewdisabled.png");
                    btnView.OnClientClick = "return false;";
                    btnView.Attributes["style"] = "cursor:default;";
                    btnView.Enabled = false;
                }
                else
                {
                    btnView.ImageUrl = ResolveUrl(ImagesPath + "view.png");
                    btnView.ToolTip = GetString("dialogs.list.actions.view");
                    btnView.OnClientClick = String.Format("javascript: window.open({0}); return false;", ScriptHelper.GetString(URLHelper.ResolveUrl(selectUrl)));
                }
            }
            else
            {
                btnView.Visible = false;
            }
        }

        // Initialize EDIT button
        ImageButton btnContentEdit = e.Item.FindControl("btnContentEdit") as ImageButton;
        if (btnContentEdit != null)
        {
            Guid guid = Guid.Empty;
            btnContentEdit.ToolTip = GetString("general.edit");

            if ((SourceType == MediaSourceEnum.MediaLibraries) && !libraryFolder && !libraryUiFolder)
            {
                // Media files coming from FS
                if (!data.ContainsColumn("FileGUID"))
                {
                    // Get file name
                    fileName = fullFileName;
                    ext = data.ContainsColumn("FileExtension") ? data.GetValue("FileExtension").ToString() : data.GetValue("Extension").ToString();

                    if (!((DisplayMode == ControlDisplayModeEnum.Simple) && isInDatabase))
                    {
                        btnContentEdit.ImageUrl = ResolveUrl(ImagesPath + "editdisabled.png");
                        btnContentEdit.Attributes["style"] = "cursor: default;";
                        btnContentEdit.Enabled = false;
                    }
                    else
                    {
                        btnContentEdit.ImageUrl = ResolveUrl(ImagesPath + "edit.png");
                        btnContentEdit.OnClientClick = String.Format("$j('#hdnFileOrigName').attr('value', {0}); SetAction('editlibraryui', {1}); RaiseHiddenPostBack(); return false;", ScriptHelper.GetString(EnsureFileName(fileName)), ScriptHelper.GetString(fileName));
                    }
                }
                else
                {
                    guid = ValidationHelper.GetGuid(data.GetValue("FileGUID"), Guid.Empty);
                    btnContentEdit.ImageUrl = ResolveUrl(ImagesPath + "edit.png");
                    btnContentEdit.AlternateText = String.Format("{0}|MediaFileGUID={1}&sitename={2}", ext, guid, GetSiteName(data, true));
                    btnContentEdit.PreRender += img_PreRender;
                }
            }
            else if (!notAttachment && (SourceType != MediaSourceEnum.DocumentAttachments) && !libraryFolder && !libraryUiFolder)
            {
                string nodeid = "";
                if (SourceType == MediaSourceEnum.Content)
                {
                    nodeid = "&nodeId=" + data.GetValue("NodeID");

                    // Get the node workflow
                    VersionHistoryID = ValidationHelper.GetInteger(data.GetValue("DocumentCheckedOutVersionHistoryID"), 0);
                }

                guid = ValidationHelper.GetGuid(data.GetValue("AttachmentGUID"), Guid.Empty);
                btnContentEdit.ImageUrl = ResolveUrl(ImagesPath + "edit.png");
                btnContentEdit.AlternateText = String.Format("{0}|AttachmentGUID={1}&sitename={2}{3}{4}", ext, guid, GetSiteName(data, false), nodeid, ((VersionHistoryID > 0) ? "&versionHistoryId=" + VersionHistoryID : ""));
                btnContentEdit.PreRender += img_PreRender;
            }
            else
            {
                btnContentEdit.Visible = false;
            }
        }

        #endregion


        #region "Special actions"

        // If attachments being displayed show additional actions
        if (SourceType == MediaSourceEnum.DocumentAttachments)
        {
            // Initialize UPDATE button
            DirectFileUploader dfuElem = e.Item.FindControl("dfuElem") as DirectFileUploader;
            if (dfuElem != null)
            {
                GetAttachmentUpdateControl(ref dfuElem, data);
            }

            string attExtension = data.GetValue("AttachmentExtension").ToString();
            // If the WebDAV is enabled and windows authentication and extension is allowed
            if (CMSContext.IsWebDAVEnabled(CMSContext.CurrentSiteName) && RequestHelper.IsWindowsAuthentication() && WebDAVSettings.IsExtensionAllowedForEditMode(attExtension, CMSContext.CurrentSiteName))
            {
                PlaceHolder plcWebDAV = e.Item.FindControl("plcWebDAV") as PlaceHolder;
                if (plcWebDAV != null)
                {
                    // Dynamically load control
                    WebDAVEditControl webDAVElem = Page.LoadControl("~/CMSModules/WebDAV/Controls/AttachmentWebDAVEditControl.ascx") as WebDAVEditControl;

                    if (webDAVElem != null)
                    {
                        plcWebDAV.Controls.Clear();
                        plcWebDAV.Controls.Add(webDAVElem);
                        webDAVElem.Visible = false;

                        // Set WebDAV edit control
                        GetWebDAVEditControl(ref webDAVElem, data);
                        webDAVElem.CssClass = null;
                        plcWebDAV.Visible = true;
                    }
                }
            }

            // Initialize EDIT button
            ImageButton btnEdit = e.Item.FindControl("btnEdit") as ImageButton;
            if (btnEdit != null)
            {
                if (!notAttachment)
                {
                    btnEdit.ToolTip = GetString("general.edit");

                    // Get file extension
                    string extension = ValidationHelper.GetString(data.GetValue("AttachmentExtension"), "").ToLower();
                    Guid guid = ValidationHelper.GetGuid(data.GetValue("AttachmentGUID"), Guid.Empty);

                    btnEdit.ImageUrl = ResolveUrl(ImagesPath + "edit.png");
                    btnEdit.AlternateText = String.Format("{0}|AttachmentGUID={1}&sitename={2}&versionHistoryId={3}", extension, guid, GetSiteName(data, false), VersionHistoryID);
                    btnEdit.PreRender += img_PreRender;
                }
            }

            // Initialize DELETE button
            ImageButton btnDelete = e.Item.FindControl("btnDelete") as ImageButton;
            if (btnDelete != null)
            {
                btnDelete.ImageUrl = ResolveUrl(ImagesPath + "delete.png");
                btnDelete.ToolTip = GetString("general.delete");

                // Initialize command
                btnDelete.Attributes["onclick"] = String.Format("if(DeleteConfirmation() == false){{return false;}} SetAction('attachmentdelete','{0}'); RaiseHiddenPostBack(); return false;", data.GetValue("AttachmentGUID"));
            }

            PlaceHolder plcContentEdit = e.Item.FindControl("plcContentEdit") as PlaceHolder;
            if (plcContentEdit != null)
            {
                plcContentEdit.Visible = false;
            }
        }
        else if ((SourceType == MediaSourceEnum.MediaLibraries) && !data.ContainsColumn("FileGUID") && !((DisplayMode == ControlDisplayModeEnum.Simple) && (libraryFolder || libraryUiFolder)))
        {
            // Initialize DELETE button
            ImageButton btnDelete = e.Item.FindControl("btnDelete") as ImageButton;
            if (btnDelete != null)
            {
                btnDelete.ImageUrl = ResolveUrl(ImagesPath + "Delete.png");
                btnDelete.ToolTip = GetString("general.delete");
                btnDelete.Attributes["onclick"] = String.Format("if(DeleteMediaFileConfirmation() == false){{return false;}} SetAction('deletefile',{0}); RaiseHiddenPostBack(); return false;", ScriptHelper.GetString(fullFileName));
            }

            // Hide attachment specific actions
            PlaceHolder plcAttachmentUpdtAction = e.Item.FindControl("plcAttachmentUpdtAction") as PlaceHolder;
            if (plcAttachmentUpdtAction != null)
            {
                plcAttachmentUpdtAction.Visible = false;
            }
        }
        else
        {
            PlaceHolder plcAttachmentActions = e.Item.FindControl("plcAttachmentActions") as PlaceHolder;
            if (plcAttachmentActions != null)
            {
                plcAttachmentActions.Visible = false;
            }
        }

        #endregion


        #region "Library action"

        if ((SourceType == MediaSourceEnum.MediaLibraries) && (DisplayMode == ControlDisplayModeEnum.Simple))
        {
            // Initialize UPDATE button
            DirectFileUploader dfuElemLib = e.Item.FindControl("dfuElemLib") as DirectFileUploader;
            if (dfuElemLib != null)
            {
                Panel pnlDisabledUpdate = (e.Item.FindControl("pnlDisabledUpdate") as Panel);
                if (pnlDisabledUpdate != null)
                {
                    bool hasModifyPermission = RaiseOnGetModifyPermission(data);

                    if (isInDatabase && hasModifyPermission)
                    {
                        GetLibraryUpdateControl(ref dfuElemLib, importedMediaData);

                        pnlDisabledUpdate.Visible = false;
                    }
                    else
                    {
                        pnlDisabledUpdate.Controls.Clear();

                        ImageButton imgBtn = new ImageButton()
                        {
                            Enabled = false,
                            ImageUrl = ResolveUrl(GetImageUrl("Design/Controls/DirectUploader/uploaddisabled.png"))
                        };
                        imgBtn.Attributes["style"] = "cursor: default;";

                        pnlDisabledUpdate.Controls.Add(imgBtn);

                        dfuElemLib.Visible = false;
                    }
                }
            }

            string siteName = GetSiteName(data, true);

            // If the WebDAV is enabled and windows authentication and extension is allowed and media file is in database
            if (CMSContext.IsWebDAVEnabled(siteName) && RequestHelper.IsWindowsAuthentication() && WebDAVSettings.IsExtensionAllowedForEditMode(ext, siteName) && isInDatabase)
            {
                PlaceHolder plcWebDAVMfi = e.Item.FindControl("plcWebDAVMfi") as PlaceHolder;
                if (plcWebDAVMfi != null)
                {
                    // Dynamically load control
                    WebDAVEditControl webDAVElem = Page.LoadControl("~/CMSModules/MediaLibrary/Controls/WebDAV/MediaFileWebDAVEditControl.ascx") as WebDAVEditControl;
                    if (webDAVElem != null)
                    {
                        webDAVElem.Visible = false;
                        plcWebDAVMfi.Controls.Clear();
                        plcWebDAVMfi.Controls.Add(webDAVElem);

                        // Set WebDAV edit control
                        GetWebDAVEditControl(ref webDAVElem, importedMediaData);
                    }
                }
            }
        }
        else if ((SourceType == MediaSourceEnum.Content && (DisplayMode == ControlDisplayModeEnum.Default) && !notAttachment && !libraryFolder && !libraryUiFolder))
        {
            // Initialize WebDAV edit button
            if (data.ContainsColumn("AttachmentGUID"))
            {
                VisibleWebDAVEditControl(e.Item, WebDAVControlTypeEnum.Attachment);
            }
        }
        else if ((SourceType == MediaSourceEnum.MediaLibraries && (DisplayMode == ControlDisplayModeEnum.Default) && !libraryFolder && !libraryUiFolder))
        {
            // Initialize WebDAV edit button
            if (data.ContainsColumn("FileGUID"))
            {
                VisibleWebDAVEditControl(e.Item, WebDAVControlTypeEnum.Media);
            }
        }
        else
        {
            PlaceHolder plcLibraryUpdtAction = e.Item.FindControl("plcLibraryUpdtAction") as PlaceHolder;
            if (plcLibraryUpdtAction != null)
            {
                plcLibraryUpdtAction.Visible = false;
            }
        }


        if ((SourceType == MediaSourceEnum.MediaLibraries) && libraryFolder && IsFullListingMode)
        {
            // Initialize SELECT SUB-FOLDERS button
            ImageButton btn = e.Item.FindControl("imgSelectSubFolders") as ImageButton;
            if (btn != null)
            {
                btn.Visible = true;
                btn.ImageUrl = ResolveUrl(ImagesPath + "subdocument.png");
                btn.ToolTip = GetString("dialogs.list.actions.showsubfolders");
                btn.Attributes["onclick"] = String.Format("SetLibParentAction({0}); return false;", ScriptHelper.GetString(fileName));
            }
        }
        else
        {
            PlaceHolder plcSelectSubFolders = e.Item.FindControl("plcSelectSubFolders") as PlaceHolder;
            if (plcSelectSubFolders != null)
            {
                plcSelectSubFolders.Visible = false;
            }
        }

        #endregion


        #region "Load file image"

        // Selectable area
        Panel pnlItemInageContainer = e.Item.FindControl("pnlTiles") as Panel;
        if (pnlItemInageContainer != null)
        {
            if (isSelectable)
            {
                if ((DisplayMode == ControlDisplayModeEnum.Simple) && !isInDatabase)
                {
                    if (libraryFolder || libraryUiFolder)
                    {
                        pnlItemInageContainer.Attributes["onclick"] = String.Format("SetAction('morefolderselect', {0}); RaiseHiddenPostBack(); return false;", ScriptHelper.GetString(fileName));
                    }
                    else
                    {
                        pnlItemInageContainer.Attributes["onclick"] = String.Format("ColorizeRow({0}); SetAction('importfile', {1}); RaiseHiddenPostBack(); return false;", ScriptHelper.GetString(GetColorizeID(data)), ScriptHelper.GetString(fullFileName));
                    }
                }
                else
                {
                    pnlItemInageContainer.Attributes["onclick"] = String.Format("ColorizeRow({0}); SetSelectAction({1}); return false;", ScriptHelper.GetString(GetColorizeID(data)), ScriptHelper.GetString(String.Format("{0}|URL|{1}", argument, selectUrl)));
                }

                pnlItemInageContainer.Attributes["style"] = "cursor:pointer;";
            }
            else
            {
                pnlItemInageContainer.Attributes["style"] = "cursor:default;";
            }
        }

        // Image area
        Image fileImage = e.Item.FindControl("imgElem") as Image;
        if (fileImage != null)
        {
            string chset = Guid.NewGuid().ToString();
            previewUrl = URLHelper.AddParameterToUrl(previewUrl, "chset", chset);

            // Add latest version requirement for live site
            int versionHistoryId = VersionHistoryID;
            if (IsLiveSite && (versionHistoryId > 0))
            {
                // Add requirement for latest version of files for current document
                string newparams = "latestforhistoryid=" + versionHistoryId;
                newparams += "&hash=" + ValidationHelper.GetHashString("h" + versionHistoryId);

                previewUrl += "&" + newparams;
            }

            fileImage.ImageUrl = ResolveUrl(previewUrl);

            // Ensure tooltip
            if (isInDatabase)
            {
                UIHelper.EnsureTooltip(fileImage, previewUrl, width, height, GetTitle(data, isContentFile), fileName, ext, GetDescription(data, isContentFile), null, 300);
            }
        }

        #endregion


        // Display only for ML UI
        if ((DisplayMode == ControlDisplayModeEnum.Simple) && !libraryFolder)
        {
            PlaceHolder plcSelectionBox = e.Item.FindControl("plcSelectionBox") as PlaceHolder;
            if (plcSelectionBox != null)
            {
                plcSelectionBox.Visible = true;

                // Multiple selection check-box
                LocalizedCheckBox chkSelected = e.Item.FindControl("chkSelected") as LocalizedCheckBox;
                if (chkSelected != null)
                {
                    chkSelected.ToolTip = GetString("general.select");
                    chkSelected.InputAttributes["alt"] = fullFileName;

                    HiddenField hdnItemName = e.Item.FindControl("hdnItemName") as HiddenField;
                    if (hdnItemName != null)
                    {
                        hdnItemName.Value = fullFileName;
                    }
                }
            }
        }
    }


    /// <summary>
    /// Sets visibility of WebDAV edit control.
    /// </summary>
    /// <param name="repeaterItem">Repeater item</param>
    /// <param name="controlType">Control type</param>
    private void VisibleWebDAVEditControl(RepeaterItem repeaterItem, WebDAVControlTypeEnum controlType)
    {
        PlaceHolder plcAttachmentActions = repeaterItem.FindControl("plcAttachmentActions") as PlaceHolder;
        PlaceHolder plcAttachmentUpdtAction = repeaterItem.FindControl("plcAttachmentUpdtAction") as PlaceHolder;
        PlaceHolder plcLibraryUpdtAction = repeaterItem.FindControl("plcLibraryUpdtAction") as PlaceHolder;
        PlaceHolder plcWebDAV = repeaterItem.FindControl("plcWebDAV") as PlaceHolder;
        PlaceHolder plcWebDAVMfi = repeaterItem.FindControl("plcWebDAVMfi") as PlaceHolder;
        Panel pnlDisabledUpdate = (repeaterItem.FindControl("pnlDisabledUpdate") as Panel);
        DirectFileUploader dfuLib = repeaterItem.FindControl("dfuElemLib") as DirectFileUploader;
        DirectFileUploader dfu = repeaterItem.FindControl("dfuElem") as DirectFileUploader;
        ImageButton btnEdit = repeaterItem.FindControl("btnEdit") as ImageButton;
        ImageButton btnDelete = repeaterItem.FindControl("btnDelete") as ImageButton;

        if ((plcAttachmentActions != null) && (plcLibraryUpdtAction != null) && (plcAttachmentUpdtAction != null) && (plcWebDAV != null)
            && (plcWebDAVMfi != null) && (pnlDisabledUpdate != null) && (dfuLib != null) && (dfu != null) && (btnEdit != null) && (btnDelete != null))
        {
            WebDAVEditControl webDAVElem = null;

            if (controlType == WebDAVControlTypeEnum.Media)
            {
                plcAttachmentActions.Visible = true;
                plcAttachmentUpdtAction.Visible = false;
                plcLibraryUpdtAction.Visible = true;
                pnlDisabledUpdate.Visible = false;
                dfuLib.Visible = false;
                dfu.Visible = false;
                btnEdit.Visible = false;
                btnDelete.Visible = false;
                plcWebDAV.Visible = false;

                webDAVElem = Page.LoadControl("~/CMSModules/MediaLibrary/Controls/WebDAV/MediaFileWebDAVEditControl.ascx") as WebDAVEditControl;
                if (webDAVElem != null)
                {
                    plcWebDAVMfi.Controls.Clear();
                    plcWebDAVMfi.Controls.Add(webDAVElem);
                }
            }
            else if (controlType == WebDAVControlTypeEnum.Attachment)
            {
                plcAttachmentActions.Visible = true;
                plcAttachmentUpdtAction.Visible = false;
                plcLibraryUpdtAction.Visible = false;
                pnlDisabledUpdate.Visible = false;
                dfuLib.Visible = false;
                dfu.Visible = false;
                btnEdit.Visible = false;
                btnDelete.Visible = false;
                plcWebDAV.Visible = true;

                // Dynamically load control
                webDAVElem = Page.LoadControl("~/CMSModules/WebDAV/Controls/AttachmentWebDAVEditControl.ascx") as WebDAVEditControl;
                if (webDAVElem != null)
                {
                    plcWebDAV.Controls.Clear();
                    plcWebDAV.Controls.Add(webDAVElem);
                }
            }

            if (webDAVElem != null)
            {
                webDAVElem.Visible = false;
                webDAVElem.CssClass = null;

                IDataContainer data = new DataRowContainer((DataRowView)repeaterItem.DataItem);
                string extension = data.GetValue(FileExtensionColumn).ToString();

                // If the WebDAV is enabled and windows authentication and extension is allowed
                if (CMSContext.IsWebDAVEnabled(CMSContext.CurrentSiteName) && RequestHelper.IsWindowsAuthentication() && WebDAVSettings.IsExtensionAllowedForEditMode(extension, CMSContext.CurrentSiteName))
                {
                    // Set WebDAV edit control
                    GetWebDAVEditControl(ref webDAVElem, data);
                }
            }
        }
    }

    #endregion


    #region "Thumbnails view methods"

    /// <summary>
    /// Initializes controls for the thumbnails view mode.
    /// </summary>
    private void InitializeThumbnailsView()
    {
        bool isAttachmentTab = SourceType == MediaSourceEnum.DocumentAttachments;

        // Initialize page size
        pageSizeThumbs.Items = (isAttachmentTab ? new string[] { "12", "24", "48", "96" } : new string[] { "10", "20", "50", "100" });
        pagerElemThumbnails.PageSize = (pageSizeThumbs.SelectedValue == "-1") ? 0 : ValidationHelper.GetInteger(pageSizeThumbs.SelectedValue, (isAttachmentTab ? 12 : 10));

        // Basic control properties
        repThumbnailsView.HideControlForZeroRows = true;

        // UniPager properties        
        pagerElemThumbnails.GroupSize = PAGER_GROUP_SIZE;
        pagerElemThumbnails.DisplayFirstLastAutomatically = false;
        pagerElemThumbnails.DisplayPreviousNextAutomatically = false;
        pagerElemThumbnails.HidePagerForSinglePage = true;
        pagerElemThumbnails.PagerMode = UniPagerMode.PostBack;
    }


    /// <summary>
    /// Loads content for media libraries thumbnails view element.
    /// </summary>
    private void ReloadThumbnailsView()
    {
        // Connects repeater with data source
        if (!DataHelper.DataSourceIsEmpty(DataSource))
        {
            repThumbnailsView.DataSource = DataSource;
            repThumbnailsView.DataBind();
        }
    }


    protected void ThumbnailsViewControl_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        #region "Load the item data"

        IDataContainer data = new DataRowContainer((DataRowView)e.Item.DataItem);

        string fileNameColumn = FileNameColumn;
        string className = "";

        bool isContent = (SourceType == MediaSourceEnum.Content);

        bool isContentFile = isContent ? (data.GetValue("ClassName").ToString().ToLower() == "cms.file") : false;
        bool notAttachment = isContent && !(isContentFile && (data.GetValue("AttachmentGUID") != DBNull.Value));
        if (notAttachment)
        {
            className = DataClassInfoProvider.GetDataClass((int)data.GetValue("NodeClassID")).ClassDisplayName;

            fileNameColumn = "DocumentName";
        }

        // Get information on file
        string fileName = HTMLHelper.HTMLEncode(data.GetValue(fileNameColumn).ToString());
        string ext = HTMLHelper.HTMLEncode(notAttachment ? className : data.GetValue(FileExtensionColumn).ToString().TrimStart('.'));
        string argument = RaiseOnGetArgumentSet(data);

        // Get full media library file name
        bool isInDatabase = true;
        string fullFileName = GetFileName(data);

        IDataContainer importedMediaData = null;
        if (SourceType == MediaSourceEnum.MediaLibraries)
        {
            importedMediaData = RaiseOnFileIsNotInDatabase(fullFileName);
            isInDatabase = (importedMediaData != null);
        }

        bool libraryFolder = ((SourceType == MediaSourceEnum.MediaLibraries) && IsFullListingMode && (data.GetValue(FileExtensionColumn).ToString().ToLower() == "<dir>"));
        bool libraryUiFolder = libraryFolder && !((DisplayMode == ControlDisplayModeEnum.Simple) && isInDatabase);

        int width = 0;
        int height = 0;
        // Get thumb preview image dimensions
        int[] thumbImgDimension = new int[] { 0, 0 };
        if (ImageHelper.IsSupportedByImageEditor(ext))
        {
            // Width & height
            if (data.ContainsColumn(FileWidthColumn))
            {
                width = ValidationHelper.GetInteger(data.GetValue(FileWidthColumn), 0);
            }
            else if (isInDatabase && importedMediaData.ContainsColumn(FileWidthColumn))
            {
                width = ValidationHelper.GetInteger(importedMediaData.GetValue(FileWidthColumn), 0);
            }

            if (data.ContainsColumn(FileHeightColumn))
            {
                height = ValidationHelper.GetInteger(data.GetValue(FileHeightColumn), 0);
            }
            else if (isInDatabase && importedMediaData.ContainsColumn(FileHeightColumn))
            {
                height = ValidationHelper.GetInteger(importedMediaData.GetValue(FileHeightColumn), 0);
            }

            thumbImgDimension = CMSDialogHelper.GetThumbImageDimensions(height, width, MaxThumbImgHeight, MaxThumbImgWidth);
        }

        // Preview URL
        string previewUrl = "";
        if ((SourceType == MediaSourceEnum.MediaLibraries) && isInDatabase)
        {
            previewUrl = RaiseOnGetTilesThumbsItemUrl(importedMediaData, true, thumbImgDimension[0], thumbImgDimension[1], 0, notAttachment);
        }
        else
        {
            previewUrl = RaiseOnGetTilesThumbsItemUrl(data, true, thumbImgDimension[0], thumbImgDimension[1], 0, notAttachment);
        }

        // Item URL
        string selectUrl = RaiseOnGetTilesThumbsItemUrl(data, false, 0, 0, 0, notAttachment);
        bool isSelectable = CMSDialogHelper.IsItemSelectable(SelectableContent, ext, isContentFile);

        #endregion


        #region "Standard controls and actions"

        // Load file name
        Label lblName = e.Item.FindControl("lblFileName") as Label;
        if (lblName != null)
        {
            lblName.Text = fileName;
        }

        // Initialize SELECT button
        ImageButton btnSelect = e.Item.FindControl("btnSelect") as ImageButton;
        if (btnSelect != null)
        {
            // Check if item is selectable, if not remove select action button
            if (!isSelectable)
            {
                btnSelect.ImageUrl = ResolveUrl(ImagesPath + "transparent.png");
                btnSelect.ToolTip = "";
                btnSelect.Attributes.Remove("onclick");
                btnSelect.Attributes["style"] = "cursor: default;";
                btnSelect.Enabled = false;
            }
            else
            {
                // If media file not imported yet - display warning sign
                if ((SourceType == MediaSourceEnum.MediaLibraries) && ((DisplayMode == ControlDisplayModeEnum.Simple) && !isInDatabase && !libraryFolder && !libraryUiFolder))
                {
                    btnSelect.ImageUrl = ResolveUrl(ImagesPath + "warning.png");
                    btnSelect.ToolTip = GetString("media.file.import");
                    btnSelect.Attributes["onclick"] = String.Format("ColorizeRow({0}); SetAction('importfile',{1}); RaiseHiddenPostBack(); return false;", ScriptHelper.GetString(GetColorizeID(data)), ScriptHelper.GetString(fullFileName));
                }
                else
                {
                    if (libraryFolder || libraryUiFolder)
                    {
                        btnSelect.ImageUrl = ResolveUrl(ImagesPath + "subdocument.png");
                        btnSelect.ToolTip = GetString("dialogs.list.actions.showsubfolders");
                        btnSelect.Attributes["onclick"] = String.Format("SetAction('morefolderselect', {0}); RaiseHiddenPostBack(); return false;", ScriptHelper.GetString(fileName));
                    }
                    else
                    {
                        btnSelect.ImageUrl = ResolveUrl(ImagesPath + "next.png");
                        btnSelect.ToolTip = GetString("dialogs.list.actions.select");
                        btnSelect.Attributes["onclick"] = String.Format("ColorizeRow({0}); SetSelectAction({1}); return false;", ScriptHelper.GetString(GetColorizeID(data)), ScriptHelper.GetString(argument + "|URL|" + selectUrl));
                    }
                }
            }
        }

        // Initialize SELECTSUBDOCS button
        ImageButton btnSelectSubDocs = e.Item.FindControl("btnSelectSubDocs") as ImageButton;
        if (btnSelectSubDocs != null)
        {
            if (IsFullListingMode && (SourceType == MediaSourceEnum.Content))
            {
                int nodeId = ValidationHelper.GetInteger(data.GetValue("NodeID"), 0);

                btnSelectSubDocs.ToolTip = GetString("dialogs.list.actions.showsubdocuments");

                // Check if item is selectable, if not remove select action button
                // Initialize command
                btnSelectSubDocs.Attributes["onclick"] = String.Format("SetParentAction('{0}'); return false;", nodeId);
                btnSelectSubDocs.ImageUrl = ResolveUrl(ImagesPath + "subdocument.png");
            }
            else
            {
                PlaceHolder plcSelectSubDocs = e.Item.FindControl("plcSelectSubDocs") as PlaceHolder;
                if (plcSelectSubDocs != null)
                {
                    plcSelectSubDocs.Visible = false;
                }
            }
        }

        // Initialize VIEW button
        ImageButton btnView = e.Item.FindControl("btnView") as ImageButton;
        if (btnView != null)
        {
            if (!notAttachment && !libraryFolder)
            {
                if (String.IsNullOrEmpty(selectUrl))
                {
                    btnView.ImageUrl = ResolveUrl(ImagesPath + "viewdisabled.png");
                    btnView.OnClientClick = "return false;";
                    btnView.Attributes["style"] = "cursor:default;";
                    btnView.Enabled = false;
                }
                else
                {
                    btnView.ImageUrl = ResolveUrl(ImagesPath + "view.png");
                    btnView.ToolTip = GetString("dialogs.list.actions.view");
                    btnView.OnClientClick = String.Format("javascript: window.open({0}); return false;", ScriptHelper.GetString(URLHelper.ResolveUrl(selectUrl)));
                }
            }
            else
            {
                btnView.Visible = false;
            }
        }

        // Initialize EDIT button
        ImageButton btnContentEdit = e.Item.FindControl("btnContentEdit") as ImageButton;
        if (btnContentEdit != null)
        {
            btnContentEdit.ToolTip = GetString("general.edit");

            Guid guid = Guid.Empty;

            if (SourceType == MediaSourceEnum.MediaLibraries && !libraryFolder && !libraryUiFolder)
            {
                // Media files coming from FS
                if (!data.ContainsColumn("FileGUID"))
                {
                    if ((DisplayMode == ControlDisplayModeEnum.Simple) && !isInDatabase)
                    {
                        btnContentEdit.ImageUrl = ResolveUrl(ImagesPath + "editdisabled.png");
                        btnContentEdit.Attributes["style"] = "cursor: default;";
                        btnContentEdit.Enabled = false;
                    }
                    else
                    {
                        btnContentEdit.ImageUrl = ResolveUrl(ImagesPath + "edit.png");
                        btnContentEdit.OnClientClick = String.Format("$j('#hdnFileOrigName').attr('value', {0}); SetAction('editlibraryui', {1}); RaiseHiddenPostBack(); return false;", ScriptHelper.GetString(EnsureFileName(fileName)), ScriptHelper.GetString(fileName));
                    }
                }
                else
                {
                    guid = ValidationHelper.GetGuid(data.GetValue("FileGUID"), Guid.Empty);
                    btnContentEdit.ImageUrl = ResolveUrl(ImagesPath + "edit.png");
                    btnContentEdit.AlternateText = String.Format("{0}|MediaFileGUID={1}&sitename={2}", ext, guid, GetSiteName(data, true));
                    btnContentEdit.PreRender += img_PreRender;
                }
            }
            else if (!notAttachment && !libraryFolder && !libraryUiFolder)
            {
                string nodeid = "";
                if (SourceType == MediaSourceEnum.Content)
                {
                    nodeid = "&nodeId=" + data.GetValue("NodeID");

                    // Get the node workflow
                    VersionHistoryID = ValidationHelper.GetInteger(data.GetValue("DocumentCheckedOutVersionHistoryID"), 0);
                }

                guid = ValidationHelper.GetGuid(data.GetValue("AttachmentGUID"), Guid.Empty);
                btnContentEdit.ImageUrl = ResolveUrl(ImagesPath + "edit.png");
                btnContentEdit.AlternateText = String.Format("{0}|AttachmentGUID={1}&sitename={2}{3}{4}", ext, guid, GetSiteName(data, false), nodeid, ((VersionHistoryID > 0) ? "&versionHistoryId=" + VersionHistoryID : ""));
                btnContentEdit.PreRender += img_PreRender;
            }
            else
            {
                btnContentEdit.Visible = false;
            }
        }

        #endregion


        #region "Special actions"

        // If attachments being displayed show additional actions
        if (SourceType == MediaSourceEnum.DocumentAttachments)
        {
            // Initialize EDIT button
            ImageButton btnEdit = e.Item.FindControl("btnEdit") as ImageButton;
            if (btnEdit != null)
            {
                if (!notAttachment)
                {
                    btnEdit.ToolTip = GetString("general.edit");

                    // Get file extension
                    string extension = ValidationHelper.GetString(data.GetValue("AttachmentExtension"), "").ToLower();
                    Guid guid = ValidationHelper.GetGuid(data.GetValue("AttachmentGUID"), Guid.Empty);

                    btnEdit.ImageUrl = ResolveUrl(ImagesPath + "edit.png");
                    btnEdit.AlternateText = String.Format("{0}|AttachmentGUID={1}&sitename={2}&versionHistoryId={3}", extension, guid, GetSiteName(data, false), VersionHistoryID);
                    btnEdit.PreRender += img_PreRender;
                }
            }

            // Initialize UPDATE button
            DirectFileUploader dfuElem = e.Item.FindControl("dfuElem") as DirectFileUploader;
            if (dfuElem != null)
            {
                GetAttachmentUpdateControl(ref dfuElem, data);
            }

            string attExtension = data.GetValue("AttachmentExtension").ToString();
            // If the WebDAV is enabled and windows authentication and extension is allowed
            if (CMSContext.IsWebDAVEnabled(CMSContext.CurrentSiteName) && RequestHelper.IsWindowsAuthentication() && WebDAVSettings.IsExtensionAllowedForEditMode(attExtension, CMSContext.CurrentSiteName))
            {
                PlaceHolder plcWebDAV = e.Item.FindControl("plcWebDAV") as PlaceHolder;
                if (plcWebDAV != null)
                {
                    // Dynamically load control
                    WebDAVEditControl webDAVElem = Page.LoadControl("~/CMSModules/WebDAV/Controls/AttachmentWebDAVEditControl.ascx") as WebDAVEditControl;

                    if (webDAVElem != null)
                    {
                        plcWebDAV.Controls.Clear();
                        plcWebDAV.Controls.Add(webDAVElem);
                        webDAVElem.Visible = false;

                        // Set WebDAV edit control
                        GetWebDAVEditControl(ref webDAVElem, data);
                        webDAVElem.CssClass = null;
                        plcWebDAV.Visible = true;
                    }
                }
            }

            // Initialize DELETE button
            ImageButton btnDelete = e.Item.FindControl("btnDelete") as ImageButton;
            if (btnDelete != null)
            {
                btnDelete.ImageUrl = ResolveUrl(ImagesPath + "delete.png");
                btnDelete.ToolTip = GetString("general.delete");

                // Initialize command
                btnDelete.Attributes["onclick"] = String.Format("if(DeleteConfirmation() == false){{return false;}} SetAction('attachmentdelete','{0}'); RaiseHiddenPostBack(); return false;", data.GetValue("AttachmentGUID"));
            }

            PlaceHolder plcContentEdit = e.Item.FindControl("plcContentEdit") as PlaceHolder;
            if (plcContentEdit != null)
            {
                plcContentEdit.Visible = false;
            }
        }
        else if ((SourceType == MediaSourceEnum.MediaLibraries) && !data.ContainsColumn("FileGUID") && ((DisplayMode == ControlDisplayModeEnum.Simple) && !libraryFolder && !libraryUiFolder))
        {
            // Initialize DELETE button
            ImageButton btnDelete = e.Item.FindControl("btnDelete") as ImageButton;
            if (btnDelete != null)
            {
                btnDelete.ImageUrl = ResolveUrl(ImagesPath + "Delete.png");
                btnDelete.ToolTip = GetString("general.delete");
                btnDelete.Attributes["onclick"] = String.Format("if(DeleteMediaFileConfirmation() == false){{return false;}} SetAction('deletefile',{0}); RaiseHiddenPostBack(); return false;", ScriptHelper.GetString(fullFileName));
            }

            // Hide attachment specific actions
            PlaceHolder plcAttachmentUpdtAction = e.Item.FindControl("plcAttachmentUpdtAction") as PlaceHolder;
            if (plcAttachmentUpdtAction != null)
            {
                plcAttachmentUpdtAction.Visible = false;
            }
        }
        else
        {
            PlaceHolder plcAttachmentActions = e.Item.FindControl("plcAttachmentActions") as PlaceHolder;
            if (plcAttachmentActions != null)
            {
                plcAttachmentActions.Visible = false;
            }
        }

        #endregion


        #region "Library update action"

        if ((SourceType == MediaSourceEnum.MediaLibraries) && (DisplayMode == ControlDisplayModeEnum.Simple))
        {
            // Initialize UPDATE button
            DirectFileUploader dfuElemLib = e.Item.FindControl("dfuElemLib") as DirectFileUploader;
            if (dfuElemLib != null)
            {
                Panel pnlDisabledUpdate = (e.Item.FindControl("pnlDisabledUpdate") as Panel);
                if (pnlDisabledUpdate != null)
                {
                    bool hasModifyPermission = RaiseOnGetModifyPermission(data);
                    if (isInDatabase && hasModifyPermission)
                    {
                        GetLibraryUpdateControl(ref dfuElemLib, importedMediaData);

                        pnlDisabledUpdate.Visible = false;
                    }
                    else
                    {
                        pnlDisabledUpdate.Controls.Clear();

                        ImageButton imgBtn = new ImageButton()
                        {
                            Enabled = false,
                            ImageUrl = ResolveUrl(GetImageUrl("Design/Controls/DirectUploader/uploaddisabled.png"))
                        };
                        imgBtn.Attributes["style"] = "cursor: default;";

                        pnlDisabledUpdate.Controls.Add(imgBtn);

                        dfuElemLib.Visible = false;
                    }
                }
            }

            string siteName = GetSiteName(data, true);

            // If the WebDAV is enabled and windows authentication and extension is allowed and media file is in database
            if (CMSContext.IsWebDAVEnabled(siteName) && RequestHelper.IsWindowsAuthentication() && WebDAVSettings.IsExtensionAllowedForEditMode(ext, siteName) && isInDatabase)
            {
                PlaceHolder plcWebDAVMfi = e.Item.FindControl("plcWebDAVMfi") as PlaceHolder;
                if (plcWebDAVMfi != null)
                {
                    // Dynamically load control
                    WebDAVEditControl webDAVElem = Page.LoadControl("~/CMSModules/MediaLibrary/Controls/WebDAV/MediaFileWebDAVEditControl.ascx") as WebDAVEditControl;
                    if (webDAVElem != null)
                    {
                        webDAVElem.Visible = false;
                        plcWebDAVMfi.Controls.Clear();
                        plcWebDAVMfi.Controls.Add(webDAVElem);
                        // Set WebDAV edit control
                        GetWebDAVEditControl(ref webDAVElem, importedMediaData);
                    }
                }
            }
        }
        else if ((SourceType == MediaSourceEnum.Content && (DisplayMode == ControlDisplayModeEnum.Default) && !notAttachment && !libraryFolder && !libraryUiFolder))
        {
            // Initialize WebDAV edit button
            if (data.ContainsColumn("AttachmentGUID"))
            {
                VisibleWebDAVEditControl(e.Item, WebDAVControlTypeEnum.Attachment);
            }
        }
        else if ((SourceType == MediaSourceEnum.MediaLibraries && (DisplayMode == ControlDisplayModeEnum.Default) && !libraryFolder && !libraryUiFolder))
        {
            // Initialize WebDAV edit button
            if (data.ContainsColumn("FileGUID"))
            {
                VisibleWebDAVEditControl(e.Item, WebDAVControlTypeEnum.Media);
            }
        }
        else
        {
            PlaceHolder plcLibraryUpdtAction = e.Item.FindControl("plcLibraryUpdtAction") as PlaceHolder;
            if (plcLibraryUpdtAction != null)
            {
                plcLibraryUpdtAction.Visible = false;
            }
        }

        if ((SourceType == MediaSourceEnum.MediaLibraries) && libraryFolder && IsFullListingMode)
        {
            // Initialize SELECT SUB-FOLDERS button
            ImageButton btn = e.Item.FindControl("imgSelectSubFolders") as ImageButton;
            if (btn != null)
            {
                btn.Visible = true;
                btn.ImageUrl = ResolveUrl(ImagesPath + "subdocument.png");
                btn.ToolTip = GetString("dialogs.list.actions.showsubfolders");
                btn.Attributes["onclick"] = String.Format("SetLibParentAction({0}); return false;", ScriptHelper.GetString(fileName));
            }
        }
        else
        {
            PlaceHolder plcSelectSubFolders = e.Item.FindControl("plcSelectSubFolders") as PlaceHolder;
            if (plcSelectSubFolders != null)
            {
                plcSelectSubFolders.Visible = false;
            }
        }

        #endregion


        #region "File image"

        // Selectable area
        Panel pnlItemInageContainer = e.Item.FindControl("pnlThumbnails") as Panel;
        if (pnlItemInageContainer != null)
        {
            if (isSelectable)
            {
                if ((DisplayMode == ControlDisplayModeEnum.Simple) && !isInDatabase)
                {
                    if (libraryFolder || libraryUiFolder)
                    {
                        pnlItemInageContainer.Attributes["onclick"] = String.Format("SetAction('morefolderselect', {0}); RaiseHiddenPostBack(); return false;", ScriptHelper.GetString(fileName));
                    }
                    else
                    {
                        pnlItemInageContainer.Attributes["onclick"] = String.Format("ColorizeRow({0}); SetAction('importfile', {1}); RaiseHiddenPostBack(); return false;", ScriptHelper.GetString(GetColorizeID(data)), ScriptHelper.GetString(fullFileName));
                    }
                }
                else
                {
                    pnlItemInageContainer.Attributes["onclick"] = String.Format("ColorizeRow({0}); SetSelectAction({1}); return false;", ScriptHelper.GetString(GetColorizeID(data)), ScriptHelper.GetString(String.Format("{0}|URL|{1}", argument, selectUrl)));
                }
                pnlItemInageContainer.Attributes["style"] = "cursor:pointer;";
            }
            else
            {
                pnlItemInageContainer.Attributes["style"] = "cursor:default;";
            }
        }

        // Image area
        Image imgFile = e.Item.FindControl("imgFile") as Image;
        if (imgFile != null)
        {
            string chset = Guid.NewGuid().ToString();
            previewUrl = URLHelper.AddParameterToUrl(previewUrl, "chset", chset);

            // Add latest version requirement for live site
            int versionHistoryId = VersionHistoryID;
            if (IsLiveSite && (versionHistoryId > 0))
            {
                // Add requirement for latest version of files for current document
                string newparams = String.Format("latestforhistoryid={0}&hash={1}", versionHistoryId, ValidationHelper.GetHashString("h" + versionHistoryId));

                previewUrl += "&" + newparams;
            }

            imgFile.ImageUrl = previewUrl;
            imgFile.AlternateText = TextHelper.LimitLength(fileName, 10);
            imgFile.Attributes["title"] = fileName.Replace("\"", "\\\"");

            // Ensure tooltip - only text description
            if (isInDatabase)
            {
                UIHelper.EnsureTooltip(imgFile, previewUrl, width, height, GetTitle(data, isContentFile), fileName, ext, GetDescription(data, isContentFile), null, 300);
            }
        }

        #endregion


        // Display only for ML UI
        if ((DisplayMode == ControlDisplayModeEnum.Simple) && !libraryFolder)
        {
            PlaceHolder plcSelectionBox = e.Item.FindControl("plcSelectionBox") as PlaceHolder;
            if (plcSelectionBox != null)
            {
                plcSelectionBox.Visible = true;

                // Multiple selection check-box
                LocalizedCheckBox chkSelected = e.Item.FindControl("chkSelected") as LocalizedCheckBox;
                if (chkSelected != null)
                {
                    chkSelected.ToolTip = GetString("general.select");
                    chkSelected.InputAttributes["alt"] = fullFileName;

                    HiddenField hdnItemName = e.Item.FindControl("hdnItemName") as HiddenField;
                    if (hdnItemName != null)
                    {
                        hdnItemName.Value = fullFileName;
                    }
                }
            }
        }
    }

    #endregion


    #region "Raise events methods"

    /// <summary>
    /// Fires specific action and returns result provided by the parent control.
    /// </summary>
    /// <param name="data">Data related to the action</param>
    private string RaiseOnGetArgumentSet(IDataContainer data)
    {
        if (GetArgumentSet != null)
        {
            return GetArgumentSet(data);
        }
        return "";
    }


    /// <summary>
    /// Fires specific action and returns result provided by the parent control.
    /// </summary>
    /// <param name="data">Data related to the action</param>
    /// <param name="isPreview">Indicates whether the URL is required for preview item</param>
    /// <param name="notAttachment">Indicates whether the URL is required for non-attachment item</param>
    private string RaiseOnGetListItemUrl(IDataContainer data, bool isPreview, bool notAttachment)
    {
        if (GetListItemUrl != null)
        {
            return GetListItemUrl(data, isPreview, notAttachment);
        }
        return "";
    }


    /// <summary>
    /// Fires specific action and returns result provided by the parent control.
    /// </summary>
    /// <param name="data">Data related to the action</param>
    /// <param name="isPreview">Indicates whether the image is required as part of preview</param>
    /// <param name="width">Maximum width of the preview image</param>
    /// <param name="maxSideSize">Maximum size of the preview image. If full-size required parameter gets zero value</param>
    /// <param name="notAttachment">Indicates whether the URL is required for non-attachment item</param>
    /// <param name="height">Maximum height of the preview image</param>
    private string RaiseOnGetTilesThumbsItemUrl(IDataContainer data, bool isPreview, int height, int width, int maxSideSize, bool notAttachment)
    {
        if (GetTilesThumbsItemUrl != null)
        {
            return GetTilesThumbsItemUrl(data, isPreview, height, width, maxSideSize, notAttachment);
        }
        return "";
    }


    /// <summary>
    /// Raises event when information on import status of specified file is required.
    /// </summary>
    /// <param name="fileName">Name of the file (including extension)</param>
    private IDataContainer RaiseOnFileIsNotInDatabase(string fileName)
    {
        if (GetInformation != null)
        {
            object result = GetInformation("fileisnotindatabase", fileName);
            if (result != null)
            {
                // Ensure the data container
                return (result is DataRow ? new DataRowContainer((DataRow)result) : (result is DataRowView ? new DataRowContainer((DataRowView)result) : (IDataContainer)result));
            }
            return null;
        }
        return null;
    }


    /// <summary>
    /// Raises event when ID of the current site is required.
    /// </summary>
    private int RaiseOnSiteIdRequired()
    {
        if (GetInformation != null)
        {
            return (int)GetInformation("siteidrequired", null);
        }

        return 0;
    }


    /// <summary>
    /// Raises event when modify permission is required.
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    private bool RaiseOnGetModifyPermission(IDataContainer data)
    {
        if (GetModifyPermission != null)
        {
            return GetModifyPermission(data);
        }
        return true;
    }

    #endregion
}