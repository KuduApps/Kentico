<%@ Page Language="C#" MasterPageFile="~/CMSMasterPages/UI/EmptyPage.master" AutoEventWireup="true"
    Inherits="CMSModules_Ecommerce_Pages_Tools_Header" Title="E-commerce - Header"
    Theme="default" CodeFile="Header.aspx.cs" %>

<%@ Register Src="~/CMSAdminControls/UI/PageElements/FrameResizer.ascx" TagName="FrameResizer"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSAdminControls/UI/UIProfiles/UIToolbar.ascx" TagName="UIToolbar"
    TagPrefix="cms" %>
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <asp:Panel runat="server" ID="pnlBody" CssClass="ContentMenu">
        <cms:FrameResizer ID="frmResizer" runat="server" MinSize="6, *;0, 0, 0, *" Vertical="True"
            CssPrefix="Vertical" />
        <asp:Panel runat="server" ID="pnlContentMenu" CssClass="ContentMenuLeft">
            <cms:UIToolbar ID="uiToolbarElem" TargetFrameset="ecommerceContent" runat="server"
                ModuleName="CMS.Ecommerce" QueryParameterName="resourcename" />
        </asp:Panel>
    </asp:Panel>
</asp:Content>
