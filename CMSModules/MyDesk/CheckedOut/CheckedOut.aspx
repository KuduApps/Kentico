<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_MyDesk_CheckedOut_CheckedOut"
    Theme="Default" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="MyDesk - Checked Out Documents"
    CodeFile="CheckedOut.aspx.cs" %>

<%@ Register Src="~/CMSModules/AdminControls/Controls/Documents/Documents.ascx" TagName="CheckedOut"
    TagPrefix="cms" %>
<asp:Content ContentPlaceHolderID="plcContent" runat="server">
    <cms:CheckedOut runat="server" ID="ucCheckedOut" ListingType="CheckedOut" IsLiveSite="false" />
</asp:Content>
