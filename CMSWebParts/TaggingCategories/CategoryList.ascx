<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="CMSWebParts_TaggingCategories_CategoryList" CodeFile="~/CMSWebParts/TaggingCategories/CategoryList.ascx.cs" %>
<asp:Label ID="lblInfo" runat="server" Visible="false" CssClass="InfoLabel" />
<cms:BasicRepeater ID="rptCategoryList" runat="server" />
<asp:Literal ID="ltlList" runat="server"></asp:Literal>