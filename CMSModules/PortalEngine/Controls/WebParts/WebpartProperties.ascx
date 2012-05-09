<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_PortalEngine_Controls_WebParts_WebpartProperties"
    CodeFile="WebpartProperties.ascx.cs" %>
<div id="CMSHeaderDiv"">
    <div id="CKToolbar" class="CKToolbar">
    </div>
</div>

<script type="text/javascript">
    //<![CDATA[
    var cmsHeader = null, cmsHeaderPad = null, cmsFooter = null, cmsFooterPad = null, disableQim = true;
    var resizeInterval = setInterval('if (window.ResizeToolbar) { ResizeToolbar(); }', 300);

    jQuery(function() {
        jQuery('.FormCategoryList a[href^=#]').each(function() {
            var jThis = jQuery(this);
            jThis.click(function() {
                var target = jQuery(this.hash);
                target = target.length && target || $('[name=' + this.hash.slice(1) + ']');
                if (target.length) {
                    // target offset - toolbar height - padding
                    var targetOffset = target.offset().top - GetHeight().header - 5;
                    jQuery('html,body').animate({ scrollTop: targetOffset }, 300);
                    return false;
                }
            });
        });
    });
    //]]>
</script>

<asp:Panel runat="server" ID="pnlTab" CssClass="TabsPageContent">
    <asp:Label ID="lblError" runat="server" CssClass="ErrorLabel" Visible="false" EnableViewState="false" />
    <asp:Label ID="lblInfo" runat="server" CssClass="InfoLabel" Visible="false" EnableViewState="false" />
    <asp:Panel runat="server" ID="pnlFormArea" CssClass="WebPartForm">
        <cms:BasicForm runat="server" ID="form" HtmlAreaToolbarLocation="Out:CKToolbar" Enabled="true"
            DefaultFormLayout="Tables" DefaultCategoryName="Default" AllowMacroEditing="true"
            IsLiveSite="false" RenderCategoryList="true" MarkRequiredFields="true" />
        <br class="ClearBoth" />
        <asp:Panel runat="server" ID="pnlExport" CssClass="InfoLabel">
            <asp:Literal runat="server" ID="ltlExport" EnableViewState="false" />
        </asp:Panel>
    </asp:Panel>
</asp:Panel>
<asp:HiddenField runat="server" ID="hidRefresh" Value="0" />
<asp:Literal ID="ltlScript" runat="server" EnableViewState="false" />
<cms:CMSButton ID="btnOnApply" runat="server" Visible="false" EnableViewState="false" />
<cms:CMSButton ID="btnOnOK" runat="server" Visible="false" EnableViewState="false" />
<asp:HiddenField ID="hdnIsNewWebPart" runat="server" />
<asp:HiddenField ID="hdnInstanceGUID" runat="server" />
<div id="CMSFooterDiv">
</div>

<script type="text/javascript">
    //<![CDATA[
    // cmsedit.js function override for CKEditor
    function SaveDocument() { }
    //]]>
</script>