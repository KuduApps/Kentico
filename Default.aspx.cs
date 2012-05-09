using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using CMS.UIControls;

public partial class _Default : TemplatePage
{
    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        this.lblText.Text = "The web site doesn't contain any content. Sign in to <a href=\"" + ResolveUrl("~/cmsdesk/default.aspx") + "\">CMS Desk</a> and edit the content.";
        this.ltlTags.Text = this.HeaderTags;
    }
}
