<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LocalizableTextBox.ascx.cs"
    Inherits="CMSFormControls_System_LocalizableTextBox" %>
<cms:CMSTextBox ID="textbox" runat="server" CssClass="TextBoxField" /><asp:Panel
    ID="pnlButtons" runat="server" CssClass="LocalizablePanel">
    <asp:Button ID="btnLocalize" runat="server" CausesValidation="false" CssClass="LocalizableIcon"
        EnableViewState="false" UseSubmitBehavior="false" /><asp:Button ID="btnOtherLanguages"
            runat="server" CausesValidation="false" CssClass="LocalizableIcon" EnableViewState="false"
            UseSubmitBehavior="false" /><asp:Button ID="btnRemoveLocalization" runat="server"
                CausesValidation="false" CssClass="LocalizableIconLast" EnableViewState="false"
                UseSubmitBehavior="false" /></asp:Panel>
<asp:HiddenField ID="hdnValue" runat="server" EnableViewState="false" />
<asp:HiddenField ID="hdnIsMacro" runat="server" EnableViewState="false" />
<asp:HiddenField ID="hdnIdentificator" runat="server" EnableViewState="false" />
<cms:LocalizedLabel ID="lblError" runat="server" CssClass="ErrorLabel" EnableViewState="false"
    Visible="false" />