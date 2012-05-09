<%@ Control Language="C#" AutoEventWireup="true" CodeFile="List.ascx.cs" Inherits="CMSModules_ContactManagement_Controls_UI_Account_List" %>
<%@ Register Src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" TagName="UniGrid" TagPrefix="cms" %>
<%@ Register Namespace="CMS.UIControls.UniGridConfig" TagPrefix="ug" Assembly="CMS.UIControls" %>
<%@ Register Src="~/CMSModules/ContactManagement/Controls/UI/Account/Filter.ascx"
    TagName="Filter" TagPrefix="cms" %>
<cms:CMSUpdatePanel ID="pnlUpdate" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <cms:Filter ID="filter" runat="server" ShortID="f" IsLiveSite="false" NotMerged="true" />
        <cms:UniGrid runat="server" ID="gridElem" OrderBy="AccountName" ObjectType="om.accountlist"
            Columns="AccountID,AccountName,PrimaryContactFullName,AccountStatusID,AccountCountryID,AccountMergedWithAccountID,AccountPrimaryContactID,AccountGlobalAccountID,AccountSiteID"
            IsLiveSite="false">
            <GridActions Parameters="AccountID">
                <ug:Action Name="edit" Caption="$General.Edit$" Icon="Edit.png" />
                <ug:Action ExternalSourceName="delete" Name="delete" Caption="$General.Delete$" Icon="Delete.png" />
            </GridActions>
            <GridColumns>
                <ug:Column Source="AccountName" Caption="$om.account.name$" Wrap="false">
                </ug:Column>
                <ug:Column Source="AccountStatusID" AllowSorting="false" ExternalSourceName="#accountstatusname" Caption="$om.accountstatus.name$" Wrap="false">
                </ug:Column>
                <ug:Column Source="##ALL##" Caption="$om.contact.primary$" Sort="PrimaryContactFullName" Wrap="false" ExternalSourceName="primarycontactname">
                </ug:Column>
                <ug:Column Source="AccountCountryID" AllowSorting="false" ExternalSourceName="#countryname" Caption="$objecttype.cms_country$" Wrap="false">
                </ug:Column>
                <ug:Column Source="AccountSiteID" AllowSorting="false" Caption="$general.site$" Wrap="false" ExternalSourceName="#sitenameorglobal"
                    Name="sitename" Localize="true">
                </ug:Column>
                <ug:Column Source="##ALL##" Caption="$om.contact.mergedinto$" Wrap="false" ExternalSourceName="accountmergedwithaccountid"
                    Name="merged">
                </ug:Column>
                <ug:Column Width="100%" />
            </GridColumns>
            <GridOptions DisplayFilter="false" ShowSelection="true" />
        </cms:UniGrid>
        <asp:Panel ID="pnlFooter" runat="server" CssClass="MassAction">
            <asp:DropDownList ID="drpWhat" runat="server" CssClass="DropDownFieldSmall" />
            <asp:DropDownList ID="drpAction" runat="server" CssClass="DropDownFieldSmall" />
            <cms:LocalizedButton ID="btnOk" runat="server" ResourceString="general.ok" CssClass="SubmitButton SelectorButton"
                EnableViewState="false" OnClick="btnOk_Click" />
            <br />
            <br />
        </asp:Panel>
        <asp:Label ID="lblInfo" runat="server" CssClass="InfoLabel" EnableViewState="false" />
        <asp:HiddenField ID="hdnIdentificator" runat="server" EnableViewState="false" />
    </ContentTemplate>
</cms:CMSUpdatePanel>
