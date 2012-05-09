<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master"
    Inherits="CMSModules_Ecommerce_Pages_Tools_Customers_Customer_Edit_Credit_List"
    Theme="Default" Title="Customer credit events" CodeFile="Customer_Edit_Credit_List.aspx.cs" %>

<%@ Register Src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" TagName="UniGrid" TagPrefix="cms" %>
<asp:Content ContentPlaceHolderID="plcContent" ID="conten" runat="server">
    <asp:Label ID="lblError" runat="server" Visible="false" EnableViewState="false" CssClass="ErrorLabel" />
    <asp:Label ID="lblGlobalInfo" runat="server" Visible="false" EnableViewState="false" Font-Bold="true" CssClass="InfoLabel" />
    <asp:Panel runat="server" ID="pnlCredit">
        <div style="padding-bottom: 5px;">
            <asp:Label ID="lblCredit" runat="server" EnableViewState="false" />
            <asp:Label ID="lblCreditValue" runat="server" EnableViewState="false" />
        </div>
        <cms:UniGrid runat="server" ID="UniGrid" GridName="Customer_Edit_Credit_List.xml"
            IsLiveSite="false" Columns="EventID,EventDate,EventName,EventCreditChange,EventDescription" />
    </asp:Panel>
</asp:Content>
