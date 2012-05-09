using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;

using CMS.GlobalHelper;
using CMS.SiteProvider;
using CMS.PortalEngine;
using CMS.UIControls;
using CMS.IO;

public partial class CMSModules_PortalEngine_UI_WebParts_Development_WebPart_Clone : CMSModalSiteManagerPage
{
    int webPartId = 0;


    /// <summary>
    /// Page load.
    /// </summary>
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Title = "Web part clone";

        LoadResources();

        txtWebPartFileName.ReadOnly = false;
        txtWebPartFileName.Enabled = true;

        if (!RequestHelper.IsPostBack())
        {
            FillDropDownList(0, 0);
        }

        // Get the webpart ID
        webPartId = ValidationHelper.GetInteger(Request.QueryString["webpartID"], 0);
        if (webPartId > 0)
        {
            // Select webpart category on dropdown list
            WebPartInfo wi = WebPartInfoProvider.GetWebPartInfo(webPartId);

            if (wi != null)
            {
                // Find unique webpart name
                string newWebpartName = wi.WebPartName;
                string newWebpartDisplayName = wi.WebPartDisplayName;
                while (WebPartInfoProvider.GetWebPartInfo(newWebpartName) != null)
                {
                    newWebpartDisplayName = Increment(newWebpartDisplayName, "(", ")");
                    newWebpartName = Increment(newWebpartName, "_", "");
                }

                string webPartFileName = wi.WebPartFileName;

                // Careful with inherited web parts                
                if (wi.WebPartParentID > 0)
                {
                    WebPartInfo wparent = WebPartInfoProvider.GetWebPartInfo(wi.WebPartParentID);
                    if (wparent != null)
                    {
                        // Cannot copy file of webpart which has not own one
                        plcFile.Visible = false;
                        chckCloneWebPartFiles.Checked = false;

                        txtWebPartFileName.ReadOnly = true;
                        txtWebPartFileName.Enabled = false;
                        lblWebPartFileName.Text = GetString("Development-WebPart_Clone.ParentWebPart");                        
                    }
                }

                if (!RequestHelper.IsPostBack())
                {
                    drpWebPartCategories.SelectedValue = wi.WebPartCategoryID.ToString();
                    txtWebPartDisplayName.Text = newWebpartDisplayName;
                    txtWebPartName.Text = newWebpartName;
                    txtWebPartFileName.Text = webPartFileName;
                }
            }
        }
    }


    /// <summary>
    /// Load resources.
    /// </summary>
    private void LoadResources()
    {
        // Init page title
        this.CurrentMaster.Title.TitleText = GetString("Development-WebPart_Clone.Title");
        this.CurrentMaster.Title.TitleImage = GetImageUrl("Objects/CMS_WebPart/object.png");
        this.CurrentMaster.Title.HelpTopicName = "clone_web_part";
        this.CurrentMaster.Title.HelpName = "helpTopic";

        // Init GUI
        lbWebPartCategory.Text = GetString("Development-WebPart_Clone.WebPartCategory");
        lbWebPartCategory.Text = GetString("Development-WebPart_Clone.WebPartCategory");

        btnOk.Text = GetString("General.OK");
        btnCancel.Text = GetString("General.Cancel");
        btnCancel.OnClientClick = "window.close(); return false;";

        rfvWebPartDisplayName.ErrorMessage = GetString("general.requiresdisplayname");
        rfvWebPartFileName.ErrorMessage = GetString("Development-WebPart_Clone.ErrorFileName");
        rfvWebPartName.ErrorMessage = GetString("general.requirescodename");
        rfvWebPartCategory.ErrorMessage = GetString("Development-WebPart_Clone.ErrorCategory");
        chckCloneWebPartFiles.Text = GetString("Development-WebPart_Clone.CloneWebPartFiles");
    }


    /// <summary>
    /// Button OK click handler.
    /// </summary>
    protected void btnOK_Click(object sender, EventArgs e)
    {
        // Trim text values
        txtWebPartName.Text = txtWebPartName.Text.Trim();
        txtWebPartDisplayName.Text = txtWebPartDisplayName.Text.Trim();
        txtWebPartFileName.Text = txtWebPartFileName.Text.Trim();

        // Validate the text box fields
        string errorMessage = new Validator()
            .NotEmpty(txtWebPartName.Text, rfvWebPartName.ErrorMessage)
            .NotEmpty(txtWebPartDisplayName.Text, rfvWebPartDisplayName.ErrorMessage)            
            .IsCodeName(txtWebPartName.Text, GetString("WebPart_Clone.InvalidCodeName"))            
            .Result;
        
        // Validate file name
        if(string.IsNullOrEmpty(errorMessage) && chckCloneWebPartFiles.Checked)
        {
            errorMessage = new Validator()
            .NotEmpty(txtWebPartFileName.Text, rfvWebPartFileName.ErrorMessage)
            .IsFileName(Path.GetFileName(txtWebPartFileName.Text.Trim('~')), GetString("WebPart_Clone.InvalidFileName")).Result;
        }

        // Check if webpart with same name exists
        if (WebPartInfoProvider.GetWebPartInfo(txtWebPartName.Text) != null)
        {
            errorMessage = GetString(String.Format("Development-WebPart_Clone.WebPartExists", txtWebPartName.Text));
        }

        // Check if webpart is not cloned to the root category
        WebPartCategoryInfo wci = WebPartCategoryInfoProvider.GetWebPartCategoryInfoByCodeName("/");
        if (wci.CategoryID == ValidationHelper.GetInteger(drpWebPartCategories.SelectedValue, -1))
        {
            errorMessage = GetString("Development-WebPart_Clone.cannotclonetoroot");
        }

        if (errorMessage != "")
        {
            lblError.Text = errorMessage;
            lblError.Visible = true;
            return;
        }

        // get web part info object
        WebPartInfo wi = WebPartInfoProvider.GetWebPartInfo(webPartId);
        if (wi == null)
        {
            lblError.Text = GetString("WebPart_Clone.InvalidWebPartID");
            lblError.Visible = true;
            return;
        }

        // Create new webpart with all properties from source webpart
        WebPartInfo nwpi = new WebPartInfo(wi, false);

        nwpi.WebPartID = 0;
        nwpi.WebPartGUID = Guid.NewGuid();

        // Modify clone info
        nwpi.WebPartName = txtWebPartName.Text;
        nwpi.WebPartDisplayName = txtWebPartDisplayName.Text;
        nwpi.WebPartCategoryID = ValidationHelper.GetInteger(drpWebPartCategories.SelectedValue, -1);

        if (nwpi.WebPartParentID <= 0)
        {
            nwpi.WebPartFileName = txtWebPartFileName.Text;
        }

        string path = String.Empty;
        string filename = String.Empty;
        string inher = String.Empty;

        try
        {
            // Copy file if needed and webpart is not ihnerited
            if (chckCloneWebPartFiles.Checked && (wi.WebPartParentID == 0))
            {
                // Get source file path
                string srcFile = GetWebPartPhysicalPath(wi.WebPartFileName);

                // Get destination file path
                string dstFile = GetWebPartPhysicalPath(nwpi.WebPartFileName);

                // Ensure directory
                DirectoryHelper.EnsureDiskPath(Path.GetDirectoryName(DirectoryHelper.EnsurePathBackSlash(dstFile)), URLHelper.WebApplicationPhysicalPath);

                // Check if source and target file path are different
                if (File.Exists(dstFile))
                {
                    throw new Exception(GetString("general.fileexists"));
                }

                // Get file name
                filename = Path.GetFileName(dstFile);
                // Get path to the partial class name replace
                string wpPath = nwpi.WebPartFileName;

                if (!wpPath.StartsWith("~/"))
                {
                    wpPath = WebPartInfoProvider.WebPartsDirectory + "/" + wpPath.TrimStart('/');
                }
                path = Path.GetDirectoryName(wpPath);

                inher = path.Replace('\\', '_').Replace('/', '_') + "_" + Path.GetFileNameWithoutExtension(filename).Replace('.', '_');
                inher = inher.Trim('~');
                inher = inher.Trim('_');

                // Read .aspx file, replace classname and save as new file
                string text = File.ReadAllText(srcFile);
                File.WriteAllText(dstFile, ReplaceASCX(text, DirectoryHelper.CombinePath(path, filename), inher));

                // Read .aspx file, replace classname and save as new file
                if (File.Exists(srcFile + ".cs"))
                {
                    text = File.ReadAllText(srcFile + ".cs");
                    File.WriteAllText(dstFile + ".cs", ReplaceASCXCS(text, inher));
                }

                if (File.Exists(srcFile + ".vb"))
                {
                    text = File.ReadAllText(srcFile + ".vb");
                    File.WriteAllText(dstFile + ".vb", ReplaceASCXVB(text, inher));
                }

                // Copy web part subfolder
                string srcDirectory = srcFile.Remove(srcFile.Length - Path.GetFileName(srcFile).Length) + Path.GetFileNameWithoutExtension(srcFile) + "_files";
                if (Directory.Exists(srcDirectory))
                {
                    string dstDirectory = dstFile.Remove(dstFile.Length - Path.GetFileName(dstFile).Length) + Path.GetFileNameWithoutExtension(dstFile) + "_files";
                    if (srcDirectory.ToLower() != dstDirectory.ToLower())
                    {
                        DirectoryHelper.EnsureDiskPath(srcDirectory, URLHelper.WebApplicationPhysicalPath);
                        DirectoryHelper.CopyDirectory(srcDirectory, dstDirectory);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
            lblError.Visible = true;
            return;
        }

        // Add new web part to database
        WebPartInfoProvider.SetWebPartInfo(nwpi);
        
        try
        {
            // Get and duplicate all webpart layouts associated to webpart
            DataSet ds = WebPartLayoutInfoProvider.GetWebPartLayouts(webPartId);
            if (!DataHelper.DataSourceIsEmpty(ds))
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    WebPartLayoutInfo wpli = new WebPartLayoutInfo(dr);
                    wpli.WebPartLayoutID = 0;                          // Create new record
                    wpli.WebPartLayoutWebPartID = nwpi.WebPartID;        // Associate layout to new webpart
                    wpli.WebPartLayoutGUID = Guid.NewGuid();
                    wpli.WebPartLayoutCheckedOutByUserID = 0;
                    wpli.WebPartLayoutCheckedOutFilename = "";
                    wpli.WebPartLayoutCheckedOutMachineName = "";

                    // Replace classname and inherits attribut
                    wpli.WebPartLayoutCode = ReplaceASCX(wpli.WebPartLayoutCode, DirectoryHelper.CombinePath(path, filename), inher);
                    WebPartLayoutInfoProvider.SetWebPartLayoutInfo(wpli);
                }
            }

            // Duplicate associated thumbnail
            MetaFileInfoProvider.CopyMetaFiles(webPartId, nwpi.WebPartID, PortalObjectType.WEBPART, MetaFileInfoProvider.OBJECT_CATEGORY_THUMBNAIL, null);
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
            lblError.Visible = true;
            return;
        }

        // Close clone window
        // Refresh web part tree and select/edit new widget
        string script = String.Empty;
        string refreshLink = URLHelper.ResolveUrl("~/CMSModules/PortalEngine/UI/WebParts/Development/WebPart_Tree.aspx?webpartid=" + nwpi.WebPartID + "&reload=true");
        if (QueryHelper.Contains("reloadAll"))
        {
            script += "wopener.parent.parent.frames['webparttree'].location.href ='" + refreshLink + "';";
        }
        else
        {
            script = "wopener.location = '" + refreshLink + "';";
        }
        script += "window.close();";

        ltlScript.Text = ScriptHelper.GetScript(script);
    }


    /// <summary>
    /// Fills existing category names in drop down list, recursive.
    /// </summary>
    /// <param name="shift">Sub-category offset in drop down list</param>
    /// <param name="parentCategoryID">ID of parent category</param>
    protected void FillDropDownList(int shift, int parentCategoryID)
    {
        if (parentCategoryID == 0)
        {
            shift = 0;
        }
        else
        {
            shift++;
        }

        DataSet categories = WebPartCategoryInfoProvider.GetCategories(parentCategoryID);

        if ((categories != null) && (categories.Tables.Count > 0) && (categories.Tables[0].Rows.Count > 0))
        {
            foreach (DataRow dr in categories.Tables[0].Rows)
            {
                ListItem category = new ListItem();
                category.Text = string.Empty;
                for (int i = 0; i < shift; i++)
                {
                    category.Text += "\xA0\xA0\xA0";
                }

                category.Text += dr.ItemArray[1].ToString();
                category.Value = dr.ItemArray[0].ToString();
                drpWebPartCategories.Items.Add(category);

                FillDropDownList(shift, ValidationHelper.GetInteger(dr.ItemArray[0], 0));
            }
        }
    }


    /// <summary>
    /// Increment counter at the end of string.
    /// </summary>
    /// <param name="s">String</param>
    /// <param name="lpar">Left parathenses</param>
    /// <param name="rpar">Right parathenses</param>
    string Increment(string s, string lpar, string rpar)
    {
        int i = 1;
        s = s.Trim();
        if ((rpar == String.Empty) || s.EndsWith(rpar))
        {
            int leftpar = s.LastIndexOf(lpar);
            if (lpar == rpar)
            {
                leftpar = s.LastIndexOf(lpar, leftpar - 1);
            }

            if (leftpar >= 0)
            {
                i = ValidationHelper.GetSafeInteger(s.Substring(leftpar + lpar.Length, s.Length - leftpar - lpar.Length - rpar.Length), 0);
                if (i > 0) // Remove parathenses only if parathenses found
                {
                    s = s.Remove(leftpar);
                }
                i++;
            }
        }

        s += lpar + i + rpar;
        return s;
    }


    /// <summary>
    /// Replaces 'CodeFile' and 'Inherits' parameters in .asxc file.
    /// </summary>
    /// <param name="text">Ascx file</param>
    /// <param name="fname">New code file</param>
    /// <param name="inher">New inherits</param>
    /// <returns>New ascx file</returns>
    public string ReplaceASCX(string text, string fname, string inher)
    {
        if (fname != null)
        {
            fname = fname.Replace("\\", "/");
        }

        // CodeFile
        string re1 = "(.*CodeFile\\s*=\\s*\")(.*?)(\".*)";
        Regex reg1 = RegexHelper.GetRegex(re1, RegexOptions.Multiline);
        if (reg1.IsMatch(text))
        {
            text = reg1.Replace(text, "$1" + fname + ".cs$3", 1);
        }

        // Codebehind
        re1 = "(.*Codebehind\\s*=\\s*\")(.*?)(\".*)";
        reg1 = RegexHelper.GetRegex(re1, RegexOptions.Multiline);
        if (reg1.IsMatch(text))
        {
            text = reg1.Replace(text, "$1" + fname + ".cs$3", 1);
        }

        // Inherits
        string re2 = "(.*Inherits\\s*=\\s*\")(.*?)(\".*)";
        Regex reg2 = RegexHelper.GetRegex(re2, RegexOptions.Multiline);
        if (reg2.IsMatch(text))
        {
            text = reg2.Replace(text, "$1" + inher + "$3", 1);
        }

        return text;
    }


    /// <summary>
    /// Replaces class name in .ascx.cs file.
    /// </summary>
    /// <param name="text">Ascx.cs file</param>
    /// <param name="classname">New class name</param>
    /// <returns>New ascx.cs file</returns>
    public string ReplaceASCXCS(string text, string classname)
    {
        // Correct class name
        string re = "public(?<firstpart>.*)class(?<secondpart>.*):";
        if (Regex.IsMatch(text, re))
        {
            text = Regex.Replace(text, re, "public${firstpart}class " + classname + ":");
        }

        // Correct constructor name
        //public CMSWebParts_Text_EditableImage12() 
        re = "public\\s\\S*\\(";
        if (Regex.IsMatch(text, re))
        {
            text = Regex.Replace(text, re, "public " + classname + "(");
        }

        return text;
    }


    /// <summary>
    /// Replaces class name in .ascx.vb file.
    /// </summary>
    /// <param name="text">Ascx.vb file</param>
    /// <param name="classname">New class name</param>
    /// <returns>New ascx.cs file</returns>
    public string ReplaceASCXVB(string text, string classname)
    {
        // Correct class name
        string re1 = "(.*Class)(.*?)(\n)*(.*?Inherits.*)";
        Regex reg1 = RegexHelper.GetRegex(re1, RegexOptions.Multiline);
        if (reg1.IsMatch(text))
        {
            text = reg1.Replace(text, "$1 " + classname + " $3$4", 1);
        }

        return text;
    }


    private string GetWebPartPhysicalPath(string webpartPath)
    {
        webpartPath = webpartPath.Trim();

        if (webpartPath.StartsWith("~/"))
        {
            return Server.MapPath(webpartPath);
        }

        string fileName = webpartPath.Trim('/').Replace('/', '\\');
        return Path.Combine(Server.MapPath(WebPartInfoProvider.WebPartsDirectory), fileName);
    }
}
