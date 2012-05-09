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
using CMS.CMSHelper;
using CMS.FormEngine;
using CMS.UIControls;
using CMS.IO;

public partial class CMSModules_BizForms_CMSPages_GetBizFormFile : GetFilePage
{
    /// <summary>
    /// GetFilePage forces to implement AllowCache property.
    /// </summary>
    public override bool AllowCache
    {
        get { return false; }
        set { }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        // Check 'ReadData' permission
        if (CurrentUser != null && CurrentUser.IsAuthorizedPerResource("cms.form", "ReadData"))
        {
            // Get file name from querystring.
            string fileName = QueryHelper.GetString("filename", String.Empty);
            string siteName = QueryHelper.GetString("sitename", CurrentSiteName);

            if ((ValidationHelper.IsFileName(fileName)) && (siteName != null))
            {
                // Get physical path to the file.
                string filePath = FormHelper.GetFilePhysicalPath(siteName, fileName);

                if (File.Exists(filePath))
                {
                    // Clear response.
                    CookieHelper.ClearResponseCookies();
                    Response.Clear();

                    // Prepare response.
                    string extension = Path.GetExtension(filePath);
                    Response.ContentType = MimeTypeHelper.GetMimetype(extension);

                    // Set the file disposition
                    SetDisposition(fileName, extension);

                    // Get file binary from file system.
                    WriteFile(filePath);

                    CompleteRequest();
                    //RequestHelper.EndResponse();
                }
            }
        }
    }
}
