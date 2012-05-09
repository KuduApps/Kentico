<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_InlineControls_Pages_Development_List"
    Theme="Default" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="Inline controls - list"
    CodeFile="List.aspx.cs" %>

<%@ Register Src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" TagName="UniGrid" TagPrefix="cms" %>
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <cms:UniGrid runat="server" ID="UniGrid" GridName="List.xml" OrderBy="ControlDisplayName"
        IsLiveSite="false" />
</asp:Content>
