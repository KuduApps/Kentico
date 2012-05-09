<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ObjectVersionDialog.aspx.cs"
    Inherits="CMSModules_Objects_Dialogs_ObjectVersionDialog" MasterPageFile="~/CMSMasterPages/UI/Dialogs/ModalDialogPage.master"
    Title="View Object Version" Theme="Default" %>

<%@ Register Src="~/CMSModules/Objects/Controls/ObjectVersionList.ascx" TagPrefix="cms"
    TagName="VersionList" %>
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <asp:Panel ID="pnlContent" runat="server" CssClass="PageContent">
        <cms:VersionList ID="versionList" runat="server" />
    </asp:Panel>
</asp:Content>
<asp:Content ID="cntFooter" ContentPlaceHolderID="plcFooter" runat="server">
    <div class="FloatRight">
        <cms:CMSButton ID="btnClose" runat="server" CssClass="SubmitButton" OnClientClick="window.close(); return false;" />
    </div>
</asp:Content>
