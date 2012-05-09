<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MappingDialog.ascx.cs" Inherits="CMSModules_ContactManagement_Controls_UI_Contact_MappingDialog" %>

<%@ Register Src="~/CMSModules/AdminControls/Controls/Class/ClassFields.ascx" TagName="ClassFields" TagPrefix="uc" %>

<cms:LocalizedLabel ID="lblInfo" runat="server" EnableViewState="false" CssClass="InfoLabel" ResourceString="class.contactmapping" />
<cms:LocalizedCheckBox ID="chkOverwrite" runat="server" ResourceString="class.allowoverwritecontactinfo" CssClass="ContentCheckbox" /><br />
<asp:Panel ID="pnlGeneral" runat="server" CssClass="ContentPanel">
    <table>
        <tr>
            <td class="ContactLabel">
                <cms:LocalizedLabel ID="lblFirstName" runat="server" EnableViewState="false" ResourceString="om.contact.firstname"
                    DisplayColon="true" />
            </td>
            <td class="ContactControl">
                <uc:ClassFields ID="fldFirstName" runat="server" FieldDataType="Text" />
            </td>
            <td class="ContactLabel">
                <cms:LocalizedLabel ID="lblLastName" runat="server" EnableViewState="false" ResourceString="om.contact.lastname"
                    DisplayColon="true" />
            </td>
            <td class="ContactControlRight">
                <uc:ClassFields ID="fldLastName" runat="server" FieldDataType="Text" />
            </td>
        </tr>
        <tr>
            <td class="ContactLabel">
                <cms:LocalizedLabel ID="lblMiddleName" runat="server" EnableViewState="false" ResourceString="om.contact.middlename"
                    DisplayColon="true" />
            </td>
            <td class="ContactControl">
                <uc:ClassFields ID="fldMiddleName" runat="server" FieldDataType="Text" />
            </td>
            <td class="ContactLabel">
                <cms:LocalizedLabel ID="lblSalutation" runat="server" EnableViewState="false" ResourceString="om.contact.salutation"
                    DisplayColon="true" />
            </td>
            <td class="ContactControlRight">
                <uc:ClassFields ID="fldSalutation" runat="server" FieldDataType="Text" />
            </td>
        </tr>
        <tr>
            <td class="ContactLabel">
                <cms:LocalizedLabel ID="lblTitleBefore" runat="server" EnableViewState="false" ResourceString="om.contact.titlebefore"
                    DisplayColon="true" />
            </td>
            <td class="ContactControl">
                <uc:ClassFields ID="fldTitleBefore" runat="server" FieldDataType="Text" />
            </td>
            <td class="ContactLabel">
                <cms:LocalizedLabel ID="lblTitleAfter" runat="server" EnableViewState="false" ResourceString="om.contact.titleafter"
                    DisplayColon="true" />
            </td>
            <td class="ContactControlRight">
                <uc:ClassFields ID="fldTitleAfter" runat="server" FieldDataType="Text" />
            </td>
        </tr>
    </table>
</asp:Panel>
<asp:Panel ID="pnlPersonal" runat="server" CssClass="ContentPanel">
    <table>
        <tr>
            <td class="ContactLabel">
                <cms:LocalizedLabel ID="lblBirthday" runat="server" EnableViewState="false" ResourceString="om.contact.birthday"
                    DisplayColon="true" />
            </td>
            <td class="ContactControl">
                <uc:ClassFields ID="fldBirthday" runat="server" FieldDataType="DateTime" />
            </td>
            <td class="ContactLabel">
                <cms:LocalizedLabel ID="lblJobTitle" runat="server" EnableViewState="false" ResourceString="om.contact.jobtitle"
                    DisplayColon="true" />
            </td>
            <td class="ContactControlRight">
                <uc:ClassFields ID="fldJobTitle" runat="server" FieldDataType="Text" />
            </td>
        </tr>
        <tr>
            <td class="ContactLabel">
                <cms:LocalizedLabel ID="lblGender" runat="server" EnableViewState="false" ResourceString="om.contact.gender"
                    DisplayColon="true" />
            </td>
            <td class="ContactControl">
                <uc:ClassFields ID="fldGender" runat="server" FieldDataType="Integer" />
            </td>
            <td colspan="2">
                &nbsp;
            </td>
        </tr>
    </table>
