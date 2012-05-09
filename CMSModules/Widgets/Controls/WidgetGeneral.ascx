<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_Widgets_Controls_WidgetGeneral" CodeFile="WidgetGeneral.ascx.cs" %>

<%@ Register Src="~/CMSFormControls/System/LocalizableTextBox.ascx" TagName="LocalizableTextBox" TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/AdminControls/Controls/MetaFiles/File.ascx" TagName="File" TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/Widgets/FormControls/SelectWidgetCategory.ascx" TagName="SelectWidgetCategory"
    TagPrefix="cms" %>
<asp:Panel ID="pnlContent" runat="server" CssClass="PageContent">
    <asp:Label runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false"
        Visible="false" />
    <asp:Label runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false"
        Visible="false" />
    <table>
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel runat="server" ID="lblDisplayName" EnableViewState="false" ResourceString="general.displayname"
                    DisplayColon="true" AssociatedControlID="txtDisplayName" />
            </td>
            <td>
                <cms:LocalizableTextBox ID="txtDisplayName" runat="server" CssClass="TextBoxField" MaxLength="100" />
                <cms:CMSRequiredFieldValidator ID="rfvDisplayName" runat="server" ControlToValidate="txtDisplayName:textbox"
                    Display="Dynamic" ValidationGroup="Required" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel runat="server" ID="lblCodeName" EnableViewState="false" ResourceString="general.codename"
                    DisplayColon="true" AssociatedControlID="txtCodeName" />
            </td>
            <td>
                <cms:CMSTextBox ID="txtCodeName" runat="server" CssClass="TextBoxField" MaxLength="100" />
                <cms:CMSRequiredFieldValidator ID="rfvCodeName" runat="server" ControlToValidate="txtCodeName"
                    Display="Dynamic" ValidationGroup="Required" />
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
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel runat="server" ID="lblBasedOn" EnableViewState="false" ResourceString="widgets.basedon"
                    DisplayColon="true" AssociatedControlID="txtCodeName" />
            </td>
            <td>
                <cms:CMSTextBox ID="txtBasedOn" runat="server" CssClass="TextBoxField" ReadOnly="true"
                    Enabled="false" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel runat="server" ID="lblDescription" EnableViewState="false" ResourceString="general.description"
                    DisplayColon="true" AssociatedControlID="txtDescription" />
            </td>
            <td>
                <cms:LocalizableTextBox ID="txtDescription" runat="server" TextMode="MultiLine" CssClass="TextAreaField" />
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel runat="server" ID="lblThumbnail" EnableViewState="false" ResourceString="general.thumbnail"
                    DisplayColon="true" />
            </td>
            <td>
                <cms:File ID="thumbnailFile" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <cms:LocalizedButton runat="server" ID="btnOk" OnClick="btnOK_Click" EnableViewState="false"
                    ResourceString="general.ok" CssClass="SubmitButton" ValidationGroup="Required" />
            </td>
        </tr>
    </table>
</asp:Panel>
