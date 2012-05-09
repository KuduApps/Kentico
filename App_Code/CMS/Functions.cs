using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Globalization;

using CMS.DataEngine;
using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.TreeEngine;
using CMS.SettingsProvider;
using CMS.SiteProvider;
using CMS.EventLog;
using CMS.ExtendedControls;
using CMS.URLRewritingEngine;
using CMS.PortalEngine;
using CMS.Synchronization;
using CMS.LicenseProvider;
using CMS.WorkflowEngine;
using CMS.WebFarmSync;
using CMS.FormEngine;

using TreeNode = CMS.TreeEngine.TreeNode;

/// <summary>
/// Common methods.
/// </summary>
public static class Functions
{
    /// <summary>
    /// Returns connection string used throughout the application.
    /// </summary>
    public static string GetConnectionString()
    {
        return SqlHelperClass.GetSqlConnectionString();
    }


    /// <summary>
    /// Creates and returns a new, initialized instance of TreeProvider with current user set.
    /// </summary>
    public static TreeProvider GetTreeProvider()
    {
        TreeProvider tree = new TreeProvider(CMSContext.CurrentUser);
        return tree;
    }


    /// <summary>
    /// Returns User ID of the current user.
    /// </summary>
    public static int GetUserID()
    {
        return CMSContext.CurrentUser.UserID;
    }


    /// <summary>
    /// Returns true if the current user is authorized to access the given resource (module) with required permission.
    /// </summary>
    /// <param name="resourceName">Resource name</param>
    /// <param name="permissionName">Permission name</param>
    public static bool IsAuthorizedPerResource(string resourceName, string permissionName)
    {
        return CMSContext.CurrentUser.IsAuthorizedPerResource(resourceName, permissionName);
    }


    /// <summary>
    /// Returns true if the current user is authorized to access the given class (document type) with required permission.
    /// </summary>
    /// <param name="className">Class name in format application.class</param>    
    /// <param name="permissionName">Name of the required permission</param>    
    public static bool IsAuthorizedPerClass(string className, string permissionName)
    {
        return CMSContext.CurrentUser.IsAuthorizedPerClassName(className, permissionName);
    }


    /// <summary>
    /// Redirects user to the "Access Denied" page.
    /// </summary>
    /// <param name="resourceName">Name of the resource that cannot be accessed</param>
    /// <param name="permissionName">Name of the permission that is not allowed</param>
    public static void RedirectToAccessDenied(string resourceName, string permissionName)
    {
        if (HttpContext.Current != null)
        {
            URLHelper.Redirect("~/CMSDesk/accessdenied.aspx?resource=" + resourceName + "&permission=" + permissionName);
        }
    }


    /// <summary>
    /// Returns preferred UI culture of the current user.
    /// </summary>
    public static System.Globalization.CultureInfo GetPreferredUICulture()
    {
        return CultureHelper.PreferredUICultureInfo;
    }


    /// <summary>
    /// Returns current alias path based on base alias path setting and "aliaspath" querystring parameter.
    /// </summary>
    public static string GetAliasPath()
    {
        return CMSContext.CurrentPageInfo.NodeAliasPath;
    }


    /// <summary>
    /// Returns preferred culture code (as string). You can modify this function so that it determines the preferred culture using some other algorithm.
    /// </summary>
    public static string GetPreferredCulture()
    {
        return CMSContext.PreferredCultureCode;
    }


    /// <summary>
    /// Returns type (such as "cms.article") of the current document.
    /// </summary>
    public static string GetDocumentType()
    {
        return CMSContext.CurrentPageInfo.ClassName;
    }


    /// <summary>
    /// Returns type (such as "cms.article") of the specified document.
    /// </summary>
    /// <param name="aliasPath">Alias path of the document</param>
    public static string GetDocumentType(string aliasPath)
    {
        TreeProvider tree = new TreeProvider(CMSContext.CurrentUser);
        TreeNode node = tree.SelectSingleNode(CMSContext.CurrentSiteName, aliasPath, CMSContext.PreferredCultureCode);
        if (node != null)
        {
            return node.NodeClassName;
        }
        else
        {
            return null;
        }
    }


    /// <summary>
    /// Returns node representing the current page.
    /// </summary>
    public static TreeNode GetCurrentPage()
    {
        return CMSContext.CurrentDocument;
    }


    /// <summary>
    /// Returns node representing the current document.
    /// </summary>
    public static TreeNode GetCurrentDocument()
    {
        return CMSContext.CurrentDocument;
    }


    /// <summary>
    /// Returns true if the current user is member of the given role.
    /// </summary>
    /// <param name="roleName">Role name</param>
    public static bool IsInRole(string roleName)
    {
        return CMSContext.CurrentUser.IsInRole(roleName, CMSContext.CurrentSiteName);
    }


