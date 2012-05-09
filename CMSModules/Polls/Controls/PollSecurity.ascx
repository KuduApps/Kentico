<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_Polls_Controls_PollSecurity" CodeFile="PollSecurity.ascx.cs" %>
<%@ Register Src="~/CMSModules/Membership/FormControls/Roles/securityAddRoles.ascx" TagName="AddRoles" TagPrefix="cms" %>
<asp:Panel ID="pnlBody" runat="server">
    <asp:Label runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false"
        Visible="false" />
    <div>
        <cms:LocalizedLabel runat="server" ID="lblTitle" CssClass="SectionTitle" ResourceString="Poll_Security.Title"
            EnableViewState="false" Visible="true" />
        <table>
            <tr>
                <td colspan="2">
                    <cms:LocalizedRadioButton ID="radAllUsers" runat="server" GroupName="polls" AutoPostBack="True"
                        ResourceString="Poll_Security.AllUsers" OnCheckedChanged="radAllUsers_CheckedChanged" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <cms:LocalizedRadioButton ID="radOnlyUsers" runat="server" GroupName="polls" AutoPostBack="True"
                        ResourceString="Poll_Security.OnlyUsers" OnCheckedChanged="radOnlyUsers_CheckedChanged" />
                </td>
            </tr>
            <asp:PlaceHolder ID="plcGroupMembers" runat="server" Visible="false">
                <tr>
                    <td colspan="2">
                        <cms:LocalizedRadioButton ID="radGroupMembers" runat="server" GroupName="polls" AutoPostBack="True"
                            ResourceString="Poll_Security.OnlyGroupMembers" OnCheckedChanged="radGroupMembers_CheckedChanged" />
                    </td>
                </tr>
            </asp:PlaceHolder>
            <tr>
                <td colspan="2">
                    <cms:LocalizedRadioButton ID="radOnlyRoles" runat="server" GroupName="polls" AutoPostBack="True"
                        ResourceString="Poll_Security.OnlyRoles" OnCheckedChanged="radOnlyRoles_CheckedChanged" />
                </td>
            </tr>
            <tr>
                <td>
                    <cms:CMSUpdatePanel runat="server" ID="pnlUpdate" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:ListBox ID="lstRoles" runat="server" CssClass="PermissionsListBox" SelectionMode="Multiple" />
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
                                <cms:LocalizedButton ID="btnRemoveRole" runat="server" CssClass="ContentButton" ResourceString="general.remove"
                                    OnClick="btnRemoveRole_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <cms:LocalizedButton ID="btnOk" runat="server" Text="" ResourceString="general.ok"
                        CssClass="SubmitButton" OnClick="btnOk_Click" />
                </td>
            </tr>
        </table>
    </div>
</asp:Panel>
