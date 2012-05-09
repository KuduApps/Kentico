<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_ContactManagement_Controls_UI_Account_Edit"
    CodeFile="Edit.ascx.cs" %>
<%@ Register Src="~/CMSModules/ContactManagement/FormControls/AccountStatusSelector.ascx"
    TagName="AccountStatusSelector" TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/ContactManagement/FormControls/AccountSelector.ascx"
    TagName="AccountSelector" TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/Membership/FormControls/Users/SelectUser.ascx" TagName="SelectUser"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSFormControls/Inputs/EmailInput.ascx" TagName="EmailInput"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSFormControls/CountrySelector.ascx" TagName="CountrySelector"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/ContactManagement/FormControls/ContactSelector.ascx"
    TagName="ContactSelector" TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/ContactManagement/FormControls/ContactRoleSelector.ascx"
    TagName="ContactRoleSelector" TagPrefix="cms" %>
<cms:LocalizedLabel ID="lblMergedInto" runat="server" EnableViewState="false" DisplayColon="true" /><asp:Literal
    ID="ltlButton" runat="server" EnableViewState="false" />
<asp:Panel ID="pnlGeneral" runat="server" CssClass="ContentPanel">
    <table>
        <tr>
            <td class="AccountLabelLeft">
                <cms:LocalizedLabel ID="lblName" runat="server" EnableViewState="false" ResourceString="om.account.name"
                    DisplayColon="true" AssociatedControlID="txtName" />
            </td>
            <td class="AccountControlLeft">
                <cms:CMSTextBox ID="txtName" runat="server" CssClass="TextBoxField" MaxLength="200" />
            </td>
            <td class="AccountLabelRight">
                <cms:LocalizedLabel ID="lblParentAccount" runat="server" EnableViewState="false"
                    ResourceString="om.account.subsidiaryof" DisplayColon="true" AssociatedControlClientID="parentAccount" />
            </td>
            <td class="AccountControlRight">
                <cms:AccountSelector ID="parentAccount" runat="server" IsLiveSite="false" />
            </td>
        </tr>
        <tr>
            <td class="AccountLabelLeft">
                <cms:LocalizedLabel ID="lblStatus" runat="server" EnableViewState="false" ResourceString="om.accountstatus"
                    DisplayColon="true" AssociatedControlID="accountStatus" />
            </td>
            <td class="AccountControlLeft">
                <cms:AccountStatusSelector ID="accountStatus" runat="server" AllowAllItem="false"
                    DisplaySiteOrGlobal="true" IsLiveSite="false" />
            </td>
            <td class="AccountLabelRight">
                <cms:LocalizedLabel ID="lblAccountOwner" runat="server" EnableViewState="false" ResourceString="om.account.owner"
                    DisplayColon="true" AssociatedControlID="accountOwner" />
            </td>
            <td class="AccountControlRight">
                <cms:SelectUser ID="accountOwner" runat="server" IsLiveSite="false" HideHiddenUsers="true" HideDisabledUsers="true" HideNonApprovedUsers="true" />
            </td>
        </tr>
    </table>
