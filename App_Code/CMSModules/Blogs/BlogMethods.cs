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
/// Blog methods - wrapping methods for macro resolver.
/// </summary>
public class BlogMethods
{
    /// <summary>
    /// Registers all blog methods to macro resolver.
    /// </summary>
    public static void RegisterMethods()
    {
        MacroMethods.RegisterMethod("GetUserName", GetUserName, typeof(string), "Returns user name.", GetMethodFormat("GetUserName"), 1, new object[,] { { "userId", typeof(object), "User ID." } }, null, new List<Type>() { typeof(TransformationNamespace) });
        MacroMethods.RegisterMethod("GetUserFullName", GetUserFullName, typeof(string), "Returns user full name.", GetMethodFormat("GetUserFullName"), 1, new object[,] { { "userId", typeof(object), "User ID." } }, null, new List<Type>() { typeof(TransformationNamespace) });
        MacroMethods.RegisterMethod("GetBlogCommentsCount", GetBlogCommentsCount, typeof(int), "Returns number of comments of given blog.", GetMethodFormat("GetBlogCommentsCount"), 2, new object[,] { { "postId", typeof(object), "Post document ID." }, { "postAliasPath", typeof(object), "Post alias path." }, { "includingTrackbacks", typeof(bool), "Indicates if trackback comments should be included (true by default)." } }, null, new List<Type>() { typeof(TransformationNamespace) });
        MacroMethods.RegisterMethod("GetDocumentTags", GetDocumentTags, typeof(string), "Gets a list of links of tags assigned for the specific document pointing to the page with URL specified.", GetMethodFormat("GetDocumentTags"), 3, new object[,] { { "documentGroupId", typeof(object), "ID of the group document tags belong to." }, { "documentTags", typeof(object), "String containing all the tags related to the document." }, { "documentListPageUrl", typeof(string), "URL of the page displaying other documents of the specified tag." } }, null, new List<Type>() { typeof(TransformationNamespace) });
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
    /// Returns user name.
    /// </summary>
    /// <param name="parameters">User id</param>
    public static object GetUserName(params object[] parameters)
    {
        switch (parameters.Length)
        {
            case 1:
                return BlogFunctions.GetUserName(parameters[0]);

            default:
                throw new NotSupportedException();
        }
    }


    /// <summary>
    /// Returns user full name.
    /// </summary>
    /// <param name="parameters">User id</param>
    public static object GetUserFullName(params object[] parameters)
    {
        switch (parameters.Length)
        {
            case 1:
                return BlogFunctions.GetUserFullName(parameters[0]);

            default:
                throw new NotSupportedException();
        }
    }


    /// <summary>
    /// Returns number of comments of given blog.
    /// </summary>
    /// <param name="parameters">(Post document id; Post alias path) OR (Post document id; Post alias path; Indicates if trackback comments should be included)</param>
    public static object GetBlogCommentsCount(params object[] parameters)
    {
        switch (parameters.Length)
        {
            case 2:
                return BlogFunctions.GetBlogCommentsCount(parameters[0], parameters[1]);

            case 3:
                return BlogFunctions.GetBlogCommentsCount(parameters[0], parameters[1], ValidationHelper.GetBoolean(parameters[2], true));

            default:
                throw new NotSupportedException();
        }
    }


    /// <summary>
    /// Gets a list of links of tags assigned for the specific document pointing to the page with URL specified.
    /// </summary>
    /// <param name="parameters">ID of the group document tags belong to; String containing all the tags related to the document; URL of the page displaying other documents of the specified tag</param>
    public static object GetDocumentTags(params object[] parameters)
    {
        switch (parameters.Length)
        {
            case 3:
                return BlogFunctions.GetDocumentTags(parameters[0], parameters[1], ValidationHelper.GetString(parameters[2], ""));

            default:
                throw new NotSupportedException();
        }
    }
}
