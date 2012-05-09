<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_BadWords_BadWords_List"
    Theme="Default" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="Bad words - List" CodeFile="BadWords_List.aspx.cs" %>
<%@ Register src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" tagname="UniGrid" tagprefix="cms" %>

<%@ Register Src="~/CMSModules/BadWords/FormControls/SelectBadWordAction.ascx" TagPrefix="cms"
    TagName="SelectBadWordAction" %>
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <table>
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel ID="lblExpression" runat="server" ResourceString="Unigrid.BadWords.Columns.WordExpression"
                    DisplayColon="true" EnableViewState="false" />
            </td>
            <td>
                <cms:CMSTextBox ID="txtExpression" runat="server" CssClass="TextBoxField" MaxLength="50" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel ID="lblAction" runat="server" ResourceString="Unigrid.BadWords.Columns.WordAction"
                    DisplayColon="true" EnableViewState="false" />
            </td>
            <td>
                <cms:SelectBadWordAction ID="ucBadWordAction" runat="server" AllowNoSelection="true" ReloadDataOnPostback="false" />
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <cms:LocalizedButton ID="btnShow" runat="server" ResourceString="general.show" CssClass="ContentButton" />
            </td>
        </tr>
    </table>
    <br />
    <cms:UniGrid runat="server" ID="UniGrid" GridName="BadWords_List.xml" IsLiveSite="false" />
</asp:Content>
