<%@ Page Language="C#" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" AutoEventWireup="true"
    Inherits="CMSModules_Ecommerce_Pages_Tools_Customers_Customer_List" Theme="Default"
    CodeFile="Customer_List.aspx.cs" %>

<%@ Register Src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" TagName="UniGrid" TagPrefix="cms" %>
<%@ Register Src="~/CMSAdminControls/UI/PageElements/HeaderActions.ascx" TagName="HeaderActions"
    TagPrefix="cms" %>
<asp:Content ID="cntActions" runat="server" ContentPlaceHolderID="plcActions">
    <cms:CMSUpdatePanel ID="pnlActons" runat="server">
        <ContentTemplate>
            <cms:HeaderActions ID="hdrActions" runat="server" />
        </ContentTemplate>
    </cms:CMSUpdatePanel>
</asp:Content>
<asp:Content ContentPlaceHolderID="plcContent" ID="content" runat="server">
    <cms:CMSUpdatePanel ID="pnlUpdate" runat="server">
        <ContentTemplate>
            <asp:Label ID="lblError" runat="server" Visible="false" EnableViewState="false" CssClass="ErrorLabel" />
            <cms:UniGrid runat="server" ID="UniGrid" GridName="Customers_List.xml" OrderBy="CustomerLastName"
                IsLiveSite="false" Columns="CustomerID, CustomerLastName, CustomerCompany, CustomerFirstName, CountryDisplayName, StateDisplayName, CustomerEmail, CustomerCreated, CustomerEnabled, CustomerUserID, CustomerSiteID" />
        </ContentTemplate>
    </cms:CMSUpdatePanel>
</asp:Content>
