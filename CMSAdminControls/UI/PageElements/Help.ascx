<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSAdminControls_UI_PageElements_Help"
    CodeFile="Help.ascx.cs" %>
<asp:HyperLink runat="server" ID="lnkHelp" Target="_blank" CssClass="HelpLink" EnableViewState="false">
    <asp:Image runat="server" ID="imgHelp" CssClass="HelpImage" EnableViewState="false" />
</asp:HyperLink>
<asp:Literal runat="server" ID="ltlScript" EnableViewState="false" />