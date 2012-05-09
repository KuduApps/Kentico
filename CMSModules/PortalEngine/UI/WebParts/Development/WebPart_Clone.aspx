<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_PortalEngine_UI_WebParts_Development_WebPart_Clone"
    MasterPageFile="~/CMSMasterPages/UI/Dialogs/ModalDialogPage.master" Theme="Default" CodeFile="WebPart_Clone.aspx.cs" %>

<%@ Register Src="~/CMSFormControls/System/LocalizableTextBox.ascx" TagName="LocalizableTextBox" TagPrefix="cms" %>

<asp:Content ContentPlaceHolderID="plcContent" ID="content" runat="server">
    <div class="PageContent">
        <asp:Label ID="lblInfo" runat="server" />
        <asp:Label runat="server" ID="lblError" ForeColor="red" EnableViewState="false" Visible="false" />
        <asp:Literal ID="ltlScript" runat="server" />
        <table>
            <tr>
                <td class="FieldLabel">
                    <cms:LocalizedLabel ID="lbWebPartDisplaytName" runat="server" EnableViewState="False"
                        ResourceString="general.displayname" DisplayColon="true" />
                </td>
                <td>
                    <cms:LocalizableTextBox ID="txtWebPartDisplayName" runat="server" CssClass="TextBoxField" MaxLength="100" />
                    <div>
                        <cms:CMSRequiredFieldValidator ID="rfvWebPartDisplayName" runat="server" EnableViewState="false"
                            ControlToValidate="txtWebPartDisplayName:textbox" Display="dynamic" />
                    </div>
                </td>
            </tr>
            <tr>
                <td class="FieldLabel">
                    <cms:LocalizedLabel ID="lbWebPartName" runat="server" EnableViewState="False" ResourceString="general.codename"
                        DisplayColon="true" />
                </td>
                <td>
                    <cms:CMSTextBox ID="txtWebPartName" runat="server" CssClass="TextBoxField" MaxLength="100" />
                    <div>
                        <cms:CMSRequiredFieldValidator ID="rfvWebPartName" runat="server" EnableViewState="false"
                            ControlToValidate="txtWebPartName" Display="dynamic" />
                    </div>
                </td>
            </tr>
            <tr>
                <td class="FieldLabel">
                    <asp:Label ID="lbWebPartCategory" runat="server" EnableViewState="False" />
                </td>
                <td>
                    <asp:DropDownList ID="drpWebPartCategories" runat="server" CssClass="DropDownField" />
                    <div>
                        <cms:CMSRequiredFieldValidator ID="rfvWebPartCategory" runat="server" ControlToValidate="drpWebPartCategories"
                            Display="Dynamic" />
                    </div>
                </td>
            </tr>
            <asp:PlaceHolder runat="server" ID="plcFile">
                <tr>
                    <td>
                        <cms:LocalizedLabel ID="lblWebPartFileName" runat="server" ResourceString="general.filename"
                            DisplayColon="true" />
                    </td>
                    <td>
                        <cms:CMSTextBox ID="txtWebPartFileName" runat="server" CssClass="TextBoxField" />
                        <div>
                            <cms:CMSRequiredFieldValidator ID="rfvWebPartFileName" runat="server" ControlToValidate="txtWebPartFileName"
                                Display="Dynamic" />
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <asp:CheckBox ID="chckCloneWebPartFiles" runat="server" Checked="True" />
                    </td>
                </tr>
            </asp:PlaceHolder>
        </table>
    </div>
</asp:Content>
<asp:Content ID="cntFooter" runat="server" ContentPlaceHolderID="plcFooter">
    <div class="FloatRight">
        <cms:CMSButton ID="btnOk" runat="server" CssClass="SubmitButton" EnableViewState="false"
            OnClick="btnOK_Click" /><cms:CMSButton ID="btnCancel" runat="server" CssClass="SubmitButton" />
    </div>
</asp:Content>
