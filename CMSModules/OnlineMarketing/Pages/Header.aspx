<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Header.aspx.cs" Inherits="CMSModules_OnlineMarketing_Pages_Header" 
    MasterPageFile="~/CMSMasterPages/UI/EmptyPage.master" Theme="default" Title="On-line marketing - Header" %>

<%@ Register Src="~/CMSAdminControls/UI/PageElements/FrameResizer.ascx" TagName="FrameResizer"
    TagPrefix="cms" %>
<%@ Register Src="~/CMSAdminControls/UI/UIProfiles/UIToolbar.ascx" TagName="UIToolbar"
    TagPrefix="cms" %>
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <asp:Panel runat="server" ID="pnlBody" CssClass="ContentMenu">
        <cms:FrameResizer ID="frmResizer" runat="server" MinSize="6, *, 30;0, 0, 0, *" Vertical="True"
            CssPrefix="Vertical" />
        <asp:Panel runat="server" ID="pnlContentMenu" CssClass="ContentMenuLeft">
            <cms:UIToolbar ID="uiToolbarElem" TargetFrameset="content" runat="server" ModuleName="CMS.OnlineMarketing"
                QueryParameterName="resourcename" RememberSelectedItem="true" />
        </asp:Panel>
    </asp:Panel>
</asp:Content>
