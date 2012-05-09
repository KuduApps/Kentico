<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_MessageBoards_Controls_Boards_BoardSecurity" CodeFile="BoardSecurity.ascx.cs" %>
<%@ Register Src="~/CMSModules/Membership/FormControls/Roles/securityAddRoles.ascx" TagName="AddRoles" TagPrefix="cms" %>

<asp:Label runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false"
    Visible="false" />
<span style="font-weight: bold;">
    <cms:LocalizedLabel runat="server" ID="lblTitleGeneral" CssClass="SectionTitle" EnableViewState="false"
        Visible="true" DisplayColon="true" /></span><br />
<asp:CheckBox runat="server" ID="chkUseCaptcha" CssClass="ContentCheckBox" />
<br />
<br />
<asp:Label runat="server" ID="lblTitleComments" CssClass="SectionTitle" EnableViewState="false"
    Visible="true" />
<table>
    <tr>
        <td colspan="2">
            <asp:RadioButton ID="radAllUsers" runat="server" GroupName="board" AutoPostBack="True"
                OnCheckedChanged="radAllUsers_CheckedChanged" EnableViewState="false" />
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:RadioButton ID="radOnlyUsers" runat="server" GroupName="board" AutoPostBack="True"
                OnCheckedChanged="radOnlyUsers_CheckedChanged" EnableViewState="false" />
        </td>
    </tr>
    <asp:PlaceHolder ID="plcGroupMembers" runat="server">
        <tr>
            <td colspan="2">
                <asp:RadioButton ID="radGroupMembers" runat="server" GroupName="board" AutoPostBack="True"
                    OnCheckedChanged="radGroupMembers_CheckedChanged" EnableViewState="false" />
            </td>
        </tr>
    </asp:PlaceHolder>
    <asp:PlaceHolder ID="plcOnlyOwner" runat="server">
        <tr>
            <td colspan="2">
                <asp:RadioButton ID="radOnlyOwner" runat="server" GroupName="board" AutoPostBack="True"
                    OnCheckedChanged="radOnlyOwner_CheckedChanged" EnableViewState="false" />
            </td>
        </tr>
    </asp:PlaceHolder>
    <asp:PlaceHolder ID="plcOnlyGroupAdmin" runat="server">
        <tr>
            <td colspan="2">
                <asp:RadioButton ID="radOnlyGroupAdmin" runat="server" GroupName="board" AutoPostBack="True"
                    OnCheckedChanged="radOnlyGroupAdmin_CheckedChanged" />
            </td>
        </tr>
    </asp:PlaceHolder>
    <tr>
        <td colspan="2">
            <asp:RadioButton ID="radOnlyRoles" runat="server" GroupName="board" AutoPostBack="True"
                OnCheckedChanged="radOnlyRoles_CheckedChanged" EnableViewState="false" />
        </td>
    </tr>
    <tr>
        <td>
            <cms:CMSUpdatePanel runat="server" ID="pnlUpdate" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:ListBox ID="lstRoles" runat="server" CssClass="PermissionsListBox" SelectionMode="Multiple"
                        DataTextField="RoleDisplayName" DataValueField="RoleID" />
                </ContentTemplate>
            </cms:CMSUpdatePanel>
        </td>
        <td style="vertical-align: top;">
            <table cellspacing="0" cellpadding="1">
                <tr>
                    <td>
                        <cms:AddRoles ID="addRoles" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <cms:CMSButton ID="btnRemoveRole" runat="server" Text="" CssClass="ContentButton"
                            OnClick="btnRemoveRole_Click" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <cms:CMSButton ID="btnOk" runat="server" Text="" CssClass="SubmitButton" OnClick="btnOk_Click"
                EnableViewState="false" />
        </td>
    </tr>
</table>
