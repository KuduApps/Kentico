<%@ Page Language="C#" Theme="Default" MasterPageFile="~/CMSMasterPages/UI/Dialogs/ModalDialogPage.master"
    AutoEventWireup="true" EnableEventValidation="false" Inherits="CMSFormControls_Selectors_LocalizableTextBox_LocalizeField"
    Title="Localize field" CodeFile="LocalizeField.aspx.cs" %>

<%@ Register Src="~/CMSFormControls/Selectors/LocalizableTextBox/ResourceStringSelector.ascx"
    TagName="ResourceSelector" TagPrefix="cms" %>
<asp:Content ID="content" ContentPlaceHolderID="plcContent" runat="Server">
    <asp:Panel ID="pnlContent" CssClass="PageContent" runat="server">
        <cms:CMSUpdatePanel ID="pnlUpdate" runat="server">
            <ContentTemplate>
                <cms:LocalizedLabel ID="lblError" runat="server" EnableViewState="false" Visible="false"
                    CssClass="ErrorLabel" />
                <asp:Panel ID="pnlControls" runat="server" CssClass="LocalizeField">
                    <asp:RadioButtonList ID="lstExistingOrNew" runat="server" RepeatDirection="Vertical"
                        AutoPostBack="true" CssClass="LocalizableChecklist">
                        <asp:ListItem Value="new" Selected="True" />
                        <asp:ListItem Value="edit" />
                    </asp:RadioButtonList>
                    <cms:LocalizedLabel ID="lblSelectKey" runat="server" ResourceString="localizable.newkey"
                        DisplayColon="true" CssClass="InfoLabel" EnableViewState="false" />
                    <span class="LocalizeKeyPanel">
                        <asp:Label ID="lblPrefix" runat="server" EnableViewState="false" Visible="false" />
                        <cms:CMSTextBox ID="txtNewResource" runat="server" Visible="true" CssClass="TextBoxField" MaxLength="200" />
                        <cms:ResourceSelector ID="resourceSelector" runat="server" IsLiveSite="false" Visible="false"
                            CssClass="ResourceSelector" />
                    </span>
                </asp:Panel>
            </ContentTemplate>
        </cms:CMSUpdatePanel>
    </asp:Panel>
</asp:Content>
<asp:Content ID="footer" ContentPlaceHolderID="plcFooter" runat="server">
    <div class="FloatRight">
        <cms:LocalizedButton ID="btnOk" runat="server" CssClass="SubmitButton" ResourceString="general.ok"
            EnableViewState="false" />
        <cms:LocalizedButton ID="btnCancel" runat="server" CssClass="SubmitButton" ResourceString="general.cancel"
            EnableViewState="false" OnClientClick="window.close(); return false;" />
    </div>
</asp:Content>
