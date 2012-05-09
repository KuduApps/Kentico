<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master"
    Title="Groups - Delete group" Inherits="CMSModules_Groups_Tools_Group_Delete"
    Theme="Default" CodeFile="Group_Delete.aspx.cs" %>

<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <asp:Panel ID="pnlError" runat="server">
        <cms:LocalizedLabel ID="lblError" runat="server" CssClass="ErrorLabel" EnableViewState="false" />
    </asp:Panel>
    <cms:LocalizedLabel ID="lblMsg" runat="server" ResourceString="group.deletemessage" /><br />
    <br />
    <cms:LocalizedCheckBox ID="chkDeleteAll" runat="server" ResourceString="group.deleterelated"
        EnableViewState="false" Checked="true" /><br />
    <br />
    <br />
    <cms:LocalizedButton ID="btnDelete" runat="server" CssClass="ContentButton" ResourceString="general.yes"
        EnableViewState="false" />
    <cms:LocalizedButton ID="btnCancel" runat="server" CssClass="ContentButton" ResourceString="general.no"
        EnableViewState="false" />
</asp:Content>
