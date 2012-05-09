using System;
using System.Text;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.UIControls;
using CMS.GlobalHelper;

public partial class CMSModules_Content_Controls_Dialogs_General_WidthHeightSelector : CMSUserControl
{
    #region "Variables"

    private bool mShowLabels = true;
    private bool mShowActions = true;
    private bool mVerticalLayout = true;
    private string mLockImg = null;
    private string mUnLockImg = null;
    private string mRefreshImg = null;
    private string mCustomRefreshCode = null;

    #endregion


    #region "Properties"

    /// <summary>
    /// Gets or sets the CSS Class of the Width & Height TextBoxes.
    /// </summary>
    public string TextBoxesClass
    {
        get
        {
            return this.txtWidth.CssClass;
        }
        set
        {
            this.txtHeight.CssClass = value;
            this.txtWidth.CssClass = value;
        }
    }


    /// <summary>
    /// Returns TextBox object for width.
    /// </summary>
    public TextBox WidthTextBox
    {
        get
        {
            return this.txtWidth;
        }
    }


    /// <summary>
    /// Returns TextBox object for height.
    /// </summary>
    public TextBox HeightTextBox
    {
        get
        {
            return this.txtHeight;
        }
    }


    /// <summary>
    /// Gets or sets custom refresh button code.
    /// </summary>
    /// <returns></returns>
    public string CustomRefreshCode
    {
        get
        {
            return this.mCustomRefreshCode;
        }
        set
        {
            this.mCustomRefreshCode = value;
        }
    }


    /// <summary>
    /// Gets or sets the value which determines whether the aspect ratio is locked.
    /// </summary>
    public bool Locked
    {
        get
        {
            return ValidationHelper.GetBoolean(this.hdnLocked.Value, true);
        }
        set
        {
            this.hdnLocked.Value = value.ToString();
            ScriptHelper.RegisterStartupScript(this.Page, typeof(Page), "lock" + this.ClientID, ScriptHelper.GetScript("var lock" + this.ClientID + " = " + (value ? "true" : "false") + ";"));
            this.imgLock.ImageUrl = (value ? this.mLockImg : this.mUnLockImg);
        }
    }


    /// <summary>
    /// Indicates if labes should be displayed.
    /// </summary>
    public bool ShowLabels
    {
        get
        {
            return this.mShowLabels;
        }
        set
        {
            this.mShowLabels = value;
        }
    }


    /// <summary>
    /// Indicates if actions should be visible.
    /// </summary>
    public bool ShowActions
    {
        get
        {
            return this.mShowActions;
        }
        set
        {
            this.mShowActions = value;
        }
    }


    /// <summary>
    /// Indicates if vertical layout should be used.
    /// </summary>
    public bool VerticalLayout
    {
        get
        {
            return this.mVerticalLayout;
        }
        set
        {
            this.mVerticalLayout = value;
        }
    }


    /// <summary>
    /// Object width.
    /// </summary>
    public int Width
    {
        get
        {
            return ValidationHelper.GetInteger(this.txtWidth.Text, 0);
        }
        set
        {
            this.hdnWidth.Value = value.ToString();
            this.txtWidth.Text = value.ToString();
        }
    }


    /// <summary>
    /// Object height.
    /// </summary>
    public int Height
    {
        get
        {
            return ValidationHelper.GetInteger(this.txtHeight.Text, 0);
        }
        set
        {
            this.hdnHeight.Value = value.ToString();
            this.txtHeight.Text = value.ToString();
        }
    }

    #endregion


