<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_TimeZones_Pages_TimeZone_Edit"
    Theme="Default" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" CodeFile="TimeZone_Edit.aspx.cs" %>

<%@ Register Src="~/CMSFormControls/System/LocalizableTextBox.ascx" TagName="LocalizableTextBox" TagPrefix="cms" %>

<%@ Register Src="~/CMSModules/TimeZones/Controls/TimeZoneRuleEdior.ascx"
    TagName="RuleEditor" TagPrefix="cms" %>
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <ajaxToolkit:ToolkitScriptManager ID="scrManager" runat="server" />
    <asp:Label runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false"
        Visible="false" />
    <asp:Label runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false"
        Visible="false" />
    <table style="vertical-align: top">
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel runat="server" ID="lblTimeZoneDisplayName" EnableViewState="false"
                    ResourceString="TimeZ.Edit.TimeZoneDisplayName" DisplayColon="true" />
            </td>
            <td>
                <cms:LocalizableTextBox ID="txtTimeZoneDisplayName" runat="server" CssClass="TextBoxField" MaxLength="200" />
                <cms:CMSRequiredFieldValidator ID="rfvDisplayName" runat="server" ControlToValidate="txtTimeZoneDisplayName:textbox"
                    Display="Dynamic" ValidationGroup="vgTimeZone"></cms:CMSRequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel runat="server" ID="lblTimeZoneName" EnableViewState="false" ResourceString="General.CodeName"
                    DisplayColon="true" />
            </td>
            <td>
                <cms:CMSTextBox ID="txtTimeZoneName" runat="server" CssClass="TextBoxField" MaxLength="200" />
                <cms:CMSRequiredFieldValidator ID="rfvName" runat="server" ControlToValidate="txtTimeZoneName"
                    Display="Dynamic" ValidationGroup="vgTimeZone"></cms:CMSRequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel runat="server" ID="lblTimeZoneGMT" EnableViewState="false" ResourceString="TimeZ.Edit.TimeZoneGMT" />
            </td>
            <td>
                <cms:CMSTextBox ID="txtTimeZoneGMT" runat="server" CssClass="TextBoxField" />
                <cms:CMSRequiredFieldValidator ID="rfvGMT" runat="server" ControlToValidate="txtTimeZoneGMT"
                    Display="Dynamic" ValidationGroup="vgTimeZone"></cms:CMSRequiredFieldValidator>
                <cms:CMSRangeValidator ID="rvGMTDouble" runat="server" ControlToValidate="txtTimeZoneGMT"
                    Display="Dynamic" Type="Double" ValidationGroup="vgTimeZone"></cms:CMSRangeValidator>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel runat="server" ID="lblTimeZoneDaylight" EnableViewState="false"
                    ResourceString="TimeZ.Edit.TimeZoneDaylight" />
            </td>
            <td>
                <asp:CheckBox ID="chkTimeZoneDaylight" AutoPostBack="true" runat="server" CssClass="CheckBoxMovedLeft" />
            </td>
        </tr>
        <asp:PlaceHolder runat="server" ID="plcDSTInfo">
            <tr>
                <td>
                    <cms:LocalizedLabel runat="server" ID="lblTimeZoneRuleStartIn" EnableViewState="false"
                        ResourceString="TimeZ.Edit.TimeZoneRuleStartIn" />
                </td>
                <td>
                    <%--<cms:CMSTextBox ID="txtTimeZoneRuleStart" runat="server" CssClass="TextBoxField" ReadOnly="true" />--%>
                    <asp:Label ID="lblTimeZoneRuleStart" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    <cms:LocalizedLabel runat="server" ID="lblTimeZoneRuleEndIn" EnableViewState="false"
                        ResourceString="TimeZ.Edit.TimeZoneRuleEndIn" />
                </td>
                <td>
                    <%--<cms:CMSTextBox ID="txtTimeZoneRuleEnd" runat="server" CssClass="TextBoxField" ReadOnly="true" />--%>
                    <asp:Label ID="lblTimeZoneRuleEnd" runat="server" />
                </td>
            </tr>
        </asp:PlaceHolder>
        <tr>
            <td>
                <cms:LocalizedLabel runat="server" ID="lblTimeZoneRuleStartRule" EnableViewState="false"
                    ResourceString="TimeZ.Edit.TimeZoneRuleStartRule" />
            </td>
            <td>
                <cms:RuleEditor ID="startRuleEditor" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                <cms:LocalizedLabel runat="server" ID="lblTimeZoneRuleEndRule" EnableViewState="false"
                    ResourceString="TimeZ.Edit.TimeZoneRuleEndRule" />
            </td>
            <td>
                <cms:RuleEditor ID="endRuleEditor" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <cms:LocalizedButton runat="server" ID="btnOk" OnClick="btnOK_Click" EnableViewState="false"
                    CssClass="SubmitButton" ValidationGroup="vgTimeZone" ResourceString="general.ok" />
            </td>
        </tr>
    </table>
</asp:Content>
