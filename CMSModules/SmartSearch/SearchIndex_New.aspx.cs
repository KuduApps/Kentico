using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using CMS.UIControls;
using CMS.GlobalHelper;

public partial class CMSModules_SmartSearch_SearchIndex_New : SiteManagerPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Init breadcrumbs - new item breadcrumb
        string[,] breadcrumbs = new string[2, 3];
        breadcrumbs[0, 0] = GetString("srch.index.list");
        breadcrumbs[0, 1] = "~/CMSModules/SmartSearch/SearchIndex_List.aspx";
        breadcrumbs[0, 2] = "";
        breadcrumbs[1, 0] = GetString("srch.index.newindex");
        breadcrumbs[1, 1] = "";
        breadcrumbs[1, 2] = "";

        // New item title
        this.CurrentMaster.Title.TitleText = GetString("srch.index.newtitle");
        this.CurrentMaster.Title.TitleImage = GetImageUrl("Objects/CMS_SearchIndex/new.png");
        this.CurrentMaster.Title.HelpTopicName = "new_index";
        this.CurrentMaster.Title.Breadcrumbs = breadcrumbs;

        IndexNew.OnSaved += new EventHandler(IndexNew_OnSaved);
    }


    /// <summary>
    /// OnSave event handler.
    /// </summary>
    void IndexNew_OnSaved(object sender, EventArgs e)
    {
        URLHelper.Redirect("SearchIndex_Frameset.aspx?indexid=" + IndexNew.ItemID + "&saved=1");
    }
}
