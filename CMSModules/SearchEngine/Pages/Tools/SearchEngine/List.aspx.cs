using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.SettingsProvider;

// Actions
[Actions(1)]
[Action(0, "Objects/CMS_SearchEngine/add.png", "searchengine.searchengine.new", "Edit.aspx")]

// Title
[Title("Objects/CMS_SearchEngine/object.png", "searchengine.searchengine.list", "searchengine_list")]

public partial class CMSModules_SearchEngine_Pages_Tools_SearchEngine_List : SiteManagerPage
{
    #region "Page events"

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    #endregion
}
