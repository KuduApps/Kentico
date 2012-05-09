<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="CMSSiteManager_Sites_Site_Edit_DomainAlias_Edit" Theme="Default" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master"
    Title="Edit Domain Aliases" CodeFile="Site_Edit_DomainAlias_Edit.aspx.cs" %>

<%@ Register Src="~/CMSFormControls/Cultures/SiteCultureSelector.ascx" TagName="SiteCultureSelector"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/Content/FormControls/Documents/SelectSinglePath.ascx"
    TagName="PageSelector" TagPrefix="cms" %>
    
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <asp:Label ID="lblInfo" runat="server" EnableViewState="false" CssClass="InfoLabel"
        Visible="false" />
    <asp:Label ID="lblError" runat="server" EnableViewState="false" CssClass="ErrorLabel"
        Visible="false" />
    <table>
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel ID="lblDomainName" runat="server" EnableViewState="false" />
            </td>
            <td>
                <cms:CMSTextBox ID="txtDomainName" runat="server" CssClass="TextBoxField" MaxLength="300" />
                <cms:CMSRequiredFieldValidator ID="rfvDomainName" runat="server" ControlToValidate="txtDomainName" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel ID="lblVisitorCulture" runat="server" EnableViewState="false" />
            </td>
            <td>
                <cms:SiteCultureSelector runat="server" ID="cultureSelector" IsLiveSite="false" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel ID="lblDefaultAliasPath" runat="server" EnableViewState="false"
                    DisplayColon="true" />
            </td>
            <td>
                <cms:PageSelector ID="pageSelector" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel ID="lblRedirectUrl" runat="server" EnableViewState="false" DisplayColon="true" />
            </td>
            <td>
                <cms:CMSTextBox ID="txtRedirectUrl" runat="server" CssClass="TextBoxField" MaxLength="450" />
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <cms:LocalizedButton ID="btnOk" runat="server" CssClass="SubmitButton" OnClick="btnOk_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
