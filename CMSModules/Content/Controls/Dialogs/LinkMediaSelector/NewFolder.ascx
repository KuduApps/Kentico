<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_Content_Controls_Dialogs_LinkMediaSelector_NewFolder" CodeFile="NewFolder.ascx.cs" %>
<asp:Panel ID="pnlFolderArea" runat="server" DefaultButton="btnOk">
    <table width="100%">
        <tr>
            <td colspan="2">
                <cms:LocalizedLabel ID="lblInfo" runat="server" DisplayColon="false" Visible="false"
                    CssClass="InfoLabel" EnableViewState="false"></cms:LocalizedLabel>
                <cms:LocalizedLabel ID="lblError" runat="server" CssClass="ErrorLabel" DisplayColon="false"
                    EnableViewState="false" Visible="false"></cms:LocalizedLabel>
            </td>
        </tr>
        <tr>
            <td style="white-space: nowrap;">
                <cms:LocalizedLabel ID="lblFolderName" runat="server" CssClass="FieldLabel" DisplayColon="true"
                    ResourceString="general.foldername" EnableViewState="false"></cms:LocalizedLabel>
            </td>
            <td>
                <cms:CMSTextBox ID="txtFolderName" runat="server" CssClass="TextBoxField" MaxLength="50"
                    EnableViewState="false" />
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                <cms:CMSRequiredFieldValidator ID="rfvFolderName" runat="server" Display="Dynamic" ControlToValidate="txtFolderName"
                    ValidationGroup="btnOk" EnableViewState="false"></cms:CMSRequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td colspan="2">
                <div class="PageFooterLine FloatRight">
                    <cms:CMSButton ID="btnOk" runat="server" OnClick="btnOk_Click" ValidationGroup="btnOk"
                        CssClass="SubmitButton" EnableViewState="false" />
                    <cms:CMSButton ID="btnCancel" runat="server" OnClick="btnCancel_Click" CssClass="SubmitButton"
                        EnableViewState="false" />
                </div>
            </td>
        </tr>
    </table>
    <asp:Literal ID="ltlScript" runat="server" EnableViewState="false" />
</asp:Panel>
