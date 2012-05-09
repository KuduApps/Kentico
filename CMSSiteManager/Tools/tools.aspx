<%@ Page Language="C#" AutoEventWireup="true" CodeFile="tools.aspx.cs" Theme="Default"
    Inherits="CMSSiteManager_Tools_tools" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" %>

<%@ Register Src="~/CMSAdminControls/UI/UIProfiles/UIGuide.ascx" TagName="UIGuide"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="plcContent" runat="server">
    <uc1:uiguide id="guide" runat="server" enableviewstate="false"
        moduleavailabilityforsiterequired="false" />
</asp:Content>
