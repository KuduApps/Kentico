<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_System_Debug_System_ViewRequest" MasterPageFile="~/CMSMasterPages/UI/Dialogs/ModalDialogPage.master"
    Theme="Default" Title="Request details" CodeFile="System_ViewRequest.aspx.cs" %>

<%@ Register Src="~/CMSAdminControls/Debug/QueryLog.ascx" TagName="QueryLog" TagPrefix="cms" %>
<%@ Register Src="~/CMSAdminControls/Debug/CacheLog.ascx" TagName="CacheLog" TagPrefix="cms" %>
<%@ Register Src="~/CMSAdminControls/Debug/ViewState.ascx" TagName="ViewState" TagPrefix="cms" %>
<%@ Register Src="~/CMSAdminControls/Debug/FilesLog.ascx" TagName="FilesLog" TagPrefix="cms" %>
<%@ Register Src="~/CMSAdminControls/Debug/SecurityLog.ascx" TagName="SecurityLog" TagPrefix="cms" %>
<%@ Register Src="~/CMSAdminControls/Debug/MacroLog.ascx" TagName="MacroLog" TagPrefix="cms" %>
<%@ Register Src="~/CMSAdminControls/Debug/RequestLog.ascx" TagName="RequestLog" TagPrefix="cms" %>
<%@ Register Src="~/CMSAdminControls/Debug/WebFarmLog.ascx" TagName="WebFarmLog" TagPrefix="cms" %>
<%@ Register Src="~/CMSAdminControls/Debug/OutputLog.ascx" TagName="OutputLog" TagPrefix="cms" %>
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <asp:Panel runat="server" ID="pnlBody" CssClass="PageContent">
        <div style="width: 97%">
            <asp:PlaceHolder runat="server" ID="plcLogs" EnableViewState="false" />
            <cms:QueryLog ID="logSQL" runat="server" />
            <cms:CacheLog ID="logCache" runat="server" />
            <cms:FilesLog ID="logFiles" runat="server" />
            <cms:ViewState ID="logState" runat="server" />
            <cms:SecurityLog ID="logSec" runat="server" />
            <cms:MacroLog ID="logMac" runat="server" />
            <cms:RequestLog ID="logReq" runat="server" />
            <cms:WebFarmLog ID="logFarm" runat="server" />
            <cms:OutputLog ID="logOutput" runat="server" />
        </div>
    </asp:Panel>
</asp:Content>
<asp:Content ID="cntFooter" ContentPlaceHolderID="plcFooter" runat="server">
    <div class="FloatRight">
        <cms:LocalizedButton ID="btnCancel" runat="server" ResourceString="General.Close"
            CssClass="SubmitButton" OnClientClick="window.close(); return false;" EnableViewState="false" />
    </div>
</asp:Content>
