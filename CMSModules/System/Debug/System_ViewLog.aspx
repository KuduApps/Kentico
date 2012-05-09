<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_System_Debug_System_ViewLog"
    Theme="Default" MasterPageFile="~/CMSMasterPages/UI/Dialogs/ModalDialogPage.master"
    MaintainScrollPositionOnPostback="true" CodeFile="System_ViewLog.aspx.cs" %>

<%@ Register Src="~/CMSAdminControls/UI/PageElements/PageTitle.ascx" TagName="PageTitle"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSAdminControls/AsyncControl.ascx" TagName="AsyncControl" TagPrefix="cms" %>
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <asp:Panel runat="server" ID="pnlBody">
        <asp:Panel runat="server" ID="pnlError" CssClass="PageContent" Visible="false">
            <asp:Label ID="lblError" runat="server" CssClass="ErrorLabel" EnableViewState="false" />
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlLog" Visible="false">
            <asp:Panel ID="pnlCancel" runat="server" CssClass="PageHeaderLine">
                <cms:CMSButton runat="server" ID="btnCancel" CssClass="SubmitButton" 
                    onclick="btnCancel_Click" />
            </asp:Panel>
            <asp:Panel ID="pnlAsyncContent" runat="server" CssClass="PageContent">
                <cms:AsyncControl ID="ctlAsync" runat="server" />
            </asp:Panel>
        </asp:Panel>
    </asp:Panel>
</asp:Content>
<asp:Content ID="cntFooter" ContentPlaceHolderID="plcFooter" runat="server">
    <div class="FloatRight">
        <cms:LocalizedButton ID="btnClose" runat="server" ResourceString="General.Close"
            CssClass="SubmitButton" OnClientClick="window.close(); return false;" EnableViewState="false" />
    </div>
</asp:Content>
