<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_System_Files_System_FilesTest"
    Theme="Default" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="Administration - System - Test files"
    CodeFile="System_FilesTest.aspx.cs" %>

<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <asp:Label ID="lblInfo" runat="server" CssClass="InfoLabel" EnableViewState="false" />
    <br />
    <cms:CMSButton runat="server" ID="btnTest" CssClass="LongSubmitButton" OnClick="btnTest_Click"
        EnableViewState="false" />
    <br />
    <br />
    <asp:Label runat="server" ID="ltlInfo" EnableViewState="false" />
</asp:Content>