</asp:Panel>
<asp:Panel ID="pnlAddress" runat="server" CssClass="ContentPanel">
    <table>
        <tr>
            <td class="AccountLabelLeft">
                <cms:LocalizedLabel ID="lblAddress1" runat="server" EnableViewState="false" ResourceString="commerceaddress.address1"
                    DisplayColon="true" AssociatedControlID="txtAddress1" />
            </td>
            <td class="AccountControlLeft">
                <cms:CMSTextBox ID="txtAddress1" runat="server" CssClass="TextBoxField" MaxLength="100" />
            </td>
            <td class="AccountLabelRight">
                <cms:LocalizedLabel ID="lblPhone" runat="server" EnableViewState="false" ResourceString="general.phone"
                    DisplayColon="true" AssociatedControlID="txtPhone" />
            </td>
            <td class="AccountControlRight">
                <cms:CMSTextBox ID="txtPhone" runat="server" CssClass="TextBoxField" MaxLength="26" />
            </td>
        </tr>
        <tr>
            <td class="AccountLabelLeft">
                <cms:LocalizedLabel ID="lblAddress2" runat="server" EnableViewState="false" ResourceString="commerceaddress.address2"
                    DisplayColon="true" AssociatedControlID="txtAddress2" />
            </td>
            <td class="AccountControlLeft">
                <cms:CMSTextBox ID="txtAddress2" runat="server" CssClass="TextBoxField" MaxLength="100" />
            </td>
            <td class="AccountLabelRight">
                <cms:LocalizedLabel ID="lblFax" runat="server" EnableViewState="false" ResourceString="general.fax"
                    DisplayColon="true" AssociatedControlID="txtFax" />
            </td>
            <td class="AccountControlRight">
                <cms:CMSTextBox ID="txtFax" runat="server" CssClass="TextBoxField" MaxLength="26" />
            </td>
        </tr>
        <tr>
            <td class="AccountLabelLeft">
                <cms:LocalizedLabel ID="lblCity" runat="server" EnableViewState="false" ResourceString="general.city"
                    DisplayColon="true" AssociatedControlID="txtCity" />
            </td>
            <td class="AccountControlLeft">
                <cms:CMSTextBox ID="txtCity" runat="server" CssClass="TextBoxField" MaxLength="100" />
            </td>
            <td class="AccountLabelRight">
                <cms:LocalizedLabel ID="lblEmail" runat="server" EnableViewState="false" ResourceString="general.email"
                    DisplayColon="true" AssociatedControlID="emailInput" />
            </td>
            <td class="AccountControlRight">
                <cms:EmailInput ID="emailInput" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="AccountLabelLeft">
                <cms:LocalizedLabel ID="lblZip" runat="server" EnableViewState="false" ResourceString="general.zip"
                    DisplayColon="true" AssociatedControlID="txtZip" />
            </td>
            <td class="AccountControlLeft">
                <cms:CMSTextBox ID="txtZip" runat="server" CssClass="TextBoxField" MaxLength="20" />
            </td>
            <td class="AccountLabelRight">
                <cms:LocalizedLabel ID="lblURL" runat="server" EnableViewState="false" ResourceString="om.account.url"
                    DisplayColon="true" AssociatedControlID="txtURL" />
            </td>
            <td class="AccountControlRight">
                <cms:CMSTextBox ID="txtURL" runat="server" CssClass="TextBoxField" MaxLength="200" />
            </td>
        </tr>
        <tr>
            <td class="AccountLabelLeft">
                <cms:LocalizedLabel ID="lblCountry" runat="server" EnableViewState="false" ResourceString="statelist.country"
                    DisplayColon="true" AssociatedControlID="countrySelector" />
            </td>
            <td class="AccountControlLeft">
                <cms:CountrySelector ID="countrySelector" runat="server" IsLiveSite="false" />
            </td>
            <td class="AccountLabelRight">
            </td>
            <td class="AccountControlRight">
            </td>
        </tr>
    </table>
</asp:Panel>
<asp:Panel ID="pnlContacts" runat="server" CssClass="ContentPanel">
    <table>
        <tr>
            <td class="AccountLabelLeft">
                <cms:LocalizedLabel ID="lblPrimaryContact" runat="server" EnableViewState="false"
                    ResourceString="om.contact.primary" DisplayColon="true" AssociatedControlID="primaryContact" />
            </td>
            <td class="AccountControlLeft">
                <cms:ContactSelector ID="primaryContact" runat="server" IsLiveSite="false" />
            </td>
            <td class="AccountLabelRight">
                <cms:LocalizedLabel ID="lblPrimaryRole" runat="server" EnableViewState="false" ResourceString="edituserroles.role"
                    DisplayColon="true" AssociatedControlID="contactRolePrimary" />
            </td>
            <td class="AccountControlRight">
                <cms:ContactRoleSelector ID="contactRolePrimary" runat="server" IsLiveSite="false" />
            </td>
        </tr>
        <tr>
            <td class="AccountLabelLeft">
                <cms:LocalizedLabel ID="lblSecondaryContact" runat="server" EnableViewState="false"
                    ResourceString="om.contact.secondary" DisplayColon="true" AssociatedControlID="secondaryContact" />
            </td>
            <td class="AccountControlLeft">
                <cms:ContactSelector ID="secondaryContact" runat="server" IsLiveSite="false" />
            </td>
            <td class="AccountLabelRight">
                <cms:LocalizedLabel ID="lblSecondaryRole" runat="server" EnableViewState="false"
                    ResourceString="edituserroles.role" DisplayColon="true" AssociatedControlID="contactRoleSecondary" />
            </td>
            <td class="AccountControlRight">
                <cms:ContactRoleSelector ID="contactRoleSecondary" runat="server" IsLiveSite="false" />
            </td>
        </tr>
    </table>
</asp:Panel>
<asp:Panel ID="pnlNotes" runat="server" CssClass="ContentPanel">
    <cms:CMSHtmlEditor ID="htmlNotes" runat="server" ToolbarSet="Basic" IsLiveSite="false" />
    <div class="MiddleButton">
        <cms:LocalizedButton ID="btnStamp" runat="server" ResourceString="om.account.stamp"
            CssClass="ContentButton" EnableViewState="false" /></div>
</asp:Panel>
