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
using System.Web.Caching;

using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.SiteProvider;
using CMS.URLRewritingEngine;
using CMS.PortalEngine;
using CMS.DataEngine;
using CMS.UIControls;
using CMS.SettingsProvider;

public partial class CMSAdminControls_ImageEditor_GetImageVersion : GetFilePage
{
    #region "Variables"

    protected TempFileInfo tfi = null;

    #endregion


    #region "Properties"

    /// <summary>
    /// Returns false - do not allow cache.
    /// </summary>
    public override bool AllowCache
    {
        get
        {
            return false;
        }
        set
        {

        }
    }

    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        DebugHelper.SetContext("GetImageVersion");

        if (!QueryHelper.ValidateHash("hash"))
        {
            URLHelper.Redirect(ResolveUrl("~/CMSMessages/Error.aspx?title=" + ResHelper.GetString("imageeditor.badhashtitle") + "&text=" + ResHelper.GetString("imageeditor.badhashtext")));
        }

        // Get the parameters
        Guid editorGuid = QueryHelper.GetGuid("editorguid", Guid.Empty);
        int num = QueryHelper.GetInteger("versionnumber", -1);

        // Load the temp file info
        if (num >= 0)
        {
            tfi = TempFileInfoProvider.GetTempFileInfo(editorGuid, num);
        }
        else
        {
            DataSet ds = TempFileInfoProvider.GetTempFiles(null, "FileNumber DESC", 1, null);
            if (!DataHelper.DataSourceIsEmpty(ds))
            {
                tfi = new TempFileInfo(ds.Tables[0].Rows[0]);
            }
        }

        // Send the data
        SendFile();

        DebugHelper.ReleaseContext();
    }


    /// <summary>
    /// Sends the given file within response.
    /// </summary>
    /// <param name="file">File to send</param>
    protected void SendFile()
    {
        // Clear response.
        CookieHelper.ClearResponseCookies();
        Response.Clear();

        Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);

        if (tfi != null)
        {

            // Prepare etag
            string etag = "\"" + tfi.FileID + "\"";

            // Setup the mime type - Fix the special types
            string mimetype = tfi.FileMimeType;
            string extension = tfi.FileExtension;
            switch (extension.ToLower())
            {
                case ".flv":
                    mimetype = "video/x-flv";
                    break;
            }

            // Prepare response
            Response.ContentType = mimetype;
            SetDisposition(tfi.FileNumber.ToString(), extension);

            // Setup Etag property
            ETag = etag;

            // Set if resumable downloads should be supported
            AcceptRange = !IsExtensionExcludedFromRanges(extension);

            // Add the file data
            tfi.Generalized.EnsureBinaryData();
            WriteBytes(tfi.FileBinary);
        }
        else
        {
            NotFound();
        }

        CompleteRequest();
    }
}