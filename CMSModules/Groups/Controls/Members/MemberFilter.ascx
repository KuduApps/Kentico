<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_Groups_Controls_Members_MemberFilter" CodeFile="MemberFilter.ascx.cs" %>
<table>
    <tr>
        <td>
            <cms:localizedlabel id="lblMemberName" associatedcontrolid="txtMemberName" resourcestring="editroleusers.username"
                displaycolon="true" runat="server" cssclass="ContentLabel" enableviewstate="false" />
        </td>
        <td>
            <cms:localizedlabel id="lblMemberDDL" associatedcontrolid="drpMemberName" resourcestring="editroleusers.username"
                runat="server" enableviewstate="false" display="false" />
            <asp:DropDownList ID="drpMemberName" runat="server" />
        </td>
        <td>
            <cms:CMSTextBox ID="txtMemberName" runat="server" CssClass="SmallTextBox" MaxLength="100" />
        </td>
    </tr>
    <tr>
        <td>
            <cms:localizedlabel id="lblMemberStatus" associatedcontrolid="drpMemberStatus" resourcestring="groups.status"
                runat="server" displaycolon="true" cssclass="ContentLabel" enableviewstate="false" />
        </td>
        <td colspan="2">
            <asp:DropDownList ID="drpMemberStatus" runat="server" Style="width: 100%" />
        </td>
    </tr>
    <tr>
        <td>
        </td>
        <td colspan="2">
            <cms:localizedbutton resourcestring="general.search" id="btnSearch" runat="server"
                cssclass="ContentButton" enableviewstate="false" />
        </td>
    </tr>
</table>
