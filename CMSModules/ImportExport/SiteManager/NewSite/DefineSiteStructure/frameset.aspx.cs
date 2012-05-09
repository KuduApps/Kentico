using System;

using CMS.ExtendedControls;
using CMS.GlobalHelper;
using CMS.UIControls;

public partial class CMSModules_ImportExport_SiteManager_NewSite_DefineSiteStructure_frameset : SiteManagerPage
{
    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
        frameMenu.Attributes.Add("src", "menu.aspx" + URLHelper.Url.Query);
        frameTree.Attributes.Add("src", "tree.aspx" + URLHelper.Url.Query);
        frameView.Attributes.Add("src", "main.aspx" + URLHelper.Url.Query);

        if (CultureHelper.IsUICultureRTL())
        {
            ControlsHelper.ReverseFrames(colsFrameset);
        }
    }

    #endregion
}
