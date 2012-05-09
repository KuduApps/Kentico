<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_Categories_Controls_MultipleCategoriesSelector"
    CodeFile="MultipleCategoriesSelector.ascx.cs" %>
<%@ Register Src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" TagName="UniGrid" TagPrefix="cms" %>
<%@ Register Src="~/CMSAdminControls/UI/PageElements/Help.ascx" TagName="Help" TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/Categories/Controls/CategoryEdit.ascx" TagName="CategoryEdit"
    TagPrefix="uc1" %>
<%@ Register Src="~/CMSAdminControls/UI/UniSelector/UniSelector.ascx" TagName="UniSelector"
    TagPrefix="cms" %>
<asp:Panel ID="pnlInfo" runat="server" Visible="false">
    <asp:Label ID="lblInfo" runat="server" CssClass="InfoLabel" EnableViewState="false" /><br />
</asp:Panel>
<cms:UniSelector ID="selectCategory" runat="server" ReturnColumnName="CategoryID"
    ObjectType="cms.category" ResourcePrefix="categoryselector" OrderBy="CategoryNamePath"
    AdditionalColumns="CategoryNamePath,CategoryEnabled" SelectionMode="Multiple"
    AllowEmpty="false" IsLiveSite="false" />
