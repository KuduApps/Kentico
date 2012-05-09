using System;

using CMS.GlobalHelper;
using CMS.UIControls;

public partial class CMSMessages_Redirect : MessagePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string url = QueryHelper.GetText("url", String.Empty);

        // Check for relative url or for hash
        if (url.StartsWith("~") || url.StartsWith("/") || QueryHelper.ValidateHash("hash"))
        {
            titleElem.TitleText = GetString("Redirect.Header");
            titleElem.TitleImage = GetImageUrl("Others/Messages/redirect.png");
            lblInfo.Text = GetString("Redirect.Info");  
          
            string target = QueryHelper.GetText("target", String.Empty);
            string frame = QueryHelper.GetText("frame", String.Empty);
            bool urlIsRelative = url.StartsWith("~/");

            string script = String.Empty;
            url = ResolveUrl(url);

            // Information about the target page
            lnkTarget.Text = url;
            lnkTarget.NavigateUrl = url;
            lnkTarget.Target = target;

            // Generate redirect script
            if (urlIsRelative && frame.Equals("top", StringComparison.InvariantCultureIgnoreCase))
            {
                script += "if (self.location != top.location) { top.location = " + ScriptHelper.GetString(url) + "; }";
            }
            else if ((target == String.Empty) && (url != String.Empty))
            {
                script += "if (IsCMSDesk()) { window.open(" + ScriptHelper.GetString(url) + "); } else { document.location = " + ScriptHelper.GetString(url) + "; }";
            }

            ltlScript.Text += ScriptHelper.GetScript(script);
        }
        else
        {
            URLHelper.Redirect(ResolveUrl("~/CMSMessages/Error.aspx?title=" + ResHelper.GetString("general.badhashtitle") + "&text=" + ResHelper.GetString("general.badhashtext")));
        }
    }
}
