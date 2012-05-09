<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master"
    Title="Bizform - Security" Inherits="CMSModules_BizForms_Tools_BizForm_Edit_Security"
    Theme="Default" CodeFile="BizForm_Edit_Security.aspx.cs" %>

<%@ Register Src="~/CMSModules/Membership/FormControls/Roles/securityAddRoles.ascx"
    TagName="AddRoles" TagPrefix="cms" %>
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <asp:Literal ID="ltlScript" runat="server" EnableViewState="false" />
    <asp:Label runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false"
        Visible="false" />
    <cms:LocalizedLabel runat="server" ID="lblTitle" CssClass="SectionTitle" EnableViewState="false"
        Visible="true" ResourceString="Bizform.Security.lblTitle" />
    <table>
        <tr>
            <td colspan="2">
                <cms:LocalizedRadioButton ID="radAllUsers" runat="server" GroupName="form" AutoPostBack="True"
                    ResourceString="Bizform.Security.lblAllUsers" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <cms:LocalizedRadioButton ID="radOnlyRoles" runat="server" GroupName="form" AutoPostBack="True"
                    OnCheckedChanged="radOnlyRoles_CheckedChanged" ResourceString="Bizform.Security.lblOnlyRoles" />
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
                            <cms:AddRoles runat="server" ID="addRoles" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <cms:LocalizedButton ID="btnRemoveRole" runat="server" CssClass="ContentButton"
                                OnClick="btnRemoveRole_Click" ResourceString="general.remove" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <cms:LocalizedButton ID="btnOk" runat="server" CssClass="SubmitButton" OnClick="btnOk_Click"
                    ResourceString="general.ok" />
            </td>
        </tr>
    </table>
</asp:Content>
