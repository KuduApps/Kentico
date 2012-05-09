<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSFormControls_System_AutoResizeConfiguration"
    CodeFile="AutoResizeConfiguration.ascx.cs" %>
<table style="width: 297px">
    <tr>
        <td colspan="2">
            <cms:LocalizedDropDownList ID="drpSettings" runat="server" UseResourceStrings="true"
                CssClass="DropDownField">
            </cms:LocalizedDropDownList>
        </td>
    </tr>
    <tr>
        <td style="width: 142px;">
            <cms:LocalizedLabel ID="lblWidth" runat="server" EnableViewState="false" ResourceString="dialogs.resize.width" />
        </td>
        <td>
            <cms:CMSTextBox ID="txtWidth" runat="server" CssClass="SmallTextBox" />
        </td>
    </tr>
    <tr>
        <td>
            <cms:LocalizedLabel ID="lblHeight" runat="server" EnableViewState="false" ResourceString="dialogs.resize.height" />
        </td>
        <td>
            <cms:CMSTextBox ID="txtHeight" runat="server" CssClass="SmallTextBox" />
        </td>
    </tr>
    <tr>
        <td style="white-space: nowrap;">
            <cms:LocalizedLabel ID="lblMax" runat="server" EnableViewState="false" ResourceString="dialogs.resize.maxsidesize" />
        </td>
        <td>
            <cms:CMSTextBox ID="txtMax" runat="server" CssClass="SmallTextBox" />
        </td>
    </tr>
</table>
<asp:Literal ID="ltlScript" runat="server" EnableViewState="false" />