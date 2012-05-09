<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_SmartSearch_Controls_UI_SearchIndex_Cultures" CodeFile="SearchIndex_Cultures.ascx.cs" %>
<%@ Register src="~/CMSAdminControls/UI/UniSelector/UniSelector.ascx" tagname="UniSelector" tagprefix="cms" %>
<%@ Register Src="~/CMSFormControls/Sites/SiteSelector.ascx" TagName="SiteSelector" TagPrefix="cms" %>

<asp:Panel ID="pnlBody" runat="server">
    <cms:CMSUpdatePanel ID="pnlUpdate" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Panel ID="pnlContent" runat="server" CssClass="PageContent">
                <asp:Label runat="server" ID="lblError" CssClass="ErrorLabel" EnableViewState="false"
                    Visible="false" />
                <asp:Label runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false"
                    Visible="false" />
                <asp:Panel runat="server" ID="pnlDisabled" CssClass="DisabledInfoPanel" Visible="false">
                    <cms:LocalizedLabel runat="server" ID="lblDisabled" EnableViewState="false" ResourceString="srch.searchdisabledinfo"
                        CssClass="InfoLabel"></cms:LocalizedLabel>
                </asp:Panel>
                <asp:Panel ID="pnlForm" runat="server">
                    <strong>
                        <cms:LocalizedLabel runat="server" ID="lblInfoLabel" CssClass="InfoLabel" EnableViewState="false"
                            ResourceString="srch.index.cultures" DisplayColon="true" /></strong>
                    <cms:UniSelector ID="uniSelector" runat="server" IsLiveSite="false" ObjectType="cms.culture"
                        OrderBy="CultureName" SelectionMode="Multiple" ResourcePrefix="cultureselect"
                        OnOnSelectionChanged="uniSelector_OnSelectionChanged" />
                </asp:Panel>
                <cms:CMSTextBox ID="hidItem" runat="server" Style="display: none;" />
                <cms:CMSTextBox ID="hidAllItems" runat="server" Style="display: none;" />
            </asp:Panel>
        </ContentTemplate>
    </cms:CMSUpdatePanel>
</asp:Panel>
