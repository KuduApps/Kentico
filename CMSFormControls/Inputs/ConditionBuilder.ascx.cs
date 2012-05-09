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

using CMS.GlobalHelper;
using CMS.FormControls;
using CMS.CMSHelper;

public partial class CMSFormControls_Inputs_ConditionBuilder : FormEngineUserControl
{
    #region "Properties"

    /// <summary>
    /// Gets or sets the enabled state of the control.
    /// </summary>
    public override bool Enabled
    {
        get
        {
            return base.Enabled;
        }
        set
        {
            base.Enabled = value;
            this.txtMacro.Editor.Enabled = value;
        }
    }


    /// <summary>
    /// Gets or sets field value.
    /// </summary>
    public override object Value
    {
        get
        {
            string val = MacroResolver.RemoveDataMacroBrackets(txtMacro.Text.Trim());
            if (!string.IsNullOrEmpty(val))
            {
                if (!MacroResolver.AllowOnlySimpleMacros)
                {
                    val = MacroResolver.AddMacroSecurityParams(val, CMSContext.CurrentUser.UserName);
                }
                return "{%" + val + "%}";
            }
            return "";
        }
        set
        {
            string val = MacroResolver.RemoveDataMacroBrackets(ValidationHelper.GetString(value, ""));
            string userName = null;
            val = MacroResolver.RemoveMacroSecurityParams(val, out userName);
            txtMacro.Text = MacroResolver.RemoveDataMacroBrackets(val.Trim());
        }
    }


    /// <summary>
    /// Gets or sets macro resolver of the macro editor.
    /// </summary>
    public MacroResolver Resolver
    {
        get
        {
            return txtMacro.Resolver;
        }
        set
        {
            txtMacro.Resolver = value;
        }
    }


    /// <summary>
    /// If true, auto completion is shown above the editor, otherwise it is below (default position is below).
    /// </summary>
    public bool ShowAutoCompletionAbove
    {
        get
        {
            return ValidationHelper.GetBoolean(GetValue("ShowAutoCompletionAbove"), false);
        }
        set
        {
            SetValue("ShowAutoCompletionAbove", value);
            this.txtMacro.ShowAutoCompletionAbove = value;
        }
    }


    /// <summary>
    /// Gets ClientID of the textbox with emailinput.
    /// </summary>
    public override string ValueElementID
    {
        get
        {
            return txtMacro.ClientID;
        }
    }

    #endregion


    /// <summary>
    /// Page load.
    /// </summary>
    protected void Page_Load(object sender, EventArgs e)
    {
        // Set control style and css class
        if (!string.IsNullOrEmpty(this.ControlStyle))
        {
            txtMacro.Editor.Attributes.Add("style", this.ControlStyle);
        }
        if (!string.IsNullOrEmpty(this.CssClass))
        {
            txtMacro.Editor.CssClass = this.CssClass;
        }

        this.txtMacro.ShowAutoCompletionAbove = this.ShowAutoCompletionAbove;
        this.txtMacro.Editor.UseSmallFonts = true;
        this.txtMacro.Editor.Height = new Unit("50px");
        this.txtMacro.MixedMode = false;
        this.txtMacro.Editor.ShowToolbar = false;
        this.txtMacro.Editor.ShowLineNumbers = false;
        ContextResolver resolver = (ContextResolver)GetValue("Resolver");
        if (resolver != null)
        {
            txtMacro.Resolver = resolver;
        }

        ScriptHelper.RegisterClientScriptBlock(this.Page, typeof(string), "InsertMacroCondition", "function InsertMacroCondition(text) {" + this.txtMacro.Editor.EditorID + ".setValue(text);}", true);
        ScriptHelper.RegisterDialogScript(this.Page);

        btnEdit.ImageUrl = GetImageUrl("Design/Controls/UniGrid/Actions/Edit.png");
        btnEdit.OnClientClick = "modalDialog('" + CMSContext.ResolveDialogUrl("~/CMSFormControls/Inputs/ConditionBuilder.aspx") + "?condition=' + encodeURIComponent(" + this.txtMacro.Editor.EditorID + ".getValue()) , 'editmacrocondition', 900, 700); return false;";
    }
}