    /// <summary>
    /// Writes event to the event log.
    /// </summary>
    /// <param name="eventType">Type of the event. I = information, E = error, W = warning</param>
    /// <param name="source">Source of the event (Content, Administration, etc.)</param>
    /// <param name="eventCode">Event code (Security, Update, Delete, etc.)</param>
    /// <param name="nodeId">ID value of the document</param>
    /// <param name="nodeNamePath">NamePath value of the document</param>
    /// <param name="eventDescription">Detailed description of the event</param>
    public static void LogEvent(string eventType, string source, string eventCode, int nodeId, string nodeNamePath, string eventDescription)
    {
        int siteId = 0;
        if (CMSContext.CurrentSite != null)
        {
            siteId = CMSContext.CurrentSite.SiteID;
        }
        EventLogProvider log = new EventLogProvider();
        log.LogEvent(eventType, DateTime.Now, source, eventCode, CMSContext.CurrentUser.UserID, HTTPHelper.GetUserName(), nodeId, nodeNamePath, HTTPHelper.UserHostAddress, eventDescription, siteId, HTTPHelper.GetAbsoluteUri());
    }


    /// <summary>
    /// Returns first N levels of the given alias path, N+1 if CMSWebSiteBaseAliasPath is set.
    /// </summary>
    /// <param name="aliasPath">Alias path</param>
    /// <param name="level">Number of levels to be returned</param>
    public static string GetPathLevel(string aliasPath, int level)
    {
        return TreePathUtils.GetPathLevel(aliasPath, level);
    }


    /// <summary>
    /// Encodes URL(just redirection for use with aspx code.
    /// </summary>
    /// <param name="url">URL to encode</param>
    public static string UrlPathEncode(object url)
    {
        string path = null;
        if (!(url == null))
        {
            path = System.Convert.ToString(url);
        }
        else
        {
            path = "";
        }
        if (HttpContext.Current != null)
        {
            return HttpContext.Current.Server.UrlPathEncode(path);
        }
        return "";
    }


    /// <summary>
    /// Returns the text of the specified region.
    /// </summary>
    /// <param name="aliasPath">Aliaspath of the region MenuItem</param>
    /// <param name="regionID">Region ID to get the text from</param>
    public static string GetEditableRegionText(string aliasPath, string regionID)
    {
        try
        {
            TreeProvider tree = new TreeProvider(CMSContext.CurrentUser);
            TreeNode node = tree.SelectSingleNode(CMSContext.CurrentSiteName, aliasPath, CMSContext.PreferredCultureCode);
            if (node != null)
            {
                PageInfo pi = new PageInfo();
                pi.LoadContentXml(Convert.ToString(node.GetValue("DocumentContent")));
                return Convert.ToString(pi.EditableRegions[regionID.ToLower()]);
            }
        }
        catch
        {
        }
        return null;
    }


    /// <summary>
    /// Returns the text of the specified region.
    /// </summary>
    /// <param name="aliasPath">Aliaspath of the region MenuItem</param>
    /// <param name="regionID">Region ID to get the text from</param>
    /// <param name="maxLength">Maximum text length</param>
    public static string GetEditableRegionText(string aliasPath, string regionID, int maxlength)
    {
        string text = GetEditableRegionText(aliasPath, regionID);
        if ((text != null) && (text != ""))
        {
            if (text.Length > maxlength)
            {
                int lastSpace = text.LastIndexOf(" ", maxlength - 4);
                if (lastSpace < maxlength / 2)
                {
                    lastSpace = maxlength - 1;
                }
                int lastStartTag = text.LastIndexOf("<", lastSpace);
                int lastEndTag = text.LastIndexOf(">", lastSpace);
                if (lastStartTag < lastSpace && lastEndTag < lastStartTag)
                {
                    lastSpace = lastStartTag;
                }
                text = text.Substring(0, lastSpace).Trim() + " ...";
            }
        }
        return text;
    }


    /// <summary>
    /// Resolves the inline control macros within the parent controls collection and loads the dynamic controls instead.
    /// </summary>
    /// <param name="parent">Parent control of the control tree to resolve</param>
    public static void ResolveDynamicControls(Control parent)
    {
        ControlsHelper.ResolveDynamicControls(parent);
    }


