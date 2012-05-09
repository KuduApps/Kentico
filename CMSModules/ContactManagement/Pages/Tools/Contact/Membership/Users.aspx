<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Users.aspx.cs" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master"
    Title="Contact properties" Inherits="CMSModules_ContactManagement_Pages_Tools_Contact_Membership_Users"
    Theme="Default" %>

<%@ Register Src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" TagName="UniGrid" TagPrefix="cms" %>
<%@ Register Namespace="CMS.UIControls.UniGridConfig" TagPrefix="ug" Assembly="CMS.UIControls" %>
<%@ Register Src="~/CMSModules/Membership/FormControls/Users/selectuser.ascx" TagName="SelectUser"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/ContactManagement/Controls/UI/Contact/Membership/UsersFilter.ascx"
    TagName="UsersFilter" TagPrefix="cms" %>
<asp:Content ID="contentControls" ContentPlaceHolderID="plcActions" runat="server">
    <div class="PageHeaderItem">
        <cms:SelectUser runat="server" ID="selectUser" HideHiddenUsers="true" HideDisabledUsers="true"
            HideNonApprovedUsers="true" />
    </div>
    <div class="ClearBoth">
        &nbsp;
    </div>
</asp:Content>
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <cms:CMSUpdatePanel ID="pnlUpdate" runat="server">
        <ContentTemplate>
            <cms:UsersFilter runat="server" ID="fltElem" runat="server" />
            <cms:UniGrid runat="server" ID="gridElem" ObjectType="om.membershipuserlist" Columns="MembershipID,UserName,LastName,FirstName,Email,ContactSiteID,ContactFullNameJoined,ContactMergedWithContactID"
                IsLiveSite="false" OrderBy="LastName" ShowObjectMenu="false">
                <GridActions Parameters="MembershipID">
                    <ug:Action Name="delete" Caption="$General.Delete$" CommandArgument="MembershipID"
                        Icon="Delete.png" ExternalSourceName="delete" Confirmation="$General.ConfirmDelete$" />
                </GridActions>
                <GridColumns>
                    <ug:Column Source="FirstName" Caption="$general.firstname$" Wrap="false" />
                    <ug:Column Source="LastName" Caption="$general.lastname$" Wrap="false" />
                    <ug:Column Source="Email" Caption="$general.email$" Wrap="false" Name="email" />
                    <ug:Column Source="UserName" Caption="$general.username$" Wrap="false" />
                    <ug:Column Source="ContactFullNameJoined" Caption="$om.contact.name$" Wrap="false"
                        Name="contactname" />
                    <ug:Column Source="ContactSiteID" ExternalSourceName="#sitenameorglobal" Caption="$general.sitename$"
                        Wrap="false" Name="sitename" />
                    <ug:Column Width="100%" />
                </GridColumns>
            </cms:UniGrid>
        </ContentTemplate>
    </cms:CMSUpdatePanel>
</asp:Content>
