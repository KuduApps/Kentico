<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_PortalEngine_Controls_WebParts_WebPartZoneProperties"
    CodeFile="WebPartZoneProperties.ascx.cs" %>
<style type="text/css">
    .ContentEditorToolbar
    {
        background-color: #f5f3ec;
        height: 76px;
        width: 94%;
        overflow: auto;
        left: 10px;
        top: 0px;
        z-index: 2;
        position: absolute;
        padding: 1px;
    }
    .ContentEditorToolbarPadding
    {
        height: 76px;
    }
</style>

<script type="text/javascript">
    //<![CDATA[
    var disableQim = true;
    //]]>
</script>

<asp:PlaceHolder ID="plcToolbar" runat="server" Visible="false" EnableViewState="false">
    <div id="CKEditorToolbar" class="ContentEditorToolbar">
    </div>
</asp:PlaceHolder>
<asp:PlaceHolder ID="plcToolbarPadding" runat="server" Visible="false" EnableViewState="false">
    <div class="ContentEditorToolbarPadding">
    </div>
</asp:PlaceHolder>
<asp:Label ID="lblInfo" runat="server" CssClass="InfoLabel" Visible="false" EnableViewState="false" />
<asp:Label runat="server" ID="lblError" CssClass="ErrorLabel" Visible="false" EnableViewState="false" />
<asp:Panel runat="server" ID="pnlFormArea" CssClass="WebPartForm">    
    <cms:BasicForm runat="server" ID="formElem" IsInsertMode="true" HtmlAreaToolbarLocation="Out:CKEditorToolbar"
        Enabled="true" DefaultFormLayout="Tables" DefaultCategoryName="Default" AllowMacroEditing="true"
        IsLiveSite="false" />
    <asp:Panel runat="server" ID="pnlExport" CssClass="InfoLabel">
        <asp:Literal runat="server" ID="ltlExport" />
    </asp:Panel>
    <asp:Literal ID="ltlScript" runat="server" EnableViewState="false" />
</asp:Panel>
