using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text.RegularExpressions;
using System.Collections;

using CMS.FormControls;
using CMS.TreeEngine;
using CMS.GlobalHelper;
using CMS.CMSHelper;
using CMS.FormEngine;
using CMS.DataEngine;
using CMS.SiteProvider;
using CMS.ExtendedControls;
using CMS.Controls;
using CMS.CKEditor;

public partial class CMSFormControls_Basic_HtmlAreaControl : FormEngineUserControl
{
    #region "Properties"

    /// <summary>
    /// Gets or sets the enabled state of the control.
    /// </summary>
    public override bool Enabled
    {
        get
        {
            return editor.Enabled;
        }
        set
        {
            editor.Enabled = value;
        }
    }


    /// <summary>
    /// Gets or sets form control value.
    /// </summary>
    public override object Value
    {
        get
        {
            return editor.ResolvedValue;
        }
        set
        {
            editor.ResolvedValue = ValidationHelper.GetString(value, null);
        }
    }


    /// <summary>
    /// Toolbar used in editor.
    /// </summary>
    public string ToolbarSet
    {
        get
        {
            return editor.ToolbarSet;
        }
        set
        {
            editor.ToolbarSet = value;
        }
    }


    /// <summary>
    /// Gets current editor.
    /// </summary>
    public CMSHtmlEditor CurrentEditor
    {
        get
        {
            return editor;
        }
    }

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        // Set control properties
        editor.AutoDetectLanguage = false;
        editor.DefaultLanguage = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
        string toolbarSet = editor.ToolbarSet;
        if (this.Form != null)
        {
            editor.DialogParameters = this.Form.DialogParameters;
            // Get editor area toolbar 
            toolbarSet = DataHelper.GetNotEmpty(this.GetValue("toolbarset"), this.Form.HtmlAreaToolbar);
            editor.ToolbarSet = toolbarSet;
            editor.ToolbarLocation = DataHelper.GetNotEmpty(this.GetValue("toolbarlocation"), this.Form.HtmlAreaToolbarLocation);

            // Set form dimensions
            editor.Width = new Unit(ValidationHelper.GetInteger(this.GetValue("width"), 700));
            editor.Height = new Unit(ValidationHelper.GetInteger(this.GetValue("height"), 300));

            // Get editor area starting path
            editor.StartingPath = ValidationHelper.GetString(this.GetValue("startingpath"), "");

            // Get editor area css file
            string mCssStylesheet = ValidationHelper.GetString(this.GetValue("cssstylesheet"), "");
            if (!String.IsNullOrEmpty(mCssStylesheet))
            {
                editor.EditorAreaCSS = CSSHelper.GetStylesheetUrl(mCssStylesheet);
            }
            else if (toolbarSet.Equals("Wireframe", StringComparison.InvariantCultureIgnoreCase))
            {
                // Special case for wireframe editor
                editor.EditorAreaCSS = CSSHelper.GetCSSUrl("~/CMSAdminControls/CKeditor/skins/kentico/wireframe.css");
            }
            else if (CMSContext.CurrentSite != null)
            {
                editor.EditorAreaCSS = FormHelper.GetHtmlEditorAreaCss(CMSContext.CurrentSiteName);
            }
        }

        // Set live site info
        editor.IsLiveSite = this.IsLiveSite;

        // Set direction
        if (CultureHelper.IsPreferredCultureRTL())
        {
            editor.ContentsLangDirection = LanguageDirection.RightToLeft;
        }
        else
        {
            editor.ContentsLangDirection = LanguageDirection.LeftToRight;
        }

        // Get dialog configuration
        DialogConfiguration mediaConfig = GetDialogConfiguration(this.FieldInfo);
        if (mediaConfig != null)
        {
            // Set configuration for 'Insert image or media' dialog                        
            editor.MediaDialogConfig = mediaConfig;
            // Set configuration for 'Insert link' dialog        
            editor.LinkDialogConfig = mediaConfig.Clone();
            // Set configuration for 'Quickly insert image' dialog
            editor.QuickInsertConfig = mediaConfig.Clone();
        }

        // Set CSS settings
        if (!String.IsNullOrEmpty(this.ControlStyle))
        {
            editor.Attributes.Add("style", this.ControlStyle);
            this.ControlStyle = null;
        }
        if (!String.IsNullOrEmpty(this.CssClass))
        {
            editor.CssClass = this.CssClass;
            this.CssClass = null;
        }

        this.CheckRegularExpression = true;
        this.CheckFieldEmptiness = true;
    }


    /// <summary>
    /// Returns true if user control is valid.
    /// </summary>
    public override bool IsValid()
    {
        string plainText = editor.ResolvedValue;
        int maxControlSize = 0;

        // Check min/max text length
        if (this.GetValue("size") != null)
        {
            maxControlSize = ValidationHelper.GetInteger(this.GetValue("size"), 0);
        }
        if (this.FieldInfo != null)
        {
            if ((this.FieldInfo.MaxStringLength > -1) && ((maxControlSize > this.FieldInfo.MaxStringLength) || (maxControlSize == 0)))
            {
                maxControlSize = this.FieldInfo.MaxStringLength;
            }
        }
        if ((maxControlSize > 0) || ((this.FieldInfo != null) && (this.FieldInfo.MinStringLength > 0)))
        {
            plainText = Regex.Replace(plainText, @"(>)(\r|\n)*(<)", "><");
            plainText = Regex.Replace(plainText, "(<[^>]*>)([^<]*)", "$2");
            // Just substitute spec.chars with one letter
            plainText = Regex.Replace(plainText, "(&#x?[0-9]{2,4};|&quot;|&amp;|&nbsp;|&lt;|&gt;|&euro;|&copy;|&reg;|&permil;|&Dagger;|&dagger;|&lsaquo;|&rsaquo;|&bdquo;|&rdquo;|&ldquo;|&sbquo;|&rsquo;|&lsquo;|&mdash;|&ndash;|&rlm;|&lrm;|&zwj;|&zwnj;|&thinsp;|&emsp;|&ensp;|&tilde;|&circ;|&Yuml;|&scaron;|&Scaron;)", "@");

            string error = null;
            int minControlSize = 0;
            if (this.FieldInfo != null)
            {
                minControlSize = this.FieldInfo.MinStringLength;
            }
            bool valid = CheckLength(minControlSize, maxControlSize, plainText.Length, ref error, this.ErrorMessage);
            ValidationError = error;
            return valid;
        }
        return true;
    }


    /// <summary>
    /// Returns the arraylist of the field IDs (Client IDs of the inner controls) that should be spell checked.
    /// </summary>
    public override ArrayList GetSpellCheckFields()
    {
        ArrayList result = new ArrayList();
        result.Add(editor.ClientID);
        return result;
    }

    #endregion
}
