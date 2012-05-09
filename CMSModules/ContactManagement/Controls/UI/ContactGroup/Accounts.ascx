<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Accounts.ascx.cs" Inherits="CMSModules_ContactManagement_Controls_UI_ContactGroup_Accounts" %>
<%@ Register Src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" TagName="UniGrid" TagPrefix="cms" %>
<%@ Register Namespace="CMS.UIControls.UniGridConfig" TagPrefix="ug" Assembly="CMS.UIControls" %>
<%@ Register Src="~/CMSModules/ContactManagement/FormControls/AccountSelector.ascx"
    TagName="AccountSelector" TagPrefix="cms" %>
<asp:Panel ID="pnlSelector" runat="server" CssClass="PageHeaderLine">
    <cms:AccountSelector ID="accountSelector" runat="server" IsLiveSite="false" />
    <div class="ClearBoth">
        &nbsp;</div>
</asp:Panel>
<asp:Panel ID="pnlContent" runat="server" CssClass="PageContent">
    <cms:CMSUpdatePanel ID="pnlUpdate" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <cms:UniGrid runat="server" ID="gridElem" OrderBy="AccountName" ObjectType="om.contactgroupaccountlist" ShowObjectMenu="false"
                IsLiveSite="false" Columns="AccountID,AccountName,AccountStatusID,AccountCountryID,AccountSiteID">
                <GridActions Parameters="AccountID">
                    <ug:Action Name="edit" Caption="$om.account.viewdetail$" Icon="accountdetail.png" OnClick="EditAccount({0});return false;"
                        ModuleName="CMS.OnlineMarketing" />
                    <ug:Action Name="remove" Caption="$General.Remove$" Icon="Delete.png" Confirmation="$General.ConfirmRemove$"
                        ExternalSourceName="remove" />
                </GridActions>
                <GridColumns>
                    <ug:Column Source="AccountName" Caption="$om.account.name$" Wrap="false">
                        <Filter Type="text" Size="100" />
                    </ug:Column>
                    <ug:Column Source="AccountStatusID" AllowSorting="false" ExternalSourceName="#accountstatusname" Caption="$om.accountstatus.name$" Wrap="false">
                        <Filter Type="text" Size="100" />
                    </ug:Column>
                    <ug:Column Source="AccountCountryID" AllowSorting="false" ExternalSourceName="#countryname" Caption="$objecttype.cms_country$" Wrap="false">
                        <Filter Type="text" Size="100" />
                    </ug:Column>
                    <ug:Column Source="AccountSiteID" AllowSorting="false" Caption="$general.sitename$" ExternalSourceName="#sitenameorglobal"
                        Name="SiteName" Wrap="false" />
                    <ug:Column Width="100%" />
                </GridColumns>
                <GridOptions DisplayFilter="true" ShowSelection="true" SelectionColumn="AccountID" />
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
        </ContentTemplate>
    </cms:CMSUpdatePanel>
</asp:Panel>
