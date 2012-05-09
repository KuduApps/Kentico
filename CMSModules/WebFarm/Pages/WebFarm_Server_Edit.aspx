<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_WebFarm_Pages_WebFarm_Server_Edit"
    Title="Web farm server - Edit" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master"
    Theme="Default" CodeFile="WebFarm_Server_Edit.aspx.cs" %>

<%@ Register Src="~/CMSFormControls/System/LocalizableTextBox.ascx" TagName="LocalizableTextBox" TagPrefix="cms" %>

<%@ Register Src="~/CMSAdminControls/UI/System/ServerChecker.ascx" TagPrefix="cms"
    TagName="ServerChecker" %>
<asp:Content ID="cntBody" ContentPlaceHolderID="plcContent" runat="server">
    <asp:Label ID="lblInfo" runat="server" CssClass="InfoLabel" Visible="false" />
    <asp:Label ID="lblError" runat="server" CssClass="ErrorLabel" Visible="false" />
    <asp:PlaceHolder ID="plcEditForm" runat="server">
        <asp:Panel ID="pnlEditForm" runat="server" DefaultButton="btnOk">
            <table>
                <tr>
                    <td class="FieldLabel">
                        <cms:LocalizedLabel ID="lblDisplayName" runat="server" CssClass="ContentLabel" ResourceString="WebFarmServers_Edit.DisplayName" />
                    </td>
                    <td>
                        <cms:LocalizableTextBox ID="txtDisplayName" runat="server" CssClass="TextBoxField" MaxLength="200" />
                        <cms:CMSRequiredFieldValidator ID="rfvDisplayName" runat="server" ControlToValidate="txtDisplayName:textbox"
                            Display="Dynamic"></cms:CMSRequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="FieldLabel">
                        <cms:LocalizedLabel ID="lblCodeName" runat="server" CssClass="ContentLabel" ResourceString="WebFarmServers_Edit.CodeName" />
                    </td>
                    <td>
                        <cms:CMSTextBox ID="txtCodeName" runat="server" CssClass="TextBoxField" MaxLength="200" />
                        <cms:CMSRequiredFieldValidator ID="rfvCodeName" runat="server" ControlToValidate="txtCodeName"
                            Display="Dynamic"></cms:CMSRequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="FieldLabel">
                        <cms:LocalizedLabel ID="lblURL" runat="server" CssClass="ContentLabel" ResourceString="WebFarmServers_Edit.URL" />
                    </td>
                    <td>
                        <cms:CMSTextBox ID="txtURL" runat="server" CssClass="TextBoxField" MaxLength="2000" />
                        <cms:CMSRequiredFieldValidator ID="rfvURL" runat="server" ControlToValidate="txtURL"
                            Display="Dynamic"></cms:CMSRequiredFieldValidator>
                        <cms:ServerChecker runat="server" ID="serverChecker" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <cms:LocalizedLabel ID="lblEnabled" runat="server" CssClass="ContentLabel" ResourceString="WebFarmServers_Edit.Enabled" />
                    </td>
                    <td>
                        <asp:CheckBox ID="chkEnabled" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <cms:CMSButton ID="btnOk" runat="server" CssClass="SubmitButton" OnClick="btnOK_Click" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </asp:PlaceHolder>
</asp:Content>
