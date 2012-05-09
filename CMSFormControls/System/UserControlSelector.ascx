<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="CMSFormControls_System_UserControlSelector" CodeFile="UserControlSelector.ascx.cs" %>
<asp:Panel ID="pnlForm" runat="server">
    <cms:CMSTextBox ID="txtPath" runat="server" CssClass="SelectorTextBox" /><cms:LocalizedButton
        ID="btnSelect" runat="server" EnableViewState="false" CssClass="ContentButton" /><cms:LocalizedButton ID="btnClear"
            runat="server" EnableViewState="false" CssClass="ContentButton"  />
</asp:Panel>
