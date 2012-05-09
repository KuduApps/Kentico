<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_System_System_Deployment" Theme="Default"
    MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="System - Deployment" CodeFile="System_Deployment.aspx.cs" %>

<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <asp:Label ID="lblError" runat="server" CssClass="ErrorLabel" EnableViewState="false"
        Visible="false" />
    <asp:Label ID="lblInfo" runat="server" CssClass="InfoLabel" EnableViewState="false" />
    <br />
    <cms:CMSButton ID="btnSaveAll" CssClass="XXLongSubmitButton" runat="server" OnClick="btnSaveAll_Click"
        EnableViewState="false" />
    <br />
    <br />
    <asp:Label ID="lblResult" runat="server" CssClass="InfoLabel" EnableViewState="false" />
</asp:Content>
