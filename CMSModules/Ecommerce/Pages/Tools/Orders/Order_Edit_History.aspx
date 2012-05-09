<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMSModules_Ecommerce_Pages_Tools_Orders_Order_Edit_History" Theme="Default" MasterPageFile="~/CMSMasterPages/UI/SimplePage.master" Title="Order edit - History" CodeFile="Order_Edit_History.aspx.cs" %>

<%@ Register Src="~/CMSAdminControls/UI/UniGrid/UniGrid.ascx" TagName="UniGrid" TagPrefix="cms" %>
<asp:Content ID="cntBody" runat="server" ContentPlaceHolderID="plcContent">
    <div class="Orders">
        <cms:UniGrid runat="server" ID="gridElem" GridName="Order_Edit_History.xml" OrderBy="Date DESC"
            IsLiveSite="false" />
    </div>
</asp:Content>
