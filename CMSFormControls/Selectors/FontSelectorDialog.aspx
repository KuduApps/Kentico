<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSFormControls_Selectors_FontSelectorDialog"
    Theme="default" Title="Font Selector" MasterPageFile="~/CMSMasterPages/UI/Dialogs/ModalDialogPage.master"
    CodeFile="~/CMSFormControls/Selectors/FontSelectorDialog.aspx.cs" %>

<asp:Content ID="cntContent" ContentPlaceHolderID="plcContent" runat="Server">
    <div>
        <asp:Label runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false"
            Visible="false" />
        <table cellspacing="3" cellpadding="3" class="FontSelectorStyleTable">
            <tr>
                <td class="FontSelectorStyleColumnHeader">
                    <%# ResHelper.GetString("fontselector.font")  %>
                </td>
                <td class="FontSelectorStyleColumnHeader">
                    <%# ResHelper.GetString("fontselector.style")  %>
                </td>
                <td class="FontSelectorStyleColumnHeader">
                    <%# ResHelper.GetString("fontselector.size")  %>
                </td>
            </tr>
            <tr>
                <td>
                    <cms:CMSTextBox CssClass="FontSelectorTypeTextBox" ID="txtFontType" runat="server" ReadOnly="true" />
                </td>
                <td>
                    <cms:CMSTextBox CssClass="FontSelectorStyleTextBox" ID="txtFontStyle" runat="server"
                        ReadOnly="true" />
                </td>
                <td>
                    <cms:CMSTextBox CssClass="FontSelectorStyleTextBox" ID="txtFontSize" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:ListBox ID="lstFontType" CssClass="FontSelectorTypeListBox" runat="server" />
                </td>
                <td>
                    <asp:ListBox ID="lstFontStyle" CssClass="FontSelectorStyleListBox" runat="server" />
                </td>
                <td>
                    <asp:ListBox CssClass="FontSelectorStyleListBox" ID="lstFontSize" runat="server" />
                </td>
            </tr>
        </table>
        <table class="FontSelectorSampleTable">
            <tr>
                <td class="FontSelectorWeightTable">
                    <asp:CheckBox ID="chkUnderline" runat="server" /><br />
                    <asp:CheckBox ID="chkStrike" runat="server" />
                </td>
                <td>
                    <asp:Panel runat="server" class="FontSelectorSamplePanel" ID="pnlSampleText">
                        <asp:Panel runat="server" class="FontSelectorTextSamplePanel">
                            <asp:Label runat="server" ID="lblSampleText" Text="AaBbZzYy" />
                        </asp:Panel>
                    </asp:Panel>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="cntFooter" ContentPlaceHolderID="plcFooter" runat="server">
    <div class="FloatRight">
        <cms:LocalizedButton ID="btnOk" runat="server" CssClass="SubmitButton" EnableViewState="False"
            OnClick="btnOk_Click" />
        <cms:LocalizedButton ID="btnCancel" runat="server" CssClass="SubmitButton" EnableViewState="False" />
    </div>
</asp:Content>
