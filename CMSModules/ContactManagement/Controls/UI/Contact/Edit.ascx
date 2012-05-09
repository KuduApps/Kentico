<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_ContactManagement_Controls_UI_Contact_Edit"
    CodeFile="Edit.ascx.cs" %>
<%@ Register Src="~/CMSFormControls/Basic/TextBoxControl.ascx" TagName="TextBoxControl"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSFormControls/Basic/CalendarControl.ascx" TagName="CalendarControl"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSFormControls/Selectors/GenderSelector.ascx" TagName="GenderSelector"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/Membership/FormControls/Users/SelectUser.ascx" TagName="SelectUser"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/ContactManagement/FormControls/ContactStatusSelector.ascx"
    TagName="ContactStatusSelector" TagPrefix="cms" %>
<%@ Register Src="~/CMSFormControls/Basic/CheckBoxControl.ascx" TagName="CheckBoxControl"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSFormControls/Inputs/EmailInput.ascx" TagName="EmailInput"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSFormControls/CountrySelector.ascx" TagName="CountrySelector"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSFormControls/Basic/HtmlAreaControl.ascx" TagName="HtmlAreaControl"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/WebAnalytics/FormControls/SelectCampaign.ascx" TagName="CampaignSelector"
    TagPrefix="cms" %>
<cms:LocalizedLabel ID="lblMergedInto" runat="server" EnableViewState="false" DisplayColon="true" /><asp:Literal
    ID="ltlButton" runat="server" EnableViewState="false" />
