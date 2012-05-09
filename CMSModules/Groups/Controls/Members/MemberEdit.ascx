<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_Groups_Controls_Members_MemberEdit" CodeFile="MemberEdit.ascx.cs" %>
<%@ Register src="~/CMSAdminControls/UI/UniSelector/UniSelector.ascx" tagname="UniSelector" tagprefix="cms" %>
<%@ Register Src="~/CMSModules/Membership/FormControls/Users/selectuser.ascx" TagName="UserSelector" TagPrefix="cms" %>

<asp:Label runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false"
    Visible="false" />
<cms:LocalizedLabel runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false"
    Visible="false" />
<table style="width: 100%" class="MemberPanel">
    <tr>
        <td style="width: 60%; vertical-align: top;">
            <table>
                <tr>
                    <td>
                        <asp:Label ID="lblFullNameLabel" runat="server" CssClass="FieldLabel" />
                    </td>
                    <td colspan="3">
                        <cms:LocalizedLabel ID="lblFullName" runat="server" />
                        <cms:UserSelector ID="userSelector" runat="server" Visible="false" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblComment" runat="server" CssClass="FieldLabel" />
                    </td>
                    <td colspan="3">
                        <cms:CMSTextBox ID="txtComment" runat="server" TextMode="MultiLine" CssClass="TextAreaField" />
                    </td>
                </tr>
                <asp:PlaceHolder ID="plcEdit" runat="server">
                    <tr>
                        <td>
                            <asp:Label ID="lblMemberJoinedLabel" runat="server" CssClass="FieldLabel" />
                        </td>
                        <td colspan="3">
                            <cms:LocalizedLabel ID="lblMemberJoined" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblMemberApprovedLabel" runat="server" CssClass="FieldLabel" />
                        </td>
                        <td colspan="3">
                            <cms:LocalizedLabel ID="lblMemberApproved" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblMemberRejectedLabel" runat="server" CssClass="FieldLabel" />
                        </td>
                        <td colspan="3">
                            <cms:LocalizedLabel ID="lblMemberRejected" runat="server" />
                        </td>
                    </tr>
                </asp:PlaceHolder>
                <asp:PlaceHolder ID="plcNew" runat="server" Visible="false">
                    <tr>
                        <td>
                            <asp:Label ID="lblMemberApprove" runat="server" CssClass="FieldLabel" />
                        </td>
                        <td colspan="3">
                            <asp:CheckBox ID="chkApprove" runat="server" Checked="true" />
                        </td>
                    </tr>
                </asp:PlaceHolder>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td colspan="3">
                        <cms:CMSButton ID="btnSave" runat="server" CssClass="SubmitButton" /><cms:CMSButton ID="btnApprove" 
                        runat="server" CssClass="SubmitButton" /><cms:CMSButton ID="btnReject" 
                        runat="server" CssClass="SubmitButton" />
                    </td>
                </tr>
            </table>
        </td>
        <td style="vertical-align: top;">
            <asp:Panel ID="pnlRoles" runat="server" Width="100%">
                <cms:CMSUpdatePanel ID="pnlUpdate" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <cms:UniSelector ID="usRoles" runat="server" IsLiveSite="false" ObjectType="cms.role"
                            SelectionMode="Multiple" ResourcePrefix="addroles" />
                    </ContentTemplate>
                </cms:CMSUpdatePanel>
                <cms:LocalizedLabel ID="lblRole" runat="server" ResourceString="group.member.role" />
            </asp:Panel>
        </td>
    </tr>
</table>
