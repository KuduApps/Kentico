<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_Blogs_FormControls_NotPingedUrls" CodeFile="NotPingedUrls.ascx.cs" %>
<asp:Panel ID="pnlTextarea" runat="server">
    <cms:CMSTextBox ID="txtSendTo" runat="server" Width="100%" TextMode="MultiLine" EnableViewState="false"
        Rows="3" /><br />
    <cms:LocalizedLabel ID="lblSendTo" runat="server" ResourceString="blogs.trackbacks.sendto"
        EnableViewState="false" Font-Italic="true" />
    <br />
</asp:Panel>
