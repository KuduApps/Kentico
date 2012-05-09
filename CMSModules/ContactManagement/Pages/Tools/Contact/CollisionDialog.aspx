<%@ Page Language="C#" AutoEventWireup="True" Theme="Default" MasterPageFile="~/CMSMasterPages/UI/Dialogs/ModalDialogPage.master"
    Inherits="CMSModules_ContactManagement_Pages_Tools_Contact_CollisionDialog" CodeFile="CollisionDialog.aspx.cs" %>

<%@ Register Src="~/CMSFormControls/CountrySelector.ascx" TagName="CountrySelector"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSFormControls/Basic/CalendarControl.ascx" TagName="CalendarControl"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSFormControls/Selectors/GenderSelector.ascx" TagName="GenderSelector"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/ContactManagement/FormControls/ContactStatusSelector.ascx"
    TagName="ContactStatusSelector" TagPrefix="cms" %>
<%@ Register Src="~/CMSFormControls/Basic/HtmlAreaControl.ascx" TagName="HtmlAreaControl"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSFormControls/Basic/DropDownListControl.ascx" TagName="DropDownList"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/WebAnalytics/FormControls/SelectCampaign.ascx" TagName="CampaignSelector"
    TagPrefix="cms" %>
<asp:Content ID="content" ContentPlaceHolderID="plcContent" runat="Server">
    <asp:Panel ID="pnlContent" CssClass="MergeDialog" runat="server">
        <cms:JQueryTabContainer ID="pnlTabs" runat="server" CssClass="Dialog_Tabs">
            <cms:JQueryTab ID="tabFields" runat="server">
                <ContentTemplate>
                    <div class="BodyPanel">
                        <div class="PaddingPanel">
                            <cms:LocalizedLabel ID="lblError" runat="server" EnableViewState="false" Visible="false"
                                CssClass="ErrorLabel" />
                            <asp:Panel ID="pnlGeneral" runat="server" CssClass="ContentPanel">
                                <table class="CollisionPanel">
                                    <tr class="CollisionRow">
                                        <td class="LabelColumn">
                                            <cms:LocalizedLabel ID="lblContactFirstName" runat="server" ResourceString="om.contact.firstname"
                                                DisplayColon="true" EnableViewState="false" CssClass="ContentLabel" AssociatedControlID="cmbContactFirstName" />
                                        </td>
                                        <td class="ComboBoxColumn">
                                            <div class="ComboBox">
                                                <cms:DropDownList ID="cmbContactFirstName" runat="server" EditText="true" IsLiveSite="false" />
                                            </div>
                                        </td>
                                        <td>
                                            <asp:Image ID="imgContactFirstName" runat="server" CssClass="ResolveButton" Visible="false" />
                                        </td>
                                    </tr>
                                    <tr class="CollisionRow">
                                        <td class="LabelColumn">
                                            <cms:LocalizedLabel ID="lblContactMiddleName" runat="server" ResourceString="om.contact.middlename"
                                                DisplayColon="true" EnableViewState="false" CssClass="ContentLabel" AssociatedControlID="cmbContactMiddleName" />
                                        </td>
                                        <td class="ComboBoxColumn">
                                            <div class="ComboBox">
                                                <cms:DropDownList ID="cmbContactMiddleName" runat="server" EditText="true" IsLiveSite="false" />
                                            </div>
                                        </td>
                                        <td>
                                            <asp:Image ID="imgContactMiddleName" runat="server" CssClass="ResolveButton" Visible="false" />
                                        </td>
                                    </tr>
                                    <tr class="CollisionRow">
                                        <td class="LabelColumn">
                                            <cms:LocalizedLabel ID="lblContactLastName" runat="server" ResourceString="om.contact.lastname"
                                                DisplayColon="true" EnableViewState="false" CssClass="ContentLabel" AssociatedControlID="cmbContactLastName" />
                                        </td>
                                        <td class="ComboBoxColumn">
                                            <div class="ComboBox">
                                                <cms:DropDownList ID="cmbContactLastName" runat="server" EditText="true" IsLiveSite="false" />
                                            </div>
                                        </td>
                                        <td>
                                            <asp:Image ID="imgContactLastName" runat="server" CssClass="ResolveButton" Visible="false" />
                                        </td>
                                    </tr>
                                    <tr class="CollisionRow">
                                        <td class="LabelColumn">
                                            <cms:LocalizedLabel ID="lblContactSalutation" runat="server" ResourceString="om.contact.salutation"
                                                DisplayColon="true" EnableViewState="false" CssClass="ContentLabel" AssociatedControlID="cmbContactSalutation" />
                                        </td>
                                        <td class="ComboBoxColumn">
                                            <div class="ComboBox">
                                                <cms:DropDownList ID="cmbContactSalutation" runat="server" CssClass="DropDownField"
                                                    EditText="true" IsLiveSite="false" />
                                            </div>
                                        </td>
                                        <td>
                                            <asp:Image ID="imgContactSalutation" runat="server" CssClass="ResolveButton" Visible="false" />
                                        </td>
                                    </tr>
                                    <tr class="CollisionRow">
                                        <td class="LabelColumn">
                                            <cms:LocalizedLabel ID="lblContactTitleBefore" runat="server" ResourceString="om.contact.titlebefore"
                                                DisplayColon="true" EnableViewState="false" CssClass="ContentLabel" AssociatedControlID="cmbContactTitleBefore" />
                                        </td>
                                        <td class="ComboBoxColumn">
                                            <div class="ComboBox">
                                                <cms:DropDownList ID="cmbContactTitleBefore" runat="server" CssClass="DropDownField"
                                                    EditText="true" IsLiveSite="false" />
                                            </div>
                                        </td>
                                        <td>
                                            <asp:Image ID="imgContactTitleBefore" runat="server" CssClass="ResolveButton" Visible="false" />
                                        </td>
                                    </tr>
                                    <tr class="CollisionRow">
                                        <td class="LabelColumn">
                                            <cms:LocalizedLabel ID="lblContactTitleAfter" runat="server" ResourceString="om.contact.titleafter"
                                                DisplayColon="true" EnableViewState="false" CssClass="ContentLabel" AssociatedControlID="cmbContactTitleAfter" />
                                        </td>
                                        <td class="ComboBoxColumn">
                                            <div class="ComboBox">
                                                <cms:DropDownList ID="cmbContactTitleAfter" runat="server" CssClass="DropDownField"
                                                    EditText="true" IsLiveSite="false" />
                                            </div>
                                        </td>
                                        <td>
                                            <asp:Image ID="imgContactTitleAfter" runat="server" CssClass="ResolveButton" Visible="false" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <asp:Panel ID="pnlPersonal" runat="server" CssClass="ContentPanel">
                                <table class="CollisionPanel">
                                    <tr class="CollisionRow">
                                        <td class="LabelColumn">
                                            <cms:LocalizedLabel ID="lblContactBirthday" runat="server" ResourceString="om.contact.birthday"
                                                DisplayColon="true" EnableViewState="false" CssClass="ContentLabel" AssociatedControlID="calendarControl" />
                                        </td>
                                        <td class="ComboBoxColumn">
                                            <cms:CalendarControl ID="calendarControl" runat="server" IsLiveSite="false" EditTime="false" />
                                        </td>
                                        <td>
                                            <asp:Image ID="imgContactBirthday" runat="server" CssClass="ResolveButton" Visible="false" />
                                        </td>
                                    </tr>
                                    <tr class="CollisionRow">
                                        <td class="LabelColumn">
                                            <cms:LocalizedLabel ID="lblContactGender" runat="server" ResourceString="om.contact.gender"
                                                DisplayColon="true" EnableViewState="false" CssClass="ContentLabel" AssociatedControlID="genderSelector" />
                                        </td>
                                        <td class="ComboBoxColumn">
                                            <cms:GenderSelector ID="genderSelector" runat="server" CssClass="DropDownField" IsLiveSite="false" />
                                        </td>
                                        <td>
                                            <asp:Image ID="imgContactGender" runat="server" CssClass="ResolveButton" Visible="false" />
                                        </td>
                                    </tr>
                                    <tr class="CollisionRow">
                                        <td class="LabelColumn">
                                            <cms:LocalizedLabel ID="lblContactJobTitle" runat="server" ResourceString="om.contact.jobtitle"
                                                DisplayColon="true" EnableViewState="false" CssClass="ContentLabel" AssociatedControlID="cmbContactJobTitle" />
                                        </td>
                                        <td class="ComboBoxColumn">
                                            <div class="ComboBox">
                                                <cms:DropDownList ID="cmbContactJobTitle" runat="server" CssClass="DropDownField"
                                                    EditText="true" IsLiveSite="false" />
                                            </div>
                                        </td>
                                        <td>
                                            <asp:Image ID="imgContactJobTitle" runat="server" CssClass="ResolveButton" Visible="false" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <asp:Panel ID="pnlSettings" runat="server" CssClass="ContentPanel">
                                <table class="CollisionPanel">
                                    <tr class="CollisionRow">
                                        <td class="LabelColumn">
                                            <cms:LocalizedLabel ID="lblContactStatusID" runat="server" ResourceString="om.contactstatus"
                                                DisplayColon="true" EnableViewState="false" CssClass="ContentLabel" AssociatedControlID="contactStatusSelector" />
                                        </td>
                                        <td class="ComboBoxColumn">
                                            <cms:ContactStatusSelector ID="contactStatusSelector" runat="server" IsLiveSite="false"
                                                AllowAllItem="false" />
                                        </td>
                                        <td>
                                            <asp:Image ID="imgContactStatus" runat="server" CssClass="ResolveButton" Visible="false" />
                                        </td>
                                    </tr>
                                    <tr class="CollisionRow">
                                        <td class="LabelColumn">
                                            <cms:LocalizedLabel ID="lblContactOwnerUserID" runat="server" ResourceString="om.contact.owner"
                                                DisplayColon="true" EnableViewState="false" CssClass="ContentLabel" />
                                        </td>
                                        <td class="ComboBoxColumn" colspan="2">
                                            <cms:LocalizedLabel ID="lblOwner" runat="server" CssClass="ContentLabel" />
                                        </td>
                                    </tr>
                                    <tr class="CollisionRow">
                                        <td class="LabelColumn">
                                            <cms:LocalizedLabel ID="lblContactMonitored" runat="server" ResourceString="om.contact.tracking"
                                                DisplayColon="true" EnableViewState="false" CssClass="ContentLabel" AssociatedControlID="chkContactMonitored" />
                                        </td>
                                        <td class="ComboBoxColumn">
                                            <cms:LocalizedCheckBox ID="chkContactMonitored" runat="server" IsLiveSite="false" />
                                        </td>
                                        <td>
                                            <asp:Image ID="imgContactMonitored" runat="server" CssClass="ResolveButton" Visible="false" />
                                        </td>
                                    </tr>
                                    <tr class="CollisionRow">
                                        <td class="LabelColumn">
                                            <cms:LocalizedLabel ID="lblContactCampaign" runat="server" ResourceString="analytics.campaign"
                                                DisplayColon="true" EnableViewState="false" CssClass="ContentLabel" AssociatedControlID="cCampaign" />
                                        </td>
                                        <td class="ComboBoxColumn">
                                            <cms:CampaignSelector ID="cCampaign" runat="server" IsLiveSite="false" AllowEmpty="true" NoneRecordValue="" />
                                        </td>
                                        <td>
                                            <asp:Image ID="imgContactCampaign" runat="server" CssClass="ResolveButton" Visible="false" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <asp:Panel ID="pnlAddress" runat="server" CssClass="ContentPanel">
                                <table class="CollisionPanel">
                                    <tr class="CollisionRow">
                                        <td class="LabelColumn">
                                            <cms:LocalizedLabel ID="lblContactAddress1" runat="server" ResourceString="om.contact.address1"
                                                DisplayColon="true" EnableViewState="false" CssClass="ContentLabel" AssociatedControlID="cmbContactAddress1" />
                                        </td>
                                        <td class="ComboBoxColumn">
                                            <div class="ComboBox">
                                                <cms:DropDownList ID="cmbContactAddress1" runat="server" CssClass="DropDownField"
                                                    EditText="true" IsLiveSite="false" />
                                            </div>
                                        </td>
                                        <td>
                                            <asp:Image ID="imgContactAddress1" runat="server" CssClass="ResolveButton" />
                                        </td>
                                    </tr>
                                    <tr class="CollisionRow">
                                        <td class="LabelColumn">
                                            <cms:LocalizedLabel ID="lblContactAddress2" runat="server" ResourceString="om.contact.address2"
                                                DisplayColon="true" EnableViewState="false" CssClass="ContentLabel" AssociatedControlID="cmbContactAddress2" />
                                        </td>
                                        <td class="ComboBoxColumn">
                                            <div class="ComboBox">
                                                <cms:DropDownList ID="cmbContactAddress2" runat="server" CssClass="DropDownField"
                                                    EditText="true" IsLiveSite="false" />
                                            </div>
                                        </td>
                                        <td>
                                            <asp:Image ID="imgContactAddress2" runat="server" CssClass="ResolveButton" />
                                        </td>
                                    </tr>
                                    <tr class="CollisionRow">
                                        <td class="LabelColumn">
                                            <cms:LocalizedLabel ID="lblContactCity" runat="server" ResourceString="general.city"
                                                DisplayColon="true" EnableViewState="false" CssClass="ContentLabel" AssociatedControlID="cmbContactCity" />
                                        </td>
                                        <td class="ComboBoxColumn">
                                            <div class="ComboBox">
                                                <cms:DropDownList ID="cmbContactCity" runat="server" CssClass="DropDownField" EditText="true"
                                                    IsLiveSite="false" />
                                            </div>
                                        </td>
                                        <td>
                                            <asp:Image ID="imgContactCity" runat="server" CssClass="ResolveButton" />
                                        </td>
                                    </tr>
                                    <tr class="CollisionRow">
                                        <td class="LabelColumn">
                                            <cms:LocalizedLabel ID="lblContactZIP" runat="server" ResourceString="general.zip"
                                                DisplayColon="true" EnableViewState="false" CssClass="ContentLabel" AssociatedControlID="cmbContactZIP" />
                                        </td>
                                        <td class="ComboBoxColumn">
                                            <div class="ComboBox">
                                                <cms:DropDownList ID="cmbContactZIP" runat="server" CssClass="DropDownField" EditText="true"
                                                    IsLiveSite="false" />
                                            </div>
                                        </td>
                                        <td>
                                            <asp:Image ID="imgContactZIP" runat="server" CssClass="ResolveButton" />
                                        </td>
                                    </tr>
                                    <tr class="CollisionRow">
                                        <td class="LabelColumn">
                                            <cms:LocalizedLabel ID="lblCountry" runat="server" ResourceString="general.country"
                                                DisplayColon="true" EnableViewState="false" CssClass="ContentLabel" AssociatedControlID="countrySelector" />
                                        </td>
                                        <td class="ComboBoxColumn" rowspan="2" style="vertical-align: top;">
                                            <cms:CountrySelector ID="countrySelector" runat="server" CssClass="DropDownField"
                                                IsLiveSite="false" />
                                        </td>
                                        <td>
                                            <asp:Image ID="imgContactCountry" runat="server" CssClass="ResolveButton" />
                                        </td>
                                    </tr>
                                    <tr class="CollisionRow">
                                        <td class="LabelColumn">
                                            <cms:LocalizedLabel ID="lblState" runat="server" ResourceString="general.state" DisplayColon="true"
                                                EnableViewState="false" CssClass="ContentLabel" />
                                        </td>
                                        <td>
                                            <asp:Image ID="imgContactState" runat="server" CssClass="ResolveButton" />
                                        </td>
                                    </tr>
                                    <tr class="CollisionRow">
                                        <td class="LabelColumn">
                                            <cms:LocalizedLabel ID="lblContactMobilePhone" runat="server" ResourceString="om.contact.mobilephone"
                                                DisplayColon="true" EnableViewState="false" CssClass="ContentLabel" AssociatedControlID="cmbContactMobilePhone" />
                                        </td>
                                        <td class="ComboBoxColumn">
                                            <div class="ComboBox">
                                                <cms:DropDownList ID="cmbContactMobilePhone" runat="server" CssClass="DropDownField"
                                                    EditText="true" IsLiveSite="false" />
                                            </div>
                                        </td>
                                        <td>
                                            <asp:Image ID="imgContactMobilePhone" runat="server" CssClass="ResolveButton" />
                                        </td>
                                    </tr>
                                    <tr class="CollisionRow">
                                        <td class="LabelColumn">
                                            <cms:LocalizedLabel ID="lblContactHomePhone" runat="server" ResourceString="om.contact.homephone"
                                                DisplayColon="true" EnableViewState="false" CssClass="ContentLabel" AssociatedControlID="cmbContactHomePhone" />
                                        </td>
                                        <td class="ComboBoxColumn">
                                            <div class="ComboBox">
                                                <cms:DropDownList ID="cmbContactHomePhone" runat="server" CssClass="DropDownField"
                                                    EditText="true" IsLiveSite="false" />
                                            </div>
                                        </td>
                                        <td>
                                            <asp:Image ID="imgContactHomePhone" runat="server" CssClass="ResolveButton" />
                                        </td>
                                    </tr>
                                    <tr class="CollisionRow">
                                        <td class="LabelColumn">
                                            <cms:LocalizedLabel ID="lblContactBusinessPhone" runat="server" ResourceString="om.contact.businessphone"
                                                DisplayColon="true" EnableViewState="false" CssClass="ContentLabel" AssociatedControlID="cmbContactBusinessPhone" />
                                        </td>
                                        <td class="ComboBoxColumn">
                                            <div class="ComboBox">
                                                <cms:DropDownList ID="cmbContactBusinessPhone" runat="server" CssClass="DropDownField"
                                                    EditText="true" IsLiveSite="false" />
                                            </div>
                                        </td>
                                        <td>
                                            <asp:Image ID="imgContactBusinessPhone" runat="server" CssClass="ResolveButton" />
                                        </td>
                                    </tr>
                                    <tr class="CollisionRow">
                                        <td class="LabelColumn">
                                            <cms:LocalizedLabel ID="lblContactEmail" runat="server" ResourceString="general.email"
                                                DisplayColon="true" EnableViewState="false" CssClass="ContentLabel" AssociatedControlID="cmbContactEmail" />
                                        </td>
                                        <td class="ComboBoxColumn">
                                            <div class="ComboBox">
                                                <cms:DropDownList ID="cmbContactEmail" runat="server" CssClass="DropDownField" EditText="true"
                                                    IsLiveSite="false" />
                                            </div>
                                        </td>
                                        <td>
                                            <asp:Image ID="imgContactEmail" runat="server" CssClass="ResolveButton" />
                                        </td>
                                    </tr>
                                    <tr class="CollisionRow">
                                        <td class="LabelColumn">
                                            <cms:LocalizedLabel ID="lblContactWebSite" runat="server" ResourceString="om.contact.website"
                                                DisplayColon="true" EnableViewState="false" CssClass="ContentLabel" AssociatedControlID="cmbContactWebSite" />
                                        </td>
                                        <td class="ComboBoxColumn">
                                            <div class="ComboBox">
                                                <cms:DropDownList ID="cmbContactWebSite" runat="server" CssClass="DropDownField"
                                                    EditText="true" IsLiveSite="false" />
                                            </div>
                                        </td>
                                        <td>
                                            <asp:Image ID="imgContactWebSite" runat="server" CssClass="ResolveButton" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <asp:Panel ID="pnlNotes" runat="server" CssClass="ContentPanel">
                                <table class="CollisionPanel">
                                    <tr class="CollisionRowLong">
                                        <td class="LabelColumn">
                                            <cms:LocalizedLabel ID="lblContactNotes" runat="server" ResourceString="om.contact.notes"
                                                DisplayColon="true" EnableViewState="false" CssClass="ContentLabel" AssociatedControlID="htmlNotes" />
                                        </td>
                                        <td style="width: 520px;" colspan="2">
                                            <cms:HtmlAreaControl ID="htmlNotes" runat="server" ToolbarSet="Basic" IsLiveSite="false" />
                                            <div class="MiddleButton">
                                                <cms:LocalizedButton ID="btnStamp" runat="server" ResourceString="om.account.stamp"
                                                    CssClass="ContentButton" EnableViewState="false" /></div>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </div>
                    </div>
                </ContentTemplate>
            </cms:JQueryTab>
            <cms:JQueryTab ID="tabCustomFields" runat="server">
                <ContentTemplate>
                    <div class="BodyPanel">
                        <div class="PaddingPanel">
                            <asp:PlaceHolder ID="plcCustomFields" runat="server"></asp:PlaceHolder>
                        </div>
                    </div>
                </ContentTemplate>
            </cms:JQueryTab>
            <cms:JQueryTab ID="tabContacts" runat="server">
                <ContentTemplate>
                    <div class="BodyPanel">
                        <div class="PaddingPanel">
                            <cms:LocalizedLabel ID="lblContactInfo" runat="server" EnableViewState="false" CssClass="BoldInfoLabel"
                                ResourceString="om.contact.accountroles" />
                            <asp:PlaceHolder ID="plcAccountContact" runat="server"></asp:PlaceHolder>
                        </div>
                    </div>
                </ContentTemplate>
            </cms:JQueryTab>
            <cms:JQueryTab ID="tabContactGroups" runat="server">
                <ContentTemplate>
                    <div class="BodyPanel">
                        <div class="PaddingPanel">
                            <cms:LocalizedLabel ID="lblContactGroupsInfo" runat="server" CssClass="BoldInfoLabel"
                                ResourceString="om.contactgroup.selectmerge" />
                            <asp:CheckBoxList ID="chkContactGroups" runat="server" />
                        </div>
                    </div>
                </ContentTemplate>
            </cms:JQueryTab>
        </cms:JQueryTabContainer>
    </asp:Panel>
</asp:Content>
<asp:Content ID="footer" ContentPlaceHolderID="plcFooter" runat="server">
    <div class="FloatRight">
        <cms:LocalizedButton ID="btnMerge" runat="server" CssClass="SubmitButton" ResourceString="om.contact.merge"
            EnableViewState="false" />
        <cms:LocalizedButton ID="btnCancel" runat="server" CssClass="SubmitButton" ResourceString="general.cancel"
            EnableViewState="false" OnClientClick="window.close(); return false;" />
    </div>
</asp:Content>
