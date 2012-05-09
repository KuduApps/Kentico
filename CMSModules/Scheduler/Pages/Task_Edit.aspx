<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Scheduler_Pages_Task_Edit"
    Theme="Default" ValidateRequest="false" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master"
    Title="Scheduled tasks - Task Edit" CodeFile="Task_Edit.aspx.cs" %>

<%@ Register Src="~/CMSFormControls/System/LocalizableTextBox.ascx" TagName="LocalizableTextBox" TagPrefix="cms" %>

<%@ Register Src="~/CMSAdminControls/UI/Selectors/ScheduleInterval.ascx" TagName="ScheduleInterval"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSFormControls/SelectModule.ascx" TagPrefix="cms" TagName="SelectModule" %>
<asp:Content ContentPlaceHolderID="plcContent" runat="server">
    <asp:Label runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false"
        Visible="false" />
    <asp:Label runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false"
        Visible="false" />
    <table style="vertical-align: top">
        <tr>
            <td>
                <asp:Label runat="server" ID="lblTaskDisplayName" EnableViewState="false" />
            </td>
            <td>
                <cms:LocalizableTextBox ID="txtTaskDisplayName" runat="server" CssClass="TextBoxField" MaxLength="200" />
                <cms:CMSRequiredFieldValidator ID="rfvDisplayName" runat="server" ControlToValidate="txtTaskDisplayName:textbox"
                    Display="Dynamic"></cms:CMSRequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label runat="server" ID="lblTaskName" EnableViewState="false" />
            </td>
            <td>
                <cms:CMSTextBox ID="txtTaskName" runat="server" CssClass="TextBoxField" MaxLength="200" />
                <cms:CMSRequiredFieldValidator ID="rfvName" runat="server" ControlToValidate="txtTaskName"
                    Display="Dynamic"></cms:CMSRequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label runat="server" ID="lblTaskAssemblyName" EnableViewState="false" />
            </td>
            <td>
                <cms:CMSTextBox ID="txtTaskAssemblyName" runat="server" CssClass="TextBoxField" MaxLength="200" />
                <cms:CMSRequiredFieldValidator ID="rfvAssembly" runat="server" ControlToValidate="txtTaskAssemblyName"
                    Display="Dynamic"></cms:CMSRequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label runat="server" ID="lblTaskClass" EnableViewState="false" />
            </td>
            <td>
                <cms:CMSTextBox ID="txtTaskClass" runat="server" CssClass="TextBoxField" MaxLength="200" />
                <cms:CMSRequiredFieldValidator ID="rfvClass" runat="server" ControlToValidate="txtTaskClass"
                    Display="Dynamic"></cms:CMSRequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label runat="server" ID="lblTaskInterval" EnableViewState="false" />
            </td>
            <td>
                <cms:ScheduleInterval ID="ScheduleInterval1" runat="server" />
                <br />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label runat="server" ID="lblTaskData" EnableViewState="false" />
            </td>
            <td>
                <cms:CMSTextBox ID="txtTaskData" runat="server" TextMode="MultiLine" CssClass="TextAreaField" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label runat="server" ID="lblTaskEnabled" EnableViewState="false" />
            </td>
            <td>
                <asp:CheckBox ID="chkTaskEnabled" runat="server" Checked="true" CssClass="CheckBoxMovedLeft" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label runat="server" ID="lblTaskDeleteAfterLastRun" EnableViewState="false" />
            </td>
            <td>
                <asp:CheckBox ID="chkTaskDeleteAfterLastRun" runat="server" Checked="false" CssClass="CheckBoxMovedLeft" />
            </td>
        </tr>
        <tr>
            <td>
                <cms:LocalizedLabel runat="server" ID="lblRunInSeparateThread" ResourceString="scheduledtask.runinseparatethread"
                    DisplayColon="true" EnableViewState="false" />
            </td>
            <td>
                <asp:CheckBox ID="chkRunTaskInSeparateThread" runat="server" Checked="false" CssClass="CheckBoxMovedLeft" />
            </td>
        </tr>
        <asp:PlaceHolder ID="plcAllowExternalService" runat="server">
            <tr>
                <td>
                    <cms:LocalizedLabel runat="server" ID="lblTaskAllowExternalService" ResourceString="ScheduledTask.TaskAllowExternalService"
                        DisplayColon="true" EnableViewState="false" />
                </td>
                <td>
                    <asp:CheckBox ID="chkTaskAllowExternalService" runat="server" Checked="true" CssClass="CheckBoxMovedLeft" />
                </td>
            </tr>
        </asp:PlaceHolder>
        <asp:PlaceHolder ID="plcUseExternalService" runat="server">
            <tr>
                <td>
                    <cms:LocalizedLabel runat="server" ID="lblTaskUseExternalService" ResourceString="ScheduledTask.TaskUseExternalService"
                        DisplayColon="true" EnableViewState="false" />
                </td>
                <td>
                    <asp:CheckBox ID="chkTaskUseExternalService" runat="server" Checked="false" CssClass="CheckBoxMovedLeft" />
                </td>
            </tr>
        </asp:PlaceHolder>
        <tr>
            <td>
                <asp:Label runat="server" ID="lblServerName" EnableViewState="false" />
            </td>
            <td>
                <cms:CMSTextBox ID="txtServerName" runat="server" CssClass="TextBoxField" MaxLength="100" /><br />
                <asp:CheckBox ID="chkAllServers" runat="server" Visible="false" />
            </td>
        </tr>
        <asp:PlaceHolder runat="server" ID="plcDevelopment">
            <tr>
                <td class="FieldLabel">
                    <cms:LocalizedLabel runat="server" ID="lblModule" EnableViewState="false" ResourceString="General.Module" />
                </td>
                <td>
                    <cms:SelectModule ID="drpModule" runat="server" DisplayNone="true" DisplayAllModules="true"
                        IsLiveSite="false" />
                </td>
            </tr>
        </asp:PlaceHolder>
        <tr>
            <td>
            </td>
            <td>
                <cms:CMSButton runat="server" ID="btnOk" OnClick="btnOK_Click" EnableViewState="false"
                    CssClass="SubmitButton" />
            </td>
        </tr>
    </table>
</asp:Content>
