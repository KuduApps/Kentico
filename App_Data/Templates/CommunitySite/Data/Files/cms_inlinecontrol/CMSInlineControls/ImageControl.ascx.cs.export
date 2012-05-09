using System;
using System.Text;
using System.Web;
using System.Web.UI;

using CMS.ExtendedControls;
using CMS.GlobalHelper;

public partial class CMSInlineControls_ImageControl : InlineUserControl
{
    #region "Properties"

    /// <summary>
    /// Gets or sets the value which determines whether to use the control in special mode (icon of the filetype with hovereffect).
    /// </summary>
    public bool ShowFileIcons
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("ShowFileIcons"), false);
        }
        set
        {
            this.SetValue("ShowFileIcons", value);
        }
    }


    /// <summary>
    /// URL of the image media.
    /// </summary>
    public string URL
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("URL"), null);
        }
        set
        {
            this.SetValue("URL", value);
        }
    }


    /// <summary>
    /// Gets or sets the value which determines whether to append size parameters to URL ot not.
    /// </summary>
    public bool SizeToURL
    {
        get
        {
            return ValidationHelper.GetBoolean(this.GetValue("SizeToURL"), true);
        }
        set
        {
            this.SetValue("SizeToURL", value);
        }
    }


    /// <summary>
    /// Image extension.
    /// </summary>
    public string Extension
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("Extension"), null);
        }
        set
        {
            this.SetValue("Extension", value);
        }
    }


    /// <summary>
    /// Image alternative text.
    /// </summary>
    public string Alt
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("Alt"), null);
        }
        set
        {
            this.SetValue("Alt", value);
        }
    }


    /// <summary>
    /// Image width.
    /// </summary>
    public int Width
    {
        get
        {
            return ValidationHelper.GetInteger(this.GetValue("Width"), -1);
        }
        set
        {
            this.SetValue("Width", value);
        }
    }


    /// <summary>
    /// Image height.
    /// </summary>
    public int Height
    {
        get
        {
            return ValidationHelper.GetInteger(this.GetValue("Height"), -1);
        }
        set
        {
            this.SetValue("Height", value);
        }
    }


    /// <summary>
    /// Image border width.
    /// </summary>
    public int BorderWidth
    {
        get
        {
            return ValidationHelper.GetInteger(this.GetValue("BorderWidth"), -1);
        }
        set
        {
            this.SetValue("BorderWidth", value);
        }
    }


    /// <summary>
    /// Image border color.
    /// </summary>
    public string BorderColor
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("BorderColor"), null);
        }
        set
        {
            this.SetValue("BorderColor", value);
        }
    }

    /// <summary>
    /// Image horizontal space.
    /// </summary>
    public int HSpace
    {
        get
        {
            return ValidationHelper.GetInteger(this.GetValue("HSpace"), -1);
        }
        set
        {
            this.SetValue("HSpace", value);
        }
    }


    /// <summary>
    /// Image vertical space.
    /// </summary>
    public int VSpace
    {
        get
        {
            return ValidationHelper.GetInteger(this.GetValue("VSpace"), -1);
        }
        set
        {
            this.SetValue("VSpace", value);
        }
    }


    /// <summary>
    /// Image align.
    /// </summary>
    public string Align
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("Align"), null);
        }
        set
        {
            this.SetValue("Align", value);
        }
    }


    /// <summary>
    /// Image ID.
    /// </summary>
    public string ImageID
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("Id"), null);
        }
        set
        {
            this.SetValue("Id", value);
        }
    }


    /// <summary>
    /// Image tooltip text.
    /// </summary>
    public string Tooltip
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("Tooltip"), null);
        }
        set
        {
            this.SetValue("Tooltip", value);
        }
    }


    /// <summary>
    /// Image css class.
    /// </summary>
    public string Class
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("Class"), null);
        }
        set
        {
            this.SetValue("Class", value);
        }
    }


    /// <summary>
    /// Image inline style.
    /// </summary>
    public string Style
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("Style"), null);
        }
        set
        {
            this.SetValue("Style", value);
        }
    }


    /// <summary>
    /// Image link destination.
    /// </summary>
    public string Link
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("Link"), null);
        }
        set
        {
            this.SetValue("Link", value);
        }
    }


    /// <summary>
    /// Image link target (_blank/_self/_parent/_top)
    /// </summary>
    public string Target
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("Target"), null);
        }
        set
        {
            this.SetValue("Target", value);
        }
    }


    /// <summary>
    /// Image behavior.
    /// </summary>
    public string Behavior
    {
        get
        {
            return ValidationHelper.GetString(this.GetValue("Behavior"), null);
        }
        set
        {
            this.SetValue("Behavior", value);
        }
    }


    /// <summary>
    /// Width of the thumbnail image which is displayed when mouse is moved over the image.
    /// </summary>
    public int MouseOverWidth
    {
        get
        {
            return ValidationHelper.GetInteger(this.GetValue("MouseOverWidth"), 0);
        }
        set
        {
            this.SetValue("MouseOverWidth", value);
        }
    }


    /// <summary>
    /// Height of the thumbnail image which is displayed when mouse is moved over the image.
    /// </summary>
    public int MouseOverHeight
    {
        get
        {
            return ValidationHelper.GetInteger(this.GetValue("MouseOverHeight"), 0);
        }
        set
        {
            this.SetValue("MouseOverHeight", value);
        }
    }


    /// <summary>
    /// Control parameter.
    /// </summary>
    public override string Parameter
    {
        get
        {
            return this.URL;
        }
        set
        {
            this.URL = value;
        }
    }

    #endregion


    #region "Page events"

    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (this.Behavior == "hover")
        { 
            StringBuilder sb = new StringBuilder();
            // If jQuery not loaded
            sb.AppendFormat(@"
if (typeof jQuery == 'undefined') {{ 
    var jQueryCore=document.createElement('script');
    jQueryCore.setAttribute('type','text/javascript'); 
    jQueryCore.setAttribute('src', '{0}');
    setTimeout('document.body.appendChild(jQueryCore)',100); 
    setTimeout('loadTooltip()',200);
}}", ScriptHelper.GetScriptUrl("~/CMSScripts/jquery/jquery-core.js"));

            // If jQuery tooltip plugin not loaded
            sb.AppendFormat(@"
var jQueryTooltips=document.createElement('script'); 
function loadTooltip() {{ 
    if (typeof jQuery == 'undefined') {{ setTimeout('loadTooltip()',200); return;}} 
    if (typeof jQuery.fn.tooltip == 'undefined') {{ 
        jQueryTooltips.setAttribute('type','text/javascript'); 
        jQueryTooltips.setAttribute('src', '{0}'); 
        setTimeout('document.body.appendChild(jQueryTooltips)',100); 
    }}
}}", ScriptHelper.GetScriptUrl("~/CMSScripts/jquery/jquery-tooltips.js"));


            string rtlDefinition = null;
            if (((this.IsLiveSite) && (CultureHelper.IsPreferredCultureRTL())) || (CultureHelper.IsUICultureRTL()))
            {
                rtlDefinition = "positionLeft: true,left: -15,";
            }

            sb.AppendFormat(@"
function hover(imgID, width, height, sizeInUrl) {{ 
    if ((typeof jQuery == 'undefined')||(typeof jQuery.fn.tooltip == 'undefined')) {{
        var imgIDForTimeOut = imgID.replace(/\\/gi,""\\\\"").replace(/'/gi,""\\'"");
        setTimeout(""loadTooltip();hover('""+imgIDForTimeOut+""',""+width+"",""+height+"",""+sizeInUrl+"")"",100); return;
    }}
    jQuery('img[id='+imgID+']').tooltip({{
        delay: 0,
        track: true,
        showBody: "" - "",
        showBody: "" - "", 
        extraClass: ""ImageExtraClass"",
        showURL: false, 
        {0}
        bodyHandler: function() {{
            var hidden = jQuery(""#"" + imgID + ""_src"");
            var source = this.src;
            if (hidden[0] != null) {{
                source = hidden[0].value;
            }}
            var hoverDiv = jQuery(""<div/>"");
            var hoverImg = jQuery(""<img/>"").attr(""class"", ""ImageTooltip"").attr(""src"", source);
            hoverImg.css({{'width' : width, 'height' : height}});
            hoverDiv.append(hoverImg);
            return hoverDiv;
        }}
    }});
}}", rtlDefinition);

            ScriptHelper.RegisterStartupScript(this.Page, typeof(Page), "JQueryImagePreview", ScriptHelper.GetScript(sb.ToString()));
        }

        ImageParameters imgParams = new ImageParameters();
        if (!String.IsNullOrEmpty(this.URL))
        {
            imgParams.Url = ResolveUrl(this.URL);
        }
        imgParams.Align = this.Align;
        imgParams.Alt = this.Alt;
        imgParams.Behavior = this.Behavior;
        imgParams.BorderColor = this.BorderColor;
        imgParams.BorderWidth = this.BorderWidth;
        imgParams.Class = this.Class;
        imgParams.Extension = this.Extension;
        imgParams.Height = this.Height;
        imgParams.HSpace = this.HSpace;
        imgParams.Id = (String.IsNullOrEmpty(this.ImageID) ? Guid.NewGuid().ToString() : this.ImageID);
        imgParams.Link = this.Link;
        imgParams.MouseOverHeight = this.MouseOverHeight;
        imgParams.MouseOverWidth = this.MouseOverWidth;
        imgParams.SizeToURL = this.SizeToURL;
        imgParams.Style = this.Style;
        imgParams.Target = this.Target;
        imgParams.Tooltip = this.Tooltip;
        imgParams.VSpace = this.VSpace;
        imgParams.Width = this.Width;

        if (this.ShowFileIcons && (this.Extension != null))
        {
            imgParams.Width = 0;
            imgParams.Height = 0;
            imgParams.Url = GetFileIconUrl(this.Extension, "List");
        }

        this.ltlImage.Text = MediaHelper.GetImage(imgParams);

        // Dynamic JQuery hover effect
        if (this.Behavior == "hover")
        {
            string imgId = HTMLHelper.HTMLEncode(HttpUtility.UrlDecode(imgParams.Id));
            string url = HttpUtility.HtmlDecode(this.URL);
            if (this.SizeToURL)
            {
                if (MouseOverWidth > 0)
                {
                    url = URLHelper.UpdateParameterInUrl(url, "width", this.MouseOverWidth.ToString());
                }
                if (MouseOverHeight > 0)
                {
                    url = URLHelper.UpdateParameterInUrl(url, "height", this.MouseOverHeight.ToString());
                }
                url = URLHelper.RemoveParameterFromUrl(url, "maxsidesize");
            }
            this.ltlImage.Text += "<input type=\"hidden\" id=\"" + imgId + "_src\" value=\"" + ResolveUrl(url) + "\" />";

            ScriptHelper.RegisterStartupScript(this.Page, typeof(Page), "ImageHover_" + imgId, ScriptHelper.GetScript("hover(" +ScriptHelper.GetString(ScriptHelper.EscapeJQueryCharacters(imgId)) + ", " + MouseOverWidth + ", " + MouseOverHeight + ", " + (SizeToURL ? "true" : "false") + ");"));
            if (!RequestStockHelper.Contains("DialogsImageHoverStyle"))
            {
                RequestStockHelper.Add("DialogsImageHoverStyle", true);
                CSSHelper.RegisterCSSBlock(this.Page, "#tooltip {position: absolute;z-index:5000;}");                
            }
        }
    }

    #endregion
}