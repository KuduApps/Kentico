<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Countries_Pages_Development_Country_New"
    Theme="Default" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="New country"
    CodeFile="New.aspx.cs" %>

<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <cms:UIForm runat="server" ID="form" ObjectType="cms.country" RedirectUrlAfterCreate="Frameset.aspx?countryid={%EditedObject.ID%}&saved=1" />
</asp:Content>
