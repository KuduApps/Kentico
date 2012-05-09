using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using CMS.SiteProvider;
using CMS.GlobalHelper;
using CMS.Blogs;
using CMS.CMSHelper;
using CMS.PortalEngine;

using TreeNode = CMS.TreeEngine.TreeNode;

/// <summary>
/// Blog functions.
/// </summary>
public class BlogFunctions
{
    /// <summary>
    /// Returns user name.
    /// </summary>
    /// <param name="userId">User id</param>
    public static string GetUserName(object userId)
    {
        int id = ValidationHelper.GetInteger(userId, 0);

        if (id > 0)
        {
            string key = "blogPostUserName" + id.ToString();

            // Most of the post will be from the same user, store fullname to the request to minimize the DB access
            if (RequestStockHelper.Contains(key))
            {
                return ValidationHelper.GetString(RequestStockHelper.GetItem(key), "");
            }
            else
            {
                DataSet ds = UserInfoProvider.GetUsers("UserID = " + id, null, 1, "UserName");
                if (!DataHelper.DataSourceIsEmpty(ds))
                {
                    string result = HTMLHelper.HTMLEncode(UserInfoProvider.TrimSitePrefix(ValidationHelper.GetString(ds.Tables[0].Rows[0]["UserName"], "")));
                    RequestStockHelper.Add(key, result);

                    return result;
                }
            }
        }
        return "";
    }


    /// <summary>
    /// Returns user full name.
    /// </summary>
    /// <param name="userId">User id</param>
    public static string GetUserFullName(object userId)
    {
        int id = ValidationHelper.GetInteger(userId, 0);

        if (id > 0)
        {
            string key = "TransfUserFullName_" + id.ToString();

            // Most of the post will be from the same user, store fullname to the request to minimize the DB access
            if (RequestStockHelper.Contains(key))
            {
                return ValidationHelper.GetString(RequestStockHelper.GetItem(key), "");
            }
            else
            {
                DataSet ds = UserInfoProvider.GetUsers("UserID = " + id, null, 1, "FullName");
                if (!DataHelper.DataSourceIsEmpty(ds))
                {
                    string result = HTMLHelper.HTMLEncode(ValidationHelper.GetString(ds.Tables[0].Rows[0]["FullName"], ""));
                    RequestStockHelper.Add(key, result);

                    return result;
                }
            }
        }
        return "";
    }


    /// <summary>
    /// Returns number of comments of given blog.
    /// </summary>
    /// <param name="postId">Post document id</param>
    /// <param name="postAliasPath">Post alias path</param>
    public static int GetBlogCommentsCount(object postId, object postAliasPath)
    {
        return GetBlogCommentsCount(postId, postAliasPath, true);
    }


    /// <summary>
    /// Returns number of comments of given blog.
    /// </summary>
    /// <param name="postId">Post document id</param>
    /// <param name="postAliasPath">Post alias path</param>
    /// <param name="includingTrackbacks">Indicates if trackback comments should be included</param>
    public static int GetBlogCommentsCount(object postId, object postAliasPath, bool includingTrackbacks)
    {
        int docId = ValidationHelper.GetInteger(postId, 0);
        string aliasPath = ValidationHelper.GetString(postAliasPath, "");
        CurrentUserInfo currentUser = CMSContext.CurrentUser;

        // There has to be the current site
        if (CMSContext.CurrentSite == null)
        {
            throw new Exception("[BlogFunctions.GetBlogCommentsCount]: There is no current site!");
        }

        bool isOwner = false;

        // Is user authorized to manage comments?
        bool selectOnlyPublished = (CMSContext.ViewMode == ViewModeEnum.LiveSite);
        TreeNode blogNode = BlogHelper.GetParentBlog(aliasPath, CMSContext.CurrentSiteName, selectOnlyPublished);
        if (blogNode != null)
        {
            isOwner = (currentUser.UserID == ValidationHelper.GetInteger(blogNode.GetValue("NodeOwner"), 0));
        }

        bool isUserAuthorized = (currentUser.IsAuthorizedPerResource("cms.blog", "Manage") || isOwner || BlogHelper.IsUserBlogModerator(currentUser.UserName, blogNode));

        // Get post comments
        return BlogCommentInfoProvider.GetPostCommentsCount(docId, !isUserAuthorized, isUserAuthorized, includingTrackbacks);
    }


    /// <summary>
    /// Gets a list of links of tags assigned for the specific document pointing to the page with URL specified
    /// </summary>
    /// <param name="documentGroupId">ID of the group document tags belong to</param>
    /// <param name="documentTags">String containing all the tags related to the document</param>
    /// <param name="documentListPage">URL of the page displaying other documents of the specified tag</param>
    public static string GetDocumentTags(object documentGroupId, object documentTags, string documentListPage)
    {
        return GetDocumentTags(documentGroupId, documentTags, null, documentListPage);
    }


    /// <summary>
    /// Gets a list of links of tags assigned for the specific document pointing to the page with URL specified.
    /// </summary>
    /// <param name="documentGroupId">ID of the group document tags belong to</param>
    /// <param name="documentTags">String containing all the tags related to the document</param>
    /// <param name="nodeAliasPath">Node alias path</param>
    /// <param name="documentListPage">Path or URL of the page displaying other documents of the specified tag</param>
    public static string GetDocumentTags(object documentGroupId, object documentTags, object nodeAliasPath, string documentListPage)
    {
        string result = "";
        string tags = ValidationHelper.GetString(documentTags, "");

        if (tags.Trim() != "")
        {
            // If list page was specified make a list of links, otherwise return just list of tags
            bool renderLink = !string.IsNullOrEmpty(documentListPage);
            string listPageUrl = "";
            int groupId = ValidationHelper.GetInteger(documentGroupId, 0);

            if (renderLink)
            {
                // If page specified by URL
                if (ValidationHelper.IsURL(documentListPage))
                {
                    // Resolve URL
                    listPageUrl = URLHelper.ResolveUrl(documentListPage);
                }
                else
                {
                    // Resolve path
                    listPageUrl = CMSContext.CurrentResolver.ResolvePath(documentListPage);
                }

                // Look for group ID of document parent if not supplied
                if (groupId == 0)
                {
                    string aliasPath = ValidationHelper.GetString(nodeAliasPath, CMSContext.CurrentPageInfo.NodeAliasPath);
                    string strGroupId = PageInfoProvider.GetParentProperty(CMSContext.CurrentSiteID,
                        (String.IsNullOrEmpty(aliasPath) ? CMSContext.CurrentPageInfo.NodeAliasPath : aliasPath),
                        "DocumentTagGroupID IS NOT NULL", "DocumentTagGroupID");
                    groupId = ValidationHelper.GetInteger(strGroupId, 0);
                }
            }

            // Go through the specified tags and make a list of them
            string[] tagsArr = tags.Split(',');
            for (int i = 0; i < tagsArr.Length; i++)
            {
                tagsArr[i] = tagsArr[i].Replace("\"", "").Trim();
            }
            Array.Sort(tagsArr);
            foreach (string tag in tagsArr)
            {
                if (renderLink)
                {
                    result += "<a href=\"" + listPageUrl + "?tagname=" + HttpUtility.UrlPathEncode(tag) + "&amp;groupid=" + groupId + "\">" + HTMLHelper.HTMLEncode(tag) + "</a>, ";
                }
                else
                {
                    result += HTMLHelper.HTMLEncode(tag) + ",";
                }
            }
            result = result.Trim().TrimEnd(',');
        }

        return result;
    }
}
