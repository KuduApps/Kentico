<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Tab_Activities.aspx.cs" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master"
    Title="Contact properties - Activities" Inherits="CMSModules_ContactManagement_Pages_Tools_Contact_Tab_Activities"
    Theme="Default" %>

<%@ Register Src="~/CMSModules/ContactManagement/Controls/UI/Activity/List.ascx"
    TagName="ActivityList" TagPrefix="cms" %>
<%@ Register Src="~/CMSModules/ContactManagement/Controls/UI/Activity/Filter.ascx"
    TagName="Filter" TagPrefix="cms" %>
<asp:Content ID="cntBefore" runat="server" ContentPlaceHolderID="plcBeforeContent">
    <asp:Panel runat="server" ID="pnlDis" CssClass="PageHeaderLine" Visible="false">
        <cms:LocalizedLabel runat="server" ID="lblDis" EnableViewState="false" ResourceString="om.activity.disabled" />
    </asp:Panel>
</asp:Content>
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <cms:Filter runat="server" ID="fltElem" />
    <cms:ActivityList runat="server" ID="listElem" ZeroRowsText="om.contact.noactivities" />
</asp:Content>
