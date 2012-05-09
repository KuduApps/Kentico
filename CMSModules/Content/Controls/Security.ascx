<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Security.ascx.cs" Inherits="CMSModules_Content_Controls_Security" %>
<%@ Register Src="~/CMSModules/Membership/FormControls/Roles/securityAddRoles.ascx"
    TagName="AddRoles" TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/Membership/FormControls/Users/securityAddUsers.ascx"
    TagName="AddUsers" TagPrefix="cms" %>
<cms:CMSUpdatePanel ID="pnlUpdate" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <table>
            <asp:PlaceHolder ID="plcSecurityMessage" runat="server" Visible="false">
                <tr>
                    <td colspan="2">
                        <asp:Label ID="lblPermission" runat="server" CssClass="InfoLabel" EnableViewState="false" />
                    </td>
                </tr>
            </asp:PlaceHolder>
            <tr>
                <td style="vertical-align: bottom;">
                    <cms:LocalizedLabel ID="lblUsersRoles" CssClass="FormGroupHeader" runat="server"
                        EnableViewState="false" ResourceString="Security.UsersRoles" />
                </td>
                <td style="vertical-align: bottom;">
                    <asp:Label ID="lblInfo" runat="server" CssClass="InfoLabel" EnableViewState="false" />
                    <br runat="server" id="infoseparator" />
                    <asp:Label ID="lblError" runat="server" CssClass="ErrorLabel" EnableViewState="false" />
                    <cms:LocalizedLabel ID="lblAccessRights" CssClass="FormGroupHeader" runat="server"
                        EnableViewState="false" ResourceString="Security.AccessRights" />
                </td>
            </tr>
            <tr>
                <td colspan="2" style="vertical-align: top;">
                    <asp:Panel ID="pnlFilter" runat="server" DefaultButton="btnFilter">
                        <cms:CMSTextBox ID="txtFilter" runat="server" CssClass="SelectorTextBox" Width="180" />&nbsp;<cms:LocalizedButton
                            ID="btnFilter" runat="server" CssClass="ContentButton" ResourceString="general.search" />
                    </asp:Panel>
                </td>
            </tr>
            <tr style="vertical-align: top;">
                <td style="width: 300px;">
                    <asp:ListBox ID="lstOperators" runat="server" AutoPostBack="True" CssClass="PermissionsListBox"
                        OnSelectedIndexChanged="lstOperators_SelectedIndexChanged" />
                    <br />
                    <br />
                    <table style="border: collapse;" cellpadding="0" cellspacing="0" width="280">
                        <tr>
                            <td>
                                <cms:AddUsers ID="addUsers" runat="server" />
                            </td>
                            <td>
                                <cms:AddRoles ID="addRoles" runat="server" />
                            </td>
                            <td>
                                <cms:LocalizedButton ID="btnRemoveOperator" runat="server" CssClass="ContentButton"
                                    ResourceString="general.remove" OnClick="btnRemoveOperator_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="text-align: right; vertical-align: middle;">
                    <asp:Panel ID="pnlAccessRights" runat="server">
                        <table class="UniGridGrid" cellpadding="3" cellspacing="0" border="1" style="border-collapse: collapse;"
                            rules="rows">
                            <tr class="UniGridHead">
                                <th style="width: 100px;">
                                    &nbsp;
                                </th>
                                <th style="width: 50px;" class="TableCell">
                                    <cms:LocalizedLabel ID="lblAllow" runat="server" EnableViewState="false" ResourceString="Security.Allow" />
                                </th>
                                <th style="width: 50px;" class="TableCell">
                                    <cms:LocalizedLabel ID="lblDeny" runat="server" EnableViewState="false" ResourceString="Security.Deny" />
                                </th>
                            </tr>
                            <tr class="EvenRow">
                                <td class="TableRowHeader">
                                    <cms:LocalizedLabel ID="lblFullControl" runat="server" EnableViewState="false" ResourceString="Security.FullControl" />
                                </td>
                                <td class="TableCell">
                                    <asp:CheckBox ID="chkFullControlAllow" runat="server" AutoPostBack="false" />
                                </td>
                                <td class="TableCell">
                                    <asp:CheckBox ID="chkFullControlDeny" runat="server" AutoPostBack="false" />
                                </td>
                            </tr>
                            <tr class="OddRow">
                                <td class="TableRowHeader">
                                    <cms:LocalizedLabel ID="lblRead" runat="server" EnableViewState="false" ResourceString="Security.Read" />
                                </td>
                                <td class="TableCell">
                                    <asp:CheckBox ID="chkReadAllow" runat="server" AutoPostBack="false" />
                                </td>
                                <td class="TableCell">
                                    <asp:CheckBox ID="chkReadDeny" runat="server" AutoPostBack="false" />
                                </td>
                            </tr>
                            <tr class="EvenRow">
                                <td class="TableRowHeader">
                                    <cms:LocalizedLabel ID="lblModify" runat="server" EnableViewState="false" ResourceString="Security.Modify" />
                                </td>
                                <td class="TableCell">
                                    <asp:CheckBox ID="chkModifyAllow" runat="server" AutoPostBack="false" />
                                </td>
                                <td class="TableCell">
                                    <asp:CheckBox ID="chkModifyDeny" runat="server" AutoPostBack="false" />
                                </td>
                            </tr>
                            <tr class="OddRow">
                                <td class="TableRowHeader">
                                    <cms:LocalizedLabel ID="lblCreate" runat="server" EnableViewState="false" ResourceString="Security.Create" />
                                </td>
                                <td class="TableCell">
                                    <asp:CheckBox ID="chkCreateAllow" runat="server" AutoPostBack="false" />
                                </td>
                                <td class="TableCell">
                                    <asp:CheckBox ID="chkCreateDeny" runat="server" AutoPostBack="false" />
                                </td>
                            </tr>
                            <tr class="EvenRow">
                                <td class="TableRowHeader">
                                    <cms:LocalizedLabel ID="lblDelete" runat="server" EnableViewState="false" ResourceString="general.delete" />
                                </td>
                                <td class="TableCell">
                                    <asp:CheckBox ID="chkDeleteAllow" runat="server" AutoPostBack="false" />
                                </td>
                                <td class="TableCell">
                                    <asp:CheckBox ID="chkDeleteDeny" runat="server" AutoPostBack="false" />
                                </td>
                            </tr>
                            <tr class="OddRow">
                                <td class="TableRowHeader">
                                    <cms:LocalizedLabel ID="lblDestroy" runat="server" EnableViewState="false" ResourceString="Security.Destroy" />
                                </td>
                                <td class="TableCell">
                                    <asp:CheckBox ID="chkDestroyAllow" runat="server" AutoPostBack="false" />
                                </td>
                                <td class="TableCell">
                                    <asp:CheckBox ID="chkDestroyDeny" runat="server" AutoPostBack="false" />
                                </td>
                            </tr>
                            <tr class="EvenRow">
                                <td class="TableRowHeader">
                                    <cms:LocalizedLabel ID="lblExploreTree" runat="server" EnableViewState="false" ResourceString="Security.ExploreTree" />
                                </td>
                                <td class="TableCell">
                                    <asp:CheckBox ID="chkExploreTreeAllow" runat="server" AutoPostBack="false" />
                                </td>
                                <td class="TableCell">
                                    <asp:CheckBox ID="chkExploreTreeDeny" runat="server" AutoPostBack="false" />
                                </td>
                            </tr>
                            <tr class="OddRow">
                                <td class="TableRowHeader">
                                    <cms:LocalizedLabel ID="lblManagePermissions" runat="server" EnableViewState="false"
                                        ResourceString="Security.ManagePermissions" />
                                </td>
                                <td class="TableCell">
                                    <asp:CheckBox ID="chkManagePermissionsAllow" runat="server" AutoPostBack="false" />
                                </td>
                                <td class="TableCell">
                                    <asp:CheckBox ID="chkManagePermissionsDeny" runat="server" AutoPostBack="false" />
                                </td>
                            </tr>
                        </table>
                        <br />
                        <cms:LocalizedButton ID="btnOk" runat="server" CssClass="SubmitButton" OnClick="btnOK_Click"
                            EnableViewState="false" ResourceString="general.ok" />
                    </asp:Panel>
                </td>
            </tr>
        </table>
    </ContentTemplate>
</cms:CMSUpdatePanel>