<cms:UIForm runat="server" ID="EditForm" ObjectType="OM.Contact" DefaultFieldLayout="Inline"
    OnOnAfterDataLoad="EditForm_OnAfterDataLoad" IsLiveSite="false">
    <LayoutTemplate>
        <asp:Panel ID="pnlGeneral" runat="server" CssClass="ContentPanel">
            <table>
                <tr>
                    <cms:FormField runat="server" ID="fFirstName" Field="ContactFirstName">
                        <td class="ContactLabel">
                            <cms:LocalizedLabel ID="lblFirstName" runat="server" EnableViewState="false" ResourceString="om.contact.firstname"
                                DisplayColon="true" />
                        </td>
                        <td class="ContactControl">
                            <cms:TextBoxControl ID="txtFirstName" runat="server" MaxLength="100" />
                        </td>
                    </cms:FormField>
                    <cms:FormField runat="server" ID="fLastName" Field="ContactLastName">
                        <td class="ContactLabel">
                            <cms:LocalizedLabel ID="lblLastName" runat="server" EnableViewState="false" ResourceString="om.contact.lastname"
                                DisplayColon="true" />
                        </td>
                        <td class="ContactControlRight">
                            <cms:TextBoxControl ID="txtLastName" runat="server" MaxLength="100" />
                        </td>
                    </cms:FormField>
                </tr>
                <tr>
                    <cms:FormField runat="server" ID="fMiddleName" Field="ContactMiddleName">
                        <td class="ContactLabel">
                            <cms:LocalizedLabel ID="lblMiddleName" runat="server" EnableViewState="false" ResourceString="om.contact.middlename"
                                DisplayColon="true" />
                        </td>
                        <td class="ContactControl">
                            <cms:TextBoxControl ID="txtMiddleName" runat="server" MaxLength="100" />
                        </td>
                    </cms:FormField>
                    <cms:FormField runat="server" ID="fSalutation" Field="ContactSalutation">
                        <td class="ContactLabel">
                            <cms:LocalizedLabel ID="lblSalutation" runat="server" EnableViewState="false" ResourceString="om.contact.salutation"
                                DisplayColon="true" />
                        </td>
                        <td class="ContactControlRight">
                            <cms:TextBoxControl ID="txtSalutation" runat="server" MaxLength="50" />
                        </td>
                    </cms:FormField>
                </tr>
                <tr>
                    <cms:FormField runat="server" ID="fTitleBefore" Field="ContactTitleBefore">
                        <td class="ContactLabel">
                            <cms:LocalizedLabel ID="lblTitleBefore" runat="server" EnableViewState="false" ResourceString="om.contact.titlebefore"
                                DisplayColon="true" />
                        </td>
                        <td class="ContactControl">
                            <cms:TextBoxControl ID="txtTitleBefore" runat="server" MaxLength="50" />
                        </td>
                    </cms:FormField>
                    <cms:FormField runat="server" ID="fTitleAfter" Field="ContactTitleAfter">
                        <td class="ContactLabel">
                            <cms:LocalizedLabel ID="lblTitleAfter" runat="server" EnableViewState="false" ResourceString="om.contact.titleafter"
                                DisplayColon="true" />
                        </td>
                        <td class="ContactControlRight">
                            <cms:TextBoxControl ID="txtTitleAfter" runat="server" MaxLength="50" />
                        </td>
                    </cms:FormField>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="pnlPersonal" runat="server" CssClass="ContentPanel">
            <table>
                <tr>
                    <cms:FormField runat="server" ID="fBirthday" Field="ContactBirthday">
                        <td class="ContactLabel">
                            <cms:LocalizedLabel ID="lblBirthday" runat="server" EnableViewState="false" ResourceString="om.contact.birthday"
                                DisplayColon="true" />
                        </td>
                        <td class="ContactControl">
                            <cms:CalendarControl ID="calBirthday" runat="server" IsLiveSite="false" EditTime="false" />
                        </td>
                    </cms:FormField>
                    <cms:FormField runat="server" ID="fJobTitle" Field="ContactJobTitle">
                        <td class="ContactLabel">
                            <cms:LocalizedLabel ID="lblJobTitle" runat="server" EnableViewState="false" ResourceString="om.contact.jobtitle"
                                DisplayColon="true" />
                        </td>
                        <td class="ContactControlRight">
                            <cms:TextBoxControl ID="txtJobTitle" runat="server" MaxLength="50" />
                        </td>
                    </cms:FormField>
                </tr>
                <tr>
                    <cms:FormField runat="server" ID="fGender" Field="ContactGender">
                        <td class="ContactLabel">
                            <cms:LocalizedLabel ID="lblGender" runat="server" EnableViewState="false" ResourceString="om.contact.gender"
                                DisplayColon="true" />
                        </td>
                        <td class="ContactControl">
                            <cms:GenderSelector ID="genderSelector" runat="server" />
                        </td>
                    </cms:FormField>
                    <cms:FormField runat="server" ID="fCreated" Field="ContactCreated">
                        <td class="ContactLabel">
                            <cms:LocalizedLabel ID="lblCreated" runat="server" EnableViewState="false" ResourceString="general.created"
                                DisplayColon="true" />
                        </td>
                        <td class="ContactControl">
                            <asp:Label ID="cCreated" runat="server" />
                        </td>
                    </cms:FormField>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="pnlSettings" runat="server" CssClass="ContentPanel">
            <table>
                <tr>
                    <cms:FormField runat="server" ID="fContactStatus" Field="ContactStatusID">
                        <td class="ContactLabel">
                            <cms:LocalizedLabel ID="lblContactStatus" runat="server" EnableViewState="false"
                                ResourceString="om.contactstatus" DisplayColon="true" />
                        </td>
                        <td class="ContactControl">
                            <cms:ContactStatusSelector ID="contactStatusSelector" runat="server" AllowAllItem="false"
                                DisplaySiteOrGlobal="true" IsLiveSite="false" />
                        </td>
                    </cms:FormField>
                    <cms:FormField runat="server" ID="fOwner" Field="ContactOwnerUserID">
                        <td class="ContactLabel">
                            <cms:LocalizedLabel ID="lblOwner" runat="server" EnableViewState="false" ResourceString="om.contact.owner"
                                DisplayColon="true" />
                        </td>
                        <td class="ContactControlRight">
                            <cms:SelectUser ID="userSelector" runat="server" ShowSiteFilter="false" IsLiveSite="false" HideHiddenUsers="true" HideDisabledUsers="true" HideNonApprovedUsers="true"  />
                        </td>
                    </cms:FormField>
                </tr>
                <tr>
                    <cms:FormField runat="server" ID="fMonitored" Field="ContactMonitored">
                        <td class="ContactLabel">
                            <cms:LocalizedLabel ID="lblMonitored" runat="server" EnableViewState="false" ResourceString="om.contact.tracking"
                                DisplayColon="true" />
                        </td>
                        <td class="ContactControl">
                            <cms:CheckBoxControl ID="chkMonitored" runat="server" />
                        </td>
                    </cms:FormField>
                    <cms:FormField runat="server" ID="fCampaignSelection" Field="ContactCampaign">
                        <td class="ContactLabel">
                            <cms:LocalizedLabel ID="lblCampaign" runat="server" EnableViewState="false" ResourceString="analytics.campaign"
                                DisplayColon="true" />
                        </td>
                        <td class="ContactControlRight">
                            <cms:CampaignSelector ID="campaignSelector" runat="server" AllowEmpty="true" NoneRecordValue="" CreateOnUnknownName="false"/>
                        </td>
                    </cms:FormField>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="pnlAddress" runat="server" CssClass="ContentPanel">
            <table>
                <tr>
                    <cms:FormField runat="server" ID="fAddress1" Field="ContactAddress1">
                        <td class="ContactLabel">
                            <cms:LocalizedLabel ID="lblAddress1" runat="server" EnableViewState="false" ResourceString="om.contact.address1"
                                DisplayColon="true" />
                        </td>
                        <td class="ContactControl">
                            <cms:TextBoxControl ID="txtAddress1" runat="server" MaxLength="100" />
                        </td>
                    </cms:FormField>
                    <cms:FormField runat="server" ID="fMobilePhone" Field="ContactMobilePhone">
                        <td class="ContactLabel">
                            <cms:LocalizedLabel ID="lblMobilePhone" runat="server" EnableViewState="false" ResourceString="om.contact.mobilephone"
                                DisplayColon="true" />
                        </td>
                        <td class="ContactControlRight">
                            <cms:TextBoxControl ID="txtMobile" runat="server" MaxLength="26" />
                        </td>
                    </cms:FormField>
                </tr>
                <tr>
                    <cms:FormField runat="server" ID="fAddress2" Field="ContactAddress2">
                        <td class="ContactLabel">
                            <cms:LocalizedLabel ID="lblAddress2" runat="server" EnableViewState="false" ResourceString="om.contact.address2"
                                DisplayColon="true" />
                        </td>
                        <td class="ContactControl">
                            <cms:TextBoxControl ID="txtAddress2" runat="server" MaxLength="100" />
                        </td>
                    </cms:FormField>
                    <cms:FormField runat="server" ID="fHomePhone" Field="ContactHomePhone">
                        <td class="ContactLabel">
                            <cms:LocalizedLabel ID="lblHomePhone" runat="server" EnableViewState="false" ResourceString="om.contact.homephone"
                                DisplayColon="true" />
                        </td>
                        <td class="ContactControlRight">
                            <cms:TextBoxControl ID="txtHomePhone" runat="server" MaxLength="26" />
                        </td>
                    </cms:FormField>
                </tr>
                <tr>
                    <cms:FormField runat="server" ID="fCity" Field="ContactCity">
                        <td class="ContactLabel">
                            <cms:LocalizedLabel ID="lblCity" runat="server" EnableViewState="false" ResourceString="general.city"
                                DisplayColon="true" />
                        </td>
                        <td class="ContactControl">
                            <cms:TextBoxControl ID="txtCity" runat="server" MaxLength="100" />
                        </td>
                    </cms:FormField>
                    <cms:FormField runat="server" ID="fBusinessPhone" Field="ContactBusinessPhone">
                        <td class="ContactLabel">
                            <cms:LocalizedLabel ID="lblBusinessPhone" runat="server" EnableViewState="false"
                                ResourceString="om.contact.businessphone" DisplayColon="true" />
                        </td>
                        <td class="ContactControlRight">
                            <cms:TextBoxControl ID="txtBusinessPhone" runat="server" MaxLength="26" />
                        </td>
                    </cms:FormField>
                </tr>
                <tr>
                    <cms:FormField runat="server" ID="fZip" Field="ContactZIP">
                        <td class="ContactLabel">
                            <cms:LocalizedLabel ID="lblZip" runat="server" EnableViewState="false" ResourceString="general.zip"
                                DisplayColon="true" />
                        </td>
                        <td class="ContactControl">
                            <cms:TextBoxControl ID="txtZip" runat="server" MaxLength="20" />
                        </td>
                    </cms:FormField>
                    <cms:FormField runat="server" ID="fEmail" Field="ContactEmail">
                        <td class="ContactLabel">
                            <cms:LocalizedLabel ID="lblEmail" runat="server" EnableViewState="false" ResourceString="general.email"
                                DisplayColon="true" />
                        </td>
                        <td class="ContactControlRight">
                            <cms:EmailInput ID="emailInput" runat="server" />
                        </td>
                    </cms:FormField>
                </tr>
                <tr>
                    <td class="ContactLabel">
                        <cms:LocalizedLabel ID="lblCountry" runat="server" EnableViewState="false" ResourceString="general.country"
                            DisplayColon="true" />
                    </td>
                    <td class="ContactControl">
                        <cms:CountrySelector ID="countrySelector" runat="server" UseCodeNameForSelection="false"
                            IsLiveSite="false" />
                    </td>
                    <cms:FormField runat="server" ID="fURL" Field="ContactWebSite">
                        <td class="ContactLabelDoubleHeight">
                            <cms:LocalizedLabel ID="lblURL" runat="server" EnableViewState="false" ResourceString="om.contact.website"
                                DisplayColon="true" />
                        </td>
                        <td class="ContactControl">
                            <cms:TextBoxControl ID="txtURL" runat="server" MaxLength="100" />
                        </td>
                    </cms:FormField>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="pnlNotes" runat="server" CssClass="ContentPanel">
            <cms:FormField runat="server" ID="fNotes" Field="ContactNotes">
                <cms:HtmlAreaControl ID="htmlNotes" runat="server" ToolbarSet="Basic" IsLiveSite="false" />
            </cms:FormField>
            <div class="MiddleButton">
                <cms:LocalizedButton ID="btnStamp" runat="server" ResourceString="om.account.stamp"
                    CssClass="ContentButton" EnableViewState="false" /></div>
        </asp:Panel>
    </LayoutTemplate>
</cms:UIForm>
