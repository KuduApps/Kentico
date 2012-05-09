using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.UIControls;
using CMS.IO;
using CMS.AmazonStorage;
using CMS.GlobalHelper;

public partial class CMSPages_GetAmazonFile : GetFilePage
{
    #region "Properties"

    /// <summary>
    /// Returns IS3ObjectInfoProvider instance.
    /// </summary>
    IS3ObjectInfoProvider Provider
    {
        get
        {
            return S3ObjectFactory.Provider;
        }
    }


    /// <summary>
    /// Caching is not allowed in case of Amazon storage (caching is done inside the provider).
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


    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        string hash = QueryHelper.GetString("hash", string.Empty);
        string path = QueryHelper.GetString("path", string.Empty);

        // Validate hash
        if (ValidationHelper.ValidateHash("?path=" + path, hash, false))
        {
            if (path.StartsWith("~"))
            {
                path = Server.MapPath(path);
            }

            // Get file content from Amazon S3
            IS3ObjectInfo obj = S3ObjectFactory.GetInfo(path);

            // Check if blob exists
            if (Provider.ObjectExists(obj))
            {
                Stream stream = Provider.GetObjectContent(obj);

                // Set right content type
                Response.ContentType = MimeTypeHelper.GetMimetype(Path.GetExtension(path));
                SetDisposition(Path.GetFileName(path), Path.GetExtension(path));

                // Send headers
                Response.Flush();

                Byte[] buffer = new Byte[DataHelper.BUFFER_SIZE];
                int bytesRead = stream.Read(buffer, 0, DataHelper.BUFFER_SIZE);

                // Copy data from blob stream to cache
                while (bytesRead > 0)
                {
                    // Write the data to the current output stream
                    Response.OutputStream.Write(buffer, 0, bytesRead);

                    // Flush the data to the output
                    Response.Flush();

                    // Read next part of data
                    bytesRead = stream.Read(buffer, 0, DataHelper.BUFFER_SIZE);
                }

                stream.Close();

                CompleteRequest();
            }
        }
        else
        {
            URLHelper.Redirect(ResolveUrl("~/CMSMessages/Error.aspx?title=" + ResHelper.GetString("general.badhashtitle") + "&text=" + ResHelper.GetString("general.badhashtext")));
        }
    }

    #endregion        
}
