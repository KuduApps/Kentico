using System;
using System.Collections;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.GlobalHelper;
using CMS.UIControls;

public partial class CMSAdminControls_UI_UniMenu_NewSite_NewSiteMenu : CMSUserControl
{
    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        const int bigButtonMinimalWidth = 40;
        const int smallButtonMinimalWidth = 65;

        string[,] bigButtons = new string[2, 9];
        bigButtons[0, 0] = GetString("general.new");
        bigButtons[0, 1] = GetString("documents.newtooltip");
        bigButtons[0, 2] = "BigButton";
        bigButtons[0, 3] = "if (!NewItem()) return;";
        bigButtons[0, 4] = null;
        bigButtons[0, 5] = GetImageUrl("CMSModules/CMS_Content/Menu/New.png");
        bigButtons[0, 6] = GetString("documents.newtooltip");
        bigButtons[0, 7] = ImageAlign.Top.ToString();
        bigButtons[0, 8] = bigButtonMinimalWidth.ToString();

        bigButtons[1, 0] = GetString("general.delete");
        bigButtons[1, 1] = GetString("documents.deletetooltip");
        bigButtons[1, 2] = "BigButton";
        bigButtons[1, 3] = "if(!DeleteItem()) return;";
        bigButtons[1, 4] = null;
        bigButtons[1, 5] = GetImageUrl("CMSModules/CMS_Content/Menu/Delete.png");
        bigButtons[1, 6] = GetString("documents.deletetooltip");
        bigButtons[1, 7] = ImageAlign.Top.ToString();
        bigButtons[1, 8] = bigButtonMinimalWidth.ToString();

        buttonsBig.Buttons = bigButtons;

        string[,] smallButtons = new string[2, 9];

        const int firstIndex = 0;
        const int secondIndex = 1;

        smallButtons[firstIndex, 0] = GetString("general.up");
        smallButtons[firstIndex, 1] = GetString("documents.uptooltip");
        smallButtons[firstIndex, 2] = "SmallButton";
        smallButtons[firstIndex, 3] = "if(!MoveUp()) return;";
        smallButtons[firstIndex, 4] = null;
        smallButtons[firstIndex, 5] = GetImageUrl("CMSModules/CMS_Content/Menu/Up.png");
        smallButtons[firstIndex, 6] = GetString("documents.uptooltip");
        smallButtons[firstIndex, 7] = ImageAlign.AbsMiddle.ToString();
        smallButtons[firstIndex, 8] = smallButtonMinimalWidth.ToString();

        smallButtons[secondIndex, 0] = GetString("general.down");
        smallButtons[secondIndex, 1] = GetString("documents.downtooltip");
        smallButtons[secondIndex, 2] = "SmallButton";
        smallButtons[secondIndex, 3] = "if(!MoveDown()) return;";
        smallButtons[secondIndex, 4] = null;
        smallButtons[secondIndex, 5] = GetImageUrl("CMSModules/CMS_Content/Menu/Down.png");
        smallButtons[secondIndex, 6] = GetString("documents.downtooltip");
        smallButtons[secondIndex, 7] = ImageAlign.AbsMiddle.ToString();
        smallButtons[secondIndex, 8] = smallButtonMinimalWidth.ToString();

        buttonsSmall.Buttons = smallButtons;
    }

    #endregion
}
