using System;

using CMS.UIControls;
using CMS.ExtendedControls;

/// <summary>
/// Represents a Search dialog window.
/// </summary>
public partial class CMSAdminControls_CodeMirror_dialogs_Search : CMSModalPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        CurrentMaster.Title.TitleText = GetString("general.search");        
        CurrentMaster.Title.TitleImage = ResolveUrl(GetImageUrl("/General/Find.png"));

        Form.DefaultButton = btnSearch.UniqueID;
        Form.DefaultFocus = btnSearch.UniqueID;
    }
}