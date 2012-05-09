<%@  Page Language="C#" AutoEventWireup="true" CodeFile="~/CMSModules/Widgets/UI/Widget_Edit_SystemProperties.aspx.cs"
    Inherits="CMSModules_Widgets_UI_Widget_Edit_SystemProperties" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master"
    Theme="Default" Title="Widget Edit - System properties" %>
<%@ Register Src="~/CMSModules/PortalEngine/UI/WebParts/Development/DefaultValueEditor.ascx"
    TagName="DefaultValueEditor" TagPrefix="cms" %>
<asp:Content ContentPlaceHolderID="plcContent" ID="content" runat="server">
    <asp:Label ID="lblInfo" runat="server" CssClass="InfoLabel" Visible="false" EnableViewState="false" />
    <cms:DefaultValueEditor ID="ucDefaultValueEditor" runat="server" />
</asp:Content>
