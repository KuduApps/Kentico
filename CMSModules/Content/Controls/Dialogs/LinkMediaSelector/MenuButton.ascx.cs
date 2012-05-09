using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

using CMS.UIControls;
using CMS.GlobalHelper;
using CMS.ExtendedControls;

public partial class CMSModules_Content_Controls_Dialogs_LinkMediaSelector_MenuButton : CMSUserControl
{
    #region "Variables"

    private string mIconUrl = null;
    private string mIconDisabledUrl = null;

    private string mIconClass = null;

    private string mText = null;
    private string mTooltip = null;

    private string mTextClass = null;

    private string mCssClass = null;
    private string mCssActiveClass = null;
    private string mCssHoverClass = null;
    private string mCssDisabledClass = null;

    private bool mEnabled = true;
    private bool mActive = false;
    private string mActiveGroup = null;
    private string mOnClickJavascript = null;

    #endregion


    #region "Properties"

    /// <summary>
    /// Icon image url.
    /// </summary>
    public string IconUrl
    {
        get
        {
            return this.mIconUrl;
        }
        set
        {
            this.mIconUrl = value;
        }
    }


    /// <summary>
    /// Url of icon when button is disabled.
    /// </summary>
    public string IconDisabledUrl
    {
        get
        {
            return this.mIconDisabledUrl;
        }
        set
        {
            this.mIconDisabledUrl = value;
        }
    }


    /// <summary>
    /// Icon main css class.
    /// </summary>
    public string IconClass
    {
        get
        {
            return this.mIconClass;
        }
        set
        {
            this.mIconClass = value;
        }
    }


    /// <summary>
    /// Button text.
    /// </summary>
    public string Text
    {
        get
        {
            return this.mText;
        }
        set
        {
            this.mText = value;
        }
    }


    /// <summary>
    /// Button tooltip.
    /// </summary>
    public string Tooltip
    {
        get
        {
            return this.mTooltip;
        }
        set
        {
            this.mTooltip = value;
        }
    }


    /// <summary>
    /// Text main css class.
    /// </summary>
    public string TextClass
    {
        get
        {
            return this.mTextClass;
        }
        set
        {
            this.mTextClass = value;
        }
    }


    /// <summary>
    /// Button main css class.
    /// </summary>
    public string CssClass
    {
        get
        {
            return this.mCssClass;
        }
        set
        {
            this.mCssClass = value;
        }
    }


    /// <summary>
    /// Css class name added to text when button is hovered.
    /// </summary>
    public string CssActiveClass
    {
        get
        {
            return this.mCssActiveClass;
        }
        set
        {
            this.mCssActiveClass = value;
        }
    }


    /// <summary>
    /// Css class name added to text when button is hovered.
    /// </summary>
    public string CssHoverClass
    {
        get
        {
            return this.mCssHoverClass;
        }
        set
        {
            this.mCssHoverClass = value;
        }
    }


    /// <summary>
    /// Css class name added to text when button is disabled.
    /// </summary>
    public string CssDisabledClass
    {
        get
        {
            return this.mCssDisabledClass;
        }
        set
        {
            this.mCssDisabledClass = value;
        }
    }


    /// <summary>
    /// Indicates if button is enabled.
    /// </summary>
    public bool Enabled
    {
        get
        {
            return this.mEnabled;
        }
        set
        {
            this.mEnabled = value;
        }
    }


    /// <summary>
    /// Indicates if button is active.
    /// </summary>
    public bool Active
    {
        get
        {
            return this.mActive;
        }
        set
        {
            this.mActive = value;
        }
    }


    /// <summary>
    /// Name of active buttons group.
    /// </summary>
    public string ActiveGroup
    {
        get
        {
            return this.mActiveGroup;
        }
        set
        {
            this.mActiveGroup = value;
        }
    }


    /// <summary>
    /// Javascript code executed in OnClick event.
    /// Variable 'elem' is used for clicked element.
    /// </summary>
    public string OnClickJavascript
    {
        get
        {
            return this.mOnClickJavascript;
        }
        set
        {
            this.mOnClickJavascript = value;
            ReloadData();
        }
    }


    /// <summary>
    /// Returns disable menu button javascript function.
    /// </summary>
    public string DisableButtonFunction
    {
        get
        {
            return String.Format("if (typeof MenuToogle === 'function') {{ MenuToogle('{0}', false); }}", this.pnlMain.ClientID);
        }
    }


    /// <summary>
    /// Returns enable menu button javascript function.
    /// </summary>
    public string EnableButtonFunction
    {
        get
        {
            return String.Format("if (typeof MenuToogle === 'function') {{ MenuToogle('{0}', true); }}", this.pnlMain.ClientID);
        }
    }


