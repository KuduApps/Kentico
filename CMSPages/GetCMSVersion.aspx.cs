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

using CMS.CMSHelper;
using CMS.UIControls;
using CMS.GlobalHelper;

public partial class CMSPages_GetCMSVersion : CMSPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Show version only if right key is inserted
        string versionKey = QueryHelper.GetString("versionkey", string.Empty);
        if (EncryptionHelper.VerifyVersionRSA(versionKey))
        {
            Version v = CMSContext.GetCMSVersion();
            if (v != null)
            {
                // Write the version to the response
                Response.Clear();
                Response.Write(v.ToString(3));

                RequestHelper.EndResponse();
            }
        }
    }
}
