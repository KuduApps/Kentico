<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Polls_Tools_Polls_Sites"
    MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="Polls - sites"
    Theme="Default" CodeFile="Polls_Sites.aspx.cs" %>

<%@ Register Src="~/CMSAdminControls/UI/UniSelector/UniSelector.ascx" TagName="UniSelector"
    TagPrefix="cms" %>
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <asp:Label runat="server" ID="lblInfo" CssClass="InfoLabel" EnableViewState="false"
        Visible="false" />
    <asp:Label runat="server" ID="lblError" runat="server" CssClass="ErrorLabel" Visible="false"
        EnableViewState="false" />
    <asp:PlaceHolder ID="plcC" runat="server" Visible="false">
        <asp:Label ID="lblAvialable" runat="server" CssClass="BoldInfoLabel" />
        <cms:UniSelector ID="usSites" runat="server" IsLiveSite="false" ObjectType="cms.site"
            SelectionMode="Multiple" ResourcePrefix="sitesselect" />
    </asp:PlaceHolder>
</asp:Content>
