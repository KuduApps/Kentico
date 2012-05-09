<%@ Control Language="C#" AutoEventWireup="true" Inherits="CMSModules_MediaLibrary_Controls_UI_MediaLibraryList"
    CodeFile="MediaLibraryList.ascx.cs" %>
<%@ Register Src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" TagName="UniGrid" TagPrefix="cms" %>
<asp:Panel ID="pnlBody" runat="server">
    <cms:LocalizedLabel ID="lblError" runat="server" CssClass="ErrorLabel" Visible="false"
        EnableViewState="false" />
    <cms:UniGrid runat="server" ID="gridElem" GridName="~/CMSModules/MediaLibrary/Tools/Library_List.xml"
        OrderBy="LibraryDisplayName" />
</asp:Panel>
