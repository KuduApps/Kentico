<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_IntranetPortal_Controls_EmployeesFilterControl" CodeFile="~/CMSModules/IntranetPortal/Controls/EmployeesFilterControl.ascx.cs" %>
<asp:Panel CssClass="Filter" DefaultButton="btnSelect" runat="server" ID="pnlUsersFilter">
    <table>
        <tr>
            <td>
                <cms:LocalizedLabel ID="lblValue" runat="server" EnableViewState="false" AssociatedControlID="txtValue"
                    ResourceString="employeesearch.searchexpression" />
            </td>
            <td>
                <cms:CMSTextBox runat="server" ID="txtValue" EnableViewState="false" Width="250" />
                <cms:CMSButton runat="server" ID="btnSelect" OnClick="btnSelect_Click" EnableViewState="false" />
            </td>
        </tr>
            <tr>
                <td>
                    <cms:LocalizedLabel ID="lblDepartment" runat="server" EnableViewState="false" AssociatedControlID="drpDepartment"
                        ResourceString="employeesearch.department" />
                </td>
                <td>
                    <asp:DropDownList ID="drpDepartment" runat="server" AutoPostBack="true" Width="256"
                        onselectedindexchanged="drpDepartment_SelectedIndexChanged" />
                </td>
            </tr>
    </table>
</asp:Panel>
