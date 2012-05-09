<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_MediaLibrary_Controls_MediaLibrary_MediaFileMultipleImport"
    CodeFile="MediaFileMultipleImport.ascx.cs" %>
<%@ Register Src="~/CMSAdminControls/UI/PageElements/PageTitle.ascx" TagName="PageTitle"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSInlineControls/MediaControl.ascx" TagPrefix="cms" TagName="MediaPreview" %>
<%@ Register Src="~/CMSInlineControls/ImageControl.ascx" TagPrefix="cms" TagName="ImagePreview" %>
<%@ Register Src="~/CMSFormControls/System/LocalizableTextBox.ascx" TagName="LocalizableTextBox"
    TagPrefix="cms" %>
<asp:Literal ID="ltlScript" runat="server"></asp:Literal>
<asp:Panel ID="pnlImportFilesHeader" runat="server" CssClass="PageHeader">
    <table style="width: 100%" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <div class="MediaLibraryImport">
                    <cms:PageTitle ID="importFilesTitleElem" runat="server" EnableViewState="false" />
                </div>
            </td>
        </tr>
    </table>
</asp:Panel>
<asp:Panel ID="pnlImportFilesContent" runat="server" CssClass="PageContent">
    <asp:Label ID="lblError" runat="server" Visible="false" CssClass="ErrorLabel" EnableViewState="false"></asp:Label>
    <div class="MediaLibraryImportForm">
        <table>
            <tr>
                <td>
                    <cms:LocalizedLabel ID="lblImportFileName" runat="server" CssClass="FieldLabel" EnableViewState="false"
                        ResourceString="general.filename" DisplayColon="true" />
                </td>
                <td colspan="2">
                    <cms:CMSTextBox ID="txtImportFileName" runat="server" CssClass="TextBoxField" MaxLength="250"
                        EnableViewState="false">
                    </cms:CMSTextBox >
                    <cms:CMSRequiredFieldValidator ID="rfvImportFileName" runat="server" ValidationGroup="MediaFileImport"
                        ControlToValidate="txtImportFileName" Display="Dynamic" EnableViewState="false" />
                </td>
            </tr>
            <tr>
                <td>
                    <cms:LocalizedLabel ID="lblImportFileTitle" runat="server" CssClass="FieldLabel"
                        EnableViewState="false"></cms:LocalizedLabel>
                </td>
                <td colspan="2">
                    <cms:LocalizableTextBox ID="txtImportFileTitle" runat="server" CssClass="TextBoxField" MaxLength="250"
                        EnableViewState="false" />
                    <cms:CMSRequiredFieldValidator ID="rfvImportFileTitle" runat="server" ValidationGroup="MediaFileImport"
                        ControlToValidate="txtImportFileTitle:textbox" Display="Dynamic" EnableViewState="false" />
                </td>
            </tr>
            <tr>
                <td style="vertical-align: top;">
                    <cms:LocalizedLabel ID="lblImportFileDescription" runat="server" CssClass="FieldLabel"
                        EnableViewState="false" DisplayColon="true"></cms:LocalizedLabel>
                </td>
                <td colspan="2">
                    <cms:LocalizableTextBox ID="txtImportFileDescription" runat="server" EnableViewState="false"
                        TextMode="MultiLine" CssClass="TextAreaField" />
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td colspan="2">
                    <asp:CheckBox ID="chkImportDescriptionToAllFiles" runat="server" EnableViewState="false"
                        CssClass="CheckBoxMovedLeft" />
                </td>
            </tr>
            <tr>
                <td style="height: 40px;">
                    &nbsp;
                </td>
                <td>
                    <cms:CMSButton ID="btnImportFile" runat="server" EnableViewState="false" CssClass="LongSubmitButton"
                        OnClick="btnImportFile_Click" ValidationGroup="MediaFileImport" />
                </td>
                <td style="width: 100%;" class="TextLeft">
                    <cms:CMSButton ID="btnImportCancel" runat="server" EnableViewState="false" CssClass="SubmitButton"
                        OnClick="btnImportCancel_Click" />
                </td>
            </tr>
        </table>
    </div>
    <div class="MediaLibraryImportPreview">
        <asp:PlaceHolder ID="plcPreview" runat="server">
            <cms:LocalizedCheckBox ID="chkDisplayPreview" runat="server" ResourceString="media.import.showpreview"
                EnableViewState="false" />
            <asp:HiddenField ID="hdnPreviewType" runat="server" />
            <div id="divImagePreview" style="display: none;">
                <div class="ImportPreview">
                    <cms:ImagePreview ID="imagePreview" runat="server" />
                </div>
                <div class="ImportPreviewLink">
                    <asp:HyperLink ID="lnkOpenImage" runat="server" EnableViewState="false"></asp:HyperLink>
                </div>
            </div>
            <div id="divMediaPreview" style="display: none;">
                <div class="ImportPreview">
                    <cms:MediaPreview ID="mediaPreview" runat="server" AutoPlay="false" />
                </div>
                <div class="ImportPreviewLink">
                    <asp:HyperLink ID="lnkOpenMedia" runat="server" EnableViewState="false"></asp:HyperLink>
                </div>
            </div>
            <div id="divOtherPreview" style="display: none;">
                <div class="ImportPreviewLink">
                    <asp:HyperLink ID="lnkOpenOther" runat="server" EnableViewState="false"></asp:HyperLink>
                </div>
            </div>
        </asp:PlaceHolder>
    </div>
</asp:Panel>
