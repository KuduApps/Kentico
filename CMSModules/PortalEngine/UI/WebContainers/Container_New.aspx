<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_PortalEngine_UI_WebContainers_Container_New"
    MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Theme="Default" ValidateRequest="false"
    Title="New container" CodeFile="Container_New.aspx.cs" %>

<%@ Register Src="~/CMSFormControls/System/LocalizableTextBox.ascx" TagName="LocalizableTextBox"
    TagPrefix="cms" %>
<asp:Content ContentPlaceHolderID="plcContent" ID="content" runat="server">
    <asp:Panel ID="pnlContainer" runat="server">
        <table style="vertical-align: top;">
            <tr>
                <td style="vertical-align: top; padding-top: 5px" class="FieldLabel">
                    <cms:LocalizedLabel runat="server" ID="lblContainerDisplayName" ResourceString="general.displayname"
                        DisplayColon="true" EnableViewState="false" />
                </td>
                <td>
                    <cms:LocalizableTextBox ID="txtContainerDisplayName" runat="server" CssClass="TextBoxField"
                        MaxLength="200" />
                    <cms:CMSRequiredFieldValidator ID="rfvDisplayName" runat="server" ControlToValidate="txtContainerDisplayName:textbox" />
                </td>
            </tr>
            <tr>
                <td style="vertical-align: top; padding-top: 5px" class="FieldLabel">
                    <cms:LocalizedLabel runat="server" ID="lblContainerName" ResourceString="general.codename"
                        DisplayColon="true" EnableViewState="false" />
                </td>
                <td>
                    <cms:CMSTextBox ID="txtContainerName" runat="server" CssClass="TextBoxField" MaxLength="200" />
                    <cms:CMSRequiredFieldValidator ID="rfvCodeName" runat="server" ControlToValidate="txtContainerName" />
                </td>
            </tr>
            <tr>
                <td class="FieldLabel">
                    <cms:LocalizedLabel runat="server" ID="lblContainerTextBefore" ResourceString="Container_Edit.ContainerTextBeforeLabel"
                        DisplayColon="true" EnableViewState="false" />
                </td>
                <td>
                    <cms:ExtendedTextArea ID="txtContainerTextBefore" runat="server" EnableViewState="false"
                        EditorMode="Advanced" Language="HTMLMixed" MarkErrors="false" Width="600px" Height="200px" />
                </td>
            </tr>
            <tr>
                <td class="FieldLabel">
                    <cms:LocalizedLabel runat="server" ID="lblContainerTextAfter" ResourceString="Container_Edit.ContainerTextAfterLabel"
                        DisplayColon="true" EnableViewState="false" />
                </td>
                <td>
                    <cms:ExtendedTextArea ID="txtContainerTextAfter" runat="server" EnableViewState="false"
                        EditorMode="Advanced" Language="HTMLMixed" MarkErrors="false" Width="600px" Height="200px" />
                </td>
            </tr>
            <asp:PlaceHolder runat="server" ID="plcCssLink">
                <tr id="cssLink">
                    <td class="FieldLabel">
                    </td>
                    <td>
                        <cms:LocalizedLinkButton runat="server" ID="lnkStyles" EnableViewState="false" ResourceString="general.addcss"
                            OnClientClick="document.getElementById('editCss').style.display = 'table-row'; document.getElementById('cssLink').style.display = 'none'; return false;" />
                    </td>
                </tr>
            </asp:PlaceHolder>
            <asp:PlaceHolder runat="server" ID="plcCss">
                <tr id="editCss" style="<%= (plcCssLink.Visible ? "display: none": "") %>">
                    <td class="FieldLabel">
                        <cms:LocalizedLabel runat="server" ID="lblContainerCSS" ResourceString="Container_Edit.ContainerCSS"
                            DisplayColon="true" EnableViewState="false" />
                    </td>
                    <td>
                        <cms:ExtendedTextArea ID="txtContainerCSS" runat="server" EnableViewState="false"
                            EditorMode="Advanced" Language="CSS" Width="600px" Height="200px" />
                    </td>
                </tr>
            </asp:PlaceHolder>
            <tr>
                <td>
                </td>
                <td>
                    <asp:CheckBox runat="server" ID="chkAssign" Visible="false" Checked="true" />
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <cms:LocalizedButton runat="server" ID="btnOk" OnClick="btnOK_Click" ResourceString="general.ok"
                        EnableViewState="false" CssClass="SubmitButton" />
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
