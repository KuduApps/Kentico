<%@ Page Language="C#" ValidateRequest="false" AutoEventWireup="true"
    Inherits="CMSFormControls_Selectors_GridColumnDesigner" MasterPageFile="~/CMSMasterPages/UI/Dialogs/ModalDialogPage.master"
    Theme="Default" Title="Grid column designer" CodeFile="GridColumnDesigner.aspx.cs" %>

<%@ Register Src="~/CMSAdminControls/UI/Selectors/ItemSelection.ascx" TagName="ItemSelection"
    TagPrefix="cms" %>
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <asp:HiddenField ID="hdnSelectedColumns" runat="server" />
    <asp:HiddenField ID="hdnClassNames" runat="server" />
    <asp:HiddenField ID="hdnColumns" runat="server" />
    <asp:HiddenField ID="hdnTextClassNames" runat="server" />
    <asp:Panel runat="server" ID="pnlBody" CssClass="PageContent">
        <table style="width: 100%;" cellpadding="3" cellspacing="0">
            <tr>
                <td>
                    <asp:RadioButton ID="radGenerate" runat="server" Checked="True" GroupName="GenerateSelect"
                        AutoPostBack="True" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:RadioButton ID="radSelect" runat="server" GroupName="GenerateSelect" AutoPostBack="True" />
                </td>
            </tr>
        </table>
        <cms:ItemSelection ID="ItemSelection1" runat="server" Visible="false" />
        <asp:Panel ID="pnlProperties" runat="server" Visible="false">
            <br />
            <br />
            <asp:Label ID="lblProperties" runat="server" Font-Bold="true" />
            <table cellpadding="3" cellspacing="0">
                <tr>
                    <td nowrap="nowrap">
                        <asp:Label ID="lblHeaderText" runat="server" />
                    </td>
                    <td width="100%">
                        <cms:CMSTextBox ID="txtHeaderText" CssClass="TextBoxField" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td nowrap="nowrap">
                        <asp:Label ID="lblDisplayAsLink" runat="server" />
                    </td>
                    <td>
                        <asp:CheckBox ID="chkDisplayAsLink" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <cms:CMSButton ID="btnOk" runat="server" Text="OK" CssClass="SubmitButton" OnClick="btnOK_Click" />&nbsp;
                        &nbsp;&nbsp;
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </asp:Panel>
</asp:Content>
<asp:Content ID="plcFooter" ContentPlaceHolderID="plcFooter" runat="server">
    <div class="FloatRight">
        <cms:CMSButton ID="btnClose" runat="server" Text="Close" CssClass="SubmitButton"
            OnClick="btnClose_Click" />
        <asp:Literal ID="ltlOk" runat="server" EnableViewState="false" />
        <asp:Literal ID="ltlLoad" runat="server" EnableViewState="false" />
    </div>
</asp:Content>
