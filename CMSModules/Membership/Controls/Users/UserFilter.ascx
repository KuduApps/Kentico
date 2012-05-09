<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_Membership_Controls_Users_UserFilter" CodeFile="UserFilter.ascx.cs" %>
<%@ Register Src="~/CMSModules/Membership/FormControls/Roles/selectrole.ascx" TagName="SelectRole"
    TagPrefix="uc1" %>
<%@ Register Src="~/CMSAdminControls/UI/UniGrid/Filters/TextSimpleFilter.ascx" TagName="TextSimpleFilter"
    TagPrefix="cms" %>
<%--<%@ Register Src="~/CMSModules/Groups/FormControls/MembershipGroupSelector.ascx" TagName="SelectGroup"
    TagPrefix="uc1" %>--%>
<div>
    <asp:Panel ID="pnlAlphabet" CssClass="AlphabetFilter" runat="server" />
</div>
<br />
<asp:Panel ID="pnlSimpleFilter" runat="server" DefaultButton="btnSimpleSearch">
    <cms:CMSTextBox ID="txtSearch" runat="server" CssClass="VeryLongTextBox" MaxLength="450" /><cms:CMSButton
        ID="btnSimpleSearch" runat="server" CssClass="ContentButton" />
    <br />
    <br />
    <div>
        <asp:Image runat="server" ID="imgShowAdvancedFilter" CssClass="NewItemImage" />
        <asp:LinkButton ID="lnkShowAdvancedFilter" runat="server" OnClick="lnkShowAdvancedFilter_Click" />
    </div>
</asp:Panel>
<asp:Panel ID="pnlAdvancedFilter" runat="server" DefaultButton="btnAdvancedSearch">
    <table cellpadding="0" cellspacing="2" class="UserFilter">
        <tr>
            <td>
                <cms:LocalizedLabel ID="lblUserName" runat="server" CssClass="ContentLabel" ResourceString="general.username"
                    DisplayColon="true" />
            </td>
            <td colspan="2">
                <cms:TextSimpleFilter ID="fltUserName" runat="server" Column="UserName" Size="100" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblFullName" runat="server" CssClass="ContentLabel" />
            </td>
            <td colspan="2">
                <cms:TextSimpleFilter ID="fltFullName" runat="server" Column="FullName" Size="450" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblEmail" runat="server" CssClass="ContentLabel" />
            </td>
            <td colspan="2">
                <cms:TextSimpleFilter ID="fltEmail" runat="server" Column="Email" Size="100" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblNickName" runat="server" CssClass="ContentLabel" />
            </td>
            <td colspan="2">
                <cms:TextSimpleFilter ID="fltNickName" runat="server" Column="UserNickName" Size="200" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblInRoles" runat="server" CssClass="ContentLabel" />
            </td>
            <td>
                <asp:DropDownList CssClass="ExtraSmallDropDown" runat="server" ID="drpTypeSelectInRoles" />
            </td>
            <td>
                <uc1:SelectRole UserFriendlyMode="true" IsLiveSite="false" ID="selectRoleElem" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblNotInRoles" runat="server" CssClass="ContentLabel" />
            </td>
            <td>
                <asp:DropDownList CssClass="ExtraSmallDropDown" runat="server" ID="drpTypeSelectNotInRoles" />
            </td>
            <td>
                <uc1:SelectRole UserFriendlyMode="true" IsLiveSite="false" ID="selectNotInRole" runat="server" />
            </td>
        </tr>
        <asp:PlaceHolder ID="plcGroups" runat="server" Visible="false">
            <tr>
                <td>
                    <asp:Label ID="lblInGroups" runat="server" CssClass="ContentLabel" />
                </td>
                <td>
                    <asp:DropDownList CssClass="ExtraSmallDropDown" runat="server" ID="drpTypeSelectInGroups" />
                </td>
                <td>
                    <asp:PlaceHolder runat="server" ID="plcSelectInGroups" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblNotInGroups" runat="server" CssClass="ContentLabel" />
                </td>
                <td>
                    <asp:DropDownList CssClass="ExtraSmallDropDown" runat="server" ID="drpTypeSelectNotInGroups" />
                </td>
                <td>
                    <asp:PlaceHolder runat="server" ID="plcSelectNotInGroups" />
                </td>
            </tr>
        </asp:PlaceHolder>
        <tr>
            <td>
            </td>
            <td>
                <cms:CMSButton ID="btnAdvancedSearch" runat="server" CssClass="ContentButton" />
            </td>
        </tr>
    </table>
    <br />
    <div>
        <asp:Image runat="server" ID="imgShowSimpleFilter" CssClass="NewItemImage" />
        <asp:LinkButton ID="lnkShowSimpleFilter" runat="server" OnClick="lnkShowSimpleFilter_Click" />
    </div>
</asp:Panel>
<asp:HiddenField ID="hdnAlpha" runat="server" />
