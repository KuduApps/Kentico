<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Contacts.ascx.cs" Inherits="CMSModules_ContactManagement_Controls_UI_ContactGroup_Contacts" %>
<%@ Register Src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" TagName="UniGrid" TagPrefix="cms" %>
<%@ Register Namespace="CMS.UIControls.UniGridConfig" TagPrefix="ug" Assembly="CMS.UIControls" %>
<%@ Register Src="~/CMSModules/ContactManagement/FormControls/ContactSelector.ascx"
    TagName="ContactSelector" TagPrefix="cms" %>
<asp:Panel ID="pnlSelector" runat="server" CssClass="PageHeaderLine">
    <cms:ContactSelector ID="contactSelector" runat="server" IsLiveSite="false" />
    <div class="ClearBoth">
        &nbsp;</div>
</asp:Panel>
<asp:Panel ID="pnlContent" runat="server" CssClass="PageContent">
    <cms:CMSUpdatePanel ID="pnlUpdate" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <cms:UniGrid runat="server" ID="gridElem" OrderBy="ContactLastName" ObjectType="om.contactgroupcontactlist" ShowObjectMenu="false"
                IsLiveSite="false" Columns="ContactID,ContactFirstName,ContactLastName,ContactEmail,ContactStatusID,ContactCountryID,ContactGroupMemberFromCondition,ContactGroupMemberFromAccount,ContactGroupMemberFromManual,ContactSiteID">
                <GridActions Parameters="ContactID">
                    <ug:Action Name="edit" Caption="$om.contact.viewdetail$" Icon="contactdetail.png" OnClick="EditContact({0});return false;"
                        ModuleName="CMS.OnlineMarketing" />
                    <ug:Action Name="remove" Caption="$General.Remove$" Icon="Delete.png" Confirmation="$General.ConfirmRemove$"
                        ModuleName="CMS.OnlineMarketing" ExternalSourceName="remove" />
                </GridActions>
                <GridColumns>
                    <ug:Column Source="ContactFirstName" Caption="$om.contact.firstname$" Wrap="false">
                        <Filter Type="text" Size="100" />
                    </ug:Column>
                    <ug:Column Source="ContactLastName" Caption="$om.contact.lastname$" Wrap="false">
                        <Filter Type="text" Size="100" />
                    </ug:Column>
                    <ug:Column Source="ContactEmail" Caption="$general.email$" Wrap="false">
                        <Filter Type="text" Size="100" />
                    </ug:Column>
                    <ug:Column Source="ContactStatusID" AllowSorting="false" ExternalSourceName="#contactstatusname" Caption="$om.contactstatus$" Wrap="false">
                        <Filter Type="text" Size="100" />
                    </ug:Column>
                    <ug:Column Source="ContactCountryID" AllowSorting="false" ExternalSourceName="#countryname" Caption="$objecttype.cms_country$" Wrap="false">
                        <Filter Type="text" Size="100" />
                    </ug:Column>
                    <ug:Column Source="ContactSiteID" AllowSorting="false" Caption="$general.sitename$" ExternalSourceName="#sitenameorglobal" Name="SiteName" Wrap="false" />
                    <ug:Column Source="ContactGroupMemberFromCondition" ExternalSourceName="#yesno" Caption="$om.contactgroupmember.memberfromcondition$"
                        Wrap="false">
                        <Filter Type="bool" />
                    </ug:Column>
                    <ug:Column Source="ContactGroupMemberFromAccount" ExternalSourceName="#yesno" Caption="$om.contactgroupmember.MemberFromAccount$"
                        Wrap="false">
                        <Filter Type="bool" />
                    </ug:Column>
                    <ug:Column Source="ContactGroupMemberFromManual" ExternalSourceName="#yesno" Caption="$om.contactgroupmember.MemberFromManual$"
                        Wrap="false">
                        <Filter Type="bool" />
                    </ug:Column>
                    <ug:Column Width="100%" />
                </GridColumns>
                <GridOptions DisplayFilter="true" ShowSelection="true" SelectionColumn="ContactID" />
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
