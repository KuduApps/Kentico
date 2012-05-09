using System.Data.SqlClient;
using System.Xml;
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

using CMS.GlobalHelper;
using CMS.SettingsProvider;
using CMS.CMSHelper;
using CMS.DataEngine;
using CMS.IO;

/// <summary>
/// Installer methods.
/// </summary>
public static class InstallerFunctions
{
    /// <summary>
    /// Redirects user to the installation page if connectionString not set.
    /// </summary>
    /// <param name="forceRedirect">If true, the redirect is forced</param>
    public static bool InstallRedirect(bool forceRedirect)
    {
        // Check if the connection string is initialized
        if (!SqlHelperClass.IsConnectionStringInitialized || forceRedirect)
        {
            // Redirect only it not installer
            string currentPath = "";

            if (HttpContext.Current != null)
            {
                currentPath = HttpContext.Current.Request.Path.ToLower();
            }

            string relativePath = URLHelper.RemoveApplicationPath(currentPath);

            string currentFile = Path.GetFileName(relativePath);
            if (String.Compare(currentFile, "install.aspx", StringComparison.InvariantCultureIgnoreCase) == 0)
            {
                return true;
            }

            string fileExtension = Path.GetExtension(currentFile);
            if ((String.Compare(fileExtension, ".aspx", StringComparison.InvariantCultureIgnoreCase) == 0) || currentFile == String.Empty || currentFile == "/")
            {
                if (!IsInstallerExcluded(relativePath))
                {
                    if (HttpContext.Current != null)
                    {
                        URLHelper.Redirect("~/cmsinstall/install.aspx");
                    }
                }
            }
            return true;
        }
        else
        {
            return false;
        }
    }


    /// <summary>
    /// Returns true if the path is excluded for the installer process.
    /// </summary>
    /// <param name="path">Path to check</param>
    private static bool IsInstallerExcluded(string path)
    {
        if (path.StartsWith("/cmsmessages"))
        {
            return true;
        }

        return false;
    }


    /// <summary>
    /// Checks if all folders of the given path exist and if not, it creates them.
    /// </summary>
    /// <param name="path">Full disk path</param>
    public static void EnsureDiskPath(string path)
    {
        // Get the folder path
        string folderPath = Path.GetDirectoryName(path);

        // Create the directory
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }
    }
}

