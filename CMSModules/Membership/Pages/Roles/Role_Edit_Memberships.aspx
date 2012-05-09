<%@ Page Language="C#" AutoEventWireup="true" Theme="Default" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master"
    CodeFile="Role_Edit_Memberships.aspx.cs" Inherits="CMSModules_Membership_Pages_Roles_Role_Edit_Memberships" %>

<%@ Register Src="~/CMSAdminControls/UI/UniSelector/UniSelector.ascx" TagName="UniSelector"
    TagPrefix="cms" %>
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <asp:PlaceHolder ID="plcTable" runat="server">
        <cms:CMSUpdatePanel ID="pnlBasic" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <strong>
                    <cms:LocalizedLabel runat="server" ID="lblMembershipInfo" DisplayColon="true" ResourceString="role.assignedtomembership"
                        EnableViewState="false" CssClass="InfoLabel" />
                </strong>
                <cms:LocalizedLabel runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false"
                    ResourceString="General.ChangesSaved" Visible="false" />
                <cms:UniSelector runat="server" ID="usMemberships" IsLiveSite="false" ObjectType="cms.membership"
                     SelectionMode="Multiple" ResourcePrefix="membershipselector" />                  
            </ContentTemplate>
        </cms:CMSUpdatePanel>
    </asp:PlaceHolder>
</asp:Content>
