using System;

using CMS.UIControls;

/// <summary>
/// Represents a Search dialog window.
/// </summary>
public partial class CMSAdminControls_CodeMirror_dialogs_EditSource : CMSModalPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        CurrentMaster.Title.TitleText = GetString("general.editsource");        
        CurrentMaster.Title.TitleImage = ResolveUrl(GetImageUrl("/General/Code.png"));        
    }
}