</asp:Panel>
<asp:Panel ID="pnlAddress" runat="server" CssClass="ContentPanel">
    <table>
        <tr>
            <td class="ContactLabel">
                <cms:LocalizedLabel ID="lblAddress1" runat="server" EnableViewState="false" ResourceString="om.contact.address1"
                    DisplayColon="true" />
            </td>
            <td class="ContactControl">
                <uc:ClassFields ID="fldAddress1" runat="server" FieldDataType="Text" />
            </td>
            <td class="ContactLabel">
                <cms:LocalizedLabel ID="lblMobilePhone" runat="server" EnableViewState="false" ResourceString="om.contact.mobilephone"
                    DisplayColon="true" />
            </td>
            <td class="ContactControlRight">
                <uc:ClassFields ID="fldMobilePhone" runat="server" FieldDataType="Text" />
            </td>
        </tr>
        <tr>
            <td class="ContactLabel">
                <cms:LocalizedLabel ID="lblAddress2" runat="server" EnableViewState="false" ResourceString="om.contact.address2"
                    DisplayColon="true" />
            </td>
            <td class="ContactControl">
                <uc:ClassFields ID="fldAddress2" runat="server" FieldDataType="Text" />
            </td>
            <td class="ContactLabel">
                <cms:LocalizedLabel ID="lblHomePhone" runat="server" EnableViewState="false" ResourceString="om.contact.homephone"
                    DisplayColon="true" />
            </td>
            <td class="ContactControlRight">
                <uc:ClassFields ID="fldHomePhone" runat="server" FieldDataType="Text" />
            </td>
        </tr>
        <tr>
            <td class="ContactLabel">
                <cms:LocalizedLabel ID="lblCity" runat="server" EnableViewState="false" ResourceString="general.city"
                    DisplayColon="true" />
            </td>
            <td class="ContactControl">
                <uc:ClassFields ID="fldCity" runat="server" FieldDataType="Text" />
            </td>
            <td class="ContactLabel">
                <cms:LocalizedLabel ID="lblBusinessPhone" runat="server" EnableViewState="false"
                    ResourceString="om.contact.businessphone" DisplayColon="true" />
            </td>
            <td class="ContactControlRight">
                <uc:ClassFields ID="fldBusinessPhone" runat="server" FieldDataType="Text" />
            </td>
        </tr>
        <tr>
            <td class="ContactLabel">
                <cms:LocalizedLabel ID="lblZip" runat="server" EnableViewState="false" ResourceString="general.zip"
                    DisplayColon="true" />
            </td>
            <td class="ContactControl">
                <uc:ClassFields ID="fldZip" runat="server" FieldDataType="Text" />
            </td>
            <td class="ContactLabel">
                <cms:LocalizedLabel ID="lblEmail" runat="server" EnableViewState="false" ResourceString="general.email"
                    DisplayColon="true" />
            </td>
            <td class="ContactControlRight">
                <uc:ClassFields ID="fldEmail" runat="server" FieldDataType="Text" />
            </td>
        </tr>
        <tr>
            <td class="ContactLabel">
                <cms:LocalizedLabel ID="lblCountry" runat="server" EnableViewState="false" ResourceString="general.country"
                    DisplayColon="true" />
            </td>
            <td class="ContactControl">
                <uc:ClassFields ID="fldCountry" runat="server" FieldDataType="Unknown" />
            </td>
            <td class="ContactLabel">
                <cms:LocalizedLabel ID="lblURL" runat="server" EnableViewState="false" ResourceString="om.contact.website"
                    DisplayColon="true" />
            </td>
            <td class="ContactControlRight">
                <uc:ClassFields ID="fldURL" runat="server" FieldDataType="Text" />
            </td>
        </tr>
        <tr>
            <td class="ContactLabel">
                <cms:LocalizedLabel ID="lblState" runat="server" EnableViewState="false" ResourceString="general.state"
                    DisplayColon="true" />
            </td>
            <td class="ContactControl">
                <uc:ClassFields ID="fldState" runat="server" FieldDataType="Unknown" />
            </td>
            <td colspan="2">&nbsp;</td>
        </tr>
    </table>
</asp:Panel>
<asp:Panel ID="pnlCustom" runat="server" CssClass="ContentPanel" Visible="false">
    <asp:PlaceHolder ID="plcCustom" runat="server" />
</asp:Panel>