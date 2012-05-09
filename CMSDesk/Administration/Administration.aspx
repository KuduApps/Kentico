<%@ Page Language="C#" AutoEventWireup="true" Theme="default"
    Inherits="CMSDesk_Administration_Administration" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master"
    Title="CMSDesk - Administration page" CodeFile="Administration.aspx.cs" %>

<%@ Register Src="~/CMSAdminControls/UI/UIProfiles/UIGuide.ascx" TagName="UIGuide" TagPrefix="uc1" %>

<asp:Content ContentPlaceHolderID="plcContent" runat="server">
    <uc1:UIGuide ID="guide" runat="server" EnableViewState="false" ModuleName="CMS.Administration"
        ModuleAvailabilityForSiteRequired="true" />
</asp:Content>
