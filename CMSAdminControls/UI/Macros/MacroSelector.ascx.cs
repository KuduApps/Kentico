using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using CMS.CMSHelper;
using CMS.GlobalHelper;
using CMS.UIControls;
using CMS.ExtendedControls;

public partial class CMSAdminControls_UI_Macros_MacroSelector : CMSUserControl
{
    #region "Variables"

    /// <summary>
    /// Macro resolver.
    /// </summary>
    protected MacroResolver mResolver = null;

    /// <summary>
    /// Javascript function called on submit.
    /// </summary>
    protected string mJavaScripFunction = "InsertMacro";

    /// <summary>
    /// CKEditor client ID.
    /// </summary>
    protected string mCKEditorID = null;

    /// <summary>
    /// Text area client ID.
    /// </summary>
    protected string mTextAreaID = null;

    /// <summary>
    /// Extended text area JS element object.
    /// </summary>
    protected string mExtendedTextAreaElem = null;
    #endregion


    #region "Properties"

    /// <summary>
    /// Macro resolver.
    /// </summary>
    public MacroResolver Resolver
    {
        get
        {
            return mResolver;
        }
        set
        {
            mResolver = value;
        }
    }


    /// <summary>
    /// Javascript function called on submit.
    /// </summary>
    public string JavaScripFunction
    {
        get
        {
            return mJavaScripFunction;
        }
        set
        {
            mJavaScripFunction = value;
        }
    }


    /// <summary>
    /// Gets or sets the left offset of the autocomplete control (to position it correctly).
    /// </summary>
    public int LeftOffset
    {
        get
        {
            return this.macroElem.LeftOffset;
        }
        set
        {
            this.macroElem.LeftOffset = value;
        }
    }


    /// <summary>
    /// Gets or sets the top offset of the autocomplete control (to position it correctly).
    /// </summary>
    public int TopOffset
    {
        get
        {
            return this.macroElem.TopOffset;
        }
        set
        {
            this.macroElem.TopOffset = value;
        }
    }


    /// <summary>
    /// If true, tree is shown above the editor, otherwise it is below (default position is below).
    /// </summary>
    public bool ShowMacroTreeAbove
    {
        get
        {
            return this.macroElem.ShowMacroTreeAbove;
        }
        set
        {
            this.macroElem.ShowMacroTreeAbove = value;
        }
    }


    /// <summary>
    /// Gets the ExtendedTextArea control.
    /// </summary>
    public ExtendedTextArea Editor
    {
        get
        {
            return this.macroElem.Editor;
        }
    }


    /// <summary>
    /// Gets or sets ID of the CKEditor. If this property is set, script for insertion is automatically registered.
    /// </summary>
    public string CKEditorID
    {
        get
        {
            return this.mCKEditorID;
        }
        set
        {
            this.mCKEditorID = value;
        }
    }


    /// <summary>
    /// Gets or sets element of the ExtendedTextArea. If this property is set, script for insertion is automatically registered.
    /// </summary>
    public string ExtendedTextAreaElem
    {
        get
        {
            return this.mExtendedTextAreaElem;
        }
        set
        {
            this.mExtendedTextAreaElem = value;
        }
    }


    /// <summary>
    /// Gets or sets ID of the TextArea. If this property is set, script for insertion is automatically registered.
    /// </summary>
    public string TextAreaID
    {
        get
        {
            return this.mTextAreaID;
        }
        set
        {
            this.mTextAreaID = value;
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
            this.macroElem.IsLiveSite = value;
        }
    }

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        if (mResolver == null)
        {
            lblError.Text = "[MacroSelector.ascx]: You need to define \"Resolver\" property for this control to work properly.";
            lblError.ForeColor = Color.Red;
            lblError.Visible = true;

            return;
        }

        // Register script for CKEditor
        if (!string.IsNullOrEmpty(this.CKEditorID))
        {
            ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "MacroInsertCKEditor_" + this.ClientID,
                ScriptHelper.GetScript("function InsertMacro_" + this.ClientID + "(macro) { var oEditor = CKEDITOR.instances['" + this.CKEditorID + "'] ; if (oEditor != null) { if (oEditor.mode == 'wysiwyg' ) { oEditor.insertHtml(macro); }  else { alert('" + GetString("macroselector.wysiwigerror") + "') } } } "));
        }
        else if (!string.IsNullOrEmpty(this.ExtendedTextAreaElem) || !string.IsNullOrEmpty(this.TextAreaID))
        {
            // Register scripts for text areas
            ScriptHelper.RegisterClientScriptInclude(this, typeof(string), "MacroSelector.js", URLHelper.ResolveUrl("~/CMSScripts/MacroSelector.js"));
            if (!string.IsNullOrEmpty(this.ExtendedTextAreaElem))
            {
                ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "MacroInsertExtendedTextArea_" + ClientID,
                    ScriptHelper.GetScript("function InsertMacro_" + this.ClientID + "(macro) { InsertMacroExtended(macro, (typeof(" + ExtendedTextAreaElem + ") != 'undefined' ? " + ExtendedTextAreaElem + " : null), '" + TextAreaID + "'); }"));
            }
            else
            {
                ScriptHelper.RegisterClientScriptBlock(this, typeof(string), "MacroInsertTextArea_" + this.ClientID,
                    ScriptHelper.GetScript("function InsertMacro_" + this.ClientID + "(macro) { InsertMacroPlain(macro, '" + this.TextAreaID + "'); }"));
            }
        }

        this.btnInsert.OnClientClick = GetInsertScript() + " return false;";
        this.macroElem.Resolver = this.Resolver;
    }


    /// <summary>
    /// Returns insert script for macro.
    /// </summary>
    private string GetInsertScript()
    {
        StringBuilder builder = new StringBuilder();

        // CKEditorID setting has higher priority, therefore override script name
        string scriptName = this.JavaScripFunction;
        if (!string.IsNullOrEmpty(CKEditorID) || !string.IsNullOrEmpty(ExtendedTextAreaElem) || !string.IsNullOrEmpty(TextAreaID))
        {
            scriptName = "InsertMacro_" + this.ClientID;
        }

        // Call javascript macro function
        builder.Append(
            "if (window." + scriptName + ") {",
            scriptName + "('{%' + " + macroElem.Editor.GetValueGetterCommand() + ".replace(/(\\r\\n|\\n|\\r)+/, '') + '%}');",
            "} else { alert('No insert function!'); } ");

        return builder.ToString();
    }

    #endregion
}
