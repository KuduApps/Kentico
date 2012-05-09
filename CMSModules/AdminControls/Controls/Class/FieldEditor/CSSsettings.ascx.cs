using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.UIControls;
using CMS.FormEngine;

public partial class CMSModules_AdminControls_Controls_Class_FieldEditor_CSSsettings : CMSUserControl
{
    #region "Variables"

    private FormFieldInfo ffi = null;

    #endregion


    #region "Parameters"

    /// <summary>
    /// Caption style.
    /// </summary>
    public string CaptionStyle
    {
        get
        {
            return this.txtCaptionStyle.Text.Trim();
        }
    }


    /// <summary>
    /// Input style.
    /// </summary>
    public string InputStyle
    {
        get
        {
            return this.txtInputStyle.Text.Trim();
        }
    }


    /// <summary>
    /// Gets or sets value which is set in Control CSS class textbox.
    /// </summary>
    public string ControlCssClass
    {
        get
        {
            return this.txtControlCssClass.Text.Trim();
        }
    }


    /// <summary>
    /// FormFieldInfo of given field.
    /// </summary>
    public FormFieldInfo FieldInfo
    {
        get
        {
            return ffi;
        }
        set
        {
            ffi = value;
        }
    }


    /// <summary>
    /// Sets value indicating if control is enabled.
    /// </summary>
    public bool Enabled
    {
        set
        {
            pnlCss.Enabled = value;
        }
    }

    #endregion


    #region "Methods"

    protected void Page_Load(object sender, EventArgs e)
    {
        pnlCss.GroupingText = GetString("templatedesigner.section.styles");
    }


    /// <summary>
    /// Load field.
    /// </summary>
    public void Reload()
    {
        if (ffi != null)
        {
            txtCaptionStyle.Text = ffi.CaptionStyle;
            txtInputStyle.Text = ffi.InputControlStyle;
            txtControlCssClass.Text = ffi.ControlCssClass;
            pnlCss.Enabled = ffi.Visible;
        }
        else
        {
            txtCaptionStyle.Text = null;
            txtInputStyle.Text = null;
            txtControlCssClass.Text = null;
            pnlCss.Enabled = true;
        }
    }

    #endregion
}
