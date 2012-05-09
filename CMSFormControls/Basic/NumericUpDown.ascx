<%@ Control Language="C#" AutoEventWireup="true" CodeFile="NumericUpDown.ascx.cs" Inherits="CMSFormControls_Basic_NumericUpDown" %>
<asp:Panel ID="pnlContainer" runat="server">
    <cms:CMSTextBox ID="textbox" runat="server" /><asp:ImageButton ID="btnDown" CssClass="ButtonDown" runat="server" Visible="false" /><asp:ImageButton ID="btnUp" CssClass="ButtonUp" runat="server" Visible="false" />
</asp:Panel>
