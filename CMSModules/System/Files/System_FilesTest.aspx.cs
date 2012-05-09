using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Security.Principal;

using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.UIControls;
using CMS.IO;
using CMS.SiteProvider;
using CMS.SettingsProvider;
using CMS.Controls;
using CMS.ExtendedControls;
using CMS.CMSImportExport;

public partial class CMSModules_System_Files_System_FilesTest : SiteManagerPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Setup the controls
        lblInfo.Text = GetString("System_Files.TestInfo");
        btnTest.Text = GetString("System_Files.TestButton");
    }


    #region "Test files methods"

    /// <summary>
    /// On btnTest click.
    /// </summary>
    protected void btnTest_Click(object sender, EventArgs e)
    {
        // Path to 'TestFiles' directory
        string path = ImportExportHelper.GetSiteUtilsFolder() + "TestFiles\\";
        DirectoryInfo di = DirectoryInfo.New(path);

        if (di.Exists)
        {
            CreateDeleteTest(path, di);
            // Modify file
            ModifyTest(path);
        }
        else
        {
            ltlInfo.Text += "Directory " + path + " wasn't found!";
        }
    }


    /// <summary>
    /// Create&Delete directory/file test.
    /// </summary>
    /// <param name="path">Path where to create subdirectory</param>
    /// <param name="di">Parent directory info</param>
    protected void CreateDeleteTest(string path, DirectoryInfo di)
    {
        FileInfo fi = null;
        DirectoryInfo sdi = null;

        string strCreating = GetString("System_Files.Creating");
        string strDeleting = GetString("System_Files.Deleting");
        string strOK = GetString("General.OK");
        string strFailed = GetString("System_Files.Failed");
        string targetPath = ImportExportHelper.GetSiteUtilsFolderRelativePath();
        DateTime dt = DateTime.Now;
        // Create subdirectory name
        string subdir = "TestFolder" + dt.Year.ToString() + dt.Month.ToString() + dt.Day.ToString() +
            dt.Hour.ToString() + dt.Minute.ToString() + dt.Second.ToString();

        // Path is full path
        if (targetPath == null)
        {
            targetPath = ImportExportHelper.GetSiteUtilsFolder() + DirectoryHelper.CombinePath("TestFiles", subdir) + "\\";
        }
        else
        {
            targetPath = targetPath + "TestFiles/" + subdir + "/";
        }


        ltlInfo.Text += strCreating + " '" + targetPath + "' - ";
        try
        {
            // Create subdirectory
            sdi = di.CreateSubdirectory(subdir);
            ltlInfo.Text += strOK;
        }
        catch
        {
            ltlInfo.Text += strFailed;
        }

        // Create test file name
        string fileName = "TestFile" + dt.Year.ToString() + dt.Month.ToString() + dt.Day.ToString() +
            dt.Hour.ToString() + dt.Minute.ToString() + dt.Second.ToString() + ".txt";

        ltlInfo.Text += "<br /><br />" + strCreating + " '" + targetPath + fileName + "' - ";
        try
        {
            // Create test file
            fi = FileInfo.New(path + subdir + "/" + fileName);
            using (StreamWriter sw = fi.CreateText())
            {
                sw.WriteLine("Edited - " + dt.ToString());
            }
            ltlInfo.Text += strOK;
        }
        catch
        {
            ltlInfo.Text += strFailed;
        }

        ltlInfo.Text += "<br /><br />" + strDeleting + " '" + targetPath + fileName + "' - ";
        try
        {
            // Delete test file
            fi.Delete();
            ltlInfo.Text += strOK;
        }
        catch
        {
            ltlInfo.Text += strFailed;
        }


        ltlInfo.Text += "<br /><br />" + strDeleting + " '" + targetPath + "' - ";
        try
        {
            // Delete subdirectory
            sdi.Delete();
            ltlInfo.Text += strOK;
        }
        catch
        {
            ltlInfo.Text += strFailed;
        }
    }


    /// <summary>
    /// Testing '~/CMSSiteUtils/TestFiles/TestModify.txt' modification.
    /// </summary>
    protected void ModifyTest(string path)
    {
        string filePath = path + "TestModify.txt";
        string strOK = GetString("General.OK");
        string strFailed = GetString("System_Files.Failed");
        string targetPath = ImportExportHelper.GetSiteUtilsFolderRelativePath();
        
        // Path is full path
        if (targetPath == null)
        {
            targetPath = ImportExportHelper.GetSiteUtilsFolder() + "TestFiles\\";
        }
        else
        {
            targetPath = targetPath + "TestFiles/";
        }
        ltlInfo.Text += "<br /><br />" + GetString("System_Files.Modifying") + " '" + targetPath + "TestModify.txt' - ";


        FileInfo fi = FileInfo.New(filePath);
        // If the file exists, try to append some text
        if (fi.Exists)
        {
            try
            {
                using (StreamWriter sw = fi.CreateText())
                {
                    sw.WriteLine(ltlInfo.Text.Replace("<br />", "\n") + strOK);
                }
                ltlInfo.Text += strOK;
            }
            catch
            {
                ltlInfo.Text += strFailed;
            }
        }
        else
        {
            ltlInfo.Text += strFailed;
        }
    }

    #endregion
}
