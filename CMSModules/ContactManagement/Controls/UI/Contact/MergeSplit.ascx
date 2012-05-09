<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MergeSplit.ascx.cs" Inherits="CMSModules_ContactManagement_Controls_UI_Contact_MergeSplit" %>
<%@ Register Src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" TagName="UniGrid" TagPrefix="cms" %>
<%@ Register Namespace="CMS.UIControls.UniGridConfig" TagPrefix="ug" Assembly="CMS.UIControls" %>
<%@ Register Src="~/CMSModules/ContactManagement/Controls/UI/Contact/Filter.ascx"
    TagName="Filter" TagPrefix="cms" %>
<cms:CMSUpdatePanel ID="pnlUpdate" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <cms:LocalizedLabel ID="lblInfo" runat="server" ResourceString="om.contact.splitting"
            EnableViewState="false" CssClass="InfoLabel" Visible="false" />
        <cms:LocalizedLabel ID="lblError" runat="server" ResourceString="om.contact.selectcontactssplit"
            EnableViewState="false" CssClass="ErrorLabel" Visible="false" />
        <cms:Filter ID="filter" runat="server" ShortID="f" HideMergedFilter="true" IsLiveSite="false" />
        <cms:LocalizedLabel ID="lblTitle" runat="server" ResourceString="om.contact.splitcontacts"
            EnableViewState="false" CssClass="BoldInfoLabel" />
        <cms:UniGrid runat="server" ID="gridElem" ObjectType="om.contactlist" OrderBy="ContactLastName"
            Columns="ContactID,ContactLastName,ContactFirstName,ContactEmail,ContactStatusID,ContactCountryID,ContactMergedWhen,ContactSiteID"
            IsLiveSite="false" ShowObjectMenu="false">
            <GridActions>
                <ug:Action ExternalSourceName="edit" Name="edit" Caption="$om.contact.viewdetail$"
                    Icon="contactdetail.png" ModuleName="CMS.OnlineMarketing" CommandArgument="ContactID" />
            </GridActions>
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
                <ug:Column Source="ContactMergedWhen" Name="mergedwhen" Caption="$om.contact.mergedwhen$"
                    Wrap="false">
                </ug:Column>
                <ug:Column Source="ContactSiteID" AllowSorting="false" Name="sitename" ExternalSourceName="#sitenameorglobal"
                    Caption="$general.sitename$" Wrap="false" Localize="true">
                </ug:Column>
                <ug:Column Width="100%" />
            </GridColumns>
            <GridOptions DisplayFilter="true" ShowSelection="true" />
        </cms:UniGrid>
        <asp:Panel ID="pnlFooter" runat="server" class="PropertiesPanel">
            <br />
            <asp:Panel ID="pnlSettings" runat="server">
                <cms:LocalizedCheckBox ID="chkCopyActivities" runat="server" ResourceString="om.contact.copyactivities" /><br />
                <cms:LocalizedCheckBox ID="chkCopyMissingFields" runat="server" ResourceString="om.contact.fillfields" /><br />
                <cms:LocalizedCheckBox ID="chkRemoveAccounts" runat="server" ResourceString="om.contact.removeaccounts" /><br />
                <cms:LocalizedCheckBox ID="chkRemoveContactGroups" runat="server" ResourceString="om.contact.removeacontactgroups" /><br />
            </asp:Panel>
            <asp:Panel ID="pnlButton" runat="server" class="PanelButton">
                <cms:LocalizedButton ID="btnSplit" runat="server" CssClass="LongSubmitButton" ResourceString="om.contact.splitselected" />
            </asp:Panel>
        </asp:Panel>
        <asp:HiddenField ID="hdnValue" runat="server" EnableViewState="false" />
    </ContentTemplate>
</cms:CMSUpdatePanel>
