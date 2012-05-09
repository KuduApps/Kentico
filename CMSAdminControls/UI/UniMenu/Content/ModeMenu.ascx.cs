using System;
using System.Text;
using System.Web.UI.WebControls;

using CMS.GlobalHelper;
using CMS.UIControls;

public partial class CMSAdminControls_UI_UniMenu_Content_ModeMenu : CMSUserControl
{
    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Set mode script
        StringBuilder setMode = new StringBuilder();
        setMode.AppendLine("function SetModeIcon(mode) {");
        setMode.AppendLine("    var index = 0;");
        setMode.AppendLine("    if (mode == 'edit')");
        setMode.AppendLine("    {");
        setMode.AppendLine("        index = 0;");
        setMode.AppendLine("    }");
        setMode.AppendLine("    else if (mode == 'preview')");
        setMode.AppendLine("    {");
        setMode.AppendLine("        index = 1;");
        setMode.AppendLine("    }");
        setMode.AppendLine("    else if (mode == 'livesite')");
        setMode.AppendLine("    {");
        setMode.AppendLine("        index = 2;");
        setMode.AppendLine("    }");
        setMode.AppendLine("    else if (mode == 'listing')");
        setMode.AppendLine("    {");
        setMode.AppendLine("        index = 3;");
        setMode.AppendLine("    }");
        setMode.AppendLine("    SelectButtonIndex_" + buttonsBig.ClientID + "(index);");
        setMode.AppendLine("    return true;");
        setMode.AppendLine("}");
        ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "setModeScript", ScriptHelper.GetScript(setMode.ToString()));

        const int modeButtonMinimalWidth = 52;

        string[,] bigButtons = new string[4, 9];
        bigButtons[0, 0] = GetString("general.edit");
        bigButtons[0, 1] = GetString("mode.edittooltip");
        bigButtons[0, 2] = "BigButton";
        bigButtons[0, 3] = "if(!SetMode('edit')) return;";
        bigButtons[0, 4] = null;
        bigButtons[0, 5] = GetImageUrl("CMSModules/CMS_Content/Menu/Edit.png");
        bigButtons[0, 6] = GetString("mode.edittooltip");
        bigButtons[0, 7] = ImageAlign.Top.ToString();
        bigButtons[0, 8] = modeButtonMinimalWidth.ToString();

        bigButtons[1, 0] = GetString("general.preview");
        bigButtons[1, 1] = GetString("mode.previewtooltip");
        bigButtons[1, 2] = "BigButton";
        bigButtons[1, 3] = "if(!SetMode('preview')) return;";
        bigButtons[1, 4] = null;
        bigButtons[1, 5] = GetImageUrl("CMSModules/CMS_Content/Menu/Preview.png");
        bigButtons[1, 6] = GetString("mode.previewtooltip");
        bigButtons[1, 7] = ImageAlign.Top.ToString();
        bigButtons[1, 8] = modeButtonMinimalWidth.ToString();

        bigButtons[2, 0] = GetString("contentmenu.iconlive");
        bigButtons[2, 1] = GetString("mode.livetooltip");
        bigButtons[2, 2] = "BigButton";
        bigButtons[2, 3] = "if(!SetMode('livesite')) return;";
        bigButtons[2, 4] = null;
        bigButtons[2, 5] = GetImageUrl("CMSModules/CMS_Content/Menu/Live.png");
        bigButtons[2, 6] = GetString("mode.livetooltip");
        bigButtons[2, 7] = ImageAlign.Top.ToString();
        bigButtons[2, 8] = modeButtonMinimalWidth.ToString();

        bigButtons[3, 0] = GetString("contentmenu.listing");
        bigButtons[3, 1] = GetString("mode.listingtooltip");
        bigButtons[3, 2] = "BigButton";
        bigButtons[3, 3] = "if(!SetMode('listing')) return;";
        bigButtons[3, 4] = null;
        bigButtons[3, 5] = GetImageUrl("CMSModules/CMS_Content/Menu/Listing.png");
        bigButtons[3, 6] = GetString("mode.listingtooltip");
        bigButtons[3, 7] = ImageAlign.Top.ToString();
        bigButtons[3, 8] = modeButtonMinimalWidth.ToString();

        buttonsBig.Buttons = bigButtons;
    }

    #endregion
}
