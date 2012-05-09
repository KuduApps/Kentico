using System;

using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.ExtendedControls;

/// <summary>
/// Dialog page that extends LargeTextArea form control and provides syntax highlihgting and macros support.
/// </summary>
public partial class CMSFormControls_Selectors_LargeTextAreaDesigner : MessagePage
{
    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        txtText.Editor.Language = LanguageEnum.HTMLMixed;

        // Set window title and image
        CurrentMaster.Title.TitleText = GetString("EditingFormControl.TitleText");
        CurrentMaster.Title.TitleImage = GetImageUrl("Design/Controls/EditingFormControl/title.png");

        // Set macro options using the querystring argument
        bool allowMacros = QueryHelper.GetBoolean("allowMacros", true);
        if (allowMacros)
        {
            macroSelectorElem.Resolver = EmailTemplateMacros.EmailTemplateResolver;
            macroSelectorElem.ExtendedTextAreaElem = txtText.Editor.EditorID;
            macroSelectorElem.TextAreaID = txtText.Editor.ClientID;
            macroSelectorElem.TopOffset = 35;
        }
        else
        {
            macroSelectorElem.Visible = false;
        }

        // Register macro scripts
        RegisterModalPageScripts();
        RegisterEscScript();
    }

    /// <summary>
    /// Disables handler base tag to fix App_Theme issues.
    /// </summary>
    protected override void OnInit(EventArgs e)
    {
        UseBaseTagForHandlerPage = false;
        base.OnInit(e);
    }

    #endregion
}