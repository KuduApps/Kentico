<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_Modules_Pages_Development_Module_Edit_General" Theme="Default"
    MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="Module Edit - General" CodeFile="Module_Edit_General.aspx.cs" %>

<%@ Register Src="~/CMSFormControls/System/LocalizableTextBox.ascx" TagName="LocalizableTextBox" TagPrefix="cms" %>

<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent" EnableViewState="false">
    <asp:Label runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false"
        Visible="false" />
    <asp:Label runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false"
        Visible="false" />
    <table>
        <tr>
            <td class="FieldLabel">
                <asp:Label ID="lbModuleDisplayName" runat="server" EnableViewState="false" />
            </td>
            <td>
                <cms:LocalizableTextBox ID="tbModuleDisplayName" runat="server" CssClass="TextBoxField" MaxLength="100"
                    EnableViewState="false" />
                <cms:CMSRequiredFieldValidator ID="rfvModuleDisplayName" runat="server" EnableViewState="false"
                    ControlToValidate="tbModuleDisplayName:textbox" Display="dynamic" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <asp:Label ID="lbModuleCodeName" runat="server" EnableViewState="false" />
            </td>
            <td>
                <cms:CMSTextBox ID="tbModuleCodeName" runat="server" CssClass="TextBoxField" MaxLength="100"
                    EnableViewState="false" />
                <cms:CMSRequiredFieldValidator ID="rfvModuleCodeName" runat="server" EnableViewState="false"
                    ControlToValidate="tbModuleCodeName" Display="dynamic" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblModuleDescription" runat="server" EnableViewState="false" />
            </td>
            <td>
                <cms:LocalizableTextBox ID="txtModuleDescription" runat="server" CssClass="TextAreaField" TextMode="MultiLine"
                    EnableViewState="false" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel" style="padding-top: 2px; vertical-align: top;">
                <asp:Label ID="lblShowIn" runat="server" EnableViewState="false" />
            </td>
            <td>
                <asp:CheckBox ID="chkShowInDevelopment" runat="server" EnableViewState="false" />
                <asp:Panel runat="server" ID="pnlResourceUrl">
                    <table>
                        <tr>
                            <td class="FieldLabel">
                                <asp:Label ID="lblResourceUrl" runat="server" EnableViewState="false" />
                            </td>
                            <td>
                                <cms:CMSTextBox ID="txtResourceUrl" runat="server" Width="214" MaxLength="1000" EnableViewState="false" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                <cms:CMSButton ID="btnOk" runat="server" OnClick="btnOK_Click" CssClass="SubmitButton"
                    EnableViewState="false" />
            </td>
        </tr>
    </table>
</asp:Content>
