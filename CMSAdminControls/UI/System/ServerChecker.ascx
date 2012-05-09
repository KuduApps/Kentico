<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ServerChecker.ascx.cs"
    Inherits="CMSAdminControls_UI_System_ServerChecker" %>
<asp:ImageButton runat="server" ID="btnCheckServer" EnableViewState="false" ValidationGroup="ServerChecker" />
<cms:LocalizedLabel runat="server" ID="lblStatus" EnableViewState="false" Text="&nbsp;" />
