<%@ Page Language="C#" AutoEventWireup="true" CodeFile="~/CMSModules/PortalEngine/UI/WebParts/Development/WebPart_Edit_SystemProperties.aspx.cs"
    MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Inherits="CMSModules_PortalEngine_UI_WebParts_Development_WebPart_Edit_SystemProperties" Theme="Default" %>

<%@ Register Src="DefaultValueEditor.ascx" TagName="DefaultValueEditor" TagPrefix="cms" %>
<asp:Content ContentPlaceHolderID="plcContent" ID="content" runat="server">
    <asp:Label ID="lblInfo" runat="server" CssClass="InfoLabel" Visible="false" EnableViewState="false" />
    <cms:DefaultValueEditor ID="ucDefaultValueEditor" runat="server" />
</asp:Content>
