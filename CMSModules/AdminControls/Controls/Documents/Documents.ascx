<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Documents.ascx.cs" Inherits="CMSModules_AdminControls_Controls_Documents_Documents" %>
<%@ Register Src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" TagName="UniGrid" TagPrefix="cms" %>
<asp:PlaceHolder ID="plcOutdatedFilter" runat="server" Visible="false">
    <table>
        <tr>
            <td>
                <cms:LocalizedLabel ID="lblFilter" AssociatedControlID="txtFilter" runat="server"
                    EnableViewState="false" ResourceString="MyDesk.OutdatedDocuments.Filter" />
            </td>
            <td>
                <cms:CMSTextBox ID="txtFilter" runat="server" CssClass="SmallTextBox" EnableViewState="false" />&nbsp;
                <asp:DropDownList ID="drpFilter" runat="server" CssClass="ContentDropdown" />
            </td>
        </tr>
        <tr>
            <td>
                <cms:LocalizedLabel ID="lblDocumentName" runat="server" EnableViewState="false" ResourceString="general.documentname"
                    DisplayColon="true" CssClass="FieldLabel" />
            </td>
            <td>
                <asp:DropDownList ID="drpDocumentName" runat="server" CssClass="ContentDropdown" />&nbsp;
                <cms:CMSTextBox ID="txtDocumentName" runat="server" CssClass="SmallTextBox" MaxLength="100" />
            </td>
        </tr>
        <tr>
            <td>
                <cms:LocalizedLabel ID="lblDocumentType" runat="server" EnableViewState="false" ResourceString="general.documenttype"
                    DisplayColon="true" CssClass="FieldLabel" />
            </td>
            <td>
                <asp:DropDownList ID="drpDocumentType" runat="server" CssClass="ContentDropdown" />&nbsp;
                <cms:CMSTextBox ID="txtDocumentType" runat="server" CssClass="SmallTextBox" MaxLength="100" />
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <cms:LocalizedButton ID="btnShow" runat="server" EnableViewState="false" CssClass="ContentButton"
                    ResourceString="general.show" />
            </td>
        </tr>
    </table>
</asp:PlaceHolder>
<cms:UniGrid ID="gridElem" runat="server" OrderBy="DocumentName" ShortID="g" />
