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

using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.UIControls;
using CMS.IO;
using CMS.SiteProvider;
using CMS.SettingsProvider;
using CMS.Controls;
using CMS.ExtendedControls;
using CMS.TreeEngine;

public partial class CMSModules_System_Files_System_FilesAttachments : SiteManagerPage
{
    #region "Variables"

    protected int currentSiteId = 0;
    protected int siteId = 0;

    protected string siteWhere = null;

    protected AttachmentManager mManager = null;

    private static readonly Hashtable mErrors = new Hashtable();

    #endregion


    #region "Properties"

    /// <summary>
    /// Attachment manager.
    /// </summary>
    protected AttachmentManager Manager
    {
        get
        {
            if (mManager == null)
            {
                mManager = new AttachmentManager();
            }

            return mManager;
        }
    }


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

        if (IsCallback)
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

        // Setup the site selection
        siteSelector.DropDownSingleSelect.AutoPostBack = true;

        if (!RequestHelper.IsPostBack() && (currentSiteId > 0))
        {
            siteSelector.Value = currentSiteId;
        }

        siteId = ValidationHelper.GetInteger(siteSelector.Value, 0);
        if (siteId > 0)
        {
            siteWhere = "AttachmentSiteID = " + siteId;
            gridFiles.WhereCondition = siteWhere;
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



    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        if (siteId > 0)
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
        int attachmentId = ValidationHelper.GetInteger(actionArgument, 0);
        string name = null;

        if (ProcessFile(attachmentId, actionName, ref name))
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
    /// <param name="attachmentId">Attachment ID</param>
    /// <param name="name">Returning the attachment name</param>
    protected bool CopyToDatabase(int attachmentId, ref string name)
    {
        // Copy the file from file system to the database
        AttachmentInfo ai = Manager.GetAttachmentInfo(attachmentId, true);
        if (ai != null)
        {
            name = ai.AttachmentName;

            if (ai.AttachmentBinary == null)
            {
                // Ensure the binary data
                ai.AttachmentBinary = Manager.GetFile(ai, GetSiteName(ai.AttachmentSiteID));
                ai.Generalized.UpdateData();

                return true;
            }
        }

        return false;
    }


    /// <summary>
    /// Copies the file binary to the file system.
    /// </summary>
    /// <param name="attachmentId">Attachment ID</param>
    /// <param name="name">Returning the attachment name</param>
    protected bool CopyToFileSystem(int attachmentId, ref string name)
    {
        // Copy the file from database to the file system
        AttachmentInfo ai = Manager.GetAttachmentInfo(attachmentId, true);
        if (ai != null)
        {
            name = ai.AttachmentName;

            // Ensure the physical file
            Manager.EnsurePhysicalFile(ai, GetSiteName(ai.AttachmentSiteID));

            return true;
        }

        return false;
    }


    /// <summary>
    /// Deletes the file binary from the database.
    /// </summary>
    /// <param name="attachmentId">Attachment ID</param>
    /// <param name="name">Returning the attachment name</param>
    protected bool DeleteFromDatabase(int attachmentId, ref string name)
    {
        // Delete the file in database and ensure it in the file system
        AttachmentInfo ai = Manager.GetAttachmentInfo(attachmentId, false);
        if (ai != null)
        {
            name = ai.AttachmentName;

            Manager.EnsurePhysicalFile(ai, GetSiteName(ai.AttachmentSiteID));

            // Clear the binary data
            ai.AttachmentBinary = null;
            ai.Generalized.UpdateData();

            return true;
        }

        return false;
    }


    /// <summary>
    /// Deletes the file binary from the file system.
    /// </summary>
    /// <param name="attachmentId">Attachment ID</param>
    /// <param name="name">Returning the attachment name</param>
    protected bool DeleteFromFileSystem(int attachmentId, ref string name)
    {
        // Delete the file in file system
        AttachmentInfo ai = Manager.GetAttachmentInfo(attachmentId, false);
        if (ai != null)
        {
            name = ai.AttachmentName;

            // Ensure the binary column first (check if exists)
            DataSet ds = Manager.GetAttachments("AttachmentID = " + attachmentId, null, true, 0, "CASE WHEN AttachmentBinary IS NULL THEN 0 ELSE 1 END AS HasBinary");
            if (!DataHelper.DataSourceIsEmpty(ds))
            {
                bool hasBinary = ValidationHelper.GetBoolean(ds.Tables[0].Rows[0][0], false);
                if (!hasBinary)
                {
                    // Copy the binary data to database
                    ai.AttachmentBinary = Manager.GetFile(ai, GetSiteName(ai.AttachmentSiteID));
                    ai.Generalized.UpdateData();
                }

                // Delete the file from the disk
                AttachmentManager.DeleteFile(ai.AttachmentGUID, GetSiteName(ai.AttachmentSiteID), true, false);

                return true;
            }
        }

        return false;
    }



    /// <summary>
    /// Processes the given file.
    /// </summary>
    /// <param name="attachmentId">Attachment ID</param>
    /// <param name="actionName">Action name</param>
    /// <param name="name">Returning the file name</param>
    protected bool ProcessFile(int attachmentId, string actionName, ref string name)
    {
        if (attachmentId > 0)
        {
            switch (actionName)
            {
                case "copytodatabase":
                    // Copy the file from file system to the database
                    return CopyToDatabase(attachmentId, ref name);

                case "copytofilesystem":
                    // Copy to file system
                    return CopyToFileSystem(attachmentId, ref name);

                case "deleteindatabase":
                    // Delete from database
                    return DeleteFromDatabase(attachmentId, ref name);

                case "deleteinfilesystem":
                    // Delete from file system
                    return DeleteFromFileSystem(attachmentId, ref name);
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
                int attachmentId = ValidationHelper.GetInteger(id, 0);

                if (ProcessFile(attachmentId, action, ref name))
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
        return SettingsKeyProvider.GetBoolValue(GetSiteName(siteId) + ".CMSStoreFilesInFileSystem");
    }


    /// <summary>
    /// Returns true if the files are stored in database on the given site.
    /// </summary>
    /// <param name="siteId">Site ID</param>
    protected bool StoreInDatabase(int siteId)
    {
        return SettingsKeyProvider.GetBoolValue(GetSiteName(siteId) + ".CMSStoreFilesInDatabase");
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
                    int siteId = ValidationHelper.GetInteger(drv["AttachmentSiteID"], 0);
                    string siteName = GetSiteName(siteId);

                    Guid guid = ValidationHelper.GetGuid(drv["AttachmentGUID"], Guid.Empty);
                    string extension = ValidationHelper.GetString(drv["AttachmentExtension"], "");

                    // Check if the file is in DB
                    bool db = ValidationHelper.GetBoolean(drv["HasBinary"], false);

                    // Check if the file is in the file system
                    bool fs = false;
                    string path = AttachmentManager.GetFilePhysicalPath(siteName, guid.ToString(), extension);
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
                    int siteId = ValidationHelper.GetInteger(drv["AttachmentSiteID"], 0);
                    string siteName = GetSiteName(siteId);

                    Guid guid = ValidationHelper.GetGuid(drv["AttachmentGUID"], Guid.Empty);
                    string extension = ValidationHelper.GetString(drv["AttachmentExtension"], "");

                    // Check if the file is in DB
                    bool db = ValidationHelper.GetBoolean(drv["HasBinary"], false);

                    // Check if the file is in the file system
                    bool fs = false;
                    string path = AttachmentManager.GetFilePhysicalPath(siteName, guid.ToString(), extension);
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
                        //btn.ImageUrl = 
                        return parameter;
                    }

                    btn.Visible = false;
                }
                break;

            case "name":
                {
                    // Attachment name
                    string name = ValidationHelper.GetString(drv["AttachmentName"], "");
                    Guid guid = ValidationHelper.GetGuid(drv["AttachmentGUID"], Guid.Empty);
                    int siteId = ValidationHelper.GetInteger(drv["AttachmentSiteID"], 0);
                    string extension = ValidationHelper.GetString(drv["AttachmentExtension"], "");

                    // File name
                    name = Path.GetFileNameWithoutExtension(name);

                    string url = ResolveUrl("~/CMSPages/GetFile.aspx?guid=") + guid;
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
                        int imageWidth = ValidationHelper.GetInteger(DataHelper.GetDataRowViewValue(drv, "AttachmentImageWidth"), 0);
                        int imageHeight = ValidationHelper.GetInteger(DataHelper.GetDataRowViewValue(drv, "AttachmentImageHeight"), 0);

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
                    int siteId = ValidationHelper.GetInteger(drv["AttachmentSiteID"], 0);
                    string siteName = GetSiteName(siteId);

                    Guid guid = ValidationHelper.GetGuid(drv["AttachmentGUID"], Guid.Empty);
                    string extension = ValidationHelper.GetString(drv["AttachmentExtension"], "");

                    // Check if the file is in DB
                    bool db = ValidationHelper.GetBoolean(drv["HasBinary"], false);

                    // Check if the file is in the file system
                    bool fs = false;
                    string path = AttachmentManager.GetFilePhysicalPath(siteName, guid.ToString(), extension);
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
                string where = siteWhere;
                switch (drpAction.SelectedValue)
                { 
                    case "deleteindatabase":
                    case "copytofilesystem":
                        // Only process those where binary is available in DB
                        where = SqlHelperClass.AddWhereCondition(where, "AttachmentBinary IS NOT NULL");
                        break;

                    case "copytodatabase":
                        // Only copy those where the binary is missing
                        where = SqlHelperClass.AddWhereCondition(where, "AttachmentBinary IS NULL");
                        break;
                }

                // Get all, build the list of items
                DataSet ds = Manager.GetAttachments(where, gridFiles.SortDirect, false, 0, "AttachmentID");
                if (!DataHelper.DataSourceIsEmpty(ds))
                {
                    items = new ArrayList();

                    // Process all rows
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        int attachmentId = ValidationHelper.GetInteger(dr["AttachmentID"], 0);
                        items.Add(attachmentId);
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
