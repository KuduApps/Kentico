<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RecycleBin_Objects.aspx.cs"
    Inherits="CMSModules_RecycleBin_Pages_RecycleBin_Objects" Theme="Default" EnableEventValidation="false"
    MaintainScrollPositionOnPostback="true" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master"
    Title="Site Manager - Objects recycle bin" %>

<%@ Register Src="~/CMSFormControls/Sites/SiteSelector.ascx" TagName="SiteSelector"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/Objects/Controls/ObjectsRecycleBin.ascx" TagName="RecycleBin"
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
    <cms:RecycleBin ID="recycleBin" runat="server" IsLiveSite="false" IsSingleSite="false" />
</asp:Content>
