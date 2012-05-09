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
using System.Text;
using System.Text.RegularExpressions;

using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.SiteProvider;
using CMS.PortalEngine;
using CMS.UIControls;
using CMS.SettingsProvider;
using CMS.FormEngine;
using CMS.IO;

public partial class CMSModules_PortalEngine_UI_WebParts_Development_WebPart_Edit_Code : SiteManagerPage
{
    private int webpartId = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        webpartId = ValidationHelper.GetInteger(Request.QueryString["webpartid"], 0);

        GenerateCode();
    }

    /// <summary>
    /// Generates the web part code.
    /// </summary>
    protected void GenerateCode()
    {
        string templateFile = MapPath("~/App_Data/CodeTemplates/WebPart");
        string baseControl = txtBaseControl.Text.Trim();
        
        WebPartInfo wpi = WebPartInfoProvider.GetWebPartInfo(webpartId);
        if (wpi != null)
        {
            // Generate the ASCX
            string ascx = File.ReadAllText(templateFile + ".ascx.template");

            // Prepare the path
            string path = URLHelper.UnResolveUrl(WebPartInfoProvider.GetWebPartUrl(wpi), SettingsKeyProvider.ApplicationPath);
            
            // Prepare the class name
            string className = path.Trim('~', '/');
            if (className.EndsWith(".ascx"))
            {
                className = className.Substring(0, className.Length - 5);
            }
            className = className.Replace("/", "_");

            ascx = Regex.Replace(ascx, "(Inherits)=\"[^\"]+\"", "$1=\"" + className + "\"");
            ascx = Regex.Replace(ascx, "(CodeFile|CodeBehind)=\"[^\"]+\"", "$1=\"" + path + "\"");

            this.txtASCX.Text = ascx;

            // Generate the code
            string code = File.ReadAllText(templateFile + ".ascx.cs.template");

            code = Regex.Replace(code, "( class\\s+)[^\\s]+", "$1" + className);

            // Prepare the properties
            FormInfo fi = new FormInfo(wpi.WebPartProperties);
            
            StringBuilder sbInit = new StringBuilder();

            string propertiesCode = CodeGenerator.GetPropertiesCode(fi, true, baseControl, sbInit);
          
            // Replace in code
            code = code.Replace("// ##PROPERTIES##", propertiesCode);
            code = code.Replace("// ##SETUP##", sbInit.ToString());

            this.txtCS.Text = code;
        }
    }


    protected void btnGenerate_Click(object sender, EventArgs e)
    {
        GenerateCode();
    }
}
