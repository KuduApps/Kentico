using System;
using System.Web.UI;
using System.Web;

using CMS.UIControls;
using CMS.GlobalHelper;
using CMS.ExtendedControls;
using CMS.CMSHelper;

[Title("Design/Controls/Validation/codeview.png", "validation.viewcodetooltip", null)]
public partial class CMSModules_Content_CMSDesk_Validation_ViewCode : CMSPage, IPostBackEventHandler
{
    protected void Page_Init(object sender, EventArgs e)
    {
        if (!QueryHelper.ValidateHash("hash"))
        {
            RedirectToAccessDenied(ResHelper.GetString("dialogs.badhashtitle"));
        }

        // Set CSS classes
        CurrentMaster.PanelFooter.CssClass = "FloatRight";

        // Add close button
        CurrentMaster.PanelFooter.Controls.Add(new LocalizedButton
        {
            ID = "btnClose",
            ResourceString = "general.close",
            EnableViewState = false,
            OnClientClick = "window.top.close(); return false;",
            CssClass = "SubmitButton"
        });
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        string url = QueryHelper.GetString("url", null);
        string format = QueryHelper.GetString("format", null);

        txtCodeText.HighlightMacros = false;

        InitializeScripts(url);

        if (!String.IsNullOrEmpty(format))
        {
            txtCodeText.Language = LanguageCode.GetLanguageEnumFromString(format);
            SetTitle("Design/Controls/Validation/codeview.png", GetString("validation.css.viewcodetooltip"), null, null);
        }
    }


    private void InitializeScripts(string url)
    {
        ScriptHelper.RegisterScriptFile(Page, "Validation.js");
        ScriptHelper.RegisterJQuery(Page);
        RegisterModalPageScripts();

        string script = @"
function ResizeCodeArea() {
    var height = $j(""#divContent"").height();
    $j(""#" + txtCodeText.ClientID + @""").parent().css(""height"", height + ""px"");
    $j("".CodeMirror-scroll"").css(""height"", height - 26 + ""px"");
}

$j(window).resize(function(){ResizeCodeArea()});
$j(document).ready(function(){setTimeout(""ResizeCodeArea()"",300);" + ((!RequestHelper.IsPostBack() && !String.IsNullOrEmpty(url)) ? "LoadHTMLToElement('" + hdnHTML.ClientID + "','" + url + "');" + ControlsHelper.GetPostBackEventReference(this, null) + ";" : "") +
@"});$j(""#divContent"").css(""overflow"", """");";

        // Register script for resizing and scroll bar remove
        ScriptHelper.RegisterStartupScript(this, typeof(string), "AreaResizeAndScrollBarRemover", ScriptHelper.GetScript(script));
    }

    #region "IPostBackEventHandler Members"

    void IPostBackEventHandler.RaisePostBackEvent(string eventArgument)
    {
        txtCodeText.Text = ValidationHelper.Base64Decode(hdnHTML.Value);
        if (txtCodeText.Language == LanguageEnum.CSS)
        {
            txtCodeText.Text = txtCodeText.Text.Trim(new char[] { '\r', '\n' });
        }
        hdnHTML.Value = "";
    }

    #endregion
}
