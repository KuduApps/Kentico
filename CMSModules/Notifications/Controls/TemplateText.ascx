<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_Notifications_Controls_TemplateText" CodeFile="TemplateText.ascx.cs" %>
<%@ Register src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" tagname="UniGrid" tagprefix="cms" %>
<cms:LocalizedLabel runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false"
    Visible="false" />
<cms:LocalizedLabel runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false"
    Visible="false" />
<asp:PlaceHolder runat="server" ID="plcTexts" EnableViewState="false" />
<asp:Panel runat="server" ID="pnlGrid">
    <cms:UniGrid runat="server" ID="gridGateways" GridName="~/CMSModules/Notifications/Controls/TemplateText.xml"
        OrderBy="GatewayDisplayName" IsLiveSite="false" Columns="GatewayID, GatewayDisplayName, GatewayEnabled" />
</asp:Panel>
