<%@ Page Language="C#" AutoEventWireup="true" CodeFile="List.aspx.cs"
    MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="Activity type list"
    Inherits="CMSModules_ContactManagement_Pages_Tools_Activities_ActivityType_List" Theme="Default" %>
    
<%@ Register Src="~/CMSModules/ContactManagement/Controls/UI/ActivityType/List.ascx" TagName="ActivityTypeList" TagPrefix="cms" %>
<asp:Content ID="cntBefore" runat="server" ContentPlaceHolderID="plcBeforeContent">
    <asp:Panel runat="server" ID="pnlDis" CssClass="PageHeaderLine" Visible="false">
        <cms:LocalizedLabel runat="server" ID="lblDis" EnableViewState="false" ResourceString="om.activity.disabled" />
    </asp:Panel>
</asp:Content>      
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <cms:ActivityTypeList ID="listElem" runat="server" IsLiveSite="false" />
</asp:Content>