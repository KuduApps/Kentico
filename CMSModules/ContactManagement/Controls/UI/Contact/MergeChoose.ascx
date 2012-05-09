<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MergeChoose.ascx.cs" Inherits="CMSModules_ContactManagement_Controls_UI_Contact_MergeChoose" %>
<%@ Register Src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" TagName="UniGrid" TagPrefix="cms" %>
<%@ Register Namespace="CMS.UIControls.UniGridConfig" TagPrefix="ug" Assembly="CMS.UIControls" %>
<%@ Register Src="~/CMSModules/ContactManagement/Controls/UI/Contact/Filter.ascx"
    TagName="Filter" TagPrefix="cms" %>
<cms:LocalizedLabel ID="lblInfo" runat="server" ResourceString="om.contact.merging"
    EnableViewState="false" CssClass="InfoLabel" Visible="false" />
<cms:LocalizedLabel ID="lblError" runat="server" ResourceString="om.contact.selectcontacts"
    EnableViewState="false" CssClass="ErrorLabel" Visible="false" />
<cms:Filter ID="filter" runat="server" ShortID="f" HideMergedFilter="true" NotMerged="true"
    IsLiveSite="false" />
<cms:LocalizedLabel ID="lblTitle" runat="server" ResourceString="om.contact.choosemerge"
    EnableViewState="false" CssClass="BoldInfoLabel" DisplayColon="true" />
<cms:UniGrid runat="server" ID="gridElem" ObjectType="om.contactlist" OrderBy="ContactLastName"
    Columns="ContactID,ContactLastName,ContactFirstName,ContactEmail,ContactStatusID,ContactCountryID,ContactSiteID"
    IsLiveSite="false" ShowObjectMenu="false">
    <GridColumns>
        <ug:Column Source="ContactFirstName" Caption="$om.contact.firstname$" Wrap="false">
        </ug:Column>
        <ug:Column Source="ContactLastName" Caption="$om.contact.lastname$" Wrap="false">
        </ug:Column>
        <ug:Column Source="ContactEmail" Caption="$general.email$" Wrap="false">
        </ug:Column>
        <ug:Column Source="ContactStatusID" AllowSorting="false" ExternalSourceName="#contactstatusname" Caption="$om.contactstatus$" Wrap="false">
        </ug:Column>
        <ug:Column Source="ContactCountryID" AllowSorting="false" ExternalSourceName="#countryname" Caption="$general.country$" Wrap="false">
        </ug:Column>
        <ug:Column Source="ContactSiteID" AllowSorting="false" Name="sitename" ExternalSourceName="#sitenameorglobal"
            Caption="$general.sitename$" Wrap="false" Localize="true">
        </ug:Column>
        <ug:Column Width="100%" />
    </GridColumns>
    <GridOptions DisplayFilter="false" ShowSelection="true" />
</cms:UniGrid>
<asp:Panel ID="pnlButton" runat="server" class="PanelButton">
    <cms:LocalizedButton ID="btnMergeSelected" runat="server" CssClass="LongSubmitButton"
        ResourceString="om.contact.mergeselected" />
    <cms:LocalizedButton ID="btnMergeAll" runat="server" CssClass="LongSubmitButton"
        ResourceString="om.contact.mergeall" />
</asp:Panel>
<asp:HiddenField ID="hdnIdentificator" runat="server" EnableViewState="false" />
