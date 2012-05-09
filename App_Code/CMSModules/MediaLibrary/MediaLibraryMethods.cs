using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;

using CMS.GlobalHelper;

/// <summary>
/// Media library methods - wrapping methods for macro resolver.
/// </summary>
public static class MediaLibraryMethods
{
    /// <summary>
    /// Registers all media library methods to macro resolver.
    /// </summary>
    public static void RegisterMethods()
    {
        MacroMethods.RegisterMethod("GetMediaFileUrl", GetMediaFileUrl, typeof(string), "Returns direct URL to the media file, user permissions are not checked.", GetMethodFormat("GetMediaFileUrl"), 5, new object[,] { { "libraryId", typeof(object), "Media library ID." }, { "filePath", typeof(object), "File path." }, { "fileGuid", typeof(object), "File GUID." }, { "fileName", typeof(object), "File name." }, { "useSecureLinks", typeof(object), "Determines whether to generate secure link." }, { "downloadlink", typeof(object), "Determines whether disposition parametr should be added to permanent link." } }, null, new List<Type>() { typeof(TransformationNamespace) });
        MacroMethods.RegisterMethod("GetMediaFileUrlWithCheck", GetMediaFileUrlWithCheck, typeof(string), "Returns URL to media file which is rewritten to calling GetMediaFile.aspx page where user permissions are checked.", GetMethodFormat("GetMediaFileUrlWithCheck"), 2, new object[,] { { "fileGuid", typeof(object), "File GUID." }, { "fileName", typeof(object), "File name." } }, null, new List<Type>() { typeof(TransformationNamespace) });
        MacroMethods.RegisterMethod("GetMediaFileDirectUrl", GetMediaFileDirectUrl, typeof(string), "Returns direct URL to the media file, user permissions are not checked.", GetMethodFormat("GetMediaFileDirectUrl"), 2, new object[,] { { "libraryId", typeof(object), "Media library ID." }, { "filePath", typeof(object), "File path." } }, null, new List<Type>() { typeof(TransformationNamespace) });
        MacroMethods.RegisterMethod("GetMediaFileDetailUrl", GetMediaFileDetailUrl, typeof(string), "Returns URL to detail of media file.", GetMethodFormat("GetMediaFileDetailUrl"), 1, new object[,] { { "fileId", typeof(object), "File ID." }, { "parameter", typeof(string), "Query parameter name (\"fileId\" by default)." } }, null, new List<Type>() { typeof(TransformationNamespace) });
    }


    /// <summary>
    /// Returns the method format for registration into the macro methods hashtable.
    /// </summary>
    /// <param name="method">Method name</param>
    private static string GetMethodFormat(string method)
    {
        return "{name} applied to {args}";
    }


    /// <summary>
    /// Returns direct URL to the media file, user permissions are not checked.
    /// </summary>
    /// <param name="parameters">
    /// (Media library ID; File path; File GUID; File name; Generate secure link) OR 
    /// (Media library ID; File path; File GUID; File name; Generate secure link; Determines whether disposition parametr should be added to permanent link)
    /// </param>
    public static object GetMediaFileUrl(params object[] parameters)
    {
        switch (parameters.Length)
        {
            case 5:
                return MediaLibraryFunctions.GetMediaFileUrl(parameters[0], parameters[1], parameters[2], parameters[3], parameters[4]);

            case 6:
                return MediaLibraryFunctions.GetMediaFileUrl(parameters[0], parameters[1], parameters[2], parameters[3], parameters[4], parameters[5]);

            default:
                throw new NotSupportedException();
        }
    }


    /// <summary>
    /// Returns URL to media file which is rewritten to calling GetMediaFile.aspx page where user permissions are checked.
    /// </summary>
    /// <param name="parameters">File GUID; File name</param>
    public static object GetMediaFileUrlWithCheck(params object[] parameters)
    {
        switch (parameters.Length)
        {
            case 2:
                return MediaLibraryFunctions.GetMediaFileUrl(parameters[0], parameters[1]);

            default:
                throw new NotSupportedException();
        }
    }


    /// <summary>
    /// Returns direct URL to the media file, user permissions are not checked.
    /// </summary>
    /// <param name="libraryId">Media library ID</param>
    /// <param name="filePath">File path</param>
    public static string GetMediaFileDirectUrl(params object[] parameters)
    {
        switch (parameters.Length)
        {
            case 2:
                return MediaLibraryFunctions.GetMediaFileDirectUrl(parameters[0], parameters[1]);

            default:
                throw new NotSupportedException();
        }
    }


    /// <summary>
    /// Returns URL to detail of media file.
    /// </summary>
    /// <param name="parameters">(FileID) OR (File ID; Query parameter)</param>
    public static object GetMediaFileDetailUrl(params object[] parameters)
    {
        switch (parameters.Length)
        {
            case 1:
                return MediaLibraryFunctions.GetMediaFileDetailUrl(parameters[0]);

            case 2:
                return MediaLibraryFunctions.GetMediaFileDetailUrl(ValidationHelper.GetString(parameters[1], ""), parameters[0]);

            default:
                throw new NotSupportedException();
        }
    }
}
