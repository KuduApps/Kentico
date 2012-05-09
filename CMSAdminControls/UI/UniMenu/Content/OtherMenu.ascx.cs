using System;
using System.Web.UI.WebControls;

using CMS.GlobalHelper;
using CMS.UIControls;

public partial class CMSAdminControls_UI_UniMenu_Content_OtherMenu : CMSUserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string[,] searchButton = new string[1, 9];
        // Search button
        searchButton[0, 0] = GetString("contentmenu.search");
        searchButton[0, 1] = GetString("contentmenu.searchtooltip");
        searchButton[0, 2] = "BigButton";
        searchButton[0, 3] = "OpenSearch()";
        searchButton[0, 4] = null;
        searchButton[0, 5] = GetImageUrl("CMSModules/CMS_Content/Menu/search.png");
        searchButton[0, 6] = GetString("contentmenu.searchtooltip");
        searchButton[0, 7] = ImageAlign.Top.ToString();
        searchButton[0, 8] = "48";

        // Maximize button - maximize button is not supported
        //searchButton[1, 0] = GetString("contentmenu.maximize");
        //searchButton[1, 1] = GetString("contentmenu.maximizetooltip");
        //searchButton[1, 2] = "ModeButton";
        //searchButton[1, 3] = "FullScreen()";
        //searchButton[1, 4] = null;
        //searchButton[1, 5] = GetImageUrl("CMSModules/CMS_Content/Menu/FullScreen.png");
        //searchButton[1, 6] = null;
        //searchButton[1, 7] = null;

        buttons.Buttons = searchButton;
    }
}
