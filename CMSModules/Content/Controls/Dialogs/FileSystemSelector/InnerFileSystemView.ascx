<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="CMSModules_Content_Controls_Dialogs_FileSystemSelector_InnerFileSystemView" CodeFile="InnerFileSystemView.ascx.cs" %>
<%@ Register Src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" TagName="UniGrid" TagPrefix="cms" %>
<asp:Literal ID="ltlScript" runat="server" EnableViewState="false"></asp:Literal>
<div class="DialogViewArea" style="height: 100%;">
    <asp:Label ID="lblInfo" runat="server" CssClass="InfoLabel" Visible="false" EnableViewState="false"></asp:Label>
    <asp:PlaceHolder ID="plcViewArea" runat="server">
        <div class="ListView">
        <cms:UniGrid runat="server" ID="gridList" />
        </div>
    </asp:PlaceHolder>
</div>
<asp:HiddenField ID="hdnItemToColorize" runat="server" />
