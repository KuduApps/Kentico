<%@ Control Language="C#" AutoEventWireup="true" CodeFile="List.ascx.cs" Inherits="CMSModules_ContactManagement_Controls_UI_Contact_List" %>
<%@ Register Src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" TagName="UniGrid" TagPrefix="cms" %>
<%@ Register Namespace="CMS.UIControls.UniGridConfig" TagPrefix="ug" Assembly="CMS.UIControls" %>
<%@ Register Src="~/CMSModules/ContactManagement/Controls/UI/Contact/Filter.ascx"
    TagName="Filter" TagPrefix="cms" %>
<cms:CMSUpdatePanel ID="pnlUpdate" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <cms:Filter ID="filter" runat="server" ShortID="f" IsLiveSite="false" NotMerged="true" />
        <cms:UniGrid runat="server" ID="gridElem" ObjectType="om.contactlist" OrderBy="ContactLastName"
            Columns="ContactID,ContactLastName,ContactFirstName,ContactEmail,ContactStatusID,ContactCountryID,ContactSiteID,ContactMergedWithContactID,ContactGlobalContactID"
            IsLiveSite="false">
            <GridActions Parameters="ContactID">
                <ug:Action Name="edit" Caption="$General.Edit$" Icon="Edit.png" />
                <ug:Action ExternalSourceName="delete" Name="delete" Caption="$General.Delete$" Icon="Delete.png"
                    ModuleName="CMS.ContactManagement" Permissions="ModifyContacts" />
            </GridActions>
            <GridColumns>
                <ug:Column Source="ContactFirstName" Caption="$om.contact.firstname$" Wrap="false">
                </ug:Column>
                <ug:Column Source="ContactLastName" Caption="$om.contact.lastname$" Wrap="false">
                </ug:Column>
                <ug:Column Source="ContactEmail" Caption="$general.email$" Wrap="false">
                </ug:Column>
                <ug:Column Source="ContactStatusID" AllowSorting="false" ExternalSourceName="#contactstatusname"
                    Caption="$om.contactstatus$" Wrap="false">
                </ug:Column>
                <ug:Column Source="ContactCountryID" AllowSorting="false" ExternalSourceName="#countryname"
                    Caption="$general.country$" Wrap="false">
                </ug:Column>
                <ug:Column Source="ContactSiteID" AllowSorting="false" Caption="$general.site$" Wrap="false"
                    ExternalSourceName="#sitenameorglobal" Name="sitedisplayname" Localize="true">
                </ug:Column>
                <ug:Column Source="##ALL##" Caption="$om.contact.mergedinto$" Wrap="false" ExternalSourceName="ContactMergedWithContactID"
                    Name="merged" Localize="true">
                </ug:Column>
                <ug:Column Width="100%" />
            </GridColumns>
            <GridOptions DisplayFilter="false" ShowSelection="true" />
        </cms:UniGrid>
        <asp:Panel ID="pnlFooter" runat="server" CssClass="MassAction">
            <asp:DropDownList ID="drpWhat" runat="server" CssClass="DropDownFieldSmall" />
            <asp:DropDownList ID="drpAction" runat="server" CssClass="DropDownFieldSmall" />
            <cms:LocalizedButton ID="btnOk" runat="server" ResourceString="general.ok" CssClass="SubmitButton"
                EnableViewState="false" OnClick="btnOk_Click" />
            <br />
            <br />
        </asp:Panel>
        <asp:Label ID="lblInfo" runat="server" CssClass="InfoLabel" EnableViewState="false" />
        <asp:HiddenField ID="hdnIdentificator" runat="server" EnableViewState="false" />
        <asp:Literal ID="ltlScript" runat="server" EnableViewState="false" />
    </ContentTemplate>
</cms:CMSUpdatePanel>
