<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MenuActions.ascx.cs" Inherits="CMSModules_Settings_Controls_MenuActions" %>
<style type="text/css">
    .CustomSettings_MenuActions
    {
        width: 220px;
        height: 49px;
        padding: 8px 10px;
        float: none;
        clear: both;
    }
    .CustomSettings_MenuLine
    {
        width: 220px;
        height: 30px;
        float: none;
        clear: both;
    }
    .CustomSettings_MenuItem
    {
        width: 105px;
        height: 18px;
        line-height: 18px;
        overflow: hidden;
    }
    .CustomSettings_MenuItem_Disabled
    {
        width: 105px;
        height: 18px;
        line-height: 18px;
        overflow: hidden;
        color: #ccc;
    }
    .CustomSettings_MenuItem_Disabled a
    {
        text-decoration: underline;
        cursor: pointer;
        color: #AAA;
    }
    .CustomSettings_MenuItem_Disabled IMG, .CustomSettings_MenuItem IMG
    {
        margin: 2px;
        text-align: center;
        float: left;
    }
    .RTL .CustomSettings_MenuItem_Disabled IMG, .RTL .CustomSettings_MenuItem IMG
    {
        float: right;
    }
    .CustomSettings_MenuSeparator
    {
        width: 10px;
        height: 30px;
    }
</style>
<div class="CustomSettings_MenuActions TreeMenuPadding">
    <div class="CustomSettings_MenuLine">
        <asp:Panel ID="pnlNewElem" runat="server" CssClass="LeftAlign CustomSettings_MenuItem"
            EnableViewState="false">
            <asp:Image ID="imgNewElem" runat="server" EnableViewState="false" />
            <asp:LinkButton ID="btnNewElem" runat="server" OnClick="btnNewElem_Click" EnableViewState="false" />
        </asp:Panel>
        <div class="LeftAlign CustomSettings_MenuSeparator">
        </div>
        <asp:Panel ID="pnlMoveUp" runat="server" CssClass="LeftAlign CustomSettings_MenuItem_Disabled"
            EnableViewState="false">
            <asp:Image ID="imgMoveUp" runat="server" EnableViewState="false" />
            <asp:LinkButton ID="btnMoveUp" runat="server" OnClick="btnMoveUp_Click" EnableViewState="false" />
        </asp:Panel>
    </div>
    <div class="CustomSettings_MenuLine">
        <asp:Panel ID="pnlDeleteElem" runat="server" CssClass="LeftAlign CustomSettings_MenuItem_Disabled"
            EnableViewState="false">
            <asp:Image ID="imgDeleteElem" runat="server" EnableViewState="false" />
            <asp:LinkButton ID="btnDeleteElem" runat="server" OnClick="btnDeleteElem_Click" EnableViewState="false" />
        </asp:Panel>
        <div class="LeftAlign CustomSettings_MenuSeparator">
        </div>
        <asp:Panel ID="pnlMoveDown" runat="server" CssClass="LeftAlign CustomSettings_MenuItem_Disabled"
            EnableViewState="false">
            <asp:Image ID="imgMoveDown" runat="server" EnableViewState="false" />
            <asp:LinkButton ID="btnMoveDown" runat="server" OnClick="btnMoveDown_Click" EnableViewState="false" />
        </asp:Panel>
    </div>
</div>
<asp:HiddenField ID="hidSelectedElem" runat="server" />
<asp:Literal ID="ltlScript" runat="server" EnableViewState="false" />
