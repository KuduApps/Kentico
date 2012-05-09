<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RecycleBin_Documents.aspx.cs"
    Inherits="CMSModules_RecycleBin_Pages_RecycleBin_Documents" Theme="Default" EnableEventValidation="true"
    MaintainScrollPositionOnPostback="true" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master"
    Title="Site Manager - Documents recycle bin" %>

<%@ Register Src="~/CMSFormControls/Sites/SiteSelector.ascx" TagName="SiteSelector"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/RecycleBin/Controls/RecycleBin.ascx" TagName="RecycleBin"
    TagPrefix="cms" %>
<asp:Content ID="cntBefore" ContentPlaceHolderID="plcBeforeContent" runat="server">
    <asp:Panel runat="server" ID="pnlBody">
        <asp:Panel ID="pnlSiteSelector" runat="server" CssClass="PageHeaderLine SiteHeaderLine">
            <asp:Label ID="lblSite" runat="server" />
            <cms:SiteSelector ID="siteSelector" runat="server" IsLiveSite="false" />
        </asp:Panel>
    </asp:Panel>
</asp:Content>
<asp:Content ID="cntBody" ContentPlaceHolderID="plcContent" runat="server">
    <cms:CMSUpdatePanel ID="pnlUpdate" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <cms:RecycleBin ID="recycleBin" runat="server" IsSingleSite="false" />
        </ContentTemplate>
    </cms:CMSUpdatePanel>
</asp:Content>
