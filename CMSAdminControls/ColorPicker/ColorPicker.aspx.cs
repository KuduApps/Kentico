using System;

using CMS.GlobalHelper;
using CMS.UIControls;

public partial class CMSAdminControls_ColorPicker_ColorPicker : CMSModalPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Registers a colorpicker script file
        ScriptHelper.RegisterScriptFile(this, "~/CMSAdminControls/ColorPicker/colorpicker.js");        
    }
}