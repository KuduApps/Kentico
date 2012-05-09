using System;
using System.Data;
using System.Web;
using System.Web.UI;

using CMS.SiteProvider;
using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.SettingsProvider;
using CMS.ExtendedControls;
using CMS.IO;
using CMS.PortalEngine;

public partial class CMSModules_PortalEngine_UI_WebContainers_Container_Edit_Theme : SiteManagerPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Setup the filesystem browser
        int containerId = QueryHelper.GetInteger("containerid", 0);
        if (containerId > 0)
        {
            WebPartContainerInfo ci = WebPartContainerInfoProvider.GetWebPartContainerInfo(containerId);
            EditedObject = ci;

            if (ci != null)
            {
                // Ensure the theme folder
                themeElem.Path = "~/App_Themes/Components/Containers/" + ci.ContainerName;
            }
        }
        else
        {
            EditedObject = null;
        }
    }
}
