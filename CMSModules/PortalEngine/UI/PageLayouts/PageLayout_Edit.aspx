<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_PortalEngine_UI_PageLayouts_PageLayout_Edit"
    Theme="Default" ValidateRequest="false" EnableEventValidation="false" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master"
    Title="Page Layout Edit" CodeFile="PageLayout_Edit.aspx.cs" %>

<%@ Register Src="~/CMSFormControls/System/LocalizableTextBox.ascx" TagName="LocalizableTextBox"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/AdminControls/Controls/MetaFiles/FileList.ascx" TagName="FileList"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/AdminControls/Controls/MetaFiles/File.ascx" TagName="File"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSAdminControls/UI/Macros/MacroEditor.ascx" TagName="MacroEditor"
    TagPrefix="cms" %>
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <asp:Panel ID="pnlCheckOutInfo" runat="server" CssClass="InfoLabel" Visible="false"
        EnableViewState="false">
        <asp:Label runat="server" ID="lblCheckOutInfo" />
        <br />
    </asp:Panel>
    <asp:Label ID="lblInfo" runat="server" CssClass="InfoLabel" EnableViewState="false"
        Visible="false" />
    <asp:Label ID="lblError" runat="server" CssClass="ErrorLabel" EnableViewState="false"
        Visible="false" />
    <table>
        <tr>
            <td class="FieldLabel">
                <asp:Label ID="lbLayoutDisplayName" runat="server" EnableViewState="False" />
            </td>
            <td>
                <cms:LocalizableTextBox ID="tbLayoutDisplayName" runat="server" CssClass="TextBoxField"
                    MaxLength="100" />
                <cms:CMSRequiredFieldValidator ID="rfvLayoutDisplayName" runat="server" EnableViewState="false"
                    ControlToValidate="tbLayoutDisplayName:textbox" Display="dynamic"></cms:CMSRequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lbCodeName" runat="server" EnableViewState="false" />
            </td>
            <td>
                <cms:CMSTextBox ID="tbCodeName" runat="server" CssClass="TextBoxField" /><br />
                <cms:CMSRequiredFieldValidator ID="rfvCodeName" runat="server" ControlToValidate="tbCodeName"
                    Display="Dynamic" EnableViewState="false"></cms:CMSRequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="FieldLabel" style="vertical-align: top">
                <asp:Label ID="lbLayoutDescription" runat="server" EnableViewState="False" />
            </td>
            <td>
                <cms:LocalizableTextBox ID="tbLayoutDescription" runat="server" CssClass="TextAreaField"
                    MaxLength="100" TextMode="MultiLine" />
            </td>
        </tr>
        <asp:PlaceHolder runat="server" ID="plcFile" Visible="false">
            <tr>
                <td class="FieldLabel" style="vertical-align: middle">
                    <asp:Label ID="lblUploadFile" runat="server" EnableViewState="false" />
                </td>
                <td>
                    <cms:File ID="UploadFile" runat="server" />
                </td>
            </tr>
        </asp:PlaceHolder>
        <tr>
            <td colspan="2">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td class="FieldLabel" style="vertical-align: middle">
                <asp:Label ID="lblType" runat="server" EnableViewState="false" />
            </td>
            <td>
                <asp:DropDownList runat="server" ID="drpType" AutoPostBack="true" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel" style="vertical-align: top">
                <asp:Label ID="lbLayoutCode" runat="server" EnableViewState="False" />
            </td>
            <td style="width: 85%">
                <asp:PlaceHolder runat="server" ID="plcHint">
                    <asp:Literal ID="ltlHint" runat="server" EnableViewState="false" /><br />
                    <br />
                </asp:PlaceHolder>
                <asp:PlaceHolder runat="server" ID="plcDirectives">
                    <asp:Label runat="server" ID="ltlDirectives" EnableViewState="false" CssClass="LayoutDirectives" /><br />
                </asp:PlaceHolder>
                <asp:PlaceHolder runat="server" ID="plcVirtualInfo" Visible="false">
                    <br />
                    <asp:Label runat="server" ID="lblVirtualInfo" CssClass="ErrorLabel" EnableViewState="false" />
                </asp:PlaceHolder>
                <cms:MacroEditor ID="tbLayoutCode" runat="server" EnableViewState="false" Width="98%" Height="300px" />
                <cms:CMSRequiredFieldValidator ID="rfvLayoutCode" runat="server" ControlToValidate="tbLayoutCode:txtCode"
                    EnableClientScript="false" Display="Dynamic" EnableViewState="False" />
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
        <tr id="editCss" style="<%= (plcCssLink.Visible ? "display: none": "") %>">
            <td class="FieldLabel">
                <cms:LocalizedLabel runat="server" ID="lblLayoutCSS" ResourceString="Container_Edit.ContainerCSS"
                    DisplayColon="true" EnableViewState="false" />
            </td>
            <td style="width: 85%">
                <cms:ExtendedTextArea ID="txtLayoutCSS" runat="server" EnableViewState="false" EditorMode="Advanced"
                    Language="CSS" Width="98%" Height="200px" />
            </td>
        </tr>
    </table>
</asp:Content>
