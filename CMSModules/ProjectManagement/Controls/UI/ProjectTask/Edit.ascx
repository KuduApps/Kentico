<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_ProjectManagement_Controls_UI_ProjectTask_Edit"
    CodeFile="Edit.ascx.cs" %>

<%@ Register Src="~/CMSFormControls/System/LocalizableTextBox.ascx" TagName="LocalizableTextBox" TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/Membership/FormControls/Users/SelectUser.ascx" TagName="UserSelector"
    TagPrefix="cms" %>
<cms:LocalizedLabel runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false"
    Visible="false" />
<cms:LocalizedLabel runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false"
    Visible="false" />
<table style="vertical-align: top" class="ProjectTaskTable">
    <tr>
        <td class="FieldLabel">
            <cms:LocalizedLabel ID="lblProjectTaskDisplayName" runat="server" EnableViewState="false"
                DisplayColon="true" ResourceString="general.title" AssociatedControlID="txtProjectTaskDisplayName" />
        </td>
        <td>
            <cms:LocalizableTextBox ID="txtProjectTaskDisplayName" runat="server" CssClass="TextBoxField"
                MaxLength="200" EnableViewState="false" />
            <cms:CMSRequiredFieldValidator ID="rfvProjectTaskDisplayName" runat="server" Display="Dynamic"
                ControlToValidate="txtProjectTaskDisplayName:textbox" ValidationGroup="vgProjectTask"
                EnableViewState="false" />
        </td>
    </tr>
    <tr>
        <td class="FieldLabel">
            <cms:LocalizedLabel ID="lblProjectTaskOwnerID" runat="server" EnableViewState="false"
                DisplayColon="true" ResourceString="pm.projecttask.owner" />
        </td>
        <td>
            <cms:UserSelector ID="selectorOwner" runat="server" />
        </td>
    </tr>
    <tr>
        <td class="FieldLabel">
            <cms:LocalizedLabel ID="lblProjectTaskProgress" runat="server" EnableViewState="false"
                DisplayColon="true" ResourceString="pm.projecttask.progress" AssociatedControlID="txtProjectTaskProgress" />
        </td>
        <td>
            <cms:CMSTextBox ID="txtProjectTaskProgress" runat="server" MaxLength="0" EnableViewState="false" />
            %
            <cms:CMSRegularExpressionValidator ID="regexProgress" runat="server" ControlToValidate="txtProjectTaskProgress" ValidationGroup="vgProjectTask"
                ValidationExpression="[0-9]*" Display="Dynamic"></cms:CMSRegularExpressionValidator>
        </td>
    </tr>
    <tr>
        <td class="FieldLabel">
            <cms:LocalizedLabel ID="lblProjectTaskEstimate" runat="server" EnableViewState="false"
                DisplayColon="true" ResourceString="pm.projecttask.estimate" AssociatedControlID="txtProjectTaskHours" />
        </td>
        <td>
            <cms:CMSTextBox ID="txtProjectTaskHours" runat="server" MaxLength="10" EnableViewState="false" />
            <asp:Label ID="lblProjectTaskHours" runat="server" EnableViewState="false" />
            <cms:CMSRangeValidator ID="rvHours" runat="server" ControlToValidate="txtProjectTaskHours"
                MaximumValue="9999999999" MinimumValue="0" Type="Double" Display="Dynamic" EnableViewState="false" />
        </td>
    </tr>
    <tr>
        <td class="FieldLabel">
            <cms:LocalizedLabel ID="lblProjectTaskDeadline" runat="server" EnableViewState="false"
                DisplayColon="true" ResourceString="pm.projecttask.deadline" />
        </td>
        <td>
            <cms:DateTimePicker ID="dtpProjectTaskDeadline" runat="server" />
        </td>
    </tr>
    <tr>
        <td class="FieldLabel">
            <cms:LocalizedLabel ID="lblProjectTaskStatusID" runat="server" EnableViewState="false"
                DisplayColon="true" ResourceString="pm.projecttask.status" AssociatedControlID="drpTaskStatus" />
        </td>
        <td>
            <asp:DropDownList ID="drpTaskStatus" CssClass="DropDownField" runat="server">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td class="FieldLabel">
            <cms:LocalizedLabel ID="lblProjectTaskPriorityID" runat="server" EnableViewState="false"
                DisplayColon="true" ResourceString="pm.projecttask.priority" AssociatedControlID="drpTaskPriority" />
        </td>
        <td>
            <asp:DropDownList ID="drpTaskPriority" CssClass="DropDownField" runat="server">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td class="FieldLabel">
            <cms:LocalizedLabel ID="lblProjectTaskIsPrivate" runat="server" EnableViewState="false"
                DisplayColon="true" ResourceString="pm.projecttask.isprivate" AssociatedControlID="chkProjectTaskIsPrivate" />
        </td>
        <td>
            <asp:CheckBox ID="chkProjectTaskIsPrivate" runat="server" EnableViewState="false"
                Checked="false" CssClass="CheckBoxMovedLeft" />
        </td>
    </tr>
    <tr>
        <td class="FieldLabel">
            <cms:LocalizedLabel ID="lblProjectTaskAssignedToUserID" runat="server" EnableViewState="false"
                DisplayColon="true" ResourceString="pm.projecttask.assingedto" />
        </td>
        <td>
            <cms:UserSelector ID="selectorAssignee" runat="server" />
        </td>
    </tr>
    <tr>
        <td class="FieldLabel">
            <cms:LocalizedLabel ID="lblProjectTaskDescription" runat="server" EnableViewState="false"
                DisplayColon="true" ResourceString="pm.projecttask.description" AssociatedControlID="htmlTaskDescription" />
        </td>
        <td>
            <div style="height: 270px;">
                <cms:CMSHtmlEditor UseValueDirtyBit="true" ID="htmlTaskDescription" runat="server"
                    Width="500px" Height="200px" />
            </div>
        </td>
    </tr>
    <asp:PlaceHolder ID="plcTaskUrl" runat="server">
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel ID="lblTaskUrl" runat="server" EnableViewState="false" DisplayColon="true"
                    ResourceString="pm.projecttask.taskurl" AssociatedControlID="txtTaskUrl" />
            </td>
            <td>
                <cms:CMSTextBox ReadOnly="true" ID="txtTaskUrl" runat="server" CssClass="TextBoxField" />
            </td>
        </tr>
    </asp:PlaceHolder>
    <tr>
        <td>
        </td>
        <td>
            <cms:CMSButton runat="server" ID="btnOk" EnableViewState="false" CssClass="SubmitButton"
                OnClick="btnOk_Click" ValidationGroup="vgProjectTask" />
        </td>
    </tr>
</table>
