using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.UIControls;
using CMS.CMSHelper;
using CMS.GlobalHelper;

public partial class CMSModules_REST_FormControls_GenerateHash : SiteManagerPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        CurrentMaster.Title.TitleText = GetString("rest.generateauthhash");
        CurrentMaster.Title.TitleImage = GetImageUrl("CMSModules/CMS_REST/GenerateAuthHash.png");

        this.btnCancel.Text = GetString("general.close");
        this.btnAuthenticate.Text = GetString("rest.authenticate");
        this.btnAuthenticate.Click += new EventHandler(btnAuthenticate_Click);
    }


    protected void btnAuthenticate_Click(object sender, EventArgs e)
    {
        StringBuilder result = new StringBuilder();
        string[] urls = this.txtUrls.Text.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
        string domain = "";
        this.txtUrls.Text = "";

        foreach (string url in urls)
        {
            string urlWithoutHash = URLHelper.RemoveParameterFromUrl(url, "hash");
            string newUrl = HttpUtility.UrlDecode(urlWithoutHash);
            string query = URLHelper.GetQuery(newUrl).TrimStart('?');

            int index = newUrl.IndexOf("/rest");
            if (index >= 0)
            {
                // Extract the domain
                domain = URLHelper.GetDomain(newUrl);

                // Separate the query
                newUrl = URLHelper.RemoveQuery(newUrl.Substring(index));

                // Rewrite the URL to physical URL
                string[] rewritten = CMS.RESTService.RESTService.RewriteRESTUrl(newUrl, query, domain);
                newUrl = rewritten[0].TrimStart('~') + "?" + rewritten[1];

                // Get the hash from real URL
                this.txtUrls.Text += URLHelper.AddParameterToUrl(urlWithoutHash, "hash", CMS.RESTService.RESTService.GetHashForURL(newUrl, domain)) + Environment.NewLine;
            }
            else
            {
                this.txtUrls.Text += url + Environment.NewLine;
            }
        }
    }
}
