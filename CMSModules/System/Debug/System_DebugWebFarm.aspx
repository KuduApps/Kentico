<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_System_Debug_System_DebugWebFarm"
    Theme="Default" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="System - SQL"
    CodeFile="System_DebugWebFarm.aspx.cs" %>

<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <asp:Label ID="lblError" runat="server" CssClass="ErrorLabel" EnableViewState="false"
        Visible="false" />
    <asp:Label ID="lblInfo" runat="server" CssClass="InfoLabel" EnableViewState="false" />
    <div class="AlignRight">
        <cms:CMSButton runat="server" ID="btnClear" OnClick="btnClear_Click" CssClass="LongButton"
            EnableViewState="false" />
    </div>
    <asp:PlaceHolder runat="server" ID="plcLogs" EnableViewState="false" />
</asp:Content>
