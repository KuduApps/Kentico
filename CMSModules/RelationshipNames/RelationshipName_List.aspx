<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_RelationshipNames_RelationshipName_List"
    Theme="Default" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="Relationship names - List"
    CodeFile="RelationshipName_List.aspx.cs" %>

<%@ Register Src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" TagName="UniGrid" TagPrefix="cms" %>
<asp:Content ContentPlaceHolderID="plcContent" runat="server">
    <cms:UniGrid ID="UniGridRelationshipNames" runat="server" GridName="RelationshipNames_List.xml"
        OrderBy="RelationshipDisplayName" IsLiveSite="false" />
</asp:Content>
