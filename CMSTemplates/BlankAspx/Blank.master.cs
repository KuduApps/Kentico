using System;

using CMS.UIControls;

public partial class CMSTemplates_BlankASPX_Blank : TemplateMasterPage
{
    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        this.lblText.Text = "The web site doesn't contain any content. Sign in to <a href=\"" + ResolveUrl("~/cmsdesk/default.aspx") + "\">CMS Desk</a> and edit the content.";
        this.ltlTags.Text = HeaderTags;
    }
}
