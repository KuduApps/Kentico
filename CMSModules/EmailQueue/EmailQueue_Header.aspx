<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EmailQueue_Header.aspx.cs"
    Inherits="CMSModules_EmailQueue_EmailQueue_Header" Theme="Default"
    MasterPageFile="~/CMSMasterPages/UI/EmptyPage.master" Title="Email Queue Edit" %>
    
<%@ Register src="~/CMSAdminControls/UI/PageElements/PageTitle.ascx" tagname="PageTitle" tagprefix="cms" %>
<%@ Register Src="~/CMSAdminControls/UI/PageElements/FrameResizer.ascx" TagName="FrameResizer" TagPrefix="cms" %>
<%@ Register Src="~/CMSFormControls/Sites/SiteSelector.ascx" TagName="SiteSelector" TagPrefix="cms" %>

<asp:Content ID="cntBody" ContentPlaceHolderID="plcContent" runat="server">
    <cms:FrameResizer ID="FrameResizer1" runat="server" MinSize="6, *" Vertical="True"
        CssPrefix="Vertical" FramesetName="rowsFrameset" />
    <asp:Panel runat="server" ID="Panel1" CssClass="TabsPageHeader">
        <asp:Panel runat="server" ID="Panel2" CssClass="PageHeader">
            <cms:PageTitle ID="titleElem" runat="server" />
        </asp:Panel>
        <asp:Panel ID="pnlActions" runat="server" CssClass="PageHeaderLine SiteHeaderLine NoBorderLine">
            <cms:LocalizedLabel ID="lblSite" runat="server" EnableViewState="false" ResourceString="general.site"
                DisplayColon="true" />
            <cms:SiteSelector ID="siteSelector" runat="server" IsLiveSite="false" />
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlTabsContainer" CssClass="TabsPageTabs LightTabs BreadTabs">
            <asp:Panel runat="server" ID="pnlLeft" CssClass="TabsLeft">
                &nbsp;
            </asp:Panel>
            <asp:Panel runat="server" ID="Panel3" CssClass="TabsTabs">
                <asp:Panel runat="server" ID="Panel4" CssClass="TabsWhite">
                    <cms:BasicTabControl ID="BasicTabControlMenu" runat="server" UseClientScript="true" />
                </asp:Panel>
            </asp:Panel>
            <asp:Panel runat="server" ID="Panel5" CssClass="TabsRight">
                &nbsp;
            </asp:Panel>
        </asp:Panel>
        <asp:Panel runat="server" ID="PanelSeparator" CssClass="HeaderSeparator">
            &nbsp;
        </asp:Panel>
    </asp:Panel>    
</asp:Content>