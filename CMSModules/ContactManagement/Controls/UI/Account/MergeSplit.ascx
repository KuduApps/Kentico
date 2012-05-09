<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MergeSplit.ascx.cs" Inherits="CMSModules_ContactManagement_Controls_UI_Account_MergeSplit" %>
<%@ Register Src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" TagName="UniGrid" TagPrefix="cms" %>
<%@ Register Namespace="CMS.UIControls.UniGridConfig" TagPrefix="ug" Assembly="CMS.UIControls" %>
<%@ Register Src="~/CMSModules/ContactManagement/Controls/UI/Account/Filter.ascx"
    TagName="Filter" TagPrefix="cms" %>
<cms:CMSUpdatePanel ID="pnlUpdate" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <cms:LocalizedLabel ID="lblInfo" runat="server" ResourceString="om.account.splitting"
            EnableViewState="false" CssClass="InfoLabel" Visible="false" />
        <cms:LocalizedLabel ID="lblError" runat="server" ResourceString="om.account.selectaccountssplit"
            EnableViewState="false" CssClass="ErrorLabel" Visible="false" />
        <cms:Filter ID="filter" runat="server" ShortID="f" HideMergedFilter="true" IsLiveSite="false" />
        <cms:LocalizedLabel ID="lblTitle" runat="server" ResourceString="om.account.splitaccount"
            EnableViewState="false" CssClass="BoldInfoLabel" DisplayColon="true" />
        <cms:UniGrid runat="server" ID="gridElem" ObjectType="om.accountlist" OrderBy="AccountName"
            Columns="AccountID,AccountName,AccountEmail,AccountStatusID,PrimaryContactFullName,AccountSiteID"
            IsLiveSite="false" ShowObjectMenu="false">
            <GridActions>
                <ug:Action ExternalSourceName="edit" Name="edit" Caption="$om.account.viewdetail$"
                    Icon="contactdetail.png" ModuleName="CMS.OnlineMarketing" CommandArgument="AccountID" />
            </GridActions>            
            <GridColumns>
                <ug:Column Source="AccountName" Caption="$om.account.name$" Wrap="false">
                </ug:Column>
                <ug:Column Source="AccountEmail" Caption="$general.email$" Wrap="false">
                </ug:Column>
                <ug:Column Source="AccountStatusID" AllowSorting="false" ExternalSourceName="#accountstatusname" Caption="$om.accountstatus$" Wrap="false">
                </ug:Column>
                <ug:Column Source="PrimaryContactFullName" Caption="$om.contact.primary$" Wrap="false">
                </ug:Column>
                <ug:Column Source="AccountSiteID" AllowSorting="false" ExternalSourceName="#sitenameorglobal" Name="sitename"
                    Caption="$general.sitename$" Wrap="false" Localize="true">
                </ug:Column>
                <ug:Column Width="100%" />
            </GridColumns>
            <GridOptions DisplayFilter="false" ShowSelection="true" />
        </cms:UniGrid>
        <asp:Panel ID="pnlFooter" runat="server" class="PropertiesPanel">
            <br />
            <asp:Panel ID="pnlSettings" runat="server">
            <cms:LocalizedCheckBox ID="chkCopyMissingFields" runat="server" ResourceString="om.contact.fillfieldsinaccounts" /><br />
            <cms:LocalizedCheckBox ID="chkRemoveContacts" runat="server" ResourceString="om.account.removecontacts" /><br />
            <cms:LocalizedCheckBox ID="chkRemoveContactGroups" runat="server" ResourceString="om.account.removeacontactgroups" /><br />
            </asp:Panel>
            <asp:Panel ID="pnlButton" runat="server" class="PanelButton">
                <cms:LocalizedButton ID="btnSplit" runat="server" CssClass="LongSubmitButton" ResourceString="om.contact.splitselected" />
            </asp:Panel>
        </asp:Panel>
        <asp:HiddenField ID="hdnValue" runat="server" EnableViewState="false" />
    </ContentTemplate>
</cms:CMSUpdatePanel>
