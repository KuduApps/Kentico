<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="CMSSiteManager_Sites_Site_Edit_General" Theme="Default" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master"
    Title="Site Edit - General" CodeFile="Site_Edit_General.aspx.cs" %>

<%@ Register Src="~/CMSFormControls/System/LocalizableTextBox.ascx" TagName="LocalizableTextBox" TagPrefix="cms" %>

<%@ Register Src="~/CMSFormControls/Cultures/SiteCultureSelector.ascx" TagName="SiteCultureSelector"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSFormControls/SelectCssStylesheet.ascx" TagName="SelectCssStylesheet"
    TagPrefix="cms" %>
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <asp:Panel ID="pnlContent" runat="server" DefaultButton="btnOk">
        <asp:Label runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false"
            Visible="false" />
        <asp:Label runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false"
            Visible="false" />
        <table>
            <tr>
                <td class="FieldLabel">
                    <asp:Label ID="lblDisplayName" runat="server" EnableViewState="false" />
                </td>
                <td>
                    <cms:LocalizableTextBox ID="txtDisplayName" runat="server" CssClass="TextBoxField" MaxLength="200" />
                    <cms:CMSRequiredFieldValidator ID="RequiredFieldValidatorDisplayName" runat="server"
                        ControlToValidate="txtDisplayName:textbox" CssClass="ContentValidator" Display="dynamic" />
                </td>
            </tr>
            <tr>
                <td class="FieldLabel">
                    <asp:Label ID="lblCodeName" runat="server" EnableViewState="false" />
                </td>
                <td>
                    <cms:CMSTextBox ID="txtCodeName" runat="server" CssClass="TextBoxField" MaxLength="100" />
                    <cms:CMSRequiredFieldValidator ID="RequiredFieldValidatorCodeName" runat="server" ErrorMessage="RequiredFieldValidator"
                        ControlToValidate="txtCodeName" CssClass="ContentValidator" Display="dynamic" />
                </td>
            </tr>
            <tr>
                <td class="FieldLabel">
                    <asp:Label ID="lblDomainName" runat="server" EnableViewState="false" />
                </td>
                <td>
                    <cms:CMSTextBox ID="txtDomainName" runat="server" CssClass="TextBoxField" MaxLength="400" />
                    <cms:CMSRequiredFieldValidator ID="RequiredFieldValidatorDomainName" runat="server"
                        ErrorMessage="RequiredFieldValidator" ControlToValidate="txtDomainName" CssClass="ContentValidator"
                        Display="dynamic" />
                </td>
            </tr>
            <tr>
                <td class="FieldLabel">
                    <asp:Label ID="lblCulture" runat="server" EnableViewState="false" />
                </td>
                <td>
                    <cms:CMSTextBox ID="txtCulture" runat="server" CssClass="SelectorTextBox" MaxLength="400"
                        EnableViewState="false" /><cms:CMSButton runat="server" ID="btnChange" CssClass="ContentButton" />
                </td>
            </tr>
            <tr>
                <td class="FieldLabel">
                    <asp:Label ID="lblVisitorCulture" runat="server" EnableViewState="false" />
                </td>
                <td>
                    <cms:SiteCultureSelector runat="server" ID="cultureSelector" IsLiveSite="false" />
                </td>
            </tr>
            <tr>
                <td class="FieldLabel">
                    <asp:Label ID="lblCssStyle" runat="server" EnableViewState="false" />
                </td>
                <td>
                    <cms:SelectCssStylesheet IsLiveSite="false" ID="ctrlSiteSelectStyleSheet" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="FieldLabel">
                    <asp:Label ID="lblEditorStyle" runat="server" EnableViewState="false" />
                </td>
                <td>
                    <cms:SelectCssStylesheet IsLiveSite="false" ID="ctrlEditorSelectStyleSheet" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="FieldLabel" style="vertical-align: top; padding-top: 5px">
                    <asp:Label ID="lblDescription" runat="server" EnableViewState="false" />
                </td>
                <td>
                    <cms:LocalizableTextBox ID="txtDescription" runat="server" CssClass="TextAreaField" TextMode="MultiLine" />
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <cms:CMSButton ID="btnOk" runat="server" Text="Button" OnClick="btnOk_Click" CssClass="SubmitButton"
                        EnableViewState="false" />
                </td>
            </tr>
        </table>

        <script type="text/javascript">
            //<![CDATA[
            function OpenCultureChanger(siteId, culture) {
                if (siteId != '0') {
                    modalDialog(pageChangeUrl + '?siteid=' + siteId + '&culture=' + culture, 'CutlureChange', 350, 200);
                }
            }
            //]]>	
        </script>

        <asp:Literal runat="server" ID="ltlScript" EnableViewState="false" />
        <cms:CMSButton runat="server" ID="btnHidden" CssClass="HiddenButton" OnClick="btnHidden_Click" />
        <asp:HiddenField runat="server" ID="hdnCulture" />
        <asp:HiddenField runat="server" ID="hdnDocumentsChangeChecked" />
    </asp:Panel>
</asp:Content>
