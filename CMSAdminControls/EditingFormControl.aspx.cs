using System;
using System.Web.UI.WebControls;

using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.ExtendedControls;
using CMS.CMSHelper;

public partial class CMSAdminControls_EditingFormControl : CMSModalPage
{
    #region "Variables"

    protected string selectorId = "";
    protected string controlPanelId = "";
    protected string selectorPanelId = "";

    private static ContextResolver mWebpartResolver = null;

    #endregion


    #region "Properties"

    /// <summary>
    /// Default webpart resolver.
    /// </summary>
    public static ContextResolver WebpartResolver
    {
        get
        {
            if (mWebpartResolver == null)
            {
                // Init resolver
                mWebpartResolver = new ContextResolver();
            }

            return mWebpartResolver;
        }
    }

    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        // Get query string parameters
        selectorId = QueryHelper.GetText("selectorid", "");
        controlPanelId = QueryHelper.GetText("controlPanelId", "");
        selectorPanelId = QueryHelper.GetText("selectorpanelid", "");

        // Initialize UI
        CurrentMaster.Title.TitleText = GetString("EditingFormControl.TitleText");
        CurrentMaster.Title.TitleImage = GetImageUrl("Design/Controls/EditingFormControl/title.png");

        btnOk.Text = GetString("General.Save");
        btnRemove.Text = GetString("editingformcontrol.removemacros");

        btnOk.OnClientClick = "setValueToParent(" + ScriptHelper.GetString(selectorId) + ", " + ScriptHelper.GetString(controlPanelId) + ", " + ScriptHelper.GetString(selectorPanelId) + "); window.close(); return false;";
        btnRemove.OnClientClick = "removeMacro(" + ScriptHelper.GetString(selectorId) + ", " + ScriptHelper.GetString(controlPanelId) + ", " + ScriptHelper.GetString(selectorPanelId) + "); window.close(); return false;";

        btnCancel.Text = GetString("General.Cancel");

        ScriptHelper.RegisterClientScriptBlock(this.Page, typeof(string), "SetNestedControlValue", @"
function setValueToParent(selId, controlPanelId, selPanelId) {
    wopener.setNestedControlValue(selId, controlPanelId, trimNewLines(" + macroEditor.Editor.GetValueGetterCommand() + @"), selPanelId); 
}", true);

        macroEditor.TopOffset = 35;
        macroEditor.MixedMode = true;
        macroEditor.Editor.Language = LanguageEnum.HTMLMixed;
        macroEditor.Editor.Width = new Unit("97%");
        macroEditor.Editor.Height = new Unit("255px");

        macroSelector.Resolver = WebpartResolver;
        macroSelector.ExtendedTextAreaElem = macroEditor.Editor.EditorID;
        macroSelector.TextAreaID = macroEditor.Editor.ClientID;
        macroSelector.ShowMacroTreeAbove = true;
    }


    protected override void Render(System.Web.UI.HtmlTextWriter writer)
    {
        base.Render(writer);
        writer.Write(ScriptHelper.GetScript(macroEditor.Editor.GetValueSetterCommand("wopener.getNestedControlValue('" + selectorId + "')")));
    }
}
