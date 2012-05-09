<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_Friends_Controls_Friends_Approve" CodeFile="Friends_Approve.ascx.cs" %>
<asp:Panel ID="pnlBody" runat="server">
    <cms:LocalizedLabel ID="lblError" CssClass="ErrorLabel" runat="server" EnableViewState="false"
        Visible="false" />
    <cms:LocalizedLabel ID="lblInfo" CssClass="InfoLabel" runat="server" EnableViewState="false"
        Visible="false" />
    <table>
        <tr>
            <td>
                <cms:LocalizedLabel ID="lblComment" runat="server" ResourceString="general.comment"
                    DisplayColon="true" CssClass="FieldLabel" EnableViewState="false" /><br />
                <cms:ExtendedTextArea ID="txtComment" runat="server" CssClass="BodyField" TextMode="MultiLine"
                    Width="380" Height="60" EnableViewState="false" />
            </td>
        </tr>
        <tr>
            <td>
                <cms:LocalizedCheckBox ID="chkSendEmail" Checked="true" runat="server" ResourceString="administration.users.email"
                    CssClass="ContentCheckbox" EnableViewState="false" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <cms:LocalizedCheckBox ID="chkSendMessage" Checked="true" runat="server" ResourceString="sendmessage.sendmessage"
                    CssClass="ContentCheckbox" EnableViewState="false" />
            </td>
        </tr>
    </table>
    <div class="PageFooterLine">
        <div class="FloatRight">
            <cms:LocalizedButton ID="btnApprove" OnClick="btnApprove_Click" runat="server" ResourceString="general.approve"
                CssClass="SubmitButton" EnableViewState="false" /><cms:LocalizedButton ID="btnCancel" OnClientClick="window.close();return false;" runat="server"
                ResourceString="general.cancel" CssClass="SubmitButton" EnableViewState="false" />
        </div>
    </div>
    <asp:Literal ID="ltlScript" runat="server" EnableViewState="false" Visible="false" />
</asp:Panel>
