using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.CMSHelper;
using CMS.DataEngine;
using CMS.GlobalHelper;
using CMS.SiteProvider;
using CMS.UIControls;
using CMS.SettingsProvider;

// Edited object
[EditedObject(SiteObjectType.SEARCHENGINE, "engineId")]

// Breadcrumbs
[Breadcrumbs(2)]
[Breadcrumb(0, "searchengine.searchengine.list", "~/CMSModules/SearchEngine/Pages/Tools/SearchEngine/List.aspx", null)]
[Breadcrumb(1, Text = "{%EditedObject.DisplayName%}", ExistingObject = true)]
[Breadcrumb(1, ResourceString = "searchengine.searchengine.new", NewObject = true)]

// Title
[Title(ImageUrl = "Objects/CMS_SearchEngine/object.png", ResourceString = "searchengine.searchengine.edit", HelpTopic = "searchengine_edit", ExistingObject = true)]
[Title(ImageUrl = "Objects/CMS_SearchEngine/new.png", ResourceString = "searchengine.searchengine.new", HelpTopic = "searchengine_edit", NewObject = true)]

public partial class CMSModules_SearchEngine_Pages_Tools_SearchEngine_Edit : SiteManagerPage
{
    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    #endregion
}
