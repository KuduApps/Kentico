<%@ Page Language="C#" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" AutoEventWireup="true"
    Inherits="CMSModules_SystemTables_Pages_Development_Views_ViewEdit_Preview" Title="View - Preview"
    Theme="Default" CodeFile="ViewEdit_Preview.aspx.cs" %>

<%@ Register Src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" TagName="UniGrid" TagPrefix="cms" %>
<asp:Content ID="Content1" ContentPlaceHolderID="plcContent" runat="Server">
    <asp:Label ID="lblError" runat="server" Visible="false" CssClass="ErrorLabel" EnableViewState="false" />
    <table cellspacing="0" cellpadding="0">
        <tr>
            <td>
                <cms:LocalizedLabel ID="lblShowItmes" runat="server" Visible="true" CssClass="FieldLabel"
                    ResourceString="systbl.views.numberofitems" DisplayColon="true" />
            </td>
            <td>
                <asp:DropDownList ID="drpItems" runat="server" AutoPostBack="true" />
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <cms:LocalizedLabel ID="lblData" runat="server" Visible="true" CssClass="FieldLabel"
                    ResourceString="general.data" DisplayColon="true" EnableViewState="false" />
            </td>
            <td>
            </td>
        </tr>
    </table>
    <br />
    <cms:LocalizedLabel ID="lblNoDataFound" runat="server" Visible="false" CssClass="FieldLabel"
        ResourceString="general.nodatafound" EnableViewState="false" />
    <asp:GridView ID="grdData" runat="server" AllowSorting="false" CellPadding="3" CssClass="UniGridGrid"
        EnableViewState="false" GridLines="Horizontal">
        <HeaderStyle HorizontalAlign="Left" CssClass="UniGridHead" />
    </asp:GridView>
</asp:Content>
