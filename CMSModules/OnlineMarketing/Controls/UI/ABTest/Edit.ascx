<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_OnlineMarketing_Controls_UI_AbTest_Edit"
    CodeFile="Edit.ascx.cs" %>

<%@ Register Src="~/CMSFormControls/System/LocalizableTextBox.ascx" TagName="LocalizableTextBox" TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/Content/FormControls/Documents/SelectSinglePath.ascx"
    TagName="pathSelector" TagPrefix="cms" %>
<%@ Register Src="~/CMSFormControls/Cultures/SiteCultureSelectorall.ascx" TagName="cultureSelector"
    TagPrefix="cms" %>
<cms:LocalizedLabel runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false"
    Visible="false" />
<cms:LocalizedLabel runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false"
    Visible="false" />
<table style="vertical-align: top">
    <tr>
        <td class="FieldLabel">
            <cms:LocalizedLabel ID="lblABTestDisplayName" runat="server" EnableViewState="false"
                DisplayColon="true" ResourceString="general.displayname" AssociatedControlID="txtABTestDisplayName" />
        </td>
        <td>
            <cms:LocalizableTextBox ID="txtABTestDisplayName" runat="server" CssClass="TextBoxField" MaxLength="100"
                EnableViewState="false" />
            <cms:CMSRequiredFieldValidator ID="rfvABTestDisplayName" runat="server" Display="Dynamic"
                ControlToValidate="txtABTestDisplayName:textbox" ValidationGroup="vgAbTest" EnableViewState="false" />
        </td>
    </tr>
    <tr>
        <td class="FieldLabel">
            <cms:LocalizedLabel ID="lblABTestName" runat="server" EnableViewState="false" DisplayColon="true"
                ResourceString="general.codename" AssociatedControlID="txtABTestName" />
        </td>
        <td>
            <cms:CMSTextBox ID="txtABTestName" runat="server" CssClass="TextBoxField" MaxLength="50"
                EnableViewState="false" />
            <cms:CMSRequiredFieldValidator ID="rfvABTestName" runat="server" Display="Dynamic" ControlToValidate="txtABTestName"
                ValidationGroup="vgAbTest" EnableViewState="false" />
        </td>
    </tr>
    <tr>
        <td class="FieldLabel">
            <cms:LocalizedLabel ID="lblABTestDescription" runat="server" EnableViewState="false"
                DisplayColon="true" ResourceString="abtesting.description" AssociatedControlID="txtABTestDescription" />
        </td>
        <td>
            <cms:CMSTextBox ID="txtABTestDescription" runat="server" TextMode="MultiLine" CssClass="TextAreaField"
                EnableViewState="false" />
        </td>
    </tr>
    <asp:PlaceHolder runat="server" ID="plcOriginalPage">
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel ID="lblABTestOriginalPage" runat="server" EnableViewState="false"
                    DisplayColon="true" ResourceString="abtesting.originalpage" AssociatedControlID="ucPath" />
            </td>
            <td>
                <cms:pathSelector runat="server" ID="ucPath" IsLiveSite="false" />
            </td>
        </tr>
    </asp:PlaceHolder>
    <tr>
        <td>
            <cms:LocalizedLabel ID="lblCulture" runat="server" EnableViewState="false" DisplayColon="true"
                ResourceString="abtesting.abtest.testculture" AssociatedControlID="chkABTestEnabled" />
        </td>
        <td>
            <cms:cultureSelector runat="server" ID="ucCultureSelector" />
        </td>
    </tr>
    <tr>
        <td class="FieldLabel">
            <cms:LocalizedLabel ID="lblABTestMaxConversions" runat="server" EnableViewState="false"
                DisplayColon="true" ResourceString="abtesting.targetconversions" AssociatedControlID="txtABTestMaxConversions" />
        </td>
        <td>
            <table cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <cms:CMSTextBox ID="txtABTestMaxConversions" runat="server" CssClass="CalendarTextBox"
                            MaxLength="0" EnableViewState="false" />
                    </td>
                    <td>
                        <cms:LocalizedRadioButton ID="radTotal" runat="server" Checked="true" GroupName="Conversion"
                            ResourceString="om.conversion.total" />
                        <cms:LocalizedRadioButton ID="radAnyVariant" runat="server" GroupName="Conversion"
                            ResourceString="om.conversion.anyvariant" />&nbsp;&nbsp;
                        <cms:CMSRangeValidator ID="rfvABTestMaxConversions" runat="server" Type="Integer" Display="Dynamic" ControlToValidate="txtABTestMaxConversions"
                            ValidationGroup="vgAbTest" EnableViewState="false" />                            
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <asp:PlaceHolder runat="server" ID="pnlConversions">
        <tr>
            <td class="FieldLabel">
                <cms:LocalizedLabel ID="lblABTestConversions" runat="server" EnableViewState="false"
                    DisplayColon="true" ResourceString="abtesting.currentconversions" AssociatedControlID="txtABTestConversions" />
            </td>
            <td>
                <cms:CMSTextBox ID="txtABTestConversions" runat="server" CssClass="CalendarTextBox"
                    MaxLength="0" EnableViewState="false" Enabled="false" />
            </td>
        </tr>
    </asp:PlaceHolder>
    <tr>
        <td class="FieldLabel">
            <cms:LocalizedLabel ID="lblABTestOpenFrom" runat="server" EnableViewState="false"
                DisplayColon="true" ResourceString="abtesting.testfrom" />
        </td>
        <td>
            <cms:DateTimePicker ID="dtpABTestOpenFrom" runat="server" />
        </td>
    </tr>
    <tr>
        <td class="FieldLabel">
            <cms:LocalizedLabel ID="lblABTestOpenTo" runat="server" EnableViewState="false" DisplayColon="true"
                ResourceString="abtesting.testto" />
        </td>
        <td>
            <cms:DateTimePicker ID="dtpABTestOpenTo" runat="server" />
        </td>
    </tr>
    <tr>
        <td class="FieldLabel">
            <cms:LocalizedLabel ID="lblABTestEnabled" runat="server" EnableViewState="false"
                DisplayColon="true" ResourceString="abtesting.testenabled" AssociatedControlID="chkABTestEnabled" />
        </td>
        <td>
            <asp:CheckBox ID="chkABTestEnabled" runat="server" EnableViewState="false" CssClass="CheckBoxMovedLeft" />
        </td>
    </tr>
    <tr>
        <td class="FieldLabel">
            <cms:LocalizedLabel ID="lblStatus" runat="server" EnableViewState="false" DisplayColon="true"
                ResourceString="general.status" />
        </td>
        <td>
            <asp:Literal ID="ltrStatusValue" runat="server"></asp:Literal>
        </td>
    </tr>
    <tr>
        <td>
        </td>
        <td>
            <cms:CMSButton runat="server" ID="btnOk" EnableViewState="false" CssClass="SubmitButton"
                OnClick="btnOk_Click" ValidationGroup="vgAbTest" />
        </td>
    </tr>
</table>
