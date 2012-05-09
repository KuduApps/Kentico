<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Configuration_Header.aspx.cs"
    Theme="Default" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Inherits="CMSModules_Ecommerce_Pages_SiteManager_Configuration_Header" %>

<%@ Register Src="~/CMSAdminControls/UI/PageElements/FrameResizer.ascx" TagName="FrameResizer"
    TagPrefix="cms" %>
<asp:Content ContentPlaceHolderID="plcBeforeContent" ID="BeforeContent" runat="server">
    <cms:FrameResizer runat="server" ID="frmResizer" IsLiveSite="false" MinSize="6, *"
        Vertical="True" CssPrefix="Vertical" />
    <asp:Panel runat="server" ID="PanelSeparator" CssClass="HeaderSeparator">
        &nbsp;</asp:Panel>
</asp:Content>
