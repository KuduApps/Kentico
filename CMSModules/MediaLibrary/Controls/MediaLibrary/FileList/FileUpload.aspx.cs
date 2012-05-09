using System;
using System.Web;

using CMS.GlobalHelper;
using CMS.MediaLibrary;
using CMS.UIControls;

public partial class CMSModules_MediaLibrary_Controls_MediaLibrary_FileList_FileUpload : CMSModalPage
{
    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Register shortcuts.js script file
        ScriptHelper.RegisterShortcuts(this);        

        fileUpload.LibraryID = QueryHelper.GetInteger("libraryid", 0);
        fileUpload.FolderPath = HttpUtility.UrlDecode(QueryHelper.GetString("folderpath", ""));
        fileUpload.IsLiveSite = false;
        fileUpload.OnNotAllowed += new CMSAdminControl.NotAllowedEventHandler(fileUpload_OnNotAllowed);
    }


    private void fileUpload_OnNotAllowed(string permissionType, CMSAdminControl sender)
    {
        if (sender != null)
        {
            sender.StopProcessing = true;
        }
        fileUpload.StopProcessing = true;
        fileUpload.Visible = false;
        messageElem.ErrorMessage = MediaLibraryHelper.GetAccessDeniedMessage("filecreate");
        messageElem.DisplayMessage = true;
    }

    #endregion
}