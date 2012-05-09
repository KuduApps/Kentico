<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSSiteManager_accessdenied"
    EnableEventValidation="false" Title="Site Manager - Access denied" Theme="default"
    MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" CodeFile="accessdenied.aspx.cs" %>

<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <asp:Label ID="lblMessage" runat="server" Text="Label" EnableViewState="false" />
    <asp:HyperLink ID="lnkGoBack" runat="server" NavigateUrl="~/default.aspx" EnableViewState="false" /><br />
    <br />
    <br />
    <cms:LocalizedButton ID="btnSignOut" runat="server" CssClass="SubmitButton" OnClick="btnSignOut_Click" EnableViewState="false" ResourceString="signoutbutton.signout" />
    <asp:Literal ID="ltlScript" runat="server" EnableViewState="false" />
</asp:Content>
