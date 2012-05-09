using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Security.Principal;
using System.Collections.Generic;

using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.UIControls;
using CMS.IO;
using CMS.SiteProvider;
using CMS.SettingsProvider;
using CMS.Controls;
using CMS.ExtendedControls;

public partial class CMSModules_System_Files_System_FilesMetafiles : SiteManagerPage
{
    #region "Variables"

    protected int currentSiteId = 0;
    protected int siteId = 0;

    protected string filterWhere = null;

    private static readonly Hashtable mErrors = new Hashtable();

    #endregion


    #region "Properties"

    /// <summary>
    /// Current log context.
    /// </summary>
    public LogContext CurrentLog
    {
        get
        {
            return EnsureLog();
        }
    }


    /// <summary>
    /// Current Error.
    /// </summary>
    private string CurrentError
    {
        get
        {
            return ValidationHelper.GetString(mErrors["DeleteError_" + ctlAsync.ProcessGUID], string.Empty);
        }
        set
        {
            mErrors["DeleteError_" + ctlAsync.ProcessGUID] = value;
        }
    }

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptHelper.RegisterTooltip(Page);

        // Initialize events
        ctlAsync.OnFinished += ctlAsync_OnFinished;
        ctlAsync.OnError += ctlAsync_OnError;
        ctlAsync.OnRequestLog += ctlAsync_OnRequestLog;
        ctlAsync.OnCancel += ctlAsync_OnCancel;

        if (RequestHelper.IsCallback())
        {
            this.pnlContent.Visible = false;

            gridFiles.StopProcessing = true;
            siteSelector.StopProcessing = true;

            return;
        }

        // Setup the controls
        gridFiles.OnExternalDataBound += new OnExternalDataBoundEventHandler(gridFiles_OnExternalDataBound);
        gridFiles.OnAction += new OnActionEventHandler(gridFiles_OnAction);

        ControlsHelper.RegisterPostbackControl(this.btnOk);

        currentSiteId = CMSContext.CurrentSiteID;

        // Setup the filters
        siteSelector.DropDownSingleSelect.AutoPostBack = true;
        siteSelector.UniSelector.OnSelectionChanged += new EventHandler(UniSelector_OnSelectionChanged);

        if (!RequestHelper.IsPostBack() && (currentSiteId > 0))
        {
            siteSelector.Value = currentSiteId;
        }

        siteId = ValidationHelper.GetInteger(siteSelector.Value, 0);
        if (siteId > 0)
        {
            filterWhere = "MetaFileSiteID = " + siteId;
            gridFiles.WhereCondition = filterWhere;
        }
        else if (siteId == UniSelector.US_GLOBAL_RECORD)
        {
            // Global files
            filterWhere = "MetaFileSiteID IS NULL";
            gridFiles.WhereCondition = filterWhere;
        }

        // Fill the objecttype DDL
        if (!RequestHelper.IsPostBack())
        {
            LoadObjectTypes();
        }

        // Add object type condition
        string selectedType = SqlHelperClass.GetSafeQueryString(drpObjectType.SelectedValue, false);
        if (!string.IsNullOrEmpty(selectedType))
        {
            filterWhere = SqlHelperClass.AddWhereCondition(filterWhere, "MetaFileObjectType = '" + selectedType + "'", "AND");
            gridFiles.WhereCondition = filterWhere;
        }

