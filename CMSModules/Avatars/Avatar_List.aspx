<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Avatars_Avatar_List"
    Theme="Default" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="Avatars - List" CodeFile="Avatar_List.aspx.cs" %>
<%@ Register src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" tagname="UniGrid" tagprefix="cms" %>

<%@ Register Src="~/CMSModules/Avatars/AvatarFilter.ascx" TagName="AvatarFilter"
    TagPrefix="cms" %>
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <asp:Label ID="lblInfo" runat="server" Visible="false" CssClass="InfoLabel" EnableViewState="false" />
    <cms:AvatarFilter runat="server" ID="filterAvatars" />
    <div style="padding-top: 10px">
    </div>
    <cms:UniGrid runat="server" ID="unigridAvatarList" GridName="Avatar_List.xml" IsLiveSite="false" />
</asp:Content>
