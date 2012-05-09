<%@ Page Language="C#" AutoEventWireup="true" Theme="default" Inherits="CMSModules_MyDesk_MyDesk"
 MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="My Desk Description" CodeFile="MyDesk.aspx.cs" %>

<%@ Register Src="~/CMSAdminControls/UI/UIProfiles/UIGuide.ascx" TagName="UIGuide" TagPrefix="uc1" %>

<asp:Content ContentPlaceHolderID="plcContent" runat="server">
    <uc1:UIGuide ID="guide" runat="server" EnableViewState="false" ModuleName="CMS.MyDesk"
        ModuleAvailabilityForSiteRequired="false" />
</asp:Content>
