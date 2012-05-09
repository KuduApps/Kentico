<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CopyDocument.ascx.cs"
    Inherits="CMSModules_DocumentLibrary_Controls_CopyDocument" %>
<div class="DialogPageContent DialogScrollableContent">
    <div class="PageBody">
        <asp:PlaceHolder ID="plcDocumentName" runat="server">
            <table>
                <tr>
                    <td>
                        <cms:LocalizedLabel ID="lblDocumentName" runat="server" EnableViewState="false" ResourceString="general.documentname"
                            DisplayColon="true" AssociatedControlID="txtDocumentName" CssClass="ContentLabel" />
                    </td>
                    <td>
                        <cms:CMSTextBox ID="txtDocumentName" runat="server" CssClass="TextBoxField" MaxLength="100" />
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <cms:CMSRequiredFieldValidator ID="rfvDocumentName" runat="server" ControlToValidate="txtDocumentName"
                            ErrorMessage="*" Display="Static" CssClass="ValidatorMessage" />
                    </td>
                </tr>
            </table>
        </asp:PlaceHolder>
        <asp:PlaceHolder ID="plcMessage" runat="server" Visible="false">
            <cms:LocalizedLabel ID="lblInfo" runat="server" EnableViewState="false" CssClass="InfoLabel" />
            <cms:LocalizedLabel ID="lblError" runat="server" EnableViewState="false" CssClass="ErrorLabel" />
        </asp:PlaceHolder>
    </div>
</div>
<div class="PageFooterLine">
    <div class="Buttons">
        <cms:LocalizedButton ID="btnSave" runat="server" EnableViewState="false" ResourceString="general.save"
            OnClick="btnSave_Click" CssClass="SubmitButton" />
        <cms:LocalizedButton ID="btnCancel" runat="server" CssClass="SubmitButton" CausesValidation="false" /></div>
    <div class="ClearBoth">
        &nbsp;</div>
</div>
