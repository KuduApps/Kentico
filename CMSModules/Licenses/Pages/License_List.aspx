<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Licenses_Pages_License_List"
    Theme="Default" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="Licenses - Licenses List" CodeFile="License_List.aspx.cs" %>
<%@ Register src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" tagname="UniGrid" tagprefix="cms" %>

<asp:Content ContentPlaceHolderID="plcContent" runat="server">
    <cms:UniGrid ID="UniGridLicenses" runat="server" GridName="License_List.xml" OrderBy="LicenseDomain"
        IsLiveSite="false" />
</asp:Content>