    #region "Page events"

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        mLockImg = GetImageUrl("Design/Controls/Dialogs/lock.png");
        mUnLockImg = GetImageUrl("Design/Controls/Dialogs/unlock.png");
        mRefreshImg = GetImageUrl("Design/Controls/UniGrid/Actions/undo.png");
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.StopProcessing)
        {
            this.Visible = false;
        }
    }


    protected void Page_PreRender(object sender, EventArgs e)
    {
        this.lblWidth.Visible = this.ShowLabels;
        this.lblHeight.Visible = this.ShowLabels;

        this.imgLock.Visible = this.mShowActions;
        this.imgRefresh.Visible = this.mShowActions;

        this.imgLock.ImageUrl = (this.Locked ? this.mLockImg : this.mUnLockImg);
        this.imgRefresh.ImageUrl = this.mRefreshImg;

        this.imgLock.ToolTip = GetString("dialogs.lockratio");
        this.imgRefresh.ToolTip = GetString("dialogs.refreshsize");

        if (String.IsNullOrEmpty(this.CustomRefreshCode))
        {
            this.imgRefresh.Attributes.Add("onClick", GetRefreshJS());
        }
        else
        {
            this.imgRefresh.OnClientClick = this.CustomRefreshCode;
        }
        this.imgLock.Attributes.Add("onClick", GetLockJS());

        this.txtWidth.Attributes["onKeyUp"] = GetWidthKeyUpS();
        this.txtHeight.Attributes["onKeyUp"] = GetHeightKeyUpS();
        this.txtWidth.Attributes["onKeyDown"] = "return OnlyNumbers(event, true);";
        this.txtHeight.Attributes["onKeyDown"] = "return OnlyNumbers(event, true);";

        this.hdnWidth.Value = this.Width.ToString();
        this.hdnHeight.Value = this.Height.ToString();

        if (VerticalLayout)
        {
            this.lblWidth.Attributes.Add("style", "width:80px;");
            this.lblHeight.Attributes.Add("style", "width:80px;");
            this.txtWidth.Attributes.Add("style", "margin-bottom:5px;");
            this.ltlBreak.Text = "<br />";
        }
        else
        {
            this.lblWidth.Attributes.Add("style", "width:80px;");
            if (CultureHelper.IsUICultureRTL())
            {
                this.lblHeight.Attributes.Add("style", "width:80px;padding-right:20px;");
            }
            else
            {
                this.lblHeight.Attributes.Add("style", "width:80px;padding-left:20px;");
            }
        }

        InitializeControlScripts();
    }

    #endregion


    #region "Private methods"

    /// <summary>
    /// Initializes and registers all the necessary JavaScript.
    /// </summary>
    private void InitializeControlScripts()
    {
        string locked = "var lock" + this.ClientID + " = " + (this.Locked ? "true" : "false") + ";";

        ScriptHelper.RegisterOnlyNumbersScript(this.Page);
        ScriptHelper.RegisterStartupScript(this.Page, typeof(Page), "lock" + this.ClientID, ScriptHelper.GetScript(locked));
    }


    /// <summary>
    /// Returns regresh Javascript function.
    /// </summary>
    private string GetRefreshJS()
    {
        StringBuilder builder = new StringBuilder();

        builder.Append("var txtWidth = document.getElementById('" + this.txtWidth.ClientID + "');");
        builder.Append("var txtHeight = document.getElementById('" + this.txtHeight.ClientID + "');");
        builder.Append("var hdnWidth = document.getElementById('" + this.hdnWidth.ClientID + "');");
        builder.Append("var hdnHeight = document.getElementById('" + this.hdnHeight.ClientID + "');");
        builder.Append("if ((txtWidth!= null)&&(hdnWidth != null)) {");
        builder.Append("txtWidth.value = hdnWidth.value;");
        builder.Append("}");
        builder.Append("if ((txtHeight!= null)&&(hdnHeight != null)) {");
        builder.Append("txtHeight.value = hdnHeight.value;");
        builder.Append("} return false;");

        return builder.ToString();
    }

    /// <summary>
    /// Returns width textbox KeuUP Javascript function.
    /// </summary>
    private string GetWidthKeyUpS()
    {
        StringBuilder builder = new StringBuilder();

        builder.Append("var txtWidth = document.getElementById('" + this.txtWidth.ClientID + "');");
        builder.Append("var txtHeight = document.getElementById('" + this.txtHeight.ClientID + "');");
        builder.Append("var hdnWidth = document.getElementById('" + this.hdnWidth.ClientID + "');");
        builder.Append("var hdnHeight = document.getElementById('" + this.hdnHeight.ClientID + "');");
        builder.Append("if (lock" + this.ClientID + ") {");
        builder.Append("if ((txtWidth!= null)&&(hdnWidth != null)&&(txtHeight!= null)&&(hdnHeight != null)) {");
        builder.Append("var val = parseInt(parseInt(txtWidth.value,10)*(parseInt(hdnHeight.value,10)/parseInt(hdnWidth.value,10)),10);");
        builder.Append("if (isNaN(val)) {txtHeight.value = 0; } else {txtHeight.value = val;}");
        builder.Append("}}");

        return builder.ToString();
    }

    /// <summary>
    /// Returns height textbox KeuUP Javascript function.
    /// </summary>
    private string GetHeightKeyUpS()
    {
        StringBuilder builder = new StringBuilder();

        builder.Append("var txtWidth = document.getElementById('" + this.txtWidth.ClientID + "');");
        builder.Append("var txtHeight = document.getElementById('" + this.txtHeight.ClientID + "');");
        builder.Append("var hdnWidth = document.getElementById('" + this.hdnWidth.ClientID + "');");
        builder.Append("var hdnHeight = document.getElementById('" + this.hdnHeight.ClientID + "');");
        builder.Append("if (lock" + this.ClientID + ") {");
        builder.Append("if ((txtWidth!= null)&&(hdnWidth != null)&&(txtHeight!= null)&&(hdnHeight != null)) {");
        builder.Append("var val = parseInt(parseInt(txtHeight.value,10)*(parseInt(hdnWidth.value,10)/parseInt(hdnHeight.value,10)),10);");
        builder.Append("if (isNaN(val)) {txtWidth.value = 0; } else {txtWidth.value = val;}");
        builder.Append("}}");

        return builder.ToString();
    }

    /// <summary>
    /// Returns lock Javascript function.
    /// </summary>
    private string GetLockJS()
    {
        StringBuilder builder = new StringBuilder();

        builder.Append("var hdnLockElem = document.getElementById('" + this.hdnLocked.ClientID + "');");
        builder.Append("if (hdnLockElem != null) {");
        builder.Append("if (lock" + this.ClientID + " == true) {");
        builder.Append("lock" + this.ClientID + " = false;");
        builder.Append("hdnLockElem.value = false;");
        builder.Append("this.src = '" + ResolveUrl(this.mUnLockImg) + "'");
        builder.Append("} else {");
        builder.Append("var hdnWidth = document.getElementById('" + this.hdnWidth.ClientID + "');");
        builder.Append("var hdnHeight = document.getElementById('" + this.hdnHeight.ClientID + "');");
        builder.Append("var txtWidth = document.getElementById('" + this.txtWidth.ClientID + "');");
        builder.Append("var txtHeight = document.getElementById('" + this.txtHeight.ClientID + "');");
        builder.Append("if ((hdnWidth != null) && (hdnHeight != null) && (txtWidth != null) && (txtHeight != null)) {");
        builder.Append("hdnWidth.value = txtWidth.value;");
        builder.Append("hdnHeight.value = txtHeight.value;");
        builder.Append("lock" + this.ClientID + " = true;");
        builder.Append("hdnLockElem.value = true;");
        builder.Append("this.src = '" + ResolveUrl(this.mLockImg) + "'");
        builder.Append("}");
        builder.Append("}");
        builder.Append("} return false;");

        return builder.ToString();
    }

    #endregion


    #region "Public methods"

    /// <summary>
    /// Validates the user input.
    /// </summary>
    public bool Validate()
    {
        return ((this.txtWidth.Text.Trim() == "") || ValidationHelper.IsInteger(this.txtWidth.Text.Trim())) &&
            ((this.txtHeight.Text.Trim() == "") || ValidationHelper.IsInteger(this.txtHeight.Text.Trim()));
    }

    #endregion
}
