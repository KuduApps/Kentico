<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSSiteManager_Development_Cultures_Culture_List"
    Theme="Default" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="Cultures - Culture List"
    CodeFile="Culture_List.aspx.cs" %>

<%@ Register Src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" TagName="UniGrid" TagPrefix="cms" %>
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <asp:Label ID="lblError" runat="server" CssClass="ErrorLabel" EnableViewState="false"
        Visible="false" />
    <cms:UniGrid ID="UniGridCultures" runat="server" GridName="Culture_List.xml" OrderBy="CultureName"
        IsLiveSite="false" />
</asp:Content>
