<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="CMSSiteManager_Sites_Site_Edit_Cultures" Theme="Default" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master"
    Title="Site Edit - Cultures" CodeFile="Site_Edit_Cultures.aspx.cs" %>
<%@ Register src="~/CMSAdminControls/UI/UniSelector/UniSelector.ascx" tagname="UniSelector" tagprefix="cms" %>


<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <asp:LinkButton ID="lnkAssignDefault" runat="server" Visible="false" OnClick="AssignDefaultCulture" />
    <asp:Label runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false"
        Visible="false" />
    <asp:Label runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false"
        Visible="false" />
    <asp:PlaceHolder ID="plcAll" runat="server">
        <asp:Label ID="lblAvialable" runat="server" CssClass="BoldInfoLabel" />
        <cms:UniSelector ID="uniSelector" runat="server" IsLiveSite="false" OrderBy="CultureName"
            ObjectType="cms.culture" SelectionMode="Multiple" ResourcePrefix="cultureselect" />
    </asp:PlaceHolder>
</asp:Content>
