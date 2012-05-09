using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.MediaLibrary;

/// <summary>
/// Summary description for MediaLibraryFunctions.
/// </summary>
public static class MediaLibraryFunctions
{
    /// <summary>
    /// Returns direct URL to the media file, user permissions are not checked.
    /// </summary>
    /// <param name="libraryId">Media library ID</param>
    /// <param name="filePath">File path</param>
    /// <param name="fileGuid">File GUID</param>
    /// <param name="fileName">File name</param>
    /// <param name="useSecureLinks">Generate secure link</param>
    /// <param name="downloadlink">Determines whether disposition parametr should be added to permanent link</param>
    public static string GetMediaFileUrl(object libraryId, object filePath, object fileGuid, object fileName, object useSecureLinks, object downloadlink)
    {
        string url = GetMediaFileUrl(libraryId, filePath, fileGuid, fileName, useSecureLinks);
        if (ValidationHelper.GetBoolean(useSecureLinks, true) && ValidationHelper.GetBoolean(downloadlink, true))
        {
            url = URLHelper.AddParameterToUrl(url, "disposition", "attachment");
        }
        return url;
    }


    /// <summary>
    /// Returns direct URL to the media file, user permissions are not checked.
    /// </summary>
    /// <param name="libraryId">Media library ID</param>
    /// <param name="filePath">File path</param>
    /// <param name="fileGuid">File GUID</param>
    /// <param name="fileName">File name</param>
    /// <param name="useSecureLinks">Generate secure link</param>
    public static string GetMediaFileUrl(object libraryId, object filePath, object fileGuid, object fileName, object useSecureLinks)
    {
        MediaLibraryInfo libInfo = MediaLibraryInfoProvider.GetMediaLibraryInfo(ValidationHelper.GetInteger(libraryId, 0));
        if (libInfo != null)
        {
            if (ValidationHelper.GetBoolean(useSecureLinks, true))
            {
                return MediaFileInfoProvider.GetMediaFileUrl(ValidationHelper.GetGuid(fileGuid, Guid.Empty), fileName.ToString());
            }
            else
            {
                return MediaFileInfoProvider.GetMediaFileUrl(CMSContext.CurrentSiteName, libInfo.LibraryFolder, filePath.ToString());
            }
        }
        return String.Empty;
    }


    /// <summary>
    /// Returns direct URL to the media file, user permissions are not checked.
    /// </summary>
    /// <param name="libraryId">Media library ID</param>
    /// <param name="filePath">File path</param>
    public static string GetMediaFileDirectUrl(object libraryId, object filePath)
    {
        MediaLibraryInfo libInfo = MediaLibraryInfoProvider.GetMediaLibraryInfo(ValidationHelper.GetInteger(libraryId, 0));
        if (libInfo != null)
        {
            return MediaFileInfoProvider.GetMediaFileUrl(CMSContext.CurrentSiteName, libInfo.LibraryFolder, filePath.ToString());
        }
        return String.Empty;
    }


    /// <summary>
    /// Returns URL to media file which is rewritten to calling GetMediaFile.aspx page where user permissions are checked.
    /// </summary>
    /// <param name="fileGuid">File GUID</param>
    /// <param name="fileName">File name</param>
    public static string GetMediaFileUrl(object fileGuid, object fileName)
    {
        if (fileName != null)
        {
            return MediaFileInfoProvider.GetMediaFileUrl(ValidationHelper.GetGuid(fileGuid, Guid.Empty), fileName.ToString());
        }

        return "";
    }


    /// <summary>
    /// Returns URL to detail of media file.
    /// </summary>
    /// <param name="fileId">File ID</param>
    public static string GetMediaFileDetailUrl(object fileId)
    {
        if (fileId != null)
        {
            return URLHelper.UpdateParameterInUrl(URLHelper.RemoveProtocolAndDomain(URLHelper.CurrentURL), "fileid", fileId.ToString());
        }

        return "";
    }


    /// <summary>
    /// Returns URL to detail of media file.
    /// </summary>
    /// <param name="parameter">Query parameter</param>
    /// <param name="fileId">File ID</param>
    public static string GetMediaFileDetailUrl(string parameter, object fileId)
    {
        return URLHelper.UpdateParameterInUrl(URLHelper.RemoveProtocolAndDomain(URLHelper.CurrentURL), parameter, fileId.ToString());
    }
}
