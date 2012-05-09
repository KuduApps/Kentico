<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSAdminControls_UI_UIProfiles_MenuActions" CodeFile="MenuActions.ascx.cs" %>
<style type="text/css">
    </style>
<div class="UIProfile_MenuActions TreeMenuPadding">
    <div class="UIProfile_MenuLine">
        <asp:Panel ID="pnlNewElem" runat="server" CssClass="LeftAlign UIProfile_MenuItem"
            EnableViewState="false">
            <asp:Image ID="imgNewElem" runat="server" EnableViewState="false" />
            <asp:LinkButton ID="btnNewElem" runat="server" OnClick="btnNewElem_Click" EnableViewState="false" />
        </asp:Panel>
        <div class="LeftAlign UIProfile_MenuSeparator">
        </div>
        <asp:Panel ID="pnlMoveUp" runat="server" CssClass="LeftAlign UIProfile_MenuItem_Disabled"
            EnableViewState="false">
            <asp:Image ID="imgMoveUp" runat="server" EnableViewState="false" />
            <asp:LinkButton ID="btnMoveUp" runat="server" OnClick="btnMoveUp_Click" EnableViewState="false" />
        </asp:Panel>
    </div>
    <div class="UIProfile_MenuLine">
        <asp:Panel ID="pnlDeleteElem" runat="server" CssClass="LeftAlign UIProfile_MenuItem_Disabled"
            EnableViewState="false">
            <asp:Image ID="imgDeleteElem" runat="server" EnableViewState="false" />
            <asp:LinkButton ID="btnDeleteElem" runat="server" OnClick="btnDeleteElem_Click" EnableViewState="false" />
        </asp:Panel>
        <div class="LeftAlign UIProfile_MenuSeparator">
        </div>
        <asp:Panel ID="pnlMoveDown" runat="server" CssClass="LeftAlign UIProfile_MenuItem_Disabled"
            EnableViewState="false">
            <asp:Image ID="imgMoveDown" runat="server" EnableViewState="false" />
            <asp:LinkButton ID="btnMoveDown" runat="server" OnClick="btnMoveDown_Click" EnableViewState="false" />
        </asp:Panel>
    </div>
</div>
<asp:HiddenField ID="hidSelectedElem" runat="server"/>
<asp:Literal ID="ltlScript" runat="server" EnableViewState="false" />
