<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_OnlineMarketing_Controls_UI_MVTest_Edit"
    CodeFile="Edit.ascx.cs" %>

<%@ Register Src="~/CMSFormControls/System/LocalizableTextBox.ascx" TagName="LocalizableTextBox" TagPrefix="cms" %>
<%@ Register Src="~/CMSFormControls/Cultures/SiteCultureSelectorall.ascx" TagName="cultureSelector"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/Content/FormControls/Documents/SelectSinglePath.ascx"
    TagName="pathSelector" TagPrefix="cms" %>
<cms:LocalizedLabel runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false"
    Visible="false" />
<cms:LocalizedLabel runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false"
    Visible="false" />
<table style="vertical-align: top">
    <tr>
        <td class="FieldLabel">
            <cms:LocalizedLabel ID="lblMVTestDisplayName" runat="server" EnableViewState="false"
                DisplayColon="true" ResourceString="general.displayname" AssociatedControlID="txtMVTestDisplayName" />
        </td>
        <td>
            <cms:LocalizableTextBox ID="txtMVTestDisplayName" runat="server" CssClass="TextBoxField" MaxLength="100"
                EnableViewState="false" />
            <cms:CMSRequiredFieldValidator ID="rfvMVTestDisplayName" runat="server" Display="Dynamic"
                ControlToValidate="txtMVTestDisplayName:textbox" ValidationGroup="vgMvtest" EnableViewState="false" />
        </td>
    </tr>
    <tr>
        <td class="FieldLabel">
            <cms:LocalizedLabel ID="lblMVTestCodeName" runat="server" EnableViewState="false" DisplayColon="true"
                ResourceString="mvtest.testCodename" AssociatedControlID="txtMVTestCodeName" />
        </td>
        <td>
            <cms:CMSTextBox ID="txtMVTestCodeName" runat="server" CssClass="TextBoxField" MaxLength="50"
                EnableViewState="false" />
            <cms:CMSRequiredFieldValidator ID="rfvMVTestCodeName" runat="server" Display="Dynamic" ControlToValidate="txtMVTestCodeName"
                ValidationGroup="vgMvtest" EnableViewState="false" />
        </td>
    </tr>
    <tr>
        <td class="FieldLabel">
            <cms:LocalizedLabel ID="lblMVTestDescription" runat="server" EnableViewState="false"
                DisplayColon="true" ResourceString="mvtest.description" AssociatedControlID="txtMVTestDescription" />
        </td>
        <td>
            <cms:CMSTextBox ID="txtMVTestDescription" runat="server" TextMode="MultiLine" CssClass="TextAreaField"
                EnableViewState="false" />
        </td>
    </tr>
    <asp:PlaceHolder runat="server" ID="plcMVTestPage">
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel ID="lblMVTestPage" runat="server" EnableViewState="false" DisplayColon="true"
                    ResourceString="mvtest.testpage" AssociatedControlID="ucPath" />
            </td>
            <td>
                <cms:pathSelector runat="server" ID="ucPath" IsLiveSite="false" />
            </td>
        </tr>
    </asp:PlaceHolder>
    <tr>
        <td class="FieldLabel">
            <cms:LocalizedLabel ID="lblMVTestCulture" runat="server" EnableViewState="false"
                DisplayColon="true" ResourceString="mvtest.testCulture" AssociatedControlID="ucCultureSelector" />
        </td>
        <td>
            <cms:cultureSelector runat="server" ID="ucCultureSelector" />
        </td>
    </tr>
    <tr>
        <td class="FieldLabel">
            <cms:LocalizedLabel ID="lblMVTestMaxConversions" runat="server" EnableViewState="false"
                DisplayColon="true" ResourceString="mvtest.targetConversions" AssociatedControlID="txtMVTestMaxConversions" />
        </td>
        <td>
            <table cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <cms:CMSTextBox ID="txtMVTestMaxConversions" runat="server" CssClass="CalendarTextBox"
                            MaxLength="0" EnableViewState="false" />
                    </td>
                    <td>
                        <cms:LocalizedRadioButton ID="radTotal" runat="server" Checked="true" GroupName="Conversion"
                            ResourceString="om.conversion.total" />
                        <cms:LocalizedRadioButton ID="radAnyVariant" runat="server" GroupName="Conversion"
                            ResourceString="om.conversion.anycombination" />&nbsp;&nbsp;
                        <cms:CMSRangeValidator ID="rfvMVTestMaxConversions" runat="server" Type="Integer" Display="Dynamic" ControlToValidate="txtMVTestMaxConversions"
                            ValidationGroup="vgAbTest" EnableViewState="false" />                            
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <asp:PlaceHolder runat="server" ID="pnlConversions">
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel ID="lblMVTestConversions" runat="server" EnableViewState="false"
                    DisplayColon="true" ResourceString="mvtest.currentconversions" AssociatedControlID="txtMVTestConversions" />
            </td>
            <td>
                <cms:CMSTextBox ID="txtMVTestConversions" runat="server" CssClass="CalendarTextBox"
                    MaxLength="0" EnableViewState="false" Enabled="false" />
            </td>
        </tr>
    </asp:PlaceHolder>
    <tr>
        <td class="FieldLabel">
            <cms:LocalizedLabel ID="lblMVTestOpenFrom" runat="server" EnableViewState="false"
                DisplayColon="true" ResourceString="mvtest.testStart" />
        </td>
        <td>
            <cms:DateTimePicker ID="dtpMVTestOpenFrom" runat="server" />
        </td>
    </tr>
    <tr>
        <td class="FieldLabel">
            <cms:LocalizedLabel ID="lblMVTestOpenTo" runat="server" EnableViewState="false" DisplayColon="true"
                ResourceString="mvtest.testEnd" />
        </td>
        <td>
            <cms:DateTimePicker ID="dtpMVTestOpenTo" runat="server" />
        </td>
    </tr>
    <tr>
        <td class="FieldLabel">
            <cms:LocalizedLabel ID="lblMVTestID" runat="server" EnableViewState="false" DisplayColon="true"
                ResourceString="mvtest.testEnabled" AssociatedControlID="chkMVTestEnabled" />
        </td>
        <td>
            <asp:CheckBox ID="chkMVTestEnabled" runat="server" EnableViewState="false" CssClass="CheckBoxMovedLeft" />
        </td>
    </tr>
    <tr>
        <td class="FieldLabel">
            <cms:LocalizedLabel ID="lblStatus" runat="server" EnableViewState="false" DisplayColon="true"
                ResourceString="general.status" />
        </td>
        <td>
            <cms:LocalizedLabel ID="lblStatusValue" runat="server" EnableViewState="false" />
        </td>
    </tr>
    <tr>
        <td>
        </td>
        <td>
            <cms:CMSButton runat="server" ID="btnOk" EnableViewState="false" CssClass="SubmitButton"
                OnClick="btnOk_Click" ValidationGroup="vgMvtest" />
        </td>
    </tr>
</table>
