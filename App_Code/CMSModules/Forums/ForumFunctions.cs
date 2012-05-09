using System;

using CMS.Forums;
using CMS.TreeEngine;
using CMS.GlobalHelper;
using CMS.CMSHelper;

/// <summary>
/// Forum functions static class.
/// </summary>
public static class ForumFunctions
{
    /// <summary>
    /// Returns link to selected post.
    /// </summary>
    /// <param name="postIdPath">Post id path</param>
    /// <param name="forumId">Forum id</param>
    public static string GetPostURL(object postIdPath, object forumId)
    {
        string pIdPath = ValidationHelper.GetString(postIdPath, string.Empty);
        int fId = ValidationHelper.GetInteger(forumId, 0);

        return ForumPostInfoProvider.GetPostURL(pIdPath, fId, true);
    }


    /// <summary>
    /// Returns link to selected post.
    /// </summary>
    /// <param name="postIdPath">Post id path</param>
    /// <param name="forumId">Forum id</param>
    /// <param name="aliasPath">Alias path</param>
    public static string GetPostURL(object aliasPath, object postIdPath, object forumId)
    {
        string alPath = ValidationHelper.GetString(aliasPath, string.Empty);
        string pIdPath = ValidationHelper.GetString(postIdPath, string.Empty);
        int fId = ValidationHelper.GetInteger(forumId, 0);

        if (!String.IsNullOrEmpty(pIdPath) && (fId > 0))
        {
            // Get thread id from post id path
            int threadId = ForumPostInfoProvider.GetPostRootFromIDPath(pIdPath);

            if (threadId > 0)
            {
                string url = URLHelper.ResolveUrl(CMSContext.CurrentResolver.ResolveMacros(TreePathUtils.GetUrl(alPath)));
                url = URLHelper.UpdateParameterInUrl(url, "ForumId", fId.ToString());
                url = URLHelper.UpdateParameterInUrl(url, "ThreadId", threadId.ToString());
                url = URLHelper.EncodeQueryString(url);
                return url;
            }
        }

        return string.Empty;
    }
}
