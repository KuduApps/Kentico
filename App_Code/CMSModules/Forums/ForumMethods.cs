using System;
using System.Collections.Generic;

using CMS.GlobalHelper;

/// <summary>
/// Forum methods - wrapping methods for macro resolver.
/// </summary>
public static class ForumMethods
{
    /// <summary>
    /// Registers all forum methods to macro resolver.
    /// </summary>
    public static void RegisterMethods()
    {
        MacroMethods.RegisterMethod("GetPostURL", GetPostURL, typeof(string), "Returns link to selected post.", GetMethodFormat("GetPostURL"), 2, new object[,] { { "postIdPath", typeof(object), "Post ID path." }, { "forumId", typeof(object), "Forum ID." }, { "aliasPath", typeof(object), "Alias path." } }, null, new List<Type>() { typeof(TransformationNamespace) });
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
    /// Returns link to selected post.
    /// </summary>
    /// <param name="parameters">(Post ID path; Forum ID) OR (Post ID path; Forum ID; Alias path)</param>
    public static object GetPostURL(params object[] parameters)
    {
        switch (parameters.Length)
        {
            case 2:
                return ForumFunctions.GetPostURL(parameters[0], parameters[1]);

            case 3:
                return ForumFunctions.GetPostURL(parameters[0], parameters[1], parameters[2]);

            default:
                throw new NotSupportedException();
        }
    }
}
