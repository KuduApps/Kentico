<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_ImportExport_Controls_NewSiteType" CodeFile="NewSiteType.ascx.cs" %>

<asp:Label runat="server" ID="lblError" CssClass="ErrorLabel" Visible="false" EnableViewState="false" />
<table>
    <tr>
        <td>
            <asp:RadioButton runat="server" ID="radBlank" Checked="true" GroupName="Type" />
        </td>
    </tr>
    <tr>
        <td>
            <asp:RadioButton runat="server" ID="radTemplate" GroupName="Type" />
        </td>
    </tr>
</table>
