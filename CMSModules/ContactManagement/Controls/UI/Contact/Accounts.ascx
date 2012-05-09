<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Accounts.ascx.cs" Inherits="CMSModules_ContactManagement_Controls_UI_Contact_Accounts" %>
<%@ Register Src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" TagName="UniGrid" TagPrefix="cms" %>
<%@ Register Namespace="CMS.UIControls.UniGridConfig" TagPrefix="ug" Assembly="CMS.UIControls" %>
<%@ Register Src="~/CMSModules/ContactManagement/FormControls/AccountSelector.ascx"
    TagName="AccountSelector" TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/ContactManagement/FormControls/ContactRoleSelector.ascx"
    TagName="ContactRoleSelector" TagPrefix="cms" %>
<asp:Panel ID="pnlSelector" runat="server" CssClass="PageHeaderLine">
    <cms:AccountSelector ID="accountSelector" ShortID="as" runat="server" CssClass="LeftAlign AddContactSelector"
        IsLiveSite="false" />
    <asp:HiddenField ID="hdnRoleID" runat="server" />
    <div class="ClearBoth">
        &nbsp;</div>
</asp:Panel>
<asp:Panel ID="pnlBody" runat="server" CssClass="PageContent">
    <cms:CMSUpdatePanel ID="pnlUpdate" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <cms:UniGrid runat="server" ID="gridElem" ShortID="g" OrderBy="AccountName" ObjectType="om.contactaccountlist"
                Columns="AccountContactID,AccountName,ContactRoleID,AccountStatusID,AccountCountryID,AccountID"
                IsLiveSite="false">
                <GridActions>
                    <ug:Action ExternalSourceName="edit" Name="edit" Caption="$om.account.viewdetail$" Icon="accountdetail.png"
                        ModuleName="CMS.OnlineMarketing" CommandArgument="AccountID" />
                    <ug:Action ExternalSourceName="selectrole" Name="selectrole" Caption="$om.contactrole.select$"
                        Icon="ContactRole.png" ModuleName="CMS.OnlineMarketing" />
                    <ug:Action ExternalSourceName="remove" Name="remove" Caption="$General.Remove$" Icon="Delete.png"
                        Confirmation="$General.ConfirmRemove$" ModuleName="CMS.OnlineMarketing" />
                </GridActions>
                <GridColumns>
                    <ug:Column Source="AccountName" Caption="$om.account.name$" Wrap="false">
                        <Filter Type="text" Size="100" />
                    </ug:Column>
                    <ug:Column Source="ContactRoleID" AllowSorting="false" ExternalSourceName="#contactrolename" Caption="$om.contactrole$" Wrap="false">
                        <Filter Type="text" Size="100" />
                    </ug:Column>
                    <ug:Column Source="AccountStatusID" AllowSorting="false" ExternalSourceName="#accountstatusname" Caption="$om.accountstatus.name$" Wrap="false">
                        <Filter Type="text" Size="100" />
                    </ug:Column>
                    <ug:Column Source="AccountCountryID" AllowSorting="false" ExternalSourceName="#countryname" Caption="$objecttype.cms_country$" Wrap="false">
                        <Filter Type="text" Size="100" />
                    </ug:Column>
                    <ug:Column Width="100%" />
                </GridColumns>
                <GridOptions DisplayFilter="true" ShowSelection="true" SelectionColumn="AccountContactID" />
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
            <asp:HiddenField ID="hdnValue" runat="server" EnableViewState="false" />
        </ContentTemplate>
    </cms:CMSUpdatePanel>
</asp:Panel>
