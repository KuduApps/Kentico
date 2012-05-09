<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Licenses_Pages_License_View"
    Theme="Default" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="Licenses - License View" CodeFile="License_View.aspx.cs" %>

<asp:Content ContentPlaceHolderID="plcContent" runat="server">
    <table>
        <tr style="vertical-align: top">
            <td class="FieldLabel">
                <asp:Label ID="lblLicenseKey" runat="server" EnableViewState="False" />
            </td>
            <td>
                <cms:CMSTextBox ID="lblLicenseKeyContent" runat="server" ReadOnly="True" EnableViewState="false" TextMode="MultiLine"
                    Enabled="False" CssClass="TextAreaHigh" Width="600" />
            </td>
        </tr>
    </table>
</asp:Content>
