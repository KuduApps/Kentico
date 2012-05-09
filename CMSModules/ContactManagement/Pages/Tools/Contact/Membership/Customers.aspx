<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Customers.aspx.cs" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master"
    Title="Contact properties" Inherits="CMSModules_ContactManagement_Pages_Tools_Contact_Membership_Customers"
    Theme="Default" %>

<%@ Register Src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" TagName="UniGrid" TagPrefix="cms" %>
<%@ Register Namespace="CMS.UIControls.UniGridConfig" TagPrefix="ug" Assembly="CMS.UIControls" %>
<%@ Register Src="~/CMSModules/ContactManagement/Controls/UI/Contact/Membership/CustomersFilter.ascx"
    TagName="CustomersFilter" TagPrefix="cms" %>
<asp:Content ID="contentControls" ContentPlaceHolderID="plcActions" runat="server">
    <asp:Panel runat="server" ID="pnlSelectCustomer" CssClass="PageHeaderItem">
    </asp:Panel>
    <div class="ClearBoth">
        &nbsp;
    </div>
</asp:Content>
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <cms:CustomersFilter runat="server" ID="fltElem" runat="server" />
    <cms:UniGrid runat="server" ID="gridElem" ObjectType="om.membershipcustomerlist"
        Columns="MembershipID,CustomerLastName,CustomerFirstName,CustomerEmail,ContactSiteID,ContactFullNameJoined,ContactMergedWithContactID,CustomerCompany"
        IsLiveSite="false" OrderBy="CustomerLastName" ShowObjectMenu="false" ShowActionsMenu="true">
        <GridActions Parameters="MembershipID">
            <ug:Action Name="delete" CommandArgument="MembershipID" Caption="$General.Delete$"
                Icon="Delete.png" ExternalSourceName="delete" Confirmation="$General.ConfirmDelete$" />
        </GridActions>
        <GridColumns>
            <ug:Column Source="CustomerFirstName" Caption="$general.firstname$" Wrap="false" />
            <ug:Column Source="CustomerLastName" Caption="$general.lastname$" Wrap="false" />
            <ug:Column Source="CustomerCompany" Caption="$Unigrid.Customers.Columns.CustomerCompanyName$" Wrap="false" />
            <ug:Column Source="CustomerEmail" Caption="$general.email$" Wrap="false" />
            <ug:Column Source="ContactFullNameJoined" Caption="$om.contact.name$" Wrap="false"
                Name="contactname" />
            <ug:Column Source="ContactSiteID" ExternalSourceName="#sitenameorglobal" Caption="$general.sitename$"
                Wrap="false" Name="sitename" />
            <ug:Column Width="100%" />
        </GridColumns>
    </cms:UniGrid>
</asp:Content>
