<%@ Page Language="C#" AutoEventWireup="true" Theme="default" Inherits="CMSSiteManager_Development_development"
 MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="Development description" CodeFile="development.aspx.cs" %>

<%@ Register Src="~/CMSAdminControls/UI/PageElements/guide.ascx" TagName="guide" TagPrefix="uc1" %>

<asp:Content ContentPlaceHolderID="plcContent" runat="server">
    <uc1:guide ID="Guide" runat="server" EnableViewState="false" />
</asp:Content>
