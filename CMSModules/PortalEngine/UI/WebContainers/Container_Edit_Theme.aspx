<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_PortalEngine_UI_WebContainers_Container_Edit_Theme"
    Theme="Default" MasterPageFile="~/CMSMasterPages/UI/EmptyPage.master" Title="Web part container - Theme"
    CodeFile="Container_Edit_Theme.aspx.cs" EnableEventValidation="false" %>

<%@ Register Src="~/CMSModules/AdminControls/Controls/CSS/ThemeEditor.ascx" TagName="ThemeEditor"
    TagPrefix="cms" %>
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <cms:ThemeEditor ID="themeElem" runat="server" />
</asp:Content>
