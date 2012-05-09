<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_ImportExport_Controls_Global_Import_Documents" CodeFile="Import_Documents.ascx.cs" %>
<table>
    <tr>
        <td>
            <asp:RadioButton ID="radImportDoc" runat="server" GroupName="coupleDoc" Checked="True" />
        </td>
    </tr>
    <tr>
        <td>
            <asp:RadioButton ID="radDoNotImportDoc" runat="server" GroupName="coupleDoc" />
        </td>
    </tr>
</table>
