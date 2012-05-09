<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_ProjectManagement_Controls_UI_Project_Edit" CodeFile="Edit.ascx.cs" %>

<%@ Register Src="~/CMSFormControls/System/LocalizableTextBox.ascx" TagName="LocalizableTextBox" TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/Membership/FormControls/Users/SelectUser.ascx" TagName="UserSelector"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/Content/FormControls/Documents/SelectDocument.ascx"
    TagName="PageSelector" TagPrefix="cms" %>
<cms:LocalizedLabel runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false"
    Visible="false" />
<cms:LocalizedLabel runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false"
    Visible="false" />
<table style="vertical-align: top">
    <tr>
        <td class="FieldLabel">
            <cms:LocalizedLabel ID="lblProjectDisplayName" runat="server" EnableViewState="false"
                DisplayColon="true" ResourceString="general.displayname" AssociatedControlID="txtProjectDisplayName" />
        </td>
        <td>
            <cms:LocalizableTextBox ID="txtProjectDisplayName" runat="server" CssClass="TextBoxField" MaxLength="200"
                EnableViewState="false" />&nbsp;<cms:CMSRequiredFieldValidator ID="rfvProjectDisplayName"
                    runat="server" Display="Dynamic" ControlToValidate="txtProjectDisplayName:textbox" ValidationGroup="vgProject"
                    EnableViewState="false" />
        </td>
    </tr>
    <asp:PlaceHolder runat="server" ID="plcCodeName">
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel ID="lblProjectName" runat="server" EnableViewState="false" DisplayColon="true"
                    ResourceString="general.codename" AssociatedControlID="txtProjectName" />
            </td>
            <td>
                <cms:CMSTextBox ID="txtProjectName" runat="server" CssClass="TextBoxField" MaxLength="200"
                    EnableViewState="false" />&nbsp;<cms:CMSRequiredFieldValidator ID="rfvProjectName" runat="server"
                        Display="Dynamic" ControlToValidate="txtProjectName" ValidationGroup="vgProject"
                        EnableViewState="false" />
            </td>
        </tr>
    </asp:PlaceHolder>
    <tr>
        <td class="FieldLabel">
            <cms:LocalizedLabel ID="lblProjectDescription" runat="server" EnableViewState="false"
                DisplayColon="true" ResourceString="pm.project.goal" AssociatedControlID="txtProjectDescription" />
        </td>
        <td>
            <cms:LocalizableTextBox ID="txtProjectDescription" runat="server" TextMode="MultiLine" CssClass="TextAreaField"
                EnableViewState="false" />
        </td>
    </tr>
    <tr>
        <td class="FieldLabel">
            <cms:LocalizedLabel ID="lblProjectStartDate" runat="server" EnableViewState="false"
                DisplayColon="true" ResourceString="pm.project.startdate" />
        </td>
        <td>
            <cms:DateTimePicker ID="dtpProjectStartDate" runat="server" />
        </td>
    </tr>
    <tr>
        <td class="FieldLabel">
            <cms:LocalizedLabel ID="lblProjectDeadline" runat="server" EnableViewState="false"
                DisplayColon="true" ResourceString="pm.project.deadline" />
        </td>
        <td>
            <cms:DateTimePicker ID="dtpProjectDeadline" runat="server" />
        </td>
    </tr>
    <asp:PlaceHolder runat="server" ID="plcProgress">
    <tr>
        <td class="FieldLabel">
            <cms:LocalizedLabel ID="lblProjectProgress" runat="server" EnableViewState="false"
                DisplayColon="true" ResourceString="pm.projecttask.progress" AssociatedControlID="ltrProjectProgress" />
        </td>
        <td>
            <asp:Literal ID="ltrProjectProgress" runat="server" EnableViewState="false" />
        </td>
    </tr>
    </asp:PlaceHolder>
    <tr>
        <td class="FieldLabel">
            <cms:LocalizedLabel ID="lblProjectOwner" runat="server" EnableViewState="false" DisplayColon="true"
                ResourceString="pm.project.owner" />
        </td>
        <td>
            <cms:UserSelector ID="userSelector" runat="server" />
        </td>
    </tr>
    <tr>
        <td class="FieldLabel">
            <cms:LocalizedLabel ID="lblProjectStatusID" runat="server" EnableViewState="false"
                DisplayColon="true" ResourceString="pm.project.status" AssociatedControlID="drpProjectStatus" />
        </td>
        <td>
            <asp:DropDownList ID="drpProjectStatus" CssClass="DropDownField" runat="server">
            </asp:DropDownList>
        </td>
    </tr>
    <asp:PlaceHolder runat="server" ID="plcProjectPage">
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel ID="lblProjectPage" runat="server" EnableViewState="false" DisplayColon="true"
                    ResourceString="pm.project.projectpage" />
            </td>
            <td>
                <cms:PageSelector ID="pageSelector" runat="server" />
            </td>
        </tr>
    </asp:PlaceHolder>
    <tr>
        <td class="FieldLabel">
            <cms:LocalizedLabel ID="lblProjectAllowOrdering" runat="server" EnableViewState="false"
                DisplayColon="true" ResourceString="pm.project.allowordering" AssociatedControlID="chkProjectAllowOrdering" />
        </td>
        <td>
            <asp:CheckBox ID="chkProjectAllowOrdering" runat="server" EnableViewState="false"
                Checked="true" CssClass="CheckBoxMovedLeft" />
        </td>
    </tr>
    <tr>
        <td>
        </td>
        <td>
            <cms:CMSButton runat="server" ID="btnOk" EnableViewState="false" CssClass="SubmitButton"
                OnClick="btnOk_Click" ValidationGroup="vgProject" />
        </td>
    </tr>
</table>
