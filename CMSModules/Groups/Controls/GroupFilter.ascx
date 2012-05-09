<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_Groups_Controls_GroupFilter" CodeFile="GroupFilter.ascx.cs" %>
<table>
    <tr>
        <td>
            <cms:LocalizedLabel ID="lblGroupName" ResourceString="groups.groupname" DisplayColon="true" runat="server" CssClass="ContentLabel" EnableViewState="false" />
        </td>
        <td>
            <asp:DropDownList ID="drpGroupName" runat="server" />
        </td>
        <td>
            <cms:CMSTextBox ID="txtGroupName" runat="server" CssClass="SmallTextBox" MaxLength="200" />
        </td>
    </tr>
    <tr>
        <td>
            <cms:LocalizedLabel ID="lblGroupStatus" ResourceString="groups.status" runat="server" DisplayColon="true" CssClass="ContentLabel" EnableViewState="false" />
        </td>
        <td colspan="2">
            <asp:DropDownList ID="drpGroupStatus" runat="server" style="width:100%" />
        </td>        
    </tr>
    <tr>
        <td></td>
        <td colspan="2">
        <cms:LocalizedButton ResourceString="general.search" ID="btnSearch" runat="server" CssClass="ContentButton" EnableViewState="false" />
        </td>        
    </tr>
</table>
