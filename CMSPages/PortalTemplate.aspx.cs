using System;

using CMS.UIControls;

public partial class CMSPages_PortalTemplate : PortalPage
{
    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        // Init the header tags
        tags.Text = HeaderTags;
    }
}
