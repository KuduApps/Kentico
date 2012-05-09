<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Widgets_Dialogs_Widget_Clone"
    MasterPageFile="~/CMSMasterPages/UI/Dialogs/ModalDialogPage.master" Theme="Default"
    Title="Clone widget" CodeFile="Widget_Clone.aspx.cs" %>

<%@ Register Src="~/CMSFormControls/System/LocalizableTextBox.ascx" TagName="LocalizableTextBox" TagPrefix="cms" %>

<%@ Register Src="~/CMSModules/Widgets/FormControls/SelectWidgetCategory.ascx" TagName="SelectWidgetCategory"
    TagPrefix="cms" %>
<asp:Content ContentPlaceHolderID="plcContent" ID="content" runat="server">
    <div class="PageContent">
        <asp:Label ID="lblInfo" runat="server" />
        <asp:Label runat="server" ID="lblError" ForeColor="red" EnableViewState="false" Visible="false" />
        <asp:Literal ID="ltlScript" runat="server" />
        <table>
            <tr>
                <td class="FieldLabel">
                    <cms:LocalizedLabel ID="lblWidgetDisplayName" runat="server" EnableViewState="False"
                        ResourceString="general.displayname" DisplayColon="true" />
                </td>
                <td>
                    <cms:LocalizableTextBox ID="txtWidgetDisplayName" runat="server" CssClass="TextBoxField" MaxLength="100" />
                    <div>
                        <cms:CMSRequiredFieldValidator ID="rfvWidgetDisplayName" runat="server" EnableViewState="false"
                            ControlToValidate="txtWidgetDisplayName:textbox" Display="dynamic"></cms:CMSRequiredFieldValidator>
                    </div>
                </td>
            </tr>
            <tr>
                <td class="FieldLabel">
                    <cms:LocalizedLabel ID="lblWidgetName" runat="server" EnableViewState="False" ResourceString="general.codename"
                        DisplayColon="true" />
                </td>
                <td>
                    <cms:CMSTextBox ID="txtWidgetName" runat="server" CssClass="TextBoxField" MaxLength="100" />
                    <div>
                        <cms:CMSRequiredFieldValidator ID="rfvWidgetName" runat="server" EnableViewState="false"
                            ControlToValidate="txtWidgetName" Display="dynamic"></cms:CMSRequiredFieldValidator>
                    </div>
                </td>
            </tr>
            <tr>
                <td class="FieldLabel">
                    <cms:LocalizedLabel runat="server" ID="lblCategory" EnableViewState="false" ResourceString="widgets.category"
                        DisplayColon="true" />
                </td>
                <td>
                    <cms:SelectWidgetCategory ID="categorySelector" runat="server" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="cntFooter" runat="server" ContentPlaceHolderID="plcFooter">
    <div class="FloatRight">
        <cms:CMSButton ID="btnOk" runat="server" CssClass="SubmitButton" EnableViewState="false"
            OnClick="btnOK_Click" /><cms:CMSButton ID="btnCancel" runat="server" CssClass="SubmitButton" />
    </div>
</asp:Content>
