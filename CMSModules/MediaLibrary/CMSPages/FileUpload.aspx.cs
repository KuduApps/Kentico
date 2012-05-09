using System;
using System.Web;

using CMS.GlobalHelper;
using CMS.MediaLibrary;
using CMS.UIControls;

public partial class CMSModules_MediaLibrary_CMSPages_FileUpload : CMSLiveModalPage
{
    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Register shortcuts.js script file
        ScriptHelper.RegisterShortcuts(this);        

        fileUpload.LibraryID = QueryHelper.GetInteger("libraryid", 0);
        fileUpload.FolderPath = HttpUtility.UrlDecode(QueryHelper.GetString("folderpath", ""));
        fileUpload.IsLiveSite = true;
        fileUpload.OnNotAllowed += new CMSAdminControl.NotAllowedEventHandler(fileUpload_OnNotAllowed);
    }

    void fileUpload_OnNotAllowed(string permissionType, CMSAdminControl sender)
    {
        if (sender != null)
        {
            sender.StopProcessing = true;
        }

        fileUpload.StopProcessing = true;
        fileUpload.Visible = false;
        messageElem.DisplayMessage = true;
        messageElem.ErrorMessage = MediaLibraryHelper.GetAccessDeniedMessage("filecreate");
    }

    #endregion
}