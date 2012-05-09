using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;

using CMS.FormControls;
using CMS.GlobalHelper;
using CMS.ExtendedControls;

public partial class CMSFormControls_Basic_BBEditorControl : FormEngineUserControl
{
    #region "Properties"

    /// <summary>
    /// Gets or sets the enabled state of the control.
    /// </summary>
    public override bool Enabled
    {
        get
        {
            return bbEditor.Enabled;
        }
        set
        {
            bbEditor.Enabled = value;
        }
    }


    /// <summary>
    /// Gets or sets form control value.
    /// </summary>
    public override object Value
    {
        get
        {
            return bbEditor.TextArea.Text;

        }
        set
        {
            bbEditor.TextArea.Text = ValidationHelper.GetString(value, null);
        }
    }


    /// <summary>
    /// Indicates if control is placed on live site.
    /// </summary>
    public override bool IsLiveSite
    {
        get
        {
            return bbEditor.IsLiveSite;
        }
        set
        {
            bbEditor.IsLiveSite = value;
        }
    }

    #endregion


    #region "Methods"

    /// <summary>
    /// Page load.
    /// </summary>
    protected void Page_Load(object sender, EventArgs e)
    {
        // Initialize editor
        bbEditor.TextArea.Columns = ValidationHelper.GetInteger(this.GetValue("cols"), 40);
        bbEditor.TextArea.Rows = ValidationHelper.GetInteger(this.GetValue("rows"), 5);
        bbEditor.ShowURL = ValidationHelper.GetBoolean(this.GetValue("showurl"), true);
        bbEditor.ShowQuote = ValidationHelper.GetBoolean(this.GetValue("showquote"), true);
        bbEditor.ShowImage = ValidationHelper.GetBoolean(this.GetValue("showimage"), true);
        bbEditor.ShowBold = ValidationHelper.GetBoolean(this.GetValue("showbold"), true);
        bbEditor.ShowItalic = ValidationHelper.GetBoolean(this.GetValue("showitalic"), true);
        bbEditor.ShowUnderline = ValidationHelper.GetBoolean(this.GetValue("showunderline"), true);
        bbEditor.ShowStrike = ValidationHelper.GetBoolean(this.GetValue("showstrike"), true);
        bbEditor.ShowColor = ValidationHelper.GetBoolean(this.GetValue("showcolor"), true);
        bbEditor.ShowCode = ValidationHelper.GetBoolean(this.GetValue("showcode"), true);
        bbEditor.UsePromptDialog = ValidationHelper.GetBoolean(this.GetValue("usepromptdialog"), true);
        bbEditor.ShowAdvancedImage = ValidationHelper.GetBoolean(this.GetValue("showadvancedimage"), false);
        bbEditor.ShowAdvancedURL = ValidationHelper.GetBoolean(this.GetValue("showadvancedurl"), false);
        int size = ValidationHelper.GetInteger(this.GetValue("size"), 0);
        if (size > 0)
        {
            bbEditor.TextArea.MaxLength = size;
        }

        bbEditor.IsLiveSite = this.IsLiveSite;

        if (!String.IsNullOrEmpty(this.CssClass))
        {
            bbEditor.CssClass = this.CssClass;
            this.CssClass = null;
        }
        else if (String.IsNullOrEmpty(bbEditor.CssClass))
        {
            bbEditor.CssClass = "BBEditorField";
        }
        if (!string.IsNullOrEmpty(this.ControlStyle))
        {
            bbEditor.TextArea.Attributes.Add("style", this.ControlStyle);
            this.ControlStyle = null;
        }

        DialogConfiguration config = this.GetDialogConfiguration(this.FieldInfo);
        bbEditor.ImageDialogConfig = config;
        bbEditor.URLDialogConfig = config.Clone();

        this.CheckRegularExpression = true;
        this.CheckFieldEmptiness = true;
    }


    /// <summary>
    /// Returns true if user control is valid.
    /// </summary>
    public override bool IsValid()
    {
        int maxControlSize = 0;
        int minControlSize = 0;

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
            if ((this.FieldInfo.Size > 0) && ((maxControlSize > this.FieldInfo.Size) || (maxControlSize == 0)))
            {
                maxControlSize = this.FieldInfo.Size;
            }

            minControlSize = this.FieldInfo.MinStringLength;
        }

        // Validate control
        string error = null;
        bool valid = CheckLength(minControlSize, maxControlSize, bbEditor.TextArea.Text.Length, ref error, this.ErrorMessage);
        this.ValidationError = error;
        return valid;
    }


    /// <summary>
    /// Returns the arraylist of the field IDs (Client IDs of the inner controls) that should be spell checked.
    /// </summary>
    public override ArrayList GetSpellCheckFields()
    {
        ArrayList result = new ArrayList();
        result.Add(bbEditor.TextArea.ClientID);
        return result;
    }

    #endregion
}