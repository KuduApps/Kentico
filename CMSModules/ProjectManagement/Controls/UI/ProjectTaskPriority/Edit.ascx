<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_ProjectManagement_Controls_UI_Projecttaskpriority_Edit" CodeFile="Edit.ascx.cs" %>

<%@ Register Src="~/CMSFormControls/System/LocalizableTextBox.ascx" TagName="LocalizableTextBox" TagPrefix="cms" %>
<cms:LocalizedLabel runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false"
    Visible="false" />
<cms:LocalizedLabel runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false"
    Visible="false" />
<table style="vertical-align: top">
    <tr>
        <td class="FieldLabel">
            <cms:LocalizedLabel ID="lblTaskPriorityDisplayName" runat="server" EnableViewState="false" DisplayColon="true"
                ResourceString="general.displayname" AssociatedControlID="txtTaskPriorityDisplayName" />
        </td>
        <td>
            <cms:LocalizableTextBox ID="txtTaskPriorityDisplayName" runat="server" CssClass="TextBoxField" MaxLength="200"
                EnableViewState="false" />
            <cms:CMSRequiredFieldValidator ID="rfvTaskPriorityDisplayName" runat="server" Display="Dynamic"
                ControlToValidate="txtTaskPriorityDisplayName:textbox" ValidationGroup="vgProjecttaskpriority" EnableViewState="false" />
        </td>
    </tr>
    <tr>
        <td class="FieldLabel">
            <cms:LocalizedLabel ID="lblTaskPriorityName" runat="server" EnableViewState="false" DisplayColon="true"
                ResourceString="general.codename" AssociatedControlID="txtTaskPriorityName" />
        </td>
        <td>
            <cms:CMSTextBox ID="txtTaskPriorityName" runat="server" CssClass="TextBoxField" MaxLength="200"
                EnableViewState="false" />
            <cms:CMSRequiredFieldValidator ID="rfvTaskPriorityName" runat="server" Display="Dynamic"
                ControlToValidate="txtTaskPriorityName" ValidationGroup="vgProjecttaskpriority" EnableViewState="false" />
        </td>
    </tr>
    <tr>
        <td class="FieldLabel">
            <cms:LocalizedLabel ID="lblTaskPriorityDefault" runat="server" EnableViewState="false" DisplayColon="true"
                ResourceString="general.default" AssociatedControlID="chkTaskPriorityDefault" />
        </td>
        <td>
            <asp:CheckBox ID="chkTaskPriorityDefault" runat="server" EnableViewState="false" Checked="false"
                CssClass="CheckBoxMovedLeft" />
        </td>
    </tr>
    <tr>
        <td class="FieldLabel">
            <cms:LocalizedLabel ID="lblTaskPriorityEnabled" runat="server" EnableViewState="false" DisplayColon="true"
                ResourceString="general.enabled" AssociatedControlID="chkTaskPriorityEnabled" />
        </td>
        <td>
            <asp:CheckBox ID="chkTaskPriorityEnabled" runat="server" EnableViewState="false" Checked="true"
                CssClass="CheckBoxMovedLeft" />
        </td>
    </tr>
    <tr>
        <td>
        </td>
        <td>
            <cms:CMSButton runat="server" ID="btnOk" EnableViewState="false" CssClass="SubmitButton"
                OnClick="btnOk_Click" ValidationGroup="vgProjecttaskpriority" />
        </td>
    </tr>
</table>