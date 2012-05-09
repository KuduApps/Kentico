<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_Messaging_Controls_SendMessage" CodeFile="SendMessage.ascx.cs" %>
<%@ Register Src="~/CMSModules/Messaging/FormControls/MessageUserSelector.ascx" TagName="MessageUserSelector"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSAdminControls/UI/UserPicture.ascx" TagName="UserPicture" TagPrefix="cms" %>
<%@ Register TagPrefix="cms" Namespace="CMS.ExtendedControls" Assembly="CMS.ExtendedControls" %>
<asp:Panel ID="pnlSendMessage" runat="server" CssClass="SendMessage">
    <asp:Label ID="lblSendInfo" runat="server" Visible="false" CssClass="InfoLabel" EnableViewState="false" />
    <asp:Label ID="lblSendError" runat="server" Visible="false" CssClass="ErrorLabel"
        EnableViewState="false" />
    <table border="0" cellspacing="0" cellpadding="0" class="HeaderTable">
        <tr>
            <td class="FieldCaption">
                <cms:LocalizedLabel ID="lblFromCaption" runat="server" ResourceString="Messaging.From"
                    EnableViewState="false" DisplayColon="true" />
            </td>
            <td class="Field">
                <asp:Label ID="lblFrom" runat="server" EnableViewState="false" /><cms:RTLfix IsLiveSite="false"
                    runat="server" ID="rtlFix" />
                <cms:CMSTextBox ID="txtFrom" runat="server" Visible="false" CssClass="FromField" EnableViewState="false"
                    MaxLength="200" />
            </td>
        </tr>
        <tr>
            <td class="FieldCaption">
                <cms:LocalizedLabel ID="lblToCaption" runat="server" ResourceString="Messaging.To"
                    EnableViewState="false" DisplayColon="true" />
            </td>
            <td class="Field">
                <asp:Label ID="lblTo" runat="server" EnableViewState="false" />
                <cms:MessageUserSelector ID="ucMessageUserSelector" runat="server" Visible="false"
                    EnableViewState="false" />
            </td>
        </tr>
    </table>
    <div class="SubjectRow">
        <cms:LocalizedLabel ID="lblSubjectCaption" runat="Server" CssClass="FieldCaption"
            EnableViewState="false" ResourceString="general.subject" AssociatedControlID="txtSubject"
            DisplayColon="true" />
        <br />
        <cms:CMSTextBox ID="txtSubject" runat="server" CssClass="SubjectField" EnableViewState="false"
            MaxLength="200" />
    </div>
    <div class="Body">
        <cms:LocalizedLabel ID="lblText" runat="server" Display="false" EnableViewState="false"
            ResourceString="messaging.body" />
        <cms:BBEditor ID="ucBBEditor" runat="server" EnableViewState="false" />
    </div>
    <asp:Panel ID="pnlButtons" runat="server" CssClass="Buttons">
        <asp:Panel ID="pnlSubPanel" runat="server">
            <cms:LocalizedButton ID="btnSendMessage" runat="server" CssClass="SubmitButton" ResourceString="general.send"
                EnableViewState="false" OnClick="btnSendMessage_Click" /><cms:LocalizedButton ID="btnClose"
                    runat="server" CssClass="SubmitButton" Visible="false" ResourceString="general.cancel"
                    EnableViewState="false" OnClick="btnClose_Click" />
        </asp:Panel>
    </asp:Panel>
</asp:Panel>
<asp:Panel ID="pnlNoUser" runat="server" Visible="false" EnableViewState="false">
    <asp:Label ID="lblNoUser" CssClass="Info" runat="server" EnableViewState="false" />
</asp:Panel>
<asp:HiddenField ID="hdnUserId" runat="server" />
