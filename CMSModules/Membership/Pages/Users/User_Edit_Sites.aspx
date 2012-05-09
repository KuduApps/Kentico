<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_Membership_Pages_Users_User_Edit_Sites" Theme="Default" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master"
    Title="User Edit - Sites" CodeFile="User_Edit_Sites.aspx.cs" %>
<%@ Register src="~/CMSAdminControls/UI/UniSelector/UniSelector.ascx" tagname="UniSelector" tagprefix="cms" %>



<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <asp:Label runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false"
        Visible="false" />
    <asp:Label runat="server" ID="lblError" CssClass="ErrorLabel" Visible="false" EnableViewState="false" />
    <asp:PlaceHolder ID="plcTable" runat="server">
        <asp:Label ID="lblAvialable" runat="server" CssClass="BoldInfoLabel" EnableViewState="false" />
        <cms:UniSelector ID="usSites" runat="server" IsLiveSite="false" ObjectType="cms.site"
            SelectionMode="Multiple" ResourcePrefix="sitesselect" />
    </asp:PlaceHolder>
</asp:Content>
