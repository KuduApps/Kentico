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

using CMS.GlobalHelper;
using CMS.SiteProvider;
using CMS.DataEngine;
using CMS.SettingsProvider;
using CMS.UIControls;
using CMS.ExtendedControls;
using CMS.IO;

public partial class CMSModules_AdminControls_Controls_CSS_ThemeEditor : CMSUserControl
{
    /// <summary>
    /// Path to the folder that is edited
    /// </summary>
    public string Path
    {
        get;
        set;
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        // Setup the filesystem browser
        if (!String.IsNullOrEmpty(Path))
        {
            // Register scripts
            ScriptHelper.RegisterJQuery(this.Page);
            CMSDialogHelper.RegisterDialogHelper(this.Page);
            ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "InitResizers", "$j(InitResizers());", true);
            CSSHelper.RegisterCSSBlock(Page, ".TooltipImage{max-width:200px; max-height:200;}");

            string filePath = Server.MapPath(Path);
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            // Setup the browser
            FileSystemDialogConfiguration config = new FileSystemDialogConfiguration();
            config.StartingPath = Path;
            config.AllowedExtensions = "gif;png;bmp;jpg;jpeg;css;skin";
            config.ShowFolders = false;
            config.AllowManage = true;

            selFile.Config = config;
        }
    }
}
