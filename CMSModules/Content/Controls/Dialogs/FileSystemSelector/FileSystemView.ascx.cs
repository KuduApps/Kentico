using System;
using System.Web.UI;
using System.Data;

using CMS.UIControls;
using CMS.ExtendedControls;
using CMS.GlobalHelper;

public partial class CMSModules_Content_Controls_Dialogs_FileSystemSelector_FileSystemView : CMSUserControl
{
    private const char ARG_SEPARATOR = '|';

    #region "Private variables"

    private FileSystemDialogConfiguration mConfig;
    private string mStartingPath = "";
    private string mSearchText = "";

    protected string mSaveText;

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
    /// Gets or sets ID of the parent of the curently selected node.
    /// </summary>
    public string NodeParentID
    {
        get
        {
            return ValidationHelper.GetString(hdnLastNodeParentID.Value, "");
        }
        set
        {
            hdnLastNodeParentID.Value = value;
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
    /// Gets current dialog configuration.
    /// </summary>
    public FileSystemDialogConfiguration Config
    {
        get
        {
            if (mConfig == null)
            {
                mConfig = new FileSystemDialogConfiguration();
            }
            return mConfig;
        }
        set
        {
            mConfig = value;
        }
    }


    /// <summary>
    /// Indicates whether the content tree is displaying more than max tree nodes.
    /// </summary>
    public bool IsDisplayMore
    {
        get
        {
            return innermedia.IsDisplayMore;
        }
        set
        {
            innermedia.IsDisplayMore = value;
        }
    }


    /// <summary>
    /// Gets or sets starting path of control.
    /// </summary>
    public string StartingPath
    {
        get
        {
            return mStartingPath;
        }
        set
        {
            mStartingPath = value.StartsWith("~/") ? Server.MapPath(value) : value;
        }
    }


    /// <summary>
    /// Search text to filter data.
    /// </summary>
    public string SearchText
    {
        get
        {
            mSearchText = ValidationHelper.GetString(ViewState["SearchText"], "");
            return mSearchText;
        }
        set
        {
            mSearchText = value;
            ViewState["SearchText"] = mSearchText;
        }
    }


    public DataSet DataSource
    {
        get
        {
            return innermedia.DataSource;
        }
        set
        {
            innermedia.DataSource = value;
        }
    }

    #endregion


    #region "Control events"

    protected void Page_Load(object sender, EventArgs e)
    {
        // If processing the request should not continue
        if (StopProcessing)
        {
            Visible = false;
        }
        else
        {
            // Initialize controls
            SetupControls();
        }
    }


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


    #region "Private methods"

    /// <summary>
    /// Initializes all nested controls.
    /// </summary>
    private void SetupControls()
    {
        InitializeControlScripts();

        // Initialize inner view control
        innermedia.FileSystemPath = StartingPath;

        // Set grid definition
        innermedia.ListViewControl.GridName = Config.ShowFolders ? "~/CMSModules/Content/Controls/Dialogs/FileSystemSelector/FolderView.xml" : "~/CMSModules/Content/Controls/Dialogs/FileSystemSelector/FileSystemView.xml";

        // Set inner control binding columns
        innermedia.FileIdColumn = "path";
        innermedia.FileNameColumn = "name";
        innermedia.FileExtensionColumn = "type";
        innermedia.FileSizeColumn = "size";
        innermedia.SearchText = SearchText;

        // Register for inner media events
        innermedia.GetArgumentSet += innermedia_GetArgumentSet;

        // Parent directory button
        if ((ShowParentButton) && (!string.IsNullOrEmpty(NodeParentID)))
        {
            plcParentButton.Visible = true;
            imgParent.ImageUrl = "~/App_Themes/Default/Images/CMSModules/CMS_Content/Dialogs/parent.gif";
            mSaveText = GetString("dialogs.mediaview.parentfolder");
            btnParent.OnClientClick = String.Format("SelectNode('{0}');SetParentAction('{0}'); return false;", NodeParentID.Replace("\\", "\\\\").Replace("'", "\\'"));
        }
        innermedia.Configuration = Config;

    }


    /// <summary>
    /// Initializes scrips used by the control.
    /// </summary>
    private void InitializeControlScripts()
    {
        const string script = @"
        function SetTreeRefreshAction(path) {
            SetAction('refreshtree', path);
            RaiseHiddenPostBack();
        }
        function SetRefreshAction() {
            SetAction('refresh', '');
            RaiseHiddenPostBack();
        }
        function SetDeleteAction(argument) {
            SetAction('delete', argument);
            RaiseHiddenPostBack();
        }
        function SetSelectAction(argument) {
            SetAction('select', argument);
            RaiseHiddenPostBack();
        }
        function SetParentAction(argument) {
            SetAction('parentselect', argument);
            RaiseHiddenPostBack();
        }";

        ScriptManager.RegisterStartupScript(this, GetType(), "DialogsSelectAction", script, true);
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
    /// <param name="dr">DataRow with all the item data</param>
    /// <param name="isSelected">Indicates whether the set is required for an selected item</param>
    string innermedia_GetArgumentSet(DataRow dr)
    {
        // Return required argument set
        return GetArgumentSet(dr);
    }

    #endregion


    #region "Helper methods"

    /// <summary>
    /// Returns argument set for the passed file data row.
    /// </summary>
    /// <param name="dr">Data row object holding all the data on current file</param>
    private string GetArgumentSet(DataRow dr)
    {
        // Common information for both content & attachments
        string result = String.Format("{1}{0}{2}{0}{3}", ARG_SEPARATOR, dr[innermedia.FileIdColumn].ToString().Replace("\\", "\\\\"), DataHelper.GetSizeString(ValidationHelper.GetLong(dr[innermedia.FileSizeColumn], 0)), dr["isfile"]);

        return result;
    }


    /// <summary>
    /// Ensures no item is selected.
    /// </summary>
    public void ResetSearch()
    {
        dialogSearch.ResetSearch();
        SearchText = "";
    }

    #endregion
}