    /// <summary>
    /// Returns true if given path is excluded from URL rewriting.
    /// </summary>
    /// <param name="requestPath">Path to be checked</param>
    public static bool IsExcluded(string requestPath)
    {
        string customExcludedPaths = "";
        // Get Custom excluded Urls path
        if (CMSContext.CurrentSite != null && CMSContext.CurrentSiteName != null && SettingsKeyProvider.GetStringValue(CMSContext.CurrentSiteName + ".CMSExcludedURLs") != null)
        {
            customExcludedPaths = SettingsKeyProvider.GetStringValue(CMSContext.CurrentSiteName + ".CMSExcludedURLs");
        }

        return URLHelper.IsExcluded(requestPath, customExcludedPaths);
    }


    /// <summary>
    /// Returns the virtual path to the CMSDesk root directory.
    /// </summary>
    public static string GetCMSDeskPath()
    {
        return "~/CMSDesk";
    }


    /// <summary>
    /// Returns URL of the document specified by alias path.
    /// </summary>
    /// <param name="aliasPath">Alias path of the document</param>
    public static string GetUrl(object aliasPath)
    {
        return CMSContext.GetUrl(Convert.ToString(aliasPath));
    }


    /// <summary>
    /// Returns URL of the document specified by alias path or URL path.
    /// </summary>
    /// <param name="aliasPath">Alias path of the document</param>
    /// <param name="urlPath">Url path of the document</param>
    public static string GetUrl(object aliasPath, object urlPath)
    {
        return CMSContext.GetUrl(Convert.ToString(aliasPath), Convert.ToString(urlPath));
    }


    /// <summary>
    /// Returns formatted username in format: username. 
    /// Allows you to customize how the usernames will look like throughout the admin UI. 
    /// </summary>
    /// <param name="username">Source user name</param>    
    public static string GetFormattedUserName(string username)
    {
        return GetFormattedUserName(username, null, false);
    }


    /// <summary>
    /// Returns formatted username in format: username. 
    /// Allows you to customize how the usernames will look like throughout the admin UI. 
    /// </summary>
    /// <param name="username">Source user name</param>   
    /// <param name="isLiveSite">Indicates if returned username should be displayed on live site</param>
    public static string GetFormattedUserName(string username, bool isLiveSite)
    {
        return GetFormattedUserName(username, null, isLiveSite);
    }


    /// <summary>
    /// Returns formatted username in format: fullname (username). 
    /// Allows you to customize how the usernames will look like throughout the admin UI. 
    /// </summary>
    /// <param name="username">Source user name</param>  
    /// <param name="fullname">Source full name</param>
    public static string GetFormattedUserName(string username, string fullname)
    {
        return GetFormattedUserName(username, fullname, false);
    }


    /// <summary>
    /// Returns formatted username in format: fullname (username). 
    /// Allows you to customize how the usernames will look like throughout the admin UI. 
    /// </summary>
    /// <param name="username">Source user name</param>
    /// <param name="fullname">Source full name</param>
    /// <param name="isLiveSite">Indicates if returned username should be displayed on live site</param>
    public static string GetFormattedUserName(string username, string fullname, bool isLiveSite)
    {
        username = UserInfoProvider.TrimSitePrefix(username);

        if (!String.IsNullOrEmpty(DataHelper.GetNotEmpty(fullname, "").Trim()))
        {
            return String.Format("{0} ({1})", fullname, username);
        }
        else
        {
            return username;
        }
    }


    /// <summary>
    /// Returns formatted username in format: fullname (nickname) if nicname specified otherwise fullname (username). 
    /// Allows you to customize how the usernames will look like throughout the friends and messaging modules UI. 
    /// </summary>
    /// <param name="username">Source user name</param>
    /// <param name="fullname">Source full name</param>
    /// <param name="nickname">Source nick name</param>
    public static string GetFormattedUserName(string username, string fullname, string nickname)
    {
        return GetFormattedUserName(username, fullname, nickname, false);
    }


    /// <summary>
    /// Returns formatted username in format: fullname (nickname) if nicname specified otherwise fullname (username). 
    /// Allows you to customize how the usernames will look like throughout the friends and messaging modules UI. 
    /// </summary>
    /// <param name="username">Source user name</param>
    /// <param name="fullname">Source full name</param>
    /// <param name="nickname">Source nick name</param>
    /// <param name="isLiveSite">Indicates if returned username should be displayed on live site</param>
    public static string GetFormattedUserName(string username, string fullname, string nickname, bool isLiveSite)
    {
        nickname = nickname.Trim();
        username = username.Trim();
        string name = ((string.IsNullOrEmpty(nickname)) || (nickname == username)) ? username : nickname;
        return GetFormattedUserName(name, fullname, isLiveSite);
    }


    /// <summary>
    /// Clear all hashtables.
    /// </summary>
    public static void ClearHashtables()
    {
        CMSObjectHelper.ClearHashtables();
    }
}
