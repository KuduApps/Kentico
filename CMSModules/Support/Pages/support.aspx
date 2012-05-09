<%@ Page Language="C#" AutoEventWireup="true" Theme="default"
    Inherits="CMSModules_Support_Pages_support" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master"
    Title="Support description" CodeFile="support.aspx.cs" %>

<%@ Register Src="~/CMSAdminControls/UI/PageElements/guide.ascx" TagName="guide" TagPrefix="uc1" %>
<asp:Content ContentPlaceHolderID="plcContent" runat="server">
    <uc1:guide ID="Guide" runat="server" />
</asp:Content>
