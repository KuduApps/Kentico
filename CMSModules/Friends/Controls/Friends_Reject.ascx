<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_Friends_Controls_Friends_Reject" CodeFile="Friends_Reject.ascx.cs" %>
<asp:Panel ID="pnlBody" runat="server">
    <cms:LocalizedLabel ID="lblError" CssClass="ErrorLabel" runat="server" EnableViewState="false"
        Visible="false" />
    <cms:LocalizedLabel ID="lblInfo" CssClass="InfoLabel" runat="server" EnableViewState="false"
        Visible="false" />
    <table>
        <tr>
            <td>
                <cms:LocalizedLabel ID="lblComment" runat="server" ResourceString="general.comment"
                    DisplayColon="true" CssClass="FieldLabel" /><br />
                <cms:ExtendedTextArea ID="txtComment" runat="server" CssClass="BodyField" TextMode="MultiLine"
                    Width="380" Height="60" />
            </td>
        </tr>
        <tr>
            <td>
                <cms:LocalizedCheckBox ID="chkSendEmail" runat="server" ResourceString="administration.users.email"
                    CssClass="ContentCheckbox" Checked="true" />
            </td>
        </tr>
        <tr>
            <td>
                <cms:LocalizedCheckBox ID="chkSendMessage" runat="server" ResourceString="sendmessage.sendmessage"
                    CssClass="ContentCheckbox" Checked="true" />
            </td>
        </tr>
    </table>
    <div class="PageFooterLine">
        <div class="FloatRight">
            <cms:LocalizedButton ID="btnReject" OnClick="btnReject_Click" runat="server" ResourceString="general.reject"
                CssClass="SubmitButton" /><cms:LocalizedButton ID="btnCancel" OnClientClick="window.close();return false;" runat="server"
                ResourceString="general.cancel" CssClass="SubmitButton" />
        </div>
    </div>
    <asp:Literal ID="ltlScript" runat="server" EnableViewState="false" Visible="false" />
</asp:Panel>
