<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_Friends_Controls_FriendsUserList" CodeFile="FriendsUserList.ascx.cs" %>
<%@ Register src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" tagname="UniGrid" tagprefix="cms" %>
<%@ Register Src="~/CMSModules/Friends/Controls/FriendsSearchBox.ascx" TagName="FriendsSearchBox"
    TagPrefix="cms" %>
<asp:Panel ID="pnlInfo" CssClass="Info" Visible="false" runat="server">
    <asp:Label runat="server" ID="lblInfo" EnableViewState="false" CssClass="InfoLabel" />
    <asp:Label runat="server" ID="lblError" EnableViewState="false" CssClass="ErrorLabel" />
</asp:Panel>
<div class="ListPanel">
    <asp:PlaceHolder ID="plcFilter" runat="server">
        <cms:FriendsSearchBox ID="searchBox" runat="server" FilterComment="false" />
        <br />
    </asp:PlaceHolder>
    <cms:UniGrid id="gridFriends" runat="server" GridName="~/CMSModules/Friends/Controls/FriendsUserList.xml" />
</div>
