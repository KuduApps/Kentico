<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_Content_Controls_EditMenu"
    CodeFile="EditMenu.ascx.cs" %>
<%@ Register TagPrefix="cms" Namespace="CMS.skmMenuControl" Assembly="CMS.skmMenuControl" %>
<%@ Register Src="~/CMSAdminControls/UI/PageElements/Help.ascx" TagName="Help" TagPrefix="cms" %>
<asp:Panel ID="pnlMenu" runat="server" CssClass="LeftAlign">
    <cms:Menu ID="menuElem" ShortID="m" runat="server" EnableViewState="False" RenderScripts="false" />
    <cms:CMSButton ID="btnSave" runat="server" EnableViewState="false" OnClick="btnSave_Click" />
    <cms:CMSButton ID="btnReject" runat="server" EnableViewState="false" OnClick="btnReject_Click" />
    <cms:CMSButton ID="btnUndoCheckout" runat="server" EnableViewState="false" OnClick="btnUndoCheckout_Click" />
    <cms:CMSButton ID="btnCheckOut" runat="server" EnableViewState="false" OnClick="btnCheckOut_Click" />
    <cms:CMSButton ID="btnApprove" runat="server" EnableViewState="false" OnClick="btnApprove_Click" />
    <cms:CMSButton ID="btnCheckIn" runat="server" EnableViewState="false" OnClick="btnCheckIn_Click" />
</asp:Panel>
<asp:Panel ID="pnlHelp" runat="server" CssClass="RightAlign" Visible="false" EnableViewState="false">
    <div class="EditMenuHelp">
        <cms:Help ID="helpElem" runat="server" TopicName="page_tab" HelpName="helpTopic"
            EnableViewState="false" />
    </div>
</asp:Panel>
