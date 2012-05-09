using System;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;

using CMS.UIControls;
using CMS.GlobalHelper;
using CMS.FormControls;
using CMS.CMSHelper;
using CMS.ExtendedControls;

public partial class CMSAdminControls_UI_Macros_MacroTreeEditor : FormEngineUserControl
{
    #region "Variables"

    private bool mShowMacroTreeAbove = false;

    #endregion


    #region "Properties"

    /// <summary>
    /// Gets client ID of the editor.
    /// </summary>
    public override string ValueElementID
    {
        get
        {
            return this.editorElem.Editor.ClientID;
        }
    }


    /// <summary>
    /// Gets or sets text in the editor.
    /// </summary>
    public override string Text
    {
        get
        {
            return this.editorElem.Editor.Text;
        }
        set
        {
            this.editorElem.Editor.Text = value;
        }
    }


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
            this.editorElem.Editor.Enabled = value;
            this.btnShowTree.Enabled = false;
        }
    }


    /// <summary>
    /// Gets or sets context resolver used for getting autocomplete options.
    /// </summary>
    public MacroResolver Resolver
    {
        get
        {
            return this.editorElem.Resolver;
        }
        set
        {
            this.editorElem.Resolver = value;
        }
    }


    /// <summary>
    /// Gets the ExtendedTextArea control.
    /// </summary>
    public ExtendedTextArea Editor
    {
        get
        {
            return this.editorElem.Editor;
        }
    }


    /// <summary>
    /// If true, tree is shown above the editor, otherwise it is below (default position is below).
    /// </summary>
    public bool ShowMacroTreeAbove
    {
        get
        {
            return mShowMacroTreeAbove;
        }
        set
        {
            mShowMacroTreeAbove = value;
        }
    }


    /// <summary>
    /// Gets or sets the left offset of the autocomplete control (to position it correctly).
    /// </summary>
    public int LeftOffset
    {
        get
        {
            return this.editorElem.LeftOffset;
        }
        set
        {
            this.editorElem.LeftOffset = value;
        }
    }


    /// <summary>
    /// Gets or sets the top offset of the autocomplete control (to position it correctly).
    /// </summary>
    public int TopOffset
    {
        get
        {
            return this.editorElem.TopOffset;
        }
        set
        {
            this.editorElem.TopOffset = value;
        }
    }


    /// <summary>
    /// Gets the name of java script object of the auto completion extender.
    /// </summary>
    public string AutoCompletionObject
    {
        get
        {
            return this.editorElem.AutoCompletionObject;
        }
    }


    /// <summary>
    /// Indicates wheter the control is on live site or not
    /// </summary>
    public override bool IsLiveSite
    {
        get
        {
            return base.IsLiveSite;
        }
        set
        {
            base.IsLiveSite = value;
            this.treeElem.IsLiveSite = value;
            this.editorElem.IsLiveSite = value;
        }
    }

    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        editorElem.Editor.ShowToolbar = false;
        editorElem.Editor.DynamicHeight = true;
        editorElem.ShowAutoCompletionAbove = this.ShowMacroTreeAbove;

        treeElem.OnNodeClickHandler = "nodeClick_" + this.ClientID;

        btnShowTree.ToolTip = GetString("macros.editor.showhidetree");
        btnShowTree.ImageUrl = GetImageUrl("General/Code.png");
        btnShowTree.OnClientClick = GetShowHideScript();

        pnlMacroTree.Style.Add("position", "absolute");
        pnlMacroTree.Style.Add("display", "none");
        pnlMacroTree.Attributes.Add("onmouseover", "macroTreeHasFocus = true;");
        pnlMacroTree.Attributes.Add("onmouseout", "macroTreeHasFocus = false;");
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        // Register the main script
        ScriptHelper.RegisterClientScriptInclude(this, typeof(string), "MacroTreeEditor.js", URLHelper.ResolveUrl("~/CMSScripts/MacroTreeEditor.js"));
    }


    protected override void Render(HtmlTextWriter writer)
    {
        base.Render(writer);

        // We need to generate this script on Render since extended area does that that late as well
        // and editor object is not available before
        string script = null;
        if (this.editorElem.Editor.SyntaxHighlightingEnabled)
        {
            script = "function nodeClick_" + this.ClientID + "(macro) { nodeClick(macro, " + this.editorElem.Editor.EditorID + ", '" + this.pnlMacroTree.ClientID + "', '" + this.editorElem.Editor.ClientID + "'); }";
        }
        else
        {
            script = "function nodeClick_" + this.ClientID + "(macro) { nodeClick(macro, null, '" + this.pnlMacroTree.ClientID + "', '" + this.editorElem.Editor.ClientID + "'); }";
        }

        ScriptHelper.RegisterStartupScript(this.Page, typeof(string), "MacroTreeEditorScript_" + this.ClientID, script, true);
    }


    private string GetShowHideScript()
    {
        if (this.editorElem.Editor.SyntaxHighlightingEnabled)
        {
            return "showHideMacroTree('" + this.pnlMacroTree.ClientID + "', " + this.editorElem.Editor.EditorID + ", " + this.editorElem.AutoCompletionObject + ", " + this.LeftOffset + ", " + this.TopOffset + ", " + (this.ShowMacroTreeAbove ? "true" : "false") + ", false); return false;";
        }
        else
        {
            return "showHideMacroTree('" + this.pnlMacroTree.ClientID + "', null, null, " + this.LeftOffset + ", " + this.TopOffset + ", " + (this.ShowMacroTreeAbove ? "true" : "false") + ", false); return false;";
        }
    }
}