<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_System_Debug_System_DebugMacros" Theme="Default"
    MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="System - Security" CodeFile="System_DebugMacros.aspx.cs" %>

<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
     <div class="FloatLeft">
        <cms:LocalizedCheckBox runat="server" ID="chkCompleteContext" ResourceString="Debug.ShowCompleteContext"
            AutoPostBack="true" />
    </div>
    <div class="FloatRight">
        <cms:CMSButton runat="server" ID="btnClear" OnClick="btnClear_Click" CssClass="LongButton" EnableViewState="false" />
    </div>
    <br />
    <br />
    <asp:Label ID="lblError" runat="server" CssClass="ErrorLabel" EnableViewState="false"
        Visible="false" />
    <asp:Label ID="lblInfo" runat="server" CssClass="InfoLabel" EnableViewState="false" />
    <asp:PlaceHolder runat="server" ID="plcLogs" />
</asp:Content>
