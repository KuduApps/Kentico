<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_SmartSearch_Controls_SearchDialog" CodeFile="SearchDialog.ascx.cs" %>
<asp:Panel  ID="pnlDialog" runat="server" DefaultButton="btnSearch">
<table>
    <tr>
        <td>
            <cms:LocalizedLabel runat="server" ID="lblSearchFor" AssociatedControlID="txtSearchFor" CssClass="FieldLabel" DisplayColon="true" ></cms:LocalizedLabel>
        </td>
        <td>
            <cms:CMSTextBox runat="server" ID="txtSearchFor" CssClass="TextBoxField" MaxLength="1000" ></cms:CMSTextBox >
        </td>
    </tr>
    <asp:PlaceHolder runat="server" ID="plcSearchMode" Visible="true">
        <tr>    
            <td>
            <cms:LocalizedLabel runat="server" ID="lblSearchMode" AssociatedControlID="drpSearchMode" CssClass="FieldLabel" DisplayColon="true" ></cms:LocalizedLabel>
            </td>
            <td>
            <asp:DropDownList runat="server" ID="drpSearchMode" CssClass="DropDownField" ></asp:DropDownList>
            </td>
        </tr>
    </asp:PlaceHolder>
    <tr>
        <td>
        </td>
        <td>
            <cms:LocalizedButton runat="server" ID="btnSearch" CssClass="ContentButton" 
                onclick="btnSearch_Click" />
        </td>
    </tr>
</table>
</asp:Panel>