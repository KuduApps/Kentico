<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Licenses_Pages_License_New"
    Theme="Default" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="Licenses - New License" CodeFile="License_New.aspx.cs" %>

<asp:Content ContentPlaceHolderID="plcContent" runat="server">
    <asp:Label ID="lblInfo" runat="server" />
    <asp:Label runat="server" ID="lblError" ForeColor="red" EnableViewState="false" Visible="false" />
    <table>
        <tr>
            <td class="FieldLabel" style="vertical-align: top">
                <asp:Label ID="lbLicenseKey" runat="server" EnableViewState="False" /></td>
            <td>
                <cms:CMSTextBox ID="tbLicenseKey" runat="server" CssClass="TextAreaHigh" TextMode="MultiLine"
                    MaxLength="100" Width="600" />
                <cms:CMSRequiredFieldValidator ID="rfvLicenseKey" runat="server" EnableViewState="false"
                    ControlToValidate="tbLicenseKey" Display="dynamic"></cms:CMSRequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <cms:CMSButton ID="btnOk" runat="server" OnClick="btnOK_Click" CssClass="SubmitButton"
                    EnableViewState="false" />
            </td>
        </tr>
    </table>
</asp:Content>
