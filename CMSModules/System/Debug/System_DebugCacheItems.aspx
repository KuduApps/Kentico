<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_System_Debug_System_DebugCacheItems"
    Theme="Default" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="Group list"
    MaintainScrollPositionOnPostback="true" CodeFile="System_DebugCacheItems.aspx.cs" %>

<%@ Register Src="CacheItemsGrid.ascx" TagName="CacheItemsGrid" TagPrefix="cms" %>
<%@ Register Src="~/CMSAdminControls/UI/PageElements/PageTitle.ascx" TagName="PageTitle"
    TagPrefix="cms" %>
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <div class="AlignRight">
        <cms:CMSButton runat="server" ID="btnClear" OnClick="btnClear_Click" CssClass="LongButton"
            EnableViewState="false" />
    </div>
    <cms:PageTitle ID="titleItems" runat="server" TitleCssClass="SubTitleHeader" />
    <br />
    <cms:CacheItemsGrid ID="gridItems" ShortID="gi" runat="server" />
    <br />
    <br />
    <br />
    <cms:PageTitle ID="titleDummy" runat="server" TitleCssClass="SubTitleHeader" />
    <br />
    <cms:CacheItemsGrid ID="gridDummy" ShortID="gd" runat="server" ShowDummyItems="true" />
</asp:Content>
