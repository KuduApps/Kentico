<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_Content_Controls_Dialogs_LinkMediaSelector_PageSize" CodeFile="PageSize.ascx.cs" %>
<span>
    <asp:Label ID="lblPageSize" runat="server" CssClass="FieldLabel" EnableViewState="false"></asp:Label></span>
<asp:DropDownList ID="drpPageSize" runat="server" AutoPostBack="true"
    OnSelectedIndexChanged="drpPageSize_SelectedIndexChanged">
</asp:DropDownList>
