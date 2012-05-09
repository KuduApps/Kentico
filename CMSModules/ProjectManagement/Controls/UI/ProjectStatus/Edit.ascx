<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_ProjectManagement_Controls_UI_Projectstatus_Edit" CodeFile="Edit.ascx.cs" %>

<%@ Register Src="~/CMSFormControls/System/LocalizableTextBox.ascx" TagName="LocalizableTextBox" TagPrefix="cms" %>
    
<cms:LocalizedLabel runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false"
    Visible="false" />
<cms:LocalizedLabel runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false"
    Visible="false" />
<table style="vertical-align: top">
    <tr>
        <td class="FieldLabel">
            <cms:LocalizedLabel ID="lblStatusDisplayName" runat="server" EnableViewState="false" DisplayColon="true"
                ResourceString="general.displayname" AssociatedControlID="txtStatusDisplayName" />
        </td>
        <td>
            <cms:LocalizableTextBox ID="txtStatusDisplayName" runat="server" CssClass="TextBoxField" MaxLength="200"
                EnableViewState="false" />
            <cms:CMSRequiredFieldValidator ID="rfvStatusDisplayName" runat="server" Display="Dynamic"
                ControlToValidate="txtStatusDisplayName:textbox" ValidationGroup="vgProjectstatus" EnableViewState="false" />
        </td>
    </tr>
    <tr>
        <td class="FieldLabel">
            <cms:LocalizedLabel ID="lblStatusName" runat="server" EnableViewState="false" DisplayColon="true"
                ResourceString="general.codename" AssociatedControlID="txtStatusName" />
        </td>
        <td>
            <cms:CMSTextBox ID="txtStatusName" runat="server" CssClass="TextBoxField" MaxLength="200"
                EnableViewState="false" />
            <cms:CMSRequiredFieldValidator ID="rfvStatusName" runat="server" Display="Dynamic"
                ControlToValidate="txtStatusName" ValidationGroup="vgProjectstatus" EnableViewState="false" />
        </td>
    </tr>
    <tr>
        <td class="FieldLabel">
            <cms:LocalizedLabel ID="lblStatusColor" runat="server" EnableViewState="false" DisplayColon="true"
                ResourceString="pm.projectstatus.edit.color" AssociatedControlID="colorPicker" />
        </td>
        <td>
            <cms:ColorPicker ID="colorPicker" runat="server"  />
        </td>
    </tr>
    <tr>
        <td class="FieldLabel">
            <cms:LocalizedLabel ID="lblStatusIcon" runat="server" EnableViewState="false" DisplayColon="true"
                ResourceString="pm.projectstatus.edit.icon" AssociatedControlID="txtStatusIcon" />
        </td>
        <td>
            <cms:CMSTextBox ID="txtStatusIcon" runat="server" CssClass="TextBoxField" MaxLength="450"
                EnableViewState="false" />
        </td>
    </tr>
    <tr>
        <td class="FieldLabel">
            <cms:LocalizedLabel ID="lblStatusIsNotStarted" runat="server" EnableViewState="false" DisplayColon="true"
                ResourceString="pm.projectstatus.isnotstartedstatus" AssociatedControlID="chkStatusIsNotStarted" />
        </td>
        <td>
            <asp:CheckBox ID="chkStatusIsNotStarted" runat="server" EnableViewState="false" 
                CssClass="CheckBoxMovedLeft" />
        </td>
    </tr>
    <tr>
        <td class="FieldLabel">
            <cms:LocalizedLabel ID="lblStatusIsFinished" runat="server" EnableViewState="false" DisplayColon="true"
                ResourceString="pm.projectstatus.isfinishstatus" AssociatedControlID="chkStatusIsFinished" />
        </td>
        <td>
            <asp:CheckBox ID="chkStatusIsFinished" runat="server" EnableViewState="false" 
                CssClass="CheckBoxMovedLeft" />
        </td>
    </tr>
    <tr>
        <td class="FieldLabel">
            <cms:LocalizedLabel ID="lblStatusEnabled" runat="server" EnableViewState="false" DisplayColon="true"
                ResourceString="general.enabled" AssociatedControlID="chkStatusEnabled" />
        </td>
        <td>
            <asp:CheckBox ID="chkStatusEnabled" runat="server" EnableViewState="false" Checked="true"
                CssClass="CheckBoxMovedLeft" />
        </td>
    </tr>
    <tr>
        <td>
        </td>
        <td>
            <cms:CMSButton runat="server" ID="btnOk" EnableViewState="false" CssClass="SubmitButton"
                OnClick="btnOk_Click" ValidationGroup="vgProjectstatus" />
        </td>
    </tr>
</table>