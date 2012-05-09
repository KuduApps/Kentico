using System;
using System.Collections.Generic;
using System.Linq;

using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.IO;

public partial class CMSModules_Content_Controls_Dialogs_FileSystemSelector_NewFolder : CMSModalSiteManagerPage
{
    #region "Variables"

    private string targetPath = null;

    #endregion


    #region "Page methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        lblName.Text = GetString("folder_edit.foldername");

        targetPath = QueryHelper.GetString("targetpath", string.Empty);

        Page.Header.Title = GetString("dialogs.newfoldertitle");

        CurrentMaster.Title.TitleText = GetString("media.folder.new");
        CurrentMaster.Title.TitleImage = ResolveUrl(GetImageUrl("CMSModules/CMS_MediaLibrary/librarynew.png", true));
    }


    protected void btnOK_Click(object sender, EventArgs e)
    {
        // Check valid input
        string errMsg = new Validator().
        NotEmpty(txtName.Text, GetString("media.folder.foldernameempty")).
        IsFolderName(txtName.Text, GetString("media.folder.foldernameerror")).
        Result;

        string path = String.Format("{0}\\{1}", targetPath, txtName.Text);

        // Check existing
        if (String.IsNullOrEmpty(errMsg) && Directory.Exists(path))
        {
            errMsg = GetString("media.folder.folderexist");
        }

        if (!String.IsNullOrEmpty(errMsg))
        {
            lblError.Text = errMsg;
            lblError.Visible = true;
        }
        else
        {
            try
            {
                // Create the folder
                Directory.CreateDirectory(path);

                ltlScript.Text = ScriptHelper.GetScript(String.Format("wopener.SetTreeRefreshAction({0}); window.close()", ScriptHelper.GetString(path)));
            }
            catch (Exception ex)
            {
                // Display an error to the user
                lblError.Text = String.Format("{0} {1}", GetString("general.erroroccurred"), ex.Message);
                lblError.Visible = true;
            }
        }
    }

    #endregion
}
