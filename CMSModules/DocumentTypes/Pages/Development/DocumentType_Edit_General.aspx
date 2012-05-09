<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_DocumentTypes_Pages_Development_DocumentType_Edit_General"
    Theme="Default" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="Document Type Edit - General"
    CodeFile="DocumentType_Edit_General.aspx.cs" %>

<%@ Register Src="~/CMSFormControls/System/LocalizableTextBox.ascx" TagName="LocalizableTextBox" TagPrefix="cms" %>

<%@ Register Src="~/CMSAdminControls/UI/Selectors/LoadGenerationSelector.ascx" TagName="LoadGenerationSelector"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/PortalEngine/FormControls/PageTemplates/selectpagetemplate.ascx"
    TagName="selectpagetemplate" TagPrefix="cms" %>
<%@ Register Src="~/CMSFormControls/Classes/SelectClass.ascx" TagName="SelectClass"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/Content/Controls/Attachments/DirectFileUploader/DirectFileUploader.ascx"
    TagName="DirectFileUploader" TagPrefix="cms" %>
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <asp:Label runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false"
        Visible="false" />
    <asp:Label runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false"
        Visible="false" />
    <table>
        <tr>
            <td class="FieldLabel">
                <asp:Label runat="server" ID="lblDisplayName" EnableViewState="false" />
            </td>
            <td>
                <cms:LocalizableTextBox runat="server" ID="tbDisplayName" CssClass="TextBoxField" MaxLength="100"
                    EnableViewState="false" />
                <cms:CMSRequiredFieldValidator ID="RequiredFieldValidatorDisplayName" runat="server"
                    EnableViewState="false" ControlToValidate="tbDisplayName:textbox" Display="dynamic" />
            </td>
        </tr>
        <tr>
            <td style="vertical-align: text-top; padding-top: 5px" class="FieldLabel">
                <asp:Label runat="server" ID="lblFullCodeName" EnableViewState="false" />
            </td>
            <td>
                <table cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <cms:CMSTextBox runat="server" ID="tbNamespaceName" CssClass="TextBoxField" MaxLength="49"
                                EnableViewState="false" />
                        </td>
                        <td>
                            .
                        </td>
                        <td>
                            <cms:CMSTextBox runat="server" ID="tbCodeName" CssClass="TextBoxField" MaxLength="50"
                                EnableViewState="false" />
                        </td>
                    </tr>
                    <tr style="color: Green">
                        <td>
                            <asp:Label runat="server" ID="lblNamespaceName" EnableViewState="false" />
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            <asp:Label runat="server" ID="lblCodeName" EnableViewState="false" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <cms:CMSRequiredFieldValidator ID="RequiredFieldValidatorNamespaceName" runat="server"
                                EnableViewState="false" ControlToValidate="tbNamespaceName" Display="dynamic" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <cms:CMSRegularExpressionValidator ID="RegularExpressionValidatorNameSpaceName" runat="server"
                                EnableViewState="false" Display="dynamic" ControlToValidate="tbNamespaceName" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <cms:CMSRequiredFieldValidator ID="RequiredFieldValidatorCodeName" runat="server" EnableViewState="false"
                                ControlToValidate="tbCodeName" Display="dynamic" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <cms:CMSRegularExpressionValidator ID="RegularExpressionValidatorCodeName" runat="server"
                                EnableViewState="false" Display="dynamic" ControlToValidate="tbCodeName" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                &nbsp;
            </td>
        </tr>
        <asp:PlaceHolder runat="server" ID="plcFields">
            <tr>
                <td class="FieldLabel">
                    <asp:Label runat="server" ID="lblTableName" EnableViewState="false" />
                </td>
                <td>
                    <asp:Label runat="server" ID="lblTableNameValue" />
                </td>
            </tr>
            <tr>
                <td class="FieldLabel">
                    <cms:LocalizedLabel runat="server" ID="lblInherits" EnableViewState="false" ResourceString="DocumentType.InheritsFrom" />
                </td>
                <td>
                    <cms:SelectClass ID="selInherits" runat="server" DisplayNoneValue="true" />
                </td>
            </tr>
        </asp:PlaceHolder>
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel runat="server" ID="lblIcons" EnableViewState="false" ResourceString="DocumentType.Icons"
                    DisplayColon="true" />
            </td>
            <td>
                <cms:CMSUpdatePanel runat="server" ID="pnlIcons">
                    <ContentTemplate>
                        <table>
                            <tr>
                                <td>
                                    <asp:Image runat="server" ID="imgSmall" EnableViewState="false" />
                                </td>
                                <td>
                                    <cms:DirectFileUploader ID="dfuSmall" runat="server" ImageHeight="20" ImageWidth="90"
                                        TargetFolderPath="~/App_Themes/Default/Images/DocumentTypeIcons" SourceType="PhysicalFile"
                                        AllowedExtensions="png" ResizeToHeight="16" ResizeToWidth="16" AfterSaveJavascript="RefreshIcons"
                                        ForceLoad="true" InnerDivClass="ButtonUploader" UploadMode="DirectSingle" />
                                    <asp:Panel runat="server" ID="btnHidden" EnableViewState="false" CssClass="HiddenButton" />
                                </td>
                                <td style="width: 25px">
                                    &nbsp;
                                </td>
                                <td>
                                    <asp:Image runat="server" ID="imgLarge" EnableViewState="false" />
                                </td>
                                <td>
                                    <cms:DirectFileUploader ID="dfuLarge" runat="server" ImageHeight="20" ImageWidth="90"
                                        TargetFolderPath="~/App_Themes/Default/Images/DocumentTypeIcons/48x48" SourceType="PhysicalFile"
                                        AllowedExtensions="png" ResizeToHeight="48" ResizeToWidth="48" AfterSaveJavascript="RefreshIcons"
                                        ForceLoad="true" InnerDivClass="ButtonUploader" UploadMode="DirectSingle" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </cms:CMSUpdatePanel>
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <asp:Label runat="server" ID="lblNewPage" EnableViewState="false" />
            </td>
            <td>
                <cms:CMSTextBox runat="server" ID="txtNewPage" CssClass="TextBoxField" MaxLength="200"
                    EnableViewState="false" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <asp:Label runat="server" ID="lblViewPage" EnableViewState="false" />
            </td>
            <td>
                <cms:CMSTextBox runat="server" ID="txtViewPage" CssClass="TextBoxField" MaxLength="200"
                    EnableViewState="false" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <asp:Label runat="server" ID="lblEditingPage" EnableViewState="false" />
            </td>
            <td>
                <cms:CMSTextBox runat="server" ID="tbEditingPage" CssClass="TextBoxField" MaxLength="200"
                    EnableViewState="false" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <asp:Label runat="server" ID="lblPreviewPage" EnableViewState="false" />
            </td>
            <td>
                <cms:CMSTextBox runat="server" ID="txtPreviewPage" CssClass="TextBoxField" MaxLength="200"
                    EnableViewState="false" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <asp:Label runat="server" ID="lblListPage" EnableViewState="false" />
            </td>
            <td>
                <cms:CMSTextBox runat="server" ID="tbListPage" CssClass="TextBoxField" MaxLength="200"
                    EnableViewState="false" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <asp:Label runat="server" ID="lblClassUsePublishFromTo" EnableViewState="false" />
            </td>
            <td>
                <asp:CheckBox runat="server" ID="chkClassUsePublishFromTo" CssClass="EditingFormCheckBox"
                    EnableViewState="false" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <asp:Label runat="server" ID="lblTemplateSelection" EnableViewState="false" />
            </td>
            <td>
                <asp:CheckBox runat="server" ID="chkTemplateSelection" CssClass="EditingFormCheckBox"
                    OnCheckedChanged="chkTemplateSelection_CheckedChanged" AutoPostBack="true" EnableViewState="false" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <asp:Label runat="server" ID="lblDefaultTemplate" EnableViewState="false" />
            </td>
            <td>
                <cms:selectpagetemplate ID="templateDefault" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <asp:Label runat="server" ID="lblIsMenuItem" EnableViewState="false" />
            </td>
            <td>
                <asp:CheckBox runat="server" ID="chkIsMenuItem" CssClass="EditingFormCheckBox" EnableViewState="false" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <asp:Label runat="server" ID="lblIsProduct" EnableViewState="false" />
            </td>
            <td>
                <asp:CheckBox runat="server" ID="chkIsProduct" CssClass="EditingFormCheckBox" EnableViewState="false" />
            </td>
        </tr>
        <asp:PlaceHolder runat="server" ID="plcLoadGeneration">
            <tr>
                <td class="FieldLabel">
                    <asp:Label runat="server" ID="lblLoadGeneration" EnableViewState="false" />
                </td>
                <td>
                    <cms:LoadGenerationSelector ID="drpGeneration" runat="server" />
                </td>
            </tr>
        </asp:PlaceHolder>
        <tr>
            <td>
            </td>
            <td>
                <cms:CMSButton runat="server" ID="btnOk" OnClick="btnOK_Click" CssClass="SubmitButton"
                    EnableViewState="false" />
            </td>
        </tr>
    </table>
    <asp:Literal ID="ltlScript" runat="server" EnableViewState="false" />
</asp:Content>
