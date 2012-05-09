<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_Newsletters_Tools_Newsletters_Newsletter_ContentEditorFooter"
    CodeFile="Newsletter_ContentEditorFooter.ascx.cs" %>
<%@ Register Src="~/CMSAdminControls/UI/Macros/MacroSelector.ascx" TagName="MacroSelector"
    TagPrefix="cms" %>
<table class="NewsletterFooterTable" cellspacing="0">
    <tr>
        <td class="NewsletterFooterCell">
            <asp:Label ID="lblInsertField" runat="server" EnableViewState="false" /><br />
        </td>
        <td class="NewsletterFooterCell">
            <asp:DropDownList ID="lstInsertField" runat="server" CssClass="SourceFieldDropDown" />
            <cms:CMSButton ID="btnInsertField" runat="server" CssClass="ContentButton" />
        </td>
    </tr>
    <tr>
        <td class="NewsletterFooterCell">
            <asp:Label ID="lblInsertMacro" runat="server" EnableViewState="false" /><br />
        </td>
        <td class="NewsletterFooterCell">
            <cms:MacroSelector ID="macroSelectorElm" runat="server" IsLiveSite="false" />
        </td>
    </tr>
</table>

<script type="text/javascript">
    //<![CDATA[
    function InsertMacro(macro) {
        if ((window.frames['iframeContent'] != null) && (window.frames['iframeContent'].InsertHTML != null)) {
            window.frames['iframeContent'].InsertHTML(macro);
        }
    }

    function RememberFocusedRegion() {
        if ((window.frames['iframeContent'] != null) && (window.frames['iframeContent'].RememberFocusedRegion != null)) {
            window.frames['iframeContent'].RememberFocusedRegion();
        }
    }
    //]]>
</script>

