using CMS.UIControls;
using CMS.MediaLibrary;
using CMS.CMSHelper;

/// <summary>
/// Media file WebDAV control.
/// </summary>
public partial class CMSModules_MediaLibrary_Controls_WebDAV_MediaFileWebDAVEditControl : WebDAVEditControl
{
    #region "Constructors"

    /// <summary>
    /// Creates instance.
    /// </summary>
    public CMSModules_MediaLibrary_Controls_WebDAV_MediaFileWebDAVEditControl()
    {
        mControlType = WebDAVControlTypeEnum.Media;
    }

    #endregion


    #region "Methods"

    /// <summary>
    /// Gets the media file URL.
    /// </summary>
    protected override string GetUrl()
    {
        return MediaFileURLProvider.GetWebDAVUrl(MediaLibraryName, MediaFilePath, GroupName);
    }


    /// <summary>
    /// Reload controls data.
    /// </summary>
    /// <param name="forceReload">Indicates if controls </param>
    public override void ReloadData(bool forceReload)
    {
        // Check media file permissions
        MediaLibraryInfo mli = (MediaLibraryInfo)MediaLibraryInfo;

        // Check authorization to filemodify or manage
        if ((mli != null) && ((MediaLibraryInfoProvider.IsUserAuthorizedPerLibrary(mli, "filemodify", CMSContext.CurrentUser)
            || MediaLibraryInfoProvider.IsUserAuthorizedPerLibrary(mli, "manage", CMSContext.CurrentUser)) || !CheckPermission))
        {
            Enabled = true;

            // Check if module 'Community' is loaded
            if (mli.LibraryGroupID > 0)
            {
                // Check 'GroupAdministrator' permission 
                Enabled = (GroupInfo != null) && CMSContext.CurrentUser.IsGroupAdministrator(mli.LibraryGroupID);
            }
        }
        else
        {
            Enabled = false;
        }

        base.ReloadData(forceReload);
    }

    #endregion
}
