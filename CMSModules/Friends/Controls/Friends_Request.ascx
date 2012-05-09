<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_Friends_Controls_Friends_Request" CodeFile="Friends_Request.ascx.cs" %>
<%@ Register Src="~/CMSModules/Membership/FormControls/Users/selectuser.ascx" TagName="selectuser" TagPrefix="cms" %>
<asp:Panel ID="pnlBody" runat="server">
    <cms:LocalizedLabel ID="lblError" CssClass="ErrorLabel" runat="server" EnableViewState="false"
        Visible="false" />
    <cms:LocalizedLabel ID="lblInfo" CssClass="InfoLabel" runat="server" EnableViewState="false"
        Visible="false" />
    <table>
        <asp:PlaceHolder ID="plcUserSelect" runat="server">
            <tr>
                <td>
                    <cms:LocalizedLabel ID="lblUser" runat="server" ResourceString="general.user" DisplayColon="true"
                        EnableViewState="false" />
                </td>
                <td>
                    <cms:selectuser ID="selectUser" runat="server" Visible="true" />
                </td>
            </tr>
        </asp:PlaceHolder>
        <tr>
            <td style="vertical-align: top;">
                <cms:LocalizedLabel ID="lblComment" runat="server" ResourceString="general.comment"
                    DisplayColon="true" CssClass="FieldLabel" EnableViewState="false" />
            </td>
            <td>
                <cms:ExtendedTextArea ID="txtComment" runat="server" CssClass="BodyField" TextMode="MultiLine"
                    Width="386" Height="100" EnableViewState="false" />
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                <cms:LocalizedCheckBox ID="chkSendEmail" Checked="true" runat="server" ResourceString="administration.users.email"
                    CssClass="ContentCheckbox" EnableViewState="false" />
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                <cms:LocalizedCheckBox ID="chkSendMessage" runat="server" Checked="true" ResourceString="sendmessage.sendmessage"
                    CssClass="ContentCheckbox" EnableViewState="false" />
            </td>
        </tr>
        <asp:PlaceHolder ID="plcAdministrator" runat="server" Visible="false" EnableViewState="false">
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    <cms:LocalizedCheckBox ID="chkAutomaticApprove" runat="server" Checked="false" ResourceString="friends.automaticapproval"
                        CssClass="ContentCheckbox" EnableViewState="false" />
                </td>
            </tr>
        </asp:PlaceHolder>
    </table>
    <div class="PageFooterLine">
        <div class="FloatRight">
            <cms:LocalizedButton ID="btnRequest" OnClick="btnRequest_Click" runat="server" ResourceString="general.request"
                CssClass="SubmitButton" EnableViewState="false" /><cms:LocalizedButton ID="btnCancel" OnClientClick="window.close();return false;" runat="server"
                ResourceString="general.cancel" CssClass="SubmitButton" EnableViewState="false" />
        </div>
    </div>
    <asp:Literal ID="ltlScript" runat="server" EnableViewState="false" Visible="false" />
</asp:Panel>
