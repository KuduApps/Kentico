<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_CssStylesheets_Pages_CssStylesheet_Theme"
    Theme="Default" MasterPageFile="~/CMSMasterPages/UI/EmptyPage.master" Title="Css stylesheets - Sites"
    CodeFile="CssStylesheet_Theme.aspx.cs" EnableEventValidation="false" %>

<%@ Register Src="~/CMSModules/AdminControls/Controls/CSS/ThemeEditor.ascx" TagName="ThemeEditor"
    TagPrefix="cms" %>
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <cms:ThemeEditor ID="themeElem" runat="server" />
</asp:Content>