    /// <summary>
    /// On click postback action.
    /// If defined postback is enabled.
    /// </summary>
    public event EventHandler OnClick;

    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
    }


    protected void Page_PreRender(object sender, EventArgs e)
    {
        ReloadData();

        // Ensure jQuery
        if (this.Visible)
        {
            ScriptHelper.RegisterJQuery(Page);
            RegisterMenuJavascript();
        }
    }


    public void ReloadData()
    {
        // Initialize main properties
        this.lblText.Text = this.Text;
        if (String.IsNullOrEmpty(this.IconUrl))
        {
            this.imgIcon.Visible = false;
        }
        else
        {
            this.imgIcon.ImageUrl = this.IconUrl;
        }
        this.imgIcon.AlternateText = this.Tooltip;
        this.pnlMain.ToolTip = this.Tooltip;

        this.pnlMain.CssClass = this.CssClass;
        this.lblText.CssClass = this.TextClass;
        this.imgIcon.CssClass = this.IconClass;

        // If disabled add disabled css classes
        if (!Enabled)
        {
            // Change icon url if defined
            if (!String.IsNullOrEmpty(this.IconDisabledUrl))
            {
                this.imgIcon.ImageUrl = this.IconDisabledUrl;
            }
            pnlMain.CssClass = AddClass(pnlMain.CssClass, CssDisabledClass);
        }

        // If active add active css classes
        if (Active)
        {
            pnlMain.CssClass = AddClass(pnlMain.CssClass, CssActiveClass);
        }

        // If OnClick is defined add OnClick postback reference
        if (OnClick != null)
        {
            string postBackRef = ControlsHelper.GetPostBackEventReference(btnMenu, "");
            ScriptHelper.RegisterStartupScript(Page, typeof(string), "menuPostBack_" + ClientID, ScriptHelper.GetScript("function MenuPostBack_" + ClientID + "(){" + postBackRef + ";}"));
        }
        else
        {
            // Hide button if OnClick is not defined
            this.btnMenu.Visible = false;
        }

        string guid = Guid.NewGuid().ToString("N");

        // Menu button click function
        StringBuilder sb = new StringBuilder();
        sb.Append("function MenuClick_" + guid + "(elem) {\n");
        sb.Append("     MenuClick(elem); \n");
        if (!String.IsNullOrEmpty(OnClickJavascript))
        {
            sb.Append(OnClickJavascript);
        }
        if (OnClick != null)
        {
            sb.Append(ControlsHelper.GetPostBackEventReference(btnMenu, "") + "\n");
        }
        sb.Append("}\n");

        // Renders as function if not postback
        if (!URLHelper.IsPostback())
        {
            sb.Append("function SetButton_" + guid + "() {\n");
        }
        sb.Append("var currentButton = $j('#" + this.pnlMain.ClientID + "')");
        if (!String.IsNullOrEmpty(CssHoverClass))
        {
            sb.Append(".attr('_hoverClass', '" + CssHoverClass + "')");
        }
        if (!String.IsNullOrEmpty(CssActiveClass))
        {
            sb.Append(".attr('_activeClass', '" + CssActiveClass + "')");
        }
        if (!String.IsNullOrEmpty(CssDisabledClass))
        {
            sb.Append(".attr('_disabledClass', '" + CssDisabledClass + "')");
        }
        if (!String.IsNullOrEmpty(ActiveGroup))
        {
            sb.Append(".attr('_activeGroup', '" + ActiveGroup + "')");
            if (Active)
            {
                sb.Append(".attr('_active', '1')");
                sb.Append(".addClass('" + CssActiveClass + "')");
            }
            else
            {
                sb.Append(".attr('_active', '0')");
            }
        }
        // Append events only if not currently disabled
        sb.Append("; if (!currentButton.hasClass('" + CssDisabledClass + "')) {");
        sb.Append("currentButton.unbind()");
        sb.Append(".bind('mouseenter',function() {\n");
        sb.Append("     MenuOver(this); })\n");
        sb.Append(".bind('mouseleave',function() {\n");
        sb.Append("     MenuOut(this); })\n");
        sb.Append(".bind('click',function() {\n");
        sb.Append("     MenuClick_" + guid + "(this); });}\n");

        // End of javascript function
        if (!URLHelper.IsPostback())
        {
            sb.Append("}\n");
        }

        // Register javascript
        if (URLHelper.IsPostback())
        {
            // Call javascript on page load 
            ScriptHelper.RegisterStartupScript(Page, typeof(String), "menuclick_" + guid, ScriptHelper.GetScript(sb.ToString()));
        }
        else
        {
            // Register setup javascript
            this.ltlScript.Text = ScriptHelper.GetScript(sb.ToString());

            // Call setup javascript on page load 
            ScriptHelper.RegisterStartupScript(Page, typeof(String), "menuclick_" + guid, ScriptHelper.GetScript("if (typeof(SetButton_" + guid + ") != 'undefined') { SetButton_" + guid + "(); }"));
        }
    }


    /// <summary>
    /// Handle menu button onClick event.
    /// </summary>
    protected void btnMenu_Click(object sender, EventArgs e)
    {
        if (OnClick != null)
        {
            OnClick(sender, e);
        }
    }


    #region "Private methods"

    /// <summary>
    /// Adds CSS class to the given class
    /// </summary>
    /// <param name="className">Current class</param>
    /// <param name="newClassName">Class to add</param>
    private string AddClass(string className, string newClassName)
    {
        if (!String.IsNullOrEmpty(newClassName))
        {
            return className + " " + newClassName;
        }
        return className;
    }


    /// <summary>
    /// Registers javascript methods for the menu
    /// </summary>
    private void RegisterMenuJavascript()
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("function MenuOver(elem) {\n");
        sb.Append(" var jThis = $j(elem);\n");
        sb.Append(" var hoverClass = jThis.attr('_hoverclass');\n");
        sb.Append(" if(typeof(hoverClass) != 'undefined') {\n");
        sb.Append("     jThis.addClass(hoverClass);\n");
        sb.Append(" }\n");
        sb.Append(" \n");
        sb.Append("}\n");

        sb.Append("function MenuOut(elem) {\n");
        sb.Append(" var jThis = $j(elem);\n");
        sb.Append(" var hoverClass = jThis.attr('_hoverclass');\n");
        sb.Append(" var activeClass = jThis.attr('_activeclass');\n");
        sb.Append(" var active= jThis.attr('_active');\n");
        sb.Append(" if(active != '1') {\n");
        sb.Append(" if(typeof(hoverClass) != 'undefined') {\n");
        sb.Append("     if((activeClass) && (activeClass != hoverClass)) {\n");
        sb.Append("         jThis.removeClass(hoverClass);\n");
        sb.Append("     }\n");
        sb.Append("     else {\n");
        sb.Append("         jThis.removeClass(hoverClass);\n");
        sb.Append("     }\n");
        sb.Append(" }\n");
        sb.Append(" }\n");
        sb.Append("}\n");

        sb.Append("function MenuClick(elem) {\n");
        sb.Append(" var jThis = $j(elem);\n");
        sb.Append(" var activeGroup = jThis.attr('_activegroup');\n");
        sb.Append(" if (typeof(activeGroup) != 'undefined') {\n");
        sb.Append("     $j(\"div[_activeGroup='\" + activeGroup + \"']\").each(function(){");
        sb.Append("         var jThis = $j(this);");
        sb.Append("         var activeClass = jThis.attr('_activeclass');\n");
        sb.Append("         if(typeof(activeClass) != 'undefined') {\n");
        sb.Append("             jThis.removeClass(activeClass);\n");
        sb.Append("         }\n");
        sb.Append("         jThis.attr('_active', '0');");
        sb.Append("     });");
        sb.Append("     var activeClass = jThis.attr('_activeclass');\n");
        sb.Append("     if(typeof(activeClass) != 'undefined') {\n");
        sb.Append("         jThis.addClass(activeClass);\n");
        sb.Append("     }\n");
        sb.Append(" }\n");
        sb.Append(" \n");
        sb.Append("}\n");

        sb.Append("function MenuToogle(elemId, enable) { \n");
        sb.Append(" var jThis = $j('#'+elemId);\n");
        sb.Append(" var disabledClass = jThis.attr('_disabledclass');\n");
        sb.Append(" if(typeof(disabledClass) != 'undefined') {\n");
        sb.Append("     jThis.unbind();\n");
        sb.Append("     if (enable) {\n");
        sb.Append("             jThis.removeClass(disabledClass);\n");
        sb.Append("             jThis.bind('mouseenter',function() {\n");
        sb.Append("                 MenuOver(this); });\n");
        sb.Append("             jThis.bind('mouseleave',function() {\n");
        sb.Append("                 MenuOut(this); });\n");
        sb.Append("             jThis.bind('click',function() {\n");
        sb.Append("                 eval('MenuClick_' + elemId + '(this);'); });\n");
        sb.Append("     } else {\n");
        sb.Append("             jThis.addClass(disabledClass);\n");
        sb.Append("             jThis.click(function() { return false;});\n");
        sb.Append("     }\n");
        sb.Append(" } else {\n");
        sb.Append("     setTimeout(\"MenuToogle('\" + elemId + \"', \" + enable + \")\",100);\n");
        sb.Append(" }\n");
        sb.Append("}\n");

        ScriptHelper.RegisterStartupScript(Page, typeof(string), "menuButtonJavascript", ScriptHelper.GetScript(sb.ToString()));
    }

    #endregion
}