        if (!RequestHelper.IsPostBack())
        {
            // Fill in the actions
            drpAction.Items.Add(new ListItem(GetString("general.selectaction"), ""));

            bool copyDB = true;
            bool copyFS = true;
            bool deleteDB = true;
            bool deleteFS = true;

            if (siteId > 0)
            {
                bool fs = StoreInFileSystem(siteId);
                bool db = StoreInDatabase(siteId);

                copyFS = deleteDB = fs;
                deleteFS = db;
                copyDB = db && fs;
            }

            if (copyDB)
            {
                drpAction.Items.Add(new ListItem("Copy to database", "copytodatabase"));
            }
            if (copyFS)
            {
                drpAction.Items.Add(new ListItem("Copy to file system", "copytofilesystem"));
            }
            if (deleteDB)
            {
                drpAction.Items.Add(new ListItem("Delete from database", "deleteindatabase"));
            }
            if (deleteFS)
            {
                drpAction.Items.Add(new ListItem("Delete from file system", "deleteinfilesystem"));
            }
        }
    }

    
    protected void UniSelector_OnSelectionChanged(object sender, EventArgs e)
    {
        LoadObjectTypes();
    }


    private void LoadObjectTypes()
    {
        drpObjectType.Items.Clear();
        drpObjectType.Items.Add(new ListItem(GetString("general.selectall"), ""));

        // Used object types
        List<string> objTypes = MetaFileInfoProvider.GetMetaFilesObjectTypes(siteId);
        ListItem[] items = new ListItem[objTypes.Count];
        int i = 0;
        foreach (string type in objTypes)
        {
            items[i++] = new ListItem(GetString("ObjectTasks." + type.Replace(".", "_").Replace("#", "_")), type);
        }
        Array.Sort(items, CompareObjectType);
        drpObjectType.Items.AddRange(items);
    }


    /// <summary>
    /// Comparison method for two ListItems (to sort the drop down with object types).
    /// </summary>
    /// <param name="item1">First item to compare</param>
    /// <param name="item2">Second item to compare</param>
    private static int CompareObjectType(ListItem item1, ListItem item2)
    {
        return item1.Text.CompareTo(item2.Text);
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        if ((siteId > 0) || (siteId == UniSelector.US_GLOBAL_RECORD))
        {
            // Hide the Site ID column
            if (this.gridFiles.GridView.Columns.Count > 0)
            {
                this.gridFiles.NamedColumns["SiteName"].Visible = false;
            }
        }

        pnlFooter.Visible = (this.gridFiles.GridView.Rows.Count > 0);

        this.lblGridInfo.Visible = !String.IsNullOrEmpty(this.lblGridInfo.Text);
        this.lblGridError.Visible = !String.IsNullOrEmpty(this.lblGridError.Text);
    }


    protected void gridFiles_OnAction(string actionName, object actionArgument)
    {
        int fileId = ValidationHelper.GetInteger(actionArgument, 0);
        string name = null;

        if (ProcessFile(fileId, actionName, ref name))
        {
            gridFiles.ReloadData();

            switch (actionName)
            {
                case "copytodatabase":
                    // Copy the file from file system to the database
                    this.lblGridInfo.Text = "The file '" + name + "' was copied to the database.";
                    break;

                case "copytofilesystem":
                    // Copy to file system
                    this.lblGridInfo.Text = "The file '" + name + "' was copied to the file system.";
                    break;

                case "deleteindatabase":
                    // Delete from database
                    this.lblGridInfo.Text = "The file '" + name + "' binary was deleted from the database.";
                    break;

                case "deleteinfilesystem":
                    // Delete from file system
                    this.lblGridInfo.Text = "The file '" + name + "' binary was deleted from the file system.";
                    break;
            }
        }
    }


    /// <summary>
    /// Copies the file binary to the database.
    /// </summary>
    /// <param name="fileId">MetaFile ID</param>
    /// <param name="name">Returning the metafile name</param>
    protected bool CopyToDatabase(int fileId, ref string name)
    {
        // Copy the file from file system to the database
        MetaFileInfo mi = MetaFileInfoProvider.GetMetaFileInfo(fileId);
        if (mi != null)
        {
            name = mi.MetaFileName;

            if (mi.MetaFileBinary == null)
            {
                // Ensure the binary data
                mi.MetaFileBinary = MetaFileInfoProvider.GetFile(mi, GetSiteName(mi.MetaFileSiteID));
                mi.Generalized.UpdateData();

                return true;
            }
        }

        return false;
    }


    /// <summary>
    /// Copies the file binary to the file system.
    /// </summary>
    /// <param name="fileId">MetaFile ID</param>
    /// <param name="name">Returning the metafile name</param>
    protected bool CopyToFileSystem(int fileId, ref string name)
    {
        // Copy the file from database to the file system
        MetaFileInfo mi = MetaFileInfoProvider.GetMetaFileInfo(fileId);
        if (mi != null)
        {
            name = mi.MetaFileName;

            // Ensure the physical file
            MetaFileInfoProvider.EnsurePhysicalFile(mi, GetSiteName(mi.MetaFileSiteID));

            return true;
        }

        return false;
    }


    /// <summary>
    /// Deletes the file binary from the database.
    /// </summary>
    /// <param name="fileId">MetaFile ID</param>
    /// <param name="name">Returning the metafile name</param>
    protected bool DeleteFromDatabase(int fileId, ref string name)
    {
        // Delete the file in database and ensure it in the file system
        MetaFileInfo mi = MetaFileInfoProvider.GetMetaFileInfo(fileId);
        if (mi != null)
        {
            name = mi.MetaFileName;

            MetaFileInfoProvider.EnsurePhysicalFile(mi, GetSiteName(mi.MetaFileSiteID));

            // Clear the binary data
            mi.MetaFileBinary = null;
            mi.Generalized.UpdateData();

            return true;
        }

        return false;
    }


    /// <summary>
    /// Deletes the file binary from the file system.
    /// </summary>
    /// <param name="fileId">MetaFile ID</param>
    /// <param name="name">Returning the metafile name</param>
    protected bool DeleteFromFileSystem(int fileId, ref string name)
    {
        // Delete the file in file system
        MetaFileInfo mi = MetaFileInfoProvider.GetMetaFileInfo(fileId);
        if (mi != null)
        {
            name = mi.MetaFileName;

            // Ensure the binary column first (check if exists)
            DataSet ds = MetaFileInfoProvider.GetMetaFiles("MetaFileID = " + fileId, null, "CASE WHEN MetaFileBinary IS NULL THEN 0 ELSE 1 END AS HasBinary", -1);
            if (!DataHelper.DataSourceIsEmpty(ds))
            {
                bool hasBinary = ValidationHelper.GetBoolean(ds.Tables[0].Rows[0][0], false);
                if (!hasBinary)
                {
                    // Copy the binary data to database
                    mi.MetaFileBinary = MetaFileInfoProvider.GetFile(mi, GetSiteName(mi.MetaFileSiteID));
                    mi.Generalized.UpdateData();
                }

                // Delete the file from the disk
                MetaFileInfoProvider.DeleteFile(GetSiteName(mi.MetaFileSiteID), mi.MetaFileGUID.ToString(), true, false);

                return true;
            }
        }

        return false;
    }



    /// <summary>
    /// Processes the given file.
    /// </summary>
    /// <param name="fileId">MetaFile ID</param>
    /// <param name="actionName">Action name</param>
    /// <param name="name">Returning the file name</param>
    protected bool ProcessFile(int fileId, string actionName, ref string name)
    {
        if (fileId > 0)
        {
            switch (actionName)
            {
                case "copytodatabase":
                    // Copy the file from file system to the database
                    return CopyToDatabase(fileId, ref name);

                case "copytofilesystem":
                    // Copy to file system
                    return CopyToFileSystem(fileId, ref name);

                case "deleteindatabase":
                    // Delete from database
                    return DeleteFromDatabase(fileId, ref name);

                case "deleteinfilesystem":
                    // Delete from file system
                    return DeleteFromFileSystem(fileId, ref name);
            }
        }

        return false;
    }


    /// <summary>
    /// Processes the files.
    /// </summary>
    /// <param name="parameter">Parameter for the action</param>
    protected void ProcessFiles(object parameter)
    {
        // Begin log
        AddLog("Processing files ...");

        object[] parameters = (object[])parameter;

        ArrayList items = (ArrayList)parameters[0];
        string action = (string)parameters[1];

        if ((items != null) && (items.Count > 0))
        {
            string name = null;
            int count = 0;

            // Process all items
            foreach (object id in items)
            {
                // Process the file
                int fileId = ValidationHelper.GetInteger(id, 0);

                if (ProcessFile(fileId, action, ref name))
                {
                    count++;

                    AddLog(name);
                }
                else if (!string.IsNullOrEmpty(name))
                {
                    AddLog(name + " SKIPPED");
                }
            }

        }
    }


    /// <summary>
    /// Gets the site name for the given site ID.
    /// </summary>
    /// <param name="siteId">Site ID</param>
    protected string GetSiteName(int siteId)
    {
        if (siteId <= 0)
        {
            return null;
        }

        // Get the site name
        SiteInfo si = SiteInfoProvider.GetSiteInfo(siteId);
        if (si != null)
        {
            return si.SiteName;
        }

        return null;
    }


    /// <summary>
    /// Returns true if the files are stored in file system on the given site.
    /// </summary>
    /// <param name="siteId">Site ID</param>
    protected bool StoreInFileSystem(int siteId)
    {
        return MetaFileInfoProvider.StoreFilesInFileSystem(GetSiteName(siteId));
    }


    /// <summary>
    /// Returns true if the files are stored in database on the given site.
    /// </summary>
    /// <param name="siteId">Site ID</param>
    protected bool StoreInDatabase(int siteId)
    {
        return MetaFileInfoProvider.StoreFilesInDatabase(GetSiteName(siteId));
    }


    /// <summary>
    /// Grid external data bound handler.
    /// </summary>
    protected object gridFiles_OnExternalDataBound(object sender, string sourceName, object parameter)
    {
        // Get the data row view from parameter
        DataRowView drv = null;
        if (parameter is DataRowView)
        {
            drv = (DataRowView)parameter;
        }
        else if (parameter is GridViewRow)
        {
            // Get data from the grid view row
            GridViewRow gvr = (parameter as GridViewRow);
            if (gvr != null)
            {
                drv = (DataRowView)gvr.DataItem;
            }
        }

        // Get the action button
        ImageButton btn = null;
        if (sender is ImageButton)
        {
            btn = (ImageButton)sender;
        }

        switch (sourceName)
        {
            case "delete":
                {
                    // Delete action
                    int siteId = ValidationHelper.GetInteger(drv["MetaFileSiteID"], 0);
                    string siteName = GetSiteName(siteId);

                    Guid guid = ValidationHelper.GetGuid(drv["MetaFileGUID"], Guid.Empty);
                    string extension = ValidationHelper.GetString(drv["MetaFileExtension"], "");

                    // Check if the file is in DB
                    bool db = ValidationHelper.GetBoolean(drv["HasBinary"], false);

                    // Check if the file is in the file system
                    bool fs = false;
                    string path = MetaFileInfoProvider.GetFilePhysicalPath(siteName, guid.ToString(), extension);
                    if (File.Exists(path))
                    {
                        fs = true;
                    }

                    // If the file is present in both file system and database, delete is allowed
                    if (fs && db)
                    {
                        // If the files are stored in file system, delete is allowed in database 
                        if (StoreInFileSystem(siteId))
                        {
                            btn.Attributes["onclick"] = btn.Attributes["onclick"].Replace("'delete'", "'deleteindatabase'");
                            btn.ToolTip = "Delete from database";
                            return parameter;
                        }
                        // If the files are stored in database, delete is allowed in file system
                        if (StoreInDatabase(siteId))
                        {
                            btn.Attributes["onclick"] = btn.Attributes["onclick"].Replace("'delete'", "'deleteinfilesystem'");
                            btn.ToolTip = "Delete from file system";
                            return parameter;
                        }
                    }

                    btn.Visible = false;
                }
                break;

            case "copy":
                {
                    // Delete action
                    int siteId = ValidationHelper.GetInteger(drv["MetaFileSiteID"], 0);
                    string siteName = GetSiteName(siteId);

                    Guid guid = ValidationHelper.GetGuid(drv["MetaFileGUID"], Guid.Empty);
                    string extension = ValidationHelper.GetString(drv["MetaFileExtension"], "");

                    // Check if the file is in DB
                    bool db = ValidationHelper.GetBoolean(drv["HasBinary"], false);

                    // Check if the file is in the file system
                    bool fs = false;
                    string path = MetaFileInfoProvider.GetFilePhysicalPath(siteName, guid.ToString(), extension);
                    if (File.Exists(path))
                    {
                        fs = true;
                    }

                    // If the file is stored in file system and the file is not present in database, copy to database is allowed
                    if (fs && !db && StoreInDatabase(siteId) && StoreInFileSystem(siteId))
                    {
                        btn.Attributes["onclick"] = btn.Attributes["onclick"].Replace("'copy'", "'copytodatabase'");
                        btn.ToolTip = "Copy to database";
                        //btn.ImageUrl = 
                        return parameter;
                    }
                    // If the file is stored in database and the file is not present in file system, copy to file system is allowed
                    if (db && !fs && StoreInFileSystem(siteId))
                    {
                        btn.Attributes["onclick"] = btn.Attributes["onclick"].Replace("'copy'", "'copytofilesystem'");
                        btn.ToolTip = "Copy to file system";
                        return parameter;
                    }

                    btn.Visible = false;
                }
                break;

            case "name":
                {
                    // MetaFile name
                    string name = ValidationHelper.GetString(drv["MetaFileName"], "");
                    Guid guid = ValidationHelper.GetGuid(drv["MetaFileGUID"], Guid.Empty);
                    int siteId = ValidationHelper.GetInteger(drv["MetaFileSiteID"], 0);
                    string extension = ValidationHelper.GetString(drv["MetaFileExtension"], "");

                    // File name
                    name = Path.GetFileNameWithoutExtension(name);

                    string url = ResolveUrl("~/CMSPages/GetMetaFile.aspx?fileguid=") + guid;
                    if (siteId != currentSiteId)
                    {
                        // Add the site name to the URL if not current site
                        SiteInfo si = SiteInfoProvider.GetSiteInfo(siteId);
                        if (si != null)
                        {
                            url += "&sitename=" + si.SiteName;
                        }
                    }

                    string tooltipSpan = name;
                    bool isImage = ImageHelper.IsImage(extension);
                    if (isImage)
                    {
                        int imageWidth = ValidationHelper.GetInteger(DataHelper.GetDataRowViewValue(drv, "MetaFileImageWidth"), 0);
                        int imageHeight = ValidationHelper.GetInteger(DataHelper.GetDataRowViewValue(drv, "MetaFileImageHeight"), 0);

                        string tooltip = UIHelper.GetTooltipAttributes(url, imageWidth, imageHeight, null, name, extension, null, null, 300);
                        tooltipSpan = "<span id=\"" + guid + "\" " + tooltip + ">" + name + "</span>";
                    }

                    return "<img class=\"Image16\" src=\"" + UIHelper.GetFileIconUrl(this.Page, extension, null) + "\" alt=\"" + name + "\" />&nbsp;<a href=\"" + url + "\" target=\"_blank\">" + tooltipSpan + "</a>";
                }

            case "size":
                // File size
                return DataHelper.GetSizeString(ValidationHelper.GetInteger(parameter, 0));

            case "yesno":
                // Yes / No
                return UniGridFunctions.ColoredSpanYesNo(parameter);

            case "site":
                {
                    int siteId = ValidationHelper.GetInteger(parameter, 0);
                    if (siteId > 0)
                    {
                        SiteInfo si = SiteInfoProvider.GetSiteInfo(siteId);
                        if (si != null)
                        {
                            return si.DisplayName;
                        }
                    }
                    return null;
                }

            case "storedinfilesystem":
                {
                    // Delete action
                    int siteId = ValidationHelper.GetInteger(drv["MetaFileSiteID"], 0);
                    string siteName = GetSiteName(siteId);

                    Guid guid = ValidationHelper.GetGuid(drv["MetaFileGUID"], Guid.Empty);
                    string extension = ValidationHelper.GetString(drv["MetaFileExtension"], "");

                    // Check if the file is in DB
                    bool db = ValidationHelper.GetBoolean(drv["HasBinary"], false);

                    // Check if the file is in the file system
                    bool fs = false;
                    string path = MetaFileInfoProvider.GetFilePhysicalPath(siteName, guid.ToString(), extension);
                    if (File.Exists(path))
                    {
                        fs = true;
                    }

                    return UniGridFunctions.ColoredSpanYesNo(fs);
                }
        }

        return parameter;
    }


    /// <summary>
    /// Action button handler.
    /// </summary>
    protected void btnOK_Click(object sender, EventArgs e)
    {
        if (!String.IsNullOrEmpty(drpAction.SelectedValue))
        {
            ArrayList items = null;

            if (drpWhat.SelectedValue == "all")
            {
                // Get only the appropriate set of items
                string where = filterWhere;
                switch (drpAction.SelectedValue)
                {
                    case "deleteindatabase":
                    case "copytofilesystem":
                        // Only process those where binary is available in DB
                        where = SqlHelperClass.AddWhereCondition(where, "MetaFileBinary IS NOT NULL");
                        break;

                    case "copytodatabase":
                        // Only copy those where the binary is missing
                        where = SqlHelperClass.AddWhereCondition(where, "MetaFileBinary IS NULL");
                        break;
                }

                // Get all, build the list of items
                DataSet ds = MetaFileInfoProvider.GetMetaFiles(where, null, "MetaFileID", 0);
                if (!DataHelper.DataSourceIsEmpty(ds))
                {
                    items = new ArrayList();

                    // Process all rows
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        int fileId = ValidationHelper.GetInteger(dr["MetaFileID"], 0);
                        items.Add(fileId);
                    }
                }
            }
            else
            {
                // Take selected items
                items = gridFiles.SelectedItems;
            }

            if ((items != null) && (items.Count > 0))
            {
                // Setup the async log
                pnlLog.Visible = true;
                pnlContent.Visible = false;

                titleElemAsync.TitleText = this.drpAction.SelectedItem.Text;
                btnCancel.Attributes.Add("onclick", ctlAsync.GetCancelScript(true) + "return false;");

                CurrentError = string.Empty;
                CurrentLog.Close();
                EnsureLog();

                // Process the file asynchronously
                ctlAsync.Parameter = new object[] { items, drpAction.SelectedValue };
                ctlAsync.RunAsync(ProcessFiles, WindowsIdentity.GetCurrent());
            }
        }
    }

    #endregion


    #region "Handling async thread"

    /// <summary>
    /// Cancel event handler.
    /// </summary>
    private void ctlAsync_OnCancel(object sender, EventArgs e)
    {
        ctlAsync.Parameter = null;
        AddError("The operation was canceled.");
        lblGridError.Text = CurrentError;
        CurrentLog.Close();

        this.pnlContent.Visible = true;
        this.pnlLog.Visible = false;

        // Reload the grid
        gridFiles.ClearSelectedItems();
        gridFiles.ReloadData();
    }


    /// <summary>
    /// Logs event handler.
    /// </summary>
    private void ctlAsync_OnRequestLog(object sender, EventArgs e)
    {
        ctlAsync.Log = CurrentLog.Log;
    }


    /// <summary>
    /// Error event handler.
    /// </summary>
    private void ctlAsync_OnError(object sender, EventArgs e)
    {
        if (ctlAsync.Status == AsyncWorkerStatusEnum.Running)
        {
            ctlAsync.Stop();
        }
        ctlAsync.Parameter = null;
        lblGridError.Text = CurrentError;
        CurrentLog.Close();

        // Reload the grid
        gridFiles.ClearSelectedItems();
        gridFiles.ReloadData();
    }


    /// <summary>
    /// Finished event handler.
    /// </summary>
    private void ctlAsync_OnFinished(object sender, EventArgs e)
    {
        lblGridError.Text = CurrentError;
        CurrentLog.Close();

        if (!string.IsNullOrEmpty(CurrentError))
        {
            ctlAsync.Parameter = null;
            lblGridError.Text = CurrentError;
        }

        this.pnlContent.Visible = true;
        this.pnlLog.Visible = false;

        // Reload the grid
        gridFiles.ClearSelectedItems();
        gridFiles.ReloadData();
    }


    /// <summary>
    /// Ensures the logging context.
    /// </summary>
    protected LogContext EnsureLog()
    {
        LogContext log = LogContext.EnsureLog(ctlAsync.ProcessGUID);
        log.Reversed = true;
        log.LineSeparator = "<br />";
        return log;
    }


    /// <summary>
    /// Adds the log information.
    /// </summary>
    /// <param name="newLog">New log information</param>
    protected void AddLog(string newLog)
    {
        EnsureLog();
        LogContext.AppendLine(newLog);
    }


    /// <summary>
    /// Adds the error to collection of errors.
    /// </summary>
    /// <param name="error">Error message</param>
    protected void AddError(string error)
    {
        AddLog(error);
        CurrentError = (error + "<br />" + CurrentError);
    }

    #endregion
}
