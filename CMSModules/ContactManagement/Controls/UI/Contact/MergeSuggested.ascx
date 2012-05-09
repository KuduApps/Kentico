<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MergeSuggested.ascx.cs"
    Inherits="CMSModules_ContactManagement_Controls_UI_Contact_MergeSuggested" %>
<%@ Register Src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" TagName="UniGrid" TagPrefix="cms" %>
<%@ Register Namespace="CMS.UIControls.UniGridConfig" TagPrefix="ug" Assembly="CMS.UIControls" %>
<%@ Register Src="~/CMSModules/ContactManagement/Controls/UI/Contact/FilterSuggest.ascx"
    TagName="Filter" TagPrefix="cms" %>
<cms:LocalizedLabel ID="lblInfo" runat="server" ResourceString="om.contact.merging"
    EnableViewState="false" CssClass="InfoLabel" Visible="false" />
<cms:LocalizedLabel ID="lblError" runat="server" ResourceString="om.contact.selectcontacts"
    EnableViewState="false" CssClass="ErrorLabel" Visible="false" />
<cms:Filter ID="filter" runat="server" ShortID="f" IsLiveSite="false" />
<cms:LocalizedLabel ID="lblTitle" runat="server" ResourceString="om.contact.choosemerge"
    EnableViewState="false" CssClass="BoldInfoLabel" DisplayColon="true" />
<cms:UniGrid runat="server" ID="gridElem" ObjectType="om.contactlist" OrderBy="ContactLastName"
    Columns="ContactID,ContactLastName,ContactFirstName,ContactEmail,ContactMobilePhone,ContactHomePhone,ContactBusinessPhone,ContactBirthday,ContactAddress1,ContactAddress2,ContactCity,ContactZIP,ContactSiteID"
    IsLiveSite="false" ShowObjectMenu="false">
    <GridColumns>
        <ug:Column Source="ContactLastName" Caption="$om.contact.lastname$" Wrap="false">
        </ug:Column>
        <ug:Column Source="ContactFirstName" Caption="$om.contact.firstname$" Wrap="false">
        </ug:Column>
        <ug:Column Source="ContactEmail" Name="email" Caption="$general.email$" Wrap="false">
        </ug:Column>
        <ug:Column Source="ContactMobilePhone" Name="mobilephone" Caption="$om.contact.mobilephone$"
            Wrap="false">
        </ug:Column>
        <ug:Column Source="ContactHomePhone" Name="homephone" Caption="$om.contact.homephone$"
            Wrap="false">
        </ug:Column>
        <ug:Column Source="ContactBusinessPhone" Name="businessphone" Caption="$om.contact.businessphone$"
            Wrap="false">
        </ug:Column>
        <ug:Column Source="ContactBirthday" Name="birthday" Caption="$om.contact.birthday$" ExternalSourceName="birthday"
            Wrap="false">
        </ug:Column>
        <ug:Column Source="ContactAddress1" Name="address1" Caption="$om.contact.address1$"
            Wrap="false">
        </ug:Column>
        <ug:Column Source="ContactAddress2" Name="address2" Caption="$om.contact.address2$"
            Wrap="false">
        </ug:Column>
        <ug:Column Source="ContactCity" Name="city" Caption="$general.city$" Wrap="false">
        </ug:Column>
        <ug:Column Source="ContactZIP" Name="zip" Caption="$general.zip$" Wrap="false">
        </ug:Column>
        <ug:Column Source="ContactSiteID" AllowSorting="false" Name="sitename" ExternalSourceName="#sitenameorglobal"
            Caption="$general.sitename$" Wrap="false" Localize="true">
        </ug:Column>
        <ug:Column Width="100%" />
    </GridColumns>
    <GridOptions DisplayFilter="false" ShowSelection="true" />
</cms:UniGrid>
<asp:Panel ID="pnlFooter" runat="server" class="PanelButton">
    <cms:LocalizedButton ID="btnMergeSelected" runat="server" CssClass="LongSubmitButton"
        ResourceString="om.contact.mergeselected" />
    <cms:LocalizedButton ID="btnMergeAll" runat="server" CssClass="LongSubmitButton"
        ResourceString="om.contact.mergeall" />
</asp:Panel>
<asp:HiddenField ID="hdnIdentificator" runat="server" EnableViewState="false" />
