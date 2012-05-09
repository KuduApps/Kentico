<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Licenses_Pages_License_Export_Domains"
Theme="Default" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="Licenses - Export domains" CodeFile="License_Export_Domains.aspx.cs" %>

<asp:Content ContentPlaceHolderID="plcContent" runat="server">
    <cms:LocalizedLabel ID="lblInfo" runat="server" CssClass="InfoLabel" />
    <cms:LocalizedLabel runat="server" ID="lblError" EnableViewState="false" Visible="false" CssClass="ErrorLabel" />
    <asp:PlaceHolder runat="server" ID="plcTextBox">
        <table>
            <tr>
                <td class="FieldLabel">
                    <cms:LocalizedLabel ID="lblFileName" runat="server" EnableViewState="False" ResourceString="general.filename" DisplayColon="true" /></td>
                <td>
                    <cms:CMSTextBox ID="txtFileName" runat="server" CssClass="TextBoxField" MaxLength="200" />
                    <cms:CMSRequiredFieldValidator ID="rfvFileName" runat="server" EnableViewState="false"
                        ControlToValidate="txtFileName" Display="dynamic"></cms:CMSRequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td colspan="2">&nbsp;</td>
            </tr>
            <tr>
                <td>
                </td>
                <td>                
                    <cms:LocalizedButton ID="btnOk" runat="server" OnClick="btnOk_Click" CssClass="SubmitButton"
                        EnableViewState="false" ResourceString="general.ok" />                    
                </td>
            </tr>
        </table>
   </asp:PlaceHolder>
</asp:Content>
