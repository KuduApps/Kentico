<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSAdminControls_AsyncControl"
    CodeFile="AsyncControl.ascx.cs" %>
<cms:CMSButton ID="btnFinished" runat="server" OnClick="btnFinished_Click" />
<cms:CMSButton ID="btnError" runat="server" OnClick="btnError_Click" />
<cms:CMSButton ID="btnCancel" runat="server" OnClick="btnCancel_Click" />
<asp:Panel ID="pnlAsync" runat="server" CssClass="AsyncPanel" />
