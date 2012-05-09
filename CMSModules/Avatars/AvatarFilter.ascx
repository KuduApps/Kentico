<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_Avatars_AvatarFilter" CodeFile="AvatarFilter.ascx.cs" %>
<table>
    <tr>
        <td>
            <cms:LocalizedLabel ID="lblAvatarName"  DisplayColon="true" runat="server" ResourceString="avat.avatarname" CssClass="ContentLabel" EnableViewState="false" />
        </td>
        <td>
            <asp:DropDownList ID="drpAvatarName" runat="server" />
        </td>
        <td>
            <cms:CMSTextBox ID="txtAvatarName" runat="server" CssClass="SmallTextBox" MaxLength="200" />
        </td>
    </tr>
    <tr>
        <td>
            <cms:LocalizedLabel ID="lblAvatarType" DisplayColon="true" ResourceString="avat.avatartype" runat="server" CssClass="ContentLabel" EnableViewState="false" />
        </td>
        <td colspan="2">
            <asp:DropDownList ID="drpAvatarType" runat="server" style="width:100%" />
        </td>        
    </tr>
    <tr>
        <td>
            <cms:LocalizedLabel ID="lblAvatarKind" ResourceString="avat.avatarkind" runat="server" DisplayColon="true" CssClass="ContentLabel" EnableViewState="false" />
        </td>
        <td colspan="2">
            <asp:DropDownList ID="drpAvatarKind" runat="server" style="width:100%" />
        </td>        
    </tr>
    <tr>
        <td></td>
        <td colspan="2">
        <cms:LocalizedButton ID="btnSearch" ResourceString="General.Search" runat="server" CssClass="ContentButton" EnableViewState="false" />
        </td>        
    </tr>
</table>
