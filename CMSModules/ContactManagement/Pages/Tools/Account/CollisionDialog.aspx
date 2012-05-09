<%@ Page Language="C#" AutoEventWireup="True" Theme="Default" MasterPageFile="~/CMSMasterPages/UI/Dialogs/ModalDialogPage.master"
    Inherits="CMSModules_ContactManagement_Pages_Tools_Account_CollisionDialog" CodeFile="CollisionDialog.aspx.cs" %>

<%@ Register Src="~/CMSFormControls/CountrySelector.ascx" TagName="CountrySelector"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSFormControls/Basic/DropDownListControl.ascx" TagName="DropDownList"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/ContactManagement/FormControls/ContactSelector.ascx"
    TagName="ContactSelector" TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/ContactManagement/FormControls/AccountStatusSelector.ascx"
    TagName="AccountStatusSelector" TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/ContactManagement/FormControls/AccountSelector.ascx"
    TagName="AccountSelector" TagPrefix="cms" %>
<%@ Register Src="~/CMSFormControls/Basic/HtmlAreaControl.ascx" TagName="HtmlAreaControl"
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
                                            <cms:LocalizedLabel ID="lblName" runat="server" EnableViewState="false" ResourceString="om.account.name"
                                                DisplayColon="true" AssociatedControlID="cmbAccountName" />
                                        </td>
                                        <td class="ComboBoxColumn">
                                            <div class="ComboBox">
                                                <cms:DropDownList ID="cmbAccountName" runat="server" EditText="true" IsLiveSite="false" />
                                            </div>
                                        </td>
                                        <td>
                                            <asp:Image ID="imgAccountName" runat="server" CssClass="ResolveButton" Visible="false" />
                                        </td>
                                    </tr>
                                    <tr class="CollisionRow">
                                        <td class="LabelColumn">
                                            <cms:LocalizedLabel ID="lblAccountStatus" runat="server" ResourceString="om.accountstatus"
                                                DisplayColon="true" EnableViewState="false" CssClass="ContentLabel" AssociatedControlID="accountStatusSelector" />
                                        </td>
                                        <td class="ComboBoxColumn">
                                            <div class="ComboBox">
                                                <cms:AccountStatusSelector ID="accountStatusSelector" runat="server" AllowAllItem="false" DisplaySiteOrGlobal="true"
                                                    IsLiveSite="false" />
                                            </div>
                                        </td>
                                        <td>
                                            <asp:Image ID="imgAccountStatus" runat="server" CssClass="ResolveButton" />
                                        </td>
                                    </tr>
                                    <tr class="CollisionRow">
                                        <td class="LabelColumn">
                                            <cms:LocalizedLabel ID="lblAccountOwner" runat="server" ResourceString="om.account.owner"
                                                DisplayColon="true" EnableViewState="false" CssClass="ContentLabel" />
                                        </td>
                                        <td class="ComboBoxColumn" colspan="2">
                                            <cms:LocalizedLabel ID="lblOwner" runat="server" CssClass="ContentLabel" />
                                        </td>
                                    </tr>
                                    <tr class="CollisionRow">
                                        <td class="LabelColumn">
                                            <cms:LocalizedLabel ID="lblAccountHeadquarters" runat="server" ResourceString="om.account.subsidiaryof"
                                                DisplayColon="true" EnableViewState="false" CssClass="ContentLabel" AssociatedControlID="accountSelector" />
                                        </td>
                                        <td class="ComboBoxColumn">
                                            <cms:AccountSelector ID="accountSelector" runat="server" IsLiveSite="false" />
                                        </td>
                                        <td>
                                            <asp:Image ID="imgAccountHeadquarters" runat="server" CssClass="ResolveButton" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <asp:Panel ID="pnlAddress" runat="server" CssClass="ContentPanel">
                                <table class="CollisionPanel">
                                    <tr class="CollisionRow">
                                        <td class="LabelColumn">
                                            <cms:LocalizedLabel ID="lblAccountAddress1" runat="server" ResourceString="om.contact.address1"
                                                DisplayColon="true" EnableViewState="false" CssClass="ContentLabel" AssociatedControlID="cmbAccountAddress1" />
                                        </td>
                                        <td class="ComboBoxColumn">
                                            <div class="ComboBox">
                                                <cms:DropDownList ID="cmbAccountAddress1" runat="server" EditText="true" IsLiveSite="false" />
                                            </div>
                                        </td>
                                        <td>
                                            <asp:Image ID="imgAccountAddress1" runat="server" CssClass="ResolveButton" Visible="false" />
                                        </td>
                                    </tr>
                                    <tr class="CollisionRow">
                                        <td class="LabelColumn">
                                            <cms:LocalizedLabel ID="lblAccountAddress2" runat="server" ResourceString="om.contact.address2"
                                                DisplayColon="true" EnableViewState="false" CssClass="ContentLabel" AssociatedControlID="cmbAccountAddress2" />
                                        </td>
                                        <td class="ComboBoxColumn">
                                            <div class="ComboBox">
                                                <cms:DropDownList ID="cmbAccountAddress2" runat="server" EditText="true" IsLiveSite="false" />
                                            </div>
                                        </td>
                                        <td>
                                            <asp:Image ID="imgAccountAddress2" runat="server" CssClass="ResolveButton" Visible="false" />
                                        </td>
                                    </tr>
                                    <tr class="CollisionRow">
                                        <td class="LabelColumn">
                                            <cms:LocalizedLabel ID="lblAccountCity" runat="server" ResourceString="general.city"
                                                DisplayColon="true" EnableViewState="false" CssClass="ContentLabel" AssociatedControlID="cmbAccountCity" />
                                        </td>
                                        <td class="ComboBoxColumn">
                                            <div class="ComboBox">
                                                <cms:DropDownList ID="cmbAccountCity" runat="server" CssClass="DropDownField" EditText="true"
                                                    IsLiveSite="false" />
                                            </div>
                                        </td>
                                        <td>
                                            <asp:Image ID="imgAccountCity" runat="server" CssClass="ResolveButton" Visible="false" />
                                        </td>
                                    </tr>
                                    <tr class="CollisionRow">
                                        <td class="LabelColumn">
                                            <cms:LocalizedLabel ID="lblAccountZIP" runat="server" ResourceString="general.zip"
                                                DisplayColon="true" EnableViewState="false" CssClass="ContentLabel" AssociatedControlID="cmbAccountZIP" />
                                        </td>
                                        <td class="ComboBoxColumn">
                                            <div class="ComboBox">
                                                <cms:DropDownList ID="cmbAccountZIP" runat="server" CssClass="DropDownField" EditText="true"
                                                    IsLiveSite="false" />
                                            </div>
                                        </td>
                                        <td>
                                            <asp:Image ID="imgAccountZIP" runat="server" CssClass="ResolveButton" Visible="false" />
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
                                            <asp:Image ID="imgAccountCountry" runat="server" CssClass="ResolveButton" />
                                        </td>
                                    </tr>
                                    <tr class="CollisionRow">
                                        <td class="LabelColumn">
                                            <cms:LocalizedLabel ID="lblState" runat="server" ResourceString="general.state" DisplayColon="true"
                                                EnableViewState="false" CssClass="ContentLabel" />
                                        </td>
                                        <td>
                                            <asp:Image ID="imgAccountState" runat="server" CssClass="ResolveButton" />
                                        </td>
                                    </tr>
                                    <tr class="CollisionRow">
                                        <td class="LabelColumn">
                                            <cms:LocalizedLabel ID="lblAccountPhone" runat="server" ResourceString="general.phone"
                                                DisplayColon="true" EnableViewState="false" CssClass="ContentLabel" AssociatedControlID="cmbAccountPhone" />
                                        </td>
                                        <td class="ComboBoxColumn">
                                            <div class="ComboBox">
                                                <cms:DropDownList ID="cmbAccountPhone" runat="server" CssClass="DropDownField" EditText="true"
                                                    IsLiveSite="false" />
                                            </div>
                                        </td>
                                        <td>
                                            <asp:Image ID="imgAccountPhone" runat="server" CssClass="ResolveButton" Visible="false" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="LabelColumn">
                                            <cms:LocalizedLabel ID="lblAccountFax" runat="server" ResourceString="general.fax"
                                                DisplayColon="true" EnableViewState="false" CssClass="ContentLabel" AssociatedControlID="cmbAccountFax" />
                                        </td>
                                        <td class="ComboBoxColumn">
                                            <div class="ComboBox">
                                                <cms:DropDownList ID="cmbAccountFax" runat="server" CssClass="DropDownField" EditText="true"
                                                    IsLiveSite="false" />
                                            </div>
                                        </td>
                                        <td>
                                            <asp:Image ID="imgAccountFax" runat="server" CssClass="ResolveButton" />
                                        </td>
                                    </tr>
                                    <tr class="CollisionRow">
                                        <td class="LabelColumn">
                                            <cms:LocalizedLabel ID="lblAccountEmail" runat="server" ResourceString="general.email"
                                                DisplayColon="true" EnableViewState="false" CssClass="ContentLabel" AssociatedControlID="cmbAccountEmail" />
                                        </td>
                                        <td class="ComboBoxColumn">
                                            <div class="ComboBox">
                                                <cms:DropDownList ID="cmbAccountEmail" runat="server" CssClass="DropDownField" EditText="true"
                                                    IsLiveSite="false" />
                                            </div>
                                        </td>
                                        <td>
                                            <asp:Image ID="imgAccountEmail" runat="server" CssClass="ResolveButton" Visible="false" />
                                        </td>
                                    </tr>
                                    <tr class="CollisionRow">
                                        <td class="LabelColumn">
                                            <cms:LocalizedLabel ID="lblAccountWebSite" runat="server" ResourceString="om.account.url"
                                                DisplayColon="true" EnableViewState="false" CssClass="ContentLabel" AssociatedControlID="cmbAccountWebSite" />
                                        </td>
                                        <td class="ComboBoxColumn">
                                            <div class="ComboBox">
                                                <cms:DropDownList ID="cmbAccountWebSite" runat="server" CssClass="DropDownField"
                                                    EditText="true" IsLiveSite="false" />
                                            </div>
                                        </td>
                                        <td>
                                            <asp:Image ID="imgAccountWebSite" runat="server" CssClass="ResolveButton" Visible="false" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <asp:Panel ID="pnlNotes" runat="server" CssClass="ContentPanel">
                                <table class="CollisionPanel">
                                    <tr class="CollisionRowLong">
                                        <td class="LabelColumn">
                                            <cms:LocalizedLabel ID="lblAccountNotes" runat="server" ResourceString="om.contact.notes"
                                                DisplayColon="true" EnableViewState="false" CssClass="ContentLabel" AssociatedControlID="htmlNotes" />
                                        </td>
                                        <td style="width: 580px;" colspan="2">
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
                                ResourceString="om.account.contactroles" />
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
