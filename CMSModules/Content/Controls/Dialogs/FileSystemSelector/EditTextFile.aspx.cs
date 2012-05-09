using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.IO;
using CMS.ExtendedControls;
using CMS.EventLog;

public partial class CMSModules_Content_Controls_Dialogs_FileSystemSelector_EditTextFile : CMSModalSiteManagerPage
{
    #region "Variables"

    private string filePath = null;
    private string fileName = null;

    private string extension = null;

    private bool newFile = false;

    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!QueryHelper.ValidateHash("hash"))
        {
            // Redirect to error page
            URLHelper.Redirect("~/CMSMessages/Error.aspx?title=" + GetString("dialogs.badhashtitle") + "&text=" + GetString("dialogs.badhashtext") + "&cancel=1");
        }

        filePath = Server.MapPath(QueryHelper.GetString("filepath", ""));
        extension = QueryHelper.GetString("newfileextension", "");

        if (!String.IsNullOrEmpty(extension))
        {
            // New file
            newFile = true;
        }
        else
        {
            // Edit file
            if (!File.Exists(filePath))
            {
                // Redirect to error page
                URLHelper.Redirect("~/CMSMessages/Error.aspx?text=" + GetString("general.filedoesntexist") + "&cancel=1");
            }

            lblName.Text = GetString("general.filename");
            fileName = Path.GetFileNameWithoutExtension(filePath);

            // Setup the controls
            if (!RequestHelper.IsPostBack())
            {
                this.txtName.Text = fileName;
                this.txtContent.Text = File.ReadAllText(filePath);
            }

            extension = Path.GetExtension(filePath);
        }

        // Setup the syntax highlighting
        switch (extension.TrimStart('.').ToLower())
        {
            case "css":
                this.txtContent.Language = LanguageEnum.CSS;
                break;

            case "skin":
                this.txtContent.Language = LanguageEnum.ASPNET;
                break;

            case "xml":
                this.txtContent.Language = LanguageEnum.XML;
                break;

            case "html":
                this.txtContent.Language = LanguageEnum.HTMLMixed;
                break;

            case "cs":
                this.txtContent.Language = LanguageEnum.CSharp;
                break;

            case "js":
                this.txtContent.Language = LanguageEnum.JavaScript;
                break;
        }

        // Setup the labels
        this.lblExt.Text = extension;

        this.Page.Header.Title = GetString("filemanageredit.header.file");

        this.CurrentMaster.Title.TitleText = GetString("filemanageredit.header.file");
        this.CurrentMaster.Title.TitleImage = ResolveUrl(GetFileIconUrl(extension, "icons"));
    }


    protected void btnOK_Click(object sender, EventArgs e)
    {
        // Check valid input
        string errMsg = new Validator().
            NotEmpty(this.txtName.Text, GetString("img.errors.filename")).
            IsFolderName(this.txtName.Text, GetString("img.errors.filename")).
            Result;

        // Prepare the path
        string path = filePath;
        if (!newFile)
        {
            path = Path.GetDirectoryName(path);
        }
        path += "/" + txtName.Text + extension;

        // Check the file name for existence
        if (!this.txtName.Text.Equals(fileName, StringComparison.InvariantCultureIgnoreCase))
        {
            if (File.Exists(path))
            {
                errMsg = GetString("general.fileexists");
            }
        }

        if (!String.IsNullOrEmpty(errMsg))
        {
            this.lblError.Text = errMsg;
            this.lblError.Visible = true;
        }
        else
        {
            try
            {
                if (!newFile && !path.Equals(filePath, StringComparison.InvariantCultureIgnoreCase))
                {
                    // Move the file to the new location
                    File.WriteAllText(filePath, this.txtContent.Text);
                    File.Move(filePath, path);
                }
                else
                {
                    // Create the file or write into it
                    File.WriteAllText(path, this.txtContent.Text);
                }

                string script = "wopener.SetRefreshAction(); window.close()";

                this.ltlScript.Text = ScriptHelper.GetScript(script);
            }
            catch (Exception ex)
            {
                this.lblError.Text = ex.Message;
                this.lblError.Visible = true;

                // Log the exception
                EventLogProvider.LogException("FileSystemSelector", "SAVEFILE", ex);
            }
        }
    }
}
