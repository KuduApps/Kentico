<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_Messaging_Controls_ViewMessage" CodeFile="ViewMessage.ascx.cs" %>
<%@ Register Src="~/CMSModules/Messaging/Controls/MessageUserButtons.ascx" TagName="MessageUserButtons"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSAdminControls/UI/UserPicture.ascx" TagName="UserPicture" TagPrefix="cms" %>
<asp:Label runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false" Visible="false" />
<asp:Panel ID="pnlViewMessage" runat="server" CssClass="ViewMessage" Visible="false">
    <table border="0" cellspacing="0" cellpadding="0" class="HeaderTable">
        <tr>
            <td class="ImageCell" style="vertical-align: top;">
                <cms:UserPicture ID="ucUserPicture" runat="server" Visible="false" Height="60" Width="60"
                    KeepAspectRatio="true" UseDefaultAvatar="true" />
            </td>
            <td style="vertical-align: top;" class="InfoCell">
                <table>
                    <tr>
                        <td class="FieldCaption">
                            <cms:LocalizedLabel ID="lblFromCaption" runat="Server" EnableViewState="false" DisplayColon="true" />
                        </td>
                        <td class="Field">
                            <asp:Label ID="lblFrom" runat="Server" EnableViewState="false" /><cms:RTLFix IsLiveSite="false" runat="server" ID="rtlFix" />
                             <cms:MessageUserButtons
                                ID="ucMessageUserButtons" runat="server" DisplayInline="true" />
                        </td>
                    </tr>
                    <tr>
                        <td class="FieldCaption">
                            <cms:LocalizedLabel ID="lblDateCaption" runat="Server" EnableViewState="false" DisplayColon="true" />
                        </td>
                        <td class="Field">
                            <asp:Label ID="lblDate" runat="Server" EnableViewState="false" />
                        </td>
                    </tr>
                    <tr>
                        <td class="FieldCaption">
                            <cms:LocalizedLabel ID="lblSubjectCaption" runat="Server" EnableViewState="false" DisplayColon="true" />
                        </td>
                        <td class="Field">
                            <asp:Label ID="lblSubject" runat="Server" EnableViewState="false" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="vertical-align: top;">
                <div class="Body">
                    <asp:Label ID="lblBody" runat="server" EnableViewState="false" />
                </div>
            </td>
        </tr>
    </table>
</asp:Panel>
