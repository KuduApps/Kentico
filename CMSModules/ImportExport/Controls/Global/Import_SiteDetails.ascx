<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_ImportExport_Controls_Global_Import_SiteDetails" CodeFile="Import_SiteDetails.ascx.cs" %>

<%@ Register Src="~/CMSFormControls/System/LocalizableTextBox.ascx" TagName="LocalizableTextBox" TagPrefix="cms" %>
<table>
    <tr>
        <td nowrap="nowrap">
            <asp:Label ID="lblSiteDisplayName" runat="server" />
        </td>
        <td>
            <cms:LocalizableTextBox ID="txtSiteDisplayName" runat="server" />
            <cms:CMSRequiredFieldValidator ID="rfvSiteDisplayName" runat="server" ControlToValidate="txtSiteDisplayName:textbox" />
        </td>
    </tr>
    <tr>
        <td nowrap="nowrap">
            <asp:Label ID="lblSiteName" runat="server" />
        </td>
        <td>
            <cms:CMSTextBox ID="txtSiteName" runat="server" />
            <cms:CMSRequiredFieldValidator ID="rfvSiteName" runat="server" ControlToValidate="txtSiteName" />
        </td>
    </tr>
</table>
