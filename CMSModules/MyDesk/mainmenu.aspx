<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_MyDesk_mainmenu"
    Theme="Default" MasterPageFile="~/CMSMasterPages/UI/EmptyPage.master" CodeFile="mainmenu.aspx.cs" %>

<%@ Register Src="~/CMSAdminControls/UI/PageElements/FrameResizer.ascx" TagName="FrameResizer"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSAdminControls/UI/UIProfiles/UIToolbar.ascx" TagName="UIToolbar"
    TagPrefix="cms" %>
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <asp:Panel runat="server" ID="pnlBody" CssClass="ContentMenu">
        <cms:FrameResizer ID="frmResizer" runat="server" MinSize="6, *, 30;0, 0, 0, *" Vertical="True"
            CssPrefix="Vertical" />
        <asp:Panel runat="server" ID="pnlContentMenu" CssClass="ContentMenuLeft">
            <cms:UIToolbar ID="ucUIToolbar" TargetFrameset="frameMain" runat="server" ModuleName="CMS.MyDesk"
                RememberSelectedItem="true" HighlightFirstItem="true" QueryParameterName="resourcename" />
        </asp:Panel>
    </asp:Panel>
    <asp:Literal runat="server" ID="litScript" EnableViewState="false"></asp:Literal>
</asp:Content>
