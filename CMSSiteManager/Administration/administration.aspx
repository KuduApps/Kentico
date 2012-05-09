<%@ Page Language="C#" AutoEventWireup="true" Theme="default"
    Inherits="CMSSiteManager_Administration_administration" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master"
    Title="Administration page" CodeFile="administration.aspx.cs" %>

<%@ Register Src="~/CMSAdminControls/UI/UIProfiles/UIGuide.ascx" TagName="UIGuide" TagPrefix="uc1" %>

<asp:Content ContentPlaceHolderID="plcContent" runat="server">
    <uc1:UIGuide ID="guide" runat="server" EnableViewState="false" ModuleName="CMS.Administration"
        ModuleAvailabilityForSiteRequired="false" />
</asp:Content>
