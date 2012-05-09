<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSAdminControls_UI_UniControls_UniMatrix"
    CodeFile="UniMatrix.ascx.cs" %>
<%@ Register Src="~/CMSAdminControls/UI/UniGrid/Controls/UniGridPager.ascx" TagName="UniGridPager"
    TagPrefix="cms" %>
<asp:Literal runat="server" ID="ltlContentBefore" EnableViewState="false" />
<asp:Label ID="lblInfo" runat="server" CssClass="InfoLabel" Visible="false" EnableViewState="false" />
<asp:PlaceHolder runat="server" ID="plcFilter">
    <asp:Literal runat="server" ID="ltlBeforeFilter" EnableViewState="false" /><asp:Panel
        runat="server" ID="pnlFilter" DefaultButton="btnFilter" Visible="false">
        <cms:LocalizedLabel ID="lblFilter" runat="server" EnableViewState="false" />
        <cms:CMSTextBox runat="server" ID="txtFilter" CssClass="ShortTextBox" /><cms:LocalizedButton
            runat="server" ID="btnFilter" OnClick="btnFilter_Click" CssClass="ShortButton" />
    </asp:Panel>
    <asp:Literal runat="server" ID="ltlAfterFilter" EnableViewState="false" />
</asp:PlaceHolder>
<asp:PlaceHolder runat="server" ID="plcBeforeRows">
    <tr id="contentbeforerows_<%= ClientID %>" class="<%=ContentBeforeRowsCssClass%>">
        <asp:Literal runat="server" ID="ltlBeforeRows" EnableViewState="false" />
    </tr>
</asp:PlaceHolder>
<asp:Literal runat="server" ID="ltlMatrix" EnableViewState="false" />
<asp:PlaceHolder runat="server" ID="plcPager">
    <asp:Literal runat="server" ID="ltlPagerBefore" EnableViewState="false" />
    <cms:UniGridPager ID="pagerElem" ShortID="pg" runat="server" DefaultPageSize="20"
        DisplayPager="true" />
    <asp:Literal runat="server" ID="ltlPagerAfter" EnableViewState="false" />
</asp:PlaceHolder>
<asp:Literal runat="server" ID="ltlContentAfter" EnableViewState="false" />
<asp:Label ID="lblInfoAfter" runat="server" CssClass="PageFooter" Visible="false"
    EnableViewState="false" />