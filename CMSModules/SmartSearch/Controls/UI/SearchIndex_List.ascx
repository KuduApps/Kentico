<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_SmartSearch_Controls_UI_SearchIndex_List" CodeFile="SearchIndex_List.ascx.cs" %>
<%@ Register src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" tagname="UniGrid" tagprefix="cms" %>
<asp:Panel ID="pnlBody" runat="server">
    <cms:LocalizedLabel runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false"
        Visible="false" />
    <cms:LocalizedLabel runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false"
        Visible="false" />
    <asp:Panel runat="server" ID="pnlDisabled" CssClass="DisabledInfoPanel" Visible="false">
        <cms:LocalizedLabel runat="server" ID="lblDisabled" EnableViewState="false" ResourceString="srch.searchdisabledinfo"
            CssClass="InfoLabel"></cms:LocalizedLabel>
    </asp:Panel>
    <asp:Panel runat="server" ID="pnlPathTooLong" CssClass="DisabledInfoPanel" Visible="false">
        <cms:LocalizedLabel runat="server" ID="lblPathTooLong" EnableViewState="false" ResourceString="srch.pathtoolong" CssClass="ErrorLabel" ></cms:LocalizedLabel>
    </asp:Panel>
    <cms:UniGrid runat="server" ID="UniGrid" GridName="~/CMSModules/SmartSearch/Controls/UI/SearchIndex_List.xml"
        IsLiveSite="false" />
</asp:Panel>
