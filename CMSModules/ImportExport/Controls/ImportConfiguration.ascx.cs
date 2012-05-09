using System;
using System.Collections;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.GlobalHelper;
using CMS.CMSImportExport;
using CMS.UIControls;
using CMS.CMSHelper;
using CMS.EventLog;
using CMS.ExtendedControls;
using CMS.IO;

public partial class CMSModules_ImportExport_Controls_ImportConfiguration : CMSUserControl
{
    private SiteImportSettings mSettings = null;

    #region "Properties"

    /// <summary>
    /// Import settings.
    /// </summary>
    public SiteImportSettings Settings
    {
        get
        {
            return mSettings;
        }
        set
        {
            mSettings = value;
        }
    }

    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!StopProcessing)
        {
            imgRefresh.ImageUrl = GetImageUrl("CMSModules/CMS_EmailQueue/refresh.png");
            imgDelete.ImageUrl = GetImageUrl("Design/Controls/EditModeButtons/delete.png");

            // Initialize button for adding packages
            newImportPackage.ImageUrl = ResolveUrl(GetImageUrl("Design/Controls/DirectUploader/uploadpackage.png"));
            newImportPackage.DisplayInline = true;
            newImportPackage.ImageWidth = 16;
            newImportPackage.ImageHeight = 16;
            newImportPackage.AllowedExtensions = "zip";
            newImportPackage.ParentElemID = ClientID;
            newImportPackage.ForceLoad = true;
            newImportPackage.ShowProgress = false;
            newImportPackage.CheckPermissions = true;
            newImportPackage.SourceType = MediaSourceEnum.PhysicalFile;
            newImportPackage.TargetFolderPath = ImportExportHelper.GetSiteUtilsFolder() + "Import";
            newImportPackage.InnerDivHtml = GetString("importconfiguration.uploadpackage");
            newImportPackage.InnerDivClass = "NewAttachment";
            newImportPackage.InnerLoadingDivHtml = GetString("importconfiguration.uploading");
            newImportPackage.InnerLoadingDivClass = "NewAttachmentLoading";
            newImportPackage.DisplayInline = true;
            newImportPackage.AfterSaveJavascript = "Refresh" + ClientID;

            ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "Refresh" + ClientID, ScriptHelper.GetScript("function Refresh" + ClientID + "(){" + Page.ClientScript.GetPostBackEventReference(btnRefresh, null).Replace("'", "\"") + "}"));

            lblImports.Text = GetString("ImportConfiguration.Imports");

            if (!Page.IsCallback && !RequestHelper.IsPostBack())
            {
                radAll.Text = GetString("ImportConfiguration.All");
                radNew.Text = GetString("ImportConfiguration.Date");

                // Load imports list
                RefreshPackageList(null);
            }
        }
    }


    /// <summary>
    /// Refresh button click handler.
    /// </summary>
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        RefreshPackageList(sender);
    }


    /// <summary>
    /// Refresh package list.
    /// </summary>
    /// <param name="sender">Sender object</param>
    private void RefreshPackageList(object sender)
    {
        // Reload the list
        lstImports.Items.Clear();
        LoadImports();

        // Update panel
        pnlUpdate.Update();

        // Select first item
        if (lstImports.Items.Count > 0)
        {
            lstImports.SelectedIndex = 0;
            btnDelete.OnClientClick = "if (!confirm('" + GetString("importconfiguration.deleteconf") + "')) { return false;}";
        }
        else
        {
            btnDelete.OnClientClick = "";
        }
    }


    /// <summary>
    /// Refresh button click handler.
    /// </summary>
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (!String.IsNullOrEmpty(lstImports.SelectedValue))
        {
            try
            {
                File.Delete(ImportExportHelper.GetSiteUtilsFolder() + "Import\\" + lstImports.SelectedValue);
            }
            catch (Exception ex)
            {
                ScriptHelper.RegisterStartupScript(Page, typeof(string), "ErrorMessage", ScriptHelper.GetAlertScript(ex.Message));
            }
            finally
            {
                RefreshPackageList(sender);
            }
        }
    }


    // Load imports lsit
    private void LoadImports()
    {
        if (lstImports.Items.Count == 0)
        {
            ArrayList files = null;

            // Get import packages
            try
            {
                files = ImportProvider.GetImportFilesList();
            }
            catch
            {
                // Show error and log exception
                string path = ImportExportHelper.GetSiteUtilsFolderRelativePath();
                if (path == null)
                {
                    path = ImportExportHelper.GetSiteUtilsFolder();
                }

                lblError.Text = GetString("importconfiguration.securityerror").Replace("{0}", path + "Import");
            }

            if ((files != null) && (files.Count != 0))
            {
                lstImports.Enabled = true;
                lstImports.DataSource = files;
                lstImports.DataBind();
            }
            else
            {
                lstImports.Enabled = false;
            }
        }

        if (!RequestHelper.IsPostBack())
        {
            try
            {
                lstImports.SelectedIndex = 0;
            }
            catch
            {
            }
        }
    }


    /// <summary>
    /// Initialize control.
    /// </summary>
    public void InitControl()
    {
        // Could be used in the future:)
    }


    /// <summary>
    /// Apply control settings.
    /// </summary>
    public bool ApplySettings()
    {
        string result = null;

        // File is not selected, inform the user
        if (lstImports.SelectedItem == null)
        {
            result = GetString("ImportConfiguration.FileError");
        }

        if (string.IsNullOrEmpty(result))
        {
            try
            {
                // Set current user information
                Settings.CurrentUser = CMSContext.CurrentUser;
                // Set source filename
                Settings.SourceFilePath = ImportExportHelper.GetSiteUtilsFolder() + "Import\\" + lstImports.SelectedValue;

                // Unpack the files
                Settings.TemporaryFilesCreated = false;

                // Init default values
                Settings.ImportType = radAll.Checked ? ImportTypeEnum.All : ImportTypeEnum.New;
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
                return false;
            }
        }
        else
        {
            lblError.Text = result;
            return false;
        }

        return true;
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        lblError.Visible = (!string.IsNullOrEmpty(lblError.Text));
    }
}
