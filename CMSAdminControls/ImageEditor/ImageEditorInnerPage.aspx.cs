using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.UIControls;
using CMS.GlobalHelper;

public partial class CMSAdminControls_ImageEditor_ImageEditorInnerPage : LivePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!QueryHelper.ValidateHash("hash"))
        {
            URLHelper.Redirect(ResolveUrl("~/CMSMessages/Error.aspx?title=" + ResHelper.GetString("imageeditor.badhashtitle") + "&text=" + ResHelper.GetString("imageeditor.badhashtext")));
        }
        else
        {
            ScriptHelper.RegisterJQueryCrop(Page);
            ScriptHelper.RegisterScriptFile(Page, "~/CMSAdminControls/ImageEditor/ImageEditorInnerPage.js");

            string imgUrl = QueryHelper.GetString("imgurl", null);
            if (String.IsNullOrEmpty(imgUrl))
            {
                string url = URLHelper.ResolveUrl("~/CMSAdminControls/ImageEditor/GetImageVersion.aspx" + URLHelper.Url.Query);
                url = URLHelper.RemoveParameterFromUrl(url, "hash");
                url = URLHelper.AddParameterToUrl(url, "hash", QueryHelper.GetHash(url));

                imgContent.ImageUrl = url;

                int imgwidth = QueryHelper.GetInteger("imgwidth", 0);
                int imgheight = QueryHelper.GetInteger("imgheight", 0);
                if ((imgwidth > 0) && (imgheight > 0))
                {
                    imgContent.Width = imgwidth;
                    imgContent.Height = imgheight;
                }
            }
            else
            {
                imgContent.ImageUrl = imgUrl;
            }
        }
    }
}
